using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using Presentacion.Marcas_Nacionales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmTramiteInicialInternacional : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento;
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }

        public FrmTramiteInicialInternacional()
        {
            InitializeComponent();
            SeleccionarMarca.idN = 0;
            panelRegistroI.Visible = false;
            //btnGuardarM.Location = new Point(59, 1050);
            //btnCancelarM.Location = new Point(339, 1050);
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

        private bool ValidarCampos(string expediente, string nombre, string paisRegistro, string clase, string signoDistintivo, string tipo, string estado,
   ref byte[] logo, bool registroChek, string registro, string folio, string libro)
        {
            // Verificar campos obligatorios
            if (!ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
                !ValidarCampo(nombre, "Por favor, ingrese el nombre.") ||
                !ValidarCampo(clase, "Por favor, ingrese la clase.") ||
                !ValidarCampo(paisRegistro, "Por favor, ingrese un pais.") ||
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

        public void GuardarMarcaInter()
        {
            // Recolectar valores de los controles
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string paisRegistro = comboBox1.SelectedItem?.ToString();
            string signoDistintivo = comboBoxSignoDistintivo.SelectedItem?.ToString();
            string tipoSigno = comboBoxTipoSigno.SelectedItem?.ToString();
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[] logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            int? idCliente = SeleccionarPersona.idPersonaC;
            bool tieneCliente = false;
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

            if (idCliente <= 0)
            {
                //no colocarle cliente
                SeleccionarMarca.idCliente = null;
                //MessageBox.Show("Por favor, seleccione un cliente válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }
            else
            {
                tieneCliente = true;
            }

            // Validar campos 
            if (!ValidarCampos(expediente, nombre, paisRegistro, clase, signoDistintivo, tipoSigno, estado, ref logo, registroChek, registro, folio, libro))
            {
                return;
            }

            // Guardar la marca
            try
            {
                int idMarca = registroChek ?
                    marcaModel.AddMarcaInternacionalRegistrada(expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente, registro, folio, libro, fecha_registro, fecha_vencimiento) :
                    marcaModel.AddMarcaInternacional(expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente);

                // Verifica si se ha guardado correctamente
                if (idMarca > 0)
                {
                    string etapa = textBoxEstatus.Text;
                    if (!string.IsNullOrEmpty(etapa))
                    {
                        historialModel.GuardarEtapa(idMarca, AgregarEtapa.fecha.Value, etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                    }
                    FrmAlerta alerta = new FrmAlerta("MARCA INTERNACIONAL " + (registroChek ? "REGISTRADA" : "GUARDADA"), "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show("Marca internacional " + (registroChek ? "registrada" : "guardada") + " con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("Error al " + (registroChek ? "registrar" : "guardar") + " la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL " + (registroChek ? "REGISTRAR" : "GUARDAR") + ":\n " + ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                //MessageBox.Show("Error al " + (registroChek ? "registrar" : "guardar") + " la marca internacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void LimpiarFormulario()
        {
            convertirImagen();
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = documento;
            txtNombreTitular.Text = "";
            txtNombreAgente.Text = "";
            txtNombreCliente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            comboBoxSignoDistintivo.SelectedIndex = -1;
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            textBoxEstatus.Text = "";
            SeleccionarPersona.idPersonaA = 0;
            SeleccionarPersona.idPersonaT = 0;
            SeleccionarPersona.idPersonaC = null;
        }



        public void mostrarPanelRegistro()
        {

            if (textBoxEstatus.Text == "Registrada")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panelRegistroI.Visible = true;
                //btnGuardarM.Location = new Point(59, panelRegistroI.Location.Y + panelRegistroI.Height + 10); // Mueve btnGuardar debajo de panel2
                //btnCancelarM.Location = new Point(339, panelRegistroI.Location.Y + panelRegistroI.Height + 10);
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panelRegistroI.Visible = false;
                //btnGuardarM.Location = new Point(59, 1050);
                //btnCancelarM.Location = new Point(339, 1050);
            }
        }

        public void LimpiarEtapaNueva()
        {
            AgregarEtapa.etapa = "";
            AgregarEtapa.fecha = null;
            AgregarEtapa.anotaciones = "";
            richTextBox1.Text = "";
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {

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
            else
            {
                SeleccionarPersona.idPersonaC = null;

            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            convertirImagen();
            pictureBox1.Image = documento;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mostrarPanelRegistro();
        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreAgente_TextChanged(object sender, EventArgs e)
        {

        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            LimpiarEtapaNueva();
            FrmAgregarEtapa frmAgregarEtapa = new FrmAgregarEtapa();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                textBoxEstatus.Text = AgregarEtapa.etapa;
                mostrarPanelRegistro();
                richTextBox1.Text += "\n" + AgregarEtapa.anotaciones;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void txtSignoDistintivo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmTramiteInicialInternacional_Load(object sender, EventArgs e)
        {
            convertirImagen();
            pictureBox1.Image = documento;
            SeleccionarPersona.idPersonaA = 0;
            SeleccionarPersona.idPersonaT = 0;
            SeleccionarPersona.idPersonaC = null;
        }

        private void btnGuardarM_Click(object sender, EventArgs e)
        {
            GuardarMarcaInter();
        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
    }
}
