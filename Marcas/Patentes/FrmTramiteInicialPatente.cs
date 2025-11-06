using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Presentacion.Patentes
{
    public partial class FrmTramiteInicialPatente : Form
    {
        PatenteModel patenteModel = new PatenteModel();
        HistorialPatenteModel historialPatenteModel = new HistorialPatenteModel();
        private Form1 _form1;
        // Estas variables las declaras en el formulario (nivel de clase)
        private string rutaArchivoLocal = null;
        private string nombreArchivo = null;
        private bool archivoSeleccionado = false;
        private static readonly Regex NombreArchivoRegex =
    new Regex(@"^[\p{L}\p{N}\p{M}_\-\.\s\(\)]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);


        public FrmTramiteInicialPatente(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            archivoSeleccionado = false;
            btnAdjuntarT.Visible = false;
            panel2I.Visible = false;
            lblVencimiento.Visible = false;
            dateTimePFecha_vencimiento.Visible = false;
            ActualizarFechaVencimiento();
            mostrarPanelRegistro("no"); 
            dateTimePFecha_vencimiento.Enabled = true;
        }

        public static bool EsNombreArchivoValido(string nombreArchivo)
        {
            if (string.IsNullOrWhiteSpace(nombreArchivo))
                return false;

            // 🔹 Normalizar a forma compuesta (NFC)
            nombreArchivo = nombreArchivo.Normalize(NormalizationForm.FormC);

            return NombreArchivoRegex.IsMatch(nombreArchivo);
        }

        private (bool ok, string? error) ValidarArchivoLocal(string rutaArchivoLocal, string nombreArchivo, long maxBytes = 20 * 1024 * 1024)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivoLocal) || string.IsNullOrWhiteSpace(nombreArchivo))
                return (false, "Archivo no seleccionado.");

            // Tamaño (20 MB)
            var fi = new FileInfo(rutaArchivoLocal);
            if (!fi.Exists) return (false, "El archivo no existe.");
            if (fi.Length > maxBytes) return (false, "El archivo supera el límite de 20 MB.");

            // Nombre (mismo regex que PHP)
            if (!EsNombreArchivoValido(nombreArchivo))
                return (false, "Nombre de archivo inválido. Solo se permiten letras, números, guiones, guiones bajos, puntos, espacios y paréntesis.");

            return (true, null);
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
            DatosRegistro.peligro = false;
            archivoSeleccionado = false;
            btnAdjuntarT.Visible = false;

            for (int i = 0; i < checkedListBoxDocumentos.Items.Count; i++)
            {
                checkedListBoxDocumentos.SetItemChecked(i, false);
            }

        }

        public void GuardarHistorial(DateTime fecha, string estado, string anotaciones, string usuario, string usuarioEdicion, int idPatente)
        {
            try
            {
                historialPatenteModel.CrearHistorialPatente(fecha, estado, anotaciones, usuario, usuarioEdicion
                    , idPatente,null);
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private static string ExtToMime(string ext)
        {
            switch ((ext ?? string.Empty).ToLowerInvariant())
            {
                case ".png": return "image/png";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".pdf": return "application/pdf";
                case ".txt": return "text/plain";
                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                default: return "application/octet-stream";
            }
        }

        public async Task<bool> SubirArchivoPorPhpAsync(int idPatente)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivoLocal) || string.IsNullOrWhiteSpace(nombreArchivo))
            {
                new FrmAlerta("No hay archivo seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                return false;
            }

            var file = new FileInfo(rutaArchivoLocal);
            if (!file.Exists)
            {
                new FrmAlerta("El archivo no existe en disco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                return false;
            }
            if (file.Length > 20 * 1024 * 1024)
            {
                new FrmAlerta("El archivo supera 20MB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
                return false;
            }

            // URL de tu PHP (el standalone que corregimos o tu endpoint actual)
            const string url = "https://foragro.com.es/subir_archivo_patente_tramite_inicial.php";

            using var form = new MultipartFormDataContent();

            // Campos de texto
            form.Add(new StringContent(idPatente.ToString()), "idPatente");
            // 👉 Enviar SIEMPRE el nombre real en UTF-8
            form.Add(new StringContent(nombreArchivo, Encoding.UTF8, "text/plain"), "nombreArchivo");

            // Contenido del archivo
            var fc = new StreamContent(File.OpenRead(file.FullName));
            fc.Headers.ContentType = new MediaTypeHeaderValue(ExtToMime(file.Extension));
            // 👉 Content-Disposition manual con filename* (UTF-8) + fallback ASCII
            var cd = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",            // el PHP standalone acepta 'file' y también 'archivo'; usa 'file' para estandarizar
                FileName = "upload.bin",  // fallback ASCII
                FileNameStar = nombreArchivo // ✅ nombre real UTF-8, p.ej. "Diseño sin título.png"
            };
            fc.Headers.ContentDisposition = cd;

            form.Add(fc); // ¡No pases el 3er parámetro aquí o se sobreescribe el header!

            using var http = new HttpClient() { Timeout = TimeSpan.FromSeconds(100) };
            using var resp = await http.PostAsync(url, form);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                new FrmAlerta($"Error HTTP {(int)resp.StatusCode}\n{body}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                return false;
            }

            // Opcional: validar el JSON {"ok":true} si tu PHP lo devuelve
            try
            {
                using var doc = JsonDocument.Parse(body);
                if (doc.RootElement.TryGetProperty("ok", out var okEl) && okEl.ValueKind == JsonValueKind.True)
                    return true;
                // si tu script devuelve {success: true}
                if (doc.RootElement.TryGetProperty("success", out var sEl) && sEl.ValueKind == JsonValueKind.True)
                    return true;
            }
            catch { /* si no es JSON, igual lo dimos como OK por el status 2xx */ }

            return true;
        }

        public async Task IngresarPatente()
        {
            string caso = txtCaso.Text;
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string tipo = comboBoxTipo.SelectedItem?.ToString();
            string anualidad = comboBoxAnualidades.SelectedItem?.ToString();


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
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;
            string erenov = null;
            string etrasp = null;
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
                        int idPatente = await patenteModel.CrearPatente(caso, expediente, nombre, estado, tipo, idTitular, idAgente, solicitud,
                            registro, folio, libro, fecha_registro, fecha_vencimiento, erenov, etrasp, int.Parse(anualidad), pct,
                            comprobante_pagos, descripcion, reivindicaciones, dibujos, resumen, documento_cesion,
                            poder_nombramiento);

                        if (idPatente > 0)
                        {
                            if (archivoSeleccionado)
                            {
                                var (okLocal, errLocal) = ValidarArchivoLocal(rutaArchivoLocal, nombreArchivo);
                                if (!okLocal)
                                {
                                    new FrmAlerta(errLocal!.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                                    return; // no continuar
                                }

                                // Validar idPatente y subir
                                if (idPatente <= 0)
                                {
                                    new FrmAlerta("ID DE PATENTE INVÁLIDO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                                    return;
                                }

                                bool exito = await SubirArchivoPorPhpAsync(idPatente);
                                if (!exito)
                                {
                                    // Si quieres, aquí podrías hacer rollback/eliminar la patente recién creada
                                    new FrmAlerta("ERROR AL SUBIR EL ARCHIVO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                                    return; // no continuar
                                }
                            }
                            else
                            {
                                // si es obligatorio subir el archivo en Registro/Concesión, corta aquí
                                new FrmAlerta("DEBE SUBIR EL TÍTULO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                                return;
                            }


                            GuardarHistorial(Convert.ToDateTime(AgregarEtapaPatente.fecha), AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones
                           , AgregarEtapaPatente.usuario, null, idPatente);



                            /* Subir archivo si fue seleccionado
                            if (archivoSeleccionado)
                            {
                                bool exito = SubirArchivoPorPhp(idPatente);
                                if (!exito)
                                {
                                    FrmAlerta alertaError = new FrmAlerta("ERROR AL SUBIR EL ARCHIVO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    alertaError.ShowDialog();
                                }
                            }*/

                            FrmAlerta alerta = new FrmAlerta("PATENTE AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
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
                        int idPatente = await patenteModel.CrearPatente(caso, expediente, nombre, estado, tipo, idTitular, idAgente, solicitud,
                            null, null, null, null, null, erenov, etrasp, int.Parse(anualidad), pct,
                            comprobante_pagos, descripcion, reivindicaciones, dibujos, resumen, documento_cesion,
                            poder_nombramiento);

                        GuardarHistorial((DateTime)AgregarEtapaPatente.fecha, AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones
                        , AgregarEtapaPatente.usuario, null, idPatente);
                        FrmAlerta alerta = new FrmAlerta("PATENTE AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        LimpiarFomulario();
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
                txtRegistro.Text = "";
                txtLibro.Text = "";
                txtFolio.Text = "";
                dateTimePFecha_Registro.Value = DateTime.Now;
                ActualizarFechaVencimiento();
                lblVencimiento.Visible = true;
                dateTimePFecha_vencimiento.Visible = true;
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel2I.Visible = true;
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 62.5f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 37.5f;
                btnAdjuntarT.Visible = true;
                //btnGuardarM.Location = new Point(197, panel2I.Location.Y + panel2I.Height + 10);
                //btnCancelarM.Location = new Point(525, panel2I.Location.Y + panel2I.Height + 10);
            }
            else
            {
                lblVencimiento.Visible = false;
                dateTimePFecha_vencimiento.Visible = false;
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel2I.Visible = false;
                tableLayoutPanel1.RowStyles[0].Height = 0;
                btnAdjuntarT.Visible = false;
                //btnGuardarM.Location = new Point(197, 1050);
                //btnCancelarM.Location = new Point(525, 1050);
            }
        }

        private void txtExpediente_TextChanged(object sender, EventArgs e)
        {
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

        private void roundedButton1_Click(object sender, EventArgs e)
        {

            FrmAgregarEtapaPatente frmAgregarEtapa = new FrmAgregarEtapaPatente();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapaPatente.etapa != "")
            {
                textBoxEstatus.Text = AgregarEtapaPatente.etapa;
                if (AgregarEtapaPatente.etapa == "Registro/concesión")
                {
                    checkBox1.Checked = true;
                    mostrarPanelRegistro("si");
                }
                else
                {
                    checkBox1.Checked = false;
                    mostrarPanelRegistro("no");
                }
            }
        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentesPatente frmMostrarAgentes = new FrmMostrarAgentesPatente();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersonaPatente.idPersonaA != 0)
            {
                txtNombreAgente.Text = SeleccionarPersonaPatente.nombre;
            }
        }

        private void roundedButton5_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesPatentes frmMostrarAgentes = new FrmMostrarTitularesPatentes();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersonaPatente.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersonaPatente.nombre;
                txtDireccionTitular.Text = SeleccionarPersonaPatente.direccion;
            }
        }

        private void datePickerFechaSolicitud_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private async void btnGuardarM_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                if(archivoSeleccionado==false && checkBox1.Checked)
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE SUBIR EL TÍTULO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
                else
                {
                    await IngresarPatente();
                }
                    
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {
            LimpiarFomulario();
        }


        private void btnCancelarM_Click_1(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {

                LimpiarFomulario();
                _form1.cargarDashboard();
            }
            else
            {
                DatosRegistro.peligro = false;
                FrmAlerta alerta = new FrmAlerta("NO SE GUARDARON LOS DATOS DE LA PATENTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                LimpiarFomulario();
                _form1.cargarDashboard();

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
        private void ResetArchivoSeleccionado()
        {
            rutaArchivoLocal = null;
            nombreArchivo = null;
            archivoSeleccionado = false;
        }
        private void btnAdjuntarT_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Seleccionar archivo para adjuntar";
                openFileDialog.Filter = "Todos los archivos (*.*)|*.*";

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                rutaArchivoLocal = openFileDialog.FileName;
                nombreArchivo = Path.GetFileName(rutaArchivoLocal);

                // 🔹 Validar nombre de archivo
                if (string.IsNullOrWhiteSpace(nombreArchivo))
                {
                    new FrmAlerta("El nombre del archivo no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                    ResetArchivoSeleccionado();
                    return;
                }

                // ❌ Evitar nombres con caracteres problemáticos
                string patronInvalido = @"[<>:""/\\|?*]";
                if (Regex.IsMatch(nombreArchivo, patronInvalido))
                {
                    new FrmAlerta("El nombre del archivo contiene caracteres no permitidos (\\ / : * ? \" < > |).",
                                  "Nombre inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
                    ResetArchivoSeleccionado();
                    return;
                }

                // 🔹 Validar tamaño máximo (20 MB)
                FileInfo fileInfo = new FileInfo(rutaArchivoLocal);
                long tamanioEnBytes = fileInfo.Length;
                long maxTamanio = 20 * 1024 * 1024; // 20 MB

                if (tamanioEnBytes > maxTamanio)
                {
                    new FrmAlerta("El archivo seleccionado supera el tamaño máximo permitido (20 MB).",
                                  "Archivo demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
                    ResetArchivoSeleccionado();
                    return;
                }

                // 🔹 Validar extensión (permitir las más comunes)
                string[] extensionesPermitidas = { ".png", ".jpg", ".jpeg", ".pdf", ".gif", ".doc", ".docx", ".xls", ".xlsx", ".txt" };
                string extension = Path.GetExtension(nombreArchivo).ToLowerInvariant();
                if (!extensionesPermitidas.Contains(extension))
                {
                    DialogResult result = MessageBox.Show(
                        $"El tipo de archivo \"{extension}\" no es común. ¿Deseas continuar de todos modos?",
                        "Tipo de archivo no reconocido",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        ResetArchivoSeleccionado();
                        return;
                    }
                }

                // ✅ Archivo validado correctamente
                archivoSeleccionado = true;
                new FrmAlerta("Archivo seleccionado correctamente.", "Archivo válido",
                              MessageBoxButtons.OK, MessageBoxIcon.Information).ShowDialog();
            }
            /*
            using var ofd = new OpenFileDialog
            {
                Filter = "Todos los archivos (*.*)|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            rutaArchivoLocal = ofd.FileName;
            nombreArchivo = Path.GetFileName(rutaArchivoLocal);

            var (ok, error) = ValidarArchivoLocal(rutaArchivoLocal, nombreArchivo);
            if (!ok)
            {
                new FrmAlerta(error!.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error).ShowDialog();
                rutaArchivoLocal = null;
                nombreArchivo = null;
                archivoSeleccionado = false;
                return;
            }

            archivoSeleccionado = true;
            new FrmAlerta("ARCHIVO SELECCIONADO", "ARCHIVO", MessageBoxButtons.OK, MessageBoxIcon.Information).ShowDialog();
            */

        }
    }
}
