using Comun.Cache;
using Presentacion.Alertas;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Presentacion.Patentes
{
    public partial class FrmAgregarEtapaPatente : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FrmAgregarEtapaPatente()
        {
            InitializeComponent();
        }

        private void FrmAgregarEtapaPatente_Load(object sender, EventArgs e)
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
            string anotaciones = richTextBoxAnotaciones.Text;
            AgregarEtapaPatente.etapa = comboBox1.SelectedItem?.ToString();
            AgregarEtapaPatente.fecha = dateTimePickerFechaIngreso.Value;
            AgregarEtapaPatente.usuario = UsuarioActivo.usuario;

            if (comboBox1.SelectedIndex != -1)
            {
                string fechaSinHora = dateTimePickerFechaIngreso.Value.ToShortDateString();
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
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGUN ESTADO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No ha seleccionado ningun estado");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string etapa = comboBox1.Text;
            DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;
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
                richTextBoxAnotaciones.Text = $"{fecha} Por objeción - {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (mostrarVencimiento)
            {
                richTextBoxAnotaciones.Text = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (etapa == "Resolución RPI favorable" ||
                     etapa == "Recurso de revocatoria" ||
                     etapa == "Resolución Ministerio de Economía (MINECO)" ||
                     etapa == "Contencioso administrativo")
            {
                richTextBoxAnotaciones.Text = $"{fecha} Por objeción - {etapa}";
            }
            else
            {
                richTextBoxAnotaciones.Text = $"{fecha} {etapa}";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotaciones.Text = dateTimePickerFechaIngreso.Value.ToShortDateString() + " " + comboBox1.SelectedItem;
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

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }
    }
}
