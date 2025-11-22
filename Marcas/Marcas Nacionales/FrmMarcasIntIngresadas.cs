using Comun;
using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using Presentacion.Marcas_Nacionales;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.Json;


namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMarcasIntIngresadas : Form, IAsyncLoadable
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento = null;
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool archivoSubido = false;
        private bool buscando = false;
        bool agregoEstado = false;
        private bool _isLoading;
        private bool _actualizando; // evita reentradas
        private bool _cargandoUI;
        private bool _guardandoHist; // campo de la clase

        //ftp
        const string URL = "https://foragro.com.es/peticiones/archivos_marcas_nacionales.php";
        const string TOKEN = "TOKEN_SECRETO_LARGO_Y_UNICO";
        static class HttpX
        {
            private static readonly HttpClient _http;
            static HttpX()
            {
                var handler = new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
                    MaxConnectionsPerServer = 8,
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
                };
                _http = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(10) };
                _http.DefaultRequestHeaders.ExpectContinue = false;
            }
            public static HttpClient Client => _http;
        }

        public async Task LoadAsync()
        {
            await LoadMarcas(); // aquí llamas a tu método actual
        }

        private async Task<bool> TieneInternetAsync()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            try
            {
                // DNS lookup rápido: no depende de tu API
                await Dns.GetHostEntryAsync("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }




        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }

        private Task RefreshPageAsync() => buscando ? filtrar() : LoadMarcas();
        private void SetLoading(bool on)
        {
            Cursor.Current = on ? Cursors.WaitCursor : Cursors.Default;

            // Habilita/deshabilita según loading y posición actual
            bool canUse = !on;
            btnFirst.Enabled = canUse && currentPageIndex > 1;
            btnPrev.Enabled = canUse && currentPageIndex > 1;
            btnNext.Enabled = canUse && currentPageIndex < totalPages;
            btnLast.Enabled = canUse && currentPageIndex < totalPages;
        }

        private void UpdatePagerLabels()
        {
            lblCurrentPage.Text = (totalPages == 0) ? "0" : currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString(); // (si no lo actualizas en Load/filtrar)
        }

        private void SetDoubleBuffering(Control control, bool enable)
        {
            // Habilitar o deshabilitar DoubleBuffering
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                           .SetValue(control, enable, null);
        }

        public FrmMarcasIntIngresadas()
        {
            InitializeComponent();
            SeleccionarMarca.idN = 0;
            archivoSubido = false;
            ActualizarFechaVencimiento();
            btnAdjuntarT.Visible = false;
            SetDoubleBuffering(this, true);
            SetDoubleBuffering(dtgMarcasIn, true);

            if (UsuarioActivo.soloLectura)
            {
                btnAgregarEstado.Visible = false;
                btnAbandonar.Visible = false;
                btnDesistir.Visible = false;
                btnEditar2.Visible = false;
                btnOposicion.Visible = false;
                btnAgregarAgente.Enabled = false;
                btnAgregarCliente.Enabled = false;
                btnAgregarTitular.Enabled = false;
                btnEditarEstadoHistorial.Visible = false;

                btnAdjuntarT.Visible = false;
                btnAdjuntarArchivo.Visible = false;
                btnEliminarArchivo.Visible = false;

                btnQuitarImagen.Visible = false;
                btnSubirImagen.Visible = false;

                txtExpediente.ReadOnly = true;
                txtNombre.ReadOnly = true;
                txtLibro.ReadOnly = true;
                txtRegistro.ReadOnly = true;
                txtFolio.ReadOnly = true;
                txtClase.ReadOnly = true;
                txtNombreAgente.ReadOnly = true;
                txtNombreCliente.ReadOnly = true;
                txtNombreTitular.ReadOnly = true;
                txtUbicacion.ReadOnly = true;
                richTextBox1.ReadOnly = true;
                comboBoxTipoSigno.Enabled = false;
                comboBoxSignoDistintivo.Enabled = false;
                datePickerFechaSolicitud.Enabled = false;

                dateTimePFecha_vencimiento.Enabled = false;
            }
            else
            {
                btnAgregarEstado.Visible = true;
                btnAbandonar.Visible = true;
                btnDesistir.Visible = true;
                btnEditar2.Visible = true;
                btnOposicion.Visible = true;
                btnAgregarAgente.Enabled = true;
                btnAgregarCliente.Enabled = true;
                btnAgregarTitular.Enabled = true;
                btnEditarEstadoHistorial.Visible = true;

                btnAdjuntarT.Visible = true;
                btnAdjuntarArchivo.Visible = true;
                btnEliminarArchivo.Visible = true;

                btnQuitarImagen.Visible = true;
                btnSubirImagen.Visible = true;

                txtExpediente.ReadOnly = false;
                txtNombre.ReadOnly = false;
                txtLibro.ReadOnly = false;
                txtRegistro.ReadOnly = false;
                txtFolio.ReadOnly = false;
                txtClase.ReadOnly = false;
                txtNombreAgente.ReadOnly = false;
                txtNombreCliente.ReadOnly = false;
                txtNombreTitular.ReadOnly = false;
                txtUbicacion.ReadOnly = false;
                richTextBox1.ReadOnly = false;
                comboBoxTipoSigno.Enabled = true;
                comboBoxSignoDistintivo.Enabled = true;
                datePickerFechaSolicitud.Enabled = true;
                dateTimePFecha_vencimiento.Enabled = true;
            }

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
            totalRows = await marcaModel.GetTotalMarcasSinRegistro();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            var marcasN = await marcaModel.GetAllMarcasNacionalesEnTramite(currentPageIndex, pageSize);

            void Apply()
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                lblCurrentPage.Text = currentPageIndex.ToString();
                dtgMarcasIn.DataSource = marcasN;
            }

            if (!IsDisposed)
            {
                if (InvokeRequired) BeginInvoke((Action)Apply);
                else Apply();
            }
        }

        public async Task filtrar()
        {
            string buscar = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(buscar))
            {
                totalRows = await marcaModel.GetFilteredMarcasSinRegistroCount(buscar);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();

                DataTable dt = await marcaModel.FiltrarMarcasNacionalesEnTramite(buscar, currentPageIndex, pageSize);
                if (dt.Rows.Count > 0)
                {
                    dtgMarcasIn.DataSource = dt;
                    if (dtgMarcasIn.Columns["id"] != null) dtgMarcasIn.Columns["id"].Visible = false;
                    dtgMarcasIn.ClearSelection();
                }
                else
                {
                    new FrmAlerta("NO EXISTEN MARCAS CON ESOS DATOS", "MENSAJE",
                                  MessageBoxButtons.OK, MessageBoxIcon.None).ShowDialog();
                    await LoadMarcas();
                }
            }
            else
            {
                await LoadMarcas();
            }
        }


        /* anterior
        private async Task LoadMarcas()
        {
           
            try
            {
                
                totalRows = await marcaModel.GetTotalMarcasSinRegistro();
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                var marcasN = await marcaModel.GetAllMarcasNacionalesEnTramite(currentPageIndex, pageSize);


                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke(() =>
                    {
                        lblTotalPages.Text = totalPages.ToString();
                        lblTotalRows.Text = totalRows.ToString();
                        dtgMarcasIn.DataSource = marcasN;
                    });
                }
            }
            catch (HttpRequestException ex)
            {
                new FrmAlerta(
                 "No se pudo conectar con el servidor. Verifique su conexión a internet.",
                 "ERROR DE CONEXIÓN",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error
                ).ShowDialog();
            }
            catch (JsonException ex)
            {
                new FrmAlerta(
                    "Hubo un problema al procesar los datos recibidos del servidor.",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                ).ShowDialog();
            }
            catch (Exception ex)
            {
                new FrmAlerta(
                     "Ocurrió un error al cargar los datos: " + ex.Message,
                     "ERROR",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                 ).ShowDialog();
            }
           
        }

        // anterior
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (!string.IsNullOrWhiteSpace(buscar))
            {
                try
                {
                    DataTable titulares = await marcaModel.FiltrarMarcasNacionalesEnTramite(buscar, currentPageIndex, pageSize);

                    if (titulares.Rows.Count > 0)
                    {
                        dtgMarcasIn.DataSource = titulares;

                        if (dtgMarcasIn.Columns["id"] != null)
                            dtgMarcasIn.Columns["id"].Visible = false;

                        dtgMarcasIn.ClearSelection();
                    }
                    else
                    {
                        FrmAlerta alerta = new FrmAlerta("NO EXISTEN MARCAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                        alerta.ShowDialog();
                        await LoadMarcas();
                    }
                }
                catch (HttpRequestException ex)
                {
                    new FrmAlerta(
                     "No se pudo conectar con el servidor. Verifique su conexión a internet.",
                     "ERROR DE CONEXIÓN",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                    ).ShowDialog();
                }
                catch (JsonException ex)
                {
                    new FrmAlerta(
                        "Hubo un problema al procesar los datos recibidos del servidor.",
                        "ERROR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    ).ShowDialog();
                }
                catch (Exception ex)
                {
                    new FrmAlerta(
                         "Ocurrió un error al cargar los datos: " + ex.Message,
                         "ERROR",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error
                     ).ShowDialog();
                }
            }
            else
            {
                await LoadMarcas();
            }
        }*/

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
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 64.69f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 35.31f;
                btnAdjuntarT.Visible = true;
            }
            else
            {
                btnAdjuntarT.Visible = false;
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
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
                (registroChek && !int.TryParse(folio, out _)) ||
                (registroChek && !int.TryParse(libro, out _)))
            {
                FrmAlerta alerta = new FrmAlerta("LA CLASE, FOLIO Y TOMO\nDEBEN SER VAORES NUMÉRICOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("El expediente, clase, folio, registro y libro deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if ((comboBoxSignoDistintivo.Text == "Marca" &&
             comboBoxTipoSigno.Text == "Gráfica/Figurativa") ||
             (comboBoxSignoDistintivo.Text == "Marca" &&
             comboBoxTipoSigno.Text == "Mixta") ||
             (comboBoxSignoDistintivo.Text == "Emblema" &&
             comboBoxTipoSigno.Text == "Gráfica/Figurativa") ||
              (comboBoxSignoDistintivo.Text == "Emblema" &&
             comboBoxTipoSigno.Text == "Mixta")
             )
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
                    logo = null;
                }
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


        public async Task ActualizarMarcaInternacional()
        {
            try
            {
                byte[]? logo = null;
                int? idCliente = SeleccionarPersona.idPersonaC;
                string ubicacionF = txtUbicacion.Text;
                bool registroChek = checkBox1.Checked;
                DateTime? fecha_vencimiento = dateTimePFecha_vencimiento.Value;
                int indefinida = 0;

                if (idCliente == null || idCliente == 0 || idCliente <= 0)
                {
                    idCliente = null;
                }

                if (SeleccionarPersona.idPersonaT <= 0 || SeleccionarPersona.idPersonaA <= 0)
                {
                    FrmAlerta alerta = new FrmAlerta("SELECCIONE UN TITULAR Y UN AGENTE VÁLIDOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return;
                }

                if (!ValidarCampos(txtExpediente.Text, txtNombre.Text, txtClase.Text, comboBoxSignoDistintivo.SelectedItem?.ToString(), comboBoxTipoSigno.SelectedItem?.ToString(), textBoxEstatus.Text, ref logo, checkBox1.Checked, txtRegistro.Text.Trim(), txtFolio.Text, txtLibro.Text))
                {
                    return;
                }

                if (registroChek && toggleIndefinido.Checked)
                {
                    indefinida = 1;
                    fecha_vencimiento = null;
                }
                else if (registroChek && !toggleIndefinido.Checked)
                {
                    indefinida = 0;
                    fecha_vencimiento = dateTimePFecha_vencimiento.Value;
                }
                else
                {
                    indefinida = 0;
                }

                    bool esActualizado = false;

                {
                    if (checkBox1.Checked)
                    {
                        string registro = txtRegistro.Text.Trim();
                        bool existeRegistro = await marcaModel.ExisteRegistro(registro, SeleccionarMarca.idN);
                        if (existeRegistro == true)
                        {
                            FrmAlerta alerta = new FrmAlerta("EL NÚMERO DE REGISTRO YA EXISTE", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            alerta.ShowDialog();
                            return;
                        }
                        else
                        {
                            esActualizado = await marcaModel.EditMarcaNacionalRegistradaNuevo(SeleccionarMarca.idN, txtExpediente.Text, txtNombre.Text, comboBoxSignoDistintivo.SelectedItem?.ToString(), comboBoxTipoSigno.SelectedItem?.ToString(), txtClase.Text, txtFolio.Text, txtLibro.Text, logo, SeleccionarPersona.idPersonaT, SeleccionarPersona.idPersonaA, datePickerFechaSolicitud.Value, txtRegistro.Text.Trim(), dateTimePFecha_Registro.Value, indefinida, fecha_vencimiento, null, null, idCliente, ubicacionF);

                        }

                    }
                    else
                    {
                        esActualizado = await marcaModel.EditMarcaNacionalNuevo(SeleccionarMarca.idN, txtExpediente.Text, txtNombre.Text, comboBoxSignoDistintivo.SelectedItem?.ToString(), comboBoxTipoSigno.SelectedItem?.ToString(), txtClase.Text, logo, SeleccionarPersona.idPersonaT, SeleccionarPersona.idPersonaA, datePickerFechaSolicitud.Value, idCliente, ubicacionF);
                    }
                }

                if (esActualizado)
                {
                    if (agregoEstado == true)
                    {
                        await historialModel.GuardarEtapa(SeleccionarMarca.idN, Convert.ToDateTime(AgregarEtapa.fecha), AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE", AgregarEtapa.fechaVencimiento);
                        agregoEstado = false;
                        FrmAlerta frmAlerta = new FrmAlerta("MARCA ACTUALIZADA CON ÉXITO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmAlerta.ShowDialog();
                        SeleccionarMarca.idN = 0;
                        EliminarTabPage(tabPageHistorialMarca);
                        EliminarTabPage(tabPageListaArchivos);
                        await LoadMarcas();
                        AnadirTabPage(tabPageIngresadasList);
                        LimpiarControles();
                    }
                    else
                    {
                        FrmAlerta frmAlerta = new FrmAlerta("MARCA ACTUALIZADA CON ÉXITO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmAlerta.ShowDialog();
                        SeleccionarMarca.idN = 0;
                        EliminarTabPage(tabPageHistorialMarca);
                        EliminarTabPage(tabPageListaArchivos);
                        await LoadMarcas();
                        AnadirTabPage(tabPageIngresadasList);
                        LimpiarControles();
                    }

                }
                else
                {
                    MessageBox.Show("Error al actualizar la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la marca nacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LimpiarControles()
        {
            convertirImagen();
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            textBoxEstatus.Text = "";
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            datePickerFechaSolicitud.Value = DateTime.Today;
            dateTimePFecha_Registro.Value = DateTime.Now;
            richTextBox1.Text = "";
            pictureBox1.Image = documento;
            txtNombreTitular.Text = "";
            txtNombreAgente.Text = "";
            txtNombreCliente.Text = "";
            checkBox1.Checked = false;
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = null;
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            AgregarEtapa.LimpiarEtapa();
        }

        public void LimpiarFormulario()
        {
            convertirImagen();
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = documento;
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
            SeleccionarPersona.idPersonaA = 0;
            SeleccionarPersona.idPersonaT = 0;
            SeleccionarPersona.idPersonaC = null;
        }

        private async Task CargarDatosMarca()
        {
            try
            {

                DataTable detallesMarcaInter = await marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN);

                if (detallesMarcaInter.Rows.Count > 0)
                {
                    DataRow row = detallesMarcaInter.Rows[0];

                    byte[] logo = null;

                    // Asignar los valores de los campos de la marca
                    SeleccionarMarca.expediente = row["expediente"] != DBNull.Value ? row["expediente"].ToString() : string.Empty;
                    SeleccionarMarca.nombre = row["nombre"] != DBNull.Value ? row["nombre"].ToString() : string.Empty;
                    SeleccionarMarca.clase = row["clase"] != DBNull.Value ? row["clase"].ToString() : string.Empty;
                    SeleccionarMarca.estado = row["estado"] != DBNull.Value ? row["estado"].ToString() : string.Empty;
                    SeleccionarMarca.signoDistintivo = row["signoDistintivo"] != DBNull.Value ? row["signoDistintivo"].ToString() : string.Empty;
                    SeleccionarMarca.tipoSigno = row["Tipo"] != DBNull.Value ? row["Tipo"].ToString() : string.Empty;

                    bool tieneLogo = await marcaModel.MarcaTieneLogoAsync(SeleccionarMarca.idN);

                    if (tieneLogo)
                    {
                        SeleccionarMarca.logo = await marcaModel.ObtenerLogoMarcaPorIdNuevo(SeleccionarMarca.idN);
                    }
                    else
                    {
                        SeleccionarMarca.logo = null;
                    }

                    txtUbicacion.Text = row["ubicacion_fisica"] != DBNull.Value ? row["ubicacion_fisica"].ToString() : string.Empty;

                    if (SeleccionarMarca.logo != null && SeleccionarMarca.logo.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(SeleccionarMarca.logo))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        convertirImagen();
                        pictureBox1.Image = documento;
                    }

                    SeleccionarMarca.idPersonaTitular = row["idTitular"] != DBNull.Value ? Convert.ToInt32(row["idTitular"]) : 0;
                    SeleccionarMarca.idPersonaAgente = row["idAgente"] != DBNull.Value ? Convert.ToInt32(row["idAgente"]) : 0;
                    SeleccionarMarca.idPersonaCliente = row["idCliente"] != DBNull.Value ? Convert.ToInt32(row["idCliente"]) : 0;
                    SeleccionarMarca.fecha_solicitud = row["fechaSolicitud"] != DBNull.Value ? Convert.ToDateTime(row["fechaSolicitud"]) : DateTime.MinValue;
                    SeleccionarMarca.observaciones = row["observaciones"] != DBNull.Value ? row["observaciones"].ToString() : string.Empty;

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
                    else
                    {
                        txtNombreCliente.Text = "";
                    }

                    txtExpediente.Text = SeleccionarMarca.expediente;
                    txtNombre.Text = SeleccionarMarca.nombre;
                    txtClase.Text = SeleccionarMarca.clase;
                    textBoxEstatus.Text = SeleccionarMarca.estado;
                    comboBoxSignoDistintivo.SelectedItem = SeleccionarMarca.signoDistintivo;
                    comboBoxTipoSigno.SelectedItem = SeleccionarMarca.tipoSigno;
                    datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                    richTextBox1.Text = SeleccionarMarca.observaciones;

                    bool contieneRegistrada = await marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idN);
                    if (contieneRegistrada)
                    {
                        checkBox1.Checked = true;
                        mostrarPanelRegistro("si");
                        VerificarDatosRegistro();

                        SeleccionarMarca.registro = row["registro"].ToString();
                        SeleccionarMarca.folio = row["folio"].ToString();
                        SeleccionarMarca.libro = row["libro"].ToString();
                        SeleccionarMarca.fechaRegistro = Convert.ToDateTime(row["fechaRegistro"]);

                        txtRegistro.Text = SeleccionarMarca.registro;
                        txtFolio.Text = SeleccionarMarca.folio;
                        txtLibro.Text = SeleccionarMarca.libro;
                        dateTimePFecha_Registro.Value = SeleccionarMarca.fechaRegistro.Value;

                        // Leer el valor de forma segura
                        string indefStr = row["indefinido"]?.ToString() ?? "0";

                        // Convertir a entero sin riesgo
                        int indefinido = int.TryParse(indefStr, out int val) ? val : 0;

                        if (indefinido == 1)
                        {
                            // Mostrar como indefinida
                            dateTimePFecha_vencimiento.Format = DateTimePickerFormat.Custom;
                            dateTimePFecha_vencimiento.CustomFormat = "--";

                            dateTimePFecha_vencimiento.Enabled = false; // opcional

                            toggleIndefinido.Checked = true;
                        }
                        else
                        {

                            toggleIndefinido.Checked = false;
                            dateTimePFecha_vencimiento.Enabled = true;
                            dateTimePFecha_vencimiento.Format = DateTimePickerFormat.Custom;
                            dateTimePFecha_vencimiento.CustomFormat = "dd/MM/yyyy";

                            if (row["fechaVencimiento"] != DBNull.Value)
                            {
                                dateTimePFecha_vencimiento.Value = Convert.ToDateTime(row["fechaVencimiento"]);
                                SeleccionarMarca.fechaVencimiento = Convert.ToDateTime(row["fechaVencimiento"]);
                            }

                        }

                        
                    }
                    else
                    {
                        mostrarPanelRegistro("no");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                SeleccionarMarca.idN = 0;
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
                            textBoxEstatus.Text = row["estado"].ToString();
                            richTextBox1.Text = row["Observaciones"].ToString();

                        }
                        else
                        {
                            MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        bool contieneRegistrada = await marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idN);

                        if (contieneRegistrada)
                        {
                            checkBox1.Checked = true;
                            mostrarPanelRegistro("si");
                            VerificarDatosRegistro();
                        }
                        else
                        {
                            checkBox1.Checked = false;
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

        private async Task loadHistorialById()
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

        private async void FrmMarcasIntIngresadas_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            try
            {
                // ===== tu init actual (déjalo igual) =====
                SeleccionarMarca.idN = 0;
                archivoSubido = false;
                btnAdjuntarT.Visible = false;
                convertirImagen();
                pictureBox1.Image = documento;

                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageListaArchivos);
                currentPageIndex = 1;
                lblCurrentPage.Text = currentPageIndex.ToString();
                // ========================================

                // 1) ¿Hay internet?
                if (!await TieneInternetAsync())
                {
                    new FrmAlerta(
                        "No hay conexión a internet. Verifique su conexión.",
                        "ERROR DE CONEXIÓN",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    ).ShowDialog();

                    // Dejar la UI en estado consistente (vacía)
                    dtgMarcasIn.DataSource = null;
                    lblTotalRows.Text = "0";
                    lblTotalPages.Text = "0";
                    lblCurrentPage.Text = "0";
                    return;
                }

                // 2) Intentar cargar desde tu servidor/API
                try
                {
                    await LoadMarcas(); // deja que lance excepciones
                }
                catch (HttpRequestException)
                {
                    new FrmAlerta(
                        "No se pudo comunicar con el servidor.",
                        "ERROR DE SERVIDOR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    ).ShowDialog();

                    dtgMarcasIn.DataSource = null;
                    lblTotalRows.Text = "0";
                    lblTotalPages.Text = "0";
                    lblCurrentPage.Text = "0";
                }
                catch (JsonException)
                {
                    new FrmAlerta(
                        "Hubo un problema al procesar los datos del servidor.",
                        "ERROR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    ).ShowDialog();

                    dtgMarcasIn.DataSource = null;
                    lblTotalRows.Text = "0";
                    lblTotalPages.Text = "0";
                    lblCurrentPage.Text = "0";
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    new FrmAlerta(
                        "Base de datos no disponible.\n" + ex.Message,
                        "ERROR BD",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    ).ShowDialog();

                    dtgMarcasIn.DataSource = null;
                    lblTotalRows.Text = "0";
                    lblTotalPages.Text = "0";
                    lblCurrentPage.Text = "0";
                }
                catch (Exception ex)
                {
                    new FrmAlerta(
                        "Ocurrió un error al cargar los datos:\n" + ex.Message,
                        "ERROR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    ).ShowDialog();

                    dtgMarcasIn.DataSource = null;
                    lblTotalRows.Text = "0";
                    lblTotalPages.Text = "0";
                    lblCurrentPage.Text = "0";
                }
            }
            finally
            {
                this.Visible = true;
            }

        }

        public async Task Editar()
        {
            VerificarSeleccionIdMarcaEdicion();
            if (SeleccionarMarca.idN > 0)
            {
                LimpiarControles();
                EliminarTabPage(tabPageIngresadasList);
                using (var loading = new FrmLoading(() => CargarDatosMarca()))
                {
                    loading.ShowDialog(this);
                }
                AnadirTabPage(tabPageMarcaDetail);
            }
        }
        private async void ibtnEditar_Click(object sender, EventArgs e)
        {
            await Editar();
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

                                // Actualizar el estado y la justificación en la base de datos
                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", justificacion, usuarioAbandono, "TRÁMITE", null);
                                FrmAlerta alerta = new FrmAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadMarcas();
                            }
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA MARCA PARA ABANDONAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            alerta.ShowDialog();
                            //MessageBox.Show("No hay marca seleccionada para abandonar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Filter = "Images (*.jpg;*.jpeg;*.png;*.tiff)|*.jpg;*.jpeg;*.png;*.tiff";
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

        private async Task MostrarEstadoNoAgregado()
        {
            await refrescarMarca();
            textBoxEstatus.Text = AgregarEtapa.etapa;
            richTextBox1.Text += "\n" + AgregarEtapa.anotaciones;
            if (AgregarEtapa.etapa == "Registrada")
            {
                checkBox1.Checked = true;
                mostrarPanelRegistro("si");
                txtRegistro.Text = "";
                txtFolio.Text = "";
                txtLibro.Text = "";
                dateTimePFecha_Registro.Value = DateTime.Now;
                ActualizarFechaVencimiento();
                VerificarDatosRegistro();

                if (comboBoxSignoDistintivo.Text == "Nombre comercial" && textBoxEstatus.Text == "Registrada")
                {
                    dateTimePFecha_vencimiento.Enabled = true;
                }
                else
                {
                    dateTimePFecha_vencimiento.Enabled = true;
                }
            }
            else
            {
                checkBox1.Checked = false;
                mostrarPanelRegistro("no");
                VerificarDatosRegistro();
            }
        }

        private async void roundedButton1_Click(object sender, EventArgs e)
        {

            FrmAgregarEtapa frmAgregarEtapa = new FrmAgregarEtapa();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                try
                {
                    await refrescarMarca();
                    agregoEstado = true;
                    textBoxEstatus.Text = AgregarEtapa.etapa;
                    richTextBox1.Text += "\n" + AgregarEtapa.anotaciones;
                    //historialModel.GuardarEtapa(SeleccionarMarca.idN, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE");
                    FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();

                    //await refrescarMarca();
                    if (AgregarEtapa.etapa == "Registrada")
                    {
                        checkBox1.Checked = true;
                        mostrarPanelRegistro("si");
                        txtRegistro.Text = "";
                        txtFolio.Text = "";
                        txtLibro.Text = "";
                        dateTimePFecha_Registro.Value = DateTime.Now;
                        ActualizarFechaVencimiento();
                        VerificarDatosRegistro();

                        if (comboBoxSignoDistintivo.Text == "Nombre comercial" && textBoxEstatus.Text == "Registrada")
                        {
                            dateTimePFecha_vencimiento.Enabled = true;
                        }
                        else
                        {
                            dateTimePFecha_vencimiento.Enabled = true;
                        }
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        mostrarPanelRegistro("no");
                        VerificarDatosRegistro();
                    }
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

        class ListarResp
        {
            public bool ok { get; set; }
            public int count { get; set; }
            public List<string> files { get; set; } = new();
            public string message { get; set; }
        }

        private async Task CrearCarpetaMarcaHttpAsync(string idMarca)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent("crear_carpeta_marca"), "action");
            form.Add(new StringContent(TOKEN), "auth");
            form.Add(new StringContent(idMarca ?? ""), "idMarca");

            using var resp = await HttpX.Client.PostAsync(URL, form);
            var body = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException($"HTTP {(int)resp.StatusCode}: {body}");
            // Opcional: validar JSON {"ok":true}
        }

        /* anterior
        public void CrearCarpetaMarca(string idMarca)
        {
            string carpetaMarca = $"{directorioBase}/marca-{idMarca}"; // Ruta completa para la carpeta de la marca

            using (FtpClient cliente = new FtpClient(host))
            {
                cliente.Credentials = new NetworkCredential(usuario, contraseña);

                try
                {
                    cliente.Connect(); // Conecta al servidor FTP

                    // Verifica si la carpeta ya existe
                    if (!cliente.DirectoryExists(carpetaMarca))
                    {
                        cliente.CreateDirectory(carpetaMarca); // Crea la carpeta
                        //MessageBox.Show($"Carpeta creada exitosamente: {carpetaMarca}");
                    }
                    else
                    {
                        //MessageBox.Show($"La carpeta ya existe: {carpetaMarca}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al crear la carpeta: {ex.Message}");
                }
                finally
                {
                    cliente.Disconnect(); // Desconecta del servidor FTP
                }
            }
        }*/

        private async Task SubirArchivoAsync(string idMarca)
        {
            using var ofd = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Seleccione un archivo",
                Filter = "Todos los archivos (*.*)|*.*"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var file = new FileInfo(ofd.FileName);
            if (file.Length > 20 * 1024 * 1024)
            {
                MessageBox.Show("El archivo supera 20MB.");
                return;
            }

            using var form = new MultipartFormDataContent();
            form.Add(new StringContent("subir"), "action");
            form.Add(new StringContent(TOKEN), "auth");
            form.Add(new StringContent(idMarca), "idMarca");

            // ✅ Enviar el nombre real como campo independiente
            form.Add(new StringContent(file.Name, System.Text.Encoding.UTF8, "text/plain"), "nombreArchivo");

            // 🔹 Archivo con header Content-Disposition manual (soporte UTF-8 con filename*)
            var fc = new StreamContent(File.OpenRead(file.FullName));

            // MIME por extensión
            var ext = file.Extension.ToLowerInvariant();
            fc.Headers.ContentType = new MediaTypeHeaderValue(ext switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream"
            });

            // 🔹 Aquí insertas el bloque del filenameStar
            var cd = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            cd.Name = "file";                   // campo "file" para PHP
            cd.FileName = "upload.bin";         // respaldo ASCII
            cd.FileNameStar = file.Name;        // ✅ nombre real UTF-8 ("Diseño sin título.png")
            fc.Headers.ContentDisposition = cd;

            // 👇 Importante: ahora agregas solo el contenido (sin pasar file.Name)
            form.Add(fc); // no uses form.Add(fc, "file", file.Name)

            using var resp = await HttpX.Client.PostAsync(URL, form);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                MessageBox.Show(body, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("ARCHIVO SUBIDO EXITOSAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /* 2
        private async Task SubirArchivoAsync(string idMarca)
        {
            using var ofd = new System.Windows.Forms.OpenFileDialog { Title = "Seleccione un archivo", Filter = "Todos los archivos (*.*)|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var file = new FileInfo(ofd.FileName);
            if (file.Length > 20 * 1024 * 1024) { MessageBox.Show("El archivo supera 20MB."); return; }

            using var form = new MultipartFormDataContent();
            form.Add(new StringContent("subir"), "action");
            form.Add(new StringContent(TOKEN), "auth");
            form.Add(new StringContent(idMarca), "idMarca");

            // 👉 Enviar el nombre real en un campo de texto (UTF-8)
            form.Add(new StringContent(file.Name, System.Text.Encoding.UTF8, "text/plain"), "nombreArchivo");

            // Archivo (el filename del header puede romper Unicode; no importa ya)
            var fc = new StreamContent(File.OpenRead(file.FullName));
            // MIME por extensión (png, jpg, pdf, etc.)
            var ext = file.Extension.ToLowerInvariant();
            fc.Headers.ContentType = new MediaTypeHeaderValue(
                ext switch { ".png" => "image/png", ".jpg" => "image/jpeg", ".jpeg" => "image/jpeg", ".gif" => "image/gif", ".pdf" => "application/pdf", _ => "application/octet-stream" }
            );
            form.Add(fc, "file", file.Name); // se mantiene, pero PHP usará nombreArchivo

            using var resp = await HttpX.Client.PostAsync(URL, form);
            var body = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode) { MessageBox.Show(body, "Error"); return; }
            MessageBox.Show("ARCHIVO SUBIDO EXITOSAMENTE");
        }*/





        /* funciona con http
        private async void SubirArchivo(string idMarca)
        {
            string carpeta = $"marca-{idMarca}";
            long limiteTamanio = 20 * 1024 * 1024; // 20MB

            using (var openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Seleccione un archivo para subir",
                Filter = "Todos los archivos (*.*)|*.*"
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string archivoLocal = openFileDialog.FileName;
                    FileInfo fileInfo = new FileInfo(archivoLocal);

                    if (fileInfo.Length > limiteTamanio)
                    {
                        MessageBox.Show("El archivo supera el límite permitido de 20MB.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        using (var httpClient = new HttpClient())
                        using (var formData = new MultipartFormDataContent())
                        {
                            formData.Add(new StringContent(idMarca), "idMarca");
                            var fileContent = new StreamContent(File.OpenRead(archivoLocal));
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                            formData.Add(fileContent, "file", Path.GetFileName(archivoLocal));

                            var response = await httpClient.PostAsync("https://foragro.com.es/peticiones/subir_archivo_marcas_nacionales.php", formData);
                            string result = await response.Content.ReadAsStringAsync();

                            MessageBox.Show(result, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al subir el archivo: {ex.Message}");
                    }
                }
            }
        }*/


        /* anterior
        private void SubirArchivo(string idMarca)
        {
            string carpeta = $"{directorioBase}/marca-{idMarca}/";
            long limiteTamanio = 20 * 1024 * 1024; // 20MB en bytes

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Seleccione un archivo para subir",
                Filter = "Todos los archivos (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string archivoLocal1 = openFileDialog.FileName;
                string nombreArchivo1 = System.IO.Path.GetFileName(archivoLocal1);

                // Verificar tamaño del archivo antes de subirlo
                FileInfo fileInfo = new FileInfo(archivoLocal1);
                if (fileInfo.Length > limiteTamanio)
                {
                    MessageBox.Show($"El archivo supera el límite de {limiteTamanio / (1024 * 1024)} MB (100MB).",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Default;
                    return; // No sube el archivo si es demasiado grande
                }

                try
                {
                    using (var client = new FtpClient(host, usuario, contraseña))
                    {
                        client.Connect();

                        // Crear carpeta si no existe
                        if (!client.DirectoryExists(carpeta))
                        {
                            client.CreateDirectory(carpeta);
                        }

                        // Subir el archivo
                        string rutaRemota = $"{carpeta}/{nombreArchivo1}";
                        client.UploadFile(archivoLocal1, rutaRemota, FtpRemoteExists.Overwrite);

                        FrmAlerta alerta = new FrmAlerta("ARCHIVO SUBIDO EXITOSAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al subir el archivo: {ex.Message}");
                }
                Cursor.Current = Cursors.Default;
            }
        }*/

        private async void roundedButton6_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            await loadHistorialById();
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

        public async Task EditarVerHistorial()
        {
            _cargandoUI = true;
            if (dtgHistorialIn.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialIn.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // OJO: la columna se llama "Id", no "id"
                    int id = Convert.ToInt32(dataRowView["Id"]);
                    SeleccionarHistorial.id = id;

                    DataTable historial = await historialModel.GetHistorialById(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];

                        // OJO: la columna se llama "origen", no "Origen"
                        if (fila["origen"].ToString() == "TRÁMITE")
                        {
                            SeleccionarHistorial.id = Convert.ToInt32(fila["Id"]);
                            SeleccionarHistorial.etapa = fila["etapa"].ToString();
                            SeleccionarHistorial.fecha = Convert.ToDateTime(fila["fecha"]);
                            SeleccionarHistorial.anotaciones = fila["anotaciones"].ToString();
                            SeleccionarHistorial.usuario = fila["usuario"].ToString();
                            SeleccionarHistorial.usuarioEdicion = fila["usuarioEdicion"].ToString();

                            // OJO: en tu DAO estás guardando todo como string,
                            // así que lo más seguro es validar string vacío.
                            if (!string.IsNullOrWhiteSpace(fila["fechaVencimiento"].ToString()))
                            {
                                labelVenc.Visible = true;
                                dateTimePickerFechaVencimiento.Visible = true;
                                dateTimePickerFechaVencimiento.Value =
                                    Convert.ToDateTime(fila["fechaVencimiento"].ToString());
                            }
                            else
                            {
                                labelVenc.Visible = false;
                                dateTimePickerFechaVencimiento.Visible = false;
                            }

                            comboBoxEstatusH.SelectedItem = SeleccionarHistorial.etapa;
                            dateTimePickerFechaIngreso.Value = SeleccionarHistorial.fecha;
                            richTextBoxAnotacionesH.Text = SeleccionarHistorial.anotaciones;
                            labelUserEditor.Text = UsuarioActivo.usuario;
                            lblUser.Text = SeleccionarHistorial.usuario;

                            AnadirTabPage(tabPageHistorialDetail);
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta(
                                "NO SE PUEDE EDITAR UN HISTORIAL QUE NO SEA DE TRÁMITE",
                                "ADVERTENCIA",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            alerta.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del historial",
                            "Mensaje",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta(
                    "SELECCIONE UNA FILA",
                    "MENSAJE",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );
                alerta.ShowDialog();
            }

            _cargandoUI = false;
        }


        /*
        public async Task EditarVerHistorial()
        {
            if (dtgHistorialIn.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialIn.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    DataTable historial = await historialModel.GetHistorialById(id);

                    if (historial.Rows.Count > 0)
                    {
                        _cargandoUI = true;
                        DataRow fila = historial.Rows[0];
                        if (fila["Origen"].ToString() == "TRÁMITE")
                        {
                            try
                            {
                                // Asignar los valores obtenidos a la clase SeleccionarPersona
                                SeleccionarHistorial.id = Convert.ToInt32(fila["id"]);
                                SeleccionarHistorial.etapa = fila["etapa"].ToString();
                                SeleccionarHistorial.fecha = Convert.ToDateTime(fila["fecha"]);
                                SeleccionarHistorial.anotaciones = fila["anotaciones"].ToString();
                                SeleccionarHistorial.usuario = fila["usuario"].ToString();
                                SeleccionarHistorial.usuarioEdicion = fila["usuarioEdicion"].ToString();




                                comboBoxEstatusH.SelectedItem = SeleccionarHistorial.etapa;
                                dateTimePickerFechaIngreso.Value = SeleccionarHistorial.fecha;
                                richTextBoxAnotacionesH.Text = SeleccionarHistorial.anotaciones;
                                labelUserEditor.Text = UsuarioActivo.usuario;
                                lblUser.Text = SeleccionarHistorial.usuario;

                                if (fila["fechaVencimiento"] != DBNull.Value && fila["fechaVencimiento"].ToString() != string.Empty)
                                {
                                    labelVenc.Visible = true;
                                    dateTimePickerFechaVencimiento.Visible = true;
                                    if (fila["fechaVencimiento"] != DBNull.Value && !string.IsNullOrWhiteSpace(fila["fechaVencimiento"].ToString()))
                                    {
                                        dateTimePickerFechaVencimiento.Value = Convert.ToDateTime(fila["fechaVencimiento"]);
                                    }
                                }
                                else
                                {
                                    labelVenc.Visible = false;
                                    dateTimePickerFechaVencimiento.Visible = false;
                                }

                                AnadirTabPage(tabPageHistorialDetail);

                            }
                            finally
                            {
                                _cargandoUI = false;

                            }
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO SE PUEDE EDITAR UN HISTORIAL QUE NO SEA DE TRÁMITE", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            alerta.ShowDialog();
                        }

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
        }*/
        private async void iconButton5_Click(object sender, EventArgs e)
        {
            await EditarVerHistorial();
        }

        private async void iconButton4_Click(object sender, EventArgs e)
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

                            DialogResult confirmacionRegistro = MessageBox.Show("Esta acción eliminará los datos de registro, folio, libro, fecha de registro y fecha de vencimiento. ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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

                        await loadHistorialById();
                        await refrescarMarca();
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
            if (_cargandoUI) return;              // <- clave

            //comboBoxEstado_SelectedIndexChanged(sender, e);
            if (!_actualizando && dateTimePickerFechaVencimiento.Visible)
            {
                _actualizando = true;
                dateTimePickerFechaVencimiento.Value = CalcularVencimiento(comboBoxEstatusH.Text, dateTimePickerFechaIngreso.Value);
                _actualizando = false;
            }
            ActualizarResumen();
        }

        private DateTime CalcularVencimiento(string etapa, DateTime fechaIngreso)
        {
            return etapa switch
            {
                "Examen de fondo" or "Objeción" or "Publicación" => fechaIngreso.AddMonths(2),
                "Requerimiento" or "Orden de pago" => fechaIngreso.AddMonths(1),
                "Resolución RPI desfavorable" => fechaIngreso.AddDays(5),
                _ => fechaIngreso
            };
        }


        private void ActualizarResumen()
        {
            string etapa = comboBoxEstatusH.Text;
            string fecha = dateTimePickerFechaIngreso.Value.ToString("dd/MM/yyyy");
            if (dateTimePickerFechaVencimiento.Visible)
            {
                string venc = dateTimePickerFechaVencimiento.Value.ToString("dd/MM/yyyy");
                if (etapa == "Resolución RPI desfavorable")
                    richTextBoxAnotacionesH.Text = $"{fecha} Por objeción - {etapa} | Fecha de vencimiento: {venc}";
                else
                    richTextBoxAnotacionesH.Text = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
            }
            else
            {
                if (etapa is "Resolución RPI favorable" or "Recurso de revocatoria" or
                    "Resolución Ministerio de Economía (MINECO)" or "Contencioso administrativo")
                    richTextBoxAnotacionesH.Text = $"{fecha} Por objeción - {etapa}";
                else
                    richTextBoxAnotacionesH.Text = $"{fecha} {etapa}";
            }
        }

        private void comboBoxEstatusH_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_cargandoUI) return;              // <- clave

            _actualizando = true;

            string etapa = comboBoxEstatusH.Text;
            DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;

            bool mostrarVencimiento =
                etapa == "Examen de fondo" ||
                etapa == "Requerimiento" ||
                etapa == "Objeción" ||
                etapa == "Publicación" ||
                etapa == "Orden de pago" ||
                etapa == "Resolución RPI desfavorable";

            labelVenc.Visible = mostrarVencimiento;
            dateTimePickerFechaVencimiento.Visible = mostrarVencimiento;

            if (mostrarVencimiento)
            {
                if (!dateTimePickerFechaVencimiento.Visible)
                    dateTimePickerFechaVencimiento.Value = CalcularVencimiento(etapa, fechaIngreso);
            }
            labelVenc.Visible = dateTimePickerFechaVencimiento.Visible = mostrarVencimiento;


            ActualizarResumen(); // arma el texto según valores actuales
            _actualizando = false;
            /*
            string etapa = comboBoxEstatusH.Text;
            DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;
            DateTime fechaVencimiento = fechaIngreso;

            // Calcular fecha de vencimiento según la etapa
            switch (etapa)
            {
                case "Examen de fondo":
                case "Objeción":
                case "Publicación":
                    fechaVencimiento = fechaIngreso.AddMonths(2);
                    break;

                case "Requerimiento":
                case "Orden de pago":
                    fechaVencimiento = fechaIngreso.AddMonths(1);
                    break;

                case "Resolución RPI desfavorable":
                    fechaVencimiento = fechaIngreso.AddDays(5);
                    break;
            }

            // Mostrar u ocultar controles de vencimiento
            bool mostrarVencimiento = etapa == "Examen de fondo" ||
                                       etapa == "Requerimiento" ||
                                       etapa == "Objeción" ||
                                       etapa == "Publicación" ||
                                       etapa == "Orden de pago" ||
                                       etapa == "Resolución RPI desfavorable";

            labelVenc.Visible = mostrarVencimiento;
            dateTimePickerFechaVencimiento.Visible = mostrarVencimiento;

            if (mostrarVencimiento)
            {
                dateTimePickerFechaVencimiento.Value = fechaVencimiento;
            }

            // Mostrar anotación en el RichTextBox
            string fecha = fechaIngreso.ToString("dd/MM/yyyy");
            string venc = fechaVencimiento.ToString("dd/MM/yyyy");

            if (etapa == "Resolución RPI desfavorable")
            {
                richTextBoxAnotacionesH.Text = $"{fecha} Por objeción - {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (mostrarVencimiento)
            {
                richTextBoxAnotacionesH.Text = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (etapa == "Resolución RPI favorable" ||
                     etapa == "Recurso de revocatoria" ||
                     etapa == "Resolución Ministerio de Economía (MINECO)" ||
                     etapa == "Contencioso administrativo")
            {
                richTextBoxAnotacionesH.Text = $"{fecha} Por objeción - {etapa}";
            }
            else
            {
                richTextBoxAnotacionesH.Text = $"{fecha} {etapa}";
            }*/
        }

        private async void btnEditarH_Click(object sender, EventArgs e)
        {


            if (_guardandoHist) return;
            if (comboBoxEstatusH.SelectedIndex == -1)
            {
                new FrmAlerta("NO HA SELECCIONADO NINGÚN ESTADO", "ADVERTENCIA",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
                return;
            }

            _guardandoHist = true;
            var btn = sender as Control;
            if (btn != null) btn.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                string usuarioCreador = lblUser.Text;           // quien creó el registro
                string usuarioEditor = labelUserEditor.Text;    // quien edita ahora
                string etapa = comboBoxEstatusH.Text;
                DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;

                bool requiereVencimiento =
                       etapa == "Examen de fondo"
                    || etapa == "Requerimiento"
                    || etapa == "Objeción"
                    || etapa == "Publicación"
                    || etapa == "Orden de pago"
                    || etapa == "Resolución RPI desfavorable";

                // RESPETAR lo que esté en el picker (si está visible / aplica)
                DateTime? fechaVencimiento = requiereVencimiento
                    ? dateTimePickerFechaVencimiento.Value
                    : (DateTime?)null;

                // Construir anotación
                string fecha = fechaIngreso.ToString("dd/MM/yyyy");
                string anotacionFinal;
                if (etapa == "Resolución RPI desfavorable" && fechaVencimiento.HasValue)
                    anotacionFinal = $"{fecha} Por objeción - {etapa} | Fecha de vencimiento: {fechaVencimiento.Value:dd/MM/yyyy}";
                else if (fechaVencimiento.HasValue)
                    anotacionFinal = $"{fecha} {etapa} | Fecha de vencimiento: {fechaVencimiento.Value:dd/MM/yyyy}";
                else if (etapa == "Resolución RPI favorable" ||
                         etapa == "Recurso de revocatoria" ||
                         etapa == "Resolución Ministerio de Economía (MINECO)" ||
                         etapa == "Contencioso administrativo")
                    anotacionFinal = $"{fecha} Por objeción - {etapa}";
                else
                    anotacionFinal = $"{fecha} {etapa}";

                // Evitar duplicar la misma línea; usa salto de línea para separar
                string actuales = richTextBoxAnotacionesH.Text ?? string.Empty;
                if (!actuales.Contains(anotacionFinal))
                    AgregarEtapa.anotaciones = string.IsNullOrWhiteSpace(actuales)
                        ? anotacionFinal
                        : anotacionFinal + Environment.NewLine + actuales;
                else
                    AgregarEtapa.anotaciones = actuales;

                // Guardar (asegúrate que el orden de parámetros coincide con tu modelo)
                bool ok = await historialModel.EditHistorialById(
                    SeleccionarHistorial.id,
                    etapa,
                    fechaIngreso,
                    AgregarEtapa.anotaciones,
                    usuarioCreador,        // mismo orden que usas en otros formularios
                    usuarioEditor,
                    fechaVencimiento       // nullable
                );

                if (ok)
                {
                    new FrmAlerta("ETAPA ACTUALIZADA", "ÉXITO",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information).ShowDialog();

                    // Refresca también el historial para ver el cambio
                    await loadHistorialById();
                    await refrescarMarca();

                    EliminarTabPage(tabPageHistorialDetail);
                    AnadirTabPage(tabPageHistorialMarca);
                    SeleccionarHistorial.id = 0;
                }
                else
                {
                    new FrmAlerta("NO SE PUDO ACTUALIZAR LA ETAPA", "ERROR",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                new FrmAlerta("ERROR AL ACTUALIZAR: " + ex.Message, "ERROR",
                              MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (btn != null) btn.Enabled = true;
                _guardandoHist = false;
            }

            /* anterior
        string usuario = lblUser.Text;
            string usuarioEditor = labelUserEditor.Text;
            string etapa = comboBoxEstatusH.Text;
            DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;
            DateTime fechaVencimiento = fechaIngreso;

            // Calcular vencimiento automático según etapa
            switch (etapa)
            {
                case "Examen de fondo":
                case "Objeción":
                case "Publicación":
                    fechaVencimiento = fechaIngreso.AddMonths(2);
                    break;

                case "Requerimiento":
                case "Orden de pago":
                    fechaVencimiento = fechaIngreso.AddMonths(1);
                    break;

                case "Resolución RPI desfavorable":
                    fechaVencimiento = fechaIngreso.AddDays(5);
                    break;
            }

            // Mostrar u ocultar controles de vencimiento
            bool requiereVencimiento = etapa == "Examen de fondo" ||
                                        etapa == "Requerimiento" ||
                                        etapa == "Objeción" ||
                                        etapa == "Publicación" ||
                                        etapa == "Orden de pago" ||
                                        etapa == "Resolución RPI desfavorable";

            // Asignar valores a AgregarEtapa
            AgregarEtapa.etapa = etapa;
            AgregarEtapa.fecha = fechaIngreso;
            AgregarEtapa.usuario = usuarioEditor;
            AgregarEtapa.fechaVencimiento = requiereVencimiento ? fechaVencimiento : null;

            if (comboBoxEstatusH.SelectedIndex != -1)
            {
                string anotaciones = richTextBoxAnotacionesH.Text;
                string fecha = fechaIngreso.ToString("dd/MM/yyyy");
                string venc = fechaVencimiento.ToString("dd/MM/yyyy");
                string anotacionFinal = "";

                if (etapa == "Resolución RPI desfavorable")
                {
                    anotacionFinal = $"{fecha} Por objeción - {etapa} | Fecha de vencimiento: {venc}";
                }
                else if (requiereVencimiento)
                {
                    anotacionFinal = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
                }
                else if (etapa == "Resolución RPI favorable" ||
                         etapa == "Recurso de revocatoria" ||
                         etapa == "Resolución Ministerio de Economía (MINECO)" ||
                         etapa == "Contencioso administrativo")
                {
                    anotacionFinal = $"{fecha} Por objeción - {etapa}";
                }
                else
                {
                    anotacionFinal = $"{fecha} {etapa}";
                }

                if (!anotaciones.Contains(anotacionFinal))
                {
                    AgregarEtapa.anotaciones = anotacionFinal + " " + anotaciones;
                }
                else
                {
                    AgregarEtapa.anotaciones = anotaciones;
                }

                // Guardar en base de datos
                bool actualizado = await historialModel.EditHistorialById(
                    SeleccionarHistorial.id,
                    etapa,
                    fechaIngreso,
                    AgregarEtapa.anotaciones,
                    usuario,
                    usuarioEditor,
                    requiereVencimiento ? fechaVencimiento : (DateTime?)null
                );

                if (actualizado)
                {
                    FrmAlerta alerta = new FrmAlerta("ETAPA ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    EliminarTabPage(tabPageHistorialDetail);
                    AnadirTabPage(tabPageMarcaDetail);
                    SeleccionarHistorial.id = 0;
                    await refrescarMarca();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGÚN ESTADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }*/
        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
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

        private async void iconButton1_Click_1(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageHistorialMarca);

            if (agregoEstado)
            {
                await MostrarEstadoNoAgregado();
            }

            AnadirTabPage(tabPageMarcaDetail);
        }

        private void btnActualizarM_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {

        }

        private async void dtgMarcasIn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            await Editar();
        }

        public void VerificarDatosRegistro()
        {
            if (checkBox1.Checked == true && (string.IsNullOrEmpty(txtRegistro.Text) || string.IsNullOrEmpty(txtFolio.Text) || string.IsNullOrEmpty(txtLibro.Text)))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }



        }

        public void VerificarDatosIngresados()
        {
            if (checkBox1.Checked == true && (string.IsNullOrEmpty(SeleccionarMarca.registro) || string.IsNullOrEmpty(SeleccionarMarca.libro) || string.IsNullOrEmpty(SeleccionarMarca.folio)))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }

        private void CentrarPanel()
        {

            int anchoMinimo = panelBusqueda.Width + 100;

            if (tabControl1.ClientSize.Width >= anchoMinimo)
            {
                // Pantalla suficientemente ancha → centrar
                panelBusqueda.Anchor = AnchorStyles.None;
                panelBusqueda.Dock = DockStyle.Top;
                //dtgMarcasIn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            else
            {
                // Pantalla pequeña → top-left
                //dtgMarcasIn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                //dtgMarcasIn.ScrollBars = ScrollBars.Both;
                panelBusqueda.Dock = DockStyle.None;
                panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panelBusqueda.Location = new Point(0, 0); // o donde quieras
            }
        }
        private async void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = await marcaModel.GetFilteredMarcasSinRegistroCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            await filtrar();
        }

        private async void iconButton4_Click_1(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                //bool tieneRegistro = await marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idN);
                bool tieneRegistro = false;
                if (!String.IsNullOrEmpty(txtRegistro.Text))
                {
                    tieneRegistro = true;
                }
                else
                {
                    tieneRegistro = false;
                }

                if (tieneRegistro)
                {
                    bool existeRegistro = await marcaModel.ExisteRegistro(txtRegistro.Text.Trim(), SeleccionarMarca.idN);
                    if (existeRegistro)
                    {
                        FrmAlerta alerta = new FrmAlerta("EL NÚMERO DE REGISTRO YA EXISTE", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        alerta.ShowDialog();
                        return;
                    }
                    else
                    {
                        if (!archivoSubido && checkBox1.Checked)
                        {
                            FrmAlerta alerta = new FrmAlerta("DEBE SUBIR EL TÍTULO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta.ShowDialog();
                        }
                        else
                        {
                            await ActualizarMarcaInternacional();

                        }
                    }
                }
                else
                {
                    await ActualizarMarcaInternacional();
                }

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void iconButton2_Click_1(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                //Enviar a oposicion

                AgregarEtapa.solicitante = txtNombreTitular.Text;
                FrmEnviarAOposicionI frmEnviarAOposicion = new FrmEnviarAOposicionI();
                frmEnviarAOposicion.ShowDialog();


                if (AgregarEtapa.enviadoAOposicion == true)
                {
                    EliminarTabPage(tabPageMarcaDetail);
                    EliminarTabPage(tabPageHistorialMarca);
                    AnadirTabPage(tabPageIngresadasList);
                    tabControl1.SelectedTab = tabPageIngresadasList;
                    await LoadMarcas();
                    FrmAlerta alerta = new FrmAlerta("MARCA ENVIADA A OPOSICIÓN", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void iconButton5_Click_1(object sender, EventArgs e)
        {
            DatosRegistro.peligro = false;
            AnadirTabPage(tabPageIngresadasList);
            EliminarTabPage(tabPageMarcaDetail);
            await LoadMarcas();
            SeleccionarMarca.idN = 0;
            LimpiarFormulario();

        }

        private async void dtgHistorialIn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                await EditarVerHistorial();
            }

        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            await filtrar();

        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = await marcaModel.GetFilteredMarcasSinRegistroCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                await filtrar();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            if (_isLoading) return;
            _isLoading = true;

            currentPageIndex = 1;
            SetLoading(true);
            try
            {
                await RefreshPageAsync();
                UpdatePagerLabels();
            }
            finally
            {
                _isLoading = false;
                SetLoading(false);
            }
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_isLoading) return;
            if (currentPageIndex <= 1) return;

            _isLoading = true;
            currentPageIndex--;
            SetLoading(true);
            try
            {
                await RefreshPageAsync();
                UpdatePagerLabels();
            }
            finally
            {
                _isLoading = false;
                SetLoading(false);
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (_isLoading) return;
            if (currentPageIndex >= totalPages) return;

            _isLoading = true;
            currentPageIndex++;
            SetLoading(true);
            try
            {
                await RefreshPageAsync();
                UpdatePagerLabels();
            }
            finally
            {
                _isLoading = false;
                SetLoading(false);
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {

            if (_isLoading) return;
            if (totalPages <= 0) return;

            _isLoading = true;
            currentPageIndex = totalPages;
            SetLoading(true);
            try
            {
                await RefreshPageAsync();
                UpdatePagerLabels();
            }
            finally
            {
                _isLoading = false;
                SetLoading(false);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtRegistro_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLibro_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEstatus_TextChanged(object sender, EventArgs e)
        {
        }

        private void dateTimePFecha_Registro_ValueChanged_1(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                Rectangle tabRect = tabControl1.GetTabRect(i);
                if (tabRect.Contains(e.Location))
                {
                    // Ignorar clic en la pestaña
                    return;
                }
            }
        }

        private async Task<List<string>> ListarNombresDeArchivosHttpAsync(string idMarca)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent("listar_archivos"), "action");
            form.Add(new StringContent(TOKEN), "auth");
            form.Add(new StringContent(idMarca ?? ""), "idMarca");

            using var resp = await HttpX.Client.PostAsync(URL, form);
            var body = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException($"HTTP {(int)resp.StatusCode}: {body}");

            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<ListarResp>(body, opts);
            if (data == null || !data.ok) throw new InvalidOperationException(data?.message ?? "Error al listar archivos");

            return data.files;
        }

        /* anterior
        private List<string> ListarNombresDeArchivos(string idMarca)
        {
            string carpetaMarca = $"{directorioBase}/marca-{idMarca}";
            var nombresArchivos = new List<string>();

            using (FtpClient cliente = new FtpClient(host))
            {
                cliente.Credentials = new NetworkCredential(usuario, contraseña);

                try
                {
                    cliente.Connect();

                    // Obtener listado de archivos en el directorio
                    var listado = cliente.GetListing(carpetaMarca);

                    foreach (var item in listado)
                    {
                        if (item.Type == FtpObjectType.File) // Solo archivos
                        {
                            nombresArchivos.Add(item.Name); // Agregar solo el nombre del archivo
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al listar archivos: {ex.Message}");
                }
                finally
                {
                    cliente.Disconnect();
                }
            }

            return nombresArchivos;
        }*/

        public async Task ListarArchivosEnGeneral()
        {
            try
            {
                // Cambiar el cursor global a "WaitCursor"
                Cursor.Current = Cursors.WaitCursor;

                AnadirTabPage(tabPageListaArchivos);
                tabControl1.Visible = false;

                string id = "" + SeleccionarMarca.idN;
                await CrearCarpetaMarcaHttpAsync(id);

                // Obtener nombres de archivos desde el servidor FTP
                var nombresArchivos = await ListarNombresDeArchivosHttpAsync(id);

                // Limpiar y configurar DataGridView
                dtgArchivos.DataSource = null;
                dtgArchivos.Columns.Clear();
                dtgArchivos.Columns.Add("NombreArchivo", "Nombre del Archivo");

                // Agregar los nombres al DataGridView
                foreach (var nombre in nombresArchivos)
                {
                    dtgArchivos.Rows.Add(nombre);
                }

                dtgArchivos.ClearSelection();

            }
            finally
            {
                // Restaurar el cursor global a "Default"
                Cursor.Current = Cursors.Default;
                tabControl1.Visible = true;
            }
        }

        private async void roundedButton2_Click_1(object sender, EventArgs e)
        {
            await ListarArchivosEnGeneral();

        }

        private async void iconButton7_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageListaArchivos);

            if (agregoEstado)
            {
                await MostrarEstadoNoAgregado();
            }

            AnadirTabPage(tabPageMarcaDetail);

        }

        private async void iconButton8_Click(object sender, EventArgs e)
        {
            await SubirArchivoAsync(SeleccionarMarca.idN.ToString());
            await ListarArchivosEnGeneral();
        }


        private async void AbrirArchivoDesdeHttpAsync(string idMarca, string archivoNombre)
        {
            try
            {
                using var form = new MultipartFormDataContent {
            { new StringContent("descargar"),     "action" },
            { new StringContent(TOKEN),           "auth" },
            { new StringContent(idMarca ?? ""),   "idMarca" },
            { new StringContent(archivoNombre ?? ""), "archivoNombre" }
        };

                // Fuerza HTTP/1.1 y acepta binario/imagen
                var req = new HttpRequestMessage(HttpMethod.Post, URL) { Content = form, Version = HttpVersion.Version11 };
                req.Headers.Accept.Clear();
                req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("image/*"));

                using var resp = await HttpX.Client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"HTTP {(int)resp.StatusCode}\n{err}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Nombre final (Content-Disposition o el que pediste)
                var cd = resp.Content.Headers.ContentDisposition;
                var nombre = cd?.FileNameStar ?? cd?.FileName?.Trim('"') ?? archivoNombre;
                foreach (var ch in Path.GetInvalidFileNameChars()) nombre = nombre.Replace(ch, '_');

                var rutaLocal = Path.Combine(Path.GetTempPath(), nombre);

                // Stream → archivo (sin convertir a texto)
                await using (var input = await resp.Content.ReadAsStreamAsync())
                await using (var output = new FileStream(rutaLocal, FileMode.Create, FileAccess.Write, FileShare.Read, 81920, true))
                {
                    await input.CopyToAsync(output);
                }

                if (File.Exists(rutaLocal))
                {
                    var p = new Process
                    {
                        StartInfo = new ProcessStartInfo { FileName = rutaLocal, UseShellExecute = true }
                    };
                    p.Start();
                }
                else
                {
                    var alerta = new FrmAlerta("EL ARCHIVO NO SE DESCARGÓ CORRECTAMENTE", "ERROR",
                                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo: {ex.Message}");
            }
        }


        /* anterior
        private void AbrirArchivoDesdeFtp(string idMarca, string archivoNombre)
        {
            string carpeta = $"{directorioBase}/marca-{idMarca}/";
            string rutaRemota = $"{carpeta}/{archivoNombre}";
            string rutaLocal = Path.Combine(Path.GetTempPath(), archivoNombre); // Carpeta temporal

            try
            {
                using (var cliente = new FtpClient(host, usuario, contraseña))
                {
                    cliente.Connect();

                    // Descargar el archivo al directorio temporal
                    cliente.DownloadFile(rutaLocal, rutaRemota, FtpLocalExists.Overwrite, FtpVerify.None);
                }

                // Asegúrate de que el archivo existe localmente antes de abrirlo
                if (File.Exists(rutaLocal))
                {
                    // Abre el archivo con la aplicación predeterminada de manera confiable
                    var process = new System.Diagnostics.Process
                    {
                        StartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = rutaLocal,
                            UseShellExecute = true // Importante para manejar rutas complejas
                        }
                    };
                    process.Start();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("EL ARCHIVO NO SE DESCARGÓ CORRECTAMENTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo: {ex.Message}");
            }
        }*/




        public void Abrir()
        {
            string idMarca = "" + SeleccionarMarca.idN; // Id de la marca actual
            string archivoNombre = dtgArchivos.CurrentRow?.Cells[0].Value?.ToString(); // Archivo seleccionado

            if (string.IsNullOrEmpty(archivoNombre))
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN ARCHIVO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            AbrirArchivoDesdeHttpAsync(idMarca, archivoNombre);
            Cursor.Current = Cursors.Default;
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void dtgArchivos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Abrir();
        }

        private async Task EliminarArchivoAsync(string idMarca, string archivoNombre)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent("eliminar"), "action");
            form.Add(new StringContent(TOKEN), "auth");
            form.Add(new StringContent(idMarca), "idMarca");
            form.Add(new StringContent(archivoNombre), "archivoNombre");

            using var resp = await HttpX.Client.PostAsync(URL, form);
            var body = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
            {
                MessageBox.Show(body, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("ARCHIVO ELIMINADO EXITOSAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /* anterior
        private void EliminarArchivoDesdeFtp(string idMarca, string archivoNombre)
        {
            string carpeta = $"{directorioBase}/marca-{idMarca}/";
            string rutaRemota = $"{carpeta}/{archivoNombre}";

            try
            {
                using (var cliente = new FtpClient(host, usuario, contraseña))
                {
                    cliente.Connect();

                    // Verifica si el archivo existe antes de intentar eliminarlo
                    if (cliente.FileExists(rutaRemota))
                    {
                        cliente.DeleteFile(rutaRemota);
                        FrmAlerta alerta = new FrmAlerta("ARCHIVO ELIMINADO EXITOSAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                    }
                    else
                    {
                        FrmAlerta alerta = new FrmAlerta("EL ARCHIVO NO EXISTE EN EL SERVIDOR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL ELIMINAR EL ARCHIVO: " + ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }*/

        public async Task Eliminar()
        {
            string idMarca = "" + SeleccionarMarca.idN; // Id de la marca actual
            string? archivoNombre = dtgArchivos.CurrentRow?.Cells[0].Value?.ToString(); // Archivo seleccionado

            if (string.IsNullOrEmpty(archivoNombre))
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN ARCHIVO A ELIMINAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }

            FrmAlerta alerta2 = new FrmAlerta($"¿ESTÁ SEGURO DE ELIMINAR EL ARCHIVO \"{archivoNombre}\"?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            var confirmacion = alerta2.ShowDialog();

            if (confirmacion == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                await EliminarArchivoAsync(idMarca, archivoNombre);

                // Actualizar la lista de archivos en el DataGridView
                await ListarArchivosEnGeneral();
                Cursor.Current = Cursors.Default;
            }
        }
        private async void iconButton10_Click(object sender, EventArgs e)
        {
            await Eliminar();
        }

        private async Task SubirArchivoRegistroAsync(string idMarca)
        {
            using var ofd = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Seleccione un archivo",
                Filter = "Todos los archivos (*.*)|*.*"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var file = new FileInfo(ofd.FileName);
            if (file.Length > 20 * 1024 * 1024)
            {
                MessageBox.Show("El archivo supera 20MB.");
                return;
            }

            using var form = new MultipartFormDataContent();
            form.Add(new StringContent("subir"), "action");
            form.Add(new StringContent(TOKEN), "auth");
            form.Add(new StringContent(idMarca), "idMarca");

            // ✅ Enviar el nombre real como campo independiente
            form.Add(new StringContent(file.Name, System.Text.Encoding.UTF8, "text/plain"), "nombreArchivo");

            // 🔹 Archivo con header Content-Disposition manual (soporte UTF-8 con filename*)
            var fc = new StreamContent(File.OpenRead(file.FullName));

            // MIME por extensión
            var ext = file.Extension.ToLowerInvariant();
            fc.Headers.ContentType = new MediaTypeHeaderValue(ext switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream"
            });

            // 🔹 Aquí insertas el bloque del filenameStar
            var cd = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            cd.Name = "file";                   // campo "file" para PHP
            cd.FileName = "upload.bin";         // respaldo ASCII
            cd.FileNameStar = file.Name;        // ✅ nombre real UTF-8 ("Diseño sin título.png")
            fc.Headers.ContentDisposition = cd;

            // 👇 Importante: ahora agregas solo el contenido (sin pasar file.Name)
            form.Add(fc); // no uses form.Add(fc, "file", file.Name)

            using var resp = await HttpX.Client.PostAsync(URL, form);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                MessageBox.Show(body, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                archivoSubido = false;
                return;
            }
            else
            {
                archivoSubido = true;
            }

            MessageBox.Show("ARCHIVO SUBIDO EXITOSAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /* anterior
        private void SubirArchivoRegistro(string idMarca)
        {
            string carpeta = $"{directorioBase}/marca-{idMarca}/";
            long limiteTamanio = 20 * 1024 * 1024; // 20MB en bytes

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Seleccione un archivo para subir",
                Filter = "Todos los archivos (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string archivoLocal1 = openFileDialog.FileName;
                string nombreArchivo1 = System.IO.Path.GetFileName(archivoLocal1);

                // Verificar tamaño del archivo antes de subirlo
                FileInfo fileInfo = new FileInfo(archivoLocal1);
                if (fileInfo.Length > limiteTamanio)
                {
                    MessageBox.Show($"El archivo supera el límite de {limiteTamanio / (1024 * 1024)} MB (20MB).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Default;
                    return; // No sube el archivo si es demasiado grande
                }

                try
                {
                    using (var client = new FtpClient(host, usuario, contraseña))
                    {
                        client.Connect();

                        // Crear carpeta si no existe
                        if (!client.DirectoryExists(carpeta))
                        {
                            client.CreateDirectory(carpeta);
                        }

                        // Subir el archivo
                        string rutaRemota = $"{carpeta}/{nombreArchivo1}";
                        client.UploadFile(archivoLocal1, rutaRemota, FtpRemoteExists.Overwrite);

                        FrmAlerta alerta = new FrmAlerta("ARCHIVO SUBIDO EXITOSAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();

                        archivoSubido = true; // Indicar que el archivo se ha subido correctamente
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al subir el archivo: {ex.InnerException.Message}");
                    archivoSubido = false;
                }
                Cursor.Current = Cursors.Default;
            }
            else
            {
                archivoSubido = false;
            }
        }*/

        private async void btnAdjuntarT_Click(object sender, EventArgs e)
        {
            await SubirArchivoRegistroAsync("" + SeleccionarMarca.idN);
            if (!archivoSubido)
            {
                FrmAlerta alerta = new FrmAlerta("NO SE HA SELECCIONADO NI SUBIDO NINGÚN ARCHIVO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                archivoSubido = false;
            }
            else
            {
                archivoSubido = true;
            }
        }

        private void comboBoxSignoDistintivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSignoDistintivo.Text == "Nombre comercial" && textBoxEstatus.Text == "Registrada")
            {
                dateTimePFecha_vencimiento.Enabled = true;
            }
            else
            {
                dateTimePFecha_vencimiento.Enabled = true;
            }
        }



        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgMarcasIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgMarcasIn.Columns["id"] != null)
                dtgMarcasIn.Columns["id"].Visible = false;

            dtgMarcasIn.Columns["CLASE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dtgMarcasIn.Columns["CLASE"].Width = 50;      // ancho fijo
            dtgMarcasIn.Columns["CLASE"].MinimumWidth = 40; // opcional

            dtgMarcasIn.ClearSelection();
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmMarcasIntIngresadas_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private async void btnDesistir_Click(object sender, EventArgs e)
        {
            using (FrmJustificacionDesistimiento justificacionForm = new FrmJustificacionDesistimiento())
            {

                if (justificacionForm.ShowDialog() == DialogResult.OK)
                {
                    string justificacion = justificacionForm.Justificacion;
                    DateTime fechaAbandono = justificacionForm.fecha;
                    string usuarioAbandono = justificacionForm.usuarioAbandono;

                    try
                    {

                        if (dtgMarcasIn.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasIn.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);

                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Desistimiento", justificacion, usuarioAbandono, "TRÁMITE", null);
                                FrmAlerta alerta = new FrmAlerta("LA MARCA HA SIDO MARCADA COMO DESISTIDA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadMarcas();
                            }
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA MARCA PARA DESISTIR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            alerta.ShowDialog();
                            //MessageBox.Show("No hay marca seleccionada para abandonar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dateTimePickerFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            if (_cargandoUI) return;

            if (_actualizando) return; // ignore cambios programáticos
                                       // NO recalcules aquí; respeta la edición manual
            ActualizarResumen();
        }

        private void dtgHistorialIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgHistorialIn.Columns["id"] != null)
            {
                dtgHistorialIn.Columns["id"].Visible = false;
                dtgHistorialIn.ClearSelection();
            }
        }

        private void toggleIndefinido_CheckedChanged(object sender, EventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                if (toggleIndefinido.Checked)
                {
                    dateTimePFecha_vencimiento.Enabled = false;
                    dateTimePFecha_vencimiento.Format = DateTimePickerFormat.Custom;
                    dateTimePFecha_vencimiento.CustomFormat = "--";

                }
                else
                {
                    dateTimePFecha_vencimiento.Enabled = true;
                    dateTimePFecha_vencimiento.Format = DateTimePickerFormat.Custom;
                    dateTimePFecha_vencimiento.CustomFormat = "dd/MM/yyyy";
                    ActualizarFechaVencimiento();
                }
            }
        }
    }
}
