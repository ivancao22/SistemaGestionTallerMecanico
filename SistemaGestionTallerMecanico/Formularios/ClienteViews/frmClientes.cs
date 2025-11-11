using System;
using System.Data;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Formularios.ClienteViews
{
    public partial class frmClientes : Form
    {
        private NegClientes negClientes = new NegClientes();
        private DataTable dtClientes;  // Guardar los datos en memoria

        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            frmAgregarCliente frm = new frmAgregarCliente();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarGrilla();  // Recargar desde la BD
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            // Filtrar en memoria cuando cambia el texto
            AplicarFiltro();
        }

        private void CargarGrilla()
        {
            // Cargar datos UNA VEZ desde la BD
            dtClientes = negClientes.CargarClientes();

            // Verificar si hubo error al cargar
            if (dtClientes == null || dtClientes.Rows.Count == 0)
            {
                dgvClientes.Rows.Clear();

                if (dtClientes == null)
                {
                    MessageBox.Show("Error al cargar los clientes. Verifique la conexión a la base de datos.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
                return;
            }

            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            dgvClientes.Rows.Clear();

            if (dtClientes == null || dtClientes.Rows.Count == 0) return;

            string filtro = txtFiltro.Text.Trim().ToLower();

            // Filtrar usando DataView (muy rápido)
            DataView dv = dtClientes.DefaultView;

            if (!string.IsNullOrEmpty(filtro))
            {
                // Escapar comillas simples para evitar errores en el RowFilter
                filtro = filtro.Replace("'", "''");

                // Filtrar por nombre (busca en cualquier parte del texto)
                dv.RowFilter = $"nombre LIKE '%{filtro}%'";
            }
            else
            {
                dv.RowFilter = "";  // Sin filtro, mostrar todos
            }

            // Llenar la grilla con los datos filtrados
            foreach (DataRowView rowView in dv)
            {
                int index = dgvClientes.Rows.Add();
                dgvClientes.Rows[index].Cells["ColCodCliente"].Value = rowView["CodCliente"];
                dgvClientes.Rows[index].Cells["ColNombre"].Value = rowView["nombre"];
                dgvClientes.Rows[index].Cells["ColTelefono"].Value = rowView["telefono"];
                dgvClientes.Rows[index].Cells["ColEmail"].Value = rowView["email"];
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string codCliente = dgvClientes.Rows[e.RowIndex].Cells["ColCodCliente"].Value.ToString();

            // EDITAR
            if (dgvClientes.Columns[e.ColumnIndex].Name == "ColEditar")
            {
                // Obtener datos completos desde la BD
                Cliente cliente = negClientes.ObtenerClientePorCodigo(codCliente);

                if (cliente != null)
                {
                    frmAgregarCliente frm = new frmAgregarCliente
                    {
                        CodCliente = cliente.CodCliente,
                        Nombre = cliente.Nombre,
                        Apellido = cliente.Apellido,
                        Telefono = cliente.Telefono,
                        Email = cliente.Email
                    };

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        CargarGrilla();  // Recargar desde la BD
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo cargar el cliente. Verifique la conexión.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
            }
            // ELIMINAR
            else if (dgvClientes.Columns[e.ColumnIndex].Name == "ColBorrar")
            {
                var resultado = MessageBox.Show("¿Está seguro que desea eliminar este cliente?",
                                                 "Confirmar eliminación",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    if (negClientes.BorrarCliente(codCliente))
                    {
                        MessageBox.Show("Cliente eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGrilla();  // Recargar desde la BD
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el cliente. Verifique la conexión.",
                                       "Error",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtFiltro_TextChanged_1(object sender, EventArgs e)
        {
            AplicarFiltro();
        }
    }
}
