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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CatTech.Layer.UI.Mantenimientos
{
    public partial class frmMantenimientoMarca : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        BLLMarca marcaBLL = new BLLMarca();
        public frmMantenimientoMarca()
        {
            InitializeComponent();
        }

        private void frmMantenimientoMarca_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Marca marca = new Marca();

                marca.CodigoMarca = txtCodigoMarca.Text; 
                marca.IdMarca = Convert.ToInt32(txtIDMarca.Text);
                marca.Descripcion = txtNombreMarca.Text;
                marca.Estado = rdoActivo.Checked;

                marcaBLL.CREATE(marca);

                MessageBox.Show("Marca guardada correctamente");

                CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void CargarDatos()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = marcaBLL.SelectAll();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdMarca"].Value);

                marcaBLL.DELETE(id);

                MessageBox.Show("Marca eliminada correctamente");

                CargarDatos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una marca");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una marca");
                    return;
                }

                Marca marca = new Marca();

                marca.IdMarca = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdMarca"].Value);
                marca.CodigoMarca = txtCodigoMarca.Text; 
                marca.Descripcion = txtNombreMarca.Text;
                marca.Estado = rdoActivo.Checked;

                marcaBLL.UPDATE(marca);

                MessageBox.Show("Marca actualizada correctamente");

                CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

        private void LimpiarCampos()
        {
            txtIDMarca.Clear();
            txtNombreMarca.Clear();
            txtCodigoMarca.Clear();

            rdoActivo.Checked = false;
            rdoInactivo.Checked = false;

        }

        private void CargarDesdeGrid()
        {
            if (dgvDatos.CurrentRow == null) return;

            var row = dgvDatos.CurrentRow;

            txtIDMarca.Text = row.Cells["IdMarca"].Value?.ToString() ?? "";
            txtCodigoMarca.Text = row.Cells["CodigoMarca"].Value?.ToString() ?? ""; 
            txtNombreMarca.Text = row.Cells["Descripcion"].Value?.ToString() ?? "";

            bool estado = Convert.ToBoolean(row.Cells["Estado"].Value ?? false);
            rdoActivo.Checked = estado;
            rdoInactivo.Checked = !estado;
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarDesdeGrid();
        }
    }
}
