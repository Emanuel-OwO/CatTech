using log4net;
using Project_CatTech.Layer.BLL;
using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CatTech.Layer.UI.Mantenimientos
{
    public partial class frmMantenimientoProveedor : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        BLLProveedor proveedorBLL = new BLLProveedor();

        public frmMantenimientoProveedor()
        {
            InitializeComponent();
        }

        private void frmMantenimientoProveedor_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedor proveedor = new Proveedor();

               // proveedor.IdProveedor = Convert.ToInt32(txtID.Text);
                proveedor.Nombre = txtNombre.Text;
                proveedor.Telefono = mskTelefono.Text.Trim().Replace("-", "");
                proveedor.Correo = txtCorreo.Text;
                proveedor.Direccion = txtDireccion.Text;
                proveedor.Estado = rdoActivo.Checked;

                proveedorBLL.CREATE(proveedor);

                MessageBox.Show("Proveedor guardado correctamente");

                CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un proveedor");
                    return;
                }

                Proveedor proveedor = new Proveedor();

                proveedor.IdProveedor = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdProveedor"].Value);
                proveedor.Nombre = txtNombre.Text;
                proveedor.Telefono = mskTelefono.Text.Trim().Replace("-", "");
                proveedor.Correo = txtCorreo.Text;
                proveedor.Direccion = txtDireccion.Text;
                proveedor.Estado = rdoActivo.Checked;

                proveedorBLL.UPDATE(proveedor);

                MessageBox.Show("Proveedor actualizado correctamente");

                CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdProveedor"].Value);

                proveedorBLL.DELETE(id);

                MessageBox.Show("Proveedor eliminado correctamente");

                CargarDatos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un proveedor");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarDesdeGrid();
        }

        private void CargarDatos()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = proveedorBLL.SelectAll();
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            mskTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();

            rdoActivo.Checked = false;
            rdoInactivo.Checked = false;
        }

        private void CargarDesdeGrid()
        {
            if (dgvDatos.CurrentRow == null) return;

            var row = dgvDatos.CurrentRow;

            txtID.Text = row.Cells["IdProveedor"].Value?.ToString() ?? "";
            txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
            mskTelefono.Text = row.Cells["Telefono"].Value?.ToString() ?? "";
            txtCorreo.Text = row.Cells["Correo"].Value?.ToString() ?? "";
            txtDireccion.Text = row.Cells["Direccion"].Value?.ToString() ?? "";

            bool estado = Convert.ToBoolean(row.Cells["Estado"].Value ?? false);
            rdoActivo.Checked = estado;
            rdoInactivo.Checked = !estado;
        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            CargarDesdeGrid();
        }
    }
}

