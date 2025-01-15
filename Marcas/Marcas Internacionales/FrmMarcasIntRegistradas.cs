using Comun.Cache;
using DocumentFormat.OpenXml.Wordprocessing;
using Dominio;
using Presentacion.Alertas;
using Presentacion.Marcas_Nacionales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMarcasIntRegistradas : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        RenovacionesMarcaModel renovacionesModel = new RenovacionesMarcaModel();
        TraspasosMarcaModel traspasosModel = new TraspasosMarcaModel();
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento;
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }
        public FrmMarcasIntRegistradas()
        {
            InitializeComponent();

            this.Load += FrmMarcasIntIngresadas_Load;
            SeleccionarMarca.idN = 0;
            ActualizarFechaVencimiento();
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }


        private async Task LoadMarcas()
        {
            totalRows = marcaModel.GetTotalMarcasRegistradas();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var marcasN = await Task.Run(() => marcaModel.GetAllMarcasNacionalesRegistradas(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgMarcasIn.DataSource = marcasN;

                if (dtgMarcasIn.Columns["id"] != null)
                {
                    dtgMarcasIn.Columns["id"].Visible = false;
                    dtgMarcasIn.ClearSelection();
                }


            }));
        }

        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = marcaModel.GetFilteredMarcasRegistradasCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = marcaModel.FiltrarMarcasNacionalesRegistradas(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgMarcasIn.DataSource = titulares;
                    if (dtgMarcasIn.Columns["id"] != null)
                    {
                        dtgMarcasIn.Columns["id"].Visible = false;
                    }
                    dtgMarcasIn.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN MARCAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadMarcas();
                }
            }
            else
            {
                await LoadMarcas();
            }
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }

            tabControl1.SelectedTab = nombre;
        }

        public void MostrarLogoEnPictureBox(byte[] logo)
        {
            if (logo != null && logo.Length > 0)
            {
                using (var ms = new MemoryStream(logo))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
        }

        public void mostrarPanelRegistro(string isRegistrada)
        {
            if (isRegistrada == "si")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel3.Visible = true;
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 64.69f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 35.31f;
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel3.Visible = false;

                tableLayoutPanel1.RowStyles[0].Height = 0;
            }

        }
        private void ActualizarFechaVencimiento()
        {
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = fecha_registro.AddYears(10).AddDays(-1);
            dateTimePFecha_vencimiento.Value = fecha_vencimiento;
        }
        private bool ValidarCampo(string campo, string mensaje)
        {
            if (string.IsNullOrEmpty(campo))
            {
                FrmAlerta alerta = new FrmAlerta(mensaje.ToUpper(), "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidarCampos(string expediente, string nombre, string clase, string signoDistintivo, string tipo, string estado,
   ref byte[] logo, bool registroChek, string registro, string folio, string libro)
        {
            // Verificar campos obligatorios
            if (!ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
                !ValidarCampo(nombre, "Por favor, ingrese el signo.") ||
                !ValidarCampo(clase, "Por favor, ingrese la clase.") ||
                !ValidarCampo(signoDistintivo, "Por favor, seleccione un signo distintivo.") ||
                !ValidarCampo(tipo, "Por favor, seleccione un tipo.") ||
                !ValidarCampo(estado, "Por favor, seleccione un estado."))
            {
                return false;
            }

            // Validar que el expediente, clase, folio, registro y libro sean enteros
            if (
                !int.TryParse(clase, out _) ||
                (registroChek && !int.TryParse(registro, out _)) ||
                (registroChek && !int.TryParse(folio, out _)) ||
                (registroChek && !int.TryParse(libro, out _)))
            {
                FrmAlerta alerta = new FrmAlerta("LA CLASE, FOLIO, REGISTRO Y TOMO\nDEBEN SER VALORES NUMÉRICOS ENTEROS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("El expediente, clase, folio, registro y tomo deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBoxSignoDistintivo.SelectedItem.ToString() == "Marca" &&
               comboBoxTipoSigno.SelectedItem.ToString() == "Gráfica/Figurativa" || comboBoxTipoSigno.SelectedItem.ToString() == "Mixta")
            {
                // Verificar que hay una imagen
                if (pictureBox1.Image != null && pictureBox1.Image != documento)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        logo = ms.ToArray();
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("INGRESE UNA IMAGEN", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return false;
                }
            }
            else
            {
                logo = null;
            }



            // Si está registrada, se verifica la información del registro
            if (registroChek)
            {
                // Validar campos adicionales para marcas registradas
                if (!ValidarCampo(folio, "Por favor, ingrese el número de folio.") ||
                    !ValidarCampo(registro, "Por favor, ingrese el número de registro.") ||
                    !ValidarCampo(libro, "Por favor, ingrese el número de tomo.")
                    )
                {
                    return false;
                }
            }

            return true; // Todas las validaciones pasaron
        }


        public void ActualizarMarcaInternacional()
        {
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string signoDistintivo = comboBoxSignoDistintivo.SelectedItem?.ToString();
            string tipoSigno = comboBoxTipoSigno.SelectedItem?.ToString();
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[] logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            int? idCliente = SeleccionarPersona.idPersonaC;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;

            string etrasp = txtETraspaso.Text;
            string erenov = txtERenovacion.Text;

            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;

            // Validaciones
            if (idTitular <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN TITULAR VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un titular válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idAgente <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN AGENTE VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un agente válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (idCliente <= 0)
            {
                idCliente = null;
                //MessageBox.Show("Por favor, seleccione un cliente válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }

            // Validar campos 
            if (!ValidarCampos(expediente, nombre, clase, signoDistintivo, tipoSigno, estado, ref logo, registroChek, registro, folio, libro))
            {
                return;
            }

            if (estado == "Trámite de renovación" && string.IsNullOrEmpty(erenov))
            {
                FrmAlerta alerta = new FrmAlerta("POR FAVOR INGRESE EL NÚMERO DE TRÁMITE DE RENOVACIÓN", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }

            if (estado == "Trámite de traspaso" && string.IsNullOrEmpty(etrasp))
            {
                FrmAlerta alerta = new FrmAlerta("POR FAVOR INGRESE EL NÚMERO DE TRÁMITE DE TRASPASO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();

                return;
            }

            // Editar la marca
            try
            {


                bool esActualizado;

                if (registroChek)
                {
                    esActualizado = marcaModel.EditMarcaNacionalRegistrada(
                        SeleccionarMarca.idN, expediente, nombre, signoDistintivo, tipoSigno, clase, folio,
                        libro, logo, idTitular, idAgente, solicitud, registro, fecha_registro, fecha_vencimiento,
                        erenov, etrasp, idCliente);
                }
                else
                {
                    esActualizado = marcaModel.EditMarcaNacional(SeleccionarMarca.idN, expediente, nombre,
                        signoDistintivo, tipoSigno, clase, logo, idTitular, idAgente, solicitud, idCliente);
                }

                DataTable marcaActualizada = marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN);

                if (esActualizado)
                {

                    if (esActualizado)
                    {

                        if (marcaActualizada.Rows.Count > 0 && marcaActualizada.Rows[0]["Observaciones"].ToString().Contains(estado))
                        {
                            FrmAlerta alerta = new FrmAlerta("MARCA NACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            //MessageBox.Show("Marca internacional actualizada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SeleccionarMarca.idN = 0;
                            tabControl1.SelectedTab = tabPageRegistradasList;
                        }
                        else
                        {
                            // Guardar la nueva etapa en el historial
                            historialModel.GuardarEtapa(SeleccionarMarca.idN, AgregarEtapa.fecha.Value, estado, AgregarEtapa.anotaciones, AgregarEtapa.usuario, "TRÁMITE");
                            FrmAlerta alerta = new FrmAlerta("MARCA NACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            //MessageBox.Show("Marca internacional actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SeleccionarMarca.idN = 0;
                            tabControl1.SelectedTab = tabPageRegistradasList;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error al actualizar la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL " + (registroChek ? "REGISTRAR" : "ACTUALIZAR") + " LA MARCA INTERNACIONAL:\n" + ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                //MessageBox.Show("Error al " + (registroChek ? "registrar" : "actualizar") + " la marca internacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarFormulario();
            }
        }



        public void LimpiarFormulario()
        {
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = null;
            txtNombreTitular.Text = "";
            txtNombreAgente.Text = "";
            txtNombreCliente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            AgregarEtapa.LimpiarEtapa();
        }

        private async Task CargarDatosMarca()
        {
            try
            {
                DataTable detallesMarcaInter = await Task.Run(() => marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN));

                if (detallesMarcaInter.Rows.Count > 0)
                {
                    DataRow row = detallesMarcaInter.Rows[0];

                    if (row["expediente"] != DBNull.Value)
                    {
                        SeleccionarMarca.expediente = row["expediente"].ToString();
                        SeleccionarMarca.nombre = row["nombre"].ToString();
                        SeleccionarMarca.clase = row["clase"].ToString();
                        SeleccionarMarca.estado = row["estado"].ToString();
                        SeleccionarMarca.signoDistintivo = row["signoDistintivo"].ToString();
                        SeleccionarMarca.tipoSigno = row["Tipo"].ToString();
                        SeleccionarMarca.logo = row["logo"] is DBNull ? null : (byte[])row["logo"];
                        SeleccionarMarca.idPersonaTitular = row["idTitular"] != DBNull.Value ? Convert.ToInt32(row["idTitular"]) : 0;
                        SeleccionarMarca.idPersonaAgente = row["idAgente"] != DBNull.Value ? Convert.ToInt32(row["idAgente"]) : 0;
                        SeleccionarMarca.idPersonaCliente = row["idCliente"] != DBNull.Value ? Convert.ToInt32(row["idCliente"]) : 0;
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
                        SeleccionarMarca.erenov = row["Erenov"].ToString();
                        SeleccionarMarca.etraspaso = row["Etrasp"].ToString();
                        // Cargar datos del titular y agente 
                        var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                        var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));

                        var clienteTask = SeleccionarMarca.idPersonaCliente != 0
                            ? Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaCliente))
                            : null;

                        await Task.WhenAll(titularTask, agenteTask);

                        var titular = titularTask.Result;
                        var agente = agenteTask.Result;
                        var cliente = clienteTask?.Result;


                        SeleccionarPersona.idPersonaT = SeleccionarMarca.idPersonaTitular;
                        SeleccionarPersona.idPersonaA = SeleccionarMarca.idPersonaAgente;
                        SeleccionarPersona.idPersonaC = SeleccionarMarca.idPersonaCliente;

                        if (titular.Count > 0)
                        {
                            txtNombreTitular.Text = titular[0].nombre;
                        }

                        if (agente.Count > 0)
                        {
                            txtNombreAgente.Text = agente[0].nombre;
                        }

                        if (cliente != null && cliente.Count > 0)
                        {
                            txtNombreCliente.Text = cliente[0].nombre;
                        }

                        // Actualizar los controles 
                        txtExpediente.Text = SeleccionarMarca.expediente;
                        txtNombre.Text = SeleccionarMarca.nombre;
                        txtClase.Text = SeleccionarMarca.clase;
                        textBoxEstatus.Text = SeleccionarMarca.estado;
                        comboBoxSignoDistintivo.SelectedItem = SeleccionarMarca.signoDistintivo;
                        comboBoxTipoSigno.SelectedItem = SeleccionarMarca.tipoSigno;
                        MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                        datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                        richTextBox1.Text = SeleccionarMarca.observaciones;

                        txtERenovacion.Text = SeleccionarMarca.erenov;
                        txtETraspaso.Text = SeleccionarMarca.etraspaso;

                        if (row["logo"] == DBNull.Value)
                        {
                            convertirImagen();
                            pictureBox1.Image = documento;
                        }

                        bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("Registrada", StringComparison.OrdinalIgnoreCase);

                        if (contieneRegistrada)
                        {
                            checkBox1.Checked = true;
                            mostrarPanelRegistro("si");
                            SeleccionarMarca.registro = row["registro"].ToString();
                            SeleccionarMarca.folio = row["folio"].ToString();
                            SeleccionarMarca.libro = row["libro"].ToString();
                            SeleccionarMarca.fechaRegistro = Convert.ToDateTime(row["fechaRegistro"]);
                            SeleccionarMarca.fechaVencimiento = Convert.ToDateTime(row["fechaVencimiento"]);

                            txtRegistro.Text = SeleccionarMarca.registro;
                            txtFolio.Text = SeleccionarMarca.folio;
                            txtLibro.Text = SeleccionarMarca.libro;
                            dateTimePFecha_Registro.Value = SeleccionarMarca.fechaRegistro.Value;
                            dateTimePFecha_vencimiento.Value = SeleccionarMarca.fechaVencimiento.Value;
                        }
                        else
                        {
                            checkBox1.Checked = false;
                            mostrarPanelRegistro("no");
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
            if (dtgMarcasIn.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasIn.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasIn.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarca.idN = id;
                    tabControl1.SelectedTab = tabPageMarcaDetail;
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task refrescarMarca()
        {
            if (SeleccionarMarca.idN > 0)
            {
                try
                {
                    DataTable detallesMarcaInt = await Task.Run(() => marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN));

                    if (detallesMarcaInt.Rows.Count > 0)
                    {
                        DataRow row = detallesMarcaInt.Rows[0];

                        if (row["estado"] != DBNull.Value && row["Observaciones"] != DBNull.Value)
                        {
                            // Actualizar los controles 
                            textBoxEstatus.Text = row["estado"].ToString();
                            richTextBox1.Text = row["Observaciones"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Verificar si "observaciones" contiene la palabra "registrada"
                        bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("registrada", StringComparison.OrdinalIgnoreCase);

                        if (contieneRegistrada)
                        {
                            mostrarPanelRegistro("si");
                        }
                        else
                        {
                            mostrarPanelRegistro("no");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles de la marca.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al refrescar los datos de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    dtgHistorialIn.AutoGenerateColumns = true;
                    dtgHistorialIn.DataSource = historial;
                    dtgHistorialIn.Refresh();

                    if (dtgHistorialIn.Columns["id"] != null)
                    {
                        dtgHistorialIn.Columns["id"].Visible = false;
                    }

                    dtgHistorialIn.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void loadRenovacionesById()
        {
            try
            {
                DataTable renovaciones = await Task.Run(() => renovacionesModel.GetAllRenovacionesByIdMarca(SeleccionarMarca.idN));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgRenovaciones.AutoGenerateColumns = true;
                    dtgRenovaciones.DataSource = renovaciones;
                    dtgRenovaciones.Refresh();

                    if (dtgRenovaciones.Columns["id"] != null)
                    {
                        dtgRenovaciones.Columns["id"].Visible = false;
                        dtgRenovaciones.Columns["IdMarca"].Visible = false;
                    }

                    dtgRenovaciones.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las renovaciones de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void loadTraspasosById()
        {
            try
            {
                DataTable traspasos = await Task.Run(() => traspasosModel.ObtenerTraspasosMarcaPorIdMarca(SeleccionarMarca.idN));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgTraspasos.AutoGenerateColumns = true;
                    dtgTraspasos.DataSource = traspasos;
                    dtgTraspasos.Refresh();

                    if (dtgTraspasos.Columns["id"] != null)
                    {
                        dtgTraspasos.Columns["id"].Visible = false;
                        dtgTraspasos.Columns["IdMarca"].Visible = false;
                        dtgTraspasos.Columns["IdTitularAnterior"].Visible = false;
                        dtgTraspasos.Columns["IdTitularNuevo"].Visible = false;
                    }

                    dtgTraspasos.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los traspasos de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmMarcasIntIngresadas_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadMarcas());
            SeleccionarMarca.idN = 0;
            tabControl1.SelectedTab = tabPageRegistradasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                EliminarTabPage(tabPageHistorialDetail);
            }
            else if (tabControl1.SelectedTab == tabPageRegistradasList)
            {
                await LoadMarcas();
                SeleccionarMarca.idN = 0;
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                await CargarDatosMarca();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
            }
            else if (tabControl1.SelectedTab == tabPageRenovacionesList)
            {
                loadRenovacionesById();
                SeleccionarRenovacion.idRenovacion = 0;
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasoDetail);
                EliminarTabPage(tabPageTraspasosList);

            }
            else if (tabControl1.SelectedTab == tabPageTraspasosList)
            {
                loadTraspasosById();
                SeleccionarTraspaso.id = 0;
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasoDetail);
            }
        }
        public async void Editar()
        {
            VerificarSeleccionIdMarcaEdicion();
            if (SeleccionarMarca.idN > 0)
            {
                await CargarDatosMarca();
                AnadirTabPage(tabPageMarcaDetail);
                tabControl1.SelectedTab = tabPageMarcaDetail;
            }
        }
        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            using (FrmJustificacion justificacionForm = new FrmJustificacion())
            {

                if (justificacionForm.ShowDialog() == DialogResult.OK)
                {
                    string justificacion = justificacionForm.Justificacion;
                    DateTime fechaAbandono = justificacionForm.fecha;
                    string usuarioAbandono = justificacionForm.usuarioAbandono;
                    // Cambiar el estado a "Abandonada" y guardar la justificación
                    try
                    {
                        // Obtener el ID de la marca seleccionada
                        if (dtgMarcasIn.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasIn.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);

                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", justificacion, usuarioAbandono, "TRÁMITE");
                                FrmAlerta alerta = new FrmAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadMarcas();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No hay marca seleccionada para abandonar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta("ERROR AL ACTUALIZAR EL ESTADO DE LA MARCA:\n" + ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                        //MessageBox.Show("Error al actualizar el estado de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            convertirImagen();
            pictureBox1.Image = documento;
        }

        private async void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaRegistrada frmAgregarEtapa = new FrmAgregarEtapaRegistrada();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                try
                {
                    historialModel.GuardarEtapa(SeleccionarMarca.idN, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE");
                    FrmAlerta alerta = new FrmAlerta("ESTADO AGREGADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    if (AgregarEtapa.etapa == "Registrada")
                    {
                        checkBox1.Checked = true;
                        mostrarPanelRegistro("si");
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        mostrarPanelRegistro("no");
                    }



                    if (AgregarEtapa.etapa == "Trámite de renovación" && AgregarEtapa.numExpediente != "")
                    {
                        txtERenovacion.Text = AgregarEtapa.numExpediente.ToString();
                        txtERenovacion.Enabled = true;
                        try
                        {
                            marcaModel.InsertarExpedienteMarca(AgregarEtapa.numExpediente.ToString(), AgregarEtapa.idMarca, "renovacion");
                        }
                        catch (Exception ex)
                        {
                            FrmAlerta alerta2 = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta2.ShowDialog();
                        }


                    }
                    else if (AgregarEtapa.etapa == "Trámite de traspaso" && AgregarEtapa.numExpediente != "0")
                    {
                        txtETraspaso.Text = AgregarEtapa.numExpediente.ToString();
                        txtETraspaso.Enabled = true;
                        try
                        {
                            marcaModel.InsertarExpedienteMarca(AgregarEtapa.numExpediente, AgregarEtapa.idMarca, "traspaso");
                        }
                        catch (Exception ex)
                        {
                            FrmAlerta alerta2 = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta2.ShowDialog();
                        }
                    }
                    else
                    {
                        txtERenovacion.Enabled = false;
                        txtETraspaso.Enabled = false;
                    }

                    await refrescarMarca();
                    await CargarDatosMarca();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {
            FrmMostrarTitulares frmMostrarTitulares = new FrmMostrarTitulares();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersona.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersona.nombre;
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

        private void roundedButton6_Click(object sender, EventArgs e)
        {
            loadHistorialById();
            AnadirTabPage(tabPageHistorialMarca);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }


        public void EditarVerHistorial()
        {
            if (dtgHistorialIn.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialIn.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    DataTable historial = historialModel.GetHistorialById(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];
                        // Asignar los valores obtenidos a la clase SeleccionarPersona
                        SeleccionarHistorial.id = Convert.ToInt32(fila["id"]);
                        SeleccionarHistorial.etapa = fila["etapa"].ToString();
                        SeleccionarHistorial.fecha = (DateTime)fila["fecha"];
                        SeleccionarHistorial.anotaciones = fila["anotaciones"].ToString();
                        SeleccionarHistorial.usuario = fila["usuario"].ToString();
                        SeleccionarHistorial.usuarioEdicion = fila["usuarioEdicion"].ToString();

                        comboBoxEstatusH.SelectedItem = SeleccionarHistorial.etapa;
                        dateTimePickerFechaH.Value = SeleccionarHistorial.fecha;
                        richTextBoxAnotacionesH.Text = SeleccionarHistorial.anotaciones;
                        labelUserEditor.Text = UsuarioActivo.usuario;
                        lblUser.Text = SeleccionarHistorial.usuario;

                        AnadirTabPage(tabPageHistorialDetail);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del historial", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void iconButton5_Click(object sender, EventArgs e)
        {
            EditarVerHistorial();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (dtgHistorialIn.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialIn.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    string etapa = dataRowView["etapa"].ToString();
                    string anotaciones = dataRowView["anotaciones"].ToString();
                    string usuario = UsuarioActivo.usuario;
                    SeleccionarHistorial.id = id;
                    SeleccionarHistorial.etapa = etapa;
                    SeleccionarHistorial.anotaciones = anotaciones;


                    DialogResult confirmacionInicial = MessageBox.Show(" ¿Está seguro que desea eliminar esta etapa? " + usuario, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmacionInicial == DialogResult.Yes)
                    {

                        if (etapa.Equals("Registrada", StringComparison.OrdinalIgnoreCase))
                        {

                            DialogResult confirmacionRegistro = MessageBox.Show("Esta acción eliminará los datos de registro, folio, tomo, fecha de registro y fecha de vencimiento. ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (confirmacionRegistro == DialogResult.Yes)
                            {
                                bool eliminarhistorial = historialModel.EliminarRegistroHistorial(id, usuario);

                                if (eliminarhistorial)
                                {

                                    MessageBox.Show("Estado eliminado y datos de registro borrados.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró el estado a eliminar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            bool eliminarhistorial = historialModel.EliminarRegistroHistorial(id, usuario);

                            if (eliminarhistorial)
                            {
                                MessageBox.Show("Estado eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el estado a eliminar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        loadHistorialById();
                        refrescarMarca();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila para eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEstatusH_SelectedValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void btnEditarH_Click(object sender, EventArgs e)
        {
            //Editar historial por id
            string etapa = comboBoxEstatusH.SelectedItem?.ToString();
            DateTime fecha = dateTimePickerFechaH.Value;
            string anotaciones = richTextBoxAnotacionesH.Text;
            SeleccionarHistorial.anotaciones = anotaciones;
            string usuario = lblUser.Text;
            string usuarioEditor = labelUserEditor.Text;
            bool actualizar;

            if (comboBoxEstatusH.SelectedIndex != -1)
            {
                string fechaSinHora = dateTimePickerFechaH.Value.ToShortDateString();
                string formato = fechaSinHora + " " + comboBoxEstatusH.SelectedItem.ToString();
                if (anotaciones.Contains(formato))
                {
                    AgregarEtapa.anotaciones = anotaciones;
                }
                else
                {
                    AgregarEtapa.anotaciones = formato + " " + anotaciones;
                }
                actualizar = historialModel.EditHistorialById(SeleccionarHistorial.id, etapa, fecha, AgregarEtapa.anotaciones, usuario, usuarioEditor);
                if (actualizar == true)
                {
                    FrmAlerta alerta = new FrmAlerta("ESTADO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show("Estado actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedTab = tabPageHistorialMarca;
                    SeleccionarHistorial.id = 0;
                    refrescarMarca();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el estado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGUN ESTADO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No ha seleccionado ningun estado");
            }


        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageHistorialMarca;
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            FrmMostrarClientes frmMostrarClientes = new FrmMostrarClientes();
            frmMostrarClientes.ShowDialog();

            if (SeleccionarPersona.idPersonaC != 0)
            {
                txtNombreCliente.Text = SeleccionarPersona.nombre;

            }
        }

        private async void FrmMarcasIntRegistradas_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadMarcas());
            tabControl1.SelectedTab = tabPageRegistradasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageRenovacionesList);
            EliminarTabPage(tabPageRenovacionDetail);
            EliminarTabPage(tabPageTraspasosList);
            EliminarTabPage(tabPageTraspasoDetail);
            ActualizarFechaVencimiento();
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void roundedButton10_Click(object sender, EventArgs e)
        {
            loadRenovacionesById();
            AnadirTabPage(tabPageRenovacionesList);
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void roundedButton9_Click(object sender, EventArgs e)
        {
            loadTraspasosById();
            AnadirTabPage(tabPageTraspasosList);
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }
        public void EditarVerRenovacion()
        {
            if (dtgRenovaciones.SelectedRows.Count > 0)
            {
                //Habilitar();
                var filaSeleccionada = dtgRenovaciones.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarRenovacion.idRenovacion = id;

                    DataTable renovacion = renovacionesModel.GetRenovacionById(id);

                    if (renovacion.Rows.Count > 0)
                    {
                        DataRow fila = renovacion.Rows[0];

                        SeleccionarRenovacion.idRenovacion = Convert.ToInt32(fila["Id"]);
                        //SeleccionarRenovacion.Reg_Antiguo = (DateTime)fila["FechaRegistroAntigua"];
                        //SeleccionarRenovacion.Reg_nuevo = (DateTime)fila["FechaRegistroNueva"];
                        SeleccionarRenovacion.Venc_antiguo = (DateTime)fila["FechaVencimientoAntigua"];
                        SeleccionarRenovacion.Venc_nuevo = (DateTime)fila["FechaVencimientoNueva"];
                        SeleccionarRenovacion.NumExpediente = fila["NumExpediente"].ToString();
                        SeleccionarRenovacion.IdMarca = Convert.ToInt32(fila["IdMarca"]);
                        //Asignar valores a controles
                        txtNoExpediente.Text = SeleccionarRenovacion.NumExpediente;

                        dateFechVencAnt.Value = SeleccionarRenovacion.Venc_antiguo;
                        dateFechVencNueva.Value = SeleccionarRenovacion.Venc_nuevo;

                        AnadirTabPage(tabPageRenovacionDetail);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles de la renovacion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnEditarRenovacion_Click(object sender, EventArgs e)
        {
            EditarVerRenovacion();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            string numExpediente = txtNoExpediente.Text;


            DateTime fechaVencimientoA = dateFechVencAnt.Value;
            DateTime fechaVencimientoN = dateFechVencNueva.Value;
            int id = SeleccionarRenovacion.idRenovacion;
            int idMarca = SeleccionarRenovacion.IdMarca;

            if (!string.IsNullOrEmpty(numExpediente))
            {
                bool actualizado = false;
                try
                {
                    actualizado = renovacionesModel.ActualizarRenovacion(id, numExpediente, idMarca, fechaVencimientoA, fechaVencimientoN);


                }
                catch (Exception ex)
                {
                    FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.Show();
                }


                if (actualizado)
                {
                    loadRenovacionesById();
                    FrmAlerta alerta = new FrmAlerta("RENOVACIÓN ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.Show();
                    //MessageBox.Show("Registro de renovación actualizado exitosamente.");
                    tabControl1.SelectedTab = tabPageRenovacionesList;

                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO FUE POSIBLE ACTUALIZAR LA RENOVACIÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.Show();
                    //MessageBox.Show("No se pudo actualizar el registro de renovación.");
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("EL NÚMERO DE EXPEDIENTE NO PUEDE ESTAR VACÍO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.Show();
                //MessageBox.Show("El número de expediente no puede estar vacío.");
            }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageRenovacionesList;
        }
        public void EditarVerTraspaso()
        {
            if (dtgTraspasos.SelectedRows.Count > 0)
            {
                //Habilitar();
                var filaSeleccionada = dtgTraspasos.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarTraspaso.id = id;

                    DataTable traspaso = traspasosModel.ObtenerTraspasoPorId(id);

                    if (traspaso.Rows.Count > 0)
                    {
                        DataRow fila = traspaso.Rows[0];

                        SeleccionarTraspaso.id = Convert.ToInt32(fila["Id"]);
                        SeleccionarTraspaso.IdMarca = Convert.ToInt32(fila["IdMarca"]);
                        SeleccionarTraspaso.numExpediente = fila["NumExpediente"].ToString();
                        SeleccionarTraspaso.idTitularA = Convert.ToInt32(fila["IdTitularAnterior"]);
                        SeleccionarTraspaso.nombreTitularA = fila["TitularAnterior"].ToString();
                        SeleccionarTraspaso.idTitularN = Convert.ToInt32(fila["IdTitularNuevo"]);
                        SeleccionarTraspaso.nombreTitularN = fila["TitularNuevo"].ToString();
                        //Asignar valores a controles
                        txtNumExpedienteTraspaso.Text = SeleccionarTraspaso.numExpediente;
                        txtNombreTitularA.Text = SeleccionarTraspaso.nombreTitularA;
                        txtNombreTitularN.Text = SeleccionarTraspaso.nombreTitularN;

                        AnadirTabPage(tabPageTraspasoDetail);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del traspaso", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnEditarTraspaso_Click(object sender, EventArgs e)
        {
            EditarVerTraspaso();
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {

            string nombreTitularAntiguo = txtNombreTitularA.Text.Trim();
            string nombreTitularNuevo = txtNombreTitularN.Text.Trim();
            string numeroExpediente = txtNumExpedienteTraspaso.Text.Trim();


            int idTraspaso = SeleccionarTraspaso.id;
            int idMarca = SeleccionarTraspaso.IdMarca;
            int idTitularAntiguo = SeleccionarTraspaso.idTitularA;
            int idTitularNuevo = SeleccionarTraspaso.idTitularN;


            if (!string.IsNullOrEmpty(numeroExpediente) &&
                !string.IsNullOrEmpty(nombreTitularAntiguo) &&
                !string.IsNullOrEmpty(nombreTitularNuevo))
            {

                traspasosModel.ActualizarTraspaso(idTraspaso, numeroExpediente, idMarca, idTitularAntiguo, idTitularNuevo);
                FrmAlerta alerta = new FrmAlerta("TRASPASO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
                tabControl1.SelectedTab = tabPageTraspasosList;
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE LLENAR TODOS LOS CAMPOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Debe llenar todos los campos para poder actualizar el traspaso", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageTraspasosList;
        }

        private void btnActualizarM_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgMarcasIn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }

        private void iconButton12_Click(object sender, EventArgs e)
        {
            ActualizarMarcaInternacional();
            EliminarTabPage(tabPageHistorialMarca);
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                if (textBoxEstatus.Text != "Registrada")
                {
                    EliminarTabPage(tabPageMarcaDetail);
                    EliminarTabPage(tabPageHistorialMarca);
                    tabControl1.SelectedTab = tabPageRegistradasList;
                }
              
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
           
        }

        private void roundedButton5_Click(object sender, EventArgs e)
        {

        }

        private async void roundedButton1_Click_1(object sender, EventArgs e)
        {
            FrmAgregarEtapaRegistrada frmAgregarEtapa = new FrmAgregarEtapaRegistrada();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                try
                {
                    historialModel.GuardarEtapa(SeleccionarMarca.idN, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE");
                    FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();

                    if (AgregarEtapa.etapa == "Registrada")
                    {
                        checkBox1.Checked = true;
                        mostrarPanelRegistro("si");
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        mostrarPanelRegistro("no");
                    }
                    await refrescarMarca();
                    await CargarDatosMarca();


                    if (AgregarEtapa.etapa == "Trámite de renovación")
                    {
                        txtERenovacion.Text = AgregarEtapa.numExpediente.ToString();
                        txtERenovacion.Enabled = true;
                        try
                        {
                            marcaModel.InsertarExpedienteMarca(AgregarEtapa.numExpediente, SeleccionarMarca.idN, "renovacion");
                        }
                        catch (Exception ex)
                        {
                            FrmAlerta alerta2 = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta2.ShowDialog();

                        }

                    }
                    else if (AgregarEtapa.etapa == "Trámite de traspaso")
                    {
                        txtETraspaso.Text = AgregarEtapa.numExpediente.ToString();
                        txtETraspaso.Enabled = true;
                        try
                        {
                            marcaModel.InsertarExpedienteMarca(AgregarEtapa.numExpediente, SeleccionarMarca.idN, "traspaso");
                        }
                        catch (Exception ex)
                        {
                            FrmAlerta alerta2 = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta2.ShowDialog();

                        }
                    }
                    else
                    {
                        txtERenovacion.Enabled = false;
                        txtETraspaso.Enabled = false;
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnAgregarTitularA_Click(object sender, EventArgs e)
        {

            FrmMostrarTitularesEdicionTraspaso frmCrearTraspaso = new FrmMostrarTitularesEdicionTraspaso();
            frmCrearTraspaso.ShowDialog();

            if (SeleccionarTraspaso.idComodin != 0)
            {
                SeleccionarTraspaso.idTitularA = SeleccionarTraspaso.idComodin;
                SeleccionarTraspaso.nombreTitularA = SeleccionarTraspaso.nombreComodin;
                txtNombreTitularA.Text = SeleccionarTraspaso.nombreTitularA;

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ UN TITULAR ANTIGUO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No selecciono un titular antiguo");
            }
        }

        private void btnAgregarTitularN_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesEdicionTraspaso frmCrearTraspaso = new FrmMostrarTitularesEdicionTraspaso();
            frmCrearTraspaso.ShowDialog();

            if (SeleccionarTraspaso.idComodin != 0)
            {
                SeleccionarTraspaso.idTitularN = SeleccionarTraspaso.idComodin;
                SeleccionarTraspaso.nombreTitularN = SeleccionarTraspaso.nombreComodin;
                txtNombreTitularN.Text = SeleccionarTraspaso.nombreTitularN;

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ UN TITULAR NUEVO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No selecciono un titular nuevo");
            }
        }

        private void btnAgregarTitular_Click(object sender, EventArgs e)
        {
            FrmMostrarTitulares frmMostrarTitulares = new FrmMostrarTitulares();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersona.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersona.nombre;
            }
        }

        private void btnAgregarAgente_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentes frmMostrarAgentes = new FrmMostrarAgentes();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersona.idPersonaA != 0)
            {
                txtNombreAgente.Text = SeleccionarPersona.nombre;

            }

        }

        private void btnAgregarCliente_Click_1(object sender, EventArgs e)
        {
            FrmMostrarClientes frmMostrarClientes = new FrmMostrarClientes();
            frmMostrarClientes.ShowDialog();

            if (SeleccionarPersona.idPersonaC != 0)
            {
                txtNombreCliente.Text = SeleccionarPersona.nombre;

            }
        }

        private async void roundedButton2_Click_1(object sender, EventArgs e)
        {
            await Task.Run(() => loadHistorialById());
            AnadirTabPage(tabPageHistorialMarca);
        }

        private void roundedButton6_Click_1(object sender, EventArgs e)
        {
            loadRenovacionesById();
            AnadirTabPage(tabPageRenovacionesList);
        }

        private void roundedButton9_Click_1(object sender, EventArgs e)
        {
            loadTraspasosById();
            AnadirTabPage(tabPageTraspasosList);
        }

        private void dtgHistorialIn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarVerHistorial();
        }

        private void dtgRenovaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarVerRenovacion();
        }

        private void dtgTraspasos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarVerTraspaso();
        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {
            filtrar();
        }

        private void iconButton14_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrar();
        }

        private void ibtnBuscar_Click_1(object sender, EventArgs e)
        {
            filtrar();
        }

        private void iconButton14_Click_1(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrar();
        }

        private void dateTimePFecha_Registro_ValueChanged_1(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filtrar();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (txtBuscar.Text != "")
            {
                filtrar();
            }
            else
            {
                await LoadMarcas();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (txtBuscar.Text != "")
                {
                    filtrar();
                }
                else
                {
                    await LoadMarcas();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (txtBuscar.Text != "")
                {
                    filtrar();
                }
                else
                {
                    await LoadMarcas();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (txtBuscar.Text != "")
            {
                filtrar();
            }
            else
            {
                await LoadMarcas();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtRegistro.Text) || string.IsNullOrWhiteSpace(txtFolio.Text)
                    || string.IsNullOrWhiteSpace(txtLibro.Text))
                {
                    DatosRegistro.peligro = true;
                }
                else
                {
                    DatosRegistro.peligro = false;
                }
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }

        private void textBoxEstatus_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
