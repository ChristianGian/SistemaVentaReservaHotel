using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class EHabitacion
    {
        public int IdHabitacion { get; set; }
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Descripcion { get; set; }
        public string Caracteristicas { get; set; }
        public decimal PrecioDiario { get; set; }
        public string Estado { get; set; }
        public string TipoHabitacion { get; set; }
    }
}
