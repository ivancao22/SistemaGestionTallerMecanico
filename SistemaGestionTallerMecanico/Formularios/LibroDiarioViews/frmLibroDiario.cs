using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocios;
using ClosedXML;
using ClosedXML.Excel;
using Formularios.LibroDiarioViews.OrdenTrabajoViews;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Formularios.LibroDiarioViews
{
    public partial class frmLibroDiario : Form
    {
        private readonly NegCajaDiaria negCajaDiaria;
        private readonly NegLibroDiario negLibroDiario;
        private int? IdCajaDiariaActual = null;
        private DateTime fechaActual;

        public frmLibroDiario()
        {
            InitializeComponent();
            this.negCajaDiaria = new NegCajaDiaria();
            this.negLibroDiario = new NegLibroDiario();
            this.fechaActual = DateTime.Today;
        }

        private async void frmLibroDiario_Load(object sender, EventArgs e)
        {
            //Deberia ejecutar una funcion de cargar grilla, la funcion de cargar grilla deberia cargar en la grilla los datos de la caja diaria en el dia que dice en el dtpFecha y ademas pisar el lbl monto caja 
            //los campos de la grilla son : ColCodMovimiento, ColNombre, ColTipoUser, ColTipoMovivimiento, ColFechaMovimiento, ColImporte, ColMetodoPago, ColEditar, ColBorrar

            //Por defecto el dtp carga con la fecha del dia en curso, ademas debe estar bloqueado para solo buscar del dia en curso hacia atras, Cuando carga un dia de una fecha anterior le debe mostrar los datos, pero debe bloquear los btn de agregar movimiento, editar o borrar

            try
            {
                // ✅ Configurar DateTimePicker
                dtpFecha.Value = DateTime.Today;
                dtpFecha.MaxDate = DateTime.Today; // Solo hasta hoy
                dtpFecha.Format = DateTimePickerFormat.Short;


                // ✅ Obtener caja del día actual
                CajaDiaria cajaHoy = await negCajaDiaria.ObtenerPorFechaAsync(DateTime.Today);

                if (cajaHoy != null)
                {
                    IdCajaDiariaActual = cajaHoy.Id;
                    fechaActual = DateTime.Today;

                    // Cargar datos
                    await CargarGrillaAsync();

                    // Habilitar botones (es día actual)
                    HabilitarBotones(true);
                }
                else
                {
                    MessageBox.Show(
                        "No se encontró caja del día actual",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                this.Close();
            }
        }

        private async Task CargarGrillaAsync()
        {
            try
            {
                if (!IdCajaDiariaActual.HasValue) return;

                this.Cursor = Cursors.WaitCursor;

                // Obtener filtro de búsqueda
                string filtroNombre = txtFiltro.Text.Trim();

                // Obtener movimientos de la BD
                var movimientos = await negLibroDiario.ListarPorCajaAsync(
                    IdCajaDiariaActual.Value,
                    string.IsNullOrEmpty(filtroNombre) ? null : filtroNombre
                );

                // Limpiar grilla
                dgvMovimientos.Rows.Clear();

                // Llenar grilla
                foreach (var mov in movimientos)
                {
                    int index = dgvMovimientos.Rows.Add();
                    DataGridViewRow row = dgvMovimientos.Rows[index];

                    row.Cells["ColCodMovimiento"].Value = mov.CodMovimiento;
                    row.Cells["ColNombre"].Value = mov.EntidadRelacionada ?? "Sin asignar";

                    // Tipo usuario (Cliente/Proveedor)
                    string tipoUsuario = "";
                    if (!string.IsNullOrEmpty(mov.CodCliente))
                        tipoUsuario = "Cliente";
                    else if (!string.IsNullOrEmpty(mov.CodProveedor))
                        tipoUsuario = "Proveedor";
                    else
                        tipoUsuario = "Sin asignar";

                    row.Cells["ColTipoUser"].Value = tipoUsuario;
                    row.Cells["ColTipoMovimiento"].Value = mov.TipoMovimiento;
                    row.Cells["ColFechaMovimiento"].Value = mov.Fecha.ToString("dd/MM/yyyy HH:mm");
                    row.Cells["ColMedioPago"].Value = mov.MetodoPago;
                    row.Cells["ColImporte"].Value = mov.Monto.ToString("C2");
                    row.Cells["ColDescripcion"].Value = mov.Concepto;

                    // Aplicar colores según tipo
                    if (mov.TipoMovimiento == "INGRESO")
                    {
                        row.Cells["ColTipoMovimiento"].Style.ForeColor = System.Drawing.Color.Green;
                        row.Cells["ColTipoMovimiento"].Style.Font = new System.Drawing.Font(dgvMovimientos.Font, FontStyle.Bold);
                    }
                    else
                    {
                        row.Cells["ColTipoMovimiento"].Style.ForeColor = System.Drawing.Color.Red;
                        row.Cells["ColTipoMovimiento"].Style.Font = new System.Drawing.Font(dgvMovimientos.Font, FontStyle.Bold);
                    }

                    // Resaltar efectivo
                    if (mov.MetodoPago == "EFECTIVO")
                    {
                        row.Cells["ColMedioPago"].Style.BackColor = System.Drawing.Color.LightYellow;
                        row.Cells["ColMedioPago"].Style.Font = new System.Drawing.Font(dgvMovimientos.Font, FontStyle.Bold);
                    }
                

                }

                // ✅ Actualizar saldo de caja (ejecutar SP)
                await ActualizarSaldoCajaAsync();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(
                    $"Error al cargar movimientos: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async Task ActualizarSaldoCajaAsync()
        {
            try
            {
                if (!IdCajaDiariaActual.HasValue) return;

                // Ejecutar SP para actualizar saldo
                decimal saldoFinal = await negCajaDiaria.ActualizarSaldoFinalAsync(IdCajaDiariaActual.Value);

                // Actualizar label
                lblMontoCaja.Text = $"$ {saldoFinal:N2}";

                // Color según saldo
                if (saldoFinal < 0)
                    lblMontoCaja.ForeColor = System.Drawing.Color.Red;
                else if (saldoFinal == 0)
                    lblMontoCaja.ForeColor = System.Drawing.Color.Gray;
                else
                    lblMontoCaja.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMontoCaja.Text = "$ Error";
                lblMontoCaja.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void HabilitarBotones(bool habilitar)
        {
            btnAgregarMovimiento.Enabled = habilitar;

            // Las columnas de editar/borrar se controlan en CellContentClick
            if (dgvMovimientos.Columns.Contains("ColEditar"))
                dgvMovimientos.Columns["ColEditar"].Visible = habilitar;

            if (dgvMovimientos.Columns.Contains("ColBorrar"))
                dgvMovimientos.Columns["ColBorrar"].Visible = habilitar;
        }

        private async void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.Trim().ToLower();

            // Si el filtro está vacío, mostrar todas las filas
            if (string.IsNullOrEmpty(filtro))
            {
                foreach (DataGridViewRow row in dgvMovimientos.Rows)
                {
                    row.Visible = true;
                }
                return;
            }

            // Filtrar filas según el nombre mostrado en ColNombre
            foreach (DataGridViewRow row in dgvMovimientos.Rows)
            {
                string nombre = row.Cells["ColNombre"].Value?.ToString().ToLower() ?? "";

                // Mostrar solo si coincide con el filtro
                row.Visible = nombre.Contains(filtro);
            }
        }

        private async void btnRecargarDgv_Click(object sender, EventArgs e)
        {
            //debe ejecutar la funcion de cargar grillas
            await CargarGrillaAsync();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMovimientos.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No hay datos para exportar",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Crear SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.Title = "Guardar archivo Excel";
                saveDialog.FileName = $"LibroDiario_{fechaActual:yyyyMMdd}.xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                this.Cursor = Cursors.WaitCursor;

                // Crear archivo Excel con ClosedXML
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Libro Diario");

                    // ✅ Encabezados
                    int col = 1;
                    worksheet.Cell(1, col++).Value = "Nombre";
                    worksheet.Cell(1, col++).Value = "Tipo Usuario";
                    worksheet.Cell(1, col++).Value = "Tipo Movimiento";
                    worksheet.Cell(1, col++).Value = "Fecha Movimiento";
                    worksheet.Cell(1, col++).Value = "Medio Pago";
                    worksheet.Cell(1, col++).Value = "Importe";
                    worksheet.Cell(1, col++).Value = "Descripción";

                    // Estilo encabezados
                    var headerRange = worksheet.Range(1, 1, 1, col - 1);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // ✅ Datos
                    int row = 2;
                    decimal totalCaja = 0;

                    foreach (DataGridViewRow dgvRow in dgvMovimientos.Rows)
                    {
                        if (dgvRow.IsNewRow) continue;

                        col = 1;

                        // Nombre
                        worksheet.Cell(row, col++).Value = dgvRow.Cells["ColNombre"].Value?.ToString() ?? "";

                        // Tipo Usuario
                        worksheet.Cell(row, col++).Value = dgvRow.Cells["ColTipoUser"].Value?.ToString() ?? "";

                        // Tipo Movimiento
                        string tipoMov = dgvRow.Cells["ColTipoMovimiento"].Value?.ToString() ?? "";
                        worksheet.Cell(row, col++).Value = tipoMov;

                        // Fecha Movimiento
                        worksheet.Cell(row, col++).Value = dgvRow.Cells["ColFechaMovimiento"].Value?.ToString() ?? "";

                        // Medio Pago
                        string medioPago = dgvRow.Cells["ColMedioPago"].Value?.ToString() ?? "";
                        worksheet.Cell(row, col++).Value = dgvRow.Cells["ColMedioPago"].Value?.ToString() ?? "";

                        // Importe
                        string importeStr = dgvRow.Cells["ColImporte"].Value?.ToString() ?? "$0";

                        // Limpiar el string para parsear
                        importeStr = importeStr.Replace("$", "").Replace(".", "").Replace(",", ".");
                        importeStr = importeStr.Trim();

                        if (decimal.TryParse(importeStr, System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture, out decimal importe))
                        {
                            worksheet.Cell(row, col).Value = importe;
                            worksheet.Cell(row, col).Style.NumberFormat.Format = "$#,##0.00";

                            // Calcular total
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
                        worksheet.Cell(row, col++).Value = dgvRow.Cells["ColDescripcion"].Value?.ToString() ?? "";

                        row++;
                    }

                    // ✅ Total al final
                    row++; // Fila vacía
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
                    workbook.SaveAs(saveDialog.FileName);
                }

                this.Cursor = Cursors.Default;

                MessageBox.Show(
                    $"✅ Archivo exportado correctamente:\n\n{saveDialog.FileName}",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Preguntar si desea abrir
                var resultado = MessageBox.Show(
                    "¿Desea abrir el archivo?",
                    "Abrir Excel",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultado == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(
                    $"Error al exportar: {ex.Message}\n\nDetalles: {ex.ToString()}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnPresupuesto_Click(object sender, EventArgs e)
        {
            frmCrearOrdenTrabajo ordenTrabajo = new frmCrearOrdenTrabajo();
            if (IdCajaDiariaActual.HasValue)
            {
                ordenTrabajo.idCajaDiariaActual = IdCajaDiariaActual.Value;
            }

            if (ordenTrabajo.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(
                    "✅ Orden de trabajo creada correctamente",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                CargarGrillaAsync().Wait();
            }

        }

        private async void btnAgregarMovimiento_Click(object sender, EventArgs e)
        {
            //debe abrir el frm de agregar movimiento frmAgregarMovimiento
            try
            {
                if (!IdCajaDiariaActual.HasValue)
                {
                    MessageBox.Show(
                        "No hay caja abierta",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Abrir formulario de agregar movimiento
                var frmAgregar = new frmAgregarMovimiento(IdCajaDiariaActual.Value);


                if (frmAgregar.ShowDialog() == DialogResult.OK)
                {
                    // Recargar grilla si guardó
                    await CargarGrillaAsync();
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

        private async void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Validar que se hizo click en una fila válida
                if (e.RowIndex < 0) return;

                string descripcion = dgvMovimientos.Rows[e.RowIndex].Cells["ColDescripcion"].Value?.ToString().ToLower();
                if (descripcion.Contains("presupuesto") || descripcion.Contains("orden"))
                {
                        MessageBox.Show(
                            "No se puede modificar/eliminar movimientos generados automáticamente por presupuestos u órdenes de trabajo.",
                            "Acción no permitida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return; // ✅ SALIR SIN HACER NADA
                }

                // Obtener el nombre de la columna clickeada
                string nombreColumna = dgvMovimientos.Columns[e.ColumnIndex].Name;

                // Obtener CodMovimiento de la fila
                string codMovimiento = dgvMovimientos.Rows[e.RowIndex].Cells["ColCodMovimiento"].Value?.ToString();

                if (string.IsNullOrEmpty(codMovimiento)) return;

                // ✅ COLUMNA EDITAR
                if (nombreColumna == "ColEditar")
                {
                    await EditarMovimientoAsync(codMovimiento);
                }
                // ✅ COLUMNA BORRAR
                else if (nombreColumna == "ColBorrar")
                {
                    await EliminarMovimientoAsync(codMovimiento);
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

        private async Task EditarMovimientoAsync(string codMovimiento)
        {
            try
            {
                // Obtener movimiento de la BD
                LibroDiario movimiento = await negLibroDiario.ObtenerPorCodigoAsync(codMovimiento);

                if (movimiento == null)
                {
                    MessageBox.Show(
                        "No se encontró el movimiento",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // ✅ Abrir formulario en modo EDICIÓN
                var frmAgregar = new frmAgregarMovimiento(IdCajaDiariaActual.Value, movimiento);

                if (frmAgregar.ShowDialog() == DialogResult.OK)
                {
                    // Recargar grilla si guardó
                    await CargarGrillaAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al editar: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // ELIMINAR MOVIMIENTO
        // =====================================================

        private async Task EliminarMovimientoAsync(string codMovimiento)
        {
            try
            {
                // Confirmación
                var resultado = MessageBox.Show(
                    $"¿Está seguro que desea eliminar el movimiento {codMovimiento}?\n\n" +
                    "Esta acción no se puede deshacer.",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resultado != DialogResult.Yes) return;

                this.Cursor = Cursors.WaitCursor;

                // Eliminar (negocio se encarga de actualizar saldo si es efectivo)
                bool exito = await negLibroDiario.EliminarMovimientoAsync(codMovimiento);

                this.Cursor = Cursors.Default;

                if (exito)
                {
                    MessageBox.Show(
                        "✅ Movimiento eliminado correctamente",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Recargar grilla
                    await CargarGrillaAsync();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo eliminar el movimiento",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(
                    $"Error al eliminar: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Solo permitir fechas hasta hoy
                if (dtpFecha.Value.Date > DateTime.Today)
                {
                    dtpFecha.Value = DateTime.Today;
                    return;
                }

                DateTime fechaSeleccionada = dtpFecha.Value.Date;

                // Buscar caja de esa fecha
                CajaDiaria caja = await negCajaDiaria.ObtenerPorFechaAsync(fechaSeleccionada);

                if (caja == null)
                {
                    MessageBox.Show(
                        $"No hay registro de caja para el día {fechaSeleccionada:dd/MM/yyyy}",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Volver a la fecha actual
                    dtpFecha.Value = fechaActual;
                    return;
                }

                // Actualizar variables
                IdCajaDiariaActual = caja.Id;
                fechaActual = fechaSeleccionada;

                // ✅ Si es día actual → Habilitar botones
                // ❌ Si es día anterior → Deshabilitar botones
                bool esDiaActual = (fechaSeleccionada.Date == DateTime.Today);
                HabilitarBotones(esDiaActual);

                if (!esDiaActual)
                {
                    MessageBox.Show(
                        "Está consultando un día anterior.\n\n" +
                        "No podrá agregar, editar o eliminar movimientos.",
                        "Modo consulta",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                // Cargar datos de esa fecha
                await CargarGrillaAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cambiar fecha: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
