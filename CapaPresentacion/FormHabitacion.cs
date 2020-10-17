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
using CapaPresentacion.Reportes;
using Entidad;

namespace CapaPresentacion
{
    public partial class FormHabitacion : Form
    {
        private readonly CNHabitacion cnHab = new CNHabitacion();
        private EHabitacion eHab;
        private bool editar = false;
        private string idHabitacion = null;

        public FormHabitacion()
        {
            InitializeComponent();
        }

        private void FormHabitacion_Load(object sender, EventArgs e)
        {
            cmbPiso.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            cmbTipoHab.SelectedIndex = 0;

            ListarHabitacion();   
            InhabilitarControles();
        }

        private void ListarHabitacion()
        {
            var habitacion = cnHab.ListarHab();
            
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

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eHab == null) eHab = new EHabitacion();

                eHab.Numero = txtNumero.Text.Trim();
                eHab.Piso = cmbPiso.Text;
                eHab.Descripcion = txtDescripcion.Text.Trim();
                eHab.Caracteristicas = txtCaracteristicas.Text.Trim();
                eHab.PrecioDiario = Convert.ToDecimal(txtPrecioDiario.Text.Trim());
                eHab.Estado = cmbEstado.Text;
                eHab.TipoHabitacion = cmbTipoHab.Text;

                if (editar)
                {
                    eHab.IdHabitacion = Convert.ToInt32(idHabitacion);
                    cnHab.EditarHab(eHab);
                    editar = false;
                }
                else
                {
                    cnHab.RegistrarHab(eHab);
                }

                if (cnHab.builder.Length != 0)
                    MessageBox.Show(cnHab.builder.ToString(), "Para continuar...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    MessageBox.Show("¡Habitación registrada/actualizada con éxito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarHabitacion();
                    Limpiar();
                    InhabilitarControles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvHabitaciones_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            editar = true;

            HabilitarControles();

            idHabitacion = dgvHabitaciones.CurrentRow.Cells[0].Value.ToString();
            txtNumero.Text = dgvHabitaciones.CurrentRow.Cells[1].Value.ToString();
            cmbPiso.Text = dgvHabitaciones.CurrentRow.Cells[2].Value.ToString();
            txtDescripcion.Text = dgvHabitaciones.CurrentRow.Cells[3].Value.ToString();
            txtCaracteristicas.Text = dgvHabitaciones.CurrentRow.Cells[4].Value.ToString();
            txtPrecioDiario.Text = dgvHabitaciones.CurrentRow.Cells[5].Value.ToString();
            cmbEstado.Text = dgvHabitaciones.CurrentRow.Cells[6].Value.ToString();
            cmbTipoHab.Text = dgvHabitaciones.CurrentRow.Cells[7].Value.ToString();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvHabitaciones.SelectedRows.Count > 0)
            {
                var respuesta = MessageBox.Show("¿Está seguro de eliminar la habitación?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    idHabitacion = dgvHabitaciones.CurrentRow.Cells[0].Value.ToString();
                    cnHab.EliminarHab(Convert.ToInt32(idHabitacion));
                    ListarHabitacion();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void InhabilitarControles()
        {
            txtNumero.Enabled = false;
            cmbPiso.Enabled = false;
            txtDescripcion.Enabled = false;
            txtCaracteristicas.Enabled = false;
            txtPrecioDiario.Enabled = false;
            cmbEstado.Enabled = false;
            cmbTipoHab.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }


        private void HabilitarControles()
        {
            txtNumero.Enabled = true;
            cmbPiso.Enabled = true;
            txtDescripcion.Enabled = true;
            txtCaracteristicas.Enabled = true;
            txtPrecioDiario.Enabled = true;
            cmbEstado.Enabled = true;
            cmbTipoHab.Enabled = true;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void Limpiar()
        {
            txtNumero.Clear();
            cmbPiso.SelectedIndex = 0;
            txtDescripcion.Clear();
            txtCaracteristicas.Clear();
            txtPrecioDiario.Clear();
            cmbEstado.SelectedIndex = 0;
            cmbTipoHab.SelectedIndex = 0;
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            txtNumero.Focus();
        }

        private void TxtPrecioDiario_Click(object sender, EventArgs e)
        {
            txtPrecioDiario.Clear();
            txtPrecioDiario.Focus();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            InhabilitarControles();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnReporte_Click(object sender, EventArgs e)
        {
            FormReportHabitaciones reportHabitaciones = new FormReportHabitaciones();
            reportHabitaciones.ShowDialog();
        }
    }
}
