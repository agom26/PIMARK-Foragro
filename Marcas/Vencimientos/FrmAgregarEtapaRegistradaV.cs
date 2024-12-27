using Comun.Cache;
using Presentacion.Alertas;

namespace Presentacion.Vencimientos
{
    public partial class FrmAgregarEtapaRegistradaV : Form
    {
        public FrmAgregarEtapaRegistradaV()
        {
            InitializeComponent();
            this.Load += FrmAgregarEtapaRegistradaV_Load;
        }

        public bool validarTramites()
        {
            bool tramiteValidado = false;
            string tramite = txtNoExpedienteRT.Text;

            if (AgregarEtapa.etapa == "Trámite de renovación" )
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



        private void FrmAgregarEtapaRegistradaV_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 63.33f;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 36.67f;
            AgregarEtapa.etapa = "Trámite de renovación";
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + AgregarEtapa.etapa;

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
            AgregarEtapa.etapa = "Trámite de renovación";
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;


            bool tramite = validarTramites();
            

            if (tramite )
            {
                string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                string formato = fechaSinHora + " " + "Trámite de renovación";

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
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + AgregarEtapa.etapa;
            if (AgregarEtapa.etapa == "Trámite de renovación")
            {
                lblNoExpediente.Text = "Renovación";
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
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + AgregarEtapa.etapa;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
