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

namespace CapaPresentacion.FormVistas
{
    public partial class FormVistaProducto : Form
    {
        private readonly CNProducto cnPro = new CNProducto();
        private string idProducto;
        private string nombreProducto;
        private string precioVenta;

        public string IdProducto { get => idProducto;  }
        public string NombreProducto { get => nombreProducto; }
        public string PrecioVenta { get => precioVenta; }

        public FormVistaProducto()
        {
            InitializeComponent();
        }

        private void FormVistaProducto_Load(object sender, EventArgs e)
        {
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

        private void DgvProductos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            idProducto = dgvProductos.CurrentRow.Cells[0].Value.ToString();
            nombreProducto = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            precioVenta = dgvProductos.CurrentRow.Cells[4].Value.ToString();

            this.Close();
        }
    }
}
