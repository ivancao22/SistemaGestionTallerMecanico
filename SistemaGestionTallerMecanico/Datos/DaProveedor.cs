using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Utiles;

namespace Datos
{
    public class DaProveedor
    {
        private string connectionString = ConfigurationManager.AppSettings.Get("Connection");

        // Obtener el siguiente código disponible (formato: PR00001)
        public string ObtenerSiguienteCodigo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT ISNULL(MAX(CAST(SUBSTRING(CodProveedor, 3, 5) AS INT)), 0) 
                                    FROM Proveedores ";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Activo", true);
                    conn.Open();

                    int maxCodigo = (int)cmd.ExecuteScalar();
                    int siguienteCodigo = maxCodigo + 1;

                    if (siguienteCodigo > 99999)
                    {
                        LogHelper.GuardarError(
                            new Exception("Se alcanzó el límite máximo de proveedores (PR99999)"),
                            "DaProveedores",
                            "ObtenerSiguienteCodigo"
                        );
                        return null;
                    }

                    return "PR" + siguienteCodigo.ToString("D5");
                }
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaProveedores", "ObtenerSiguienteCodigo");
                return null;
            }
        }

        // Cargar todos los proveedores activos
        public DataTable CargarProveedores()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                        CodProveedor,
                                        LTRIM(RTRIM(ISNULL(Nombre, '') + ' ' + ISNULL(Apellido, ''))) AS nombre,
                                        ISNULL(RazonSocial, '') AS razonSocial,
                                        ISNULL(CUIT, '') AS cuit,
                                        ISNULL(Email, '') AS email,
                                        ISNULL(Telefono, '') AS telefono
                                     FROM Proveedores 
                                     WHERE Activo = 1
                                     ORDER BY CodProveedor DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaProveedores", "CargarProveedores");
                return new DataTable();
            }
        }

        // Obtener un proveedor por código
        public Proveedor ObtenerProveedorPorCodigo(string codProveedor)
        {
            try
            {
                Proveedor proveedor = null;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Proveedores WHERE CodProveedor = @CodProveedor AND Activo = 1";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodProveedor", codProveedor);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        proveedor = new Proveedor
                        {
                            CodProveedor = reader["CodProveedor"].ToString(),
                            Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : "",
                            Apellido = reader["Apellido"] != DBNull.Value ? reader["Apellido"].ToString() : "",
                            RazonSocial = reader["RazonSocial"] != DBNull.Value ? reader["RazonSocial"].ToString() : "",
                            CUIT = reader["CUIT"] != DBNull.Value ? reader["CUIT"].ToString() : "",
                            Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : "",
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
                    }
                }
                return proveedor;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaProveedores", "ObtenerProveedorPorCodigo");
                return null;
            }
        }

        // Insertar un nuevo proveedor
        public bool InsertarProveedor(Proveedor proveedor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Proveedores 
                                    (CodProveedor, Nombre, Apellido, RazonSocial, CUIT, Telefono, Email, Activo) 
                                     VALUES 
                                    (@CodProveedor, @Nombre, @Apellido, @RazonSocial, @CUIT, @Telefono, @Email, 1)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodProveedor", proveedor.CodProveedor);
                    cmd.Parameters.AddWithValue("@Nombre", string.IsNullOrEmpty(proveedor.Nombre) ? (object)DBNull.Value : proveedor.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", string.IsNullOrEmpty(proveedor.Apellido) ? (object)DBNull.Value : proveedor.Apellido);
                    cmd.Parameters.AddWithValue("@RazonSocial", string.IsNullOrEmpty(proveedor.RazonSocial) ? (object)DBNull.Value : proveedor.RazonSocial);
                    cmd.Parameters.AddWithValue("@CUIT", string.IsNullOrEmpty(proveedor.CUIT) ? (object)DBNull.Value : proveedor.CUIT);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(proveedor.Telefono) ? (object)DBNull.Value : proveedor.Telefono);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(proveedor.Email) ? (object)DBNull.Value : proveedor.Email);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaProveedores", "InsertarProveedor");
                return false;
            }
        }

        // Actualizar un proveedor existente
        public bool ActualizarProveedor(Proveedor proveedor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Proveedores 
                                     SET Nombre = @Nombre, 
                                         Apellido = @Apellido, 
                                         RazonSocial = @RazonSocial,
                                         CUIT = @CUIT,
                                         Telefono = @Telefono, 
                                         Email = @Email 
                                     WHERE CodProveedor = @CodProveedor";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodProveedor", proveedor.CodProveedor);
                    cmd.Parameters.AddWithValue("@Nombre", string.IsNullOrEmpty(proveedor.Nombre) ? (object)DBNull.Value : proveedor.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", string.IsNullOrEmpty(proveedor.Apellido) ? (object)DBNull.Value : proveedor.Apellido);
                    cmd.Parameters.AddWithValue("@RazonSocial", string.IsNullOrEmpty(proveedor.RazonSocial) ? (object)DBNull.Value : proveedor.RazonSocial);
                    cmd.Parameters.AddWithValue("@CUIT", string.IsNullOrEmpty(proveedor.CUIT) ? (object)DBNull.Value : proveedor.CUIT);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(proveedor.Telefono) ? (object)DBNull.Value : proveedor.Telefono);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(proveedor.Email) ? (object)DBNull.Value : proveedor.Email);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaProveedores", "ActualizarProveedor");
                return false;
            }
        }

        // Borrar proveedor (borrado lógico)
        public bool BorrarProveedor(string codProveedor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Proveedores SET Activo = 0 WHERE CodProveedor = @CodProveedor";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodProveedor", codProveedor);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaProveedores", "BorrarProveedor");
                return false;
            }
        }
    }
}
