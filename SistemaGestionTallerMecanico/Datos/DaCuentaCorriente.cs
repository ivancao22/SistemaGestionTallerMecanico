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
    public class DaCuentaCorriente
    {
        private readonly string connectionString;

        public DaCuentaCorriente()
        {
            this.connectionString = ConfigurationManager.AppSettings.Get("Connection");//ConfigHelper.ObtenerConnectionString();
        }

        /// <summary>
        /// Lista movimientos para una entidad (Cliente o Proveedor).
        /// Si 'desde'/'hasta' son null no filtra por fecha.
        /// Devuelve lista ordenada por Fecha ascendente.
        /// </summary>
        public async Task<List<LibroDiario>> ListarPorEntidadAsync(string tipoEntidad, string codigo, DateTime? desde = null, DateTime? hasta = null)
        {
            var resultado = new List<LibroDiario>();
            if (string.IsNullOrWhiteSpace(tipoEntidad)) throw new ArgumentNullException(nameof(tipoEntidad));
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentNullException(nameof(codigo));

            string whereCol = tipoEntidad.Equals("Cliente", StringComparison.OrdinalIgnoreCase) ? "CodCliente" : "CodProveedor";

            string sql = $@"
                SELECT 
                    CodMovimiento,
                    IdCajaDiaria,
                    Fecha,
                    TipoMovimiento,
                    MetodoPago,
                    Concepto,
                    Monto,
                    CodCliente,
                    CodProveedor,
                    CodOrdenTrabajo,
                    ISNULL(Signo, CASE WHEN TipoMovimiento = 'INGRESO' THEN 1 ELSE -1 END) AS Signo
                FROM LibroDiario
                WHERE {whereCol} = @Codigo
                  AND ISNULL(Activo, 1) = 1
            ";

            if (desde.HasValue) sql += " AND Fecha >= @Desde";
            if (hasta.HasValue) sql += " AND Fecha <= @Hasta";

            sql += " ORDER BY Fecha;";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    if (desde.HasValue) cmd.Parameters.AddWithValue("@Desde", desde.Value);
                    if (hasta.HasValue) cmd.Parameters.AddWithValue("@Hasta", hasta.Value);

                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var mov = new LibroDiario
                            {
                                CodMovimiento = reader.IsDBNull(0) ? null : reader.GetString(0),
                                IdCajaDiaria = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                Fecha = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2),
                                TipoMovimiento = reader.IsDBNull(3) ? null : reader.GetString(3),
                                MetodoPago = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Concepto = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Monto = reader.IsDBNull(6) ? 0m : reader.GetDecimal(6),
                                CodCliente = reader.IsDBNull(7) ? null : reader.GetString(7),
                                CodProveedor = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CodOrdenTrabajo = reader.IsDBNull(9) ? null : reader.GetString(9),
                                SignoMovimiento = reader.IsDBNull(10) ? 0 : reader.GetInt16(10)
                            };

                            resultado.Add(mov);
                        }
                    }
                }

                return resultado;
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCuentaCorriente", "ListarPorEntidadAsync", $"Tipo:{tipoEntidad},Codigo:{codigo}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene el saldo actual (SUM(Signo * Monto)) para la entidad.
        /// </summary>
        public async Task<decimal> ObtenerSaldoActualAsync(string tipoEntidad, string codigo)
        {
            if (string.IsNullOrWhiteSpace(tipoEntidad)) throw new ArgumentNullException(nameof(tipoEntidad));
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentNullException(nameof(codigo));

            string whereCol = tipoEntidad.Equals("Cliente", StringComparison.OrdinalIgnoreCase) ? "CodCliente" : "CodProveedor";

            string sql = $@"
                SELECT ISNULL(SUM(ISNULL(Signo, CASE WHEN TipoMovimiento = 'INGRESO' THEN 1 ELSE -1 END) * Monto), 0)
                FROM LibroDiario
                WHERE {whereCol} = @Codigo
                  AND ISNULL(Activo,1) = 1;";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    await conn.OpenAsync();

                    var scalar = await cmd.ExecuteScalarAsync();
                    if (scalar == null || scalar == DBNull.Value) return 0m;
                    return Convert.ToDecimal(scalar);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCuentaCorriente", "ObtenerSaldoActualAsync", $"Tipo:{tipoEntidad},Codigo:{codigo}");
                throw;
            }
        }

        /// <summary>
        /// Obtener saldo hasta una fecha (inclusive).
        /// </summary>
        public async Task<decimal> ObtenerSaldoAlAsync(string tipoEntidad, string codigo, DateTime fechaHasta)
        {
            if (string.IsNullOrWhiteSpace(tipoEntidad)) throw new ArgumentNullException(nameof(tipoEntidad));
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentNullException(nameof(codigo));

            string whereCol = tipoEntidad.Equals("Cliente", StringComparison.OrdinalIgnoreCase) ? "CodCliente" : "CodProveedor";

            string sql = $@"
                SELECT ISNULL(SUM(ISNULL(Signo, CASE WHEN TipoMovimiento = 'INGRESO' THEN 1 ELSE -1 END) * Monto), 0)
                FROM LibroDiario
                WHERE {whereCol} = @Codigo
                  AND ISNULL(Activo,1) = 1
                  AND Fecha <= @FechaHasta;";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@FechaHasta", fechaHasta);
                    await conn.OpenAsync();

                    var scalar = await cmd.ExecuteScalarAsync();
                    return scalar == DBNull.Value ? 0m : Convert.ToDecimal(scalar);
                }
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCuentaCorriente", "ObtenerSaldoAlAsync", $"Tipo:{tipoEntidad},Codigo:{codigo},Fecha:{fechaHasta}");
                throw;
            }
        }

        /// <summary>
        /// Obtener un movimiento por su código.
        /// </summary>
        public async Task<LibroDiario> ObtenerMovimientoPorCodigoAsync(string codMovimiento)
        {
            if (string.IsNullOrWhiteSpace(codMovimiento)) throw new ArgumentNullException(nameof(codMovimiento));

            const string sql = @"
                SELECT CodMovimiento, IdCajaDiaria, Fecha, TipoMovimiento, MetodoPago, Concepto, Monto, CodCliente, CodProveedor, CodOrdenTrabajo, ISNULL(Signo, CASE WHEN TipoMovimiento = 'INGRESO' THEN 1 ELSE -1 END) AS Signo
                FROM LibroDiario
                WHERE CodMovimiento = @CodMovimiento
                  AND ISNULL(Activo,1) = 1;";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CodMovimiento", codMovimiento);
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var mov = new LibroDiario
                            {
                                CodMovimiento = reader.IsDBNull(0) ? null : reader.GetString(0),
                                IdCajaDiaria = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                Fecha = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2),
                                TipoMovimiento = reader.IsDBNull(3) ? null : reader.GetString(3),
                                MetodoPago = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Concepto = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Monto = reader.IsDBNull(6) ? 0m : reader.GetDecimal(6),
                                CodCliente = reader.IsDBNull(7) ? null : reader.GetString(7),
                                CodProveedor = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CodOrdenTrabajo = reader.IsDBNull(9) ? null : reader.GetString(9)
                            };
                            return mov;
                        }
                    }
                }

                return null;
            }
            catch (SqlException ex)
            {
                LogHelper.GuardarError(ex, "DaCuentaCorriente", "ObtenerMovimientoPorCodigoAsync", $"Cod:{codMovimiento}");
                throw;
            }
        }
    }
}
