using ClosedXML.Excel;
using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PuppeteerSharp;
using System.Diagnostics;
using PuppeteerSharp.Media;
using System.Security.RightsManagement;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Presentacion.Reportes
{
    public partial class FrmReportesMarcasPatentes : Form
    {
        public int numRegistros = 0;
        public float escala = 0;
        MarcaModel marcamodel = new MarcaModel();
        string titulo;

        public FrmReportesMarcasPatentes()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Load += FrmReportesMarcasPatentes_Load;
            this.Resize += FrmReportesMarcasPatentes_Resize;
            SeleccionarPersonaReportes.LimpiarCliente();
            SeleccionarPersonaReportes.LimpiarTitular();
            SeleccionarPersonaReportes.LimpiarAgente();
            SetDoubleBuffering(this, true);
            SetDoubleBuffering(dtgReportes, true);
        }

        private void SetDoubleBuffering(System.Windows.Forms.Control control, bool enable)
        {
            // Habilitar o deshabilitar DoubleBuffering
            typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                           .SetValue(control, enable, null);
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

                    // Generar los encabezados de la tabla
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
                 width: 100px;
                 height: auto;
             }}
             @page {{
                 size: legal landscape;
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
             {titulo}
         </div>
         <div class='fecha'>
             <center>
                 Fecha: {DateTime.Now.ToString("dd-MM-yyyy")}
             </center>
         </div>
         {imageHtml}
         <table>
             <thead>
                 <tr>
                     {headers}
                 </tr>
             </thead>
             <tbody>
                 {tableContent}
             </tbody>
         </table>
         {(pagina < totalPaginas - 1 ? "<div style='page-break-before: always;'></div>" : "")}
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
            }
            else
            {
                MessageBox.Show("No se seleccionó ninguna ruta para guardar el PDF.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        /*
        public void ExportarDataTableAExcel(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }
            string nombre = titulo+"-" + DateTime.Now.ToString("dd-MM-yyyy");

            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
            {
                Title = "Guardar archivo Excel",
                Filter = "Archivos Excel (*.xlsx)|*.xlsx",
                FileName = nombre+".xlsx",
                DefaultExt = "xlsx",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string tempLogoPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_logo.png");

                    // Guardar el recurso de imagen en un archivo temporal
                    Properties.Resources.logoBPA.Save(tempLogoPath);

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(titulo);
                        // Fecha actual en el formato deseado
                        string fecha = DateTime.Now.ToString("dd-MM-yyyy");

                        // Insertar el título "Próximos vencimientos" en la celda A1
                        worksheet.Cell(3, 5).Value = titulo;
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Style.Font.Underline = XLFontUnderlineValues.Single;
                        worksheet.Cell(3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;  // Centrar el título

                        // Insertar la fecha debajo del título (en la celda A2)
                        worksheet.Cell(4, 5).Value = "Fecha: " + fecha;
                        worksheet.Cell(4, 5).Style.Font.Italic = true;
                        worksheet.Cell(4, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;  // Centrar la fecha

                        worksheet.Column(1).AdjustToContents();
                        // Agregar logo antes de la tabla
                        if (System.IO.File.Exists(tempLogoPath))
                        {
                            var image = worksheet.AddPicture(tempLogoPath)
                                .MoveTo(worksheet.Cell(3, 1)) // Posición del logo
                                .Scale(0.5); // Ajustar tamaño
                        }

                        // Insertar tabla después del logo
                        int startRow = 10; // Ajustar según el espacio requerido
                        worksheet.Cell(startRow, 1).InsertTable(dataTable);

                        // Ajustar ancho de las columnas
                        worksheet.Columns().AdjustToContents();

                        // Guardar archivo
                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    // Eliminar archivo temporal
                    if (System.IO.File.Exists(tempLogoPath))
                        System.IO.File.Delete(tempLogoPath);

                    FrmAlerta alerta = new FrmAlerta("ARCHIVO GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el archivo: {ex.Message}");
                }
            }
        }
        */
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
                        var worksheet = workbook.Worksheets.Add("REPORTE");

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






        public async void Filtrar()
        {
            string objeto = null;
            string? estado = null;
            string? nombre = null;
            string? pais = null;
            string? folio = null;
            string? tomo = null;
            string? numRegistro = null;
            string? clase = null;
            string? fechaSolicitudInicio = null;
            string? fechaSolicitudFin = null;
            string? fechaRegistroInicio = null;
            string? fechaRegistroFin = null;
            string? fechaVencimientoInicio = null;
            string? fechaVencimientoFinal = null;
            string? titular = null;
            string? agente = null;
            string? cliente = null;


            switch (comboBoxObjeto.SelectedIndex)
            {
                case 0:
                    objeto = "nacional";
                    numRegistros = 9;
                    escala = 0.85f;
                    break;
                case 1:
                    objeto = "internacional";
                    numRegistros = 13;
                    escala = 0.75f;
                    break;
                case 2:
                    objeto = "marca";
                    numRegistros = 13;
                    escala = 0.75f;
                    break;
                case 3:
                    objeto = "patentes";
                    numRegistros = 13;
                    escala = 0.7f;
                    break;
                case 4:
                    objeto = "v_internacionales";
                    numRegistros = 13;
                    escala = 0.7f;
                    break;
                case 5:
                    objeto = "v_nacionales";
                    numRegistros = 9;
                    escala = 0.85f;
                    break;
                case 6:
                    objeto = "v_marcas";
                    numRegistros = 13;
                    escala = 0.75f;
                    break;
                case 7:
                    objeto = "v_patentes";
                    numRegistros = 13;
                    escala = 0.7f;
                    break;
            }

            if (checkBoxEstado.Checked)
            {
                estado = comboBoxEstado.SelectedItem.ToString();
            }
            else
            {
                estado = null;
            }

            if (checkBoxNombre.Checked)
            {
                nombre = txtNombre.Text;
            }
            else
            {
                nombre = null;
            }

            if (checkBoxPais.Checked)
            {
                pais = comboBoxPais.SelectedItem.ToString();
            }
            else
            {
                pais = null;
            }

            if (checkBoxFolio.Checked)
            {
                folio = txtFolio.Text;
            }
            else
            {
                folio = null;
            }

            if (checkBoxTomo.Checked)
            {
                tomo = txtTomo.Text;
            }
            else
            {
                tomo = null;
            }

            if (checkBoxRegistro.Checked)
            {
                numRegistro = txtRegistro.Text;
            }
            else
            {
                numRegistro = null;
            }

            if (checkBoxClase.Checked)
            {
                clase = txtClase.Text;
            }
            else
            {
                clase = null;
            }

            if (checkBoxTitular.Checked)
            {
                titular = richTextBoxTitular.Text;
            }
            else
            {
                titular = null;
            }

            if (checkBoxAgente.Checked)
            {
                agente = richTextBoxAgente.Text;
            }
            else
            {
                agente = null;
            }

            if (checkBoxCliente.Checked)
            {
                cliente = richTextBoxCliente.Text;
            }
            else
            {
                cliente = null;
            }

            if (checkBoxSolicitud.Checked)
            {
                fechaSolicitudInicio = dtpSolicitudInicial.Value.ToString("yyyy-MM-dd");
                fechaSolicitudFin = dtpSolicitudFinal.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaSolicitudInicio = null;
                fechaSolicitudFin = null;
            }

            if (checkBoxReigstro.Checked)
            {
                fechaRegistroInicio = dtpFRegistroInicial.Value.ToString("yyyy-MM-dd");
                fechaRegistroFin = dtpFechaRegistroFinal.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaRegistroInicio = null;
                fechaRegistroFin = null;
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
            dtgReportes.SuspendLayout();

            dtgReportes.DataSource = await marcamodel.Filtrar(objeto, estado, nombre, pais,
                folio, tomo, numRegistro, clase, fechaSolicitudInicio, fechaSolicitudFin,
                fechaRegistroInicio, fechaRegistroFin, fechaVencimientoInicio, fechaVencimientoFinal,
                titular, agente, cliente
                );
            dtgReportes.ClearSelection();
            dtgReportes.ResumeLayout();


        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtRegistro_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void comboBoxObjeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = comboBoxObjeto.SelectedItem.ToString();
            titulo = "REPORTE DE " + seleccion.ToUpper();
            if (seleccion == "Marcas nacionales" || seleccion == "Marcas internacionales" || seleccion == "Marcas nacionales e internacionales")
            {
                comboBoxEstado.Items.Clear();
                comboBoxEstado.Items.Add("");
                comboBoxEstado.Items.Add("Ingresada");
                comboBoxEstado.Items.Add("Examen de forma");
                comboBoxEstado.Items.Add("Examen de fondo");
                comboBoxEstado.Items.Add("Requerimiento");
                comboBoxEstado.Items.Add("Objeción");
                comboBoxEstado.Items.Add("Resolución RPI favorable");
                comboBoxEstado.Items.Add("Resolución RPI desfavorable");
                comboBoxEstado.Items.Add("Recurso de revocatoria");
                comboBoxEstado.Items.Add("Resolución Ministerio de Economía (MINECO)");
                comboBoxEstado.Items.Add("Contencioso administrativo");
                comboBoxEstado.Items.Add("Edicto");
                comboBoxEstado.Items.Add("Publicación");
                comboBoxEstado.Items.Add("Oposición");
                comboBoxEstado.Items.Add("Orden de pago");
                comboBoxEstado.Items.Add("Abandono");
                comboBoxEstado.Items.Add("Registrada");
                comboBoxEstado.Items.Add("Licencia de uso");
                comboBoxEstado.Items.Add("Trámite de renovación");
                comboBoxEstado.Items.Add("Trámite de traspaso");

            }
            else if (seleccion == "Patentes")
            {

                comboBoxEstado.Items.Clear();
                comboBoxEstado.Items.Add("");
                comboBoxEstado.Items.Add("Ingresada");
                comboBoxEstado.Items.Add("Examen de forma");
                comboBoxEstado.Items.Add("Examen de publicación");
                comboBoxEstado.Items.Add("Edicto");
                comboBoxEstado.Items.Add("Examen de fondo");
                comboBoxEstado.Items.Add("Prórroga");
                comboBoxEstado.Items.Add("Rechazo");
                comboBoxEstado.Items.Add("Registro/concesión");
                comboBoxEstado.Items.Add("Modificación");
                comboBoxEstado.Items.Add("Conversión de solicitud");
                comboBoxEstado.Items.Add("Corrección del certificado o inscripción");
                comboBoxEstado.Items.Add("Trámite de renovación");
                comboBoxEstado.Items.Add("Trámite de traspaso");
                comboBoxEstado.Items.Add("Abandono");
            }
        }

        private async void FrmReportesMarcasPatentes_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            dtgReportes.DataSource = null;
            dtgReportes.ClearSelection();
            SeleccionarPersonaReportes.LimpiarCliente();
            SeleccionarPersonaReportes.LimpiarTitular();
            SeleccionarPersonaReportes.LimpiarAgente();
        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesReportes frmMostrarTitulares = new FrmMostrarTitularesReportes();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersonaReportes.nombreTitular != "")
            {
                richTextBoxTitular.Text = SeleccionarPersonaReportes.nombreTitular;
            }
            else
            {
                richTextBoxTitular.Text = "";
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentesReportes frmMostrarAgentes = new FrmMostrarAgentesReportes();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersonaReportes.nombreAgente != "")
            {
                richTextBoxAgente.Text = SeleccionarPersonaReportes.nombreAgente;
            }
            else
            {
                richTextBoxAgente.Text = "";
            }
        }

        private void roundedButton5_Click(object sender, EventArgs e)
        {
            FrmMostrarClientesReportes frmMostrarClientes = new FrmMostrarClientesReportes();
            frmMostrarClientes.ShowDialog();

            if (SeleccionarPersonaReportes.nombreCliente != "")
            {
                richTextBoxCliente.Text = SeleccionarPersonaReportes.nombreCliente;
            }
            else
            {
                richTextBoxCliente.Text = "";
            }
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {

            DataTable datos = dtgReportes.DataSource as DataTable;

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



        private void roundedButton3_Click(object sender, EventArgs e)
        {
            DataTable datos = dtgReportes.DataSource as DataTable;

            if (datos != null)
            {
                CrearPdfDesdeHtmlConLogoYDataTable(datos, numRegistros, escala, titulo);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        private void FrmReportesMarcasPatentes_Click(object sender, EventArgs e)
        {

        }

        private void dtpFRegistroInicial_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpVencimientoFinal_ValueChanged(object sender, EventArgs e)
        {

        }

        private void richTextBoxTitular_TextChanged(object sender, EventArgs e)
        {

        }

        private void roundedButton7_Click(object sender, EventArgs e)
        {

        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpFechaRegistroFinal_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpVencimientoInicial_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBoxCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpSolicitudInicial_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CentrarTableLayoutReporte()
        {
            int anchoMinimo = 862 + 100;

            if (this.ClientSize.Width >= anchoMinimo)
            {
                int x = (this.ClientSize.Width - tableLayoutPanelReportes.Width) / 2;
                int y = tableLayoutPanelReportes.Location.Y;

                tableLayoutPanelReportes.Location = new System.Drawing.Point(x, y);
            }
            else
            {
                tableLayoutPanelReportes.Location = new System.Drawing.Point(0, tableLayoutPanelReportes.Location.Y);
            }
        }


        /*
        private void CentrarTableLayoutReporte()
        {
            int anchoMinimo = 862 + 100;

            if (this.ClientSize.Width >= anchoMinimo)
            {
                // Centrar horizontalmente
                tableLayoutPanelReportes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                int x = (this.ClientSize.Width - tableLayoutPanelReportes.Width) / 2;
                int y = tableLayoutPanelReportes.Location.Y; // mantener posición vertical

                tableLayoutPanelReportes.Location = new System.Drawing.Point(x, y);
            }
            else
            {
                // Pantalla más pequeña → alinear arriba a la izquierda
                tableLayoutPanelReportes.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                tableLayoutPanelReportes.Location = new System.Drawing.Point(0, tableLayoutPanelReportes.Location.Y);
            }
        }*/
        /*
        private void CentrarDataGridView()
        {
            int anchoMinimo = tableLayoutPanelReportes.Width + 100;

            if (this.ClientSize.Width >= anchoMinimo)
            {
                panelDataGridView.Width = tableLayoutPanelReportes.Width;
                panelDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                int x = (this.ClientSize.Width - panelDataGridView.Width) / 2;
                panelDataGridView.Location = new System.Drawing.Point(x, panelDataGridView.Location.Y);
            }
            else
            {
                panelDataGridView.Width = tableLayoutPanelReportes.Width;
                panelDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panelDataGridView.Location = new System.Drawing.Point(0, panelDataGridView.Location.Y);
            }
        }*/
        private void CentrarDataGridView()
        {
            panelDataGridView.Width = tableLayoutPanelReportes.Width;

            int x = tableLayoutPanelReportes.Location.X;
            int y = panelDataGridView.Location.Y;

            panelDataGridView.Location = new System.Drawing.Point(x, y);
        }





        private void FrmReportesMarcasPatentes_Resize(object sender, EventArgs e)
        {
            CentrarTableLayoutReporte();
            CentrarDataGridView();
            CentrarDataGridDentroDelPanel(); // <<--- Agregado

            PosicionarPanelDebajoDerecha();
        }

        private async void FrmReportesMarcasPatentes_Shown(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            comboBoxObjeto.SelectedIndex = 0;
            await Task.Delay(800);
            tabControl1.Visible = true;

            CentrarTableLayoutReporte();
            CentrarDataGridView();
            CentrarDataGridDentroDelPanel(); // <<--- Agregado

            PosicionarPanelDebajoDerecha();
        }

        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            dtgReportes.DataSource = null;
            dtgReportes.ClearSelection();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            DataTable datos = dtgReportes.DataSource as DataTable;

            if (datos != null)
            {
                CrearPdfDesdeHtmlConLogoYDataTable(datos, numRegistros, escala, titulo);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            DataTable datos = dtgReportes.DataSource as DataTable;

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

        private void btnTitularReporte_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesReportes frmMostrarTitulares = new FrmMostrarTitularesReportes();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersonaReportes.nombreTitular != "")
            {
                richTextBoxTitular.Text = SeleccionarPersonaReportes.nombreTitular;
            }
            else
            {
                richTextBoxTitular.Text = "";
            }
        }

        private void btnAgenteReporte_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentesReportes frmMostrarAgentes = new FrmMostrarAgentesReportes();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersonaReportes.nombreAgente != "")
            {
                richTextBoxAgente.Text = SeleccionarPersonaReportes.nombreAgente;
            }
            else
            {
                richTextBoxAgente.Text = "";
            }
        }

        private void PosicionarPanelDebajoDerecha()
        {
            // Obtener posición absoluta de tableLayoutPanel2
            System.Drawing.Point punto1 = tableLayoutPanelReportes.Location;
            int x1 = punto1.X + tableLayoutPanelReportes.Width - panelBotones.Width;
            int y1 = punto1.Y + tableLayoutPanelReportes.Height + 10; // separacion de 10 px abajo
            panelBotones.Location = new System.Drawing.Point(x1, y1);

            // Obtener posición absoluta de panel23
            System.Drawing.Point punto2 = panelDataGridView.Location;
            int x2 = punto2.X + panelDataGridView.Width - panelBotones2.Width;
            int y2 = punto2.Y + panelDataGridView.Height + 10;
            panelBotones2.Location = new System.Drawing.Point(x2, y2);
        }
        private void CentrarDataGridDentroDelPanel()
        {
            if (panelDataGridView.Width > dtgReportes.Width)
            {
                int x = (panelDataGridView.Width - dtgReportes.Width) / 2;
                int y = dtgReportes.Location.Y;
                dtgReportes.Location = new System.Drawing.Point(x, y);
            }
            else
            {
                // Si el DataGridView ocupa todo el ancho, alinearlo a la izquierda
                dtgReportes.Location = new System.Drawing.Point(0, dtgReportes.Location.Y);
            }
        }


        private void roundedButton1_Click_1(object sender, EventArgs e)
        {
            FrmMostrarClientesReportes frmMostrarClientes = new FrmMostrarClientesReportes();
            frmMostrarClientes.ShowDialog();

            if (SeleccionarPersonaReportes.nombreCliente != "")
            {
                richTextBoxCliente.Text = SeleccionarPersonaReportes.nombreCliente;
            }
            else
            {
                richTextBoxCliente.Text = "";
            }
        }
    }
}
