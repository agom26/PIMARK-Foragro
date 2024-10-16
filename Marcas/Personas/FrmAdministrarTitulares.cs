using Comun.Cache;
using Dominio;
using Dominio;
using FontAwesome.Sharp;
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
        public FrmAdministrarTitulares()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(tabTitularDetail);
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
            txtPais.Text = "";
            txtCorreoContacto.Text = "";
            txtTelefonoContacto.Text = "";
            txtNombreContacto.Text = "";
        }
        private void MostrarTitulares()
        {
            dtgTitulares.DataSource = personaModel.GetAllTitulares();
            // Ocultar la columna 'id'
            if (dtgTitulares.Columns["id"] != null)
            {
                dtgTitulares.Columns["id"].Visible = false;
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

        private void ibtnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            // Asegúrate de que el tabPageUserDetail esté agregado al TabControl (solo si no está ya agregado)
            if (!tabControl1.TabPages.Contains(tabTitularDetail))
            {
                tabControl1.TabPages.Add(tabTitularDetail);
            }

            // Muestra el TabPage especificado (lo selecciona)
            tabControl1.SelectedTab = tabTitularDetail;
            btnGuardarTit.Text = "Guardar";
        }

        private void btnCancelarTit_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabTitularDetail);
        }

        private void btnGuardarTit_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreTitular.Text;
            string direccion = txtDireccionTitular.Text;
            string nit = txtNitTitular.Text;
            string pais = txtPais.Text;
            string correo = txtCorreoContacto.Text;
            string telefono = txtTelefonoContacto.Text;
            string contacto = txtNombreContacto.Text;
            string tipo = "titular";


            // Verificar que el campo de nombre de usuario no esté vacío
            if (string.IsNullOrWhiteSpace(txtNombreTitular.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionTitular.Text) ||
                string.IsNullOrWhiteSpace(txtNitTitular.Text) ||
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

                    if (btnGuardarTit.Text == "Guardar")
                    {

                        personaModel.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo);
                        MessageBox.Show("Titular agregado exitosamente");
                    }
                    else if (btnGuardarTit.Text == "Actualizar")
                    {
                        try
                        {
                            // Llamar al método para actualizar el titular
                            bool update = personaModel.UpdatePersona(EditarPersona.idPersona,
                                txtNombreTitular.Text,
                                txtDireccionTitular.Text,
                                txtNitTitular.Text,
                                txtPais.Text,
                                txtCorreoContacto.Text,
                                txtTelefonoContacto.Text,
                                txtNombreContacto.Text);

                            if (update)
                            {
                                MessageBox.Show("Titular actualizado exitosamente");
                                MostrarTitulares(); // Refrescar la lista de titulares
                                EliminarTabPage(tabTitularDetail); // Cerrar el formulario de edición
                            }
                            else
                            {
                                MessageBox.Show("Error al actualizar el titular.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al actualizar el titular: " + ex.Message);
                        }
                    }


                    // Redirigir a tabPage1
                    tabControl1.SelectedTab = tabListTitulares;
                    MostrarTitulares();
                    EliminarTabPage(tabTitularDetail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo ingresar el titular por :" + ex.Message);
                }
            }
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            if (dtgTitulares.SelectedRows.Count > 0)
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
                    AnadirTabPage(tabTitularDetail);
                    txtNombreTitular.Text = EditarPersona.nombre;
                    txtDireccionTitular.Text = EditarPersona.direccion;
                    txtNitTitular.Text = EditarPersona.nit;
                    txtPais.Text = EditarPersona.pais;
                    txtCorreoContacto.Text = EditarPersona.correo;
                    txtTelefonoContacto.Text = EditarPersona.telefono;
                    txtNombreContacto.Text = EditarPersona.contacto;
                    btnGuardarTit.Text = "Actualizar"; // Cambiar el texto del botón a "Actualizar"
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

        private void FrmAdministrarTitulares_Load(object sender, EventArgs e)
        {

            dtgTitulares.DataSource = personaModel.GetAllTitulares();

            // Ocultar la columna 'id'
            if (dtgTitulares.Columns["id"] != null)
            {
                dtgTitulares.Columns["id"].Visible = false;
            }

            dtgTitulares.AutoGenerateColumns = true; // Asegurarte de que se generen las columnas automáticamente
        }

        private void ibtnEliminar_Click(object sender, EventArgs e)
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
                        bool isDeleted = personaModel.DeletePersona(userDetails[0].id, userDetails[0].nombre, currentUser);

                        if (isDeleted)
                        {
                            MessageBox.Show("Titular eliminado correctamente.");
                            MostrarTitulares(); // Actualizar la lista de titulares
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
                MessageBox.Show("Por favor, selecciona un usuario para eliminar.");
            }
        }
    }
}
