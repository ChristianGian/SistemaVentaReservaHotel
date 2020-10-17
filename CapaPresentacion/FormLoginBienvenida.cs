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
    public partial class FormLoginBienvenida : Form
    {
        public FormLoginBienvenida()
        {
            InitializeComponent();
        }

        //Nos permitira controlar el tiempo
        int contador = 0;

        private void TimerAparecer_Tick(object sender, EventArgs e)
        {
            //Hacemos que el formulario pararezca de forma gradual
            //Opacity trabaja con porcetnajes
            if (this.Opacity < 1) this.Opacity += 0.05;
            contador++;

            //SI el contador llega a los 100, es decir 3mil milisegundos (3 seg)
            if (contador == 100)
            {
                timerAparecer.Stop();
                timerDesvanecer.Start();
            }
        }

        private void TimerDesvanecer_Tick(object sender, EventArgs e)
        {
            //Haremos que el formulario se desvanesca gradualmente
            this.Opacity -= 0.1;
            if (this.Opacity == 0)
            {
                timerDesvanecer.Stop();
                this.Close();
            }
        }

        private void FormLoginBienvenida_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"{UserLoginCache.Nombre}, {UserLoginCache.ApePaterno} {UserLoginCache.ApeMaterno}";

            //Inicializamos la opacidad en 0
            this.Opacity = 0.0;
            timerAparecer.Start();
        }
    }
}
