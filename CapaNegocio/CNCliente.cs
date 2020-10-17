using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using Entidad;

namespace CapaNegocio
{
    public class CNCliente
    {
        private CDCliente cdCli = new CDCliente();
        public readonly StringBuilder builder = new StringBuilder();

        public List<ECliente> ListarCli()
        {
            return cdCli.Listar();
        }

        public List<ECliente> BuscarCli(string numeroDoc)
        {
            return cdCli.BuscarCliente(numeroDoc);
        }

        public int RegistrarPersonaCli(ECliente cliente)
        {
            if (Validar(cliente)) return cdCli.RegistrarPersonaCliente(cliente);
            else return 0;
        }

        public void RegistrarCli(ECliente cliente)
        {
            cdCli.RegistrarCliente(cliente);
        }

        public bool EditarPersonaCli(ECliente cliente)
        {
                bool res = cdCli.EditarPersonaCliente(cliente);
                return res;
        }

        public bool EditarCli(ECliente cliente)
        {
            bool res = cdCli.EditarCliente(cliente);
            return res;
        }

        public bool EliminarPersonaCli(int idPersona)
        {
            return cdCli.EliminarPersonaCliente(idPersona);
        }

        public bool EliminarCli(int idPersona)
        {
            return cdCli.EliminarCliente(idPersona);
        }

        private bool Validar(ECliente cliente)
        {
            builder.Clear();

            if (string.IsNullOrEmpty(cliente.Nombre)) builder.Append("Ingrese el nombre");
            if (string.IsNullOrEmpty(cliente.ApePaterno)) builder.Append("\nIngrese el apellido paterno");
            if (string.IsNullOrEmpty(cliente.ApeMaterno)) builder.Append("\nIngrese el apellido materno");
            if (string.IsNullOrEmpty(cliente.NumeroDoc)) builder.Append("\nIngrese el N° de documento");
            if (string.IsNullOrEmpty(cliente.Direccion)) builder.Append("\nIngrese la dirección");
            if (string.IsNullOrEmpty(cliente.Telefono)) builder.Append("\nIngrese el teléfono");
            if (string.IsNullOrEmpty(cliente.Email)) builder.Append("\nIngrese el email");
            if (string.IsNullOrEmpty(cliente.IdCliente)) builder.Append("\nIngrese el código");

            return builder.Length == 0;
        }
    }
}
