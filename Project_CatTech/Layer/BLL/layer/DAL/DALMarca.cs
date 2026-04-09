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
using System.Windows.Forms;

namespace Project_CatTech.Layer.DAL
{
    public class DALMarca : IDALMarca
    {
        private static readonly ILog _log = LogManager.GetLogger("MyControlEventos");

        public void CREATE(Marca marca)
        {
            string msg = "";
            string sql = @"INSERT INTO Marca (CodigoMarca, Descripcion, Estado)
                           VALUES (@CodigoMarca, @Descripcion, @Estado)";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@CodigoMarca", marca.CodigoMarca);
                command.Parameters.AddWithValue("@Descripcion", marca.Descripcion);
                command.Parameters.AddWithValue("@Estado", marca.Estado);

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
            }
            catch (SqlException er)
            {
                _log.Error($"Error {msg}", er);
                throw;
            }
        }

        public  void DELETE(int id)
        {
            string msg = "";
            string sql = @"DELETE FROM Marca WHERE IdMarca=@IdMarca";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@IdMarca", id);

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
            }
            catch (SqlException er)
            {
                _log.Error($"Error {msg}", er);
                throw;
            }
        }

        public  List<Marca> SelectAll()
        {
            string msg = "";
            DataSet ds = null;
            List<Marca> lista = new List<Marca>();

            string sql = @"SELECT IdMarca, CodigoMarca, Descripcion, Estado 
                   FROM Marca 
                   WHERE Estado = 1 
                   ORDER BY Descripcion";
            //Agregue el where estado para que solo traiga las m,asrcas activas


            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "Marca");
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Marca m = new Marca()
                    {
                        IdMarca = Convert.ToInt32(dr["IdMarca"]),
                        CodigoMarca = dr["CodigoMarca"].ToString(),
                        Descripcion = dr["Descripcion"].ToString(),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };

                    lista.Add(m);
                }

                return lista;
            }
            catch (SqlException er)
            {
                _log.Error($"Error {msg}", er);
                throw;
            }
        }

        public   Marca SelectById(int id)
        {
            string msg = "";
            DataSet ds = null;
            Marca m = null;

            string sql = @"SELECT IdMarca, CodigoMarca, Descripcion, Estado
                           FROM Marca
                           WHERE IdMarca=@IdMarca";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@IdMarca", id);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "Marca");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    m = new Marca()
                    {
                        IdMarca = Convert.ToInt32(dr["IdMarca"]),
                        CodigoMarca = dr["CodigoMarca"].ToString(),
                        Descripcion = dr["Descripcion"].ToString(),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };
                }

                return m;
            }
            catch (SqlException er)
            {
                _log.Error($"Error {msg}", er);
                throw;
            }
        }

        public void UPDATE(Marca marca)
        {
            string msg = "";
            string sql = @"UPDATE Marca 
                           SET CodigoMarca=@CodigoMarca,
                               Descripcion=@Descripcion,
                               Estado=@Estado
                           WHERE IdMarca=@IdMarca";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@IdMarca", marca.IdMarca);
                command.Parameters.AddWithValue("@CodigoMarca", marca.CodigoMarca);
                command.Parameters.AddWithValue("@Descripcion", marca.Descripcion);
                command.Parameters.AddWithValue("@Estado", marca.Estado);

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
            }
            catch (SqlException er)
            {
                _log.Error($"Error {msg}", er);
                throw;
            }
        }
    }
}
