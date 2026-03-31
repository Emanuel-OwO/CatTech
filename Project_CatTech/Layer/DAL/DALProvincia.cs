using appSweetTech.Extensiones;
using log4net;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Project_CatTech.Layer.DAL
{
    public class DALProvincia : IDALProvincia
    {
        private static readonly ILog _myLogControlEventos = LogManager.GetLogger("MyControlEventos");
        public bool Delete(int pId)
        {
            string msg = "";
            SqlCommand command = new SqlCommand();
            int filasAfectadas = 0;

            string sql = @"DELETE FROM Provincia WHERE IdProvincia = @IdProvincia";

            command.Parameters.AddWithValue("@IdProvincia", pId);
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            try
            {
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    filasAfectadas = (int)db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                return filasAfectadas > 0;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }
        public List<Provincia> GetAll()
        {
            string msg = "";
            IDataReader reader = null;
            List<Provincia> lista = new List<Provincia>();
            SqlCommand command = new SqlCommand();
            string sql = @" select * from  Provincia WITH (NOLOCK)  ";
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            try
            {
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);
                    while (reader.Read())
                    {
                        // Mapping 
                        Provincia oProvincia = new Provincia();
                        oProvincia.ID_provincia = int.Parse(reader["IdProvincia"].ToString());
                        oProvincia.Descripcion = reader["Descripcion"].ToString();
                        lista.Add(oProvincia);
                    }
                }
                return lista;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }

        public Provincia GetById(int pId)
        {
            string msg = "";
            IDataReader reader = null;
            Provincia oProvincia = null;
            SqlCommand command = new SqlCommand();
            string sql = @" select * from  Provincia WITH (NOLOCK)  where IdProvincia = @IdProvincia ";
            command.Parameters.AddWithValue("@IdProvincia", pId);
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            try
            {
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);
                    while (reader.Read())
                    {
                        // Mapping 
                        oProvincia = new Provincia();
                        oProvincia.ID_provincia = int.Parse(reader["IdProvincia"].ToString());
                        oProvincia.Descripcion = reader["Descripcion"].ToString();
                    }
                }
                return oProvincia;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }
        public Provincia Save(Provincia pProvincia)
        {
            string msg = "";
            SqlCommand command = new SqlCommand();
            Provincia oProvincia = null;

            string sql = @"INSERT INTO Provincia (IdProvincia, Descripcion)
                           VALUES (@IdProvincia, @Descripcion)";

            command.Parameters.AddWithValue("@IdProvincia", pProvincia.ID_provincia);
            command.Parameters.AddWithValue("@Descripcion", pProvincia.Descripcion);
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            try
            {
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                oProvincia = this.GetById(pProvincia.ID_provincia);
                return oProvincia;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));

            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }
        public Provincia Update(Provincia pProvincia)
        {
            string msg = "";
            SqlCommand command = new SqlCommand();
            Provincia oProvincia = null;

            string sql = @"UPDATE Provincia
                           SET Descripcion = @Descripcion
                           WHERE IdProvincia = @IdProvincia";

            command.Parameters.AddWithValue("@IdProvincia", pProvincia.ID_provincia);
            command.Parameters.AddWithValue("@Descripcion", pProvincia.Descripcion);
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            try
            {
                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                oProvincia = this.GetById(pProvincia.ID_provincia);
                return oProvincia;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }

        public List<Provincia> GetProvinciasFromInternet()
        {
            List<Provincia> lista = new List<Provincia>();
            string json = "";

            // Leer URL del App.config
            string url = ConfigurationManager.AppSettings["URLProvincia"];

            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";

            using (WebResponse webResponse = request.GetResponse())
            {
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                json = reader.ReadToEnd();
            }

            lista = System.Text.Json.JsonSerializer.Deserialize<List<Provincia>>(json);

            return lista;
        }
    }
}

