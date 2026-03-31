using Project_CatTech.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Entities
{
    public class Provincia
    {
        public int ID_provincia { set; get; }
        public string Descripcion { set; get; }
        public override string ToString() => ID_provincia + " " + Descripcion;
    }
}
