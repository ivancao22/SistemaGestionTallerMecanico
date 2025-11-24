namespace Formularios.LibroDiarioViews
{
    partial class frmAbrirCajaDiaria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbrirCajaDiaria));
            this.btnObtenerMonto = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.grpBoxCajaD = new System.Windows.Forms.GroupBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblSaldoAyer = new System.Windows.Forms.Label();
            this.grpBoxCajaAyer = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBoxCajaD.SuspendLayout();
            this.grpBoxCajaAyer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnObtenerMonto
            // 
            this.btnObtenerMonto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnObtenerMonto.Location = new System.Drawing.Point(126, 228);
            this.btnObtenerMonto.Name = "btnObtenerMonto";
            this.btnObtenerMonto.Size = new System.Drawing.Size(83, 35);
            this.btnObtenerMonto.TabIndex = 0;
            this.btnObtenerMonto.Text = "Usar saldo final de ayer";
            this.btnObtenerMonto.UseVisualStyleBackColor = true;
            this.btnObtenerMonto.Click += new System.EventHandler(this.btnObtenerMonto_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ingrese el monto inicial de la caja : *";
            // 
            // txtMonto
            // 
            this.txtMonto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMonto.Location = new System.Drawing.Point(110, 194);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(113, 20);
            this.txtMonto.TabIndex = 2;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(240, 287);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // grpBoxCajaD
            // 
            this.grpBoxCajaD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxCajaD.Controls.Add(this.lblUsuario);
            this.grpBoxCajaD.Controls.Add(this.lblFecha);
            this.grpBoxCajaD.Location = new System.Drawing.Point(12, 12);
            this.grpBoxCajaD.Name = "grpBoxCajaD";
            this.grpBoxCajaD.Size = new System.Drawing.Size(303, 78);
            this.grpBoxCajaD.TabIndex = 4;
            this.grpBoxCajaD.TabStop = false;
            this.grpBoxCajaD.Text = "Caja diaria";
            // 
            // lblFecha
            // 
            this.lblFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(23, 23);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(219, 15);
            this.lblFecha.TabIndex = 5;
            this.lblFecha.Text = "📅 Fecha: Viernes, 24 de Octubre 2025";
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(23, 46);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(131, 15);
            this.lblUsuario.TabIndex = 6;
            this.lblUsuario.Text = "👤 Usuario: ivancao22 ";
            // 
            // lblSaldoAyer
            // 
            this.lblSaldoAyer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSaldoAyer.AutoSize = true;
            this.lblSaldoAyer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldoAyer.Location = new System.Drawing.Point(23, 23);
            this.lblSaldoAyer.Name = "lblSaldoAyer";
            this.lblSaldoAyer.Size = new System.Drawing.Size(193, 15);
            this.lblSaldoAyer.TabIndex = 5;
            this.lblSaldoAyer.Text = "📊 Saldo final de ayer: $13,520.50";
            // 
            // grpBoxCajaAyer
            // 
            this.grpBoxCajaAyer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxCajaAyer.Controls.Add(this.label3);
            this.grpBoxCajaAyer.Controls.Add(this.lblSaldoAyer);
            this.grpBoxCajaAyer.Location = new System.Drawing.Point(12, 96);
            this.grpBoxCajaAyer.Name = "grpBoxCajaAyer";
            this.grpBoxCajaAyer.Size = new System.Drawing.Size(303, 53);
            this.grpBoxCajaAyer.TabIndex = 6;
            this.grpBoxCajaAyer.TabStop = false;
            this.grpBoxCajaAyer.Text = "Caja diaria dia anterior";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "ℹ️ * Campos obligatorios";
            // 
            // frmAbrirCajaDiaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // Mantener padding interior para separación visual
            this.ClientSize = new System.Drawing.Size(327, 322);
            this.Padding = new System.Windows.Forms.Padding(6);
            // Permitir redimensionar pero con límites (cambio mínimo y seguro)
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(327, 322);
            this.MaximumSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grpBoxCajaAyer);
            this.Controls.Add(this.grpBoxCajaD);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnObtenerMonto);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAbrirCajaDiaria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Abrir caja diaria";
            this.Load += new System.EventHandler(this.frmAbrirCajaDiaria_Load);
            this.grpBoxCajaD.ResumeLayout(false);
            this.grpBoxCajaD.PerformLayout();
            this.grpBoxCajaAyer.ResumeLayout(false);
            this.grpBoxCajaAyer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnObtenerMonto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox grpBoxCajaD;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblSaldoAyer;
        private System.Windows.Forms.GroupBox grpBoxCajaAyer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}