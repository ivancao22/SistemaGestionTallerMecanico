namespace SistemaGestionTallerMecanico
{
    partial class frmMenuPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuPrincipal));
            this.btnLibroDiario = new System.Windows.Forms.Button();
            this.btnCuentaCorriente = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnProveedores = new System.Windows.Forms.Button();
            this.btnVehiculos = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLibroDiario
            // 
            this.btnLibroDiario.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLibroDiario.Image = ((System.Drawing.Image)(resources.GetObject("btnLibroDiario.Image")));
            this.btnLibroDiario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLibroDiario.Location = new System.Drawing.Point(33, 148);
            this.btnLibroDiario.Name = "btnLibroDiario";
            this.btnLibroDiario.Size = new System.Drawing.Size(121, 42);
            this.btnLibroDiario.TabIndex = 1;
            this.btnLibroDiario.Text = "Libro diario  ";
            this.btnLibroDiario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLibroDiario.UseVisualStyleBackColor = true;
            this.btnLibroDiario.Click += new System.EventHandler(this.btnLibroDiario_Click);
            // 
            // btnCuentaCorriente
            // 
            this.btnCuentaCorriente.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.btnCuentaCorriente.Image = ((System.Drawing.Image)(resources.GetObject("btnCuentaCorriente.Image")));
            this.btnCuentaCorriente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCuentaCorriente.Location = new System.Drawing.Point(181, 148);
            this.btnCuentaCorriente.Name = "btnCuentaCorriente";
            this.btnCuentaCorriente.Size = new System.Drawing.Size(121, 42);
            this.btnCuentaCorriente.TabIndex = 2;
            this.btnCuentaCorriente.Text = "Cta. corriente";
            this.btnCuentaCorriente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCuentaCorriente.UseVisualStyleBackColor = true;
            this.btnCuentaCorriente.Click += new System.EventHandler(this.btnCuentaCorriente_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.btnClientes.Image = ((System.Drawing.Image)(resources.GetObject("btnClientes.Image")));
            this.btnClientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClientes.Location = new System.Drawing.Point(181, 217);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(121, 42);
            this.btnClientes.TabIndex = 3;
            this.btnClientes.Text = "Clientes        ";
            this.btnClientes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnProveedores
            // 
            this.btnProveedores.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.btnProveedores.Image = ((System.Drawing.Image)(resources.GetObject("btnProveedores.Image")));
            this.btnProveedores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProveedores.Location = new System.Drawing.Point(33, 217);
            this.btnProveedores.Name = "btnProveedores";
            this.btnProveedores.Size = new System.Drawing.Size(121, 42);
            this.btnProveedores.TabIndex = 4;
            this.btnProveedores.Text = "Proveedores";
            this.btnProveedores.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProveedores.UseVisualStyleBackColor = true;
            this.btnProveedores.Click += new System.EventHandler(this.btnProveedores_Click);
            // 
            // btnVehiculos
            // 
            this.btnVehiculos.Font = new System.Drawing.Font("Segoe UI Emoji", 9F);
            this.btnVehiculos.Image = ((System.Drawing.Image)(resources.GetObject("btnVehiculos.Image")));
            this.btnVehiculos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVehiculos.Location = new System.Drawing.Point(105, 285);
            this.btnVehiculos.Name = "btnVehiculos";
            this.btnVehiculos.Size = new System.Drawing.Size(121, 42);
            this.btnVehiculos.TabIndex = 5;
            this.btnVehiculos.Text = "Vehiculos    ";
            this.btnVehiculos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVehiculos.UseVisualStyleBackColor = true;
            this.btnVehiculos.Click += new System.EventHandler(this.btnVehiculos_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(73, -58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(185, 181);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // frmMenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 349);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnVehiculos);
            this.Controls.Add(this.btnProveedores);
            this.Controls.Add(this.btnClientes);
            this.Controls.Add(this.btnCuentaCorriente);
            this.Controls.Add(this.btnLibroDiario);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLibroDiario;
        private System.Windows.Forms.Button btnCuentaCorriente;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnProveedores;
        private System.Windows.Forms.Button btnVehiculos;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

