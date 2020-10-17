using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Entidad.Cache;

namespace CapaDatos
{
    public class CDUser
    {
        public bool Login(string user, string pass)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            bool res = false;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LoginTrabajador";

                        cmd.Parameters.AddWithValue("@Sesion", user);
                        cmd.Parameters.AddWithValue("@Contrasenia", pass);

                        var drd = cmd.ExecuteReader();

                        //01
                        //if (drd.HasRows) res = true;
                        //else res = false;

                        //02 Capturamos los datos del trabajador para el FormInicio Datos estaticos
                        if (drd.HasRows)
                        {
                            while (drd.Read())
                            {
                                UserLoginCache.IdPersona = drd.GetInt32(drd.GetOrdinal("IdPersona"));
                                UserLoginCache.Nombre = drd.GetString(drd.GetOrdinal("Nombre"));
                                UserLoginCache.ApePaterno = drd.GetString(drd.GetOrdinal("ApePaterno"));
                                UserLoginCache.ApeMaterno = drd.GetString(drd.GetOrdinal("ApeMaterno"));
                                UserLoginCache.Acceso = drd.GetString(drd.GetOrdinal("Acceso"));
                                UserLoginCache.Estado = drd.GetString(drd.GetOrdinal("Estado"));
                            }

                            res = true;
                        }else
                        { 
                            res = false; 
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Error SQL Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return res;
        }

        //Seguridad según tipo de Acceso - Administrador Digitador
        public void AlgunMetodo()
        {
            if (UserLoginCache.Acceso == Acceso.Administrador)
            {
                //código
            }
            if (UserLoginCache.Acceso == Acceso.Digitador)
            {
                //código
            }
        }
    }
}
