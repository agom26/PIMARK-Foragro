using Comun.Cache;
using Dominio;
using FontAwesome.Sharp;
using Presentacion.Alertas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Presentacion.Personas
{
    public partial class FrmAdministrarTitulares : Form
    {
        PersonaModel personaModel = new PersonaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;
        public FrmAdministrarTitulares()
        {
            InitializeComponent();
            this.Load += FrmAdministrarTitulares_Load; // Mueve la lógica de carga aquí
            if (UsuarioActivo.isAdmin == false)
            {
                ibtnEditar.Visible = false;
                btnEliminarTitular.Visible = false;

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
            txtNombreTitular.Text = "";
            txtDireccionTitular.Text = "";
            txtNitTitular.Text = "";
            comboBox1.SelectedItem = -1;
            txtCorreoContacto.Text = "";
            txtTelefonoContacto.Text = "";
            txtNombreContacto.Text = "";
            comboBox1.SelectedIndex = -1;
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


        private async Task LoadTitulares()
        {
            totalRows = personaModel.GetTotalTitulares();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var titulares = await Task.Run(() => personaModel.GetAllTitulares(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgTitulares.DataSource = titulares;

                if (dtgTitulares.Columns["id"] != null)
                {
                    dtgTitulares.Columns["id"].Visible = false;
                    dtgTitulares.ClearSelection();
                }


            }));
        }

        public void Habilitar()
        {
            txtNombreTitular.Enabled = true;
            txtDireccionTitular.Enabled = true;
            txtNitTitular.Enabled = true;
            comboBox1.Enabled = true;
            txtCorreoContacto.Enabled = true;
            txtTelefonoContacto.Enabled = true;
            txtNombreContacto.Enabled = true;
            txtNombreContacto.Enabled = true;
            btnGuardarU.Visible = true;
        }
        public void Deshabilitar()
        {
            txtNombreTitular.Enabled = false;
            txtDireccionTitular.Enabled = false;
            txtNitTitular.Enabled = false;
            comboBox1.Enabled = false;
            txtCorreoContacto.Enabled = false;
            txtTelefonoContacto.Enabled = false;
            txtNombreContacto.Enabled = false;
            txtNombreContacto.Enabled = false;
            btnGuardarU.Visible = false;
        }

        private void VerificarSeleccion()
        {
            if (dtgTitulares.RowCount <= 0)
            {
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditarPersona.idPersona = 0;
                return;
            }
            else
            {
                if (dtgTitulares.SelectedRows.Count > 0)
                {
                    var filaSeleccionada = dtgTitulares.SelectedRows[0];
                    if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                    {
                        int id = Convert.ToInt32(dataRowView["id"]);
                        EditarPersona.idPersona = id;

                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("POR FAVOR SELECCIONE UN TITULAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EditarUsuario.idUser = 0;
                }
            }


        }

        public async Task Editar()
        {
            Habilitar();
            if (dtgTitulares.SelectedRows.Count > 0)
            {
                tabControl1.Visible = false;
                int idPersona = EditarPersona.idPersona;
                btnCambios.Image = Properties.Resources.lapiz;
                btnCambios.Text = "EDITAR";

                var titularDetails = personaModel.GetPersonaById(idPersona);

                if (titularDetails.Count > 0)
                {

                    EditarPersona.idPersona = titularDetails[0].id;
                    EditarPersona.nombre = titularDetails[0].nombre;
                    EditarPersona.direccion = titularDetails[0].direccion;
                    EditarPersona.nit = titularDetails[0].nit;
                    EditarPersona.pais = titularDetails[0].pais;
                    EditarPersona.correo = titularDetails[0].correo;
                    EditarPersona.telefono = titularDetails[0].telefono;
                    EditarPersona.contacto = titularDetails[0].contacto;


                    txtNombreTitular.Text = EditarPersona.nombre;
                    txtDireccionTitular.Text = EditarPersona.direccion;
                    txtNitTitular.Text = EditarPersona.nit;
                    comboBox1.SelectedItem = EditarPersona.pais;
                    txtCorreoContacto.Text = EditarPersona.correo;
                    txtTelefonoContacto.Text = EditarPersona.telefono;
                    txtNombreContacto.Text = EditarPersona.contacto;
                    AnadirTabPage(tabTitularDetail);
                    btnGuardarU.Text = "EDITAR";
                    btnGuardarU.BackColor = Color.FromArgb(96, 149, 241);
                    btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.Pen;
                    tabControl1.Visible = true;
                }
                else
                {
                    MessageBox.Show("No se encontró el titular.");
                }
            }
            else
            {

                //MessageBox.Show("Por favor, seleccione una fila de titular.");
            }
        }

        private void ibtnAgregar_Click(object sender, EventArgs e)
        {
            Habilitar();
            LimpiarCampos();
            // Asegúrate de que el tabPageUserDetail esté agregado al TabControl (solo si no está ya agregado)
            AnadirTabPage(tabTitularDetail);
            btnCambios.Image = Properties.Resources.agregar;
            btnCambios.Text = "AGREGAR";
            btnGuardarU.BackColor = Color.FromArgb(50, 164, 115);
            btnGuardarU.Text = "AGREGAR";
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
        }

        private void btnCancelarTit_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabTitularDetail);
            dtgTitulares.ClearSelection();
        }

        private async void btnGuardarTit_Click(object sender, EventArgs e)
        {

        }


        private async void ibtnEditar_Click(object sender, EventArgs e)
        {
            VerificarSeleccion();
            await Editar();
        }

        private void dtgTitulares_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verificar que una fila válida esté seleccionada
            {
                // Obtener el valor del 'id' de la fila seleccionada
                EditarPersona.idPersona = Convert.ToInt32(dtgTitulares.Rows[e.RowIndex].Cells["id"].Value);

                // Usar el valor del 'id' como desees
                //MessageBox.Show("ID del usuario seleccionado: " + EditarPersona.idPersona);
            }
        }

        private async void FrmAdministrarTitulares_Load(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
            // Cargar titulares en segundo plano
            await Task.Run(() => LoadTitulares());

            // Eliminar la tabPage de detalle
            tabControl1.TabPages.Remove(tabTitularDetail);
        }

        private async void ibtnEliminar_Click(object sender, EventArgs e)
        {
            //Eliminar

            // Verificar si hay un titular seleccionado
            if (dtgTitulares.SelectedRows.Count > 0)
            {
                var userDetails = personaModel.GetPersonaById(EditarPersona.idPersona);

                // Preguntar si el usuario está seguro de eliminar ese Usuario
                DialogResult result = MessageBox.Show(UsuarioActivo.usuario + $" ¿Está seguro de que desea eliminar al titular '{userDetails[0].nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Eliminar el usuario y registrar en el log
                        string currentUser = UsuarioActivo.usuario; // El nombre del usuario que está realizando la eliminación (cambiar según tu sistema)
                        bool isDeleted = personaModel.DeleteTitular(userDetails[0].id, userDetails[0].nombre, currentUser);

                        if (isDeleted)
                        {
                            MessageBox.Show("Titular eliminado correctamente.");
                            await LoadTitulares(); // Actualizar la lista de titulares
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el titular.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar eliminar el titular: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un titular para eliminar.");
            }
        }

        private void dtgTitulares_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {

        }
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = personaModel.GetFilteredTitularesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = personaModel.GetTitularByValue(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgTitulares.DataSource = titulares;
                    if (dtgTitulares.Columns["id"] != null)
                    {
                        dtgTitulares.Columns["id"].Visible = false;
                        dtgTitulares.Columns["tipo"].Visible = false;
                    }
                    dtgTitulares.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN TITULARES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadTitulares();
                }
            }
            else
            {
                await LoadTitulares();
            }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = personaModel.GetFilteredTitularesCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (dtgTitulares.SelectedRows.Count > 0)
            {
                int idPersona = EditarPersona.idPersona;

                var titularDetails = personaModel.GetPersonaById(idPersona);

                if (titularDetails.Count > 0)
                {

                    EditarPersona.idPersona = titularDetails[0].id;
                    EditarPersona.nombre = titularDetails[0].nombre;
                    EditarPersona.direccion = titularDetails[0].direccion;
                    EditarPersona.nit = titularDetails[0].nit;
                    EditarPersona.pais = titularDetails[0].pais;
                    EditarPersona.correo = titularDetails[0].correo;
                    EditarPersona.telefono = titularDetails[0].telefono;
                    EditarPersona.contacto = titularDetails[0].contacto;

                    // Mostrar el formulario de edición con los valores del titular

                    txtNombreTitular.Text = EditarPersona.nombre;
                    txtDireccionTitular.Text = EditarPersona.direccion;
                    txtNitTitular.Text = EditarPersona.nit;
                    comboBox1.SelectedItem = EditarPersona.pais;
                    txtCorreoContacto.Text = EditarPersona.correo;
                    txtTelefonoContacto.Text = EditarPersona.telefono;
                    txtNombreContacto.Text = EditarPersona.contacto;
                    Deshabilitar();
                    AnadirTabPage(tabTitularDetail);
                }
                else
                {
                    MessageBox.Show("No se encontró el titular.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila de titular.");
            }
        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {

        }

        private async void btnGuardarTitular_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private async void btnGuardarU_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreTitular.Text;
            string direccion = txtDireccionTitular.Text;
            string nit = txtNitTitular.Text;
            string pais = comboBox1.SelectedItem?.ToString();
            string correo = txtCorreoContacto.Text;
            string telefono = txtTelefonoContacto.Text;
            string contacto = txtNombreContacto.Text;
            string tipo = "titular";

            // Verificar que el campo de nombre de usuario no esté vacío
            if (string.IsNullOrWhiteSpace(txtNombreTitular.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionTitular.Text) ||
                string.IsNullOrWhiteSpace(txtNitTitular.Text) ||
                string.IsNullOrWhiteSpace(pais) ||
                string.IsNullOrWhiteSpace(txtCorreoContacto.Text) ||
                string.IsNullOrWhiteSpace(txtNombreContacto.Text)
                )
            {
                FrmAlerta alerta = new FrmAlerta("DEBE LLENAR LOS CAMPOS OBLIGATORIOS:\n" +
                    "TITULAR\nDIRECCIÓN\nNIT\nPAÍS\nCORREO\nCONTACTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                //MessageBox.Show("Los campos no pueden estar vacíos.");
            }
            else
            {
                try
                {

                    btnGuardarU.Enabled = false;

                    if (btnGuardarU.Text == "AGREGAR")
                    {

                        await Task.Run(() => personaModel.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo));
                        FrmAlerta alerta = new FrmAlerta("TITULAR AGREGADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        //MessageBox.Show("Titular agregado exitosamente");
                        dtgTitulares.ClearSelection();
                    }
                    else if (btnGuardarU.Text == "EDITAR")
                    {
                        bool update = await Task.Run(() => personaModel.UpdatePersona(EditarPersona.idPersona,
                            txtNombreTitular.Text,
                            txtDireccionTitular.Text,
                            txtNitTitular.Text,
                            pais,
                            txtCorreoContacto.Text,
                            telefono,
                            txtNombreContacto.Text));

                        if (update)
                        {
                            FrmAlerta alerta = new FrmAlerta("TITULAR ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            //MessageBox.Show("Titular actualizado exitosamente");
                            await LoadTitulares(); // Refrescar la lista de titulares
                            EliminarTabPage(tabTitularDetail); // Cerrar el formulario de edición
                            dtgTitulares.ClearSelection();
                        }
                        else
                        {
                            MessageBox.Show("Error al actualizar el titular.");
                        }
                    }

                    // Redirigir a tabPage1
                    tabControl1.SelectedTab = tabListTitulares;
                    await LoadTitulares();
                    EliminarTabPage(tabTitularDetail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo ingresar el titular por :" + ex.Message);
                }
                finally
                {
                    btnGuardarU.Enabled = true;
                }
            }
        }

        private void btnCancelarU_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabTitularDetail);
            dtgTitulares.ClearSelection();
        }

        private async void dtgTitulares_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            VerificarSeleccion();
            await Editar();
        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            await LoadTitulares();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = personaModel.GetFilteredTitularesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                filtrar();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (buscando == true)
            {
                filtrar();
            }
            else
            {
                await LoadTitulares();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (buscando == true)
                {
                    filtrar();
                }
                else
                {
                    await LoadTitulares();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (buscando == true)
                {
                    filtrar();
                }
                else
                {
                    await LoadTitulares();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (buscando == true)
            {
                filtrar();
            }
            else
            {
                await LoadTitulares();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void lblTotalPages_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {

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



        private void FrmAdministrarTitulares_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private async void btnEliminarAgente_Click(object sender, EventArgs e)
        {
            VerificarSeleccion();
            if (dtgTitulares.SelectedRows.Count > 0)
            {
                var userDetails = personaModel.GetPersonaById(EditarPersona.idPersona);


                DialogResult result = MessageBox.Show(UsuarioActivo.usuario + $" ¿Está seguro de que desea eliminar al titular'{userDetails[0].nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {

                        string currentUser = UsuarioActivo.usuario;
                        bool isDeleted = personaModel.DeleteAgente(userDetails[0].id, userDetails[0].nombre, currentUser);

                        if (isDeleted)
                        {
                            FrmAlerta alerta = new FrmAlerta("TITULAR ELIMINADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.None);
                            alerta.ShowDialog();
                            await LoadTitulares();
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("ERROR AL ELIMINAR AL TITULAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar eliminar el titular: " + ex.Message+"\nEs posible que se encuentre asociada a una marca/patente.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un agente para eliminar.");
            }
        }
    }
}
