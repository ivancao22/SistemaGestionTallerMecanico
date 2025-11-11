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
using Negocios;

namespace Formularios.ProveedorViews
{
    public partial class frmAgregarProveedor : Form
    {
        private NegProveedor negProveedor = new NegProveedor();

        // Propiedades públicas para pre-carga
        public string CodProveedor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        private bool esNuevo => string.IsNullOrEmpty(CodProveedor);


        public frmAgregarProveedor()
        {
            InitializeComponent();
        }

        private void frmAgregarProveedor_Load(object sender, EventArgs e)
        {
            // Cambiar título según sea nuevo o edición
            this.Text = esNuevo ? "Agregar proveedor" : "Editar proveedor";

            // Precargar los textbox si vienen datos
            if (!string.IsNullOrEmpty(Nombre)) txtNombre.Text = Nombre;
            if (!string.IsNullOrEmpty(Apellido)) txtApellido.Text = Apellido;
            if (!string.IsNullOrEmpty(RazonSocial)) txtRazonSocial.Text = RazonSocial;
            if (!string.IsNullOrEmpty(CUIT)) txtCuit.Text = CUIT;
            if (!string.IsNullOrEmpty(Telefono)) txtTelefono.Text = Telefono;
            if (!string.IsNullOrEmpty(Email)) txtEmail.Text = Email;

            // Enfocar el primer campo
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // VALIDACIÓN ESPECIAL DE PROVEEDORES

            bool tieneNombre = !string.IsNullOrWhiteSpace(txtNombre.Text);
            bool tieneApellido = !string.IsNullOrWhiteSpace(txtApellido.Text);
            bool tieneRazonSocial = !string.IsNullOrWhiteSpace(txtRazonSocial.Text);

            // Al menos uno de los 3 debe tener valor
            if (!tieneNombre && !tieneApellido && !tieneRazonSocial)
            {
                MessageBox.Show("Debe completar al menos uno de los siguientes campos:\n- Nombre\n- Apellido\n- Razón Social",
                               "Validación",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }


            // Validar email si tiene valor
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !EsEmailValido(txtEmail.Text.Trim()))
            {
                MessageBox.Show("El formato del email no es válido",
                               "Validación",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Validar teléfono si tiene valor
            if (!string.IsNullOrWhiteSpace(txtTelefono.Text) && !EsTelefonoValido(txtTelefono.Text.Trim()))
            {
                MessageBox.Show("El formato del teléfono no es válido. Solo se permiten números, espacios, guiones y paréntesis",
                               "Validación",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            // Validar CUIT si tiene valor (formato: 20-12345678-9 o solo números)
            if (!string.IsNullOrWhiteSpace(txtCuit.Text) && !EsCuitValido(txtCuit.Text.Trim()))
            {
                MessageBox.Show("El formato del CUIT no es válido. Use el formato: 20-12345678-9 o solo números (11 dígitos)",
                               "Validación",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                txtCuit.Focus();
                return;
            }

           

            // Crear objeto Proveedor
            Proveedor proveedor = new Proveedor
            {
                CodProveedor = CodProveedor,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                RazonSocial = txtRazonSocial.Text.Trim(),
                CUIT = txtCuit.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            // Guardar en la base de datos
            bool resultado;

            if (esNuevo)
            {
                proveedor.CodProveedor = negProveedor.ObtenerSiguienteCodigo();

                resultado = negProveedor.AgregarProveedor(proveedor);

                if (resultado)
                {
                    MessageBox.Show("Proveedor guardado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar el proveedor. Intente nuevamente o contacte con soporte.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                resultado = negProveedor.ActualizarProveedor(proveedor);

                if (resultado)
                {
                    MessageBox.Show("Proveedor actualizado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al actualizar el proveedor. Intente nuevamente o contacte con soporte.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

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

        private bool EsCuitValido(string cuit)
        {
            // Permitir formato: 20-12345678-9 o solo números (debe tener 11 dígitos sin guiones)
            string cuitLimpio = cuit.Replace("-", "").Replace(" ", "");

            // Debe tener exactamente 11 dígitos
            if (cuitLimpio.Length != 11)
                return false;

            // Todos deben ser números
            return Regex.IsMatch(cuitLimpio, @"^\d{11}$");
        }

    }
}
