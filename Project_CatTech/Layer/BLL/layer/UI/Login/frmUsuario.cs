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

namespace Project_CatTech.Layer.UI.Login
{
    public partial class frmUsuario : Form
    {
        private BLLUsuario _bllUsuario = new BLLUsuario();
        public frmUsuario()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            CargarPerfiles();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();

            usuario.Nombre = txtNombre.Text;
            usuario.PrimerApellido = txtPrimerApellido.Text;
            usuario.SegundoApellido = txtSegundoApellido.Text;
            usuario.Clave = txtClave.Text;
            usuario.IdPerfil = Convert.ToInt32(cmbPerfiles.SelectedValue);
            usuario .Estado = rdoActivo.Checked;

            _bllUsuario.CREATE(usuario);

            MessageBox.Show("Usuario agregado correctamente");

            CargarUsuarios();
            Limpiar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Usuario u = new Usuario();

            u.IdUsuario = Convert.ToInt32(txtIdUsuario.Text);
            u.Nombre = txtNombre.Text;
            u.PrimerApellido = txtPrimerApellido.Text;
            u.SegundoApellido = txtSegundoApellido.Text;
            u.Clave = txtClave.Text;
            u.IdPerfil = Convert.ToInt32(cmbPerfiles.SelectedValue);
            u.Estado = rdoActivo.Checked;

            _bllUsuario.UPDATE(u);

            MessageBox.Show("Usuario actualizado");

            CargarUsuarios();
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdUsuario.Text)) return;

            _bllUsuario.DELETE(Convert.ToInt32(txtIdUsuario.Text));

            MessageBox.Show("Usuario eliminado");

            CargarUsuarios();
            Limpiar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDatos_SizeChanged(object sender, EventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{ 
            //    var fila = dgvDatos.Rows[e.RowIndex];

            //    txtIdUsuario.Text = fila.Cells["IdUsuario"].Value.ToString();
            //    txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
            //    txtPrimerApellido.Text = fila.Cells["PrimerApellido"].Value.ToString();
            //    txtSegundoApellido.Text = fila.Cells["SegundoApellido"].Value.ToString();
            //    txtClave.Text = fila.Cells["Clave"].Value.ToString();

            //    cmbPerfiles.SelectedValue = fila.Cells["IdPerfil"].Value;

            //    bool estado = Convert.ToBoolean(fila.Cells["Estado"].Value);
            //    rdoActivo.Checked = estado;
            //    rdoInactivo.Checked = !estado;
            //}
        }
        private void CargarUsuarios()
        {
            dgvDatos.DataSource = _bllUsuario.SELECT_ALL();
        }
        private void CargarPerfiles()
        {
            cmbPerfiles.Items.Clear();

            cmbPerfiles.Items.Add("Administrador");
            cmbPerfiles.Items.Add("Vendedor");
            cmbPerfiles.Items.Add("Reportes");

            cmbPerfiles.SelectedIndex = 0;
        }

        private void Limpiar()
        {
            txtIdUsuario.Clear();
            txtNombre.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtClave.Clear();

            cmbPerfiles.SelectedIndex = 0;
            rdoActivo.Checked = true;
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
