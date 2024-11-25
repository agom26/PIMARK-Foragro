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

namespace Presentacion.Reportes
{
    public partial class FrmReportesMarcasPatentes : Form
    {
        MarcaModel marcamodel = new MarcaModel();
        public FrmReportesMarcasPatentes()
        {

            InitializeComponent();
            this.Load += FrmReportesMarcasPatentes_Load;
            SeleccionarPersonaReportes.LimpiarCliente();
            SeleccionarPersonaReportes.LimpiarTitular();
            SeleccionarPersonaReportes.LimpiarAgente();

        }
        private async void CrearPdfDesdeHtmlConLogoYDataTable(DataTable dt)
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

                // Obtener el contenido de la tabla desde el DataTable
                string tableContent = "";
                foreach (DataColumn column in dt.Columns)
                {
                    tableContent += $"<th>{column.ColumnName}</th>"; // Cabecera de la tabla
                }

                foreach (DataRow row in dt.Rows)
                {
                    tableContent += "<tr>";
                    foreach (DataColumn column in dt.Columns)
                    {
                        tableContent += $"<td>{row[column]}</td>"; // Datos de la fila
                    }
                    tableContent += "</tr>";
                }

                // HTML que deseas convertir a PDF con el logo y DataTable
                string html = $@"
        <html>
            <head>
                <style>
                    table {{ border-collapse: collapse; width: 100%; }}
                    th, td {{ border: 1px solid black; padding: 8px; text-align: left; }}
                    th {{ background-color: #f2f2f2; }}
                    img {{ width: 100px; height: 50px; }}
                </style>
            </head>
            <body>
                <h1>Reporte de Marcas y Patentes</h1>
                <img src='cid:logo' /> <!-- Aquí el logo -->
                <table>
                    <thead>
                        <tr>
                            {tableContent} <!-- Agregar las filas generadas dinámicamente -->
                        </tr>
                    </thead>
                </table>
            </body>
        </html>";

                // Establecer el contenido HTML
                await page.SetContentAsync(html);

                // Generar el PDF en la ruta seleccionada por el usuario
                string pdfFilePath = saveFileDialog.FileName;

                // Generar el PDF
                await page.PdfAsync(pdfFilePath, new PdfOptions
                {
                    Format = PaperFormat.A4,  // Tamaño A4
                    PrintBackground = true    // Incluir el fondo en la impresión
                });

                // Cerrar el navegador
                await browser.CloseAsync();

                // Confirmar que el PDF se ha generado
                MessageBox.Show($"PDF generado: {pdfFilePath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    break;
                case 1:
                    objeto = "internacional";
                    break;
                case 2:
                    objeto = "marca";
                    break;
                case 3:
                    objeto = "patentes";
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
                CrearPdfDesdeHtmlConLogoYDataTable(datos);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }
    }
}
