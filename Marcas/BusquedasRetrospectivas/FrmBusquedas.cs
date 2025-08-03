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

namespace Presentacion.BusquedasRetrospectivas

{
    public partial class FrmBusquedas : Form
    {
        HistorialModel historialModel = new HistorialModel();
        HistorialPatenteModel historialPatenteModel = new HistorialPatenteModel();
        BusquedaRetrospectivaModel busquedaRetrospectivaModel = new BusquedaRetrospectivaModel();
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
        private DataTable tablaPaises;
        private int idPaisEditando = 0;
        private int idBusquedaActual = 0;
        bool eliminarPais = false;
        bool editarPais = false;
        private DataTable tablaPaisesOriginal; // Declarar como campo del formulario

        string titulo;

        public FrmBusquedas()
        {
            InitializeComponent();
            this.Load += FrmBusquedas_Load;
            //EliminarTabPage(tabPageHistorialDetail);
            SetDoubleBuffering(this, true);
            EliminarTabPage(tabPageBusquedaDetail);
        }

        private void SetDoubleBuffering(System.Windows.Forms.Control control, bool enable)
        {
            // Habilitar o deshabilitar DoubleBuffering
            typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                           .SetValue(control, enable, null);
        }

        private async void FrmBusquedas_Load(object sender, EventArgs e)
        {
            CrearTablaPaises();

            // Opcional: vincula al DataGridView si tienes uno
            dtgPaises.DataSource = tablaPaises;
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
            await LoadBusquedas();

        }

        private async Task LoadBusquedas()
        {
            // Obtener total y tabla 
            var (totalRows, datos) = await busquedaRetrospectivaModel.ObtenerBusquedasAsync(pageSize, currentPageIndex);
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

        private void InicializarTablaPaises()
        {
            tablaPaises = new DataTable();
            tablaPaises.Columns.Add("Pais", typeof(string));
            tablaPaises.Columns.Add("Nivel Riesgo", typeof(string));
            tablaPaises.Columns.Add("Observaciones", typeof(string));

            dtgPaises.DataSource = tablaPaises;
        }

        private async void filtrar()
        {
            string buscar = txtBuscar.Text.Trim();

            if (!string.IsNullOrEmpty(buscar))
            {
                DataTable datos;
                (totalRows, datos) = await busquedaRetrospectivaModel.ObtenerBusquedasFiltradoAsync(buscar, pageSize, currentPageIndex);
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
                await LoadBusquedas();

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
                FrmAlerta alerta = new FrmAlerta("NO HAY BÚSQUEDAS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }

            if (dtgPlazos.SelectedRows.Count > 0)
            {
                if (tablaPaises == null || tablaPaises.Columns.Count == 0)
                {
                    tablaPaises = CrearTablaPaises();
                }
                else
                {
                    tablaPaises.Clear();
                }

                var filaSeleccionada = dtgPlazos.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int idBusqueda = Convert.ToInt32(dataRowView["Id"]);
                    BusquedaR.idBusqueda = idBusqueda;


                    // Obtener búsqueda y países desde el modelo
                    var resultado = await busquedaRetrospectivaModel.ObtenerBusquedaConPaisesAsync(idBusqueda);
                    var busqueda = resultado.busqueda;
                    var paises = resultado.paises;

                    if (busqueda != null)
                    {
                        // Cargar datos de la búsqueda
                        txtNombre.Text = busqueda["Nombre"].ToString();
                        txtClase.Text = busqueda["Clase"].ToString();
                        comboBoxSignoDistintivo.Text = busqueda["SignoDistintivo"].ToString();
                        comboBoxTipoSigno.Text = busqueda["TipoSigno"].ToString();
                        string multiclaseStr = busqueda["multiclase"].ToString().Trim();

                        checkBoxMulticlaseEditar.Checked = multiclaseStr == "1" || multiclaseStr.ToLower() == "true";


                        tablaPaises.Clear();
                        tablaPaisesOriginal = paises.Copy();
                        tablaPaises = paises.Copy(); // Copia para mantener referencia viva
                        dtgPaises.DataSource = tablaPaises;
                        AgregarColumnasAccionEditar();
                        AnadirTabPage(tabPageBusquedaDetail);
                    }
                    else
                    {
                        FrmAlerta alerta = new FrmAlerta("NO SE PUDO CARGAR LA BÚSQUEDA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA BÚSQUEDA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
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

        }


        public async void Ver()
        {

            Cursor = Cursors.WaitCursor;
            await VerificarSeleccionId();
            Cursor = Cursors.Default;

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
            EliminarTabPage(tabPageBusquedaDetail);

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



        public void ExportarDataTableAExcel(DataTable dataTable)
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
                    Properties.Resources.logoForagro1.Save(tempLogoPath);

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
                //datos = await Task.Run(() => vencimientoModel.ObtenerTodosLosVencimientosFiltradosReporte(txtBuscar.Text));
            }
            else
            {
                //datos = await Task.Run(() => vencimientoModel.ObtenerVencimientos());
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
                        EliminarTabPage(tabPageBusquedaDetail);

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
                        EliminarTabPage(tabPageBusquedaDetail);

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

        private void FrmBusquedas_Resize(object sender, EventArgs e)
        {
            CentrarPanel();

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



        }

        private void comboBoxEstado_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void dateTimePickerFechaIngreso_ValueChanged(object sender, EventArgs e)
        {
            comboBoxEstado_SelectedIndexChanged(sender, e);
        }

        private void dateTimePickerVencimiento_ValueChanged(object sender, EventArgs e)
        {
            comboBoxEstado_SelectedIndexChanged(sender, e);
        }

        private async void iconButton2_Click(object sender, EventArgs e)
        {

            AnadirTabPage(tabPageVencimientosList);
            BusquedaR.idBusqueda = 0;
            LimpiarTodo();
            await LoadBusquedas();
            EliminarTabPage(tabPageBusquedaDetail);
        }

        private bool ValidarCampo(string campo, string mensaje)
        {
            if (string.IsNullOrEmpty(campo))
            {
                FrmAlerta alerta = new FrmAlerta(mensaje.ToUpper(), "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();

                return false;
            }
            return true;
        }

        private bool ValidarCampos(ref string clase)
        {

            if (
                !ValidarCampo(clase, "Ingrese la clase")
                )
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




            return true;
        }

        private bool ValidarCamposEditar(ref string clase)
        {

            if (
                !ValidarCampo(clase, "Ingrese la clase")
                )
            {
                return false;
            }

            // Normalizar clase quitando espacios extra
            clase = string.Join(",", clase.Split(',')
                                          .Select(c => c.Trim())
                                          .Where(c => !string.IsNullOrWhiteSpace(c)));

            if (checkBoxMulticlaseEditar.Checked)
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




            return true;
        }

        private async Task AgregarBusqueda()
        {
            string signo = textBoxSignoA.Text.Trim();
            string clase = textBoxClaseA.Text.Trim();
            string signoDistintivo = comboBoxSignoDistintivoA.Text;
            string tipo = comboBoxTipoSignoA.Text;
            bool multiclase = checkBoxMulticlase.Checked;

            if (string.IsNullOrWhiteSpace(textBoxSignoA.Text) ||
                   string.IsNullOrWhiteSpace(textBoxClaseA.Text) ||
                   comboBoxSignoDistintivoA.SelectedItem == null ||
                   comboBoxTipoSignoA.SelectedItem == null)
            {
                FrmAlerta alerta = new FrmAlerta("COMPLETE TODOS LOS CAMPOS DEL SIGNO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }

            if (!ValidarCampos(ref clase))
            {
                return;
            }


            if (tablaPaises == null || tablaPaises.Rows.Count == 0)
            {
                FrmAlerta alerta = new FrmAlerta("DEBE AGREGAR AL MENOS UN PAÍS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }


            // Construir arreglo de países
            var listaPaises = new List<object>();
            foreach (DataRow row in tablaPaises.Rows)
            {
                listaPaises.Add(new
                {
                    pais = row["Pais"].ToString(),
                    nivel = row["Nivel Riesgo"].ToString(),
                    observaciones = row["Observaciones"].ToString()
                });
            }


            var (idBusqueda, mensajeError) = await busquedaRetrospectivaModel.CrearBusquedaAsync(signo, clase, signoDistintivo, tipo, multiclase);

            if (!string.IsNullOrEmpty(mensajeError))
            {
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (idBusqueda != null)
            {
                foreach (dynamic pais in listaPaises)
                {
                    string nombrePais = pais.pais;
                    string nivel = pais.nivel;
                    string observaciones = pais.observaciones;

                    await busquedaRetrospectivaModel.AgregarPaisAsync(idBusqueda.Value, nombrePais, nivel, observaciones);
                    FrmAlerta alerta = new FrmAlerta("BÚSQUEDA RETROSPECTIVA GUARDADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    AnadirTabPage(tabPageVencimientosList);
                    await LoadBusquedas();
                    EliminarTabPage(tabPageAgregarBusqueda);
                }


            }
            else
            {
                MessageBox.Show("Búsqueda no guardada.");

            }
        }

        private async Task GuardarPaisesEditadosAsync(int idBusqueda, DataTable tablaPaises)
        {
            foreach (DataRow row in tablaPaises.Rows)
            {
                string pais = row["Pais"].ToString();
                string nivel = row["Nivel Riesgo"].ToString();
                string observaciones = row["Observaciones"].ToString();

                // Verifica si hay columna IdPais y si tiene valor
                if (tablaPaises.Columns.Contains("Id") &&
                    int.TryParse(row["Id"].ToString(), out int idPais) && idPais > 0)
                {
                    // EDITAR
                    await busquedaRetrospectivaModel.EditarPaisAsync(idPais, idBusqueda, pais, nivel, observaciones);
                }
                else
                {
                    // AGREGAR
                    await busquedaRetrospectivaModel.AgregarPaisAsync(idBusqueda, pais, nivel, observaciones);
                }
            }

            // 2. Eliminar (si algún país original ya no está en la tabla actual)
            var idsActuales = tablaPaises.AsEnumerable()
             .Where(r => tablaPaises.Columns.Contains("Id") &&
                         r["Id"] != DBNull.Value &&
                         !string.IsNullOrWhiteSpace(r["Id"].ToString()))
             .Select(r => Convert.ToInt32(r["Id"]))
             .ToHashSet();


            foreach (DataRow rowOriginal in tablaPaisesOriginal.Rows)
            {
                int idOriginal = Convert.ToInt32(rowOriginal["Id"]);
                if (!idsActuales.Contains(idOriginal))
                {
                    await busquedaRetrospectivaModel.EliminarPaisAsync(idOriginal);
                }
            }
        }


        private async Task EditarBusqueda()
        {
            string signo = txtNombre.Text.Trim();
            string clase = txtClase.Text.Trim();
            string signoDistintivo = comboBoxSignoDistintivo.SelectedItem.ToString();
            string tipo = comboBoxTipoSigno.SelectedItem.ToString();
            bool multiclase = checkBoxMulticlaseEditar.Checked;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtClase.Text) ||
                comboBoxSignoDistintivo.SelectedItem == null ||
                comboBoxTipoSigno.SelectedItem == null)
            {
                MessageBox.Show("Complete todos los campos del signo.");
                return;
            }

            if (!ValidarCamposEditar(ref clase))
            {
                return;
            }


            if (!int.TryParse(BusquedaR.idBusqueda.ToString(), out int idBusqueda))
            {
                MessageBox.Show("ID de búsqueda no válido.");
                return;
            }

            string mensajeError = await busquedaRetrospectivaModel.EditarBusquedaAsync(idBusqueda, signo, clase, signoDistintivo, tipo, multiclase);

            if (!string.IsNullOrEmpty(mensajeError))
            {
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await GuardarPaisesEditadosAsync(BusquedaR.idBusqueda, tablaPaises);


            FrmAlerta alerta = new FrmAlerta("BÚSQUEDA RETROSPECTIVA ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            alerta.ShowDialog();

            await LoadBusquedas();
            LimpiarTodo();
            BusquedaR.idBusqueda = 0;
            AnadirTabPage(tabPageVencimientosList);
            EliminarTabPage(tabPageBusquedaDetail);


        }


        private async void btnEditarBusqueda_Click(object sender, EventArgs e)
        {

            await EditarBusqueda();

        }




        private void btnIrAReportes_Click(object sender, EventArgs e)
        {

        }





        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {


        }



        private void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        private void iconButton9_Click_1(object sender, EventArgs e)
        {

            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
            AnadirTabPage(tabPageVencimientosList);

        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {

        }

        private async void CrearPdfDesdeHtmlConLogoYDataTable(DataTable dt, int registrosPagina, float escalas, string titulo)
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
                        Properties.Resources.logoForagro1.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
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
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ NINGUNA RUTA PARA GUARDAR EL PDF", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnAgenteReporte_Click(object sender, EventArgs e)
        {

        }

        private void btnTitularReporte_Click(object sender, EventArgs e)
        {

        }

        private async void btnAgregarPais_Click(object sender, EventArgs e)
        {
            /*
            if (comboBoxPais.SelectedItem == null || comboBoxNivelRiesgo.SelectedItem == null)
            {
                MessageBox.Show("Selecciona país y nivel.");
                return;
            }

            string nuevoPais = comboBoxPais.SelectedItem.ToString();
            string nuevoNivel = comboBoxNivelRiesgo.SelectedItem.ToString();
            string nuevasObs = richTextBoxObservacionesPais.Text.Trim();

            // Verifica duplicado (en memoria)
            foreach (DataRow row in tablaPaises.Rows)
            {
                if (row["Pais"].ToString() == nuevoPais)
                {
                    MessageBox.Show("Este país ya ha sido agregado.");
                    return;
                }
            }

            // Si hay una búsqueda activa (ya guardada en BD)
            if (BusquedaR.idBusqueda != 0)
            {
                // Agrega directamente a la BD
                await busquedaRetrospectivaModel.AgregarPaisAsync(BusquedaR.idBusqueda, nuevoPais, nuevoNivel, nuevasObs);
                // Obtener búsqueda y países desde el modelo
                var resultado = await busquedaRetrospectivaModel.ObtenerBusquedaConPaisesAsync(BusquedaR.idBusqueda);

                var paises = resultado.paises;

                if (paises != null)
                {

                    tablaPaises.Clear();
                    tablaPaises = paises.Copy(); // Copia para mantener referencia viva
                    dtgPaises.DataSource = tablaPaises;


                }
                
            }
            else
            {
                // Si es búsqueda nueva, solo agrega en tabla temporal
                if (tablaPaises == null || tablaPaises.Columns.Count == 0)
                {
                    tablaPaises = CrearTablaPaises();
                }

                DataRow fila = tablaPaises.NewRow();
                fila["Pais"] = nuevoPais;
                fila["NivelRiesgo"] = nuevoNivel;
                fila["Observaciones"] = nuevasObs;
                tablaPaises.Rows.Add(fila);
            }

            // Refresca grid
            dtgPaises.DataSource = null;
            dtgPaises.DataSource = tablaPaises;

            // Limpiar entradas
            comboBoxPais.SelectedIndex = -1;
            comboBoxNivelRiesgo.SelectedIndex = -1;
            richTextBoxObservacionesPais.Clear();

            /*
            foreach (DataRow row in tablaPaises.Rows)
            {
                if (row["Pais"].ToString() == comboBoxPais.SelectedItem.ToString())
                {
                    MessageBox.Show("Este país ya ha sido agregado.");
                    return;
                }
            }


            if (comboBoxPais.SelectedItem == null || comboBoxNivelRiesgo.SelectedItem == null)
            {
                MessageBox.Show("Selecciona país y nivel.");
                return;
            }

            if (tablaPaises == null || tablaPaises.Columns.Count == 0)
            {
                tablaPaises = CrearTablaPaises();
            }


            DataRow fila = tablaPaises.NewRow();
            fila["Pais"] = comboBoxPais.SelectedItem.ToString();
            fila["NivelRiesgo"] = comboBoxNivelRiesgo.SelectedItem.ToString();
            fila["Observaciones"] = richTextBoxObservacionesPais.Text.Trim();

            tablaPaises.Rows.Add(fila);
            
            dtgPaises.DataSource = tablaPaises;
            // Limpiar entradas
            comboBoxPais.SelectedIndex = -1;
            comboBoxNivelRiesgo.SelectedIndex = -1;
            richTextBoxObservacionesPais.Clear();*/
        }

        private async void btnEliminarPais_Click(object sender, EventArgs e)
        {

            if (dtgPaises.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un país para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Seguro que desea eliminar el país seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;

            var fila = dtgPaises.SelectedRows[0].DataBoundItem as DataRowView;

            if (fila == null)
            {
                MessageBox.Show("No se pudo obtener la fila seleccionada.");
                return;
            }

            if (fila.Row.Table.Columns.Contains("Id") && int.TryParse(fila["Id"]?.ToString(), out int idPais) && idPais > 0)
            {
                bool eliminado = await busquedaRetrospectivaModel.EliminarPaisAsync(idPais);
                if (eliminado)
                {
                    fila.Row.Delete();
                    tablaPaises.AcceptChanges(); // Elimina físicamente

                    // 🔄 REFRESCA EL DATAGRID
                    dtgPaises.DataSource = null;
                    dtgPaises.DataSource = tablaPaises;

                    MessageBox.Show("País eliminado correctamente.");
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el país de la base de datos.");
                }
            }
            else
            {
                if (fila.Row.Table == tablaPaises)
                {
                    fila.Row.Delete();
                    tablaPaises.AcceptChanges();

                    dtgPaises.DataSource = null;
                    dtgPaises.DataSource = tablaPaises;

                    MessageBox.Show("País eliminado correctamente.");
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar. La fila no pertenece a la tabla temporal.");
                }
            }

            eliminarPais = true;
            /*
            if (dtgPaises.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un país para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Seguro que desea eliminar el país seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;

            var fila = dtgPaises.SelectedRows[0].DataBoundItem as DataRowView;

            if (fila == null)
            {
                MessageBox.Show("No se pudo obtener la fila seleccionada.");
                return;
            }

            if (fila.Row.Table.Columns.Contains("Id") && int.TryParse(fila["Id"]?.ToString(), out int idPais) && idPais > 0)
            {
                bool eliminado = await busquedaRetrospectivaModel.EliminarPaisAsync(idPais);
                if (eliminado)
                {
                    tablaPaises.dele
                    fila.Row.Delete(); // se oculta, pero sigue en la tabla como Deleted
                    MessageBox.Show("País marcado como eliminado.");
                }
                else
                {
                    MessageBox.Show("No se pudo marcar como eliminado.");
                }
            }
            else
            {
                // País temporal
                if (fila.Row.Table == tablaPaises)
                {
                    fila.Row.Delete(); // se marca como eliminado, pero sigue presente
                    MessageBox.Show("País temporal marcado como eliminado.");
                }
                else
                {
                    MessageBox.Show("No se pudo marcar. La fila no pertenece a la tabla temporal.");
                }
            }

            eliminarPais = true;

            */



        }


        private void LimpiarTodo()
        {
            txtNombre.Text = "";
            txtClase.Text = "";
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            checkBoxMulticlaseEditar.Checked = false;
            if (tablaPaises == null || tablaPaises.Columns.Count == 0)
            {
                tablaPaises = CrearTablaPaises();
            }
            else
            {
                tablaPaises.Clear();
            }

            if (tablaPaisesOriginal == null || tablaPaisesOriginal.Columns.Count == 0)
            {
                tablaPaisesOriginal = CrearTablaPaises();
            }
            else
            {
                tablaPaisesOriginal.Clear();
            }
           

        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            CrearTablaPaises();

            LimpiarTodo();


            dataGridViewAgregarPaises.DataSource = null;
            dataGridViewAgregarPaises.DataSource = tablaPaises;
            AnadirTabPage(tabPageAgregarBusqueda);

        }

        private async void roundedButton5_Click(object sender, EventArgs e)
        {
            /*
            if (dtgPaises.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un país para editar.");
                return;
            }

            // Bloquear agregar mientras se edita
            btnAgregarPais.Enabled = false;
            btnEditarPais.Enabled = false;
            btnGuardarCambiosPais.BringToFront();
            btnGuardarCambiosPais.Enabled = true;
            try
            {
                var fila = dtgPaises.SelectedRows[0];
                string pais = fila.Cells["Pais"].Value.ToString();
                string nivel = fila.Cells["NivelRiesgo"].Value.ToString();
                string observaciones = fila.Cells["Observaciones"].Value.ToString();

                // Establecer valores en los controles
                comboBoxPais.SelectedItem = pais;
                comboBoxNivelRiesgo.SelectedItem = nivel;
                richTextBoxObservacionesPais.Text = observaciones;

                // Si los valores no están en el ComboBox, puedes agregarlos dinámicamente:
                if (!comboBoxPais.Items.Contains(pais))
                    comboBoxPais.Items.Add(pais);
                if (!comboBoxNivelRiesgo.Items.Contains(nivel))
                    comboBoxNivelRiesgo.Items.Add(nivel);

                // Activar un estado "modo edición" si es necesario
                lblModo.Text = "Editando país"; // opcional, puede servir como indicador
                lblModo.ForeColor = Color.DarkOrange;

                idPaisEditando = int.Parse(fila.Cells["Id"].Value.ToString());
                idBusquedaActual = int.Parse(BusquedaR.idBusqueda.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos para editar: " + ex.Message);
            }*/
        }

        private async Task CargarBusquedaConPaises(int idBusqueda)
        {
            var (busqueda, paises) = await busquedaRetrospectivaModel.ObtenerBusquedaConPaisesAsync(idBusqueda);
            if (paises != null)
            {
                dtgPaises.DataSource = paises;
            }
        }

        private DataTable CrearTablaPaises()
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Pais", typeof(string));
            tabla.Columns.Add("Nivel Riesgo", typeof(string));
            tabla.Columns.Add("Observaciones", typeof(string));
            return tabla;
        }


        private async void btnGuardarCambiosPais_Click(object sender, EventArgs e)
        {
            /*
            if (dtgPaises.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un país para editar.");
                return;
            }

            var fila = dtgPaises.SelectedRows[0].DataBoundItem as DataRowView;

            if (fila == null)
            {
                MessageBox.Show("Error al obtener la fila seleccionada.");
                return;
            }

            string nuevoPais = comboBoxPais.SelectedItem?.ToString();
            string nuevoNivel = comboBoxNivelRiesgo.SelectedItem?.ToString();
            string nuevasObs = richTextBoxObservacionesPais.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevoPais) || string.IsNullOrWhiteSpace(nuevoNivel))
            {
                MessageBox.Show("Debe completar país y nivel.");
                return;
            }

            // Verifica si la fila tiene un ID válido (editado desde la BD o es nuevo)
            if (fila.Row.Table.Columns.Contains("Id") &&
                int.TryParse(fila["Id"]?.ToString(), out int idPais) &&
                idPais > 0 &&
                idBusquedaActual > 0)
            {
                // ✅ Editar en base de datos
                var error = await busquedaRetrospectivaModel.EditarPaisAsync(idPais, idBusquedaActual, nuevoPais, nuevoNivel, nuevasObs);

                if (error == null)
                {
                    fila["Pais"] = nuevoPais;
                    fila["NivelRiesgo"] = nuevoNivel;
                    fila["Observaciones"] = nuevasObs;

                    MessageBox.Show("País actualizado correctamente.");
                }
                else
                {
                    MessageBox.Show("Error al guardar cambios: " + error);
                    return;
                }
            }
            else
            {
                // ✅ Editar solo en memoria
                fila["Pais"] = nuevoPais;
                fila["NivelRiesgo"] = nuevoNivel;
                fila["Observaciones"] = nuevasObs;

                MessageBox.Show("País actualizado correctamente (temporal).");
            }

            // Limpiar campos y estado
            comboBoxPais.SelectedIndex = -1;
            comboBoxNivelRiesgo.SelectedIndex = -1;
            richTextBoxObservacionesPais.Clear();

            btnAgregarPais.Enabled = true;
            btnEditarPais.Enabled = true;
            btnAgregarPais.BringToFront();
            lblModo.Text = "AGREGAR PAÍS A LA BÚSQUEDA";
            lblModo.ForeColor = Color.Black;
            idPaisEditando = 0;
            idBusquedaActual = 0;*/

        }

        private void dtgPaises_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgPaises.Columns["id"] != null)
            {
                dtgPaises.Columns["id"].Visible = false;
            }

            if (dtgPaises.Columns["Id"] != null)
            {
                dtgPaises.Columns["Id"].Visible = false;
            }

            dtgPaises.ClearSelection();
        }

        private void btnAgregarPais_Click_1(object sender, EventArgs e)
        {
            //agregar pais solo al datagridview 
            if (tablaPaises == null || tablaPaises.Columns.Count == 0)
            {
                tablaPaises = CrearTablaPaises();
                dtgPaises.DataSource = tablaPaises;
            }


            FrmAgregarPais frm = new FrmAgregarPais();
            frm.ShowDialog();

            if (frm.agregoPais)
            {
                DataRow nuevaFila = tablaPaises.NewRow();
                nuevaFila["Pais"] = frm.comboBoxPaises.Text;
                nuevaFila["Nivel Riesgo"] = frm.comboBoxNivelRiesgo.Text;
                nuevaFila["Observaciones"] = frm.richTextBoxObservaciones.Text;
                tablaPaises.Rows.Add(nuevaFila);
            }
        }

        private async void iconButton3_Click_1(object sender, EventArgs e)
        {
            await AgregarBusqueda();
        }

        private void AgregarColumnasAccion()
        {
            // Evita duplicaciones
            if (dataGridViewAgregarPaises.Columns.Contains("Editar"))
                dataGridViewAgregarPaises.Columns.Remove("Editar");
            if (dataGridViewAgregarPaises.Columns.Contains("Eliminar"))
                dataGridViewAgregarPaises.Columns.Remove("Eliminar");

            // Agrega columnas
            DataGridViewImageColumn colEditar = new DataGridViewImageColumn
            {
                Name = "Editar",
                HeaderText = "",
                Image = Properties.Resources.edit__1_,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            colEditar.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2);

            DataGridViewImageColumn colEliminar = new DataGridViewImageColumn
            {
                Name = "Eliminar",
                HeaderText = "",
                Image = Properties.Resources.delete__1_,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            colEliminar.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2);

            dataGridViewAgregarPaises.Columns.Add(colEditar);
            dataGridViewAgregarPaises.Columns.Add(colEliminar);
        }

        private void AgregarColumnasAccionEditar()
        {
            // Evita duplicaciones
            if (dtgPaises.Columns.Contains("Editar"))
                dtgPaises.Columns.Remove("Editar");
            if (dtgPaises.Columns.Contains("Eliminar"))
                dtgPaises.Columns.Remove("Eliminar");

            // Agrega columnas
            DataGridViewImageColumn colEditar = new DataGridViewImageColumn
            {
                Name = "Editar",
                HeaderText = "",
                Image = Properties.Resources.edit__1_,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            colEditar.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2);

            DataGridViewImageColumn colEliminar = new DataGridViewImageColumn
            {
                Name = "Eliminar",
                HeaderText = "",
                Image = Properties.Resources.delete__1_,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            colEliminar.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2);

            dtgPaises.Columns.Add(colEditar);
            dtgPaises.Columns.Add(colEliminar);
        }


        private void roundedButton8_Click(object sender, EventArgs e)
        {
            FrmAgregarPais frmAgregarPais = new FrmAgregarPais();
            frmAgregarPais.ShowDialog();

            if (frmAgregarPais.agregoPais == true)
            {
                string nombrePais = frmAgregarPais.comboBoxPaises.Text;
                string nivelRiesgo = frmAgregarPais.comboBoxNivelRiesgo.Text;
                string observaciones = frmAgregarPais.richTextBoxObservaciones.Text;

                if (tablaPaises == null || tablaPaises.Columns.Count == 0)
                {
                    tablaPaises = CrearTablaPaises();
                }

                // ✅ Verifica si ya existe
                bool yaExiste = tablaPaises.AsEnumerable()
                    .Any(row => row.Field<string>("Pais") == nombrePais);

                if (yaExiste)
                {
                    FrmAlerta alerta = new FrmAlerta("YA SE AGREGÓ ESTE PAÍS", "DUPLICADO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return;
                }

                // Agrega a la tabla
                DataRow fila = tablaPaises.NewRow();
                fila["Pais"] = nombrePais;
                fila["Nivel Riesgo"] = nivelRiesgo;
                fila["Observaciones"] = observaciones;
                tablaPaises.Rows.Add(fila);

                // Refresca el DataGridView
                dataGridViewAgregarPaises.DataSource = null;
                dataGridViewAgregarPaises.DataSource = tablaPaises;
                AgregarColumnasAccion();
            }
            /*else
            {
                FrmAlerta alerta = new FrmAlerta("NO SE AGREGÓ UN PAÍS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
            }*/
        }

        private void LimpiarAgregarBusqueda()
        {
            textBoxSignoA.Text = "";
            textBoxClaseA.Text = "";
            comboBoxSignoDistintivoA.SelectedIndex = -1;
            comboBoxTipoSignoA.SelectedIndex = -1;

            if (tablaPaises == null || tablaPaises.Columns.Count == 0)
            {
                tablaPaises = CrearTablaPaises();
            }
            else
            {
                tablaPaises.Clear();
            }

            dataGridViewAgregarPaises.DataSource = null;
        }


        private async void iconButton1_Click(object sender, EventArgs e)
        {
            await LoadBusquedas();
            AnadirTabPage(tabPageVencimientosList);
            LimpiarAgregarBusqueda();
        }

        private void EditarPaisDesdeFormulario(int rowIndex)
        {
            DataRow fila = tablaPaises.Rows[rowIndex];

            FrmEditarPais frmEditar = new FrmEditarPais();
            frmEditar.comboBoxPaises.Text = fila["Pais"].ToString();
            frmEditar.comboBoxNivelRiesgo.Text = fila["Nivel Riesgo"].ToString();
            frmEditar.richTextBoxObservaciones.Text = fila["Observaciones"].ToString();

            frmEditar.ShowDialog();

            if (frmEditar.editoPais)
            {
                fila["Pais"] = frmEditar.comboBoxPaises.Text;
                fila["Nivel Riesgo"] = frmEditar.comboBoxNivelRiesgo.Text;
                fila["Observaciones"] = frmEditar.richTextBoxObservaciones.Text;
                dataGridViewAgregarPaises.Refresh();
            }
        }

        private void EliminarPais(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < tablaPaises.Rows.Count)
            {
                string pais = tablaPaises.Rows[rowIndex]["Pais"].ToString();

                FrmAlerta alerta = new FrmAlerta($"¿ESTÁ SEGURO DE ELIMINAR EL PAÍS '{pais}' DE LA LISTA?", "CONFIRMAR ELIMINACIÓN",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);



                if (alerta.ShowDialog() == DialogResult.Yes)
                {
                    tablaPaises.Rows.RemoveAt(rowIndex);
                }
            }
        }

        private void EditarPaisDesdeFormularioEdicion(int rowIndex)
        {
            DataRow fila = tablaPaises.Rows[rowIndex];

            FrmEditarPais frmEditar = new FrmEditarPais();
            frmEditar.comboBoxPaises.Text = fila["Pais"].ToString();
            frmEditar.comboBoxNivelRiesgo.Text = fila["Nivel Riesgo"].ToString();
            frmEditar.richTextBoxObservaciones.Text = fila["Observaciones"].ToString();

            frmEditar.ShowDialog();

            if (frmEditar.editoPais)
            {
                fila["Pais"] = frmEditar.comboBoxPaises.Text;
                fila["Nivel Riesgo"] = frmEditar.comboBoxNivelRiesgo.Text;
                fila["Observaciones"] = frmEditar.richTextBoxObservaciones.Text;
                dtgPaises.Refresh();
            }
        }

        private void EliminarPaisEdicion(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < tablaPaises.Rows.Count)
            {
                string pais = tablaPaises.Rows[rowIndex]["Pais"].ToString();

                FrmAlerta alerta = new FrmAlerta($"¿ESTÁ SEGURO DE ELIMINAR EL PAÍS '{pais}' DE LA LISTA?", "CONFIRMAR ELIMINACIÓN",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);



                if (alerta.ShowDialog() == DialogResult.Yes)
                {
                    tablaPaises.Rows.RemoveAt(rowIndex);
                }
            }
        }


        private void dataGridViewAgregarPaises_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string nombreColumna = dataGridViewAgregarPaises.Columns[e.ColumnIndex].Name;

                if (nombreColumna == "Editar")
                {
                    EditarPaisDesdeFormulario(e.RowIndex);
                }
                else if (nombreColumna == "Eliminar")
                {
                    EliminarPais(e.RowIndex);
                }
            }
        }

        private void panelBusqueda_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgPaises_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string nombreColumna = dtgPaises.Columns[e.ColumnIndex].Name;

                if (nombreColumna == "Editar")
                {
                    EditarPaisDesdeFormularioEdicion(e.RowIndex);
                }
                else if (nombreColumna == "Eliminar")
                {
                    EliminarPaisEdicion(e.RowIndex);
                }
            }
        }
    }
}
