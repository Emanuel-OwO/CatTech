using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces
{
    public interface IDALCliente
    {
            void CREATE(Cliente cliente);
            void UPDATE(Cliente cliente);
            void DELETE(int id);
            List<Cliente> SelectAll();
            Cliente SelectById(int id);
    }
}
