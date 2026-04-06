using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces
{
    public interface IBLLProducto
    {
        void CREATE(Producto producto);
        void UPDATE(Producto producto);
        void DELETE(int id);
        List<Producto> SelectAll();
        Producto SelectById(int id);
    }
}
