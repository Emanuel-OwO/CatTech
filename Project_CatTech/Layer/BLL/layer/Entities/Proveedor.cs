using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Entities
{
    public class Proveedor
    {
        public int IdProveedor { set; get; }
        public string Nombre { set; get; }
        public string Telefono { set; get; }
        public string Correo { set; get; }
        public string Direccion { set; get; }
        public bool Estado { set; get; }

    }
}
