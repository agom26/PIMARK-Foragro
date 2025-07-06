using Comun.Cache;
using DocumentFormat.OpenXml.Wordprocessing;
using Dominio;
using Presentacion.Alertas;
using Presentacion.Clases;
using Presentacion.Marcas_Nacionales;
using System.Runtime.InteropServices;
namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmCrearTraspasoInt : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();


        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmCrearTraspasoInt()
        {
            InitializeComponent();
            this.Load += FrmCrearTraspasoInt_Load;
        }

        private void ActualizarFechaVencimiento()
        {

        }

        private void ActualizarFechaVencimientoNueva()
        {


        }

        private void ConfigurarControlesHistorial()
        {
            // Asumiendo que los controles ya están creados en el diseñador
            // Solo modificamos sus propiedades aquí.

            // Centrar lblUser en groupBoxHistorial
            if (groupBoxHistorial.Controls["lblUser"] is Label lblUser)
            {
                lblUser.Text = $"Usuario: {UsuarioActivo.usuario}";
                lblUser.ForeColor = System.Drawing.Color.Gray;
                lblUser.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Italic);
                lblUser.Location = new Point((groupBoxHistorial.Width - lblUser.PreferredWidth) / 2, 10);
                lblUser.AutoSize = true;
                lblUser.Visible = true;
            }

            if (groupBoxHistorial.Controls["labelFecha"] is Label labelFecha)
            {
                labelFecha.Text = "Fecha:";
                labelFecha.Location = new Point(20, groupBoxHistorial.Controls["lblUser"].Bottom + 20);
                labelFecha.AutoSize = true;
            }

            if (groupBoxHistorial.Controls["labelEstado"] is Label labelEstado && groupBoxHistorial.Controls["labelFecha"] is Label lf)
            {
                labelEstado.Text = "Estado:";
                labelEstado.Location = new Point(lf.Right + 150, lf.Top);
                labelEstado.AutoSize = true;
            }

            if (groupBoxHistorial.Controls["txtEstado"] is TextBox textBoxEstado && groupBoxHistorial.Controls["labelEstado"] is Label le)
            {
                textBoxEstado.Width = 150;
                textBoxEstado.Location = new Point(le.Left, le.Bottom + 5);
                textBoxEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }

            if (groupBoxHistorial.Controls["dateTimePickerFecha"] is DateTimePicker dateTimePickerFecha && groupBoxHistorial.Controls["labelFecha"] is Label lf2)
            {
                dateTimePickerFecha.Format = DateTimePickerFormat.Custom;
                dateTimePickerFecha.Location = new Point(lf2.Left, lf2.Bottom + 5);
                dateTimePickerFecha.Width = groupBoxHistorial.ClientSize.Width / 3;
                dateTimePickerFecha.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }

            if (groupBoxHistorial.Controls["labelAnotaciones"] is Label labelAnotaciones && groupBoxHistorial.Controls["labelFecha"] is Label lf3 && groupBoxHistorial.Controls["textBoxEstado"] is TextBox tbe)
            {
                int topPos = Math.Max(groupBoxHistorial.Controls["dateTimePickerFecha"].Bottom, tbe.Bottom) + 20;
                labelAnotaciones.Text = "Anotaciones:";
                labelAnotaciones.Location = new Point(20, topPos);
                labelAnotaciones.AutoSize = true;
            }

            if (groupBoxHistorial.Controls["textBoxAnotaciones"] is TextBox textBoxAnotaciones && groupBoxHistorial.Controls["labelAnotaciones"] is Label la)
            {
                textBoxAnotaciones.Multiline = true;
                textBoxAnotaciones.ScrollBars = ScrollBars.Vertical;
                textBoxAnotaciones.Location = new Point(la.Left, la.Bottom + 5);
                textBoxAnotaciones.Size = new Size(groupBoxHistorial.ClientSize.Width - 40, groupBoxHistorial.ClientSize.Height - la.Bottom - 40);
                textBoxAnotaciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            }

            // Actualizar la posición del lblUser y tamaño de controles cuando se redimensione groupBoxHistorial
            groupBoxHistorial.Resize -= groupBoxHistorial_Resize; // Evita duplicar eventos
            groupBoxHistorial.Resize += groupBoxHistorial_Resize;

            groupBoxHistorial.PerformLayout();
            groupBoxHistorial.Refresh();

        }


        private void CentrarBotonEncabezado()
        {
            // Calcula el centro del panel
            int centerX = (panelEncabezado.Width - btnTraspasar.Width) / 2;
            int centerY = (panelEncabezado.Height - btnTraspasar.Height) / 2;

            // Asigna la nueva posición
            btnTraspasar.Location = new Point(centerX, centerY);
        }

        private void CentrarBotonesEnPanel()
        {
            // Definir el espacio deseado entre los botones
            int espacioEntreBotones = 20;

            // Sumar el ancho total que ocupan ambos botones y el espacio entre ellos
            int anchoTotal = btnAceptar.Width + espacioEntreBotones + btnCancelar.Width;

            // Calcular el punto X de inicio para que la suma quede centrada en el panel
            int startX = (panelBotones.Width - anchoTotal) / 2;

            // Calcular la posición vertical centrada (misma para ambos botones)
            int centerY = (panelBotones.Height - btnAceptar.Height) / 2;

            // Asignar posición a btnOposicion
            btnAceptar.Location = new Point(startX, centerY);

            // Asignar posición a btnCancelar, que va a la derecha del btnOposicion con el espacio definido
            btnCancelar.Location = new Point(startX + btnAceptar.Width + espacioEntreBotones, centerY);
        }

        private void InicializarControlesHistorial()
        {

            int margenIzquierdo = 20;
            int espacioEntreControles = 20;

            // Label: Usuario centrado arriba
            lblUser.Text = $"Usuario: {UsuarioActivo.usuario}";
            lblUser.AutoSize = true;
            lblUser.ForeColor = System.Drawing.Color.Gray;
            lblUser.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Italic);
            
            lblUser.Location = new Point((groupBoxHistorial.Width - lblUser.PreferredWidth) / 2, 10);


            labelFecha.AutoSize = true;
            labelFecha.Location = new Point(margenIzquierdo, lblUser.Bottom + espacioEntreControles);

            labelEstado.AutoSize = true;
            labelEstado.Location = new Point(labelFecha.Right + 150, labelFecha.Top);



            // TextBox: Estado debajo de labelEstado
            txtEstado.Width = 150;
            txtEstado.Location = new Point(labelEstado.Left, labelEstado.Bottom + 5);
            txtEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            

            // DateTimePicker debajo de labelFecha
            dateTimePickerFecha.Location = new Point(labelFecha.Left, labelFecha.Bottom + 5);
            dateTimePickerFecha.Width = groupBoxHistorial.ClientSize.Width / 3;
            dateTimePickerFecha.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            

            // Label: Anotaciones
            labelAnotaciones.AutoSize = true;
            labelAnotaciones.Location = new Point(labelFecha.Left, Math.Max(dateTimePickerFecha.Bottom, txtEstado.Bottom) + espacioEntreControles);


            // TextBox: Anotaciones
            textBoxAnotaciones.Multiline = true;
            textBoxAnotaciones.ScrollBars = RichTextBoxScrollBars.Vertical;
            textBoxAnotaciones.Location = new Point(labelAnotaciones.Left, labelAnotaciones.Bottom + 5);
            textBoxAnotaciones.Size = new Size(groupBoxHistorial.ClientSize.Width - 40, groupBoxHistorial.ClientSize.Height - labelAnotaciones.Bottom - 40);
            textBoxAnotaciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            
            groupBoxHistorial.Resize += (s, e) =>
            {
                lblUser.Location = new Point((groupBoxHistorial.Width - lblUser.PreferredWidth) / 2, 10);
                dateTimePickerFecha.Width = groupBoxHistorial.ClientSize.Width / 3;
                textBoxAnotaciones.Size = new Size(groupBoxHistorial.ClientSize.Width - 40, groupBoxHistorial.ClientSize.Height - textBoxAnotaciones.Top - 20);
            };
        }

        private void InicializarControlesTraspaso()
        {
            int espacioVertical = 5;
            int espacioEntreControles = 15;
            int margenLateral = 10;

            int alturaLabel = 20;
            int alturaTextBox = 25;
            int alturaBoton = btnNuevoTitular.Height;

            // Calcular altura total ocupada por los controles
            int alturaTotal =
                alturaLabel + espacioVertical +      // labelNoExpediente
                alturaTextBox + espacioEntreControles + // txtNoExpediente
                alturaLabel + espacioVertical +      // labelAntiguoTitular
                alturaTextBox + espacioEntreControles + // txtNombreTitularA
                alturaBoton + espacioVertical +      // btnNuevoTitular
                alturaTextBox;                       // txtNombreTitularN

            // Centrado vertical
            int topInicial = (groupBoxTraspaso.ClientSize.Height - alturaTotal) / 2;

            // Label: No. Expediente (centrado horizontal)
            labelNoExpediente.Text = "No.Expediente";
            labelNoExpediente.AutoSize = true;
            labelNoExpediente.Location = new Point(
                (groupBoxTraspaso.ClientSize.Width - labelNoExpediente.PreferredWidth) / 2,
                topInicial
            );

            // TextBox: No. Expediente
            txtNoExpediente.Location = new Point(margenLateral, labelNoExpediente.Bottom + espacioVertical);
            txtNoExpediente.Width = groupBoxTraspaso.ClientSize.Width - 2 * margenLateral;
            txtNoExpediente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;


            // Label: Antiguo Titular
            labelAntiguoTitular.AutoSize = true;
            labelAntiguoTitular.Location = new Point(margenLateral, txtNoExpediente.Bottom + espacioEntreControles);

            // TextBox: Antiguo Titular
            txtNombreTitularA.Location = new Point(margenLateral, labelAntiguoTitular.Bottom + espacioVertical);
            txtNombreTitularA.Width = groupBoxTraspaso.ClientSize.Width - 2 * margenLateral;
            txtNombreTitularA.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Botón: Nuevo Titular
            btnNuevoTitular.AutoSize = true;
            btnNuevoTitular.Location = new Point(margenLateral, txtNombreTitularA.Bottom + espacioEntreControles);

            // TextBox: Nuevo Titular
            txtNombreTitularN.Location = new Point(margenLateral, btnNuevoTitular.Bottom + espacioVertical);
            txtNombreTitularN.Width = groupBoxTraspaso.ClientSize.Width - 2 * margenLateral;
            txtNombreTitularN.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Reposicionar automáticamente al cambiar tamaño del contenedor
            groupBoxTraspaso.Resize -= groupBoxTraspaso_Resize; // evitar acumulación
            groupBoxTraspaso.Resize += groupBoxTraspaso_Resize;
        }

        private void ConfigurarControlesTraspaso()
        {
            // Asumiendo que los controles ya están creados en el diseñador y tienen estos nombres:
            // labelNoExpediente, txtNoExpediente, labelAntiguoTitular, txtNombreTitularA, btnNuevoTitular, txtNombreTitularN

            int espacioVertical = 5;
            int espacioEntreControles = 15;
            int margenLateral = 10;

            if (groupBoxTraspaso.Controls["labelNoExpediente"] is Label labelNoExpediente)
            {
                labelNoExpediente.Text = "No.Expediente";
                labelNoExpediente.AutoSize = true;
                labelNoExpediente.Location = new Point((groupBoxTraspaso.ClientSize.Width - labelNoExpediente.PreferredWidth) / 2, 10);
                labelNoExpediente.Visible = true;
            }

            if (groupBoxTraspaso.Controls["txtNoExpediente"] is TextBox txtNoExpediente && groupBoxTraspaso.Controls["labelNoExpediente"] is Label lblNoExp)
            {
                txtNoExpediente.Location = new Point(margenLateral, lblNoExp.Bottom + espacioVertical);
                txtNoExpediente.Width = groupBoxTraspaso.ClientSize.Width - 2 * margenLateral;
                txtNoExpediente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }

            if (groupBoxTraspaso.Controls["labelAntiguoTitular"] is Label labelAntiguoTitular && groupBoxTraspaso.Controls["txtNoExpediente"] is TextBox txtNoExp)
            {
                labelAntiguoTitular.Text = "Antiguo titular";
                labelAntiguoTitular.AutoSize = true;
                labelAntiguoTitular.Location = new Point(margenLateral, txtNoExp.Bottom + espacioEntreControles);
            }

            if (groupBoxTraspaso.Controls["txtNombreTitularA"] is TextBox txtNombreTitularA && groupBoxTraspaso.Controls["labelAntiguoTitular"] is Label lblAntTit)
            {
                txtNombreTitularA.Location = new Point(margenLateral, lblAntTit.Bottom + espacioVertical);
                txtNombreTitularA.Width = groupBoxTraspaso.ClientSize.Width - 2 * margenLateral;
                txtNombreTitularA.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }

            if (groupBoxTraspaso.Controls["btnNuevoTitular"] is Button btnNuevoTitular && groupBoxTraspaso.Controls["txtNombreTitularA"] is TextBox txtAntTitA)
            {
                btnNuevoTitular.Text = "Nuevo titular";
                btnNuevoTitular.Location = new Point(margenLateral, txtAntTitA.Bottom + espacioEntreControles);
                btnNuevoTitular.AutoSize = true;
            }

            if (groupBoxTraspaso.Controls["txtNombreTitularN"] is TextBox txtNombreTitularN && groupBoxTraspaso.Controls["btnNuevoTitular"] is Button btnNewTit)
            {
                txtNombreTitularN.Location = new Point(margenLateral, btnNewTit.Bottom + espacioVertical);
                txtNombreTitularN.Width = groupBoxTraspaso.ClientSize.Width - 2 * margenLateral;
                txtNombreTitularN.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }

            // Reasignar evento Resize para ajustar posiciones cuando se redimensione el groupBox
            groupBoxTraspaso.Resize -= groupBoxTraspaso_Resize;
            groupBoxTraspaso.Resize += groupBoxTraspaso_Resize;

            groupBoxTraspaso.PerformLayout();
            groupBoxTraspaso.Refresh();

        }


        private void FrmCrearTraspasoInt_Load(object sender, EventArgs e)
        {

            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            if (screenWidth <= 1105 && screenHeight <= 600)
            {
                this.Size = new Size(750, 540);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.AutoScroll = true;
                groupBoxHistorial.Margin = new Padding(0);
                groupBoxTraspaso.Margin = new Padding(0);

            }

            InicializarControlesHistorial();
            InicializarControlesTraspaso();
            CentrarBotonEncabezado();
            CentrarBotonesEnPanel();

            lblUser.Text = UsuarioActivo.usuario;
            lblUser.Visible = false;
            txtNoExpediente.Text = SeleccionarMarca.etraspaso;
            txtNombreTitularA.Text = AgregarTraspaso.nombreTitulara;
            textBoxAnotaciones.Text = dateTimePickerFecha.Value.ToString("dd/MM/yyyy") + " " + txtEstado.Text;
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

            string anotaciones = textBoxAnotaciones.Text;
            AgregarEtapa.etapa = txtEstado.Text;
            AgregarEtapa.fecha = dateTimePickerFecha.Value;
            AgregarEtapa.usuario = UsuarioActivo.usuario;

            // Datos del traspaso
            string noExpediente = txtNoExpediente.Text;
            int idMarca = SeleccionarMarca.idN;
            string nuevoTitular = txtNombreTitularN.Text;

            if (!string.IsNullOrEmpty(txtEstado.Text) && !string.IsNullOrEmpty(txtNoExpediente.Text))
            {
                if (!string.IsNullOrEmpty(nuevoTitular))
                {
                    // Formatear anotaciones con fecha y etapa
                    string fechaSinHora = dateTimePickerFecha.Value.ToString("dd/MM/yyyy");
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
            textBoxAnotaciones.Text = dateTimePickerFecha.Value.ToString("dd/MM/yyyy") + " " + txtEstado.Text;
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

        private void btnTraspasar_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxHistorial_Resize(object sender, EventArgs e)
        {
            if (groupBoxHistorial.Controls["lblUser"] is Label lblUser)
            {
                lblUser.Location = new Point((groupBoxHistorial.Width - lblUser.PreferredWidth) / 2, 10);
            }
            if (groupBoxHistorial.Controls["dateTimePickerFecha"] is DateTimePicker dtp)
            {
                dtp.Width = groupBoxHistorial.ClientSize.Width / 3;
            }
            if (groupBoxHistorial.Controls["textBoxAnotaciones"] is TextBox txtAnotaciones && groupBoxHistorial.Controls["labelAnotaciones"] is Label lblAnot)
            {
                txtAnotaciones.Size = new Size(groupBoxHistorial.ClientSize.Width - 40, groupBoxHistorial.ClientSize.Height - txtAnotaciones.Top - 20);
            }
        }

        private void groupBoxTraspaso_Resize(object sender, EventArgs e)
        {
            ConfigurarControlesHistorial();
            ConfigurarControlesTraspaso();
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
