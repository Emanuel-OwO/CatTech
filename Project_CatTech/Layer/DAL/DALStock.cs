using log4net;
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
    public class DALStock : IDALStock
    {
        private static readonly ILog _log = LogManager.GetLogger("MyControlEventos");
        public void CREATE(Stock stock)
        {
            string sql = @"EXEC sp_MovimientoInventario_Insertar 
                       @IdProducto, @TipoMovimiento, @Cantidad, 
                       @Observaciones, @NumeroFacturaCompra";

            SqlCommand command = new SqlCommand();
            try
            {
                command.Parameters.AddWithValue("@IdProducto", stock.IdProducto);
                command.Parameters.AddWithValue("@TipoMovimiento", stock.TipoMovimiento);
                command.Parameters.AddWithValue("@Cantidad", stock.Cantidad);
                command.Parameters.AddWithValue("@Observaciones", stock.Observaciones ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@NumeroFacturaCompra", stock.NumeroFacturaCompra ?? (object)DBNull.Value);

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
            }
            catch (SqlException er)
            {
                _log.Error("Error en CREATE Stock", er);
                throw;
            }
        }

        public List<Stock> SelectAll()
        {
            string sql = @"SELECT m.IdMovimiento, m.IdProducto,
                              p.CodigoInterno + ' - ' + ISNULL(p.Modelo,'') AS NombreProducto,
                              m.TipoMovimiento, m.Cantidad, m.Fecha,
                              m.Observaciones, m.NumeroFacturaCompra
                       FROM MovimientoInventario m
                       INNER JOIN Producto p ON p.IdProducto = m.IdProducto
                       ORDER BY m.Fecha DESC";

            SqlCommand command = new SqlCommand();
            DataSet ds = null;
            List<Stock> lista = new List<Stock>();

            try
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "Stock");
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lista.Add(new Stock()
                    {
                        IdMovimiento = Convert.ToInt32(dr["IdMovimiento"]),
                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                        NombreProducto = dr["NombreProducto"].ToString(),
                        TipoMovimiento = dr["TipoMovimiento"].ToString(),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Fecha = Convert.ToDateTime(dr["Fecha"]),
                        Observaciones = dr["Observaciones"].ToString(),
                        NumeroFacturaCompra = dr["NumeroFacturaCompra"].ToString()
                    });
                }
                return lista;
            }
            catch (SqlException er)
            {
                _log.Error("Error en SelectAll Stock", er);
                throw;
            }
        }
    }
}
