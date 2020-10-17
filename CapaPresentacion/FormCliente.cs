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
    public partial class FormCliente : Form
    {
        private readonly CNCliente cnCli = new CNCliente();
        private ECliente eCli;
        private bool editar = false;
        private string idPersona;
        

        public FormCliente()
        {
            InitializeComponent();
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            cmbTipoDoc.SelectedIndex = 0;

            Inhabilitar();
            ListarCliente();
        }

        private void ListarCliente()
        {
            var cliente = cnCli.ListarCli();

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
                MessageBox.Show("No existen clientes registrados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (eCli == null) eCli = new ECliente();

            try
            {
                eCli.Nombre = txtNombre.Text.Trim();
                eCli.ApePaterno = txtAPaterno.Text.Trim();
                eCli.ApeMaterno = txtAMaterno.Text.Trim();
                eCli.TipoDoc = cmbTipoDoc.Text;
                eCli.NumeroDoc = txtNDoc.Text.Trim();
                eCli.Direccion = txtDireccion.Text.Trim();
                eCli.Telefono = txtTelefono.Text.Trim();
                eCli.Email = txtEmail.Text.Trim();
                eCli.IdCliente = txtCodigo.Text.Trim();

                if (editar)
                {
                    idPersona = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                    eCli.IdPersona = Convert.ToInt32(idPersona);
                    bool resPer = cnCli.EditarPersonaCli(eCli);

                    if (cnCli.builder.Length == 0)
                    {
                        bool resCli = false;
                        if (resPer) resCli = cnCli.EditarCli(eCli);

                        if (resPer && resCli)
                        {
                            MessageBox.Show("¡Cliente editado con éxito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarCliente();
                            Limpiar();
                            Inhabilitar();
                        }
                    }
                }
                else
                {
                    eCli.IdPersona = cnCli.RegistrarPersonaCli(eCli);

                    if (cnCli.builder.Length == 0)
                    {
                        if (eCli.IdPersona != 0)
                        {
                            cnCli.RegistrarCli(eCli);
                            MessageBox.Show("¡Cliente registrado con éxito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarCliente();
                            Limpiar();
                            Inhabilitar();
                        }
                    }
                }

                if (cnCli.builder.Length != 0)
                {
                    MessageBox.Show(cnCli.builder.ToString(), "Para continuar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Inhabilitar()
        {
            txtNombre.Enabled = false;
            txtAPaterno.Enabled = false;
            txtAMaterno.Enabled = false;
            cmbTipoDoc.Enabled = false;
            txtNDoc.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            txtCodigo.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void Habilitar()
        {
            txtNombre.Enabled = true;
            txtAPaterno.Enabled = true;
            txtAMaterno.Enabled = true;
            cmbTipoDoc.Enabled = true;
            txtNDoc.Enabled = true;
            txtDireccion.Enabled = true;
            txtTelefono.Enabled = true;
            txtEmail.Enabled = true;
            txtCodigo.Enabled = true;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtAPaterno.Clear();
            txtAMaterno.Clear();
            cmbTipoDoc.SelectedIndex = 0;
            txtNDoc.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtCodigo.Clear();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar();
            txtNombre.Focus();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Inhabilitar();
        }

        private void DgvClientes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            editar = true;
            Habilitar();

            txtNombre.Text = dgvClientes.CurrentRow.Cells[1].Value.ToString();
            txtAPaterno.Text = dgvClientes.CurrentRow.Cells[2].Value.ToString();
            txtAMaterno.Text = dgvClientes.CurrentRow.Cells[3].Value.ToString();
            cmbTipoDoc.Text = dgvClientes.CurrentRow.Cells[4].Value.ToString();
            txtNDoc.Text = dgvClientes.CurrentRow.Cells[5].Value.ToString();
            txtDireccion.Text = dgvClientes.CurrentRow.Cells[6].Value.ToString();
            txtTelefono.Text = dgvClientes.CurrentRow.Cells[7].Value.ToString();
            txtEmail.Text = dgvClientes.CurrentRow.Cells[8].Value.ToString();
            txtCodigo.Text = dgvClientes.CurrentRow.Cells[9].Value.ToString();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var res = MessageBox.Show("¿Esta seguro de eliminar al cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    idPersona = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                    bool eliminarCli = cnCli.EliminarCli(Convert.ToInt32(idPersona));

                    if (eliminarCli)
                    {
                        cnCli.EliminarPersonaCli(Convert.ToInt32(idPersona));
                        ListarCliente();
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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

        private void TxtNDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloNumeros(e);
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloNumeros(e);
        }

        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            if (!Validaciones.EsEmail(txtEmail.Text))
            {
                MessageBox.Show("Email no válido, el email debe tener el formato: nombre@dominio.com.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.SelectAll();
                txtEmail.Focus();
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
