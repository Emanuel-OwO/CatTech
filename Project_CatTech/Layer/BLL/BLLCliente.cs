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
        private IDALCliente _dal = new DALCliente();

        public void DELETE(int id)
        {
            if (id > 0)
            {
                _dal.DELETE(id);
            }
        }

        public void INSERT(Cliente cliente)
        {
            if (!string.IsNullOrEmpty(cliente.Identificacion))
            {
                if (Existe(cliente.Identificacion))
                {
                    _dal.UPDATE(cliente);
                }
                else
                {
                    _dal.CREATE(cliente);
                }
            }
        }

        public List<Cliente> SELECTALL()
        {
            return _dal.SelectAll();
        }

        public void UPDATE(Cliente cliente)
        {
            if (cliente.IdCliente > 0)
            {
                _dal.UPDATE(cliente);
            }
        }
        public Cliente SELECTBYID(int id)
        {
            return _dal.SelectById(id);
        }
        private bool Existe(string identificacion)
        {
            var lista = _dal.SelectAll();

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
