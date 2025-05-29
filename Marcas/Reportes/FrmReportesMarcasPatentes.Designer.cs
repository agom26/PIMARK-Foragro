namespace Presentacion.Reportes
{
    partial class FrmReportesMarcasPatentes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel6 = new Panel();
            richTextBoxCliente = new RichTextBox();
            checkBoxSolicitud = new CheckBox();
            roundedButton5 = new Presentacion.Clases.RoundedButton();
            dtpFechaRegistroFinal = new DateTimePicker();
            checkBoxCliente = new CheckBox();
            dtpFRegistroInicial = new DateTimePicker();
            richTextBoxAgente = new RichTextBox();
            dtpSolicitudFinal = new DateTimePicker();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            dtpVencimientoInicial = new DateTimePicker();
            roundedButton4 = new Presentacion.Clases.RoundedButton();
            dtpSolicitudInicial = new DateTimePicker();
            richTextBoxTitular = new RichTextBox();
            dtpVencimientoFinal = new DateTimePicker();
            checkBoxAgente = new CheckBox();
            checkBoxReigstro = new CheckBox();
            checkBoxTitular = new CheckBox();
            checkBoxVencimiento = new CheckBox();
            panel5 = new Panel();
            comboBoxObjeto = new ComboBox();
            txtClase = new TextBox();
            checkBoxEstado = new CheckBox();
            checkBoxClase = new CheckBox();
            txtRegistro = new TextBox();
            comboBoxEstado = new ComboBox();
            checkBoxRegistro = new CheckBox();
            txtTomo = new TextBox();
            txtNombre = new TextBox();
            checkBoxTomo = new CheckBox();
            checkBoxNombre = new CheckBox();
            txtFolio = new TextBox();
            checkBoxFolio = new CheckBox();
            comboBoxPais = new ComboBox();
            checkBoxPais = new CheckBox();
            panel1 = new Panel();
            roundedButton2 = new Presentacion.Clases.RoundedButton();
            btnConsultar = new Presentacion.Clases.RoundedButton();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            btnCancelar = new Presentacion.Clases.RoundedButton();
            roundedButton6 = new Presentacion.Clases.RoundedButton();
            panel2 = new Panel();
            dtgReportes = new DataGridView();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgReportes).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 10F);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(848, 591);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.AutoScroll = true;
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(roundedButton2);
            tabPage1.Controls.Add(btnConsultar);
            tabPage1.Controls.Add(roundedButton3);
            tabPage1.Controls.Add(btnCancelar);
            tabPage1.Controls.Add(roundedButton6);
            tabPage1.Controls.Add(panel2);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(840, 561);
            tabPage1.TabIndex = 0;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.BackColor = Color.FromArgb(222, 227, 234);
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(panel6, 1, 0);
            tableLayoutPanel1.Controls.Add(panel5, 0, 0);
            tableLayoutPanel1.Location = new Point(36, 56);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(736, 534);
            tableLayoutPanel1.TabIndex = 242;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Left;
            panel6.AutoScroll = true;
            panel6.BackColor = Color.FromArgb(222, 227, 234);
            panel6.Controls.Add(richTextBoxCliente);
            panel6.Controls.Add(checkBoxSolicitud);
            panel6.Controls.Add(roundedButton5);
            panel6.Controls.Add(dtpFechaRegistroFinal);
            panel6.Controls.Add(checkBoxCliente);
            panel6.Controls.Add(dtpFRegistroInicial);
            panel6.Controls.Add(richTextBoxAgente);
            panel6.Controls.Add(dtpSolicitudFinal);
            panel6.Controls.Add(roundedButton1);
            panel6.Controls.Add(dtpVencimientoInicial);
            panel6.Controls.Add(roundedButton4);
            panel6.Controls.Add(dtpSolicitudInicial);
            panel6.Controls.Add(richTextBoxTitular);
            panel6.Controls.Add(dtpVencimientoFinal);
            panel6.Controls.Add(checkBoxAgente);
            panel6.Controls.Add(checkBoxReigstro);
            panel6.Controls.Add(checkBoxTitular);
            panel6.Controls.Add(checkBoxVencimiento);
            panel6.Location = new Point(371, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(344, 528);
            panel6.TabIndex = 244;
            // 
            // richTextBoxCliente
            // 
            richTextBoxCliente.Location = new Point(76, 434);
            richTextBoxCliente.Margin = new Padding(3, 2, 3, 2);
            richTextBoxCliente.Name = "richTextBoxCliente";
            richTextBoxCliente.ReadOnly = true;
            richTextBoxCliente.Size = new Size(242, 36);
            richTextBoxCliente.TabIndex = 240;
            richTextBoxCliente.Text = "";
            richTextBoxCliente.TextChanged += richTextBoxCliente_TextChanged;
            // 
            // checkBoxSolicitud
            // 
            checkBoxSolicitud.AutoSize = true;
            checkBoxSolicitud.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxSolicitud.Location = new Point(3, 81);
            checkBoxSolicitud.Margin = new Padding(3, 2, 3, 2);
            checkBoxSolicitud.Name = "checkBoxSolicitud";
            checkBoxSolicitud.Size = new Size(97, 23);
            checkBoxSolicitud.TabIndex = 205;
            checkBoxSolicitud.Text = "F.Solicitud";
            checkBoxSolicitud.UseVisualStyleBackColor = false;
            // 
            // roundedButton5
            // 
            roundedButton5.BackColor = Color.LightSteelBlue;
            roundedButton5.BackgroundColor = Color.LightSteelBlue;
            roundedButton5.BorderColor = Color.LightSteelBlue;
            roundedButton5.BorderRadius = 26;
            roundedButton5.BorderSize = 0;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.Font = new Font("Century Gothic", 10F);
            roundedButton5.ForeColor = Color.Black;
            roundedButton5.Location = new Point(76, 404);
            roundedButton5.Margin = new Padding(3, 2, 3, 2);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(170, 26);
            roundedButton5.TabIndex = 239;
            roundedButton5.Text = "+ CLIENTE";
            roundedButton5.TextColor = Color.Black;
            roundedButton5.UseVisualStyleBackColor = false;
            roundedButton5.Click += roundedButton5_Click;
            // 
            // dtpFechaRegistroFinal
            // 
            dtpFechaRegistroFinal.Format = DateTimePickerFormat.Short;
            dtpFechaRegistroFinal.Location = new Point(218, 127);
            dtpFechaRegistroFinal.Margin = new Padding(3, 2, 3, 2);
            dtpFechaRegistroFinal.Name = "dtpFechaRegistroFinal";
            dtpFechaRegistroFinal.Size = new Size(103, 24);
            dtpFechaRegistroFinal.TabIndex = 222;
            dtpFechaRegistroFinal.ValueChanged += dtpFechaRegistroFinal_ValueChanged;
            // 
            // checkBoxCliente
            // 
            checkBoxCliente.AutoSize = true;
            checkBoxCliente.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxCliente.Location = new Point(3, 403);
            checkBoxCliente.Margin = new Padding(3, 2, 3, 2);
            checkBoxCliente.Name = "checkBoxCliente";
            checkBoxCliente.Size = new Size(77, 23);
            checkBoxCliente.TabIndex = 238;
            checkBoxCliente.Text = "Cliente";
            checkBoxCliente.UseVisualStyleBackColor = false;
            // 
            // dtpFRegistroInicial
            // 
            dtpFRegistroInicial.Format = DateTimePickerFormat.Short;
            dtpFRegistroInicial.Location = new Point(106, 127);
            dtpFRegistroInicial.Margin = new Padding(3, 2, 3, 2);
            dtpFRegistroInicial.Name = "dtpFRegistroInicial";
            dtpFRegistroInicial.Size = new Size(103, 24);
            dtpFRegistroInicial.TabIndex = 220;
            dtpFRegistroInicial.ValueChanged += dtpFRegistroInicial_ValueChanged;
            // 
            // richTextBoxAgente
            // 
            richTextBoxAgente.Location = new Point(76, 342);
            richTextBoxAgente.Margin = new Padding(3, 2, 3, 2);
            richTextBoxAgente.Name = "richTextBoxAgente";
            richTextBoxAgente.ReadOnly = true;
            richTextBoxAgente.Size = new Size(242, 36);
            richTextBoxAgente.TabIndex = 235;
            richTextBoxAgente.Text = "";
            // 
            // dtpSolicitudFinal
            // 
            dtpSolicitudFinal.Format = DateTimePickerFormat.Short;
            dtpSolicitudFinal.Location = new Point(218, 84);
            dtpSolicitudFinal.Margin = new Padding(3, 2, 3, 2);
            dtpSolicitudFinal.Name = "dtpSolicitudFinal";
            dtpSolicitudFinal.Size = new Size(103, 24);
            dtpSolicitudFinal.TabIndex = 224;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.LightSteelBlue;
            roundedButton1.BackgroundColor = Color.LightSteelBlue;
            roundedButton1.BorderColor = Color.LightSteelBlue;
            roundedButton1.BorderRadius = 26;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 10F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(76, 312);
            roundedButton1.Margin = new Padding(3, 2, 3, 2);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(170, 26);
            roundedButton1.TabIndex = 234;
            roundedButton1.Text = "+ AGENTE";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // dtpVencimientoInicial
            // 
            dtpVencimientoInicial.Format = DateTimePickerFormat.Short;
            dtpVencimientoInicial.Location = new Point(106, 173);
            dtpVencimientoInicial.Margin = new Padding(3, 2, 3, 2);
            dtpVencimientoInicial.Name = "dtpVencimientoInicial";
            dtpVencimientoInicial.Size = new Size(103, 24);
            dtpVencimientoInicial.TabIndex = 221;
            dtpVencimientoInicial.ValueChanged += dtpVencimientoInicial_ValueChanged;
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.LightSteelBlue;
            roundedButton4.BackgroundColor = Color.LightSteelBlue;
            roundedButton4.BorderColor = Color.LightSteelBlue;
            roundedButton4.BorderRadius = 26;
            roundedButton4.BorderSize = 0;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.Font = new Font("Century Gothic", 10F);
            roundedButton4.ForeColor = Color.Black;
            roundedButton4.Location = new Point(76, 220);
            roundedButton4.Margin = new Padding(3, 2, 3, 2);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(170, 26);
            roundedButton4.TabIndex = 230;
            roundedButton4.Text = "+ TITULAR";
            roundedButton4.TextColor = Color.Black;
            roundedButton4.UseVisualStyleBackColor = false;
            roundedButton4.Click += roundedButton4_Click;
            // 
            // dtpSolicitudInicial
            // 
            dtpSolicitudInicial.Format = DateTimePickerFormat.Short;
            dtpSolicitudInicial.Location = new Point(106, 84);
            dtpSolicitudInicial.Margin = new Padding(3, 2, 3, 2);
            dtpSolicitudInicial.Name = "dtpSolicitudInicial";
            dtpSolicitudInicial.Size = new Size(103, 24);
            dtpSolicitudInicial.TabIndex = 219;
            dtpSolicitudInicial.ValueChanged += dtpSolicitudInicial_ValueChanged;
            // 
            // richTextBoxTitular
            // 
            richTextBoxTitular.Location = new Point(76, 250);
            richTextBoxTitular.Margin = new Padding(3, 2, 3, 2);
            richTextBoxTitular.Name = "richTextBoxTitular";
            richTextBoxTitular.ReadOnly = true;
            richTextBoxTitular.Size = new Size(242, 36);
            richTextBoxTitular.TabIndex = 232;
            richTextBoxTitular.Text = "";
            richTextBoxTitular.TextChanged += richTextBoxTitular_TextChanged;
            // 
            // dtpVencimientoFinal
            // 
            dtpVencimientoFinal.Format = DateTimePickerFormat.Short;
            dtpVencimientoFinal.Location = new Point(218, 173);
            dtpVencimientoFinal.Margin = new Padding(3, 2, 3, 2);
            dtpVencimientoFinal.Name = "dtpVencimientoFinal";
            dtpVencimientoFinal.Size = new Size(103, 24);
            dtpVencimientoFinal.TabIndex = 223;
            dtpVencimientoFinal.ValueChanged += dtpVencimientoFinal_ValueChanged;
            // 
            // checkBoxAgente
            // 
            checkBoxAgente.AutoSize = true;
            checkBoxAgente.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxAgente.Location = new Point(3, 311);
            checkBoxAgente.Margin = new Padding(3, 2, 3, 2);
            checkBoxAgente.Name = "checkBoxAgente";
            checkBoxAgente.Size = new Size(78, 23);
            checkBoxAgente.TabIndex = 233;
            checkBoxAgente.Text = "Agente";
            checkBoxAgente.UseVisualStyleBackColor = false;
            // 
            // checkBoxReigstro
            // 
            checkBoxReigstro.AutoSize = true;
            checkBoxReigstro.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxReigstro.Location = new Point(3, 127);
            checkBoxReigstro.Margin = new Padding(3, 2, 3, 2);
            checkBoxReigstro.Name = "checkBoxReigstro";
            checkBoxReigstro.Size = new Size(92, 23);
            checkBoxReigstro.TabIndex = 206;
            checkBoxReigstro.Text = "F.Registro";
            checkBoxReigstro.UseVisualStyleBackColor = false;
            // 
            // checkBoxTitular
            // 
            checkBoxTitular.AutoSize = true;
            checkBoxTitular.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxTitular.Location = new Point(3, 219);
            checkBoxTitular.Margin = new Padding(3, 2, 3, 2);
            checkBoxTitular.Name = "checkBoxTitular";
            checkBoxTitular.Size = new Size(67, 23);
            checkBoxTitular.TabIndex = 202;
            checkBoxTitular.Text = "Titular";
            checkBoxTitular.UseVisualStyleBackColor = false;
            // 
            // checkBoxVencimiento
            // 
            checkBoxVencimiento.AutoSize = true;
            checkBoxVencimiento.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxVencimiento.Location = new Point(3, 174);
            checkBoxVencimiento.Margin = new Padding(3, 2, 3, 2);
            checkBoxVencimiento.Name = "checkBoxVencimiento";
            checkBoxVencimiento.Size = new Size(75, 23);
            checkBoxVencimiento.TabIndex = 207;
            checkBoxVencimiento.Text = "F.Venc";
            checkBoxVencimiento.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Right;
            panel5.AutoScroll = true;
            panel5.BackColor = Color.FromArgb(222, 227, 234);
            panel5.Controls.Add(comboBoxObjeto);
            panel5.Controls.Add(txtClase);
            panel5.Controls.Add(checkBoxEstado);
            panel5.Controls.Add(checkBoxClase);
            panel5.Controls.Add(txtRegistro);
            panel5.Controls.Add(comboBoxEstado);
            panel5.Controls.Add(checkBoxRegistro);
            panel5.Controls.Add(txtTomo);
            panel5.Controls.Add(txtNombre);
            panel5.Controls.Add(checkBoxTomo);
            panel5.Controls.Add(checkBoxNombre);
            panel5.Controls.Add(txtFolio);
            panel5.Controls.Add(checkBoxFolio);
            panel5.Controls.Add(comboBoxPais);
            panel5.Controls.Add(checkBoxPais);
            panel5.Location = new Point(22, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(343, 528);
            panel5.TabIndex = 243;
            panel5.Paint += panel5_Paint;
            // 
            // comboBoxObjeto
            // 
            comboBoxObjeto.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxObjeto.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxObjeto.FlatStyle = FlatStyle.Flat;
            comboBoxObjeto.Font = new Font("Century Gothic", 9F);
            comboBoxObjeto.FormattingEnabled = true;
            comboBoxObjeto.Items.AddRange(new object[] { "Marcas nacionales", "Marcas internacionales", "Marcas nacionales e internacionales", "Patentes", "Vencimientos marcas internacionales", "Vencimientos marcas nacionales", "Vencimientos marcas nacionales e internacionales", "Vencimientos patentes" });
            comboBoxObjeto.Location = new Point(8, 83);
            comboBoxObjeto.Margin = new Padding(3, 2, 3, 2);
            comboBoxObjeto.Name = "comboBoxObjeto";
            comboBoxObjeto.Size = new Size(305, 25);
            comboBoxObjeto.TabIndex = 237;
            comboBoxObjeto.SelectedIndexChanged += comboBoxObjeto_SelectedIndexChanged;
            // 
            // txtClase
            // 
            txtClase.Location = new Point(85, 405);
            txtClase.Margin = new Padding(3, 2, 3, 2);
            txtClase.Name = "txtClase";
            txtClase.Size = new Size(228, 24);
            txtClase.TabIndex = 229;
            // 
            // checkBoxEstado
            // 
            checkBoxEstado.AutoSize = true;
            checkBoxEstado.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxEstado.Location = new Point(3, 127);
            checkBoxEstado.Margin = new Padding(3, 2, 3, 2);
            checkBoxEstado.Name = "checkBoxEstado";
            checkBoxEstado.Size = new Size(75, 23);
            checkBoxEstado.TabIndex = 196;
            checkBoxEstado.Text = "Estado";
            checkBoxEstado.UseVisualStyleBackColor = false;
            // 
            // checkBoxClase
            // 
            checkBoxClase.AutoSize = true;
            checkBoxClase.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxClase.Location = new Point(3, 406);
            checkBoxClase.Margin = new Padding(3, 2, 3, 2);
            checkBoxClase.Name = "checkBoxClase";
            checkBoxClase.Size = new Size(66, 23);
            checkBoxClase.TabIndex = 204;
            checkBoxClase.Text = "Clase";
            checkBoxClase.UseVisualStyleBackColor = false;
            // 
            // txtRegistro
            // 
            txtRegistro.Location = new Point(85, 359);
            txtRegistro.Margin = new Padding(3, 2, 3, 2);
            txtRegistro.Name = "txtRegistro";
            txtRegistro.Size = new Size(228, 24);
            txtRegistro.TabIndex = 228;
            txtRegistro.TextChanged += txtRegistro_TextChanged;
            // 
            // comboBoxEstado
            // 
            comboBoxEstado.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEstado.FlatStyle = FlatStyle.Flat;
            comboBoxEstado.Font = new Font("Century Gothic", 9F);
            comboBoxEstado.FormattingEnabled = true;
            comboBoxEstado.Items.AddRange(new object[] { "", "Ingresada", "Examen de forma", "Examen de fondo", "Requerimiento", "Objeción", "Resolución RPI favorable", "Resolución RPI desfavorable", "Recurso de revocatoria", "Resolución Ministerio de Economía (MINECO)", "Contencioso administrativo", "Edicto", "Publicación", "Orden de pago", "Abandono", "Registrada", "Licencia de uso", "Trámite de renovación", "Trámite de traspaso" });
            comboBoxEstado.Location = new Point(85, 129);
            comboBoxEstado.Margin = new Padding(3, 2, 3, 2);
            comboBoxEstado.Name = "comboBoxEstado";
            comboBoxEstado.Size = new Size(228, 25);
            comboBoxEstado.TabIndex = 217;
            // 
            // checkBoxRegistro
            // 
            checkBoxRegistro.AutoSize = true;
            checkBoxRegistro.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxRegistro.Location = new Point(3, 360);
            checkBoxRegistro.Margin = new Padding(3, 2, 3, 2);
            checkBoxRegistro.Name = "checkBoxRegistro";
            checkBoxRegistro.Size = new Size(81, 23);
            checkBoxRegistro.TabIndex = 201;
            checkBoxRegistro.Text = "Registro";
            checkBoxRegistro.UseVisualStyleBackColor = false;
            // 
            // txtTomo
            // 
            txtTomo.Location = new Point(85, 313);
            txtTomo.Margin = new Padding(3, 2, 3, 2);
            txtTomo.Name = "txtTomo";
            txtTomo.Size = new Size(228, 24);
            txtTomo.TabIndex = 227;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(85, 221);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Multiline = true;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(228, 24);
            txtNombre.TabIndex = 225;
            // 
            // checkBoxTomo
            // 
            checkBoxTomo.AutoSize = true;
            checkBoxTomo.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxTomo.Location = new Point(3, 311);
            checkBoxTomo.Margin = new Padding(3, 2, 3, 2);
            checkBoxTomo.Name = "checkBoxTomo";
            checkBoxTomo.Size = new Size(64, 23);
            checkBoxTomo.TabIndex = 200;
            checkBoxTomo.Text = "Tomo";
            checkBoxTomo.UseVisualStyleBackColor = false;
            // 
            // checkBoxNombre
            // 
            checkBoxNombre.AutoSize = true;
            checkBoxNombre.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxNombre.Location = new Point(3, 222);
            checkBoxNombre.Margin = new Padding(3, 2, 3, 2);
            checkBoxNombre.Name = "checkBoxNombre";
            checkBoxNombre.Size = new Size(83, 23);
            checkBoxNombre.TabIndex = 197;
            checkBoxNombre.Text = "Nombre";
            checkBoxNombre.UseVisualStyleBackColor = false;
            // 
            // txtFolio
            // 
            txtFolio.Location = new Point(85, 267);
            txtFolio.Margin = new Padding(3, 2, 3, 2);
            txtFolio.Name = "txtFolio";
            txtFolio.Size = new Size(228, 24);
            txtFolio.TabIndex = 226;
            txtFolio.TextChanged += txtFolio_TextChanged;
            // 
            // checkBoxFolio
            // 
            checkBoxFolio.AutoSize = true;
            checkBoxFolio.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxFolio.Location = new Point(3, 268);
            checkBoxFolio.Margin = new Padding(3, 2, 3, 2);
            checkBoxFolio.Name = "checkBoxFolio";
            checkBoxFolio.Size = new Size(59, 23);
            checkBoxFolio.TabIndex = 199;
            checkBoxFolio.Text = "Folio";
            checkBoxFolio.UseVisualStyleBackColor = false;
            // 
            // comboBoxPais
            // 
            comboBoxPais.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxPais.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPais.FlatStyle = FlatStyle.Flat;
            comboBoxPais.Font = new Font("Century Gothic", 9F);
            comboBoxPais.FormattingEnabled = true;
            comboBoxPais.Items.AddRange(new object[] { "", "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita", "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Baréin", "Bangladés", "Barbados", "Bielorrusia", "Birmania (Myanmar)", "Burundi", "Bután", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo (Congo-Brazzaville)", "Corea del Norte", "Corea del Sur", "Costa Rica", "Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Eswatini", "Etiopía", "Fiyi", "Filipinas", "Finlandia", "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Grecia", "Granada", "Guatemala", "Guinea", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Hungría", "Islandia", "India", "Indonesia", "Irán", "Irak", "Irlanda", "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kosovo", "Kuwait", "Laos", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein", "Lituania", "Luxemburgo", "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Mauricio", "Mauritania", "México", "Micronesia", "Moldavia", "Mónaco", "Mongolia", "Mozambique", "Namibia", "Nauru", "Nepal", "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Pakistán", "Palaos", "Palestina", "Panamá", "Paraguay", "Perú", "Polonia", "Portugal", "Qatar", "República Centroafricana", "República Checa", "República del Congo (Congo-Kinshasa)", "República Dominicana", "Rumania", "Rusia", "Ruanda", "San Cristóbal y Nieves", "San Marino", "Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Tailandia", "Taiwán", "Tanzania", "Tayikistán", "Timor Oriental", "Togo", "Tonga", "Trinidad y Tobago", "Túnez", "Turquía", "Turkmenistán", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabue" });
            comboBoxPais.Location = new Point(85, 175);
            comboBoxPais.Margin = new Padding(3, 2, 3, 2);
            comboBoxPais.Name = "comboBoxPais";
            comboBoxPais.Size = new Size(228, 25);
            comboBoxPais.TabIndex = 218;
            // 
            // checkBoxPais
            // 
            checkBoxPais.AutoSize = true;
            checkBoxPais.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxPais.Location = new Point(3, 176);
            checkBoxPais.Margin = new Padding(3, 2, 3, 2);
            checkBoxPais.Name = "checkBoxPais";
            checkBoxPais.Size = new Size(54, 23);
            checkBoxPais.TabIndex = 198;
            checkBoxPais.Text = "Pais";
            checkBoxPais.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSize = true;
            panel1.Location = new Point(7, 1158);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(81, 36);
            panel1.TabIndex = 236;
            // 
            // roundedButton2
            // 
            roundedButton2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            roundedButton2.AutoSize = true;
            roundedButton2.BackColor = Color.FromArgb(0, 137, 123);
            roundedButton2.BackgroundColor = Color.FromArgb(0, 137, 123);
            roundedButton2.BorderColor = Color.FromArgb(0, 137, 123);
            roundedButton2.BorderRadius = 33;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Image = Properties.Resources.excel;
            roundedButton2.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton2.Location = new Point(594, 1115);
            roundedButton2.Margin = new Padding(3, 2, 3, 2);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(178, 37);
            roundedButton2.TabIndex = 214;
            roundedButton2.Text = "EXPORTAR A EXCEL";
            roundedButton2.TextColor = Color.White;
            roundedButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // btnConsultar
            // 
            btnConsultar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConsultar.BackColor = Color.FromArgb(251, 140, 0);
            btnConsultar.BackgroundColor = Color.FromArgb(251, 140, 0);
            btnConsultar.BorderColor = Color.FromArgb(251, 140, 0);
            btnConsultar.BorderRadius = 37;
            btnConsultar.BorderSize = 0;
            btnConsultar.FlatAppearance.BorderSize = 0;
            btnConsultar.FlatStyle = FlatStyle.Flat;
            btnConsultar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnConsultar.ForeColor = Color.White;
            btnConsultar.Location = new Point(412, 596);
            btnConsultar.Margin = new Padding(3, 2, 3, 2);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(178, 37);
            btnConsultar.TabIndex = 215;
            btnConsultar.Text = "CONSULTAR";
            btnConsultar.TextColor = Color.White;
            btnConsultar.UseVisualStyleBackColor = false;
            btnConsultar.Click += btnConsultar_Click;
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            roundedButton3.AutoSize = true;
            roundedButton3.BackColor = Color.FromArgb(229, 115, 115);
            roundedButton3.BackgroundColor = Color.FromArgb(229, 115, 115);
            roundedButton3.BorderColor = Color.FromArgb(229, 115, 115);
            roundedButton3.BorderRadius = 33;
            roundedButton3.BorderSize = 0;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Image = Properties.Resources.pdf_1_;
            roundedButton3.ImageAlign = ContentAlignment.MiddleLeft;
            roundedButton3.Location = new Point(411, 1115);
            roundedButton3.Margin = new Padding(3, 2, 3, 2);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(178, 37);
            roundedButton3.TabIndex = 215;
            roundedButton3.Text = "EXPORTAR A PDF";
            roundedButton3.TextColor = Color.White;
            roundedButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            roundedButton3.UseVisualStyleBackColor = false;
            roundedButton3.Click += roundedButton3_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.BackColor = Color.Gainsboro;
            btnCancelar.BackgroundColor = Color.Gainsboro;
            btnCancelar.BorderColor = Color.Gainsboro;
            btnCancelar.BorderRadius = 37;
            btnCancelar.BorderSize = 0;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(594, 595);
            btnCancelar.Margin = new Padding(3, 2, 3, 2);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(178, 37);
            btnCancelar.TabIndex = 214;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // roundedButton6
            // 
            roundedButton6.Anchor = AnchorStyles.Top;
            roundedButton6.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton6.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton6.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton6.BorderRadius = 37;
            roundedButton6.BorderSize = 0;
            roundedButton6.FlatAppearance.BorderSize = 0;
            roundedButton6.FlatStyle = FlatStyle.Flat;
            roundedButton6.Font = new Font("Century Gothic", 15F);
            roundedButton6.ForeColor = Color.Black;
            roundedButton6.Image = Properties.Resources.simbolo_de_interfaz_grafica_de_tres_barras;
            roundedButton6.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton6.Location = new Point(212, 4);
            roundedButton6.Margin = new Padding(3, 2, 3, 2);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(416, 37);
            roundedButton6.TabIndex = 188;
            roundedButton6.Text = "REPORTES";
            roundedButton6.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton6.TextColor = Color.Black;
            roundedButton6.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.FromArgb(192, 202, 212);
            panel2.Controls.Add(dtgReportes);
            panel2.Location = new Point(36, 724);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(736, 387);
            panel2.TabIndex = 216;
            // 
            // dtgReportes
            // 
            dtgReportes.AllowUserToAddRows = false;
            dtgReportes.AllowUserToDeleteRows = false;
            dtgReportes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgReportes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dtgReportes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dtgReportes.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgReportes.BorderStyle = BorderStyle.None;
            dtgReportes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgReportes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgReportes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgReportes.ColumnHeadersHeight = 40;
            dtgReportes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgReportes.EnableHeadersVisualStyles = false;
            dtgReportes.GridColor = Color.LightGray;
            dtgReportes.Location = new Point(8, 8);
            dtgReportes.Margin = new Padding(3, 2, 3, 2);
            dtgReportes.Name = "dtgReportes";
            dtgReportes.ReadOnly = true;
            dtgReportes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgReportes.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dtgReportes.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dtgReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgReportes.Size = new Size(721, 371);
            dtgReportes.TabIndex = 7;
            // 
            // FrmReportesMarcasPatentes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(848, 591);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmReportesMarcasPatentes";
            Text = "FrmReportesMarcasPatentes";
            Load += FrmReportesMarcasPatentes_Load;
            Click += FrmReportesMarcasPatentes_Click;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgReportes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Clases.RoundedButton roundedButton6;
        private CheckBox checkBoxVencimiento;
        private CheckBox checkBoxReigstro;
        private CheckBox checkBoxSolicitud;
        private CheckBox checkBoxClase;
        private CheckBox checkBoxTitular;
        private CheckBox checkBoxRegistro;
        private CheckBox checkBoxTomo;
        private CheckBox checkBoxFolio;
        private CheckBox checkBoxPais;
        private CheckBox checkBoxNombre;
        private CheckBox checkBoxEstado;
        private Clases.RoundedButton btnConsultar;
        private Clases.RoundedButton btnCancelar;
        private Panel panel2;
        private DataGridView dtgReportes;
        private ComboBox comboBoxEstado;
        private ComboBox comboBoxPais;
        private DateTimePicker dtpSolicitudFinal;
        private DateTimePicker dtpVencimientoFinal;
        private DateTimePicker dtpFechaRegistroFinal;
        private DateTimePicker dtpVencimientoInicial;
        private DateTimePicker dtpFRegistroInicial;
        private DateTimePicker dtpSolicitudInicial;
        private TextBox txtFolio;
        private TextBox txtNombre;
        private TextBox txtRegistro;
        private TextBox txtTomo;
        private TextBox txtClase;
        private Clases.RoundedButton roundedButton4;
        private RichTextBox richTextBoxTitular;
        private RichTextBox richTextBoxAgente;
        private Clases.RoundedButton roundedButton1;
        private CheckBox checkBoxAgente;
        private Clases.RoundedButton roundedButton2;
        private Clases.RoundedButton roundedButton3;
        private Panel panel1;
        private ComboBox comboBoxObjeto;
        private RichTextBox richTextBoxCliente;
        private Clases.RoundedButton roundedButton5;
        private CheckBox checkBoxCliente;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel5;
        private Panel panel6;
    }
}