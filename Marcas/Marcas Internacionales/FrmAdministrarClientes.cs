using Comun.Cache;
using Dominio;
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
        private void MostrarClientes()
        {
            dtgClientes.DataSource = personaModel.GetAllClientes();
            
            if (dtgClientes.Columns["id"] != null)
            {
                dtgClientes.Columns["id"].Visible = false;
            }
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }
            
            tabControl1.SelectedTab = nombre;
        }


        private void LoadTitulares()
        {


            // Obtiene los usuarios
            var clientes = personaModel.GetAllClientes();

            Invoke(new Action(() =>
            {
                dtgClientes.DataSource = clientes;

                if (dtgClientes.Columns["id"] != null)
                {
                    dtgClientes.Columns["id"].Visible = false;
                    dtgClientes.ClearSelection();
                }


            }));
        }
        private void btnGuardarTit_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabClienteDetail);
            dtgClientes.ClearSelection();
        }

        private async void btnGuardarCliente_Click(object sender, EventArgs e)
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
                MessageBox.Show("Los campos no pueden estar vacíos.");
            }
            else
            {
                try
                {
                    // Deshabilitar el botón para evitar múltiples clics
                    btnGuardarCliente.Enabled = false;

                    if (btnGuardarCliente.Text == "Guardar")
                    {
                        // Ejecutar la operación de guardado de manera asíncrona
                        await Task.Run(() => personaModel.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo));
                        MessageBox.Show("Cliente agregado exitosamente");
                        dtgClientes.ClearSelection();
                    }
                    else if (btnGuardarCliente.Text == "Actualizar")
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
                            MessageBox.Show("Cliente actualizado exitosamente");
                            dtgClientes.ClearSelection();

                        }
                        else
                        {
                            MessageBox.Show("Error al actualizar el cliente.");
                        }
                    }

                    // Redirigir a tabPage1
                    tabControl1.SelectedTab = tabClientesList;
                    MostrarClientes();
                    EliminarTabPage(tabClienteDetail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo ingresar el cliente por :" + ex.Message);
                }
                finally
                {
                    // Volver a habilitar el botón
                    btnGuardarCliente.Enabled = true;
                }
            }
        }

        private async void FrmAdministrarClientes_Load(object sender, EventArgs e)
        {
            // Cargar titulares en segundo plano
            await Task.Run(() => LoadTitulares());


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
            btnGuardarCliente.Visible = true;
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
            btnGuardarCliente.Visible = false;
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
            iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = tabClienteDetail;
            btnGuardarCliente.Text = "Guardar";
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

        private async void ibtnEditar_Click(object sender, EventArgs e)
        {
            Habilitar();
            if (dtgClientes.SelectedRows.Count > 0)
            {
                ibtnEditar.Enabled = false;
                iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.Pen;
                try
                {
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
                        btnGuardarCliente.Text = "Actualizar";
                        AnadirTabPage(tabClienteDetail);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el cliente.");
                    }
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
                MessageBox.Show("Por favor, selecciona un cliente.");
            }
        }

        private void ibtnEliminar_Click(object sender, EventArgs e)
        {
            //Eliminar
            if (dtgClientes.SelectedRows.Count > 0)
            {
                var userDetails = personaModel.GetPersonaById(EditarPersona.idPersona);

                DialogResult result = MessageBox.Show(UsuarioActivo.usuario + $" ¿Está seguro de que desea eliminar al cliente '{userDetails[0].nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {

                        string currentUser = UsuarioActivo.usuario;
                        bool isDeleted = personaModel.DeleteTitular(userDetails[0].id, userDetails[0].nombre, currentUser);

                        if (isDeleted)
                        {
                            MessageBox.Show("Cliente eliminado correctamente.");
                            MostrarClientes();
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
            string buscar = textBox1.Text;
            if (buscar != "")
            {
                DataTable clientes = personaModel.GetClienteByValue(buscar);
                if (clientes.Rows.Count > 0)
                {
                    dtgClientes.DataSource = clientes;
                    if (dtgClientes.Columns["id"] != null)
                    {
                        dtgClientes.Columns["id"].Visible = false;
                        dtgClientes.Columns["tipo"].Visible = false;
                    }
                    dtgClientes.ClearSelection();
                }
                else
                {
                    MessageBox.Show("No existen clientes con esos datos");
                    MostrarClientes();
                }
            }
            else
            {
                MostrarClientes();
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (dtgClientes.SelectedRows.Count > 0)
            {
                int idPersona = EditarPersona.idPersona;

                var clienteDetails = personaModel.GetPersonaById(idPersona);
                iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.Eye;
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
                MessageBox.Show("Por favor, seleccione una fila de cliente.");
            }
        }
    }
}
