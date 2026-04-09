using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces.IBLL;
using Project_CatTech.Layer.Interfaces.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLUsuario : IBLLUsuario
    {
        private IDALUsuario dal = new DALUsuario();
        public void CREATE(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new Exception("El nombre de usuario es obligatorio");
            if (string.IsNullOrWhiteSpace(usuario.Clave))
                throw new Exception("La clave es obligatoria");
            if (usuario.Clave.Length < 6)
                throw new Exception("La clave debe tener al menos 6 caracteres");

            // ← Encriptar AQUÍ en la BLL, no en el formulario
            usuario.Clave = Cryptography.EncrypthAES(usuario.Clave);
            dal.INSERT(usuario);
        }

        public void DELETE(int idUsuario)
        {
            dal.DELETE(idUsuario);
        }

        public Usuario LOGIN(string nombreUsuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("El nombre de usuario es obligatorio.");
            if (string.IsNullOrWhiteSpace(clave))
                throw new Exception("La contraseña es obligatoria.");

            // Encriptar antes de comparar con BD
            string claveEncriptada = Cryptography.EncrypthAES(clave);
            return dal.LOGIN(nombreUsuario.Trim(), claveEncriptada);
        }

        public List<Usuario> SELECT_ALL()
        {
            return dal.SELECT_ALL();
        }

        public DataTable SELECT_ALL_PERFILES()
        {
            return dal.SELECT_ALL_PERFILES();
        }

        public Usuario SELECT_BY_ID(int idUsuario)
        {
            return dal.SELECT_BY_ID(idUsuario);
        }

        public void UPDATE(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new Exception("El nombre de usuario es obligatorio");

            // Si viene clave nueva, encriptarla; si viene vacía, mantener la de BD
            if (!string.IsNullOrWhiteSpace(usuario.Clave))
                usuario.Clave = Cryptography.EncrypthAES(usuario.Clave);
            else
                usuario.Clave = dal.SELECT_BY_ID(usuario.IdUsuario).Clave; // mantiene la actual

            dal.UPDATE(usuario);
        }

    }
}
