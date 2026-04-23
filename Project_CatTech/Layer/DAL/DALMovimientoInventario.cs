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
    public class DALMovimientoInventario : IDALMovimientoInventario
    {
        public void Insert(MovimientoInventario movimiento)
        {
            using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                var command = new SqlCommand("usp_INSERT_MovimientoInventario");

                command.Parameters.AddWithValue("@IdProducto", movimiento.IdProducto);
                command.Parameters.AddWithValue("@TipoMovimiento", movimiento.TipoMovimiento);
                command.Parameters.AddWithValue("@Cantidad", movimiento.Cantidad);
                command.Parameters.AddWithValue("@Fecha", movimiento.Fecha);
                command.Parameters.AddWithValue("@Observaciones", movimiento.Observaciones);

                command.CommandType = CommandType.StoredProcedure;

                db.ExecuteNonQuery(command);
            }
        }

        public List<MovimientoInventario> SELECT_ALL()
        {
            DataSet ds = null;

            using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                var command = new SqlCommand("usp_SELECT_MovimientoInventario_All");
                command.CommandType = CommandType.StoredProcedure;

                ds = db.ExecuteReader(command, "MovimientoInventario");
            }

            List<MovimientoInventario> lista = new List<MovimientoInventario>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MovimientoInventario movimiento = new MovimientoInventario();

                    movimiento.IdMovimiento = Convert.ToInt32(dr["IdMovimiento"]);
                    movimiento.IdProducto = Convert.ToInt32(dr["IdProducto"]);
                    movimiento.TipoMovimiento = dr["TipoMovimiento"].ToString();
                    movimiento.Cantidad = Convert.ToInt32(dr["Cantidad"]);
                    movimiento.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    movimiento.Observaciones = dr["Observaciones"].ToString();
                    movimiento.NumeroFacturaCompra = dr["NumeroFacturaCompra"].ToString();

                    lista.Add(movimiento);
                }
            }

            return lista;
        }

        public MovimientoInventario SELECT_BY_ID(int idMovimiento)
        {
            DataSet ds = null;

            using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                var command = new SqlCommand("usp_SELECT_MovimientoInventario_ByID");

                command.Parameters.AddWithValue("@IdMovimiento", idMovimiento);
                command.CommandType = CommandType.StoredProcedure;

                ds = db.ExecuteReader(command, "MovimientoInventario");
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                MovimientoInventario movimiento = new MovimientoInventario();

                movimiento.IdMovimiento = Convert.ToInt32(dt.Rows[0]["IdMovimiento"]);
                movimiento.IdProducto = Convert.ToInt32(dt.Rows[0]["IdProducto"]);
                movimiento.TipoMovimiento = dt.Rows[0]["TipoMovimiento"].ToString();
                movimiento.Cantidad = Convert.ToInt32(dt.Rows[0]["Cantidad"]);
                movimiento.Fecha = Convert.ToDateTime(dt.Rows[0]["Fecha"]);
                movimiento.Observaciones = dt.Rows[0]["Observaciones"].ToString();
                movimiento.NumeroFacturaCompra = dt.Rows[0]["NumeroFacturaCompra"].ToString();

                return movimiento;
            }

            return null;
        }
    }
}
