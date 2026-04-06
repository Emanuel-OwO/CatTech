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
    public class DALProveedor : IDALProveedor
    {
        private static readonly ILog _log = LogManager.GetLogger("MyControlEventos");
        public void CREATE(Proveedor proveedor)
        {
            string msg = "";
            string sql = @"INSERT INTO Proveedor 
                           (Nombre, Telefono, Correo, Direccion, Estado)
                           VALUES 
                           (@Nombre, @Telefono, @Correo, @Direccion, @Estado)";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                command.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                command.Parameters.AddWithValue("@Correo", proveedor.Correo);
                command.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                command.Parameters.AddWithValue("@Estado", proveedor.Estado);

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

        public void DELETE(int id)
        {
            string msg = "";
            string sql = @"DELETE FROM Proveedor WHERE IdProveedor=@IdProveedor";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@IdProveedor", id);

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

        public List<Proveedor> SelectAll()
        {
            string msg = "";
            DataSet ds = null;
            List<Proveedor> lista = new List<Proveedor>();

            string sql = @"SELECT IdProveedor, Nombre, Telefono, Correo, Direccion, Estado
                           FROM Proveedor
                           ORDER BY IdProveedor";

            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "Proveedor");
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Proveedor p = new Proveedor()
                    {
                        IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                        Nombre = dr["Nombre"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };

                    lista.Add(p);
                }

                return lista;
            }
            catch (SqlException er)
            {
                _log.Error($"Error {msg}", er);
                throw;
            }
        }

        public Proveedor SelectById(int id)
        {
            string msg = "";
            DataSet ds = null;
            Proveedor p = null;

            string sql = @"SELECT IdProveedor, Nombre, Telefono, Correo, Direccion, Estado
                           FROM Proveedor
                           WHERE IdProveedor=@IdProveedor";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@IdProveedor", id);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "Proveedor");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    p = new Proveedor()
                    {
                        IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                        Nombre = dr["Nombre"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };
                }

                return p;
            }
            catch (SqlException er)
            {
                _log.Error($"Error {msg}", er);
                throw;
            }
        }

        public void UPDATE(Proveedor proveedor)
        {
            string msg = "";
            string sql = @"UPDATE Proveedor 
                           SET Nombre=@Nombre,
                               Telefono=@Telefono,
                               Correo=@Correo,
                               Direccion=@Direccion,
                               Estado=@Estado
                           WHERE IdProveedor=@IdProveedor";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@IdProveedor", proveedor.IdProveedor);
                command.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                command.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                command.Parameters.AddWithValue("@Correo", proveedor.Correo);
                command.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                command.Parameters.AddWithValue("@Estado", proveedor.Estado);

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
