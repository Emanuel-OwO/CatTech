using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.DTO
{
    public class ProductoVendidoReporteDTO
    {
        public int IdProducto { get; set; }
        public string CodigoInterno { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string TipoDispositivo { get; set; }
        public decimal Precio { get; set; }
        public int CantidadVendida { get; set; }
        public byte[] Fotografia { get; set; }
    }
}
