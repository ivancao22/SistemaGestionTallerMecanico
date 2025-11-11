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
    public class DaVehiculo
    {

        private string connectionString = ConfigurationManager.AppSettings.Get("Connection");

        public string ObtenerSiguienteCodigo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT ISNULL(MAX(CAST(SUBSTRING(CodVehiculo, 3, 5) AS INT)), 0) 
                                    FROM Vehiculos 
                                    WHERE Activo = @Activo";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Activo", true);
                    conn.Open();

                    int maxCodigo = (int)cmd.ExecuteScalar();
                    int siguienteCodigo = maxCodigo + 1;

                    if (siguienteCodigo > 99999)
                    {
                        LogHelper.GuardarError(
                            new Exception("Se alcanzó el límite máximo de vehículos (VH99999)"),
                            "DaVehiculos",
                            "ObtenerSiguienteCodigo"
                        );
                        return null;
                    }

                    return "VH" + siguienteCodigo.ToString("D5");
                }
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaVehiculos", "ObtenerSiguienteCodigo");
                return null;
            }
        }

        public DataTable CargarVehiculos()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                        v.CodVehiculo,
                                        (c.Nombre + ' ' + c.Apellido) AS cliente,
                                        v.Modelo AS modelo,
                                        v.Patente AS patente,
                                        ISNULL(CAST(v.Kilometraje AS VARCHAR), '') AS kilometraje
                                     FROM Vehiculos v
                                     INNER JOIN Clientes c ON v.CodCliente = c.CodCliente
                                     WHERE v.Activo = 1
                                     ORDER BY c.CodCliente, v.CodVehiculo DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaVehiculos", "CargarVehiculos");
                return new DataTable();
            }
        }

        public Vehiculo ObtenerVehiculoPorCodigo(string codVehiculo)
        {
            try
            {
                Vehiculo vehiculo = null;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT v.*, (c.Nombre + ' ' + c.Apellido) AS NombreCliente
                                    FROM Vehiculos v
                                    INNER JOIN Clientes c ON v.CodCliente = c.CodCliente
                                    WHERE v.CodVehiculo = @CodVehiculo AND v.Activo = 1";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodVehiculo", codVehiculo);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        vehiculo = new Vehiculo
                        {
                            CodVehiculo = reader["CodVehiculo"].ToString(),
                            CodCliente = reader["CodCliente"].ToString(),
                            Modelo = reader["Modelo"].ToString(),
                            Patente = reader["Patente"].ToString(),
                            Kilometraje = reader["Kilometraje"] != DBNull.Value ? Convert.ToInt32(reader["Kilometraje"]) : (int?)null,
                            FechaAlta = Convert.ToDateTime(reader["FechaAlta"]),
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
                    }
                }
                return vehiculo;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaVehiculos", "ObtenerVehiculoPorCodigo");
                return null;
            }
        }

        public bool InsertarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Vehiculos 
                                    (CodVehiculo, CodCliente, Modelo, Patente, Kilometraje, FechaAlta, Activo) 
                                     VALUES 
                                    (@CodVehiculo, @CodCliente, @Modelo, @Patente, @Kilometraje, GETDATE(), 1)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodVehiculo", vehiculo.CodVehiculo);
                    cmd.Parameters.AddWithValue("@CodCliente", vehiculo.CodCliente);
                    cmd.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                    cmd.Parameters.AddWithValue("@Patente", vehiculo.Patente.ToUpper());
                    cmd.Parameters.AddWithValue("@Kilometraje", vehiculo.Kilometraje.HasValue ? (object)vehiculo.Kilometraje.Value : DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaVehiculos", "InsertarVehiculo");
                return false;
            }
        }

        public bool ActualizarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Vehiculos 
                                     SET CodCliente = @CodCliente,
                                         Modelo = @Modelo, 
                                         Patente = @Patente, 
                                         Kilometraje = @Kilometraje 
                                     WHERE CodVehiculo = @CodVehiculo";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodVehiculo", vehiculo.CodVehiculo);
                    cmd.Parameters.AddWithValue("@CodCliente", vehiculo.CodCliente);
                    cmd.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                    cmd.Parameters.AddWithValue("@Patente", vehiculo.Patente.ToUpper());
                    cmd.Parameters.AddWithValue("@Kilometraje", vehiculo.Kilometraje.HasValue ? (object)vehiculo.Kilometraje.Value : DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaVehiculos", "ActualizarVehiculo");
                return false;
            }
        }

        public bool BorrarVehiculo(string codVehiculo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Vehiculos SET Activo = 0 WHERE CodVehiculo = @CodVehiculo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodVehiculo", codVehiculo);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaVehiculos", "BorrarVehiculo");
                return false;
            }
        }

        public DataTable CargarClientesActivos()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                        CodCliente, 
                                        (Nombre + ' ' + Apellido) AS NombreCompleto
                                     FROM Clientes 
                                     WHERE Activo = 1
                                     ORDER BY Nombre, Apellido";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaVehiculos", "CargarClientesActivos");
                return new DataTable();
            }
        }

    }
}
