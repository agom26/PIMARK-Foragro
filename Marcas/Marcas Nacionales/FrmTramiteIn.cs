using Comun.Cache;
using Dominio;
using MySqlX.XDevAPI.Common;
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


        public FrmTramiteIn()
        {
            InitializeComponent();
            panel2I.Visible = false;
            btnGuardar.Location = new Point(200, 950);
            ActualizarFechaVencimiento();
            checkBox1.Checked = false;
            checkBox1.Enabled = false;
        }
        private void ActualizarFechaVencimiento()
        {
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = fecha_registro.AddYears(10).AddDays(-1);
            dateTimePFecha_vencimiento.Value = fecha_vencimiento; // Asigna la fecha de vencimiento al DateTimePicker correspondiente
        }
        private bool ValidarCampos(string expediente, string nombre, string clase, string signoDistintivo, string estado, ref byte[] logo, bool registroChek, string registro)
        {
            // Verificar campos obligatorios
            if (string.IsNullOrEmpty(expediente) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(clase) || string.IsNullOrEmpty(signoDistintivo))
            {
                MessageBox.Show("Por favor, llene todos los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(estado))
            {
                MessageBox.Show("Por favor, seleccione un estado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar que hay una imagen
            if (pictureBox1.Image != null)
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    logo = ms.ToArray();
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese una imagen.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Si está registrada, se verifica la información del registro
            if (registroChek && string.IsNullOrEmpty(registro))
            {
                MessageBox.Show("Por favor, ingrese el número de registro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // Todas las validaciones pasaron
        }
        public void GuardarMarcaNacional()
        {
            // Recolectar valores de los controles
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string signoDistintivo = txtSignoDistintivo.Text;
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
            if (!ValidarCampos(expediente, nombre, clase, signoDistintivo, estado, ref logo, registroChek, registro))
            {
                return; 
            }

            // Guardar la marca
            try
            {
                int idMarca = registroChek ?
                    marcaModel.AddMarcaNacionalRegistrada(expediente, nombre, signoDistintivo, clase, folio, libro, logo, idTitular, idAgente, solicitud, registro, fecha_registro, fecha_vencimiento) :
                    marcaModel.AddMarcaNacional(expediente, nombre, signoDistintivo, clase, logo, idTitular, idAgente, solicitud);

                // Verifica si se ha guardado correctamente
                if (idMarca > 0)
                {
                    string etapa = textBoxEstatus.Text;
                    if (!string.IsNullOrEmpty(etapa))
                    {
                        historialModel.GuardarEtapa(idMarca, AgregarEtapa.fecha.Value, etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                    }
                    MessageBox.Show("Marca nacional " + (registroChek ? "registrada" : "guardada") + " con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("Error al " + (registroChek ? "registrar" : "guardar") + " la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al " + (registroChek ? "registrar" : "guardar") + " la marca nacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void LimpiarFormulario()
        {
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtSignoDistintivo.Text = "";
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = null;
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
        }

        public void mostrarPanelRegistro()
        {
            if (textBoxEstatus.Text == "Registrada")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel2I.Visible = true;
                btnGuardar.Location = new Point(200, panel2I.Location.Y + panel2I.Height + 10); // Mueve btnGuardar debajo de panel2
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel2I.Visible = false;
                btnGuardar.Location = new Point(200, 950);
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
            pictureBox1.Image = null;
        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {
            GuardarMarcaNacional();

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
            FrmAgregarEtapa frmAgregarEtapa = new FrmAgregarEtapa();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                textBoxEstatus.Text = AgregarEtapa.etapa;
                mostrarPanelRegistro();
                richTextBox1.Text += AgregarEtapa.anotaciones;
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void FrmTramiteIn_Load(object sender, EventArgs e)
        {

        }

        private void roundedButton4_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
