using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Project_CatTech.Layer.Entities
{
    public class Factura
    {
        public int IdFactura { set; get; }
        public string NumeroFactura { set; get; }
        public DateTime Fecha { set; get; }
        public int IdCliente { set; get; }
        public int IdUsuario { set; get; }
        public double SubTotal { set; get; }
        public double Impuesto { set; get; }
        public double TotalColones { set; get; }
        public double TotalDolares { set; get; }
        public XElement XMLFactura { set; get; }
        public byte[] FirmaCliente { set; get; }
        public bool Estado { set; get; }

    }
}
