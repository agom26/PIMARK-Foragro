using Comun;
using Comun.Cache;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Wordprocessing;
using Dominio;
using FluentFTP;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;
using Presentacion.Alertas;
using Presentacion.Marcas_Internacionales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{

    public partial class FrmRegistradas : Form, IAsyncLoadable
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        RenovacionesMarcaModel renovacionesModel = new RenovacionesMarcaModel();
        TraspasosMarcaModel traspasosModel = new TraspasosMarcaModel();
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento = null;
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        bool agregoEstado = false;
        private bool buscando = false;
        private bool _isLoading;
        //ftp
        const string URL = "https://foragro.com.es/peticiones/archivos_marcas_internacionales.php";
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
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }

        // Implementación de la interfaz
        public async Task LoadAsync()
        {
            await LoadMarcas(); // aquí llamas a tu método actual
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

        public FrmRegistradas()
        {
            InitializeComponent();
            SeleccionarMarca.idInt = 0;


            if (UsuarioActivo.soloLectura)
            {
                btnDesistir.Visible = false;
                btnAbandonar.Visible = false;
                btnEliminarMarca.Visible = false;

                //formulario
                txtExpediente.Enabled = false;
                txtNombre.Enabled = false;
                comboBox1.Enabled = false;
                datePickerFechaSolicitud.Enabled = false;
                txtClase.Enabled = false;
                checkBoxMulticlase.Enabled = false;
                txtUbicacion.Enabled = false;
                comboBoxSignoDistintivo.Enabled = false;
                comboBoxTipoSigno.Enabled = false;
                btnSubirImagen.Enabled = false;
                btnQuitarImagen.Enabled = false;
                btnAgregarEstado.Enabled = false;
                textBoxEstatus.Enabled = false;
                btnAgregarTitular.Enabled = false;
                txtNombreTitular.Enabled = false;
                btnAgregarAgente.Enabled = false;
                txtNombreAgente.Enabled = false;
                btnAgregarCliente.Enabled = false;
                txtNombreCliente.Enabled = false;
                richTextBox1.Enabled = false;
                txtRegistro.Enabled = false;
                txtFolio.Enabled = false;
                txtLibro.Enabled = false;
                txtERenovacion.Enabled = false;
                txtETraspaso.Enabled = false;
                dateTimePFecha_Registro.Enabled = false;
                dateTimePFecha_vencimiento.Enabled = false;
                checkBoxTienePoder.Enabled = false;

                //historial
                btnEditarEstadoHistorial.Visible = false;
                btnEditarH.Visible = false;
                comboBoxEstatusH.Enabled = false;
                dateTimePickerFechaIngreso.Enabled = false;
                dateTimePickerFechaVencimiento.Enabled = false;
                richTextBoxAnotacionesH.Enabled = false;

                //archivos
                btnSubirArchivos.Visible = false;
                btnEliminarArchivos.Visible = false;

                //renovaciones
                btnEditarRenovacion.Visible = false;
                btnEditarRenovacionDetalle.Visible = false;
                txtNoExpediente.Enabled = false;
                dateFechVencAnt.Enabled = false;
                dateFechVencNueva.Enabled = false;

                //traspasos
                btnEditarTraspaso.Visible = false;
                btnEditarTraspasoDetalle.Visible = false;
                txtNumExpedienteTraspaso.Enabled = false;
                btnAgregarTitularA.Enabled = false;
                txtNombreTitularA.Enabled = false;
                btnAgregarTitularN.Enabled = false;
                txtNombreTitularN.Enabled = false;


                //botones
                btnAbandonar.Visible = false;
                

            }
            else
            {
                if (UsuarioActivo.isAdmin)
                {
                    btnEliminarMarca.Visible = true;
                }
                else
                {
                    btnEliminarMarca.Visible = false;
                }


                //demas
                btnDesistir.Visible = true;
                btnAbandonar.Visible = true;
                btnEliminarMarca.Visible = true;

                //formulario
                txtExpediente.Enabled = true;
                txtNombre.Enabled = true;
                comboBox1.Enabled = true;
                datePickerFechaSolicitud.Enabled = true;
                txtClase.Enabled = true;
                checkBoxMulticlase.Enabled = true;
                txtUbicacion.Enabled = true;
                comboBoxSignoDistintivo.Enabled = true;
                comboBoxTipoSigno.Enabled = true;
                btnSubirImagen.Enabled = true;
                btnQuitarImagen.Enabled = true;
                btnAgregarEstado.Enabled = true;
                textBoxEstatus.Enabled = true;
                btnAgregarTitular.Enabled = true;
                txtNombreTitular.Enabled = true;
                btnAgregarAgente.Enabled = true;
                txtNombreAgente.Enabled = true;
                btnAgregarCliente.Enabled = true;
                txtNombreCliente.Enabled = true;
                richTextBox1.Enabled = true;
                txtRegistro.Enabled = true;
                txtFolio.Enabled = true;
                txtLibro.Enabled = true;
                txtERenovacion.Enabled = true;
                txtETraspaso.Enabled = true;
                dateTimePFecha_Registro.Enabled = true;
                dateTimePFecha_vencimiento.Enabled = true;
                checkBoxTienePoder.Enabled = true;

                //historial
                btnEditarEstadoHistorial.Visible = true;
                btnEditarH.Visible = true;
                comboBoxEstatusH.Enabled = true;
                dateTimePickerFechaIngreso.Enabled = true;
                dateTimePickerFechaVencimiento.Enabled = true;
                richTextBoxAnotacionesH.Enabled = true;

                //archivos
                btnSubirArchivos.Visible = true;
                btnEliminarArchivos.Visible = true;

                //archivos
                btnSubirArchivos.Visible = true;
                btnEliminarArchivos.Visible = true;

                //renovaciones
                btnEditarRenovacion.Visible = true;
                btnEditarRenovacionDetalle.Visible = true;
                txtNoExpediente.Enabled = true;
                dateFechVencAnt.Enabled = true;
                dateFechVencNueva.Enabled = true;

                //traspasos
                btnEditarTraspaso.Visible = true;
                btnEditarTraspasoDetalle.Visible = true;
                txtNumExpedienteTraspaso.Enabled = true;
                btnAgregarTitularA.Enabled = true;
                txtNombreTitularA.Enabled = true;
                btnAgregarTitularN.Enabled = true;
                txtNombreTitularN.Enabled = true;
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
            totalRows = await marcaModel.GetTotalMarcasInternacionalesRegistradas();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            var marcasN = await marcaModel.GetAllMarcasInternacionalesRegistradas(currentPageIndex, pageSize);

            void Apply()
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                lblCurrentPage.Text = currentPageIndex.ToString();
                dtgMarcasR.DataSource = marcasN;
            }

            if (!IsDisposed)
            {
                if (InvokeRequired) BeginInvoke((Action)Apply);
                else Apply();
            }
        }

        private async Task filtrar()
        {
            string buscar = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(buscar))
            {
                totalRows = await marcaModel.GetFilteredMarcasInternacionalesRegistradasCount(buscar);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();

                DataTable dt = await marcaModel.FiltrarMarcasInternacionalesRegistradas(buscar, currentPageIndex, pageSize);
                if (dt.Rows.Count > 0)
                {
                    dtgMarcasR.DataSource = dt;
                    if (dtgMarcasR.Columns["id"] != null) dtgMarcasR.Columns["id"].Visible = false;
                    dtgMarcasR.ClearSelection();
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
                totalRows = await marcaModel.GetTotalMarcasInternacionalesRegistradas();
                totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalRows) / pageSize));

                var marcasN = await  marcaModel.GetAllMarcasInternacionalesRegistradas(currentPageIndex, pageSize);

                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke(() =>
                    {
                        lblTotalPages.Text = totalPages.ToString();
                        lblTotalRows.Text = totalRows.ToString();
                        dtgMarcasR.DataSource = marcasN;
                    });
                }
            }
            catch (HttpRequestException)
            {
                new FrmAlerta(
                    "No se pudo conectar con el servidor. Verifique su conexión a internet.",
                    "ERROR DE CONEXIÓN",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                ).ShowDialog();
            }
            catch (JsonException)
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
                    totalRows = await marcaModel.GetFilteredMarcasInternacionalesRegistradasCount(buscar);
                    totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalRows) / pageSize));

                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();

                    DataTable titulares = await marcaModel.FiltrarMarcasInternacionalesRegistradas(buscar, currentPageIndex, pageSize);

                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.Invoke(() =>
                        {
                            if (titulares.Rows.Count > 0)
                            {
                                dtgMarcasR.DataSource = titulares;

                                if (dtgMarcasR.Columns["id"] != null)
                                {
                                    dtgMarcasR.Columns["id"].Visible = false;
                                }

                                dtgMarcasR.ClearSelection();
                            }
                            else
                            {
                                new FrmAlerta(
                                    "NO EXISTEN MARCAS CON ESOS DATOS",
                                    "MENSAJE",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.None
                                ).ShowDialog();

                                _ = LoadMarcas(); // No await dentro de Invoke
                            }
                        });
                    }
                }
                catch (HttpRequestException)
                {
                    new FrmAlerta(
                        "No se pudo conectar con el servidor. Verifica tu conexión a internet.",
                        "ERROR DE CONEXIÓN",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    ).ShowDialog();
                }
                catch (JsonException)
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
            if (logo != null && logo.Length > 0) // Verificar que el logo no esté vacío
            {
                using (var ms = new MemoryStream(logo))
                {
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            else
            {

                pictureBox1.Image = documento;
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

        private bool EsAlfanumerico(string texto)
        {
            // Permite letras, números, guiones
            return Regex.IsMatch(texto, @"^[a-zA-Z0-9\-_]+$");

        }
        private bool ValidarCampos(string pais, string expediente, string nombre, ref string clase, string signoDistintivo, string tipo, string estado,
    ref byte[] logo, bool registroChek, string registro, string folio, string libro)
        {
            // Verificar campos obligatorios
            if (!ValidarCampo(pais, "Por favor, ingrese un pais.") ||
                !ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
                !ValidarCampo(nombre, "Por favor, ingrese el signo.") ||
                !ValidarCampo(clase, "Por favor, ingrese la clase.") ||
                !ValidarCampo(signoDistintivo, "Por favor, seleccione un signo distintivo.") ||
                !ValidarCampo(tipo, "Por favor, seleccione un tipo.") ||
                !ValidarCampo(estado, "Por favor, seleccione un estado."))
            {
                return false;
            }

            // Normalizar clase quitando espacios extra
            clase = string.Join(",", clase.Split(',')
                                          .Select(c => c.Trim())
                                          .Where(c => !string.IsNullOrWhiteSpace(c)));

            if (checkBoxMulticlase.Checked)
            {
                string[] clases = clase.Split(',');

                foreach (string c in clases)
                {
                    if (!int.TryParse(c, out _))
                    {
                        FrmAlerta alerta = new FrmAlerta("SI EL MODO MULTICLASE ESTÁ ACTIVO,\nLA CLASE DEBE CONTENER SOLO NÚMEROS ENTEROS SEPARADOS POR COMAS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        alerta.ShowDialog();
                        return false;
                    }
                }
            }
            else
            {
                // Solo permitir un número entero
                if (!int.TryParse(clase, out _))
                {
                    FrmAlerta alerta = new FrmAlerta("LA CLASE DEBE SER UN VALOR NUMÉRICO ENTERO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return false;
                }
            }


            // ✅ Nuevo bloque para validar campos alfanuméricos
            if (!string.IsNullOrWhiteSpace(folio) && !EsAlfanumerico(folio))
            {
                FrmAlerta alerta = new FrmAlerta("EL FOLIO DEBE SER UN VALOR ALFANUMÉRICO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(libro) && !EsAlfanumerico(libro))
            {
                FrmAlerta alerta = new FrmAlerta("EL TOMO DEBE SER UN VALOR ALFANUMÉRICO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return false;
            }

            if (registroChek && !string.IsNullOrWhiteSpace(registro) && !EsAlfanumerico(registro))
            {
                FrmAlerta alerta = new FrmAlerta("EL REGISTRO DEBE SER UN VALOR ALFANUMÉRICO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
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
                    //MessageBox.Show("Por favor, ingrese una imagen.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (
                    !ValidarCampo(registro, "Por favor, ingrese el número de registro.")

                    )
                {
                    return false;
                }
            }

            return true;
        }


        public async void ActualizarMarcaNacional()
        {
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string signoDistintivo = comboBoxSignoDistintivo.Text;
            string tipoSigno = comboBoxTipoSigno.Text;
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[] logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            int? idCliente = SeleccionarPersona.idPersonaC;

            if (idCliente == 0)
            {
                idCliente = null;
            }

            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;

            string etrasp = txtETraspaso.Text;
            string erenov = txtERenovacion.Text;

            string paisRegistro = comboBox1.Text;
            string tiene_poder = "no";
            int multiclase = 0;
            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;
            string ubicacionF = txtUbicacion.Text;

            if (checkBoxTienePoder.Checked == true)
            {
                tiene_poder = "si";
            }
            else
            {
                tiene_poder = "no";
            }

            if (checkBoxMulticlase.Checked == true)
            {
                multiclase = 1;
            }
            else
            {
                multiclase = 0;
            }

            // Validaciones
            if (idTitular <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("INGRESE UN TITULAR VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }

            if (idAgente <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("INGRESE UN AGENTE VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }


            // Validar campos 
            if (!ValidarCampos(paisRegistro, expediente, nombre, ref clase, signoDistintivo, tipoSigno, estado, ref logo, registroChek, registro, folio, libro))
            {
                return;
            }

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
                bool esActualizado;


                if (registroChek)
                {
                    esActualizado = await marcaModel.EditMarcaInternacionalRegistradaNuevo(
                        SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, tipoSigno, clase, multiclase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente, registro, folio, libro, fecha_registro, fecha_vencimiento, erenov, etrasp, ubicacionF);
                }
                else
                {
                    esActualizado = await marcaModel.EditMarcaInternacionalNuevo(SeleccionarMarca.idInt, expediente, nombre, signoDistintivo
                        , tipoSigno, clase, multiclase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente, ubicacionF);
                }

                if (!esActualizado)
                {
                    MessageBox.Show("Error al actualizar la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (esActualizado == true)
                {
                    if (agregoEstado == true)
                    {
                        await historialModel.GuardarEtapa(SeleccionarMarca.idInt, Convert.ToDateTime(AgregarEtapa.fecha), AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE", null);
                        agregoEstado = false;
                    }

                    new FrmAlerta("MARCA INTERNACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information).ShowDialog();
                    SeleccionarMarca.idInt = 0;
                    await LoadMarcas();
                    AnadirTabPage(tabPageRegistradasList);
                    EliminarTabPage(tabPageMarcaDetail);
                    EliminarTabPage(tabPageHistorialMarca);
                    EliminarTabPage(tabPageHistorialDetail);
                    EliminarTabPage(tabPageListaArchivos);
                    EliminarTabPage(tabPageRenovacionesList);
                    EliminarTabPage(tabPageRenovacionDetail);
                    EliminarTabPage(tabPageTraspasosList);
                    EliminarTabPage(tabPageTraspasoDetail);
                    LimpiarFormulario();

                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("ERROR AL ACTUALIZAR LA MARCA INTERNACIONAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al " + (registroChek ? "registrar" : "actualizar") + " la marca internacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
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
            txtETraspaso.Text = "";
            txtERenovacion.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            checkBoxMulticlase.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            AgregarEtapa.LimpiarEtapa();
            txtERenovacion.Text = "";
            txtETraspaso.Text = "";
        }

        private async Task CargarDatosMarca()
        {
            try
            {
                DataTable detallesMarcaInter = await Task.Run(() => marcaModel.GetMarcaInternacionalById(SeleccionarMarca.idInt));

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
                        //SeleccionarMarca.logo = row["logo"] is DBNull ? null : (byte[])row["logo"];
                        SeleccionarMarca.idPersonaTitular = row["idTitular"] != DBNull.Value ? Convert.ToInt32(row["idTitular"]) : 0;
                        SeleccionarMarca.idPersonaAgente = row["idAgente"] != DBNull.Value ? Convert.ToInt32(row["idAgente"]) : 0;
                        SeleccionarMarca.idPersonaCliente = row["idCliente"] != DBNull.Value ? Convert.ToInt32(row["idCliente"]) : 0;
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
                        SeleccionarMarca.tiene_poder = row["tiene_poder"] != DBNull.Value ? row["tiene_poder"].ToString() : string.Empty;
                        SeleccionarMarca.pais_de_registro = row["pais_de_registro"] != DBNull.Value ? row["pais_de_registro"].ToString() : string.Empty;
                        txtUbicacion.Text = row["ubicacion_fisica"] != DBNull.Value ? row["ubicacion_fisica"].ToString() : string.Empty;
                        SeleccionarMarca.logo = await marcaModel.ObtenerLogoMarcaPorIdNuevo(SeleccionarMarca.idInt);

                        if (SeleccionarMarca.logo != null && SeleccionarMarca.logo.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(SeleccionarMarca.logo))
                            {
                                pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            convertirImagen();
                            pictureBox1.Image = documento;
                        }

                        if (row["Erenov"] != DBNull.Value)
                        {
                            SeleccionarMarca.erenov = row["Erenov"].ToString();
                            txtERenovacion.Text = SeleccionarMarca.erenov;
                        }

                        if (row["Etrasp"] != DBNull.Value)
                        {
                            SeleccionarMarca.etraspaso = row["Etrasp"].ToString();
                            txtETraspaso.Text = SeleccionarMarca.etraspaso;
                        }

                        checkBoxTienePoder.Checked = SeleccionarMarca.tiene_poder.Equals("si", StringComparison.OrdinalIgnoreCase);
                        int index = comboBox1.FindString(SeleccionarMarca.pais_de_registro);
                        comboBox1.SelectedIndex = index;

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


                        // Actualizar los controles 
                        txtExpediente.Text = SeleccionarMarca.expediente;
                        txtNombre.Text = SeleccionarMarca.nombre;
                        txtClase.Text = SeleccionarMarca.clase;
                        textBoxEstatus.Text = SeleccionarMarca.estado;
                        comboBoxSignoDistintivo.SelectedItem = SeleccionarMarca.signoDistintivo;
                        comboBoxTipoSigno.SelectedItem = SeleccionarMarca.tipoSigno;
                        //MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                        datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                        richTextBox1.Text = SeleccionarMarca.observaciones;

                        if (row["multiclase"] != DBNull.Value && int.TryParse(row["multiclase"].ToString(), out int multiclaseInt))
                        {
                            checkBoxMulticlase.Checked = multiclaseInt == 1;
                        }
                        else
                        {
                            checkBoxMulticlase.Checked = false; // o lo que quieras por defecto
                        }

                        bool contieneRegistrada = await marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idInt);

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
            if (dtgMarcasR.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasR.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasR.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarca.idInt = id;
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


        private async Task loadHistorialById()
        {
            try
            {
                var historial = await Task.Run(() => historialModel.GetHistorialMarcaById(SeleccionarMarca.idInt));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgHistorialR.AutoGenerateColumns = true;
                    dtgHistorialR.DataSource = historial;
                    dtgHistorialR.Refresh();


                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task loadRenovacionesById()
        {
            try
            {
                DataTable renovaciones = await  renovacionesModel.GetAllRenovacionesByIdMarca(SeleccionarMarca.idInt);

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgRenovaciones.AutoGenerateColumns = true;
                    dtgRenovaciones.DataSource = renovaciones;
                    dtgRenovaciones.Refresh();


                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las renovaciones de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task loadTraspasosById()
        {
            try
            {
                DataTable traspasos = await Task.Run(() => traspasosModel.ObtenerTraspasosMarcaPorIdMarca(SeleccionarMarca.idInt));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgTraspasos.AutoGenerateColumns = true;
                    dtgTraspasos.DataSource = traspasos;
                    dtgTraspasos.Refresh();

                    if (dtgTraspasos.Columns["Id"] != null)
                    {
                        dtgTraspasos.Columns["Id"].Visible = false;
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
        private async Task refrescarMarca()
        {
            if (SeleccionarMarca.idInt > 0)
            {
                try
                {
                    DataTable detallesMarcaInt = await marcaModel.GetMarcaInternacionalById(SeleccionarMarca.idInt);

                    if (detallesMarcaInt.Rows.Count > 0)
                    {
                        DataRow row = detallesMarcaInt.Rows[0];

                        if (row["estado"] != DBNull.Value && row["observaciones"] != DBNull.Value)
                        {
                            // Actualizar los controles 
                            textBoxEstatus.Text = row["estado"].ToString();
                            richTextBox1.Text = row["observaciones"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Verificar si "observaciones" contiene la palabra "registrada"
                        bool contieneRegistrada = await marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idInt);

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

        private async void FrmRegistradas_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            try
            {
                // ===== tu init actual (déjalo igual) =====
                SeleccionarMarca.idInt = 0;
                convertirImagen();
                pictureBox1.Image = documento;

                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
                EliminarTabPage(tabPageListaArchivos);
                ActualizarFechaVencimiento();
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
                    dtgMarcasR.DataSource = null;
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

                    dtgMarcasR.DataSource = null;
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

                    dtgMarcasR.DataSource = null;
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

                    dtgMarcasR.DataSource = null;
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

                    dtgMarcasR.DataSource = null;
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
            LimpiarFormulario();
            if (SeleccionarMarca.idInt > 0)
            {
                EliminarTabPage(tabPageRegistradasList);
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

                    try
                    {

                        if (dtgMarcasR.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasR.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);

                                // Agrega la fecha con texto si aún no está en la justificación
                                string fechaSinHora = fechaAbandono.ToString("dd/MM/yyyy");
                                string formato = fechaSinHora + " Abandono";

                                if (justificacion.Contains(formato))
                                {
                                    
                                    await historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono",
                                        justificacion, usuarioAbandono, "TRÁMITE", null);
                                }
                                else
                                {
                                    justificacion = formato + " " + justificacion;
                                    await historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", fechaAbandono.ToString("dd/MM/yyyy") + " Abandono " + justificacion, usuarioAbandono, "TRÁMITE", null);

                                }

                                MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadMarcas();
                            }
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA PARA PARA ABANDONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                            alerta.ShowDialog();
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

        private async void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaRegistrada frmAgregarEtapa = new FrmAgregarEtapaRegistrada();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                try
                {
                    await refrescarMarca();
                    agregoEstado = true;
                    richTextBox1.Text += "\n" + AgregarEtapa.anotaciones;
                    textBoxEstatus.Text = AgregarEtapa.etapa;
                    FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();

                    if (AgregarEtapa.etapa == "Registrada" || AgregarEtapa.etapa == "Trámite de renovación" || AgregarEtapa.etapa == "Trámite de traspaso" || AgregarEtapa.etapa == "Licencia de uso")
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



                    if (AgregarEtapa.etapa == "Trámite de renovación")
                    {
                        txtERenovacion.Text = AgregarEtapa.numExpediente.ToString();
                        txtERenovacion.Enabled = true;


                    }
                    else if (AgregarEtapa.etapa == "Trámite de traspaso")
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

        private async void roundedButton6_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                await loadRenovacionesById();
                AnadirTabPage(tabPageRenovacionesList);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        public async void EditarHistorial()
        {
            if (dtgHistorialR.SelectedRows.Count > 0)
            {
                Habilitar();
                var filaSeleccionada = dtgHistorialR.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    DataTable historial = await historialModel.GetHistorialById(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];
                        if (fila["Origen"].ToString() == "TRÁMITE")
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

                            if (fila["fechaVencimiento"] != DBNull.Value)
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
        }
        private void iconButton5_Click(object sender, EventArgs e)
        {
            EditarHistorial();
        }

        private async void iconButton4_Click(object sender, EventArgs e)
        {
            if (dtgHistorialR.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialR.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    string etapa = dataRowView["etapa"].ToString();
                    string anotaciones = dataRowView["anotaciones"].ToString();
                    string usuario = UsuarioActivo.usuario;
                    SeleccionarHistorial.id = id;
                    SeleccionarHistorial.etapa = etapa;
                    SeleccionarHistorial.anotaciones = anotaciones;


                    DialogResult confirmacionInicial = MessageBox.Show("¿Está seguro que desea eliminar este estado? " + usuario, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

                        await loadHistorialById();
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
            }
        }

        private async void btnEditarH_Click(object sender, EventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
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
                }
            }

        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }
        public async void VerHistorial()
        {
            if (dtgHistorialR.SelectedRows.Count > 0)
            {
                Deshabilitar();
                var filaSeleccionada = dtgHistorialR.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    DataTable historial = await historialModel.GetHistorialById(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];

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


                        if (fila["fechaVencimiento"] != DBNull.Value)
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
        private void iconButton7_Click(object sender, EventArgs e)
        {
            VerHistorial();
        }

        private async void roundedButton8_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                await loadHistorialById();
                AnadirTabPage(tabPageHistorialMarca);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void iconButton5_Click_1(object sender, EventArgs e)
        {
            if (dtgRenovaciones.SelectedRows.Count > 0)
            {
                //DeshabilitarRenovacion();
                var filaSeleccionada = dtgRenovaciones.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(filaSeleccionada.Cells["Id"].Value);
                    //int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    DataTable renovacion = await historialModel.GetHistorialById(id);

                    if (renovacion.Rows.Count > 0)
                    {
                        DataRow fila = renovacion.Rows[0];

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

                        AnadirTabPage(tabPageRenovacionDetail);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles de la renovación", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione una fila de renovación.");
            }
        }

        public async Task EditarRenovaciones()
        {
            if (dtgRenovaciones.SelectedRows.Count > 0)
            {
                //Habilitar();
                var filaSeleccionada = dtgRenovaciones.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    //int id = Convert.ToInt32(dataRowView["id"]);
                    int id = Convert.ToInt32(filaSeleccionada.Cells["Id"].Value);
                    SeleccionarRenovacion.idRenovacion = id;

                    DataTable renovacion = await renovacionesModel.GetRenovacionById(id);

                    if (renovacion.Rows.Count > 0)
                    {
                        DataRow fila = renovacion.Rows[0];

                        SeleccionarRenovacion.idRenovacion = Convert.ToInt32(fila["Id"]);
                        //SeleccionarRenovacion.Reg_Antiguo = (DateTime)fila["FechaRegistroAntigua"];
                        //SeleccionarRenovacion.Reg_nuevo = (DateTime)fila["FechaRegistroNueva"];
                        SeleccionarRenovacion.Venc_antiguo = Convert.ToDateTime(fila["FechaVencimientoAntigua"].ToString());
                        SeleccionarRenovacion.Venc_nuevo = Convert.ToDateTime(fila["FechaVencimientoNueva"].ToString());
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
        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            EditarRenovaciones();
        }

        private async void iconButton1_Click_1(object sender, EventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                string numExpediente = txtNoExpediente.Text;

                DateTime fechaVencimientoA = dateFechVencAnt.Value;
                DateTime fechaVencimientoN = dateFechVencNueva.Value;
                int id = SeleccionarRenovacion.idRenovacion;
                int idMarca = SeleccionarRenovacion.IdMarca;

                if (!string.IsNullOrEmpty(numExpediente))
                {
                    bool actualizado = await renovacionesModel.ActualizarRenovacion(id, numExpediente, idMarca, fechaVencimientoA, fechaVencimientoN);

                    if (actualizado)
                    {
                        await loadRenovacionesById();
                        FrmAlerta alerta = new FrmAlerta("RENOVACIÓN ACTUALIZADA CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.Show();
                        AnadirTabPage(tabPageRenovacionesList);
                        EliminarTabPage(tabPageRenovacionDetail);

                    }
                    else
                    {
                        FrmAlerta alerta = new FrmAlerta("NO FUE POSIBLE ACTUALIZAR LA RENOVACIÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.Show();
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("EL NÚMERO DE EXPEDIENTE NO PUEDE ESTAR VACÍO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.Show();
                    //MessageBox.Show("El número de expediente no puede estar vacío.");
                }
            }


        }

        private void dateFechRegAntigua_ValueChanged(object sender, EventArgs e)
        {
            //ActualizarFechaRegistroAntiguoRenovacion();
        }

        private void dateFechRegNueva_ValueChanged(object sender, EventArgs e)
        {
            //ActualizarFechaRegistroNuevoRenovacion();
        }

        private async void roundedButton9_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                await loadTraspasosById();
                AnadirTabPage(tabPageTraspasosList);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        public async Task EditarTraspaso()
        {
            if (dtgTraspasos.SelectedRows.Count > 0)
            {
                //Habilitar();
                var filaSeleccionada = dtgTraspasos.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    //int id = Convert.ToInt32(dataRowView["id"]);
                    int id = Convert.ToInt32(filaSeleccionada.Cells["Id"].Value);
                    SeleccionarTraspaso.id = id;

                    DataTable traspaso = await traspasosModel.ObtenerTraspasoPorId(id);

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

        private async void btnEditarTraspaso_Click(object sender, EventArgs e)
        {
            await EditarTraspaso();
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

        private async Task iconButton4_Click_2(object sender, EventArgs e)
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
                await loadTraspasosById();
                AnadirTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE LLENAR TODOS LOS CAMPOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Debe llenar todos los campos para poder actualizar el traspaso", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButton5_Click_2(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageTraspasosList);
            EliminarTabPage(tabPageTraspasoDetail);
        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageRenovacionesList);
            EliminarTabPage(tabPageRenovacionDetail);
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageRenovacionesList);
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageTraspasosList);
        }

        public void VerificarDatosRegistro()
        {
            if (checkBox1.Checked == true && (string.IsNullOrEmpty(txtRegistro.Text)
                //|| string.IsNullOrEmpty(txtFolio.Text) 
                //|| string.IsNullOrEmpty(txtLibro.Text)
                ))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }

        private async void btnActualizarM_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                ActualizarMarcaNacional();

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void btnCancelarM_Click(object sender, EventArgs e)
        {

            DatosRegistro.peligro = false;
            EliminarTabPage(tabPageHistorialMarca);
            AnadirTabPage(tabPageRegistradasList);
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageListaArchivos);
            tabControl1.SelectedTab = tabPageRegistradasList;
            await LoadMarcas();
            SeleccionarMarca.idInt = 0;
            LimpiarFormulario();
        }

        private async void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = await marcaModel.GetFilteredMarcasInternacionalesRegistradasCount(txtBuscar.Text);
            totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalRows) / pageSize));

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }

        private void dtgMarcasR_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }

        private void dtgHistorialR_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarHistorial();
        }

        private async void dtgRenovaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            await EditarRenovaciones();
        }

        private async void dtgTraspasos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            await EditarTraspaso();
        }

        private async void iconButton14_Click(object sender, EventArgs e)
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
                totalRows = await marcaModel.GetFilteredMarcasInternacionalesRegistradasCount(txtBuscar.Text);
                totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalRows) / pageSize));

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

        private void iconButton10_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageListaArchivos);
        }

        private async void roundedButton11_Click(object sender, EventArgs e)
        {
            await ListarArchivosEnGeneral();
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

        public async Task ListarArchivosEnGeneral()
        {
            try
            {
                // Cambiar el cursor global a "WaitCursor"
                Cursor.Current = Cursors.WaitCursor;

                AnadirTabPage(tabPageListaArchivos);
                tabControl1.Visible = false;

                string id = "" + SeleccionarMarca.idInt;
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

        public void Abrir()
        {
            string idMarca = "" + SeleccionarMarca.idInt; // Id de la marca actual
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

        public async Task Eliminar()
        {
            string idMarca = "" + SeleccionarMarca.idInt; // Id de la marca actual
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

        private async void iconButton13_Click(object sender, EventArgs e)
        {
            await SubirArchivoAsync("" + SeleccionarMarca.idInt);
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

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            FrmMostrarClientes frmMostrarClientes = new FrmMostrarClientes();
            frmMostrarClientes.ShowDialog();

            if (SeleccionarPersona.idPersonaC != 0)
            {
                txtNombreCliente.Text = SeleccionarPersona.nombre;
            }
            else
            {
                SeleccionarPersona.idPersonaC = null;
                txtNombreCliente.Text = "";
            }
        }

        private void tabPageRegistradasList_Click(object sender, EventArgs e)
        {

        }

        private void dtgMarcasR_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgMarcasR.Columns["id"] != null)
            {
                dtgMarcasR.Columns["id"].Visible = false;
            }

            dtgMarcasR.ClearSelection();
        }

        private void dtgHistorialR_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgHistorialR.Columns["id"] != null)
            {
                dtgHistorialR.Columns["id"].Visible = false;
            }

            dtgHistorialR.ClearSelection();
        }

        private void dtgRenovaciones_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgRenovaciones.Columns["id"] != null)
            {
                dtgRenovaciones.Columns["id"].Visible = false;
                dtgRenovaciones.Columns["IdMarca"].Visible = false;
            }

            dtgRenovaciones.ClearSelection();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnEliminarMarca_Click(object sender, EventArgs e)
        {
            // Verificar que haya una fila seleccionada
            if (dtgMarcasR.SelectedRows.Count == 0)
            {
                FrmAlerta alerta = new FrmAlerta("Debe seleccionar una marca para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }

            var filaSeleccionada = dtgMarcasR.SelectedRows[0];
            if (!(filaSeleccionada.DataBoundItem is DataRowView dataRowView))
            {
                FrmAlerta alerta = new FrmAlerta("Error al obtener la información de la marca", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                return;
            }

            int idMarca = Convert.ToInt32(dataRowView["Id"]);

            FrmAlerta confirmar = new FrmAlerta(
                "¿Está seguro que desea eliminar esta marca?\n\n" +
                "Nota: Si está relacionada con licencias de uso, también serán eliminadas.",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            var resultado = confirmar.ShowDialog();

            if (resultado == DialogResult.Yes)
            {
                MarcaModel model = new MarcaModel();
                bool eliminado = false;

                try
                {
                    eliminado = await model.EliminarMarcaConLog(idMarca, UsuarioActivo.usuario);
                }
                catch (Exception ex)
                {
                    FrmAlerta error = new FrmAlerta("Error al eliminar la marca:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error.ShowDialog();
                    return;
                }

                if (eliminado)
                {
                    FrmAlerta exito = new FrmAlerta("Marca eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    exito.ShowDialog();

                    await LoadMarcas();
                }
                else
                {
                    FrmAlerta error = new FrmAlerta("No se pudo eliminar la marca. Puede que no exista o esté relacionada con datos protegidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error.ShowDialog();
                }
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

        private void FrmRegistradas_Resize(object sender, EventArgs e)
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

                        if (dtgMarcasR.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasR.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);
                                
                                // Agrega la fecha con texto si aún no está en la justificación
                                string fechaSinHora = fechaAbandono.ToString("dd/MM/yyyy");
                                string formato = fechaSinHora + " Desistimiento";

                                if (justificacion.Contains(formato))
                                {

                                    await historialModel.GuardarEtapa(idMarca, fechaAbandono, "Desistimiento",
                                        justificacion, usuarioAbandono, "TRÁMITE", null);
                                }
                                else
                                {
                                    justificacion = formato + " " + justificacion;
                                    await historialModel.GuardarEtapa(idMarca, fechaAbandono, "Desistimiento", fechaAbandono.ToString("dd/MM/yyyy") + " Desistimiento " + justificacion, usuarioAbandono, "TRÁMITE", null);

                                }


                                MessageBox.Show("La marca ha sido marcada como 'Desistida'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
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
            if (labelVenc.Visible)
            {
                comboBoxEstatusH_SelectedIndexChanged(sender, e);
            }
        }

        private async void btnEditarTraspasoDetalle_Click(object sender, EventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
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

                    await traspasosModel.ActualizarTraspaso(idTraspaso, numeroExpediente, idMarca, idTitularAntiguo, idTitularNuevo);
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
            
        }
    }
}
