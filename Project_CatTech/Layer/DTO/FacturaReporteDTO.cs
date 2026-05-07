using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.DTO
{
    public class FacturaReporteDTO
    {
        public int IdFactura { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Usuario { get; set; }
        public string TipoPago { get; set; }
        public decimal TotalColones { get; set; }
        public string Estado { get; set; }
    }
}
