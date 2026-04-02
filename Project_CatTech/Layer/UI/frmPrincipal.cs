using Project_CatTech.Layer.UI.Mantenimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
