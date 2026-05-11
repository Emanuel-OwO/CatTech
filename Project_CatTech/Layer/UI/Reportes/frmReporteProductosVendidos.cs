using appSweetTech.Utilitarios;
using Project_CatTech.Layer.BLL;
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
    public partial class frmReporteProductosVendidos : Form
    {

        // aqui efalta crear esa clase  
        private IBLLReporte _bllReporte = new BLLReporte();
        private BLLMarca _bllMarca = new BLLMarca();
        private BLLTipoDispositivo _bllTipo = new BLLTipoDispositivo();

        public frmReporteProductosVendidos()
        {
            InitializeComponent();
        }

        private void frmReporteProductosVendidos_Load(object sender, EventArgs e)
        {
            cboMarca.DataSource = _bllMarca.SelectAll();
            cboMarca.DisplayMember = "Descripcion";
            cboMarca.ValueMember = "IdMarca";
            cboMarca.SelectedIndex = -1;

            cboTipoDispositivo.DataSource = _bllTipo.SELECTALL();
            cboTipoDispositivo.DisplayMember = "Descripcion";
            cboTipoDispositivo.ValueMember = "IdTipoDispositivo";
            cboTipoDispositivo.SelectedIndex = -1;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                int? idMarca = cboMarca.SelectedIndex >= 0 ? Convert.ToInt32(cboMarca.SelectedValue) : (int?)null;
                int? idTipo = cboTipoDispositivo.SelectedIndex >= 0 ? Convert.ToInt32(cboTipoDispositivo.SelectedValue) : (int?)null;
                string modelo = txtModelo.Text.Trim();

                var lista = _bllReporte.GetProductosVendidos(idMarca, modelo, idTipo);

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No hay productos vendidos con esos filtros.");
                    return;
                }

                ProductoVendidoReportePdfService pdf = new ProductoVendidoReportePdfService();
                pdf.GenerarPdf(lista, cboMarca.Text, modelo, cboTipoDispositivo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
