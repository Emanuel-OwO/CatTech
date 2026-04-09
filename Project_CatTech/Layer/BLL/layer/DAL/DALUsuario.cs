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
using System.Windows.Forms;

namespace Project_CatTech.Layer.DAL
{
    public class DALUsuario : IDALUsuario
    {
        private static readonly ILog _log = LogManager.GetLogger("MyControlEventos");
        public void DELETE(int idUsuario)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Usuario_Eliminar");
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    command.CommandType = CommandType.StoredProcedure;
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception er)
            {
                _log.Error("Error DELETE Usuario", er);
                MessageBox.Show(er.Message);
            }
        }

        public void INSERT(Usuario usuario)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Usuario_Insertar");

                    command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("@Clave", usuario.Clave);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@PrimerApellido", usuario.PrimerApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", usuario.SegundoApellido);
                    command.Parameters.AddWithValue("@IdPerfil", usuario.IdPerfil);
                    command.Parameters.AddWithValue("@Estado", usuario.Estado);

                    command.CommandType = CommandType.StoredProcedure;
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception er)
            {
                _log.Error("Error CREATE Usuario", er);
                MessageBox.Show(er.Message);
            }
        }

        public Usuario LOGIN(string nombreUsuario, string clave)
        {
            try
            {
                DataSet ds = null;

                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("usp_LOGIN_Usuario");

                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    command.Parameters.AddWithValue("@Clave", clave);
                    command.CommandType = CommandType.StoredProcedure;

                    ds = db.ExecuteReader(command, "Usuario");
                }

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    Usuario usuario = new Usuario();

                    usuario.IdUsuario = Convert.ToInt32(dt.Rows[0]["IdUsuario"]);
                    usuario.NombreUsuario = dt.Rows[0]["NombreUsuario"].ToString();
                    usuario.Clave = dt.Rows[0]["Clave"].ToString();
                    usuario.Nombre = dt.Rows[0]["Nombre"].ToString();
                    usuario.PrimerApellido = dt.Rows[0]["Apellidos"].ToString();
                    usuario.IdPerfil = Convert.ToInt32(dt.Rows[0]["IdPerfil"]);
                    usuario.Estado = Convert.ToBoolean(dt.Rows[0]["Estado"]);

                    return usuario;
                }

                return null;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return null;
            }
        }

        public List<Usuario> SELECT_ALL()
        {
            try
            {
                DataSet ds = null;

                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Usuario_ObtenerTodos");
                    command.CommandType = CommandType.StoredProcedure;

                    ds = db.ExecuteReader(command, "Usuario");
                }

                List<Usuario> lista = new List<Usuario>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Usuario u = new Usuario();
                    u.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    u.NombreUsuario = dr["NombreUsuario"].ToString();
                    u.Clave = dr["Clave"].ToString();
                    u.Nombre = dr["Nombre"].ToString();
                    u.PrimerApellido = dr["PrimerApellido"].ToString();
                    u.SegundoApellido = dr["SegundoApellido"].ToString();
                    u.IdPerfil = Convert.ToInt32(dr["IdPerfil"]);
                    u.Estado = Convert.ToBoolean(dr["Estado"]);

                    lista.Add(u);
                }

                return lista;
            }
            catch (Exception er)
            {
                _log.Error("Error SelectAll Usuario", er);
                MessageBox.Show(er.Message);
                return null;
            }
        }

        public Usuario SELECT_BY_ID(int idUsuario)
        {
            try
            {
                DataSet ds = null;

                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Usuario_ObtenerPorId");
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.CommandType = CommandType.StoredProcedure;

                    ds = db.ExecuteReader(command, "Usuario");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Usuario u = new Usuario();
                    u.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    u.NombreUsuario = dr["NombreUsuario"].ToString();
                    u.Clave = dr["Clave"].ToString();
                    u.Nombre = dr["Nombre"].ToString();
                    u.PrimerApellido = dr["PrimerApellido"].ToString();
                    u.SegundoApellido = dr["SegundoApellido"].ToString();
                    u.IdPerfil = Convert.ToInt32(dr["IdPerfil"]);
                    u.Estado = Convert.ToBoolean(dr["Estado"]);

                    return u;
                }

                return null;
            }
            catch (Exception er)
            {
                _log.Error("Error SelectById Usuario", er);
                MessageBox.Show(er.Message);
                return null;
            }
        }

        public void UPDATE(Usuario usuario)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Usuario_Actualizar");

                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("@Clave", usuario.Clave);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@PrimerApellido", usuario.PrimerApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", usuario.SegundoApellido);
                    command.Parameters.AddWithValue("@IdPerfil", usuario.IdPerfil);
                    command.Parameters.AddWithValue("@Estado", usuario.Estado);

                    command.CommandType = CommandType.StoredProcedure;
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception er)
            {
                _log.Error("Error UPDATE Usuario", er);
                MessageBox.Show(er.Message);
            }
        }
    }
}
