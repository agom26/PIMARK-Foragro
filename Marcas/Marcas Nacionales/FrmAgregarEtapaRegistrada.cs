using Comun.Cache;
using Presentacion.Alertas;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmAgregarEtapaRegistrada : Form
    {
        public FrmAgregarEtapaRegistrada()
        {
            InitializeComponent();
        }

        public bool validarTramites()
        {
            bool tramiteValidado = false;

            
            if (comboBox1.SelectedItem?.ToString() == "Trámite de renovación" ||
                comboBox1.SelectedItem?.ToString() == "Trámite de traspaso")
            {
                
                if (string.IsNullOrWhiteSpace(txtNoExpedienteRT.Text) ||
                    !int.TryParse(txtNoExpedienteRT.Text, out int tramite) ||
                    tramite <= 0)
                {
                    
                    return false;
                }

                
                AgregarEtapa.numExpediente = tramite;
                tramiteValidado = true;
            }
            else
            {
                
                tramiteValidado = true;
            }

            return tramiteValidado;
        }



        private void FrmAgregarEtapaRegistrada_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            btnSeleccionar.Location = new Point(185, 400);
            btnCancelar.Location = new Point(390, 400);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarEtapa.LimpiarEtapa();
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            this.Close();
            AgregarEtapa.LimpiarEtapa();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            string anotaciones = richTextBox1.Text;
            AgregarEtapa.etapa = comboBox1.SelectedItem?.ToString();
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;

            
            bool tramite = validarTramites();
            bool estadoSeleccionado = comboBox1.SelectedIndex != -1;

            if (tramite && estadoSeleccionado)
            {
                string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                string formato = fechaSinHora + " " + comboBox1.SelectedItem.ToString();

                if (anotaciones.Contains(formato))
                {
                    AgregarEtapa.anotaciones = anotaciones;
                }
                else
                {
                    AgregarEtapa.anotaciones = formato + " " + anotaciones;
                }
                this.Close();
            }
            else
            {

                // Construir un mensaje dinámico según las validaciones que fallaron
                string mensajeError = "";

                if (!tramite)
                {
                    mensajeError += "- DEBE INGRESAR UN NÚMERO DE EXPEDIENTE VÁLIDO.\n";
                }

                if (!estadoSeleccionado)
                {
                    mensajeError += "- DEBE SELECCIONAR UN ESTADO.";
                }

                // Mostrar mensaje de error
                FrmAlerta alerta = new FrmAlerta(
                    mensajeError,
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                alerta.ShowDialog();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + comboBox1.SelectedItem;
            if(comboBox1.SelectedItem.ToString()=="Trámite de renovación")
            {
                lblNoExpediente.Text = "Renovación";
                btnSeleccionar.Location = new Point(185, 518);
                btnCancelar.Location = new Point(390, 518);
                groupBox1.Visible = true;
            }
            else if(comboBox1.SelectedItem.ToString()=="Trámite de traspaso")
            {
                lblNoExpediente.Text = "Traspaso";
                btnSeleccionar.Location = new Point(185, 518);
                btnCancelar.Location = new Point(390, 518);
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
                btnSeleccionar.Location = new Point(185, 400);
                btnCancelar.Location = new Point(390, 400);
            }
                
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + comboBox1.SelectedItem;
        }
    }
}
