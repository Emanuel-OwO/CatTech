using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces
{
    public interface IDALMarca
    {
        void CREATE(Marca marca);
        void UPDATE(Marca marca);
        void DELETE(int id);
        List<Marca> SelectAll();
        Marca SelectById(int id);
    }
}
