using Project_CatTech.Layer.BLL;
using Project_CatTech.Layer.Interfaces.IBLL;
using Project_CatTech.Utilitarios;
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
    public partial class frmReporteFacturas : Form
    {
        private IBLLReporte _bllReporte = new BLLReporte();
        public frmReporteFacturas()
        {
            InitializeComponent();
        }

        private void frmReporteFacturas_Load(object sender, EventArgs e)
        {

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = _bllReporte.GetFacturasPorFecha(
                    dtpFechaInicial.Value,
                    dtpFechaFinal.Value
                );

                if (lista.Count == 0)
                {
                    MessageBox.Show("No hay datos para ese rango.");
                    return;
                }

                decimal total = lista.Sum(x => x.TotalColones);

                FacturaReportePdfService pdf = new FacturaReportePdfService();
                pdf.GenerarPdf(lista, total, dtpFechaInicial.Value, dtpFechaFinal.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
