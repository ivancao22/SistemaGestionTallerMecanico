namespace Formularios.CuentaCorrienteViews
{
    partial class frmCuentaCorriente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCuentaCorriente));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvCuentaCorriente = new System.Windows.Forms.DataGridView();
            this.lblBalance = new System.Windows.Forms.Label();
            this.btnGenerarExcel = new System.Windows.Forms.Button();
            this.btnGenerarPDF = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelDgv = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.ColCodMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFechaMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTipoMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPersona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescripcionMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentaCorriente)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelDgv.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buscar cliente/proveedor :";
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusqueda.Location = new System.Drawing.Point(149, 11);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(220, 20);
            this.txtBusqueda.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(379, 7);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(30, 28);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvCuentaCorriente
            // 
            this.dgvCuentaCorriente.AllowUserToAddRows = false;
            this.dgvCuentaCorriente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuentaCorriente.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodMovimiento,
            this.ColFechaMovimiento,
            this.ColTipoMovimiento,
            this.ColPersona,
            this.ColImporte,
            this.ColDescripcionMovimiento});
            this.dgvCuentaCorriente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCuentaCorriente.Location = new System.Drawing.Point(4, 4);
            this.dgvCuentaCorriente.Name = "dgvCuentaCorriente";
            this.dgvCuentaCorriente.RowHeadersVisible = false;
            this.dgvCuentaCorriente.Size = new System.Drawing.Size(780, 353);
            this.dgvCuentaCorriente.TabIndex = 4;
            // 
            // lblBalance
            // 
            this.lblBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(10, 6);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(61, 13);
            this.lblBalance.TabIndex = 5;
            this.lblBalance.Text = "Balance : $";
            // 
            // btnGenerarExcel
            // 
            this.btnGenerarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarExcel.Image")));
            this.btnGenerarExcel.Location = new System.Drawing.Point(754, 7);
            this.btnGenerarExcel.Name = "btnGenerarExcel";
            this.btnGenerarExcel.Size = new System.Drawing.Size(32, 31);
            this.btnGenerarExcel.TabIndex = 7;
            this.btnGenerarExcel.UseVisualStyleBackColor = true;
            this.btnGenerarExcel.Click += new System.EventHandler(this.btnGenerarExcel_Click);
            // 
            // btnGenerarPDF
            // 
            this.btnGenerarPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPDF.Image")));
            this.btnGenerarPDF.Location = new System.Drawing.Point(716, 7);
            this.btnGenerarPDF.Name = "btnGenerarPDF";
            this.btnGenerarPDF.Size = new System.Drawing.Size(32, 31);
            this.btnGenerarPDF.TabIndex = 8;
            this.btnGenerarPDF.UseVisualStyleBackColor = true;
            this.btnGenerarPDF.Click += new System.EventHandler(this.btnGenerarPDF_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.txtBusqueda);
            this.panelTop.Controls.Add(this.btnBuscar);
            this.panelTop.Controls.Add(this.btnGenerarPDF);
            this.panelTop.Controls.Add(this.btnGenerarExcel);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(788, 42);
            this.panelTop.TabIndex = 9;
            // 
            // panelDgv
            // 
            this.panelDgv.Controls.Add(this.dgvCuentaCorriente);
            this.panelDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDgv.Location = new System.Drawing.Point(4, 46);
            this.panelDgv.Name = "panelDgv";
            this.panelDgv.Padding = new System.Windows.Forms.Padding(4);
            this.panelDgv.Size = new System.Drawing.Size(788, 361);
            this.panelDgv.TabIndex = 10;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblBalance);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(4, 407);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(788, 19);
            this.panelBottom.TabIndex = 11;
            // 
            // ColCodMovimiento
            // 
            this.ColCodMovimiento.HeaderText = "CodMovimiento";
            this.ColCodMovimiento.Name = "ColCodMovimiento";
            this.ColCodMovimiento.Visible = false;
            // 
            // ColFechaMovimiento
            // 
            this.ColFechaMovimiento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColFechaMovimiento.HeaderText = "Fecha movimiento";
            this.ColFechaMovimiento.Name = "ColFechaMovimiento";
            this.ColFechaMovimiento.Width = 120;
            // 
            // ColTipoMovimiento
            // 
            this.ColTipoMovimiento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColTipoMovimiento.HeaderText = "Tipo movimiento";
            this.ColTipoMovimiento.Name = "ColTipoMovimiento";
            this.ColTipoMovimiento.Width = 110;
            // 
            // ColPersona
            // 
            this.ColPersona.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColPersona.HeaderText = "Persona";
            this.ColPersona.Name = "ColPersona";
            this.ColPersona.Width = 170;
            // 
            // ColImporte
            // 
            this.ColImporte.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColImporte.HeaderText = "Importe";
            this.ColImporte.Name = "ColImporte";
            this.ColImporte.Width = 90;
            // 
            // ColDescripcionMovimiento
            // 
            this.ColDescripcionMovimiento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColDescripcionMovimiento.HeaderText = "Descripcion movimiento";
            this.ColDescripcionMovimiento.Name = "ColDescripcionMovimiento";
            // 
            // frmCuentaCorriente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 430);
            this.Controls.Add(this.panelDgv);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCuentaCorriente";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuenta corriente ";
            this.Load += new System.EventHandler(this.frmCuentaCorriente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentaCorriente)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelDgv.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvCuentaCorriente;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Button btnGenerarExcel;
        private System.Windows.Forms.Button btnGenerarPDF;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelDgv;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFechaMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTipoMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPersona;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColImporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescripcionMovimiento;
    }
}