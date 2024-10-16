
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
            tabControl1.TabPages.Remove(tabPageUserDetail);
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
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
            ibtnBuscar.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };
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

        private void FrmAdministrarUsuarios_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
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

        private void iconButton5_Click(object sender, EventArgs e)
        {
            string usuario = txtUsername.Text;
            string contrasena = "";
            string nombres = txtNombres.Text;
            string apellidos = txtApellidos.Text;
            string correo = txtCorreo.Text;
            bool isAdmin = false;

            // Verificar si el checkbox está marcado
            if (chckbIsAdmin.Checked)
            {
                isAdmin = true;
            }

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
                        if (iconButton5.Text == "Guardar")
                        {
                            UserModel.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo);
                            MessageBox.Show("Usuario agregado exitosamente");
                        }
                        else if (iconButton5.Text == "Actualizar")
                        {
                            
                            UserModel.UpdateUser(EditarUsuario.idUser, txtUsername.Text, contrasena, nombres, apellidos, isAdmin, correo);
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
                // Obtener el valor de la celda "usuario" de la fila seleccionada
                string usuario = dtgUsuarios.CurrentRow.Cells["usuario"].Value.ToString();

                // Llamar al método que obtiene los datos del usuario basándose en el campo 'usuario'
                var userDetails = UserModel.GetByValue(usuario);

                if (userDetails.Count > 0) // Asegurarse de que se haya encontrado el usuario
                {
                    // Asignar los valores obtenidos a la clase estática EditarUsuario
                    EditarUsuario.idUser = userDetails[0].id;
                    EditarUsuario.usuario = userDetails[0].usuario;
                    EditarUsuario.nombres = userDetails[0].nombres;
                    EditarUsuario.apellidos = userDetails[0].apellidos;
                    EditarUsuario.correo = userDetails[0].correo;
                    EditarUsuario.contrasena = userDetails[0].contrasena;
                    EditarUsuario.isAdmin = userDetails[0].isAdmin;
                    // Aquí puedes proceder con la lógica adicional que desees
                    // Por ejemplo, mostrar un formulario para editar el usuario
                    AnadirTabPage(tabPageUserDetail);
                    txtUsername.Text = EditarUsuario.usuario;
                    txtNombres.Text=EditarUsuario.nombres;
                    txtApellidos.Text = EditarUsuario.apellidos;
                    txtCorreo.Text = EditarUsuario.correo;
                    txtCont.Text = EditarUsuario.contrasena;
                    txtConfirmarCont.Text = EditarUsuario.contrasena;
                    chckbIsAdmin.Checked = EditarUsuario.isAdmin;
                    iconButton5.Text = "Actualizar";
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un usuario.");
            }

        }
    }
}
