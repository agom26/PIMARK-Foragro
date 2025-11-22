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
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMarcasIntRegistradas : Form, IAsyncLoadable
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
        private bool archivoSubido = false;
        private bool buscando = false;
        bool agregoEstado = false;
        private bool _isLoading;
        //ftp
        const string URL = "https://foragro.com.es/peticiones/archivos_marcas_nacionales.php";
        const string TOKEN = "TOKEN_SECRETO_LARGO_Y_UNICO";
        //private string host = "ftp.foragro.com.es"; // Tu host FTP
        //private string usuario = "foragro"; // Tu usuario FTP
        //private string contraseña = "gqL8ygtSv6Z8"; // Tu contraseña FTP
        //private string directorioBase = "/foragro.com.es/marcas/nacionales"; // La ruta base de tu servidor
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

        public FrmMarcasIntRegistradas()
        {
            InitializeComponent();
            SeleccionarMarca.idN = 0;
            ActualizarFechaVencimiento();

            if (UsuarioActivo.isAdmin)
            {
                btnEliminarMarca.Visible = true;
            }
            else
            {
                btnEliminarMarca.Visible = false;
            }


            if (UsuarioActivo.soloLectura)
            {
                btnAbandonar.Visible = false;
                btnDesistir.Visible = false;
                btnEditar2.Visible = false;
                btnAgregarEstado.Visible = false;
                btnAgregarAgente.Enabled = false;
                btnAgregarCliente.Enabled = false;
                btnAgregarTitular.Enabled = false;
                btnEditarHistorial2.Visible = false;

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
                txtERenovacion.ReadOnly = true;
                txtETraspaso.ReadOnly = true;
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
                btnAbandonar.Visible = true;
                btnDesistir.Visible = true;
                btnEditar2.Visible = true;
                btnAgregarEstado.Visible = true;
                btnAgregarAgente.Enabled = true;
                btnAgregarCliente.Enabled = true;
                btnAgregarTitular.Enabled = true;
                btnEditarHistorial2.Visible = true;


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
                txtERenovacion.ReadOnly = false;
                txtETraspaso.ReadOnly = false;
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
            totalRows = await marcaModel.GetTotalMarcasRegistradas();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            var marcasN = await marcaModel.GetAllMarcasNacionalesRegistradas(currentPageIndex, pageSize);

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


        private async Task filtrar()
        {
            string buscar = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(buscar))
            {
                totalRows = await marcaModel.GetFilteredMarcasRegistradasCount(buscar);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();

                DataTable dt = await marcaModel.FiltrarMarcasNacionalesRegistradas(buscar, currentPageIndex, pageSize);
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

        /* anteriores
        private async Task LoadMarcas()
        {
           
            try
            {
                totalRows = await marcaModel.GetTotalMarcasRegistradas();
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                var marcasN = await marcaModel.GetAllMarcasNacionalesRegistradas(currentPageIndex, pageSize);

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

            if (!string.IsNullOrEmpty(buscar))
            {
                try
                {
                    totalRows = await marcaModel.GetFilteredMarcasRegistradasCount(buscar);
                    totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();

                    DataTable titulares = await marcaModel.FiltrarMarcasNacionalesRegistradas(buscar, currentPageIndex, pageSize);

                    if (titulares.Rows.Count > 0)
                    {
                        dtgMarcasIn.DataSource = titulares;

                        if (dtgMarcasIn.Columns["id"] != null)
                            dtgMarcasIn.Columns["id"].Visible = false;

                        dtgMarcasIn.ClearSelection();
                    }
                    else
                    {
                        new FrmAlerta(
                            "NO EXISTEN MARCAS CON ESOS DATOS",
                            "MENSAJE",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.None
                        ).ShowDialog();

                        await LoadMarcas();
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
            else
            {
                await LoadMarcas();
            }
        }*/



        /*
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = await marcaModel.GetFilteredMarcasRegistradasCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = await marcaModel.FiltrarMarcasNacionalesRegistradas(buscar, currentPageIndex, pageSize);
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
                (registroChek && !int.TryParse(folio, out _)) ||
                (registroChek && !int.TryParse(libro, out _)))
            {
                FrmAlerta alerta = new FrmAlerta("LA CLASE, FOLIO Y TOMO\nDEBEN SER VALORES NUMÉRICOS ENTEROS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("El expediente, clase, folio, registro y tomo deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if ((comboBoxSignoDistintivo.Text == "Marca" &&
                 comboBoxTipoSigno.Text == "Gráfica/Figurativa" || comboBoxTipoSigno.Text == "Mixta")
                 || (comboBoxSignoDistintivo.Text == "Emblema" && comboBoxTipoSigno.Text == "Gráfica/Figurativa")
                 || (comboBoxSignoDistintivo.Text == "Emblema" && comboBoxTipoSigno.Text == "Mixta")
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
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string? signoDistintivo = comboBoxSignoDistintivo.SelectedItem?.ToString();
            string? tipoSigno = comboBoxTipoSigno.SelectedItem?.ToString();
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[]? logo = null;

            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            int? idCliente = SeleccionarPersona.idPersonaC;

            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;

            string etrasp = txtETraspaso.Text;
            string erenov = txtERenovacion.Text;
            string estado = textBoxEstatus.Text;

            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text.Trim();
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime? fecha_vencimiento = dateTimePFecha_vencimiento.Value;
            string ubicacionF = txtUbicacion.Text;
            int indefinida = 0;

            if (idTitular <= 0)
            {
                new FrmAlerta("SELECCIONE UN TITULAR VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
                return;
            }

            if (idAgente <= 0)
            {
                new FrmAlerta("SELECCIONE UN AGENTE VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
                return;
            }

            if (idCliente <= 0)
                idCliente = null;

            if (!ValidarCampos(expediente, nombre, clase, signoDistintivo, tipoSigno, estado, ref logo, registroChek, registro, folio, libro))
                return;

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

            if (estado == "Trámite de renovación" && string.IsNullOrEmpty(erenov))
            {
                new FrmAlerta("POR FAVOR INGRESE EL NÚMERO DE TRÁMITE DE RENOVACIÓN", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
                return;
            }

            if (estado == "Trámite de traspaso" && string.IsNullOrEmpty(etrasp))
            {
                new FrmAlerta("POR FAVOR INGRESE EL NÚMERO DE TRÁMITE DE TRASPASO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
                return;
            }

            if (registroChek && await marcaModel.ExisteRegistro(registro, SeleccionarMarca.idN))
            {
                new FrmAlerta("ESTE REGISTRO YA EXISTE EN LA BASE DE DATOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
                return;
            }

            try
            {
                bool esActualizado;

                if (registroChek)
                {
                    esActualizado = await marcaModel.EditMarcaNacionalRegistradaNuevo(
                        SeleccionarMarca.idN, expediente, nombre, signoDistintivo, tipoSigno, clase, folio,
                        libro, logo, idTitular, idAgente, solicitud, registro, fecha_registro, indefinida,fecha_vencimiento,
                        erenov, etrasp, idCliente, ubicacionF
                    );
                }
                else
                {
                    esActualizado = await marcaModel.EditMarcaNacionalNuevo(
                        SeleccionarMarca.idN, expediente, nombre, signoDistintivo, tipoSigno, clase,
                        logo, idTitular, idAgente, solicitud, idCliente, ubicacionF
                    );
                }

                // 4. Verificación post-actualización
                if (!esActualizado)
                {
                    MessageBox.Show("Error al actualizar la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (agregoEstado)
                {
                    await historialModel.GuardarEtapa(
                       SeleccionarMarca.idN,
                       Convert.ToDateTime(AgregarEtapa.fecha),
                       AgregarEtapa.etapa,
                       AgregarEtapa.anotaciones,
                       UsuarioActivo.usuario,
                       "TRÁMITE",
                       null
                   );
                    agregoEstado = false;

                }
                new FrmAlerta("MARCA NACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information).ShowDialog();

                SeleccionarMarca.idN = 0;
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageMarcaDetail);
                await LoadMarcas();
                AnadirTabPage(tabPageRegistradasList);
            }
            catch (Exception ex)
            {
                string mensaje = $"ERROR AL {(registroChek ? "REGISTRAR" : "ACTUALIZAR")} LA MARCA INTERNACIONAL:\n{ex.Message.ToUpper()}";
                new FrmAlerta(mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
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
            txtNombreCliente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
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
                // Obtener los datos de la marca seleccionada
                DataTable detallesMarcaInter = await Task.Run(() => marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN));

                if (detallesMarcaInter.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron detalles de la marca", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow row = detallesMarcaInter.Rows[0];

                // Validar si hay expediente
                if (row["expediente"] == DBNull.Value)
                {
                    MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Asignar datos a la clase SeleccionarMarca
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
                SeleccionarMarca.erenov = row["Erenov"].ToString();
                SeleccionarMarca.etraspaso = row["Etrasp"].ToString();
                txtUbicacion.Text = row["ubicacion_fisica"] != DBNull.Value ? row["ubicacion_fisica"].ToString() : string.Empty;

                bool tieneLogo = await marcaModel.MarcaTieneLogoAsync(SeleccionarMarca.idN);

                if (tieneLogo)
                {
                    SeleccionarMarca.logo = await marcaModel.ObtenerLogoMarcaPorIdNuevo(SeleccionarMarca.idN);
                }
                else
                {
                    SeleccionarMarca.logo = null;
                }



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

                // Guardar IDs en SeleccionarPersona
                SeleccionarPersona.idPersonaT = SeleccionarMarca.idPersonaTitular;
                SeleccionarPersona.idPersonaA = SeleccionarMarca.idPersonaAgente;
                SeleccionarPersona.idPersonaC = SeleccionarMarca.idPersonaCliente;

                // Cargar datos de titular, agente y cliente en paralelo
                var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));
                var clienteTask = SeleccionarMarca.idPersonaCliente != 0
                    ? Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaCliente))
                    : null;

                await Task.WhenAll(titularTask, agenteTask);

                var titular = titularTask.Result;
                var agente = agenteTask.Result;
                var cliente = clienteTask?.Result;

                // Mostrar datos de las personas en los controles
                if (titular.Count > 0)
                    txtNombreTitular.Text = titular[0].nombre;

                if (agente.Count > 0)
                    txtNombreAgente.Text = agente[0].nombre;

                if (cliente != null && cliente.Count > 0)
                    txtNombreCliente.Text = cliente[0].nombre;

                // Mostrar datos generales en controles del formulario
                txtExpediente.Text = SeleccionarMarca.expediente;
                txtNombre.Text = SeleccionarMarca.nombre;
                txtClase.Text = SeleccionarMarca.clase;
                textBoxEstatus.Text = SeleccionarMarca.estado;
                comboBoxSignoDistintivo.SelectedItem = SeleccionarMarca.signoDistintivo;
                comboBoxTipoSigno.SelectedItem = SeleccionarMarca.tipoSigno;
                datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                richTextBox1.Text = SeleccionarMarca.observaciones;
                txtERenovacion.Text = SeleccionarMarca.erenov;
                txtETraspaso.Text = SeleccionarMarca.etraspaso;


                // Verificar si la marca tiene etapa registrada
                bool contieneRegistrada = await marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idN);

                if (contieneRegistrada)
                {
                    checkBox1.Checked = true;
                    mostrarPanelRegistro("si");

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

                            dateTimePFecha_vencimiento.Value = Convert.ToDateTime(row["fechaVencimiento"].ToString());
                            SeleccionarMarca.fechaVencimiento = Convert.ToDateTime(row["fechaVencimiento"].ToString());
                        }

                    }


                    
                    

                }
                else
                {
                    checkBox1.Checked = false;
                    mostrarPanelRegistro("no");
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
                DataTable renovaciones = await renovacionesModel.GetAllRenovacionesByIdMarca(SeleccionarMarca.idN);

                // Invoca el método para actualizar el DataGridView en el hilo principal

                dtgRenovaciones.AutoGenerateColumns = true;
                dtgRenovaciones.DataSource = renovaciones;
                dtgRenovaciones.Refresh();


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
                DataTable traspasos = await traspasosModel.ObtenerTraspasosMarcaPorIdMarca(SeleccionarMarca.idN);

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgTraspasos.AutoGenerateColumns = true;
                    dtgTraspasos.DataSource = traspasos;
                    dtgTraspasos.Refresh();


                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los traspasos de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                
                EliminarTabPage(tabPageListaArchivos);
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
                EliminarTabPage(tabPageListaArchivos);
            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                
                await CargarDatosMarca();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasoDetail);
            }
            else if (tabControl1.SelectedTab == tabPageRenovacionesList)
            {
                loadRenovacionesById();
                SeleccionarRenovacion.idRenovacion = 0;
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
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
            }*/
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

        public void Editar()
        {
            VerificarSeleccionIdMarcaEdicion();
            if (SeleccionarMarca.idN > 0)
            {
                LimpiarControles();
                EliminarTabPage(tabPageRegistradasList);
                using (var loading = new FrmLoading(() => CargarDatosMarca()))
                {
                    loading.ShowDialog(this);
                }
                AnadirTabPage(tabPageMarcaDetail);
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

                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", justificacion, usuarioAbandono, "TRÁMITE", null);
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

        private async void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaRegistrada frmAgregarEtapa = new FrmAgregarEtapaRegistrada();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                try
                {
                    historialModel.GuardarEtapa(SeleccionarMarca.idN, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE", null);
                    FrmAlerta alerta = new FrmAlerta("ESTADO AGREGADO CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    if (AgregarEtapa.etapa == "Registrada")
                    {
                        checkBox1.Checked = true;
                        mostrarPanelRegistro("si");
                        await refrescarMarca();
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
                            await CargarDatosMarca();
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
                            await CargarDatosMarca();
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


        public async void EditarVerHistorial()
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

                            comboBoxEstatusH.SelectedItem = SeleccionarHistorial.etapa;
                            dateTimePickerFechaIngreso.Value = SeleccionarHistorial.fecha;
                            richTextBoxAnotacionesH.Text = SeleccionarHistorial.anotaciones;
                            labelUserEditor.Text = UsuarioActivo.usuario;
                            lblUser.Text = SeleccionarHistorial.usuario;

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
            if (!UsuarioActivo.soloLectura)
            {
                EditarVerHistorial();
            }

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
            if (labelVenc.Visible)
            {
                comboBoxEstatusH_SelectedValueChanged(sender, e);
            }
        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string etapa = comboBoxEstatusH.Text;
            bool mostrarVencimiento = etapa == "Examen de fondo" ||
                             etapa == "Requerimiento" ||
                             etapa == "Objeción" ||
                             etapa == "Publicación" ||
                             etapa == "Orden de pago";

            labelVenc.Visible = mostrarVencimiento;
            dateTimePickerFechaVencimiento.Visible = mostrarVencimiento;

            string fecha = dateTimePickerFechaIngreso.Value.ToString("dd/MM/yyyy");
            string venc = dateTimePickerFechaVencimiento.Value.ToString("dd/MM/yyyy");

            if (mostrarVencimiento)
            {
                richTextBoxAnotacionesH.Text = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (etapa == "Resolución RPI favorable" ||
                     etapa == "Resolución RPI desfavorable" ||
                     etapa == "Recurso de revocatoria" ||
                     etapa == "Resolución Ministerio de Economía (MINECO)" ||
                     etapa == "Contencioso administrativo")
            {
                richTextBoxAnotacionesH.Text = $"{fecha} Por objeción-{etapa}";
            }
            else
            {
                richTextBoxAnotacionesH.Text = $"{fecha} {etapa}";
            }
        }

        private void comboBoxEstatusH_SelectedValueChanged(object sender, EventArgs e)
        {
            string etapa = comboBoxEstatusH.Text;
            bool mostrarVencimiento = etapa == "Examen de fondo" ||
                             etapa == "Requerimiento" ||
                             etapa == "Objeción" ||
                             etapa == "Publicación" ||
                             etapa == "Orden de pago";

            labelVenc.Visible = mostrarVencimiento;
            dateTimePickerFechaVencimiento.Visible = mostrarVencimiento;

            string fecha = dateTimePickerFechaIngreso.Value.ToString("dd/MM/yyyy");
            string venc = dateTimePickerFechaVencimiento.Value.ToString("dd/MM/yyyy");

            if (mostrarVencimiento)
            {
                richTextBoxAnotacionesH.Text = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (etapa == "Resolución RPI favorable" ||
                     etapa == "Resolución RPI desfavorable" ||
                     etapa == "Recurso de revocatoria" ||
                     etapa == "Resolución Ministerio de Economía (MINECO)" ||
                     etapa == "Contencioso administrativo")
            {
                richTextBoxAnotacionesH.Text = $"{fecha} Por objeción-{etapa}";
            }
            else
            {
                richTextBoxAnotacionesH.Text = $"{fecha} {etapa}";
            }

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

        private async void FrmMarcasIntRegistradas_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            try
            {
                // ===== tu init actual (déjalo igual) =====
                SeleccionarMarca.idN = 0;
                archivoSubido = false;
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

        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
        }

        private async void roundedButton10_Click(object sender, EventArgs e)
        {
            await loadRenovacionesById();
            AnadirTabPage(tabPageRenovacionesList);
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageRenovacionesList);
        }

        private async void roundedButton9_Click(object sender, EventArgs e)
        {
            await loadTraspasosById();
            AnadirTabPage(tabPageTraspasosList);
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageTraspasosList);
        }

        public async Task EditarVerRenovacion()
        {
            if (dtgRenovaciones.SelectedRows.Count > 0)
            {
                //Habilitar();
                var filaSeleccionada = dtgRenovaciones.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(filaSeleccionada.Cells["Id"].Value);
                    SeleccionarRenovacion.idRenovacion = id;

                    DataTable renovacion = await renovacionesModel.GetRenovacionById(id);

                    if (renovacion.Rows.Count > 0)
                    {
                        DataRow fila = renovacion.Rows[0];

                        SeleccionarRenovacion.idRenovacion = id;
                        //SeleccionarRenovacion.Reg_Antiguo = (DateTime)fila["FechaRegistroAntigua"];
                        //SeleccionarRenovacion.Reg_nuevo = (DateTime)fila["FechaRegistroNueva"];
                        SeleccionarRenovacion.Venc_antiguo = Convert.ToDateTime(fila["FechaVencimientoAntigua"].ToString());
                        SeleccionarRenovacion.Venc_nuevo = Convert.ToDateTime(fila["FechaVencimientoNueva"].ToString());
                        SeleccionarRenovacion.NumExpediente = fila["NumExpediente"].ToString();
                        SeleccionarRenovacion.IdMarca = Convert.ToInt32(fila["IdMarca"].ToString());
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
        private async void btnEditarRenovacion_Click(object sender, EventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                await EditarVerRenovacion();
            }

        }

        private async void iconButton7_Click(object sender, EventArgs e)
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
                    actualizado = await renovacionesModel.ActualizarRenovacion(id, numExpediente, idMarca, fechaVencimientoA, fechaVencimientoN);


                }
                catch (Exception ex)
                {
                    FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                }


                if (actualizado)
                {
                    await loadRenovacionesById();
                    FrmAlerta alerta = new FrmAlerta("RENOVACIÓN ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show("Registro de renovación actualizado exitosamente.");
                    EliminarTabPage(tabPageRenovacionDetail);
                    AnadirTabPage(tabPageRenovacionesList);

                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO FUE POSIBLE ACTUALIZAR LA RENOVACIÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                    //MessageBox.Show("No se pudo actualizar el registro de renovación.");
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("EL NÚMERO DE EXPEDIENTE NO PUEDE ESTAR VACÍO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("El número de expediente no puede estar vacío.");
            }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageRenovacionesList);
            EliminarTabPage(tabPageRenovacionDetail);
        }

        public async Task EditarVerTraspaso()
        {
            if (dtgTraspasos.SelectedRows.Count > 0)
            {
                //Habilitar();
                var filaSeleccionada = dtgTraspasos.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(filaSeleccionada.Cells["Id"].Value);
                    SeleccionarTraspaso.id = id;

                    DataTable traspaso = await traspasosModel.ObtenerTraspasoPorId(id);

                    if (traspaso.Rows.Count > 0)
                    {
                        DataRow fila = traspaso.Rows[0];

                        SeleccionarTraspaso.id = id;
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
            if (!UsuarioActivo.soloLectura)
            {
                await EditarVerTraspaso();
            }

        }

        private async void iconButton10_Click(object sender, EventArgs e)
        {
            iconButton10.Enabled = false;
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
                try
                {
                    using (var loading = new FrmLoading(() => traspasosModel.ActualizarTraspaso(idTraspaso, numeroExpediente, idMarca, idTitularAntiguo, idTitularNuevo)))
                    {

                        loading.ShowDialog(this);
                    }
                    await loadTraspasosById();
                    await CargarDatosMarca();
                    FrmAlerta alerta = new FrmAlerta("TRASPASO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    EliminarTabPage(tabPageTraspasoDetail);
                    AnadirTabPage(tabPageTraspasosList);
                }
                finally
                {
                    iconButton10.Enabled = true;
                }

               
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
            AnadirTabPage(tabPageTraspasosList);
            EliminarTabPage(tabPageTraspasoDetail);
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
            buscando = false;
            Editar();
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

        private async void iconButton12_Click(object sender, EventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                VerificarDatosRegistro();
                if (DatosRegistro.peligro == false)
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
                        await ActualizarMarcaInternacional();
                    }

                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
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
        private async void iconButton13_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                AnadirTabPage(tabPageRegistradasList);
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageListaArchivos);
                await LoadMarcas();
                SeleccionarMarca.idN = 0;
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
                    await refrescarMarca();
                    agregoEstado = true;
                    richTextBox1.Text += "\n" + AgregarEtapa.anotaciones;
                    textBoxEstatus.Text = AgregarEtapa.etapa;
                    //historialModel.GuardarEtapa(SeleccionarMarca.idN, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE");
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



                    if (AgregarEtapa.etapa == "Trámite de renovación" && AgregarEtapa.numExpediente != "")
                    {
                        txtERenovacion.Text = AgregarEtapa.numExpediente.ToString();
                        txtERenovacion.Enabled = true;

                    }
                    else if (AgregarEtapa.etapa == "Trámite de traspaso" && AgregarEtapa.numExpediente != "")
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
        private void LimpiarTablaHistorial()
        {
            dtgHistorialIn.DataSource = null;
            dtgHistorialIn.Rows.Clear();
            dtgHistorialIn.Refresh();
        }

        private async void roundedButton2_Click_1(object sender, EventArgs e)
        {
            LimpiarTablaHistorial();
            await loadHistorialById();
            AnadirTabPage(tabPageHistorialMarca);

        }

        private async void roundedButton6_Click_1(object sender, EventArgs e)
        {
            await loadRenovacionesById();
            AnadirTabPage(tabPageRenovacionesList);
        }

        private async void roundedButton9_Click_1(object sender, EventArgs e)
        {
            await loadTraspasosById();
            AnadirTabPage(tabPageTraspasosList);
        }

        private void dtgHistorialIn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                EditarVerHistorial();
            }

        }

        private async void dtgRenovaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                await EditarVerRenovacion();
            }

        }

        private async void dtgTraspasos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                await EditarVerTraspaso();
            }

        }

        private async void ibtnBuscar_Click(object sender, EventArgs e)
        {
            await filtrar();
        }

        private async void iconButton14_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            await filtrar();
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

        private async void ibtnBuscar_Click_1(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = await marcaModel.GetFilteredMarcasRegistradasCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            await filtrar();
        }

        private async void iconButton14_Click_1(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            await filtrar();
        }

        private void dateTimePFecha_Registro_ValueChanged_1(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = await marcaModel.GetFilteredMarcasRegistradasCount(txtBuscar.Text);
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

        private void textBoxEstatus_TextChanged(object sender, EventArgs e)
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
        /*
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
            var fc = new StreamContent(System.IO.File.OpenRead(file.FullName));

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

                    MessageBox.Show($"Error al subir el archivo: {ex.Message}\n" + ex.InnerException.Message);
                }
                Cursor.Current = Cursors.Default;
            }
        }*/

        class ListarResp
        {
            public bool ok { get; set; }
            public int count { get; set; }
            public List<string> files { get; set; } = new();
            public string message { get; set; }
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
        /*
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

                if (System.IO.File.Exists(rutaLocal))
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
        /*
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

                await ListarArchivosEnGeneral();
                Cursor.Current = Cursors.Default;
            }
        }


        private async void roundedButton13_Click(object sender, EventArgs e)
        {
            await ListarArchivosEnGeneral();
        }

        private void iconButton18_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageListaArchivos);
        }

        private async void iconButton17_Click(object sender, EventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                await SubirArchivoAsync("" + SeleccionarMarca.idN);
                await ListarArchivosEnGeneral();
            }

        }

        private void iconButton16_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void iconButton15_Click(object sender, EventArgs e)
        {
            if (!UsuarioActivo.soloLectura)
            {
                Eliminar();
            }

        }

        private void dtgArchivos_DoubleClick(object sender, EventArgs e)
        {
            Abrir();
        }

        private void dtgMarcasIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgMarcasIn.Columns["id"] != null)
                dtgMarcasIn.Columns["id"].Visible = false;


            dtgMarcasIn.Columns["CLASE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dtgMarcasIn.Columns["CLASE"].Width = 50;      // ancho fijo
            dtgMarcasIn.Columns["CLASE"].MinimumWidth = 40; // opcional

            dtgMarcasIn.Columns["REGISTRO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dtgMarcasIn.Columns["REGISTRO"].Width = 80;      // ancho fijo
            dtgMarcasIn.Columns["REGISTRO"].MinimumWidth = 60; // opcional

            dtgMarcasIn.Columns["TOMO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dtgMarcasIn.Columns["TOMO"].Width = 50;      // ancho fijo
            dtgMarcasIn.Columns["TOMO"].MinimumWidth = 40; // opcional

            dtgMarcasIn.Columns["FOLIO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dtgMarcasIn.Columns["FOLIO"].Width = 50;      // ancho fijo
            dtgMarcasIn.Columns["FOLIO"].MinimumWidth = 40; // opcional

            dtgMarcasIn.ClearSelection();
        }

        private async void btnEliminarMarca_Click(object sender, EventArgs e)
        {
            // Verificar que haya una fila seleccionada
            if (dtgMarcasIn.SelectedRows.Count == 0)
            {
                FrmAlerta alerta = new FrmAlerta("Debe seleccionar una marca para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }

            var filaSeleccionada = dtgMarcasIn.SelectedRows[0];
            if (!(filaSeleccionada.DataBoundItem is DataRowView dataRowView))
            {
                FrmAlerta alerta = new FrmAlerta("Error al obtener la información de la marca", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                return;
            }

            int idMarca = Convert.ToInt32(dataRowView["Id"]);

            // Confirmar eliminación con advertencia de licencias
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
                    // Aquí debes tener un método que llame al procedimiento almacenado para eliminar con log
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

        private void dtgHistorialIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgHistorialIn.Columns["id"] != null)
            {
                dtgHistorialIn.Columns["id"].Visible = false;
            }

            dtgHistorialIn.ClearSelection();
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

        private void dtgTraspasos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgTraspasos.Columns["Id"] != null)
            {
                dtgTraspasos.Columns["Id"].Visible = false;
                dtgTraspasos.Columns["IdMarca"].Visible = false;
                dtgTraspasos.Columns["IdTitularAnterior"].Visible = false;
                dtgTraspasos.Columns["IdTitularNuevo"].Visible = false;
            }

            dtgTraspasos.ClearSelection();
        }

        private void FrmMarcasIntRegistradas_Resize(object sender, EventArgs e)
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
            if (labelVenc.Visible)
            {
                comboBoxEstatusH_SelectedValueChanged(sender, e);
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
