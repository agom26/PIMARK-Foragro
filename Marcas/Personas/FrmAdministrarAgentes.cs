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
            txtPais.Text = "";
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

        private void ibtnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            // Asegúrate de que el tabPageUserDetail esté agregado al TabControl (solo si no está ya agregado)
            if (!tabControl1.TabPages.Contains(tabPageAgenteDetail))
            {
                tabControl1.TabPages.Add(tabPageAgenteDetail);
            }

            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = tabPageAgenteDetail;
            btnGuardarTit.Text = "Guardar";
        }

        private async void btnGuardarTit_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreAgente.Text;
            string direccion = txtDireccionAgente.Text;
            string nit = txtNitAgente.Text;
            string pais = txtPais.Text;
            string correo = txtCorreoContacto.Text;
            string telefono = txtTelefonoContacto.Text;
            string contacto = txtNombreContacto.Text;
            string tipo = "agente";

            // Verificar que el campo de nombre de usuario no esté vacío
            if (string.IsNullOrWhiteSpace(txtNombreAgente.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionAgente.Text) ||
                string.IsNullOrWhiteSpace(txtNitAgente.Text) ||
                string.IsNullOrWhiteSpace(txtPais.Text) ||
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
                    btnGuardarTit.Enabled = false; // Deshabilitar el botón para evitar múltiples clics

                    if (btnGuardarTit.Text == "Guardar")
                    {
                        // Ejecutar la operación de adición de manera asíncrona
                        await Task.Run(() => personaModel.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo));
                        MessageBox.Show("Agente agregado exitosamente");
                    }
                    else if (btnGuardarTit.Text == "Actualizar")
                    {
                        try
                        {
                            // Ejecutar la operación de actualización de manera asíncrona
                            bool update = await Task.Run(() => personaModel.UpdatePersona(EditarPersona.idPersona,
                                txtNombreAgente.Text,
                                txtDireccionAgente.Text,
                                txtNitAgente.Text,
                                txtPais.Text,
                                txtCorreoContacto.Text,
                                txtTelefonoContacto.Text,
                                txtNombreContacto.Text));

                            if (update)
                            {
                                MessageBox.Show("Agente actualizado exitosamente");
                                MostrarAgentes(); // Refrescar la lista de agentes
                                EliminarTabPage(tabPageAgenteDetail); // Cerrar el formulario de edición
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

                    // Redirigir a tabPageListado
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
                    btnGuardarTit.Enabled = true; // Volver a habilitar el botón
                }
            }
        }


        private async void FrmAdministrarAgentes_Load(object sender, EventArgs e)
        {



            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadAgentes());



            // Eliminar la tabPage de detalle
            tabControl1.TabPages.Remove(tabPageAgenteDetail);


        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            if (dtgAgentes.SelectedRows.Count > 0)
            {
                // Obtener el valor del 'id' de la fila seleccionada
                int idPersona = EditarPersona.idPersona;

                // Llamar al método que obtiene los datos del titular basándose en el campo 'idPersona'
                var titularDetails = personaModel.GetPersonaById(idPersona);

                if (titularDetails.Count > 0) // Asegurarse de que se haya encontrado el titular
                {
                    // Asignar los valores obtenidos a la clase estática EditarPersona
                    EditarPersona.idPersona = titularDetails[0].id;
                    EditarPersona.nombre = titularDetails[0].nombre;
                    EditarPersona.direccion = titularDetails[0].direccion;
                    EditarPersona.nit = titularDetails[0].nit;
                    EditarPersona.pais = titularDetails[0].pais;
                    EditarPersona.correo = titularDetails[0].correo;
                    EditarPersona.telefono = titularDetails[0].telefono;
                    EditarPersona.contacto = titularDetails[0].contacto;
                    // Mostrar el formulario de edición con los valores del titular
                    AnadirTabPage(tabPageAgenteDetail);
                    txtNombreAgente.Text = EditarPersona.nombre;
                    txtDireccionAgente.Text = EditarPersona.direccion;
                    txtNitAgente.Text = EditarPersona.nit;
                    txtPais.Text = EditarPersona.pais;
                    txtCorreoContacto.Text = EditarPersona.correo;
                    txtTelefonoContacto.Text = EditarPersona.telefono;
                    txtNombreContacto.Text = EditarPersona.contacto;
                    btnGuardarTit.Text = "Actualizar"; // Cambiar el texto del botón a "Actualizar"
                }
                else
                {
                    MessageBox.Show("No se encontró el agente.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un agente.");
            }
        }

        private void dtgAgentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verificar que una fila válida esté seleccionada
            {
                // Obtener el valor del 'id' de la fila seleccionada
                EditarPersona.idPersona = Convert.ToInt32(dtgAgentes.Rows[e.RowIndex].Cells["id"].Value);

                // Usar el valor del 'id' como desees
                //MessageBox.Show("ID del usuario seleccionado: " + EditarPersona.idPersona);
            }
        }

        private void ibtnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay un titular seleccionado
            if (dtgAgentes.SelectedRows.Count > 0)
            {
                var userDetails = personaModel.GetPersonaById(EditarPersona.idPersona);

                // Preguntar si el usuario está seguro de eliminar ese agente
                DialogResult result = MessageBox.Show(UsuarioActivo.usuario + $" ¿Está seguro de que desea eliminar al agente '{userDetails[0].nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Eliminar el usuario y registrar en el log
                        string currentUser = UsuarioActivo.usuario; // El nombre del usuario que está realizando la eliminación
                        bool isDeleted = personaModel.DeleteAgente(userDetails[0].id, userDetails[0].nombre, currentUser);

                        if (isDeleted)
                        {
                            MessageBox.Show("Agente eliminado correctamente.");
                            MostrarAgentes(); // Actualizar la lista de agentes
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
            EliminarTabPage(tabPageAgenteDetail);
        }

        private void dtgAgentes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
