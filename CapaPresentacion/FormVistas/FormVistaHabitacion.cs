using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using Entidad;

namespace CapaPresentacion.FormVistas
{
    public partial class FormVistaHabitacion : Form
    {
        private readonly CNHabitacion cnHab = new CNHabitacion();
        private string idHabitacion;
        private string numero;

        public string IdHabitacion { get => idHabitacion; }
        public string Numero { get => numero; }

        public FormVistaHabitacion()
        {
            InitializeComponent();
        }

        private void FormVistaHabitacion_Load(object sender, EventArgs e)
        {
            ListarHabitacionDisponible();
        }

        private void ListarHabitacionDisponible()
        {
            var habitacion = cnHab.ListarHabDisponible();

            int numHabitaciones = habitacion.Count;
            lblRegistros.Text = numHabitaciones.ToString();

            if (numHabitaciones > 0)
            {
                dgvHabitaciones.AutoGenerateColumns = false;
                dgvHabitaciones.DataSource = habitacion;

                dgvHabitaciones.Columns[0].DataPropertyName = "IdHabitacion";
                dgvHabitaciones.Columns[1].DataPropertyName = "Numero";
                dgvHabitaciones.Columns[2].DataPropertyName = "Piso";
                dgvHabitaciones.Columns[3].DataPropertyName = "Descripcion";
                dgvHabitaciones.Columns[4].DataPropertyName = "Caracteristicas";
                dgvHabitaciones.Columns[5].DataPropertyName = "PrecioDiario";
                dgvHabitaciones.Columns[6].DataPropertyName = "Estado";
                dgvHabitaciones.Columns[7].DataPropertyName = "TipoHabitacion";
            }
            else
            {
                MessageBox.Show("No existen habitaciones registradas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            string numero = txtBuscar.Text;
            var habitacion = cnHab.BuscarHab(numero);

            dgvHabitaciones.AutoGenerateColumns = false;
            dgvHabitaciones.DataSource = habitacion;

            dgvHabitaciones.Columns[0].DataPropertyName = "IdHabitacion";
            dgvHabitaciones.Columns[1].DataPropertyName = "Numero";
            dgvHabitaciones.Columns[2].DataPropertyName = "Piso";
            dgvHabitaciones.Columns[3].DataPropertyName = "Descripcion";
            dgvHabitaciones.Columns[4].DataPropertyName = "Caracteristicas";
            dgvHabitaciones.Columns[5].DataPropertyName = "PrecioDiario";
            dgvHabitaciones.Columns[6].DataPropertyName = "Estado";
            dgvHabitaciones.Columns[7].DataPropertyName = "TipoHabitacion";
        }

        private void DgvHabitaciones_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            idHabitacion = dgvHabitaciones.CurrentRow.Cells[0].Value.ToString();
            numero = dgvHabitaciones.CurrentRow.Cells[1].Value.ToString();

            this.Close();
        }
    }
}
