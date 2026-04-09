using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces
{
    public interface IBLLCliente
    {
        void INSERT(Cliente cliente);
        void UPDATE(Cliente cliente);
        void DELETE(int id);
        List<Cliente> SELECTALL();
    }
}
