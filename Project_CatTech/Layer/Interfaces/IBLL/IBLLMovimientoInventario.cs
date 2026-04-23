using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IBLL
{
    public interface IBLLMovimientoInventario
    {
        void Save(MovimientoInventario movimiento);
        void INSERT(MovimientoInventario movimiento);
        MovimientoInventario SELECT_BY_ID(int idMovimiento);
        List<MovimientoInventario> SELECT_ALL();

    }
}
