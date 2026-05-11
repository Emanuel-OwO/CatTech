using Project_CatTech.Layer.BLL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces.IBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CatTech.Layer.UI.Reportes
{
    public partial class frmReporteClientes : Form
    {
        private IBLLReporte _bllReporte = new BLLReporte();
        public frmReporteClientes()
        {
            InitializeComponent();
        }

        private void frmReporteClientes_Load(object sender, EventArgs e)
        {

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                List<Cliente> lista = _bllReporte.GetClientesReporte();

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No hay clientes para generar el reporte.");
                    return;
                }

                ClienteReportePdfService pdf = new ClienteReportePdfService();
                pdf.GenerarPdf(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte de clientes: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
