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
    public class BLLMarca : IBLLMarca
    {
        public void CREATE(Marca marca)
        {
            IDALMarca dal = new DALMarca();

            if (!string.IsNullOrEmpty(marca.CodigoMarca))
            {
                if (Existe(marca.CodigoMarca))
                {
                    dal.UPDATE(marca);
                }
                else
                {
                    dal.CREATE(marca);
                }
            }
            else
            {
                throw new Exception("El código de marca es obligatorio");
            }
        }

        public void DELETE(int id)
        {
            IDALMarca dal = new DALMarca();

            if (id <= 0)
                throw new Exception("Id inválido");

            dal.DELETE(id);
        }

        public List<Marca> SelectAll()
        {
            IDALMarca dal = new DALMarca();
            return dal.SelectAll();
        }

        public Marca SelectById(int id)
        {
            IDALMarca dal = new DALMarca();
            return dal.SelectById(id);
        }

        public void UPDATE(Marca marca)
        {
            IDALMarca dal = new DALMarca();

            if (marca.IdMarca <= 0)
                throw new Exception("Id inválido");

            dal.UPDATE(marca);

        }

        private bool Existe(string codigoMarca)
        {
            IDALMarca dal = new DALMarca();
            var lista = dal.SelectAll();

            if (lista == null) return false;

            foreach (var m in lista)
            {
                if (m.CodigoMarca == codigoMarca)
                    return true;
            }

            return false;
        }
    }
}
