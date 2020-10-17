using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ETrabajador: EPersona
    {
        public decimal Sueldo { get; set; }
        public string Acceso { get; set; }
        public string Sesion { get; set; }
        public string Contrasenia { get; set; }
        public string Estado { get; set; }
    }
}
