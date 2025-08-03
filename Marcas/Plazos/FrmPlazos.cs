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
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Presentacion.Reportes;
using System.Reflection;

namespace Presentacion.Plazos
{
    public partial class FrmPlazos : Form
    {
        PlazosModel plazosModel = new PlazosModel();
        HistorialModel historialModel = new HistorialModel();
        HistorialPatenteModel historialPatenteModel = new HistorialPatenteModel();

        public string tipoHistorial = "";
        public int IdHistorialMarca = 0;
        public int IdHistorialPatente = 0;
        public string usuarioCreador = "";
        public int numRegistros = 11;
        public float escala = 0.85f;
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;

        string titulo;

        public FrmPlazos()
        {
            InitializeComponent();
            this.Load += FrmPlazos_Load;
            comboBoxTipoFiltro.SelectedIndex = 0;
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageReportes);
            SetDoubleBuffering(this, true);
            SetDoubleBuffering(dtgReportes, true);

            if (UsuarioActivo.soloLectura)
            {
                btnEditar.Visible = false;
                comboBoxEstado.Enabled = false;
                dateTimePickerFechaIngreso.Enabled = false;
                dateTimePickerVencimiento.Enabled = false;
                richTextBoxAnotaciones.Enabled = false;
            }
            else
            {
                btnEditar.Visible = true;
                comboBoxEstado.Enabled = true;
                dateTimePickerFechaIngreso.Enabled = true;
                dateTimePickerVencimiento.Enabled = true;
                richTextBoxAnotaciones.Enabled = true;
            }
        }

        private void SetDoubleBuffering(System.Windows.Forms.Control control, bool enable)
        {
            // Habilitar o deshabilitar DoubleBuffering
            typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                           .SetValue(control, enable, null);
        }

        private void FrmPlazos_Load(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async Task LoadPlazos(string tipoRegistro)
        {
            // Obtener total y tabla 
            var (totalRows, datos) = await plazosModel.ObtenerPlazosAsync(tipoRegistro, pageSize, currentPageIndex);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();
                    dtgPlazos.DataSource = datos;
                }));
            }
        }


        private async void filtrar()
        {
            string buscar = txtBuscar.Text.Trim();

            if (!string.IsNullOrEmpty(buscar))
            {
                DataTable datos;
                string tipo = comboBoxTipoFiltro.Text;
                string tipoRegistro = "";
                if (tipo == "MARCAS")
                {
                    tipoRegistro = "marca";
                }
                else if (tipo == "PATENTES")
                {
                    tipoRegistro = "patente";
                }

                (totalRows, datos) = await plazosModel.ObtenerPlazosFiltradoAsync(tipoRegistro, pageSize, currentPageIndex, buscar);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        lblTotalPages.Text = totalPages.ToString();
                        lblTotalRows.Text = totalRows.ToString();
                        dtgPlazos.DataSource = datos;
                    }));
                }
            }
            else
            {

                string tipo = comboBoxTipoFiltro.Text;
                if (tipo == "MARCAS")
                {
                    await LoadPlazos("marca");
                }
                else if (tipo == "PATENTES")
                {
                    await LoadPlazos("patente");
                }
            }
        }




        public string ConvertirRichTextBoxAHtml(System.Windows.Forms.RichTextBox richTextBox, string logoUrl = null)
        {
            // 1. Convierte el texto RTF a HTML
            string rtfHtml = RtfToHtml(richTextBox.Rtf, richTextBox)
                             .Replace("\r\n", "<br>").Replace("\n", "<br>");

            // 2. Construye el fragmento del logo solo si logoUrl no está vacío
            string logoHtml = "";
            if (!string.IsNullOrWhiteSpace(logoUrl))
            {
                logoHtml = $"<br/><img src='{logoUrl}' alt='Logo' " +
                           "style='max-width:250px;height:auto;display:block;margin-top:20px;' />";
            }

            // 3. Arma el HTML completo
            return $"<html><body>{rtfHtml}{logoHtml}</body></html>";
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



        private void ibtnEditar_Click(object sender, EventArgs e)
        {

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;

            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }
        private async Task VerificarSeleccionId()
        {
            if (dtgPlazos.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY PLAZOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }

            if (dtgPlazos.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgPlazos.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    if (comboBoxTipoFiltro.Text == "MARCAS")
                    {
                        tipoHistorial = "marca";
                        IdHistorialMarca = Convert.ToInt32(dataRowView["Id"]);
                        IdHistorialPatente = 0;
                    }
                    else if (comboBoxTipoFiltro.Text == "PATENTES")
                    {
                        tipoHistorial = "patente";
                        IdHistorialMarca = 0;
                        IdHistorialPatente = Convert.ToInt32(dataRowView["Id"]);
                    }

                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN PLAZO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
            }
        }


        public async Task CargarDatosHistorialMarca()
        {
            DataTable historial = await historialModel.GetHistorialById(IdHistorialMarca);

            if (historial.Rows.Count > 0)
            {
                DataRow row = historial.Rows[0];

                if (row["fecha"] != DBNull.Value)
                    dateTimePickerFechaIngreso.Value = Convert.ToDateTime(row["fecha"]);

                if (row["fechaVencimiento"] != DBNull.Value)
                    dateTimePickerVencimiento.Value = Convert.ToDateTime(row["fechaVencimiento"]);

                if (row["anotaciones"] != DBNull.Value)
                    richTextBoxAnotaciones.Text = row["anotaciones"].ToString();

                if (row["etapa"] != DBNull.Value)
                    comboBoxEstado.SelectedItem = row["etapa"].ToString();

                if (row["usuario"] != DBNull.Value)
                    usuarioCreador = row["usuario"].ToString();

                AnadirTabPage(tabPageHistorialDetail);
            }
            else
            {
                MessageBox.Show("No se encontraron datos de historial para esta marca.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public async Task CargarDatosHistorialPatente()
        {
            DataTable historial = await historialPatenteModel.ObtenerHistorialPorId(IdHistorialPatente);

            if (historial.Rows.Count > 0)
            {
                DataRow row = historial.Rows[0];

                if (row["fecha"] != DBNull.Value)
                    dateTimePickerFechaIngreso.Value = Convert.ToDateTime(row["fecha"]);

                if (row["fechaVencimiento"] != DBNull.Value)
                    dateTimePickerVencimiento.Value = Convert.ToDateTime(row["fechaVencimiento"]);

                if (row["anotaciones"] != DBNull.Value)
                    richTextBoxAnotaciones.Text = row["anotaciones"].ToString();

                if (row["etapa"] != DBNull.Value)
                    comboBoxEstado.SelectedItem = row["etapa"].ToString();

                if (row["usuario"] != DBNull.Value)
                    usuarioCreador = row["usuario"].ToString();

                AnadirTabPage(tabPageHistorialDetail);
            }
            else
            {
                MessageBox.Show("No se encontraron datos de historial para esta patente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public async Task ActualizarDatosHistorialMarca()
        {
            string usuario = UsuarioActivo.usuario;
            string etapa = comboBoxEstado.Text;
            string anotaciones = richTextBoxAnotaciones.Text;
            AgregarEtapa.etapa = comboBoxEstado.Text;
            AgregarEtapa.fecha = dateTimePickerFechaIngreso.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;
            bool requiereVencimiento = etapa == "Examen de fondo" ||
                              etapa == "Requerimiento" ||
                              etapa == "Objeción" ||
                              etapa == "Publicación" ||
                              etapa == "Orden de pago";

            if (requiereVencimiento)
            {
                AgregarEtapa.fechaVencimiento = dateTimePickerVencimiento.Value;
            }
            else
            {
                AgregarEtapa.fechaVencimiento = null;
            }


            if (comboBoxEstado.SelectedIndex != -1)
            {
                string fecha = dateTimePickerFechaIngreso.Value.ToString("dd/MM/yyyy");
                string venc = dateTimePickerVencimiento.Value.ToString("dd/MM/yyyy");

                string anotacionFinal = "";

                if (requiereVencimiento)
                {
                    anotacionFinal = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
                }
                else if (etapa == "Resolución RPI favorable" ||
                         etapa == "Recurso de revocatoria" ||
                         etapa == "Resolución Ministerio de Economía (MINECO)" ||
                         etapa == "Contencioso administrativo")
                {
                    anotacionFinal = $"{fecha} Por objeción-{etapa}";
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

                bool actualizado = await historialModel.EditHistorialById(IdHistorialMarca, etapa, Convert.ToDateTime(fecha), anotaciones, usuarioCreador, usuario, Convert.ToDateTime(venc));
                if (actualizado)
                {
                    FrmAlerta alerta = new FrmAlerta("PLAZO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    usuarioCreador = "";
                    EliminarTabPage(tabPageHistorialDetail);
                    comboBoxTipoFiltro.SelectedIndex = 0;
                    IdHistorialMarca = 0;
                    AgregarEtapa.LimpiarEtapa();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGUN ESTADO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
            }



        }

        public async Task ActualizarDatosHistorialPatente()
        {
            string usuario = UsuarioActivo.usuario;
            string etapa = comboBoxEstado.Text;
            string anotaciones = richTextBoxAnotaciones.Text;
            AgregarEtapa.etapa = comboBoxEstado.Text;
            AgregarEtapa.fecha = dateTimePickerFechaIngreso.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;
            bool requiereVencimiento = etapa == "Examen de fondo" ||
                              etapa == "Requerimiento" ||
                              etapa == "Objeción" ||
                              etapa == "Publicación" ||
                              etapa == "Orden de pago";

            if (requiereVencimiento)
            {
                AgregarEtapa.fechaVencimiento = dateTimePickerVencimiento.Value;
            }
            else
            {
                AgregarEtapa.fechaVencimiento = null;
            }


            if (comboBoxEstado.SelectedIndex != -1)
            {
                string fecha = dateTimePickerFechaIngreso.Value.ToString("dd/MM/yyyy");
                string venc = dateTimePickerVencimiento.Value.ToString("dd/MM/yyyy");

                string anotacionFinal = "";

                if (requiereVencimiento)
                {
                    anotacionFinal = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
                }
                else if (etapa == "Resolución RPI favorable" ||
                         etapa == "Recurso de revocatoria" ||
                         etapa == "Resolución Ministerio de Economía (MINECO)" ||
                         etapa == "Contencioso administrativo")
                {
                    anotacionFinal = $"{fecha} Por objeción-{etapa}";
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

                try
                {
                    historialPatenteModel.EditarHistorialPatente(IdHistorialPatente, Convert.ToDateTime(fecha), etapa, AgregarEtapa.anotaciones, usuarioCreador, usuario, Convert.ToDateTime(venc));

                    FrmAlerta alerta = new FrmAlerta("PLAZO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    usuarioCreador = "";
                    EliminarTabPage(tabPageHistorialDetail);
                    comboBoxTipoFiltro.SelectedIndex = 0;
                    IdHistorialPatente = 0;
                    AgregarEtapa.LimpiarEtapa();
                }
                catch (Exception ex)
                {
                    FrmAlerta alerta = new FrmAlerta("NO SE HA ACTUALIZADO LA ETAPA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }


            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGUN ESTADO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
            }



        }

        private readonly List<string> etapasMarca = new List<string>
            {
                "Ingresada",
                "Examen de forma",
                "Examen de fondo",
                "Requerimiento",
                "Objeción",
                "Resolución RPI favorable",
                "Resolución RPI desfavorable",
                "Recurso de revocatoria",
                "Resolución Ministerio de Economía (MINECO)",
                "Contencioso administrativo",
                "Edicto",
                "Publicación",
                "Orden de pago",
                "Registrada",
                "Licencia de uso"
            };

        private readonly List<string> etapasPatente = new List<string>
        {
            "Ingresada",
            "Examen de forma",
            "Examen de publicación",
            "Edicto",
            "Examen de fondo",
            "Prórroga",
            "Rechazo",
            "Registro/concesión",
            "Modificación",
            "Conversión de solicitud",
            "Corrección del certificado o inscripción"
        };

        private void CargarEtapasEnComboBox(bool esHistorialMarca)
        {
            comboBoxEstado.Items.Clear();

            var listaEtapas = esHistorialMarca ? etapasMarca : etapasPatente;

            foreach (var etapa in listaEtapas)
            {
                comboBoxEstado.Items.Add(etapa);
            }

            comboBoxEstado.SelectedIndex = 0; // Selecciona el primero por defecto
        }


        public async void Ver()
        {
            await VerificarSeleccionId();
            if (tipoHistorial == "marca")
            {
                Cursor = Cursors.WaitCursor;
                CargarEtapasEnComboBox(true);
                await CargarDatosHistorialMarca();

                Cursor = Cursors.Default;
            }
            else if (tipoHistorial == "patente")
            {
                Cursor = Cursors.WaitCursor;
                CargarEtapasEnComboBox(false);

                await CargarDatosHistorialPatente();
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
            EliminarTabPage(tabPageHistorialDetail);

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void iconButton10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            tabControl1.Visible = false;

            await Task.Yield();



            tabControl1.Visible = true;

            Cursor = Cursors.Default;



        }

        private void checkBoxPCT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageVencimientosList);

        }



        public async Task ExportarDataTableAExcel(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }
            string nombre = titulo + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");
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
                    Properties.Resources.logo_comprimido_foragro.Save(tempLogoPath);

                    using (var workbook = new XLWorkbook())
                    {
                        // Crear la hoja de trabajo
                        var worksheet = workbook.Worksheets.Add("REPORTE OPOSICIÓN");

                        // Fecha actual en el formato deseado
                        string fecha = DateTime.Now.ToString("dd-MM-yyyy-HH-mm");

                        // Insertar el título "titulo" en la celda A1
                        worksheet.Cell(3, 5).Value = titulo;
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
                                .Scale(0.08); // Ajustar tamaño
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

                    System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(saveFileDialog.FileName));

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

        private async Task CrearPdfDesdeHtmlConLogoYDataTable(DataTable dt, int registrosPagina, float escalas)
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
                        Properties.Resources.logo_comprimido_foragro.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
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
                System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(pdfFilePath));

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
                //datos = await Task.Run(() => vencimientoModel.ObtenerTodosLosVencimientosFiltradosReporte(txtBuscar.Text));
            }
            else
            {
                //datos = await Task.Run(() => vencimientoModel.ObtenerVencimientos());
            }


            if (datos != null)
            {
                Func<Task> tarea = () => CrearPdfDesdeHtmlConLogoYDataTable(datos, numRegistros, escala);
                using (FrmLoading frm = new FrmLoading(tarea))
                {
                    frm.ShowDialog();
                }
                
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
                // datos = await Task.Run(() => vencimientoModel.ObtenerTodosLosVencimientosFiltradosReporte(txtBuscar.Text));
            }
            else
            {
                //datos = await Task.Run(() => vencimientoModel.ObtenerVencimientos());
            }
            datos = null;
            if (datos != null)
            {
                ExportarDataTableAExcel(datos);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            //FrmAgregarEtapaRegistradaV frmAgregarEtapaRegistradaV = new FrmAgregarEtapaRegistradaV();
            //frmAgregarEtapaRegistradaV.ShowDialog();


        }

        private void ActualizarEstadoMarca(int idMarca, DateTime fechaAbandono, string justificacion, string usuarioAbandono)
        {
            // Actualizar el estado y la justificación de la marca
            historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", justificacion, usuarioAbandono, "TRÁMITE", null);
        }

        private void ActualizarEstadoPatente(int idPatente, DateTime fechaAbandono, string justificacion, string usuarioAbandono)
        {
            // Actualizar el estado y la justificación de la patente
            historialPatenteModel.CrearHistorialPatente(fechaAbandono, "Abandono", justificacion, usuarioAbandono, null, idPatente, null);
        }

        private void ActualizarEstadoMarca2(int idMarca, DateTime fechaAbandono, string estado, string anotaciones, string usuario)
        {
            // Actualizar el estado y la justificación de la marca
            historialModel.GuardarEtapa(idMarca, fechaAbandono, estado, anotaciones, usuario, "TRÁMITE", null);
        }

        private void ActualizarEstadoPatente2(int idPatente, DateTime fechaAbandono, string estado, string anotaciones, string usuario)
        {
            // Actualizar el estado y la justificación de la patente
            historialPatenteModel.CrearHistorialPatente(fechaAbandono, estado, anotaciones, usuario, null, idPatente, null);
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
                        EliminarTabPage(tabPageHistorialDetail);

                        //await LoadVencimientos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                        EliminarTabPage(tabPageHistorialDetail);

                        //await LoadVencimientos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                //totalRows = vencimientoModel.GetFilteredVencimientosCount(txtBuscar.Text);
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
                string tipo = comboBoxTipoFiltro.Text;
                if (tipo == "MARCAS")
                {
                    await LoadPlazos("marca");
                }
                else if (tipo == "PATENTES")
                {
                    await LoadPlazos("patente");
                }
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
                    string tipo = comboBoxTipoFiltro.Text;
                    if (tipo == "MARCAS")
                    {
                        await LoadPlazos("marca");
                    }
                    else if (tipo == "PATENTES")
                    {
                        await LoadPlazos("patente");
                    }
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
                    string tipo = comboBoxTipoFiltro.Text;
                    if (tipo == "MARCAS")
                    {
                        await LoadPlazos("marca");
                    }
                    else if (tipo == "PATENTES")
                    {
                        await LoadPlazos("patente");
                    }


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
                string tipo = comboBoxTipoFiltro.Text;
                if (tipo == "MARCAS")
                {
                    await LoadPlazos("marca");
                }
                else if (tipo == "PATENTES")
                {
                    await LoadPlazos("patente");
                }
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void dtgVencimientos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgPlazos.Columns["id"] != null)
            {
                dtgPlazos.Columns["id"].Visible = false;
            }

            if (dtgPlazos.Columns["IdMarca"] != null)
            {
                dtgPlazos.Columns["IdMarca"].Visible = false;
            }


            if (dtgPlazos.Columns["IdPatente"] != null)
            {
                dtgPlazos.Columns["IdPatente"].Visible = false;
            }

            dtgPlazos.ClearSelection();
            SeleccionarMarca.idInt = 0;
            SeleccionarMarca.idN = 0;
            SeleccionarPatente.id = 0;
            dtgPlazos.Refresh();
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

        private void FrmPlazos_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
            PosicionarPanelDebajoDerecha();
        }




        private void roundedButton21_Click(object sender, EventArgs e)
        {



        }


        private async void iconButton16_Click(object sender, EventArgs e)
        {
            // 1. Mostrar diálogo de confirmación
            var result = MessageBox.Show(
                "¿Desea eliminar el logo actual? Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 2. Llamar al método que elimina el logo en el servidor


                    // 3. Parsear la respuesta si quieres, o simplemente mostrarla
                    MessageBox.Show(
                        "Logo eliminado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Aquí podrías además actualizar la UI, p. ej. deshabilitar el botón de eliminar

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

        private void iconButton16_Click_1(object sender, EventArgs e)
        {
            // 1. Mostrar diálogo de confirmación
            var result = MessageBox.Show(
                "¿Desea eliminar el logo actual? Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 2. Llamar al método que elimina el logo en el servidor


                    // 3. Parsear la respuesta si quieres, o simplemente mostrarla
                    MessageBox.Show(
                        "Logo eliminado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Aquí podrías además actualizar la UI, p. ej. deshabilitar el botón de eliminar

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

        private void btnSubirLogo2_Click(object sender, EventArgs e)
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

                        MessageBox.Show("Logo subido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al subir logo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private async void comboBoxTipoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoSeleccionado = comboBoxTipoFiltro.Text; // "marca" o "patente"
            if (tipoSeleccionado == "MARCAS")
            {
                currentPageIndex = 1; // Reinicia a la primera página
                await LoadPlazos("marca");
            }
            else if (tipoSeleccionado == "PATENTES")
            {
                currentPageIndex = 1; // Reinicia a la primera página
                await LoadPlazos("patente");
            }


        }

        private void comboBoxEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string etapa = comboBoxEstado.Text;
            DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;
            DateTime fechaVencimiento = fechaIngreso; // valor por defecto

            // Establecer vencimiento automático según la etapa
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

            // Mostrar u ocultar el campo de vencimiento según la etapa
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

            // Mostrar resumen en el RichTextBox
            string fecha = fechaIngreso.ToString("dd/MM/yyyy");
            string venc = fechaVencimiento.ToString("dd/MM/yyyy");

            if (etapa == "Resolución RPI desfavorable")
            {
                richTextBoxAnotaciones.Text = $"{fecha} Por objeción - {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (mostrarVencimiento)
            {
                richTextBoxAnotaciones.Text = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (etapa == "Resolución RPI favorable" ||
                     etapa == "Recurso de revocatoria" ||
                     etapa == "Resolución Ministerio de Economía (MINECO)" ||
                     etapa == "Contencioso administrativo")
            {
                richTextBoxAnotaciones.Text = $"{fecha} Por objeción - {etapa}";
            }
            else
            {
                richTextBoxAnotaciones.Text = $"{fecha} {etapa}";
            }
        }

        private void dateTimePickerFechaIngreso_ValueChanged(object sender, EventArgs e)
        {
            comboBoxEstado_SelectedIndexChanged(sender, e);
        }

        private void dateTimePickerVencimiento_ValueChanged(object sender, EventArgs e)
        {
            comboBoxEstado_SelectedIndexChanged(sender, e);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            AnadirTabPage(tabPageVencimientosList);
            comboBoxTipoFiltro.SelectedIndex = 0;
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageReportes);
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            if (tipoHistorial == "marca")
            {
                await ActualizarDatosHistorialMarca();
            }
            else
            {
                await ActualizarDatosHistorialPatente();
            }

        }

        private void PosicionarPanelDebajoDerecha()
        {
            // Obtener posición absoluta de tableLayoutPanel2
            System.Drawing.Point punto1 = tableLayoutPanel2.Location;
            int x1 = punto1.X + tableLayoutPanel2.Width - panelBotones.Width;
            int y1 = punto1.Y + tableLayoutPanel2.Height + 10; // separacion de 10 px abajo
            panelBotones.Location = new System.Drawing.Point(x1, y1);

            // Obtener posición absoluta de panel23
            System.Drawing.Point punto2 = panel23.Location;
            int x2 = punto2.X + panel23.Width - panelBotones2.Width;
            int y2 = punto2.Y + panel23.Height + 10;
            panelBotones2.Location = new System.Drawing.Point(x2, y2);
        }

        /*
        private void PosicionarPanelDebajoDerecha()
        {

            // Asumiendo que quieres que panelB esté debajo y alineado a la derecha de panelA
            int x = tableLayoutPanel2.Right - panelBotones.Width; // Alineado a la derecha de panelA
            int y = tableLayoutPanel2.Bottom; // Justo debajo de panelA

            panelBotones.Location = new System.Drawing.Point(x, y);

            // Asumiendo que quieres que panelB esté debajo y alineado a la derecha de panelA
            int x2 = panel23.Right - panelBotones2.Width; // Alineado a la derecha de panelA
            int y2 = panel23.Bottom; // Justo debajo de panelA

            panelBotones2.Location = new System.Drawing.Point(x2, y2);
        }

        */
        private void btnIrAReportes_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageReportes);
            PosicionarPanelDebajoDerecha();
            cmbFiltro.SelectedIndex = 0;
        }





        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            dtgReportes.DataSource = null;
            dtgReportes.ClearSelection();
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
            AnadirTabPage(tabPageVencimientosList);
            comboBoxTipoFiltro.SelectedIndex = 0;
            EliminarTabPage(tabPageReportes);
        }


        public async void Filtrar()
        {
            string objeto = null;
            string expediente = null;
            string signo = null;
            string estado = null;
            string? fechaIngresoInicio = null;
            string? fechaingresoFin = null;
            string? fechaVencimientoInicio = null;
            string? fechaVencimientoFinal = null;
            string clase = null;
            string titular = null;
            string agente = null;

            string tipoPlazo = null;
            numRegistros = 9;
            escala = 0.85f;

            titulo = "REPORTE DE PLAZOS ";

            switch (cmbFiltro.SelectedIndex)
            {
                case 0:
                    tipoPlazo = "MARCAS";
                    titulo += "MARCAS";
                    break;
                case 1:
                    tipoPlazo = "PATENTES";
                    titulo += "PATENTES";
                    break;


            }


            if (checkBoxEstadoReporte.Checked)
            { estado = comboBoxEstadoReporte.Text; }
            else { estado = null; }

            if (chckExpedienteReporte.Checked)
            { expediente = txtExpedienteReporte.Text; }
            else
            { expediente = null; }

            if (chckSignoRepo.Checked) { signo = txtSignoReporte.Text; }
            else { signo = null; }

            if (chckClaseReporte.Checked) { clase = txtClaseReporte.Text; }
            else { clase = null; }

            if (checkBoxFIngreso.Checked)
            {
                fechaIngresoInicio = dtpFIngresoInicial.Value.ToString("yyyy-MM-dd");
                fechaingresoFin = dtpFechaingresoFinal.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaIngresoInicio = null;
                fechaingresoFin = null;
            }

            if (checkBoxVencimiento.Checked)
            {
                fechaVencimientoInicio = dtpVencimientoInicial.Value.ToString("yyyy-MM-dd");
                fechaVencimientoFinal = dtpVencimientoFinal.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaVencimientoInicio = null;
                fechaVencimientoFinal = null;
            }


            if (chckTitularReporte.Checked) { titular = richTextBoxTitularReporte.Text; }
            else { titular = null; }

            if (chckAgenteReporte.Checked) { agente = richTextBoxAgenteReporte.Text; }
            else { agente = null; }




            dtgReportes.DataSource = await plazosModel.FiltrarPlazosAsync(tipoPlazo, expediente, signo,
                estado, clase, fechaIngresoInicio, fechaingresoFin, fechaVencimientoInicio, fechaVencimientoFinal,
                titular, agente);
            dtgReportes.ClearSelection();



        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void iconButton9_Click_1(object sender, EventArgs e)
        {
            dtgReportes.DataSource = null;
            dtgReportes.ClearSelection();
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
            AnadirTabPage(tabPageVencimientosList);
            comboBoxTipoFiltro.SelectedIndex = 0;
            cmbFiltro.SelectedIndex = 0;
            EliminarTabPage(tabPageReportes);
        }

            private void btnExportarPDF_Click(object sender, EventArgs e)
            {
            DataTable datos = dtgReportes.DataSource as DataTable;

            if (datos != null)
            {
                Func<Task> tarea = () => CrearPdfDesdeHtmlConLogoYDataTable(datos, numRegistros, escala, titulo);
                using (FrmLoading frm = new FrmLoading(tarea))
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        

        private async Task CrearPdfDesdeHtmlConLogoYDataTable(DataTable dt, int registrosPagina, float escalas, string titulo)
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

            string nombre = titulo + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = nombre + ".pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = chromePath
                });

                var page = await browser.NewPageAsync();
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
                                : (column.ColumnName == "TIPO_OPOSICION" || column.ColumnName == "SITUACION_ACTUAL")
                                    ? "style='padding: 8px; text-align: center; border: 1px solid #ddd;'"
                                    : "style='padding: 8px; text-align: left; border: 1px solid #ddd;'";

                            object cellValue = row[column];
                            if (cellValue is DateTime dateValue)
                            {
                                cellValue = dateValue.ToString("dd/MM/yyyy");
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
                        Properties.Resources.logo_comprimido_foragro.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageBytes = ms.ToArray();
                        base64Logo = Convert.ToBase64String(imageBytes);
                    }

                    string imageHtml = $"<img src='data:image/png;base64,{base64Logo}' />";

                    fullHtmlContent += $@"<html>
                        <head>
                            <style>
                                * {{box-sizing: border-box}}
                                body {{ font-family: Arial, sans-serif; }}
                                table {{ border-collapse: collapse; width: 100%; }}
                                th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
                                th {{ background-color: #f2f2f2; font-weight: bold; }}
                                img {{ width: 100px; height: auto; }}
                                @page {{ size: legal landscape; margin: 25mm; }}
                                table {{ page-break-inside: auto; }}
                                tr {{ page-break-inside: avoid; }}
                                td {{ page-break-before: auto; }}
                                .header {{ text-align: center; font-size: 20px; font-weight: bold; margin-bottom: 10px; }}
                            </style>
                        </head>
                        <body>
                            <div class='header'>{titulo}</div>
                            <div class='fecha'><center>Fecha: {DateTime.Now:dd-MM-yyyy HH:mm}</center></div>
                            {imageHtml}
                            <table>
                                <thead><tr>{headers}</tr></thead>
                                <tbody>{tableContent}</tbody>
                            </table>
                            {(pagina < totalPaginas - 1 ? "<div style='page-break-before: always;'></div>" : "")}
                        </body>
                        </html>";
                }

                await page.SetContentAsync(fullHtmlContent);
                string pdfFilePath = saveFileDialog.FileName;

                await page.PdfAsync(pdfFilePath, new PdfOptions
                {
                    Format = PaperFormat.Legal,
                    PrintBackground = true,
                    Landscape = true,
                    Scale = (Decimal)escalas
                });

                await browser.CloseAsync();
                FrmAlerta alerta = new FrmAlerta("PDF GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();

                System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(pdfFilePath));

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ NINGUNA RUTA PARA GUARDAR EL PDF", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            DataTable datos = dtgReportes.DataSource as DataTable;

            if (datos != null)
            {
                Func<Task> tarea = () => ExportarDataTableAExcel(datos);
                using (FrmLoading frm = new FrmLoading(tarea))
                {
                    frm.ShowDialog();
                }
                
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para exportar.");
            }
        }

        private void btnAgenteReporte_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentesReportes frmMostrarAgentes = new FrmMostrarAgentesReportes();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersonaReportes.nombreAgente != "")
            {
                richTextBoxAgenteReporte.Text = SeleccionarPersonaReportes.nombreAgente;
            }
            else
            {
                richTextBoxAgenteReporte.Text = "";
            }
        }

        private void btnTitularReporte_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesReportes frmMostrarTitulares = new FrmMostrarTitularesReportes();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersonaReportes.nombreTitular != "")
            {
                richTextBoxTitularReporte.Text = SeleccionarPersonaReportes.nombreTitular;
            }
            else
            {
                richTextBoxTitularReporte.Text = "";
            }
        }
    }
}
