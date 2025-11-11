using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Entidades;
using Utiles;

namespace Datos
{
    public class DaCajaDiaria
    {
        private readonly string connectionString;

        public DaCajaDiaria()
        {
            this.connectionString = ConfigurationManager.AppSettings.Get("Connection");//ConfigHelper.ObtenerConnectionString();
        }

        public async Task<CajaDiaria> ObtenerPorFechaAsync(DateTime fecha)
        {
            try
            {
                string query = @"
                    SELECT 
                        Id, 
                        Fecha, 
                        SaldoInicial, 
                        SaldoFinal, 
                        Usuario, 
                        FechaHoraApertura
                    FROM CajaDiaria
                    WHERE Fecha = @Fecha";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                    await conn.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new CajaDiaria
                            {
                                Id = reader.GetInt32(0),
                                Fecha = reader.GetDateTime(1),
                                SaldoInicial = reader.GetDecimal(2),
                                SaldoFinal = reader.GetDecimal(3),
                                Usuario = reader.IsDBNull(4) ? null : reader.GetString(4),
                                FechaHoraApertura = reader.GetDateTime(5)
                            };
                        }
                    }
                }

                return null;
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ObtenerPorFechaAsync", $"Fecha: {fecha:yyyy-MM-dd}");
                throw new Exception($"Error al obtener caja del día {fecha:dd/MM/yyyy}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ObtenerPorFechaAsync");
                throw;
            }
        }

        // =====================================================
        // OBTENER SALDO FINAL DEL DÍA ANTERIOR
        // =====================================================

        public async Task<decimal?> ObtenerSaldoFinalDiaAnteriorAsync(DateTime fecha)
        {
            try
            {
                DateTime ayer = fecha.AddDays(-1).Date;

                string query = @"
                    SELECT SaldoFinal 
                    FROM CajaDiaria 
                    WHERE Fecha = @Fecha";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Fecha", ayer);

                    await conn.OpenAsync();
                    object result = await cmd.ExecuteScalarAsync();

                    return result != null ? (decimal?)Convert.ToDecimal(result) : null;
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ObtenerSaldoFinalDiaAnteriorAsync", $"Fecha: {fecha:yyyy-MM-dd}");
                throw new Exception($"Error al obtener saldo del día anterior: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ObtenerSaldoFinalDiaAnteriorAsync");
                throw;
            }
        }

        // =====================================================
        // INSERTAR NUEVA CAJA
        // =====================================================

        public async Task<int> InsertarAsync(CajaDiaria caja)
        {
            try
            {
                string query = @"
                    INSERT INTO CajaDiaria 
                    (Fecha, SaldoInicial, SaldoFinal, Usuario, FechaHoraApertura) 
                    OUTPUT INSERTED.Id
                    VALUES 
                    (@Fecha, @SaldoInicial, @SaldoInicial, @Usuario, GETDATE())";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Fecha", caja.Fecha.Date);
                    cmd.Parameters.AddWithValue("@SaldoInicial", caja.SaldoInicial);
                    cmd.Parameters.AddWithValue("@Usuario", caja.Usuario ?? (object)DBNull.Value);

                    await conn.OpenAsync();
                    int idGenerado = (int)await cmd.ExecuteScalarAsync();

                    LogHelper.GuardarInfo(
                        $"Caja creada: Fecha={caja.Fecha:dd/MM/yyyy}, SaldoInicial={caja.SaldoInicial:C}, Usuario={caja.Usuario}",
                        "DaCajaDiaria",
                        "InsertarAsync"
                    );

                    return idGenerado;
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "InsertarAsync", $"Fecha: {caja.Fecha:yyyy-MM-dd}, SaldoInicial: {caja.SaldoInicial}");

                // Verificar error de UNIQUE constraint
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    throw new Exception($"Ya existe una caja para el día {caja.Fecha:dd/MM/yyyy}", ex);
                }

                throw new Exception($"Error al crear la caja: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "InsertarAsync");
                throw;
            }
        }

        // =====================================================
        // MODIFICAR SALDO INICIAL (solo si no hay movimientos)
        // =====================================================

        public async Task<bool> ModificarSaldoInicialAsync(int idCajaDiaria, decimal nuevoSaldoInicial)
        {
            try
            {
                string query = @"
                    UPDATE CajaDiaria
                    SET SaldoInicial = @NuevoSaldo,
                        SaldoFinal = @NuevoSaldo
                    WHERE Id = @IdCajaDiaria";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCajaDiaria", idCajaDiaria);
                    cmd.Parameters.AddWithValue("@NuevoSaldo", nuevoSaldoInicial);

                    await conn.OpenAsync();
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                    if (filasAfectadas > 0)
                    {
                        LogHelper.GuardarInfo(
                            $"Saldo inicial modificado: IdCaja={idCajaDiaria}, NuevoSaldo={nuevoSaldoInicial:C}",
                            "DaCajaDiaria",
                            "ModificarSaldoInicialAsync"
                        );
                    }

                    return filasAfectadas > 0;
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ModificarSaldoInicialAsync", $"IdCaja: {idCajaDiaria}, NuevoSaldo: {nuevoSaldoInicial}");
                throw new Exception($"Error al modificar el saldo inicial: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ModificarSaldoInicialAsync");
                throw;
            }
        }

        // =====================================================
        // EJECUTAR SP: Actualizar saldo final
        // =====================================================

        public async Task<decimal> ActualizarSaldoFinalAsync(int idCajaDiaria)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_ActualizarSaldoCaja", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCajaDiaria", idCajaDiaria);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            decimal saldoFinal = reader.GetDecimal(0); // SaldoFinal

                            LogHelper.GuardarInfo(
                                $"Saldo actualizado: IdCaja={idCajaDiaria}, SaldoFinal={saldoFinal:C}",
                                "DaCajaDiaria",
                                "ActualizarSaldoFinalAsync"
                            );

                            return saldoFinal;
                        }
                    }
                }

                return 0;
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ActualizarSaldoFinalAsync", $"IdCaja: {idCajaDiaria}");
                throw new Exception($"Error al actualizar el saldo final: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ActualizarSaldoFinalAsync");
                throw;
            }
        }

        // =====================================================
        // CONTAR MOVIMIENTOS DE UNA CAJA
        // (necesario para validar si se puede modificar saldo inicial)
        // =====================================================

        public async Task<int> ContarMovimientosAsync(int idCajaDiaria)
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM LibroDiario 
                    WHERE IdCajaDiaria = @IdCajaDiaria 
                      AND Activo = 1";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCajaDiaria", idCajaDiaria);

                    await conn.OpenAsync();
                    return (int)await cmd.ExecuteScalarAsync();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ContarMovimientosAsync", $"IdCaja: {idCajaDiaria}");
                throw new Exception($"Error al contar movimientos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ContarMovimientosAsync");
                throw;
            }
        }

        // =====================================================
        // VERIFICAR SI EXISTE CAJA EN UNA FECHA
        // =====================================================

        public async Task<bool> ExisteCajaEnFechaAsync(DateTime fecha)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM CajaDiaria WHERE Fecha = @Fecha";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                    await conn.OpenAsync();
                    int count = (int)await cmd.ExecuteScalarAsync();

                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ExisteCajaEnFechaAsync", $"Fecha: {fecha:yyyy-MM-dd}");
                throw new Exception($"Error al verificar existencia de caja: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaCajaDiaria", "ExisteCajaEnFechaAsync");
                throw;
            }
        }
    }
}
