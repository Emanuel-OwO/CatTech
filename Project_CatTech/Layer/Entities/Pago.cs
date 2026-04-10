using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Entities
{
    public class Pago
    {
        public int IdPago { set; get; }
        public int IdFactura { set; get; }
        public int IdTipoPago { set; get; }
        public string NumeroTarjeta { set; get; }
        public string TipoTarjeta { set; get; }
        public int IdBanco { set; get; }
        public string NumeroReferencia { set; get; }

    }
}
