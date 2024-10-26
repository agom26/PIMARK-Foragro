using Comun.Cache;
using Dominio;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmTramiteIn : Form
    {
        MarcaModel marcaModel = new MarcaModel();

        public FrmTramiteIn()
        {
            InitializeComponent();
            panel2.Visible = false;
            btnGuardar.Location = new Point(272, 1135);
            //panel1.Size = new Size(1081, 1252);
        }

        public void GuardarMarcaNacional()
        {
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
            string estado = cmbEstado.SelectedItem?.ToString(); // Mejor usar SelectedItem

            if (estado == null)
            {
                // Manejar el caso en que no hay un estado seleccionado
                MessageBox.Show("Por favor, selecciona un estado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (pictureBox1.Image != null) // Verificar que hay una imagen
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Guarda la imagen en el MemoryStream
                    logo = ms.ToArray(); // Convierte el MemoryStream a byte[]
                }
            }

            bool resultado = marcaModel.AddMarcaNacional(expediente, nombre, signoDistintivo, clase, folio, libro, logo, idTitular, idAgente, solicitud, estado);
            if (resultado)
            {
                MessageBox.Show("Marca nacional guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al guardar la marca nacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            cmbEstado.SelectedIndex = 0;
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
            LimpiarFormulario();
        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                panel2.Visible = true;
                btnGuardar.Location = new Point(272, panel2.Location.Y + panel2.Height + 10); // Mueve btnGuardar debajo de panel2
                
                
            }
            else
            {
                panel2.Visible = false;
                btnGuardar.Location = new Point(272, 1135);
                
            }
        }
    }
}
