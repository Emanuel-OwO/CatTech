using appSweetTech.Extensiones;
using Project_CatTech.Layer.BLL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using Project_CatTech.Layer.UI.Filtros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CatTech.Layer.UI.Procesos
{
    public partial class frmFactura : Form
    {

        private readonly BLLFactura _bllFactura = new BLLFactura();
        private readonly BLLFacturaDetalle _bllDetalle = new BLLFacturaDetalle();
        private readonly BLLProducto _bllProducto = new BLLProducto();
        private readonly BLLDolar _bllDolar = new BLLDolar();

        private List<FacturaDetalle> listaDetalle = new List<FacturaDetalle>();


        private int _idClienteSeleccionado = 0;
        private int _idProductoSeleccionado = 0;
        private int _stockProductoSeleccionado = 0;
        private double _tipoCambio = 1;

        private List<FacturaDetalle> _detalleFactura = new List<FacturaDetalle>();
        private byte[] _firmaBytes;
        private Producto producto; 
        private Cliente clienteSelect;

        // El usuario actualmente logueado se debe pasar por constructor o propiedad
        public int IdUsuarioLogueado { get; set; } = 1;

        public frmFactura()
        {
            InitializeComponent();
        }

        private void frmFactura_Load(object sender, EventArgs e)
        {
            CargarTipoCambio();
            ConfigurarGrid();
            ConfigurarComboPago();
            IniciarNuevaFactura();
        }

        private void btnFiltroCliente_Click(object sender, EventArgs e)
        {
            frmFiltroCliente frm = new frmFiltroCliente();
            frm.ShowDialog();

            if (frm.cliente != null)
            {
                clienteSelect = frm.cliente; // 🔥 IMPORTANTE

                txtNombreCliente.Text = clienteSelect.Nombre;
                txtCedula.Text = clienteSelect.Identificacion;
                txtCelular.Text = clienteSelect.Telefono;
            }
        }

        private void btnFiltroProducto_Click(object sender, EventArgs e)
        {
            frmFiltroProducto frm = new frmFiltroProducto();
            frm.ShowDialog();

            if (frm.producto != null)
            {
                producto = frm.producto; // 🔥 IMPORTANTE

                txtProducto.Text = producto.Modelo;
                txtPrecio.Text = producto.Precio.ToString("N2");
            }
        }

        private void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNumeroFactura.Text = "Pendiente";
            IniciarNuevaFactura();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idClienteSeleccionado <= 0)
                    throw new Exception("Debe seleccionar un cliente.");

                if (listaDetalle.Count == 0)
                    throw new Exception("Debe agregar al menos un producto.");

                // Verificar stock real en BD antes de guardar
                foreach (FacturaDetalle item in listaDetalle)
                {
                    Producto p = _bllProducto.SelectById(item.IdProducto);
                    if (p == null)
                        throw new Exception("No se encontró el producto con Id: " + item.IdProducto);

                    if (item.Cantidad > p.CantidadStock)
                        throw new Exception("Stock insuficiente para el producto: " + p.Modelo);
                }

                // Armar la factura
                Factura factura = new Factura
                {
                    Fecha = DateTime.Now,
                    IdCliente = _idClienteSeleccionado,
                    IdUsuario = IdUsuarioLogueado,
                    SubTotal = Convert.ToDouble(txtSubTotal.Text),
                    Impuesto = Convert.ToDouble(txtImpreso.Text),
                    TotalColones = Convert.ToDouble(txtTotalColones.Text),
                    TotalDolares = Convert.ToDouble(txtTotalDolares.Text),
                    Estado = true
                };

                // Guardar cabecera → retorna el objeto con IdFactura y NumeroFactura ya asignados
                int idFactura = _bllFactura.Save(factura);
                Factura facturaGuardada = _bllFactura.GetById(idFactura);

                // Guardar detalles y rebajar stock
                foreach (FacturaDetalle item in listaDetalle)
                {
                    item.IdFactura = facturaGuardada.IdFactura;
                    _bllDetalle.Save(item);

                    // Rebajar stock (usar el método que ya existe en BLLProducto / DALProducto)
                    Producto p = _bllProducto.SelectById(item.IdProducto);
                    p.CantidadStock -= item.Cantidad;
                    _bllProducto.UPDATE(p);
                }

                txtNumeroFactura.Text = facturaGuardada.NumeroFactura;

                MessageBox.Show("Factura guardada correctamente.\nNúmero: " + facturaGuardada.NumeroFactura,
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                IniciarNuevaFactura();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al facturar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnFirmar_Click(object sender, EventArgs e)
        {
            frmFirma frm = new frmFirma();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _firmaBytes = frm.FirmaBytes;

                using (MemoryStream ms = new MemoryStream(_firmaBytes))
                {
                    picFirma.Image = Image.FromStream(ms);
                }
            }
        }

        private void btnCalcularFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idClienteSeleccionado <= 0)
                    throw new Exception("Debe seleccionar un cliente.");

                if (_idProductoSeleccionado <= 0)
                    throw new Exception("Debe seleccionar un producto.");

                if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                    throw new Exception("Debe indicar la cantidad.");

                int cantidad = Convert.ToInt32(txtCantidad.Text);
                double precio = Convert.ToDouble(txtPrecio.Text);

                if (cantidad <= 0)
                    throw new Exception("La cantidad debe ser mayor a cero.");

                if (cantidad > _stockProductoSeleccionado)
                    throw new Exception("No hay stock suficiente para este producto.");

                double subtotalLinea = (double)_bllDetalle.CalcularSubTotal(cantidad, (decimal)precio);

                // Si el producto ya está en la lista, sumar cantidad
                FacturaDetalle detalleExistente = listaDetalle
                    .FirstOrDefault(x => x.IdProducto == _idProductoSeleccionado);

                if (detalleExistente != null)
                {
                    int nuevaCantidad = detalleExistente.Cantidad + cantidad;
                    if (nuevaCantidad > _stockProductoSeleccionado)
                        throw new Exception("La cantidad total supera el stock disponible.");

                    detalleExistente.Cantidad = nuevaCantidad;
                    detalleExistente.Subtotal = detalleExistente.Cantidad * detalleExistente.Precio;

                    // Actualizar fila en el grid
                    foreach (DataGridViewRow row in dgvDatos.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["colIdProducto"].Value) == _idProductoSeleccionado)
                        {
                            row.Cells["colCantidad"].Value = detalleExistente.Cantidad;
                            row.Cells["colSubtotal"].Value = detalleExistente.Subtotal.ToString("N2");
                            break;
                        }
                    }
                }
                else
                {
                    FacturaDetalle detalle = new FacturaDetalle
                    {
                        IdProducto = _idProductoSeleccionado,
                        Cantidad = cantidad,
                        Precio = (decimal)precio,
                        Subtotal = (decimal)subtotalLinea
                    };
                    listaDetalle.Add(detalle);

                    dgvDatos.Rows.Add(
                        _idProductoSeleccionado,
                        txtProducto.Text,  // código                      
                        precio.ToString("N2"),
                        cantidad,
                        subtotalLinea.ToString("N2")
                    );
                }

                CalcularTotales();
                LimpiarProducto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTotales()
        {
            try
            {
                decimal subtotal = _bllFactura.CalcularSubTotal(listaDetalle);
                decimal impuesto = _bllFactura.CalcularIVA(subtotal);
                decimal totalColones = _bllFactura.CalcularTotalColones(subtotal, impuesto);

                decimal tipoCambio = 1m;
                decimal totalDolares = 0m;

                //if (!decimal.TryParse(txtTi.Text, out tipoCambio))
                //    tipoCambio = 1m;

                if (tipoCambio > 0)
                    totalDolares = _bllFactura.CalcularTotalDolares(totalColones, tipoCambio);

                txtSubTotal.Text = subtotal.ToString("N2");
                txtImpreso.Text = impuesto.ToString("N2");
                txtTotalColones.Text = totalColones.ToString("N2");
                txtTotalDolares.Text = totalDolares.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al calcular", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Nueva factura ────────────────────────────────────────────────────────
        private void IniciarNuevaFactura()
        {
            listaDetalle.Clear();
            dgvDatos.Rows.Clear();

            _idClienteSeleccionado = 0;
            _idProductoSeleccionado = 0;
            _stockProductoSeleccionado = 0;
            // Cabecera
            txtNumeroFactura.Text = "Pendiente";
            dtpFecha.Value = DateTime.Now;
            txtUsuario.Text = "";
            txtEstado.Text = "Activa";

            // Cliente
            txtNombreCliente.Text = "";
            txtCedula.Text = "";
            txtCelular.Text = "";

            // Producto a agregar
            txtProducto.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";

            // Totales
            txtSubTotal.Text = "0.00";
            txtImpreso.Text = "0.00";
            txtTotal.Text = "0.00";
            txtTotalColones.Text = "0.00";
            txtTotalDolares.Text = "0.00";

            // Pago
            txtNroTarjeta.Text = "";
            txtTipoTarjerta.Text = "";
            txtNroReferencia.Text = "";

            if (cmbTipoPago.Items.Count > 0) cmbTipoPago.SelectedIndex = 0;
            if (cmbBanco.Items.Count > 0) cmbBanco.SelectedIndex = 0;

            // Firma
            picFirma.Image = null;
            ActualizarCamposPago();
        }


        // ─── Configuraciones iniciales ────────────────────────────────────────────
        private void CargarTipoCambio()
        {
            try
            {
                _tipoCambio = _bllDolar.GetVentaDolar();
                // textBox15 = campo Tipo de Cambio (según el Designer)
                txtTipoTarjerta.Text = _tipoCambio.ToString("N2");
            }
            catch
            {
                txtDolar.Text = "1.00";
                MessageBox.Show("No se pudo obtener el tipo de cambio del BCCR. Se usará 1.00 por defecto.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarCamposPago()
        {
            string tipo = cmbTipoPago.SelectedItem?.ToString() ?? "";
            bool esTarjeta = tipo == "Tarjeta";
            bool esEfectivo = tipo == "Efectivo";

            txtNroTarjeta.Enabled = esTarjeta;
            txtTipoTarjerta.Enabled = esTarjeta;
            cmbBanco.Enabled = esTarjeta || tipo == "Transferencia";
            txtNroReferencia.Enabled = !esEfectivo;
        }

        private void ConfigurarGrid()
        {
            dgvDatos.Columns.Clear();
            dgvDatos.AutoGenerateColumns = false;
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.ReadOnly = true;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.MultiSelect = false;

            dgvDatos.Columns.Add("colIdProducto", "IdProducto");
            dgvDatos.Columns["colIdProducto"].Visible = false;

            dgvDatos.Columns.Add("colCodigo", "Código");
            dgvDatos.Columns.Add("colProducto", "Producto");
            dgvDatos.Columns.Add("colPrecio", "Precio");
            dgvDatos.Columns.Add("colCantidad", "Cantidad");
            dgvDatos.Columns.Add("colSubtotal", "Subtotal");
        }

        private void LimpiarProducto()
        {
            _idProductoSeleccionado = 0;
            _stockProductoSeleccionado = 0;
            txtProducto.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
        }

        private void ConfigurarComboPago()
        {
            cmbTipoPago.Items.Clear();
            cmbTipoPago.Items.Add("Tarjeta");
            cmbTipoPago.Items.Add("Transferencia");
            cmbTipoPago.Items.Add("SINPE");
            cmbTipoPago.SelectedIndex = 0;
        }

        private void LimpiarFormulario()
        {
            txtNombreCliente.Clear();
            txtCedula.Clear();
            txtCelular.Clear();
            txtProducto.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();

            txtTotalDolares.Clear();
            txtTotalColones.Clear();
            txtSubTotal.Clear();
            txtTotal.Clear();
            txtDolar.Clear();

            dgvDatos.DataSource = null;

            _detalleFactura.Clear();
            clienteSelect = null;
            producto = null;

            picFirma.Image = null;
        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCamposPago();
        }

        private void txtNumeroFactura_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
