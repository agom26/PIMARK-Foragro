using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Personas
{
    public partial class FrmAdministrarAgentes : Form
    {
        PersonaModel personaModel = new PersonaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;
        public FrmAdministrarAgentes()
        {
            InitializeComponent();
            this.Load += FrmAdministrarAgentes_Load; // Mueve la lógica de carga aquí
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
            txtNombreAgente.Text = "";
            txtDireccionAgente.Text = "";
            txtNitAgente.Text = "";
            comboBox1.SelectedIndex = -1;
            txtCorreoContacto.Text = "";
            txtTelefonoContacto.Text = "";
            txtNombreContacto.Text = "";
        }

        private async Task LoadAgentes()
        {
            totalRows = personaModel.GetTotalAgentes();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var titulares = await Task.Run(() => personaModel.GetAllAgentes(currentPageIndex, pageSize));
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                Invoke(new Action(() =>
                {
                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();
                    dtgAgentes.DataSource = titulares;

                    if (dtgAgentes.Columns["id"] != null)
                    {
                        dtgAgentes.Columns["id"].Visible = false;
                        dtgAgentes.ClearSelection();
                    }


                }));
            }

               
        }
        private async Task AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }
            tabControl1.SelectedTab = nombre;


        }

        public void Habilitar()
        {
            txtNombreAgente.Enabled = true;
            txtDireccionAgente.Enabled = true;
            txtNitAgente.Enabled = true;
            comboBox1.Enabled = true;
            txtCorreoContacto.Enabled = true;
            txtTelefonoContacto.Enabled = true;
            txtNombreContacto.Enabled = true;
            txtNombreContacto.Enabled = true;
            btnGuardarU.Visible = true;
        }
        public void Deshabilitar()
        {
            txtNombreAgente.Enabled = false;
            txtDireccionAgente.Enabled = false;
            txtNitAgente.Enabled = false;
            comboBox1.Enabled = false;
            txtCorreoContacto.Enabled = false;
            txtTelefonoContacto.Enabled = false;
            txtNombreContacto.Enabled = false;
            txtNombreContacto.Enabled = false;
            btnGuardarU.Visible = false;
        }

        private async void ibtnAgregar_Click(object sender, EventArgs e)
        {
            Habilitar();
            tabControl1.Visible = false;
            LimpiarCampos();
            await AnadirTabPage(tabPageAgenteDetail);

            btnGuardarU.Text = "AGREGAR";
            btnGuardarU.BackColor = Color.FromArgb(50, 164, 115);
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnCambios.Image = Properties.Resources.agregar;
            btnCambios.Text = "AGREGAR";
            tabControl1.Visible = true;
        }

        private void VerificarSeleccion()
        {
            if (dtgAgentes.RowCount <= 0)
            {
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditarPersona.idPersona = 0;
                return;
            }
            else
            {
                if (dtgAgentes.SelectedRows.Count > 0)
                {
                    var filaSeleccionada = dtgAgentes.SelectedRows[0];
                    if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                    {
                        int id = Convert.ToInt32(dataRowView["id"]);
                        EditarPersona.idPersona = id;

                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("POR FAVOR SELECCIONE UN AGENTE", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EditarUsuario.idUser = 0;
                }
            }


        }

        public async Task Editar()
        {

            Habilitar();
            if (dtgAgentes.SelectedRows.Count > 0)
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

                    txtNombreAgente.Text = EditarPersona.nombre;
                    txtDireccionAgente.Text = EditarPersona.direccion;
                    txtNitAgente.Text = EditarPersona.nit;
                    comboBox1.SelectedItem = EditarPersona.pais;
                    txtCorreoContacto.Text = EditarPersona.correo;
                    txtTelefonoContacto.Text = EditarPersona.telefono;
                    txtNombreContacto.Text = EditarPersona.contacto;
                    btnGuardarU.Text = "EDITAR";
                    btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.Pen;
                    btnGuardarU.BackColor = Color.FromArgb(96, 149, 241);
                    AnadirTabPage(tabPageAgenteDetail);
                    tabControl1.Visible = true;
                }
                else
                {
                    MessageBox.Show("No se encontró el agente.");
                }
            }
            else
            {

                //MessageBox.Show("Por favor, seleccione una fila de agente.");
            }

        }

        private async void btnGuardarTit_Click(object sender, EventArgs e)
        {

        }


        private async void FrmAdministrarAgentes_Load(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
            tabControl1.Visible = false;
            await LoadAgentes();
            EliminarTabPage(tabPageAgenteDetail);
            tabControl1.Visible = true;
        }

        private async void ibtnEditar_Click(object sender, EventArgs e)
        {
            VerificarSeleccion();
            await Editar();


        }

        private void dtgAgentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void ibtnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay un titular seleccionado
            if (dtgAgentes.SelectedRows.Count > 0)
            {
                var userDetails = personaModel.GetPersonaById(EditarPersona.idPersona);


                DialogResult result = MessageBox.Show(UsuarioActivo.usuario + $" ¿Está seguro de que desea eliminar al agente '{userDetails[0].nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {

                        string currentUser = UsuarioActivo.usuario;
                        bool isDeleted = personaModel.DeleteAgente(userDetails[0].id, userDetails[0].nombre, currentUser);

                        if (isDeleted)
                        {
                            MessageBox.Show("Agente eliminado correctamente.");
                            await LoadAgentes();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el agente.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar eliminar el agente: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un agente para eliminar.");
            }
        }

        private void btnCancelarTit_Click(object sender, EventArgs e)
        {

        }

        private void dtgAgentes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void tabPageListado_Click(object sender, EventArgs e)
        {

        }
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = personaModel.GetFilteredAgentesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = personaModel.GetAgenteByValue(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgAgentes.DataSource = titulares;
                    if (dtgAgentes.Columns["id"] != null)
                    {
                        dtgAgentes.Columns["id"].Visible = false;
                        dtgAgentes.Columns["tipo"].Visible = false;
                    }
                    dtgAgentes.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN AGENTES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadAgentes();
                }
            }
            else
            {
                await LoadAgentes();
            }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = personaModel.GetFilteredAgentesCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (dtgAgentes.SelectedRows.Count > 0)
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

                    txtNombreAgente.Text = EditarPersona.nombre;
                    txtDireccionAgente.Text = EditarPersona.direccion;
                    txtNitAgente.Text = EditarPersona.nit;
                    comboBox1.SelectedItem = EditarPersona.pais;
                    txtCorreoContacto.Text = EditarPersona.correo;
                    txtTelefonoContacto.Text = EditarPersona.telefono;
                    txtNombreContacto.Text = EditarPersona.contacto;
                    Deshabilitar();

                }
                else
                {
                    MessageBox.Show("No se encontró el agente.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila de agente.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private async void btnGuardarU_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreAgente.Text;
            string direccion = txtDireccionAgente.Text;
            string nit = txtNitAgente.Text;
            string pais = comboBox1.SelectedItem?.ToString();
            string correo = txtCorreoContacto.Text;
            string telefono = txtTelefonoContacto.Text;
            string contacto = txtNombreContacto.Text;
            string tipo = "agente";

            // Verificar que el campo de nombre de usuario no esté vacío
            if (string.IsNullOrWhiteSpace(txtNombreAgente.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionAgente.Text) ||
                string.IsNullOrWhiteSpace(txtNitAgente.Text) ||
                string.IsNullOrWhiteSpace(pais) ||
                string.IsNullOrWhiteSpace(txtCorreoContacto.Text) ||
                string.IsNullOrWhiteSpace(txtNombreContacto.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoContacto.Text))
            {
                FrmAlerta alerta = new FrmAlerta("DEBE LLENAR LOS CAMPOS OBLIGATORIOS:\nNOMBRE\nDIRECCION\n" +
                    "NIT\nPAIS\nCORREO\nCONTACTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
            else
            {
                try
                {
                    btnGuardarU.Enabled = false; // Deshabilitar el botón para evitar múltiples clics

                    if (btnGuardarU.Text == "AGREGAR")
                    {
                        // Ejecutar la operación de adición de manera asíncrona
                        await Task.Run(() => personaModel.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo));
                        FrmAlerta alerta = new FrmAlerta("AGENTE AGREGADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        EliminarTabPage(tabPageAgenteDetail);
                        tabControl1.SelectedTab = tabPageListado;
                        await LoadAgentes();
                        dtgAgentes.ClearSelection();
                        //MessageBox.Show("Agente agregado exitosamente");
                    }
                    else if (btnGuardarU.Text == "EDITAR")
                    {
                        try
                        {
                            // Ejecutar la operación de actualización de manera asíncrona
                            bool update = await Task.Run(() => personaModel.UpdatePersona(EditarPersona.idPersona,
                                txtNombreAgente.Text,
                                txtDireccionAgente.Text,
                                txtNitAgente.Text,
                                pais,
                                txtCorreoContacto.Text,
                                txtTelefonoContacto.Text,
                                txtNombreContacto.Text));

                            if (update)
                            {
                                FrmAlerta alerta = new FrmAlerta("AGENTE ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("Agente actualizado exitosamente");
                                await LoadAgentes();
                                EliminarTabPage(tabPageAgenteDetail);
                                tabControl1.SelectedTab = tabPageListado;
                                dtgAgentes.ClearSelection();
                            }
                            else
                            {
                                MessageBox.Show("Error al actualizar el agente.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al actualizar el agente: " + ex.Message);
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo ingresar el agente por :" + ex.Message);
                }
                finally
                {
                    btnGuardarU.Enabled = true;
                }
            }
        }

        private void btnCancelarU_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageAgenteDetail);
            dtgAgentes.ClearSelection();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void dtgAgentes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            await Editar();
            if (EditarPersona.idPersona > 0)
            {
                //await AnadirTabPage(tabPageAgenteDetail);
            }
        }

        private async void dtgAgentes_DoubleClick(object sender, EventArgs e)
        {
            buscando = false;
            VerificarSeleccion();
            await Editar();
            if (EditarPersona.idPersona > 0)
            {
                //await AnadirTabPage(tabPageAgenteDetail);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            buscando = false;
            await LoadAgentes();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = personaModel.GetFilteredAgentesCount(txtBuscar.Text);
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
            if (buscando==true)
            {
                filtrar();
            }
            else
            {
                await LoadAgentes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (buscando==true)
                {
                    filtrar();
                }
                else
                {
                    await LoadAgentes();
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
                    await LoadAgentes();
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
                await LoadAgentes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void lblTotalRows_Click(object sender, EventArgs e)
        {

        }
    }
}
