using appSweetTech.Extensiones;
using log4net;
using Project_CatTech.Layer.BLL;
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
    public partial class frmFiltroProducto : Form
    {
        private static readonly ILog _myLogControlEventos =
          log4net.LogManager.GetLogger("MyControlEventos");

        public Producto producto { get; set; }
        public frmFiltroProducto()
        {
            InitializeComponent();
        }

        private void frmFiltroProducto_Load(object sender, EventArgs e)
        {
            try
            {
                IBLLProducto bLLProducto = new BLLProducto();
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = bLLProducto.Get_By_Filter("%%");

                dgvDatos.Columns.Clear();
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdProducto", DataPropertyName = "IdProducto", HeaderText = "Id", Width = 40 });
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoInterno", DataPropertyName = "CodigoInterno", HeaderText = "Código", Width = 70 });
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Modelo", DataPropertyName = "Modelo", HeaderText = "Modelo", Width = 130 });
              
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Precio", DataPropertyName = "Precio", HeaderText = "Precio", Width = 90 });
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "CantidadStock", DataPropertyName = "CantidadStock", HeaderText = "Stock", Width = 55 });
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando productos: " + ex.Message);
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            IBLLProducto bLLProducto = new BLLProducto();

            try
            {
                string filtro = txtBuscarProducto.Text;
                filtro = "%" + filtro.Replace(' ', '%') + "%";

                dgvDatos.AutoGenerateColumns = true;

                dgvDatos.DataSource = bLLProducto.Get_By_Filter(filtro);

               
                if (dgvDatos.Columns["Foto"] != null)
                    dgvDatos.Columns["Foto"].Visible = false;
            }
            catch (Exception er)
            {
                string msg = "";
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod()));
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //this.dgvDatos.SelectionMode =   DataGridViewSelectionMode.FullRowSelect;
                if (dgvDatos.RowCount > 0 && dgvDatos.SelectedRows.Count > 0)
                {
                    if (dgvDatos.CurrentCell.Selected)
                    {
                        producto = dgvDatos.SelectedRows[0].DataBoundItem as Producto;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
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

        private void dgvDatos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDatos.CurrentRow != null)
                {
                    producto = dgvDatos.CurrentRow.DataBoundItem as Producto;

                    this.DialogResult = DialogResult.OK; 
                    this.Close(); 
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
