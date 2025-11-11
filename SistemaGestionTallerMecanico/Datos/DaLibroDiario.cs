using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Utiles;

namespace Datos
{
    public class DaLibroDiario
    {
        private readonly string connectionString;

        public DaLibroDiario()
        {
            this.connectionString = ConfigurationManager.AppSettings.Get("Connection");//ConfigHelper.ObtenerConnectionString();
        }

        // =====================================================
        // LISTAR MOVIMIENTOS POR CAJA (con filtro opcional)
        // =====================================================

        public async Task<List<LibroDiario>> ListarPorCajaAsync(int idCajaDiaria, string filtroNombre = null)
        {
            try
            {
                var movimientos = new List<LibroDiario>();

                string query = @"
                    SELECT 
                        ld.Id, 
                        ld.CodMovimiento, 
                        ld.IdCajaDiaria, 
                        ld.Fecha, 
                        ld.TipoMovimiento, 
                        ld.MetodoPago,
                        ld.Concepto, 
                        ld.Monto, 
                        ld.CodCliente, 
                        ld.CodProveedor, 
                        ld.CodOrdenTrabajo,
                        ld.Activo, 
                        ld.FechaBaja, 
                        ld.FechaModificacion,
                        ISNULL(ISNULL(
                            c.Nombre + ' ' + c.Apellido, 
                            ISNULL(p.RazonSocial, p.Nombre + ' ' + ISNULL(p.Apellido, ''))
                        ), 'Mov. interno') AS EntidadRelacionada
                    FROM LibroDiario ld
                    LEFT JOIN Clientes c ON ld.CodCliente = c.CodCliente
                    LEFT JOIN Proveedores p ON ld.CodProveedor = p.CodProveedor
                    WHERE ld.IdCajaDiaria = @IdCajaDiaria
                      AND ld.Activo = 1";

                // Filtro opcional por nombre
                if (!string.IsNullOrEmpty(filtroNombre))
                {
                    query += @" AND (
                        c.Nombre LIKE @Filtro OR 
                        c.Apellido LIKE @Filtro OR
                        p.RazonSocial LIKE @Filtro OR
                        p.Nombre LIKE @Filtro OR
                        p.Apellido LIKE @Filtro
                    )";
                }

                query += " ORDER BY ld.Fecha DESC";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCajaDiaria", idCajaDiaria);

                    if (!string.IsNullOrEmpty(filtroNombre))
                    {
                        cmd.Parameters.AddWithValue("@Filtro", $"%{filtroNombre}%");
                    }

                    await conn.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            movimientos.Add(new LibroDiario
                            {
                                Id = reader.GetInt32(0),
                                CodMovimiento = reader.GetString(1),
                                IdCajaDiaria = reader.GetInt32(2),
                                Fecha = reader.GetDateTime(3),
                                TipoMovimiento = reader.GetString(4),
                                MetodoPago = reader.GetString(5),
                                Concepto = reader.GetString(6),
                                Monto = reader.GetDecimal(7),
                                CodCliente = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CodProveedor = reader.IsDBNull(9) ? null : reader.GetString(9),
                                CodOrdenTrabajo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Activo = reader.GetBoolean(11),
                                FechaBaja = reader.IsDBNull(12) ? null : (DateTime?)reader.GetDateTime(12),
                                FechaModificacion = reader.IsDBNull(13) ? null : (DateTime?)reader.GetDateTime(13),
                                EntidadRelacionada = reader.IsDBNull(14) ? null : reader.GetString(14)
                            });
                        }
                    }
                }

                return movimientos;
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "ListarPorCajaAsync", $"IdCaja: {idCajaDiaria}, Filtro: {filtroNombre}");
                throw new Exception($"Error al listar movimientos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "ListarPorCajaAsync");
                throw;
            }
        }

        // =====================================================
        // OBTENER MOVIMIENTO POR CÓDIGO
        // =====================================================

        public async Task<LibroDiario> ObtenerPorCodigoAsync(string codMovimiento)
        {
            try
            {
                string query = @"
                    SELECT 
                        Id, CodMovimiento, IdCajaDiaria, Fecha, TipoMovimiento, MetodoPago,
                        Concepto, Monto, CodCliente, CodProveedor, CodOrdenTrabajo,
                        Activo, FechaBaja, FechaModificacion
                    FROM LibroDiario
                    WHERE CodMovimiento = @CodMov";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodMov", codMovimiento);

                    await conn.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new LibroDiario
                            {
                                Id = reader.GetInt32(0),
                                CodMovimiento = reader.GetString(1),
                                IdCajaDiaria = reader.GetInt32(2),
                                Fecha = reader.GetDateTime(3),
                                TipoMovimiento = reader.GetString(4),
                                MetodoPago = reader.GetString(5),
                                Concepto = reader.GetString(6),
                                Monto = reader.GetDecimal(7),
                                CodCliente = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CodProveedor = reader.IsDBNull(9) ? null : reader.GetString(9),
                                CodOrdenTrabajo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Activo = reader.GetBoolean(11),
                                FechaBaja = reader.IsDBNull(12) ? null : (DateTime?)reader.GetDateTime(12),
                                FechaModificacion = reader.IsDBNull(13) ? null : (DateTime?)reader.GetDateTime(13)
                            };
                        }
                    }
                }

                return null;
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "ObtenerPorCodigoAsync", $"CodMov: {codMovimiento}");
                throw new Exception($"Error al obtener movimiento: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "ObtenerPorCodigoAsync");
                throw;
            }
        }

        // =====================================================
        // INSERTAR MOVIMIENTO
        // =====================================================

        public async Task<string> InsertarAsync(LibroDiario movimiento)
        {
            try
            {
                // Generar código automáticamente
                string codMovimiento = await GenerarCodigoAsync();

                string query = @"
                    INSERT INTO LibroDiario 
                    (CodMovimiento, IdCajaDiaria, Fecha, TipoMovimiento, MetodoPago, 
                     Concepto, Monto, CodCliente, CodProveedor, CodOrdenTrabajo, Activo)
                    VALUES 
                    (@CodMov, @IdCaja, GETDATE(), @Tipo, @Metodo, 
                     @Concepto, @Monto, @CodCliente, @CodProveedor, @CodOrden, 1)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodMov", codMovimiento);
                    cmd.Parameters.AddWithValue("@IdCaja", movimiento.IdCajaDiaria);
                    cmd.Parameters.AddWithValue("@Tipo", movimiento.TipoMovimiento);
                    cmd.Parameters.AddWithValue("@Metodo", movimiento.MetodoPago);
                    cmd.Parameters.AddWithValue("@Concepto", movimiento.Concepto);
                    cmd.Parameters.AddWithValue("@Monto", movimiento.Monto);
                    cmd.Parameters.AddWithValue("@CodCliente", (object)movimiento.CodCliente ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CodProveedor", (object)movimiento.CodProveedor ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CodOrden", (object)movimiento.CodOrdenTrabajo ?? DBNull.Value);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }

                LogHelper.GuardarInfo(
                    $"Movimiento creado: {codMovimiento}, Tipo={movimiento.TipoMovimiento}, Metodo={movimiento.MetodoPago}, Monto={movimiento.Monto:C}",
                    "DaLibroDiario",
                    "InsertarAsync"
                );

                return codMovimiento;
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "InsertarAsync", $"Tipo: {movimiento.TipoMovimiento}, Monto: {movimiento.Monto}");
                throw new Exception($"Error al insertar movimiento: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "InsertarAsync");
                throw;
            }
        }

        // =====================================================
        // ACTUALIZAR MOVIMIENTO
        // =====================================================

        public async Task<bool> ActualizarAsync(LibroDiario movimiento)
        {
            try
            {
                string query = @"
                    UPDATE LibroDiario
                    SET Concepto = @Concepto,
                        Monto = @Monto,
                        MetodoPago = @Metodo,
                        FechaModificacion = GETDATE()
                    WHERE CodMovimiento = @CodMov
                      AND Activo = 1";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodMov", movimiento.CodMovimiento);
                    cmd.Parameters.AddWithValue("@Concepto", movimiento.Concepto);
                    cmd.Parameters.AddWithValue("@Monto", movimiento.Monto);
                    cmd.Parameters.AddWithValue("@Metodo", movimiento.MetodoPago);

                    await conn.OpenAsync();
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                    if (filasAfectadas > 0)
                    {
                        LogHelper.GuardarInfo(
                            $"Movimiento actualizado: {movimiento.CodMovimiento}",
                            "DaLibroDiario",
                            "ActualizarAsync"
                        );
                    }

                    return filasAfectadas > 0;
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "ActualizarAsync", $"CodMov: {movimiento.CodMovimiento}");
                throw new Exception($"Error al actualizar movimiento: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "ActualizarAsync");
                throw;
            }
        }

        // =====================================================
        // ELIMINAR MOVIMIENTO (borrado lógico)
        // =====================================================

        public async Task<bool> EliminarAsync(string codMovimiento)
        {
            try
            {
                string query = @"
                    UPDATE LibroDiario
                    SET Activo = 0, 
                        FechaBaja = GETDATE()
                    WHERE CodMovimiento = @CodMov 
                      AND Activo = 1";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodMov", codMovimiento);

                    await conn.OpenAsync();
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                    if (filasAfectadas > 0)
                    {
                        LogHelper.GuardarInfo(
                            $"Movimiento eliminado: {codMovimiento}",
                            "DaLibroDiario",
                            "EliminarAsync"
                        );
                    }

                    return filasAfectadas > 0;
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "EliminarAsync", $"CodMov: {codMovimiento}");
                throw new Exception($"Error al eliminar movimiento: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "EliminarAsync");
                throw;
            }
        }

        // =====================================================
        // OBTENER MÉTODO DE PAGO DE UN MOVIMIENTO
        // (necesario para saber si afecta la caja al eliminar)
        // =====================================================

        public async Task<string> ObtenerMetodoPagoAsync(string codMovimiento)
        {
            try
            {
                string query = @"
                    SELECT MetodoPago 
                    FROM LibroDiario 
                    WHERE CodMovimiento = @CodMov 
                      AND Activo = 1";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodMov", codMovimiento);

                    await conn.OpenAsync();
                    object result = await cmd.ExecuteScalarAsync();

                    return result?.ToString();
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "ObtenerMetodoPagoAsync", $"CodMov: {codMovimiento}");
                throw new Exception($"Error al obtener método de pago: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "ObtenerMetodoPagoAsync");
                throw;
            }
        }

        // =====================================================
        // GENERAR CÓDIGO DE MOVIMIENTO (LD00001, LD00002, etc.)
        // =====================================================

        private async Task<string> GenerarCodigoAsync()
        {
            try
            {
                string query = @"
                    SELECT ISNULL(MAX(CAST(SUBSTRING(CodMovimiento, 3, 8) AS INT)), 0) 
                    FROM LibroDiario";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    await conn.OpenAsync();

                    int maxCodigo = (int)await cmd.ExecuteScalarAsync();
                    int siguienteCodigo = maxCodigo + 1;

                    return "LD" + siguienteCodigo.ToString("D5"); // LD00001, LD00002, etc.
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "GenerarCodigoAsync");
                throw new Exception($"Error al generar código: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "DaLibroDiario", "GenerarCodigoAsync");
                throw;
            }
        }
    }
}
