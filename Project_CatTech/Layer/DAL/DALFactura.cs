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
    public class DALFactura : IDALFactura
    {
        private static readonly ILog _log = LogManager.GetLogger("MyControlEventos");
        public void Delete(int idFactura)
        {
            
        }

        public List<Factura> GetAll()
        {
            throw new NotImplementedException();
        }

        public Factura GetById(int idFactura)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Factura_ObtenerPorId");
                    command.Parameters.AddWithValue("@IdFactura", idFactura);
                    command.CommandType = CommandType.StoredProcedure;
                    var ds = db.ExecuteReader(command, "Factura");

                    if (ds.Tables[0].Rows.Count == 0) return null;
                    DataRow dr = ds.Tables[0].Rows[0];

                    return new Factura
                    {
                        IdFactura = Convert.ToInt32(dr["IdFactura"]),
                        NumeroFactura = dr["NumeroFactura"].ToString(),
                        Fecha = Convert.ToDateTime(dr["Fecha"]),
                        IdCliente = Convert.ToInt32(dr["IdCliente"]),
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        SubTotal = Convert.ToDouble(dr["SubTotal"]),
                        Impuesto = Convert.ToDouble(dr["Impuesto"]),
                        TotalColones = Convert.ToDouble(dr["TotalColones"]),
                        TotalDolares = Convert.ToDouble(dr["TotalDolares"]),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };
                }
            }
            catch (Exception er) { _log.Error("Error SELECT_BY_ID Factura", er); throw; }
        }

        public int Insert(Factura factura)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Factura_Insertar");
                    command.Parameters.AddWithValue("@NumeroFactura", factura.NumeroFactura);
                    command.Parameters.AddWithValue("@Fecha", factura.Fecha);
                    command.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                    command.Parameters.AddWithValue("@IdUsuario", factura.IdUsuario);
                    command.Parameters.AddWithValue("@SubTotal", factura.SubTotal);
                    command.Parameters.AddWithValue("@Impuesto", factura.Impuesto);
                    command.Parameters.AddWithValue("@TotalColones", factura.TotalColones);
                    command.Parameters.AddWithValue("@TotalDolares", factura.TotalDolares);
                    command.Parameters.AddWithValue("@XMLFactura",
                        factura.XMLFactura != null ? (object)factura.XMLFactura.ToString() : DBNull.Value);
                    command.Parameters.AddWithValue("@FirmaCliente",
                        factura.FirmaCliente != null ? (object)factura.FirmaCliente : DBNull.Value);
                    command.Parameters.AddWithValue("@Estado", true);

                    // Parámetro OUTPUT para recibir el IdFactura
                    var paramId = new SqlParameter("@IdFactura", SqlDbType.Int);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    command.CommandType = CommandType.StoredProcedure;
                    db.ExecuteNonQuery(command);

                    return Convert.ToInt32(paramId.Value);
                }
            }
            catch (Exception er) { _log.Error("Error INSERT Factura", er); throw; }
        
        }

        public void Update(Factura factura)
        {
            throw new NotImplementedException();
        }
    }
}
