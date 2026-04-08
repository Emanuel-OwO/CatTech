using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces.IBLL;
using Project_CatTech.Layer.Interfaces.IDAL;
using System;
using System.Collections.Generic;
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
            if (string.IsNullOrEmpty(usuario.NombreUsuario))
                throw new Exception("El nombre de usuario es obligatorio");

            if (string.IsNullOrEmpty(usuario.Clave))
                throw new Exception("La clave es obligatoria");

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

            return dal.LOGIN(nombreUsuario.Trim(), clave.Trim());
        }

        public List<Usuario> SELECT_ALL()
        {
            return dal.SELECT_ALL();
        }

        public Usuario SELECT_BY_ID(int idUsuario)
        {
            return dal.SELECT_BY_ID(idUsuario);
        }

        public void UPDATE(Usuario usuario)
        {
            dal.UPDATE(usuario);
        }
    }
}
