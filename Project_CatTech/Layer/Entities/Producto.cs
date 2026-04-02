using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Entities
{
    public class Producto
    {
        public int IdProducto { set; get; }
        public string CodigoInterno { set; get; }
        public string CodigoBarras { set; get; }
        public int IdModelo { set; get; }
        public int IdProveedor { set; get; }
        public string Color { set; get; }
        public string Caracteristicas { set; get; }
        public string Extras { set; get; }
        public byte[] DocumentoEspecificaciones { set; get; }
        public byte[] Foto { set; get; }
        public double Precio { set; get; }
        public int CantidadStock { set; get; }
        public bool Estado { set; get; }

    }
}
