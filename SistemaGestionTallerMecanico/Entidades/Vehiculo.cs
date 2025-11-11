using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Vehiculo
    {
        public string CodVehiculo { get; set; }
        public string CodCliente { get; set; }
        public string Modelo { get; set; }
        public string Patente { get; set; }
        public int? Kilometraje { get; set; }  // Nullable porque puede estar vacío
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }

        public Vehiculo() 
        {
            Activo = true;
            FechaAlta = DateTime.Now;
        }
    }
}
