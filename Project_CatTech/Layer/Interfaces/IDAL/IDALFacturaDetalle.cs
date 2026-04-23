using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IDAL
{
    public interface IDALFacturaDetalle
    {
        int Insert(FacturaDetalle facturaDetalle);
        bool DeleteByFactura(int pIdFactura);
        List<FacturaDetalle> GetByFactura(int pIdFactura);

    }
}
