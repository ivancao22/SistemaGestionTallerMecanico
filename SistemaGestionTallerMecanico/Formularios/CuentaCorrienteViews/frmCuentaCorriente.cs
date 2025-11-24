using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Entidades;
using Formularios.Utils;
using Negocios;
using Utiles;

namespace Formularios.CuentaCorrienteViews
{
    public partial class frmCuentaCorriente : Form
    {
        private readonly NegCuentaCorriente _negCuentaCorriente;
        private string _codigoSeleccionado = null;
        private string _tipoSeleccionado = null; // "Cliente" o "Proveedor"
        private List<LibroDiario> _movimientosCache = new List<LibroDiario>();
        private string _nombreSeleccionado = null;

        public frmCuentaCorriente()
        {
            InitializeComponent();
            _negCuentaCorriente = new NegCuentaCorriente();
        }

        private async void frmCuentaCorriente_Load(object sender, EventArgs e)
        {
            // inicializaciones si hacen falta
            lblBalance.Text = "Balance : $ 0,00";
            lblBalance.ForeColor = Color.Green;
            // opcional: configurar columnas dgvCuentaCorriente (formatos)
            dgvCuentaCorriente.Columns["ColImporte"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCuentaCorriente.Columns["ColFechaMovimiento"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            // no cargamos nada hasta seleccionar entidad
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var buscador = new frmBuscador
                {
                    // Query: devolver código y nombre y tipo
                    QueryBase = @"
                        SELECT CodCliente AS Codigo, (Nombre + ' ' + Apellido) AS Nombre, 'Cliente' AS Tipo
                        FROM Clientes
                        WHERE Activo = 1
                        UNION ALL
                        SELECT CodProveedor AS Codigo, ISNULL(RazonSocial, Nombre + ' ' + Apellido) AS Nombre, 'Proveedor' AS Tipo
                        FROM Proveedores
                        WHERE Activo = 1
                    ",
                    CampoFiltro = "Nombre",
                    OrdenCampo = "Nombre"
                };

                if (buscador.ShowDialog() == DialogResult.OK)
                {
                    DataRow row = buscador.RegistroSeleccionado;
                    if (row != null)
                    {
                        _codigoSeleccionado = row["Codigo"].ToString();
                        _tipoSeleccionado = row["Tipo"].ToString();
                        _nombreSeleccionado = row["Nombre"].ToString();

                        txtBusqueda.Text = _nombreSeleccionado;
                        // Cargar grilla para la entidad seleccionada
                        _ = CargarGrillaAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar entidad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarGrillaAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_codigoSeleccionado) || string.IsNullOrWhiteSpace(_tipoSeleccionado))
                {
                    MessageBox.Show("Seleccione un cliente o proveedor primero.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Bloquear UI mínimo
                this.Cursor = Cursors.WaitCursor;
                btnBuscar.Enabled = false;
                btnGenerarExcel.Enabled = false;
                btnGenerarPDF.Enabled = false;

                // Llamada a la capa de negocio para obtener movimientos de la entidad (activo = 1)
                _movimientosCache = await _negCuentaCorriente.ListarPorEntidadAsync(_tipoSeleccionado, _codigoSeleccionado);

                dgvCuentaCorriente.Rows.Clear();

                decimal balance = 0m;

                foreach (var mov in _movimientosCache.OrderBy(m => m.Fecha))
                {
                    int idx = dgvCuentaCorriente.Rows.Add();
                    var row = dgvCuentaCorriente.Rows[idx];

                    // Fecha
                    row.Cells["ColFechaMovimiento"].Value = mov.Fecha;

                    // Tipo visual según contexto (perspectiva de la empresa)
                    string displayTipo;
                    if (_tipoSeleccionado == "Cliente")
                    {
                        // Cliente: PAGA (INGRESO para la empresa) / DEBE (EGRESO para la empresa)
                        displayTipo = string.Equals(mov.TipoMovimiento, "INGRESO", StringComparison.OrdinalIgnoreCase) ? "PAGA" : "DEBE";
                    }
                    else if (_tipoSeleccionado == "Proveedor")
                    {
                        // Proveedor: desde el punto de vista de la empresa
                        // EGRESO (nos sale plata) => "SE LE DEBE" (le debemos) ; INGRESO => "SE LE PAGA"
                        displayTipo = string.Equals(mov.TipoMovimiento, "EGRESO", StringComparison.OrdinalIgnoreCase) ? "SE LE PAGA" : "SE LE DEBE";
                    }
                    else
                    {
                        displayTipo = mov.TipoMovimiento?.ToUpperInvariant() ?? "";
                    }
                    row.Cells["ColTipoMovimiento"].Value = displayTipo;

                    // CodMovimiento
                    row.Cells["ColCodMovimiento"].Value = mov.CodMovimiento;

                    // Obtener signo robusto (revisa Signo o SignoMovimiento si existe)
                    int signo = mov.SignoMovimiento;

                    decimal importeConSigno;
                    if (mov.Concepto.ToLower().Contains("presupuesto"))
                    {
                        // Presupuestos siempre se muestran positivos (sin importar el tipo de movimiento)
                        importeConSigno = mov.Monto;
                    }
                    else
                    {
                        // Importe desde la perspectiva de la empresa (signo * monto)                    
                        importeConSigno = signo * mov.Monto;
                    }
                    

                    // Mostrar importe: si negativo mostrar con prefijo "-" y color rojo, si positivo verde
                    if (importeConSigno < 0m)
                    {
                        row.Cells["ColImporte"].Value = "-" + Math.Abs(importeConSigno).ToString("C2", CultureInfo.CurrentCulture);
                        row.Cells["ColImporte"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        row.Cells["ColImporte"].Value = importeConSigno.ToString("C2", CultureInfo.CurrentCulture);
                        row.Cells["ColImporte"].Style.ForeColor = Color.Green;
                    }

                    // Persona (nombre ya conocido)
                    row.Cells["ColPersona"].Value = _nombreSeleccionado;

                    // Descripcion
                    row.Cells["ColDescripcionMovimiento"].Value = mov.Concepto;

                    // Colorear tipo (INGRESO verde, EGRESO rojo) para mantener consistencia visual en la columna TipoMovimiento
                    if (string.Equals(mov.TipoMovimiento, "INGRESO", StringComparison.OrdinalIgnoreCase) && mov.CodProveedor == null)
                    {
                        row.Cells["ColTipoMovimiento"].Style.ForeColor = Color.Green;
                        row.Cells["ColTipoMovimiento"].Style.Font = new Font(dgvCuentaCorriente.Font, FontStyle.Bold);
                    }
                    else
                    {
                        row.Cells["ColTipoMovimiento"].Style.ForeColor = Color.Red;
                        row.Cells["ColTipoMovimiento"].Style.Font = new Font(dgvCuentaCorriente.Font, FontStyle.Bold);
                    }

                    //Si no es presupuesto, acumular al balance, el presupuesto no deberia afectar al balance, ya que se considera que asi como se le genero una deuda al cliente, se cancelo con el pago al momento de generar el presupuesto
                    if (!mov.Concepto.ToLower().Contains("presupuesto"))
                    {
                        // acumular balance (ya en perspectiva de la empresa)
                        balance += importeConSigno;
                    }

                }

                // Actualizar label de balance
                ActualizarLabelBalance(balance);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cuenta corriente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restaurar UI
                this.Cursor = Cursors.Default;
                btnBuscar.Enabled = true;
                btnGenerarExcel.Enabled = true;
                btnGenerarPDF.Enabled = true;
            }
        }
        private void ActualizarLabelBalance(decimal balance)
        {
            // Formateo del importe sin signo
            string importeFormateado = Math.Abs(balance).ToString("C2", CultureInfo.CurrentCulture);

            // Normalizar el tipo seleccionado para comparaciones
            string tipo = (_tipoSeleccionado ?? "").Trim();

            if (balance == 0m)
            {
                lblBalance.Text = $"Balance : {importeFormateado} (Saldo 0)";
                lblBalance.ForeColor = Color.Black;
                return;
            }

            // Perspectiva de la EMPRESA:
            // balance > 0  => la empresa tiene posición positiva frente a la entidad (la empresa está a favor)
            // balance < 0  => la entidad tiene posición positiva (la empresa le debe)
            if (balance > 0m)
            {
                lblBalance.Text = $"Balance : {importeFormateado} A FAVOR DE LA EMPRESA";
                lblBalance.ForeColor = Color.Green;
            }
            else
            {
                string entidadUpper = string.IsNullOrWhiteSpace(tipo) ? "ENTIDAD" : tipo.ToUpperInvariant();
                string preposicion = (entidadUpper == "CLIENTE" || entidadUpper == "PROVEEDOR") ? "DEL" : "DE LA";
                lblBalance.Text = $"Balance : {importeFormateado} A FAVOR {preposicion} {entidadUpper}";
                lblBalance.ForeColor = Color.Red;
            }
        }
        private async void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            try
            {

                if (_movimientosCache == null || !_movimientosCache.Any())
                {
                    MessageBox.Show("No hay movimientos para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Title = "Guardar Cuenta Corriente (PDF)";
                    sfd.Filter = "PDF files (*.pdf)|*.pdf";
                    sfd.FileName = $"{_tipoSeleccionado}_{_codigoSeleccionado}_CuentaCorriente_{DateTime.Now:yyyyMMdd}.pdf";
                    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    string ruta = sfd.FileName;
                    this.Cursor = Cursors.WaitCursor;
                    btnGenerarPDF.Enabled = false;

                    try
                    {
                        // Ejecutar generación en background pero esperar su finalización (no bloquear UI)
                        await Task.Run(() =>
                        {
                            // Llama a tu helper síncrono
                            PdfHelper.GenerarPdfCuentaCorriente(_nombreSeleccionado ?? _codigoSeleccionado, _movimientosCache, ruta);
                        });

                        // Al llegar aquí estamos en el hilo de UI (await retorna al contexto)
                        try { PdfHelper.AbrirPdf(ruta); } catch { /* ignorar si no se puede abrir */ }

                        MessageBox.Show("PDF generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception exPdf)
                    {
                        MessageBox.Show($"Error generando PDF: {exPdf.GetBaseException().Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                        btnGenerarPDF.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Error al generar PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_movimientosCache == null || !_movimientosCache.Any())
                {
                    MessageBox.Show("No hay movimientos para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Title = "Guardar Cuenta Corriente (Excel)";
                    sfd.Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv";
                    sfd.FileName = $"{_tipoSeleccionado}_{_codigoSeleccionado}_CuentaCorriente_{DateTime.Now:yyyyMMdd}.xlsx";
                    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    string ruta = sfd.FileName;

                    // Si tenés un ExcelHelper / Export helper puedes llamarlo. Si no, crear CSV simple:
                    if (Path.GetExtension(ruta).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        ExportarCsv(ruta, _movimientosCache);
                        MessageBox.Show("CSV exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Intentar usar ExcelHelper si existe:
                        try
                        {
                            ExcelHelper.ExportCuentaCorrienteExcel(_nombreSeleccionado ?? _codigoSeleccionado, _movimientosCache, ruta);
                            MessageBox.Show("Excel generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            // Fallback a CSV si no está disponible
                            ExportarCsv(ruta.Replace(".xlsx", ".csv"), _movimientosCache);
                            MessageBox.Show("Excel no disponible. Generado CSV como alternativa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarCsv(string ruta, List<LibroDiario> movimientos)
        {
            using (var sw = new StreamWriter(ruta, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine("Fecha;Tipo movimiento;CodMovimiento;Importe;Persona;Descripcion");
                foreach (var m in movimientos.OrderBy(m => m.Fecha))
                {
                    string tipoVis = (_tipoSeleccionado == "Cliente")
                        ? (string.Equals(m.TipoMovimiento, "INGRESO", StringComparison.OrdinalIgnoreCase) ? "PAGA" : "DEBE")
                        : (_tipoSeleccionado == "Proveedor")
                            ? (string.Equals(m.TipoMovimiento, "INGRESO", StringComparison.OrdinalIgnoreCase) ? "SE LE DEBE" : "SE LE PAGA")
                            : (m.TipoMovimiento?.ToUpperInvariant() ?? "");

                    sw.WriteLine($"{m.Fecha:yyyy-MM-dd HH:mm};{tipoVis};{m.CodMovimiento};{m.Monto.ToString("F2", CultureInfo.InvariantCulture)};{_nombreSeleccionado};{(m.Concepto ?? "").Replace(';', ',')}");
                }
            }
        }
    }
}
