using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IBLL
{
    public interface IBLLUsuario
    {
        void CREATE(Usuario usuario);
        void UPDATE(Usuario usuario);
        void DELETE(int idUsuario);
        Usuario SELECT_BY_ID(int idUsuario);
        List<Usuario> SELECT_ALL();
        Usuario LOGIN(string nombreUsuario, string clave);
        DataTable SELECT_ALL_PERFILES();
    }
}
