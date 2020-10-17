using CapaDatos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio
{
    public class CNReserva
    {
        private CDReserva cdRva = new CDReserva();
        public readonly StringBuilder builder = new StringBuilder();

        public List<EReserva> ListarRva()
        {
            return cdRva.Listar();
        }

        public List<EReserva> BuscarRva(DateTime fechaReserva)
        {
            return cdRva.Buscar(fechaReserva);
        }
        public bool RegistrarRva(EReserva reserva)
        {
            bool res = false;
            if (Validar(reserva)) res = cdRva.Registrar(reserva);

            return res;
        }

        public bool EditarRva(EReserva reserva)
        {
            bool res = false;
            if (Validar(reserva)) res = cdRva.Editar(reserva);

            return res;
        }

        public void EditarEstadoRva(int idReserva)
        {
            cdRva.EditarEstado(idReserva);
        }

        public bool EliminarRva(int idReserva)
        {
            return cdRva.Eliminar(idReserva);
        }

        private bool Validar(EReserva reserva)
        {
            builder.Clear();

            if (reserva.CostoAlojamiento <= 0) builder.Append("Ingrese un costo válido");

            return builder.Length == 0;
        }
    }
}
