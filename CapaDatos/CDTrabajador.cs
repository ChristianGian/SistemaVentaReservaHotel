using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidad;
using System.Configuration;
using System.Windows.Forms;

namespace CapaDatos
{
    public class CDTrabajador
    {
        public List<ETrabajador> ListarTrabajador()
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<ETrabajador>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ListarTrabajador";
                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var trabajador = new ETrabajador()
                            {
                                IdPersona = Convert.ToInt32(drd["IdPersona"]),
                                Nombre = Convert.ToString(drd["Nombre"]),
                                ApePaterno = Convert.ToString(drd["ApePaterno"]),
                                ApeMaterno = Convert.ToString(drd["ApeMaterno"]),
                                TipoDoc = Convert.ToString(drd["TipoDoc"]),
                                NumeroDoc = Convert.ToString(drd["NumeroDoc"]),
                                Direccion = Convert.ToString(drd["Direccion"]),
                                Telefono = Convert.ToString(drd["Telefono"]),
                                Email = Convert.ToString(drd["Email"]),
                                Sueldo = Convert.ToDecimal(drd["Sueldo"]),
                                Acceso = Convert.ToString(drd["Acceso"]),
                                Sesion = Convert.ToString(drd["Sesion"]),
                                Contrasenia = Convert.ToString(drd["Contrasenia"]),
                                Estado = Convert.ToString(drd["Estado"])
                            };
                            lista.Add(trabajador);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Listar Trabajador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return lista;
        }

        public int RegistrarPersonaTrabajador(ETrabajador trabajador)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            int idPersona = 0;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "RegistrarPersona";

                        cmd.Parameters.AddWithValue("@Nombre", trabajador.Nombre);
                        cmd.Parameters.AddWithValue("@ApePaterno", trabajador.ApePaterno);
                        cmd.Parameters.AddWithValue("@ApeMaterno", trabajador.ApeMaterno);
                        cmd.Parameters.AddWithValue("@TipoDoc", trabajador.TipoDoc);
                        cmd.Parameters.AddWithValue("@NumeroDoc", trabajador.NumeroDoc);
                        cmd.Parameters.AddWithValue("@Direccion", trabajador.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", trabajador.Telefono);
                        cmd.Parameters.AddWithValue("@Email", trabajador.Email);
                        cmd.Parameters.Add("@IdPersona", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        idPersona = Convert.ToInt32(cmd.Parameters["@IdPersona"].Value.ToString());
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Registrar PersonaTrabajador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return idPersona;
        }

        public bool RegistrarTrabajador(ETrabajador trabajador)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            int res = 0;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "RegistrarTrabajador";

                        cmd.Parameters.AddWithValue("@IdPersona", trabajador.IdPersona);
                        cmd.Parameters.AddWithValue("@Sueldo", trabajador.Sueldo);
                        cmd.Parameters.AddWithValue("@Acceso", trabajador.Acceso);
                        cmd.Parameters.AddWithValue("@Sesion", trabajador.Sesion);
                        cmd.Parameters.AddWithValue("@Contrasenia", trabajador.Contrasenia);
                        cmd.Parameters.AddWithValue("@Estado", trabajador.Estado);

                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Registrar Trabajador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (res == 1) return true;
            else return false;
        }

        public bool EditarPersonaTrabajador(ETrabajador trabajador)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            int respuesta = 0;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "EditarPersona";

                        cmd.Parameters.AddWithValue("@IdPersona", trabajador.IdPersona);
                        cmd.Parameters.AddWithValue("@Nombre", trabajador.Nombre);
                        cmd.Parameters.AddWithValue("@ApePaterno", trabajador.ApePaterno);
                        cmd.Parameters.AddWithValue("@ApeMaterno", trabajador.ApeMaterno);
                        cmd.Parameters.AddWithValue("@TipoDoc", trabajador.TipoDoc);
                        cmd.Parameters.AddWithValue("@NumeroDoc", trabajador.NumeroDoc);
                        cmd.Parameters.AddWithValue("@Direccion", trabajador.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", trabajador.Telefono);
                        cmd.Parameters.AddWithValue("@Email", trabajador.Email);

                        respuesta = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar PersonaTrabajador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (respuesta == 1) return true;
            else return false;
        }

        public bool EditarTrabajador(ETrabajador trabajador)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            int respuesta = 0;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "EditarTrabajador";

                        cmd.Parameters.AddWithValue("@IdPersona", trabajador.IdPersona);
                        cmd.Parameters.AddWithValue("@Sueldo", trabajador.Sueldo);
                        cmd.Parameters.AddWithValue("@Acceso", trabajador.Acceso);
                        cmd.Parameters.AddWithValue("@Sesion", trabajador.Sesion);
                        cmd.Parameters.AddWithValue("@Contrasenia", trabajador.Contrasenia);
                        cmd.Parameters.AddWithValue("@Estado", trabajador.Estado);

                        respuesta = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar Traabajador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (respuesta == 1) return true;
            else return false;
        }

        public bool EliminarPersonaTrabajador(int idPersona)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            int respuesta = 0;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "EliminarPersona";

                        cmd.Parameters.AddWithValue("@IdPersona", idPersona);

                        respuesta = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Eliminar PersonaTrabajador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (respuesta == 1) return true;
            else return false;
        }

        public bool EliminarTrabajador(int idPersona)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            int respuesta = 0;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "EliminarTrabajador";

                        cmd.Parameters.AddWithValue("@IdPersona", idPersona);

                        respuesta = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Eliminar Trabajador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (respuesta == 1) return true;
            else return false;
        }

        public List<ETrabajador> BuscarTrabajador(string numeroDoc)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<ETrabajador>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "BuscarTrabajador";

                        cmd.Parameters.AddWithValue("@NumeroDoc", numeroDoc);

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var trabajador = new ETrabajador()
                            {
                                IdPersona = Convert.ToInt32(drd["IdPersona"]),
                                Nombre = Convert.ToString(drd["Nombre"]),
                                ApePaterno = Convert.ToString(drd["ApePaterno"]),
                                ApeMaterno = Convert.ToString(drd["ApeMaterno"]),
                                TipoDoc = Convert.ToString(drd["TipoDoc"]),
                                NumeroDoc = Convert.ToString(drd["NumeroDoc"]),
                                Direccion = Convert.ToString(drd["Direccion"]),
                                Telefono = Convert.ToString(drd["Telefono"]),
                                Email = Convert.ToString(drd["Email"]),
                                Sueldo = Convert.ToDecimal(drd["Sueldo"]),
                                Acceso = Convert.ToString(drd["Acceso"]),
                                Sesion = Convert.ToString(drd["Sesion"]),
                                Contrasenia = Convert.ToString(drd["Contrasenia"]),
                                Estado = Convert.ToString(drd["Estado"])
                            };
                            lista.Add(trabajador);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Buscar trabajador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return lista;
        }
    }
}
