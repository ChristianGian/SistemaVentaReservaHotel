using CapaNegocio.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Reportes
{
    public partial class FormReportComprobante : Form
    {
        //Propiedades
        public int IdPago { get; set; }
        public int IdReserva { get; set; }

        public FormReportComprobante()
        {
            InitializeComponent();
        }

        private void FormReportComprobante_Load(object sender, EventArgs e)
        {
            GetComprobante(IdPago);   
        }

        private void GetComprobante(int IdPago)
        {
            try
            {
                NComprobante comprobante = new NComprobante();
                var lista = comprobante.MostrarComprobante(IdPago);

                EComprobanteBindingSource.DataSource = lista;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error al obtener datos (Comprobante)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
