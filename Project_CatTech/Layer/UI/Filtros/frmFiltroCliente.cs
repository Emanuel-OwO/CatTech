using appSweetTech.Extensiones;
using log4net;
using Project_CatTech.Layer.BLL;
using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CatTech.Layer.UI.Filtros
{
    public partial class frmFiltroCliente : Form
    {
        private static readonly ILog _myLogControlEventos =
          log4net.LogManager.GetLogger("MyControlEventos");
        public Cliente cliente { get; set; }
        public frmFiltroCliente()
        {
            InitializeComponent();
        }

        private void frmFiltroCliente_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            IBLLCliente bLLCliente = new BLLCliente();
            string filtro = string.Empty;
            try
            {
                filtro = this.txtBuscarCliente.Text;
                filtro = filtro.Replace(' ', '%');
                filtro = "%" + filtro + "%";
                this.dgvDatos.AutoGenerateColumns = true;
                this.dgvDatos.DataSource = bLLCliente.Get_By_Filter(filtro);
                dgvDatos.Columns["Fotografia"].Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDatos.RowCount > 0 && dgvDatos.SelectedRows.Count > 0)
                {
                    if (dgvDatos.CurrentCell.Selected)
                    {
                        cliente = dgvDatos.SelectedRows[0].DataBoundItem as Cliente;
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception er)
            {

                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDatos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                cliente = new Cliente()
                {
                    IdCliente = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdCliente"].Value),
                    Nombre = dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString(),
                    Identificacion = dgvDatos.CurrentRow.Cells["Identificacion"].Value.ToString(),
                    Telefono = dgvDatos.CurrentRow.Cells["Telefono"].Value.ToString(),
                    Correo = dgvDatos.CurrentRow.Cells["Correo"].Value.ToString()
                };

                this.Close(); 
            }
        }
    }
}
