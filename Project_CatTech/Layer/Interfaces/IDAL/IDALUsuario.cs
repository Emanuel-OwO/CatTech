using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IDAL
{
    public interface IDALUsuario
    {
        void INSERT(Usuario usuario);
        void UPDATE(Usuario usuario);
        void DELETE(int idUsuario);
        Usuario SELECT_BY_ID(int idUsuario);
        List<Usuario> SELECT_ALL();
        Usuario LOGIN(string nombreUsuario, string clave);
    }
}
