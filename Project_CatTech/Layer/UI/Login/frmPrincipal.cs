using Project_CatTech.Layer.UI;
using Project_CatTech.Layer.UI.FrmBCCR;
using Project_CatTech.Layer.UI.Login;
using Project_CatTech.Layer.UI.Mantenimientos;
using Project_CatTech.Layer.UI.Procesos;
using Project_CatTech.Layer.UI.Reportes;
using Project_CatTech.Layer.UI.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CatTech
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmMantenimientoCliente frmMantenimientoCliente = new frmMantenimientoCliente();
            frmMantenimientoCliente.MdiParent = this;
            frmMantenimientoCliente.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmMantenimientoMarca  frmMantenimientoMarca = new frmMantenimientoMarca();
            frmMantenimientoMarca.MdiParent = this;
            frmMantenimientoMarca.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmMantenimientoProducto frmMantenimientoProducto = new frmMantenimientoProducto();
            frmMantenimientoProducto.MdiParent = this;
            frmMantenimientoProducto.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmMantenimientoTipoDispositivo frmMantenimientoTipoDispositivo = new frmMantenimientoTipoDispositivo();
            frmMantenimientoTipoDispositivo.MdiParent = this;
            frmMantenimientoTipoDispositivo.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmMantenimientoProveedor frmMantenimientoProveedor = new frmMantenimientoProveedor();
            frmMantenimientoProveedor.MdiParent = this;
            frmMantenimientoProveedor.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            frmMantenimientoStock frmMantenimientoStock = new frmMantenimientoStock();
            frmMantenimientoStock.MdiParent = this;
            frmMantenimientoStock.Show();
        }

        private void ToolStripMenuItemUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuario frmUsuario = new frmUsuario();
            frmUsuario.MdiParent = this;
            frmUsuario.Show();
        }

        private void toolStripMenuItemFactura_Click(object sender, EventArgs e)
        {
            frmFactura frmFactura = new frmFactura();   
            frmFactura.MdiParent = this;
            frmFactura.Show();
        }

        private void toolStripMenuItemConsultarDolar_Click(object sender, EventArgs e)
        {
            frmDolar frmDolar = new frmDolar();
            frmDolar.MdiParent = this;
            frmDolar.Show();
        }

        private void toolStripMenuCambiarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                // Cerrar todas las ventanas hijas abiertas
                foreach (Form child in this.MdiChildren)
                {
                    child.Close();
                }

                // Deshabilitar todos los menús principales
                foreach (ToolStripItem opcionMenu in this.menuStrip1.Items)
                {
                    opcionMenu.Enabled = false;
                }

                // Dejar activos solo los que siempre deben verse
                List<string> menus = new List<string>();
                menus.Add("toolStripMenuItemAcercaEmpresa");
                menus.Add("toolStripMenuItemSalir");
                menus.Add("toolStripMenuCambiarUsuario");
                menus.Add("toolStripMenuManualUsuario");

                foreach (ToolStripItem opcionMenu in this.menuStrip1.Items)
                {
                    if (menus.Contains(opcionMenu.Name))
                    {
                        opcionMenu.Enabled = true;
                    }
                }

                // Limpiar sesión actual
                Project_CatTech.Properties.Settings.Default.Login = string.Empty;
                Project_CatTech.Properties.Settings.Default.Nombre = string.Empty;
                Project_CatTech.Properties.Settings.Default.RolId = string.Empty;
                Project_CatTech.Properties.Settings.Default.Save();

                // Volver a mostrar login
                using (frmLogin ofrmLogin = new frmLogin())
                {
                    ofrmLogin.ShowDialog();

                    if (ofrmLogin.DialogResult == DialogResult.OK)
                    {
                        Seguridad();
                        CargarStatusStrip();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al cambiar de usuario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItemAcercaEmpresa_Click(object sender, EventArgs e)
        {
            InformacionEmpresa frmINFO = new InformacionEmpresa();
            frmINFO.MdiParent = this;
            frmINFO.Show();
        }

        private void toolStripMenuManualUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = Path.GetFullPath(
                    //Aqui tengo que cambiar la ruta si no va a dar error
                    @"..\..\Instrucciones\Manual de Usuario_SweetTech_v2.pdf");

                System.Diagnostics.Process.Start(ruta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el manual: " + ex.Message);
            }
        }

        private void toolStripMenuItemSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItemAdministracion_Click(object sender, EventArgs e)
        {

        }

        private void Seguridad()
        {
            List<string> menus = new List<string>();

            // Desactiva todos los menús principales
            foreach (ToolStripItem opcionMenu in this.menuStrip1.Items)
            {
               
               opcionMenu.Enabled = false;
            }

            // Menús que siempre estarán habilitados para todos
            menus.Add("toolStripMenuItemAcercaEmpresa");
            menus.Add("toolStripMenuItemSalir");
            menus.Add("toolStripMenuCambiarUsuario");
            menus.Add("toolStripMenuManualUsuario");

            string rol = Project_CatTech.Properties.Settings.Default.RolId.Trim();

            // ADMINISTRADOR
            if (rol == "1")
            {
                menus.Add("toolStripMenuItemMantenimientos");
                menus.Add("toolStripMenuItemProcesos");
                menus.Add("toolStripMenuItemReportes");
                menus.Add("toolStripMenuItemAdministracion");
            }
            // VENDEDOR
            else if (rol == "2")
            {
                menus.Add("toolStripMenuItemMantenimientos");
                menus.Add("toolStripMenuItemProcesos");
                menus.Add("toolStripMenuItemReportes");
            }
            // REPORTES
            else if (rol == "3")
            {
                menus.Add("toolStripMenuItemReportes");
            }

            // Habilitar menús permitidos
            foreach (ToolStripItem opcionMenu in this.menuStrip1.Items)
            {
                if (menus.Contains(opcionMenu.Name))
                {
                    opcionMenu.Enabled = true;
                }
            }
        }

        private void CargarStatusStrip()
        {
            try
            {
                string usuario = Project_CatTech.Properties.Settings.Default.Login;
                string rolId = Project_CatTech.Properties.Settings.Default.RolId;
                string nombreRol = "";

                if (rolId == "1")
                    nombreRol = "Administrador";
                else if (rolId == "2")
                    nombreRol = "Vendedor";
                else if (rolId == "3")
                    nombreRol = "Reportes";
                else
                    nombreRol = "Sin rol";

                toolStripStatusEstado.Text = "Usuario: " + usuario;
                toolStripStatusRol.Text = "Rol: " + nombreRol;
                toolStripStatusFecha.Text = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la barra de estado: " + ex.Message);
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                // MessageBox.Show("Rol recibido: " + Project_CatTech.Properties.Settings.Default.RolId);
                CargarStatusStrip();
                Seguridad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario principal: " + ex.Message);
            }
        }

        private void toolStripMenuReporteFactura_Click(object sender, EventArgs e)
        {
            frmReporteFacturas frmReporte = new frmReporteFacturas();
            frmReporte.MdiParent = this;
            frmReporte.Show();
        }

        private void toolStripMenuReporteCliente_Click(object sender, EventArgs e)
        {
            frmReporteClientes frmReporte = new frmReporteClientes();
            frmReporte.MdiParent = this;
            frmReporte.Show();
        }

        private void toolStripMenuReporteProductos_Click(object sender, EventArgs e)
        {
            frmReporteProductosVendidos frmReporte = new frmReporteProductosVendidos();
            frmReporte.MdiParent = this;
            frmReporte.Show();
        }

        private void graficoToolStripMenuItoolStripMenuGrafico_Click(object sender, EventArgs e)
        {
            frmReporteGrafico frmReporte = new frmReporteGrafico();
            frmReporte.MdiParent = this;
            frmReporte.Show();
        }
    }
}
