using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IDAL
{
    public interface IDALFactura
    {
        int Insert(Factura factura);
        void Update(Factura factura);
        void Delete(int idFactura);
        Factura GetById(int idFactura);
        List<Factura> GetAll();
    }
}
