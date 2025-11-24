using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
using Utiles;

namespace Negocios
{
    /// <summary>
    /// Capa de negocio para Cuenta Corriente (wrapper sobre DaCuentaCorriente).
    /// Contiene validaciones ligeras y logging.
    /// </summary>
    public class NegCuentaCorriente
    {
        private readonly DaCuentaCorriente _da;

        public NegCuentaCorriente()
        {
            _da = new DaCuentaCorriente();
        }

        /// <summary>
        /// Lista movimientos para una entidad (Cliente o Proveedor).
        /// Valida tipoEntidad y delega al DA.
        /// </summary>
        public async Task<List<LibroDiario>> ListarPorEntidadAsync(string tipoEntidad, string codigo, DateTime? desde = null, DateTime? hasta = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tipoEntidad)) throw new ArgumentNullException(nameof(tipoEntidad));
                if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentNullException(nameof(codigo));

                tipoEntidad = tipoEntidad.Trim();

                if (!EsTipoEntidadValido(tipoEntidad))
                    throw new ArgumentException("Tipo de entidad inválido. Valores permitidos: 'Cliente' o 'Proveedor'.");

                return await _da.ListarPorEntidadAsync(tipoEntidad, codigo, desde, hasta);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "NegCuentaCorriente", "ListarPorEntidadAsync", $"Tipo:{tipoEntidad},Codigo:{codigo}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene el saldo actual (SUM(Signo * Monto)) para la entidad.
        /// </summary>
        public async Task<decimal> ObtenerSaldoActualAsync(string tipoEntidad, string codigo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tipoEntidad)) throw new ArgumentNullException(nameof(tipoEntidad));
                if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentNullException(nameof(codigo));

                tipoEntidad = tipoEntidad.Trim();

                if (!EsTipoEntidadValido(tipoEntidad))
                    throw new ArgumentException("Tipo de entidad inválido. Valores permitidos: 'Cliente' o 'Proveedor'.");

                return await _da.ObtenerSaldoActualAsync(tipoEntidad, codigo);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "NegCuentaCorriente", "ObtenerSaldoActualAsync", $"Tipo:{tipoEntidad},Codigo:{codigo}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene el saldo acumulado hasta fecha (inclusive).
        /// </summary>
        public async Task<decimal> ObtenerSaldoAlAsync(string tipoEntidad, string codigo, DateTime fechaHasta)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tipoEntidad)) throw new ArgumentNullException(nameof(tipoEntidad));
                if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentNullException(nameof(codigo));

                tipoEntidad = tipoEntidad.Trim();

                if (!EsTipoEntidadValido(tipoEntidad))
                    throw new ArgumentException("Tipo de entidad inválido. Valores permitidos: 'Cliente' o 'Proveedor'.");

                return await _da.ObtenerSaldoAlAsync(tipoEntidad, codigo, fechaHasta);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "NegCuentaCorriente", "ObtenerSaldoAlAsync", $"Tipo:{tipoEntidad},Codigo:{codigo},Fecha:{fechaHasta}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un movimiento por su código.
        /// </summary>
        public async Task<LibroDiario> ObtenerMovimientoPorCodigoAsync(string codMovimiento)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codMovimiento)) throw new ArgumentNullException(nameof(codMovimiento));
                return await _da.ObtenerMovimientoPorCodigoAsync(codMovimiento);
            }
            catch (Exception ex)
            {
                LogHelper.GuardarError(ex, "NegCuentaCorriente", "ObtenerMovimientoPorCodigoAsync", $"Cod:{codMovimiento}");
                throw;
            }
        }

        #region Helpers privados

        private bool EsTipoEntidadValido(string tipo)
        {
            return string.Equals(tipo, "Cliente", StringComparison.OrdinalIgnoreCase)
                || string.Equals(tipo, "Proveedor", StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
