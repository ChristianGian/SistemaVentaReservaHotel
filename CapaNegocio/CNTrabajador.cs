using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CNTrabajador
    {
        private CDTrabajador cdTra = new CDTrabajador();
        public readonly StringBuilder builder = new StringBuilder();

        public List<ETrabajador> ListarTra()
        {
            return cdTra.ListarTrabajador();
        }

        public List<ETrabajador> BuscarTra(string numDoc)
        {
            return cdTra.BuscarTrabajador(numDoc);
        }

        public int RegistrarPersonaTra(ETrabajador trabajador)
        {
            if (ValidarTrabajador(trabajador))
            {
                return cdTra.RegistrarPersonaTrabajador(trabajador);
            }
            else
            {
                return 0;
            }
        }

        public bool RegistrarTra(ETrabajador trabajador)
        {
            return cdTra.RegistrarTrabajador(trabajador);
        }

        public bool EditarPersonaTra(ETrabajador trabajador)
        {
            bool res = false;

            if (ValidarTrabajador(trabajador)) res = cdTra.EditarPersonaTrabajador(trabajador);
            return res;
        }

        public bool EditarTra(ETrabajador trabajador)
        {
            bool res = cdTra.EditarTrabajador(trabajador);
            return res;
        }

        public bool EliminarPersonaTra(int idPersona)
        {
            return cdTra.EliminarPersonaTrabajador(idPersona);
        }

        public bool EliminarTra(int idPersona)
        {
            return cdTra.EliminarTrabajador(idPersona);
        }

        private bool ValidarTrabajador(ETrabajador trabajador)
        {
            builder.Clear();

            if (string.IsNullOrEmpty(trabajador.Nombre)) builder.Append("Ingrese el nombre");
            if (string.IsNullOrEmpty(trabajador.ApePaterno)) builder.Append("\nIngrese el apellido paterno");
            if (string.IsNullOrEmpty(trabajador.ApeMaterno)) builder.Append("\nIngrese el apellido materno");
            if (string.IsNullOrEmpty(trabajador.NumeroDoc)) builder.Append("\nIngrese el N° de documento");
            if (string.IsNullOrEmpty(trabajador.Direccion)) builder.Append("\nIngrese la dirección");
            if (string.IsNullOrEmpty(trabajador.Telefono)) builder.Append("\nIngrese el teléfono");
            if (string.IsNullOrEmpty(trabajador.Email)) builder.Append("\nIngrese el email");
            if (trabajador.Sueldo <= 0) builder.Append("\nIngrese un sueldo válido");
            if (string.IsNullOrEmpty(trabajador.Sesion)) builder.Append("\nIngrese el login");
            if (string.IsNullOrEmpty(trabajador.Contrasenia)) builder.Append("\nIngrese su password");

            return builder.Length == 0;
        }
    }
}
