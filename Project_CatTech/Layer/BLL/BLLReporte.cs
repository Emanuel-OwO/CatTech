using Project_CatTech.Layer.DAL;
using Project_CatTech.Layer.DTO;
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
    public class BLLReporte : IBLLReporte
    {
        private readonly IDALReporte _dal = new DALReporte();

        public List<Cliente> GetClientesReporte()
        {
            return _dal.GetClientesReporte();
        }

        public List<FacturaReporteDTO> GetFacturasPorFecha(DateTime fechaInicial, DateTime fechaFinal)
        {
            if (fechaInicial > fechaFinal)
                throw new Exception("La fecha inicial no puede ser mayor que la final.");

            return _dal.GetFacturasPorFecha(fechaInicial, fechaFinal);
        }

        public List<ProductoVendidoReporteDTO> GetProductosVendidos(int? idMarca, string modelo, int? idTipoDispositivo)
        {
            return _dal.GetProductosVendidos(idMarca, modelo, idTipoDispositivo);
        }

        public List<VentasPorFechaDTO> GetVentasPorFecha(DateTime fechaInicial, DateTime fechaFinal)
        {
            if (fechaInicial > fechaFinal)
                throw new Exception("La fecha inicial no puede ser mayor que la final.");

            return _dal.GetVentasPorFecha(fechaInicial, fechaFinal);
        }
    }
}
