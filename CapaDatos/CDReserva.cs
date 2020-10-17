using Entidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CapaDatos
{
    public class CDReserva
    {
        public List<EReserva> Listar()
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<EReserva>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ListarReserva";

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var reserva = new EReserva()
                            {
                                IdReserva = drd.GetInt32(drd.GetOrdinal("IdReserva")),
                                IdHabitacion = drd.GetInt32(drd.GetOrdinal("IdHabitacion")),
                                Numero = drd.GetString(drd.GetOrdinal("Numero")),
                                IdCliente = drd.GetInt32(drd.GetOrdinal("IdCliente")),
                                NomCompCliente = drd.GetString(drd.GetOrdinal("NomCompCliente")),
                                IdTrabajador = drd.GetInt32(drd.GetOrdinal("IdTrabajador")),
                                NomCompTrabajador = drd.GetString(drd.GetOrdinal("NomCompTrabajador")),
                                TipoReserva = drd.GetString(drd.GetOrdinal("TipoReserva")),
                                FechaReserva = drd.GetDateTime(drd.GetOrdinal("FechaReserva")),
                                FechaIngreso = drd.GetDateTime(drd.GetOrdinal("FechaIngreso")),
                                FechaSalida = drd.GetDateTime(drd.GetOrdinal("FechaSalida")),
                                CostoAlojamiento = drd.GetDecimal(drd.GetOrdinal("CostoAlojamiento")),
                                Estado = drd.GetString(drd.GetOrdinal("Estado"))
                            };
                            lista.Add(reserva);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Error SQL Listar Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return lista;
        }

        public bool Registrar(EReserva reserva)
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
                        cmd.CommandText = "RegistrarReserva";

                        cmd.Parameters.AddWithValue("@IdHabitacion", reserva.IdHabitacion);
                        cmd.Parameters.AddWithValue("@IdCliente", reserva.IdCliente);
                        cmd.Parameters.AddWithValue("@IdTrabajador", reserva.IdTrabajador);
                        cmd.Parameters.AddWithValue("@TipoReserva", reserva.TipoReserva);
                        cmd.Parameters.AddWithValue("@FechaReserva", reserva.FechaReserva);
                        cmd.Parameters.AddWithValue("@FechaIngreso", reserva.FechaIngreso);
                        cmd.Parameters.AddWithValue("@FechaSalida", reserva.FechaSalida);
                        cmd.Parameters.AddWithValue("@CostoAlojamiento", reserva.CostoAlojamiento);
                        cmd.Parameters.AddWithValue("@Estado", reserva.Estado);

                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Registrar Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (res == 1) return true;
            else return false;
        }

        public bool Editar(EReserva reserva)
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
                        cmd.CommandText = "EditarReserva";

                        cmd.Parameters.AddWithValue("@IdReserva", reserva.IdReserva);
                        cmd.Parameters.AddWithValue("@IdHabitacion", reserva.IdHabitacion);
                        cmd.Parameters.AddWithValue("@IdCliente", reserva.IdCliente);
                        cmd.Parameters.AddWithValue("@IdTrabajador", reserva.IdTrabajador);
                        cmd.Parameters.AddWithValue("@TipoReserva", reserva.TipoReserva);
                        cmd.Parameters.AddWithValue("@FechaReserva", reserva.FechaReserva);
                        cmd.Parameters.AddWithValue("@FechaIngreso", reserva.FechaIngreso);
                        cmd.Parameters.AddWithValue("@FechaSalida", reserva.FechaSalida);
                        cmd.Parameters.AddWithValue("@CostoAlojamiento", reserva.CostoAlojamiento);
                        cmd.Parameters.AddWithValue("@Estado", reserva.Estado);

                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (res == 1) return true;
            else return false;
        }

        public void EditarEstado(int idReserva)
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
                        cmd.CommandText = "EditarEstadoReserva";

                        cmd.Parameters.AddWithValue("@IdReserva", idReserva);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar Estado Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
        }

        public bool Eliminar(int idReserva)
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
                        cmd.CommandText = "EliminarReserva";

                        cmd.Parameters.AddWithValue("@IdReserva", idReserva);


                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Eliminar Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (res == 1) return true;
            else return false;
        }

        public List<EReserva> Buscar(DateTime fechaReserva)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<EReserva>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "BuscarReserva";

                        cmd.Parameters.AddWithValue("@FechaReserva", fechaReserva);

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var reserva = new EReserva()
                            {
                                IdReserva = drd.GetInt32(drd.GetOrdinal("IdReserva")),
                                IdHabitacion = drd.GetInt32(drd.GetOrdinal("IdHabitacion")),
                                Numero = drd.GetString(drd.GetOrdinal("Numero")),
                                IdCliente = drd.GetInt32(drd.GetOrdinal("IdCliente")),
                                NomCompCliente = drd.GetString(drd.GetOrdinal("NomCompCliente")),
                                IdTrabajador = drd.GetInt32(drd.GetOrdinal("IdTrabajador")),
                                NomCompTrabajador = drd.GetString(drd.GetOrdinal("NomCompTrabajador")),
                                TipoReserva = drd.GetString(drd.GetOrdinal("TipoReserva")),
                                FechaReserva = drd.GetDateTime(drd.GetOrdinal("FechaReserva")),
                                FechaIngreso = drd.GetDateTime(drd.GetOrdinal("FechaIngreso")),
                                FechaSalida = drd.GetDateTime(drd.GetOrdinal("FechaSalida")),
                                CostoAlojamiento = drd.GetDecimal(drd.GetOrdinal("CostoAlojamiento")),
                                Estado = drd.GetString(drd.GetOrdinal("Estado"))
                            };
                            lista.Add(reserva);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Error SQL Buscar Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
