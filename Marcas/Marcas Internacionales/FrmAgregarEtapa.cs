using Comun.Cache;
using Presentacion.Alertas;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmAgregarEtapa : Form
    {
        public FrmAgregarEtapa()
        {
            InitializeComponent();
            dateTimePicker1.KeyDown += dateTimePicker1_KeyDown;
        }

        private void FrmAgregarEtapa_Load(object sender, EventArgs e)
        {
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

            if (comboBox1.SelectedIndex != -1)
            {
                string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                string formatoSinObjecion = fechaSinHora + " " + comboBox1.SelectedItem?.ToString();
                string formatoConObjecion = fechaSinHora + " Por objeción-" + comboBox1.SelectedItem?.ToString();
                
                if(AgregarEtapa.etapa == "Resolución RPI favorable" || AgregarEtapa.etapa == "Resolución RPI favorable" ||
                AgregarEtapa.etapa == "Recurso de revocatoria" || AgregarEtapa.etapa == "Resolución Ministerio de Economía (MINECO)"
                || AgregarEtapa.etapa == "Contencioso administrativo")
                {
                    if (anotaciones.Contains(formatoConObjecion))
                    {
                        AgregarEtapa.anotaciones = anotaciones;
                    }
                    else
                    {
                        AgregarEtapa.anotaciones = formatoConObjecion + " " + anotaciones;
                    }
                    this.Close();
                }
                else
                {
                    if (anotaciones.Contains(formatoSinObjecion))
                    {
                        AgregarEtapa.anotaciones = anotaciones;
                    }
                    else
                    {
                        AgregarEtapa.anotaciones = formatoSinObjecion + " " + anotaciones;
                    }
                    this.Close();
                }

               
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGUN ESTADO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No ha seleccionado ningun estado");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string etapa = comboBox1.SelectedItem.ToString();
            if (etapa=="Resolución RPI favorable"||etapa== "Resolución RPI favorable"||
                etapa == "Recurso de revocatoria"|| etapa == "Resolución Ministerio de Economía (MINECO)"
                ||etapa == "Contencioso administrativo")
            {
                richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " Por objeción-" + comboBox1.SelectedItem;
            }
            else
            {
                richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + comboBox1.SelectedItem;
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + comboBox1.SelectedItem;
        }

       


        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true; // Evita que Tab pase al siguiente control
                SendKeys.Send("{RIGHT}");  // Simula la tecla Flecha Derecha para moverse entre día, mes y año
            }
        }

        

    }
}
