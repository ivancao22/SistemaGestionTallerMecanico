using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CajaDiaria
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaHoraApertura { get; set; }

        // Constructor
        public CajaDiaria()
        {
            Fecha = DateTime.Today;
            SaldoInicial = 0;
            SaldoFinal = 0;
            Usuario = Environment.UserName;
            FechaHoraApertura = DateTime.Now;
        }

        public CajaDiaria(DateTime fecha, decimal saldoInicial, string usuario)
        {
            Fecha = fecha;
            SaldoInicial = saldoInicial;
            SaldoFinal = 0;
            Usuario = usuario;
            FechaHoraApertura = DateTime.Now;
        }
    }
}
