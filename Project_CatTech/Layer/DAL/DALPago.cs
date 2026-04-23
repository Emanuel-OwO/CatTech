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
    public class DALPago : IDALPago
    {
        public int Insert(Pago pago)
        {
            int idPago = 0;

            using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_INSERT_Pago"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdFactura", SqlDbType.Int).Value = pago.IdFactura;
                    cmd.Parameters.Add("@TipoPago", SqlDbType.VarChar, 50).Value = pago.IdTipoPago;
                    cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 100).Value = pago.NumeroReferencia;

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                        idPago = Convert.ToInt32(result);
                }
            }

            return idPago;
        }
    }
}
