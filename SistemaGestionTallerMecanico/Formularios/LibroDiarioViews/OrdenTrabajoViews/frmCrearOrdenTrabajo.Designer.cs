namespace Formularios.LibroDiarioViews.OrdenTrabajoViews
{
    partial class frmCrearOrdenTrabajo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCrearOrdenTrabajo));
            this.grpCabecera = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscarVehiculo = new System.Windows.Forms.Button();
            this.txtKm = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVehiculo = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.grpDetalleOrden = new System.Windows.Forms.GroupBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDetallesOrden = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotalGral = new System.Windows.Forms.Label();
            this.lblTotalMano = new System.Windows.Forms.Label();
            this.lblTotalRepuestos = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnVistaPrevia = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnAbrirHistorial = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ColTipoDet = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColDescripcionDet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrecioDet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOperacion = new System.Windows.Forms.DataGridViewImageColumn();
            this.grpCabecera.SuspendLayout();
            this.grpDetalleOrden.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesOrden)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCabecera
            // 
            this.grpCabecera.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCabecera.Controls.Add(this.label4);
            this.grpCabecera.Controls.Add(this.dtpFecha);
            this.grpCabecera.Controls.Add(this.label3);
            this.grpCabecera.Controls.Add(this.btnBuscarVehiculo);
            this.grpCabecera.Controls.Add(this.txtKm);
            this.grpCabecera.Controls.Add(this.btnBuscarCliente);
            this.grpCabecera.Controls.Add(this.label2);
            this.grpCabecera.Controls.Add(this.label1);
            this.grpCabecera.Controls.Add(this.txtVehiculo);
            this.grpCabecera.Controls.Add(this.txtCliente);
            this.grpCabecera.Location = new System.Drawing.Point(10, 8);
            this.grpCabecera.Name = "grpCabecera";
            this.grpCabecera.Size = new System.Drawing.Size(603, 102);
            this.grpCabecera.TabIndex = 0;
            this.grpCabecera.TabStop = false;
            this.grpCabecera.Text = "Datos de la orden";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(438, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "Fecha : ";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(494, 32);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(86, 22);
            this.dtpFecha.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(412, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "Kilometraje :";
            // 
            // btnBuscarVehiculo
            // 
            this.btnBuscarVehiculo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarVehiculo.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarVehiculo.Image")));
            this.btnBuscarVehiculo.Location = new System.Drawing.Point(345, 63);
            this.btnBuscarVehiculo.Name = "btnBuscarVehiculo";
            this.btnBuscarVehiculo.Size = new System.Drawing.Size(30, 28);
            this.btnBuscarVehiculo.TabIndex = 26;
            this.btnBuscarVehiculo.UseVisualStyleBackColor = true;
            this.btnBuscarVehiculo.Click += new System.EventHandler(this.btnBuscarVehiculo_Click);
            // 
            // txtKm
            // 
            this.txtKm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKm.Location = new System.Drawing.Point(494, 66);
            this.txtKm.Name = "txtKm";
            this.txtKm.Size = new System.Drawing.Size(86, 22);
            this.txtKm.TabIndex = 27;
            this.txtKm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKm_KeyPress);
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCliente.Image")));
            this.btnBuscarCliente.Location = new System.Drawing.Point(345, 26);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(30, 28);
            this.btnBuscarCliente.TabIndex = 23;
            this.btnBuscarCliente.UseVisualStyleBackColor = true;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "Vehiculo * :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "Cliente * :";
            // 
            // txtVehiculo
            // 
            this.txtVehiculo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVehiculo.Location = new System.Drawing.Point(91, 66);
            this.txtVehiculo.Name = "txtVehiculo";
            this.txtVehiculo.Size = new System.Drawing.Size(248, 22);
            this.txtVehiculo.TabIndex = 25;
            // 
            // txtCliente
            // 
            this.txtCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCliente.Location = new System.Drawing.Point(80, 29);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(259, 22);
            this.txtCliente.TabIndex = 22;
            // 
            // grpDetalleOrden
            // 
            this.grpDetalleOrden.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDetalleOrden.Controls.Add(this.txtDescripcion);
            this.grpDetalleOrden.Location = new System.Drawing.Point(10, 116);
            this.grpDetalleOrden.Name = "grpDetalleOrden";
            this.grpDetalleOrden.Size = new System.Drawing.Size(603, 72);
            this.grpDetalleOrden.TabIndex = 1;
            this.grpDetalleOrden.TabStop = false;
            this.grpDetalleOrden.Text = "Descripcion del trabajo";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescripcion.Location = new System.Drawing.Point(3, 18);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(597, 51);
            this.txtDescripcion.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvDetallesOrden);
            this.groupBox1.Location = new System.Drawing.Point(10, 194);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 320);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle de trabajo";
            // 
            // dgvDetallesOrden
            // 
            this.dgvDetallesOrden.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetallesOrden.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallesOrden.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTipoDet,
            this.ColDescripcionDet,
            this.ColPrecioDet,
            this.ColOperacion});
            this.dgvDetallesOrden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetallesOrden.Location = new System.Drawing.Point(3, 18);
            this.dgvDetallesOrden.Name = "dgvDetallesOrden";
            this.dgvDetallesOrden.RowHeadersVisible = false;
            this.dgvDetallesOrden.Size = new System.Drawing.Size(597, 299);
            this.dgvDetallesOrden.TabIndex = 2;
            this.dgvDetallesOrden.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetallesOrden_CellContentClick);
            this.dgvDetallesOrden.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetallesOrden_CellEndEdit);
            this.dgvDetallesOrden.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDetallesOrden_CellValidating);
            this.dgvDetallesOrden.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDetallesOrden_EditingControlShowing);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblTotalGral);
            this.groupBox2.Controls.Add(this.lblTotalMano);
            this.groupBox2.Controls.Add(this.lblTotalRepuestos);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(10, 520);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(603, 89);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Totales";
            // 
            // lblTotalGral
            // 
            this.lblTotalGral.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalGral.AutoSize = true;
            this.lblTotalGral.Location = new System.Drawing.Point(499, 65);
            this.lblTotalGral.Name = "lblTotalGral";
            this.lblTotalGral.Size = new System.Drawing.Size(41, 16);
            this.lblTotalGral.TabIndex = 7;
            this.lblTotalGral.Text = "$ 0.00";
            // 
            // lblTotalMano
            // 
            this.lblTotalMano.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalMano.AutoSize = true;
            this.lblTotalMano.Location = new System.Drawing.Point(499, 35);
            this.lblTotalMano.Name = "lblTotalMano";
            this.lblTotalMano.Size = new System.Drawing.Size(41, 16);
            this.lblTotalMano.TabIndex = 7;
            this.lblTotalMano.Text = "$ 0.00";
            // 
            // lblTotalRepuestos
            // 
            this.lblTotalRepuestos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalRepuestos.AutoSize = true;
            this.lblTotalRepuestos.Location = new System.Drawing.Point(499, 15);
            this.lblTotalRepuestos.Name = "lblTotalRepuestos";
            this.lblTotalRepuestos.Size = new System.Drawing.Size(41, 16);
            this.lblTotalRepuestos.TabIndex = 6;
            this.lblTotalRepuestos.Text = "$ 0.00";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(364, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(238, 10);
            this.label8.TabIndex = 3;
            this.label8.Text = "-------------------------------------------------------";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(365, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Total general : ";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(365, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Total mano de obra : ";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(365, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Total repuestos : ";
            // 
            // btnVistaPrevia
            // 
            this.btnVistaPrevia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVistaPrevia.Image = ((System.Drawing.Image)(resources.GetObject("btnVistaPrevia.Image")));
            this.btnVistaPrevia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVistaPrevia.Location = new System.Drawing.Point(12, 615);
            this.btnVistaPrevia.Name = "btnVistaPrevia";
            this.btnVistaPrevia.Size = new System.Drawing.Size(111, 30);
            this.btnVistaPrevia.TabIndex = 4;
            this.btnVistaPrevia.Text = "Vista previa";
            this.btnVistaPrevia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVistaPrevia.UseVisualStyleBackColor = true;
            this.btnVistaPrevia.Click += new System.EventHandler(this.btnVistaPrevia_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(521, 615);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(88, 30);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAbrirHistorial
            // 
            this.btnAbrirHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbrirHistorial.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirHistorial.Image")));
            this.btnAbrirHistorial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirHistorial.Location = new System.Drawing.Point(129, 615);
            this.btnAbrirHistorial.Name = "btnAbrirHistorial";
            this.btnAbrirHistorial.Size = new System.Drawing.Size(115, 30);
            this.btnAbrirHistorial.TabIndex = 6;
            this.btnAbrirHistorial.Text = "Ver ordenes";
            this.btnAbrirHistorial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAbrirHistorial.UseVisualStyleBackColor = true;
            this.btnAbrirHistorial.Click += new System.EventHandler(this.btnAbrirHistorial_Click);
            // 
            // ColTipoDet
            // 
            this.ColTipoDet.FillWeight = 34.88372F;
            this.ColTipoDet.HeaderText = "Tipo";
            this.ColTipoDet.Items.AddRange(new object[] {
            "MO",
            "R"});
            this.ColTipoDet.Name = "ColTipoDet";
            // 
            // ColDescripcionDet
            // 
            this.ColDescripcionDet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColDescripcionDet.FillWeight = 186.3689F;
            this.ColDescripcionDet.HeaderText = "Descripcion detalle";
            this.ColDescripcionDet.Name = "ColDescripcionDet";
            this.ColDescripcionDet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColPrecioDet
            // 
            this.ColPrecioDet.FillWeight = 78.74741F;
            this.ColPrecioDet.HeaderText = "Precio";
            this.ColPrecioDet.Name = "ColPrecioDet";
            this.ColPrecioDet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColOperacion
            // 
            this.ColOperacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColOperacion.HeaderText = "";
            this.ColOperacion.Image = ((System.Drawing.Image)(resources.GetObject("ColOperacion.Image")));
            this.ColOperacion.Name = "ColOperacion";
            this.ColOperacion.Width = 25;
            // 
            // frmCrearOrdenTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 649);
            this.Controls.Add(this.btnAbrirHistorial);
            this.Controls.Add(this.btnVistaPrevia);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpDetalleOrden);
            this.Controls.Add(this.grpCabecera);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.MinimumSize = new System.Drawing.Size(640, 560);
            this.Name = "frmCrearOrdenTrabajo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crear orden de trabajo";
            this.Load += new System.EventHandler(this.frmCrearOrdenTrabajo_Load);
            this.grpCabecera.ResumeLayout(false);
            this.grpCabecera.PerformLayout();
            this.grpDetalleOrden.ResumeLayout(false);
            this.grpDetalleOrden.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesOrden)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCabecera;
        private System.Windows.Forms.GroupBox grpDetalleOrden;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuscarVehiculo;
        private System.Windows.Forms.TextBox txtKm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVehiculo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVistaPrevia;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalGral;
        private System.Windows.Forms.Label lblTotalMano;
        private System.Windows.Forms.Label lblTotalRepuestos;
        private System.Windows.Forms.DataGridView dgvDetallesOrden;
        private System.Windows.Forms.Button btnAbrirHistorial;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColTipoDet;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescripcionDet;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrecioDet;
        private System.Windows.Forms.DataGridViewImageColumn ColOperacion;
    }
}