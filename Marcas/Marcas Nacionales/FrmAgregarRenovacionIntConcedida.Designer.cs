namespace Presentacion.Marcas_Internacionales
{
    partial class FrmAgregarRenovacionIntConcedida
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            richTextBox1 = new RichTextBox();
            lblUser = new Label();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            panel2 = new Panel();
            button2 = new Button();
            button1 = new Button();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            txtEstado = new TextBox();
            dateFechVencAnt = new DateTimePicker();
            label6 = new Label();
            dateFechVencNueva = new DateTimePicker();
            label7 = new Label();
            Fechas = new GroupBox();
            txtNoExpediente = new TextBox();
            label8 = new Label();
            groupBox1 = new GroupBox();
            tabControl1 = new TabControl();
            tabPageRenovarMarca = new TabPage();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            tabPageListarLicencias = new TabPage();
            btnAgregarArchivo = new FontAwesome.Sharp.IconButton();
            iconButton18 = new FontAwesome.Sharp.IconButton();
            label18 = new Label();
            panel3 = new Panel();
            dtgLicencias = new DataGridView();
            tabPageRenovarLicencia = new TabPage();
            panel4 = new Panel();
            panel5 = new Panel();
            btnEditarH = new FontAwesome.Sharp.IconButton();
            btnCancelarH = new FontAwesome.Sharp.IconButton();
            txtTituloVerifica = new TextBox();
            label9 = new Label();
            label5 = new Label();
            dtpFechaFinNueva = new DateTimePicker();
            label24 = new Label();
            dtpFechaFinAntigua = new DateTimePicker();
            roundedButton7 = new Presentacion.Clases.RoundedButton();
            panel1 = new Panel();
            panel2.SuspendLayout();
            Fechas.SuspendLayout();
            groupBox1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageRenovarMarca.SuspendLayout();
            tabPageListarLicencias.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgLicencias).BeginInit();
            tabPageRenovarLicencia.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 70);
            label1.Name = "label1";
            label1.Size = new Size(45, 17);
            label1.TabIndex = 0;
            label1.Text = "Fecha";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(265, 70);
            label2.Name = "label2";
            label2.Size = new Size(48, 17);
            label2.TabIndex = 1;
            label2.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 149);
            label3.Name = "label3";
            label3.Size = new Size(84, 17);
            label3.TabIndex = 2;
            label3.Text = "Anotaciones";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(31, 93);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(154, 22);
            dateTimePicker1.TabIndex = 1;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.Control;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(31, 172);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(431, 70);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(212, 28);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(45, 17);
            lblUser.TabIndex = 6;
            lblUser.Text = "Fecha";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 13F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(346, 19);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(329, 50);
            roundedButton1.TabIndex = 7;
            roundedButton1.Text = "RENOVACIÓN CONCEDIDA";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(34, 77, 112);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1053, 34);
            panel2.TabIndex = 8;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 12F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(1002, 0);
            button2.Name = "button2";
            button2.Size = new Size(51, 34);
            button2.TabIndex = 5;
            button2.Text = "X";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 12F);
            button1.ForeColor = Color.White;
            button1.Location = new Point(1035, 0);
            button1.Name = "button1";
            button1.Size = new Size(51, 29);
            button1.TabIndex = 0;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            // 
            // iconButton3
            // 
            iconButton3.BackColor = Color.FromArgb(1, 87, 155);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Check;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 25;
            iconButton3.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton3.Location = new Point(215, 416);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(179, 44);
            iconButton3.TabIndex = 3;
            iconButton3.Text = "ACEPTAR";
            iconButton3.TextAlign = ContentAlignment.MiddleRight;
            iconButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton2
            // 
            iconButton2.BackColor = Color.Gainsboro;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton2.Location = new Point(418, 416);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(179, 44);
            iconButton2.TabIndex = 4;
            iconButton2.Text = "CANCELAR";
            iconButton2.TextAlign = ContentAlignment.MiddleRight;
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // txtEstado
            // 
            txtEstado.BackColor = SystemColors.Window;
            txtEstado.BorderStyle = BorderStyle.FixedSingle;
            txtEstado.Location = new Point(265, 95);
            txtEstado.Name = "txtEstado";
            txtEstado.ReadOnly = true;
            txtEstado.Size = new Size(87, 22);
            txtEstado.TabIndex = 11;
            txtEstado.Text = "Registrada";
            // 
            // dateFechVencAnt
            // 
            dateFechVencAnt.Enabled = false;
            dateFechVencAnt.Format = DateTimePickerFormat.Short;
            dateFechVencAnt.Location = new Point(24, 172);
            dateFechVencAnt.Name = "dateFechVencAnt";
            dateFechVencAnt.Size = new Size(185, 22);
            dateFechVencAnt.TabIndex = 17;
            dateFechVencAnt.ValueChanged += dateFechVencAnt_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 149);
            label6.Name = "label6";
            label6.Size = new Size(149, 17);
            label6.TabIndex = 16;
            label6.Text = "Antigua fecha de venc.";
            // 
            // dateFechVencNueva
            // 
            dateFechVencNueva.Format = DateTimePickerFormat.Short;
            dateFechVencNueva.Location = new Point(296, 172);
            dateFechVencNueva.Name = "dateFechVencNueva";
            dateFechVencNueva.Size = new Size(178, 22);
            dateFechVencNueva.TabIndex = 19;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(296, 149);
            label7.Name = "label7";
            label7.Size = new Size(142, 17);
            label7.TabIndex = 18;
            label7.Text = "Nueva fecha de venc.";
            // 
            // Fechas
            // 
            Fechas.Controls.Add(txtNoExpediente);
            Fechas.Controls.Add(label8);
            Fechas.Controls.Add(dateFechVencAnt);
            Fechas.Controls.Add(dateFechVencNueva);
            Fechas.Controls.Add(label7);
            Fechas.Controls.Add(label6);
            Fechas.Location = new Point(518, 117);
            Fechas.Name = "Fechas";
            Fechas.Size = new Size(503, 272);
            Fechas.TabIndex = 20;
            Fechas.TabStop = false;
            Fechas.Text = "Renovacion";
            Fechas.Enter += Fechas_Enter;
            // 
            // txtNoExpediente
            // 
            txtNoExpediente.BackColor = SystemColors.Control;
            txtNoExpediente.BorderStyle = BorderStyle.None;
            txtNoExpediente.Location = new Point(175, 93);
            txtNoExpediente.Name = "txtNoExpediente";
            txtNoExpediente.ReadOnly = true;
            txtNoExpediente.Size = new Size(120, 15);
            txtNoExpediente.TabIndex = 12;
            txtNoExpediente.TextAlign = HorizontalAlignment.Center;
            txtNoExpediente.TextChanged += textBox2_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(175, 70);
            label8.Name = "label8";
            label8.Size = new Size(98, 17);
            label8.TabIndex = 12;
            label8.Text = "No. Expediente";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(txtEstado);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Controls.Add(lblUser);
            groupBox1.Location = new Point(22, 117);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(472, 272);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Historial";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageRenovarMarca);
            tabControl1.Controls.Add(tabPageListarLicencias);
            tabControl1.Controls.Add(tabPageRenovarLicencia);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1053, 566);
            tabControl1.TabIndex = 22;
            // 
            // tabPageRenovarMarca
            // 
            tabPageRenovarMarca.AutoScroll = true;
            tabPageRenovarMarca.BackColor = SystemColors.Window;
            tabPageRenovarMarca.Controls.Add(iconButton1);
            tabPageRenovarMarca.Controls.Add(iconButton2);
            tabPageRenovarMarca.Controls.Add(iconButton3);
            tabPageRenovarMarca.Controls.Add(Fechas);
            tabPageRenovarMarca.Controls.Add(roundedButton1);
            tabPageRenovarMarca.Controls.Add(groupBox1);
            tabPageRenovarMarca.Location = new Point(4, 26);
            tabPageRenovarMarca.Name = "tabPageRenovarMarca";
            tabPageRenovarMarca.Padding = new Padding(3);
            tabPageRenovarMarca.Size = new Size(1045, 536);
            tabPageRenovarMarca.TabIndex = 0;
            // 
            // iconButton1
            // 
            iconButton1.BackColor = Color.Green;
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton1.ForeColor = Color.White;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.FileContract;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 25;
            iconButton1.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton1.Location = new Point(629, 416);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(179, 44);
            iconButton1.TabIndex = 22;
            iconButton1.Text = "LICENCIAS DE USO";
            iconButton1.TextAlign = ContentAlignment.MiddleRight;
            iconButton1.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // tabPageListarLicencias
            // 
            tabPageListarLicencias.AutoScroll = true;
            tabPageListarLicencias.Controls.Add(btnAgregarArchivo);
            tabPageListarLicencias.Controls.Add(iconButton18);
            tabPageListarLicencias.Controls.Add(label18);
            tabPageListarLicencias.Controls.Add(panel3);
            tabPageListarLicencias.Location = new Point(4, 24);
            tabPageListarLicencias.Name = "tabPageListarLicencias";
            tabPageListarLicencias.Padding = new Padding(3);
            tabPageListarLicencias.Size = new Size(1045, 538);
            tabPageListarLicencias.TabIndex = 1;
            tabPageListarLicencias.UseVisualStyleBackColor = true;
            // 
            // btnAgregarArchivo
            // 
            btnAgregarArchivo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAgregarArchivo.BackColor = Color.FromArgb(0, 137, 123);
            btnAgregarArchivo.FlatAppearance.BorderSize = 0;
            btnAgregarArchivo.FlatStyle = FlatStyle.Flat;
            btnAgregarArchivo.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnAgregarArchivo.ForeColor = Color.White;
            btnAgregarArchivo.IconChar = FontAwesome.Sharp.IconChar.FileCircleCheck;
            btnAgregarArchivo.IconColor = Color.White;
            btnAgregarArchivo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAgregarArchivo.IconSize = 30;
            btnAgregarArchivo.ImageAlign = ContentAlignment.MiddleRight;
            btnAgregarArchivo.Location = new Point(910, 66);
            btnAgregarArchivo.Name = "btnAgregarArchivo";
            btnAgregarArchivo.Size = new Size(166, 49);
            btnAgregarArchivo.TabIndex = 77;
            btnAgregarArchivo.Text = "RENOVAR";
            btnAgregarArchivo.TextAlign = ContentAlignment.MiddleLeft;
            btnAgregarArchivo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregarArchivo.UseVisualStyleBackColor = false;
            btnAgregarArchivo.Click += btnAgregarArchivo_Click;
            // 
            // iconButton18
            // 
            iconButton18.BackColor = Color.White;
            iconButton18.FlatAppearance.BorderSize = 0;
            iconButton18.FlatStyle = FlatStyle.Flat;
            iconButton18.IconChar = FontAwesome.Sharp.IconChar.AngleLeft;
            iconButton18.IconColor = Color.Black;
            iconButton18.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton18.Location = new Point(38, 11);
            iconButton18.Name = "iconButton18";
            iconButton18.Size = new Size(62, 49);
            iconButton18.TabIndex = 76;
            iconButton18.UseVisualStyleBackColor = false;
            iconButton18.Click += iconButton18_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Century Gothic", 19F);
            label18.Location = new Point(106, 14);
            label18.Name = "label18";
            label18.Size = new Size(245, 32);
            label18.TabIndex = 75;
            label18.Text = "LICENCIAS DE USO";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.BackColor = Color.FromArgb(192, 202, 212);
            panel3.Controls.Add(dtgLicencias);
            panel3.Location = new Point(38, 66);
            panel3.Name = "panel3";
            panel3.Size = new Size(866, 430);
            panel3.TabIndex = 0;
            // 
            // dtgLicencias
            // 
            dtgLicencias.AllowUserToAddRows = false;
            dtgLicencias.AllowUserToDeleteRows = false;
            dtgLicencias.AllowUserToResizeRows = false;
            dtgLicencias.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgLicencias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgLicencias.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgLicencias.BorderStyle = BorderStyle.None;
            dtgLicencias.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgLicencias.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgLicencias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgLicencias.ColumnHeadersHeight = 40;
            dtgLicencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgLicencias.EnableHeadersVisualStyles = false;
            dtgLicencias.GridColor = Color.LightGray;
            dtgLicencias.Location = new Point(15, 15);
            dtgLicencias.Name = "dtgLicencias";
            dtgLicencias.ReadOnly = true;
            dtgLicencias.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgLicencias.RowHeadersWidth = 51;
            dtgLicencias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgLicencias.Size = new Size(838, 403);
            dtgLicencias.TabIndex = 46;
            dtgLicencias.DataBindingComplete += dtgLicencias_DataBindingComplete;
            // 
            // tabPageRenovarLicencia
            // 
            tabPageRenovarLicencia.AutoScroll = true;
            tabPageRenovarLicencia.Controls.Add(panel4);
            tabPageRenovarLicencia.Location = new Point(4, 24);
            tabPageRenovarLicencia.Name = "tabPageRenovarLicencia";
            tabPageRenovarLicencia.Size = new Size(1045, 538);
            tabPageRenovarLicencia.TabIndex = 2;
            tabPageRenovarLicencia.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel4.BackColor = Color.FromArgb(192, 202, 212);
            panel4.Controls.Add(panel5);
            panel4.Location = new Point(77, 63);
            panel4.Name = "panel4";
            panel4.Size = new Size(608, 409);
            panel4.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.Window;
            panel5.Controls.Add(btnEditarH);
            panel5.Controls.Add(btnCancelarH);
            panel5.Controls.Add(txtTituloVerifica);
            panel5.Controls.Add(label9);
            panel5.Controls.Add(label5);
            panel5.Controls.Add(dtpFechaFinNueva);
            panel5.Controls.Add(label24);
            panel5.Controls.Add(dtpFechaFinAntigua);
            panel5.Controls.Add(roundedButton7);
            panel5.Location = new Point(18, 17);
            panel5.Name = "panel5";
            panel5.Size = new Size(876, 376);
            panel5.TabIndex = 0;
            // 
            // btnEditarH
            // 
            btnEditarH.Anchor = AnchorStyles.Top;
            btnEditarH.BackColor = Color.FromArgb(0, 137, 123);
            btnEditarH.FlatAppearance.BorderSize = 0;
            btnEditarH.FlatStyle = FlatStyle.Flat;
            btnEditarH.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnEditarH.ForeColor = Color.White;
            btnEditarH.IconChar = FontAwesome.Sharp.IconChar.FileCircleCheck;
            btnEditarH.IconColor = Color.White;
            btnEditarH.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditarH.IconSize = 30;
            btnEditarH.ImageAlign = ContentAlignment.MiddleLeft;
            btnEditarH.Location = new Point(288, 275);
            btnEditarH.Name = "btnEditarH";
            btnEditarH.Size = new Size(160, 49);
            btnEditarH.TabIndex = 35;
            btnEditarH.Text = "RENOVAR";
            btnEditarH.TextAlign = ContentAlignment.MiddleRight;
            btnEditarH.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnEditarH.UseVisualStyleBackColor = false;
            btnEditarH.Click += btnEditarH_Click;
            // 
            // btnCancelarH
            // 
            btnCancelarH.Anchor = AnchorStyles.Top;
            btnCancelarH.BackColor = Color.Gainsboro;
            btnCancelarH.FlatAppearance.BorderSize = 0;
            btnCancelarH.FlatStyle = FlatStyle.Flat;
            btnCancelarH.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnCancelarH.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarH.IconColor = Color.Black;
            btnCancelarH.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarH.IconSize = 30;
            btnCancelarH.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarH.Location = new Point(475, 275);
            btnCancelarH.Name = "btnCancelarH";
            btnCancelarH.Size = new Size(160, 49);
            btnCancelarH.TabIndex = 34;
            btnCancelarH.Text = "CANCELAR";
            btnCancelarH.TextAlign = ContentAlignment.MiddleRight;
            btnCancelarH.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelarH.UseVisualStyleBackColor = false;
            btnCancelarH.Click += btnCancelarH_Click;
            // 
            // txtTituloVerifica
            // 
            txtTituloVerifica.Location = new Point(294, 147);
            txtTituloVerifica.Name = "txtTituloVerifica";
            txtTituloVerifica.ReadOnly = true;
            txtTituloVerifica.Size = new Size(335, 22);
            txtTituloVerifica.TabIndex = 33;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.BackColor = Color.White;
            label9.Location = new Point(294, 127);
            label9.Name = "label9";
            label9.Size = new Size(170, 17);
            label9.TabIndex = 32;
            label9.Text = "Título por el cual se verifica";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Location = new Point(475, 199);
            label5.Name = "label5";
            label5.Size = new Size(126, 17);
            label5.TabIndex = 30;
            label5.Text = "Nueva Fecha Venc.";
            // 
            // dtpFechaFinNueva
            // 
            dtpFechaFinNueva.Anchor = AnchorStyles.Top;
            dtpFechaFinNueva.Format = DateTimePickerFormat.Short;
            dtpFechaFinNueva.Location = new Point(475, 222);
            dtpFechaFinNueva.Name = "dtpFechaFinNueva";
            dtpFechaFinNueva.Size = new Size(154, 22);
            dtpFechaFinNueva.TabIndex = 31;
            // 
            // label24
            // 
            label24.Anchor = AnchorStyles.Top;
            label24.AutoSize = true;
            label24.BackColor = Color.White;
            label24.Location = new Point(294, 199);
            label24.Name = "label24";
            label24.Size = new Size(133, 17);
            label24.TabIndex = 24;
            label24.Text = "Antigua Fecha Venc.";
            // 
            // dtpFechaFinAntigua
            // 
            dtpFechaFinAntigua.Anchor = AnchorStyles.Top;
            dtpFechaFinAntigua.Format = DateTimePickerFormat.Short;
            dtpFechaFinAntigua.Location = new Point(294, 222);
            dtpFechaFinAntigua.Name = "dtpFechaFinAntigua";
            dtpFechaFinAntigua.Size = new Size(154, 22);
            dtpFechaFinAntigua.TabIndex = 26;
            // 
            // roundedButton7
            // 
            roundedButton7.Anchor = AnchorStyles.Top;
            roundedButton7.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderRadius = 40;
            roundedButton7.BorderSize = 0;
            roundedButton7.Enabled = false;
            roundedButton7.FlatAppearance.BorderSize = 0;
            roundedButton7.FlatStyle = FlatStyle.Flat;
            roundedButton7.Font = new Font("Century Gothic", 13F);
            roundedButton7.ForeColor = Color.Black;
            roundedButton7.Location = new Point(303, 19);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(270, 50);
            roundedButton7.TabIndex = 23;
            roundedButton7.Text = "LICENCIA";
            roundedButton7.TextColor = Color.Black;
            roundedButton7.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(tabControl1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(1053, 566);
            panel1.TabIndex = 23;
            // 
            // FrmAgregarRenovacionIntConcedida
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1070, 581);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAgregarRenovacionIntConcedida";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAgregarRenovacionIntConcedida";
            Load += FrmAgregarRenovacionIntConcedida_Load;
            panel2.ResumeLayout(false);
            Fechas.ResumeLayout(false);
            Fechas.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPageRenovarMarca.ResumeLayout(false);
            tabPageListarLicencias.ResumeLayout(false);
            tabPageListarLicencias.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgLicencias).EndInit();
            tabPageRenovarLicencia.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private RichTextBox richTextBox1;
        private Label lblUser;
        private Clases.RoundedButton roundedButton1;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton iconButton2;
        private TextBox txtEstado;
        private DateTimePicker dateFechVencAnt;
        private Label label6;
        private DateTimePicker dateFechVencNueva;
        private Label label7;
        private GroupBox Fechas;
        private GroupBox groupBox1;
        private TextBox txtNoExpediente;
        private Label label8;
        private TabControl tabControl1;
        private TabPage tabPageListarLicencias;
        private TabPage tabPageRenovarMarca;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private Panel panel3;
        private DataGridView dtgLicencias;
        private FontAwesome.Sharp.IconButton iconButton18;
        private Label label18;
        private FontAwesome.Sharp.IconButton btnAgregarArchivo;
        private TabPage tabPageRenovarLicencia;
        private Panel panel4;
        private Panel panel5;
        private Clases.RoundedButton roundedButton7;
        private Label label24;
        private DateTimePicker dtpFechaFinAntigua;
        private Label label5;
        private DateTimePicker dtpFechaFinNueva;
        private Label label9;
        private TextBox txtTituloVerifica;
        private FontAwesome.Sharp.IconButton btnEditarH;
        private FontAwesome.Sharp.IconButton btnCancelarH;
    }
}