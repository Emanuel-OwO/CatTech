using Project_CatTech.Layer.DTO;
using Project_CatTech.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.Interfaces.IBLL
{
    public interface IBLLReporte
    {
        List<FacturaReporteDTO> GetFacturasPorFecha(DateTime fechaInicial, DateTime fechaFinal);
        List<Cliente> GetClientesReporte();
        List<ProductoVendidoReporteDTO> GetProductosVendidos(int? idMarca, string modelo, int? idTipoDispositivo);
        List<VentasPorFechaDTO> GetVentasPorFecha(DateTime fechaInicial, DateTime fechaFinal);
    }

}
