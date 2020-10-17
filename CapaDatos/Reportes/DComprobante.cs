using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Entidad.Reportes;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CapaDatos.Reportes
{
    public class DComprobante
    {
        public List<EComprobante> Mostrar(int idPago)
        {
            var cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            var lista = new List<EComprobante>();

            using (var cn = new SqlConnection(cadena))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ReporteComprobante";

                        cmd.Parameters.AddWithValue("@IdPago", idPago);

                        var drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            var enti = new EComprobante()
                            {
                                Cliente = drd.GetString(drd.GetOrdinal("Cliente")),
                                NumDocumento = drd.GetString(drd.GetOrdinal("NumeroDoc")),
                                Direccion = drd.GetString(drd.GetOrdinal("Direccion")),
                                TipoComprobante = drd.GetString(drd.GetOrdinal("TipoComprobante")),
                                NumComprobante = drd.GetString(drd.GetOrdinal("NumComprobante")),
                                FechaEmision = drd.GetDateTime(drd.GetOrdinal("FechaEmision")),
                                Descripcion = drd.GetString(drd.GetOrdinal("Descripcion")),
                                Precio = drd.GetDecimal(drd.GetOrdinal("Precio")),
                                Cantidad = drd.GetDecimal(drd.GetOrdinal("Cantidad")),
                                Total = drd.GetDecimal(drd.GetOrdinal("TotalPago"))
                            };
                            lista.Add(enti);
                        }
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "Error al mostrar datos (Comprobante)", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
