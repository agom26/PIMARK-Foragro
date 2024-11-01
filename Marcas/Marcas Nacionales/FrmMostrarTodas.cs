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
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmMostrarTodas : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        public FrmMostrarTodas()
        {
            InitializeComponent();
            this.Load += FrmMostrarTodas_Load;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }

        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }

        private void MostrarMarcasTramite()
        {
            dtgMarcasN.DataSource = marcaModel.GetAllMarcasNacionalesEnTramite();
            // Ocultar la columna 'id'
            if (dtgMarcasN.Columns["id"] != null)
            {
                dtgMarcasN.Columns["id"].Visible = false;

                // Desactiva la selección automática de la primera fila
                dtgMarcasN.ClearSelection();
            }
        }
        private void LoadMarcas()
        {
            // Obtiene los usuarios
            var marcasN = marcaModel.GetAllMarcasNacionalesEnTramite();

            // Invoca el método para actualizar el DataGridView en el hilo principal
            Invoke(new Action(() =>
            {
                dtgMarcasN.DataSource = marcasN;
                dtgMarcasN.Refresh();
                // Oculta la columna 'id'
                if (dtgMarcasN.Columns["id"] != null)
                {
                    dtgMarcasN.Columns["id"].Visible = false;
                    // Desactiva la selección automática de la primera fila
                    dtgMarcasN.ClearSelection();
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

        // Método para mostrar el logo en un PictureBox
        public void MostrarLogoEnPictureBox(byte[] logo)
        {
            if (logo != null && logo.Length > 0) // Verificar que el logo no esté vacío
            {
                using (var ms = new MemoryStream(logo)) // Crear un MemoryStream a partir del array de bytes
                {
                    pictureBox1.Image = Image.FromStream(ms); // Convertir el MemoryStream a imagen y asignarlo al PictureBox
                }
            }
            else
            {
                pictureBox1.Image = null; // Si el logo es nulo, limpiar la imagen del PictureBox
            }
        }

        public void mostrarPanelRegistro()
        {
            if (textBoxEstatus.Text == "Registrada")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel3.Visible = true;
                btnActualizar.Location = new Point(47, panel3.Location.Y + panel3.Height + 10); // Mueve btnGuardar debajo de panel2
                btnCancelar.Location = new Point(370, panel3.Location.Y + panel3.Height + 10);
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel3.Visible = false;
                btnActualizar.Location = new Point(47, 960);
                btnCancelar.Location = new Point(370, 960);
            }
        }
        private void ActualizarFechaVencimiento()
        {
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = fecha_registro.AddYears(10).AddDays(-1);
            dateTimePFecha_vencimiento.Value = fecha_vencimiento; // Asigna la fecha de vencimiento al DateTimePicker correspondiente
        }

        public void ActualizarMarcaNacional()
        {
            // Recolectar valores de los controles
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string signoDistintivo = txtSignoDistintivo.Text;
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[] logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;


            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;

            // Validaciones
            if (string.IsNullOrEmpty(expediente) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(clase) || string.IsNullOrEmpty(signoDistintivo))
            {
                MessageBox.Show("Por favor, llene todos los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (estado == null)
            {
                MessageBox.Show("Por favor, seleccione un estado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pictureBox1.Image != null) // Verificar que hay una imagen
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    logo = ms.ToArray();
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese una imagen.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si está registrada, se verifica la información del registro
            if (registroChek)
            {
                if (string.IsNullOrEmpty(registro))
                {
                    MessageBox.Show("Por favor, ingrese el número de registro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // Guardar la marca y obtener su ID
                    bool esActualizado = marcaModel.EditMarcaNacionalRegistrada(
                        SeleccionarMarca.idN, expediente, nombre, signoDistintivo, clase, folio, libro, logo, idTitular, idAgente, solicitud, estado, observaciones, registro, fecha_registro, fecha_vencimiento);


                    if (esActualizado)
                    {
                        string etapa = textBoxEstatus.Text;


                        if (!string.IsNullOrEmpty(etapa))
                        {
                            historialModel.GuardarEtapa(SeleccionarMarca.idN, AgregarEtapa.fecha.Value, etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Marca nacional registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar la marca nacional." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                try
                {
                    // Guardar la marca nacional y obtener su ID
                    bool esactualizada = marcaModel.EditMarcaNacional(SeleccionarMarca.idN,
                        expediente, nombre, signoDistintivo, clase, logo, idTitular, idAgente, solicitud, estado, observaciones);

                    // Verifica si se ha guardado correctamente
                    if (esactualizada)
                    {
                        string etapa = textBoxEstatus.Text;

                        // Solo guardamos la etapa si la etapa no está vacía
                        if (!string.IsNullOrEmpty(etapa))
                        {
                            historialModel.GuardarEtapa(SeleccionarMarca.idN, AgregarEtapa.fecha.Value, etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Marca nacional actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la marca nacional." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public void LimpiarFormulario()
        {
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtSignoDistintivo.Text = "";
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = null;
            txtNombreTitular.Text = "";
            txtDireccionTitular.Text = "";
            txtEntidadTitular.Text = "";
            txtNombreAgente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            AgregarEtapa.LimpiarEtapa();
        }

        private async void EditarAsync()
        {
            if (dtgMarcasN.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasN.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasN.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarca.idN = id;


                    var detallesMarcaN = await Task.Run(() => marcaModel.GetMarcaNacionalById(id));

                    if (detallesMarcaN.Count > 0)
                    {
                        // Verifica que el primer elemento de detallesMarcaN no sea nulo
                        if (detallesMarcaN[0].registro != null)
                        {
                            SeleccionarMarca.expediente = detallesMarcaN[0].expediente;
                            SeleccionarMarca.nombre = detallesMarcaN[0].nombre;
                            SeleccionarMarca.clase = detallesMarcaN[0].clase;
                            SeleccionarMarca.estado = detallesMarcaN[0].estado;
                            SeleccionarMarca.signoDistintivo = detallesMarcaN[0].signoDistintivo;
                            SeleccionarMarca.logo = detallesMarcaN[0].logo;
                            SeleccionarMarca.idPersonaTitular = detallesMarcaN[0].idTitular;
                            SeleccionarMarca.idPersonaAgente = detallesMarcaN[0].idAgente;
                            SeleccionarMarca.fecha_solicitud = (DateTime)detallesMarcaN[0].fechaSolicitud;
                            SeleccionarMarca.observaciones = detallesMarcaN[0].observaciones;

                            if (detallesMarcaN[0].registro == null)
                            {
                                SeleccionarMarca.registro = detallesMarcaN[0].registro;
                                SeleccionarMarca.folio = detallesMarcaN[0].folio;
                                SeleccionarMarca.libro = detallesMarcaN[0].libro;
                                SeleccionarMarca.fechaRegistro = (DateTime)detallesMarcaN[0].fechaRegistro;
                                SeleccionarMarca.fechaVencimiento = (DateTime)detallesMarcaN[0].fechaVencimiento;

                                txtRegistro.Text = SeleccionarMarca.registro;
                                txtFolio.Text = SeleccionarMarca.folio;
                                txtLibro.Text = SeleccionarMarca.libro;
                                dateTimePFecha_Registro.Value = SeleccionarMarca.fechaRegistro;
                                dateTimePFecha_vencimiento.Value = SeleccionarMarca.fechaVencimiento;
                            }
                            else
                            {

                                //MessageBox.Show("El campo 'registro' es nulo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            // Cargar los detalles de titular y agente en segundo plano
                            var titular = await Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                            var agente = await Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));
                            SeleccionarPersona.idPersonaT = SeleccionarMarca.idPersonaTitular;
                            SeleccionarPersona.idPersonaA = SeleccionarMarca.idPersonaAgente;
                            if (titular.Count > 0)
                            {
                                txtNombreTitular.Text = titular[0].nombre;
                                txtDireccionTitular.Text = titular[0].direccion;
                                txtEntidadTitular.Text = titular[0].pais;
                            }

                            if (agente.Count > 0)
                            {
                                txtNombreAgente.Text = agente[0].nombre;
                            }

                            // Actualizar los controles de la interfaz con los datos obtenidos
                            AnadirTabPage(tabPageMarcaDetail);
                            txtExpediente.Text = SeleccionarMarca.expediente;
                            txtNombre.Text = SeleccionarMarca.nombre;
                            txtClase.Text = SeleccionarMarca.clase;
                            textBoxEstatus.Text = SeleccionarMarca.estado;
                            txtSignoDistintivo.Text = SeleccionarMarca.signoDistintivo;
                            MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                            datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                            richTextBox1.Text = SeleccionarMarca.observaciones;
                            //cmbEstado.SelectedText = SeleccionarMarca.estado;
                            // Verificar si "observaciones" contiene la palabra "registrada"
                            bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("registrada", StringComparison.OrdinalIgnoreCase);

                            if (contieneRegistrada)
                            {
                                // La palabra "registrada" está presente en las observaciones
                                mostrarPanelRegistro();
                                //MessageBox.Show("La observación contiene la palabra 'registrada'.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {

                                // La palabra "registrada" no está presente
                                mostrarPanelRegistro();
                                //MessageBox.Show("La observación NO contiene la palabra 'registrada'.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles de la marca", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Mostrar Historial
        private void loadHistorialById()
        {
            // Obtiene los usuarios
            var historial = historialModel.GetHistorialMarcaById(SeleccionarMarca.idN);

            // Invoca el método para actualizar el DataGridView en el hilo principal
            Invoke(new Action(() =>
            {
                // Activa la generación automática de columnas
                dtgHistorial.AutoGenerateColumns = true;
                dtgHistorial.DataSource = historial;

                // Refresca el DataGridView para asegurar la visualización correcta
                dtgHistorial.Refresh();

                // Verifica y oculta la columna 'id' si existe
                if (dtgHistorial.Columns["id"] != null)
                {
                    dtgHistorial.Columns["id"].Visible = false;
                }

                // Desactiva la selección automática de la primera fila
                dtgHistorial.ClearSelection();
            }));
        }





        private void ibtnAgregar_Click(object sender, EventArgs e)
        {

        }

        private async void FrmMostrarTodas_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadMarcas());

            // Eliminar la tabPage de detalle
            tabControl1.TabPages.Remove(tabPageMarcaDetail);
            tabControl1.TabPages.Remove(tabPageHistorialMarca);
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            EditarAsync();
        }

        private void ibtnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void tabPageMarcaDetail_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapa frmAgregarEtapa = new FrmAgregarEtapa();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                textBoxEstatus.Text = AgregarEtapa.etapa;
                mostrarPanelRegistro();
                richTextBox1.Text = SeleccionarMarca.observaciones + "\n" + AgregarEtapa.anotaciones;
            }
        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {
            FrmMostrarTitulares frmMostrarTitulares = new FrmMostrarTitulares();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersona.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersona.nombre;
                txtDireccionTitular.Text = SeleccionarPersona.direccion;
                txtEntidadTitular.Text = SeleccionarPersona.pais;
            }
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentes frmMostrarAgentes = new FrmMostrarAgentes();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersona.idPersonaA != 0)
            {
                txtNombreAgente.Text = SeleccionarPersona.nombre;

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ActualizarMarcaNacional();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            //Agregar una imagen al cuadro de imagen para la foto del usuario.
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //Borrar foto del usuario
            pictureBox1.Image = null;
        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private async void roundedButton6_Click(object sender, EventArgs e)
        {
            await Task.Run(() => LoadMarcas());
            EliminarTabPage(tabPageMarcaDetail);
            tabControl1.SelectedTab = tabPageListaMarcas;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            using (FrmJustificacion justificacionForm = new FrmJustificacion())
            {
                // Mostrar el popup
                if (justificacionForm.ShowDialog() == DialogResult.OK)
                {
                    string justificacion = justificacionForm.Justificacion;

                    // Cambiar el estado a "Abandonada" y guardar la justificación
                    try
                    {
                        // Obtener el ID de la marca seleccionada
                        if (dtgMarcasN.SelectedCells.Count >= 1)
                        {
                            var filaSeleccionada = dtgMarcasN.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);

                                // Actualizar el estado y la justificación en la base de datos
                                marcaModel.AbandonarMarca(idMarca, "Abandonada", justificacion);

                                MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MostrarMarcasTramite();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No hay marca seleccionada para abandonar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void roundedButton6_Click_1(object sender, EventArgs e)
        {
           
            AnadirTabPage(tabPageHistorialMarca);

        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica si la pestaña activa es tabPage1
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                // Cargar usuarios en segundo plano
                await Task.Run(() => loadHistorialById());
            }
        }
    }
}
