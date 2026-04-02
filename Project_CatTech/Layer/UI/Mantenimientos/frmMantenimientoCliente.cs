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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_CatTech.Utilities;

namespace Project_CatTech.Layer.UI.Mantenimientos
{


    public partial class frmMantenimientoCliente : Form
    {
        private static readonly ILog _myLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        private ErrorProvider oErrorProvider = new ErrorProvider();
        BLLCliente clienteBLL = new BLLCliente();
        ErrorProvider erp = new ErrorProvider();

        public frmMantenimientoCliente()
        {
            InitializeComponent();
        }

        private void frmMantenimientoCliente_Load(object sender, EventArgs e)
        {
            CargarProvincias();
            CargarUsuarios();
            txtIdentificacion.TextChanged += txtTipoID_TextChanged;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Boton Agregar
            Cliente cliente = new Cliente();

            cliente.Identificacion = txtIdentificacion.Text;
            cliente.TipoIdentificacion = txtTipoID.Text;
            cliente.Nombre = txtNombre.Text;
            cliente.PrimerApellido = txtPrimerApellido.Text;
            cliente.SegundoApellido = txtSegundoApellido.Text;
            cliente.Provincia = cmbProvincia.Text;
            cliente.Telefono = mskTelefono.Text.Trim().Replace("-", "");
            cliente.Correo = txtCorreo.Text;
            cliente.Direccion = txtDescripcion.Text;
            cliente.Fotografia = (byte[])pctFoto.Tag;
            cliente.Estado = rdoActivo.Checked;

            if (rdoMasculino.Checked)
                cliente.Sexo = "M";
            else if (rdoFemenino.Checked)
                cliente.Sexo = "F";
            else
            {
                MessageBox.Show("Debe seleccionar el sexo");
                return;
            }

            BLLCliente oClienteBLL = new BLLCliente();
            oClienteBLL.INSERT(cliente); // 🔥 CLAVE

            MessageBox.Show("Cliente agregado correctamente");

            CargarUsuarios(); // 🔥 recarga el grid

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Boton Modificar o Editar
            try
            {
                Cliente cliente = new Cliente();

                // IMPORTANTE: Debes tener el ID del cliente seleccionado
                cliente.IdCliente = Convert.ToInt32(dgvDatos.CurrentRow.Cells["IdCliente"].Value); // o de donde lo tengas

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
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = clienteBLL.SELECTALL();
            dgvDatos.Columns["Fotografia"].Visible = false;
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

        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            IBLLPadron bLLPadron = new BLLPadron();
            try
            {
                erp.Clear();

                if (string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    erp.SetError(txtIdentificacion, "Id Requerido");
                    txtIdentificacion.Focus();
                    return;
                }

                if (txtIdentificacion.Text.Trim().Length != 9)
                {
                    erp.SetError(txtIdentificacion, "Largo de la Cédula 9 digitos");
                    txtIdentificacion.Focus();
                    return;
                }


                // ToDo: Cree La validación que solo permita números en la cédula 

                PadronElectoral oPadronDTO = bLLPadron.GetById(txtIdentificacion.Text.Trim());


                string[] array = oPadronDTO.nombre.Split(' ');

                // 1 nombres y dos apellidos
                if (array.Length == 3)
                {
                    txtNombre.Text = array[0];
                    txtPrimerApellido.Text = array[1];
                    txtSegundoApellido.Text = array[2];
                }

                // 2 nombres y dos apellidos
                if (array.Length == 4)
                {
                    txtNombre.Text = array[0] + " " + array[1];
                    txtPrimerApellido.Text = array[2];
                    txtSegundoApellido.Text = array[3];
                }

                // Ejemplo con varios nombres. 203960070 - ANTONIO MARIA DE LA TRINIDAD RODRIGUEZ CHAVES 
                // 2 nombres y dos apellidos
                // Nota: No se valida apellidos compuestos por ejemplo Maria de la O
                if (array.Length > 4)
                {
                    txtNombre.Text = array[0] + " " + array[1];
                    txtPrimerApellido.Text = array[array.Length - 2];
                    txtSegundoApellido.Text = array[array.Length - 1];
                }


            }
            catch (Exception er)
            {
                _myLogControlEventos.ErrorFormat("Error en {0}: {1}", MethodBase.GetCurrentMethod().Name, er.ToString());

                MessageBox.Show(
                    $"Se ha producido el siguiente error:\n{er.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }
        private void DetectarTipoID()
        {

            string cedula = txtIdentificacion.Text.Trim().Replace("-", "");

            if (cedula.Length == 9 && cedula.All(char.IsDigit))
                txtTipoID.Text = "Nacional";
            else if ((cedula.Length == 11 || cedula.Length == 12) && cedula.All(char.IsDigit))
                txtTipoID.Text = "Extranjero";
            else if (cedula.Any(char.IsLetter))
                txtTipoID.Text = "Pasaporte";
            else
                txtTipoID.Text = "";
        }

        private void txtTipoID_TextChanged(object sender, EventArgs e)
        {
            DetectarTipoID();
        }

        private void CargarDatos()
        {
            if (dgvDatos.CurrentRow == null) return;

            var row = dgvDatos.CurrentRow;

            txtIdentificacion.Text = row.Cells["Identificacion"].Value?.ToString() ?? "";
            txtTipoID.Text = row.Cells["TipoIdentificacion"].Value?.ToString() ?? "";
            txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
            txtPrimerApellido.Text = row.Cells["PrimerApellido"].Value?.ToString() ?? "";
            txtSegundoApellido.Text = row.Cells["SegundoApellido"].Value?.ToString() ?? "";

            // 🔥 Provincia (más seguro)
            if (row.Cells["Provincia"].Value != null)
            {
                cmbProvincia.SelectedItem = row.Cells["Provincia"].Value.ToString();
            }

            mskTelefono.Text = row.Cells["Telefono"].Value?.ToString() ?? "";
            txtCorreo.Text = row.Cells["Correo"].Value?.ToString() ?? "";
            txtDescripcion.Text = row.Cells["Direccion"].Value?.ToString() ?? "";

            // 🖼️ Imagen segura
            if (row.Cells["Fotografia"].Value != null)
            {
                byte[] fotoBytes = row.Cells["Fotografia"].Value as byte[];

                if (fotoBytes != null && fotoBytes.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(fotoBytes))
                    {
                        pctFoto.Image = Image.FromStream(ms);
                        pctFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                        pctFoto.Tag = fotoBytes;
                    }
                }
            }
            else
            {
                pctFoto.Image = null;
                pctFoto.Tag = null;
            }

            // ✅ Estado
            bool estado = Convert.ToBoolean(row.Cells["Estado"].Value ?? false);
            rdoActivo.Checked = estado;
            rdoInactivo.Checked = !estado;

            // ✅ Sexo
            string sexo = row.Cells["Sexo"].Value?.ToString() ?? "";
            rdoMasculino.Checked = sexo == "M";
            rdoFemenino.Checked = sexo == "F";
        }


        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}
