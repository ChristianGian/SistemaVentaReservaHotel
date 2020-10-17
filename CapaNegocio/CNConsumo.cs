using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using Entidad;

namespace CapaNegocio
{
    public  class CNConsumo
    {
        private CDConsumo cdCon = new CDConsumo();
        public readonly StringBuilder builder = new StringBuilder();

        public List<EConsumo> ListarCon(decimal idReserva)
        {
            return cdCon.Listar(idReserva);
        }

        public bool RegistrarCon(EConsumo consumo)
        {
            bool res = false;
            if (Validar(consumo)) res = cdCon.Registrar(consumo);

            return res;
        }

        public bool EliminarCon(int idConsumo)
        {
            return cdCon.Eliminar(idConsumo);
        }

        private bool Validar(EConsumo consumo)
        {
            builder.Clear();

            if (string.IsNullOrEmpty(consumo.IdReserva.ToString())) builder.Append("No se ha podido capturar el IdReserva");
            if (string.IsNullOrEmpty(consumo.IdProducto.ToString())) builder.Append("\nNo se ha podido capturar el IdProducto");
            if (consumo.Cantidad <= 0) builder.Append("\nIngrese una cantidad válida");
            if (consumo.PrecioVenta <= 0) builder.Append("\nIngrese un precio válido");

            return builder.Length == 0;
        }
    }
}
