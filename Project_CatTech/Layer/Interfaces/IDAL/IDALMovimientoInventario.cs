using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IDAL
{
    public interface IDALMovimientoInventario
    {
        List<MovimientoInventario> SELECT_ALL();
        MovimientoInventario SELECT_BY_ID(int idMovimiento);
        void Insert(MovimientoInventario movimiento);
    }
}
