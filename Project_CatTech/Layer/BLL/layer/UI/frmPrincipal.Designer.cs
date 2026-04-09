namespace Project_CatTech
{
    partial class frmPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemMantenimientos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemProcesos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAdministracion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAcercaEmpresa = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFactura = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemConsultarDolar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 907);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1743, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMantenimientos,
            this.toolStripMenuItemProcesos,
            this.toolStripMenuItemReportes,
            this.toolStripMenuItemAdministracion,
            this.toolStripMenuItemAcercaEmpresa,
            this.toolStripMenuItemSalir});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1743, 72);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemMantenimientos
            // 
            this.toolStripMenuItemMantenimientos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7});
            this.toolStripMenuItemMantenimientos.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemMantenimientos.Image")));
            this.toolStripMenuItemMantenimientos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemMantenimientos.Name = "toolStripMenuItemMantenimientos";
            this.toolStripMenuItemMantenimientos.Size = new System.Drawing.Size(170, 68);
            this.toolStripMenuItemMantenimientos.Text = "Mantenimientos";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "Cliente";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem3.Text = "Marca";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem4.Text = "Producto";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem5.Text = "Tipo Dispositivo";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem6.Text = "Proveedor";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem7.Text = "Stock";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItemProcesos
            // 
            this.toolStripMenuItemProcesos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFactura,
            this.toolStripMenuItemConsultarDolar});
            this.toolStripMenuItemProcesos.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemProcesos.Image")));
            this.toolStripMenuItemProcesos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemProcesos.Name = "toolStripMenuItemProcesos";
            this.toolStripMenuItemProcesos.Size = new System.Drawing.Size(130, 68);
            this.toolStripMenuItemProcesos.Text = "Procesos";
            // 
            // toolStripMenuItemReportes
            // 
            this.toolStripMenuItemReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem14,
            this.toolStripMenuItem16});
            this.toolStripMenuItemReportes.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemReportes.Image")));
            this.toolStripMenuItemReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemReportes.Name = "toolStripMenuItemReportes";
            this.toolStripMenuItemReportes.Size = new System.Drawing.Size(129, 68);
            this.toolStripMenuItemReportes.Text = "Reportes";
            // 
            // toolStripMenuItemAdministracion
            // 
            this.toolStripMenuItemAdministracion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemUsuarios});
            this.toolStripMenuItemAdministracion.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemAdministracion.Image")));
            this.toolStripMenuItemAdministracion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemAdministracion.Name = "toolStripMenuItemAdministracion";
            this.toolStripMenuItemAdministracion.Size = new System.Drawing.Size(164, 68);
            this.toolStripMenuItemAdministracion.Text = "Administracion";
            // 
            // toolStripMenuItemAcercaEmpresa
            // 
            this.toolStripMenuItemAcercaEmpresa.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemAcercaEmpresa.Image")));
            this.toolStripMenuItemAcercaEmpresa.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemAcercaEmpresa.Name = "toolStripMenuItemAcercaEmpresa";
            this.toolStripMenuItemAcercaEmpresa.Size = new System.Drawing.Size(195, 68);
            this.toolStripMenuItemAcercaEmpresa.Text = "Acerca de la Embresa";
            // 
            // toolStripMenuItemSalir
            // 
            this.toolStripMenuItemSalir.Image = global::Project_CatTech.Properties.Resources.encendido;
            this.toolStripMenuItemSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemSalir.Name = "toolStripMenuItemSalir";
            this.toolStripMenuItemSalir.Size = new System.Drawing.Size(108, 68);
            this.toolStripMenuItemSalir.Text = "Salir ";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStripMenuItemFactura
            // 
            this.toolStripMenuItemFactura.Name = "toolStripMenuItemFactura";
            this.toolStripMenuItemFactura.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemFactura.Text = "Facturar";
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(186, 22);
            this.toolStripMenuItem14.Text = "Reportes";
            // 
            // ToolStripMenuItemUsuarios
            // 
            this.ToolStripMenuItemUsuarios.Name = "ToolStripMenuItemUsuarios";
            this.ToolStripMenuItemUsuarios.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemUsuarios.Text = "Usuarios";
            this.ToolStripMenuItemUsuarios.Click += new System.EventHandler(this.ToolStripMenuItemUsuarios_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(186, 22);
            this.toolStripMenuItem16.Text = "toolStripMenuItem16";
            // 
            // toolStripMenuItemConsultarDolar
            // 
            this.toolStripMenuItemConsultarDolar.Name = "toolStripMenuItemConsultarDolar";
            this.toolStripMenuItemConsultarDolar.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemConsultarDolar.Text = "Consultar Dolar";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1743, 929);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMantenimientos;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemProcesos;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReportes;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdministracion;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAcercaEmpresa;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSalir;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFactura;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemUsuarios;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConsultarDolar;
    }
}

