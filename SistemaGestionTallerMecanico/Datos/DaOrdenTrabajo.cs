using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Entidades;

namespace Datos
{
    public class DaOrdenTrabajo
    {
        private readonly string connectionString;
        public DaOrdenTrabajo()
        {
            this.connectionString = ConfigurationManager.AppSettings.Get("Connection");//ConfigHelper.ObtenerConnectionString();
        }

        /// <summary>
        /// Guarda la orden de trabajo y sus detalles en una transacción.
        /// Además actualiza KM del vehículo (si se provee) y registra un movimiento en LibroDiario.
        /// Devuelve true si todo se ejecutó correctamente.
        /// </summary>
        public async Task<bool> GuardarOrdenTrabajoAsync(OrdenTrabajo orden, int? idCajaDiariaParaMovimiento = null)
        {
            using (var cn = new SqlConnection(connectionString))
            {
                await cn.OpenAsync();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        string codOrden = await GenerarCodigoOrdenAsync(cn, tx);
                        orden.CodOrden = codOrden;

                        // Insert cabecera
                        string sqlCab = @"
                        INSERT INTO OrdenTrabajo
                        (CodPresupuesto, Fecha, CodCliente, CodVehiculo, Kilometraje, DescripcionTrabajo, TotalRepuestos, TotalManoObra, TotalGeneral, Usuario, Activo)
                        VALUES (@Cod, @Fecha, @CodCliente, @CodVehiculo, @Kilometraje, @DescripcionTrabajo, @TotalRepuestos, @TotalManoObra, @TotalGeneral, @Usuario, 1);
                        ";
                        using (var cmd = new SqlCommand(sqlCab, cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@Cod", orden.CodOrden);
                            cmd.Parameters.AddWithValue("@Fecha", orden.Fecha.Date);
                            cmd.Parameters.AddWithValue("@CodCliente", (object)orden.CodCliente ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@CodVehiculo", (object)orden.CodVehiculo ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Kilometraje", (object)orden.Kilometraje ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@DescripcionTrabajo", (object)orden.DescripcionTrabajo ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@TotalRepuestos", orden.TotalRepuestos);
                            cmd.Parameters.AddWithValue("@TotalManoObra", orden.TotalManoObra);
                            cmd.Parameters.AddWithValue("@TotalGeneral", orden.TotalGeneral);
                            cmd.Parameters.AddWithValue("@Usuario", (object)orden.Usuario ?? DBNull.Value);

                            await cmd.ExecuteNonQueryAsync();
                        }

                        // Insert detalles
                        string sqlDet = @"
                        INSERT INTO OrdenTrabajoDetalle
                        (CodPresupuesto, Renglon, Tipo, Descripcion, Precio)
                        VALUES (@Cod, @Renglon, @Tipo, @Descripcion, @Precio);
                        ";
                        using (var cmdDet = new SqlCommand(sqlDet, cn, tx))
                        {
                            cmdDet.Parameters.Add("@Cod", SqlDbType.VarChar, 30);
                            cmdDet.Parameters.Add("@Renglon", SqlDbType.Int);
                            cmdDet.Parameters.Add("@Tipo", SqlDbType.VarChar, 2);
                            cmdDet.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 500);
                            cmdDet.Parameters.Add("@Precio", SqlDbType.Decimal).Precision = 18;
                            cmdDet.Parameters["@Precio"].Scale = 2;

                            foreach (var d in orden.Detalles)
                            {
                                cmdDet.Parameters["@Cod"].Value = orden.CodOrden;
                                cmdDet.Parameters["@Renglon"].Value = d.Renglon;
                                cmdDet.Parameters["@Tipo"].Value = d.Tipo;
                                cmdDet.Parameters["@Descripcion"].Value = (object)d.Descripcion ?? DBNull.Value;
                                cmdDet.Parameters["@Precio"].Value = d.Precio;
                                await cmdDet.ExecuteNonQueryAsync();
                            }
                        }

                        // Si Kilometraje fue provisto y CodVehiculo no es null -> actualizar vehiculo
                        if (!string.IsNullOrEmpty(orden.CodVehiculo) && orden.Kilometraje.HasValue)
                        {
                            string sqlUpdKm = @"
                            UPDATE Vehiculos
                            SET Kilometraje = @KM
                            WHERE CodVehiculo = @CodVehiculo;
                            ";
                            using (var cmdKm = new SqlCommand(sqlUpdKm, cn, tx))
                            {
                                cmdKm.Parameters.AddWithValue("@KM", orden.Kilometraje.Value);
                                cmdKm.Parameters.AddWithValue("@CodVehiculo", orden.CodVehiculo);
                                await cmdKm.ExecuteNonQueryAsync();
                            }
                        }

                        // Registrar movimiento en LibroDiario referente al presupuesto (TRANSFERENCIA por defecto)
                        // Monto = TotalGeneral, TipoMovimiento = INGRESO (supuesto) — ajustar según reglas de negocio
                        if (orden.TotalGeneral > 0)
                        {
                            string codMovimiento= await GenerarCodigoMovimientoAsync(cn, tx);

                            string sqlMov = @"
                            INSERT INTO LibroDiario
                            (CodMovimiento, IdCajaDiaria, Fecha, TipoMovimiento, MetodoPago, Concepto, Monto, CodCliente, CodOrdenTrabajo, Activo)
                            VALUES (@CodMov, @IdCaja, @Fecha, @TipoMov, @MetodoPago, @Concepto, @Monto, @CodCliente, @CodOrdenTrabajo, 1);
                            ";
                            using (var cmdMov = new SqlCommand(sqlMov, cn, tx))
                            {
                                cmdMov.Parameters.AddWithValue("@CodMov", codMovimiento); // reutilizar código como referencia
                                cmdMov.Parameters.AddWithValue("@IdCaja", (object)idCajaDiariaParaMovimiento ?? DBNull.Value);
                                cmdMov.Parameters.AddWithValue("@Fecha", DateTime.Now);
                                cmdMov.Parameters.AddWithValue("@TipoMov", "INGRESO");
                                cmdMov.Parameters.AddWithValue("@MetodoPago", "TRANSFERENCIA");
                                cmdMov.Parameters.AddWithValue("@Concepto", $"Presupuesto/Orden {orden.CodOrden}");
                                cmdMov.Parameters.AddWithValue("@Monto", orden.TotalGeneral);
                                cmdMov.Parameters.AddWithValue("@CodCliente", (object)orden.CodCliente ?? DBNull.Value);
                                cmdMov.Parameters.AddWithValue("@CodOrdenTrabajo", orden.CodOrden);

                                await cmdMov.ExecuteNonQueryAsync();
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        private async Task<string> GenerarCodigoOrdenAsync(SqlConnection conn, SqlTransaction tx)
        {
            // Usamos bloqueo para evitar race conditions
            string sql = @"
SELECT ISNULL(MAX(CAST(SUBSTRING(CodPresupuesto, 3, 8) AS INT)), 0)
FROM OrdenTrabajo WITH (UPDLOCK, HOLDLOCK);
";
            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                object res = await cmd.ExecuteScalarAsync();
                int max = (res == DBNull.Value || res == null) ? 0 : Convert.ToInt32(res);
                int siguiente = max + 1;
                return "OT" + siguiente.ToString("D5"); // OT00001
            }
        }

        // Genera código para LibroDiario con formato LD00001 (sin guiones)
        private async Task<string> GenerarCodigoMovimientoAsync(SqlConnection conn, SqlTransaction tx)
        {
            string query = @"
SELECT ISNULL(MAX(CAST(SUBSTRING(CodMovimiento, 3, 8) AS INT)), 0)
FROM LibroDiario WITH (UPDLOCK, HOLDLOCK);
";
            using (var cmd = new SqlCommand(query, conn, tx))
            {
                object res = await cmd.ExecuteScalarAsync();
                int max = (res == DBNull.Value || res == null) ? 0 : Convert.ToInt32(res);
                int siguiente = max + 1;
                return "LD" + siguiente.ToString("D5"); // LD00001
            }
        }

    }
}
