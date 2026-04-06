using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces
{
    public interface IBLLTipoDispositivo
    {
        void CREATE(TipoDispositivo tipo);
        void UPDATE(TipoDispositivo tipo);
        void DELETE(int id);
        List<TipoDispositivo> SELECTALL();
        TipoDispositivo SELECTBYID(int id);
    }
}
