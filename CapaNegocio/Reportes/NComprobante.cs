using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad.Reportes;
using CapaDatos.Reportes;

namespace CapaNegocio.Reportes
{
    public class NComprobante
    {
        private DComprobante comprobante = new DComprobante();

        public List<EComprobante> MostrarComprobante(int idPago)
        {
            return comprobante.Mostrar(idPago);
        }
    }
}
