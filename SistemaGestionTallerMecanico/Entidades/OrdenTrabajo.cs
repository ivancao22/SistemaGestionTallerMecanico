using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class OrdenTrabajo
    {
        public string CodOrden { get; set; }
        public DateTime Fecha { get; set; }
        public string CodCliente { get; set; }
        public string CodVehiculo { get; set; }
        public int? Kilometraje { get; set; }
        public string DescripcionTrabajo { get; set; }
        public string Usuario { get; set; }
        public List<OrdenTrabajoDetalle> Detalles { get; set; } = new List<OrdenTrabajoDetalle>();
        public string NombreCliente { get; set; }  // NUEVO: "Ivan Cao"
        public string ModeloVehiculo { get; set; }  // NUEVO: "Honda Civic EX 1.7 2005"

        public string PatenteVehiculo { get; set; }  // NUEVO: "AB123CD"

        // Propiedades calculadas (si no las tienes)
        public decimal TotalRepuestos => Detalles?.Where(d => d.Tipo == "R").Sum(d => d.Precio) ?? 0m;
        public decimal TotalManoObra => Detalles?.Where(d => d.Tipo == "MO").Sum(d => d.Precio) ?? 0m;
        public decimal TotalGeneral => TotalRepuestos + TotalManoObra;
    }

    public class OrdenTrabajoDetalle
    {
        public int Renglon { get; set; }
        public string Tipo { get; set; } // "R" o "MO"
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}

