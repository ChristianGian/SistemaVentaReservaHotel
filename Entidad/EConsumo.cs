using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class EConsumo
    {
        public int IdConsumo { get; set; }
        public int IdReserva { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Estado { get; set; }
    }
}
