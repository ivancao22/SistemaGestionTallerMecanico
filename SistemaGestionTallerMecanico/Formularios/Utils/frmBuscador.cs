using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formularios.Utils
{

    public partial class frmBuscador : Form
    {
        public string QueryBase { get; set; }
        public string CampoFiltro { get; set; }
        public string OrdenCampo { get; set; }
        public string WhereAdicional { get; set; }
        public object FiltroParametroExtra { get; set; }

        public DataRow RegistroSeleccionado;

        public frmBuscador()
        {
            InitializeComponent();

            // Agregar evento Load
            this.Load += frmBuscador_Load;

            txtFiltro.TextChanged += txtFiltro_TextChanged;
            chkOrdenar.CheckedChanged += chkOrdenar_CheckedChanged;
            dgvBusqueda.CellDoubleClick += dgvBusqueda_CellDoubleClick;
        }

        // Cargar datos al abrir el formulario
        private void frmBuscador_Load(object sender, EventArgs e)
        {
            Buscar();  // Carga todos los registros
            txtFiltro.Focus();  // Enfoca el campo de búsqueda
        }

        private void Buscar()
        {
            string filtro = txtFiltro.Text.Trim();
            string query = QueryBase;

            // Armar filtro WHERE dinámico
            var hasWhere = query.ToUpper().Contains("WHERE");

            if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrEmpty(CampoFiltro))
            {
                query += hasWhere ? " AND " : " WHERE ";
                query += $"{CampoFiltro} LIKE @filtro";
            }

            if (!string.IsNullOrEmpty(WhereAdicional))
            {
                query += (query.ToUpper().Contains("WHERE") ? " AND " : " WHERE ") + WhereAdicional;
            }

            // Orden
            if (chkOrdenar.Checked && !string.IsNullOrEmpty(OrdenCampo))
            {
                query += $" ORDER BY {OrdenCampo}";
            }

            // Ejecutar query y cargar la grilla
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings.Get("Connection")))
                {
                    SqlCommand comm = new SqlCommand(query, conn);

                    if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrEmpty(CampoFiltro))
                        comm.Parameters.AddWithValue("@filtro", $"%{filtro}%");

                    conn.Open();
                    var dt = new DataTable();
                    dt.Load(comm.ExecuteReader());
                    dgvBusqueda.DataSource = dt;

                    // Configurar columnas automáticamente después de cargar datos
                    ConfigurarColumnas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para configurar las columnas de la grilla
        private void ConfigurarColumnas()
        {
            if (dgvBusqueda.Columns.Count == 0) return;

            // Ocultar columnas que contengan "Codigo" o "Cod" en su nombre
            foreach (DataGridViewColumn col in dgvBusqueda.Columns)
            {
                string nombreColumna = col.Name.ToLower();

                // Ocultar columnas de código
                if (nombreColumna.Contains("codigo") || nombreColumna.Contains("cod"))
                {
                    col.Visible = false;
                }
                // Hacer Fill las columnas principales (Cliente, Nombre, Proveedor, etc.)
                else if (nombreColumna.Contains("cliente") ||
                         nombreColumna.Contains("nombre") ||
                         nombreColumna.Contains("proveedor") ||
                         nombreColumna.Contains("vehiculo"))
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                // Ajustar ancho de otras columnas
                else
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void chkOrdenar_CheckedChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void dgvBusqueda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                RegistroSeleccionado = ((DataRowView)dgvBusqueda.Rows[e.RowIndex].DataBoundItem).Row;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
