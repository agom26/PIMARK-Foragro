using AccesoDatos.ServiciosEmail;
using Dominio;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun.Cache;
using Presentacion.Alertas;
using System.Text;
using System.Windows.Controls;
using Mysqlx.Datatypes;
using ClosedXML.Excel;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using Presentacion.Marcas_Nacionales;

namespace Presentacion.Vencimientos
{
    public partial class FrmVencimientos : Form
    {
        VencimientoModel vencimientoModel = new VencimientoModel();
        HistorialModel historialModel = new HistorialModel();
        HistorialPatenteModel historialPatenteModel = new HistorialPatenteModel();
        MarcaModel marcaModel = new MarcaModel();
        PatenteModel patenteModel = new PatenteModel();
        PersonaModel personaModel = new PersonaModel();
        private bool isSendingEmail = false;
        public int numRegistros = 11;
        public float escala = 0.85f;
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;
        System.Drawing.Image documento;
        byte[] defaultImage = Properties.Resources.logoImage;
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }
        public FrmVencimientos()
        {
            InitializeComponent();
            EliminarTabPage(tabPageMensajeMarca);
            EliminarTabPage(tabPageMensajePatente);
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPagePatenteDetail);
            this.Load += FrmVencimientos_Load;
            toolTip1.SetToolTip(pictureBoxInfo, "Debe usar las palabras clave SIGNO y F_VENCIMIENTO.\n" +
    "El sistema las reemplazará automáticamente al enviar el mensaje.");

        }

        private async void FrmVencimientos_Load(object sender, EventArgs e)
        {
            await Task.Run(() => vencimientoModel.EjecutarProcedimiento());
            await Task.Run(() => LoadVencimientos());

            foreach (FontFamily font in FontFamily.Families)
            {
                fontComboBox.Items.Add(font.Name);
            }

            // Poblar el ComboBox con tamaños de fuente
            for (int i = 8; i <= 72; i++)
            {
                fontSizeComboBox.Items.Add(i.ToString());
            }

            foreach (FontFamily font in FontFamily.Families)
            {
                fontComboBox2.Items.Add(font.Name);
            }

            // Poblar el ComboBox con tamaños de fuente
            for (int i = 8; i <= 72; i++)
            {
                fontSizeComboBox2.Items.Add(i.ToString());
            }

            // Seleccionar fuente y tamaño por defecto
            fontComboBox.SelectedItem = "Arial";
            fontSizeComboBox.SelectedItem = "12";
            fontComboBox2.SelectedItem = "Arial";
            fontSizeComboBox2.SelectedItem = "12";
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async Task LoadVencimientos()
        {
            totalRows = vencimientoModel.GetTotalVencimientos();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            var marcasN = await Task.Run(() => vencimientoModel.GetAllVencimientos(currentPageIndex, pageSize));
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();

                    dtgVencimientos.DataSource = marcasN;

                }));
            }

        }

        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = vencimientoModel.GetFilteredVencimientosCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = vencimientoModel.FiltrarVencimientos(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgVencimientos.DataSource = titulares;
                    if (dtgVencimientos.Columns["id"] != null)
                    {
                        dtgVencimientos.Columns["id"].Visible = false;
                    }
                    dtgVencimientos.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN VENCIMIENTOS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadVencimientos();
                }
            }
            else
            {
                await LoadVencimientos();
            }
        }


        public async Task<string> SubirLogoAsync(string rutaArchivoLocal)
        {
            string urlApi = "https://bpa.com.es/logoCorreo/subirLogo.php"; // Reemplaza por tu dominio real

            using (var client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            {
                using (var fileStream = File.OpenRead(rutaArchivoLocal))
                {
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                    content.Add(fileContent, "archivo", Path.GetFileName(rutaArchivoLocal));

                    HttpResponseMessage response = await client.PostAsync(urlApi, content);
                    string respuesta = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                        return respuesta;
                    else
                        throw new Exception($"Error al subir imagen: {response.StatusCode} - {respuesta}");
                }
            }
        }


        public async Task<string> EliminarLogoAsync()
        {
            string urlApi = "https://bpa.com.es/logoCorreo/eliminarLogo.php";
            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(new Dictionary<string, string>());
            var response = await client.PostAsync(urlApi, content);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new Exception($"[{response.StatusCode}] {json}");
            return json;
        }



        public string ConvertirRichTextBoxAHtml(System.Windows.Forms.RichTextBox richTextBox)
        {

            string logoUrl = "https://www.blita.com/hubfs/uXsa_Xqw-1.jpeg";
            string logoHtml = $"<img src='{logoUrl}' alt='Logo de la Empresa' style='max-width: 250px; height: auto; display: block; margin-top: 20px;' />";
            string rtfContent = richTextBox.Rtf;

            string htmlContent = RtfToHtml(rtfContent, richTextBox);

            // Asegúrate de reemplazar saltos de línea por <br>
            htmlContent = htmlContent.Replace("\r\n", "<br>").Replace("\n", "<br>");

            // Armar el cuerpo completo
            string finalHtml = "<html><body>" + htmlContent + "<br/><br/>" + logoHtml + "</body></html>";

            return finalHtml;
        }

        public string RtfToHtml(string rtf, System.Windows.Forms.RichTextBox richTextBox)
        {
            // Inicializamos el HTML que contendrá el texto convertido
            StringBuilder htmlContent = new StringBuilder("<html><body>");

            // Recorremos cada fragmento del texto con estilos diferentes
            for (int i = 0; i < richTextBox.TextLength; i++)
            {
                // Obtener el estilo actual de la posición
                richTextBox.Select(i, 1);

                // Obtener el color, fuente y tamaño de la letra de la selección
                Color selectionColor = richTextBox.SelectionColor;
                string hexColor = ColorTranslator.ToHtml(selectionColor);

                string selectedFont = richTextBox.SelectionFont.FontFamily.Name;
                float selectedSize = richTextBox.SelectionFont.Size;

                // Verificar si la selección está en negrita, cursiva o subrayado
                bool isBold = richTextBox.SelectionFont.Bold;
                bool isItalic = richTextBox.SelectionFont.Italic;
                bool isUnderline = richTextBox.SelectionFont.Underline;

                // Agregar las etiquetas HTML correspondientes para aplicar el estilo
                htmlContent.Append("<span style='");

                // Establecer el color, fuente y tamaño
                htmlContent.Append($"color:{hexColor}; font-family:{selectedFont}; font-size:{selectedSize}px;");

                // Agregar las etiquetas de negrita, cursiva o subrayado si están activadas
                if (isBold) htmlContent.Append(" font-weight: bold;");
                if (isItalic) htmlContent.Append(" font-style: italic;");
                if (isUnderline) htmlContent.Append(" text-decoration: underline;");

                // Cerrar el tag <span> y agregar el texto
                htmlContent.Append("'>");
                htmlContent.Append(richTextBox.SelectedText);
                htmlContent.Append("</span>");
            }

            // Finalizar el HTML
            htmlContent.Append("</body></html>");

            return htmlContent.ToString();
        }







        public async void EnviarCorreo(string subject, string receptor, string tipo, int id)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Berger Pemueller y Asociados", "avisos@bpa.com.es"));
                message.To.Add(new MailboxAddress("Destinatario", receptor));
                message.Subject = subject;
                string htmlMessage = null;

                string mensajeCuerpo = htmlMessage;


                if (tipo.Equals("patente", StringComparison.OrdinalIgnoreCase))
                {
                    if (richTextBoxMensajeP.InvokeRequired)
                    {
                        richTextBoxMensajeP.Invoke((MethodInvoker)(() =>
                        {
                            htmlMessage = ConvertirRichTextBoxAHtml(richTextBoxMensajeP);
                        }));
                    }
                    else
                    {
                        htmlMessage = ConvertirRichTextBoxAHtml(richTextBoxMensajeP);
                    }
                    mensajeCuerpo = htmlMessage
                                    ;
                }
                else if (tipo.Equals("marca", StringComparison.OrdinalIgnoreCase))
                {
                    if (richTextBoxMensajeM.InvokeRequired)
                    {
                        richTextBoxMensajeM.Invoke((MethodInvoker)(() =>
                        {
                            htmlMessage = ConvertirRichTextBoxAHtml(richTextBoxMensajeM);
                        }));
                    }
                    else
                    {
                        htmlMessage = ConvertirRichTextBoxAHtml(richTextBoxMensajeM);
                    }
                    mensajeCuerpo = htmlMessage;
                }


                message.Body = new TextPart("html") { Text = mensajeCuerpo };

                using (var client = new SmtpClient())
                {
                    client.Connect("mail.bpa.com.es", 465, true);
                    client.Authenticate("avisos@bpa.com.es", "Berger*Pemueller*2024");
                    await client.SendAsync(message);
                    client.Disconnect(true);

                    try
                    {
                        vencimientoModel.ActualizarNotificado(id, tipo);


                    }
                    catch (Exception updateEx)
                    {
                        MessageBox.Show($"Error al actualizar el estado de notificado: {updateEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = vencimientoModel.GetFilteredVencimientosCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }
        private async Task VerificarSeleccionId()
        {
            if (dtgVencimientos.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgVencimientos.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgVencimientos.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    if (dataRowView["patenteID"] != DBNull.Value)
                    {
                        int id = Convert.ToInt32(dataRowView["patenteID"]);
                        SeleccionarPatente.id = id;
                        tabControl1.SelectedTab = tabPagePatenteDetail;
                        SeleccionarMarca.idInt = 0;
                        SeleccionarMarca.idN = 0;
                    }
                    else if (dataRowView["marcaID"] != DBNull.Value)
                    {
                        int id = Convert.ToInt32(dataRowView["marcaID"]);

                        DataTable tipoMarca = await Task.Run(() => marcaModel.ObtenerTipoMarca(id));

                        if (tipoMarca.Rows.Count > 0)
                        {
                            DataRow row = tipoMarca.Rows[0];

                            if (row["tipo"] != DBNull.Value)
                            {
                                if (row["tipo"].ToString() == "internacional")
                                {
                                    SeleccionarMarca.idInt = id;
                                    SeleccionarMarca.idN = 0;
                                    SeleccionarPatente.id = 0;
                                }
                                else if (row["tipo"].ToString() == "nacional")
                                {
                                    SeleccionarMarca.idN = id;
                                    SeleccionarMarca.idInt = 0;
                                    SeleccionarPatente.id = 0;
                                }
                            }
                        }
                        tabControl1.SelectedTab = tabPageMarcaDetail;
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
        public async void Ver()
        {
            await VerificarSeleccionId();
            if (SeleccionarPatente.id > 0)
            {
                Cursor = Cursors.WaitCursor;
                await CargarDatosPatente();
                Cursor = Cursors.Default;
            }
            else if (SeleccionarMarca.idN > 0)
            {
                Cursor = Cursors.WaitCursor;
                await CargarDatosMarcaN();
                Cursor = Cursors.Default;
            }
            else if (SeleccionarMarca.idInt > 0)
            {
                Cursor = Cursors.WaitCursor;
                await CargarDatosMarcaInt();
                Cursor = Cursors.Default;
            }
        }
        private void ibtnEditar_Click_1(object sender, EventArgs e)
        {
            Ver();
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }

            tabControl1.SelectedTab = nombre;
        }
        public void mostrarPanelRegistroPatente(string isRegistrada)
        {
            if (isRegistrada == "si")
            {
                lblVencimiento.Visible = true;
                dateTimePFecha_vencimientoM.Visible = true;
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
                lblVencimiento.Visible = false;
                dateTimePFecha_vencimientoM.Visible = false;
                checkBox2.Enabled = false;
                checkBox2.Checked = false;
                panel2I.Visible = false;
                tableLayoutPanel1.RowStyles[0].Height = 0;
            }
        }

        public async Task CargarDatosPatente()
        {
            try
            {

                tabControl1.Visible = false;
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
                        SeleccionarPatente.fecha_solicitud = (DateTime)row["fecha_solicitud"];
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
                            txtRenovP.Text = SeleccionarPatente.Erenov;
                        }

                        if (row["Etrasp"] != DBNull.Value)
                        {
                            SeleccionarPatente.Etrasp = row["Etrasp"].ToString();
                            txtTraspasoP.Text = SeleccionarPatente.Etrasp;
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

                            txtNombreTitularP.Text = titular[0].nombre;
                            txtDireccionTitularP.Text = titular[0].direccion;

                        }

                        if (agente.Count > 0)
                        {
                            txtNombreAgenteP.Text = agente[0].nombre;
                            SeleccionarPatente.correoAgente = agente[0].correo;
                        }


                        // Actualizar los controles 
                        txtCaso.Text = SeleccionarPatente.caso;
                        txtExpedienteP.Text = SeleccionarPatente.expediente;
                        txtNombreP.Text = SeleccionarPatente.nombre;
                        txtEstadoP.Text = SeleccionarPatente.estado;
                        dateTPSolicitudP.Value = SeleccionarPatente.fecha_solicitud;
                        comboBoxTipoP.SelectedItem = SeleccionarPatente.tipo;
                        comboBoxAnualidadesP.SelectedItem = SeleccionarPatente.anualidades.ToString();

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

                            if (row["registro"].ToString() != null)
                            {
                                SeleccionarPatente.registro = row["registro"].ToString();
                                SeleccionarPatente.folio = row["folio"].ToString();
                                SeleccionarPatente.libro = row["libro"].ToString();
                                SeleccionarPatente.fecha_registro = Convert.ToDateTime(row["fecha_registro"]);
                                SeleccionarPatente.fecha_vencimiento = Convert.ToDateTime(row["fecha_vencimiento"]);

                                txtRegistroP.Text = SeleccionarPatente.registro;
                                txtFolioP.Text = SeleccionarPatente.folio;
                                txtTomoP.Text = SeleccionarPatente.libro;
                                dtpFRegistroP.Value = SeleccionarPatente.fecha_registro.Value;
                                dtpVencimientoP.Value = SeleccionarPatente.fecha_vencimiento.Value;
                            }
                            checkBox1.Checked = true;
                            mostrarPanelRegistroPatente("si");
                            tabControl1.Visible = true;
                        }
                        else
                        {
                            checkBox1.Checked = false;
                            mostrarPanelRegistroPatente("no");
                            tabControl1.Visible = true;
                        }


                        AnadirTabPage(tabPagePatenteDetail);
                    }
                    else
                    {
                        tabControl1.Visible = true;
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

        public void MostrarLogoEnPictureBox(byte[] logo)
        {
            if (logo != null && logo.Length > 0)
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

        public async Task CargarDatosMarcaN()
        {
            try
            {
                tabControl1.Visible = false;
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
                            txtNombreTitularM.Text = titular[0].nombre;
                        }

                        if (agente.Count > 0)
                        {
                            txtNombreAgenteM.Text = agente[0].nombre;
                            SeleccionarMarca.correoAgente = agente[0].correo;
                        }

                        if (cliente != null && cliente.Count > 0)
                        {
                            txtNombreClienteM.Text = cliente[0].nombre;
                        }

                        // Actualizar los controles 
                        txtExpedienteM.Text = SeleccionarMarca.expediente;
                        txtNombreM.Text = SeleccionarMarca.nombre;
                        txtClaseM.Text = SeleccionarMarca.clase;
                        textBoxEstatusM.Text = SeleccionarMarca.estado;
                        comboBoxSignoDistintivoM.SelectedItem = SeleccionarMarca.signoDistintivo;
                        comboBoxTipoSignoM.SelectedItem = SeleccionarMarca.tipoSigno;
                        MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                        datePickerFechaSolicitudM.Value = SeleccionarMarca.fecha_solicitud;
                        richTextBox1M.Text = SeleccionarMarca.observaciones;

                        txtERenovacionM.Text = SeleccionarMarca.erenov;
                        txtETraspasoM.Text = SeleccionarMarca.etraspaso;


                        bool contieneRegistrada = marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idN);

                        if (contieneRegistrada)
                        {
                            checkBox1.Checked = true;
                            mostrarPanelRegistro("si");
                            SeleccionarMarca.registro = row["registro"].ToString();
                            SeleccionarMarca.folio = row["folio"].ToString();
                            SeleccionarMarca.libro = row["libro"].ToString();
                            SeleccionarMarca.fechaRegistro = Convert.ToDateTime(row["fechaRegistro"]);
                            SeleccionarMarca.fechaVencimiento = Convert.ToDateTime(row["fechaVencimiento"]);

                            txtRegistroM.Text = SeleccionarMarca.registro;
                            txtFolioM.Text = SeleccionarMarca.folio;
                            txtLibroM.Text = SeleccionarMarca.libro;
                            dateTimePFecha_RegistroM.Value = SeleccionarMarca.fechaRegistro.Value;
                            dateTimePFecha_vencimientoM.Value = SeleccionarMarca.fechaVencimiento.Value;
                        }
                        else
                        {

                            checkBox1.Checked = false;
                            mostrarPanelRegistro("no");
                        }
                        AnadirTabPage(tabPageMarcaDetail);
                        tabControl1.Visible = true;
                    }
                    else
                    {
                        tabControl1.Visible = true;
                        MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                    tabControl1.Visible = true;

                }
                else
                {
                    tabControl1.Visible = true;
                    MessageBox.Show("No se encontraron detalles de la marca", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                tabControl1.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tabControl1.Visible = true;
        }

        public async Task CargarDatosMarcaInt()
        {
            try
            {
                tabControl1.Visible = false;
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
                        SeleccionarMarca.logo = row["logo"] is DBNull ? null : (byte[])row["logo"];
                        SeleccionarMarca.idPersonaTitular = Convert.ToInt32(row["idTitular"]);
                        SeleccionarMarca.idPersonaAgente = Convert.ToInt32(row["idAgente"]);
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
                        SeleccionarMarca.tiene_poder = row["tiene_poder"] != DBNull.Value ? row["tiene_poder"].ToString() : string.Empty;
                        SeleccionarMarca.pais_de_registro = row["pais_de_registro"] != DBNull.Value ? row["pais_de_registro"].ToString() : string.Empty;

                        if (row["Erenov"] != DBNull.Value)
                        {
                            SeleccionarMarca.erenov = row["Erenov"].ToString();
                            txtERenovacionM.Text = SeleccionarMarca.erenov;
                        }

                        if (row["Etrasp"] != DBNull.Value)
                        {
                            SeleccionarMarca.etraspaso = row["Etrasp"].ToString();
                            txtETraspasoM.Text = SeleccionarMarca.etraspaso;
                        }

                        checkBoxTienePoder.Checked = SeleccionarMarca.tiene_poder.Equals("si", StringComparison.OrdinalIgnoreCase);
                        int index = comboBox1.FindString(SeleccionarMarca.pais_de_registro);
                        comboBox1.SelectedIndex = index;

                        var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                        var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));

                        await Task.WhenAll(titularTask, agenteTask);

                        var titular = titularTask.Result;
                        var agente = agenteTask.Result;

                        SeleccionarPersona.idPersonaT = SeleccionarMarca.idPersonaTitular;
                        SeleccionarPersona.idPersonaA = SeleccionarMarca.idPersonaAgente;

                        if (titular.Count > 0)
                        {
                            txtNombreTitularM.Text = titular[0].nombre;
                        }

                        if (agente.Count > 0)
                        {
                            txtNombreAgenteM.Text = agente[0].nombre;
                            SeleccionarMarca.correoAgente = agente[0].correo;
                        }


                        // Actualizar los controles 
                        txtExpedienteM.Text = SeleccionarMarca.expediente;
                        txtNombreM.Text = SeleccionarMarca.nombre;
                        txtClaseM.Text = SeleccionarMarca.clase;
                        textBoxEstatusM.Text = SeleccionarMarca.estado;
                        comboBoxSignoDistintivoM.SelectedItem = SeleccionarMarca.signoDistintivo;
                        comboBoxTipoSignoM.SelectedItem = SeleccionarMarca.tipoSigno;
                        MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                        datePickerFechaSolicitudM.Value = SeleccionarMarca.fecha_solicitud;
                        richTextBox1M.Text = SeleccionarMarca.observaciones;


                        bool contieneRegistrada = marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idInt);

                        if (contieneRegistrada)
                        {

                            checkBox1.Checked = true;
                            mostrarPanelRegistro("si");
                            SeleccionarMarca.registro = row["registro"].ToString();
                            SeleccionarMarca.folio = row["folio"].ToString();
                            SeleccionarMarca.libro = row["libro"].ToString();
                            SeleccionarMarca.fechaRegistro = Convert.ToDateTime(row["fechaRegistro"]);
                            SeleccionarMarca.fechaVencimiento = Convert.ToDateTime(row["fechaVencimiento"]);

                            txtRegistroM.Text = SeleccionarMarca.registro;
                            txtFolioM.Text = SeleccionarMarca.folio;
                            txtLibroM.Text = SeleccionarMarca.libro;
                            dateTimePFecha_RegistroM.Value = SeleccionarMarca.fechaRegistro.Value;
                            dateTimePFecha_vencimientoM.Value = SeleccionarMarca.fechaVencimiento.Value;
                        }
                        else
                        {
                            checkBox1.Checked = false;
                            mostrarPanelRegistro("no");
                        }

                        AnadirTabPage(tabPageMarcaDetail);
                        tabControl1.Visible = true;
                    }
                    else
                    {
                        tabControl1.Visible = true;
                        MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    tabControl1.Visible = true;

                }
                else
                {
                    MessageBox.Show("No se encontraron detalles de la marca", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                tabControl1.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            tabControl1.Visible = true;
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }

        private void tabPageVencimientosList_Click(object sender, EventArgs e)
        {

        }

        private void dtgVencimientos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Ver();
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageVencimientosList);
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPagePatenteDetail);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            if (tabControl1.SelectedTab == tabPageVencimientosList)
            {
                EliminarTabPage(tabPagePatenteDetail);
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageMensajePatente);
                EliminarTabPage(tabPageMensajeMarca);
            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                EliminarTabPage(tabPagePatenteDetail);
                EliminarTabPage(tabPageMensajePatente);
                EliminarTabPage(tabPageMensajeMarca);
            }
            else if (tabControl1.SelectedTab == tabPagePatenteDetail)
            {
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageMensajeMarca);
                EliminarTabPage(tabPageMensajePatente);
            }*/
        }

        private async void iconButton10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            tabControl1.Visible = false;

            await Task.Yield();

            AnadirTabPage(tabPageMensajePatente);

            await CargarCorreoPatente();

            tabControl1.Visible = true;

            Cursor = Cursors.Default;



        }

        private void checkBoxPCT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void iconButton11_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageVencimientosList);
            EliminarTabPage(tabPagePatenteDetail);
            await LoadVencimientos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBoxMensajeP.SelectionFont != null)
            {
                Font currentFont = richTextBoxMensajeP.SelectionFont;
                FontStyle newFontStyle = currentFont.Style ^ FontStyle.Italic;
                richTextBoxMensajeP.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void boldButton_Click(object sender, EventArgs e)
        {
            if (richTextBoxMensajeP.SelectionFont != null)
            {
                Font currentFont = richTextBoxMensajeP.SelectionFont;
                FontStyle newFontStyle = currentFont.Style ^ FontStyle.Bold;
                richTextBoxMensajeP.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void underlineButton_Click(object sender, EventArgs e)
        {
            if (richTextBoxMensajeP.SelectionFont != null)
            {
                Font currentFont = richTextBoxMensajeP.SelectionFont;
                FontStyle newFontStyle = currentFont.Style ^ FontStyle.Underline;
                richTextBoxMensajeP.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            // Abrir un cuadro de diálogo para elegir el color
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBoxMensajeP.SelectionColor = colorDialog.Color;
            }
        }

        private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFont = fontComboBox.SelectedItem.ToString();
            richTextBoxMensajeP.SelectionFont = new Font(selectedFont, richTextBoxMensajeP.SelectionFont.Size);
        }

        private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            float selectedSize = float.Parse(fontSizeComboBox.SelectedItem.ToString());
            richTextBoxMensajeP.SelectionFont = new Font(richTextBoxMensajeP.SelectionFont.FontFamily, selectedSize);
        }

        public async Task EditarTexto(string tipo, string mensaje)
        {
            vencimientoModel.EditarTextoRtf(tipo, mensaje);
        }




        public async Task CargarCorreoPatente()
        {
            string mensajeMarca = await Task.Run(() => vencimientoModel.ObtenerTextoRtfPorTipo("patente"));
            richTextBoxMensajeP.Rtf = mensajeMarca;
        }

        public async Task CargarCorreoMarca()
        {
            string mensajeMarca = await Task.Run(() => vencimientoModel.ObtenerTextoRtfPorTipo("marca"));
            richTextBoxMensajeM.Rtf = mensajeMarca;
        }


        private async void iconButton12_Click(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor;

            tabControl1.Visible = false;

            await Task.Yield();

            AnadirTabPage(tabPageMensajeMarca);
            string urlLogo = "https://bpa.com.es/logoCorreo/logoCorreo/logoCorreo.png";
            await MostrarLogoDesdeUrlAsync(urlLogo);

            await CargarCorreoMarca();

            tabControl1.Visible = true;

            Cursor = Cursors.Default;
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            string asunto = txtAsuntoP.Text;
            string mensaje = richTextBoxMensajeP.Rtf;
            string receptor = SeleccionarPatente.correoAgente;
            int patenteId = SeleccionarPatente.id;
            string nombre = SeleccionarPatente.nombre;
            DateTime fechaV = SeleccionarPatente.fecha_vencimiento.Value;

            Convertir(nombre, fechaV);

            if (richTextBoxMensajeP.Text != "" && receptor != "" && txtAsuntoP.Text != "")
            {
                try
                {
                    if (isSendingEmail) return;

                    isSendingEmail = true;

                    try
                    {
                        iconButton1.Enabled = false;
                        await Task.Run(() => EnviarCorreo(asunto, receptor, "patente", patenteId));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al enviar el correo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        iconButton1.Enabled = true;
                        AnadirTabPage(tabPageVencimientosList);
                        await LoadVencimientos();
                        EliminarTabPage(tabPageMensajeMarca);
                        EliminarTabPage(tabPageMarcaDetail);
                    }
                    isSendingEmail = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE REDACTAR EL ASUNTO Y UN MENSAJE PARA PODER NOTIFICAR",
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }


        }
        public void Convertir(string nombre, DateTime fechaV)
        {
            string mensaje = richTextBoxMensajeP.Rtf;

            if (richTextBoxMensajeP.Text.Contains("SIGNO") || richTextBoxMensajeP.Text.Contains("F_VENCIMIENTO"))
            {
                mensaje = mensaje.Replace("SIGNO", nombre);
                mensaje = mensaje.Replace("F_VENCIMIENTO", fechaV.ToString("dd/MM/yyyy")); // Si quieres un formato específico para la fecha
                richTextBoxMensajeP.Rtf = mensaje;
            }
        }

        public void ConvertirMarca(string nombre, DateTime fechaV)
        {
            string mensaje = richTextBoxMensajeM.Rtf;

            if (richTextBoxMensajeM.Text.Contains("SIGNO") || richTextBoxMensajeM.Text.Contains("F_VENCIMIENTO"))
            {
                mensaje = mensaje.Replace("SIGNO", nombre);
                mensaje = mensaje.Replace("F_VENCIMIENTO", fechaV.ToString("dd/MM/yyyy")); // Si quieres un formato específico para la fecha
                richTextBoxMensajeM.Rtf = mensaje;
            }
        }
        private void iconButton3_Click(object sender, EventArgs e)
        {

            Convertir(SeleccionarPatente.nombre, (DateTime)SeleccionarPatente.fecha_vencimiento);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            string mensaje = richTextBoxMensajeP.Rtf;
            if (mensaje != "" && richTextBoxMensajeP.Text.Contains("SIGNO") && richTextBoxMensajeP.Text.Contains("F_VENCIMIENTO"))
            {
                try
                {
                    vencimientoModel.EditarTextoRtf("patente", mensaje);
                    FrmAlerta alerta = new FrmAlerta("MENSAJE MODIFICADO PARA PATENTE", "ÉXITO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    alerta.ShowDialog();

                }
                catch (Exception ex)
                {
                    FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(),
                  "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("EL MENSAJE DEBE CONTENER LAS PALABRAS CLAVE",
                  "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            string asunto = txtAsuntoM.Text;
            string mensaje = richTextBoxMensajeM.Rtf;
            string receptor = SeleccionarMarca.correoAgente;
            int marcaId = 0;

            if (mensaje != "" && !richTextBoxMensajeM.Text.Contains("SIGNO") && !richTextBoxMensajeM.Text.Contains("F_VENCIMIENTO"))
            {
                FrmAlerta alerta = new FrmAlerta("EL MENSAJE DEBE CONTENER LAS PALABRAS CLAVE",
                     "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

            if (SeleccionarMarca.idN > 0)
            {
                marcaId = SeleccionarMarca.idN;
            }
            else if (SeleccionarMarca.idInt > 0)
            {
                marcaId = SeleccionarMarca.idInt;
            }


            string nombre = SeleccionarMarca.nombre;
            DateTime fechaV = SeleccionarMarca.fechaVencimiento.Value;

            ConvertirMarca(nombre, fechaV);

            if (richTextBoxMensajeM.Text != "" && receptor != "" && txtAsuntoM.Text != "" && marcaId > 0)
            {

                try
                {
                    if (isSendingEmail) return;

                    isSendingEmail = true;

                    try
                    {
                        iconButton1.Enabled = false;
                        await Task.Run(() => EnviarCorreo(asunto, receptor, "marca", marcaId));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al enviar el correo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        iconButton1.Enabled = true;
                        AnadirTabPage(tabPageVencimientosList);
                        await LoadVencimientos();
                        FrmAlerta alerta = new FrmAlerta("CORREO ENVIADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        EliminarTabPage(tabPageMensajeMarca);
                    }
                    isSendingEmail = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE REDACTAR EL ASUNTO Y UN MENSAJE PARA PODER NOTIFICAR",
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void iconButton3_Click_1(object sender, EventArgs e)
        {
            string mensaje = richTextBoxMensajeM.Rtf;
            if (mensaje != "" && richTextBoxMensajeM.Text.Contains("SIGNO") && richTextBoxMensajeM.Text.Contains("F_VENCIMIENTO"))
            {
                try
                {
                    vencimientoModel.EditarTextoRtf("marca", mensaje);
                    FrmAlerta alerta = new FrmAlerta("MENSAJE MODIFICADO PARA MARCA", "ÉXITO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    alerta.ShowDialog();

                }
                catch (Exception ex)
                {
                    FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(),
                  "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("EL MENSAJE DEBE CONTENER LAS PALABRAS CLAVE",
                  "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageMensajeMarca);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPagePatenteDetail);
            EliminarTabPage(tabPageMensajePatente);

        }

        private void ActualizarEstadoBotonCursiva()
        {
            if (richTextBoxMensajeM.SelectionFont != null)
            {
                if (richTextBoxMensajeM.SelectionFont.Italic)
                {
                    button3.BackColor = Color.LightGray; // activo
                }
                else
                {
                    button3.BackColor = Color.FromArgb(222, 227, 234); // normal
                }
            }
        }

        private void ActualizarEstadoBotonSubrayado()
        {
            if (richTextBoxMensajeM.SelectionFont != null)
            {
                if (richTextBoxMensajeM.SelectionFont.Underline)
                {
                    button2.BackColor = Color.LightGray; // activo
                }
                else
                {
                    button2.BackColor = Color.FromArgb(222, 227, 234); // normal
                }
            }
        }


        private void ActualizarEstadoBotonNegrita()
        {
            if (richTextBoxMensajeM.SelectionFont != null)
            {
                if (richTextBoxMensajeM.SelectionFont.Bold)
                {
                    button4.BackColor = Color.LightGray; // se ve "activo"
                }
                else
                {
                    button4.BackColor = Color.FromArgb(222, 227, 234);
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {

            if (richTextBoxMensajeM.SelectionFont != null)
            {
                Font currentFont = richTextBoxMensajeM.SelectionFont;
                FontStyle newFontStyle;

                // Alternar negrita
                if (currentFont.Bold)
                {
                    newFontStyle = currentFont.Style & ~FontStyle.Bold; // quitar negrita
                }
                else
                {
                    newFontStyle = currentFont.Style | FontStyle.Bold; // poner negrita
                }

                richTextBoxMensajeM.SelectionFont = new Font(currentFont, newFontStyle);
                ActualizarEstadoBotonNegrita(); // actualizar visualmente el botón
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBoxMensajeM.SelectionFont != null)
            {
                Font currentFont = richTextBoxMensajeM.SelectionFont;
                FontStyle newFontStyle = currentFont.Style ^ FontStyle.Italic;
                richTextBoxMensajeM.SelectionFont = new Font(currentFont, newFontStyle);
                ActualizarEstadoBotonCursiva(); // actualiza visualmente el botón
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (richTextBoxMensajeM.SelectionFont != null)
            {
                Font currentFont = richTextBoxMensajeM.SelectionFont;
                FontStyle newFontStyle = currentFont.Style ^ FontStyle.Underline;
                richTextBoxMensajeM.SelectionFont = new Font(currentFont, newFontStyle);
                ActualizarEstadoBotonSubrayado(); // actualiza visualmente el botón
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBoxMensajeM.SelectionFont != null)
            {
                string selectedFont = fontComboBox2.SelectedItem.ToString();
                Font currentFont = richTextBoxMensajeM.SelectionFont;

                richTextBoxMensajeM.SelectionFont = new Font(
                    selectedFont,
                    currentFont.Size,
                    currentFont.Style
                );

                ActualizarEstadoBotonNegrita(); // opcional si quieres que el botón se actualice
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBoxMensajeM.SelectionFont != null)
            {
                float selectedSize = float.Parse(fontSizeComboBox2.SelectedItem.ToString());
                Font currentFont = richTextBoxMensajeM.SelectionFont;

                richTextBoxMensajeM.SelectionFont = new Font(
                    currentFont.FontFamily,
                    selectedSize,
                    currentFont.Style
                );

                ActualizarEstadoBotonNegrita(); // opcional
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Abrir un cuadro de diálogo para elegir el color
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBoxMensajeM.SelectionColor = colorDialog.Color;
            }
        }

        public void ExportarDataTableAExcel(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }
            string nombre = "Próximos vencimientos-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
            {
                Title = "Guardar archivo Excel",
                Filter = "Archivos Excel (*.xlsx)|*.xlsx",
                FileName = nombre + ".xlsx",
                DefaultExt = "xlsx",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string tempLogoPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_logo.png");

                    // Guardar el recurso de imagen en un archivo temporal
                    Properties.Resources.logoBPA2.Save(tempLogoPath);

                    using (var workbook = new XLWorkbook())
                    {
                        // Crear la hoja de trabajo
                        var worksheet = workbook.Worksheets.Add("PRÓXIMOS VENCIMIENTOS");

                        // Fecha actual en el formato deseado
                        string fecha = DateTime.Now.ToString("dd-MM-yyyy-HH-mm");

                        // Insertar el título "Próximos vencimientos" en la celda A1
                        worksheet.Cell(3, 5).Value = "PRÓXIMOS VENCIMIENTOS";
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Style.Font.Underline = XLFontUnderlineValues.Single;
                        worksheet.Cell(3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;  // Centrar el título

                        // Insertar la fecha debajo del título (en la celda A2)
                        worksheet.Cell(4, 5).Value = "Fecha: " + fecha;
                        worksheet.Cell(4, 5).Style.Font.Italic = true;
                        worksheet.Cell(4, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;  // Centrar la fecha

                        // Ajustar el ancho de la columna A (para que todo el texto se vea bien centrado)
                        worksheet.Column(1).AdjustToContents();

                        // Agregar logo después del título y la fecha (en la celda A3)
                        if (System.IO.File.Exists(tempLogoPath))
                        {
                            var image = worksheet.AddPicture(tempLogoPath)
                                .MoveTo(worksheet.Cell(3, 1)) // Posicionar el logo en la celda 3, 1
                                .Scale(0.5); // Ajustar tamaño
                        }

                        // Insertar tabla después del logo (a partir de la fila 10)
                        int startRow = 10; // Ajustar según el espacio requerido
                        worksheet.Cell(startRow, 1).InsertTable(dataTable);

                        // Ajustar el ancho de las columnas
                        worksheet.Columns().AdjustToContents();

                        // Guardar archivo
                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    // Eliminar archivo temporal
                    if (System.IO.File.Exists(tempLogoPath))
                        System.IO.File.Delete(tempLogoPath);

                    // Mostrar mensaje de éxito
                    FrmAlerta alerta = new FrmAlerta("ARCHIVO GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el archivo: {ex.Message}");
                }
            }
        }
        /*
        private async void CrearPdfDesdeHtmlConLogoYDataTable(DataTable dt, int registrosPagina, float escalas)
        {
            // Ruta al ejecutable de Chrome en tu sistema
            string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"; // Cambia la ruta según tu instalación

            string nombre = "Próximos vencimientos-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");

            // Abre un SaveFileDialog para que el usuario seleccione la ruta de guardado
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = nombre + ".pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lanza el navegador Chrome
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,  // Ejecutar en modo headless (sin interfaz gráfica)
                    ExecutablePath = chromePath // Usa Google Chrome en lugar de Chromium
                });

                // Crea una nueva página
                var page = await browser.NewPageAsync();

                // Límite de registros por página (12 registros por página)
                int registrosPorPagina = registrosPagina;
                int totalPaginas = (int)Math.Ceiling((double)dt.Rows.Count / registrosPorPagina);

                // Crear el contenido HTML completo para todo el PDF
                string fullHtmlContent = "";

                // Recorrer las páginas y generar el contenido HTML para cada una
                for (int pagina = 0; pagina < totalPaginas; pagina++)
                {
                    // Crear el contenido de la tabla para la página actual
                    string tableContent = "";
                    int startRecord = pagina * registrosPorPagina;
                    int endRecord = Math.Min((pagina + 1) * registrosPorPagina, dt.Rows.Count);

                    // Crear las filas para la tabla
                    for (int i = startRecord; i < endRecord; i++)
                    {
                        DataRow row = dt.Rows[i];
                        tableContent += "<tr>";

                        foreach (DataColumn column in dt.Columns)
                        {
                            // Verificar si la columna debe alinearse a la derecha
                            string alignStyle = (column.ColumnName == "REGISTRO" || column.ColumnName == "FOLIO" || column.ColumnName == "TOMO" || column.ColumnName == "CLASE")
                                ? "style='padding: 8px; text-align: right; border: 1px solid #ddd;'"
                                : (column.ColumnName == "NOTIFICADO"
                                    ? "style='padding: 8px; text-align: center; border: 1px solid #ddd;'"
                                    : "style='padding: 8px; text-align: left; border: 1px solid #ddd;'");

                            // Agregar la celda con el estilo correspondiente
                            tableContent += $"<td {alignStyle}>{row[column]}</td>";
                        }

                        tableContent += "</tr>";
                    }

                    // Generar los encabezados de la tabla dinámicamente basados en las columnas del DataTable
                    string headers = "";
                    foreach (DataColumn column in dt.Columns)
                    {
                        headers += $"<th style='padding: 8px; text-align: left; border: 1px solid #ddd; background-color: #f2f2f2; font-weight: bold;'>{column.ColumnName}</th>";
                    }

                    // HTML con el logo y el título "Reportes" en el header
                    fullHtmlContent += $@"
                     <html>
                         <head>
                             <style>
                                 body {{
                                     font-family: Arial, sans-serif;
                                 }}
                                 table {{ border-collapse: collapse; width: 100%; }}
                                 th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
                                 th {{ background-color: #f2f2f2; font-weight: bold; }}
                                 img {{
                                     width: 200px; // Tamaño del logo 
                                     height: auto; // Altura automática 
                                 }}
                                 @page {{
                                     size: legal landscape; // Configura tamaño legal y orientación horizontal 
                                     margin: 20mm;
                                 }}
                                 table {{
                                     page-break-inside: auto;
                                 }}
                                 tr {{
                                     page-break-inside: avoid;
                                 }}
                                 td {{
                                     page-break-before: auto;
                                 }}
                                 .footer {{
                                     text-align: center;
                                     position: fixed;
                                     bottom: 10mm;
                                     left: 0;
                                     right: 0;
                                     font-size: 10px;
                                 }}
                                 .header {{
                                     text-align: center;
                                     font-size: 20px;
                                     font-weight: bold;
                                     margin-bottom: 10px;
                                 }}
                             </style>
                         </head>
                         <body>
                             <div class='header'>
                                 PRÓXIMOS VENCIMIENTOS
                             </div>
                             <div class='fecha'>
                                 <center>
                                 Fecha: {DateTime.Now.ToString("dd-MM-yyyy HH:mm")}
                                 </center>
                             </div>
                             <img src='https://bergerpemueller.com/wp-content/uploads/2024/02/LogoBPA-e1709094810910.jpg' /> <!-- Aquí el logo -->
                             <table>
                                 <thead>
                                     <tr>
                                         {headers} <!-- Encabezados generados dinámicamente -->
                                     </tr>
                                 </thead>
                                 <tbody>
                                     {tableContent} <!-- Las filas generadas dinámicamente -->
                                 </tbody>
                             </table>
                             {(pagina < totalPaginas - 1 ? "<div style='page-break-before: always;'></div>" : "")} <!-- No agregar salto de página al final -->
                         </body>
                     </html>";
                }


                // Establecer el contenido HTML completo para el PDF
                await page.SetContentAsync(fullHtmlContent);

                // Ruta para guardar el PDF
                string pdfFilePath = saveFileDialog.FileName;

                // Generar el PDF para todo el contenido
                await page.PdfAsync(pdfFilePath, new PdfOptions
                {
                    Format = PaperFormat.Legal,      // Tamaño oficio
                    PrintBackground = true,         // Incluir fondo
                    Landscape = true,               // Orientación horizontal
                    Scale = (Decimal)escalas           // Reducir la escala para ajustar mejor
                });

                // Cerrar el navegador
                await browser.CloseAsync();

                // Confirmar que el PDF se ha generado
                FrmAlerta alerta = new FrmAlerta("PDF GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
                //MessageBox.Show("PDF generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ NINGUNA RUTA PARA GUARDAR EL PDF", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("No se seleccionó ninguna ruta para guardar el PDF.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
*/

        private async void CrearPdfDesdeHtmlConLogoYDataTable(DataTable dt, int registrosPagina, float escalas)
        {
            // Buscar la ruta de Chrome automáticamente
            string chromePath = "chrome"; // Intentará usar Chrome desde PATH

            string[] possiblePaths = {
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Google\\Chrome\\Application\\chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google\\Chrome\\Application\\chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\Application\\chrome.exe")
    };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    chromePath = path;
                    break;
                }
            }

            string nombre = "Próximos vencimientos-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");

            // Abre un SaveFileDialog para que el usuario seleccione la ruta de guardado
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = nombre + ".pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lanza el navegador Chrome
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = chromePath // Usa la ruta encontrada o la predeterminada
                });

                // Crea una nueva página
                var page = await browser.NewPageAsync();

                // Límite de registros por página
                int totalPaginas = (int)Math.Ceiling((double)dt.Rows.Count / registrosPagina);

                string fullHtmlContent = "";

                for (int pagina = 0; pagina < totalPaginas; pagina++)
                {
                    string tableContent = "";
                    int startRecord = pagina * registrosPagina;
                    int endRecord = Math.Min((pagina + 1) * registrosPagina, dt.Rows.Count);

                    for (int i = startRecord; i < endRecord; i++)
                    {
                        DataRow row = dt.Rows[i];
                        tableContent += "<tr>";

                        foreach (DataColumn column in dt.Columns)
                        {
                            string alignStyle = (column.ColumnName == "REGISTRO" || column.ColumnName == "FOLIO" || column.ColumnName == "TOMO" || column.ColumnName == "CLASE")
                                ? "style='padding: 8px; text-align: right; border: 1px solid #ddd;'"
                                : (column.ColumnName == "NOTIFICADO"
                                    ? "style='padding: 8px; text-align: center; border: 1px solid #ddd;'"
                                    : "style='padding: 8px; text-align: left; border: 1px solid #ddd;'");


                            object cellValue = row[column];
                            if (cellValue is DateTime dateValue)
                            {
                                cellValue = dateValue.ToString("dd/MM/yyyy"); // Cambia el formato según necesites
                            }

                            tableContent += $"<td {alignStyle}>{cellValue}</td>";
                        }
                        tableContent += "</tr>";
                    }

                    string headers = "";
                    foreach (DataColumn column in dt.Columns)
                    {
                        headers += $"<th style='padding: 8px; text-align: left; border: 1px solid #ddd; background-color: #f2f2f2; font-weight: bold;'>{column.ColumnName}</th>";
                    }
                    string base64Logo;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Properties.Resources.logoBPA2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageBytes = ms.ToArray();
                        base64Logo = Convert.ToBase64String(imageBytes);
                    }

                    string imageHtml = $"<img src='data:image/png;base64,{base64Logo}' />";

                    fullHtmlContent += $@"
             <html>
                 <head>
                     <style>
                         body {{ font-family: Arial, sans-serif; }}
                         table {{ border-collapse: collapse; width: 100%; }}
                         th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
                         th {{ background-color: #f2f2f2; font-weight: bold; }}
                         img {{ width: 200px; height: auto; }}
                         @page {{ size: legal landscape; margin: 20mm; }}
                         table {{ page-break-inside: auto; }}
                         tr {{ page-break-inside: avoid; }}
                         td {{ page-break-before: auto; }}
                         .footer {{ text-align: center; position: fixed; bottom: 10mm; left: 0; right: 0; font-size: 10px; }}
                         .header {{ text-align: center; font-size: 20px; font-weight: bold; margin-bottom: 10px; }}
                     </style>
                 </head>
                 <body>
                     <div class='header'>PRÓXIMOS VENCIMIENTOS</div>
                     <div class='fecha'><center>Fecha: {DateTime.Now.ToString("dd-MM-yyyy HH:mm")}</center></div>
                    {imageHtml}
                     <table>
                         <thead>
                             <tr>{headers}</tr>
                         </thead>
                         <tbody>{tableContent}</tbody>
                     </table>
                     {(pagina < totalPaginas - 1 ? "<div style='page-break-before: always;'></div>" : "")}
                 </body>
             </html>";
                }

                await page.SetContentAsync(fullHtmlContent);

                // Generar el PDF
                string pdfFilePath = saveFileDialog.FileName;
                await page.PdfAsync(pdfFilePath, new PdfOptions
                {
                    Format = PaperFormat.Legal,
                    PrintBackground = true,
                    Landscape = true,
                    Scale = (Decimal)escalas
                });

                // Cerrar el navegador
                await browser.CloseAsync();

                FrmAlerta alerta = new FrmAlerta("PDF GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ NINGUNA RUTA PARA GUARDAR EL PDF", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        private async void roundedButton19_Click(object sender, EventArgs e)
        {
            DataTable datos = null;
            if (txtBuscar.Text != "")
            {
                datos = await Task.Run(() => vencimientoModel.ObtenerTodosLosVencimientosFiltradosReporte(txtBuscar.Text));
            }
            else
            {
                datos = await Task.Run(() => vencimientoModel.ObtenerVencimientos());
            }


            if (datos != null)
            {
                CrearPdfDesdeHtmlConLogoYDataTable(datos, numRegistros, escala);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        private async void roundedButton11_Click(object sender, EventArgs e)
        {
            DataTable datos;

            if (txtBuscar.Text != "")
            {
                datos = await Task.Run(() => vencimientoModel.ObtenerTodosLosVencimientosFiltradosReporte(txtBuscar.Text));
            }
            else
            {
                datos = await Task.Run(() => vencimientoModel.ObtenerVencimientos());
            }

            if (datos != null)
            {
                ExportarDataTableAExcel(datos);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para exportar.");
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaRegistradaV frmAgregarEtapaRegistradaV = new FrmAgregarEtapaRegistradaV();
            frmAgregarEtapaRegistradaV.ShowDialog();


        }

        private void ActualizarEstadoMarca(int idMarca, DateTime fechaAbandono, string justificacion, string usuarioAbandono)
        {
            // Actualizar el estado y la justificación de la marca
            historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", justificacion, usuarioAbandono, "TRÁMITE");
        }

        private void ActualizarEstadoPatente(int idPatente, DateTime fechaAbandono, string justificacion, string usuarioAbandono)
        {
            // Actualizar el estado y la justificación de la patente
            historialPatenteModel.CrearHistorialPatente(fechaAbandono, "Abandono", justificacion, usuarioAbandono, null, idPatente);
        }

        private void ActualizarEstadoMarca2(int idMarca, DateTime fechaAbandono, string estado, string anotaciones, string usuario)
        {
            // Actualizar el estado y la justificación de la marca
            historialModel.GuardarEtapa(idMarca, fechaAbandono, estado, anotaciones, usuario, "TRÁMITE");
        }

        private void ActualizarEstadoPatente2(int idPatente, DateTime fechaAbandono, string estado, string anotaciones, string usuario)
        {
            // Actualizar el estado y la justificación de la patente
            historialPatenteModel.CrearHistorialPatente(fechaAbandono, estado, anotaciones, usuario, null, idPatente);
        }

        private void MostrarAlerta(string mensaje)
        {
            // Mostrar una alerta genérica
            FrmAlerta alerta = new FrmAlerta(mensaje, "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            alerta.ShowDialog();
        }

        private async void iconButton9_Click(object sender, EventArgs e)
        {
            using (FrmJustificacion justificacionForm = new FrmJustificacion())
            {
                // Mostrar formulario y obtener datos
                if (justificacionForm.ShowDialog() == DialogResult.OK)
                {
                    string justificacion = justificacionForm.Justificacion;
                    DateTime fechaAbandono = justificacionForm.fecha;
                    string usuarioAbandono = justificacionForm.usuarioAbandono;
                    string textoJustificado = "";

                    string fechaSinHora = fechaAbandono.ToShortDateString();
                    string formato = fechaSinHora + " " + "Abandono";
                    if (justificacion.Contains(formato))
                    {
                        textoJustificado = justificacion;
                    }
                    else
                    {
                        textoJustificado = formato + " " + justificacion;
                    }

                    try
                    {
                        int idMarca = 0;
                        int idPatente = 0;

                        // Verificar el tipo de entidad seleccionada (marca o patente)
                        if (SeleccionarMarca.idInt != 0)
                        {
                            idMarca = SeleccionarMarca.idInt;
                            ActualizarEstadoMarca(idMarca, fechaAbandono, textoJustificado, usuarioAbandono);
                            MostrarAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA");


                        }
                        else if (SeleccionarMarca.idN != 0)
                        {
                            idMarca = SeleccionarMarca.idN;
                            ActualizarEstadoMarca(idMarca, fechaAbandono, textoJustificado, usuarioAbandono);
                            MostrarAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA");
                        }
                        else if (SeleccionarPatente.id != 0)
                        {
                            idPatente = SeleccionarPatente.id;
                            ActualizarEstadoPatente(idPatente, fechaAbandono, textoJustificado, usuarioAbandono);
                            MostrarAlerta("LA PATENTE HA SIDO MARCADA COMO ABANDONADA");
                        }

                        AnadirTabPage(tabPageVencimientosList);
                        EliminarTabPage(tabPageMarcaDetail);
                        EliminarTabPage(tabPagePatenteDetail);
                        await LoadVencimientos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private async void EditarPatente()
        {

        }
        private async void iconButton8_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaRegistradaV frmAgregarEtapaRegistradaV = new FrmAgregarEtapaRegistradaV();
            frmAgregarEtapaRegistradaV.ShowDialog();

            if (AgregarEtapa.numExpediente != "" && AgregarEtapa.etapa == "Trámite de renovación")
            {
                try
                {
                    int idMarca = 0;
                    int idPatente = 0;
                    if (SeleccionarMarca.idInt != 0)
                    {
                        idMarca = SeleccionarMarca.idInt;
                        marcaModel.ActualizarExpedienteMarca(idMarca, AgregarEtapa.numExpediente, (DateTime)AgregarEtapa.fecha,
                            AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                        MostrarAlerta("LA MARCA HA SIDO ENVIADA A RENOVACIÓN");
                        AnadirTabPage(tabPageVencimientosList);
                        EliminarTabPage(tabPageMarcaDetail);
                    }
                    else if (SeleccionarMarca.idN != 0)
                    {
                        idMarca = SeleccionarMarca.idN;
                        marcaModel.ActualizarExpedienteMarca(idMarca, AgregarEtapa.numExpediente, (DateTime)AgregarEtapa.fecha,
                            AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                        MostrarAlerta("LA MARCA HA SIDO ENVIADA A RENOVACIÓN");
                        AnadirTabPage(tabPageVencimientosList);
                        EliminarTabPage(tabPageMarcaDetail);
                    }
                    else if (SeleccionarPatente.id != 0)
                    {
                        idPatente = SeleccionarPatente.id;
                        ActualizarEstadoPatente2(idPatente, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                        MostrarAlerta("LA PATENTE HA SIDO ENVIADA A RENOVACIÓN");
                        AnadirTabPage(tabPageVencimientosList);
                        EliminarTabPage(tabPagePatenteDetail);
                    }

                    await LoadVencimientos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private async void iconButton14_Click(object sender, EventArgs e)
        {
            using (FrmJustificacion justificacionForm = new FrmJustificacion())
            {
                // Mostrar formulario y obtener datos
                if (justificacionForm.ShowDialog() == DialogResult.OK)
                {
                    string justificacion = justificacionForm.Justificacion;
                    DateTime fechaAbandono = justificacionForm.fecha;
                    string usuarioAbandono = justificacionForm.usuarioAbandono;
                    string textoJustificado = "";

                    string fechaSinHora = fechaAbandono.ToShortDateString();
                    string formato = fechaSinHora + " " + "Abandono";
                    if (justificacion.Contains(formato))
                    {
                        textoJustificado = justificacion;
                    }
                    else
                    {
                        textoJustificado = formato + " " + justificacion;
                    }

                    try
                    {
                        int idMarca = 0;
                        int idPatente = 0;

                        // Verificar el tipo de entidad seleccionada (marca o patente)
                        if (SeleccionarMarca.idInt != 0)
                        {
                            idMarca = SeleccionarMarca.idInt;
                            ActualizarEstadoMarca(idMarca, fechaAbandono, textoJustificado, usuarioAbandono);
                            MostrarAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA");
                        }
                        else if (SeleccionarMarca.idN != 0)
                        {
                            idMarca = SeleccionarMarca.idN;
                            ActualizarEstadoMarca(idMarca, fechaAbandono, textoJustificado, usuarioAbandono);
                            MostrarAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA");
                        }
                        else if (SeleccionarPatente.id != 0)
                        {
                            idPatente = SeleccionarPatente.id;
                            ActualizarEstadoPatente(idPatente, fechaAbandono, textoJustificado, usuarioAbandono);
                            MostrarAlerta("LA PATENTE HA SIDO MARCADA COMO ABANDONADA");
                        }

                        AnadirTabPage(tabPageVencimientosList);
                        EliminarTabPage(tabPageMarcaDetail);
                        EliminarTabPage(tabPagePatenteDetail);
                        await LoadVencimientos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private async void iconButton15_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaRegistradaV frmAgregarEtapaRegistradaV = new FrmAgregarEtapaRegistradaV();
            frmAgregarEtapaRegistradaV.ShowDialog();

            if (AgregarEtapa.numExpediente != "" && AgregarEtapa.etapa == "Trámite de renovación")
            {
                try
                {
                    int idMarca = 0;
                    int idPatente = 0;
                    if (SeleccionarMarca.idInt != 0)
                    {
                        idMarca = SeleccionarMarca.idInt;
                        marcaModel.ActualizarExpedienteMarca(idMarca, AgregarEtapa.numExpediente, (DateTime)AgregarEtapa.fecha,
                            AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                        MostrarAlerta("LA MARCA HA SIDO ENVIADA A RENOVACIÓN");
                        AnadirTabPage(tabPageVencimientosList);
                        EliminarTabPage(tabPageMarcaDetail);
                    }
                    else if (SeleccionarMarca.idN != 0)
                    {
                        idMarca = SeleccionarMarca.idN;
                        marcaModel.ActualizarExpedienteMarca(idMarca, AgregarEtapa.numExpediente, (DateTime)AgregarEtapa.fecha,
                            AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                        MostrarAlerta("LA MARCA HA SIDO ENVIADA A RENOVACIÓN");
                        AnadirTabPage(tabPageVencimientosList);
                        EliminarTabPage(tabPageMarcaDetail);
                    }
                    else if (SeleccionarPatente.id != 0)
                    {
                        idPatente = SeleccionarPatente.id;
                        patenteModel.ActualizarExpedientePatente(idPatente, AgregarEtapa.numExpediente, (DateTime)AgregarEtapa.fecha,
                            AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                        MostrarAlerta("LA PATENTE HA SIDO ENVIADA A RENOVACIÓN");
                        AnadirTabPage(tabPageVencimientosList);
                        EliminarTabPage(tabPagePatenteDetail);
                    }

                    await LoadVencimientos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            filtrar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = vencimientoModel.GetFilteredVencimientosCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                filtrar();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (buscando == true)
            {
                filtrar();
            }
            else
            {
                await LoadVencimientos();
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
                    filtrar();
                }
                else
                {
                    await LoadVencimientos();
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
                    filtrar();
                }
                else
                {
                    await LoadVencimientos();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (buscando == true)
            {
                filtrar();
            }
            else
            {
                await LoadVencimientos();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void dtgVencimientos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgVencimientos.Columns["id"] != null)
            {
                dtgVencimientos.Columns["id"].Visible = false;
                dtgVencimientos.Columns["marcaID"].Visible = false;
                dtgVencimientos.Columns["patenteID"].Visible = false;
            }
            dtgVencimientos.ClearSelection();
            SeleccionarMarca.idInt = 0;
            SeleccionarMarca.idN = 0;
            SeleccionarPatente.id = 0;
            dtgVencimientos.Refresh();
        }
        private void CentrarPanel()
        {

            int anchoMinimo = panelBusqueda.Width + 100;

            if (tabControl1.ClientSize.Width >= anchoMinimo)
            {
                // Pantalla suficientemente ancha → centrar
                panelBusqueda.Anchor = AnchorStyles.None;

                int x = (tabControl1.ClientSize.Width - panelBusqueda.Width) / 2;
                int y = 0; // o donde quieras posicionarlo verticalmente
                panelBusqueda.Location = new System.Drawing.Point(x, y);
            }
            else
            {
                // Pantalla pequeña → top-left
                panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panelBusqueda.Location = new System.Drawing.Point(0, 0); // o donde quieras
            }
        }
        private void FrmVencimientos_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private void richTextBoxMensajeM_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonNegrita();
            ActualizarEstadoBotonCursiva();
            ActualizarEstadoBotonSubrayado();
        }


        private void roundedButton21_Click(object sender, EventArgs e)
        {



        }
        private async Task MostrarLogoDesdeUrlAsync(string url)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(url);
                convertirImagen();
                if (response.IsSuccessStatusCode)
                {
                   
                    byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                    using var ms = new MemoryStream(bytes);
                    var img = System.Drawing.Image.FromStream(ms);
                    pictureBoxLogo.Image = img;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Si 404, muestro el icono de documento por defecto
                    pictureBoxLogo.Image = documento;
                }
                else
                {
                    // Para cualquier otro error también muestro el icono por defecto
                    pictureBoxLogo.Image = documento;
                }
            }
            catch
            {
                // Si hay cualquier excepción de red, muestro el icono por defecto
                pictureBoxLogo.Image = documento;
            }
            finally
            {
                pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }




        private async void btnSubirLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg";
                ofd.Title = "Selecciona el logo para subir";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = ofd.FileName;

                    try
                    {
                        // Llama al método para subir el logo
                        string resultado = await SubirLogoAsync(rutaArchivo);
                        MessageBox.Show("Logo subido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnEliminarLogo.Enabled = true;
                        string urlLogo = "https://bpa.com.es/logoCorreo/logoCorreo/logoCorreo.png";
                        await MostrarLogoDesdeUrlAsync(urlLogo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al subir logo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void iconButton16_Click(object sender, EventArgs e)
        {
            // 1. Mostrar diálogo de confirmación
            var result = MessageBox.Show(
                "¿Deseas eliminar el logo actual? Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 2. Llamar al método que elimina el logo en el servidor
                    string respuestaJson = await EliminarLogoAsync();

                    // 3. Parsear la respuesta si quieres, o simplemente mostrarla
                    MessageBox.Show(
                        "Logo eliminado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Aquí podrías además actualizar la UI, p. ej. deshabilitar el botón de eliminar
                    btnEliminarLogo.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error al eliminar el logo:\n{ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                // El usuario canceló: no hacemos nada
            }
        }
    }
}
