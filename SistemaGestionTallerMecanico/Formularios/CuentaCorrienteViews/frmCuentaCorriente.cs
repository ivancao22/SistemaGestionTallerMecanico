using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formularios.Utils;

namespace Formularios.CuentaCorrienteViews
{
    public partial class frmCuentaCorriente : Form
    {
        public frmCuentaCorriente()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmBuscador buscador = new frmBuscador();
            buscador.QueryBase = "SELECT Id, (nombre + apellido) as nombre, 'Cliente' as tipo FROM  Clientes" +
                                 " UNION ALL " +
                                 " SELECT Id, (Nombre + Apellido) as nombre, 'Proveedor' as tipo FROM Porveedores";
            buscador.CampoFiltro = "nombre";
            buscador.OrdenCampo = "nombre";

            if (buscador.ShowDialog() == DialogResult.OK)
            {
                DataRow row = buscador.RegistroSeleccionado;
                txtFiltro.Text = row["nombre"].ToString();
            }

        }
    }
}
