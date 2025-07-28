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
            dateTimePickerVencimiento.ValueChanged += dateTimePickerVencimiento_ValueChanged;

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

            labelVenc.Visible = false;
            dateTimePickerVencimiento.Visible = false;
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
            string etapa = comboBox1.Text;
            string anotaciones = richTextBox1.Text;
            AgregarEtapa.etapa = comboBox1.Text;
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;
            bool requiereVencimiento = etapa == "Examen de fondo" ||
                              etapa == "Requerimiento" ||
                              etapa == "Objeción" ||
                              etapa == "Publicación" ||
                              etapa == "Orden de pago";

            if (requiereVencimiento)
            {
                AgregarEtapa.fechaVencimiento = dateTimePickerVencimiento.Value;
            }
            else
            {
                AgregarEtapa.fechaVencimiento = null;
            }


            if (comboBox1.SelectedIndex != -1)
            {
                string fecha = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                string venc = dateTimePickerVencimiento.Value.ToString("dd/MM/yyyy");

                string anotacionFinal = "";

                if (requiereVencimiento)
                {
                    anotacionFinal = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
                }
                else if (etapa == "Resolución RPI favorable" ||
                         etapa == "Resolución RPI desfavorable" ||
                         etapa == "Recurso de revocatoria" ||
                         etapa == "Resolución Ministerio de Economía (MINECO)" ||
                         etapa == "Contencioso administrativo")
                {
                    anotacionFinal = $"{fecha} Por objeción-{etapa}";
                }
                else
                {
                    anotacionFinal = $"{fecha} {etapa}";
                }

                if (!anotaciones.Contains(anotacionFinal))
                {
                    AgregarEtapa.anotaciones = anotacionFinal + " " + anotaciones;
                }
                else
                {
                    AgregarEtapa.anotaciones = anotaciones;
                }

                this.Close();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGUN ESTADO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string etapa = comboBox1.Text;
            DateTime fechaIngreso = dateTimePicker1.Value;
            DateTime fechaVencimiento = fechaIngreso; // valor por defecto

            // Establecer vencimiento automático según la etapa
            switch (etapa)
            {
                case "Examen de fondo":
                case "Objeción":
                case "Publicación":
                    fechaVencimiento = fechaIngreso.AddMonths(2);
                    break;

                case "Requerimiento":
                case "Orden de pago":
                    fechaVencimiento = fechaIngreso.AddMonths(1);
                    break;

                case "Resolución RPI desfavorable":
                    fechaVencimiento = fechaIngreso.AddDays(5);
                    break;
            }

            // Mostrar u ocultar el campo de vencimiento según la etapa
            bool mostrarVencimiento = etapa == "Examen de fondo" ||
                                       etapa == "Requerimiento" ||
                                       etapa == "Objeción" ||
                                       etapa == "Publicación" ||
                                       etapa == "Orden de pago" ||
                                       etapa == "Resolución RPI desfavorable";

            labelVenc.Visible = mostrarVencimiento;
            dateTimePickerVencimiento.Visible = mostrarVencimiento;

            if (mostrarVencimiento)
            {
                dateTimePickerVencimiento.Value = fechaVencimiento;
            }

            // Mostrar resumen en el RichTextBox
            string fecha = fechaIngreso.ToString("dd/MM/yyyy");
            string venc = fechaVencimiento.ToString("dd/MM/yyyy");

            if (etapa == "Resolución RPI desfavorable")
            {
                richTextBox1.Text = $"{fecha} Por objeción - {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (mostrarVencimiento)
            {
                richTextBox1.Text = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (etapa == "Resolución RPI favorable" ||
                     etapa == "Recurso de revocatoria" ||
                     etapa == "Resolución Ministerio de Economía (MINECO)" ||
                     etapa == "Contencioso administrativo")
            {
                richTextBox1.Text = $"{fecha} Por objeción - {etapa}";
            }
            else
            {
                richTextBox1.Text = $"{fecha} {etapa}";
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);
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

        private void dateTimePickerVencimiento_ValueChanged(object sender, EventArgs e)
        {
            if (labelVenc.Visible)
            {
                comboBox1_SelectedIndexChanged(sender, e);
            }
        }
    }
}
