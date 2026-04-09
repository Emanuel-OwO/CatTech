using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLProveedor : IBLLProveedor
    {
        DALProveedor dal = new DALProveedor();

        public void CREATE(Proveedor proveedor)
        {
            if (proveedor == null)
                throw new Exception("El proveedor es nulo");

            if (string.IsNullOrEmpty(proveedor.Nombre))
                throw new Exception("El nombre es obligatorio");

            dal.CREATE(proveedor);
        }

        public void DELETE(int id)
        {
            if (id <= 0)
                throw new Exception("ID inválido");

            dal.DELETE(id);
        }

        public List<Proveedor> SelectAll()
        {
            return dal.SelectAll();
        }

        public Proveedor SelectById(int id)
        {
            if (id <= 0)
                throw new Exception("ID inválido");

            return dal.SelectById(id);
        }

        public void UPDATE(Proveedor proveedor)
        {
            if (proveedor == null)
                throw new Exception("El proveedor es nulo");

            if (proveedor.IdProveedor <= 0)
                throw new Exception("ID inválido");

            dal.UPDATE(proveedor);
        }
    }
}
