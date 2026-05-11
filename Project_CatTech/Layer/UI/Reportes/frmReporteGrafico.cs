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
using System.Windows.Forms.DataVisualization.Charting;

namespace Project_CatTech.Layer.UI.Reportes
{
    public partial class frmReporteGrafico : Form
    {
        private IBLLReporte _bllReporte = new BLLReporte();
        public frmReporteGrafico()
        {
            InitializeComponent();
        }

        private void frmReporteGrafico_Load(object sender, EventArgs e)
        {
            cmbTipoGrafico.Items.Clear();
            cmbTipoGrafico.Items.Add("Column");
            cmbTipoGrafico.Items.Add("Line");
            cmbTipoGrafico.Items.Add("Bar");
            cmbTipoGrafico.Items.Add("Pie");
            cmbTipoGrafico.Items.Add("Doughnut");
            cmbTipoGrafico.Items.Add("Area");
            cmbTipoGrafico.Items.Add("Spline");
            cmbTipoGrafico.SelectedIndex = 0;

            dtpFechaInicial.Value = DateTime.Today.AddDays(-7);
            dtpFechaFinal.Value = DateTime.Today;

            ConfigurarGrafico();
        }

        private void ConfigurarGrafico()
        {
            chartVentas.Series.Clear();
            chartVentas.ChartAreas.Clear();
            chartVentas.Titles.Clear();

            ChartArea area = new ChartArea("Area1");
            area.AxisX.Title = "Fecha";
            area.AxisY.Title = "Ventas";
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisX.Interval = 1;

            chartVentas.ChartAreas.Add(area);
        }

        private void btnGrafico_Click(object sender, EventArgs e)
        {
            try
            {
// aqui hay que crear esa clase 
                 var lista =   _bllReporte.GetVentasPorFecha(dtpFechaInicial.Value, dtpFechaFinal.Value);

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No hay ventas para ese rango.");
                    return;
                }

                chartVentas.Series.Clear();
                chartVentas.Titles.Clear();

                Series serie = new Series("Ventas");
                serie.XValueMember = "FechaVenta";
                serie.YValueMembers = "TotalVenta";
                serie.IsValueShownAsLabel = true;
                serie.LabelFormat = "N2";

                if (cmbTipoGrafico.SelectedIndex == 0) serie.ChartType = SeriesChartType.Column;
                if (cmbTipoGrafico.SelectedIndex == 1) serie.ChartType = SeriesChartType.Line;
                if (cmbTipoGrafico.SelectedIndex == 2) serie.ChartType = SeriesChartType.Bar;
                if (cmbTipoGrafico.SelectedIndex == 3) serie.ChartType = SeriesChartType.Pie;
                if (cmbTipoGrafico.SelectedIndex == 4) serie.ChartType = SeriesChartType.Doughnut;
                if (cmbTipoGrafico.SelectedIndex == 5) serie.ChartType = SeriesChartType.Area;
                if (cmbTipoGrafico.SelectedIndex == 6) serie.ChartType = SeriesChartType.Spline;

                chartVentas.Series.Add(serie);
                chartVentas.DataSource = lista;
                chartVentas.Titles.Add("Ventas por rango de fecha");
                chartVentas.Titles[0].Font = new Font("Segoe UI", 13F, FontStyle.Bold);
                chartVentas.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar gráfico: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "Guardar gráfico";
                dialog.FileName = "GraficoVentas.png";
                dialog.Filter = "Imagen PNG|*.png";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    chartVentas.SaveImage(dialog.FileName, ChartImageFormat.Png);
                    MessageBox.Show("Gráfico exportado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar gráfico: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
