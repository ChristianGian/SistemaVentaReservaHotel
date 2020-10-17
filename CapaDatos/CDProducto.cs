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
    public class CDProducto
    {
        public List<EProducto> Listar()
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<EProducto>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ListarProducto";

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var producto = new EProducto()
                            {
                                IdProducto = Convert.ToInt32(drd["IdProducto"]),
                                Nombre = Convert.ToString(drd["Nombre"]),
                                Descripcion = Convert.ToString(drd["Descripcion"]),
                                UnidadMedida = Convert.ToString(drd["Unidad"]),
                                PrecioVenta = Convert.ToDecimal(drd["Precio"])
                            };
                            lista.Add(producto);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Error SQL Listar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return lista;
        }

        public void Registrar(EProducto producto)
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
                        cmd.CommandText = "RegistrarProducto";

                        cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        cmd.Parameters.AddWithValue("@Unidad", producto.UnidadMedida);
                        cmd.Parameters.AddWithValue("@Precio", producto.PrecioVenta);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Registrar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
        }

        public void Editar(EProducto producto)
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
                        cmd.CommandText = "EditarProducto";

                        cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                        cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        cmd.Parameters.AddWithValue("@Unidad", producto.UnidadMedida);
                        cmd.Parameters.AddWithValue("@Precio", producto.PrecioVenta);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Editar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
        }

        public void Eliminar(int idProducto)
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
                        cmd.CommandText = "EliminarProducto";

                        cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "SQL Error Eliminar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
        }

        public List<EProducto> Buscar(string nombre)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<EProducto>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "BuscarProductoPorNombre";

                        cmd.Parameters.AddWithValue("@Nombre", nombre);

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var producto = new EProducto()
                            {
                                IdProducto = Convert.ToInt32(drd["IdProducto"]),
                                Nombre = Convert.ToString(drd["Nombre"]),
                                Descripcion = Convert.ToString(drd["Descripcion"]),
                                UnidadMedida = Convert.ToString(drd["Unidad"]),
                                PrecioVenta = Convert.ToDecimal(drd["Precio"])
                            };
                            lista.Add(producto);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Error SQL Listar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
