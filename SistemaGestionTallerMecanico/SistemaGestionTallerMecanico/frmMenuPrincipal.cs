using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Formularios.ClienteViews;
using Formularios.CuentaCorrienteViews;
using Formularios.LibroDiarioViews;
using Formularios.ProveedorViews;
using Formularios.VehiculoViews;
using Negocios;

namespace SistemaGestionTallerMecanico
{
    public partial class frmMenuPrincipal : Form
    {

        private readonly NegCajaDiaria negCajaDiaria;

        public frmMenuPrincipal()
        {
            InitializeComponent();
            negCajaDiaria = new NegCajaDiaria();
        }

        private async void btnLibroDiario_Click(object sender, EventArgs e)
        {
            try
            {
                // Deshabilitar botón temporalmente (evitar múltiples clicks)
                btnLibroDiario.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                // PASO 1: Verificar si existe caja de HOY
                DateTime fechaHoy = DateTime.Today;
                CajaDiaria cajaHoy = await negCajaDiaria.ObtenerPorFechaAsync(fechaHoy);

                if (cajaHoy == null)
                {
                    // NO hay caja de hoy → Abrir formulario de apertura
                    this.Cursor = Cursors.Default;

                    var frmAbrirCaja = new frmAbrirCajaDiaria();
                    DialogResult resultado = frmAbrirCaja.ShowDialog();

                    if (resultado == DialogResult.OK)
                    {
                        // Usuario abrió la caja correctamente
                        // Ahora sí abrir el libro diario
                        var frmLibroDiario = new frmLibroDiario();
                        frmLibroDiario.ShowDialog();
                    }
                    else
                    {
                        // Usuario canceló la apertura
                        MessageBox.Show(
                            "Debe abrir la caja para acceder al Libro Diario",
                            "Apertura requerida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }
                else
                {
                    // Ya existe caja de hoy → Abrir directamente
                    this.Cursor = Cursors.Default;

                    var frmLibroDiario = new frmLibroDiario();
                    frmLibroDiario.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show(
                    $"Error al abrir Libro Diario:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                // Rehabilitar botón
                btnLibroDiario.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCuentaCorriente_Click(object sender, EventArgs e)
        {
            frmCuentaCorriente frmCuentaCorriente = new frmCuentaCorriente();   
            frmCuentaCorriente.ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes frmClientes = new frmClientes();
            frmClientes.ShowDialog();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            frmProveedor frmProveedor = new frmProveedor();
            frmProveedor.ShowDialog();
        }

        private void btnVehiculos_Click(object sender, EventArgs e)
        {
            frmVehiculo frmVehiculo = new frmVehiculo();
            frmVehiculo.ShowDialog();
        }
    }
}
