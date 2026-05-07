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
            ConfigurarGridCliente();
            CargarClientes("%%");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = "%" + txtBuscarCliente.Text.Replace(' ', '%') + "%";
            CargarClientes(filtro);
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
                // Usar DataBoundItem igual que el clic simple
                cliente = dgvDatos.CurrentRow.DataBoundItem as Cliente;

                if (cliente != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void ConfigurarGridCliente()
        {
            dgvDatos.AutoGenerateColumns = false;
            dgvDatos.Columns.Clear();
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdCliente", DataPropertyName = "IdCliente", HeaderText = "Id", Width = 35 });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoIdentificacion", DataPropertyName = "TipoIdentificacion", HeaderText = "Tipo", Width = 45 });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Identificacion", DataPropertyName = "Identificacion", HeaderText = "Cédula", Width = 90 });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre", Width = 90 });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrimerApellido", DataPropertyName = "PrimerApellido", HeaderText = "1er Apellido", Width = 90 });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "SegundoApellido", DataPropertyName = "SegundoApellido", HeaderText = "2do Apellido", Width = 90 });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Telefono", DataPropertyName = "Telefono", HeaderText = "Teléfono", Width = 90 });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Correo", DataPropertyName = "Correo", HeaderText = "Correo", Width = 160 });
        }

        private void CargarClientes(string filtro)
        {
            try
            {
                dgvDatos.DataSource = new BLLCliente().Get_By_Filter(filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando clientes: " + ex.Message);
            }
        }
    }
}
