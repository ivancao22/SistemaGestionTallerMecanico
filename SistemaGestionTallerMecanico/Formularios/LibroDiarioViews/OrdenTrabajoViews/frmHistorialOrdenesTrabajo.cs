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
using Entidades;
using Negocios;
using Utiles;

namespace Formularios.LibroDiarioViews.OrdenTrabajoViews
{
    public partial class frmHistorialOrdenesTrabajo : Form
    {
        private readonly NegOrdenTrabajo _negOrden;
        private List<OrdenTrabajo> _ordenesCache = new List<OrdenTrabajo>();

        public frmHistorialOrdenesTrabajo()
        {
            InitializeComponent();
            _negOrden = new NegOrdenTrabajo();
        }

        private void frmHistorialOrdenesTrabajo_Load(object sender, EventArgs e)
        {
            // Eventos
            txtBuscar.TextChanged -= txtBuscar_TextChanged;
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            dgvOrdenes.CellContentClick -= dgvOrdenes_CellContentClick;
            dgvOrdenes.CellContentClick += dgvOrdenes_CellContentClick;

            // Cargar datos async (no bloquear UI)
            _ = CargarOrdenesAsync();
        }

        private async Task CargarOrdenesAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // Listar órdenes activas desde la capa de negocio
                _ordenesCache = await _negOrden.ListarOrdenesActivasAsync();
                LlenarGrilla(_ordenesCache);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar órdenes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void LlenarGrilla(IEnumerable<OrdenTrabajo> datos)
        {
            dgvOrdenes.SuspendLayout();
            dgvOrdenes.Rows.Clear();

            foreach (var o in datos)
            {
                int rowIndex = dgvOrdenes.Rows.Add();
                var row = dgvOrdenes.Rows[rowIndex];

                // Ajustar los valores de las columnas según tu grilla
                row.Cells["ColCodOrden"].Value = o.CodOrden;
                row.Cells["ColVehiculo"].Value = o.ModeloVehiculo;
                row.Cells["ColPatente"].Value = o.PatenteVehiculo;
                row.Cells["ColNombre"].Value = o.NombreCliente;
                row.Cells["ColFechaOrden"].Value = o.Fecha.ToString("dd/MM/yyyy");
                row.Cells["ColDescripcion"].Value = o.DescripcionTrabajo;

                // ColGenerarPDF: si la columna es tipo Image, el diseñador ya debe tener la imagen o podes dejarla vacía.
                // Si querés mostrar un texto o ícono, asignalo aquí; si no, no hace falta.
                // row.Cells["ColGenerarPDF"].Value = tuIconoPdf;
            }

            dgvOrdenes.ResumeLayout();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text?.Trim().ToLower() ?? "";
            if (string.IsNullOrWhiteSpace(filtro))
            {
                LlenarGrilla(_ordenesCache);
                return;
            }

            var filtradas = _ordenesCache.Where(o =>
                (!string.IsNullOrWhiteSpace(o.ModeloVehiculo) && o.ModeloVehiculo.ToLower().Contains(filtro)) ||
                (!string.IsNullOrWhiteSpace(o.NombreCliente) && o.NombreCliente.ToLower().Contains(filtro)) ||
                (!string.IsNullOrWhiteSpace(o.PatenteVehiculo) && o.PatenteVehiculo.ToLower().Contains(filtro))
            ).ToList();

            LlenarGrilla(filtradas);
        }

        private async void dgvOrdenes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var colName = dgvOrdenes.Columns[e.ColumnIndex].Name;
                if (colName != "ColGenerarPDF") return;

                string codOrden = dgvOrdenes.Rows[e.RowIndex].Cells["ColCodOrden"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(codOrden))
                {
                    MessageBox.Show("No se encontró el código de la orden seleccionada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                // Obtener la orden completa (con detalles) desde la capa de negocio
                OrdenTrabajo orden = await _negOrden.ObtenerPorCodigoAsync(codOrden);
                if (orden == null)
                {
                    MessageBox.Show("No se encontró la orden completa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Title = "Guardar PDF de la Orden";
                    sfd.Filter = "PDF files (*.pdf)|*.pdf";
                    sfd.FileName = $"{codOrden}.pdf";

                    // Carpeta sugerida por defecto: Documentos/Ordenes
                    try
                    {
                        string defaultDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ordenes");
                        if (!Directory.Exists(defaultDir))
                            Directory.CreateDirectory(defaultDir);
                        sfd.InitialDirectory = defaultDir;
                    }
                    catch
                    {
                        // ignorar problemas con la carpeta por defecto, usar la predeterminada del sistema
                    }

                    var dr = sfd.ShowDialog();
                    if (dr != DialogResult.OK)
                    {
                        // Usuario canceló
                        return;
                    }

                    string rutaElegida = sfd.FileName;

                    try
                    {
                        // Si ya existe el archivo, eliminamos para evitar problemas de escritura
                        if (File.Exists(rutaElegida))
                        {
                            try { File.Delete(rutaElegida); } catch { /* si no se pudo borrar, lo sobrescribirá iTextSharp */ }
                        }

                        // Generar PDF en background para no bloquear la UI
                        await Task.Run(() => PdfHelper.GenerarPdfOrdenTrabajo(orden, rutaElegida));

                        // Abrir el PDF guardado
                        PdfHelper.AbrirPdf(rutaElegida);
                    }
                    catch (Exception exPdf)
                    {
                        MessageBox.Show($"Error al generar/guardar el PDF: {exPdf.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

    }
}
