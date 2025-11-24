using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Entidades;

namespace Utiles
{
    public class ExcelHelper
    {
        /// <summary>
        /// Exporta un DataGridView a Excel (.xlsx) en el formato compacto que usás en Libro Diario.
        /// El método no muestra SaveFileDialog: recibe la ruta completa donde guardar el archivo.
        /// </summary>
        /// <param name="dgv">DataGridView con los datos</param>
        /// <param name="rutaArchivo">Ruta completa destino (.xlsx)</param>
        /// <param name="fechaActual">fecha para el nombre / cabecera (opcional)</param>
        public static void ExportFromDataGridView(DataGridView dgv, string rutaArchivo, DateTime fechaActual)
        {
            if (dgv == null) throw new ArgumentNullException(nameof(dgv));
            if (string.IsNullOrWhiteSpace(rutaArchivo)) throw new ArgumentNullException(nameof(rutaArchivo));

            var directory = Path.GetDirectoryName(rutaArchivo);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Libro Diario");

                // Encabezados según tu ejemplo
                int col = 1;
                worksheet.Cell(1, col++).Value = "Nombre";
                worksheet.Cell(1, col++).Value = "Tipo Usuario";
                worksheet.Cell(1, col++).Value = "Tipo Movimiento";
                worksheet.Cell(1, col++).Value = "Fecha Movimiento";
                worksheet.Cell(1, col++).Value = "Medio Pago";
                worksheet.Cell(1, col++).Value = "Importe";
                worksheet.Cell(1, col++).Value = "Descripción";

                // Estilo encabezado
                var headerRange = worksheet.Range(1, 1, 1, col - 1);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int row = 2;
                decimal totalCaja = 0m;

                foreach (DataGridViewRow dgvRow in dgv.Rows)
                {
                    if (dgvRow.IsNewRow) continue;

                    col = 1;

                    // Nombre
                    worksheet.Cell(row, col++).Value = dgvRow.Cells["ColNombre"]?.Value?.ToString() ?? "";

                    // Tipo Usuario
                    worksheet.Cell(row, col++).Value = dgvRow.Cells["ColTipoUser"]?.Value?.ToString() ?? "";

                    // Tipo Movimiento
                    string tipoMov = dgvRow.Cells["ColTipoMovimiento"]?.Value?.ToString() ?? "";
                    worksheet.Cell(row, col++).Value = tipoMov;

                    // Fecha Movimiento
                    worksheet.Cell(row, col++).Value = dgvRow.Cells["ColFechaMovimiento"]?.Value?.ToString() ?? "";

                    // Medio Pago
                    string medioPago = dgvRow.Cells["ColMedioPago"]?.Value?.ToString() ?? "";
                    worksheet.Cell(row, col++).Value = medioPago;

                    // Importe: limpiamos como en tu snippet y escribimos como número si podemos
                    string importeStr = dgvRow.Cells["ColImporte"]?.Value?.ToString() ?? "$0";

                    // Normalizar: quitar símbolo y puntos de miles, transformar coma decimal a punto (invariante)
                    string cleaned = importeStr.Replace("$", "").Replace(".", "").Replace(",", ".");
                    cleaned = cleaned.Trim();

                    if (decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal importe))
                    {
                        worksheet.Cell(row, col).Value = importe;
                        worksheet.Cell(row, col).Style.NumberFormat.Format = "$#,##0.00";

                        // Calcular total caja según regla (tu snippet)
                        if (tipoMov == "INGRESO" && medioPago == "EFECTIVO")
                            totalCaja += importe;
                        else if (tipoMov == "EGRESO" && medioPago == "EFECTIVO")
                            totalCaja -= importe;
                    }
                    else
                    {
                        worksheet.Cell(row, col).Value = importeStr;
                    }
                    col++;

                    // Descripción
                    worksheet.Cell(row, col++).Value = dgvRow.Cells["ColDescripcion"]?.Value?.ToString() ?? "";

                    row++;
                }

                // Total al final
                row++; // fila vacía
                worksheet.Cell(row, 5).Value = "TOTAL CAJA:";
                worksheet.Cell(row, 5).Style.Font.Bold = true;
                worksheet.Cell(row, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                worksheet.Cell(row, 6).Value = totalCaja;
                worksheet.Cell(row, 6).Style.NumberFormat.Format = "$#,##0.00";
                worksheet.Cell(row, 6).Style.Font.Bold = true;
                worksheet.Cell(row, 6).Style.Fill.BackgroundColor = XLColor.LightYellow;

                // Ajustar columnas
                worksheet.Columns().AdjustToContents();

                // Guardar
                workbook.SaveAs(rutaArchivo);
            }

            // Intentar abrir el archivo (silencioso en caso de error)
            try
            {
                var psi = new ProcessStartInfo(rutaArchivo) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch { /* no bloqueante */ }
        }

        /// <summary>
        /// Exporta una colección de movimientos (cuenta corriente) a Excel.
        /// Útil para llamar desde la lógica del formulario pasando la lista de movimientos.
        /// </summary>
        /// <param name="movimientos">Colección de movimientos</param>
        /// <param name="rutaArchivo">Ruta destino (.xlsx)</param>
        /// <param name="fechaActual">Fecha actual para nombre (opcional)</param>
        public static void ExportCuentaCorrienteExcel(
     string nombre,
     List<LibroDiario> movimientos,
     string rutaDestino
 )
        {
            if (string.IsNullOrWhiteSpace(rutaDestino))
                throw new ArgumentNullException(nameof(rutaDestino));

            if (movimientos == null)
                movimientos = new List<LibroDiario>();

            var directory = Path.GetDirectoryName(rutaDestino);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Cuenta Corriente");

                // ==========================================================
                // ENCABEZADO
                // ==========================================================
                ws.Cell("A1").Value = $"Cuenta Corriente - {nombre}";
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 16;

                ws.Cell("A2").Value = $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}";
                ws.Cell("A2").Style.Font.Italic = true;

                int row = 4;

                // ==========================================================
                // ENCABEZADOS TABLA (idénticos al PDF)
                // ==========================================================
                ws.Cell(row, 1).Value = "Fecha";
                ws.Cell(row, 2).Value = "Tipo movimiento";
                ws.Cell(row, 3).Value = "Importe";
                ws.Cell(row, 4).Value = "Descripción";

                var header = ws.Range(row, 1, row, 4);
                header.Style.Font.Bold = true;
                header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                header.Style.Fill.BackgroundColor = XLColor.LightGray;

                row++;

                // ==========================================================
                // DETALLE DE MOVIMIENTOS
                // ==========================================================
                decimal balance = 0m;
                int totalMovimientos = movimientos.Count;

                foreach (var m in movimientos.OrderBy(x => x.Fecha))
                {
                    // Fecha
                    ws.Cell(row, 1).Value = m.Fecha.ToString("dd/MM/yyyy HH:mm");

                    // ======= MISMA TRANSFORMACIÓN QUE EN EL PDF ===========
                    string displayTipo;

                    if (!string.IsNullOrWhiteSpace(m.CodCliente))
                    {
                        // Cliente:
                        // INGRESO -> PAGA
                        // EGRESO  -> DEBE
                        displayTipo = string.Equals(m.TipoMovimiento, "INGRESO", StringComparison.OrdinalIgnoreCase)
                            ? "PAGA"
                            : "DEBE";
                    }
                    else if (!string.IsNullOrWhiteSpace(m.CodProveedor))
                    {
                        // Proveedor:
                        // EGRESO  -> SE LE PAGA
                        // INGRESO -> SE LE DEBE
                        displayTipo = string.Equals(m.TipoMovimiento, "EGRESO", StringComparison.OrdinalIgnoreCase)
                            ? "SE LE PAGA"
                            : "SE LE DEBE";
                    }
                    else
                    {
                        displayTipo = m.TipoMovimiento?.ToUpperInvariant() ?? "";
                    }

                    ws.Cell(row, 2).Value = displayTipo;

                    // Importe
                    ws.Cell(row, 3).Value = m.Monto;
                    ws.Cell(row, 3).Style.NumberFormat.Format = "$ #,##0.00";
                    ws.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    // Descripción
                    ws.Cell(row, 4).Value = m.Concepto ?? "";

                    // ==========================================================
                    // BALANCE (idéntico al PDF)
                    // ==========================================================
                    int signo = m.SignoMovimiento != 0
                        ? m.SignoMovimiento
                        : (string.Equals(m.TipoMovimiento, "INGRESO", StringComparison.OrdinalIgnoreCase) ? 1 : -1);

                    // Presupsuestos NO afectan al balance (misma regla PDF)
                    if (!m.Concepto.ToLower().Contains("presupuesto"))
                    {
                        balance += signo * m.Monto;
                    }

                    row++;
                }

                // ==========================================================
                // TOTALES (igual al PDF)
                // ==========================================================
                row += 2;

                ws.Cell(row, 3).Value = "Total movimientos:";
                ws.Cell(row, 3).Style.Font.Bold = true;

                ws.Cell(row, 4).Value = totalMovimientos;
                ws.Cell(row, 4).Style.Font.Bold = true;
                ws.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                row++;

                ws.Cell(row, 3).Value = "Balance:";
                ws.Cell(row, 3).Style.Font.Bold = true;

                string balanceStr = (balance >= 0 ? "+ " : "- ") +
                                    Math.Abs(balance).ToString("C2", CultureInfo.CurrentCulture);

                ws.Cell(row, 4).Value = balanceStr;
                ws.Cell(row, 4).Style.Font.Bold = true;
                ws.Cell(row, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                ws.Columns().AdjustToContents();

                wb.SaveAs(rutaDestino);
            }

            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo(rutaDestino) { UseShellExecute = true });
            }
            catch { }
        }
    }
}
