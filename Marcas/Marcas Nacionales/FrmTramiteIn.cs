using Comun.Cache;
using Dominio;
using MySqlX.XDevAPI.Common;
using Presentacion.Alertas;
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

        //valores
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento;
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }

        public FrmTramiteIn()
        {
            InitializeComponent();
            int x = (panel17.Size.Width - label30.Size.Width - iconPictureBox3.Size.Width) / 2;
            int y = (panel17.Size.Height - label30.Size.Height) / 2;
            panel18.Location = new Point(x, y);

            int x2 = (panel15.Size.Width - label29.Size.Width) / 2;
            int y2 = (panel15.Size.Height - label29.Size.Height) / 2;
            panel16.Location = new Point(x2, y2);
            iconPictureBox3.IconSize = 25;
            panel2I.Visible = false;
            ActualizarFechaVencimiento();
            checkBox1.Checked = false;
            checkBox1.Enabled = false;
            this.Load += FrmTramiteIn_Load;
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
                !ValidarCampo(nombre, "Por favor, ingrese el nombre.") ||
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
                (registroChek && !int.TryParse(registro, out _)) ||
                (registroChek && !int.TryParse(folio, out _)) ||
                (registroChek && !int.TryParse(libro, out _)))
            {
                FrmAlerta alerta = new FrmAlerta("LA CLASE, FOLIO, REGISTRO Y TOMO\n DEBEN SER VALORES NUMÉRICOS ENTEROS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("El expediente, clase, folio, registro y libro deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
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
                FrmAlerta alerta = new FrmAlerta("INGRESE UNA IMAGEN", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, ingrese una imagen.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
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

        public void GuardarMarcaNacional()
        {
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string signoDistintivo = comboBoxSignoDistintivo.SelectedItem?.ToString(); // Suponiendo que esto es un ComboBox
            string tipo = comboBoxTipoSigno.SelectedItem?.ToString(); // Suponiendo que esto es un ComboBox
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[] logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;


            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;

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

            // Validar campos 
            if (!ValidarCampos(expediente, nombre, clase, signoDistintivo, tipo, estado, ref logo, registroChek, registro, folio, libro))
            {
                return;
            }



            // Guardar la marca
            try
            {
                int idMarca = registroChek ?
                    marcaModel.AddMarcaNacionalRegistrada(expediente, nombre, signoDistintivo, tipo, clase, folio, libro, logo, idTitular, idAgente, solicitud, registro, fecha_registro, fecha_vencimiento) :
                    marcaModel.AddMarcaNacional(expediente, nombre, signoDistintivo, tipo, clase, logo, idTitular, idAgente, solicitud);

                // Verifica si se ha guardado correctamente
                if (idMarca > 0)
                {
                    string etapa = textBoxEstatus.Text;
                    if (!string.IsNullOrEmpty(etapa))
                    {
                        historialModel.GuardarEtapa(idMarca, AgregarEtapa.fecha.Value, etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                    }
                    FrmAlerta alerta = new FrmAlerta("MARCA NACIONAL " + (registroChek ? "REGISTRADA" : "GUARDADA"), "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show("Marca nacional " + (registroChek ? "registrada" : "guardada") + " con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("ERROR AL " + (registroChek ? "REGISTRAR" : "GUARDAR") + " LA MARCA NACIONAL.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                    //MessageBox.Show("Error al " + (registroChek ? "registrar" : "guardar") + " la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL " + (registroChek ? "REGISTRAR" : "GUARDAR") + " LA MARCA NACIONAL.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtDireccionTitular.Text = "";
            txtEntidadTitular.Text = "";
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
            SeleccionarPersona.idPersonaT = 0;
            SeleccionarPersona.idPersonaA = 0;
            //btnGuardarM.Location = new Point(105, 950);
            //btnCancelarM.Location = new Point(413, 950);
        }

        public void mostrarPanelRegistro()
        {
          
            int espaciadoBotones = 20;  

            if (textBoxEstatus.Text == "Registrada")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel2I.Visible = true;

                
               
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel2I.Visible = false;

               
            }
        }





        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmMostrarTitulares frmMostrarTitulares = new FrmMostrarTitulares();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersona.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersona.nombre;
                txtDireccionTitular.Text = SeleccionarPersona.direccion;
                txtEntidadTitular.Text = SeleccionarPersona.pais;
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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            //Agregar una imagen al cuadro de imagen para la foto del usuario.
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images(.jpg,.png)|*.png;*.jpg";
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
            mostrarPanelRegistro();
        }

        private void roundedButton4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardarM_Click(object sender, EventArgs e)
        {
            GuardarMarcaNacional();
        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
    }
}
