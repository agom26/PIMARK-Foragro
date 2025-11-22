using Comun;
using Comun.Cache;
using Dominio;
using FluentFTP;
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
    public partial class FrmMostrarTramiteTraspasoPatente : Form, IAsyncLoadable
    {
        PatenteModel patenteModel = new PatenteModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialPatenteModel historialPatenteModel = new HistorialPatenteModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;
        private bool archivoSubido = false;
        private bool _isLoading;
        private bool _actualizando; // evita reentradas
        private bool _cargandoUI;
        private bool _guardandoHist;
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
        public FrmMostrarTramiteTraspasoPatente()
        {
            InitializeComponent();
            SetDoubleBuffering(dtgPatentes, true);
            SetDoubleBuffering(dtgHistorial, true);
            SetDoubleBuffering(dtgArchivos, true);
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
                btnAgregarEstado.Visible = false;
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
                dateTimePFecha_Registro.Enabled = false;
                btnGuardarM.Visible = false;

                txtERenovacion.Enabled = false;
                txtETraspaso.Enabled = false;

                //archivos
                btnSubirArchivos.Visible = false;
                btnEliminarArchivos.Visible = false;

                //historial
                comboBoxEstatusH.Enabled = false;
                dateTimePickerFechaIngreso.Enabled = false;
                dateTimePickerVencimiento.Enabled = false;
                richTextBoxAnotacionesH.Enabled = false;
                btnEditarH.Visible = false;
                btnEditarEstadoHistorial.Visible = false;

                //renovaciones
                btnEditarRenovacion.Visible = false;
                txtNoExpediente.Enabled = false;
                dateFechVencAnt.Enabled = false;
                dateFechVencNueva.Enabled = false;
                btnEditarRenovacionDetalle.Visible = false;

                //traspasos
                btnEditarTraspaso.Visible = false;
                txtNumExpedienteTraspaso.Enabled = false;
                btnAgregarTitularA.Enabled = false;
                txtNombreTitularA.Enabled = false;
                btnAgregarTitularN.Enabled = false;
                txtNombreTitularN.Enabled = false;
                btnEditarTraspasoDetalle.Visible = false;

                btnAdjuntarT.Visible = false;
                btnTraspasar.Visible = false;

                toggleIndefinido.Enabled = false;
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
                btnAgregarEstado.Visible = false;
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

                dateTimePFecha_Registro.Enabled = true;
                btnGuardarM.Visible = true;

                txtERenovacion.Enabled = true;
                txtETraspaso.Enabled = true;

                //archivos
                btnSubirArchivos.Visible = true;
                btnEliminarArchivos.Visible = true;

                //historial
                comboBoxEstatusH.Enabled = true;
                dateTimePickerFechaIngreso.Enabled = true;
                dateTimePickerVencimiento.Enabled = true;
                richTextBoxAnotacionesH.Enabled = true;
                btnEditarH.Visible = true;
                btnEditarEstadoHistorial.Visible = true;

                //renovaciones
                btnEditarRenovacion.Visible = true;
                txtNoExpediente.Enabled = true;
                dateFechVencAnt.Enabled = true;
                dateFechVencNueva.Enabled = true;
                btnEditarRenovacionDetalle.Visible = true;

                //traspasos
                btnEditarTraspaso.Visible = true;
                txtNumExpedienteTraspaso.Enabled = true;
                btnAgregarTitularA.Enabled = true;
                txtNombreTitularA.Enabled = true;
                btnAgregarTitularN.Enabled = true;
                txtNombreTitularN.Enabled = true;
                btnEditarTraspasoDetalle.Visible = true;

                btnAdjuntarT.Visible = true;
                btnTraspasar.Visible = true;

                toggleIndefinido.Enabled = true;
            }


            archivoSubido = false;
        }

        private async Task LoadPatentes()
        {
            totalRows = await Task.Run(() => patenteModel.GetTotalPatentesRegistradasEnTramiteDeTraspaso());
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            var marcasN = await Task.Run(() =>
                    patenteModel.GetAllPatentesRegistradasEnTramiteDeTraspaso(currentPageIndex, pageSize));

            void Apply()
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                lblCurrentPage.Text = currentPageIndex.ToString();
                dtgPatentes.DataSource = marcasN;
                if (dtgPatentes.Columns["id"] != null) dtgPatentes.Columns["id"].Visible = false;
                dtgPatentes.ClearSelection();

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
                       patenteModel.GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount(buscar));
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();

                DataTable dt = await Task.Run(() =>
                        patenteModel.FiltrarPatentesRegistradasEnTramiteDeTraspaso(buscar, currentPageIndex, pageSize));

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
        private void CentrarPanel()
        {

            int anchoMinimo = panelBusqueda.Width + 100;

            if (tabControl1.ClientSize.Width >= anchoMinimo)
            {
                // Pantalla suficientemente ancha → centrar
                panelBusqueda.Anchor = AnchorStyles.None;
                panelBusqueda.Dock = DockStyle.Top;


            }
            else
            {
                // Pantalla pequeña → top-left

                panelBusqueda.Dock = DockStyle.None;
                panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panelBusqueda.Location = new Point(0, 0); // o donde quieras
            }
        }

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
                //ActualizarFechaVencimiento();
                lblVencimiento.Visible = true;
                dateTimePFecha_vencimiento.Visible = true;
                checkBox2.Checked = true;
                checkBox2.Enabled = false;
                panel2I.Visible = true;
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 62.5f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 37.5f;
                toggleIndefinido.Visible = true;
                labelIndefinido.Visible = true;
            }
            else
            {
                lblVencimiento.Visible = false;
                dateTimePFecha_vencimiento.Visible = false;
                checkBox2.Enabled = false;
                checkBox2.Checked = false;
                panel2I.Visible = false;
                tableLayoutPanel1.RowStyles[0].Height = 0;
                toggleIndefinido.Visible = false;
                labelIndefinido.Visible = false;
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


                        if (row["Erenov"] != DBNull.Value)
                        {
                            SeleccionarPatente.Erenov = row["Erenov"].ToString();
                            txtERenovacion.Text = SeleccionarPatente.Erenov;
                        }

                        if (row["Etrasp"] != DBNull.Value)
                        {
                            SeleccionarPatente.Etrasp = row["Etrasp"].ToString();
                            txtETraspaso.Text = SeleccionarPatente.Etrasp;
                        }

                        var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarPatente.idTitular));
                        var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarPatente.idAgente));

                        await Task.WhenAll(titularTask, agenteTask);

                        var titular = titularTask.Result;
                        var agente = agenteTask.Result;

                        SeleccionarPersonaPatente.idPersonaT = SeleccionarPatente.idTitular;
                        SeleccionarPersonaPatente.idPersonaA = SeleccionarPatente.idAgente;

                        if (titular.Count > 0)
                        {
                            AgregarTraspasoPatente.nombreTitulara = titular[0].nombre;
                            AgregarTraspasoPatente.idTitularAnterior = SeleccionarPatente.idTitular;
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

                            checkBox1.Checked = true;
                            mostrarPanelRegistro("si");
                            SeleccionarPatente.registro = row["registro"].ToString();
                            SeleccionarPatente.folio = row["folio"].ToString();
                            SeleccionarPatente.libro = row["libro"].ToString();
                            SeleccionarPatente.fecha_registro = Convert.ToDateTime(row["fecha_registro"]);
                            
                            AgregarRenovacionPatente.fechaVencimientoAntigua = Convert.ToDateTime(SeleccionarPatente.fecha_vencimiento);

                            txtRegistro.Text = SeleccionarPatente.registro;
                            txtFolio.Text = SeleccionarPatente.folio;
                            txtLibro.Text = SeleccionarPatente.libro;
                            dateTimePFecha_Registro.Value = SeleccionarPatente.fecha_registro.Value;


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

                                if (row["fecha_vencimiento"] != DBNull.Value)
                                {
                                    dateTimePFecha_vencimiento.Value = Convert.ToDateTime(row["fecha_vencimiento"]);
                                    SeleccionarPatente.fecha_vencimiento = Convert.ToDateTime(row["fecha_vencimiento"]);
                                }
                            }
                        }
                        else
                        {
                            checkBox1.Checked = false;
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
            checkedListBoxDocumentos.ClearSelected();
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
        }

        public async Task EditarPatente()
        {
            string caso = txtCaso.Text;
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string tipo = comboBoxTipo.SelectedItem?.ToString();
            string anualidad = comboBoxAnualidades.SelectedItem?.ToString();
            int anualidades = int.Parse(anualidad);
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            int idTitular = SeleccionarPersonaPatente.idPersonaT;
            int idAgente = SeleccionarPersonaPatente.idPersonaA;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string pct = "no";
            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime? fecha_vencimiento = dateTimePFecha_vencimiento.Value;
            string etrasp = txtETraspaso.Text;
            string erenov = txtERenovacion.Text;
            string comprobante_pagos = "no";
            string descripcion = "no";
            string reivindicaciones = "no";
            string dibujos = "no";
            string resumen = "no";
            string documento_cesion = "no";
            string poder_nombramiento = "no";
            int indefinida = 0;

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

            try
            {
                if (registroChek)
                {
                    try
                    {
                        bool actualizada = await patenteModel.EditarPatente(SeleccionarPatente.id, caso, expediente, nombre, estado, tipo, idTitular, idAgente, solicitud,
                            registro, folio, libro, fecha_registro,indefinida, fecha_vencimiento, erenov, etrasp, anualidades, pct,
                            comprobante_pagos, descripcion, reivindicaciones, dibujos, resumen, documento_cesion,
                            poder_nombramiento);

                        FrmAlerta alerta = new FrmAlerta("PATENTE ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        await LoadPatentes();
                        LimpiarFomulario();
                        AnadirTabPage(tabPageIngresadasList);
                        EliminarTabPage(tabPageMarcaDetail);
                        EliminarTabPage(tabPageListaArchivos);
                        EliminarTabPage(tabPageHistorialMarca);
                        tabControl1.SelectedTab = tabPageIngresadasList;
                        
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
                            null, null, null, null, indefinida, null, null, null, anualidades, pct,
                            comprobante_pagos, descripcion, reivindicaciones, dibujos, resumen, documento_cesion,
                            poder_nombramiento);
                        FrmAlerta alerta = new FrmAlerta("PATENTE ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        await LoadPatentes();
                        LimpiarFomulario();
                        AnadirTabPage(tabPageIngresadasList);
                        EliminarTabPage(tabPageMarcaDetail);
                        EliminarTabPage(tabPageListaArchivos);
                        EliminarTabPage(tabPageHistorialMarca);
                        tabControl1.SelectedTab = tabPageIngresadasList;
                        
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

        public void LimpiarFormulario()
        {
            txtCaso.Text = "";
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtFolio.Text = "";
            comboBoxTipo.SelectedIndex = -1;
            comboBoxAnualidades.SelectedIndex = -1;
            txtLibro.Text = "";
            txtNombreTitular.Text = "";
            txtDireccionTitular.Text = "";
            txtNombreAgente.Text = "";
            txtETraspaso.Text = "";
            txtERenovacion.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            AgregarEtapaPatente.LimpiarEtapa();
            SeleccionarPersonaPatente.LimpiarPersona();
            checkedListBoxDocumentos.ClearSelected();
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
            if (!UsuarioActivo.soloLectura)
            {
                dateTimePickerFechaIngreso.Enabled = true;
                comboBoxEstatusH.Enabled = true;
                richTextBoxAnotacionesH.Enabled = true;
                btnEditarH.Enabled = true;
            }

        }
        public void Deshabilitar()
        {
            if (!UsuarioActivo.soloLectura)
            {
                dateTimePickerFechaIngreso.Enabled = false;
                comboBoxEstatusH.Enabled = false;
                richTextBoxAnotacionesH.Enabled = true;
                richTextBoxAnotacionesH.ReadOnly = true;
                btnEditarH.Enabled = false;
            }

        }

        private async Task refrescarMarca()
        {
            if (SeleccionarPatente.id > 0)
            {
                try
                {
                    DataTable detallesPatente = await Task.Run(() => patenteModel.ObtenerPatentePorId(SeleccionarPatente.id));

                    if (detallesPatente.Rows.Count > 0)
                    {
                        DataRow row = detallesPatente.Rows[0];

                        bool contieneRegistrada = false;

                        if (SeleccionarPatente.estado.Contains("Registro/concesión", StringComparison.OrdinalIgnoreCase) || SeleccionarPatente.estado.Contains("Trámite de renovación", StringComparison.OrdinalIgnoreCase) || SeleccionarPatente.estado.Contains("Trámite de traspaso", StringComparison.OrdinalIgnoreCase))
                        {
                            contieneRegistrada = true;
                        }
                        else
                        {
                            contieneRegistrada = false;
                        }


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

        private async void FrmMostrarTramiteTraspasoPatente_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            try
            {
                // ===== tu init actual (déjalo igual) =====
                SeleccionarPatente.id = 0;

                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
                EliminarTabPage(tabPageListaArchivos);
                btnVerRenovaciones.Visible = false;
                btnVerTraspasos.Visible = false;
                tabControl1.Visible = true;
                archivoSubido = false;

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
            VerificarSeleccionIdPatenteEdicion();
            if (SeleccionarPatente.id > 0)
            {
                using (var loading = new FrmLoading(() => CargarDatosPatente()))
                {
                    loading.ShowDialog(this);
                }
                AnadirTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageIngresadasList);

            }
        }
        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private async void btnGuardarM_Click(object sender, EventArgs e)
        {
            await EditarPatente();
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
            }
            else if (tabControl1.SelectedTab == tabPageIngresadasList)
            {
                await LoadPatentes();
                SeleccionarPatente.id = 0;
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
                EliminarTabPage(tabPageListaArchivos);

            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                await CargarDatosPatente();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
                EliminarTabPage(tabPageListaArchivos);
            }
            else if (tabControl1.SelectedTab == tabPageRenovacionesList)
            {
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
            }
            else if (tabControl1.SelectedTab == tabPageTraspasosList)
            {
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
            }*/
        }

        private async void roundedButton8_Click(object sender, EventArgs e)
        {

            AnadirTabPage(tabPageHistorialMarca);
            await Task.Run(() => loadHistorialById());

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
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
                        SeleccionarHistorialPatente.fecha = Convert.ToDateTime(fila["fecha"].ToString());
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
            tabControl1.SelectedTab = tabPageHistorialMarca;
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

                    try
                    {
                        historialPatenteModel.EditarHistorialPatente(SeleccionarHistorialPatente.id, fechaIngreso, etapa, AgregarEtapaPatente.anotaciones, usuario, usuarioEditor, requiereVencimiento ? fechaVencimiento : (DateTime?)null);
                        FrmAlerta alerta = new FrmAlerta("ETAPA ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        EliminarTabPage(tabPageHistorialDetail);
                        AnadirTabPage(tabPageMarcaDetail);
                        SeleccionarHistorialPatente.LimpiarHistorial();
                        await refrescarPatente();
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta frmAlerta = new FrmAlerta("ERROR :" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        frmAlerta.ShowDialog();
                    }



                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGÚN ESTADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
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

        private async Task EditarHistorial()
        {
            _cargandoUI = true;
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
                        SeleccionarHistorialPatente.fecha = Convert.ToDateTime(fila["fecha"].ToString());
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

            _cargandoUI = false;
        }
        private async void btnEditarEstadoHistorial_Click(object sender, EventArgs e)
        {
            await EditarHistorial();
        }

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {
            if (_cargandoUI) return;              // <- clave

            //comboBoxEstado_SelectedIndexChanged(sender, e);
            if (!_actualizando && dateTimePickerVencimiento.Visible)
            {
                _actualizando = true;
                dateTimePickerVencimiento.Value = CalcularVencimiento(comboBoxEstatusH.Text, dateTimePickerFechaIngreso.Value);
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
            if (dateTimePickerVencimiento.Visible)
            {
                string venc = dateTimePickerVencimiento.Value.ToString("dd/MM/yyyy");
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

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
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
            dateTimePickerVencimiento.Visible = mostrarVencimiento;

            if (mostrarVencimiento)
            {
                if (!dateTimePickerVencimiento.Visible)
                    dateTimePickerVencimiento.Value = CalcularVencimiento(etapa, fechaIngreso);
            }
            labelVenc.Visible = dateTimePickerVencimiento.Visible = mostrarVencimiento;


            ActualizarResumen(); // arma el texto según valores actuales
            _actualizando = false;
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
        private async void btnCancelarM_Click(object sender, EventArgs e)
        {

            LimpiarFomulario();

            DatosRegistro.peligro = false;
            AnadirTabPage(tabPageIngresadasList);
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            tabControl1.SelectedTab = tabPageIngresadasList;
            await LoadPatentes();

        }

        private async void roundedButton6_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaRegistradaPatente frmAgregarEtapa = new FrmAgregarEtapaRegistradaPatente();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapaPatente.etapa != "")
            {
                try
                {
                    historialPatenteModel.CrearHistorialPatente(Convert.ToDateTime(AgregarEtapaPatente.fecha), AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones, AgregarEtapaPatente.usuario, null, SeleccionarPatente.id, null);

                    FrmAlerta alerta = new FrmAlerta("ESTADO AGREGADO CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();

                    if (AgregarEtapaPatente.etapa == "Registro/concesión" || AgregarEtapaPatente.etapa == "Trámite de renovación" || AgregarEtapaPatente.etapa == "Trámite de traspaso")
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
                    await CargarDatosPatente();


                    if (AgregarEtapaPatente.etapa == "Trámite de renovación" && AgregarEtapaPatente.numExpediente != "0")
                    {
                        txtERenovacion.Text = AgregarEtapaPatente.numExpediente.ToString();
                        txtERenovacion.Enabled = true;
                    }
                    else if (AgregarEtapaPatente.etapa == "Trámite de traspaso" && AgregarEtapaPatente.numExpediente != "0")
                    {
                        txtETraspaso.Text = AgregarEtapa.numExpediente.ToString();
                        txtETraspaso.Enabled = true;
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

        private void datePickerFechaSolicitud_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void btnTraspasar_Click(object sender, EventArgs e)
        {
            FrmAgregarRenovacionConcedidaPatente frmAgregarConcesion = new FrmAgregarRenovacionConcedidaPatente();
            frmAgregarConcesion.ShowDialog();

            if (AgregarRenovacionPatente.renovacionTerminada == true)
            {
                LimpiarFormulario();
                AgregarRenovacionPatente.renovacionTerminada = false;
                tabControl1.SelectedTab = tabPageIngresadasList;
                FrmAlerta alerta = new FrmAlerta("RENOVACIÓN GUARDADA CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.Show();

            }
        }

        private void roundedButton10_Click(object sender, EventArgs e)
        {

        }

        private async void btnTraspasar_Click_1(object sender, EventArgs e)
        {
            VerificarDatosRegistro();

            if (!archivoSubido)
            {
                FrmAlerta alerta = new FrmAlerta("DEBE SUBIR EL TÍTULO DE TRASPASO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                return;
            }

            AgregarTraspasoPatente.antiguoNombre = SeleccionarPatente.nombre;
            FrmCrearTraspasoPatente frmCrearTraspaso = new FrmCrearTraspasoPatente();
            frmCrearTraspaso.ShowDialog();

            if (AgregarTraspasoPatente.traspasoFinalizado == true)
            {
                //Limpiar campos
                LimpiarFormulario();
                AgregarTraspasoPatente.traspasoFinalizado = false;
                DatosRegistro.peligro = false;
                FrmAlerta alerta = new FrmAlerta("TRASPASO GUARDADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
                AnadirTabPage(tabPageIngresadasList);
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageListaArchivos);
                EliminarTabPage(tabPageHistorialMarca);
                tabControl1.SelectedTab = tabPageIngresadasList;
                await LoadPatentes();

                //MessageBox.Show("Traspaso guardado correctamente");
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

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = await patenteModel.GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                await filtrar();
            }
        }

        private async void iconButton12_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            await filtrar();
        }

        private async void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = await patenteModel.GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            await filtrar();
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (buscando == true)
            {
                await filtrar();
            }
            else
            {
                await LoadPatentes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (buscando == true)
                {
                    await filtrar();
                }
                else
                {
                    await LoadPatentes();
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
                    await filtrar();
                }
                else
                {
                    await LoadPatentes();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (buscando == true)
            {
                await filtrar();
            }
            else
            {
                await LoadPatentes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtETraspaso_TextChanged(object sender, EventArgs e)
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
        /*
        private List<string> ListarNombresDeArchivos(string idMarca)
        {
            string carpetaMarca = $"{directorioBase}/patente-{idMarca}";
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

            }
            finally
            {
                tabControl1.Visible = true;
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

        /*
        private void AbrirArchivoDesdeFtp(string idMarca, string archivoNombre)
        {
            string carpeta = $"{directorioBase}/patente-{idMarca}/";
            string rutaRemota = $"{carpeta}/{archivoNombre}";
            string rutaLocal = System.IO.Path.Combine(System.IO.Path.GetTempPath(), archivoNombre); // Carpeta temporal

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
            string idMarca = "" + SeleccionarPatente.id; // Id de la marca actual
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

        /*
        private void EliminarArchivoDesdeFtp(string idMarca, string archivoNombre)
        {
            string carpeta = $"{directorioBase}/patente-{idMarca}/";
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
            string idMarca = "" + SeleccionarPatente.id; // Id de la marca actual
            string archivoNombre = dtgArchivos.CurrentRow?.Cells[0].Value?.ToString(); // Archivo seleccionado

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
        /*
        public void CrearCarpetaMarca(string idMarca)
        {
            string carpetaMarca = $"{directorioBase}/patente-{idMarca}"; // Ruta completa para la carpeta de la marca

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
        /*
        private void SubirArchivo(string idMarca)
        {
            string carpeta = $"{directorioBase}/patente-{idMarca}/";
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
                    MessageBox.Show($"El archivo supera el límite de {limiteTamanio / (1024 * 1024)} MB (20MB).",
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
        private async void roundedButton9_Click(object sender, EventArgs e)
        {
            await ListarArchivosEnGeneral();
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private async void iconButton14_Click(object sender, EventArgs e)
        {
            await SubirArchivoAsync("" + SeleccionarPatente.id);
            await ListarArchivosEnGeneral();
        }

        private void iconButton13_Click(object sender, EventArgs e)
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
        private async Task SubirArchivoTraspaso(string idPatente)
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
            var cd = new ContentDispositionHeaderValue("form-data");
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
        /*
        private void SubirArchivoTraspaso(string idMarca)
        {
            string carpeta = $"{directorioBase}/patente-{idMarca}/";
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

        private void btnAdjuntarT_Click(object sender, EventArgs e)
        {
            SubirArchivoTraspaso("" + SeleccionarPatente.id);
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

        private void dateTimePickerVencimiento_ValueChanged(object sender, EventArgs e)
        {
            if (labelVenc.Visible)
            {
                comboBoxEstatusH_SelectedIndexChanged(sender, e);
            }
        }

        private void FrmMostrarTramiteTraspasoPatente_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private async void dtgHistorial_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            await EditarHistorial();
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
