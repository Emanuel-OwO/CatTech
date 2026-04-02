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
    public class BLLProducto : IBLLProducto
    {
        private IDALProducto dal = new DALProducto();
        public void CREATE(Producto producto)
        {
            if (string.IsNullOrEmpty(producto.CodigoInterno))
                throw new Exception("El código interno es obligatorio");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a 0");

            dal.CREATE(producto);
        }

        public void DELETE(int id)
        {

            if (id <= 0)
                throw new Exception("Id inválido");

            dal.DELETE(id);
        }

        public List<Producto> SelectAll()
        {
            return dal.SelectAll();
        }

        public Producto SelectById(int id)
        {
            return dal.SelectById(id);
        }

        public void UPDATE(Producto producto)
        {
            if (producto.IdProducto <= 0)
                throw new Exception("Id inválido");

            dal.UPDATE(producto);
        }
    }
}
