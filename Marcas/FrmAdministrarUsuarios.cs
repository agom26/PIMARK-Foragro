
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun.Cache;
using Dominio;

namespace Presentacion
{
    public partial class FrmAdministrarUsuarios : Form
    {
        UserModel UserModel = new UserModel();
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        public FrmAdministrarUsuarios()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            this.Load += FrmAdministrarUsuarios_Load; // Mueve la lógica de carga aquí

        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
                dtgUsuarios.ClearSelection();
            }
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }
            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = nombre;
        }

        private void AssociateAndRaiseViewEvents()
        {

        }



        //eventos
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //metodos
        public void SetUserListBindingSource(BindingSource userList)
        {
            dtgUsuarios.DataSource = userList;
        }
        public void LimpiarCampos()
        {
            txtUsername.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCorreo.Text = "";
            txtConfirmarCont.Text = "";
            txtCont.Text = "";
            chckbIsAdmin.Checked = false;
        }

        private void MostrarUsuarios()
        {
            dtgUsuarios.DataSource = UserModel.GetAllUsers();
        }

        private void LoadUsers()
        {
            // Obtiene los usuarios
            var users = UserModel.GetAllUsers();

            // Invoca el método para actualizar el DataGridView en el hilo principal
            Invoke(new Action(() =>
            {
                dtgUsuarios.DataSource = users;

                // Oculta la columna 'id'
                if (dtgUsuarios.Columns["id"] != null)
                {
                    dtgUsuarios.Columns["id"].Visible = false;
                }
                dtgUsuarios.ClearSelection();
            }));
        }

        

        private async void FrmAdministrarUsuarios_Load(object sender, EventArgs e)
        {


            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadUsers());



            // Eliminar la tabPage de detalle
            tabControl1.TabPages.Remove(tabPageUserDetail);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            // Asegúrate de que el tabPageUserDetail esté agregado al TabControl (solo si no está ya agregado)
            if (!tabControl1.TabPages.Contains(tabPageUserDetail))
            {
                tabControl1.TabPages.Add(tabPageUserDetail);
            }

            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = tabPageUserDetail;
            iconButton5.Text = "Guardar";

        }

        private async void iconButton5_Click(object sender, EventArgs e)
        {
            string usuario = txtUsername.Text;
            string contrasena = "";
            string nombres = txtNombres.Text;
            string apellidos = txtApellidos.Text;
            string correo = txtCorreo.Text;
            bool isAdmin = chckbIsAdmin.Checked;

            // Verificar que el campo de nombre de usuario no esté vacío
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtCont.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmarCont.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                MessageBox.Show("Los campos no pueden estar vacíos.");
            }
            else
            {
                // Verificar si las contraseñas coinciden
                if (txtCont.Text == txtConfirmarCont.Text)
                {
                    try
                    {
                        contrasena = txtConfirmarCont.Text;
                        iconButton5.Enabled = false; // Deshabilitar el botón para evitar múltiples clics

                        if (iconButton5.Text == "Guardar")
                        {
                            // Ejecutar la operación de adición de manera asíncrona
                            await Task.Run(() => UserModel.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo));
                            MessageBox.Show("Usuario agregado exitosamente");
                        }
                        else if (iconButton5.Text == "Actualizar")
                        {
                            // Ejecutar la operación de actualización de manera asíncrona
                            await Task.Run(() => UserModel.UpdateUser(EditarUsuario.idUser, txtUsername.Text, contrasena, nombres, apellidos, isAdmin, correo));
                            MessageBox.Show("Usuario actualizado exitosamente");
                        }

                        // Redirigir a tabPage1
                        tabControl1.SelectedTab = tabPage1;
                        MostrarUsuarios();
                        EliminarTabPage(tabPageUserDetail);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo ingresar el usuario por :" + ex.Message);
                    }
                    finally
                    {
                        iconButton5.Enabled = true; // Volver a habilitar el botón
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden.");
                }
            }
        }


        private void iconButton6_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageUserDetail);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dtgUsuarios.SelectedRows.Count > 0)
            {
                // Ensure the selected row is valid and the "usuario" column exists
                if (dtgUsuarios.CurrentRow != null && dtgUsuarios.CurrentRow.Cells["usuario"].Value != DBNull.Value)
                {
                    // Get the value of the "usuario" column from the selected row
                    string usuario = dtgUsuarios.CurrentRow.Cells["usuario"].Value.ToString();

                    // Call the method to get user details based on the 'usuario'
                    DataTable userDetails = UserModel.GetByValue(usuario);

                    if (userDetails.Rows.Count > 0) // Ensure that user details were found
                    {
                        // Assign the obtained values to the static class EditarUsuario from the first row of the DataTable
                        EditarUsuario.idUser = Convert.ToInt32(userDetails.Rows[0]["id"]);
                        EditarUsuario.usuario = userDetails.Rows[0]["usuario"].ToString();
                        EditarUsuario.nombres = userDetails.Rows[0]["nombres"].ToString();
                        EditarUsuario.apellidos = userDetails.Rows[0]["apellidos"].ToString();
                        EditarUsuario.correo = userDetails.Rows[0]["correo"].ToString();
                        EditarUsuario.contrasena = userDetails.Rows[0]["contrasena"].ToString();
                        EditarUsuario.isAdmin = Convert.ToBoolean(userDetails.Rows[0]["isAdmin"]);

                        // Optionally show a form to edit the user
                        AnadirTabPage(tabPageUserDetail);

                        // Fill the form fields with the user data
                        txtUsername.Text = EditarUsuario.usuario;
                        txtNombres.Text = EditarUsuario.nombres;
                        txtApellidos.Text = EditarUsuario.apellidos;
                        txtCorreo.Text = EditarUsuario.correo;
                        txtCont.Text = EditarUsuario.contrasena;
                        txtConfirmarCont.Text = EditarUsuario.contrasena;
                        chckbIsAdmin.Checked = EditarUsuario.isAdmin;

                        iconButton5.Text = "Actualizar"; // Change the button text for updating
                    }
                    else
                    {
                        // If no user details are found
                        MessageBox.Show("No se encontró el usuario.");
                    }
                }
                else
                {
                    // If the "usuario" column value is DBNull or invalid
                    MessageBox.Show("El usuario seleccionado no tiene un valor válido.");
                }
            }
            else
            {
                // If no row is selected
                MessageBox.Show("Por favor, seleccione una fila.");
            }


        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dtgUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DataTable usuarios=UserModel.GetByValue(textBox1.Text);
                if(usuarios.Rows.Count > 0)
                {
                    dtgUsuarios.DataSource = usuarios;

                    if (dtgUsuarios.Columns["id"] != null)
                    {
                        dtgUsuarios.Columns["id"].Visible = false;
                    }
                    dtgUsuarios.ClearSelection();
                }
                else
                {
                    MessageBox.Show("No existen usuarios con esos datos");
                    LoadUsers();
                }
                
            }
            else
            {
                LoadUsers();
            }
        }
    }
}
