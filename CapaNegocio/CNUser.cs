using CapaDatos;
using Entidad.Cache;

namespace CapaNegocio
{
    public class CNUser
    {
        private CDUser cdUser = new CDUser();

        public bool LoginTrabajador(string user, string pass)
        {
            return cdUser.Login(user, pass);
        }

        public void AlgunMetodo()
        {
            //Seguridad y permisos
            if (UserLoginCache.Acceso == Acceso.Administrador)
            {
                //código
            }
            if (UserLoginCache.Acceso == Acceso.Digitador)
            {
                //código
            }
        }
    }
}
