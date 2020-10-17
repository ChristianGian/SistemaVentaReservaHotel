using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using Entidad;

namespace CapaNegocio
{
    public class CNHabitacion
    {
        private CDHabitacion cdHab = new CDHabitacion();
        public readonly StringBuilder builder = new StringBuilder();

        public List<EHabitacion> ListarHab()
        {
            return cdHab.Listar();
        }

        public List<EHabitacion> ListarHabDisponible()
        {
            return cdHab.ListarHabDisp();
        }

        public void RegistrarHab(EHabitacion habitacion)
        {
            if (ValidarHabitacion(habitacion)) cdHab.Registrar(habitacion);
        }

        public void EditarHab(EHabitacion habitacion)
        {
            if (ValidarHabitacion(habitacion)) cdHab.Editar(habitacion);
        }

        public void DesocuparHab(int idHabitacion)
        {
            cdHab.Desocupar(idHabitacion);
        }

        public void OcuparHab(int idHabitacion)
        {
            cdHab.Ocupar(idHabitacion);
        }

        public void EliminarHab(int idHabitacion)
        {
            builder.Clear();

            if (idHabitacion == 0) builder.Append("Por favor proporcione un Id válido");
            if (builder.Length == 0) cdHab.Eliminar(idHabitacion);
        }

        public List<EHabitacion> BuscarHab(string numero)
        {
            return cdHab.Buscar(numero);
        }

        private bool ValidarHabitacion(EHabitacion habitacion)
        {
            builder.Clear();

            if (string.IsNullOrEmpty(habitacion.Numero)) builder.Append("Ingrese el Número de Habitación");
            if (habitacion.PrecioDiario <= 0) builder.Append("\nIngrese un Precio Diario válido");

            return builder.Length == 0;
        }
    }
}
