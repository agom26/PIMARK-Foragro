using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using Presentacion.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmAdministrarClientes : Form
    {

        PersonaModel personaModel = new PersonaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;

        public FrmAdministrarClientes()
        {
            InitializeComponent();

            this.Load += FrmAdministrarClientes_Load;
            if (UsuarioActivo.isAdmin == false)
            {
                ibtnEditar.Visible = false;

            }
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }
        public void LimpiarCampos()
        {
            txtNombreCliente.Text = "";
            txtDireccionCliente.Text = "";
            txtNitCliente.Text = "";
            comboBox1.SelectedIndex = -1;
            txtCorreoContacto.Text = "";
            txtTelefonoContacto.Text = "";
            txtNombreContacto.Text = "";
        }

        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }

            tabControl1.SelectedTab = nombre;
        }


        private async Task LoadClientes()
        {
            totalRows = personaModel.GetTotalClientes();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var titulares = await Task.Run(() => personaModel.GetAllClientes(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgClientes.DataSource = titulares;

                if (dtgClientes.Columns["id"] != null)
                {
                    dtgClientes.Columns["id"].Visible = false;
                    dtgClientes.ClearSelection();
                }


            }));
        }
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = personaModel.GetFilteredClientesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = personaModel.GetClienteByValue(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgClientes.DataSource = titulares;
                    if (dtgClientes.Columns["id"] != null)
                    {
                        dtgClientes.Columns["id"].Visible = false;
                        dtgClientes.Columns["tipo"].Visible = false;
                    }
                    dtgClientes.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN CLIENTES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadClientes();
                }
            }
            else
            {
                await LoadClientes();
            }
        }
        private void btnGuardarTit_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private async void btnGuardarCliente_Click(object sender, EventArgs e)
        {

        }

        private async void FrmAdministrarClientes_Load(object sender, EventArgs e)
        {
            // Cargar titulares en segundo plano
            await Task.Run(() => LoadClientes());
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();

            // Eliminar la tabPage de detalle
            tabControl1.TabPages.Remove(tabClienteDetail);
        }

        public void Habilitar()
        {
            txtNombreCliente.Enabled = true;
            txtDireccionCliente.Enabled = true;
            txtNitCliente.Enabled = true;
            comboBox1.Enabled = true;
            txtCorreoContacto.Enabled = true;
            txtTelefonoContacto.Enabled = true;
            txtNombreContacto.Enabled = true;
            txtNombreContacto.Enabled = true;
            btnGuardarU.Visible = true;
        }
        public void Deshabilitar()
        {
            txtNombreCliente.Enabled = false;
            txtDireccionCliente.Enabled = false;
            txtNitCliente.Enabled = false;
            comboBox1.Enabled = false;
            txtCorreoContacto.Enabled = false;
            txtTelefonoContacto.Enabled = false;
            txtNombreContacto.Enabled = false;
            txtNombreContacto.Enabled = false;
            btnGuardarU.Visible = false;
        }

        private void ibtnAgregar_Click(object sender, EventArgs e)
        {
            Habilitar();
            LimpiarCampos();
            // Asegúrate de que el tabPageUserDetail esté agregado al TabControl (solo si no está ya agregado)
            if (!tabControl1.TabPages.Contains(tabClienteDetail))
            {
                tabControl1.TabPages.Add(tabClienteDetail);
            }
            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = tabClienteDetail;
            btnGuardarU.Text = "AGREGAR";
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;

            btnGuardarU.BackColor = Color.FromArgb(50, 164, 115);
            btnCambios.Image = Properties.Resources.agregar;
            btnCambios.Text = "AGREGAR";
        }

        private void dtgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verificar que una fila válida esté seleccionada
            {
                // Obtener el valor del 'id' de la fila seleccionada
                EditarPersona.idPersona = Convert.ToInt32(dtgClientes.Rows[e.RowIndex].Cells["id"].Value);

                //MessageBox.Show("ID del usuario seleccionado: " + EditarPersona.idPersona);
            }
        }

        public async void EditarCliente()
        {
            Habilitar();
            if (dtgClientes.SelectedRows.Count > 0)
            {

                ibtnEditar.Enabled = false;
                try
                {
                    tabControl1.Visible = false;
                    int idPersona = EditarPersona.idPersona;

                    var clienteDetails = await Task.Run(() => personaModel.GetPersonaById(idPersona));

                    if (clienteDetails.Count > 0)
                    {
                        // Asignar los valores obtenidos a la clase estática EditarPersona
                        EditarPersona.idPersona = clienteDetails[0].id;
                        EditarPersona.nombre = clienteDetails[0].nombre;
                        EditarPersona.direccion = clienteDetails[0].direccion;
                        EditarPersona.nit = clienteDetails[0].nit;
                        EditarPersona.pais = clienteDetails[0].pais;
                        EditarPersona.correo = clienteDetails[0].correo;
                        EditarPersona.telefono = clienteDetails[0].telefono;
                        EditarPersona.contacto = clienteDetails[0].contacto;

                        // Mostrar el formulario de edición con los valores del cliente

                        txtNombreCliente.Text = EditarPersona.nombre;
                        txtDireccionCliente.Text = EditarPersona.direccion;
                        txtNitCliente.Text = EditarPersona.nit;
                        comboBox1.SelectedItem = EditarPersona.pais;
                        txtCorreoContacto.Text = EditarPersona.correo;
                        txtTelefonoContacto.Text = EditarPersona.telefono;
                        txtNombreContacto.Text = EditarPersona.contacto;
                        btnGuardarU.Text = "EDITAR";

                        btnGuardarU.BackColor = Color.FromArgb(96, 149, 241);
                        btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.Pen;
                        btnCambios.Image = Properties.Resources.lapiz;
                        btnCambios.Text = "EDITAR";
                        AnadirTabPage(tabClienteDetail);
                    }
                    else
                    {
                        FrmAlerta alerta = new FrmAlerta("NO SE ENCONTRÓ AL CLIENTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                        //MessageBox.Show("No se encontró el cliente.");
                    }
                    tabControl1.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos del cliente: " + ex.Message);
                }
                finally
                {
                    // Volver a habilitar el botón después de cargar el formulario
                    ibtnEditar.Enabled = true;
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN CLIENTE", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, selecciona un cliente.");
            }
        }

        private async void ibtnEditar_Click(object sender, EventArgs e)
        {
            EditarCliente();
        }

        private async void ibtnEliminar_Click(object sender, EventArgs e)
        {
            //Eliminar
            if (dtgClientes.SelectedRows.Count > 0)
            {
                var userDetails = personaModel.GetPersonaById(EditarPersona.idPersona);

                DialogResult result = MessageBox.Show(UsuarioActivo.usuario + $" ¿ESTÁ SEGURO DE ELIMINAR AL CLIENTE '{userDetails[0].nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {

                        string currentUser = UsuarioActivo.usuario;
                        bool isDeleted = personaModel.DeleteTitular(userDetails[0].id, userDetails[0].nombre, currentUser);

                        if (isDeleted)
                        {

                            MessageBox.Show("Cliente eliminado correctamente.");
                            await LoadClientes();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el cliente.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar eliminar el cliente: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un cliente para eliminar.");
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            filtrar();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (dtgClientes.SelectedRows.Count > 0)
            {
                int idPersona = EditarPersona.idPersona;

                var clienteDetails = personaModel.GetPersonaById(idPersona);
                if (clienteDetails.Count > 0)
                {

                    EditarPersona.idPersona = clienteDetails[0].id;
                    EditarPersona.nombre = clienteDetails[0].nombre;
                    EditarPersona.direccion = clienteDetails[0].direccion;
                    EditarPersona.nit = clienteDetails[0].nit;
                    EditarPersona.pais = clienteDetails[0].pais;
                    EditarPersona.correo = clienteDetails[0].correo;
                    EditarPersona.telefono = clienteDetails[0].telefono;
                    EditarPersona.contacto = clienteDetails[0].contacto;

                    // Mostrar el formulario de edición con los valores del titular

                    txtNombreCliente.Text = EditarPersona.nombre;
                    txtDireccionCliente.Text = EditarPersona.direccion;
                    txtNitCliente.Text = EditarPersona.nit;
                    comboBox1.SelectedItem = EditarPersona.pais;
                    txtCorreoContacto.Text = EditarPersona.correo;
                    txtTelefonoContacto.Text = EditarPersona.telefono;
                    txtNombreContacto.Text = EditarPersona.contacto;
                    Deshabilitar();
                    AnadirTabPage(tabClienteDetail);
                }
                else
                {
                    MessageBox.Show("No se encontró el cliente.");
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione una fila de cliente.");
            }
        }

        private void btnCancelarU_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabClienteDetail);
            dtgClientes.ClearSelection();
        }

        private async void btnGuardarU_Click(object sender, EventArgs e)
        {
            string nombre = txtNitCliente.Text;
            string direccion = txtDireccionCliente.Text;
            string nit = txtNitCliente.Text;
            string pais = comboBox1.SelectedItem?.ToString();
            string correo = txtCorreoContacto.Text;
            string telefono = txtTelefonoContacto.Text;
            string contacto = txtNombreContacto.Text;
            string tipo = "cliente";

            // Verificar que el campo de nombre de usuario no esté vacío
            if (string.IsNullOrWhiteSpace(txtNitCliente.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionCliente.Text) ||
                string.IsNullOrWhiteSpace(txtNitCliente.Text) ||
                string.IsNullOrWhiteSpace(pais) ||
                string.IsNullOrWhiteSpace(txtCorreoContacto.Text) ||
                string.IsNullOrWhiteSpace(txtNombreContacto.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoContacto.Text))
            {
                FrmAlerta alerta = new FrmAlerta("LOS CAMPOS NO PUEDEN ESTAR VACÍOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Los campos no pueden estar vacíos.");
            }
            else
            {
                try
                {

                    btnGuardarU.Enabled = false;

                    if (btnGuardarU.Text == "GUARDAR")
                    {

                        await Task.Run(() => personaModel.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo));
                        FrmAlerta alerta = new FrmAlerta("CLIENTE AGREGADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        //MessageBox.Show("Cliente agregado exitosamente");
                        dtgClientes.ClearSelection();
                    }
                    else if (btnGuardarU.Text == "EDITAR")
                    {
                        bool update = await Task.Run(() => personaModel.UpdatePersona(EditarPersona.idPersona,
                            txtNombreCliente.Text,
                            txtDireccionCliente.Text,
                            txtNitCliente.Text,
                            pais,
                            txtCorreoContacto.Text,
                            txtTelefonoContacto.Text,
                            txtNombreContacto.Text));

                        if (update)
                        {
                            FrmAlerta alerta = new FrmAlerta("CLIENTE ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            //MessageBox.Show("Cliente actualizado exitosamente");
                            dtgClientes.ClearSelection();

                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("CLIENTE NO ACTUALIZADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta.ShowDialog();
                            //MessageBox.Show("Error al actualizar el cliente.");
                        }
                    }

                    tabControl1.SelectedTab = tabClientesList;
                    LoadClientes();
                    EliminarTabPage(tabClienteDetail);
                }
                catch (Exception ex)
                {
                    FrmAlerta alerta = new FrmAlerta("NO SE PUDO INGRESAR AL CLIENTE POR:\n" + ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                    //MessageBox.Show("No se pudo ingresar el cliente por :" + ex.Message);
                }
                finally
                {
                    btnGuardarU.Enabled = true;
                }
            }
        }

        private void dtgClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarCliente();
        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filtrar();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (txtBuscar.Text != "")
            {
                filtrar();
            }
            else
            {
                await LoadClientes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (txtBuscar.Text != "")
                {
                    filtrar();
                }
                else
                {
                    await LoadClientes();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (txtBuscar.Text != "")
                {
                    filtrar();
                }
                else
                {
                    await LoadClientes();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (txtBuscar.Text != "")
            {
                filtrar();
            }
            else
            {
                await LoadClientes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }
    }
}
