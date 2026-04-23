using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces.IBLL;
using Project_CatTech.Layer.Interfaces.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLPago : IBLLPago
    {
        private readonly IDALPago _dalPago;

        public BLLPago()
        {
            _dalPago = new DALPago();
        }

        public int Save(Pago pago)
        {
            if (pago == null)
                throw new Exception("El pago no puede ser nulo.");

            if (pago.IdFactura <= 0)
                throw new Exception("El IdFactura es inválido.");

            //if (string.IsNullOrWhiteSpace(pago.IdTipoPago))
            //    throw new Exception("Debe seleccionar un tipo de pago.");
            if (pago.IdTipoPago <= 0)
            {
                throw new Exception("Seleccione un tipo de pago válido");
            }

            if (string.IsNullOrWhiteSpace(pago.NumeroReferencia))
                pago.NumeroReferencia = "N/A";

            return _dalPago.Insert(pago);
        }
    }
}
