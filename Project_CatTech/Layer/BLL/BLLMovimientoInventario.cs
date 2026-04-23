using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using Project_CatTech.Layer.Interfaces.IBLL;
using Project_CatTech.Layer.Interfaces.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLMovimientoInventario : IBLLMovimientoInventario
    {
        private IDALMovimientoInventario dALMovimientoInventario = new DALMovimientoInventario();
        private IBLLProducto bLLProducto = new BLLProducto();
        private readonly IDALMovimientoInventario _objdALMovimientoInventario = new DALMovimientoInventario();

        public void INSERT(MovimientoInventario movimiento)
        {
            if (movimiento == null)
                throw new Exception("El movimiento no puede ser nulo.");

            if (movimiento.IdProducto <= 0)
                throw new Exception("Debe seleccionar un producto.");

            if (string.IsNullOrWhiteSpace(movimiento.TipoMovimiento))
                throw new Exception("Debe seleccionar un tipo de movimiento.");

            if (movimiento.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            movimiento.TipoMovimiento = movimiento.TipoMovimiento.Trim().ToUpper();

            if (movimiento.TipoMovimiento != "E" && movimiento.TipoMovimiento != "S")
                throw new Exception("El tipo de movimiento debe ser E o S.");

            if (movimiento.Observaciones == null)
                movimiento.Observaciones = "";

            if (movimiento.NumeroFacturaCompra == null)
                movimiento.NumeroFacturaCompra = "";

            Producto producto = bLLProducto.SelectById(movimiento.IdProducto);

            if (producto == null)
                throw new Exception("No se encontró el producto seleccionado.");

            if (movimiento.TipoMovimiento == "E")
            {
                producto.CantidadStock += movimiento.Cantidad;
            }
            else if (movimiento.TipoMovimiento == "S")
            {
                if (producto.CantidadStock < movimiento.Cantidad)
                    throw new Exception("No hay suficiente stock para realizar la salida.");

                producto.CantidadStock -= movimiento.Cantidad;
            }

            movimiento.Fecha = DateTime.Now;

            _objdALMovimientoInventario.Insert(movimiento);
            bLLProducto.UPDATE(producto);
        }

        public void Save(MovimientoInventario movimiento)
        {
            if (movimiento == null)
                throw new Exception("El movimiento no puede ser nulo.");

            if (movimiento.IdProducto <= 0)
                throw new Exception("El Id del producto es inválido.");

            if (string.IsNullOrWhiteSpace(movimiento.TipoMovimiento))
                throw new Exception("El tipo de movimiento es obligatorio.");

            if (movimiento.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(movimiento.Observaciones))
                movimiento.Observaciones = "Movimiento de inventario";

            _objdALMovimientoInventario.Insert(movimiento);
        }

        public List<MovimientoInventario> SELECT_ALL()
        {
            return dALMovimientoInventario.SELECT_ALL();
        }

        public MovimientoInventario SELECT_BY_ID(int idMovimiento)
        {
            if (idMovimiento <= 0)
                throw new Exception("El Id del movimiento es inválido.");

            return dALMovimientoInventario.SELECT_BY_ID(idMovimiento);
        }
    }
}
