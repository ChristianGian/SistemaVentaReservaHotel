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
    public class CDPago
    {
        public List<Epago> Listar(int idReserva)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<Epago>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ListarPago";

                        cmd.Parameters.AddWithValue("@IdReserva", idReserva);

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var pago = new Epago()
                            {
                                IdPago = drd.GetInt32(drd.GetOrdinal("IdPago")),
                                IdReserva = drd.GetInt32(drd.GetOrdinal("IdReserva")),
                                TipoComprobante = drd.GetString(drd.GetOrdinal("TipoComprobante")),
                                NumComprobante = drd.GetString(drd.GetOrdinal("NumComprobante")),
                                Igv = drd.GetDecimal(drd.GetOrdinal("Igv")),
                                TotalPago = drd.GetDecimal(drd.GetOrdinal("TotalPago")),
                                FechaEmision = drd.GetDateTime(drd.GetOrdinal("FechaEmision")),
                                FechaPago = drd.GetDateTime(drd.GetOrdinal("FechaPago"))
                            };
                            lista.Add(pago);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Listar Pago", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return lista;
        }

        public bool Registrar(Epago pago)
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
                        cmd.CommandText = "RegistrarPago";

                        cmd.Parameters.AddWithValue("@IdReserva", pago.IdReserva);
                        cmd.Parameters.AddWithValue("@TipoComprobante", pago.TipoComprobante);
                        cmd.Parameters.AddWithValue("@NumComprobante", pago.NumComprobante);
                        cmd.Parameters.AddWithValue("@Igv", pago.Igv);
                        cmd.Parameters.AddWithValue("@TotalPago", pago.TotalPago);
                        cmd.Parameters.AddWithValue("@FechaEmision", pago.FechaEmision);
                        cmd.Parameters.AddWithValue("@FechaPago", pago.FechaPago);

                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Registrar Pago", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (res == 1) return true;
            else return false;
        }

        public bool Editar(Epago pago)
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
                        cmd.CommandText = "EditarPago";

                        cmd.Parameters.AddWithValue("@IdPago", pago.IdPago);
                        cmd.Parameters.AddWithValue("@IdReserva", pago.IdReserva);
                        cmd.Parameters.AddWithValue("@TipoComprobante", pago.TipoComprobante);
                        cmd.Parameters.AddWithValue("@NumComprobante", pago.NumComprobante);
                        cmd.Parameters.AddWithValue("@Igv", pago.Igv);
                        cmd.Parameters.AddWithValue("@TotalPago", pago.TotalPago);
                        cmd.Parameters.AddWithValue("@FechaEmision", pago.FechaEmision);
                        cmd.Parameters.AddWithValue("@FechaPago", pago.FechaPago);

                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar Pago", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (res == 1) return true;
            else return false;
        }

        public bool Eliminar(int idPago)
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
                        cmd.CommandText = "EliminarPago";

                        cmd.Parameters.AddWithValue("@IdPago", idPago);

                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Eliminar Pago", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (res == 1) return true;
            else return false;
        }
    }
}
