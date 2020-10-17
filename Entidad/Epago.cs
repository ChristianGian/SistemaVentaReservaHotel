using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Epago
    {
        public int IdPago { get; set; }
        public int IdReserva { get; set; }
        public string TipoComprobante { get; set; }
        public string NumComprobante { get; set; }
        public decimal Igv { get; set; }
        public decimal TotalPago { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
