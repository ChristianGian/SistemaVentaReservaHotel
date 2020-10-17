using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Reportes
{
    public class EComprobante
    {
        public string Cliente { get; set; }
        public string NumDocumento { get; set; }
        public string Direccion { get; set; }
        public string TipoComprobante { get; set; }
        public string NumComprobante { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}
