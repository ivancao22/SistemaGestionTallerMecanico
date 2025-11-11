namespace Formularios.ProveedorViews
{
    partial class frmProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProveedor));
            this.dgvProveedores = new System.Windows.Forms.DataGridView();
            this.btnAgregarProveedor = new System.Windows.Forms.Button();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ColCodProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCuit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEditar = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColBorrar = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProveedores
            // 
            this.dgvProveedores.AllowUserToAddRows = false;
            this.dgvProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProveedores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodProveedor,
            this.ColNombre,
            this.ColRazonSocial,
            this.ColCuit,
            this.ColEmail,
            this.ColTelefono,
            this.ColEditar,
            this.ColBorrar});
            this.dgvProveedores.GridColor = System.Drawing.SystemColors.Control;
            this.dgvProveedores.Location = new System.Drawing.Point(6, 39);
            this.dgvProveedores.Name = "dgvProveedores";
            this.dgvProveedores.RowHeadersVisible = false;
            this.dgvProveedores.Size = new System.Drawing.Size(668, 367);
            this.dgvProveedores.TabIndex = 21;
            this.dgvProveedores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProveedores_CellContentClick);
            // 
            // btnAgregarProveedor
            // 
            this.btnAgregarProveedor.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarProveedor.Image")));
            this.btnAgregarProveedor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarProveedor.Location = new System.Drawing.Point(548, 3);
            this.btnAgregarProveedor.Name = "btnAgregarProveedor";
            this.btnAgregarProveedor.Size = new System.Drawing.Size(126, 30);
            this.btnAgregarProveedor.TabIndex = 20;
            this.btnAgregarProveedor.Text = "Agregar proveedor";
            this.btnAgregarProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarProveedor.UseVisualStyleBackColor = true;
            this.btnAgregarProveedor.Click += new System.EventHandler(this.btnAgregarProveedor_Click);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(103, 10);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(134, 20);
            this.txtFiltro.TabIndex = 19;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Buscar proveedor :";
            // 
            // ColCodProveedor
            // 
            this.ColCodProveedor.HeaderText = "CodProveedor";
            this.ColCodProveedor.Name = "ColCodProveedor";
            this.ColCodProveedor.Visible = false;
            // 
            // ColNombre
            // 
            this.ColNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColNombre.HeaderText = "Nombre";
            this.ColNombre.Name = "ColNombre";
            // 
            // ColRazonSocial
            // 
            this.ColRazonSocial.HeaderText = "Razon social";
            this.ColRazonSocial.Name = "ColRazonSocial";
            this.ColRazonSocial.Width = 120;
            // 
            // ColCuit
            // 
            this.ColCuit.HeaderText = "Cuit";
            this.ColCuit.Name = "ColCuit";
            this.ColCuit.Width = 80;
            // 
            // ColEmail
            // 
            this.ColEmail.HeaderText = "Email";
            this.ColEmail.Name = "ColEmail";
            this.ColEmail.Width = 140;
            // 
            // ColTelefono
            // 
            this.ColTelefono.HeaderText = "Telefono";
            this.ColTelefono.Name = "ColTelefono";
            this.ColTelefono.Width = 110;
            // 
            // ColEditar
            // 
            this.ColEditar.HeaderText = "";
            this.ColEditar.Image = ((System.Drawing.Image)(resources.GetObject("ColEditar.Image")));
            this.ColEditar.Name = "ColEditar";
            this.ColEditar.Width = 20;
            // 
            // ColBorrar
            // 
            this.ColBorrar.HeaderText = "";
            this.ColBorrar.Image = ((System.Drawing.Image)(resources.GetObject("ColBorrar.Image")));
            this.ColBorrar.Name = "ColBorrar";
            this.ColBorrar.Width = 20;
            // 
            // frmProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 410);
            this.Controls.Add(this.dgvProveedores);
            this.Controls.Add(this.btnAgregarProveedor);
            this.Controls.Add(this.txtFiltro);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proveedores";
            this.Load += new System.EventHandler(this.frmProveedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Button btnAgregarProveedor;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRazonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCuit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTelefono;
        private System.Windows.Forms.DataGridViewImageColumn ColEditar;
        private System.Windows.Forms.DataGridViewImageColumn ColBorrar;
    }
}