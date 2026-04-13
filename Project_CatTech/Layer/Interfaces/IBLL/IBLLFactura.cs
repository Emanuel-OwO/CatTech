using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IBLL
{
    public interface IBLLFactura
    {
        Factura Save(Factura pfactura);
        int GetNextNumeroFactura();
        int GetPrevNumeroFactura();
        double CalcularTax(double precio,int cantidad);

        Task<Factura> Save(DateTime pFechaInicial,DateTime pFechaFinal);
    }
}
