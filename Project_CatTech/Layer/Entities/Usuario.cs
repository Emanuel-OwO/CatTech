using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Entities
{
    public class Usuario
    {
        public int IdUsuario { set; get; }
        public string NombreUsuario { set; get; }
        public string Clave { set; get; }
        public string Nombre { set; get; }
        public string PrimerApellido { set; get; }
        public string SegundoApellido { set; get; }
        public int IdPerfil { set; get; }
        public string Perfil { get; set; }
        public bool Estado { set; get; }

    }
}
