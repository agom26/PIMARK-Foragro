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

namespace Presentacion.Patentes
{

    public partial class FrmMostrarIngresadasPatentes : Form, IAsyncLoadable
    {
        PatenteModel patenteModel = new PatenteModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialPatenteModel historialPatenteModel = new HistorialPatenteModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        bool agregoEstado = false;
        private bool archivoSubido = false;
        private bool buscando = false;
        private bool _isLoading;

        //ftp
        const string URL = "https://foragro.com.es/peticiones/archivos_patentes.php";
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
        class ListarResp
        {
            public bool ok { get; set; }
            public int count { get; set; }
            public List<string> files { get; set; } = new();
            public string message { get; set; }
        }
        public async Task LoadAsync()
        {
            await LoadPatentes(); // aquí llamas a tu método actual
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

        private Task RefreshPageAsync() => buscando ? filtrar() : LoadPatentes();
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

        private void SetDoubleBuffering(System.Windows.Forms.Control control, bool enable)
        {
            // Habilitar o deshabilitar DoubleBuffering
            typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                           .SetValue(control, enable, null);
        }

        public FrmMostrarIngresadasPatentes()
        {
            InitializeComponent();
            SetDoubleBuffering(dtgPatentes,true);
            SetDoubleBuffering(dtgHistorial, true);

            if (UsuarioActivo.soloLectura)
            {
                //botones
                btnAbandonar.Visible = false;
                btnDesistir.Visible = false;

                //formulario
                txtCaso.Enabled = false;
                txtExpediente.Enabled = false;
                txtNombre.Enabled = false;
                comboBoxTipo.Enabled = false;
                comboBoxAnualidades.Enabled = false;
                checkBoxPCT.Enabled = false;
                datePickerFechaSolicitud.Enabled = false;
                dateTimePFecha_vencimiento.Enabled = false;
                btnAgregarEstado.Enabled = false;
                textBoxEstatus.Enabled = false;

                btnAgregarTitular.Enabled = false;
                txtDireccionTitular.Enabled = false;
                txtNombreTitular.Enabled = false;

                btnAgregarAgente.Enabled = false;
                txtNombreAgente.Enabled = false;

                checkedListBoxDocumentos.Enabled = false;
                txtRegistro.Enabled = false;
                txtLibro.Enabled = false;
                txtFolio.Enabled = false;
                dateTimePFecha_vencimiento.Enabled = false;

                btnEditarPatente.Visible = false;
                btnAdjuntarT.Visible = false;

                //archivos
                btnSubirArchivos.Visible = false;
                btnEliminarArchivos.Visible = false;

                //historial
                comboBoxEstatusH.Enabled = false;
                dateTimePickerFechaIngreso.Enabled = false;
                dateTimePFecha_vencimiento.Enabled = false;
                richTextBoxAnotacionesH.Enabled = false;
                btnEditarH.Visible = false;
                btnEditarEstadoHistorial.Visible = false;
               
            }
            else
            {
                //botones
                btnAbandonar.Visible = true;
                btnDesistir.Visible = true;

                //formulario
                txtCaso.Enabled = true;
                txtExpediente.Enabled = true;
                txtNombre.Enabled = true;
                comboBoxTipo.Enabled = true;
                comboBoxAnualidades.Enabled = true;
                checkBoxPCT.Enabled = true;
                datePickerFechaSolicitud.Enabled = true;
                dateTimePFecha_vencimiento.Enabled = true;
                btnAgregarEstado.Enabled = true;
                textBoxEstatus.Enabled = true;

                btnAgregarTitular.Enabled = true;
                txtDireccionTitular.Enabled = true;
                txtNombreTitular.Enabled = true;

                btnAgregarAgente.Enabled = true;
                txtNombreAgente.Enabled = true;

                checkedListBoxDocumentos.Enabled = true;
                txtRegistro.Enabled = true;
                txtLibro.Enabled = true;
                txtFolio.Enabled = true;
                dateTimePFecha_vencimiento.Enabled = true;

                btnEditarPatente.Visible = true;
                btnAdjuntarT.Visible = false;

                //variable
                archivoSubido = false;

                //archivos
                btnSubirArchivos.Visible = true;
                btnEliminarArchivos.Visible = true;

                //historial
                comboBoxEstatusH.Enabled = true;
                dateTimePickerFechaIngreso.Enabled = true;
                dateTimePFecha_vencimiento.Enabled = true;
                richTextBoxAnotacionesH.Enabled = true;
                btnEditarH.Visible = true;
                btnEditarEstadoHistorial.Visible = true;

            }

                
        }

        private async Task LoadPatentes()
        {
            totalRows = await patenteModel.GetTotalPatentesSinRegistro();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            var marcasN = await  patenteModel.GetAllPatentesEnTramite(currentPageIndex, pageSize);

            void Apply()
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                lblCurrentPage.Text = currentPageIndex.ToString();
                dtgPatentes.DataSource = marcasN;
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
                totalRows = await Task.Run(() =>
                        patenteModel.GetFilteredPatentesSinRegistroCount(buscar));
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();

                DataTable dt = await Task.Run(() =>
                        patenteModel.FiltrarPatentesEnTramite(buscar, currentPageIndex, pageSize));
                if (dt.Rows.Count > 0)
                {
                    dtgPatentes.DataSource = dt;
                    if (dtgPatentes.Columns["id"] != null) dtgPatentes.Columns["id"].Visible = false;
                    dtgPatentes.ClearSelection();
                }
                else
                {
                    new FrmAlerta("NO EXISTEN PATENTES CON ESOS DATOS", "MENSAJE",
                                  MessageBoxButtons.OK, MessageBoxIcon.None).ShowDialog();
                    await LoadPatentes();
                }
            }
            else
            {
                await LoadPatentes();
            }
        }

        /* anterior

        private async Task LoadPatentes()
        {
            try
            {
                totalRows = await Task.Run(() => patenteModel.GetTotalPatentesSinRegistro());
                totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalRows) / pageSize));
                // Obtiene los usuarios
                var marcasN = await Task.Run(() => patenteModel.GetAllPatentesEnTramite(currentPageIndex, pageSize));

                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        lblTotalPages.Text = totalPages.ToString();
                        lblTotalRows.Text = totalRows.ToString();
                        dtgPatentes.DataSource = marcasN;

                    }));
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
        public async void filtrar()
        {
            string buscar = txtBuscar.Text.Trim();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                try
                {
                    totalRows = await Task.Run(() =>
                        patenteModel.GetFilteredPatentesSinRegistroCount(buscar));

                    totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();

                    DataTable titulares = await Task.Run(() =>
                        patenteModel.FiltrarPatentesEnTramite(buscar, currentPageIndex, pageSize));

                    if (titulares.Rows.Count > 0)
                    {
                        dtgPatentes.DataSource = titulares;

                        if (dtgPatentes.Columns["id"] != null)
                        {
                            dtgPatentes.Columns["id"].Visible = false;
                        }

                        dtgPatentes.ClearSelection();
                    }
                    else
                    {
                        new FrmAlerta(
                            "NO EXISTEN PATENTES CON ESOS DATOS",
                            "MENSAJE",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.None
                        ).ShowDialog();

                        await LoadPatentes();
                    }
                }
                catch (Exception ex)
                {
                    new FrmAlerta(
                        "Ocurrió un error al filtrar los datos: " + ex.Message,
                        "ERROR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    ).ShowDialog();
                }
            }
            else
            {
                await LoadPatentes();
            }
        }*/



        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }
        private void VerificarSeleccionIdPatenteEdicion()
        {
            if (dtgPatentes.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgPatentes.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgPatentes.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPatente.id = id;
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
        private void ActualizarFechaVencimiento()
        {
            DateTime fecha_solicitud = datePickerFechaSolicitud.Value;
            DateTime fecha_vencimiento = fecha_solicitud.AddYears(20);
            dateTimePFecha_vencimiento.Value = fecha_vencimiento;
        }

        public void mostrarPanelRegistro(string isRegistrada)
        {
            if (isRegistrada == "si")
            {
                btnAdjuntarT.Visible = true;
                ActualizarFechaVencimiento();
                lblVencimiento.Visible = true;
                dateTimePFecha_vencimiento.Visible = true;
                checkBox2.Checked = true;
                checkBox2.Enabled = false;
                panel2I.Visible = true;
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 62.5f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 37.5f;
            }
            else
            {
                btnAdjuntarT.Visible = false;
                lblVencimiento.Visible = false;
                dateTimePFecha_vencimiento.Visible = false;
                checkBox2.Enabled = false;
                checkBox2.Checked = false;
                panel2I.Visible = false;
                tableLayoutPanel1.RowStyles[0].Height = 0;
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

        private async Task CargarDatosPatente()
        {
            try
            {
                DataTable detallesPatente = await Task.Run(() => patenteModel.ObtenerPatentePorId(SeleccionarPatente.id));

                if (detallesPatente.Rows.Count > 0)
                {
                    DataRow row = detallesPatente.Rows[0];

                    if (row["expediente"] != DBNull.Value)
                    {
                        SeleccionarPatente.caso = row["caso"].ToString();
                        SeleccionarPatente.expediente = row["expediente"].ToString();
                        SeleccionarPatente.nombre = row["nombre"].ToString();
                        SeleccionarPatente.tipo = row["tipo"].ToString();
                        SeleccionarPatente.anualidades = int.Parse(row["anualidades"].ToString());
                        SeleccionarPatente.pct = row["pct"].ToString();
                        SeleccionarPatente.fecha_solicitud = Convert.ToDateTime(row["fecha_solicitud"].ToString());
                        SeleccionarPatente.estado = row["estado"].ToString();
                        SeleccionarPatente.idTitular = int.Parse(row["IdTitular"].ToString());
                        SeleccionarPatente.idAgente = int.Parse(row["IdAgente"].ToString());
                        SeleccionarPatente.comprobante_pagos = row["comprobante_pagos"].ToString();
                        SeleccionarPatente.descripcion = row["descripcion"].ToString();
                        SeleccionarPatente.reivindicaciones = row["reivindicaciones"].ToString();
                        SeleccionarPatente.dibujos = row["dibujos"].ToString();
                        SeleccionarPatente.resumen = row["resumen"].ToString();
                        SeleccionarPatente.documento_cesion = row["documento_cesion"].ToString();
                        SeleccionarPatente.poder_nombramiento = row["poder_nombramiento"].ToString();

                        /*
                        if (row["Erenov"] != DBNull.Value)
                        {
                            SeleccionarPatente.Erenov = row["Erenov"].ToString();
                            txtERenovacion.Text = SeleccionarMarca.erenov;
                        }

                        if (row["Etrasp"] != DBNull.Value)
                        {
                            SeleccionarPatente.Etrasp = row["Etrasp"].ToString();
                            txtETraspaso.Text = SeleccionarMarca.etraspaso;
                        }*/

                        var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarPatente.idTitular));
                        var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarPatente.idAgente));

                        await Task.WhenAll(titularTask, agenteTask);

                        var titular = titularTask.Result;
                        var agente = agenteTask.Result;

                        SeleccionarPersonaPatente.idPersonaT = SeleccionarPatente.idTitular;
                        SeleccionarPersonaPatente.idPersonaA = SeleccionarPatente.idAgente;

                        if (titular.Count > 0)
                        {
                            txtNombreTitular.Text = titular[0].nombre;
                            txtDireccionTitular.Text = titular[0].direccion;

                        }

                        if (agente.Count > 0)
                        {
                            txtNombreAgente.Text = agente[0].nombre;
                        }


                        // Actualizar los controles 
                        txtCaso.Text = SeleccionarPatente.caso;
                        txtExpediente.Text = SeleccionarPatente.expediente;
                        txtNombre.Text = SeleccionarPatente.nombre;
                        textBoxEstatus.Text = SeleccionarPatente.estado;
                        datePickerFechaSolicitud.Value = SeleccionarPatente.fecha_solicitud;
                        comboBoxTipo.SelectedItem = SeleccionarPatente.tipo;
                        comboBoxAnualidades.SelectedItem = SeleccionarPatente.anualidades.ToString();

                        if (SeleccionarPatente.pct == "si")
                        {
                            checkBoxPCT.Checked = true;
                        }
                        else
                        {
                            checkBoxPCT.Checked = false;
                        }

                        // Recorrer cada ítem del CheckedListBox
                        for (int i = 0; i < checkedListBoxDocumentos.Items.Count; i++)
                        {
                            string itemName = checkedListBoxDocumentos.Items[i].ToString();

                            // Comparar el nombre del ítem con las propiedades de SeleccionarPatente
                            if (itemName == "Comprobante de pagos" && SeleccionarPatente.comprobante_pagos == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Descripción (original y 1 copia)" && SeleccionarPatente.descripcion == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Reivindicaciones (original y 1 copia)" && SeleccionarPatente.reivindicaciones == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Dibujo(s) o fórmula (original y 1 copia)" && SeleccionarPatente.dibujos == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Resumen (original y 1 copia)" && SeleccionarPatente.resumen == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Documento de cesión" && SeleccionarPatente.documento_cesion == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Poder o nombramiento" && SeleccionarPatente.poder_nombramiento == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                        }

                        bool contieneRegistrada = await patenteModel.TieneEtapaRegistradaPatente(SeleccionarPatente.id);


                        if (contieneRegistrada)
                        {
                            if (SeleccionarPatente.registro != null)
                            {
                                SeleccionarPatente.registro = row["registro"].ToString();
                                SeleccionarPatente.folio = row["folio"].ToString();
                                SeleccionarPatente.libro = row["libro"].ToString();
                                SeleccionarPatente.fecha_registro = Convert.ToDateTime(row["fecha_registro"]);
                                SeleccionarPatente.fecha_vencimiento = Convert.ToDateTime(row["fecha_vencimiento"]);

                                txtRegistro.Text = SeleccionarPatente.registro;
                                txtFolio.Text = SeleccionarPatente.folio;
                                txtLibro.Text = SeleccionarPatente.libro;
                                dateTimePFecha_Registro.Value = SeleccionarPatente.fecha_registro.Value;
                                dateTimePFecha_vencimiento.Value = SeleccionarPatente.fecha_vencimiento.Value;
                            }
                            checkBox2.Checked = true;
                            mostrarPanelRegistro("si");

                        }
                        else
                        {
                            checkBox2.Checked = false;
                            mostrarPanelRegistro("no");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la patente seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles de la patente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarCampo(string campo)
        {
            return !string.IsNullOrEmpty(campo);
        }


        private bool ValidarCampos(string caso, string expediente, string nombre, string tipo, string anualidad, string estado,
                    bool registroChek, string registro, string folio, string libro)
        {
            // Lista para acumular mensajes de error
            List<string> mensajesError = new List<string>();

            // Validaciones de campos requeridos
            if (!ValidarCampo(caso))
                mensajesError.Add("INGRESE EL CASO\n");
            if (!ValidarCampo(expediente))
                mensajesError.Add("INGRESE EL EXPEDIENTE\n");
            if (!ValidarCampo(nombre))
                mensajesError.Add("INGRESE EL SIGNO\n");
            if (!ValidarCampo(tipo))
                mensajesError.Add("SELECCIONE UN TIPO\n");
            if (!ValidarCampo(anualidad))
                mensajesError.Add("SELECCIONE UN NÚMERO DE ANUALIDAD\n");
            if (!ValidarCampo(estado))
                mensajesError.Add("SELECCIONE UN ESTADO\n");

            // Validación de valores numéricos 

            if (!int.TryParse(anualidad, out _))
                mensajesError.Add("LA ANUALIDAD DEBE SER UN VALOR NUMÉRICO\n");

            if (registroChek)
            {
                if (!int.TryParse(registro, out _))
                    mensajesError.Add("EL REGISTRO DEBE SER UN VALOR NUMÉRICO\n");
                if (!int.TryParse(folio, out _))
                    mensajesError.Add("EL FOLIO DEBE SER UN VALOR NUMÉRICO\n");
                if (!int.TryParse(libro, out _))
                    mensajesError.Add("EL TOMO DEBE SER UN VALOR NUMÉRICO\n");
            }

            // Validación de campos de registro 
            if (registroChek)
            {
                if (!ValidarCampo(folio))
                    mensajesError.Add("INGRESE EL NÚMERO DE FOLIO\n");
                if (!ValidarCampo(registro))
                    mensajesError.Add("INGRESE EL NÚMERO DE REGISTRO\n");
                if (!ValidarCampo(libro))
                    mensajesError.Add("INGRESE EL NÚMERO DE TOMO\n");
            }

            // Si hay mensajes de error, mostrar la alerta con todos los mensajes
            if (mensajesError.Any())
            {
                string mensajeConcatenado = string.Join("", mensajesError);
                FrmAlerta alerta = new FrmAlerta(mensajeConcatenado, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return false;
            }

            return true;
        }

        public void LimpiarFomulario()
        {
            txtCaso.Text = "";
            txtExpediente.Text = "";
            txtNombre.Text = "";
            comboBoxTipo.SelectedIndex = -1;
            comboBoxAnualidades.SelectedIndex = -1;
            checkBoxPCT.Checked = false;
            datePickerFechaSolicitud.Value = DateTime.Now;
            AgregarEtapaPatente.LimpiarEtapa();
            textBoxEstatus.Text = "";
            SeleccionarPersonaPatente.LimpiarPersona();
            txtFolio.Text = "";
            txtLibro.Text = "";
            txtRegistro.Text = "";
            dateTimePFecha_Registro.Value = DateTime.Now;
            mostrarPanelRegistro("no");
            checkBoxPCT.Checked = false;
            txtNombreAgente.Text = "";
            txtDireccionTitular.Text = "";
            txtNombreTitular.Text = "";
            SeleccionarPersonaPatente.LimpiarPersona();
            ActualizarFechaVencimiento();

            for (int i = 0; i < checkedListBoxDocumentos.Items.Count; i++)
            {
                checkedListBoxDocumentos.SetItemChecked(i, false);
            }

            DatosRegistro.peligro = false;
        }

        public async Task EditarPatente()
        {
            string caso = txtCaso.Text;
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string? tipo = comboBoxTipo.SelectedItem?.ToString();
            string? anualidad = comboBoxAnualidades.SelectedItem?.ToString();
            int anualidades = int.Parse(anualidad);
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            int idTitular = SeleccionarPersonaPatente.idPersonaT;
            int idAgente = SeleccionarPersonaPatente.idPersonaA;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string pct = "no";
            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox2.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;
            string? erenov = null;
            string? etrasp = null;
            string comprobante_pagos = "no";
            string descripcion = "no";
            string reivindicaciones = "no";
            string dibujos = "no";
            string resumen = "no";
            string documento_cesion = "no";
            string poder_nombramiento = "no";

            // Validaciones
            if (idTitular <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("INGRESE UN TITULAR VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un titular válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idAgente <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("INGRESE UN AGENTE VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un agente válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkBoxPCT.Checked)
            {
                pct = "si";
            }

            // Validar las selecciones en el CheckedListBox
            if (checkedListBoxDocumentos.CheckedItems.Contains("Comprobante de pagos"))
            {
                comprobante_pagos = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Descripción (original y 1 copia)"))
            {
                descripcion = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Reivindicaciones (original y 1 copia)"))
            {
                reivindicaciones = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Dibujo(s) o fórmula (original y 1 copia)"))
            {
                dibujos = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Resumen (original y 1 copia)"))
            {
                resumen = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Documento de cesión"))
            {
                documento_cesion = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Poder o nombramiento"))
            {
                poder_nombramiento = "si";
            }



            // Validar campos 
            if (!ValidarCampos(caso, expediente, nombre, tipo, anualidad, estado, registroChek, registro, folio, libro))
            {
                return;
            }

            try
            {
                if (registroChek)
                {
                    try
                    {



                        bool actualizada = await patenteModel.EditarPatente(SeleccionarPatente.id, caso, expediente, nombre, estado, tipo, idTitular, idAgente, solicitud,
                            registro, folio, libro, fecha_registro, fecha_vencimiento, erenov, etrasp, anualidades, pct,
                            comprobante_pagos, descripcion, reivindicaciones, dibujos, resumen, documento_cesion,
                            poder_nombramiento);

                        if (actualizada)
                        {
                            if (agregoEstado == true)
                            {
                                historialPatenteModel.CrearHistorialPatente(Convert.ToDateTime(AgregarEtapaPatente.fecha), AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones, UsuarioActivo.usuario, null, SeleccionarPatente.id, null);
                                agregoEstado = false;
                            }

                            DatosRegistro.peligro = false;
                            FrmAlerta alerta = new FrmAlerta("PATENTE ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            await LoadPatentes();
                            AnadirTabPage(tabPageIngresadasList);
                            EliminarTabPage(tabPageMarcaDetail);
                            EliminarTabPage(tabPageListaArchivos);
                            EliminarTabPage(tabPageHistorialMarca);

                            LimpiarFomulario();
                        }


                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }
                }
                else
                {
                    try
                    {


                        bool actualizada = await patenteModel.EditarPatente(SeleccionarPatente.id, caso, expediente, nombre, estado, tipo, idTitular, idAgente, solicitud,
                            null, null, null, null, null, erenov, etrasp, anualidades, pct,
                            comprobante_pagos, descripcion, reivindicaciones, dibujos, resumen, documento_cesion,
                            poder_nombramiento);


                        if (actualizada)
                        {
                            if (agregoEstado == true)
                            {
                                historialPatenteModel.CrearHistorialPatente(Convert.ToDateTime(AgregarEtapaPatente.fecha), AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones, UsuarioActivo.usuario, null, SeleccionarPatente.id, null);
                                agregoEstado = false;
                            }

                            DatosRegistro.peligro = false;
                            FrmAlerta alerta = new FrmAlerta("PATENTE ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            await LoadPatentes();
                            AnadirTabPage(tabPageIngresadasList);
                            LimpiarFomulario();
                            EliminarTabPage(tabPageMarcaDetail);
                            EliminarTabPage(tabPageListaArchivos);
                            EliminarTabPage(tabPageHistorialMarca);


                        }

                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }
                }


                //LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al " + (registroChek ? "registrar" : "actualizar") + " la marca nacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //LimpiarFormulario();
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
        private async void loadHistorialById()
        {
            try
            {
                var historial = await Task.Run(() => historialPatenteModel.ObtenerHistorialPorIdPatente(SeleccionarPatente.id));


                Invoke(new Action(() =>
                {
                    dtgHistorial.AutoGenerateColumns = true;
                    dtgHistorial.DataSource = historial;
                    dtgHistorial.Refresh();

                    if (dtgHistorial.Columns["id"] != null)
                    {
                        dtgHistorial.Columns["id"].Visible = false;
                    }

                    dtgHistorial.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Habilitar()
        {
            dateTimePickerFechaIngreso.Enabled = true;
            comboBoxEstatusH.Enabled = true;
            richTextBoxAnotacionesH.Enabled = true;
            btnEditarH.Enabled = true;
        }
        public void Deshabilitar()
        {
            dateTimePickerFechaIngreso.Enabled = false;
            comboBoxEstatusH.Enabled = false;
            richTextBoxAnotacionesH.Enabled = true;
            richTextBoxAnotacionesH.ReadOnly = true;
            btnEditarH.Enabled = false;
        }

        private async Task refrescarPatente()
        {
            if (SeleccionarPatente.id > 0)
            {
                try
                {
                    DataTable detallesPatente = await Task.Run(() => patenteModel.ObtenerPatentePorId(SeleccionarPatente.id));

                    if (detallesPatente.Rows.Count > 0)
                    {
                        DataRow row = detallesPatente.Rows[0];

                        if (row["estado"] != DBNull.Value)
                        {
                            SeleccionarPatente.estado = row["estado"].ToString();
                            textBoxEstatus.Text = row["estado"].ToString();
                        }
                        else
                        {
                            //MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        bool contieneRegistrada = SeleccionarPatente.estado.Contains("Registro/concesión", StringComparison.OrdinalIgnoreCase);

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
                    MessageBox.Show("Error al refrescar los datos de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void FrmMostrarIngresadasPatentes_Load(object sender, EventArgs e)
        {

            this.Visible = false;
            try
            {
                // ===== tu init actual (déjalo igual) =====
                SeleccionarMarca.idN = 0;
                archivoSubido = false;
                btnAdjuntarT.Visible = false;

                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
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
                    dtgPatentes.DataSource = null;
                    lblTotalRows.Text = "0";
                    lblTotalPages.Text = "0";
                    lblCurrentPage.Text = "0";
                    return;
                }

                // 2) Intentar cargar desde tu servidor/API
                try
                {
                    await LoadPatentes(); // deja que lance excepciones
                }
                catch (HttpRequestException)
                {
                    new FrmAlerta(
                        "No se pudo comunicar con el servidor.",
                        "ERROR DE SERVIDOR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    ).ShowDialog();

                    dtgPatentes.DataSource = null;
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

                    dtgPatentes.DataSource = null;
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

                    dtgPatentes.DataSource = null;
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

                    dtgPatentes.DataSource = null;
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

        public async void Editar()
        {
            LimpiarFomulario();
            VerificarSeleccionIdPatenteEdicion();
            if (SeleccionarPatente.id > 0)
            {
                await CargarDatosPatente();
                AnadirTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageIngresadasList);

            }
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnGuardarM_Click(object sender, EventArgs e)
        {

        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                EliminarTabPage(tabPageHistorialDetail);
            }
            else if (tabControl1.SelectedTab == tabPageIngresadasList)
            {
                AnadirTabPage(tabPageIngresadasList);
                await LoadPatentes();
                SeleccionarPatente.id = 0;
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageListaArchivos);

            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                await CargarDatosPatente();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageIngresadasList);
                EliminarTabPage(tabPageListaArchivos);
            }*/
        }

        private async void roundedButton8_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                await Task.Run(() => loadHistorialById());
                AnadirTabPage(tabPageHistorialMarca);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
        }

        private async void iconButton7_Click(object sender, EventArgs e)
        {
            if (dtgHistorial.SelectedRows.Count > 0)
            {
                Deshabilitar();
                var filaSeleccionada = dtgHistorial.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorialPatente.id = id;

                    DataTable historial = await historialPatenteModel.ObtenerHistorialPorId(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];

                        SeleccionarHistorialPatente.id = Convert.ToInt32(fila["id"]);
                        SeleccionarHistorialPatente.etapa = fila["etapa"].ToString();
                        SeleccionarHistorialPatente.fecha = Convert.ToDateTime(fila["fecha"]);
                        SeleccionarHistorialPatente.anotaciones = fila["anotaciones"].ToString();
                        SeleccionarHistorialPatente.usuario = fila["usuario"].ToString();
                        SeleccionarHistorialPatente.usuarioEdicion = fila["usuarioEdicion"].ToString();

                        comboBoxEstatusH.SelectedItem = SeleccionarHistorialPatente.etapa;
                        dateTimePickerFechaIngreso.Value = SeleccionarHistorialPatente.fecha;
                        richTextBoxAnotacionesH.Text = SeleccionarHistorialPatente.anotaciones;
                        labelUserEditor.Text = UsuarioActivo.usuario;
                        lblUser.Text = SeleccionarHistorialPatente.usuario;
                        if (fila["fechaVencimiento"] != DBNull.Value)
                        {
                            labelVenc.Visible = true;
                            dateTimePickerVencimiento.Visible = true;
                            if (fila["fechaVencimiento"] != DBNull.Value && !string.IsNullOrWhiteSpace(fila["fechaVencimiento"].ToString()))
                            {
                                dateTimePickerVencimiento.Value = Convert.ToDateTime(fila["fechaVencimiento"]);
                            }
                        }
                        else
                        {
                            labelVenc.Visible = false;
                            dateTimePickerVencimiento.Visible = false;
                        }

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
                //MessageBox.Show("Por favor, seleccione una fila del historial.");
            }
        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
        }

        private async void btnEditarH_Click(object sender, EventArgs e)
        {

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
            AgregarEtapaPatente.etapa = etapa;
            AgregarEtapaPatente.fecha = fechaIngreso;
            AgregarEtapaPatente.usuario = usuarioEditor;
            AgregarEtapaPatente.fechaVencimiento = requiereVencimiento ? fechaVencimiento : null;

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
                    AgregarEtapaPatente.anotaciones = anotacionFinal + " " + anotaciones;
                }
                else
                {
                    AgregarEtapaPatente.anotaciones = anotaciones;
                }


                historialPatenteModel.EditarHistorialPatente(SeleccionarHistorialPatente.id, fechaIngreso, etapa, AgregarEtapaPatente.anotaciones, usuario, usuarioEditor, requiereVencimiento ? fechaVencimiento : (DateTime?)null);
                FrmAlerta alerta = new FrmAlerta("ETAPA ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
                EliminarTabPage(tabPageHistorialDetail);
                AnadirTabPage(tabPageMarcaDetail);
                SeleccionarHistorialPatente.LimpiarHistorial();
                await refrescarPatente();


            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGÚN ESTADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }


        }

        private async void btnEditarEstadoHistorial_Click(object sender, EventArgs e)
        {
            if (dtgHistorial.SelectedRows.Count > 0)
            {
                Habilitar();
                var filaSeleccionada = dtgHistorial.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorialPatente.id = id;

                    DataTable historial = await historialPatenteModel.ObtenerHistorialPorId(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];
                        SeleccionarHistorialPatente.id = Convert.ToInt32(fila["id"]);
                        SeleccionarHistorialPatente.etapa = fila["etapa"].ToString();
                        SeleccionarHistorialPatente.fecha = Convert.ToDateTime(fila["fecha"]);
                        SeleccionarHistorialPatente.anotaciones = fila["anotaciones"].ToString();
                        SeleccionarHistorialPatente.usuario = fila["usuario"].ToString();
                        SeleccionarHistorialPatente.usuarioEdicion = fila["usuarioEdicion"].ToString();

                        comboBoxEstatusH.SelectedItem = SeleccionarHistorialPatente.etapa;
                        dateTimePickerFechaIngreso.Value = SeleccionarHistorialPatente.fecha;
                        richTextBoxAnotacionesH.Text = SeleccionarHistorialPatente.anotaciones;
                        labelUserEditor.Text = UsuarioActivo.usuario;
                        lblUser.Text = SeleccionarHistorialPatente.usuario;

                        if (fila["fechaVencimiento"] != DBNull.Value)
                        {
                            labelVenc.Visible = true;
                            dateTimePickerVencimiento.Visible = true;
                            if (fila["fechaVencimiento"] != DBNull.Value && !string.IsNullOrWhiteSpace(fila["fechaVencimiento"].ToString()))
                            {
                                dateTimePickerVencimiento.Value = Convert.ToDateTime(fila["fechaVencimiento"]);
                            }
                        }
                        else
                        {
                            labelVenc.Visible = false;
                            dateTimePickerVencimiento.Visible = false;
                        }

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

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {
            if (labelVenc.Visible)
            {
                comboBoxEstatusH_SelectedIndexChanged(sender, e);
            }
        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            dateTimePickerVencimiento.Visible = mostrarVencimiento;

            if (mostrarVencimiento)
            {
                dateTimePickerVencimiento.Value = fechaVencimiento;
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
            }
        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {
            if (textBoxEstatus.Text != "Registrada")
            {
                LimpiarFomulario();
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                tabControl1.SelectedTab = tabPageIngresadasList;
            }
            else
            {/*
                if (!ValidarCampo(txtFolio.Text, "Por favor, ingrese el número de folio.\n No es posible salir sin ingresar datos de registro,\n a menos que edite esa etapa") ||
                    !ValidarCampo(txtRegistro.Text, "Por favor, ingrese el número de registro.\n No es posible salir sin ingresar datos de registro,\n a menos que edite esa etapa") ||
                    !ValidarCampo(txtLibro.Text, "Por favor, ingrese el número de tomo.\n No es posible salir sin ingresar datos de registro,\n a menos que edite esa etapa")
                    )
                {

                }
                else
                {
                    if (
                        (!int.TryParse(txtRegistro.Text, out _)) ||
                        (!int.TryParse(txtFolio.Text, out _)) ||
                        (!int.TryParse(txtLibro.Text, out _)))
                    {
                        FrmAlerta alerta = new FrmAlerta("EL REGISTRO, FOLIO Y TOMO\nDEBEN SER VALORES NUMÉRICOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        alerta.ShowDialog();
                        //MessageBox.Show("El registro, folio y tomo deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        //ActualizarMarcaNacional();
                        EliminarTabPage(tabPageMarcaDetail);
                        EliminarTabPage(tabPageHistorialMarca);
                        tabControl1.SelectedTab = tabPageIngresadasList;
                    }

                }
                */
            }
        }
        public void VerificarDatosRegistro()
        {
            if (checkBox2.Checked == true && (string.IsNullOrEmpty(txtRegistro.Text) || string.IsNullOrEmpty(txtFolio.Text) || string.IsNullOrEmpty(txtLibro.Text)))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }
        private async void roundedButton6_Click(object sender, EventArgs e)
        {

            FrmAgregarEtapaPatente frmAgregarEtapa = new FrmAgregarEtapaPatente();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapaPatente.etapa != "")
            {
                try
                {
                    agregoEstado = true;
                    textBoxEstatus.Text = AgregarEtapaPatente.etapa;
                    //historialPatenteModel.CrearHistorialPatente((DateTime)AgregarEtapaPatente.fecha, AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones, UsuarioActivo.usuario, null, SeleccionarPatente.id);
                    FrmAlerta alerta = new FrmAlerta("ESTADO AGREGADO CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show("Etapa agregada con éxito");
                    await refrescarPatente();
                    if (AgregarEtapaPatente.etapa == "Registro/concesión")
                    {
                        checkBox2.Checked = true;
                        checkBox2.Checked = true;
                        mostrarPanelRegistro("si");
                        txtRegistro.Text = "";
                        txtLibro.Text = "";
                        txtFolio.Text = "";
                        dateTimePFecha_Registro.Value = DateTime.Now;
                        ActualizarFechaVencimiento();
                        //VerificarDatosRegistro();
                    }
                    else
                    {
                        checkBox2.Checked = false;
                        checkBox2.Checked = false;
                        mostrarPanelRegistro("no");
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }


        }

        private void roundedButton5_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesPatentes frmMostrarTitulares = new FrmMostrarTitularesPatentes();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersonaPatente.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersonaPatente.nombre;
                txtDireccionTitular.Text = SeleccionarPersonaPatente.direccion;
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentesPatente frmMostrarAgentes = new FrmMostrarAgentesPatente();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersonaPatente.idPersonaA != 0)
            {
                txtNombreAgente.Text = SeleccionarPersonaPatente.nombre;

            }
        }

        private void datePickerFechaSolicitud_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            if (dtgPatentes.SelectedRows.Count > 0)
            {

                using (FrmJustificacion justificacionForm = new FrmJustificacion())
                {

                    if (justificacionForm.ShowDialog() == DialogResult.OK)
                    {
                        string justificacion = justificacionForm.Justificacion;
                        DateTime fechaAbandono = justificacionForm.fecha;
                        string usuarioAbandono = justificacionForm.usuarioAbandono;

                        try
                        {

                            var filaSeleccionada = dtgPatentes.SelectedRows[0];


                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {

                                int idPatente = Convert.ToInt32(dataRowView["id"]);

                                // Validar si la justificación ya contiene la fecha con "Abandono"
                                string fechaSinHora = fechaAbandono.ToString("dd/MM/yyyy");
                                string formato = fechaSinHora + " Abandono";
                                if (!justificacion.Contains(formato))
                                {
                                    justificacion = formato + " " + justificacion;
                                }

                                historialPatenteModel.CrearHistorialPatente(
                                    fechaAbandono,
                                    "Abandono",
                                    justificacion,
                                    usuarioAbandono,
                                    null,
                                    idPatente,
                                    null
                                );

                                FrmAlerta alerta = new FrmAlerta("LA PATENTE HA SIDO MARCADA COMO ABANDONADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();

                                await LoadPatentes();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al actualizar el estado de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA PATENTE PARA ABANDONAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }

        }

        private void dtgPatentes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                if (!archivoSubido && checkBox2.Checked)
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE SUBIR EL TÍTULO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
                else
                {
                    EditarPatente();
                }


            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }
        public void VerificarDatosIngresados()
        {
            if (checkBox2.Checked == true && (string.IsNullOrEmpty(SeleccionarPatente.registro) || string.IsNullOrEmpty(SeleccionarPatente.libro) || string.IsNullOrEmpty(SeleccionarPatente.folio)))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }

        private async void iconButton2_Click(object sender, EventArgs e)
        {/*
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                LimpiarFomulario();
                DatosRegistro.peligro = false;
                AnadirTabPage(tabPageIngresadasList);
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageListaArchivos);
                EliminarTabPage(tabPageHistorialMarca);
                await LoadPatentes();
            }
            else
            {
                if (!archivoSubido)
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO Y SU TÍTULO", "ERROR ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
            }*/

            DatosRegistro.peligro = false;
            await LoadPatentes();
            AnadirTabPage(tabPageIngresadasList);
            LimpiarFomulario();
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageListaArchivos);
            EliminarTabPage(tabPageHistorialMarca);


        }

        private async void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = await patenteModel.GetFilteredPatentesSinRegistroCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            await filtrar();
        }

        private async void iconButton4_Click(object sender, EventArgs e)
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
                totalRows = await patenteModel.GetFilteredPatentesSinRegistroCount(txtBuscar.Text);
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
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
            //VerificarDatosRegistro();
        }
        private async Task<List<string>> ListarNombresDeArchivosHttpAsync(string idPatente)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent("listar_archivos"), "action");
            form.Add(new StringContent(TOKEN), "auth");
            form.Add(new StringContent(idPatente ?? ""), "idPatente");

            using var resp = await HttpX.Client.PostAsync(URL, form);
            var body = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException($"HTTP {(int)resp.StatusCode}: {body}");

            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<ListarResp>(body, opts);
            if (data == null || !data.ok) throw new InvalidOperationException(data?.message ?? "Error al listar archivos");

            return data.files;
        }

        public async Task ListarArchivosEnGeneral()
        {
            try
            {
                // Cambiar el cursor global a "WaitCursor"
                Cursor.Current = Cursors.WaitCursor;

                AnadirTabPage(tabPageListaArchivos);
                tabControl1.Visible = false;

                string id = "" + SeleccionarPatente.id;
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
                tabControl1.Visible = true;
            }
            finally
            {
                // Restaurar el cursor global a "Default"
                Cursor.Current = Cursors.Default;
            }
        }
        private async void AbrirArchivoDesdeHttpAsync(string idPatente, string archivoNombre)
        {
            try
            {
                using var form = new MultipartFormDataContent {
            { new StringContent("descargar"),     "action" },
            { new StringContent(TOKEN),           "auth" },
            { new StringContent(idPatente ?? ""),   "idPatente" },
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

        public void Abrir()
        {
            string idMarca = "" + SeleccionarPatente.id; // Id de la marca actual
            string? archivoNombre = dtgArchivos.CurrentRow?.Cells[0].Value?.ToString(); // Archivo seleccionado

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

        private async Task EliminarArchivoAsync(string idPatente, string archivoNombre)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent("eliminar"), "action");
            form.Add(new StringContent(TOKEN), "auth");
            form.Add(new StringContent(idPatente), "idPatente");
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

        public async Task Eliminar()
        {
            string idMarca = "" + SeleccionarPatente.id; // Id de la marca actual
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

        private async Task CrearCarpetaMarcaHttpAsync(string idPatente)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent("crear_carpeta_patente"), "action");
            form.Add(new StringContent(TOKEN), "auth");
            form.Add(new StringContent(idPatente ?? ""), "idPatente");

            using var resp = await HttpX.Client.PostAsync(URL, form);
            var body = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException($"HTTP {(int)resp.StatusCode}: {body}");
            // Opcional: validar JSON {"ok":true}
        }
        private async Task SubirArchivoAsync(string idPatente)
        {
            using var ofd = new OpenFileDialog
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
            form.Add(new StringContent(idPatente), "idPatente");

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

        private async void roundedButton11_Click(object sender, EventArgs e)
        {
            await ListarArchivosEnGeneral();
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageListaArchivos);
        }

        private async void iconButton13_Click(object sender, EventArgs e)
        {
            await SubirArchivoAsync("" + SeleccionarPatente.id);
            await ListarArchivosEnGeneral();
        }

        private void iconButton12_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void dtgArchivos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Abrir();
        }

        private async void iconButton11_Click(object sender, EventArgs e)
        {
            await Eliminar();
        }
        private async Task SubirArchivoRegistro(string idPatente)
        {
            using var ofd = new OpenFileDialog
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
            form.Add(new StringContent(idPatente), "idPatente");

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
        
        private async void btnAdjuntarT_Click(object sender, EventArgs e)
        {
            await SubirArchivoRegistro("" + SeleccionarPatente.id);
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

        private void dtgPatentes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgPatentes.Columns["id"] != null)
            {
                dtgPatentes.Columns["id"].Visible = false;
                dtgPatentes.ClearSelection();
            }
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

                        if (dtgPatentes.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgPatentes.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idPatente = Convert.ToInt32(dataRowView["id"]);

                                historialPatenteModel.CrearHistorialPatente(
                                   fechaAbandono,
                                   "Desistimiento",
                                   justificacion,
                                   usuarioAbandono,
                                   null,
                                   idPatente,
                                   null
                               );
                                FrmAlerta alerta = new FrmAlerta("LA PATENTE HA SIDO MARCADA COMO DESISTIDA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadPatentes();
                            }
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA PATENTE PARA DESISTIR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            alerta.ShowDialog();
                            //MessageBox.Show("No hay marca seleccionada para abandonar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void dtgHistorial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgHistorial.SelectedRows.Count > 0)
            {
                Habilitar();
                var filaSeleccionada = dtgHistorial.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorialPatente.id = id;

                    DataTable historial = await historialPatenteModel.ObtenerHistorialPorId(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];
                        SeleccionarHistorialPatente.id = Convert.ToInt32(fila["id"]);
                        SeleccionarHistorialPatente.etapa = fila["etapa"].ToString();
                        SeleccionarHistorialPatente.fecha = Convert.ToDateTime(fila["fecha"]);
                        SeleccionarHistorialPatente.anotaciones = fila["anotaciones"].ToString();
                        SeleccionarHistorialPatente.usuario = fila["usuario"].ToString();
                        SeleccionarHistorialPatente.usuarioEdicion = fila["usuarioEdicion"].ToString();

                        comboBoxEstatusH.SelectedItem = SeleccionarHistorialPatente.etapa;
                        dateTimePickerFechaIngreso.Value = SeleccionarHistorialPatente.fecha;
                        richTextBoxAnotacionesH.Text = SeleccionarHistorialPatente.anotaciones;
                        labelUserEditor.Text = UsuarioActivo.usuario;
                        lblUser.Text = SeleccionarHistorialPatente.usuario;

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

        private void dateTimePickerVencimiento_ValueChanged(object sender, EventArgs e)
        {
            if (labelVenc.Visible)
            {
                comboBoxEstatusH_SelectedIndexChanged(sender, e);
            }
        }

        private void FrmMostrarIngresadasPatentes_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }
    }
}
