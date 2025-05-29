
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
using Org.BouncyCastle.Bcpg.OpenPgp;
using Presentacion.Alertas;

namespace Presentacion
{
    public partial class FrmAdministrarUsuarios : Form
    {
        UserModel UserModel = new UserModel();
        private const int pageSize = 10;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = true;

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



        private async Task LoadUsers()
        {
            totalRows = UserModel.GetTotalUsers();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                // Invoca el método para actualizar tanto lblTotalPages como dtgUsuarios en el hilo principal
                Invoke(new Action(() =>
            {
                // Actualizar el texto de lblTotalPages en el hilo principal
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                // Obtiene los usuarios
                var users = UserModel.GetAllUsers(currentPageIndex, pageSize);

                // Actualiza el DataGridView
                dtgUsuarios.DataSource = users;

                // Oculta la columna 'id'
                if (dtgUsuarios.Columns["id"] != null)
                {
                    dtgUsuarios.Columns["id"].Visible = false;
                }
                dtgUsuarios.ClearSelection();

                // Centrar la columna 'Administrador'
                if (dtgUsuarios.Columns["Administrador"] != null)
                {
                    dtgUsuarios.Columns["Administrador"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dtgUsuarios.Columns["Administrador"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }));
            }
        }


        public async Task CargarDatos()
        {
            if (EditarUsuario.idUser > 0)
            {
                btnCambios.Text = "EDITAR";
                btnCambios.Image = Properties.Resources.lapiz;
                int idUser = EditarUsuario.idUser;


                DataTable userDetails = await Task.Run(() => UserModel.GetById(idUser));

                if (userDetails.Rows.Count > 0)
                {
                    DataRow row = userDetails.Rows[0];


                    EditarUsuario.nombres = row["nombres"].ToString();
                    EditarUsuario.apellidos = row["apellidos"].ToString();
                    EditarUsuario.usuario = row["usuario"].ToString();
                    //EditarUsuario.contrasena = row["contrasena"].ToString();
                    EditarUsuario.correo = row["correo"].ToString();
                    EditarUsuario.isAdmin = Convert.ToBoolean(row["isAdmin"]);


                    txtNombres.Text = EditarUsuario.nombres;
                    txtUsername.Text = EditarUsuario.usuario;
                    txtApellidos.Text = EditarUsuario.apellidos;
                    txtCorreo.Text = EditarUsuario.correo;
                    //txtCont.Text = EditarUsuario.contrasena;
                    //txtConfirmarCont.Text = EditarUsuario.contrasena;
                    txtCont.Text = "";
                    txtConfirmarCont.Text = "";
                    chckbIsAdmin.Checked = EditarUsuario.isAdmin;
                    btnGuardarU.Text = "EDITAR";
                    btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.Pen;

                    btnGuardarU.BackColor = Color.FromArgb(96, 149, 241);
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

                int idUser = EditarUsuario.idUser;


                DataTable userDetails = await Task.Run(() => UserModel.GetById(idUser));

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


            currentPageIndex = 1;
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadUsers());
            // Asegúrate de que el DataGridView esté configurado correctamente

            lblCurrentPage.Text = currentPageIndex.ToString();
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
            chkCambiarContrasena.Visible = false;
            chkCambiarContrasena.Checked = false;
            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = tabPageUserDetail;
            btnGuardarU.Text = "AGREGAR";
            btnGuardarU.BackColor = Color.FromArgb(50, 164, 115);
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnCambios.Text = "AGREGAR";
            btnCambios.Image = Properties.Resources.agregar;

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
                chkCambiarContrasena.Visible = true;
                chkCambiarContrasena.Checked = false;
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
                    FrmAlerta alerta = new FrmAlerta("POR FAVOR SELECCIONE UN USUARIO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EditarUsuario.idUser = 0;
                }
            }


        }

        public async void FiltrarUsuarios()
        {
            if (txtBuscar.Text != "")
            {
                totalRows = UserModel.GetFilteredUserCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                DataTable usuarios = UserModel.GetByValue(txtBuscar.Text, currentPageIndex, pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
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
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN USUARIOS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen usuarios con esos datos");
                    await LoadUsers();
                }

            }
            else
            {
                await LoadUsers();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            FiltrarUsuarios();
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
                FrmAlerta alerta = new FrmAlerta("DEBE LLENAR TODOS LOS CAMPOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
            else
            {

                if (txtCont.Text == txtConfirmarCont.Text)
                {
                    try
                    {
                        contrasena = txtConfirmarCont.Text;
                        btnGuardarU.Enabled = false;

                        if (btnGuardarU.Text == "AGREGAR")
                        {
                            await Task.Run(() => UserModel.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo));
                            FrmAlerta alerta = new FrmAlerta("USUARIO AGREGADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                        }
                        else if (btnGuardarU.Text == "EDITAR")
                        {
                            bool cambiarContra = chkCambiarContrasena.Checked;
                            await Task.Run(() => UserModel.UpdateUserSecure(EditarUsuario.idUser, txtUsername.Text, contrasena, nombres, apellidos, isAdmin, correo, cambiarContra));

                            FrmAlerta alerta = new FrmAlerta("USUARIO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                        }


                        tabControl1.SelectedTab = tabPage1;
                        LoadUsers();
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
                FrmAlerta alerta = new FrmAlerta("DEBE LLENAR TODOS LOS CAMPOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Los campos no pueden estar vacíos.");
            }
            else
            {

                if (txtCont.Text == txtConfirmarCont.Text)
                {
                    try
                    {
                        contrasena = txtConfirmarCont.Text;
                        btnGuardarU.Enabled = false;

                        if (btnGuardarU.Text == "AGREGAR")
                        {
                            await Task.Run(() => UserModel.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo));
                            FrmAlerta alerta = new FrmAlerta("USUARIO AGREGADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                        }
                        else if (btnGuardarU.Text == "EDITAR")
                        {
                            bool cambiarContra = chkCambiarContrasena.Checked;
                            await Task.Run(() => UserModel.UpdateUserSecure(EditarUsuario.idUser, txtUsername.Text, contrasena, nombres, apellidos, isAdmin, correo, cambiarContra));

                            FrmAlerta alerta = new FrmAlerta("USUARIO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                        }


                        await LoadUsers();
                        AnadirTabPage(tabPage1);
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
                int numAdmin = await Task.Run(() => UserModel.CountAdmins());

                if (numAdmin > 1)
                {
                    await CargarDatosEliminar();
                    FrmAlerta alerta = new FrmAlerta("¿ESTÁ SEGURO DE ELIMINAR AL USUARIO " + EditarUsuario.usuario + "?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    var resultado = alerta.ShowDialog();

                    if (resultado == DialogResult.Yes)
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
                else if (numAdmin == 1)
                {
                    FrmAlerta alerta = new FrmAlerta("NO ES POSIBLE ELIMINAR AL ÚNICO ADMINISTRADOR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();

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
                buscando = false;
                await CargarDatos();
                tabControl1.SelectedTab = tabPageUserDetail;
            }

        }

        private void iconButton6_Click_1(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            FiltrarUsuarios();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FiltrarUsuarios();
            }

        }

        private void CentrarPanel()
        {
            int anchoMinimo = panelBusqueda.Width + 100;

            if (tabControl1.ClientSize.Width >= anchoMinimo)
            {
                // Pantalla suficientemente ancha → centrar
                panelBusqueda.Anchor = AnchorStyles.None;

                int x = (tabControl1.ClientSize.Width - panelBusqueda.Width) / 2;
                int y = 0; // o donde quieras posicionarlo verticalmente
                panelBusqueda.Location = new Point(x, y);
            }
            else
            {
                // Pantalla pequeña → top-left
                panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panelBusqueda.Location = new Point(0, 0); // o donde quieras
            }
        }
        private async void iconButton1_Click_1(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = UserModel.GetFilteredUserCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            FiltrarUsuarios();

        }

        private void iconButton6_Click_2(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            FiltrarUsuarios();
        }

        private void txtBuscar_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = UserModel.GetFilteredUserCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                FiltrarUsuarios();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (buscando == true)
            {
                FiltrarUsuarios();
            }
            else
            {
                await LoadUsers();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (buscando == true)
                {
                    FiltrarUsuarios();
                }
                else
                {
                    await LoadUsers();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (buscando == true)
                {
                    FiltrarUsuarios();
                }
                else
                {
                    await LoadUsers();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (buscando == true)
            {
                FiltrarUsuarios();
            }
            else
            {
                await LoadUsers();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void FrmAdministrarUsuarios_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }
    }
}
