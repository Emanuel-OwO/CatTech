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

        public bool Delete(int idFactura)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("usp_DELETE_Factura_ByID");
                    command.Parameters.AddWithValue("@IdFactura", idFactura);
                    command.CommandType = CommandType.StoredProcedure;

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception er) { _log.Error("Error DELETE Factura", er); throw; }
        
            return true;
        }

        public List<Factura> GetAll()
        {
            List<Factura> lista = new List<Factura>();

            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("usp_SELECT_Factura_All");
                    command.CommandType = CommandType.StoredProcedure;

                    var ds = db.ExecuteReader(command, "Factura");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new Factura
                        {
                            NumeroFactura = dr["NumeroFactura"].ToString(),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            IdCliente = Convert.ToInt32(dr["IdCliente"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            SubTotal = Convert.ToDouble(dr["SubTotal"]),
                            Impuesto = Convert.ToDouble(dr["Impuesto"]),
                            TotalColones = Convert.ToDouble(dr["TotalColones"]),
                            TotalDolares = Convert.ToDouble(dr["TotalDolares"]),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            catch (Exception er) { _log.Error("Error GET_ALL Factura", er); throw; }

            return lista;
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
                    command.CommandType = CommandType.StoredProcedure;

                    // ── Parámetros INPUT (sin @NumeroFactura — el SP lo genera solo) ──
                    command.Parameters.AddWithValue("@Fecha", factura.Fecha);
                    command.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                    command.Parameters.AddWithValue("@IdUsuario", factura.IdUsuario);
                    command.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = (decimal)factura.SubTotal;
                    command.Parameters.Add("@Impuesto", SqlDbType.Decimal).Value = (decimal)factura.Impuesto;
                    command.Parameters.Add("@TotalColones", SqlDbType.Decimal).Value = (decimal)factura.TotalColones;
                    command.Parameters.Add("@TotalDolares", SqlDbType.Decimal).Value = (decimal)factura.TotalDolares;
                    command.Parameters.AddWithValue("@XMLFactura",
                        factura.XMLFactura != null ? (object)factura.XMLFactura.ToString() : DBNull.Value);
                    command.Parameters.AddWithValue("@FirmaCliente",
                        factura.FirmaCliente != null ? (object)factura.FirmaCliente : DBNull.Value);

                    // ── Parámetros OUTPUT — el SP los llena ──────────────────────────
                    var paramNumero = new SqlParameter("@NumeroFactura", SqlDbType.VarChar, 50);
                    paramNumero.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramNumero);

                    var paramId = new SqlParameter("@IdFactura", SqlDbType.Int);
                    paramId.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramId);

                    db.ExecuteNonQuery(command);

                    // Guardar el número generado por el SP en la entidad
                    factura.NumeroFactura = paramNumero.Value?.ToString() ?? "";
                    return Convert.ToInt32(paramId.Value);
                }
            }
            catch (Exception er)
            {
                _log.Error("Error INSERT Factura", er);
                throw;
            }
        }

        public bool Update(Factura factura)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("usp_UPDATE_Factura");

                    command.Parameters.AddWithValue("@IdFactura", factura.IdFactura);
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
                    command.Parameters.AddWithValue("@Estado", factura.Estado);

                    command.CommandType = CommandType.StoredProcedure;

                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception er) { _log.Error("Error UPDATE Factura", er); throw; }

            return true;
        }

        public void UpDateNumFactura(int idFactura, string numeroFactura)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var cmd = new SqlCommand("usp_UPDATE_NumeroFactura");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdFactura", SqlDbType.Int).Value = idFactura;
                    cmd.Parameters.Add("@NumeroFactura", SqlDbType.VarChar, 50).Value = numeroFactura;

                    // ← ESTO es lo que faltaba: pasar el cmd al db, no llamarlo directo
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception er)
            {
                _log.Error("Error UPDATE NumeroFactura", er);
                throw;
            }
        }

        public void UpDateXMLFactura(int idFactura, string xmlFactura)
        {
            using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_UPDATE_XMLFactura"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdFactura", SqlDbType.Int).Value = idFactura;
                    cmd.Parameters.Add("@XMLFactura", SqlDbType.Xml).Value = xmlFactura;

                    db.ExecuteNonQuery(cmd); // ← este es el cambio
                }
            }
        }
    }
}
