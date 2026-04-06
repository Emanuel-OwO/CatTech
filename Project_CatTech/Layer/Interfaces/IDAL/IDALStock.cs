using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IDAL
{
    public interface IDALStock
    {
        void CREATE(Stock stock);
        List<Stock> SelectAll();
    }
}
