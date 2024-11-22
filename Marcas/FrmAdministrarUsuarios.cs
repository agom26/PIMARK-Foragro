
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using Comun.Cache;
using Dominio;
using MetroFramework;
using MetroFramework.Controls;
using Presentacion.Alertas;

namespace Presentacion
{
    public partial class FrmAdministrarUsuarios : Form
    {
        UserModel UserModel = new UserModel();


        public FrmAdministrarUsuarios()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            dtgUsuarios.CellDoubleClick += dtgUsuarios_CellDoubleClick;
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

        public async Task CargarDatos()
        {
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
                    btnGuardarU.Text = "ACTUALIZAR";
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

        public async Task CargarDatosEliminar()
        {
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
            btnGuardarU.Text = "GUARDAR";
            iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;

        }

        private async void iconButton5_Click(object sender, EventArgs e)
        {

        }


        private void iconButton6_Click(object sender, EventArgs e)
        {

        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            VerificarSeleccionIdUser();
            if (EditarUsuario.idUser > 0)
            {
                await CargarDatos();
                tabControl1.SelectedTab = tabPageUserDetail;
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
            if (txtBuscar.Text != "")
            {
                DataTable usuarios = UserModel.GetByValue(txtBuscar.Text);
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

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageUserDetail);
        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {

        }

        private async void btnGuardarU_Click(object sender, EventArgs e)
        {
            string usuario = txtUsername.Text;
            string contrasena = "";
            string nombres = txtNombres.Text;
            string apellidos = txtApellidos.Text;
            string correo = txtCorreo.Text;
            bool isAdmin = chckbIsAdmin.Checked;


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

                if (txtCont.Text == txtConfirmarCont.Text)
                {
                    try
                    {
                        contrasena = txtConfirmarCont.Text;
                        btnGuardarU.Enabled = false;

                        if (btnGuardarU.Text == "GUARDAR")
                        {
                            await Task.Run(() => UserModel.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo));
                            FrmAlerta alerta = new FrmAlerta("USUARIO AGREGADO CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                        }
                        else if (btnGuardarU.Text == "ACTUALIZAR")
                        {
                            await Task.Run(() => UserModel.UpdateUser(EditarUsuario.idUser, txtUsername.Text, contrasena, nombres, apellidos, isAdmin, correo));

                            FrmAlerta alerta = new FrmAlerta("USUARIO ACTUALIZADO CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                        }


                        tabControl1.SelectedTab = tabPage1;
                        MostrarUsuarios();
                        EliminarTabPage(tabPageUserDetail);
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta("NO SE PUDO INGRESAR EL USUARIO POR :" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();

                    }
                    finally
                    {
                        btnGuardarU.Enabled = true;
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("LAS CONTRASEÑAS NO COINCIDEN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();

                }
            }
        }

        private void btnCancelarU_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageUserDetail);
        }

        private void tabPage1_Resize(object sender, EventArgs e)
        {

        }

        private void btnGuardarU_Click_1(object sender, EventArgs e)
        {

        }

        private async void btnGuardarU_Click_2(object sender, EventArgs e)
        {
            string usuario = txtUsername.Text;
            string contrasena = "";
            string nombres = txtNombres.Text;
            string apellidos = txtApellidos.Text;
            string correo = txtCorreo.Text;
            bool isAdmin = chckbIsAdmin.Checked;


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

                if (txtCont.Text == txtConfirmarCont.Text)
                {
                    try
                    {
                        contrasena = txtConfirmarCont.Text;
                        btnGuardarU.Enabled = false;

                        if (btnGuardarU.Text == "GUARDAR")
                        {
                            await Task.Run(() => UserModel.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo));
                            FrmAlerta alerta = new FrmAlerta("USUARIO AGREGADO CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                        }
                        else if (btnGuardarU.Text == "ACTUALIZAR")
                        {
                            await Task.Run(() => UserModel.UpdateUser(EditarUsuario.idUser, txtUsername.Text, contrasena, nombres, apellidos, isAdmin, correo));

                            FrmAlerta alerta = new FrmAlerta("USUARIO ACTUALIZADO CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                        }


                        tabControl1.SelectedTab = tabPage1;
                        MostrarUsuarios();
                        EliminarTabPage(tabPageUserDetail);
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta("NO SE PUDO INGRESAR EL USUARIO POR :" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();

                    }
                    finally
                    {
                        btnGuardarU.Enabled = true;
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("LAS CONTRASEÑAS NO COINCIDEN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();

                }
            }
        }

        private void btnCancelarU_Click_1(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageUserDetail);
        }

        private async void iconButton4_Click_1(object sender, EventArgs e)
        {
            VerificarSeleccionIdUser();
            if (EditarUsuario.idUser > 0)
            {
                await CargarDatosEliminar();
                FrmAlerta alerta = new FrmAlerta("¿ESTÁ SEGURO DE ELIMINAR AL USUARIO " + EditarUsuario.usuario + "?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                var resultado=alerta.ShowDialog();

                if(resultado== DialogResult.Yes)
                {
                    try
                    {
                        UserModel.RemoveUser(EditarUsuario.idUser, EditarUsuario.usuario, UsuarioActivo.usuario);
                        FrmAlerta alertae = new FrmAlerta("USUARIO ELIMINADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alertae.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alertae = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alertae.ShowDialog();
                    }
                    
                }
                else
                {
                    
                }
            }
           
        }

        private void dtgUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void dtgUsuarios_DoubleClick(object sender, EventArgs e)
        {
            VerificarSeleccionIdUser();
            if (EditarUsuario.idUser > 0)
            {
                await CargarDatos();
                tabControl1.SelectedTab = tabPageUserDetail;
            }
            
        }
    }
}
