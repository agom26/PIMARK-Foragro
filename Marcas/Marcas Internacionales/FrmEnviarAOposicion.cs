using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System.Runtime.InteropServices;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmEnviarAOposicion : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();


        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmEnviarAOposicion()
        {
            InitializeComponent();
            this.Load += FrmEnviarAOposicion_Load;
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
        }

        private void ActualizarFechaVencimiento()
        {

        }

        private void ActualizarFechaVencimientoNueva()
        {


        }

        private void CentrarPanel()
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            // Si el formulario es más ancho que el panel → centrar horizontalmente
            if (screenWidth <= 1055 && screenHeight <= 600)
            {
                this.Size = new Size(700, 540);
                this.StartPosition = FormStartPosition.CenterScreen;

            }
           
        }


        private void FrmEnviarAOposicion_Load(object sender, EventArgs e)
        {
            
            CentrarPanel();

            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarEtapa.LimpiarEtapa();
            this.Close();
            AgregarEtapa.enviadoAOposicion = false;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            this.Close();
            AgregarEtapa.LimpiarEtapa();
            AgregarEtapa.enviadoAOposicion = false;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            HistorialModel historialModel = new HistorialModel();
            OposicionModel oposicionModel = new OposicionModel();
            string anotaciones = richTextBox1.Text;
            string opositor = txtNombreOpositor.Text;
            string solicitante = AgregarEtapa.solicitante;
            string situacion_actual = "EN TRÁMITE";
            AgregarEtapa.etapa = "Oposición";
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;
            int idSolicitante = SeleccionarMarca.idPersonaTitular;
            string signoOpositor = txtSolicitante.Text;

            //traspaso
            int idMarca = SeleccionarMarca.idInt;
            string nuevoTitular = txtSolicitante.Text;

            if (txtSolicitante.Text != "" && txtNombreOpositor.Text != "")
            {
                string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                string formato = fechaSinHora + " " + "Oposición";
                if (anotaciones.Contains(formato))
                {
                    AgregarEtapa.anotaciones = anotaciones;
                }
                else
                {
                    AgregarEtapa.anotaciones = formato + " " + anotaciones;
                }

                try
                {
                    oposicionModel.CrearOposicion(SeleccionarMarca.expediente, SeleccionarMarca.nombre,
                        SeleccionarMarca.signoDistintivo, SeleccionarMarca.clase,
                        solicitante, SeleccionarMarca.idPersonaTitular, null, opositor, signoOpositor,
                        situacion_actual, idMarca, null, SeleccionarMarca.logo, "internacional", "recibida");
                    historialModel.GuardarEtapa(idMarca, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario, "TRÁMITE");
                    string nuevaAnotacion = fechaSinHora + " Oposición presentada";
                    historialModel.GuardarEtapa(idMarca, (DateTime)AgregarEtapa.fecha, "Oposición presentada", nuevaAnotacion, AgregarEtapa.usuario, "OPOSICIÓN");

                    AgregarEtapa.enviadoAOposicion = true;
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    AgregarEtapa.enviadoAOposicion = false;
                }


            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR AL OPOSITOR Y EL SIGNO OPOSITOR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("No ha seleccionado ningun estado");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + "Oposición";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateFechRegNueva_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimientoNueva();
        }

        private void dateFechRegAntigua_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void roundedButton2_Click(object sender, EventArgs e)
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

        private void panelBusqueda_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
