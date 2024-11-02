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
using System.Windows.Controls;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmMostrarAbandonadas : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        public FrmMostrarAbandonadas()
        {
            InitializeComponent();
            this.Load += FrmMostrarAbandonadas_Load;
            SeleccionarMarca.idN = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }
        private void MostrarMarcasAbandono()
        {
            //Mostrar marcas en abandono
            dtgMarcasAban.DataSource = marcaModel.GetAllMarcasNacionalesEnAbandono();
            // Ocultar la columna 'id'
            if (dtgMarcasAban.Columns["id"] != null)
            {
                dtgMarcasAban.Columns["id"].Visible = false;

                dtgMarcasAban.ClearSelection();
            }
        }
        private async void LoadMarcas()
        {
            // Obtiene las marcas en oposicion
            var marcasN = await Task.Run(() => marcaModel.GetAllMarcasNacionalesEnAbandono());

            Invoke(new Action(() =>
            {
                dtgMarcasAban.DataSource = marcasN;
                dtgMarcasAban.Refresh();
                // Oculta la columna 'id'
                if (dtgMarcasAban.Columns["id"] != null)
                {
                    dtgMarcasAban.Columns["id"].Visible = false;
                    dtgMarcasAban.ClearSelection();
                }
            }));
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }

            tabControl1.SelectedTab = nombre;
        }
        // Método para mostrar el logo en un PictureBox
        public void MostrarLogoEnPictureBox(byte[] logo)
        {
            if (logo != null && logo.Length > 0) // Verificar que el logo no esté vacío
            {
                using (var ms = new MemoryStream(logo))
                {
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
        }
        public void mostrarPanelRegistro()
        {
            if (textBoxEstatus.Text == "Registrada")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel3.Visible = true;
                btnActualizar.Location = new Point(47, panel3.Location.Y + panel3.Height + 10);
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
            dateTimePFecha_vencimiento.Value = fecha_vencimiento;
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

            if (pictureBox1.Image != null)
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
                    // Guardar la marca 
                    bool esActualizado = marcaModel.EditMarcaNacionalRegistrada(
                        SeleccionarMarca.idN, expediente, nombre, signoDistintivo, clase, folio, libro, logo, idTitular, idAgente, solicitud, registro, fecha_registro, fecha_vencimiento);
                    var marcaActualizada = marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN);

                    if (esActualizado)
                    {

                        if (marcaActualizada[0].observaciones.Contains(estado))
                        {
                            MessageBox.Show("Marca nacional actualizada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Guardar la nueva etapa
                            historialModel.GuardarEtapa(SeleccionarMarca.idN, AgregarEtapa.fecha.Value, estado, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                            MessageBox.Show("Marca nacional actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar la marca nacional 1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //MessageBox.Show("Marca nacional registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar la marca nacional 2" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                try
                {
                    // Guardar la marca nacional 
                    bool esactualizada = marcaModel.EditMarcaNacional(SeleccionarMarca.idN,
                        expediente, nombre, signoDistintivo, clase, logo, idTitular, idAgente, solicitud);
                    var marcaActualizada = marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN);

                    if (esactualizada)
                    {

                        if (marcaActualizada[0].observaciones.Contains(estado))
                        {
                            MessageBox.Show("Marca nacional actualizada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Guardar la nueva etapa
                            historialModel.GuardarEtapa(SeleccionarMarca.idN, AgregarEtapa.fecha.Value, estado, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                            MessageBox.Show("Marca nacional actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar la marca nacional 1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la marca nacional." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarFormulario();
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

        private async void CargarDatosMarca()
        {
            try
            {
                // Obtiene los detalles de la marca en segundo plano
                var detallesMarcaN = await Task.Run(() => marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN));

                if (detallesMarcaN.Count > 0)
                {
                    if (detallesMarcaN[0].registro != null)
                    {
                        // Carga los datos de la marca
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

                        // Cargar los detalles de titular y agente en segundo plano
                        var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                        var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));

                        // Esperar ambas tareas
                        await Task.WhenAll(titularTask, agenteTask);

                        var titular = titularTask.Result;
                        var agente = agenteTask.Result;

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
                        txtExpediente.Text = SeleccionarMarca.expediente;
                        txtNombre.Text = SeleccionarMarca.nombre;
                        txtClase.Text = SeleccionarMarca.clase;
                        textBoxEstatus.Text = SeleccionarMarca.estado;
                        txtSignoDistintivo.Text = SeleccionarMarca.signoDistintivo;
                        MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                        datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                        richTextBox1.Text = SeleccionarMarca.observaciones;

                        // Verificar si "observaciones" contiene la palabra "registrada"
                        bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("registrada", StringComparison.OrdinalIgnoreCase);

                        if (contieneRegistrada)
                        {
                            mostrarPanelRegistro();
                        }
                        else
                        {
                            mostrarPanelRegistro();
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void VerificarSeleccionIdMarcaEdicion()
        {
            if (dtgMarcasAban.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasAban.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasAban.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarca.idN = id;
                    tabControl1.SelectedTab = tabPageMarcaDetail;
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void refrescarMarca()
        {
            if (SeleccionarMarca.idN > 0)
            {
                try
                {
                    // Obtén los detalles de la marca de manera asíncrona
                    var detallesMarcaN = await Task.Run(() => marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN));

                    if (detallesMarcaN.Count > 0)
                    {
                        // Actualiza los detalles de la marca en los controles de la interfaz
                        var detalle = detallesMarcaN[0]; // Supongamos que solo necesitas el primer resultado

                        textBoxEstatus.Text = detalle.estado;
                        richTextBox1.Text = detalle.observaciones;

                        // Verificar si "observaciones" contiene la palabra "registrada"
                        bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("registrada", StringComparison.OrdinalIgnoreCase);

                        if (contieneRegistrada)
                        {
                            mostrarPanelRegistro();
                        }
                        else
                        {
                            mostrarPanelRegistro();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles de la marca.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error al refrescar los datos de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void loadHistorialById()
        {
            try
            {
                var historial = await Task.Run(() => historialModel.GetHistorialMarcaById(SeleccionarMarca.idN));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgMarcasAban.AutoGenerateColumns = true;
                    dtgMarcasAban.DataSource = historial;
                    dtgMarcasAban.Refresh();

                    if (dtgMarcasAban.Columns["id"] != null)
                    {
                        dtgMarcasAban.Columns["id"].Visible = false;
                    }

                    dtgMarcasAban.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmMostrarAbandonadas_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadMarcas());
            tabControl1.SelectedTab = tabPageAbandonadasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            //EliminarTabPage(tabPageHistorialDetalle);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            VerificarSeleccionIdMarcaEdicion();
            if (SeleccionarMarca.idN > 0)
            {
                CargarDatosMarca();
                AnadirTabPage(tabPageMarcaDetail);
                tabControl1.SelectedTab = tabPageMarcaDetail;
            }
        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            tabControl1.SelectedTab = tabPageAbandonadasList;
        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {
            loadHistorialById();
            AnadirTabPage(tabPageHistorialMarca);
        }
    }
}
