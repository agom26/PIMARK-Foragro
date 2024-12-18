using Comun.Cache;
using Presentacion.Alertas;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmAgregarEtapaRegistrada : Form
    {
        public FrmAgregarEtapaRegistrada()
        {
            InitializeComponent();
            this.Load += FrmAgregarEtapaRegistrada_Load;
        }

        public bool validarTramites()
        {
            bool tramiteValidado = false;
            string tramite = txtNoExpedienteRT.Text;

            if (comboBox1.SelectedItem?.ToString() == "Trámite de renovación" ||
                comboBox1.SelectedItem?.ToString() == "Trámite de traspaso")
            {

                if (string.IsNullOrWhiteSpace(txtNoExpedienteRT.Text)
                    )

                {

                    return false;
                }
                else
                {
                    AgregarEtapa.numExpediente = tramite;
                    tramiteValidado = true;
                }

                
            }
            else
            {

                tramiteValidado = true;
            }

            return tramiteValidado;
        }



        private void FrmAgregarEtapaRegistrada_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowStyles[0].Height = 0;
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
          
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
            if (comboBox1.SelectedItem.ToString() == "Trámite de renovación")
            {
                lblNoExpediente.Text = "Renovación";
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 63.33f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 36.67f;

            }
            else if (comboBox1.SelectedItem.ToString() == "Trámite de traspaso")
            {
                lblNoExpediente.Text = "Traspaso";
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 63.33f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 36.67f;

            }
            else
            {
                tableLayoutPanel1.RowStyles[0].Height = 0;
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + comboBox1.SelectedItem;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
