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

namespace CapaPresentacion
{
    public partial class FormConsumo : Form
    {
        private CNConsumo cnCon = new CNConsumo();
        private EConsumo eCon;
        private string idConsumo;

        private string idReserva;
        private string nombreCliente;

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
            set
            {
                nombreCliente = value;
                txtReserva.Text = nombreCliente;
            }
        }

        public FormConsumo()
        {
            InitializeComponent();
        }

        private void FormConsumo_Load(object sender, EventArgs e)
        {
            InicializarControles();
            Inhabilitar();
            ListarConsumos(Convert.ToInt32(idReserva));
        }

        private void ListarConsumos(int idReserva)
        {
            var consumos = cnCon.ListarCon(idReserva);
            lblRegistros.Text = consumos.Count.ToString();

            if (consumos.Count > 0)
            {
                dgvProductos.AutoGenerateColumns = false;
                dgvProductos.DataSource = consumos;

                dgvProductos.Columns[0].DataPropertyName = "IdConsumo";
                dgvProductos.Columns[1].DataPropertyName = "IdReserva";
                dgvProductos.Columns[2].DataPropertyName = "IdProducto";
                dgvProductos.Columns[3].DataPropertyName = "NombreProducto";
                dgvProductos.Columns[4].DataPropertyName = "Cantidad";
                dgvProductos.Columns[5].DataPropertyName = "PrecioVenta";
                dgvProductos.Columns[6].DataPropertyName = "Estado";

                CalcularTotalConsumo();
            }
            else
            {
                MessageBox.Show("No hay consumos registrados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAbrirProducto_Click(object sender, EventArgs e)
        {
            var vistaPro = new FormVistas.FormVistaProducto();
            vistaPro.ShowDialog();

            txtIdProducto.Text = vistaPro.IdProducto;
            txtProducto.Text = vistaPro.NombreProducto;
            txtPrecioVenta.Text = vistaPro.PrecioVenta;
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Inhabilitar();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eCon == null) eCon = new EConsumo();

                eCon.IdReserva = Convert.ToInt32(txtIdReserva.Text);
                eCon.IdProducto = Convert.ToInt32(txtIdProducto.Text);
                eCon.Cantidad = Convert.ToDecimal(txtCantidad.Text.Trim());
                eCon.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text.Trim());
                eCon.Estado = cmbEstado.Text;

                bool reg = cnCon.RegistrarCon(eCon);

                if (cnCon.builder.Length == 0)
                {
                    if (reg)
                    {
                        MessageBox.Show("¡Consumo registrado con éxito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        Inhabilitar();
                        ListarConsumos(Convert.ToInt32(idReserva));
                    }
                }

                if (cnCon.builder.Length != 0)
                {
                    MessageBox.Show(cnCon.builder.ToString(), "Para continuar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if  (dgvProductos.SelectedRows.Count > 0)
            {
                var respuesta = MessageBox.Show("¿Está seguro de eliminar el consumo?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    idConsumo = dgvProductos.CurrentRow.Cells[0].Value.ToString();
                    cnCon.EliminarCon(Convert.ToInt32(idConsumo));
                    ListarConsumos(Convert.ToInt32(IdReserva));
                }

            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void CalcularTotalConsumo()
        {
            decimal totalConsumo = 0;

            for (int i = 0; i < dgvProductos.Rows.Count; i++)
            {
                decimal cant = Convert.ToDecimal(dgvProductos.Rows[i].Cells[4].Value.ToString());
                decimal precio = Convert.ToDecimal(dgvProductos.Rows[i].Cells[5].Value.ToString());

                totalConsumo += cant * precio;
            }
            lblTotalConsumo.Text = $"{totalConsumo:C}";
        }

        private void Inhabilitar()
        {
            txtReserva.Enabled = false;
            txtProducto.Enabled = false;
            txtCantidad.Enabled = false;
            txtPrecioVenta.Enabled = false;
            cmbEstado.Enabled = false;

            btnAbrirProducto.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void Habilitar()
        {
            txtReserva.Enabled = true;
            txtProducto.Enabled = true;
            txtCantidad.Enabled = true;
            txtPrecioVenta.Enabled = true;
            cmbEstado.Enabled = true;

            btnAbrirProducto.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void Limpiar()
        {
            txtIdProducto.Clear();
            txtProducto.Clear();
            txtCantidad.Clear();
            txtPrecioVenta.Clear();
            cmbEstado.SelectedIndex = 0;
        }

        private void InicializarControles()
        {
            cmbEstado.SelectedIndex = 0;
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
