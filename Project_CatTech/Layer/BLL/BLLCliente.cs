using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLCliente : IBLLCliente
    {
        public void INSERT(Cliente cliente)
        {
            if (!string.IsNullOrEmpty(cliente.Identificacion))
            {
                if (Existe(cliente.Identificacion))
                {
                    DALCliente.UPDATE(cliente);
                }
                else
                {
                    DALCliente.CREATE(cliente);
                }
            }
        }

        public void UPDATE(Cliente cliente)
        {
            if (cliente.IdCliente > 0)
            {
                DALCliente.UPDATE(cliente);
            }
        }

        public void DELETE(int id)
        {
            if (id > 0)
            {
                DALCliente.DELETE(id);
            }
        }

        public List<Cliente> SELECTALL()
        {
            return DALCliente.SelectAll();
        }

        public Cliente SELECTBYID(int id)
        {
            return DALCliente.SelectById(id);
        }

        private bool Existe(string identificacion)
        {
            var lista = DALCliente.SelectAll();

            if (lista == null) return false; 

            foreach (var c in lista)
            {
                if (c.Identificacion == identificacion)
                    return true;
            }

            return false;
        }
    }
}

