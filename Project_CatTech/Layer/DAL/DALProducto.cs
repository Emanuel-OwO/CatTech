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
    public class DALProducto : IDALProducto
    {
        private static readonly ILog _log = LogManager.GetLogger("MyControlEventos");

        public void CREATE(Producto producto)
        {
            string msg = "";
            string sql = @"INSERT INTO Producto 
                      (CodigoInterno, CodigoBarras, IdProveedor, Color, Caracteristicas, Extras,
                       DocumentoEspecificaciones, Foto, Precio, CantidadStock, Estado,
                       Modelo, IdMarca, IdTipoDispositivo)
                       VALUES 
                      (@CodigoInterno, @CodigoBarras, @IdProveedor, @Color, @Caracteristicas, @Extras,
                       @Documento, @Foto, @Precio, @CantidadStock, @Estado,
                       @Modelo, @IdMarca, @IdTipoDispositivo)";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@CodigoInterno", producto.CodigoInterno);
                command.Parameters.AddWithValue("@CodigoBarras", producto.CodigoBarras ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IdProveedor", producto.IdProveedor);
                command.Parameters.AddWithValue("@Color", producto.Color ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Caracteristicas", producto.Caracteristicas ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Extras", producto.Extras ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Documento", producto.DocumentoEspecificaciones ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Foto", producto.Foto ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@CantidadStock", producto.CantidadStock);
                command.Parameters.AddWithValue("@Estado", producto.Estado);
                command.Parameters.AddWithValue("@Modelo", producto.Modelo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IdMarca", producto.IdMarca);
                command.Parameters.AddWithValue("@IdTipoDispositivo", producto.IdTipoDispositivo);

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
            string sql = @"UPDATE Producto SET Estado = 0 WHERE IdProducto = @IdProducto";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@IdProducto", id);
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

        public List<Producto> SelectAll()
        {
            string msg = "";
            DataSet ds = null;
            List<Producto> lista = new List<Producto>();

            string sql = @"SELECT IdProducto, CodigoInterno, CodigoBarras, IdProveedor,
                              Color, Caracteristicas, Extras,
                              DocumentoEspecificaciones, Foto,
                              Precio, CantidadStock, Estado,
                              Modelo, IdMarca, IdTipoDispositivo
                       FROM Producto
                       WHERE Estado = 1
                       ORDER BY IdProducto";

            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "Producto");
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Producto p = new Producto()
                    {
                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                        CodigoInterno = dr["CodigoInterno"].ToString(),
                        CodigoBarras = dr["CodigoBarras"].ToString(),
                        IdProveedor = dr["IdProveedor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProveedor"]),
                        Color = dr["Color"].ToString(),
                        Caracteristicas = dr["Caracteristicas"].ToString(),
                        Extras = dr["Extras"].ToString(),
                        DocumentoEspecificaciones = dr["DocumentoEspecificaciones"] == DBNull.Value ? null : (byte[])dr["DocumentoEspecificaciones"],
                        Foto = dr["Foto"] == DBNull.Value ? null : (byte[])dr["Foto"],
                        Precio = Convert.ToDouble(dr["Precio"]),
                        CantidadStock = Convert.ToInt32(dr["CantidadStock"]),
                        Estado = Convert.ToBoolean(dr["Estado"]),
                        Modelo = dr["Modelo"].ToString(),
                        IdMarca = dr["IdMarca"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdMarca"]),
                        IdTipoDispositivo = dr["IdTipoDispositivo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdTipoDispositivo"])
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

        public Producto SelectById(int id)
        {
            string msg = "";
            DataSet ds = null;
            Producto p = null;

            string sql = @"SELECT IdProducto, CodigoInterno, CodigoBarras, IdProveedor,
                              Color, Caracteristicas, Extras,
                              DocumentoEspecificaciones, Foto,
                              Precio, CantidadStock, Estado,
                              Modelo, IdMarca, IdTipoDispositivo
                       FROM Producto
                       WHERE IdProducto = @IdProducto";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@IdProducto", id);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "Producto");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    p = new Producto()
                    {
                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                        CodigoInterno = dr["CodigoInterno"].ToString(),
                        CodigoBarras = dr["CodigoBarras"].ToString(),
                        IdProveedor = dr["IdProveedor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdProveedor"]),
                        Color = dr["Color"].ToString(),
                        Caracteristicas = dr["Caracteristicas"].ToString(),
                        Extras = dr["Extras"].ToString(),
                        DocumentoEspecificaciones = dr["DocumentoEspecificaciones"] == DBNull.Value ? null : (byte[])dr["DocumentoEspecificaciones"],
                        Foto = dr["Foto"] == DBNull.Value ? null : (byte[])dr["Foto"],
                        Precio = Convert.ToDouble(dr["Precio"]),
                        CantidadStock = Convert.ToInt32(dr["CantidadStock"]),
                        Estado = Convert.ToBoolean(dr["Estado"]),
                        Modelo = dr["Modelo"].ToString(),
                        IdMarca = dr["IdMarca"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdMarca"]),
                        IdTipoDispositivo = dr["IdTipoDispositivo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdTipoDispositivo"])
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

        public void UPDATE(Producto producto)
        {
            string msg = "";
            string sql = @"UPDATE Producto 
                       SET CodigoInterno    = @CodigoInterno,
                           CodigoBarras     = @CodigoBarras,
                           IdProveedor      = @IdProveedor,
                           Color            = @Color,
                           Caracteristicas  = @Caracteristicas,
                           Extras           = @Extras,
                           DocumentoEspecificaciones = @Documento,
                           Foto             = @Foto,
                           Precio           = @Precio,
                           CantidadStock    = @CantidadStock,
                           Estado           = @Estado,
                           Modelo           = @Modelo,
                           IdMarca          = @IdMarca,
                           IdTipoDispositivo = @IdTipoDispositivo
                       WHERE IdProducto = @IdProducto";

            SqlCommand command = new SqlCommand();

            try
            {
                command.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                command.Parameters.AddWithValue("@CodigoInterno", producto.CodigoInterno);
                command.Parameters.AddWithValue("@CodigoBarras", producto.CodigoBarras ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IdProveedor", producto.IdProveedor);
                command.Parameters.AddWithValue("@Color", producto.Color ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Caracteristicas", producto.Caracteristicas ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Extras", producto.Extras ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Documento", producto.DocumentoEspecificaciones ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Foto", producto.Foto ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@CantidadStock", producto.CantidadStock);
                command.Parameters.AddWithValue("@Estado", producto.Estado);
                command.Parameters.AddWithValue("@Modelo", producto.Modelo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IdMarca", producto.IdMarca);
                command.Parameters.AddWithValue("@IdTipoDispositivo", producto.IdTipoDispositivo);

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
