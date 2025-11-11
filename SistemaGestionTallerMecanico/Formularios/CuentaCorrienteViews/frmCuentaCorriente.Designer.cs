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
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvCuentaCorriente = new System.Windows.Forms.DataGridView();
            this.ColFechaMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTipoMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescripcionMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.lblImporteBalance = new System.Windows.Forms.Label();
            this.btnExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentaCorriente)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buscar cliente/proveedor :";
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(146, 11);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(150, 20);
            this.txtFiltro.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(302, 7);
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
            this.ColFechaMovimiento,
            this.ColTipoMovimiento,
            this.ColCliente,
            this.ColProveedor,
            this.ColImporte,
            this.ColDescripcionMovimiento});
            this.dgvCuentaCorriente.Location = new System.Drawing.Point(7, 41);
            this.dgvCuentaCorriente.Name = "dgvCuentaCorriente";
            this.dgvCuentaCorriente.RowHeadersVisible = false;
            this.dgvCuentaCorriente.Size = new System.Drawing.Size(783, 357);
            this.dgvCuentaCorriente.TabIndex = 4;
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
            // ColCliente
            // 
            this.ColCliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColCliente.HeaderText = "Cliente";
            this.ColCliente.Name = "ColCliente";
            this.ColCliente.Width = 170;
            // 
            // ColProveedor
            // 
            this.ColProveedor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColProveedor.HeaderText = "Proveedor";
            this.ColProveedor.Name = "ColProveedor";
            this.ColProveedor.Width = 170;
            // 
            // ColImporte
            // 
            this.ColImporte.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColImporte.HeaderText = "Importe";
            this.ColImporte.Name = "ColImporte";
            this.ColImporte.Width = 60;
            // 
            // ColDescripcionMovimiento
            // 
            this.ColDescripcionMovimiento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColDescripcionMovimiento.HeaderText = "Descripcion movimiento";
            this.ColDescripcionMovimiento.Name = "ColDescripcionMovimiento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Balance : $";
            // 
            // lblImporteBalance
            // 
            this.lblImporteBalance.AutoSize = true;
            this.lblImporteBalance.Location = new System.Drawing.Point(69, 410);
            this.lblImporteBalance.Name = "lblImporteBalance";
            this.lblImporteBalance.Size = new System.Drawing.Size(28, 13);
            this.lblImporteBalance.TabIndex = 6;
            this.lblImporteBalance.Text = "0.00";
            // 
            // btnExcel
            // 
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.Location = new System.Drawing.Point(758, 4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(32, 31);
            this.btnExcel.TabIndex = 7;
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // frmCuentaCorriente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 430);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.lblImporteBalance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvCuentaCorriente);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtFiltro);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCuentaCorriente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuenta corriente ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentaCorriente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvCuentaCorriente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblImporteBalance;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFechaMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTipoMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColImporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescripcionMovimiento;
    }
}