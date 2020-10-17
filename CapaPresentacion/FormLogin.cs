using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CapaNegocio;
using Entidad.Cache;

namespace CapaPresentacion
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        //Permitir mover el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void TxtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "USUARIO")
            {
                txtUser.Clear();
                txtUser.ForeColor = Color.LightGray;
            }
        }

        private void TxtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "USUARIO";
                txtUser.ForeColor = Color.DimGray;
            }
        }

        private void TxtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "CONTRASEÑA")
            {
                txtPass.Clear();
                txtPass.ForeColor = Color.LightGray;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void TxtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "CONTRASEÑA";
                txtPass.ForeColor = Color.DimGray;
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #region Mover Form
        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void PictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "USUARIO")
            {
                if (txtPass.Text != "CONTRASEÑA")
                {
                    //Comprobamos si el usuario y contraseña son correctos
                    CNUser cnUser = new CNUser();
                    bool validarLogin = cnUser.LoginTrabajador(txtUser.Text, txtPass.Text);

                    if (validarLogin)
                    {
                        if (UserLoginCache.Estado == "A")
                        {
                            FormInicio principal = new FormInicio();

                            //Mostramos un saludo opcional
                            //MessageBox.Show($"¡Bienvenido!, {UserLoginCache.Nombre} {UserLoginCache.ApePaterno}");
                            this.Hide();
                            FormLoginBienvenida bienvenida = new FormLoginBienvenida();
                            bienvenida.ShowDialog();

                            principal.Show();
                            //02 Sobrecargamos el evento FormClose del formulario principal con el método de CerrarSesion
                            principal.FormClosed += CerrarSesion;
                            //Ocultamos el formulario login
                            //this.Hide();
                        }
                        else
                        {
                            MensajeError("Usuario deshabilitado.\n     Póngase en contacto con el administrador.");
                        }
                    }
                    else
                    {
                        MensajeError("Usuario o contraseña incorrecta.\n     Por favor intente otra vez.");
                        txtPass.Clear();
                        txtUser.Focus();
                    }
                }
                else
                {
                    MensajeError("Por favor ingrese su contraseña");
                }
            }
            else
            {
                MensajeError("Por favor ingrese su usuario");
            }
        }

        private void MensajeError(string msj)
        {
            lblMensajeError.Text = "     " + msj;
            lblMensajeError.Visible = true;
        }


        //Cerrar sesion actual
        //Parametros: Un objeto y un evento de Cerrar formulario
        private void CerrarSesion(object sender, FormClosedEventArgs e)
        {
            txtUser.Text = "USUARIO";
            txtPass.Text = "CONTRASEÑA";
            txtPass.UseSystemPasswordChar = false;

            lblMensajeError.Visible = false;
            this.Show();
            //txtUser.Focus();
        }
    }
}
