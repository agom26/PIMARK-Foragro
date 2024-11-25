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
            comboBox1 = new ComboBox();
            panel1 = new Panel();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton2 = new Clases.RoundedButton();
            roundedButton3 = new Clases.RoundedButton();
            richTextBox2 = new RichTextBox();
            comboBoxEstado = new ComboBox();
            btnConsultar = new Clases.RoundedButton();
            label29 = new Label();
            btnCancelar = new Clases.RoundedButton();
            checkBox1 = new CheckBox();
            roundedButton1 = new Clases.RoundedButton();
            checkBox11 = new CheckBox();
            checkBox2 = new CheckBox();
            richTextBox1 = new RichTextBox();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton4 = new Clases.RoundedButton();
            checkBox6 = new CheckBox();
            checkBox12 = new CheckBox();
            checkBox3 = new CheckBox();
            txtClase = new TextBox();
            roundedButton6 = new Clases.RoundedButton();
            checkBox10 = new CheckBox();
            txtRegistro = new TextBox();
            checkBox9 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox8 = new CheckBox();
            checkBox7 = new CheckBox();
            dtpSolicitudFinal = new DateTimePicker();
            checkBox5 = new CheckBox();
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
            tabControl1.Size = new Size(969, 487);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.AutoScroll = true;
            tabPage1.Controls.Add(comboBox1);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(richTextBox2);
            tabPage1.Controls.Add(comboBoxEstado);
            tabPage1.Controls.Add(btnConsultar);
            tabPage1.Controls.Add(label29);
            tabPage1.Controls.Add(btnCancelar);
            tabPage1.Controls.Add(checkBox1);
            tabPage1.Controls.Add(roundedButton1);
            tabPage1.Controls.Add(checkBox11);
            tabPage1.Controls.Add(checkBox2);
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Controls.Add(iconPictureBox4);
            tabPage1.Controls.Add(roundedButton4);
            tabPage1.Controls.Add(checkBox6);
            tabPage1.Controls.Add(checkBox12);
            tabPage1.Controls.Add(checkBox3);
            tabPage1.Controls.Add(txtClase);
            tabPage1.Controls.Add(roundedButton6);
            tabPage1.Controls.Add(checkBox10);
            tabPage1.Controls.Add(txtRegistro);
            tabPage1.Controls.Add(checkBox9);
            tabPage1.Controls.Add(checkBox4);
            tabPage1.Controls.Add(checkBox8);
            tabPage1.Controls.Add(checkBox7);
            tabPage1.Controls.Add(dtpSolicitudFinal);
            tabPage1.Controls.Add(checkBox5);
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
            tabPage1.Size = new Size(961, 453);
            tabPage1.TabIndex = 0;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top;
            comboBox1.BackColor = Color.FromArgb(241, 240, 245);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Century Gothic", 9F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Marcas nacionales", "Marcas internacionales", "Marcas nacionales e internacionales", "Patentes" });
            comboBox1.Location = new Point(263, 74);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(342, 28);
            comboBox1.TabIndex = 237;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(iconPictureBox1);
            panel1.Controls.Add(iconPictureBox2);
            panel1.Controls.Add(roundedButton2);
            panel1.Controls.Add(roundedButton3);
            panel1.Location = new Point(6, 1360);
            panel1.Name = "panel1";
            panel1.Size = new Size(886, 96);
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
            iconPictureBox1.Location = new Point(369, 22);
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
            iconPictureBox2.Location = new Point(585, 22);
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
            roundedButton2.Location = new Point(438, 16);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(203, 49);
            roundedButton2.TabIndex = 214;
            roundedButton2.Text = "EXPORTAR A EXCEL";
            roundedButton2.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
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
            roundedButton3.Location = new Point(229, 16);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(203, 49);
            roundedButton3.TabIndex = 215;
            roundedButton3.Text = "EXPORTAR A PDF";
            roundedButton3.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton3.TextColor = Color.White;
            roundedButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // richTextBox2
            // 
            richTextBox2.Anchor = AnchorStyles.Top;
            richTextBox2.Location = new Point(263, 733);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(342, 46);
            richTextBox2.TabIndex = 235;
            richTextBox2.Text = "";
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
            comboBoxEstado.Location = new Point(263, 119);
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
            btnConsultar.Location = new Point(263, 785);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(168, 49);
            btnConsultar.TabIndex = 215;
            btnConsultar.Text = "CONSULTAR";
            btnConsultar.TextColor = Color.White;
            btnConsultar.UseVisualStyleBackColor = false;
            // 
            // label29
            // 
            label29.Anchor = AnchorStyles.Top;
            label29.AutoSize = true;
            label29.BackColor = Color.FromArgb(175, 192, 218);
            label29.Font = new Font("Century Gothic", 12F);
            label29.Location = new Point(366, 18);
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
            btnCancelar.Location = new Point(437, 785);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(168, 49);
            btnCancelar.TabIndex = 214;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Top;
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(152, 120);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(90, 25);
            checkBox1.TabIndex = 196;
            checkBox1.Text = "Estado";
            checkBox1.UseVisualStyleBackColor = true;
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
            roundedButton1.Location = new Point(273, 687);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(194, 35);
            roundedButton1.TabIndex = 234;
            roundedButton1.Text = "+ AGENTE";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // checkBox11
            // 
            checkBox11.Anchor = AnchorStyles.Top;
            checkBox11.AutoSize = true;
            checkBox11.Location = new Point(154, 698);
            checkBox11.Name = "checkBox11";
            checkBox11.Size = new Size(95, 25);
            checkBox11.TabIndex = 233;
            checkBox11.Text = "Agente";
            checkBox11.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.Anchor = AnchorStyles.Top;
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(152, 164);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(99, 25);
            checkBox2.TabIndex = 197;
            checkBox2.Text = "Nombre";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top;
            richTextBox1.Location = new Point(263, 621);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(342, 46);
            richTextBox1.TabIndex = 232;
            richTextBox1.Text = "";
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
            iconPictureBox4.Location = new Point(320, 18);
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
            roundedButton4.Location = new Point(273, 575);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(194, 35);
            roundedButton4.TabIndex = 230;
            roundedButton4.Text = "+ TITULAR";
            roundedButton4.TextColor = Color.Black;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // checkBox6
            // 
            checkBox6.Anchor = AnchorStyles.Top;
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(156, 341);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(97, 25);
            checkBox6.TabIndex = 201;
            checkBox6.Text = "Registro";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            checkBox12.Anchor = AnchorStyles.Top;
            checkBox12.AutoSize = true;
            checkBox12.Location = new Point(154, 586);
            checkBox12.Name = "checkBox12";
            checkBox12.Size = new Size(80, 25);
            checkBox12.TabIndex = 202;
            checkBox12.Text = "Titular";
            checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.Anchor = AnchorStyles.Top;
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(152, 204);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(64, 25);
            checkBox3.TabIndex = 198;
            checkBox3.Text = "Pais";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // txtClase
            // 
            txtClase.Anchor = AnchorStyles.Top;
            txtClase.Location = new Point(263, 386);
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
            roundedButton6.Location = new Point(152, 6);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(475, 49);
            roundedButton6.TabIndex = 188;
            roundedButton6.TextColor = Color.White;
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // checkBox10
            // 
            checkBox10.Anchor = AnchorStyles.Top;
            checkBox10.AutoSize = true;
            checkBox10.Location = new Point(154, 388);
            checkBox10.Name = "checkBox10";
            checkBox10.Size = new Size(79, 25);
            checkBox10.TabIndex = 204;
            checkBox10.Text = "Clase";
            checkBox10.UseVisualStyleBackColor = true;
            // 
            // txtRegistro
            // 
            txtRegistro.Anchor = AnchorStyles.Top;
            txtRegistro.Location = new Point(263, 339);
            txtRegistro.Name = "txtRegistro";
            txtRegistro.Size = new Size(342, 28);
            txtRegistro.TabIndex = 228;
            txtRegistro.TextChanged += txtRegistro_TextChanged;
            // 
            // checkBox9
            // 
            checkBox9.Anchor = AnchorStyles.Top;
            checkBox9.AutoSize = true;
            checkBox9.Location = new Point(154, 438);
            checkBox9.Name = "checkBox9";
            checkBox9.Size = new Size(113, 25);
            checkBox9.TabIndex = 205;
            checkBox9.Text = "F.Solicitud";
            checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.Anchor = AnchorStyles.Top;
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(156, 248);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(68, 25);
            checkBox4.TabIndex = 199;
            checkBox4.Text = "Folio";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            checkBox8.Anchor = AnchorStyles.Top;
            checkBox8.AutoSize = true;
            checkBox8.Location = new Point(154, 485);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new Size(110, 25);
            checkBox8.TabIndex = 206;
            checkBox8.Text = "F.Registro";
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            checkBox7.Anchor = AnchorStyles.Top;
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(154, 538);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(90, 25);
            checkBox7.TabIndex = 207;
            checkBox7.Text = "F.Venc";
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // dtpSolicitudFinal
            // 
            dtpSolicitudFinal.Anchor = AnchorStyles.Top;
            dtpSolicitudFinal.Format = DateTimePickerFormat.Short;
            dtpSolicitudFinal.Location = new Point(421, 433);
            dtpSolicitudFinal.Name = "dtpSolicitudFinal";
            dtpSolicitudFinal.Size = new Size(132, 28);
            dtpSolicitudFinal.TabIndex = 224;
            // 
            // checkBox5
            // 
            checkBox5.Anchor = AnchorStyles.Top;
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(156, 291);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(77, 25);
            checkBox5.TabIndex = 200;
            checkBox5.Text = "Tomo";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // dtpSolicitudInicial
            // 
            dtpSolicitudInicial.Anchor = AnchorStyles.Top;
            dtpSolicitudInicial.Format = DateTimePickerFormat.Short;
            dtpSolicitudInicial.Location = new Point(273, 433);
            dtpSolicitudInicial.Name = "dtpSolicitudInicial";
            dtpSolicitudInicial.Size = new Size(132, 28);
            dtpSolicitudInicial.TabIndex = 219;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.FromArgb(192, 202, 212);
            panel2.Controls.Add(dtgReportes);
            panel2.Location = new Point(34, 840);
            panel2.Name = "panel2";
            panel2.Size = new Size(807, 516);
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
            dtgReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            dtgReportes.Size = new Size(790, 495);
            dtgReportes.TabIndex = 7;
            // 
            // dtpVencimientoFinal
            // 
            dtpVencimientoFinal.Anchor = AnchorStyles.Top;
            dtpVencimientoFinal.Format = DateTimePickerFormat.Short;
            dtpVencimientoFinal.Location = new Point(421, 533);
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
            comboBoxPais.Location = new Point(263, 203);
            comboBoxPais.Name = "comboBoxPais";
            comboBoxPais.Size = new Size(342, 28);
            comboBoxPais.TabIndex = 218;
            // 
            // dtpFRegistroInicial
            // 
            dtpFRegistroInicial.Anchor = AnchorStyles.Top;
            dtpFRegistroInicial.Format = DateTimePickerFormat.Short;
            dtpFRegistroInicial.Location = new Point(273, 485);
            dtpFRegistroInicial.Name = "dtpFRegistroInicial";
            dtpFRegistroInicial.Size = new Size(132, 28);
            dtpFRegistroInicial.TabIndex = 220;
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.Top;
            txtNombre.Location = new Point(263, 163);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(342, 28);
            txtNombre.TabIndex = 225;
            // 
            // dtpFechaRegistroFinal
            // 
            dtpFechaRegistroFinal.Anchor = AnchorStyles.Top;
            dtpFechaRegistroFinal.Format = DateTimePickerFormat.Short;
            dtpFechaRegistroFinal.Location = new Point(421, 483);
            dtpFechaRegistroFinal.Name = "dtpFechaRegistroFinal";
            dtpFechaRegistroFinal.Size = new Size(132, 28);
            dtpFechaRegistroFinal.TabIndex = 222;
            // 
            // txtFolio
            // 
            txtFolio.Anchor = AnchorStyles.Top;
            txtFolio.Location = new Point(263, 246);
            txtFolio.Name = "txtFolio";
            txtFolio.Size = new Size(342, 28);
            txtFolio.TabIndex = 226;
            // 
            // dtpVencimientoInicial
            // 
            dtpVencimientoInicial.Anchor = AnchorStyles.Top;
            dtpVencimientoInicial.Format = DateTimePickerFormat.Short;
            dtpVencimientoInicial.Location = new Point(273, 533);
            dtpVencimientoInicial.Name = "dtpVencimientoInicial";
            dtpVencimientoInicial.Size = new Size(132, 28);
            dtpVencimientoInicial.TabIndex = 221;
            // 
            // txtTomo
            // 
            txtTomo.Anchor = AnchorStyles.Top;
            txtTomo.Location = new Point(263, 287);
            txtTomo.Name = "txtTomo";
            txtTomo.Size = new Size(342, 28);
            txtTomo.TabIndex = 227;
            // 
            // FrmReportesMarcasPatentes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(969, 487);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmReportesMarcasPatentes";
            Text = "FrmReportesMarcasPatentes";
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
        private CheckBox checkBox7;
        private CheckBox checkBox8;
        private CheckBox checkBox9;
        private CheckBox checkBox10;
        private CheckBox checkBox12;
        private CheckBox checkBox6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
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
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private Clases.RoundedButton roundedButton1;
        private CheckBox checkBox11;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Clases.RoundedButton roundedButton2;
        private Clases.RoundedButton roundedButton3;
        private Panel panel1;
        private ComboBox comboBox1;
    }
}