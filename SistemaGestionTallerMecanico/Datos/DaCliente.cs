using System;
using System.Data.SqlClient;
using System.Data;
using Entidades;
using System.Configuration;
using Utiles;


namespace Datos
{
    public class DaCliente
    {
        private string connectionString = ConfigurationManager.AppSettings.Get("Connection");

        // Obtener el siguiente código disponible (formato: 00001, 00002, etc.)
        public string ObtenerSiguienteCodigo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // SUBSTRING(CodCliente, 3, 5) extrae los 5 dígitos después de 'CL'
                    // Ejemplo: 'CL00001' -> extrae '00001' -> convierte a INT -> 1
                    string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(CodCliente, 3, 5) AS INT)), 0)  FROM Clientes";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Activo", true);
                    conn.Open();

                    int maxCodigo = (int)cmd.ExecuteScalar();
                    int siguienteCodigo = maxCodigo + 1;

                    // Validar que no supere el máximo (99999)
                    if (siguienteCodigo > 99999)
                    {
                        LogHelper.GuardarError(
                            new Exception("Se alcanzó el límite máximo de clientes (CL99999)"),
                            "DaClientes",
                            "ObtenerSiguienteCodigo"
                        );
                        return null;
                    }

                    // Si maxCodigo es 0, significa que no hay clientes, entonces devolver 00001
                    // Si maxCodigo > 0, incrementar y devolver el siguiente
                    return "CL" + siguienteCodigo.ToString("D5");
                }
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaClientes", "ObtenerSiguienteCodigo");
                return null;  // Indicar que hubo un error (no asumir que es el primero)
            }
        }

        // Cargar todos los clientes habilitados (para la grilla)
        public DataTable CargarClientes()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT CodCliente, 
                                            (Nombre + ' ' + Apellido) as nombre, 
                                            ISNULL(Telefono, '') as telefono,
                                            ISNULL(Email, '') as email 
                                     FROM Clientes 
                                     WHERE activo = @Activo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Activo", true);
                    conn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaClientes", "CargarClientes");
                return new DataTable();
            }
        }

        // Obtener un cliente por su código (para editar)
        public Cliente ObtenerClientePorCodigo(string codCliente)
        {
            try
            {
                Cliente cliente = null;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Clientes WHERE CodCliente = @CodCliente AND activo = @Activo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodCliente", codCliente);
                    cmd.Parameters.AddWithValue("@Activo", true);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            CodCliente = reader["CodCliente"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : "",
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                            Activo = Convert.ToBoolean(reader["activo"])
                        };
                    }
                }
                return cliente;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaClientes", "ObtenerClientePorCodigo");
                return null;
            }
        }

        // Insertar un nuevo cliente
        public bool InsertarCliente(Cliente cliente)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Clientes (CodCliente, Nombre, Apellido, Telefono, Email, activo) 
                                     VALUES (@CodCliente, @Nombre, @Apellido, @Telefono, @Email, @Activo)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodCliente", cliente.CodCliente);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(cliente.Telefono) ? (object)DBNull.Value : cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(cliente.Email) ? (object)DBNull.Value : cliente.Email);
                    cmd.Parameters.AddWithValue("@Activo", true);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaClientes", "InsertarCliente");
                return false;
            }
        }

        // Actualizar un cliente existente
        public bool ActualizarCliente(Cliente cliente)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Clientes 
                                     SET Nombre = @Nombre, 
                                         Apellido = @Apellido, 
                                         Telefono = @Telefono, 
                                         Email = @Email 
                                     WHERE CodCliente = @CodCliente";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodCliente", cliente.CodCliente);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(cliente.Telefono) ? (object)DBNull.Value : cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(cliente.Email) ? (object)DBNull.Value : cliente.Email);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaClientes", "ActualizarCliente");
                return false;
            }
        }

        // Borrar cliente (borrado lógico del cliente y sus vehículos)
        public bool BorrarCliente(string codCliente)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Clientes SET activo = @Activo WHERE CodCliente = @CodCliente;
                                     UPDATE Vehiculos SET activo = @Activo WHERE CodCliente = @CodCliente;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Activo", false);
                    cmd.Parameters.AddWithValue("@CodCliente", codCliente);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaClientes", "BorrarCliente");
                return false;
            }
        }
    }
}
