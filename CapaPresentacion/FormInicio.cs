using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad.Cache;

namespace CapaPresentacion
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void FormInicio_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            CargarDatosUsuario();

            PermisosDeAdministrador();
        }

        //Deshabilitamos botones (o controles) segun el perfil de usuario (Acceso)
        private void PermisosDeAdministrador()
        {
            if (UserLoginCache.Acceso == Acceso.Digitador)
            {
                toolStripArchivo.Enabled = false;
                toolStripConfiguraciones.Enabled = false;
            }

            if (UserLoginCache.Acceso == Acceso.Administrador)
            {
                //Código
            }
        }

        private void CargarDatosUsuario()
        {
            lblIdPersona.Text = UserLoginCache.IdPersona.ToString();
            lblNombre.Text = UserLoginCache.Nombre;
            lblAPaterno.Text = UserLoginCache.ApePaterno;
            lblAMaterno.Text = UserLoginCache.ApeMaterno;
            lblAcceso.Text = UserLoginCache.Acceso;
        }

        private void HabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormHabitacion habitacion = new FormHabitacion();

            //habitacion.MdiParent = this;
            //habitacion.Show();
            AbrirFormHijo(new FormHabitacion());
        }

        private void ProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new FormProducto());
        }

        private void ClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new FormCliente());
        }

        private void UsuariosYAccesosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new FormTrabajador());
        }

        private void ToolStripSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar sesión?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)== DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ReservasYConsumosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new FormReserva());
        }


        /*ABRIR FORM HIJO*/
        /****************************************************************************/
        private Form formActivo = null;

        private void AbrirFormHijo(Form formHijo)
        {
            if (formActivo != null) formActivo.Close();

            formActivo = formHijo;
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;

            panelFormHijo.Controls.Add(formHijo);
            panelFormHijo.Tag = formHijo;

            formHijo.BringToFront();
            formHijo.Show();
        }
    }
}
