using Comun.Cache;
using Dominio;
using MySqlX.XDevAPI.Common;
using Presentacion.Alertas;
using Presentacion.Marcas_Internacionales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmTramiteIn : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        HistorialModel historialModel = new HistorialModel();
        private Form1 _form1;
        private int focoActual = 0; // 0: Día, 1: Mes, 2: Año
        //valores
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento;
        // Estas variables las declaras en el formulario (nivel de clase)
        private string rutaArchivoLocal = null;
        private string nombreArchivo = null;
        private bool archivoSeleccionado = false;
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }

        public FrmTramiteIn(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            this.AutoScroll = true;
            panel2I.Visible = false;
            ActualizarFechaVencimiento();
            checkBox1.Checked = false;
            checkBox1.Enabled = false;
            this.Load += FrmTramiteIn_Load;
            mostrarPanelRegistro();
            archivoSeleccionado = false;

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

        private bool ValidarCampos(string pais, string expediente, string nombre, string clase, string signoDistintivo, string tipo, string estado,
    ref byte[] logo, bool registroChek, string registro, string folio, string libro)
        {
            // Verificar campos obligatorios
            if (!ValidarCampo(expediente, "Por favor, ingrese un pais.") ||
                !ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
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
                FrmAlerta alerta = new FrmAlerta("LA CLASE, FOLIO Y TOMO\n DEBEN SER VALORES NUMÉRICOS ENTEROS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("El expediente, clase, folio, registro y libro deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBoxSignoDistintivo.SelectedItem.ToString() == "Marca" &&
              comboBoxTipoSigno.SelectedItem.ToString() == "Gráfica/Figurativa" || comboBoxTipoSigno.SelectedItem.ToString() == "Mixta")
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
                if (!ValidarCampo(folio, "Por favor, ingrese el número de folio.") ||
                    !ValidarCampo(registro, "Por favor, ingrese el número de registro.") ||
                    !ValidarCampo(libro, "Por favor, ingrese el número de tomo.")
                    )
                {
                    return false;
                }
            }

            return true;
        }

        public bool SubirArchivoPorPhp(int idMarca)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        // Agregamos el ID de la marca
                        form.Add(new StringContent(idMarca.ToString()), "idMarca");

                        // Agregamos el archivo (mejor con FileStream)
                        using (var fileStream = new FileStream(rutaArchivoLocal, FileMode.Open, FileAccess.Read))
                        {
                            var streamContent = new StreamContent(fileStream);
                            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                            form.Add(streamContent, "archivo", nombreArchivo);

                            // URL de tu PHP
                            string url = "https://bpa.com.es/subir_archivo_marca_internacional_tramite_inicial.php"; // Asegúrate de que esta URL sea la correcta

                            // Realizamos la solicitud
                            var responseTask = client.PostAsync(url, form);
                            responseTask.Wait();

                            var response = responseTask.Result;
                            var responseContent = response.Content.ReadAsStringAsync().Result;

                            if (response.IsSuccessStatusCode)
                            {
                                //FrmAlerta alerta = new FrmAlerta("ARCHIVO SUBIDO CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //alerta.ShowDialog();
                                return true;
                            }
                            else
                            {
                                FrmAlerta alerta = new FrmAlerta("ERROR AL SUBIR EL ARCHIVO: " + response.StatusCode.ToString() + "\n" + responseContent, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                alerta.ShowDialog();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL SUBIR EL ARCHIVO: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                return false;
            }
        }

        public async Task GuardarMarcaInternacional()
        {
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string paisRegistro = comboBox1.SelectedItem?.ToString();
            string signoDistintivo = comboBoxSignoDistintivo.SelectedItem?.ToString(); // Suponiendo que esto es un ComboBox
            string tipo = comboBoxTipoSigno.SelectedItem?.ToString(); // Suponiendo que esto es un ComboBox
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[] logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            int? idCliente = SeleccionarPersona.idPersonaC;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;
            string tiene_poder = "no";


            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;

            if (checkBoxTienePoder.Checked)
            {
                tiene_poder = "si";
            }
            else
            {
                tiene_poder = "no";
            }

            // Validaciones
            if (idTitular <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN TITULAR VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un titular válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idAgente <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN AGENTE VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un agente válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idCliente == null || idCliente <= 0)
            {
                idCliente = null;
            }

            // Validar campos 
            if (!ValidarCampos(paisRegistro, expediente, nombre, clase, signoDistintivo, tipo, estado, ref logo, registroChek, registro, folio, libro))
            {
                return;
            }



            // Guardar la marca internacional
            try
            {
                int idMarca = registroChek ?
                    await marcaModel.AddMarcaInternacionalRegistrada(expediente, nombre, signoDistintivo, tipo, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente, registro, folio, libro, fecha_registro, fecha_vencimiento) :
                    await marcaModel.AddMarcaInternacional(expediente, nombre, signoDistintivo, tipo, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente);

                if (idMarca > 0)
                {

                    string etapa = textBoxEstatus.Text;
                    if (!string.IsNullOrEmpty(etapa))
                    {
                        historialModel.GuardarEtapa(idMarca, AgregarEtapa.fecha.Value, etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario, "TRÁMITE");
                    }


                    // Subir archivo si fue seleccionado
                    if (archivoSeleccionado)
                    {
                        bool exito = SubirArchivoPorPhp(idMarca);
                        if (!exito)
                        {
                            FrmAlerta alertaError = new FrmAlerta("ERROR AL SUBIR EL ARCHIVO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alertaError.ShowDialog();
                        }
                    }

                    FrmAlerta alerta = new FrmAlerta("MARCA INTERNACIONAL " + (registroChek ? "REGISTRADA" : "GUARDADA"), "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show("Marca nacional " + (registroChek ? "registrada" : "guardada") + " con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("ERROR AL " + (registroChek ? "REGISTRAR" : "GUARDAR") + " LA MARCA INTERNACIONAL.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                    //MessageBox.Show("Error al " + (registroChek ? "registrar" : "guardar") + " la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL " + (registroChek ? "REGISTRAR" : "GUARDAR") + " LA MARCA INTERNACIONAL." + ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                //MessageBox.Show("Error al " + (registroChek ? "registrar" : "guardar") + " la marca nacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LimpiarFormulario()
        {
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = documento;

            txtNombreTitular.Text = "";
            txtNombreAgente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            checkBox1_CheckedChanged(checkBox1, EventArgs.Empty);
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            AgregarEtapa.LimpiarEtapa();
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            checkBoxTienePoder.Checked = false;
            SeleccionarPersona.idPersonaT = 0;
            SeleccionarPersona.idPersonaA = 0;
            SeleccionarPersona.idPersonaC = 0;
            btnAdjuntarT.Visible = false;
            archivoSeleccionado = false;
        }

        public void mostrarPanelRegistro()
        {

            int espaciadoBotones = 20;

            if (textBoxEstatus.Text == "Registrada")
            {
                txtRegistro.Text = "";
                txtLibro.Text = "";
                txtFolio.Text = "";
                dateTimePFecha_Registro.Value = DateTime.Now;
                ActualizarFechaVencimiento();
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel2I.Visible = true;
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 64.69f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 35.31f;
                btnAdjuntarT.Visible = true;

            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel2I.Visible = false;
                tableLayoutPanel1.RowStyles[0].Height = 0;
                btnAdjuntarT.Visible = false;
            }
        }





        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmMostrarTitulares frmMostrarTitulares = new FrmMostrarTitulares();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersona.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersona.nombre;
                //txtDireccionTitular.Text = SeleccionarPersona.direccion;
                //txtEntidadTitular.Text = SeleccionarPersona.pais;
            }
            else
            {

                txtNombreTitular.Text = "";
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
            else
            {
                txtNombreAgente.Text = "";
            }

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            //Agregar una imagen al cuadro de imagen para la foto del usuario.
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images (*.jpg;*.jpeg;*.png;*.tiff)|*.jpg;*.jpeg;*.png;*.tiff";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //Borrar foto del usuario
            convertirImagen();
            pictureBox1.Image = documento;
        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {


        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtDireccionTitular_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePFecha_vencimiento_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
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
        private void roundedButton3_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            FrmAgregarEtapa frmAgregarEtapa = new FrmAgregarEtapa();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                textBoxEstatus.Text = AgregarEtapa.etapa;
                mostrarPanelRegistro();
                richTextBox1.Text = AgregarEtapa.anotaciones;
                VerificarDatosRegistro();

                if (comboBoxSignoDistintivo.Text == "Nombre comercial" && textBoxEstatus.Text == "Registrada")
                {
                    dateTimePFecha_vencimiento.Enabled = true;
                }
                else
                {
                    if (UsuarioActivo.isAdmin)
                    {
                        dateTimePFecha_vencimiento.Enabled = true;
                    }
                    else
                    {
                        dateTimePFecha_vencimiento.Enabled = false;

                    }
                }
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void FrmTramiteIn_Load(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
            convertirImagen();
            pictureBox1.Image = documento;
            SeleccionarPersona.idPersonaA = 0;
            SeleccionarPersona.idPersonaT = 0;
            SeleccionarPersona.idPersonaC = 0;
            mostrarPanelRegistro();
        }

        private void roundedButton4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
                    await GuardarMarcaInternacional();
                }
                    
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }


        private void CentrarPanel()
        {
            // Si el formulario es más ancho que el panel → centrar horizontalmente
            if (this.ClientSize.Width > panel1.Width)
            {
                // Solo si no está ya centrado
                if (panel1.Anchor != AnchorStyles.Top || panel1.Dock != DockStyle.None)
                {
                    panel1.Anchor = AnchorStyles.Top;
                    panel1.Dock = DockStyle.None;
                }

                int x = (this.ClientSize.Width - panel1.Width) / 2;
                int y = panel1.Location.Y;
                panel1.Location = new Point(x, y);
            }
            // Si el formulario es más angosto que el panel → ubicar arriba a la izquierda
            else if (panel1.Location != new Point(0, panel1.Location.Y) || panel1.Anchor != (AnchorStyles.Top | AnchorStyles.Left))
            {
                panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panel1.Dock = DockStyle.None;
                panel1.Location = new Point(0, panel1.Location.Y);
            }
        }
        private void btnCancelarM_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == true)
            {
                FrmAlerta alerta = new FrmAlerta("LA MARCA NO FUE INGRESADA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
            else
            {

            }
            LimpiarFormulario();
            DatosRegistro.peligro = false;
            //llamar a DashboardPrincipal
            _form1.cargarDashboard();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtEntidadTitular_TextChanged(object sender, EventArgs e)
        {

        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {

        }

        private void txtRegistro_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && string.IsNullOrEmpty(txtRegistro.Text))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;

            }
        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && string.IsNullOrEmpty(txtFolio.Text))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }

        private void txtLibro_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && string.IsNullOrEmpty(txtLibro.Text))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;

            }
        }

        private void textBoxEstatus_TextChanged(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
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

        private void dateTimePFecha_Registro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{RIGHT}");
                // Código a ejecutar cuando se presiona la tecla Tabulador
                MessageBox.Show("Se presionó la tecla Tabulador");
            }
        }

        private void dateTimePFecha_Registro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Tab)
            {

                SendKeys.Send("{RIGHT}");

                MessageBox.Show("Hola");
            }

        }

        private void dateTimePFecha_Registro_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePFecha_Registro_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSignoDistintivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSignoDistintivo.Text == "Nombre comercial" && textBoxEstatus.Text == "Registrada")
            {
                dateTimePFecha_vencimiento.Enabled = true;
            }
            else
            {
                dateTimePFecha_vencimiento.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmTramiteIn_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private void btnAdjuntarT_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Todos los archivos (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivoLocal = openFileDialog.FileName;
                nombreArchivo = Path.GetFileName(rutaArchivoLocal);

                // Validamos el tamaño máximo (20 MB)
                FileInfo fileInfo = new FileInfo(rutaArchivoLocal);
                long tamanioEnBytes = fileInfo.Length;
                long maxTamanio = 20 * 1024 * 1024; // 20 MB

                if (tamanioEnBytes > maxTamanio)
                {
                    FrmAlerta alerta = new FrmAlerta("EL archivo seleccionado supera el tamaño máximo permitido de 20 MB.",
                        "Archivo demasiado grande", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();

                    // Reiniciar selección
                    rutaArchivoLocal = null;
                    nombreArchivo = null;
                    archivoSeleccionado = false;
                    return;
                }

                archivoSeleccionado = true;

                FrmAlerta alerta2 = new FrmAlerta("ARCHIVO SELECCIONADO", "ARCHIVO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta2.ShowDialog();

            }
        }
    }
}
