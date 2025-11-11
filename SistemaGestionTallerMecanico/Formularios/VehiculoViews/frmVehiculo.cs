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

namespace Formularios.VehiculoViews
{
    public partial class frmVehiculo : Form
    {
        private NegVehiculo negVehiculos = new NegVehiculo();
        private DataTable dtVehiculos;

        public frmVehiculo()
        {
            InitializeComponent();
        }

        private void frmVehiculo_Load(object sender, EventArgs e)
        {
            // Por defecto, filtro por Patente está seleccionado
            optPatente.Checked = true;

            CargarGrilla();
        }

        private void btnAgregarVehiculo_Click(object sender, EventArgs e)
        {
            frmAgregarVehiculo frm = new frmAgregarVehiculo();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarGrilla();
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }



        private void CargarGrilla()
        {
            dtVehiculos = negVehiculos.CargarVehiculos();

            if (dtVehiculos == null || dtVehiculos.Rows.Count == 0)
            {
                dgvVehiculos.Rows.Clear();

                if (dtVehiculos == null)
                {
                    MessageBox.Show("Error al cargar los vehículos. Verifique la conexión a la base de datos.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
                return;
            }

            AplicarFiltro();
        }

        private void optPatente_CheckedChanged(object sender, EventArgs e)
        {
            if (optPatente.Checked)
            {
                txtFiltro.Focus();
                AplicarFiltro();
            }
        }

        private void optModelo_CheckedChanged(object sender, EventArgs e)
        {
            if (optModelo.Checked)
            {
                txtFiltro.Focus();
                AplicarFiltro();
            }
        }

        private void optCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (optCliente.Checked)
            {
                txtFiltro.Focus();
                AplicarFiltro();
            }
        }

        private void AplicarFiltro()
        {
            dgvVehiculos.Rows.Clear();

            if (dtVehiculos == null || dtVehiculos.Rows.Count == 0) return;

            string filtro = txtFiltro.Text.Trim().ToLower();
            DataView dv = dtVehiculos.DefaultView;

            if (!string.IsNullOrEmpty(filtro))
            {
                // Escapar comillas simples
                filtro = filtro.Replace("'", "''");

                // Determinar el campo a filtrar según el RadioButton seleccionado
                string campoFiltro = "";

                if (optPatente.Checked)
                {
                    campoFiltro = "patente";
                }
                else if (optModelo.Checked)
                {
                    campoFiltro = "modelo";
                }
                else if (optCliente.Checked)
                {
                    campoFiltro = "cliente";
                }

                // Aplicar filtro en el campo seleccionado
                dv.RowFilter = $"{campoFiltro} LIKE '%{filtro}%'";
            }
            else
            {
                dv.RowFilter = "";
            }

            // Llenar la grilla con los datos filtrados
            foreach (DataRowView rowView in dv)
            {
                int index = dgvVehiculos.Rows.Add();
                dgvVehiculos.Rows[index].Cells["ColCodVehiculo"].Value = rowView["CodVehiculo"];
                dgvVehiculos.Rows[index].Cells["ColCliente"].Value = rowView["cliente"];
                dgvVehiculos.Rows[index].Cells["ColModelo"].Value = rowView["modelo"];
                dgvVehiculos.Rows[index].Cells["ColPatente"].Value = rowView["patente"];
                dgvVehiculos.Rows[index].Cells["ColKm"].Value = rowView["kilometraje"];
            }
        }

        private void dgvVehiculos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string codVehiculo = dgvVehiculos.Rows[e.RowIndex].Cells["ColCodVehiculo"].Value.ToString();

            // EDITAR
            if (dgvVehiculos.Columns[e.ColumnIndex].Name == "ColEditar")
            {
                Vehiculo vehiculo = negVehiculos.ObtenerVehiculoPorCodigo(codVehiculo);

                if (vehiculo != null)
                {
                    frmAgregarVehiculo frm = new frmAgregarVehiculo
                    {
                        CodVehiculo = vehiculo.CodVehiculo,
                        CodCliente = vehiculo.CodCliente,
                        Modelo = vehiculo.Modelo,
                        Patente = vehiculo.Patente,
                        Kilometraje = vehiculo.Kilometraje
                    };

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        CargarGrilla();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo cargar el vehículo. Verifique la conexión.",
                                   "Error",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
            }
            // ELIMINAR
            else if (dgvVehiculos.Columns[e.ColumnIndex].Name == "ColBorrar")
            {
                var resultado = MessageBox.Show("¿Está seguro que desea eliminar este vehículo?",
                                                 "Confirmar eliminación",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    if (negVehiculos.BorrarVehiculo(codVehiculo))
                    {
                        MessageBox.Show("Vehículo eliminado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGrilla();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el vehículo. Verifique la conexión.",
                                       "Error",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                    }
                }
            }
        }




    }
}
