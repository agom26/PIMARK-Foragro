using Comun.Cache;
using Dominio;
using Presentacion.Marcas_Nacionales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmTramiteInicialInternacional : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        public FrmTramiteInicialInternacional()
        {
            InitializeComponent();
            panel2.Visible = false;
            btnGuardar.Location = new Point(380, 1219);
        }

        private void ActualizarFechaVencimiento()
        {
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = fecha_registro.AddYears(10).AddDays(-1);
            dateTimePFecha_vencimiento.Value = fecha_vencimiento; // Asigna la fecha de vencimiento al DateTimePicker correspondiente
        }

        public void GuardarMarcaInternacional()
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
            int idCliente = SeleccionarPersona.idPersonaC;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string estado = cmbEstado.SelectedItem?.ToString();
            string pais_de_registro = comboBox1.Text;
            string tiene_poder = "no";
            DateTime presentacion = dateTimePresentacion.Value;
            DateTime ultimo_pago = dateTimeUltimoPago.Value;
            DateTime vencimiento = dateTimeVencimiento.Value;

            //registro
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;

            // Validaciones
            if (string.IsNullOrEmpty(expediente) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(clase) || string.IsNullOrEmpty(signoDistintivo))
            {
                MessageBox.Show("Por favor, llene todos los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (estado == null)
            {
                MessageBox.Show("Por favor, selecciona un estado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pictureBox1.Image != null) // Verificar que hay una imagen
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
                return;
            }


            if (checkBoxTienePoder.Checked)
            {
                tiene_poder = "si";
            }
            else
            {
                tiene_poder = "no";
            }


            // Si está registrada, se verifica la información del registro
            if (registroChek)
            {
                if (string.IsNullOrEmpty(registro))
                {
                    MessageBox.Show("Por favor, ingrese el número de registro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Intentar guardar la marca registrada
                bool resultadoRegistrada = marcaModel.AddMarcaInternacionalRegistrada(expediente, nombre, signoDistintivo, clase, logo, idTitular, idAgente, solicitud, estado, pais_de_registro, tiene_poder, presentacion, ultimo_pago, vencimiento, idCliente, registro, folio, libro, fecha_registro, fecha_vencimiento);

                if (resultadoRegistrada)
                {
                    MessageBox.Show("Marca internacional registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("Error al registrar la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Intentar guardar la marca sin registro
                int resultado = marcaModel.AddMarcaInternacional(
                    expediente, nombre, signoDistintivo, clase, logo, idTitular, idAgente, solicitud, estado, pais_de_registro, tiene_poder, presentacion, ultimo_pago, vencimiento, idCliente);

                if (resultado>0)
                {
                    MessageBox.Show("Marca internacional guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("Error al guardar la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            txtNombreAgente.Text = "";
            txtNombreCliente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            cmbEstado.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarMarcaInternacional();
        }

        private void btnAgregarTitular_Click(object sender, EventArgs e)
        {
            FrmMostrarTitulares frmMostrarTitulares = new FrmMostrarTitulares();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersona.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersona.nombre;
            }
        }

        private void btnAgregarAgente_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentes frmMostrarAgentes = new FrmMostrarAgentes();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersona.idPersonaA != 0)
            {
                txtNombreAgente.Text = SeleccionarPersona.nombre;

            }
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            FrmMostrarClientes frmMostrarClientes = new FrmMostrarClientes();
            frmMostrarClientes.ShowDialog();

            if (SeleccionarPersona.idPersonaC != 0)
            {
                txtNombreCliente.Text = SeleccionarPersona.nombre;

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                panel2.Visible = true;
                btnGuardar.Location = new Point(380, panel2.Location.Y + panel2.Height + 10); // Mueve btnGuardar debajo de panel2

            }
            else
            {
                panel2.Visible = false;
                btnGuardar.Location = new Point(380, 1219);

            }
        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }
    }
}
