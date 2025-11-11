using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;

namespace Formularios.LibroDiarioViews
{
    public partial class frmAbrirCajaDiaria : Form
    {
        // Variables privadas
        private readonly NegCajaDiaria negCajaDiaria;
        private readonly DateTime fechaCaja = DateTime.Today;
        private decimal? saldoFinalAyer = null; // Guardar para no consultar 2 veces
        public int IdCajaDiariaCreada;
        public frmAbrirCajaDiaria()
        {
            InitializeComponent();
            negCajaDiaria = new NegCajaDiaria();
        }

        private async void frmAbrirCajaDiaria_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. FECHA (con cultura español Argentina)
                CultureInfo culturaEspanol = new CultureInfo("es-AR");
                string fechaFormateada = fechaCaja.ToString("dddd, dd 'de' MMMM 'de' yyyy", culturaEspanol);
                // Capitalizar primera letra
                fechaFormateada = char.ToUpper(fechaFormateada[0]) + fechaFormateada.Substring(1);
                lblFecha.Text = $"📅 Fecha: {fechaFormateada}";

                // 2. USUARIO
                lblUsuario.Text = $"👤 Usuario: {Environment.UserName}";

                // 3. SALDO DE AYER (consultar BD)
                await CargarSaldoAyerAsync();

                // ✅ 4. INICIALIZAR TEXTBOX
                txtMonto.Text = "0";
                txtMonto.Focus();
                txtMonto.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar formulario: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private async Task CargarSaldoAyerAsync()
        {
            try
            {
                // Consultar saldo de ayer
                saldoFinalAyer = await negCajaDiaria.ObtenerSaldoDiaAnteriorAsync(fechaCaja);

                if (saldoFinalAyer.HasValue)
                {
                    // Hay saldo de ayer
                    lblSaldoAyer.Text = $"💰 Saldo final de ayer: {saldoFinalAyer.Value:C}";
                    lblSaldoAyer.ForeColor = Color.Green;
                    btnObtenerMonto.Enabled = true;
                    btnObtenerMonto.Text = "Usar monto final de ayer";
                }
                else
                {
                    // No hay saldo de ayer
                    lblSaldoAyer.Text = "📊 Saldo final de ayer: Sin registro";
                    lblSaldoAyer.ForeColor = Color.Gray;
                    btnObtenerMonto.Enabled = false;
                    btnObtenerMonto.Text = "Sin saldo anterior";
                }
            }
            catch (Exception ex)
            {
                lblSaldoAyer.Text = "❌ Error al obtener saldo de ayer";
                lblSaldoAyer.ForeColor = Color.Red;
                btnObtenerMonto.Enabled = false;

                MessageBox.Show(
                    $"Error al obtener saldo del día anterior: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void btnObtenerMonto_Click(object sender, EventArgs e)
        {
            try
            {
                // Usar la variable que ya tiene el saldo (no consultar BD de nuevo)
                if (saldoFinalAyer.HasValue)
                {
                    txtMonto.Text = saldoFinalAyer.Value.ToString("0.00");
                    txtMonto.SelectAll();
                    txtMonto.Focus();
                }
                else
                {
                    MessageBox.Show(
                        "No hay saldo del día anterior disponible",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ VALIDACIÓN 1: Campo vacío
                if (string.IsNullOrWhiteSpace(txtMonto.Text))
                {
                    MessageBox.Show(
                        "Debe ingresar un monto inicial",
                        "Campo requerido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtMonto.Focus();
                    return;
                }

                // ✅ VALIDACIÓN 2: Formato válido
                if (!decimal.TryParse(txtMonto.Text, out decimal montoInicial))
                {
                    MessageBox.Show(
                        "El monto ingresado no es válido.\n\n" +
                        "Ejemplos válidos: 0 / 100 / 1000.50 / 15000",
                        "Monto inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    txtMonto.Focus();
                    txtMonto.SelectAll();
                    return;
                }

                // ✅ VALIDACIÓN 3: Monto negativo
                if (montoInicial < 0)
                {
                    MessageBox.Show(
                        "El monto inicial no puede ser negativo",
                        "Monto inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    txtMonto.Focus();
                    txtMonto.SelectAll();
                    return;
                }

                // ✅ CONFIRMACIÓN
                var resultado = MessageBox.Show(
                    $"¿Confirma abrir la caja con los siguientes datos?\n\n" +
                    $"📅 Fecha: {fechaCaja:dd/MM/yyyy}\n" +
                    $"💵 Saldo inicial: {montoInicial:C}\n" +
                    $"👤 Usuario: {Environment.UserName}",
                    "Confirmar Apertura",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultado != DialogResult.Yes)
                    return;

                // DESHABILITAR BOTÓN (evitar doble click)
                btnGuardar.Enabled = false;
                btnObtenerMonto.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                // GUARDAR EN BD
                IdCajaDiariaCreada = await negCajaDiaria.AbrirCajaAsync(
                    fechaCaja,
                    montoInicial,
                    Environment.UserName
                );

                this.Cursor = Cursors.Default;

                // ✅ MENSAJE DE ÉXITO
                MessageBox.Show(
                    $"✅ Caja abierta correctamente\n\n" +
                    $"📅 Fecha: {fechaCaja:dd/MM/yyyy}\n" +
                    $"💵 Saldo inicial: {montoInicial:C}\n" +
                    $"👤 Usuario: {Environment.UserName}\n" +
                    $"🆔 ID Caja: {IdCajaDiariaCreada}",
                    "Apertura Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // CERRAR FORMULARIO CON DIALOGRESULT OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // RESTAURAR ESTADO EN CASO DE ERROR
                this.Cursor = Cursors.Default;
                btnGuardar.Enabled = true;

                if (saldoFinalAyer.HasValue)
                {
                    btnObtenerMonto.Enabled = true;
                }

                MessageBox.Show(
                    $"❌ Error al abrir la caja:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
