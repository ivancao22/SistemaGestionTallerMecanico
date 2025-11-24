namespace Formularios.LibroDiarioViews
{
    partial class frmAgregarMovimiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgregarMovimiento));
            this.optCliente = new System.Windows.Forms.RadioButton();
            this.optProveedor = new System.Windows.Forms.RadioButton();
            this.optOtro = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.imgProveedor = new System.Windows.Forms.PictureBox();
            this.btnBuscarPersona = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.txtVehiculo = new System.Windows.Forms.TextBox();
            this.txtPersona = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBuscarVehiculo = new System.Windows.Forms.Button();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.cmbTipoMov = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgProveedor)).BeginInit();
            this.SuspendLayout();
            // 
            // optCliente
            // 
            this.optCliente.AutoSize = true;
            this.optCliente.Location = new System.Drawing.Point(90, 108);
            this.optCliente.Name = "optCliente";
            this.optCliente.Size = new System.Drawing.Size(57, 17);
            this.optCliente.TabIndex = 0;
            this.optCliente.TabStop = true;
            this.optCliente.Text = "Cliente";
            this.optCliente.UseVisualStyleBackColor = true;
            this.optCliente.CheckedChanged += new System.EventHandler(this.optCliente_CheckedChanged);
            // 
            // optProveedor
            // 
            this.optProveedor.AutoSize = true;
            this.optProveedor.Location = new System.Drawing.Point(158, 108);
            this.optProveedor.Name = "optProveedor";
            this.optProveedor.Size = new System.Drawing.Size(74, 17);
            this.optProveedor.TabIndex = 1;
            this.optProveedor.TabStop = true;
            this.optProveedor.Text = "Proveedor";
            this.optProveedor.UseVisualStyleBackColor = true;
            this.optProveedor.CheckedChanged += new System.EventHandler(this.optProveedor_CheckedChanged);
            // 
            // optOtro
            // 
            this.optOtro.AutoSize = true;
            this.optOtro.Location = new System.Drawing.Point(242, 108);
            this.optOtro.Name = "optOtro";
            this.optOtro.Size = new System.Drawing.Size(50, 17);
            this.optOtro.TabIndex = 2;
            this.optOtro.TabStop = true;
            this.optOtro.Text = "Otros";
            this.optOtro.UseVisualStyleBackColor = true;
            this.optOtro.CheckedChanged += new System.EventHandler(this.optOtro_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(144, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Movimiento de :";
            // 
            // imgProveedor
            // 
            this.imgProveedor.Image = ((System.Drawing.Image)(resources.GetObject("imgProveedor.Image")));
            this.imgProveedor.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgProveedor.InitialImage")));
            this.imgProveedor.Location = new System.Drawing.Point(163, 21);
            this.imgProveedor.Name = "imgProveedor";
            this.imgProveedor.Size = new System.Drawing.Size(51, 53);
            this.imgProveedor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgProveedor.TabIndex = 20;
            this.imgProveedor.TabStop = false;
            this.imgProveedor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // btnBuscarPersona
            // 
            this.btnBuscarPersona.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarPersona.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarPersona.Image")));
            this.btnBuscarPersona.Location = new System.Drawing.Point(315, 147);
            this.btnBuscarPersona.Name = "btnBuscarPersona";
            this.btnBuscarPersona.Size = new System.Drawing.Size(30, 28);
            this.btnBuscarPersona.TabIndex = 30;
            this.btnBuscarPersona.UseVisualStyleBackColor = true;
            this.btnBuscarPersona.Click += new System.EventHandler(this.btnBuscarPersona_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(284, 470);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 29;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtMonto
            // 
            this.txtMonto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonto.Location = new System.Drawing.Point(120, 329);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(144, 20);
            this.txtMonto.TabIndex = 28;
            // 
            // txtVehiculo
            // 
            this.txtVehiculo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVehiculo.Location = new System.Drawing.Point(121, 199);
            this.txtVehiculo.Name = "txtVehiculo";
            this.txtVehiculo.Size = new System.Drawing.Size(183, 20);
            this.txtVehiculo.TabIndex = 26;
            // 
            // txtPersona
            // 
            this.txtPersona.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPersona.Location = new System.Drawing.Point(121, 151);
            this.txtPersona.Name = "txtPersona";
            this.txtPersona.Size = new System.Drawing.Size(183, 20);
            this.txtPersona.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 332);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Monto * :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Metodo de pago * :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Vehiculo :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Cliente/Proveedor :";
            // 
            // btnBuscarVehiculo
            // 
            this.btnBuscarVehiculo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarVehiculo.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarVehiculo.Image")));
            this.btnBuscarVehiculo.Location = new System.Drawing.Point(315, 194);
            this.btnBuscarVehiculo.Name = "btnBuscarVehiculo";
            this.btnBuscarVehiculo.Size = new System.Drawing.Size(30, 28);
            this.btnBuscarVehiculo.TabIndex = 32;
            this.btnBuscarVehiculo.UseVisualStyleBackColor = true;
            this.btnBuscarVehiculo.Click += new System.EventHandler(this.btnBuscarVehiculo_Click);
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Location = new System.Drawing.Point(121, 286);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(143, 21);
            this.cmbMetodoPago.TabIndex = 33;
            // 
            // cmbTipoMov
            // 
            this.cmbTipoMov.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipoMov.FormattingEnabled = true;
            this.cmbTipoMov.Location = new System.Drawing.Point(122, 243);
            this.cmbTipoMov.Name = "cmbTipoMov";
            this.cmbTipoMov.Size = new System.Drawing.Size(143, 21);
            this.cmbTipoMov.TabIndex = 36;
            this.cmbTipoMov.SelectedIndexChanged += new System.EventHandler(this.cmbTipoMov_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Tipo movimiento * :";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(160, 368);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Comentario";
            // 
            // txtComentario
            // 
            this.txtComentario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComentario.Location = new System.Drawing.Point(30, 384);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(315, 65);
            this.txtComentario.TabIndex = 38;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.Location = new System.Drawing.Point(12, 473);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 15);
            this.label8.TabIndex = 39;
            // Replaced emoji with plain text to ensure Win7 compatibility
            this.label8.Text = "* Campos obligatorios";
            // 
            // frmAgregarMovimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // padding interior para separación visual y comportamiento responsivo
            this.ClientSize = new System.Drawing.Size(371, 502);
            this.Padding = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(380, 520);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbTipoMov);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbMetodoPago);
            this.Controls.Add(this.btnBuscarVehiculo);
            this.Controls.Add(this.btnBuscarPersona);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.txtVehiculo);
            this.Controls.Add(this.txtPersona);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.imgProveedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.optOtro);
            this.Controls.Add(this.optProveedor);
            this.Controls.Add(this.optCliente);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAgregarMovimiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar movimiento";
            this.Load += new System.EventHandler(this.frmAgregarMovimiento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgProveedor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton optCliente;
        private System.Windows.Forms.RadioButton optProveedor;
        private System.Windows.Forms.RadioButton optOtro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox imgProveedor;
        private System.Windows.Forms.Button btnBuscarPersona;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.TextBox txtVehiculo;
        private System.Windows.Forms.TextBox txtPersona;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBuscarVehiculo;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.ComboBox cmbTipoMov;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Label label8;
    }
}