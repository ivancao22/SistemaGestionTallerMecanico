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
using Utiles;

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
                    string tipoUsuario;
                    if (!string.IsNullOrEmpty(mov.CodCliente))
                        tipoUsuario = "Cliente";
                    else if (!string.IsNullOrEmpty(mov.CodProveedor))
                        tipoUsuario = "Proveedor";
                    else
                        tipoUsuario = "Sin asignar";

                    row.Cells["ColTipoUser"].Value = tipoUsuario;

                    // Determinar etiqueta visible para el usuario según contexto y tipo real
                    // manteniendo por detrás el valor real mov.TipoMovimiento ("INGRESO"/"EGRESO")
                    string displayTipo;
                    if (tipoUsuario == "Cliente")
                    {
                        // Cliente: "DEBE" (EGRESO) / "PAGA" (INGRESO)
                        displayTipo = string.Equals(mov.TipoMovimiento, "INGRESO", StringComparison.OrdinalIgnoreCase)
                            ? "PAGA"
                            : "DEBE";
                    }
                    else if (tipoUsuario == "Proveedor")
                    {
                        // Proveedor: "SE LE DEBE" (EGRESO) / "SE LE PAGA" (INGRESO PORQUE CANCELO MI DEUDA)
                        displayTipo = string.Equals(mov.TipoMovimiento, "INGRESO", StringComparison.OrdinalIgnoreCase)
                            ? "SE LE DEBE"
                            : "SE LE PAGA";
                    }
                    else
                    {
                        // Sin asignar u otros: mostramos el valor técnico (INGRESO/EGRESO)
                        displayTipo = mov.TipoMovimiento?.ToUpperInvariant() ?? string.Empty;
                    }

                    row.Cells["ColTipoMovimiento"].Value = displayTipo;
                    row.Cells["ColFechaMovimiento"].Value = mov.Fecha.ToString("dd/MM/yyyy HH:mm");
                    row.Cells["ColMedioPago"].Value = mov.MetodoPago;
                    row.Cells["ColImporte"].Value = mov.Monto.ToString("C2");
                    row.Cells["ColDescripcion"].Value = mov.Concepto;

                    row.Cells["ColCodOrdenTrabajo"].Value = mov.CodOrdenTrabajo;

                    // Aplicar colores según el valor real (mov.TipoMovimiento)
                    if (string.Equals(mov.TipoMovimiento, "INGRESO", StringComparison.OrdinalIgnoreCase) && mov.CodProveedor == null)
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
                    if (string.Equals(mov.MetodoPago, "EFECTIVO", StringComparison.OrdinalIgnoreCase))
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

                // SaveFileDialog
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel Files|*.xlsx";
                    saveDialog.Title = "Guardar archivo Excel";
                    saveDialog.FileName = $"LibroDiario_{fechaActual:yyyyMMdd}.xlsx";

                    if (saveDialog.ShowDialog() != DialogResult.OK)
                        return;

                    this.Cursor = Cursors.WaitCursor;

                    // Llamada al helper que ya implementaste
                    ExcelHelper.ExportFromDataGridView(dgvMovimientos, saveDialog.FileName, fechaActual);

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
                        try
                        {
                            var psi = new System.Diagnostics.ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true };
                            System.Diagnostics.Process.Start(psi);
                        }
                        catch
                        {
                            // ignorar si no se puede abrir
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(
                    $"Error al exportar: {ex.Message}\n\nDetalles: {ex}",
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
                CargarGrillaAsync();
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

                // Obtener el nombre de la columna clickeada
                string nombreColumna = dgvMovimientos.Columns[e.ColumnIndex].Name;

                // Obtener CodMovimiento de la fila
                string codMovimiento = dgvMovimientos.Rows[e.RowIndex].Cells["ColCodMovimiento"].Value?.ToString();

                if (string.IsNullOrEmpty(codMovimiento)) return;

                // ✅ COLUMNA EDITAR
                if (nombreColumna == "ColEditar")
                {
                    string descripcion = dgvMovimientos.Rows[e.RowIndex].Cells["ColDescripcion"].Value?.ToString().ToLower();
                    if (descripcion.Contains("presupuesto") || descripcion.Contains("orden"))
                    {
                        MessageBox.Show(
                            "No se puede modificar movimientos generados automáticamente por presupuestos u órdenes de trabajo.",
                            "Acción no permitida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return; // ✅ SALIR SIN HACER NADA
                    }
                    else
                    {
                        await EditarMovimientoAsync(codMovimiento);
                    }
                }
                // ✅ COLUMNA BORRAR
                else if (nombreColumna == "ColBorrar")
                {
                    string codOrdenTrabajo = dgvMovimientos.Rows[e.RowIndex].Cells["ColCodOrdenTrabajo"].Value?.ToString();
                    await EliminarMovimientoAsync(codMovimiento, codOrdenTrabajo);
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

                if (movimiento.Concepto == "Apertura de caja")
                {
                    movimiento.esAperturaCaja = true;
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

        private async Task EliminarMovimientoAsync(string codMovimiento, string codOrdenTrabajo)
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
                bool exito = await negLibroDiario.EliminarMovimientoAsync(codMovimiento, codOrdenTrabajo);

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
                //HabilitarBotones(esDiaActual); COMENTO YA QUE EL USUARIO QUIERE EDITAR O PODER AGREGAR O ELIMINAR COSAS EN DIAS PREVIOS

                if (!esDiaActual)
                {
                    MessageBox.Show(
                        "Está consultando un día anterior.\n\n" +
                        "lo que modifique afectara al saldo de la caja en el dia consultado, y tendra impacto en las cuentas corrientes en caso que involucre a un cliente o proveedor.",
                        "Aviso",
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
