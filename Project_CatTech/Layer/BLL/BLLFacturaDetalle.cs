using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces.IBLL;
using Project_CatTech.Layer.Interfaces.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_CatTech.Layer.BLL
{
    public class BLLFacturaDetalle : IBLLFacturaDetalle
    {
        private readonly IDALFacturaDetalle _dALFacturaDetalle;
        public BLLFacturaDetalle()
        {
            _dALFacturaDetalle = new DALFacturaDetalle();
        }

        public decimal CalcularSubTotal(int cantidad, decimal precio)
        {
            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            if (precio <= 0)
                throw new Exception("El precio debe ser mayor a cero.");

            return cantidad * precio;
        }

        public bool DeleteByFactura(int pIdFactura)
        {
            if (pIdFactura <= 0)
                throw new Exception("IdFactura inválido.");

            return _dALFacturaDetalle.DeleteByFactura(pIdFactura);
        }

        public List<FacturaDetalle> GetByFactura(int pIdFactura)
        {
            if (pIdFactura <= 0)
                throw new Exception("IdFactura inválido.");
            return _dALFacturaDetalle.GetByFactura(pIdFactura);
        }

        public int Save(FacturaDetalle facturaDetalle)
        {
            if (facturaDetalle == null)
                throw new Exception("El detalle no puede ser nulo.");

            if (facturaDetalle.IdFactura <= 0)
                throw new Exception("IdFactura inválido.");

            if (facturaDetalle.IdProducto <= 0)
                throw new Exception("Debe seleccionar un producto válido.");

            if (facturaDetalle.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            if (facturaDetalle.Precio <= 0)
                throw new Exception("El precio debe ser mayor a cero.");

            if (facturaDetalle.Subtotal <= 0)
                throw new Exception("El subtotal debe ser mayor a cero.");

            return _dALFacturaDetalle.Insert(facturaDetalle);
        }
    }
}
