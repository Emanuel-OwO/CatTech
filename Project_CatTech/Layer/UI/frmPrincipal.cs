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
    }
}
