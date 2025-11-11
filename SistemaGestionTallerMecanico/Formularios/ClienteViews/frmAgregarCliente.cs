using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Formularios.ClienteViews
{
    public partial class frmAgregarCliente : Form
    {
        private NegClientes negClientes = new NegClientes();

        // Propiedades públicas para pre-carga (INTERNAS, no visibles en el formulario)
        public string CodCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        private bool esNuevo => string.IsNullOrEmpty(CodCliente);

        public frmAgregarCliente()
        {
            InitializeComponent();
        }

        private void frmAgregarCliente_Load(object sender, EventArgs e)
        {
            // Cambiar título según sea nuevo o edición
            this.Text = esNuevo ? "Agregar Cliente" : "Editar Cliente";

            // Precargar los textbox si vienen datos (para edición)
            if (!string.IsNullOrEmpty(Nombre)) txtNombre.Text = Nombre;
            if (!string.IsNullOrEmpty(Apellido)) txtApellido.Text = Apellido;
            if (!string.IsNullOrEmpty(Telefono)) txtTelefono.Text = Telefono;
            if (!string.IsNullOrEmpty(Email)) txtEmail.Text = Email;

            // Enfocar el primer campo
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            // 1. Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es obligatorio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return;
            }

            // 2. Validar formato de EMAIL (opcional, pero si tiene valor debe ser válido)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !EsEmailValido(txtEmail.Text.Trim()))
            {
                MessageBox.Show("El formato del email no es válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // 3. Validar formato de TELÉFONO (opcional, pero si tiene valor debe ser válido)
            if (!string.IsNullOrWhiteSpace(txtTelefono.Text) && !EsTelefonoValido(txtTelefono.Text.Trim()))
            {
                MessageBox.Show("El formato del teléfono no es válido. Solo se permiten números, espacios, guiones y paréntesis",
                               "Validación",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }
                

            // Crear objeto Cliente
            
            Cliente cliente = new Cliente
            {             
                CodCliente = CodCliente,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            // Guardar en la base de datos
            bool resultado;

            if (esNuevo)
            {
                cliente.CodCliente = negClientes.ObtenerSiguienteCodigo();

                // AGREGAR nuevo cliente
                resultado = negClientes.AgregarCliente(cliente);

                if (resultado)
                {
                    MessageBox.Show("Cliente guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar el cliente. Intente nuevamente o contactese con su soporte.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                // ACTUALIZAR cliente existente
                resultado = negClientes.ActualizarCliente(cliente);

                if (resultado)
                {
                    MessageBox.Show("Cliente actualizado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al actualizar el cliente. Intente nuevamente o contactese con su soporte.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return;
                }
            }

            // Cerrar el formulario con resultado OK para que el formulario padre recargue la grilla
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // MÉTODOS DE VALIDACIÓN DE FORMATO

        // Validación de formato de EMAIL
        private bool EsEmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Validación de formato de TELÉFONO
        private bool EsTelefonoValido(string telefono)
        {
            // Permitir números, espacios, guiones, paréntesis
            // Ejemplos válidos: "123456789", "123-456-789", "(011) 1234-5678", "11 2345 6789"
            string patron = @"^[\d\s\-\(\)]+$";
            return Regex.IsMatch(telefono, patron) && telefono.Length >= 7;
        }


    }
}
