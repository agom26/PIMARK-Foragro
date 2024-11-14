using Comun.Cache;
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
            this.Load += FrmAdministrarTitulares_Load; // Mueve la lógica de carga aquí
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
            txtNombreTitular.Text = "";
            txtDireccionTitular.Text = "";
            txtNitTitular.Text = "";
            comboBox1.SelectedItem = -1;
            txtCorreoContacto.Text = "";
            txtTelefonoContacto.Text = "";
            txtNombreContacto.Text = "";
            comboBox1.SelectedIndex = -1;
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


        private async void LoadTitulares()
        {
            // Obtiene los usuarios
            var titulares = await Task.Run(() => personaModel.GetAllTitulares());

            Invoke(new Action(() =>
            {
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
            btnGuardarTitular.Visible = true;
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
            btnGuardarTitular.Visible = false;
        }

        private void ibtnAgregar_Click(object sender, EventArgs e)
        {
            Habilitar();
            LimpiarCampos();
            // Asegúrate de que el tabPageUserDetail esté agregado al TabControl (solo si no está ya agregado)
            AnadirTabPage(tabTitularDetail);
            iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnGuardarTitular.Text = "GUARDAR";
        }

        private void btnCancelarTit_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabTitularDetail);
            dtgTitulares.ClearSelection();
        }

        private async void btnGuardarTit_Click(object sender, EventArgs e)
        {

        }


        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Habilitar();
            if (dtgTitulares.SelectedRows.Count > 0)
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

                    txtNombreTitular.Text = EditarPersona.nombre;
                    txtDireccionTitular.Text = EditarPersona.direccion;
                    txtNitTitular.Text = EditarPersona.nit;
                    comboBox1.SelectedItem = EditarPersona.pais;
                    txtCorreoContacto.Text = EditarPersona.correo;
                    txtTelefonoContacto.Text = EditarPersona.telefono;
                    txtNombreContacto.Text = EditarPersona.contacto;
                    AnadirTabPage(tabTitularDetail);
                    btnGuardarTitular.Text = "ACTUALIZAR"; // Cambiar el texto del botón a "Actualizar"
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
            // Cargar titulares en segundo plano
            await Task.Run(() => LoadTitulares());


            // Eliminar la tabPage de detalle
            tabControl1.TabPages.Remove(tabTitularDetail);
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
                        bool isDeleted = personaModel.DeleteTitular(userDetails[0].id, userDetails[0].nombre, currentUser);

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
                MessageBox.Show("Por favor, selecciona un titular para eliminar.");
            }
        }

        private void dtgTitulares_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string buscar = textBox1.Text;
            if (buscar != "")
            {
                DataTable titulares = personaModel.GetTitularByValue(buscar);
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
                    MessageBox.Show("No existen titulares con esos datos");
                    LoadTitulares();
                }
            }
            else
            {
                LoadTitulares();
            }
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
            string nombre = txtNombreTitular.Text;
            string direccion = txtDireccionTitular.Text;
            string nit = txtNitTitular.Text;
            string pais = comboBox1.SelectedItem.ToString();
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
                string.IsNullOrWhiteSpace(txtNombreContacto.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoContacto.Text))
            {
                MessageBox.Show("Los campos no pueden estar vacíos.");
            }
            else
            {
                try
                {
                    // Deshabilitar el botón para evitar múltiples clics
                    btnGuardarTitular.Enabled = false;

                    if (btnGuardarTitular.Text == "GUARDAR")
                    {
                        // Ejecutar la operación de guardado de manera asíncrona
                        await Task.Run(() => personaModel.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo));
                        MessageBox.Show("Titular agregado exitosamente");
                        dtgTitulares.ClearSelection();
                    }
                    else if (btnGuardarTitular.Text == "ACTUALIZAR")
                    {
                        bool update = await Task.Run(() => personaModel.UpdatePersona(EditarPersona.idPersona,
                            txtNombreTitular.Text,
                            txtDireccionTitular.Text,
                            txtNitTitular.Text,
                            pais,
                            txtCorreoContacto.Text,
                            txtTelefonoContacto.Text,
                            txtNombreContacto.Text));

                        if (update)
                        {
                            MessageBox.Show("Titular actualizado exitosamente");
                            MostrarTitulares(); // Refrescar la lista de titulares
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
                    MostrarTitulares();
                    EliminarTabPage(tabTitularDetail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo ingresar el titular por :" + ex.Message);
                }
                finally
                {
                    btnGuardarTitular.Enabled = true;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabTitularDetail);
            dtgTitulares.ClearSelection();
        }
    }
}
