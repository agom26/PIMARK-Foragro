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

namespace Presentacion.Personas
{
    public partial class FrmAdministrarAgentes : Form
    {
        PersonaModel personaModel = new PersonaModel();
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
        private void MostrarAgentes()
        {
            dtgAgentes.DataSource = personaModel.GetAllAgentes();
            // Ocultar la columna 'id'
            if (dtgAgentes.Columns["id"] != null)
            {
                dtgAgentes.Columns["id"].Visible = false;

                // Desactiva la selección automática de la primera fila
                dtgAgentes.ClearSelection();
            }
        }
        private void LoadAgentes()
        {
            // Obtiene los usuarios
            var agentes = personaModel.GetAllAgentes();

            // Invoca el método para actualizar el DataGridView en el hilo principal
            Invoke(new Action(() =>
            {
                dtgAgentes.DataSource = agentes;

                // Oculta la columna 'id'
                if (dtgAgentes.Columns["id"] != null)
                {
                    dtgAgentes.Columns["id"].Visible = false;
                    // Desactiva la selección automática de la primera fila
                    dtgAgentes.ClearSelection();
                }
            }));
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

        private void ibtnAgregar_Click(object sender, EventArgs e)
        {
            Habilitar();
            LimpiarCampos();

            // Asegúrate de que el tabPageUserDetail esté agregado al TabControl (solo si no está ya agregado)
            if (!tabControl1.TabPages.Contains(tabPageAgenteDetail))
            {
                tabControl1.TabPages.Add(tabPageAgenteDetail);
            }

            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = tabPageAgenteDetail;
            btnGuardarU.Text = "GUARDAR";
            iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
        }

        private async void btnGuardarTit_Click(object sender, EventArgs e)
        {

        }


        private async void FrmAdministrarAgentes_Load(object sender, EventArgs e)
        {

            await Task.Run(() => LoadAgentes());
            tabControl1.TabPages.Remove(tabPageAgenteDetail);
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Habilitar();
            if (dtgAgentes.SelectedRows.Count > 0)
            {
                int idPersona = EditarPersona.idPersona;
                iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.Pen;
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
                    btnGuardarU.Text = "ACTUALIZAR";
                    AnadirTabPage(tabPageAgenteDetail);
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

        private void dtgAgentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditarPersona.idPersona = Convert.ToInt32(dtgAgentes.Rows[e.RowIndex].Cells["id"].Value);
            }
        }

        private void ibtnEliminar_Click(object sender, EventArgs e)
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
                            MostrarAgentes();
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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string buscar = textBox1.Text;
            if (buscar != "")
            {
                DataTable agentes = personaModel.GetAgenteByValue(buscar);
                if (agentes.Rows.Count > 0)
                {
                    dtgAgentes.DataSource = agentes;
                    if (dtgAgentes.Columns["id"] != null)
                    {
                        dtgAgentes.Columns["id"].Visible = false;
                        dtgAgentes.Columns["tipo"].Visible = false;
                    }
                    dtgAgentes.ClearSelection();
                }
                else
                {
                    MessageBox.Show("No existen agentes con esos datos");
                    LoadAgentes();
                }
            }
            else
            {
                LoadAgentes();
            }
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
                    AnadirTabPage(tabPageAgenteDetail);
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
                MessageBox.Show("Los campos no pueden estar vacíos.");
            }
            else
            {
                try
                {
                    btnGuardarU.Enabled = false; // Deshabilitar el botón para evitar múltiples clics

                    if (btnGuardarU.Text == "GUARDAR")
                    {
                        // Ejecutar la operación de adición de manera asíncrona
                        await Task.Run(() => personaModel.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo));
                        MessageBox.Show("Agente agregado exitosamente");
                    }
                    else if (btnGuardarU.Text == "ACTUALIZAR")
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
                                MessageBox.Show("Agente actualizado exitosamente");
                                MostrarAgentes();
                                EliminarTabPage(tabPageAgenteDetail);
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

                    tabControl1.SelectedTab = tabPageListado;
                    MostrarAgentes();
                    EliminarTabPage(tabPageAgenteDetail);
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
    }
}
