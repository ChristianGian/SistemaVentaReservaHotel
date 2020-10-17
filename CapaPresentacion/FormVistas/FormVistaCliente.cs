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
    public partial class FormVistaCliente : Form
    {
        private readonly CNCliente cnCli = new CNCliente();
        private string idCliente;
        private string nombre;

        public string IdCliente { get => idCliente; }
        public string Nombre { get => nombre; }

        public FormVistaCliente()
        {
            InitializeComponent();
        }


        private void FormVistaCliente_Load(object sender, EventArgs e)
        {
            ListarCliente();
        }

        private void ListarCliente()
        {
            var cliente = cnCli.ListarCli();
            lblRegistros.Text = $"Total Registros: {cliente.Count}";

            if (cliente.Count > 0)
            {
                dgvClientes.AutoGenerateColumns = false;
                dgvClientes.DataSource = cliente;

                dgvClientes.Columns[0].DataPropertyName = "IdPersona";
                dgvClientes.Columns[1].DataPropertyName = "Nombre";
                dgvClientes.Columns[2].DataPropertyName = "ApePaterno";
                dgvClientes.Columns[3].DataPropertyName = "ApeMaterno";
                dgvClientes.Columns[4].DataPropertyName = "TipoDoc";
                dgvClientes.Columns[5].DataPropertyName = "NumeroDoc";
                dgvClientes.Columns[6].DataPropertyName = "Direccion";
                dgvClientes.Columns[7].DataPropertyName = "Telefono";
                dgvClientes.Columns[8].DataPropertyName = "Email";
                dgvClientes.Columns[9].DataPropertyName = "IdCliente";
            }
            else
            {
                MessageBox.Show("No existen clientes registrados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DgvClientes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            idCliente = dgvClientes.CurrentRow.Cells[0].Value.ToString();
            nombre = dgvClientes.CurrentRow.Cells[1].Value.ToString() + " " + dgvClientes.CurrentRow.Cells[2].Value.ToString() + " " + dgvClientes.CurrentRow.Cells[3].Value.ToString();

            this.Close();
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            string numeroDoc = txtBuscar.Text;
            var cliente = cnCli.BuscarCli(numeroDoc);

            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.DataSource = cliente;

            dgvClientes.Columns[0].DataPropertyName = "IdPersona";
            dgvClientes.Columns[1].DataPropertyName = "Nombre";
            dgvClientes.Columns[2].DataPropertyName = "ApePaterno";
            dgvClientes.Columns[3].DataPropertyName = "ApeMaterno";
            dgvClientes.Columns[4].DataPropertyName = "TipoDoc";
            dgvClientes.Columns[5].DataPropertyName = "NumeroDoc";
            dgvClientes.Columns[6].DataPropertyName = "Direccion";
            dgvClientes.Columns[7].DataPropertyName = "Telefono";
            dgvClientes.Columns[8].DataPropertyName = "Email";
            dgvClientes.Columns[9].DataPropertyName = "IdCliente";
        }
    }
}
