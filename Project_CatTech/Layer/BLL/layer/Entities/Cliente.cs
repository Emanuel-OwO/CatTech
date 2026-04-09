using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Entities
{
    public class Cliente
    {
        public int IdCliente { set; get; }
        public string TipoIdentificacion { set; get; }
        public string Identificacion { set; get; }
        public string Nombre { set; get; }
        public string PrimerApellido { set; get; }
        public string SegundoApellido { set; get; }
        public string Sexo { set; get; }
        public string Telefono { set; get; }
        public string Correo { set; get; }
        public string Direccion { set; get; }
        public string Provincia { set; get; }
        public byte[] Fotografia { set; get; }
        public bool Estado { set; get; }

    }
}
