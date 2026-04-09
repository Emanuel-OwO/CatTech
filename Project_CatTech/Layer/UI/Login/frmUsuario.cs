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
            CargarPerfiles();
            CargarUsuarios();
            txtIdUsuario.ReadOnly = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtClave.Text))
                {
                    MessageBox.Show("NombreUsuario y Clave son obligatorios");
                    return;
                }

                Usuario usuario = new Usuario();
                usuario.NombreUsuario = txtNombre.Text.Trim();      // ← txtNombre hace de NombreUsuario
                usuario.Nombre = txtPrimerApellido.Text.Trim(); // ← reaprovechás los campos
                usuario.PrimerApellido = txtSegundoApellido.Text.Trim();
                usuario.SegundoApellido = "";
                usuario.Clave = txtClave.Text;
                usuario.IdPerfil = Convert.ToInt32(cmbPerfiles.SelectedValue);
                usuario.Estado = rdoActivo.Checked;

                _bllUsuario.CREATE(usuario);
                MessageBox.Show("Usuario agregado correctamente");
                CargarUsuarios();
                Limpiar();
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
                if (string.IsNullOrEmpty(txtIdUsuario.Text))
                {
                    MessageBox.Show("Seleccione un usuario del listado");
                    return;
                }

                Usuario u = new Usuario();
                u.IdUsuario = Convert.ToInt32(txtIdUsuario.Text);
                u.NombreUsuario = txtNombreUsuario.Text.Trim();
                u.Nombre = txtNombre.Text.Trim();
                u.PrimerApellido = txtPrimerApellido.Text.Trim();
                u.SegundoApellido = txtSegundoApellido.Text.Trim();
                u.Clave = txtClave.Text;   // ← encriptada
                u.IdPerfil = Convert.ToInt32(cmbPerfiles.SelectedValue);
                u.Estado = rdoActivo.Checked;

                _bllUsuario.UPDATE(u);
                MessageBox.Show("Usuario actualizado");
                CargarUsuarios();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdUsuario.Text))
            {
                MessageBox.Show("Seleccione un usuario del listado");
                return;
            }

            var confirm = MessageBox.Show(
                "¿Está seguro que desea eliminar este usuario?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _bllUsuario.DELETE(Convert.ToInt32(txtIdUsuario.Text));
                MessageBox.Show("Usuario eliminado correctamente");
                CargarUsuarios();
                Limpiar();
            }
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
           
        }
        private void CargarUsuarios()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = _bllUsuario.SELECT_ALL();

         
        }
        private void CargarPerfiles()
        {
            cmbPerfiles.DataSource = null;
            cmbPerfiles.DisplayMember = "Descripcion";
            cmbPerfiles.ValueMember = "IdPerfil";
            cmbPerfiles.DataSource = _bllUsuario.SELECT_ALL_PERFILES();
            cmbPerfiles.SelectedIndex = -1;
        }

        private void Limpiar()
        {
            txtIdUsuario.Clear();
            txtNombreUsuario.Clear();
            txtNombre.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtClave.Clear();
            cmbPerfiles.SelectedIndex = -1;
            rdoActivo.Checked = true;
            rdoInactivo.Checked = false;
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dgvDatos.Rows[e.RowIndex];

            txtIdUsuario.Text = fila.Cells["IdUsuario"].Value?.ToString();
            txtNombreUsuario.Text = fila.Cells["NombreUsuario"].Value?.ToString();
            txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
            txtPrimerApellido.Text = fila.Cells["PrimerApellido"].Value?.ToString();
            txtSegundoApellido.Text = fila.Cells["SegundoApellido"].Value?.ToString();

            // La clave viene encriptada de BD — mostrar vacío por seguridad
            // Si el usuario no cambia la clave, se re-encripta la misma
            txtClave.Text = "";

            cmbPerfiles.SelectedValue = fila.Cells["IdPerfil"].Value;

            bool estado = Convert.ToBoolean(fila.Cells["Estado"].Value ?? false);
            rdoActivo.Checked = estado;
            rdoInactivo.Checked = !estado;
        }
    }
}
