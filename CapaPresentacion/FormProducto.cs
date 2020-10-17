using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FormProducto : Form
    {
        private readonly CNProducto cnPro = new CNProducto();
        private EProducto ePro;
        private bool editar = false;
        private string idProducto = null;

        public FormProducto()
        {
            InitializeComponent();
        }

        private void FormProducto_Load(object sender, EventArgs e)
        {
            cmbUnidadMedida.SelectedIndex = 0;

            InhabilitarControles();
            ListarProducto();
        }

        private void ListarProducto()
        {
            var lista = cnPro.ListarPro();

            int numRegistros = lista.Count;
            lblRegistros.Text = numRegistros.ToString();

            if (numRegistros > 0)
            {
                dgvProductos.AutoGenerateColumns = false;
                dgvProductos.DataSource = lista;

                dgvProductos.Columns[0].DataPropertyName = "IdProducto";
                dgvProductos.Columns[1].DataPropertyName = "Nombre";
                dgvProductos.Columns[2].DataPropertyName = "Descripcion";
                dgvProductos.Columns[3].DataPropertyName = "UnidadMedida";
                dgvProductos.Columns[4].DataPropertyName = "PrecioVenta";
            }
            else
            {
                MessageBox.Show("No hay productos registrados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            string nombre = txtBuscar.Text;
            var lista = cnPro.BuscarPro(nombre);

            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.DataSource = lista;

            dgvProductos.Columns[0].DataPropertyName = "IdProducto";
            dgvProductos.Columns[1].DataPropertyName = "Nombre";
            dgvProductos.Columns[2].DataPropertyName = "Descripcion";
            dgvProductos.Columns[3].DataPropertyName = "UnidadMedida";
            dgvProductos.Columns[4].DataPropertyName = "PrecioVenta";
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ePro == null) ePro = new EProducto();

                ePro.Nombre = txtNombre.Text.Trim();
                ePro.Descripcion = txtDescripcion.Text.Trim();
                ePro.UnidadMedida = cmbUnidadMedida.Text;
                ePro.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text.Trim());

                if (editar)
                {
                    idProducto = dgvProductos.CurrentRow.Cells[0].Value.ToString();
                    ePro.IdProducto = (Convert.ToInt32(idProducto));
                    cnPro.EditarPro(ePro);
                    editar = false;

                    if (cnPro.builder.Length == 0)
                    {
                        MessageBox.Show("¡Producto editado correctamente!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarProducto();
                        Limpiar();
                        InhabilitarControles();
                    }
                }
                else
                {
                    cnPro.RegistrarPro(ePro);

                    if (cnPro.builder.Length == 0)
                    {
                        MessageBox.Show("¡Producto registrado correctamente!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarProducto();
                        Limpiar();
                        InhabilitarControles();
                    }
                }

                if (cnPro.builder.Length != 0)
                {
                    MessageBox.Show(cnPro.builder.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            txtNombre.Focus();
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            cmbUnidadMedida.SelectedIndex = 0;
            txtPrecioVenta.Clear();
        }

        private void InhabilitarControles()
        {
            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
            cmbUnidadMedida.Enabled = false;
            txtPrecioVenta.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void HabilitarControles()
        {
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            cmbUnidadMedida.Enabled = true;
            txtPrecioVenta.Enabled = true;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }


        //Permite pasar los datos del DGV a los TextBox
        private void DgvProductos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            editar = true;
            HabilitarControles();

            txtNombre.Text = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = dgvProductos.CurrentRow.Cells[2].Value.ToString();
            cmbUnidadMedida.Text = dgvProductos.CurrentRow.Cells[3].Value.ToString();
            txtPrecioVenta.Text = dgvProductos.CurrentRow.Cells[4].Value.ToString();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var respuesta = MessageBox.Show("¿Está seguro de eliminar el producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    idProducto = dgvProductos.CurrentRow.Cells[0].Value.ToString();
                    cnPro.EliminarPro(Convert.ToInt32(idProducto));
                    ListarProducto();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            InhabilitarControles();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
