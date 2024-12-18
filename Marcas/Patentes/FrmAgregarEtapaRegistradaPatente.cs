using Comun.Cache;
using Presentacion.Alertas;

namespace Presentacion.Patentes
{
    public partial class FrmAgregarEtapaRegistradaPatente : Form
    {
        public FrmAgregarEtapaRegistradaPatente()
        {
            InitializeComponent();
            this.Load+= FrmAgregarEtapaRegistradaPatente_Load;
        }

        public bool validarTramites()
        {
            bool tramiteValidado = false;

            
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
                    AgregarEtapaPatente.numExpediente = txtNoExpedienteRT.Text;
                    tramiteValidado = true;
                }

            }
            else
            {
                
                tramiteValidado = true;
            }

            return tramiteValidado;
        }



        private void FrmAgregarEtapaRegistradaPatente_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowStyles[0].Height = 0;
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarEtapaPatente.LimpiarEtapa();
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            this.Close();
            AgregarEtapaPatente.LimpiarEtapa();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            string anotaciones = richTextBox1.Text;
            AgregarEtapaPatente.etapa = comboBox1.SelectedItem?.ToString();
            AgregarEtapaPatente.fecha = dateTimePicker1.Value;
            AgregarEtapaPatente.usuario = UsuarioActivo.usuario;

            
            bool tramite = validarTramites();
            bool estadoSeleccionado = comboBox1.SelectedIndex != -1;

            if (tramite && estadoSeleccionado)
            {
                string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                string formato = fechaSinHora + " " + comboBox1.SelectedItem.ToString();

                if (anotaciones.Contains(formato))
                {
                    AgregarEtapaPatente.anotaciones = anotaciones;
                }
                else
                {
                    AgregarEtapaPatente.anotaciones = formato + " " + anotaciones;
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
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 53.16f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 46.84f;

            }
            else if(comboBox1.SelectedItem.ToString()=="Trámite de traspaso")
            {
                lblNoExpediente.Text = "Traspaso";
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 53.16f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 46.84f;
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
    }
}
