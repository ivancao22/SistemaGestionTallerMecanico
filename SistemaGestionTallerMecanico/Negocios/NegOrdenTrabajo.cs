using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocios
{
    public class NegOrdenTrabajo
    {
        private readonly DaOrdenTrabajo _da;
        public NegOrdenTrabajo()
        {
            _da = new DaOrdenTrabajo();
        }

        /// <summary>
        /// Valida, genera código y guarda la orden de trabajo. Devuelve el CodPresupuesto generado.
        /// </summary>
        public async Task<string> GuardarOrdenTrabajoAsync(OrdenTrabajo orden, int? idCajaDiariaParaMovimiento = null)
        {
            if (orden == null) throw new ArgumentNullException(nameof(orden));
            if (orden.Detalles == null || !orden.Detalles.Any())
                throw new InvalidOperationException("La orden debe tener al menos un detalle.");

            // Generar código único simple
            orden.CodOrden = GenerarCodigo();
            orden.Fecha = orden.Fecha.Date;
            //orden.TotalRepuestos = orden.Detalles.Where(d => string.Equals(d.Tipo, "R", StringComparison.OrdinalIgnoreCase)).Sum(d => d.Precio);
            //orden.TotalManoObra = orden.Detalles.Where(d => string.Equals(d.Tipo, "MO", StringComparison.OrdinalIgnoreCase)).Sum(d => d.Precio);
            //orden.TotalGeneral = orden.TotalRepuestos + orden.TotalManoObra;

            bool ok = await _da.GuardarOrdenTrabajoAsync(orden, idCajaDiariaParaMovimiento);
            if (!ok) throw new Exception("No se pudo guardar la orden de trabajo.");
            return orden.CodOrden;
        }

        private string GenerarCodigo()
        {
            // PRE-YYYYMMDD-HHMMSS-usu
            return $"OT-{DateTime.Now:yyyyMMddHHmmss}";
        }
    }
}
