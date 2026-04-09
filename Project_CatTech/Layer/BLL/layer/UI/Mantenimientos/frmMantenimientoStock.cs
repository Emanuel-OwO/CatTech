using Project_CatTech.Layer.BLL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
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

namespace Project_CatTech.Layer.UI.Mantenimientos
{
    public partial class frmMantenimientoStock : Form
    {
        private IBLLStock bllStock = new BLLStock();
        private IBLLProducto bllProducto = new BLLProducto();

        public frmMantenimientoStock()
        {
            InitializeComponent();
        }

        private void frmMantenimientoStock_Load(object sender, EventArgs e)
        {
            CargarComboProductos();
            CargarDatos();
            rdoEntrada.Checked = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProducto.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un producto");
                    return;
                }

                if (!rdoEntrada.Checked && !rdoSalida.Checked)
                {
                    MessageBox.Show("Seleccione ENTRADA o SALIDA");
                    return;
                }

                Stock stock = new Stock();
                stock.IdProducto = Convert.ToInt32(cmbProducto.SelectedValue);
                stock.TipoMovimiento = rdoEntrada.Checked ? "ENTRADA" : "SALIDA";
                stock.Cantidad = Convert.ToInt32(txtCantidad.Text);
                stock.Observaciones = txtObservaciones.Text;
                stock.NumeroFacturaCompra = rdoEntrada.Checked ? txtNumFactura.Text : null;

                bllStock.CREATE(stock);

                MessageBox.Show("Movimiento registrado correctamente");
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

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

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

        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow == null) return;

            var row = dgvDatos.CurrentRow;

            cmbProducto.SelectedValue = row.Cells["IdProducto"].Value;
            txtCantidad.Text = row.Cells["Cantidad"].Value?.ToString();
            txtObservaciones.Text = row.Cells["Observaciones"].Value?.ToString();
            txtNumFactura.Text = row.Cells["NumeroFacturaCompra"].Value?.ToString();

            bool esEntrada = row.Cells["TipoMovimiento"].Value?.ToString() == "ENTRADA";
            rdoEntrada.Checked = esEntrada;
            rdoSalida.Checked = !esEntrada;
        }
        private void LimpiarCampos()
        {
            cmbProducto.SelectedIndex = -1;
            txtCantidad.Clear();
            txtObservaciones.Clear();
            txtNumFactura.Clear();
            rdoEntrada.Checked = true;
        }

        private void CargarComboProductos()
        {
            cmbProducto.DataSource = null;
            cmbProducto.DisplayMember = "CodigoInterno";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.DataSource = bllProducto.SelectAll();
            cmbProducto.SelectedIndex = -1;
        }

        private void CargarDatos()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = bllStock.SelectAll();
        }

      

        private void rdoEntrada_CheckedChanged_1(object sender, EventArgs e)
        {

            // Mostrar Nro Factura solo en ENTRADA
            txtNumFactura.Visible = rdoEntrada.Checked;
            lblNumeroFactura.Visible = rdoEntrada.Checked;
        }
    }
}
