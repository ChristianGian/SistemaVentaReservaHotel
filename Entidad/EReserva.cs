using System;

namespace Entidad
{
    public class EReserva
    {
        public int IdReserva { get; set; }
        public int IdHabitacion { get; set; }
        public string Numero { get; set; }
        public int IdCliente { get; set; }
        public string NomCompCliente { get; set; }
        public int IdTrabajador { get; set; }
        public string NomCompTrabajador { get; set; }
        public string TipoReserva { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }
        public decimal CostoAlojamiento { get; set; }
        public string Estado { get; set; }
    }
}
