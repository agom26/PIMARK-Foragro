
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
            btnGuardarUsuario.Text = "GUARDAR";
            iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;

        }

        private async void iconButton5_Click(object sender, EventArgs e)
        {

        }


        private void iconButton6_Click(object sender, EventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            VerificarSeleccionIdUser();
            if (EditarUsuario.idUser > 0)
            {
                iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.Pen;
                int idUser = EditarUsuario.idUser;


                DataTable userDetails = UserModel.GetById(idUser);

                if (userDetails.Rows.Count > 0)
                {
                    DataRow row = userDetails.Rows[0];


                    EditarUsuario.nombres = row["nombres"].ToString();
                    EditarUsuario.apellidos = row["apellidos"].ToString();
                    EditarUsuario.usuario = row["usuario"].ToString();
                    EditarUsuario.contrasena = row["contrasena"].ToString();
                    EditarUsuario.correo = row["correo"].ToString();
                    EditarUsuario.isAdmin = Convert.ToBoolean(row["isAdmin"]);


                    txtNombres.Text = EditarUsuario.nombres;
                    txtUsername.Text = EditarUsuario.usuario;
                    txtApellidos.Text = EditarUsuario.apellidos;
                    txtCorreo.Text = EditarUsuario.correo;
                    txtCont.Text = EditarUsuario.contrasena;
                    txtConfirmarCont.Text = EditarUsuario.contrasena;
                    chckbIsAdmin.Checked = EditarUsuario.isAdmin;
                    btnGuardarUsuario.Text = "ACTUALIZAR";
                    AnadirTabPage(tabPageUserDetail);
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario.");
                }
            }
            else
            {
                //MessageBox.Show("Por favor, seleccione una fila.");
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
        private void VerificarSeleccionIdUser()
        {
            if (dtgUsuarios.RowCount <= 0)
            {
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditarUsuario.idUser = 0;
                return;
            }
            else
            {
                if (dtgUsuarios.SelectedRows.Count > 0)
                {
                    var filaSeleccionada = dtgUsuarios.SelectedRows[0];
                    if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                    {
                        int id = Convert.ToInt32(dataRowView["id"]);
                        EditarUsuario.idUser = id;
                        tabControl1.SelectedTab = tabPageUserDetail;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EditarUsuario.idUser = 0;
                }
            }


        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DataTable usuarios = UserModel.GetByValue(textBox1.Text);
                if (usuarios.Rows.Count > 0)
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

        private void dtgUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegúrate de que el índice de la fila es válido (no en el encabezado)
            if (e.RowIndex >= 0)
            {

            }
            else
            {
                //MessageBox.Show("Por favor, selecciona una fila válida.");
            }

        }

        private async void btnGuardarTitular_Click(object sender, EventArgs e)
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
                        btnGuardarUsuario.Enabled = false; // Deshabilitar el botón para evitar múltiples clics

                        if (btnGuardarUsuario.Text == "GUARDAR")
                        {
                            // Ejecutar la operación de adición de manera asíncrona
                            await Task.Run(() => UserModel.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo));
                            MessageBox.Show("Usuario agregado exitosamente");
                        }
                        else if (btnGuardarUsuario.Text == "ACTUALIZAR")
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
                        btnGuardarUsuario.Enabled = true; // Volver a habilitar el botón
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden.");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageUserDetail);
        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageUserDetail);
        }
    }
}
