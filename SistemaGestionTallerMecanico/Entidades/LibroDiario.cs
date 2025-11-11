using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LibroDiario
    {
        public int Id { get; set; }
        public string CodMovimiento { get; set; }
        public int IdCajaDiaria { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }  // INGRESO / EGRESO
        public string MetodoPago { get; set; }      // EFECTIVO / DEBITO / CREDITO / TRANSFERENCIA / CHEQUE
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public string CodCliente { get; set; }
        public string CodProveedor { get; set; }
        public string CodVehiculo { get; set; }
        public string CodOrdenTrabajo { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Propiedad adicional para mostrar en grilla (se llena con join)
        public string EntidadRelacionada { get; set; }

        // Constructor por defecto
        public LibroDiario()
        {
            Fecha = DateTime.Now;
            Activo = true;
        }

        // Constructor con parámetros
        public LibroDiario(int idCajaDiaria, string tipoMovimiento, string metodoPago,
                          string concepto, decimal monto)
        {
            IdCajaDiaria = idCajaDiaria;
            Fecha = DateTime.Now;
            TipoMovimiento = tipoMovimiento;
            MetodoPago = metodoPago;
            Concepto = concepto;
            Monto = monto;
            Activo = true;
        }
    }
}
