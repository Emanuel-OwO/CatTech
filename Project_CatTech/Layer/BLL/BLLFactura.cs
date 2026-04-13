using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLFactura : IBLLFactura
    {
        public double CalcularTax(double precio, int cantidad)
        {
            throw new NotImplementedException();
        }

        public int GetNextNumeroFactura()
        {
            throw new NotImplementedException();
        }

        public int GetPrevNumeroFactura()
        {
            throw new NotImplementedException();
        }

        public Factura Save(Factura pfactura)
        {
            throw new NotImplementedException();
        }

        public Task<Factura> Save(DateTime pFechaInicial, DateTime pFechaFinal)
        {
            throw new NotImplementedException();
        }
    }
}
