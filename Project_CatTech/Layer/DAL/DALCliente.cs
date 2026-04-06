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
    public class DALCliente : IDALCliente
    {
        private static readonly ILog _log = LogManager.GetLogger("MyControlEventos");

        public  void CREATE(Cliente cliente)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Cliente_Insertar");

                    command.Parameters.AddWithValue("@TipoIdentificacion", cliente.TipoIdentificacion);
                    command.Parameters.AddWithValue("@Identificacion", cliente.Identificacion);
                    command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    command.Parameters.AddWithValue("@PrimerApellido", cliente.PrimerApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", cliente.SegundoApellido);
                    command.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@Correo", cliente.Correo);
                    command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                    command.Parameters.AddWithValue("@Fotografia", cliente.Fotografia);
                    command.Parameters.AddWithValue("@Estado", cliente.Estado);
                

                    command.CommandType = CommandType.StoredProcedure;
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public  void UPDATE(Cliente cliente)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Cliente_Actualizar");

                    command.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                    command.Parameters.AddWithValue("@TipoIdentificacion", cliente.TipoIdentificacion);
                    command.Parameters.AddWithValue("@Identificacion", cliente.Identificacion);
                    command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    command.Parameters.AddWithValue("@PrimerApellido", cliente.PrimerApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", cliente.SegundoApellido);
                    command.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@Correo", cliente.Correo);
                    command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                    command.Parameters.AddWithValue("@Fotografia", cliente.Fotografia);
                    command.Parameters.AddWithValue("@Estado", cliente.Estado);

                    command.CommandType = CommandType.StoredProcedure;
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public  void DELETE(int id)
        {
            try
            {
                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Cliente_Eliminar");
                    command.Parameters.AddWithValue("@IdCliente", id);

                    command.CommandType = CommandType.StoredProcedure;
                    db.ExecuteNonQuery(command);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public  Cliente SelectById(int id)
        {
            try
            {
                DataSet ds = null;

                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("usp_Cliente_ObtenerPorId");
                    command.Parameters.AddWithValue("@IdCliente", id);
                    command.CommandType = CommandType.StoredProcedure;

                    ds = db.ExecuteReader(command, "Cliente");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Cliente c = new Cliente();
                    c.TipoIdentificacion = dr["TipoIdentificacion"].ToString();
                    c.Identificacion = dr["Identificacion"].ToString();
                    c.Nombre = dr["Nombre"].ToString();
                    c.PrimerApellido = dr["PrimerApellido"].ToString();
                    c.SegundoApellido = dr["SegundoApellido"].ToString();
                    c.Sexo = dr["Sexo"].ToString();
                    c.Telefono = dr["Telefono"].ToString();
                    c.Correo = dr["Correo"].ToString();
                    c.Direccion = dr["Direccion"].ToString();
                    c.Provincia = dr["Provincia"].ToString();
                    c.Fotografia = (byte[])dr["Fotografia"];
                    c.Estado = Convert.ToBoolean(dr["Estado"]);

                    return c;
                }

                return null;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return null;
            }
        }

        public  List<Cliente> SelectAll()
        {
            try
            {
                DataSet ds = null;

                using (var db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var command = new SqlCommand("sp_Cliente_ObtenerTodos");
                    command.CommandType = CommandType.StoredProcedure;

                    ds = db.ExecuteReader(command, "Cliente");
                }

                List<Cliente> lista = new List<Cliente>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Cliente c = new Cliente();
                    c.TipoIdentificacion = dr["TipoIdentificacion"].ToString();
                    c.Identificacion = dr["Identificacion"].ToString();
                    c.Nombre = dr["Nombre"].ToString();
                    c.PrimerApellido = dr["PrimerApellido"].ToString();
                    c.SegundoApellido = dr["SegundoApellido"].ToString();
                    c.Sexo = dr["Sexo"].ToString();
                    c.Telefono = dr["Telefono"].ToString();
                    c.Correo = dr["Correo"].ToString();
                    c.Direccion = dr["Direccion"].ToString();
                    c.Provincia = dr["Provincia"].ToString();
                    c.Fotografia = (byte[])dr["Fotografia"];
                    c.Estado = Convert.ToBoolean(dr["Estado"]);

                    lista.Add(c);
                }

                return lista;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return null;
            }
        }
    }
}

