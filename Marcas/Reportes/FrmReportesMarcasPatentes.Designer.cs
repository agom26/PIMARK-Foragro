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
            richTextBoxCliente = new RichTextBox();
            roundedButton5 = new Clases.RoundedButton();
            checkBoxCliente = new CheckBox();
            comboBoxObjeto = new ComboBox();
            panel1 = new Panel();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton2 = new Clases.RoundedButton();
            roundedButton3 = new Clases.RoundedButton();
            richTextBoxAgente = new RichTextBox();
            comboBoxEstado = new ComboBox();
            btnConsultar = new Clases.RoundedButton();
            label29 = new Label();
            btnCancelar = new Clases.RoundedButton();
            checkBoxEstado = new CheckBox();
            roundedButton1 = new Clases.RoundedButton();
            checkBoxAgente = new CheckBox();
            checkBoxNombre = new CheckBox();
            richTextBoxTitular = new RichTextBox();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton4 = new Clases.RoundedButton();
            checkBoxRegistro = new CheckBox();
            checkBoxTitular = new CheckBox();
            checkBoxPais = new CheckBox();
            txtClase = new TextBox();
            roundedButton6 = new Clases.RoundedButton();
            checkBoxClase = new CheckBox();
            txtRegistro = new TextBox();
            checkBoxSolicitud = new CheckBox();
            checkBoxFolio = new CheckBox();
            checkBoxReigstro = new CheckBox();
            checkBoxVencimiento = new CheckBox();
            dtpSolicitudFinal = new DateTimePicker();
            checkBoxTomo = new CheckBox();
            dtpSolicitudInicial = new DateTimePicker();
            panel2 = new Panel();
            dtgReportes = new DataGridView();
            dtpVencimientoFinal = new DateTimePicker();
            comboBoxPais = new ComboBox();
            dtpFRegistroInicial = new DateTimePicker();
            txtNombre = new TextBox();
            dtpFechaRegistroFinal = new DateTimePicker();
            txtFolio = new TextBox();
            dtpVencimientoInicial = new DateTimePicker();
            txtTomo = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
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
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(969, 900);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.AutoScroll = true;
            tabPage1.Controls.Add(richTextBoxCliente);
            tabPage1.Controls.Add(roundedButton5);
            tabPage1.Controls.Add(checkBoxCliente);
            tabPage1.Controls.Add(comboBoxObjeto);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(richTextBoxAgente);
            tabPage1.Controls.Add(comboBoxEstado);
            tabPage1.Controls.Add(btnConsultar);
            tabPage1.Controls.Add(label29);
            tabPage1.Controls.Add(btnCancelar);
            tabPage1.Controls.Add(checkBoxEstado);
            tabPage1.Controls.Add(roundedButton1);
            tabPage1.Controls.Add(checkBoxAgente);
            tabPage1.Controls.Add(checkBoxNombre);
            tabPage1.Controls.Add(richTextBoxTitular);
            tabPage1.Controls.Add(iconPictureBox4);
            tabPage1.Controls.Add(roundedButton4);
            tabPage1.Controls.Add(checkBoxRegistro);
            tabPage1.Controls.Add(checkBoxTitular);
            tabPage1.Controls.Add(checkBoxPais);
            tabPage1.Controls.Add(txtClase);
            tabPage1.Controls.Add(roundedButton6);
            tabPage1.Controls.Add(checkBoxClase);
            tabPage1.Controls.Add(txtRegistro);
            tabPage1.Controls.Add(checkBoxSolicitud);
            tabPage1.Controls.Add(checkBoxFolio);
            tabPage1.Controls.Add(checkBoxReigstro);
            tabPage1.Controls.Add(checkBoxVencimiento);
            tabPage1.Controls.Add(dtpSolicitudFinal);
            tabPage1.Controls.Add(checkBoxTomo);
            tabPage1.Controls.Add(dtpSolicitudInicial);
            tabPage1.Controls.Add(panel2);
            tabPage1.Controls.Add(dtpVencimientoFinal);
            tabPage1.Controls.Add(comboBoxPais);
            tabPage1.Controls.Add(dtpFRegistroInicial);
            tabPage1.Controls.Add(txtNombre);
            tabPage1.Controls.Add(dtpFechaRegistroFinal);
            tabPage1.Controls.Add(txtFolio);
            tabPage1.Controls.Add(dtpVencimientoInicial);
            tabPage1.Controls.Add(txtTomo);
            tabPage1.Location = new Point(4, 30);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(961, 866);
            tabPage1.TabIndex = 0;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // richTextBoxCliente
            // 
            richTextBoxCliente.Anchor = AnchorStyles.Top;
            richTextBoxCliente.Location = new Point(287, 829);
            richTextBoxCliente.Name = "richTextBoxCliente";
            richTextBoxCliente.ReadOnly = true;
            richTextBoxCliente.Size = new Size(342, 46);
            richTextBoxCliente.TabIndex = 240;
            richTextBoxCliente.Text = "";
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.Top;
            roundedButton5.BackColor = Color.LightSteelBlue;
            roundedButton5.BackgroundColor = Color.LightSteelBlue;
            roundedButton5.BorderColor = Color.LightSteelBlue;
            roundedButton5.BorderRadius = 30;
            roundedButton5.BorderSize = 0;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.Font = new Font("Century Gothic", 10F);
            roundedButton5.ForeColor = Color.Black;
            roundedButton5.Location = new Point(297, 783);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(194, 35);
            roundedButton5.TabIndex = 239;
            roundedButton5.Text = "+ CLIENTE";
            roundedButton5.TextColor = Color.Black;
            roundedButton5.UseVisualStyleBackColor = false;
            roundedButton5.Click += roundedButton5_Click;
            // 
            // checkBoxCliente
            // 
            checkBoxCliente.Anchor = AnchorStyles.Top;
            checkBoxCliente.AutoSize = true;
            checkBoxCliente.Location = new Point(178, 794);
            checkBoxCliente.Name = "checkBoxCliente";
            checkBoxCliente.Size = new Size(91, 25);
            checkBoxCliente.TabIndex = 238;
            checkBoxCliente.Text = "Cliente";
            checkBoxCliente.UseVisualStyleBackColor = true;
            // 
            // comboBoxObjeto
            // 
            comboBoxObjeto.Anchor = AnchorStyles.Top;
            comboBoxObjeto.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxObjeto.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxObjeto.FlatStyle = FlatStyle.Flat;
            comboBoxObjeto.Font = new Font("Century Gothic", 9F);
            comboBoxObjeto.FormattingEnabled = true;
            comboBoxObjeto.Items.AddRange(new object[] { "Marcas nacionales", "Marcas internacionales", "Marcas nacionales e internacionales", "Patentes" });
            comboBoxObjeto.Location = new Point(287, 71);
            comboBoxObjeto.Name = "comboBoxObjeto";
            comboBoxObjeto.Size = new Size(342, 28);
            comboBoxObjeto.TabIndex = 237;
            comboBoxObjeto.SelectedIndexChanged += comboBoxObjeto_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(iconPictureBox1);
            panel1.Controls.Add(iconPictureBox2);
            panel1.Controls.Add(roundedButton2);
            panel1.Controls.Add(roundedButton3);
            panel1.Location = new Point(8, 1485);
            panel1.Name = "panel1";
            panel1.Size = new Size(842, 96);
            panel1.TabIndex = 236;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.Anchor = AnchorStyles.Top;
            iconPictureBox1.BackColor = Color.FromArgb(229, 115, 115);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            iconPictureBox1.IconColor = Color.White;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 40;
            iconPictureBox1.Location = new Point(382, 23);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(40, 40);
            iconPictureBox1.TabIndex = 217;
            iconPictureBox1.TabStop = false;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.Anchor = AnchorStyles.Top;
            iconPictureBox2.BackColor = Color.FromArgb(0, 137, 123);
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            iconPictureBox2.IconColor = Color.White;
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 40;
            iconPictureBox2.Location = new Point(598, 23);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(40, 40);
            iconPictureBox2.TabIndex = 218;
            iconPictureBox2.TabStop = false;
            // 
            // roundedButton2
            // 
            roundedButton2.Anchor = AnchorStyles.Top;
            roundedButton2.BackColor = Color.FromArgb(0, 137, 123);
            roundedButton2.BackgroundColor = Color.FromArgb(0, 137, 123);
            roundedButton2.BorderColor = Color.FromArgb(0, 137, 123);
            roundedButton2.BorderRadius = 50;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(451, 17);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(203, 49);
            roundedButton2.TabIndex = 214;
            roundedButton2.Text = "EXPORTAR A EXCEL";
            roundedButton2.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.Top;
            roundedButton3.BackColor = Color.FromArgb(229, 115, 115);
            roundedButton3.BackgroundColor = Color.FromArgb(229, 115, 115);
            roundedButton3.BorderColor = Color.FromArgb(229, 115, 115);
            roundedButton3.BorderRadius = 50;
            roundedButton3.BorderSize = 0;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            roundedButton3.ForeColor = Color.White;
            roundedButton3.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton3.Location = new Point(242, 17);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(203, 49);
            roundedButton3.TabIndex = 215;
            roundedButton3.Text = "EXPORTAR A PDF";
            roundedButton3.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton3.TextColor = Color.White;
            roundedButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // richTextBoxAgente
            // 
            richTextBoxAgente.Anchor = AnchorStyles.Top;
            richTextBoxAgente.Location = new Point(287, 723);
            richTextBoxAgente.Name = "richTextBoxAgente";
            richTextBoxAgente.ReadOnly = true;
            richTextBoxAgente.Size = new Size(342, 46);
            richTextBoxAgente.TabIndex = 235;
            richTextBoxAgente.Text = "";
            // 
            // comboBoxEstado
            // 
            comboBoxEstado.Anchor = AnchorStyles.Top;
            comboBoxEstado.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEstado.FlatStyle = FlatStyle.Flat;
            comboBoxEstado.Font = new Font("Century Gothic", 9F);
            comboBoxEstado.FormattingEnabled = true;
            comboBoxEstado.Items.AddRange(new object[] { "", "Ingresada", "Examen de forma", "Examen de fondo", "Requerimiento", "Objeción", "Edicto", "Publicación", "Oposición", "Orden de pago", "Abandono", "Registrada", "Licencia de uso", "Trámite de renovación", "Trámite de traspaso" });
            comboBoxEstado.Location = new Point(287, 116);
            comboBoxEstado.Name = "comboBoxEstado";
            comboBoxEstado.Size = new Size(342, 28);
            comboBoxEstado.TabIndex = 217;
            // 
            // btnConsultar
            // 
            btnConsultar.Anchor = AnchorStyles.Top;
            btnConsultar.BackColor = Color.FromArgb(251, 140, 0);
            btnConsultar.BackgroundColor = Color.FromArgb(251, 140, 0);
            btnConsultar.BorderColor = Color.FromArgb(251, 140, 0);
            btnConsultar.BorderRadius = 50;
            btnConsultar.BorderSize = 0;
            btnConsultar.FlatAppearance.BorderSize = 0;
            btnConsultar.FlatStyle = FlatStyle.Flat;
            btnConsultar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnConsultar.ForeColor = Color.White;
            btnConsultar.Location = new Point(287, 897);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(168, 49);
            btnConsultar.TabIndex = 215;
            btnConsultar.Text = "CONSULTAR";
            btnConsultar.TextColor = Color.White;
            btnConsultar.UseVisualStyleBackColor = false;
            btnConsultar.Click += btnConsultar_Click;
            // 
            // label29
            // 
            label29.Anchor = AnchorStyles.Top;
            label29.AutoSize = true;
            label29.BackColor = Color.FromArgb(175, 192, 218);
            label29.Font = new Font("Century Gothic", 12F);
            label29.Location = new Point(390, 15);
            label29.Name = "label29";
            label29.Size = new Size(103, 23);
            label29.TabIndex = 187;
            label29.Text = "REPORTES";
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top;
            btnCancelar.BackColor = Color.Gainsboro;
            btnCancelar.BackgroundColor = Color.Gainsboro;
            btnCancelar.BorderColor = Color.Gainsboro;
            btnCancelar.BorderRadius = 50;
            btnCancelar.BorderSize = 0;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(461, 897);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(168, 49);
            btnCancelar.TabIndex = 214;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // checkBoxEstado
            // 
            checkBoxEstado.Anchor = AnchorStyles.Top;
            checkBoxEstado.AutoSize = true;
            checkBoxEstado.Location = new Point(176, 117);
            checkBoxEstado.Name = "checkBoxEstado";
            checkBoxEstado.Size = new Size(90, 25);
            checkBoxEstado.TabIndex = 196;
            checkBoxEstado.Text = "Estado";
            checkBoxEstado.UseVisualStyleBackColor = true;
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top;
            roundedButton1.BackColor = Color.LightSteelBlue;
            roundedButton1.BackgroundColor = Color.LightSteelBlue;
            roundedButton1.BorderColor = Color.LightSteelBlue;
            roundedButton1.BorderRadius = 30;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 10F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(297, 677);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(194, 35);
            roundedButton1.TabIndex = 234;
            roundedButton1.Text = "+ AGENTE";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // checkBoxAgente
            // 
            checkBoxAgente.Anchor = AnchorStyles.Top;
            checkBoxAgente.AutoSize = true;
            checkBoxAgente.Location = new Point(178, 688);
            checkBoxAgente.Name = "checkBoxAgente";
            checkBoxAgente.Size = new Size(95, 25);
            checkBoxAgente.TabIndex = 233;
            checkBoxAgente.Text = "Agente";
            checkBoxAgente.UseVisualStyleBackColor = true;
            // 
            // checkBoxNombre
            // 
            checkBoxNombre.Anchor = AnchorStyles.Top;
            checkBoxNombre.AutoSize = true;
            checkBoxNombre.Location = new Point(176, 161);
            checkBoxNombre.Name = "checkBoxNombre";
            checkBoxNombre.Size = new Size(99, 25);
            checkBoxNombre.TabIndex = 197;
            checkBoxNombre.Text = "Nombre";
            checkBoxNombre.UseVisualStyleBackColor = true;
            // 
            // richTextBoxTitular
            // 
            richTextBoxTitular.Anchor = AnchorStyles.Top;
            richTextBoxTitular.Location = new Point(287, 618);
            richTextBoxTitular.Name = "richTextBoxTitular";
            richTextBoxTitular.ReadOnly = true;
            richTextBoxTitular.Size = new Size(342, 46);
            richTextBoxTitular.TabIndex = 232;
            richTextBoxTitular.Text = "";
            // 
            // iconPictureBox4
            // 
            iconPictureBox4.Anchor = AnchorStyles.Top;
            iconPictureBox4.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox4.ForeColor = SystemColors.ControlText;
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.ChartSimple;
            iconPictureBox4.IconColor = SystemColors.ControlText;
            iconPictureBox4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox4.IconSize = 28;
            iconPictureBox4.Location = new Point(344, 15);
            iconPictureBox4.Name = "iconPictureBox4";
            iconPictureBox4.Size = new Size(40, 28);
            iconPictureBox4.TabIndex = 189;
            iconPictureBox4.TabStop = false;
            // 
            // roundedButton4
            // 
            roundedButton4.Anchor = AnchorStyles.Top;
            roundedButton4.BackColor = Color.LightSteelBlue;
            roundedButton4.BackgroundColor = Color.LightSteelBlue;
            roundedButton4.BorderColor = Color.LightSteelBlue;
            roundedButton4.BorderRadius = 30;
            roundedButton4.BorderSize = 0;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.Font = new Font("Century Gothic", 10F);
            roundedButton4.ForeColor = Color.Black;
            roundedButton4.Location = new Point(297, 572);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(194, 35);
            roundedButton4.TabIndex = 230;
            roundedButton4.Text = "+ TITULAR";
            roundedButton4.TextColor = Color.Black;
            roundedButton4.UseVisualStyleBackColor = false;
            roundedButton4.Click += roundedButton4_Click;
            // 
            // checkBoxRegistro
            // 
            checkBoxRegistro.Anchor = AnchorStyles.Top;
            checkBoxRegistro.AutoSize = true;
            checkBoxRegistro.Location = new Point(180, 338);
            checkBoxRegistro.Name = "checkBoxRegistro";
            checkBoxRegistro.Size = new Size(97, 25);
            checkBoxRegistro.TabIndex = 201;
            checkBoxRegistro.Text = "Registro";
            checkBoxRegistro.UseVisualStyleBackColor = true;
            // 
            // checkBoxTitular
            // 
            checkBoxTitular.Anchor = AnchorStyles.Top;
            checkBoxTitular.AutoSize = true;
            checkBoxTitular.Location = new Point(178, 583);
            checkBoxTitular.Name = "checkBoxTitular";
            checkBoxTitular.Size = new Size(80, 25);
            checkBoxTitular.TabIndex = 202;
            checkBoxTitular.Text = "Titular";
            checkBoxTitular.UseVisualStyleBackColor = true;
            // 
            // checkBoxPais
            // 
            checkBoxPais.Anchor = AnchorStyles.Top;
            checkBoxPais.AutoSize = true;
            checkBoxPais.Location = new Point(176, 201);
            checkBoxPais.Name = "checkBoxPais";
            checkBoxPais.Size = new Size(64, 25);
            checkBoxPais.TabIndex = 198;
            checkBoxPais.Text = "Pais";
            checkBoxPais.UseVisualStyleBackColor = true;
            // 
            // txtClase
            // 
            txtClase.Anchor = AnchorStyles.Top;
            txtClase.Location = new Point(287, 383);
            txtClase.Name = "txtClase";
            txtClase.Size = new Size(342, 28);
            txtClase.TabIndex = 229;
            // 
            // roundedButton6
            // 
            roundedButton6.Anchor = AnchorStyles.Top;
            roundedButton6.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton6.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton6.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton6.BorderRadius = 50;
            roundedButton6.BorderSize = 0;
            roundedButton6.Enabled = false;
            roundedButton6.FlatAppearance.BorderSize = 0;
            roundedButton6.FlatStyle = FlatStyle.Flat;
            roundedButton6.ForeColor = Color.White;
            roundedButton6.Location = new Point(176, 3);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(475, 49);
            roundedButton6.TabIndex = 188;
            roundedButton6.TextColor = Color.White;
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // checkBoxClase
            // 
            checkBoxClase.Anchor = AnchorStyles.Top;
            checkBoxClase.AutoSize = true;
            checkBoxClase.Location = new Point(178, 385);
            checkBoxClase.Name = "checkBoxClase";
            checkBoxClase.Size = new Size(79, 25);
            checkBoxClase.TabIndex = 204;
            checkBoxClase.Text = "Clase";
            checkBoxClase.UseVisualStyleBackColor = true;
            // 
            // txtRegistro
            // 
            txtRegistro.Anchor = AnchorStyles.Top;
            txtRegistro.Location = new Point(287, 336);
            txtRegistro.Name = "txtRegistro";
            txtRegistro.Size = new Size(342, 28);
            txtRegistro.TabIndex = 228;
            txtRegistro.TextChanged += txtRegistro_TextChanged;
            // 
            // checkBoxSolicitud
            // 
            checkBoxSolicitud.Anchor = AnchorStyles.Top;
            checkBoxSolicitud.AutoSize = true;
            checkBoxSolicitud.Location = new Point(178, 435);
            checkBoxSolicitud.Name = "checkBoxSolicitud";
            checkBoxSolicitud.Size = new Size(113, 25);
            checkBoxSolicitud.TabIndex = 205;
            checkBoxSolicitud.Text = "F.Solicitud";
            checkBoxSolicitud.UseVisualStyleBackColor = true;
            // 
            // checkBoxFolio
            // 
            checkBoxFolio.Anchor = AnchorStyles.Top;
            checkBoxFolio.AutoSize = true;
            checkBoxFolio.Location = new Point(180, 245);
            checkBoxFolio.Name = "checkBoxFolio";
            checkBoxFolio.Size = new Size(68, 25);
            checkBoxFolio.TabIndex = 199;
            checkBoxFolio.Text = "Folio";
            checkBoxFolio.UseVisualStyleBackColor = true;
            // 
            // checkBoxReigstro
            // 
            checkBoxReigstro.Anchor = AnchorStyles.Top;
            checkBoxReigstro.AutoSize = true;
            checkBoxReigstro.Location = new Point(178, 482);
            checkBoxReigstro.Name = "checkBoxReigstro";
            checkBoxReigstro.Size = new Size(110, 25);
            checkBoxReigstro.TabIndex = 206;
            checkBoxReigstro.Text = "F.Registro";
            checkBoxReigstro.UseVisualStyleBackColor = true;
            // 
            // checkBoxVencimiento
            // 
            checkBoxVencimiento.Anchor = AnchorStyles.Top;
            checkBoxVencimiento.AutoSize = true;
            checkBoxVencimiento.Location = new Point(178, 535);
            checkBoxVencimiento.Name = "checkBoxVencimiento";
            checkBoxVencimiento.Size = new Size(90, 25);
            checkBoxVencimiento.TabIndex = 207;
            checkBoxVencimiento.Text = "F.Venc";
            checkBoxVencimiento.UseVisualStyleBackColor = true;
            // 
            // dtpSolicitudFinal
            // 
            dtpSolicitudFinal.Anchor = AnchorStyles.Top;
            dtpSolicitudFinal.Format = DateTimePickerFormat.Short;
            dtpSolicitudFinal.Location = new Point(445, 430);
            dtpSolicitudFinal.Name = "dtpSolicitudFinal";
            dtpSolicitudFinal.Size = new Size(132, 28);
            dtpSolicitudFinal.TabIndex = 224;
            // 
            // checkBoxTomo
            // 
            checkBoxTomo.Anchor = AnchorStyles.Top;
            checkBoxTomo.AutoSize = true;
            checkBoxTomo.Location = new Point(180, 288);
            checkBoxTomo.Name = "checkBoxTomo";
            checkBoxTomo.Size = new Size(77, 25);
            checkBoxTomo.TabIndex = 200;
            checkBoxTomo.Text = "Tomo";
            checkBoxTomo.UseVisualStyleBackColor = true;
            // 
            // dtpSolicitudInicial
            // 
            dtpSolicitudInicial.Anchor = AnchorStyles.Top;
            dtpSolicitudInicial.Format = DateTimePickerFormat.Short;
            dtpSolicitudInicial.Location = new Point(297, 430);
            dtpSolicitudInicial.Name = "dtpSolicitudInicial";
            dtpSolicitudInicial.Size = new Size(132, 28);
            dtpSolicitudInicial.TabIndex = 219;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.FromArgb(192, 202, 212);
            panel2.Controls.Add(dtgReportes);
            panel2.Location = new Point(8, 965);
            panel2.Name = "panel2";
            panel2.Size = new Size(842, 516);
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
            dtgReportes.Location = new Point(9, 10);
            dtgReportes.Name = "dtgReportes";
            dtgReportes.ReadOnly = true;
            dtgReportes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgReportes.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dtgReportes.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dtgReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgReportes.Size = new Size(825, 495);
            dtgReportes.TabIndex = 7;
            // 
            // dtpVencimientoFinal
            // 
            dtpVencimientoFinal.Anchor = AnchorStyles.Top;
            dtpVencimientoFinal.Format = DateTimePickerFormat.Short;
            dtpVencimientoFinal.Location = new Point(445, 530);
            dtpVencimientoFinal.Name = "dtpVencimientoFinal";
            dtpVencimientoFinal.Size = new Size(132, 28);
            dtpVencimientoFinal.TabIndex = 223;
            // 
            // comboBoxPais
            // 
            comboBoxPais.Anchor = AnchorStyles.Top;
            comboBoxPais.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxPais.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPais.FlatStyle = FlatStyle.Flat;
            comboBoxPais.Font = new Font("Century Gothic", 9F);
            comboBoxPais.FormattingEnabled = true;
            comboBoxPais.Items.AddRange(new object[] { "", "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita", "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Baréin", "Bangladés", "Barbados", "Bielorrusia", "Birmania (Myanmar)", "Burundi", "Bután", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo (Congo-Brazzaville)", "Corea del Norte", "Corea del Sur", "Costa Rica", "Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Eswatini", "Etiopía", "Fiyi", "Filipinas", "Finlandia", "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Grecia", "Granada", "Guatemala", "Guinea", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Hungría", "Islandia", "India", "Indonesia", "Irán", "Irak", "Irlanda", "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kosovo", "Kuwait", "Laos", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein", "Lituania", "Luxemburgo", "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Mauricio", "Mauritania", "México", "Micronesia", "Moldavia", "Mónaco", "Mongolia", "Mozambique", "Namibia", "Nauru", "Nepal", "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Pakistán", "Palaos", "Palestina", "Panamá", "Paraguay", "Perú", "Polonia", "Portugal", "Qatar", "República Centroafricana", "República Checa", "República del Congo (Congo-Kinshasa)", "República Dominicana", "Rumania", "Rusia", "Ruanda", "San Cristóbal y Nieves", "San Marino", "Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Tailandia", "Taiwán", "Tanzania", "Tayikistán", "Timor Oriental", "Togo", "Tonga", "Trinidad y Tobago", "Túnez", "Turquía", "Turkmenistán", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabue" });
            comboBoxPais.Location = new Point(287, 200);
            comboBoxPais.Name = "comboBoxPais";
            comboBoxPais.Size = new Size(342, 28);
            comboBoxPais.TabIndex = 218;
            // 
            // dtpFRegistroInicial
            // 
            dtpFRegistroInicial.Anchor = AnchorStyles.Top;
            dtpFRegistroInicial.Format = DateTimePickerFormat.Short;
            dtpFRegistroInicial.Location = new Point(297, 482);
            dtpFRegistroInicial.Name = "dtpFRegistroInicial";
            dtpFRegistroInicial.Size = new Size(132, 28);
            dtpFRegistroInicial.TabIndex = 220;
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.Top;
            txtNombre.Location = new Point(287, 160);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(342, 28);
            txtNombre.TabIndex = 225;
            // 
            // dtpFechaRegistroFinal
            // 
            dtpFechaRegistroFinal.Anchor = AnchorStyles.Top;
            dtpFechaRegistroFinal.Format = DateTimePickerFormat.Short;
            dtpFechaRegistroFinal.Location = new Point(445, 480);
            dtpFechaRegistroFinal.Name = "dtpFechaRegistroFinal";
            dtpFechaRegistroFinal.Size = new Size(132, 28);
            dtpFechaRegistroFinal.TabIndex = 222;
            // 
            // txtFolio
            // 
            txtFolio.Anchor = AnchorStyles.Top;
            txtFolio.Location = new Point(287, 243);
            txtFolio.Name = "txtFolio";
            txtFolio.Size = new Size(342, 28);
            txtFolio.TabIndex = 226;
            // 
            // dtpVencimientoInicial
            // 
            dtpVencimientoInicial.Anchor = AnchorStyles.Top;
            dtpVencimientoInicial.Format = DateTimePickerFormat.Short;
            dtpVencimientoInicial.Location = new Point(297, 530);
            dtpVencimientoInicial.Name = "dtpVencimientoInicial";
            dtpVencimientoInicial.Size = new Size(132, 28);
            dtpVencimientoInicial.TabIndex = 221;
            // 
            // txtTomo
            // 
            txtTomo.Anchor = AnchorStyles.Top;
            txtTomo.Location = new Point(287, 284);
            txtTomo.Name = "txtTomo";
            txtTomo.Size = new Size(342, 28);
            txtTomo.TabIndex = 227;
            // 
            // FrmReportesMarcasPatentes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(969, 900);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmReportesMarcasPatentes";
            Text = "FrmReportesMarcasPatentes";
            Load += FrmReportesMarcasPatentes_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgReportes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private Label label29;
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
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Clases.RoundedButton roundedButton2;
        private Clases.RoundedButton roundedButton3;
        private Panel panel1;
        private ComboBox comboBoxObjeto;
        private RichTextBox richTextBoxCliente;
        private Clases.RoundedButton roundedButton5;
        private CheckBox checkBoxCliente;
    }
}