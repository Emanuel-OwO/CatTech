using Project_CatTech.Layer.Entities.BCR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CatTech.Layer.UI.FrmBCCR
{
    public partial class frmDolar : Form
    {
        public frmDolar()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                double tipoCambioVenta = 1;

                ServicesBCCR services = new ServicesBCCR();
                List<Dolar> cambioDolar = services.GetDolar(DateTime.Today, DateTime.Today, "v").ToList();

                if (cambioDolar != null && cambioDolar.Count > 0)
                {
                    tipoCambioVenta = cambioDolar[0].Monto;

                    // Mostrar en el TextBox
                    txtDolar.Text = tipoCambioVenta.ToString("N2");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void frmDolar_Load(object sender, EventArgs e)
        {

        }
    }
}
