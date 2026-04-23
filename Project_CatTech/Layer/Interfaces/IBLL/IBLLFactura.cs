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
        Factura GetById(int pIdFactura);
        List<Factura> GetAll();
        int Save(Factura factura);
        bool Delete(int pIdFactura);
        bool Update(Factura factura);



        decimal CalcularSubTotal(List<FacturaDetalle> listaDetalle);
        decimal CalcularIVA(decimal subTotal);
        decimal CalcularTotalColones(decimal subTotal,decimal impuesto);
        decimal CalcularTotalDolares(decimal totalColones, decimal tipoCambio);




        void UpdateNumFactura(int idFactura, string numFactura);
        void UpdateXMLFactura(int idFactura, string xmlFactura);


    }
}
