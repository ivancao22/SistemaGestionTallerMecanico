using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Entidades;
using Formularios.Utils;
using Negocios;
using iTextSharp.text; //  PDF
using iTextSharp.text.pdf; // PDF
using PdfFont = iTextSharp.text.Font; // Alias para evitar conflicto con System.Drawing.Font
using PdfDocument = iTextSharp.text.Document;
using PdfChunk = iTextSharp.text.Chunk;
using PdfParagraph = iTextSharp.text.Paragraph;
using PdfPhrase = iTextSharp.text.Phrase;
using PdfRectangle = iTextSharp.text.Rectangle;
using Utiles;



namespace Formularios.LibroDiarioViews.OrdenTrabajoViews
{
    public partial class frmCrearOrdenTrabajo : Form
    {
        private readonly NegVehiculo negVehiculo;
        private string codClienteSeleccionado = null;
        private string codVehiculoSeleccionado = null;
        public int idCajaDiariaActual;

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public frmCrearOrdenTrabajo()
        {
            InitializeComponent();
            this.negVehiculo = new NegVehiculo();
        }

        // =====================================================
        // EVENTO: Load
        // =====================================================

        private void frmCrearOrdenTrabajo_Load(object sender, EventArgs e)
        {
            try
            {
                // Fecha actual (readonly)
                dtpFecha.Value = DateTime.Today;
                dtpFecha.Enabled = false;

                // Kilometraje inicial
                txtKm.Text = "0";

                // Configurar grilla
                ConfigurarGrilla();

                // Agregar primera fila vacía
                AgregarFilaVacia();

                // Totales en 0
                ActualizarTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar formulario: {ex.Message}", "Error");
            }
        }

        // =====================================================
        // MÉTODO: Configurar Grilla
        // =====================================================

        private void ConfigurarGrilla()
        {
            dgvDetallesOrden.AllowUserToAddRows = false;
            dgvDetallesOrden.AllowUserToDeleteRows = false;
            dgvDetallesOrden.RowHeadersVisible = false;
            dgvDetallesOrden.BackgroundColor = System.Drawing.Color.White;
            dgvDetallesOrden.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvDetallesOrden.MultiSelect = false;
            dgvDetallesOrden.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvDetallesOrden.EditMode = DataGridViewEditMode.EditOnEnter;

            // Configurar ColTipoDet (ComboBox)
            if (dgvDetallesOrden.Columns["ColTipoDet"] is DataGridViewComboBoxColumn colTipo)
            {
                colTipo.Items.Clear();
                colTipo.Items.Add("R");   // Repuesto
                colTipo.Items.Add("MO");  // Mano de Obra
                colTipo.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            }

            // Configurar ColPrecioDet (formato numérico)
            if (dgvDetallesOrden.Columns["ColPrecioDet"] != null)
            {
                dgvDetallesOrden.Columns["ColPrecioDet"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDetallesOrden.Columns["ColPrecioDet"].DefaultCellStyle.Format = "N2";
            }

            // Configurar ColOperacion (imagen)
            if (dgvDetallesOrden.Columns["ColOperacion"] is DataGridViewImageColumn colOp)
            {
                colOp.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        // =====================================================
        // MÉTODO: Agregar Fila Vacía
        // =====================================================

        private void AgregarFilaVacia()
        {
            int index = dgvDetallesOrden.Rows.Add();
            DataGridViewRow row = dgvDetallesOrden.Rows[index];

            row.Cells["ColTipoDet"].Value = "R";
            row.Cells["ColDescripcionDet"].Value = "";
            row.Cells["ColPrecioDet"].Value = 0;
            row.Cells["ColOperacion"].Value = CrearIconoMas();
            row.Tag = "NUEVA";
            row.ReadOnly = false;
        }

        // =====================================================
        // MÉTODO: Crear Icono Más (➕)
        // =====================================================

        private Bitmap CrearIconoMas()
        {
            Bitmap bmp = new Bitmap(24, 24);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Transparent);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Círculo verde
                using (SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(76, 175, 80)))
                {
                    g.FillEllipse(brush, 2, 2, 20, 20);
                }

                // Símbolo +
                using (Pen pen = new Pen(System.Drawing.Color.White, 3))
                {
                    g.DrawLine(pen, 12, 7, 12, 17);  // Vertical
                    g.DrawLine(pen, 7, 12, 17, 12);  // Horizontal
                }
            }
            return bmp;
        }

        // =====================================================
        // MÉTODO: Crear Icono Eliminar (🗑️)
        // =====================================================

        private Bitmap CrearIconoEliminar()
        {
            Bitmap bmp = new Bitmap(24, 24);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Transparent);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Círculo rojo
                using (SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(244, 67, 54)))
                {
                    g.FillEllipse(brush, 2, 2, 20, 20);
                }

                // Símbolo X
                using (Pen pen = new Pen(System.Drawing.Color.White, 3))
                {
                    g.DrawLine(pen, 8, 8, 16, 16);
                    g.DrawLine(pen, 16, 8, 8, 16);
                }
            }
            return bmp;
        }

        // =====================================================
        // EVENTO: CellContentClick (Click en celda)
        // =====================================================

        private void dgvDetallesOrden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex != dgvDetallesOrden.Columns["ColOperacion"].Index) return;

                DataGridViewRow row = dgvDetallesOrden.Rows[e.RowIndex];

                if (row.Tag?.ToString() == "NUEVA")
                {
                    // ➕ AGREGAR
                    if (!ValidarFila(row)) return;

                    // Cambiar a icono eliminar
                    row.Cells["ColOperacion"].Value = CrearIconoEliminar();
                    row.Tag = "GUARDADA";

                    AgregarFilaVacia();
                    ActualizarTotales();

                    // ✅ SOLUCIÓN: Usar BeginInvoke para diferir la edición
                    this.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            int ultimaFila = dgvDetallesOrden.Rows.Count - 1;
                            if (ultimaFila >= 0)
                            {
                                dgvDetallesOrden.CurrentCell = dgvDetallesOrden.Rows[ultimaFila].Cells["ColDescripcionDet"];
                                dgvDetallesOrden.BeginEdit(true);
                            }
                        }
                        catch { }
                    }));
                }
                else
                {
                    // 🗑️ ELIMINAR
                    var resultado = MessageBox.Show(
                        "¿Está seguro que desea eliminar este ítem?",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (resultado == DialogResult.Yes)
                    {
                        dgvDetallesOrden.Rows.RemoveAt(e.RowIndex);
                        ActualizarTotales();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        // =====================================================
        // MÉTODO: Validar Fila
        // =====================================================

        private bool ValidarFila(DataGridViewRow row)
        {
            if (row.Cells["ColTipoDet"].Value == null)
            {
                MessageBox.Show("Seleccione el tipo de movimiento", "Validación");
                dgvDetallesOrden.CurrentCell = row.Cells["ColTipoDet"];
                dgvDetallesOrden.BeginEdit(true);
                return false;
            }

            string descripcion = row.Cells["ColDescripcionDet"].Value?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Ingrese una descripción", "Validación");
                dgvDetallesOrden.CurrentCell = row.Cells["ColDescripcionDet"];
                dgvDetallesOrden.BeginEdit(true);
                return false;
            }

            string precioStr = row.Cells["ColPrecioDet"].Value?.ToString() ?? "0";
            if (!decimal.TryParse(precioStr, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio válido mayor a cero", "Validación");
                dgvDetallesOrden.CurrentCell = row.Cells["ColPrecioDet"];
                dgvDetallesOrden.BeginEdit(true);
                return false;
            }

            return true;
        }

        // =====================================================
        // EVENTO: CellEndEdit (Fin de edición)
        // =====================================================

        private void dgvDetallesOrden_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvDetallesOrden.Rows[e.RowIndex];

                if (e.ColumnIndex == dgvDetallesOrden.Columns["ColPrecioDet"].Index &&
                    row.Tag?.ToString() == "NUEVA")
                {
                    if (ValidarFila(row))
                    {
                        // ✅ SOLUCIÓN: Usar BeginInvoke aquí también
                        this.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                dgvDetallesOrden_CellContentClick(sender, new DataGridViewCellEventArgs(
                                    dgvDetallesOrden.Columns["ColOperacion"].Index,
                                    e.RowIndex
                                ));
                            }
                            catch { }
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        // =====================================================
        // EVENTO: CellValidating (Validación de celda)
        // =====================================================

        private void dgvDetallesOrden_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvDetallesOrden.Columns[e.ColumnIndex].Name == "ColPrecioDet")
                {
                    string valor = e.FormattedValue.ToString();

                    if (!string.IsNullOrWhiteSpace(valor))
                    {
                        if (!decimal.TryParse(valor, out _))
                        {
                            e.Cancel = true;
                            MessageBox.Show("Ingrese solo números", "Validación");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        // =====================================================
        // EVENTO: EditingControlShowing (Control de edición)
        // =====================================================


        private void dgvDetallesOrden_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    // ✅ PASO 1: SIEMPRE desconectar el evento KeyPress
                    tb.KeyPress -= tb_KeyPress;
                    //tb.KeyPress -= tb_KeyPressDescripcion; // Por si agregamos otro después

                    // ✅ PASO 2: Solo conectar si es la columna de Precio
                    if (dgvDetallesOrden.CurrentCell != null &&
                        dgvDetallesOrden.CurrentCell.ColumnIndex == dgvDetallesOrden.Columns["ColPrecioDet"].Index)
                    {
                        tb.KeyPress += tb_KeyPress;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en EditingControlShowing: {ex.Message}", "Error");
            }
        }

        // =====================================================
        // KeyPress para Precio (solo números)
        // =====================================================

        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo números, punto/coma decimal, backspace
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Solo un separador decimal
            TextBox tb = sender as TextBox;
            if (tb != null && (e.KeyChar == '.' || e.KeyChar == ','))
            {
                if (tb.Text.Contains(".") || tb.Text.Contains(","))
                {
                    e.Handled = true;
                }
            }
        }

        // =====================================================
        // MÉTODO: Actualizar Totales
        // =====================================================

        private void ActualizarTotales()
        {
            decimal totalRepuestos = 0;
            decimal totalManoObra = 0;

            foreach (DataGridViewRow row in dgvDetallesOrden.Rows)
            {
                if (row.Tag?.ToString() != "GUARDADA") continue;

                string tipo = row.Cells["ColTipoDet"].Value?.ToString() ?? "";
                string precioStr = row.Cells["ColPrecioDet"].Value?.ToString() ?? "0";

                if (decimal.TryParse(precioStr, out decimal precio))
                {
                    if (tipo == "R")
                        totalRepuestos += precio;
                    else if (tipo == "MO")
                        totalManoObra += precio;
                }
            }

            decimal totalGeneral = totalRepuestos + totalManoObra;

            lblTotalRepuestos.Text = $"$ {totalRepuestos:N2}";
            lblTotalMano.Text = $"$ {totalManoObra:N2}";
            lblTotalGral.Text = $"$ {totalGeneral:N2}";

            lblTotalGral.Font = new System.Drawing.Font(lblTotalGral.Font.FontFamily, 9, FontStyle.Bold);
            lblTotalGral.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
        }

        // =====================================================
        // EVENTO: Buscar Cliente
        // =====================================================

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmBuscador
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
                        txtCliente.Tag = codClienteSeleccionado;
                        txtCliente.Text = registro["Cliente"].ToString();
                        txtCliente.ForeColor = System.Drawing.Color.Black;
                        btnBuscarVehiculo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cliente: {ex.Message}", "Error");
            }
        }

        // =====================================================
        // EVENTO: Buscar Vehículo
        // =====================================================

        private void btnBuscarVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(codClienteSeleccionado))
                {
                    MessageBox.Show("Primero seleccione un cliente", "Información");
                    btnBuscarCliente.Focus();
                    return;
                }

                var frm = new frmBuscador
                {
                    QueryBase = @"SELECT 
                                    CodVehiculo AS Codigo,
                                    (Patente + ' - ' + Modelo) AS Vehiculo,
                                    ISNULL(Kilometraje, 0) AS KM
                                  FROM Vehiculos",
                    CampoFiltro = "(Patente + Modelo)",
                    OrdenCampo = "Patente",
                    WhereAdicional = $"CodCliente = '{codClienteSeleccionado}' AND Activo = 1"
                };

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataRow registro = frm.RegistroSeleccionado;
                    if (registro != null)
                    {
                        codVehiculoSeleccionado = registro["Codigo"].ToString();
                        txtVehiculo.Tag = codVehiculoSeleccionado;
                        txtVehiculo.Text = registro["Vehiculo"].ToString();
                        txtVehiculo.ForeColor = System.Drawing.Color.Black;
                        txtKm.Text = registro["KM"].ToString();
                        txtDescripcion.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar vehículo: {ex.Message}", "Error");
            }
        }

        // =====================================================
        // EVENTO: Validar Kilometraje (solo números)
        // =====================================================

        private void txtKm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // =====================================================
        // EVENTO: Vista Previa
        // =====================================================

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario()) return;

                // Armar entidad temporal (sin guardar en BD)
                var ordenTemporal = new OrdenTrabajo
                {
                    CodOrden = "PREVIO", // código temporal para vista previa
                    Fecha = dtpFecha.Value.Date,
                    CodCliente = txtCliente.Tag?.ToString(),
                    CodVehiculo = txtVehiculo.Tag?.ToString(),
                    NombreCliente = txtCliente.Text?.ToString(),
                    Kilometraje = int.TryParse(txtKm.Text, out int km) ? (int?)km : null,
                    DescripcionTrabajo = txtDescripcion.Text?.Trim(),
                    Usuario = Environment.UserName
                };

                if (txtVehiculo.Tag != null)
                {
                    Vehiculo vehiculo = new Vehiculo();
                    string codVehiculo = txtVehiculo.Tag.ToString();
                    vehiculo = negVehiculo.ObtenerVehiculoPorCodigo(codVehiculo);
                    ordenTemporal.ModeloVehiculo = vehiculo.Modelo;
                    ordenTemporal.PatenteVehiculo = vehiculo.Patente;
                }
                
                


                int reng = 1;
                foreach (DataGridViewRow row in dgvDetallesOrden.Rows)
                {
                    if (row.Tag?.ToString() != "GUARDADA") continue;
                    ordenTemporal.Detalles.Add(new OrdenTrabajoDetalle
                    {
                        Renglon = reng++,
                        Tipo = row.Cells["ColTipoDet"].Value?.ToString(),
                        Descripcion = row.Cells["ColDescripcionDet"].Value?.ToString(),
                        Precio = decimal.TryParse(row.Cells["ColPrecioDet"].Value?.ToString(), out decimal precio) ? precio : 0m
                    });
                }

                // Generar PDF temporal
                string rutaTemporal = Path.Combine(Path.GetTempPath(), $"VistaPrevia_{DateTime.Now:yyyyMMddHHmmss}.pdf");
                PdfHelper.GenerarPdfOrdenTrabajo(ordenTemporal, rutaTemporal);

                // Abrir PDF para vista previa
                PdfHelper.AbrirPdf(rutaTemporal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar vista previa: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // EVENTO: Guardar
        // =====================================================

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario()) return;

                // Deshabilitar UI
                btnGuardar.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                // Armar entidad
                var orden = new OrdenTrabajo
                {
                    Fecha = dtpFecha.Value.Date,
                    CodCliente = txtCliente.Tag?.ToString(),
                    CodVehiculo = txtVehiculo.Tag?.ToString(),
                    NombreCliente = txtCliente.Text?.ToString(),
                    Kilometraje = int.TryParse(txtKm.Text, out int km) ? (int?)km : null,
                    DescripcionTrabajo = txtDescripcion.Text?.Trim(),
                    Usuario = Environment.UserName,

                };

                if (txtVehiculo.Tag != null)
                {
                    Vehiculo vehiculo = new Vehiculo();
                    string codVehiculo = txtVehiculo.Tag.ToString();
                    vehiculo = negVehiculo.ObtenerVehiculoPorCodigo(codVehiculo);
                    orden.ModeloVehiculo = vehiculo.Modelo;
                    orden.PatenteVehiculo = vehiculo.Patente;
                }

                int reng = 1;
                foreach (DataGridViewRow row in dgvDetallesOrden.Rows)
                {
                    if (row.Tag?.ToString() != "GUARDADA") continue;
                    orden.Detalles.Add(new OrdenTrabajoDetalle
                    {
                        Renglon = reng++,
                        Tipo = row.Cells["ColTipoDet"].Value?.ToString(),
                        Descripcion = row.Cells["ColDescripcionDet"].Value?.ToString(),
                        Precio = decimal.TryParse(row.Cells["ColPrecioDet"].Value?.ToString(), out decimal precio) ? precio : 0m
                    });
                }

                var neg = new NegOrdenTrabajo();


                // Guardar (async)
                string codGenerado = await neg.GuardarOrdenTrabajoAsync(orden, idCajaDiariaActual);

                orden.CodOrden = codGenerado;

                // Generar PDF automáticamente en carpeta temporal
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Title = "Guardar PDF de la Orden de Trabajo";
                    sfd.Filter = "PDF files (*.pdf)|*.pdf";
                    sfd.FileName = $"{codGenerado}.pdf";

                    // Carpeta sugerida por defecto: Documentos/Ordenes
                    string defaultDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ordenes");
                    if (!Directory.Exists(defaultDir))
                        Directory.CreateDirectory(defaultDir);
                    sfd.InitialDirectory = defaultDir;

                    var dr = sfd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        string rutaElegida = sfd.FileName;
                        try
                        {
                            // Generar PDF en la ruta elegida por el usuario
                            PdfHelper.GenerarPdfOrdenTrabajo(orden, rutaElegida);



                            // Abrir PDF para vista previa
                            PdfHelper.AbrirPdf(rutaElegida);

                            MessageBox.Show($"Orden guardada correctamente.\nCódigo: {codGenerado}\nPDF guardado en: {rutaElegida}",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception pdfEx)
                        {
                            MessageBox.Show($"Orden guardada correctamente (Código: {codGenerado}), pero hubo un error al generar el PDF:\n{pdfEx.Message}",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        // Usuario canceló el guardado del PDF
                        MessageBox.Show($"Orden guardada correctamente.\nCódigo: {codGenerado}\n(No se generó PDF porque cancelaste el guardado)",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Indicar éxito al form llamador y cerrar
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // Mostrar error y mantener el form abierto para correcciones
                MessageBox.Show($"Error al guardar la orden: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restaurar UI
                btnGuardar.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        // =====================================================
        // MÉTODO: Validar Formulario
        // =====================================================

        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(codClienteSeleccionado))
            {
                MessageBox.Show("Debe seleccionar un cliente", "Validación");
                btnBuscarCliente.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(codVehiculoSeleccionado))
            {
                MessageBox.Show("Debe seleccionar un vehículo", "Validación");
                btnBuscarVehiculo.Focus();
                return false;
            }

            int cantidadItems = dgvDetallesOrden.Rows.Cast<DataGridViewRow>()
                .Count(r => r.Tag?.ToString() == "GUARDADA");

            if (cantidadItems == 0)
            {
                MessageBox.Show("Debe agregar al menos un ítem", "Validación");
                dgvDetallesOrden.Focus();
                return false;
            }

            return true;
        }

        private void btnAbrirHistorial_Click(object sender, EventArgs e)
        {
            //abrir frm de visor de ordenes, donde se listan todas las ordenes, y se pueden volver a generar los pdf 
            try
            {
                using (var frm = new frmHistorialOrdenesTrabajo())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
