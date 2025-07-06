using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System.Runtime.InteropServices;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmEnviarAOposicionI : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();


        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmEnviarAOposicionI()
        {
            InitializeComponent();
            this.Load += FrmEnviarAOposicionI_Load;
            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
        }

        private void ActualizarFechaVencimiento()
        {

        }

        private void ActualizarFechaVencimientoNueva()
        {


        }
        private void InicializarControlesHistorial()
        {

            int margenIzquierdo = 20;
            int espacioEntreLabels = 30;

            // Label: Fecha
            labelFecha.Text = "Fecha de oposición:";
            labelFecha.AutoSize = true;
            labelFecha.Location = new Point(margenIzquierdo, 30);

            // Label: Usuario (al lado pero diferenciado)
            lblUser.Text = $"Usuario: {UsuarioActivo.usuario}"; // Le da contexto
            lblUser.AutoSize = true;
            lblUser.ForeColor = Color.Gray; // Color diferente para separarlo visualmente
            lblUser.Font = new Font("Segoe UI", 9, FontStyle.Italic); // O cambia el estilo
            lblUser.Location = new Point(labelFecha.Right + espacioEntreLabels, labelFecha.Top);


            // DateTimePicker
            dateTimePickerFecha.Format = DateTimePickerFormat.Custom;
            dateTimePickerFecha.Location = new Point(labelFecha.Left, labelFecha.Bottom + 5);
            dateTimePickerFecha.Width = groupBoxHistorial.ClientSize.Width / 3;
            dateTimePickerFecha.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Label: Anotaciones
            labelAnotaciones.Text = "Anotaciones:";
            labelAnotaciones.AutoSize = true;
            labelAnotaciones.Location = new Point(labelFecha.Left, dateTimePickerFecha.Bottom + 20);


            // TextBox: Anotaciones
            textBoxAnotaciones.Multiline = true;
            textBoxAnotaciones.ScrollBars = RichTextBoxScrollBars.Vertical;
            textBoxAnotaciones.Location = new Point(labelAnotaciones.Left, labelAnotaciones.Bottom + 5);
            textBoxAnotaciones.Size = new Size(groupBoxHistorial.ClientSize.Width - 40, groupBoxHistorial.ClientSize.Height - labelAnotaciones.Bottom - 40);
            textBoxAnotaciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            

            // Resize handler
            groupBoxHistorial.Resize += (s, e) =>
            {
                dateTimePickerFecha.Width = groupBoxHistorial.ClientSize.Width / 3;
                textBoxAnotaciones.Size = new Size(groupBoxHistorial.ClientSize.Width - 40, groupBoxHistorial.ClientSize.Height - textBoxAnotaciones.Top - 20);
            };
        }


        private void InicializarControlesOposicion()
        {

            int espacioVertical = 5;
            int espacioEntreControles = 15;
            int margenLateral = 10;

            // Altura aproximada de cada control
            int alturaLabel = 20;
            int alturaTextBox = 25;

            // Calcular alto total (2 etiquetas + 2 textbox + espacios)
            int altoTotal = alturaLabel + espacioVertical + alturaTextBox + espacioEntreControles +
                            alturaLabel + espacioVertical + alturaTextBox;

            // Calcular el offset vertical para centrar todo el bloque
            int offsetY = (groupBoxOposicion.ClientSize.Height - altoTotal) / 2;

            // Etiqueta: Opositor
            labelOpositor.Text = "Opositor";
            labelOpositor.Location = new Point(margenLateral, offsetY);
            labelOpositor.AutoSize = true;

            // TextBox: Opositor
            txtNombreOpositor.Location = new Point(labelOpositor.Left, labelOpositor.Bottom + espacioVertical);
            txtNombreOpositor.Width = groupBoxOposicion.ClientSize.Width - 2 * margenLateral;
            txtNombreOpositor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;


            // Etiqueta: Signo opositor
            labelSignoOpositor.Text = "Signo opositor";
            labelSignoOpositor.Location = new Point(margenLateral, txtNombreOpositor.Bottom + espacioEntreControles);
            labelSignoOpositor.AutoSize = true;


            // TextBox: Signo opositor

            txtSolicitante.Location = new Point(labelSignoOpositor.Left, labelSignoOpositor.Bottom + espacioVertical);
            txtSolicitante.Width = groupBoxOposicion.ClientSize.Width - 2 * margenLateral;
            txtSolicitante.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            

            // Hacer que se reposicionen si el tamaño cambia
            groupBoxOposicion.Resize += (s, e) => InicializarControlesOposicion();
        }


        private void CentrarBotonesEnPanel()
        {
            // Definir el espacio deseado entre los botones
            int espacioEntreBotones = 20;

            // Sumar el ancho total que ocupan ambos botones y el espacio entre ellos
            int anchoTotal = btnOposicion.Width + espacioEntreBotones + btnCancelar.Width;

            // Calcular el punto X de inicio para que la suma quede centrada en el panel
            int startX = (panelBotones.Width - anchoTotal) / 2;

            // Calcular la posición vertical centrada (misma para ambos botones)
            int centerY = (panelBotones.Height - btnOposicion.Height) / 2;

            // Asignar posición a btnOposicion
            btnOposicion.Location = new Point(startX, centerY);

            // Asignar posición a btnCancelar, que va a la derecha del btnOposicion con el espacio definido
            btnCancelar.Location = new Point(startX + btnOposicion.Width + espacioEntreBotones, centerY);
        }


        private void CentrarBotonEncabezado()
        {
            // Calcula el centro del panel
            int centerX = (panelEncabezado.Width - btnEnviarOposicion.Width) / 2;
            int centerY = (panelEncabezado.Height - btnEnviarOposicion.Height) / 2;

            // Asigna la nueva posición
            btnEnviarOposicion.Location = new Point(centerX, centerY);
        }


        private void FrmEnviarAOposicionI_Load(object sender, EventArgs e)
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            if (screenWidth <= 1105 && screenHeight <= 600)
            {
                this.Size = new Size(750, 540);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.AutoScroll = true;
            }

            InicializarControlesHistorial();
            InicializarControlesOposicion();
            CentrarBotonEncabezado();
            CentrarBotonesEnPanel();
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
            string anotaciones = textBoxAnotaciones.Text;
            string opositor = txtNombreOpositor.Text;
            string solicitante = AgregarEtapa.solicitante;
            string situacion_actual = "EN TRÁMITE";
            AgregarEtapa.etapa = "Oposición";
            AgregarEtapa.fecha = dateTimePickerFecha.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;
            int idSolicitante = SeleccionarMarca.idPersonaTitular;
            string signoOpositor = txtSolicitante.Text;

            //traspaso
            int idMarca = SeleccionarMarca.idN;
            string nuevoTitular = txtSolicitante.Text;

            if (txtSolicitante.Text != "" && txtNombreOpositor.Text != "")
            {
                string fechaSinHora = dateTimePickerFecha.Value.ToString("dd/MM/yyyy");
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
                        situacion_actual, idMarca, null, SeleccionarMarca.logo, "nacional", "recibida");
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
            textBoxAnotaciones.Text = dateTimePickerFecha.Value.ToString("dd/MM/yyyy") + " " + "Oposición";
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

        private void btnEnviarOposicion_Click(object sender, EventArgs e)
        {

        }

        private void panelSuperior_MouseDown(object sender, MouseEventArgs e)
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
