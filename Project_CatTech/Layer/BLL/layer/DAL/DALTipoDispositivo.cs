using log4net;
using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.DAL
{
    public class DALTipoDispositivo : IDALTipoDispositivo
    {
        private static readonly ILog _log = LogManager.GetLogger("MyControlEventos");
        public void CREATE(TipoDispositivo tipo)
        {
            string sql = @"INSERT INTO TipoDispositivo (Descripcion, Estado)
                       VALUES (@Descripcion, @Estado)";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@Descripcion", tipo.Descripcion);
                command.Parameters.AddWithValue("@Estado", tipo.Estado);

                command.CommandText = sql;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
            }
            catch (SqlException er)
            {
                _log.Error("Error CREATE TipoDispositivo", er);
                throw;
            }
        }

        public void DELETE(int id)
        {
            string sql = @"DELETE FROM TipoDispositivo WHERE IdTipoDispositivo=@Id";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@Id", id);
                command.CommandText = sql;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
            }
            catch (SqlException er)
            {
                _log.Error("Error DELETE TipoDispositivo", er);
                throw;
            }
        }

        public List<TipoDispositivo> SelectAll()
        {
            List<TipoDispositivo> lista = new List<TipoDispositivo>();
            DataSet ds = null;

            string sql = @"SELECT IdTipoDispositivo, Descripcion, Estado
                       FROM TipoDispositivo";

            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = sql;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "TipoDispositivo");
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TipoDispositivo t = new TipoDispositivo()
                    {
                        IdTipoDispositivo = Convert.ToInt32(dr["IdTipoDispositivo"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };

                    lista.Add(t);
                }

                return lista;
            }
            catch (SqlException er)
            {
                _log.Error("Error SelectAll TipoDispositivo", er);
                throw;
            }
        }

        public TipoDispositivo SelectById(int id)
        {
            TipoDispositivo t = null;
            DataSet ds = null;

            string sql = @"SELECT IdTipoDispositivo, Descripcion, Estado
                       FROM TipoDispositivo
                       WHERE IdTipoDispositivo=@Id";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@Id", id);
                command.CommandText = sql;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "TipoDispositivo");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    t = new TipoDispositivo()
                    {
                        IdTipoDispositivo = Convert.ToInt32(dr["IdTipoDispositivo"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };
                }

                return t;
            }
            catch (SqlException er)
            {
                _log.Error("Error SelectById TipoDispositivo", er);
                throw;
            }
        }

        public void UPDATE(TipoDispositivo tipo)
        {
            string sql = @"UPDATE TipoDispositivo
                       SET Descripcion=@Descripcion,
                           Estado=@Estado
                       WHERE IdTipoDispositivo=@Id";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@Id", tipo.IdTipoDispositivo);
                command.Parameters.AddWithValue("@Descripcion", tipo.Descripcion);
                command.Parameters.AddWithValue("@Estado", tipo.Estado);

                command.CommandText = sql;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
            }
            catch (SqlException er)
            {
                _log.Error("Error UPDATE TipoDispositivo", er);
                throw;
            }
        }
    }
}
