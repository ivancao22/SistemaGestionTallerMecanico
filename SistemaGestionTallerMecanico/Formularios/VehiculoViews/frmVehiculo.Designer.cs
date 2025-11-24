namespace Formularios.VehiculoViews
{
    partial class frmVehiculo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVehiculo));
            this.dgvVehiculos = new System.Windows.Forms.DataGridView();
            this.ColCodVehiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColModelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPatente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColKm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEditar = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColBorrar = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnAgregarVehiculo = new System.Windows.Forms.Button();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.optPatente = new System.Windows.Forms.RadioButton();
            this.optModelo = new System.Windows.Forms.RadioButton();
            this.optCliente = new System.Windows.Forms.RadioButton();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelDgv = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehiculos)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelDgv.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvVehiculos
            // 
            this.dgvVehiculos.AllowUserToAddRows = false;
            this.dgvVehiculos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVehiculos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVehiculos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodVehiculo,
            this.ColCliente,
            this.ColModelo,
            this.ColPatente,
            this.ColKm,
            this.ColEditar,
            this.ColBorrar});
            this.dgvVehiculos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVehiculos.GridColor = System.Drawing.SystemColors.Control;
            this.dgvVehiculos.Location = new System.Drawing.Point(4, 4);
            this.dgvVehiculos.Name = "dgvVehiculos";
            this.dgvVehiculos.RowHeadersVisible = false;
            this.dgvVehiculos.Size = new System.Drawing.Size(665, 361);
            this.dgvVehiculos.TabIndex = 25;
            this.dgvVehiculos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVehiculos_CellContentClick);
            // 
            // ColCodVehiculo
            // 
            this.ColCodVehiculo.HeaderText = "CodVehiculo";
            this.ColCodVehiculo.Name = "ColCodVehiculo";
            this.ColCodVehiculo.Visible = false;
            // 
            // ColCliente
            // 
            this.ColCliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColCliente.HeaderText = "Cliente";
            this.ColCliente.Name = "ColCliente";
            // 
            // ColModelo
            // 
            this.ColModelo.HeaderText = "Modelo";
            this.ColModelo.Name = "ColModelo";
            // 
            // ColPatente
            // 
            this.ColPatente.HeaderText = "Patente";
            this.ColPatente.Name = "ColPatente";
            // 
            // ColKm
            // 
            this.ColKm.HeaderText = "Kilometraje";
            this.ColKm.Name = "ColKm";
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
            // btnAgregarVehiculo
            // 
            this.btnAgregarVehiculo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarVehiculo.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarVehiculo.Image")));
            this.btnAgregarVehiculo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarVehiculo.Location = new System.Drawing.Point(543, 20);
            this.btnAgregarVehiculo.MaximumSize = new System.Drawing.Size(130, 30);
            this.btnAgregarVehiculo.Name = "btnAgregarVehiculo";
            this.btnAgregarVehiculo.Size = new System.Drawing.Size(126, 30);
            this.btnAgregarVehiculo.TabIndex = 24;
            this.btnAgregarVehiculo.Text = "Agregar vehiculo";
            this.btnAgregarVehiculo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarVehiculo.UseVisualStyleBackColor = true;
            this.btnAgregarVehiculo.Click += new System.EventHandler(this.btnAgregarVehiculo_Click);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltro.Location = new System.Drawing.Point(53, 27);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(220, 20);
            this.txtFiltro.TabIndex = 23;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Buscar :";
            // 
            // optPatente
            // 
            this.optPatente.AutoSize = true;
            this.optPatente.Location = new System.Drawing.Point(11, 6);
            this.optPatente.Name = "optPatente";
            this.optPatente.Size = new System.Drawing.Size(62, 17);
            this.optPatente.TabIndex = 26;
            this.optPatente.TabStop = true;
            this.optPatente.Text = "Patente";
            this.optPatente.UseVisualStyleBackColor = true;
            this.optPatente.CheckedChanged += new System.EventHandler(this.optPatente_CheckedChanged);
            // 
            // optModelo
            // 
            this.optModelo.AutoSize = true;
            this.optModelo.Location = new System.Drawing.Point(79, 6);
            this.optModelo.Name = "optModelo";
            this.optModelo.Size = new System.Drawing.Size(60, 17);
            this.optModelo.TabIndex = 27;
            this.optModelo.TabStop = true;
            this.optModelo.Text = "Modelo";
            this.optModelo.UseVisualStyleBackColor = true;
            this.optModelo.CheckedChanged += new System.EventHandler(this.optModelo_CheckedChanged);
            // 
            // optCliente
            // 
            this.optCliente.AutoSize = true;
            this.optCliente.Location = new System.Drawing.Point(147, 6);
            this.optCliente.Name = "optCliente";
            this.optCliente.Size = new System.Drawing.Size(57, 17);
            this.optCliente.TabIndex = 28;
            this.optCliente.TabStop = true;
            this.optCliente.Text = "Cliente";
            this.optCliente.UseVisualStyleBackColor = true;
            this.optCliente.CheckedChanged += new System.EventHandler(this.optCliente_CheckedChanged);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.optPatente);
            this.panelTop.Controls.Add(this.optModelo);
            this.panelTop.Controls.Add(this.optCliente);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.txtFiltro);
            this.panelTop.Controls.Add(this.btnAgregarVehiculo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(673, 56);
            this.panelTop.TabIndex = 29;
            // 
            // panelDgv
            // 
            this.panelDgv.Controls.Add(this.dgvVehiculos);
            this.panelDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDgv.Location = new System.Drawing.Point(4, 60);
            this.panelDgv.Name = "panelDgv";
            this.panelDgv.Padding = new System.Windows.Forms.Padding(4);
            this.panelDgv.Size = new System.Drawing.Size(673, 369);
            this.panelDgv.TabIndex = 30;
            // 
            // frmVehiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 433);
            this.Controls.Add(this.panelDgv);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVehiculo";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehiculos";
            this.Load += new System.EventHandler(this.frmVehiculo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehiculos)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelDgv.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVehiculos;
        private System.Windows.Forms.Button btnAgregarVehiculo;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton optPatente;
        private System.Windows.Forms.RadioButton optModelo;
        private System.Windows.Forms.RadioButton optCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodVehiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPatente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColKm;
        private System.Windows.Forms.DataGridViewImageColumn ColEditar;
        private System.Windows.Forms.DataGridViewImageColumn ColBorrar;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelDgv;
    }
}