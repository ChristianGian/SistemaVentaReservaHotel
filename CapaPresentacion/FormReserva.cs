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
using Entidad.Cache;

namespace CapaPresentacion
{
    public partial class FormReserva : Form
    {
        private readonly CNReserva cnRva = new CNReserva();
        private EReserva eRva = null;
        private string idReserva;
        private bool editar = false;
        public FormReserva()
        {
            InitializeComponent();
        }

        private void FormReserva_Load(object sender, EventArgs e)
        {
            InicializarElementos();
            Inhabilitar();
            ListarReserva();
        }

        private void ListarReserva()
        {
            List<EReserva> reserva = cnRva.ListarRva();
            lblRegistros.Text = $"Total Registros: {reserva.Count}";

            if (reserva.Count > 0)
            {
                dgvReservas.AutoGenerateColumns = false;
                dgvReservas.DataSource = reserva;

                dgvReservas.Columns[0].DataPropertyName = "IdReserva";
                dgvReservas.Columns[1].DataPropertyName = "IdHabitacion";
                dgvReservas.Columns[2].DataPropertyName = "Numero";
                dgvReservas.Columns[3].DataPropertyName = "IdCliente";
                dgvReservas.Columns[4].DataPropertyName = "NomCompCliente";
                dgvReservas.Columns[5].DataPropertyName = "IdTrabajador";
                dgvReservas.Columns[6].DataPropertyName = "NomCompTrabajador";
                dgvReservas.Columns[7].DataPropertyName = "TipoReserva";
                dgvReservas.Columns[8].DataPropertyName = "FechaReserva";
                dgvReservas.Columns[9].DataPropertyName = "FechaIngreso";
                dgvReservas.Columns[10].DataPropertyName = "FechaSalida";
                dgvReservas.Columns[11].DataPropertyName = "CostoAlojamiento";
                dgvReservas.Columns[12].DataPropertyName = "Estado";
            }
            else
            {
                MessageBox.Show("No existen reversas registradas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Inhabilitar()
        {
            txtIdHabitacion.Enabled = false;
            txtIdCliente.Enabled = false;
            txtIdTrabajador.Enabled = false;

            btnAbrirHabitacion.Enabled = false;
            btnAbrirCliente.Enabled = false;

            txtHabitacion.Enabled = false;
            txtCliente.Enabled = false;
            txtTrabajador.Enabled = false;
            cmbTipoReserva.Enabled = false;
            dtpFechaReserva.Enabled = false;
            dtpFechaIngreso.Enabled = false;
            dtpFechaSalida.Enabled = false;
            txtCosto.Enabled = false;
            cmbEstado.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void Habilitar()
        {
            txtIdHabitacion.Enabled = true;
            txtIdCliente.Enabled = true;
            txtIdTrabajador.Enabled = true;

            btnAbrirHabitacion.Enabled = true;
            btnAbrirCliente.Enabled = true;

            txtHabitacion.Enabled = true;
            txtCliente.Enabled = true;
            txtTrabajador.Enabled = true;
            cmbTipoReserva.Enabled = true;
            dtpFechaReserva.Enabled = true;
            dtpFechaIngreso.Enabled = true;
            dtpFechaSalida.Enabled = true;
            txtCosto.Enabled = true;
            cmbEstado.Enabled = true;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void InicializarElementos()
        {
            cmbTipoReserva.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;

            txtIdTrabajador.Text = UserLoginCache.IdPersona.ToString();
            txtTrabajador.Text = $"{UserLoginCache.Nombre} {UserLoginCache.ApePaterno} {UserLoginCache.ApeMaterno}";
        }

        private void Limpiar()
        {
            txtIdHabitacion.Clear();
            txtIdCliente.Clear();
            //txtIdTrabajador.Clear();

            txtHabitacion.Clear();
            txtCliente.Clear();
            //txtTrabajador.Clear();
            cmbTipoReserva.SelectedIndex = 0;
            dtpFechaReserva.Value = DateTime.Now;
            dtpFechaIngreso.Value = DateTime.Now;
            dtpFechaSalida.Value = DateTime.Now;
            txtCosto.Clear();
            cmbEstado.SelectedIndex = 0;
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Inhabilitar();
        }

        private void BtnAbrirHabitacion_Click(object sender, EventArgs e)
        {
            var vistaHab = new FormVistas.FormVistaHabitacion();
            vistaHab.ShowDialog();

            txtIdHabitacion.Text = vistaHab.IdHabitacion;
            txtHabitacion.Text = vistaHab.Numero;
            
        }

        private void BtnAbrirCliente_Click(object sender, EventArgs e)
        {
            var vistaCli = new FormVistas.FormVistaCliente();
            vistaCli.ShowDialog();

            txtIdCliente.Text = vistaCli.IdCliente;
            txtCliente.Text = vistaCli.Nombre;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eRva == null) eRva = new EReserva();

                eRva.IdHabitacion = Convert.ToInt32(txtIdHabitacion.Text);
                eRva.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                eRva.IdTrabajador = Convert.ToInt32(txtIdTrabajador.Text);
                eRva.TipoReserva = cmbTipoReserva.Text;
                eRva.FechaReserva = dtpFechaReserva.Value;
                eRva.FechaIngreso = dtpFechaIngreso.Value;
                eRva.FechaSalida = dtpFechaSalida.Value;
                eRva.CostoAlojamiento = Convert.ToDecimal(txtCosto.Text.Trim());
                eRva.Estado = cmbEstado.Text;

                if (editar)
                {
                    idReserva = dgvReservas.CurrentRow.Cells[0].Value.ToString();
                    eRva.IdReserva = Convert.ToInt32(idReserva);

                    bool res = cnRva.EditarRva(eRva);
                    if (cnRva.builder.Length == 0)
                    {
                        if (res)
                        {
                            MessageBox.Show("¡Reserva editada con éxito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarReserva();
                            Limpiar();
                            Inhabilitar();
                        }
                    }
                }
                else
                {
                    bool res = cnRva.RegistrarRva(eRva);

                    if (cnRva.builder.Length == 0)
                    {
                        if (res)
                        {
                            MessageBox.Show("¡Reserva registrada con éxito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarReserva();
                            Limpiar();
                            Inhabilitar();

                            //Ocupamos la habitación alquilada
                            CNHabitacion hb = new CNHabitacion();
                            hb.OcuparHab(eRva.IdHabitacion);
                        }
                    }
                }

                if (cnRva.builder.Length != 0)
                {
                    MessageBox.Show(cnRva.builder.ToString(), "Para continuar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvReservas.SelectedRows.Count > 0)
            {
                var res = MessageBox.Show("¿Esta seguro de eliminar la reserva?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    idReserva = dgvReservas.CurrentRow.Cells[0].Value.ToString();
                    bool eliminarRva = cnRva.EliminarRva(Convert.ToInt32(idReserva));

                    if (eliminarRva) ListarReserva();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DtpBuscar_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaReserva = dtpBuscar.Value;
            var lista = cnRva.BuscarRva(fechaReserva);

            dgvReservas.AutoGenerateColumns = false;
            dgvReservas.DataSource = lista;

            dgvReservas.Columns[0].DataPropertyName = "IdReserva";
            dgvReservas.Columns[1].DataPropertyName = "Numero";
            dgvReservas.Columns[2].DataPropertyName = "NomCompCliente";
            dgvReservas.Columns[3].DataPropertyName = "NomCompTrabajador";
            dgvReservas.Columns[4].DataPropertyName = "TipoReserva";
            dgvReservas.Columns[5].DataPropertyName = "FechaReserva";
            dgvReservas.Columns[6].DataPropertyName = "FechaIngreso";
            dgvReservas.Columns[7].DataPropertyName = "FechaSalida";
            dgvReservas.Columns[8].DataPropertyName = "CostoAlojamiento";
            dgvReservas.Columns[9].DataPropertyName = "Estado";
        }

        private void BtnVerTodo_Click(object sender, EventArgs e)
        {
            ListarReserva();
        }

        private void DgvReservas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            editar = true;
            Habilitar();

            txtIdHabitacion.Text = dgvReservas.CurrentRow.Cells[1].Value.ToString();
            txtHabitacion.Text = dgvReservas.CurrentRow.Cells[2].Value.ToString();
            txtIdCliente.Text = dgvReservas.CurrentRow.Cells[3].Value.ToString();
            txtCliente.Text = dgvReservas.CurrentRow.Cells[4].Value.ToString();
            txtIdTrabajador.Text = dgvReservas.CurrentRow.Cells[5].Value.ToString();
            txtTrabajador.Text = dgvReservas.CurrentRow.Cells[6].Value.ToString();
            cmbTipoReserva.Text = dgvReservas.CurrentRow.Cells[7].Value.ToString();
            dtpFechaReserva.Text = dgvReservas.CurrentRow.Cells[8].Value.ToString();
            dtpFechaIngreso.Text = dgvReservas.CurrentRow.Cells[9].Value.ToString();
            dtpFechaSalida.Text = dgvReservas.CurrentRow.Cells[10].Value.ToString();
            txtCosto.Text = dgvReservas.CurrentRow.Cells[11].Value.ToString();
            cmbEstado.Text = dgvReservas.CurrentRow.Cells[12].Value.ToString();
        }

        private void BtnConsumo_Click(object sender, EventArgs e)
        {
            if (dgvReservas.SelectedRows.Count > 0)
            {
                FormConsumo consumo = new FormConsumo();
                consumo.IdReserva = dgvReservas.CurrentRow.Cells[0].Value.ToString();
                consumo.NombreCliente = dgvReservas.CurrentRow.Cells[4].Value.ToString();

                consumo.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un reserva", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRealizarPagos_Click(object sender, EventArgs e)
        {
            if (dgvReservas.SelectedRows.Count > 0)
            {
                FormPago pago = new FormPago();
                pago.IdReserva = dgvReservas.CurrentRow.Cells[0].Value.ToString();
                pago.NombreCliente = dgvReservas.CurrentRow.Cells[4].Value.ToString();
                pago.CostoHabitacion = dgvReservas.CurrentRow.Cells[11].Value.ToString();
                pago.IdHabitacion = dgvReservas.CurrentRow.Cells[1].Value.ToString();
                pago.NumHabitacion = dgvReservas.CurrentRow.Cells[2].Value.ToString();

                pago.ShowDialog();

                ListarReserva();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un reserva", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }   
}
