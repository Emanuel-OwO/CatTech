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
    public class BLLTipoDispositivo : IBLLTipoDispositivo
    {
        private IDALTipoDispositivo dal = new DALTipoDispositivo();
        public void CREATE(TipoDispositivo tipo)
        {
            if (!string.IsNullOrEmpty(tipo.Descripcion))
            {
                if (Existe(tipo.Descripcion))
                {
                    dal.UPDATE(tipo);
                }
                else
                {
                    dal.CREATE(tipo);
                }
            }
        }

        public void DELETE(int id)
        {
            if (id > 0)
            {
                dal.DELETE(id);
            }
        }

        public List<TipoDispositivo> SELECTALL()
        {
            return dal.SelectAll();
        }

        public TipoDispositivo SELECTBYID(int id)
        {
            return dal.SelectById(id);
        }

        public void UPDATE(TipoDispositivo tipo)
        {
            if (tipo.IdTipoDispositivo > 0)
            {
                dal.UPDATE(tipo);
            }
        }

        private bool Existe(string descripcion)
        {
            var lista = dal.SelectAll();

            if (lista == null) return false;

            foreach (var t in lista)
            {
                if (t.Descripcion == descripcion)
                    return true;
            }

            return false;
        }
    }
}
