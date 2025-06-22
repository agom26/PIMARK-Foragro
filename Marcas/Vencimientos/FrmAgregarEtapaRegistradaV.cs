using Comun.Cache;
using Presentacion.Alertas;
using System.Runtime.InteropServices;

namespace Presentacion.Vencimientos
{
    public partial class FrmAgregarEtapaRegistradaV : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmAgregarEtapaRegistradaV()
        {
            InitializeComponent();
            this.Load += FrmAgregarEtapaRegistradaV_Load;
        }

        public bool validarTramites()
        {
            bool tramiteValidado = false;
            string tramite = txtNoExpedienteRT.Text;

            if (AgregarEtapa.etapa == "Trámite de renovación")
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


            if (tramite)
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
