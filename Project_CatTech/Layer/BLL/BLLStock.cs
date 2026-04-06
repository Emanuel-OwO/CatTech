using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using Project_CatTech.Layer.Interfaces.IBLL;
using Project_CatTech.Layer.Interfaces.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLStock : IBLLStock
    {
        private IDALStock dal = new DALStock();
        private IBLLProducto bllProducto = new BLLProducto();

        public void CREATE(Stock stock)
        {
            if (stock.IdProducto <= 0)
                throw new Exception("Debe seleccionar un producto");

            if (stock.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0");

            if (string.IsNullOrEmpty(stock.TipoMovimiento))
                throw new Exception("Debe seleccionar ENTRADA o SALIDA");

            // Validar stock suficiente si es SALIDA
            if (stock.TipoMovimiento == "SALIDA")
            {
                Producto p = bllProducto.SelectById(stock.IdProducto);
                if (p.CantidadStock < stock.Cantidad)
                    throw new Exception($"Stock insuficiente. Stock actual: {p.CantidadStock}");
            }

            dal.CREATE(stock);
        }

        public List<Stock> SelectAll()
        {
            return dal.SelectAll();
        }
    }
}
