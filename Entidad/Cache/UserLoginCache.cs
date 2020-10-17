using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Cache
{
    public static class UserLoginCache
    {
        public static int IdPersona { get; set; }
        public static string Nombre { get; set; }
        public static string ApePaterno { get; set; }
        public static string ApeMaterno { get; set; }
        public static string Acceso { get; set; }
        public static string Estado { get; set; }
    }
}
