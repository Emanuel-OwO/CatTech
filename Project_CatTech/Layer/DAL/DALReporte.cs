using Project_CatTech.Layer.DTO;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.DAL
{
    public class DALReporte : IDALReporte
    {
        private readonly IDataBase _db;

        public DALReporte()
        {
            _db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection());
        }

        public List<Cliente> GetClientesReporte()
        {
            List<Cliente> lista = new List<Cliente>();

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "usp_REPORTE_Clientes"
            };

            DataSet ds = _db.ExecuteReader(cmd, "query");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Cliente cliente = new Cliente
                {
                    IdCliente = Convert.ToInt32(row["IdCliente"]),
                    Identificacion = row["Identificacion"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    PrimerApellido = row["Apellido1"].ToString(),
                    SegundoApellido = row["Apellido2"].ToString(),
                    Telefono = row["Telefono"].ToString(),
                    Correo = row["Correo"].ToString(),
                    Direccion = row["Direccion"].ToString(),
                    Provincia = row["Provincia"].ToString(),
                    Fotografia = row["Fotografia"] != DBNull.Value ? (byte[])row["Fotografia"] : null
                };

                lista.Add(cliente);
            }

            return lista;
        }

        public List<FacturaReporteDTO> GetFacturasPorFecha(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<FacturaReporteDTO> lista = new List<FacturaReporteDTO>();

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "usp_REPORTE_FacturasPorFecha"
            };

            cmd.Parameters.AddWithValue("@FechaInicial", fechaInicial);
            cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal);

            DataSet ds = _db.ExecuteReader(cmd, "query");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lista.Add(new FacturaReporteDTO
                {
                    IdFactura = Convert.ToInt32(row["IdFactura"]),
                    NumeroFactura = row["NumeroFactura"].ToString(),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Cliente = row["Cliente"].ToString(),
                    Usuario = row["Usuario"].ToString(),
                    TipoPago = row["TipoPago"].ToString(),
                    TotalColones = Convert.ToDecimal(row["TotalColones"]),
                    Estado = row["Estado"].ToString()
                });
            }

            return lista;
        }

        public List<ProductoVendidoReporteDTO> GetProductosVendidos(int? idMarca, string modelo, int? idTipoDispositivo)
        {
            List<ProductoVendidoReporteDTO> lista = new List<ProductoVendidoReporteDTO>();

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "usp_REPORTE_ProductosVendidosFiltrado"
            };

            cmd.Parameters.AddWithValue("@IdMarca", (object)idMarca ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Modelo", string.IsNullOrWhiteSpace(modelo) ? (object)DBNull.Value : modelo);
            cmd.Parameters.AddWithValue("@IdTipoDispositivo", (object)idTipoDispositivo ?? DBNull.Value);

            DataSet ds = _db.ExecuteReader(cmd, "query");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ProductoVendidoReporteDTO item = new ProductoVendidoReporteDTO
                {
                    IdProducto = Convert.ToInt32(row["IdProducto"]),
                    CodigoInterno = row["CodigoInterno"].ToString(),
                    Marca = row["Marca"].ToString(),
                    Modelo = row["Modelo"].ToString(),
                    TipoDispositivo = row["TipoDispositivo"].ToString(),
                    Precio = Convert.ToDecimal(row["Precio"]),
                    CantidadVendida = Convert.ToInt32(row["CantidadVendida"]),
                    Fotografia = row["Fotografia"] != DBNull.Value ? (byte[])row["Fotografia"] : null
                };

                lista.Add(item);
            }

            return lista;
        }

        public List<VentasPorFechaDTO> GetVentasPorFecha(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<VentasPorFechaDTO> lista = new List<VentasPorFechaDTO>();

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "usp_REPORTE_VentasPorFecha"
            };

            cmd.Parameters.AddWithValue("@FechaInicial", fechaInicial);
            cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal);

            DataSet ds = _db.ExecuteReader(cmd, "query");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lista.Add(new VentasPorFechaDTO
                {
                    FechaVenta = Convert.ToDateTime(row["FechaVenta"]),
                    TotalVenta = Convert.ToDecimal(row["TotalVenta"])
                });
            }

            return lista;
        }
    }
}
