using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces.IBLL;
using Project_CatTech.Layer.Interfaces.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLFactura : IBLLFactura
    {
        private readonly IDALFactura _dalFactura;
        public BLLFactura()
        {
            _dalFactura = new DALFactura();
        }

        public decimal CalcularIVA(decimal subTotal)
        {
            return subTotal * 0.13m;
        }

        public decimal CalcularSubTotal(List<FacturaDetalle> listaDetalle)
        {
            decimal subtotal = 0;
            if(listaDetalle != null)
            {
                foreach (FacturaDetalle item in listaDetalle)
                {
                    subtotal += item.Subtotal;
                }
            }
            return subtotal;
        }

        public decimal CalcularTotalColones(decimal subTotal, decimal impuesto)
        {
           return subTotal + impuesto;
        }

        public decimal CalcularTotalDolares(decimal totalColones, decimal tipoCambio)
        {
            if (tipoCambio <= 0)
                throw new Exception("El tipo de cambio debe ser mayor a cero.");

            return totalColones / tipoCambio;

        }

        public bool Delete(int pIdFactura)
        {
            if (pIdFactura <= 0)
                throw new Exception("IdFactura inválido.");

            return _dalFactura.Delete(pIdFactura);
        }

        public List<Factura> GetAll()
        {
           return _dalFactura.GetAll();
        }

        public Factura GetById(int pIdFactura)
        {
            if (pIdFactura <= 0)
                throw new Exception("IdFactura inválido.");

            return _dalFactura.GetById(pIdFactura);
        }

        public int Save(Factura factura)
        {
            if (_dalFactura == null)
                throw new Exception("DAL no inicializado.");
            if (factura == null)
                throw new Exception("La facturación no puede ser nula.");

            // ↓ COMENTÁ o BORRÁ esta línea — el SP genera el número, no el form
            // if (string.IsNullOrWhiteSpace(factura.NumeroFactura))
            //     throw new Exception("El número de factura es obligatorio.");

            if (factura.IdCliente <= 0)
                throw new Exception("Debe seleccionar un cliente válido.");
            if (factura.IdUsuario <= 0)
                throw new Exception("Debe existir un usuario válido.");
            if (factura.TotalColones <= 0)
                throw new Exception("El total en colones debe ser mayor a cero.");

            factura.Estado = false; // pendiente
            return _dalFactura.Insert(factura);
        }


        public bool Update(Factura factura)
        {
            if (factura == null)
                throw new Exception("La facturación no puede ser nula.");

            if (factura.IdFactura <= 0)
                throw new Exception("IdFactura inválido.");
            return _dalFactura.Update(factura);
        }

        public void UpdateNumFactura(int idFactura, string numFactura)
        {
            _dalFactura.UpDateNumFactura(idFactura, numFactura);
        }

        public void UpdateXMLFactura(int idFactura, string xmlFactura)
        {
            if (idFactura <= 0)
                throw new Exception("IdFactura inválido.");

            if (string.IsNullOrWhiteSpace(xmlFactura))
                throw new Exception("El XML de la factura está vacío.");

            _dalFactura.UpDateXMLFactura(idFactura, xmlFactura);
        }
    }
}
 