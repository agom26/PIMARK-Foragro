using Comun.Cache;
using Presentacion.Alertas;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmAgregarEtapaOposicionI : Form
    {
        public FrmAgregarEtapaOposicionI()
        {
            InitializeComponent();
        }

        private void FrmAgregarEtapaOposicionI_Load(object sender, EventArgs e)
        {
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarEtapaOposicion.LimpiarEtapa();
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            this.Close();
            AgregarEtapaOposicion.LimpiarEtapa();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            string anotaciones = richTextBox1.Text;
            AgregarEtapaOposicion.etapa = comboBox1.SelectedItem?.ToString();
            AgregarEtapaOposicion.fecha = dateTimePicker1.Value;
            AgregarEtapaOposicion.usuario = UsuarioActivo.usuario;

            if (comboBox1.SelectedIndex != -1)
            {
                string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                string formato = fechaSinHora + " " + comboBox1.SelectedItem.ToString();
                if (anotaciones.Contains(formato))
                {
                    AgregarEtapaOposicion.anotaciones = anotaciones;
                }
                else
                {
                    AgregarEtapaOposicion.anotaciones = formato + " " + anotaciones;
                }
                this.Close();
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
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + comboBox1.SelectedItem;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + comboBox1.SelectedItem;
        }
    }
}
