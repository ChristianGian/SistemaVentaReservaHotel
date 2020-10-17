using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CapaDatos
{
    public class CDConsumo
    {
        public List<EConsumo> Listar(decimal idReserva)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<EConsumo>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ListarConsumo";
                        cmd.Parameters.AddWithValue("@IdReserva", idReserva);

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var consumo = new EConsumo()
                            {
                                IdConsumo = drd.GetInt32(drd.GetOrdinal("IdConsumo")),
                                IdReserva = drd.GetInt32(drd.GetOrdinal("IdReserva")),
                                IdProducto = drd.GetInt32(drd.GetOrdinal("IdProducto")),
                                NombreProducto = drd.GetString(drd.GetOrdinal("Nombre")),
                                Cantidad = drd.GetDecimal(drd.GetOrdinal("Cantidad")),
                                PrecioVenta = drd.GetDecimal(drd.GetOrdinal("Precio")),
                                Estado = drd.GetString(drd.GetOrdinal("Estado"))
                            };
                            lista.Add(consumo);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Listar Consumo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return lista;
        }

        public bool Registrar(EConsumo consumo)
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
                        cmd.CommandText = "RegistrarConsumo";

                        cmd.Parameters.AddWithValue("@IdReserva", consumo.IdReserva);
                        cmd.Parameters.AddWithValue("@IdProducto", consumo.IdProducto);
                        cmd.Parameters.AddWithValue("@Cantidad", consumo.Cantidad);
                        cmd.Parameters.AddWithValue("@PrecioVenta", consumo.PrecioVenta);
                        cmd.Parameters.AddWithValue("@Estado", consumo.Estado);

                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Registrar Consumo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (res == 1) return true;
            else return false;
        }

        public bool Editar(EConsumo consumo)
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
                        cmd.CommandText = "EditarConsumo";

                        cmd.Parameters.AddWithValue("@IdConsumo", consumo.IdConsumo);
                        cmd.Parameters.AddWithValue("@IdReserva", consumo.IdReserva);
                        cmd.Parameters.AddWithValue("@IdProducto", consumo.IdProducto);
                        cmd.Parameters.AddWithValue("@Cantidad", consumo.IdConsumo);
                        cmd.Parameters.AddWithValue("@PrecioVenta", consumo.PrecioVenta);
                        cmd.Parameters.AddWithValue("@Estado", consumo.Estado);

                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar Consumo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            if (res == 1) return true;
            else return false;
        }

        public bool Eliminar(int idConsumo)
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
                        cmd.CommandText = "EliminarConsumo";

                        cmd.Parameters.AddWithValue("@IdConsumo", idConsumo);

                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Eliminar Consumo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
