using Project_CatTech.Layer.BLL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.UI.Filtros;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Project_CatTech.Utilitarios;


namespace Project_CatTech.Layer.UI.Procesos
{
    public partial class frmFactura : Form
    {

        private string correoClienteSeleccionado = "";

        private readonly BLLFactura _bllFactura = new BLLFactura();
        private readonly BLLFacturaDetalle _bllDetalle = new BLLFacturaDetalle();
        private readonly BLLProducto _bllProducto = new BLLProducto();
        private readonly BLLDolar _bllDolar = new BLLDolar();

        private List<FacturaDetalle> listaDetalle = new List<FacturaDetalle>();


        private int _idClienteSeleccionado = 0;
        private int _idProductoSeleccionado = 0;
        private int _stockProductoSeleccionado = 0;
        private double _tipoCambio = 1;
        private string _codigoProductoSeleccionado = "";

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
            CargarBancos();
            IniciarNuevaFactura();
        }

        private void btnFiltroCliente_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmFiltroCliente frm = new frmFiltroCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        Cliente c = frm.cliente;
                        _idClienteSeleccionado = c.IdCliente;
                        correoClienteSeleccionado = c.Correo ?? ""; // ← AGREGÁ ESTA LÍNEA

                        txtNombreCliente.Text = c.Nombre + " "
                                              + c.PrimerApellido + " "
                                              + c.SegundoApellido;
                        txtCedula.Text = c.Identificacion;
                        txtCelular.Text = c.Telefono;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar cliente: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltroProducto_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmFiltroProducto frm = new frmFiltroProducto())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        Producto p = frm.producto;
                        _idProductoSeleccionado = p.IdProducto;
                        _stockProductoSeleccionado = p.CantidadStock;
                        _codigoProductoSeleccionado = p.CodigoInterno;

                        txtProducto.Text = p.Modelo;
                        txtPrecio.Text = p.Precio.ToString("N2");
                        txtCantidad.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar producto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Verificar stock real en BD
                foreach (FacturaDetalle item in listaDetalle)
                {
                    Producto p = _bllProducto.SelectById(item.IdProducto);
                    if (p == null)
                        throw new Exception("No se encontró el producto con Id: " + item.IdProducto);
                    if (item.Cantidad > p.CantidadStock)
                        throw new Exception($"Stock insuficiente para '{p.Modelo}'. Disponible: {p.CantidadStock}.");
                }

                // Calcular totales
                decimal subTotal = _bllFactura.CalcularSubTotal(listaDetalle);
                decimal impuesto = _bllFactura.CalcularIVA(subTotal);
                decimal totalColones = _bllFactura.CalcularTotalColones(subTotal, impuesto);

                decimal tipoCambio = 530m;
                if (decimal.TryParse(txtDolar.Text.Replace(",", ""), out decimal tc) && tc > 0)
                    tipoCambio = tc;

                decimal totalDolares = _bllFactura.CalcularTotalDolares(totalColones, tipoCambio);

                // Armar cabecera
                Factura factura = new Factura
                {
                    NumeroFactura = "",
                    Fecha = dtpFecha.Value,
                    IdCliente = _idClienteSeleccionado,
                    IdUsuario = IdUsuarioLogueado,
                    SubTotal = (double)subTotal,
                    Impuesto = (double)impuesto,
                    TotalColones = (double)totalColones,
                    TotalDolares = (double)totalDolares,
                    FirmaCliente = _firmaBytes,
                    Estado = txtEstado.Text.Trim().ToLower() == "activa"
                };

                // Guardar cabecera — el SP genera el número y lo retorna
                int idFactura = _bllFactura.Save(factura);
                string numeroFinal = factura.NumeroFactura;
                factura.IdFactura = idFactura;   // ← necesario para el XML

                // Guardar detalles y rebajar stock
                foreach (FacturaDetalle item in listaDetalle)
                {
                    item.IdFactura = idFactura;
                    _bllDetalle.Save(item);

                    Producto prod = _bllProducto.SelectById(item.IdProducto);
                    prod.CantidadStock -= item.Cantidad;
                    _bllProducto.UPDATE(prod);
                }

                // Actualizar pantalla
                txtNumeroFactura.Text = numeroFinal;
                txtEstado.Text = "Guardada";

                // Generar y guardar XML
                string xmlGenerado = Project_CatTech.Utilitarios.Util.FacturaXmlHelper.GenerarXml(
                    factura,
                    listaDetalle,
                    txtNombreCliente.Text.Trim(),
                    cmbTipoPago.SelectedItem.ToString()
                );

                string carpetaXml = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FacturasXML");
                if (!System.IO.Directory.Exists(carpetaXml))
                    System.IO.Directory.CreateDirectory(carpetaXml);

                string rutaXml = System.IO.Path.Combine(carpetaXml, numeroFinal + ".xml");
                System.IO.File.WriteAllText(rutaXml, xmlGenerado, System.Text.Encoding.UTF8);
                _bllFactura.UpdateXMLFactura(idFactura, xmlGenerado);

                // Generar PDF con QR
                string contenidoQR =
                    "CatTech\n\n" +
                    "Factura: " + numeroFinal + "\n" +
                    "Cliente: " + txtNombreCliente.Text + "\n" +
                    "Cédula: " + txtCedula.Text + "\n" +
                    "Fecha: " + dtpFecha.Value.ToString("dd/MM/yyyy") + "\n" +
                    "Total CRC: " + txtTotalColones.Text + "\n" +
                    "Pago: " + cmbTipoPago.Text;

                System.Drawing.Image qrImage = QuickResponse.QuickResponseGenerador(contenidoQR, 10);

                string rutaPdf = Project_CatTech.Layer.Utilitarios.FacturaPdfService.GenerarPdfFactura(
                    factura,
                    listaDetalle,
                    txtNombreCliente.Text.Trim(),
                    txtCedula.Text.Trim(),
                    txtUsuario.Text.Trim(),
                    cmbTipoPago.SelectedItem.ToString(),
                    _firmaBytes,
                    qrImage
                );

                // Enviar correo
                if (!string.IsNullOrWhiteSpace(correoClienteSeleccionado))
                {
                    try
                    {
                        Project_CatTech.Utilitarios.EnviarCorreo correo = new Project_CatTech.Utilitarios.EnviarCorreo();

                        string asunto = "Factura " + numeroFinal + " - CatTech";
                        string body = "<h2>CatTech</h2>" +
                                        "<p>Estimado cliente,</p>" +
                                        "<p>Adjuntamos su factura en formato PDF y XML.</p>" +
                                        "<p><b>Número de factura:</b> " + numeroFinal + "</p>" +
                                        "<p>Gracias por su compra.</p>";

                        correo.enviarCorreoGmail(body, correoClienteSeleccionado, asunto,
                            new List<string> { rutaPdf, rutaXml });
                    }
                    catch (Exception exCorreo)
                    {
                        MessageBox.Show(
                            "La factura se guardó, pero no se pudo enviar el correo:\n" + exCorreo.Message,
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("La factura se generó, pero el cliente no tiene correo registrado.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                MessageBox.Show("Factura guardada correctamente. Número: " + numeroFinal,
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
    _codigoProductoSeleccionado, // ← código interno real
    txtProducto.Text,
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

                if (decimal.TryParse(txtDolar.Text.Replace(",", ""), out decimal tc) && tc > 0)
                    tipoCambio = tc;

                if (tipoCambio > 0)
                    totalDolares = _bllFactura.CalcularTotalDolares(totalColones, tipoCambio);

                txtSubTotal.Text = subtotal.ToString("N2");
                txtImpreso.Text = impuesto.ToString("N2");
                txtTotal.Text = totalColones.ToString("N2");
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
                txtDolar.Text = _tipoCambio.ToString("N2");
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

        private void CargarBancos()
        {
            cmbBanco.Items.Clear();
            cmbBanco.Items.Add("BCR");
            cmbBanco.Items.Add("BNCR");
            cmbBanco.Items.Add("BAC");
            cmbBanco.Items.Add("Scotiabank");
            cmbBanco.Items.Add("Davivienda");
            cmbBanco.Items.Add("Otro");
            cmbBanco.SelectedIndex = -1;
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
