using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IBLL
{
    public interface IBLLFacturaDetalle
    {
        int Save(FacturaDetalle facturaDetalle);
        bool DeleteByFactura(int  pIdFactura);
        List<FacturaDetalle> GetByFactura(int pIdFactura);
        decimal CalcularSubTotal(int cantidad, decimal precio);
    }
}
