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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_CatTech.Layer.DAL
{
    public class DALFacturaDetalle : IDALFacturaDetalle
    {
        private static readonly ILog _log = LogManager.GetLogger("MyControlEventos");

        public bool DeleteByFactura(int pIdFactura)
        {
            try
            {
                string sql = "DELETE FROM FacturaDetalle WHERE IdFactura = @IdFactura";

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@IdFactura", pIdFactura);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                return true;
            }
            catch (Exception er)
            {
                _log.Error("Error DELETE FacturaDetalle", er);
                throw;
            }
        }

        public List<FacturaDetalle> GetByFactura(int pIdFactura)
        {
            try
            {
                string sql = "SELECT * FROM FacturaDetalle WHERE IdFactura = @IdFactura";

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@IdFactura", pIdFactura);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                var lista = new List<FacturaDetalle>();

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    DataSet ds = db.ExecuteReader(command, "FacturaDetalle");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new FacturaDetalle
                        {
                            IdDetalle = Convert.ToInt32(dr["IdDetalle"]),
                            IdFactura = Convert.ToInt32(dr["IdFactura"]),
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            Precio = (decimal)Convert.ToDouble(dr["Precio"]),
                            Subtotal = (decimal)Convert.ToDouble(dr["Subtotal"])
                        });
                    }
                }

                return lista;
            }
            catch (Exception er)
            {
                _log.Error("Error SELECT FacturaDetalle", er);
                throw;
            }
        }

        public int Insert(FacturaDetalle facturaDetalle)
        {
            try
            {
                // Usamos ExecuteNonQuery (no ExecuteScalar) porque el IDataBase
                // de este proyecto retorna double en ExecuteScalar, no object.
                // El IdDetalle no se necesita en el flujo de facturación.
                string sql = @"INSERT INTO FacturaDetalle 
                               (IdFactura, IdProducto, Cantidad, Precio, Subtotal)
                               VALUES 
                               (@IdFactura, @IdProducto, @Cantidad, @Precio, @Subtotal)";

                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@IdFactura", facturaDetalle.IdFactura);
                command.Parameters.AddWithValue("@IdProducto", facturaDetalle.IdProducto);
                command.Parameters.AddWithValue("@Cantidad", facturaDetalle.Cantidad);

                // Cast explícito a decimal para que coincida con decimal(10,2) de la BD
                command.Parameters.Add("@Precio", SqlDbType.Decimal).Value = (decimal)facturaDetalle.Precio;
                command.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = (decimal)facturaDetalle.Subtotal;
                command.Parameters["@Precio"].Precision = 10;
                command.Parameters["@Precio"].Scale = 2;
                command.Parameters["@Subtotal"].Precision = 10;
                command.Parameters["@Subtotal"].Scale = 2;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command);
                }

                return 1; // éxito
            }
            catch (Exception er)
            {
                _log.Error("Error INSERT FacturaDetalle", er);
                throw;
            }
        }
    }
}
