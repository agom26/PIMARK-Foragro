using Comun.Cache;
using Presentacion.Alertas;
using System.Runtime.InteropServices;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmAgregarEtapa : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmAgregarEtapa()
        {
            InitializeComponent();
            dateTimePicker1.KeyDown += dateTimePicker1_KeyDown;
            this.Load += FrmAgregarEtapa_Load;
        }

        private void FrmAgregarEtapa_Load(object sender, EventArgs e)
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            // Si el formulario es más ancho que el panel → centrar horizontalmente
            if (screenWidth <= 1155 && screenHeight <= 600)
            {
                this.Size = new Size(581, 532);
                this.StartPosition = FormStartPosition.CenterScreen;
            }
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

                if (AgregarEtapa.etapa == "Resolución RPI favorable" || AgregarEtapa.etapa == "Resolución RPI favorable" ||
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
            if (etapa == "Resolución RPI favorable" || etapa == "Resolución RPI favorable" ||
                etapa == "Recurso de revocatoria" || etapa == "Resolución Ministerio de Economía (MINECO)"
                || etapa == "Contencioso administrativo")
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

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x83;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                m.Result = new IntPtr(0xF0);   // Align client area to all borders
                return;
            }
            base.WndProc(ref m);
        }
    }
}
