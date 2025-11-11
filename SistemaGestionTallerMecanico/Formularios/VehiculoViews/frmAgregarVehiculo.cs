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
using Formularios.Utils;
using Negocio;
using Negocios;

namespace Formularios.VehiculoViews
{
    public partial class frmAgregarVehiculo : Form
    {
        private NegVehiculo negVehiculos = new NegVehiculo();

        // Propiedades públicas para pre-carga (modo edición)
        public string CodVehiculo { get; set; }
        public string CodCliente { get; set; }
        public string Modelo { get; set; }
        public string Patente { get; set; }
        public int? Kilometraje { get; set; }

        private bool esNuevo => string.IsNullOrEmpty(CodVehiculo);

        public frmAgregarVehiculo()
        {
            InitializeComponent();
        }

        private void frmAgregarVehiculo_Load(object sender, EventArgs e)
        {
            // Cambiar título según sea nuevo o edición
            this.Text = esNuevo ? "Agregar vehículo" : "Editar vehículo";

            // Si es EDICIÓN, cargar datos previos
            if (!esNuevo)
            {
                // Cargar nombre del cliente desde su código
                if (!string.IsNullOrEmpty(CodCliente))
                {
                    NegClientes negClientes = new NegClientes();
                    Cliente cliente = negClientes.ObtenerClientePorCodigo(CodCliente);

                    if (cliente != null)
                    {
                        txtCliente.Text = $"{cliente.Nombre} {cliente.Apellido}";  
                        txtCliente.Tag = cliente.CodCliente;        // Guardar "CL00001" en Tag
                    }
                }

                // Precargar los demás campos
                if (!string.IsNullOrEmpty(Modelo)) txtModelo.Text = Modelo;
                if (!string.IsNullOrEmpty(Patente)) txtPatente.Text = Patente;
                if (Kilometraje.HasValue) txtKm.Text = Kilometraje.Value.ToString();
            }

            // Enfocar el primer campo editable
            if (esNuevo)
                btnBuscar.Focus();  // En nuevo, primero debe buscar cliente
            else
                txtModelo.Focus();  // En edición, puede cambiar directamente el modelo
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Configurar el buscador genérico para Clientes
            frmBuscador frm = new frmBuscador
            {
                QueryBase = @"SELECT 
                                CodCliente as Codigo, 
                                (Nombre + ' ' + Apellido) AS Cliente
                              FROM Clientes",
                CampoFiltro = "(Nombre + ' ' + Apellido)",  // Filtra por nombre completo
                OrdenCampo = "Nombre, Apellido",
                WhereAdicional = "Activo = 1"  // Solo clientes activos
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Obtener el registro seleccionado
                DataRow registro = frm.RegistroSeleccionado;

                if (registro != null)
                {
                    // Guardar el código en el Tag y mostrar el nombre
                    txtCliente.Tag = registro[0].ToString();      // "CL00001"
                    txtCliente.Text = registro[1].ToString();        // "Juan Pérez"

                    // Enfocar el siguiente campo
                    txtModelo.Focus();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // VALIDACIONES

            // Validar que se haya seleccionado un cliente
            if (txtCliente.Tag == null || string.IsNullOrWhiteSpace(txtCliente.Tag.ToString()))
            {
                MessageBox.Show("Debe seleccionar un cliente usando el botón de búsqueda",
                               "Validación",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                btnBuscar.Focus();
                return;
            }

            // Validar modelo
            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show("El modelo es obligatorio",
                               "Validación",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                txtModelo.Focus();
                return;
            }

            // Validar patente
            if (string.IsNullOrWhiteSpace(txtPatente.Text))
            {
                MessageBox.Show("La patente es obligatoria",
                               "Validación",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                txtPatente.Focus();
                return;
            }

            // Validar kilometraje (si tiene valor, debe ser un número válido)
            int? kilometraje = null;
            if (!string.IsNullOrWhiteSpace(txtKm.Text))
            {
                if (!int.TryParse(txtKm.Text.Trim(), out int km))
                {
                    MessageBox.Show("El kilometraje debe ser un número válido, ej. 153000",
                                   "Validación",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Warning);
                    txtKm.Focus();
                    return;
                }

                if (km < 0)
                {
                    MessageBox.Show("El kilometraje no puede ser negativo",
                                   "Validación",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Warning);
                    txtKm.Focus();
                    return;
                }

                kilometraje = km;
            }

            // ========================================
            // Crear objeto Vehiculo
            // ========================================
            Vehiculo vehiculo = new Vehiculo
            {
                CodVehiculo = CodVehiculo,  // Puede estar vacío si es nuevo
                CodCliente = txtCliente.Tag.ToString(),  
                Modelo = txtModelo.Text.Trim(),
                Patente = txtPatente.Text.Trim(),
                Kilometraje = kilometraje
            };

            // ========================================
            // Guardar en la base de datos
            // ========================================
            bool resultado;

            if (esNuevo)
            {
                // Generar código
                vehiculo.CodVehiculo = negVehiculos.ObtenerSiguienteCodigo();

                if (vehiculo.CodVehiculo == null)
                {
                    MessageBox.Show("Error al generar el código del vehículo. Verifique la conexión.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return;
                }

                resultado = negVehiculos.AgregarVehiculo(vehiculo);

                if (resultado)
                {
                    MessageBox.Show("Vehículo guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar el vehículo. Intente nuevamente o contacte con soporte.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                resultado = negVehiculos.ActualizarVehiculo(vehiculo);

                if (resultado)
                {
                    MessageBox.Show("Vehículo actualizado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al actualizar el vehículo. Intente nuevamente o contacte con soporte.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
