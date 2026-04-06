using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces
{
    public interface IDALProveedor
    {
        void CREATE(Proveedor proveedor);

        void UPDATE(Proveedor proveedor);

        void DELETE(int id);

        Proveedor SelectById(int id);

        List<Proveedor> SelectAll();
    }
}
