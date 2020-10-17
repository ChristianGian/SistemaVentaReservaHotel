using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using Entidad;

namespace CapaNegocio
{
    public class CNPago
    {
        private CDPago cdPag = new CDPago();
        public readonly StringBuilder builder = new StringBuilder();

        public List<Epago> ListarP(int idReserva)
        {
            return cdPag.Listar(idReserva);
        }

        public bool RegistrarP(Epago pago)
        {
            bool res = false;

            if (Validar(pago)) res = cdPag.Registrar(pago);
            return res;
        }

        public bool EditarP(Epago pago)
        {
            bool res = false;

            if (Validar(pago)) res = cdPag.Editar(pago);
            return res;
        }

        public bool Eliminar(int idPago)
        {
            return cdPag.Eliminar(idPago);
        }

        private bool Validar(Epago pago)
        {
            builder.Clear();

            if (string.IsNullOrEmpty(pago.NumComprobante)) builder.Append("Ingrese el número del comprobando");
            if (pago.Igv <= 0) builder.Append("\nIngrese el IGV válido");

            return builder.Length == 0;
        }
    }
}
