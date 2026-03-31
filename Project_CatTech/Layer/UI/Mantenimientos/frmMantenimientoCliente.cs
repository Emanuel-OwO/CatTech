using Project_CatTech.Layer.BLL;
using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTNLeccion8B.Utilities;

namespace Project_CatTech.Layer.UI.Mantenimientos
{
    public partial class frmMantenimientoCliente : Form
    {
        private ErrorProvider oErrorProvider = new ErrorProvider();
        BLLCliente clienteBLL = new BLLCliente();
        public frmMantenimientoCliente()
        {
            InitializeComponent();
        }

        private void frmMantenimientoCliente_Load(object sender, EventArgs e)
        {
            CargarProvincias();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Boton Agregar
            Cliente cliente = new Cliente();

            cliente.TipoIdentificacion = txtIdentificacion.Text;
            cliente.Identificacion = txtTipoID.Text;
            cliente.Nombre = txtNombre.Text;
            cliente.PrimerApellido = txtPrimerApellido.Text;
            cliente.SegundoApellido = txtSegundoApellido.Text;
            cmbProvincia.SelectedItem = cliente.Provincia;
            cliente.Telefono = mskTelefono.Text.Trim().Replace("-", "");
            cliente.Correo = txtCorreo.Text;
            cliente.Direccion = txtDescripcion.Text;
            cliente.Fotografia = (byte[])pctFoto.Tag;
            cliente.Estado = rdoActivo.Checked;

            if (rdoMasculino.Checked)
            {
                cliente.Sexo = "M";
            }
            else if (rdoFemenino.Checked)
            {
                cliente.Sexo = "F";
            }
            else
            {
                MessageBox.Show("Debe seleccionar el sexo");
                return;
            }

            BLL.BLLCliente oClienteBLL = new BLL.BLLCliente();
            MessageBox.Show("Cliente agregado correctamente");

            CargarUsuarios();
            LimpiarCampos();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Boton Modificar o Editar
            try
            {
                Cliente cliente = new Cliente();

                // IMPORTANTE: Debes tener el ID del cliente seleccionado
                cliente.IdCliente = int.Parse(txtIdentificacion.Text); // o de donde lo tengas

                cliente.TipoIdentificacion = txtIdentificacion.Text;
                cliente.Identificacion = txtTipoID.Text;
                cliente.Nombre = txtNombre.Text;
                cliente.PrimerApellido = txtPrimerApellido.Text;
                cliente.SegundoApellido = txtSegundoApellido.Text;

                // Provincia (aquí estaba mal en tu agregar)
                cliente.Provincia = cmbProvincia.SelectedItem.ToString();

                cliente.Telefono = mskTelefono.Text.Trim().Replace("-", "");
                cliente.Correo = txtCorreo.Text;
                cliente.Direccion = txtDescripcion.Text;
                cliente.Fotografia = (byte[])pctFoto.Tag;
                cliente.Estado = rdoActivo.Checked;

                // Sexo
                if (rdoMasculino.Checked)
                {
                    cliente.Sexo = "M";
                }
                else if (rdoFemenino.Checked)
                {
                    cliente.Sexo = "F";
                }
                else
                {
                    MessageBox.Show("Debe seleccionar el sexo");
                    return;
                }

                // Llamar al BLL
                BLL.BLLCliente oClienteBLL = new BLL.BLLCliente();
                oClienteBLL.UPDATE(cliente); // ESTE ES EL MÉTODO UPDATE

                MessageBox.Show("Cliente actualizado correctamente");

                CargarUsuarios();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }


        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //Boton Eliminar
            if (dgvDatos.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdCliente"].Value);

                BLLCliente clienteBLL = new BLLCliente();
                clienteBLL.DELETE(id);

                MessageBox.Show("Cliente eliminado correctamente");

                CargarUsuarios();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente");
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //Boton Limpiar 
            LimpiarCampos();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //Boton Salir
            this.Close();
        }

        private void CargarUsuarios()
        {
            BLLCliente clienteBLL = new BLLCliente();
            dgvDatos.DataSource = clienteBLL.SELECTALL();

            dgvDatos.Columns["Provincia"].Visible = false;
            dgvDatos.Columns["Canton"].Visible = false;
            dgvDatos.Columns["Distrito"].Visible = false;
            dgvDatos.Columns["Pais"].Visible = false;
        }

        private void LimpiarCampos()
        {
            txtIdentificacion.Clear();
            txtTipoID.Clear();
            txtNombre.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtCorreo.Clear();
            txtDescripcion.Clear();

            mskTelefono.Clear();

            dtpFechaNacimiento.Value = DateTime.Now;


            pctFoto.Image = null;
            pctFoto.Tag = null;


            rdoActivo.Checked = false;
            rdoInactivo.Checked = false;


            rdoMasculino.Checked = false;
            rdoFemenino.Checked = false;

        }

        private void CargarProvincias()
        {
            try
            {
                cmbProvincia.DataSource = null;

                var lista = new DALProvincia().GetProvinciasFromInternet();

                if (lista != null && lista.Count > 0)
                {
                    cmbProvincia.DataSource = lista;
                    cmbProvincia.DisplayMember = "Descripcion";
                    cmbProvincia.ValueMember = "ID_provincia";

                    cmbProvincia.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando provincias: " + ex.Message);
            }
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            if (LeerDatos.Es_Email(this.txtCorreo))
            {
                this.oErrorProvider.SetError(this.txtCorreo, string.Empty);    
                this.txtCorreo.BackColor = Color.Honeydew;
            }
            else
            {
                this.oErrorProvider.SetError(this.txtCorreo, "Campo correo no es correcto");
                this.txtCorreo.BackColor = Color.MistyRose;
            }
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
    }
}
