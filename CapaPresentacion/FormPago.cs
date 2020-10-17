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
    public partial class FormPago : Form
    {
        private readonly CNConsumo cnCon = new CNConsumo();
        private readonly CNPago cnPag = new CNPago();
        private EConsumo eCon;
        private Epago ePag;
        private bool editar = false;
        private string idPago;

        private decimal totalConsumo = 0;

        private string idReserva;
        private string nombreCliente;
        private string costoHabitacion;
        private string idHabitacion;
        private string numHabitacion;
        private string estadoHabitacion;

        public string IdReserva
        { 
            get => idReserva;
            set
            {
                idReserva = value;
                txtIdReserva.Text = idReserva;
            }
        }
        public string NombreCliente
        { 
            get => nombreCliente;
            set
            {
                nombreCliente = value;
                txtReserva.Text = nombreCliente;
            }
        }

        public string CostoHabitacion
        {
            get => costoHabitacion;
            set
            {
                costoHabitacion = value;
                txtTotalReserva.Text = costoHabitacion;
            }
        }

        public string IdHabitacion
        {
            get => idHabitacion;
            set
            {
                idHabitacion = value;
                txtIdHabitacion.Text = idHabitacion;
            }
        }
        public string NumHabitacion
        {
            get => numHabitacion;
            set
            {
                numHabitacion = value;
                txtHabitacion.Text = numHabitacion;
            }
        }
        public FormPago()
        {
            InitializeComponent();
        }

        private void FormPago_Load(object sender, EventArgs e)
        {
            cmbTipoComprobante.SelectedIndex = 0;

            Inhabilitar();
            ListarConsumos(Convert.ToInt32(idReserva));
            ListarPagos(Convert.ToInt32(idReserva));
            CalcularTotalPago();
        }

        private void ListarConsumos(int idReserva)
        {
            var consumos = cnCon.ListarCon(idReserva);
            lblTotalRegistros.Text = $"Total Registros: {consumos.Count}";

            if (consumos.Count > 0)
            {
                dgvConsumos.AutoGenerateColumns = false;
                dgvConsumos.DataSource = consumos;

                dgvConsumos.Columns[0].DataPropertyName = "IdConsumo";
                dgvConsumos.Columns[1].DataPropertyName = "IdReserva";
                dgvConsumos.Columns[2].DataPropertyName = "IdProducto";
                dgvConsumos.Columns[3].DataPropertyName = "NombreProducto";
                dgvConsumos.Columns[4].DataPropertyName = "Cantidad";
                dgvConsumos.Columns[5].DataPropertyName = "PrecioVenta";
                dgvConsumos.Columns[6].DataPropertyName = "Estado";

                CalcularTotalConsumo();
            }
            else
            {
                MessageBox.Show("No hay consumos registrados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ListarPagos(int idReserva)
        {
            var pagos = cnPag.ListarP(idReserva);
            lblTotalRegistrosPagos.Text = $"Total Pagos: {pagos.Count}";

            if (pagos.Count > 0)
            {
                dgvPagos.AutoGenerateColumns = false;
                dgvPagos.DataSource = pagos;

                dgvPagos.Columns[0].DataPropertyName = "IdPago";
                dgvPagos.Columns[1].DataPropertyName = "IdReserva";
                dgvPagos.Columns[2].DataPropertyName = "TipoComprobante";
                dgvPagos.Columns[3].DataPropertyName = "NumComprobante";
                dgvPagos.Columns[4].DataPropertyName = "Igv";
                dgvPagos.Columns[5].DataPropertyName = "TotalPago";
                dgvPagos.Columns[6].DataPropertyName = "FechaEmision";
                dgvPagos.Columns[7].DataPropertyName = "FechaPago";
            }
            else
            {
                MessageBox.Show("No hay pagos registrados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CalcularTotalConsumo()
        {
            for (int i = 0; i < dgvConsumos.Rows.Count; i++)
            {
                decimal cant = Convert.ToDecimal(dgvConsumos.Rows[i].Cells[4].Value.ToString());
                decimal precio = Convert.ToDecimal(dgvConsumos.Rows[i].Cells[5].Value.ToString());

                totalConsumo += cant * precio;
            }
            lblTotalConsumo.Text = $"Total Consumo: {totalConsumo:C}";
        }

        private void CalcularTotalPago()
        {
            decimal totalReserva = Convert.ToDecimal(costoHabitacion);

            decimal totalPago = totalConsumo + totalReserva;
            txtTotalPago.Text = totalPago.ToString();
        }

        private void Inhabilitar()
        {
            txtReserva.Enabled = false;
            txtTotalReserva.Enabled = false;
            txtHabitacion.Enabled = false;
            cmbTipoComprobante.Enabled = false;
            txtNumComprobante.Enabled = false;
            txtTotalPago.Enabled = false;
            txtIgv.Enabled = false;
            dtpFechaEmision.Enabled = false;
            dtpFechaPago.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void Habilitar()
        {
            txtReserva.Enabled = true;
            txtTotalReserva.Enabled = true;
            txtHabitacion.Enabled = true;
            cmbTipoComprobante.Enabled = true;
            txtNumComprobante.Enabled = true;
            txtTotalPago.Enabled = true;
            txtIgv.Enabled = true;
            dtpFechaEmision.Enabled = true;
            dtpFechaPago.Enabled = true;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void Limpiar()
        {
            cmbTipoComprobante.SelectedIndex = 0;
            txtNumComprobante.Clear();
            txtIgv.Clear();
            dtpFechaEmision.Value = DateTime.Now;
            dtpFechaPago.Value = DateTime.Now;
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Inhabilitar();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ePag == null) ePag = new Epago();
                ePag.IdReserva = Convert.ToInt32(txtIdReserva.Text);
                ePag.TipoComprobante = cmbTipoComprobante.Text;
                ePag.NumComprobante = txtNumComprobante.Text.Trim();
                ePag.Igv = Convert.ToDecimal(txtTotalPago.Text);
                ePag.TotalPago = Convert.ToDecimal(txtTotalPago.Text);
                ePag.FechaEmision = Convert.ToDateTime(dtpFechaEmision.Value);
                ePag.FechaPago = Convert.ToDateTime(dtpFechaPago.Value);

                if (editar)
                {
                    idPago = dgvPagos.CurrentRow.Cells[0].Value.ToString();
                    ePag.IdPago = Convert.ToInt32(idPago);

                    bool res = cnPag.EditarP(ePag);
                    if (cnPag.builder.Length == 0)
                    {
                        if (res)
                        {
                            MessageBox.Show("¡Pago editado con éxito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarPagos(Convert.ToInt32(idReserva));
                            Limpiar();
                            Inhabilitar();
                        }
                    }
                }
                else
                {
                    bool resPago = cnPag.RegistrarP(ePag);

                    if (cnPag.builder.Length == 0)
                    {
                        if (resPago)
                        {
                            MessageBox.Show("¡Pago registrado con éxito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarPagos(Convert.ToInt32(idReserva));
                            Limpiar();
                            Inhabilitar();

                            //Cambiar Estado a la habitación (Desocupar)
                            CNHabitacion hb = new CNHabitacion();
                            hb.DesocuparHab(Convert.ToInt32(idHabitacion));

                            //Cambiar estado a la reserva (Pagado)
                            CNReserva rv = new CNReserva();
                            rv.EditarEstadoRva(Convert.ToInt32(idReserva));
                        }
                    }
                }

                if (cnPag.builder.Length != 0)
                {
                    MessageBox.Show(cnPag.builder.ToString(), "Para continuar...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPagos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvPagos.SelectedRows.Count > 0)
            {
                editar = true;
                Habilitar();

                idPago = dgvConsumos.CurrentRow.Cells[0].Value.ToString();

                cmbTipoComprobante.Text = dgvConsumos.CurrentRow.Cells[2].Value.ToString();
                txtNumComprobante.Text = dgvConsumos.CurrentRow.Cells[3].Value.ToString();
                txtIgv.Text = dgvConsumos.CurrentRow.Cells[4].Value.ToString();
                txtTotalPago.Text = dgvConsumos.CurrentRow.Cells[5].Value.ToString();
                dtpFechaEmision.Text = dgvConsumos.CurrentRow.Cells[6].Value.ToString();
                dtpFechaPago.Text = dgvConsumos.CurrentRow.Cells[7].Value.ToString();
            }
            else
            {
                MessageBox.Show("Seleccones una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
           if (dgvPagos.SelectedRows.Count > 0)
            {
                var respuesta = MessageBox.Show("¿Está seguro de eliminar el pago?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    idPago = dgvPagos.CurrentRow.Cells[0].Value.ToString();
                    cnPag.Eliminar(Convert.ToInt32(idPago));
                    ListarPagos(Convert.ToInt32(idReserva));
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvPagos.SelectedRows.Count > 0)
            {
                FormReportComprobante reportComprobante = new FormReportComprobante();
                reportComprobante.IdPago = int.Parse(dgvPagos.CurrentRow.Cells[0].Value.ToString());
                reportComprobante.IdReserva = int.Parse(dgvPagos.CurrentRow.Cells[1].Value.ToString());

                reportComprobante.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un pago", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
