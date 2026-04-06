using log4net;
using Project_CatTech.Layer.BLL;
using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Project_CatTech.Layer.UI.Mantenimientos
{
    public partial class frmMantenimientoProducto : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        private ErrorProvider oErrorProvider = new ErrorProvider();
        private IBLLProducto pBLLProducto = new BLLProducto();
        private IBLLMarca bLLMarca = new BLLMarca();
        private IBLLTipoDispositivo bLLTipoDispositivo = new BLLTipoDispositivo();
        private IBLLProveedor bLLProveedor = new BLLProveedor();


        public frmMantenimientoProducto()
        {
            InitializeComponent();
        }

        private void frmMantenimientoProducto_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarCombos();
            CargarDesdeGrid();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();

                producto.CodigoInterno = txtCodigoInterno.Text;
                producto.CodigoBarras = txtCodigoBarras.Text;
                producto.Caracteristicas = txtCaracteristicas.Text;
                producto.Extras = txtExtras.Text;
                producto.Color = txtColor.Text;

                producto.Modelo = txtModelo.Text;
                producto.IdMarca = Convert.ToInt32(cmbMarca.SelectedValue);
                producto.IdTipoDispositivo = Convert.ToInt32(cmbTipoDispositivo.SelectedValue);
                producto.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);

                producto.Precio = Convert.ToDouble(txtPrecio.Text);
                producto.CantidadStock = Convert.ToInt32(txtStock.Text);
                producto.Estado = rdoActivo.Checked;

                producto.Foto = pctFoto.Tag != null ? (byte[])pctFoto.Tag : null;
                producto.DocumentoEspecificaciones = pctDocumento.Tag != null ? (byte[])pctDocumento.Tag : null;

                pBLLProducto.CREATE(producto);

                MessageBox.Show("Producto guardado correctamente");

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
                    MessageBox.Show("Seleccione un producto");
                    return;
                }

                Producto producto = new Producto();

                producto.IdProducto = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdProducto"].Value);
                producto.CodigoInterno = txtCodigoInterno.Text;
                producto.CodigoBarras = txtCodigoBarras.Text;
                producto.Caracteristicas = txtCaracteristicas.Text;
                producto.Extras = txtExtras.Text;
                producto.Color = txtColor.Text;

                producto.Modelo = txtModelo.Text;
                producto.IdMarca = Convert.ToInt32(cmbMarca.SelectedValue);
                producto.IdTipoDispositivo = Convert.ToInt32(cmbTipoDispositivo.SelectedValue);
                producto.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);

                producto.Precio = Convert.ToDouble(txtPrecio.Text);
                producto.CantidadStock = Convert.ToInt32(txtStock.Text);
                producto.Estado = rdoActivo.Checked;

                producto.Foto = pctFoto.Tag != null ? (byte[])pctFoto.Tag : null;
                producto.DocumentoEspecificaciones = pctDocumento.Tag != null ? (byte[])pctDocumento.Tag : null;

                pBLLProducto.UPDATE(producto);

                MessageBox.Show("Producto actualizado correctamente");

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
                int id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdProducto"].Value);

                pBLLProducto.DELETE(id);

                MessageBox.Show("Producto eliminado correctamente");

                CargarDatos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto");
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

        private void pctFoto_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostrar el Dialogo de archivos
                OpenFileDialog opt = new OpenFileDialog();
                // Parametros del dialogo
                opt.Title = "Seleccione la imagen";
                opt.SupportMultiDottedExtensions = true;
                opt.DefaultExt = "*.jpg";
                opt.Filter = "Archivos de Imagenes (*.jpg)|*.jpg| All files (*.*)|*.*";
                opt.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                opt.FileName = "";

                if (opt.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        this.pctFoto.ImageLocation = opt.FileName;
                        this.pctFoto.SizeMode = PictureBoxSizeMode.StretchImage;

                        byte[] cadenaBytes = File.ReadAllBytes(opt.FileName);

                        this.pctFoto.Tag = (byte[])cadenaBytes;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:  " + ex.Message);
                    }
                }
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat("Message        {0}\n", er.Message);
                msg.AppendFormat("Source         {0}\n", er.Source);
                msg.AppendFormat("InnerException {0}\n", er.InnerException);
                msg.AppendFormat("StackTrace     {0}\n", er.StackTrace);
                msg.AppendFormat("TargetSite     {0}\n", er.TargetSite);
                this.oErrorProvider.SetError(this.pctFoto, msg.ToString());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarDesdeGrid();
        }

        private void pctDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opt = new OpenFileDialog();
                opt.Title = "Seleccione el Documento ";
                opt.SupportMultiDottedExtensions = true;
                opt.DefaultExt = "*.docx";
                opt.Filter = "Archivos de Documentos (*.docx)|*.docx| All files (*.*)|*.*";
                opt.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                opt.FileName = "";
                if (opt.ShowDialog(this) == DialogResult.OK)
                {
                    // Leer todo el archivo de bytes
                    byte[] cadenaBytes = File.ReadAllBytes(opt.FileName);
                    this.pctDocumento.Tag = cadenaBytes;
                    this.pctDocumento.Image = Project_CatTech.Properties.Resources.MSWordAcepted;
                }
            }
            catch (SqlException sqlError)
            {
                // Mensaje de Error
                MessageBox.Show("Se ha producido el siguiente error: \n" + Utilitarios.GetCustomErrorByNumber(sqlError), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(Utilitarios.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                // Mensaje de Error
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarDatos()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = pBLLProducto.SelectAll();

            if (dgvDatos.Columns["Foto"] != null)
                dgvDatos.Columns["Foto"].Visible = false;

            if (dgvDatos.Columns["DocumentoEspecificaciones"] != null)
                dgvDatos.Columns["DocumentoEspecificaciones"].Visible = false;
        }

        private void LimpiarCampos()
        {
            txtCodigoInterno.Clear();
            txtCodigoBarras.Clear();
            txtCaracteristicas.Clear();
            txtPrecio.Clear();
            txtStock.Clear();

            cmbMarca.SelectedIndex = -1;

            rdoActivo.Checked = false;
            rdoInactivo.Checked = false;

            pctFoto.Image = null;
            pctFoto.Tag = null;

            pctDocumento.Image = null;
            pctDocumento.Tag = null;
        }

        private void CargarDesdeGrid()
        {
            if (dgvDatos.CurrentRow == null) return;

            var row = dgvDatos.CurrentRow;

            txtCodigoInterno.Text = row.Cells["CodigoInterno"].Value?.ToString();
            txtCodigoBarras.Text = row.Cells["CodigoBarras"].Value?.ToString();
            txtCaracteristicas.Text = row.Cells["Caracteristicas"].Value?.ToString();
            txtExtras.Text = row.Cells["Extras"].Value?.ToString();
            txtColor.Text = row.Cells["Color"].Value?.ToString();
            txtPrecio.Text = row.Cells["Precio"].Value?.ToString();
            txtStock.Text = row.Cells["CantidadStock"].Value?.ToString();

            txtModelo.Text = row.Cells["Modelo"].Value?.ToString();
            cmbMarca.SelectedValue = row.Cells["IdMarca"].Value;
            cmbTipoDispositivo.SelectedValue = row.Cells["IdTipoDispositivo"].Value;
            cmbProveedor.SelectedValue = row.Cells["IdProveedor"].Value;

            bool estado = Convert.ToBoolean(row.Cells["Estado"].Value ?? false);
            rdoActivo.Checked = estado;
            rdoInactivo.Checked = !estado;

            try
            {
                int idProducto = Convert.ToInt32(row.Cells["IdProducto"].Value);
                Producto p = pBLLProducto.SelectById(idProducto);

                // Mostrar Foto
                if (p.Foto != null && p.Foto.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(p.Foto))
                    {
                        pctFoto.Image = Image.FromStream(ms);
                        pctFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                        pctFoto.Tag = p.Foto;
                    }
                }
                else
                {
                    pctFoto.Image = null;
                    pctFoto.Tag = null;
                }

                // Mostrar Documento (icono Word si tiene archivo)
                if (p.DocumentoEspecificaciones != null && p.DocumentoEspecificaciones.Length > 0)
                {
                    pctDocumento.Image = Project_CatTech.Properties.Resources.MSWordAcepted;
                    pctDocumento.Tag = p.DocumentoEspecificaciones;
                }
                else
                {
                    pctDocumento.Image = null;
                    pctDocumento.Tag = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando foto/documento: " + ex.Message);
            }
        }

        private void CargarCombos()
        {
            cmbMarca.DataSource = null;
            cmbMarca.DisplayMember = "Descripcion";
            cmbMarca.ValueMember = "IdMarca";
            cmbMarca.DataSource = bLLMarca.SelectAll();

            cmbTipoDispositivo.DataSource = null;
            cmbTipoDispositivo.DisplayMember = "Descripcion";
            cmbTipoDispositivo.ValueMember = "IdTipoDispositivo";
            cmbTipoDispositivo.DataSource = bLLTipoDispositivo.SELECTALL();

            cmbProveedor.DataSource = null;
            cmbProveedor.DisplayMember = "Nombre";
            cmbProveedor.ValueMember = "IdProveedor";
            cmbProveedor.DataSource = bLLProveedor.SelectAll();

            cmbMarca.SelectedIndex = -1;
            cmbTipoDispositivo.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
        }


    }
}
