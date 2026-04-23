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
            int filas = 0;

            using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_DELETE_FacturaDetalle_ByFactura"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFactura", pIdFactura);

                   
                    filas = cmd.ExecuteNonQuery();
                }
            }

            return filas > 0;
        }

        public List<FacturaDetalle> GetByFactura(int pIdFactura)
        {
            List<FacturaDetalle> lista = new List<FacturaDetalle>();

            using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_SELECT_FacturaDetalle_ByFactura"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFactura", pIdFactura);

                    

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FacturaDetalle detalle = new FacturaDetalle
                            {
                                IdDetalle = Convert.ToInt32(reader["IdDetalle"]),
                                IdFactura = Convert.ToInt32(reader["IdFactura"]),
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Subtotal = Convert.ToDecimal(reader["Subtotal"])
                            };

                            lista.Add(detalle);
                        }
                    }
                }
            }

            return lista;
        }

        public int Insert(FacturaDetalle facturaDetalle)
        {
            int idDetalle = 0;

            using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_INSERT_FacturaDetalle"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdFactura", facturaDetalle.IdFactura);
                    cmd.Parameters.AddWithValue("@IdProducto", facturaDetalle.IdProducto);
                    cmd.Parameters.AddWithValue("@Cantidad", facturaDetalle.Cantidad);
                    cmd.Parameters.AddWithValue("@Precio", facturaDetalle.Precio);
                    cmd.Parameters.AddWithValue("@Subtotal", facturaDetalle.Subtotal);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                        idDetalle = Convert.ToInt32(result);
                }
            }

            return idDetalle;
        }
    }
}
