namespace Formularios.LibroDiarioViews.OrdenTrabajoViews
{
    partial class frmHistorialOrdenesTrabajo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistorialOrdenesTrabajo));
            this.panelTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.panelDgv = new System.Windows.Forms.Panel();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.ColCodOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColVehiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPatente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFechaOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColGenerarPDF = new System.Windows.Forms.DataGridViewImageColumn();
            this.panelTop.SuspendLayout();
            this.panelDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.txtBuscar);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(6, 6);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(6);
            this.panelTop.Size = new System.Drawing.Size(630, 40);
            this.panelTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Buscar : ";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscar.Location = new System.Drawing.Point(61, 10);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(548, 20);
            this.txtBuscar.TabIndex = 22;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // panelDgv
            // 
            this.panelDgv.Controls.Add(this.dgvOrdenes);
            this.panelDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDgv.Location = new System.Drawing.Point(6, 46);
            this.panelDgv.Name = "panelDgv";
            this.panelDgv.Padding = new System.Windows.Forms.Padding(6);
            this.panelDgv.Size = new System.Drawing.Size(630, 393);
            this.panelDgv.TabIndex = 1;
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.AllowUserToAddRows = false;
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodOrden,
            this.ColVehiculo,
            this.ColPatente,
            this.ColNombre,
            this.ColFechaOrden,
            this.ColDescripcion,
            this.ColGenerarPDF});
            this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenes.Location = new System.Drawing.Point(6, 6);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.RowHeadersVisible = false;
            this.dgvOrdenes.Size = new System.Drawing.Size(618, 381);
            this.dgvOrdenes.TabIndex = 21;
            this.dgvOrdenes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenes_CellContentClick);
            this.dgvOrdenes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // ColCodOrden
            // 
            this.ColCodOrden.HeaderText = "Cod movimiento";
            this.ColCodOrden.Name = "ColCodOrden";
            this.ColCodOrden.Visible = false;
            // 
            // ColVehiculo
            // 
            this.ColVehiculo.HeaderText = "Vehiculo";
            this.ColVehiculo.Name = "ColVehiculo";
            this.ColVehiculo.Width = 130;
            this.ColVehiculo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            // 
            // ColPatente
            // 
            this.ColPatente.HeaderText = "Patente";
            this.ColPatente.Name = "ColPatente";
            this.ColPatente.Width = 80;
            this.ColPatente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            // 
            // ColNombre
            // 
            this.ColNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColNombre.HeaderText = "Nombre";
            this.ColNombre.Name = "ColNombre";
            // 
            // ColFechaOrden
            // 
            this.ColFechaOrden.HeaderText = "Fecha movimiento";
            this.ColFechaOrden.Name = "ColFechaOrden";
            this.ColFechaOrden.Width = 100;
            this.ColFechaOrden.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            // 
            // ColDescripcion
            // 
            this.ColDescripcion.HeaderText = "Descripcion";
            this.ColDescripcion.Name = "ColDescripcion";
            this.ColDescripcion.Width = 160;
            this.ColDescripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            // 
            // ColGenerarPDF
            // 
            this.ColGenerarPDF.HeaderText = "";
            this.ColGenerarPDF.Image = ((System.Drawing.Image)(resources.GetObject("ColGenerarPDF.Image")));
            this.ColGenerarPDF.Name = "ColGenerarPDF";
            this.ColGenerarPDF.Width = 24;
            this.ColGenerarPDF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            // 
            // frmHistorialOrdenesTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // padding interior y tamaño mínimo
            this.ClientSize = new System.Drawing.Size(642, 445);
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Controls.Add(this.panelDgv);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.Name = "frmHistorialOrdenesTrabajo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial de ordenes de trabajo";
            this.Load += new System.EventHandler(this.frmHistorialOrdenesTrabajo_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelDgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelDgv;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodOrden;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVehiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPatente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFechaOrden;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescripcion;
        private System.Windows.Forms.DataGridViewImageColumn ColGenerarPDF;
    }
}