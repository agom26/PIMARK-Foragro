using ClosedXML.Excel;
using Comun.Cache;
using Dominio;
using PdfSharp.Drawing;
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

namespace Presentacion.Reportes
{
    public partial class FrmReportesMarcasPatentes : Form
    {
        public int numRegistros = 0;
        public float escala = 0;
        MarcaModel marcamodel = new MarcaModel();
        public FrmReportesMarcasPatentes()
        {

            InitializeComponent();
            this.Load += FrmReportesMarcasPatentes_Load;
            SeleccionarPersonaReportes.LimpiarCliente();
            SeleccionarPersonaReportes.LimpiarTitular();
            SeleccionarPersonaReportes.LimpiarAgente();

        }
        private async void CrearPdfDesdeHtmlConLogoYDataTable(DataTable dt,int registrosPagina,float escalas)
        {
            // Ruta al ejecutable de Chrome en tu sistema
            string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"; // Cambia la ruta según tu instalación

            // Abre un SaveFileDialog para que el usuario seleccione la ruta de guardado
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = "ReporteMarcasYPatentes.pdf"
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
                            tableContent += $"<td style='padding: 8px; text-align: left; border: 1px solid #ddd;'>{row[column]}</td>";
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
                            width: 200px; /* Tamaño del logo */
                            height: auto; /* Altura automática */
                        }}
                        @page {{
                            size: legal landscape; /* Configura tamaño legal y orientación horizontal */
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
                        Reportes
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
                    <div style='page-break-before: always;'></div> <!-- Salto de página para separar los contenidos -->
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
                MessageBox.Show("PDF generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se seleccionó ninguna ruta para guardar el PDF.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }





        public void ExportarDataTableAExcel(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
            {
                Title = "Guardar archivo Excel",
                Filter = "Archivos Excel (*.xlsx)|*.xlsx",
                FileName = "Reporte.xlsx",
                DefaultExt = "xlsx",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Reporte");
                        worksheet.Cell(1, 1).InsertTable(dataTable);

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(saveFileDialog.FileName);
                    }
                    FrmAlerta alerta = new FrmAlerta("ARCHIVO GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show($"Archivo Excel generado exitosamente en: {saveFileDialog.FileName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el archivo: {ex.Message}");
                }
            }
        }

        




        public void Filtrar()
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
                    numRegistros = 11;
                    escala = 0.85f;
                    break;
                case 1:
                    objeto = "internacional";
                    numRegistros = 15;
                    escala = 0.75f;
                    break;
                case 2:
                    objeto = "marca";
                    numRegistros = 15;
                    escala = 0.75f;
                    break;
                case 3:
                    objeto = "patentes";
                    numRegistros = 15;
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
                pais = comboBoxPais.SelectedIndex.ToString();
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

            if (checkBoxReigstro.Checked)
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
                fechaSolicitudInicio = dtpFRegistroInicial.Value.ToString("yyyy-MM-dd");
                fechaSolicitudFin = dtpFechaRegistroFinal.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                fechaSolicitudInicio = null;
                fechaSolicitudFin = null;
            }

            if (checkBoxRegistro.Checked)
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

            dtgReportes.DataSource = marcamodel.Filtrar(objeto, estado, nombre, pais,
                folio, tomo, numRegistro, clase, fechaSolicitudInicio, fechaSolicitudFin,
                fechaRegistroInicio, fechaRegistroFin, fechaVencimientoInicio, fechaVencimientoFinal,
                titular, agente, cliente
                );
            dtgReportes.ClearSelection();

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
            if (seleccion == "Marcas nacionales" || seleccion == "Marcas internacionales" || seleccion == "Marcas nacionales e internacionales")
            {
                comboBoxEstado.Items.Clear();
                comboBoxEstado.Items.Add("");
                comboBoxEstado.Items.Add("Ingresada");
                comboBoxEstado.Items.Add("Examen de forma");
                comboBoxEstado.Items.Add("Examen de fondo");
                comboBoxEstado.Items.Add("Requerimiento");
                comboBoxEstado.Items.Add("Objeción");
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
            }
        }

        private void FrmReportesMarcasPatentes_Load(object sender, EventArgs e)
        {
            comboBoxObjeto.SelectedIndex = 0;
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
                CrearPdfDesdeHtmlConLogoYDataTable(datos,numRegistros,escala);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }
    }
}
