namespace Formularios.LibroDiarioViews
{
    partial class frmLibroDiario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLibroDiario));
            this.lblMontoCaja = new System.Windows.Forms.Label();
            this.lblTextCaja = new System.Windows.Forms.Label();
            this.btnPresupuesto = new System.Windows.Forms.Button();
            this.btnAgregarMovimiento = new System.Windows.Forms.Button();
            this.btnRecargarDgv = new System.Windows.Forms.Button();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.ColCodMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTipoUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTipoMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFechaMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMedioPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCodOrdenTrabajo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEditar = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColBorrar = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelDgv = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelDgv.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMontoCaja
            // 
            this.lblMontoCaja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMontoCaja.AutoSize = true;
            this.lblMontoCaja.Location = new System.Drawing.Point(92, 11);
            this.lblMontoCaja.Name = "lblMontoCaja";
            this.lblMontoCaja.Size = new System.Drawing.Size(37, 13);
            this.lblMontoCaja.TabIndex = 25;
            this.lblMontoCaja.Text = "$ 0.00";
            // 
            // lblTextCaja
            // 
            this.lblTextCaja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTextCaja.AutoSize = true;
            this.lblTextCaja.Location = new System.Drawing.Point(11, 11);
            this.lblTextCaja.Name = "lblTextCaja";
            this.lblTextCaja.Size = new System.Drawing.Size(84, 13);
            this.lblTextCaja.TabIndex = 24;
            this.lblTextCaja.Text = "Monto en caja : ";
            // 
            // btnPresupuesto
            // 
            this.btnPresupuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPresupuesto.Image = ((System.Drawing.Image)(resources.GetObject("btnPresupuesto.Image")));
            this.btnPresupuesto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPresupuesto.Location = new System.Drawing.Point(512, 0);
            this.btnPresupuesto.Name = "btnPresupuesto";
            this.btnPresupuesto.Size = new System.Drawing.Size(139, 32);
            this.btnPresupuesto.TabIndex = 23;
            this.btnPresupuesto.Text = "Realizar presupuesto";
            this.btnPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPresupuesto.UseVisualStyleBackColor = true;
            this.btnPresupuesto.Click += new System.EventHandler(this.btnPresupuesto_Click);
            // 
            // btnAgregarMovimiento
            // 
            this.btnAgregarMovimiento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarMovimiento.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarMovimiento.Image")));
            this.btnAgregarMovimiento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarMovimiento.Location = new System.Drawing.Point(653, 0);
            this.btnAgregarMovimiento.Name = "btnAgregarMovimiento";
            this.btnAgregarMovimiento.Size = new System.Drawing.Size(132, 32);
            this.btnAgregarMovimiento.TabIndex = 22;
            this.btnAgregarMovimiento.Text = "Agregar movimiento";
            this.btnAgregarMovimiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarMovimiento.UseVisualStyleBackColor = true;
            this.btnAgregarMovimiento.Click += new System.EventHandler(this.btnAgregarMovimiento_Click);
            // 
            // btnRecargarDgv
            // 
            this.btnRecargarDgv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecargarDgv.Image = ((System.Drawing.Image)(resources.GetObject("btnRecargarDgv.Image")));
            this.btnRecargarDgv.Location = new System.Drawing.Point(714, 32);
            this.btnRecargarDgv.Name = "btnRecargarDgv";
            this.btnRecargarDgv.Size = new System.Drawing.Size(32, 31);
            this.btnRecargarDgv.TabIndex = 21;
            this.btnRecargarDgv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecargarDgv.UseVisualStyleBackColor = true;
            this.btnRecargarDgv.Click += new System.EventHandler(this.btnRecargarDgv_Click);
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.AllowUserToAddRows = false;
            this.dgvMovimientos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodMovimiento,
            this.ColNombre,
            this.ColTipoUser,
            this.ColTipoMovimiento,
            this.ColFechaMovimiento,
            this.ColMedioPago,
            this.ColImporte,
            this.ColDescripcion,
            this.ColCodOrdenTrabajo,
            this.ColEditar,
            this.ColBorrar});
            this.dgvMovimientos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMovimientos.Location = new System.Drawing.Point(6, 6);
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.RowHeadersVisible = false;
            this.dgvMovimientos.Size = new System.Drawing.Size(778, 354);
            this.dgvMovimientos.TabIndex = 20;
            this.dgvMovimientos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMovimientos_CellContentClick);
            // 
            // ColCodMovimiento
            // 
            this.ColCodMovimiento.HeaderText = "Cod movimiento";
            this.ColCodMovimiento.Name = "ColCodMovimiento";
            this.ColCodMovimiento.Visible = false;
            // 
            // ColNombre
            // 
            this.ColNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColNombre.HeaderText = "Nombre";
            this.ColNombre.Name = "ColNombre";
            // 
            // ColTipoUser
            // 
            this.ColTipoUser.HeaderText = "Tipo usuario";
            this.ColTipoUser.Name = "ColTipoUser";
            // 
            // ColTipoMovimiento
            // 
            this.ColTipoMovimiento.HeaderText = "Tipo movimiento";
            this.ColTipoMovimiento.Name = "ColTipoMovimiento";
            // 
            // ColFechaMovimiento
            // 
            this.ColFechaMovimiento.HeaderText = "Fecha movimiento";
            this.ColFechaMovimiento.Name = "ColFechaMovimiento";
            // 
            // ColMedioPago
            // 
            this.ColMedioPago.HeaderText = "Medio pago";
            this.ColMedioPago.Name = "ColMedioPago";
            // 
            // ColImporte
            // 
            this.ColImporte.HeaderText = "Importe";
            this.ColImporte.Name = "ColImporte";
            // 
            // ColDescripcion
            // 
            this.ColDescripcion.HeaderText = "Descripcion";
            this.ColDescripcion.Name = "ColDescripcion";
            // 
            // ColCodOrdenTrabajo
            // 
            this.ColCodOrdenTrabajo.HeaderText = "OrdenAsociada";
            this.ColCodOrdenTrabajo.Name = "ColCodOrdenTrabajo";
            this.ColCodOrdenTrabajo.Visible = false;
            // 
            // ColEditar
            // 
            this.ColEditar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColEditar.HeaderText = "";
            this.ColEditar.Image = ((System.Drawing.Image)(resources.GetObject("ColEditar.Image")));
            this.ColEditar.Name = "ColEditar";
            this.ColEditar.Width = 20;
            // 
            // ColBorrar
            // 
            this.ColBorrar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColBorrar.HeaderText = "";
            this.ColBorrar.Image = ((System.Drawing.Image)(resources.GetObject("ColBorrar.Image")));
            this.ColBorrar.Name = "ColBorrar";
            this.ColBorrar.Width = 20;
            // 
            // txtFiltro
            // 
            this.txtFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltro.Location = new System.Drawing.Point(126, 41);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(267, 20);
            this.txtFiltro.TabIndex = 18;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "👤 Cliente/Proveedor : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "📅 Fecha caja :";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(94, 12);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(95, 20);
            this.dtpFecha.TabIndex = 13;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.Location = new System.Drawing.Point(752, 32);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(32, 31);
            this.btnExcel.TabIndex = 26;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.dtpFecha);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.txtFiltro);
            this.panelTop.Controls.Add(this.btnRecargarDgv);
            this.panelTop.Controls.Add(this.btnExcel);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(6);
            this.panelTop.Size = new System.Drawing.Size(790, 72);
            this.panelTop.TabIndex = 0;
            // 
            // panelDgv
            // 
            this.panelDgv.Controls.Add(this.dgvMovimientos);
            this.panelDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDgv.Location = new System.Drawing.Point(4, 76);
            this.panelDgv.Name = "panelDgv";
            this.panelDgv.Padding = new System.Windows.Forms.Padding(6);
            this.panelDgv.Size = new System.Drawing.Size(790, 366);
            this.panelDgv.TabIndex = 1;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblTextCaja);
            this.panelBottom.Controls.Add(this.lblMontoCaja);
            this.panelBottom.Controls.Add(this.btnPresupuesto);
            this.panelBottom.Controls.Add(this.btnAgregarMovimiento);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(4, 442);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(6);
            this.panelBottom.Size = new System.Drawing.Size(790, 35);
            this.panelBottom.TabIndex = 2;
            // 
            // frmLibroDiario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 481);
            this.Controls.Add(this.panelDgv);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 420);
            this.Name = "frmLibroDiario";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Libro diario";
            this.Load += new System.EventHandler(this.frmLibroDiario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelDgv.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMontoCaja;
        private System.Windows.Forms.Label lblTextCaja;
        private System.Windows.Forms.Button btnPresupuesto;
        private System.Windows.Forms.Button btnAgregarMovimiento;
        private System.Windows.Forms.Button btnRecargarDgv;
        private System.Windows.Forms.DataGridView dgvMovimientos;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTipoUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTipoMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFechaMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMedioPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColImporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodOrdenTrabajo;
        private System.Windows.Forms.DataGridViewImageColumn ColEditar;
        private System.Windows.Forms.DataGridViewImageColumn ColBorrar;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelDgv;
        private System.Windows.Forms.Panel panelBottom;
    }
}