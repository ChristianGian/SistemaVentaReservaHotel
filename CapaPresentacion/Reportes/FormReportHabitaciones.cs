using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Reportes
{
    public partial class FormReportHabitaciones : Form
    {
        public FormReportHabitaciones()
        {
            InitializeComponent();
        }

        private void FormReportHabitaciones_Load(object sender, EventArgs e)
        {
            GetHabitaciones();
        }

        private void GetHabitaciones()
        {
            try
            {
                CNHabitacion habitacion = new CNHabitacion();
                var lista = habitacion.ListarHab();

                EHabitacionBindingSource.DataSource = lista;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error al obtener datos (Habitaciones)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.reportViewer1.RefreshReport();
            }
            
        }
    }
}
