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
using Negocios;

namespace Formularios.ProveedorViews
{
    public partial class frmProveedor : Form
    {
        private NegProveedor negProveedores = new NegProveedor();
        private DataTable dtProveedores;

        public frmProveedor()
        {
            InitializeComponent();
        }

        private void frmProveedor_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string codProveedor = dgvProveedores.Rows[e.RowIndex].Cells["ColCodProveedor"].Value.ToString();

            // EDITAR
            if (dgvProveedores.Columns[e.ColumnIndex].Name == "ColEditar")
            {
                Proveedor proveedor = negProveedores.ObtenerProveedorPorCodigo(codProveedor);

                if (proveedor != null)
                {
                    frmAgregarProveedor frm = new frmAgregarProveedor
                    {
                        CodProveedor = proveedor.CodProveedor,
                        Nombre = proveedor.Nombre,
                        Apellido = proveedor.Apellido,
                        RazonSocial = proveedor.RazonSocial,
                        CUIT = proveedor.CUIT,
                        Telefono = proveedor.Telefono,
                        Email = proveedor.Email
                    };

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        CargarGrilla();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo cargar el proveedor. Verifique la conexión.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
            }
            // ELIMINAR
            else if (dgvProveedores.Columns[e.ColumnIndex].Name == "ColBorrar")
            {
                var resultado = MessageBox.Show("¿Está seguro que desea eliminar este proveedor?",
                                                 "Confirmar eliminación",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    if (negProveedores.BorrarProveedor(codProveedor))
                    {
                        MessageBox.Show("Proveedor eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGrilla();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el proveedor. Verifique la conexión.",
                                       "Error",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            frmAgregarProveedor frm = new frmAgregarProveedor();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            dtProveedores = negProveedores.CargarProveedores();

            if (dtProveedores == null || dtProveedores.Rows.Count == 0)
            {
                dgvProveedores.Rows.Clear();

                if (dtProveedores == null)
                {
                    MessageBox.Show("Error al cargar los proveedores. Verifique la conexión a la base de datos.",
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
            dgvProveedores.Rows.Clear();

            if (dtProveedores == null || dtProveedores.Rows.Count == 0) return;

            string filtro = txtFiltro.Text.Trim().ToLower();
            DataView dv = dtProveedores.DefaultView;

            if (!string.IsNullOrEmpty(filtro))
            {
                // Escapar comillas simples
                filtro = filtro.Replace("'", "''");

                // Filtrar solo por nombre O razón social
                dv.RowFilter = $"nombre LIKE '%{filtro}%' OR razonSocial LIKE '%{filtro}%'";
            }
            else
            {
                dv.RowFilter = "";
            }

            // Llenar la grilla con los datos filtrados
            foreach (DataRowView rowView in dv)
            {
                int index = dgvProveedores.Rows.Add();
                dgvProveedores.Rows[index].Cells["ColCodProveedor"].Value = rowView["CodProveedor"];
                dgvProveedores.Rows[index].Cells["ColNombre"].Value = rowView["nombre"];
                dgvProveedores.Rows[index].Cells["ColRazonSocial"].Value = rowView["razonSocial"];
                dgvProveedores.Rows[index].Cells["ColCUIT"].Value = rowView["cuit"];
                dgvProveedores.Rows[index].Cells["ColEmail"].Value = rowView["email"];
                dgvProveedores.Rows[index].Cells["ColTelefono"].Value = rowView["telefono"];
            }
        }

    }
}
