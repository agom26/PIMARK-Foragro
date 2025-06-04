using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System.Runtime.InteropServices;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmCrearTraspaso : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();


        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmCrearTraspaso()
        {
            InitializeComponent();
            this.Load += FrmCrearTraspaso_Load1;
        }

        private void CentrarPanel()
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            // Si el formulario es más ancho que el panel → centrar horizontalmente
            if (screenWidth <= 1025 && screenHeight <= 600)
            {
                this.Size = new Size(700, 540);
                this.StartPosition = FormStartPosition.CenterScreen;

            }
        }

        private void FrmCrearTraspaso_Load1(object? sender, EventArgs e)
        {
            CentrarPanel();
        }

        private void ActualizarFechaVencimiento()
        {

        }

        private void ActualizarFechaVencimientoNueva()
        {


        }

        private void FrmCrearTraspaso_Load(object sender, EventArgs e)
        {

            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            // Si el formulario es más ancho que el panel → centrar horizontalmente
            if (screenWidth <= 1155 && screenHeight <= 600)
            {
                this.Size = new Size(700, 540);
                this.StartPosition = FormStartPosition.CenterScreen;

            }

            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            txtNoExpediente.Text = SeleccionarMarca.etraspaso;
            txtNombreTitularA.Text = AgregarTraspaso.nombreTitulara;
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
            AgregarTraspaso.LimpiarTraspaso();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            // Instancias de modelos
            MarcaModel marcaModel = new MarcaModel();

            string anotaciones = richTextBox1.Text;
            AgregarEtapa.etapa = txtEstado.Text;
            AgregarEtapa.fecha = dateTimePicker1.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;

            // Datos del traspaso
            string noExpediente = txtNoExpediente.Text;
            int idMarca = SeleccionarMarca.idInt;
            string nuevoTitular = txtNombreTitularN.Text;

            if (!string.IsNullOrEmpty(txtEstado.Text) && !string.IsNullOrEmpty(txtNoExpediente.Text))
            {
                if (!string.IsNullOrEmpty(nuevoTitular))
                {
                    // Formatear anotaciones con fecha y etapa
                    string fechaSinHora = dateTimePicker1.Value.ToShortDateString();
                    string formato = fechaSinHora + " " + txtEstado.Text;
                    AgregarEtapa.anotaciones = anotaciones.Contains(formato) ? anotaciones : formato + " " + anotaciones;

                    try
                    {
                        int idTitularNuevo = AgregarTraspaso.idNuevoTitular;
                        int idTitularViejo = AgregarTraspaso.idTitularAnterior;

                        // Llamar al método con transacción en MarcaModel
                        marcaModel.InsertarTraspasoYHistorialMarca(
                            noExpediente, idMarca, idTitularViejo, idTitularNuevo,
                            (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa,
                            AgregarEtapa.anotaciones, AgregarEtapa.usuario
                        );

                        AgregarTraspaso.traspasoFinalizado = true;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR UN TITULAR Y NOMBRE NUEVO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR EL NÚMERO DE EXPEDIENTE", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = dateTimePicker1.Value.ToShortDateString() + " " + txtEstado.Text;
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
            FrmMostrarTitularesTraspaso frmCrearTraspaso = new FrmMostrarTitularesTraspaso();
            frmCrearTraspaso.ShowDialog();

            if (AgregarTraspaso.idNuevoTitular != 0)
            {
                txtNombreTitularN.Text = AgregarTraspaso.nombreTitularN;

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ UN TITULAR NUEVO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("No selecciono un nuevo titular");
            }

        }

        private void FrmCrearTraspaso_KeyDown(object sender, KeyEventArgs e)
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
