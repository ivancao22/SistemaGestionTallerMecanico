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
        private readonly bool esAperturaCaja;

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

        // Clase auxiliar para mostrar label amigable y mantener el valor real (INGRESO/EGRESO)
        private class TipoComboItem
        {
            public string Display { get; }
            public string Value { get; } // "INGRESO" or "EGRESO"

            public TipoComboItem(string display, string value)
            {
                Display = display;
                Value = value;
            }

            public override string ToString() => Display;
        }

        // Constructor para AGREGAR
        public frmAgregarMovimiento(int idCajaDiaria)
        {
            InitializeComponent();
            this.negLibroDiario = new NegLibroDiario();

            // Inicializar servicios auxiliares para evitar NullReference si se usan
            this.negClientes = new NegClientes();
            this.negProveedores = new NegProveedor();
            this.negVehiculos = new NegVehiculo();

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
            this.esAperturaCaja = movimiento.esAperturaCaja;
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
                //cmbMetodoPago.Items.Add(MetodoPago.CHEQUE);
                cmbMetodoPago.Items.Add("NO APLICA");
                cmbMetodoPago.SelectedIndex = 0; // EFECTIVO por defecto

                // Poblamos el combo según el contexto inicial (optCliente por defecto)
                PoblacionTipoSegunContexto();

                // Configurar RadioButtons: cliente por defecto
                optCliente.Checked = true;

                // Suscribir evento común para manejar cambio de contexto (evita duplicar lógica)
                optCliente.CheckedChanged -= RadioContext_CheckedChanged;
                optCliente.CheckedChanged += RadioContext_CheckedChanged;
                optProveedor.CheckedChanged -= RadioContext_CheckedChanged;
                optProveedor.CheckedChanged += RadioContext_CheckedChanged;
                optOtro.CheckedChanged -= RadioContext_CheckedChanged;
                optOtro.CheckedChanged += RadioContext_CheckedChanged;

                // Asegurar que el estado inicial de enabled/disabled de controles se aplique
                RadioContext_CheckedChanged(this, EventArgs.Empty);


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

        // Evento común para cuando cambie el contexto Cliente/Proveedor/Otro
        private void RadioContext_CheckedChanged(object sender, EventArgs e)
        {
            // Limpiar texto/tag y repoblar el combo tipo
            txtPersona.Clear();
            txtPersona.Tag = null;
            codClienteSeleccionado = null;
            codProveedorSeleccionado = null;

            if (optCliente.Checked || optProveedor.Checked)
            {
                txtPersona.Enabled = true;
                btnBuscarPersona.Enabled = true;

                txtVehiculo.Enabled = true;
                btnBuscarVehiculo.Enabled = true;
            }
            else // optOtro.Checked
            {
                txtVehiculo.Clear();
                txtVehiculo.Tag = null;
                codVehiculoSeleccionado = null;

                txtPersona.Enabled = false;
                btnBuscarPersona.Enabled = false;

                txtVehiculo.Enabled = false;
                btnBuscarVehiculo.Enabled = false;
            }

            PoblacionTipoSegunContexto();
            ConfigurarPlaceholders();
        }

        // Población del combo Tipo según el contexto seleccionado, esto es para que los tipos de movimiento sean mas intuitivos para el cliente
        private void PoblacionTipoSegunContexto()
        {
            cmbTipoMov.Items.Clear();

            if (optCliente.Checked)
            {
                // Cliente: "DEBE" (EGRESO) / "PAGA" (INGRESO)
                cmbTipoMov.Items.Add(new TipoComboItem("DEBE", "EGRESO"));
                cmbTipoMov.Items.Add(new TipoComboItem("PAGA", "INGRESO"));
                cmbTipoMov.SelectedIndex = 1; // por defecto PAGA (ingreso) - ajustar si querés otro default
            }
            else if (optProveedor.Checked)
            {
                // Proveedor: "SE LE PAGA" (INGRESO PORQUE ESTOY CANCELANDO LO QUE LE DEBIA), "SE LE DEBE" (EGRESO PORQUE ESTOY EN DEUDA CON EL PROVEEDOR)
                cmbTipoMov.Items.Add(new TipoComboItem("SE LE PAGA", "EGRESO"));
                cmbTipoMov.Items.Add(new TipoComboItem("SE LE DEBE", "INGRESO"));    
                cmbTipoMov.SelectedIndex = 0; // por defecto SE LE PAGA
            }
            else // Otro
            {
                cmbTipoMov.Items.Add(new TipoComboItem("INGRESO", "INGRESO"));
                cmbTipoMov.Items.Add(new TipoComboItem("EGRESO", "EGRESO"));
                cmbTipoMov.SelectedIndex = 0;
            }
        }


        private void CargarDatosParaEditar()
        {
            try
            {
                if (!string.IsNullOrEmpty(movimientoAEditar.CodCliente))
                    optCliente.Checked = true;
                else if (!string.IsNullOrEmpty(movimientoAEditar.CodProveedor))
                    optProveedor.Checked = true;
                else
                    optOtro.Checked = true;

                // Poblar el combo segun el contexto
                PoblacionTipoSegunContexto();

                // Seleccionar en el combo el item cuyo Value == movimientoAEditar.TipoMovimiento
                SeleccionarItemComboTipoPorValorGuardado(movimientoAEditar.TipoMovimiento);

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
                        // si lo tenías habilitado
                        // cmbMetodoPago.SelectedItem = MetodoPago.CHEQUE;
                        break;
                    case "NO APLICA":
                        // Seleccionamos la string que agregamos
                        for (int i = 0; i < cmbMetodoPago.Items.Count; i++)
                            if (cmbMetodoPago.Items[i] is string s && s == "NO APLICA")
                            {
                                cmbMetodoPago.SelectedIndex = i;
                                break;
                            }
                        break;
                    default:
                        // fallback: si viene null o inesperado, dejar el primer item
                        if (cmbMetodoPago.Items.Count > 0 && !(cmbMetodoPago.SelectedItem is TipoComboItem))
                            cmbMetodoPago.SelectedIndex = 0;
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
                if (!string.IsNullOrEmpty(movimientoAEditar.CodVehiculo))
                {
                    // TODO: Cargar datos del vehículo
                    Vehiculo vehiculo = negVehiculos.ObtenerVehiculoPorCodigo(movimientoAEditar.CodVehiculo);
                    codVehiculoSeleccionado = movimientoAEditar.CodVehiculo;
                    txtVehiculo.Tag = movimientoAEditar.CodVehiculo;
                    txtVehiculo.Text = vehiculo.Patente + "-" + vehiculo.Modelo?? "Vehículo asociado";
                }

                revisarEsAperturaCaja();

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

        private void SeleccionarItemComboTipoPorValorGuardado(string valorGuardado)
        {
            if (string.IsNullOrWhiteSpace(valorGuardado)) return;

            for (int i = 0; i < cmbTipoMov.Items.Count; i++)
            {
                if (cmbTipoMov.Items[i] is TipoComboItem item)
                {
                    if (string.Equals(item.Value, valorGuardado, StringComparison.OrdinalIgnoreCase))
                    {
                        cmbTipoMov.SelectedIndex = i;
                        return;
                    }
                }
                else
                {
                    if (string.Equals(cmbTipoMov.Items[i].ToString(), valorGuardado, StringComparison.OrdinalIgnoreCase))
                    {
                        cmbTipoMov.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        private void optCliente_CheckedChanged(object sender, EventArgs e)
        {
            // ahora el cambio de contexto lo maneja RadioContext_CheckedChanged
            ConfigurarPlaceholders();
        }

        private void optProveedor_CheckedChanged(object sender, EventArgs e)
        {
            // ahora el cambio de contexto lo maneja RadioContext_CheckedChanged
            ConfigurarPlaceholders();
        }

        private void optOtro_CheckedChanged(object sender, EventArgs e)
        {
            // ahora el cambio de contexto lo maneja RadioContext_CheckedChanged
            ConfigurarPlaceholders();
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
                frmBuscador frm = new frmBuscador();

                // ✅ BUSCAR VEHÍCULO
                if (optCliente.Checked)
                {
                    frm.QueryBase = @"SELECT 
                                    CodVehiculo AS Codigo,
                                    (Patente + ' - ' + Modelo) AS Vehiculo
                                  FROM Vehiculos";
                    frm.CampoFiltro = "(Patente + Modelo)";
                    frm.OrdenCampo = "Patente";
                    frm.WhereAdicional = "Activo = 1 and CodCliente = '" + txtPersona.Tag.ToString() + "'";              
                }
                else
                {
                    frm.QueryBase = @"SELECT 
                                    CodVehiculo AS Codigo,
                                    (Patente + ' - ' + Modelo) AS Vehiculo
                                  FROM Vehiculos";
                    frm.CampoFiltro = "(Patente + Modelo)";
                    frm.OrdenCampo = "Patente";
                    frm.WhereAdicional = "Activo = 1 ";               
                }
                    

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

                // Antes de crear objeto movimiento, obtener el tipo real (INGRESO/EGRESO)
                string tipoSeleccionado;
                if (cmbTipoMov.SelectedItem is TipoComboItem tci)
                    tipoSeleccionado = tci.Value;
                else
                    tipoSeleccionado = cmbTipoMov.SelectedItem?.ToString()?.ToUpperInvariant() ?? "INGRESO"; // fallback

                // Luego crear movimiento usando tipoSeleccionado:
                var movimiento = new LibroDiario
                {
                    IdCajaDiaria = idCajaDiaria,
                    TipoMovimiento = tipoSeleccionado,
                    MetodoPago = cmbMetodoPago.SelectedItem.ToString(),
                    Concepto = txtComentario.Text.Trim(),
                    Monto = monto,
                    CodCliente = optCliente.Checked ? (txtPersona.Tag?.ToString() ?? null) : null,
                    CodProveedor = optProveedor.Checked ? (txtPersona.Tag?.ToString() ?? null) : null,
                    CodOrdenTrabajo = null,
                    CodVehiculo = txtVehiculo.Tag?.ToString(),
                    SignoMovimiento = CalcularSignoSeleccionado(),
                };


                // CONFIRMACIÓN

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
                    movimiento.esAperturaCaja = this.esAperturaCaja;

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

        public void revisarEsAperturaCaja()
        {
            if (esAperturaCaja)
            {
                optCliente.Enabled = false;
                optProveedor.Enabled = false;
                txtPersona.Enabled = false;
                txtVehiculo.Enabled = false;
                btnBuscarPersona.Enabled = false;
                btnBuscarVehiculo.Enabled = false;
                txtComentario.Enabled = false;
                cmbMetodoPago.Enabled = false;
                cmbTipoMov.Enabled = false;
            }
        }

        private void cmbTipoMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMetodoPagoEnabledState();
        }

        // Lógica que decide si habilitar/deshabilitar cmbMetodoPago según contexto y opción seleccionada
        private void UpdateMetodoPagoEnabledState()
        {
            // Si el combo no está poblado, no hacemos nada
            if (cmbTipoMov.Items.Count == 0) return;

            // Obtener el item seleccionado del tipo (puede ser TipoComboItem)
            string selectedDisplay = cmbTipoMov.SelectedItem?.ToString() ?? "";
            string selectedValue = null; // "INGRESO"/"EGRESO" cuando usamos TipoComboItem

            if (cmbTipoMov.SelectedItem is TipoComboItem tci)
                selectedValue = tci.Value;

            // Regla:
            // - Si contexto Cliente: habilitar solo cuando display == "PAGA" (o value == "INGRESO")
            // - Si contexto Proveedor: habilitar solo cuando display == "SE LE PAGA" (o value == "EGRESO")
            // - Si contexto Otro: siempre habilitado

            bool habilitarMetodo = true;

            if (optCliente.Checked)
            {
                // habilitar solo si el item implica que el cliente paga (value == "INGRESO")
                habilitarMetodo = string.Equals(selectedValue, "INGRESO", StringComparison.OrdinalIgnoreCase);
            }
            else if (optProveedor.Checked)
            {
                // habilitar solo si estamos pagando al proveedor (es decir display "SE LE PAGA", value == "EGRESO")
                habilitarMetodo = string.Equals(selectedValue, "EGRESO", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                // optOtro -> habilitado siempre
                habilitarMetodo = true;
            }

            // Aplicar estado UI
            cmbMetodoPago.Enabled = habilitarMetodo;
            btnBuscarPersona.Enabled = optCliente.Checked || optProveedor.Checked; // mantenemos lógica de búsqueda

            // Si está deshabilitado, asignar "NO APLICA" como seleccionado
            if (!habilitarMetodo)
            {
                // Si existe el item "NO APLICA" en items, seleccionarlo
                for (int i = 0; i < cmbMetodoPago.Items.Count; i++)
                {
                    if (cmbMetodoPago.Items[i] is string s && s == "NO APLICA")
                    {
                        cmbMetodoPago.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                // Si fue habilitado y actualmente está en "NO APLICA", seleccionar por defecto el primer método disponible
                if (cmbMetodoPago.SelectedItem is string s2 && s2 == "NO APLICA")
                {
                    // seleccionar EFECTIVO si existe (índice 0 según cómo lo llenamos)
                    cmbMetodoPago.SelectedIndex = 0;
                }
            }
        }

        private int CalcularSignoSeleccionado()
        {
            // Obtener value del combo (INGRESO/EGRESO) de forma robusta
            string value = null;
            if (cmbTipoMov.SelectedItem is TipoComboItem tci)
                value = tci.Value?.ToUpperInvariant();
            else
                value = cmbTipoMov.SelectedItem?.ToString()?.ToUpperInvariant();

            // Normalizar tipo de entidad
            string tipoEntidad;
            if (optCliente.Checked) tipoEntidad = "CLIENTE";
            else if (optProveedor.Checked) tipoEntidad = "PROVEEDOR";
            else tipoEntidad = "OTRO";

            // Aplicar reglas
            if (tipoEntidad == "CLIENTE")
            {
                // INGRESO (cliente paga) => reduce deuda => signo -1
                // EGRESO  (cliente debe) => aumenta deuda => signo +1
                return string.Equals(value, "INGRESO", StringComparison.OrdinalIgnoreCase) ? -1 : +1;
            }
            else if (tipoEntidad == "PROVEEDOR")
            {
                // EGRESO (nosotros pagamos al proveedor) => reduce deuda => signo +1
                // INGRESO (se registra deuda/compras al proveedor) => aumenta deuda => signo -1
                return string.Equals(value, "EGRESO", StringComparison.OrdinalIgnoreCase) ? +1 : -1;
            }
            else
            {
                // Otro: IN/EG se interpretan según flujo de caja (INGRESO positiva)
                return string.Equals(value, "INGRESO", StringComparison.OrdinalIgnoreCase) ? +1 : -1;
            }
        }

    }
}
