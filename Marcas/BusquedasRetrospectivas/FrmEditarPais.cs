using Comun.Cache;
using Presentacion.Alertas;
using System.Runtime.InteropServices;

namespace Presentacion.BusquedasRetrospectivas
{
    public partial class FrmEditarPais : Form
    {
        public bool editoPais = false;

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmEditarPais()
        {
            InitializeComponent();
            this.Load += FrmEditarPais_Load;
        }

        private void FrmEditarPais_Load(object sender, EventArgs e)
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            // Si el formulario es más ancho que el panel → centrar horizontalmente
            if (screenWidth <= 1155 && screenHeight <= 600)
            {
                this.Size = new Size(581, 532);
                this.StartPosition = FormStartPosition.CenterScreen;
            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            editoPais = false;
            this.Close();
           
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if(comboBoxPaises.Text!="" && comboBoxNivelRiesgo.Text != "")
            {
                editoPais = true;
                this.Close();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE SELECCIONAR UN PAIS Y NIVEL DE RIESGO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }




        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
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

        private void dateTimePickerVencimiento_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
