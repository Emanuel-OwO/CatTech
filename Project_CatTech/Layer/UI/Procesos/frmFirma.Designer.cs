namespace Project_CatTech.Layer.UI.Procesos
{
    partial class frmFirma
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlFirma = new System.Windows.Forms.PictureBox();
            this.btnLimpieza = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFirma)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(93, 23);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(218, 23);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // pnlFirma
            // 
            this.pnlFirma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFirma.Location = new System.Drawing.Point(93, 73);
            this.pnlFirma.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFirma.Name = "pnlFirma";
            this.pnlFirma.Size = new System.Drawing.Size(414, 273);
            this.pnlFirma.TabIndex = 4;
            this.pnlFirma.TabStop = false;
            this.pnlFirma.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlFirma_MouseDown);
            this.pnlFirma.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlFirma_MouseMove);
            this.pnlFirma.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlFirma_MouseUp);
            // 
            // btnLimpieza
            // 
            this.btnLimpieza.Location = new System.Drawing.Point(366, 23);
            this.btnLimpieza.Name = "btnLimpieza";
            this.btnLimpieza.Size = new System.Drawing.Size(75, 23);
            this.btnLimpieza.TabIndex = 5;
            this.btnLimpieza.Text = "Limpieza";
            this.btnLimpieza.UseVisualStyleBackColor = true;
            this.btnLimpieza.Click += new System.EventHandler(this.btnLimpieza_Click);
            // 
            // frmFirma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLimpieza);
            this.Controls.Add(this.pnlFirma);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Name = "frmFirma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmFirma";
            this.Load += new System.EventHandler(this.frmFirma_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFirma)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.PictureBox pnlFirma;
        private System.Windows.Forms.Button btnLimpieza;
    }
}