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
    public partial class frmMantenimientoTipoDispositivo : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        BLLTipoDispositivo bLLTipoDispositivo = new BLLTipoDispositivo();

        public frmMantenimientoTipoDispositivo()
        {
            InitializeComponent();
        }

        private void frmMantenimientoTipoDispositivo_Load(object sender, EventArgs e)
        {
            CargarDatos();
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoDispositivo tipo = new TipoDispositivo();

               // tipo.IdTipoDispositivo = Convert.ToInt32(txtID_Dispositivo.Text);
                tipo.Descripcion = txtDescripcion.Text;
                tipo.Estado = rdoActivo.Checked;

                bLLTipoDispositivo.CREATE(tipo);

                MessageBox.Show("Tipo de dispositivo guardado correctamente");

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
                    MessageBox.Show("Seleccione un registro");
                    return;
                }

                TipoDispositivo tipo = new TipoDispositivo();

                tipo.IdTipoDispositivo = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdTipoDispositivo"].Value);
                tipo.Descripcion = txtDescripcion.Text;
                tipo.Estado = rdoActivo.Checked;

                bLLTipoDispositivo.UPDATE(tipo);

                MessageBox.Show("Tipo de dispositivo actualizado correctamente");

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
                int id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdTipoDispositivo"].Value);

                bLLTipoDispositivo.DELETE(id);

                MessageBox.Show("Tipo de dispositivo eliminado correctamente");

                CargarDatos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
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
            dgvDatos.DataSource = bLLTipoDispositivo.SELECTALL(); ;
        }

        private void LimpiarCampos()
        {
            txtID_Dispositivo.Clear();
            txtDescripcion.Clear();

            rdoActivo.Checked = false;
            rdoInactivo.Checked = false;
        }

        private void CargarDesdeGrid()
        {
            if (dgvDatos.CurrentRow == null) return;

            var row = dgvDatos.CurrentRow;

            txtID_Dispositivo.Text = row.Cells["IdTipoDispositivo"].Value?.ToString() ?? "";
            txtDescripcion.Text = row.Cells["Descripcion"].Value?.ToString() ?? "";

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

