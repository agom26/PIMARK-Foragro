using Comun.Cache;
using Presentacion.Alertas;
using System.Runtime.InteropServices;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmAgregarEtapaOposicionI : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmAgregarEtapaOposicionI()
        {
            InitializeComponent();
        }

        private void FrmAgregarEtapaOposicionI_Load(object sender, EventArgs e)
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            // Si el formulario es más ancho que el panel → centrar horizontalmente
            if (screenWidth <= 1155 && screenHeight <= 600)
            {
                this.Size = new Size(581, 532);
                this.AutoScroll = true;
                panel1.AutoScroll = true;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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
