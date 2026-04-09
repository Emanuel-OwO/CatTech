using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Entities
{
    public class Stock
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string TipoMovimiento { get; set; } // ENTRADA o SALIDA
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public string NumeroFacturaCompra { get; set; }
    }
}
