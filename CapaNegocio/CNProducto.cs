using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CNProducto
    {
        private CDProducto cdPro = new CDProducto();
        public readonly StringBuilder builder = new StringBuilder();

        public List<EProducto> ListarPro()
        {
            return cdPro.Listar();
        }

        public List<EProducto> BuscarPro(string nombre)
        {
            return cdPro.Buscar(nombre);
        }

        public void RegistrarPro(EProducto producto)
        {
            if (Validar(producto)) cdPro.Registrar(producto);
        }

        public void EditarPro(EProducto producto)
        {
            if (Validar(producto)) cdPro.Editar(producto);
        }

        public void EliminarPro(int idProducto)
        {
            cdPro.Eliminar(idProducto);
        }

        private bool Validar(EProducto producto)
        {
            builder.Clear();

            if (string.IsNullOrEmpty(producto.Nombre)) builder.Append("Inserte el nombre del producto");
            if (producto.PrecioVenta <= 0) builder.Append("\nIngrese un precio de venta válido");

            return builder.Length == 0;
        }
    }
}
