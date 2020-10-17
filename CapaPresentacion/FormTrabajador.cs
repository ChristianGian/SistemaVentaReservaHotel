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
    public partial class FormTrabajador : Form
    {
        private readonly CNTrabajador cnTra = new CNTrabajador();
        private ETrabajador eTra;
        private bool editar = false;
        private string idPersona;

        public FormTrabajador()
        {
            InitializeComponent();
        }

        private void FormTrabajador_Load(object sender, EventArgs e)
        {
            cmbTipoDoc.SelectedIndex = 0;
            cmbAcceso.SelectedIndex = 1;
            cmbEstado.SelectedIndex = 0;

            Deshabilitar();
            ListarTrabajador();
        }

        private void ListarTrabajador()
        {
            var trabajador = cnTra.ListarTra();
            lblRegistros.Text = $"Total Registros: {trabajador.Count}";

            if (trabajador.Count > 0)
            {
                dgvTrabajadores.AutoGenerateColumns = false;
                dgvTrabajadores.DataSource = trabajador;

                dgvTrabajadores.Columns[0].DataPropertyName = "IdPersona";
                dgvTrabajadores.Columns[1].DataPropertyName = "Nombre";
                dgvTrabajadores.Columns[2].DataPropertyName = "ApePaterno";
                dgvTrabajadores.Columns[3].DataPropertyName = "ApeMaterno";
                dgvTrabajadores.Columns[4].DataPropertyName = "TipoDoc";
                dgvTrabajadores.Columns[5].DataPropertyName = "NumeroDoc";
                dgvTrabajadores.Columns[6].DataPropertyName = "Direccion";
                dgvTrabajadores.Columns[7].DataPropertyName = "Telefono";
                dgvTrabajadores.Columns[8].DataPropertyName = "Email";
                dgvTrabajadores.Columns[9].DataPropertyName = "Sueldo";
                dgvTrabajadores.Columns[10].DataPropertyName = "Acceso";
                dgvTrabajadores.Columns[11].DataPropertyName = "Sesion";
                dgvTrabajadores.Columns[12].DataPropertyName = "Contrasenia";
                dgvTrabajadores.Columns[13].DataPropertyName = "Estado";
            }
            else
            {
                MessageBox.Show("No hay trabajadores registrado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eTra == null) eTra = new ETrabajador();

                eTra.Nombre = txtNombre.Text.Trim();
                eTra.ApePaterno = txtAPaterno.Text.Trim();
                eTra.ApeMaterno = txtAMaterno.Text.Trim();
                eTra.TipoDoc = cmbTipoDoc.Text;
                eTra.NumeroDoc = txtNDoc.Text.Trim();
                eTra.Direccion = txtDireccion.Text.Trim();
                eTra.Telefono = txtTelefono.Text.Trim();
                eTra.Email = txtEmail.Text.Trim();
                eTra.Sueldo = Convert.ToDecimal(txtSueldo.Text);
                eTra.Acceso = cmbAcceso.Text;
                eTra.Sesion = txtLogin.Text.Trim();
                eTra.Contrasenia = txtPass.Text.Trim();
                eTra.Estado = cmbEstado.Text;

                if (editar)
                {
                    idPersona = dgvTrabajadores.CurrentRow.Cells[0].Value.ToString();
                    eTra.IdPersona = Convert.ToInt32(idPersona);

                    bool res = cnTra.EditarPersonaTra(eTra);

                    if (cnTra.builder.Length == 0)
                    {
                        bool resTra = false;
                        if (res) resTra = cnTra.EditarTra(eTra);

                        if (res && resTra)
                        {
                            MessageBox.Show("¡Trabajador editado con éxito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarTrabajador();
                            Limpiar();
                            Deshabilitar();
                        }
                    }
                }
                else
                {
                    int idTrabajador = cnTra.RegistrarPersonaTra(eTra);

                    if (cnTra.builder.Length == 0)
                    {
                        if (idTrabajador != 0)
                        {
                            eTra.IdPersona = idTrabajador;
                            bool res = cnTra.RegistrarTra(eTra);

                            if (res)
                            {
                                Console.WriteLine("¡Trabajador registrado correctamente!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ListarTrabajador();
                                Limpiar();
                                Deshabilitar();

                            }
                        }
                    }
                }

                if (cnTra.builder.Length != 0)
                {
                    MessageBox.Show(cnTra.builder.ToString(), "Para continuar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Deshabilitar()
        {
            txtNombre.Enabled = false;
            txtAPaterno.Enabled = false;
            txtAMaterno.Enabled = false;
            cmbTipoDoc.Enabled = false;
            txtNDoc.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            txtSueldo.Enabled = false;
            cmbAcceso.Enabled = false;
            txtLogin.Enabled = false;
            txtPass.Enabled = false;
            cmbEstado.Enabled = false;

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
            txtSueldo.Enabled = true;
            cmbAcceso.Enabled = true;
            txtLogin.Enabled = true;
            txtPass.Enabled = true;
            cmbEstado.Enabled = true;

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
            txtSueldo.Clear();
            cmbAcceso.SelectedIndex = 1;
            txtLogin.Clear();
            txtPass.Clear();
            cmbEstado.SelectedIndex = 0;
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void DgvTrabajadores_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            editar = true;
            Habilitar();

            txtNombre.Text = dgvTrabajadores.CurrentRow.Cells[1].Value.ToString();
            txtAPaterno.Text = dgvTrabajadores.CurrentRow.Cells[2].Value.ToString();
            txtAMaterno.Text = dgvTrabajadores.CurrentRow.Cells[3].Value.ToString();
            cmbTipoDoc.Text = dgvTrabajadores.CurrentRow.Cells[4].Value.ToString();
            txtNDoc.Text = dgvTrabajadores.CurrentRow.Cells[5].Value.ToString();
            txtDireccion.Text = dgvTrabajadores.CurrentRow.Cells[6].Value.ToString();
            txtTelefono.Text = dgvTrabajadores.CurrentRow.Cells[7].Value.ToString();
            txtEmail.Text = dgvTrabajadores.CurrentRow.Cells[8].Value.ToString();
            txtSueldo.Text = dgvTrabajadores.CurrentRow.Cells[9].Value.ToString();
            cmbAcceso.Text = dgvTrabajadores.CurrentRow.Cells[10].Value.ToString();
            txtLogin.Text = dgvTrabajadores.CurrentRow.Cells[11].Value.ToString();
            txtPass.Text = dgvTrabajadores.CurrentRow.Cells[12].Value.ToString();
            cmbEstado.Text = dgvTrabajadores.CurrentRow.Cells[13].Value.ToString();
            
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTrabajadores.SelectedRows.Count > 0)
            {
                var res = MessageBox.Show("¿Esta seguro de eliminar al trabajador?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    idPersona = dgvTrabajadores.CurrentRow.Cells[0].Value.ToString();
                    bool eliminarTra = cnTra.EliminarTra(Convert.ToInt32(idPersona));

                    if (eliminarTra)
                    {
                        cnTra.EliminarPersonaTra(Convert.ToInt32(idPersona));
                        ListarTrabajador();
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
            var trabajador = cnTra.BuscarTra(numeroDoc);

            dgvTrabajadores.AutoGenerateColumns = false;
            dgvTrabajadores.DataSource = trabajador;

            dgvTrabajadores.Columns[0].DataPropertyName = "IdPersona";
            dgvTrabajadores.Columns[1].DataPropertyName = "Nombre";
            dgvTrabajadores.Columns[2].DataPropertyName = "ApePaterno";
            dgvTrabajadores.Columns[3].DataPropertyName = "ApeMaterno";
            dgvTrabajadores.Columns[4].DataPropertyName = "TipoDoc";
            dgvTrabajadores.Columns[5].DataPropertyName = "NumeroDoc";
            dgvTrabajadores.Columns[6].DataPropertyName = "Direccion";
            dgvTrabajadores.Columns[7].DataPropertyName = "Telefono";
            dgvTrabajadores.Columns[8].DataPropertyName = "Email";
            dgvTrabajadores.Columns[9].DataPropertyName = "Sueldo";
            dgvTrabajadores.Columns[10].DataPropertyName = "Acceso";
            dgvTrabajadores.Columns[11].DataPropertyName = "Sesion";
            dgvTrabajadores.Columns[12].DataPropertyName = "Contrasenia";
            dgvTrabajadores.Columns[13].DataPropertyName = "Estado";
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Deshabilitar();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
