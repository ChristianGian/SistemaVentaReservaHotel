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
    public class CDHabitacion
    {
        public List<EHabitacion> Listar()
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<EHabitacion>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ListarHabitacion";
                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var habitacion = new EHabitacion()
                            {
                                IdHabitacion = Convert.ToInt32(drd["IdHabitacion"]),
                                Numero = Convert.ToString(drd["Numero"]),
                                Piso = Convert.ToString(drd["Piso"]),
                                Descripcion = Convert.ToString(drd["Descripcion"]),
                                Caracteristicas = Convert.ToString(drd["Caracteristicas"]),
                                PrecioDiario = Convert.ToDecimal(drd["PrecioDiario"]),
                                Estado = Convert.ToString(drd["Estado"]),
                                TipoHabitacion = Convert.ToString(drd["TipoHabitacion"])
                            };
                            lista.Add(habitacion);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Listar habitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return lista;
        }

        public List<EHabitacion> ListarHabDisp()
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<EHabitacion>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ListarHabDisponibles";
                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var habitacion = new EHabitacion()
                            {
                                IdHabitacion = Convert.ToInt32(drd["IdHabitacion"]),
                                Numero = Convert.ToString(drd["Numero"]),
                                Piso = Convert.ToString(drd["Piso"]),
                                Descripcion = Convert.ToString(drd["Descripcion"]),
                                Caracteristicas = Convert.ToString(drd["Caracteristicas"]),
                                PrecioDiario = Convert.ToDecimal(drd["PrecioDiario"]),
                                Estado = Convert.ToString(drd["Estado"]),
                                TipoHabitacion = Convert.ToString(drd["TipoHabitacion"])
                            };
                            lista.Add(habitacion);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Listar Habitación Disponible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return lista;
        }

        public void Registrar(EHabitacion habitacion)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "RegistrarHabitacion";

                        cmd.Parameters.AddWithValue("@Numero", habitacion.Numero);
                        cmd.Parameters.AddWithValue("@Piso", habitacion.Piso);
                        cmd.Parameters.AddWithValue("@Descripcion", habitacion.Descripcion);
                        cmd.Parameters.AddWithValue("@Caracteristicas", habitacion.Caracteristicas);
                        cmd.Parameters.AddWithValue("@PrecioDiario", habitacion.PrecioDiario);
                        cmd.Parameters.AddWithValue("@Estado", habitacion.Estado);
                        cmd.Parameters.AddWithValue("@TipoHabitacion", habitacion.TipoHabitacion);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Registrar habitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
        }

        public void Editar(EHabitacion habitacion)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "EditarHabitacion";

                        cmd.Parameters.AddWithValue("@IdHabitacion", habitacion.IdHabitacion);
                        cmd.Parameters.AddWithValue("@Numero", habitacion.Numero);
                        cmd.Parameters.AddWithValue("@Piso", habitacion.Piso);
                        cmd.Parameters.AddWithValue("@Descripcion", habitacion.Descripcion);
                        cmd.Parameters.AddWithValue("@Caracteristicas", habitacion.Caracteristicas);
                        cmd.Parameters.AddWithValue("@PrecioDiario", habitacion.PrecioDiario);
                        cmd.Parameters.AddWithValue("@Estado", habitacion.Estado);
                        cmd.Parameters.AddWithValue("@TipoHabitacion", habitacion.TipoHabitacion);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar habitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
        }

        //Desocupar la habitación
        public void Desocupar(int idHabitacion)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "DesocuparHabitacion";

                        cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Desocupar Habitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
        }

        //Ocupar la habitación
        public void Ocupar(int idHabitacion)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "OcuparHabitacion";

                        cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Ocupar Habitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
        }

        public void Eliminar(int idHabitacion)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "EliminarHabitacion";

                        cmd.Parameters.AddWithValue("@IdHabitacion", idHabitacion);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Eliminar habitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
        }

        public List<EHabitacion> Buscar(string numero)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<EHabitacion>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ListarHabPorNum";

                        cmd.Parameters.AddWithValue("@Numero", numero);

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var habitacion = new EHabitacion()
                            {
                                IdHabitacion = Convert.ToInt32(drd["IdHabitacion"]),
                                Numero = Convert.ToString(drd["Numero"]),
                                Piso = Convert.ToString(drd["Piso"]),
                                Descripcion = Convert.ToString(drd["Descripcion"]),
                                Caracteristicas = Convert.ToString(drd["Caracteristicas"]),
                                PrecioDiario = Convert.ToDecimal(drd["PrecioDiario"]),
                                Estado = Convert.ToString(drd["Estado"]),
                                TipoHabitacion = Convert.ToString(drd["TipoHabitacion"])
                            };
                            lista.Add(habitacion);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Buscar habitación", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
