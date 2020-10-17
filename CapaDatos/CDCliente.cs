using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entidad;
using System.Windows.Forms;

namespace CapaDatos
{
    public class CDCliente
    {
        public List<ECliente> Listar()
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<ECliente>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ListarCliente";
                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var cliente = new ECliente()
                            {
                                IdPersona = Convert.ToInt32(drd["IdPersona"]),
                                IdCliente = Convert.ToString(drd["IdCliente"]),
                                Nombre = Convert.ToString(drd["Nombre"]),
                                ApePaterno = Convert.ToString(drd["ApePaterno"]),
                                ApeMaterno = Convert.ToString(drd["ApeMaterno"]),
                                TipoDoc = Convert.ToString(drd["TipoDoc"]),
                                NumeroDoc = Convert.ToString(drd["NumeroDoc"]),
                                Direccion = Convert.ToString(drd["Direccion"]),
                                Telefono = Convert.ToString(drd["Telefono"]),
                                Email = Convert.ToString(drd["Email"])
                            };
                            lista.Add(cliente);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Listar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return lista;
        }

        public int RegistrarPersonaCliente(ECliente cliente)
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

                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@ApePaterno", cliente.ApePaterno);
                        cmd.Parameters.AddWithValue("@ApeMaterno", cliente.ApeMaterno);
                        cmd.Parameters.AddWithValue("@TipoDoc", cliente.TipoDoc);
                        cmd.Parameters.AddWithValue("@NumeroDoc", cliente.NumeroDoc);
                        cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.Parameters.Add("@IdPersona", SqlDbType.Int).Direction = ParameterDirection.Output;

                        
                        cmd.ExecuteNonQuery();
                        //Capturamos el IdPersona
                        idPersona = Convert.ToInt32(cmd.Parameters["@IdPersona"].Value.ToString());
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Registrar PersonaCliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return idPersona;
        }
        
        public bool RegistrarCliente(ECliente cliente)
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
                        cmd.CommandText = "RegistrarCliente";

                        cmd.Parameters.AddWithValue("@IdPersona", cliente.IdPersona);
                        cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);

                        respuesta = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Registrar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (respuesta == 1) return true;
            else return false;
        }

        public bool EditarPersonaCliente(ECliente cliente)
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

                        cmd.Parameters.AddWithValue("@IdPersona", cliente.IdPersona);
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@ApePaterno", cliente.ApePaterno);
                        cmd.Parameters.AddWithValue("@ApeMaterno", cliente.ApeMaterno);
                        cmd.Parameters.AddWithValue("@TipoDoc", cliente.TipoDoc);
                        cmd.Parameters.AddWithValue("@NumeroDoc", cliente.NumeroDoc);
                        cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);

                        respuesta = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar PersonaCliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (respuesta == 1) return true;
            else return false;
        }

        public bool EditarCliente(ECliente cliente)
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
                        cmd.CommandText = "EditarCliente";

                        cmd.Parameters.AddWithValue("@IdPersona", cliente.IdPersona);
                        cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);

                        respuesta = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (respuesta == 1) return true;
            else return false;
        }

        public bool EliminarPersonaCliente(int idPersona)
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
                    MessageBox.Show(e.Message, "SQL Error Eliminar PersonaCliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (respuesta == 1) return true;
            else return false;
        }

        public bool EliminarCliente(int idPersona)
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
                        cmd.CommandText = "EliminarCliente";

                        cmd.Parameters.AddWithValue("@IdPersona", idPersona);

                        respuesta = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Eliminar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (respuesta == 1) return true;
            else return false;
        }

        public List<ECliente> BuscarCliente(string numeroDoc)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<ECliente>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "BuscarCliente";

                        cmd.Parameters.AddWithValue("@NumeroDoc", numeroDoc);

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var cliente = new ECliente()
                            {
                                IdPersona = Convert.ToInt32(drd["IdPersona"]),
                                IdCliente = Convert.ToString(drd["IdCliente"]),
                                Nombre = Convert.ToString(drd["Nombre"]),
                                ApePaterno = Convert.ToString(drd["ApePaterno"]),
                                ApeMaterno = Convert.ToString(drd["ApeMaterno"]),
                                TipoDoc = Convert.ToString(drd["TipoDoc"]),
                                NumeroDoc = Convert.ToString(drd["NumeroDoc"]),
                                Direccion = Convert.ToString(drd["Direccion"]),
                                Telefono = Convert.ToString(drd["Telefono"]),
                                Email = Convert.ToString(drd["Email"])
                            };
                            lista.Add(cliente);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Buscar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
