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

namespace Formularios.LibroDiarioViews
{
    public partial class frmAgregarMovimiento : Form
    {
        private readonly NegLibroDiario negLibroDiario;
        private readonly NegVehiculo negVehiculos;
        private readonly NegClientes negClientes;
        private readonly NegProveedor negProveedores;
        private readonly int idCajaDiaria;
        private LibroDiario movimientoAEditar = null; // null = AGREGAR, con datos = EDITAR

        // Variables para guardar códigos seleccionados
        private string codClienteSeleccionado = null;
        private string codProveedorSeleccionado = null;
        private string codVehiculoSeleccionado = null;

        // ENUMS PARA LOS COMBOS
        public enum MetodoPago
        {
            EFECTIVO,
            DEBITO,
            CREDITO,
            TRANSFERENCIA,
            CHEQUE
        }

        public enum TipoMovimiento
        {
            INGRESO,
            EGRESO
        }

        // Constructor para AGREGAR
        public frmAgregarMovimiento(int idCajaDiaria)
        {
            InitializeComponent();
            this.negLibroDiario = new NegLibroDiario();
            this.idCajaDiaria = idCajaDiaria;
            this.movimientoAEditar = null; // Modo AGREGAR
        }

        // Constructor para EDITAR
        public frmAgregarMovimiento(int idCajaDiaria, LibroDiario movimiento)
        {
            InitializeComponent();
            this.negLibroDiario = new NegLibroDiario();
            this.negClientes = new NegClientes();
            this.negProveedores = new NegProveedor();
            this.negVehiculos = new NegVehiculo();
            this.idCajaDiaria = idCajaDiaria;
            this.movimientoAEditar = movimiento; // Modo EDITAR
        }

        private void frmAgregarMovimiento_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ Cargar ComboBox Método de Pago
                cmbMetodoPago.Items.Clear();
                cmbMetodoPago.Items.Add(MetodoPago.EFECTIVO);
                cmbMetodoPago.Items.Add(MetodoPago.DEBITO);
                cmbMetodoPago.Items.Add(MetodoPago.CREDITO);
                cmbMetodoPago.Items.Add(MetodoPago.TRANSFERENCIA);
                cmbMetodoPago.Items.Add(MetodoPago.CHEQUE);
                cmbMetodoPago.SelectedIndex = 0; // EFECTIVO por defecto

                // ✅ Cargar ComboBox Tipo de Movimiento
                cmbTipoMov.Items.Clear();
                cmbTipoMov.Items.Add(TipoMovimiento.INGRESO);
                cmbTipoMov.Items.Add(TipoMovimiento.EGRESO);
                cmbTipoMov.SelectedIndex = 0; // INGRESO por defecto

                // ✅ Configurar RadioButtons
                optCliente.Checked = true; // Cliente por defecto

                // ✅ Agregar label informativo sobre método de pago
                Label lblInfo = new Label();
                lblInfo.Text = "ℹ️ Solo EFECTIVO afecta el saldo de caja";
                lblInfo.AutoSize = true;
                lblInfo.ForeColor = Color.Gray;
                lblInfo.Font = new Font(this.Font.FontFamily, 8, FontStyle.Italic);
                // Posicionar debajo del combo de método de pago
                lblInfo.Location = new Point(cmbMetodoPago.Left, cmbMetodoPago.Bottom + 5);
                this.Controls.Add(lblInfo);

                // ✅ Placeholder manual (usar Enter/Leave)
                ConfigurarPlaceholders();

                // ✅ Si es MODO EDITAR, cargar datos
                if (movimientoAEditar != null)
                {
                    this.Text = "Editar movimiento";
                    btnGuardar.Text = "💾 Guardar cambios";
                    CargarDatosParaEditar();
                }
                else
                {
                    this.Text = "Agregar movimiento";
                    btnGuardar.Text = "💾 Guardar";
                }
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

        private void CargarDatosParaEditar()
        {
            try
            {
                // Tipo de movimiento
                if (movimientoAEditar.TipoMovimiento == "INGRESO")
                    cmbTipoMov.SelectedItem = TipoMovimiento.INGRESO;
                else
                    cmbTipoMov.SelectedItem = TipoMovimiento.EGRESO;

                // Método de pago
                switch (movimientoAEditar.MetodoPago)
                {
                    case "EFECTIVO":
                        cmbMetodoPago.SelectedItem = MetodoPago.EFECTIVO;
                        break;
                    case "DEBITO":
                        cmbMetodoPago.SelectedItem = MetodoPago.DEBITO;
                        break;
                    case "CREDITO":
                        cmbMetodoPago.SelectedItem = MetodoPago.CREDITO;
                        break;
                    case "TRANSFERENCIA":
                        cmbMetodoPago.SelectedItem = MetodoPago.TRANSFERENCIA;
                        break;
                    case "CHEQUE":
                        cmbMetodoPago.SelectedItem = MetodoPago.CHEQUE;
                        break;
                }

                // Monto
                txtMonto.Text = movimientoAEditar.Monto.ToString("0.00");

                // Comentario
                txtComentario.Text = movimientoAEditar.Concepto;

                // Cliente/Proveedor/Otro
                if (!string.IsNullOrEmpty(movimientoAEditar.CodCliente))
                {
                    Cliente cliente = negClientes.ObtenerClientePorCodigo(movimientoAEditar.CodCliente);
                    optCliente.Checked = true;
                    codClienteSeleccionado = movimientoAEditar.CodCliente;
                    txtPersona.Text = cliente.Nombre + " " + cliente.Apellido ?? "Cliente";
                    txtPersona.Tag = movimientoAEditar.CodCliente;
                }
                else if (!string.IsNullOrEmpty(movimientoAEditar.CodProveedor))
                {
                    Proveedor provee = negProveedores.ObtenerProveedorPorCodigo(movimientoAEditar.CodProveedor);
                    optProveedor.Checked = true;
                    codProveedorSeleccionado = movimientoAEditar.CodProveedor;
                    txtPersona.Text = provee.RazonSocial??provee.Nombre + " " + provee.Apellido;
                    txtPersona.Tag = movimientoAEditar.CodProveedor;
                }
                else
                {
                    optOtro.Checked = true;
                }

                // Vehículo (si existe)
                if (!string.IsNullOrEmpty(movimientoAEditar.CodOrdenTrabajo))
                {
                    // TODO: Cargar datos del vehículo
                    //Vehiculo vehiculo = negVehiculos.ObtenerVehiculoPorCodigo(movimientoAEditar.CodVehiculo);
                    //codVehiculoSeleccionado = movimientoAEditar.CodVehiculo;
                    //txtVehiculo.Tag = movimientoAEditar.CodVehiculo; vehiculo.Patente + "-" + vehiculo.Modelo??
                    txtVehiculo.Text = "Vehículo asociado";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar datos: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void optCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (optCliente.Checked)
            {
                // Habilitar búsqueda
                txtPersona.Enabled = true;
                btnBuscarPersona.Enabled = true;

                // Limpiar si cambia de opción
                txtPersona.Clear();
                txtPersona.Tag = null;
                codClienteSeleccionado = null;
                codProveedorSeleccionado = null;

                ConfigurarPlaceholders();
            }
        }

        private void optProveedor_CheckedChanged(object sender, EventArgs e)
        {
            if (optProveedor.Checked)
            {
                // Habilitar búsqueda
                txtPersona.Enabled = true;
                btnBuscarPersona.Enabled = true;

                // Limpiar si cambia de opción
                txtPersona.Clear();
                txtPersona.Tag = null;
                codClienteSeleccionado = null;
                codProveedorSeleccionado = null;

                ConfigurarPlaceholders();
            }
        }

        private void optOtro_CheckedChanged(object sender, EventArgs e)
        {
            if (optOtro.Checked)
            {
                // Deshabilitar búsqueda
                txtPersona.Enabled = false;
                btnBuscarPersona.Enabled = false;

                txtVehiculo.Enabled = false;
                btnBuscarVehiculo.Enabled = false;

                // Limpiar
                txtPersona.Clear();
                txtPersona.Tag = null;
                codClienteSeleccionado = null;
                codProveedorSeleccionado = null;

                ConfigurarPlaceholders();
            }
        }

        private void btnBuscarPersona_Click(object sender, EventArgs e)
        {
            try
            {
                if (optCliente.Checked)
                {
                    // ✅ BUSCAR CLIENTE
                    frmBuscador frm = new frmBuscador
                    {
                        QueryBase = @"SELECT 
                                        CodCliente AS Codigo, 
                                        (Nombre + ' ' + Apellido) AS Cliente
                                      FROM Clientes",
                        CampoFiltro = "(Nombre + ' ' + Apellido)",
                        OrdenCampo = "Nombre, Apellido",
                        WhereAdicional = "Activo = 1"
                    };

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        DataRow registro = frm.RegistroSeleccionado;

                        if (registro != null)
                        {
                            codClienteSeleccionado = registro["Codigo"].ToString();
                            txtPersona.Tag = codClienteSeleccionado;
                            txtPersona.Text = registro["Cliente"].ToString();
                            txtPersona.ForeColor = Color.Black;
                            txtVehiculo.Focus();
                        }
                    }
                }
                else if (optProveedor.Checked)
                {
                    // ✅ BUSCAR PROVEEDOR
                    frmBuscador frm = new frmBuscador
                    {
                        QueryBase = @"SELECT 
                                        CodProveedor AS Codigo,
                                        ISNULL(RazonSocial, Nombre + ' ' + ISNULL(Apellido, '')) AS Proveedor
                                      FROM Proveedores",
                        CampoFiltro = "ISNULL(RazonSocial, Nombre + ' ' + ISNULL(Apellido, ''))",
                        OrdenCampo = "RazonSocial, Nombre",
                        WhereAdicional = "Activo = 1"
                    };

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        DataRow registro = frm.RegistroSeleccionado;

                        if (registro != null)
                        {
                            codProveedorSeleccionado = registro["Codigo"].ToString();
                            txtPersona.Tag = codProveedorSeleccionado;
                            txtPersona.Text = registro["Proveedor"].ToString();
                            txtPersona.ForeColor = Color.Black;
                            txtVehiculo.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error");
            }
        }

        private void btnBuscarVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ BUSCAR VEHÍCULO
                frmBuscador frm = new frmBuscador
                {
                    QueryBase = @"SELECT 
                                    CodVehiculo AS Codigo,
                                    (Patente + ' - ' + Modelo) AS Vehiculo
                                  FROM Vehiculos",
                    CampoFiltro = "(Patente + Modelo)",
                    OrdenCampo = "Patente",
                    WhereAdicional = "Activo = 1"
                };

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataRow registro = frm.RegistroSeleccionado;

                    if (registro != null)
                    {
                        codVehiculoSeleccionado = registro["Codigo"].ToString();
                        txtVehiculo.Tag = codVehiculoSeleccionado;
                        txtVehiculo.Text = registro["Vehiculo"].ToString();
                        txtVehiculo.ForeColor = Color.Black;
                        cmbMetodoPago.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error");
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ VALIDACIONES

                // Validar Tipo de Movimiento
                if (cmbTipoMov.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Debe seleccionar el tipo de movimiento",
                        "Campo requerido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    cmbTipoMov.Focus();
                    return;
                }

                // Validar Método de Pago
                if (cmbMetodoPago.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Debe seleccionar el método de pago",
                        "Campo requerido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    cmbMetodoPago.Focus();
                    return;
                }

                // Validar Monto
                if (string.IsNullOrWhiteSpace(txtMonto.Text))
                {
                    MessageBox.Show(
                        "Debe ingresar el monto",
                        "Campo requerido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtMonto.Focus();
                    return;
                }

                if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
                {
                    MessageBox.Show(
                        "El monto debe ser un número válido mayor a cero",
                        "Monto inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    txtMonto.Focus();
                    txtMonto.SelectAll();
                    return;
                }

                // Validar Comentario
                if (string.IsNullOrWhiteSpace(txtComentario.Text))
                {
                    MessageBox.Show(
                        "Debe ingresar un comentario o concepto",
                        "Campo requerido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtComentario.Focus();
                    return;
                }

                // ✅ CREAR OBJETO MOVIMIENTO

                var movimiento = new LibroDiario
                {
                    IdCajaDiaria = idCajaDiaria,
                    TipoMovimiento = cmbTipoMov.SelectedItem.ToString(),
                    MetodoPago = cmbMetodoPago.SelectedItem.ToString(),
                    Concepto = txtComentario.Text.Trim(),
                    Monto = monto,
                    CodCliente = optCliente.Checked ? (txtPersona.Tag?.ToString() ?? null) : null,
                    CodProveedor = optProveedor.Checked ? (txtPersona.Tag?.ToString() ?? null) : null,
                    CodOrdenTrabajo = txtVehiculo.Tag?.ToString()
                };

                // ✅ CONFIRMACIÓN

                string tipoAccion = (movimientoAEditar == null) ? "agregar" : "modificar";
                string relacionado = "Sin asignar";

                if (optCliente.Checked && !string.IsNullOrEmpty(txtPersona.Text))
                    relacionado = $"Cliente: {txtPersona.Text}";
                else if (optProveedor.Checked && !string.IsNullOrEmpty(txtPersona.Text))
                    relacionado = $"Proveedor: {txtPersona.Text}";

                var resultado = MessageBox.Show(
                    $"¿Confirma {tipoAccion} el movimiento?\n\n" +
                    $"Tipo: {movimiento.TipoMovimiento}\n" +
                    $"Método: {movimiento.MetodoPago}\n" +
                    $"Monto: {movimiento.Monto:C}\n" +
                    $"Relacionado: {relacionado}\n" +
                    $"Concepto: {movimiento.Concepto}",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultado != DialogResult.Yes)
                    return;

                // ✅ DESHABILITAR BOTÓN
                btnGuardar.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                // ✅ EJECUTAR

                if (movimientoAEditar == null)
                {
                    // MODO AGREGAR
                    string codMovimiento = await negLibroDiario.AgregarMovimientoAsync(movimiento);

                    this.Cursor = Cursors.Default;

                    MessageBox.Show(
                        $"✅ Movimiento agregado correctamente\n\n" +
                        $"Código: {codMovimiento}\n" +
                        $"Tipo: {movimiento.TipoMovimiento}\n" +
                        $"Monto: {movimiento.Monto:C}",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    // MODO EDITAR
                    movimiento.CodMovimiento = movimientoAEditar.CodMovimiento;
                    movimiento.IdCajaDiaria = movimientoAEditar.IdCajaDiaria;

                    bool exito = await negLibroDiario.ModificarMovimientoAsync(movimiento);

                    this.Cursor = Cursors.Default;

                    if (exito)
                    {
                        MessageBox.Show(
                            $"✅ Movimiento modificado correctamente\n\n" +
                            $"Código: {movimiento.CodMovimiento}\n" +
                            $"Tipo: {movimiento.TipoMovimiento}\n" +
                            $"Monto: {movimiento.Monto:C}",
                            "Éxito",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo modificar el movimiento",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        btnGuardar.Enabled = true;
                        return;
                    }
                }

                // ✅ CERRAR FORMULARIO CON DIALOGRESULT.OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                btnGuardar.Enabled = true;

                MessageBox.Show(
                    $"❌ Error al guardar:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ConfigurarPlaceholders()
        {
            // Placeholder para txtPersona
            txtPersona.Text = "Buscar cliente...";
            txtPersona.ForeColor = Color.Gray;

            txtPersona.Enter += (s, e) =>
            {
                if (txtPersona.Text == "Buscar cliente..." || txtPersona.Text == "Buscar proveedor..." || txtPersona.Text == "Sin asignar")
                {
                    txtPersona.Text = "";
                    txtPersona.ForeColor = Color.Black;
                }
            };

            txtPersona.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPersona.Text))
                {
                    if (optCliente.Checked)
                        txtPersona.Text = "Buscar cliente...";
                    else if (optProveedor.Checked)
                        txtPersona.Text = "Buscar proveedor...";
                    else
                        txtPersona.Text = "Sin asignar";

                    txtPersona.ForeColor = Color.Gray;
                }
            };

            // Placeholder para txtVehiculo
            txtVehiculo.Text = "Buscar vehículo (opcional)...";
            txtVehiculo.ForeColor = Color.Gray;

            txtVehiculo.Enter += (s, e) =>
            {
                if (txtVehiculo.Text == "Buscar vehículo (opcional)...")
                {
                    txtVehiculo.Text = "";
                    txtVehiculo.ForeColor = Color.Black;
                }
            };

            txtVehiculo.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtVehiculo.Text))
                {
                    txtVehiculo.Text = "Buscar vehículo (opcional)...";
                    txtVehiculo.ForeColor = Color.Gray;
                }
            };
        }
    }
}
