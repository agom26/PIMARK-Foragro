namespace Presentacion.Marcas_Nacionales
{
    partial class FrmRenovaciones
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabPageRegistradasList = new TabPage();
            panel20 = new Panel();
            panel12 = new Panel();
            panel13 = new Panel();
            panel10 = new Panel();
            label28 = new Label();
            roundedButton1 = new Clases.RoundedButton();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            panel14 = new Panel();
            panel9 = new Panel();
            label1 = new Label();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton2 = new Clases.RoundedButton();
            label2 = new Label();
            txtBuscar = new TextBox();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            roundedButton3 = new Clases.RoundedButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            panel4 = new Panel();
            dtgMarcasRenov = new DataGridView();
            tabPageMarcaDetail = new TabPage();
            panel1 = new Panel();
            Renovacion = new GroupBox();
            txtETraspaso = new TextBox();
            label26 = new Label();
            txtERenovacion = new TextBox();
            label27 = new Label();
            comboBoxTipoSigno = new ComboBox();
            comboBoxSignoDistintivo = new ComboBox();
            label21 = new Label();
            panel3 = new Panel();
            dateTimePFecha_vencimiento = new DateTimePicker();
            label19 = new Label();
            dateTimePFecha_Registro = new DateTimePicker();
            label18 = new Label();
            txtRegistro = new TextBox();
            label17 = new Label();
            txtFolio = new TextBox();
            label15 = new Label();
            txtLibro = new TextBox();
            label20 = new Label();
            checkBox1 = new CheckBox();
            richTextBox1 = new RichTextBox();
            label16 = new Label();
            roundedButton6 = new Clases.RoundedButton();
            label3 = new Label();
            textBoxEstatus = new TextBox();
            label14 = new Label();
            datePickerFechaSolicitud = new DateTimePicker();
            label13 = new Label();
            txtNombreAgente = new TextBox();
            label12 = new Label();
            btnAgregarAgente = new Clases.RoundedButton();
            txtEntidadTitular = new TextBox();
            label11 = new Label();
            txtDireccionTitular = new TextBox();
            label10 = new Label();
            txtNombreTitular = new TextBox();
            label9 = new Label();
            btnAgregarTitular = new Clases.RoundedButton();
            btnQuitarImagen = new FontAwesome.Sharp.IconButton();
            btnSubirImagen = new FontAwesome.Sharp.IconButton();
            pictureBox1 = new PictureBox();
            label8 = new Label();
            label5 = new Label();
            txtClase = new TextBox();
            label4 = new Label();
            txtNombre = new TextBox();
            label6 = new Label();
            txtExpediente = new TextBox();
            label7 = new Label();
            btnTraspasar = new FontAwesome.Sharp.IconButton();
            btnActualizarM = new FontAwesome.Sharp.IconButton();
            btnCancelarM = new FontAwesome.Sharp.IconButton();
            tabPageHistorialMarca = new TabPage();
            panel2 = new Panel();
            iconButton9 = new FontAwesome.Sharp.IconButton();
            btnEditarEstadoHistorial = new FontAwesome.Sharp.IconButton();
            dtgHistorialR = new DataGridView();
            panel5 = new Panel();
            label22 = new Label();
            tabPageHistorialDetail = new TabPage();
            panel6 = new Panel();
            roundedButton7 = new Clases.RoundedButton();
            panel7 = new Panel();
            btnEditarH = new FontAwesome.Sharp.IconButton();
            labelUserEditor = new Label();
            btnCancelarH = new FontAwesome.Sharp.IconButton();
            lblUser = new Label();
            richTextBoxAnotacionesH = new RichTextBox();
            comboBoxEstatusH = new ComboBox();
            label24 = new Label();
            dateTimePickerFechaH = new DateTimePicker();
            label23 = new Label();
            label25 = new Label();
            panel8 = new Panel();
            tabControl1.SuspendLayout();
            tabPageRegistradasList.SuspendLayout();
            panel20.SuspendLayout();
            panel12.SuspendLayout();
            panel13.SuspendLayout();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            panel14.SuspendLayout();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgMarcasRenov).BeginInit();
            tabPageMarcaDetail.SuspendLayout();
            panel1.SuspendLayout();
            Renovacion.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPageHistorialMarca.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgHistorialR).BeginInit();
            tabPageHistorialDetail.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageRegistradasList);
            tabControl1.Controls.Add(tabPageMarcaDetail);
            tabControl1.Controls.Add(tabPageHistorialMarca);
            tabControl1.Controls.Add(tabPageHistorialDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 827);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPageRegistradasList
            // 
            tabPageRegistradasList.AutoScroll = true;
            tabPageRegistradasList.Controls.Add(panel20);
            tabPageRegistradasList.Controls.Add(iconButton3);
            tabPageRegistradasList.Controls.Add(ibtnEditar);
            tabPageRegistradasList.Controls.Add(panel4);
            tabPageRegistradasList.Location = new Point(4, 26);
            tabPageRegistradasList.Name = "tabPageRegistradasList";
            tabPageRegistradasList.Padding = new Padding(3);
            tabPageRegistradasList.Size = new Size(1161, 797);
            tabPageRegistradasList.TabIndex = 0;
            tabPageRegistradasList.UseVisualStyleBackColor = true;
            // 
            // panel20
            // 
            panel20.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel20.Controls.Add(panel12);
            panel20.Controls.Add(label2);
            panel20.Controls.Add(txtBuscar);
            panel20.Controls.Add(ibtnBuscar);
            panel20.Controls.Add(roundedButton3);
            panel20.Location = new Point(22, 6);
            panel20.Name = "panel20";
            panel20.Size = new Size(1130, 153);
            panel20.TabIndex = 171;
            // 
            // panel12
            // 
            panel12.Controls.Add(panel13);
            panel12.Controls.Add(iconPictureBox1);
            panel12.Controls.Add(panel14);
            panel12.Location = new Point(3, 3);
            panel12.Name = "panel12";
            panel12.Size = new Size(595, 49);
            panel12.TabIndex = 174;
            // 
            // panel13
            // 
            panel13.Controls.Add(panel10);
            panel13.Controls.Add(roundedButton1);
            panel13.Dock = DockStyle.Left;
            panel13.Location = new Point(346, 0);
            panel13.Name = "panel13";
            panel13.Size = new Size(249, 49);
            panel13.TabIndex = 170;
            // 
            // panel10
            // 
            panel10.BackColor = Color.FromArgb(175, 192, 218);
            panel10.Controls.Add(label28);
            panel10.Location = new Point(18, 11);
            panel10.Margin = new Padding(0);
            panel10.Name = "panel10";
            panel10.Size = new Size(219, 28);
            panel10.TabIndex = 175;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Dock = DockStyle.Fill;
            label28.Font = new Font("Century Gothic", 12F);
            label28.Location = new Point(0, 0);
            label28.Name = "label28";
            label28.Size = new Size(220, 21);
            label28.TabIndex = 166;
            label28.Text = "TRÁMITE DE RENOVACIÓN";
            label28.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // roundedButton1
            // 
            roundedButton1.AutoSize = true;
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderRadius = 50;
            roundedButton1.BorderSize = 0;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(0, 0);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(249, 49);
            roundedButton1.TabIndex = 167;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.Transparent;
            iconPictureBox1.Dock = DockStyle.Left;
            iconPictureBox1.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox1.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 49;
            iconPictureBox1.Location = new Point(291, 0);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(55, 49);
            iconPictureBox1.TabIndex = 166;
            iconPictureBox1.TabStop = false;
            // 
            // panel14
            // 
            panel14.Controls.Add(panel9);
            panel14.Controls.Add(roundedButton2);
            panel14.Dock = DockStyle.Left;
            panel14.Location = new Point(0, 0);
            panel14.Name = "panel14";
            panel14.Size = new Size(291, 49);
            panel14.TabIndex = 169;
            // 
            // panel9
            // 
            panel9.BackColor = Color.FromArgb(175, 192, 218);
            panel9.Controls.Add(label1);
            panel9.Controls.Add(iconPictureBox4);
            panel9.Location = new Point(29, 10);
            panel9.Name = "panel9";
            panel9.Size = new Size(232, 29);
            panel9.TabIndex = 174;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("Century Gothic", 12F);
            label1.Location = new Point(30, 0);
            label1.Name = "label1";
            label1.Size = new Size(192, 21);
            label1.TabIndex = 166;
            label1.Text = "MARCAS NACIONALES";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // iconPictureBox4
            // 
            iconPictureBox4.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox4.Dock = DockStyle.Left;
            iconPictureBox4.ForeColor = SystemColors.ControlText;
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.Flag;
            iconPictureBox4.IconColor = SystemColors.ControlText;
            iconPictureBox4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox4.IconSize = 29;
            iconPictureBox4.Location = new Point(0, 0);
            iconPictureBox4.Name = "iconPictureBox4";
            iconPictureBox4.Size = new Size(30, 29);
            iconPictureBox4.TabIndex = 165;
            iconPictureBox4.TabStop = false;
            // 
            // roundedButton2
            // 
            roundedButton2.AutoSize = true;
            roundedButton2.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderRadius = 50;
            roundedButton2.BorderSize = 0;
            roundedButton2.Enabled = false;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(0, 0);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(291, 49);
            roundedButton2.TabIndex = 164;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(236, 236, 238);
            label2.Font = new Font("Century Gothic", 10F);
            label2.Location = new Point(269, 92);
            label2.Name = "label2";
            label2.Size = new Size(236, 19);
            label2.TabIndex = 0;
            label2.Text = "Buscar por nombre o expediente";
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top;
            txtBuscar.Location = new Point(285, 116);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(301, 22);
            txtBuscar.TabIndex = 1;
            // 
            // ibtnBuscar
            // 
            ibtnBuscar.Anchor = AnchorStyles.Top;
            ibtnBuscar.BackColor = Color.FromArgb(251, 140, 0);
            ibtnBuscar.FlatAppearance.BorderSize = 0;
            ibtnBuscar.FlatStyle = FlatStyle.Flat;
            ibtnBuscar.Font = new Font("Century Gothic", 10F);
            ibtnBuscar.ForeColor = Color.White;
            ibtnBuscar.IconChar = FontAwesome.Sharp.IconChar.Search;
            ibtnBuscar.IconColor = Color.White;
            ibtnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnBuscar.IconSize = 30;
            ibtnBuscar.Location = new Point(619, 102);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(144, 36);
            ibtnBuscar.TabIndex = 16;
            ibtnBuscar.Text = "BUSCAR";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            ibtnBuscar.Click += ibtnBuscar_Click;
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.Top;
            roundedButton3.BackColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BackgroundColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderRadius = 60;
            roundedButton3.BorderSize = 0;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(232, 90);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(560, 61);
            roundedButton3.TabIndex = 22;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // iconButton3
            // 
            iconButton3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconButton3.BackColor = Color.FromArgb(255, 112, 67);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 30;
            iconButton3.Location = new Point(989, 220);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(166, 37);
            iconButton3.TabIndex = 48;
            iconButton3.Text = "ABANDONAR";
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // ibtnEditar
            // 
            ibtnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ibtnEditar.BackColor = Color.FromArgb(96, 149, 241);
            ibtnEditar.FlatAppearance.BorderSize = 0;
            ibtnEditar.FlatStyle = FlatStyle.Flat;
            ibtnEditar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            ibtnEditar.ForeColor = Color.White;
            ibtnEditar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            ibtnEditar.IconColor = Color.White;
            ibtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEditar.IconSize = 30;
            ibtnEditar.Location = new Point(989, 165);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(166, 37);
            ibtnEditar.TabIndex = 46;
            ibtnEditar.Text = "EDITAR/ VER";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel4.BackColor = Color.FromArgb(192, 202, 212);
            panel4.Controls.Add(dtgMarcasRenov);
            panel4.Location = new Point(22, 165);
            panel4.Name = "panel4";
            panel4.Size = new Size(961, 542);
            panel4.TabIndex = 45;
            // 
            // dtgMarcasRenov
            // 
            dtgMarcasRenov.AllowUserToAddRows = false;
            dtgMarcasRenov.AllowUserToDeleteRows = false;
            dtgMarcasRenov.AllowUserToResizeRows = false;
            dtgMarcasRenov.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgMarcasRenov.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgMarcasRenov.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgMarcasRenov.BorderStyle = BorderStyle.None;
            dtgMarcasRenov.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgMarcasRenov.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgMarcasRenov.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgMarcasRenov.ColumnHeadersHeight = 40;
            dtgMarcasRenov.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtgMarcasRenov.DefaultCellStyle = dataGridViewCellStyle2;
            dtgMarcasRenov.EnableHeadersVisualStyles = false;
            dtgMarcasRenov.GridColor = Color.LightGray;
            dtgMarcasRenov.Location = new Point(10, 10);
            dtgMarcasRenov.MultiSelect = false;
            dtgMarcasRenov.Name = "dtgMarcasRenov";
            dtgMarcasRenov.ReadOnly = true;
            dtgMarcasRenov.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgMarcasRenov.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgMarcasRenov.RowHeadersWidth = 51;
            dtgMarcasRenov.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgMarcasRenov.Size = new Size(940, 514);
            dtgMarcasRenov.TabIndex = 29;
            dtgMarcasRenov.CellDoubleClick += dtgMarcasRenov_CellDoubleClick;
            // 
            // tabPageMarcaDetail
            // 
            tabPageMarcaDetail.AutoScroll = true;
            tabPageMarcaDetail.Controls.Add(panel1);
            tabPageMarcaDetail.Location = new Point(4, 24);
            tabPageMarcaDetail.Name = "tabPageMarcaDetail";
            tabPageMarcaDetail.Padding = new Padding(3);
            tabPageMarcaDetail.Size = new Size(1161, 799);
            tabPageMarcaDetail.TabIndex = 1;
            tabPageMarcaDetail.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(Renovacion);
            panel1.Controls.Add(comboBoxTipoSigno);
            panel1.Controls.Add(comboBoxSignoDistintivo);
            panel1.Controls.Add(label21);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(roundedButton6);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBoxEstatus);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(datePickerFechaSolicitud);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(txtNombreAgente);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(btnAgregarAgente);
            panel1.Controls.Add(txtEntidadTitular);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(txtDireccionTitular);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(txtNombreTitular);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(btnAgregarTitular);
            panel1.Controls.Add(btnQuitarImagen);
            panel1.Controls.Add(btnSubirImagen);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(txtClase);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtNombre);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(txtExpediente);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(btnTraspasar);
            panel1.Controls.Add(btnActualizarM);
            panel1.Controls.Add(btnCancelarM);
            panel1.Location = new Point(52, 43);
            panel1.Name = "panel1";
            panel1.Size = new Size(1081, 1241);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // Renovacion
            // 
            Renovacion.Controls.Add(txtETraspaso);
            Renovacion.Controls.Add(label26);
            Renovacion.Controls.Add(txtERenovacion);
            Renovacion.Controls.Add(label27);
            Renovacion.Font = new Font("Century Gothic", 9F);
            Renovacion.Location = new Point(781, 696);
            Renovacion.Name = "Renovacion";
            Renovacion.Size = new Size(276, 188);
            Renovacion.TabIndex = 146;
            Renovacion.TabStop = false;
            Renovacion.Text = "No. Expediente";
            // 
            // txtETraspaso
            // 
            txtETraspaso.Font = new Font("Century Gothic", 10F);
            txtETraspaso.Location = new Point(16, 129);
            txtETraspaso.Name = "txtETraspaso";
            txtETraspaso.Size = new Size(233, 24);
            txtETraspaso.TabIndex = 48;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Century Gothic", 10F);
            label26.Location = new Point(16, 105);
            label26.Name = "label26";
            label26.Size = new Size(67, 19);
            label26.TabIndex = 47;
            label26.Text = "Traspaso";
            // 
            // txtERenovacion
            // 
            txtERenovacion.Font = new Font("Century Gothic", 10F);
            txtERenovacion.Location = new Point(16, 62);
            txtERenovacion.Name = "txtERenovacion";
            txtERenovacion.Size = new Size(233, 24);
            txtERenovacion.TabIndex = 46;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Century Gothic", 10F);
            label27.Location = new Point(16, 38);
            label27.Name = "label27";
            label27.Size = new Size(93, 19);
            label27.TabIndex = 45;
            label27.Text = "Renovación";
            // 
            // comboBoxTipoSigno
            // 
            comboBoxTipoSigno.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxTipoSigno.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTipoSigno.FlatStyle = FlatStyle.Flat;
            comboBoxTipoSigno.FormattingEnabled = true;
            comboBoxTipoSigno.Items.AddRange(new object[] { "Denominativa", "Mixta", "Gráfica/Figurativa", "Sonora", "Olfativa" });
            comboBoxTipoSigno.Location = new Point(399, 254);
            comboBoxTipoSigno.Name = "comboBoxTipoSigno";
            comboBoxTipoSigno.Size = new Size(292, 25);
            comboBoxTipoSigno.TabIndex = 145;
            // 
            // comboBoxSignoDistintivo
            // 
            comboBoxSignoDistintivo.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxSignoDistintivo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSignoDistintivo.FlatStyle = FlatStyle.Flat;
            comboBoxSignoDistintivo.FormattingEnabled = true;
            comboBoxSignoDistintivo.Items.AddRange(new object[] { "Marca", "Nombre comercial", "Señal de publicidad", "Emblema", "Indicación geográfica", "Denominación de origen" });
            comboBoxSignoDistintivo.Location = new Point(55, 254);
            comboBoxSignoDistintivo.Name = "comboBoxSignoDistintivo";
            comboBoxSignoDistintivo.Size = new Size(280, 25);
            comboBoxSignoDistintivo.TabIndex = 144;
            // 
            // label21
            // 
            label21.Anchor = AnchorStyles.None;
            label21.AutoSize = true;
            label21.Font = new Font("Century Gothic", 10F);
            label21.Location = new Point(399, 230);
            label21.Name = "label21";
            label21.Size = new Size(36, 19);
            label21.TabIndex = 143;
            label21.Text = "Tipo";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.Controls.Add(dateTimePFecha_vencimiento);
            panel3.Controls.Add(label19);
            panel3.Controls.Add(dateTimePFecha_Registro);
            panel3.Controls.Add(label18);
            panel3.Controls.Add(txtRegistro);
            panel3.Controls.Add(label17);
            panel3.Controls.Add(txtFolio);
            panel3.Controls.Add(label15);
            panel3.Controls.Add(txtLibro);
            panel3.Controls.Add(label20);
            panel3.Font = new Font("Century Gothic", 10F);
            panel3.Location = new Point(55, 954);
            panel3.Name = "panel3";
            panel3.Size = new Size(636, 185);
            panel3.TabIndex = 140;
            // 
            // dateTimePFecha_vencimiento
            // 
            dateTimePFecha_vencimiento.CalendarForeColor = Color.Red;
            dateTimePFecha_vencimiento.Enabled = false;
            dateTimePFecha_vencimiento.Format = DateTimePickerFormat.Short;
            dateTimePFecha_vencimiento.Location = new Point(425, 138);
            dateTimePFecha_vencimiento.Name = "dateTimePFecha_vencimiento";
            dateTimePFecha_vencimiento.Size = new Size(198, 24);
            dateTimePFecha_vencimiento.TabIndex = 41;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(425, 103);
            label19.Name = "label19";
            label19.Size = new Size(166, 19);
            label19.TabIndex = 40;
            label19.Text = "Fecha de vencimiento";
            // 
            // dateTimePFecha_Registro
            // 
            dateTimePFecha_Registro.Format = DateTimePickerFormat.Short;
            dateTimePFecha_Registro.Location = new Point(39, 138);
            dateTimePFecha_Registro.Name = "dateTimePFecha_Registro";
            dateTimePFecha_Registro.Size = new Size(172, 24);
            dateTimePFecha_Registro.TabIndex = 39;
            dateTimePFecha_Registro.ValueChanged += dateTimePFecha_Registro_ValueChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(39, 113);
            label18.Name = "label18";
            label18.Size = new Size(128, 19);
            label18.TabIndex = 38;
            label18.Text = "Fecha de registro";
            // 
            // txtRegistro
            // 
            txtRegistro.Location = new Point(39, 56);
            txtRegistro.Name = "txtRegistro";
            txtRegistro.Size = new Size(172, 24);
            txtRegistro.TabIndex = 39;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(37, 31);
            label17.Name = "label17";
            label17.Size = new Size(62, 19);
            label17.TabIndex = 38;
            label17.Text = "Registro";
            // 
            // txtFolio
            // 
            txtFolio.Location = new Point(245, 56);
            txtFolio.Name = "txtFolio";
            txtFolio.Size = new Size(172, 24);
            txtFolio.TabIndex = 9;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(245, 30);
            label15.Name = "label15";
            label15.Size = new Size(40, 19);
            label15.TabIndex = 8;
            label15.Text = "Folio";
            // 
            // txtLibro
            // 
            txtLibro.Location = new Point(451, 56);
            txtLibro.Name = "txtLibro";
            txtLibro.Size = new Size(172, 24);
            txtLibro.TabIndex = 11;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(451, 30);
            label20.Name = "label20";
            label20.Size = new Size(41, 19);
            label20.TabIndex = 10;
            label20.Text = "Libro";
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.None;
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Century Gothic", 10F);
            checkBox1.Location = new Point(55, 912);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(86, 23);
            checkBox1.TabIndex = 139;
            checkBox1.Text = "Registrar";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.None;
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Font = new Font("Century Gothic", 10F);
            richTextBox1.Location = new Point(55, 786);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(638, 120);
            richTextBox1.TabIndex = 138;
            richTextBox1.Text = "";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.None;
            label16.AutoSize = true;
            label16.Font = new Font("Century Gothic", 10F);
            label16.Location = new Point(55, 760);
            label16.Name = "label16";
            label16.Size = new Size(111, 19);
            label16.TabIndex = 137;
            label16.Text = "Observaciones";
            // 
            // roundedButton6
            // 
            roundedButton6.BackColor = Color.LightSteelBlue;
            roundedButton6.BackgroundColor = Color.LightSteelBlue;
            roundedButton6.BorderColor = Color.LightSteelBlue;
            roundedButton6.BorderRadius = 40;
            roundedButton6.BorderSize = 0;
            roundedButton6.FlatAppearance.BorderSize = 0;
            roundedButton6.FlatStyle = FlatStyle.Flat;
            roundedButton6.Font = new Font("Century Gothic", 9F);
            roundedButton6.ForeColor = Color.Black;
            roundedButton6.Location = new Point(765, 617);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(276, 56);
            roundedButton6.TabIndex = 136;
            roundedButton6.Text = "VER HISTORIAL";
            roundedButton6.TextColor = Color.Black;
            roundedButton6.UseVisualStyleBackColor = false;
            roundedButton6.Click += roundedButton6_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 19F);
            label3.Location = new Point(432, 11);
            label3.Name = "label3";
            label3.Size = new Size(0, 32);
            label3.TabIndex = 108;
            // 
            // textBoxEstatus
            // 
            textBoxEstatus.Anchor = AnchorStyles.None;
            textBoxEstatus.Enabled = false;
            textBoxEstatus.Font = new Font("Century Gothic", 10F);
            textBoxEstatus.Location = new Point(399, 345);
            textBoxEstatus.Name = "textBoxEstatus";
            textBoxEstatus.ReadOnly = true;
            textBoxEstatus.Size = new Size(290, 24);
            textBoxEstatus.TabIndex = 134;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.None;
            label14.AutoSize = true;
            label14.Font = new Font("Century Gothic", 10F);
            label14.Location = new Point(399, 316);
            label14.Name = "label14";
            label14.Size = new Size(106, 19);
            label14.TabIndex = 133;
            label14.Text = "Estado actual";
            // 
            // datePickerFechaSolicitud
            // 
            datePickerFechaSolicitud.Anchor = AnchorStyles.None;
            datePickerFechaSolicitud.Font = new Font("Century Gothic", 10F);
            datePickerFechaSolicitud.Format = DateTimePickerFormat.Short;
            datePickerFechaSolicitud.Location = new Point(399, 170);
            datePickerFechaSolicitud.Name = "datePickerFechaSolicitud";
            datePickerFechaSolicitud.Size = new Size(292, 24);
            datePickerFechaSolicitud.TabIndex = 132;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.None;
            label13.AutoSize = true;
            label13.Font = new Font("Century Gothic", 10F);
            label13.Location = new Point(399, 145);
            label13.Name = "label13";
            label13.Size = new Size(67, 19);
            label13.TabIndex = 131;
            label13.Text = "Solicitud";
            // 
            // txtNombreAgente
            // 
            txtNombreAgente.Enabled = false;
            txtNombreAgente.Font = new Font("Century Gothic", 10F);
            txtNombreAgente.Location = new Point(55, 722);
            txtNombreAgente.Name = "txtNombreAgente";
            txtNombreAgente.Size = new Size(280, 24);
            txtNombreAgente.TabIndex = 130;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Century Gothic", 10F);
            label12.Location = new Point(55, 696);
            label12.Name = "label12";
            label12.Size = new Size(64, 19);
            label12.TabIndex = 129;
            label12.Text = "Nombre";
            // 
            // btnAgregarAgente
            // 
            btnAgregarAgente.BackColor = Color.LightSteelBlue;
            btnAgregarAgente.BackgroundColor = Color.LightSteelBlue;
            btnAgregarAgente.BorderColor = Color.LightSteelBlue;
            btnAgregarAgente.BorderRadius = 40;
            btnAgregarAgente.BorderSize = 0;
            btnAgregarAgente.FlatAppearance.BorderSize = 0;
            btnAgregarAgente.FlatStyle = FlatStyle.Flat;
            btnAgregarAgente.Font = new Font("Century Gothic", 10F);
            btnAgregarAgente.ForeColor = Color.Black;
            btnAgregarAgente.Location = new Point(55, 617);
            btnAgregarAgente.Name = "btnAgregarAgente";
            btnAgregarAgente.Size = new Size(638, 56);
            btnAgregarAgente.TabIndex = 128;
            btnAgregarAgente.Text = "+ AGENTE";
            btnAgregarAgente.TextColor = Color.Black;
            btnAgregarAgente.UseVisualStyleBackColor = false;
            btnAgregarAgente.Click += roundedButton2_Click;
            // 
            // txtEntidadTitular
            // 
            txtEntidadTitular.Enabled = false;
            txtEntidadTitular.Font = new Font("Century Gothic", 10F);
            txtEntidadTitular.Location = new Point(55, 571);
            txtEntidadTitular.Name = "txtEntidadTitular";
            txtEntidadTitular.Size = new Size(280, 24);
            txtEntidadTitular.TabIndex = 127;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Century Gothic", 10F);
            label11.Location = new Point(55, 545);
            label11.Name = "label11";
            label11.Size = new Size(64, 19);
            label11.TabIndex = 126;
            label11.Text = "Entidad";
            // 
            // txtDireccionTitular
            // 
            txtDireccionTitular.Enabled = false;
            txtDireccionTitular.Font = new Font("Century Gothic", 10F);
            txtDireccionTitular.Location = new Point(407, 504);
            txtDireccionTitular.Name = "txtDireccionTitular";
            txtDireccionTitular.Size = new Size(286, 24);
            txtDireccionTitular.TabIndex = 125;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 10F);
            label10.Location = new Point(407, 480);
            label10.Name = "label10";
            label10.Size = new Size(74, 19);
            label10.TabIndex = 124;
            label10.Text = "Dirección";
            // 
            // txtNombreTitular
            // 
            txtNombreTitular.Enabled = false;
            txtNombreTitular.Font = new Font("Century Gothic", 10F);
            txtNombreTitular.Location = new Point(55, 504);
            txtNombreTitular.Name = "txtNombreTitular";
            txtNombreTitular.Size = new Size(280, 24);
            txtNombreTitular.TabIndex = 123;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 10F);
            label9.Location = new Point(55, 478);
            label9.Name = "label9";
            label9.Size = new Size(64, 19);
            label9.TabIndex = 122;
            label9.Text = "Nombre";
            // 
            // btnAgregarTitular
            // 
            btnAgregarTitular.BackColor = Color.LightSteelBlue;
            btnAgregarTitular.BackgroundColor = Color.LightSteelBlue;
            btnAgregarTitular.BorderColor = Color.LightSteelBlue;
            btnAgregarTitular.BorderRadius = 40;
            btnAgregarTitular.BorderSize = 0;
            btnAgregarTitular.FlatAppearance.BorderSize = 0;
            btnAgregarTitular.FlatStyle = FlatStyle.Flat;
            btnAgregarTitular.Font = new Font("Century Gothic", 10F);
            btnAgregarTitular.ForeColor = Color.Black;
            btnAgregarTitular.Location = new Point(55, 403);
            btnAgregarTitular.Name = "btnAgregarTitular";
            btnAgregarTitular.Size = new Size(638, 56);
            btnAgregarTitular.TabIndex = 121;
            btnAgregarTitular.Text = "+ TITULAR";
            btnAgregarTitular.TextColor = Color.Black;
            btnAgregarTitular.UseVisualStyleBackColor = false;
            btnAgregarTitular.Click += roundedButton4_Click;
            // 
            // btnQuitarImagen
            // 
            btnQuitarImagen.Anchor = AnchorStyles.None;
            btnQuitarImagen.BackColor = Color.MistyRose;
            btnQuitarImagen.FlatAppearance.BorderSize = 0;
            btnQuitarImagen.FlatStyle = FlatStyle.Flat;
            btnQuitarImagen.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            btnQuitarImagen.IconColor = Color.Black;
            btnQuitarImagen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnQuitarImagen.IconSize = 30;
            btnQuitarImagen.Location = new Point(903, 343);
            btnQuitarImagen.Name = "btnQuitarImagen";
            btnQuitarImagen.Size = new Size(74, 33);
            btnQuitarImagen.TabIndex = 120;
            btnQuitarImagen.UseVisualStyleBackColor = false;
            btnQuitarImagen.Click += iconButton2_Click;
            // 
            // btnSubirImagen
            // 
            btnSubirImagen.Anchor = AnchorStyles.None;
            btnSubirImagen.BackColor = Color.PowderBlue;
            btnSubirImagen.FlatAppearance.BorderSize = 0;
            btnSubirImagen.FlatStyle = FlatStyle.Flat;
            btnSubirImagen.IconChar = FontAwesome.Sharp.IconChar.Upload;
            btnSubirImagen.IconColor = Color.Black;
            btnSubirImagen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSubirImagen.IconSize = 30;
            btnSubirImagen.Location = new Point(823, 343);
            btnSubirImagen.Name = "btnSubirImagen";
            btnSubirImagen.Size = new Size(74, 33);
            btnSubirImagen.TabIndex = 119;
            btnSubirImagen.UseVisualStyleBackColor = false;
            btnSubirImagen.Click += iconButton1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(763, 97);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(276, 240);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 118;
            pictureBox1.TabStop = false;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 10F);
            label8.Location = new Point(855, 71);
            label8.Name = "label8";
            label8.Size = new Size(62, 19);
            label8.TabIndex = 117;
            label8.Text = "Imagen";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 10F);
            label5.Location = new Point(55, 230);
            label5.Name = "label5";
            label5.Size = new Size(110, 19);
            label5.TabIndex = 115;
            label5.Text = "Signo distintivo";
            // 
            // txtClase
            // 
            txtClase.Anchor = AnchorStyles.None;
            txtClase.Font = new Font("Century Gothic", 10F);
            txtClase.Location = new Point(53, 170);
            txtClase.Name = "txtClase";
            txtClase.Size = new Size(280, 24);
            txtClase.TabIndex = 114;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10F);
            label4.Location = new Point(53, 146);
            label4.Name = "label4";
            label4.Size = new Size(47, 19);
            label4.TabIndex = 113;
            label4.Text = "Clase";
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.None;
            txtNombre.Font = new Font("Century Gothic", 10F);
            txtNombre.Location = new Point(399, 97);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(292, 24);
            txtNombre.TabIndex = 112;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 10F);
            label6.Location = new Point(399, 73);
            label6.Name = "label6";
            label6.Size = new Size(64, 19);
            label6.TabIndex = 111;
            label6.Text = "Nombre";
            // 
            // txtExpediente
            // 
            txtExpediente.Anchor = AnchorStyles.None;
            txtExpediente.Font = new Font("Century Gothic", 10F);
            txtExpediente.Location = new Point(53, 97);
            txtExpediente.Name = "txtExpediente";
            txtExpediente.Size = new Size(280, 24);
            txtExpediente.TabIndex = 110;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 10F);
            label7.Location = new Point(53, 71);
            label7.Name = "label7";
            label7.Size = new Size(88, 19);
            label7.TabIndex = 109;
            label7.Text = "Expediente";
            // 
            // btnTraspasar
            // 
            btnTraspasar.BackColor = Color.FromArgb(0, 137, 123);
            btnTraspasar.FlatAppearance.BorderSize = 0;
            btnTraspasar.FlatStyle = FlatStyle.Flat;
            btnTraspasar.Font = new Font("Century Gothic", 10F);
            btnTraspasar.ForeColor = Color.White;
            btnTraspasar.IconChar = FontAwesome.Sharp.IconChar.FileCircleCheck;
            btnTraspasar.IconColor = Color.White;
            btnTraspasar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTraspasar.IconSize = 30;
            btnTraspasar.Location = new Point(823, 1010);
            btnTraspasar.Name = "btnTraspasar";
            btnTraspasar.Size = new Size(191, 58);
            btnTraspasar.TabIndex = 153;
            btnTraspasar.Text = "RENOVAR";
            btnTraspasar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnTraspasar.UseVisualStyleBackColor = false;
            btnTraspasar.Click += btnTraspasar_Click;
            // 
            // btnActualizarM
            // 
            btnActualizarM.BackColor = Color.FromArgb(1, 87, 155);
            btnActualizarM.FlatAppearance.BorderSize = 0;
            btnActualizarM.FlatStyle = FlatStyle.Flat;
            btnActualizarM.Font = new Font("Century Gothic", 10F);
            btnActualizarM.ForeColor = Color.White;
            btnActualizarM.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnActualizarM.IconColor = Color.White;
            btnActualizarM.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnActualizarM.IconSize = 30;
            btnActualizarM.Location = new Point(823, 937);
            btnActualizarM.Name = "btnActualizarM";
            btnActualizarM.Size = new Size(191, 58);
            btnActualizarM.TabIndex = 152;
            btnActualizarM.Text = "ACTUALIZAR";
            btnActualizarM.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnActualizarM.UseVisualStyleBackColor = false;
            btnActualizarM.Click += btnActualizarM_Click;
            // 
            // btnCancelarM
            // 
            btnCancelarM.BackColor = Color.Gainsboro;
            btnCancelarM.FlatAppearance.BorderSize = 0;
            btnCancelarM.FlatStyle = FlatStyle.Flat;
            btnCancelarM.Font = new Font("Century Gothic", 10F);
            btnCancelarM.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarM.IconColor = Color.Black;
            btnCancelarM.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarM.IconSize = 30;
            btnCancelarM.Location = new Point(823, 1092);
            btnCancelarM.Name = "btnCancelarM";
            btnCancelarM.Size = new Size(191, 58);
            btnCancelarM.TabIndex = 151;
            btnCancelarM.Text = "CANCELAR";
            btnCancelarM.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelarM.UseVisualStyleBackColor = false;
            btnCancelarM.Click += btnCancelarM_Click;
            // 
            // tabPageHistorialMarca
            // 
            tabPageHistorialMarca.Controls.Add(panel2);
            tabPageHistorialMarca.Location = new Point(4, 24);
            tabPageHistorialMarca.Name = "tabPageHistorialMarca";
            tabPageHistorialMarca.Size = new Size(1161, 799);
            tabPageHistorialMarca.TabIndex = 2;
            tabPageHistorialMarca.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(iconButton9);
            panel2.Controls.Add(btnEditarEstadoHistorial);
            panel2.Controls.Add(dtgHistorialR);
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(label22);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1161, 799);
            panel2.TabIndex = 0;
            // 
            // iconButton9
            // 
            iconButton9.BackColor = Color.White;
            iconButton9.FlatAppearance.BorderSize = 0;
            iconButton9.FlatStyle = FlatStyle.Flat;
            iconButton9.IconChar = FontAwesome.Sharp.IconChar.AngleLeft;
            iconButton9.IconColor = Color.Black;
            iconButton9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton9.Location = new Point(3, 21);
            iconButton9.Name = "iconButton9";
            iconButton9.Size = new Size(62, 49);
            iconButton9.TabIndex = 62;
            iconButton9.UseVisualStyleBackColor = false;
            iconButton9.Click += iconButton9_Click;
            // 
            // btnEditarEstadoHistorial
            // 
            btnEditarEstadoHistorial.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditarEstadoHistorial.BackColor = Color.FromArgb(96, 149, 241);
            btnEditarEstadoHistorial.FlatAppearance.BorderSize = 0;
            btnEditarEstadoHistorial.FlatStyle = FlatStyle.Flat;
            btnEditarEstadoHistorial.ForeColor = Color.White;
            btnEditarEstadoHistorial.IconChar = FontAwesome.Sharp.IconChar.Pen;
            btnEditarEstadoHistorial.IconColor = Color.White;
            btnEditarEstadoHistorial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditarEstadoHistorial.IconSize = 30;
            btnEditarEstadoHistorial.Location = new Point(1006, 76);
            btnEditarEstadoHistorial.Name = "btnEditarEstadoHistorial";
            btnEditarEstadoHistorial.Size = new Size(144, 37);
            btnEditarEstadoHistorial.TabIndex = 52;
            btnEditarEstadoHistorial.Text = "EDITAR/ VER";
            btnEditarEstadoHistorial.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditarEstadoHistorial.UseVisualStyleBackColor = false;
            btnEditarEstadoHistorial.Click += iconButton5_Click;
            // 
            // dtgHistorialR
            // 
            dtgHistorialR.AllowUserToAddRows = false;
            dtgHistorialR.AllowUserToDeleteRows = false;
            dtgHistorialR.AllowUserToResizeRows = false;
            dtgHistorialR.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgHistorialR.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgHistorialR.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgHistorialR.BorderStyle = BorderStyle.None;
            dtgHistorialR.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgHistorialR.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtgHistorialR.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtgHistorialR.ColumnHeadersHeight = 40;
            dtgHistorialR.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgHistorialR.EnableHeadersVisualStyles = false;
            dtgHistorialR.GridColor = Color.LightGray;
            dtgHistorialR.Location = new Point(17, 97);
            dtgHistorialR.Name = "dtgHistorialR";
            dtgHistorialR.ReadOnly = true;
            dtgHistorialR.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgHistorialR.RowHeadersWidth = 51;
            dtgHistorialR.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgHistorialR.Size = new Size(950, 492);
            dtgHistorialR.TabIndex = 50;
            dtgHistorialR.CellDoubleClick += dtgHistorialR_CellDoubleClick;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel5.BackColor = Color.FromArgb(192, 202, 212);
            panel5.Location = new Point(3, 76);
            panel5.Name = "panel5";
            panel5.Size = new Size(988, 542);
            panel5.TabIndex = 51;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Century Gothic", 19F);
            label22.Location = new Point(55, 21);
            label22.Name = "label22";
            label22.Size = new Size(138, 32);
            label22.TabIndex = 49;
            label22.Text = "HISTORIAL";
            // 
            // tabPageHistorialDetail
            // 
            tabPageHistorialDetail.Controls.Add(panel6);
            tabPageHistorialDetail.Location = new Point(4, 24);
            tabPageHistorialDetail.Name = "tabPageHistorialDetail";
            tabPageHistorialDetail.Size = new Size(1161, 799);
            tabPageHistorialDetail.TabIndex = 3;
            tabPageHistorialDetail.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Top;
            panel6.Controls.Add(roundedButton7);
            panel6.Controls.Add(panel7);
            panel6.Location = new Point(28, 37);
            panel6.Name = "panel6";
            panel6.Size = new Size(1112, 555);
            panel6.TabIndex = 2;
            // 
            // roundedButton7
            // 
            roundedButton7.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderRadius = 50;
            roundedButton7.BorderSize = 0;
            roundedButton7.Enabled = false;
            roundedButton7.FlatAppearance.BorderSize = 0;
            roundedButton7.FlatStyle = FlatStyle.Flat;
            roundedButton7.Font = new Font("Century Gothic", 13F);
            roundedButton7.ForeColor = Color.Black;
            roundedButton7.Location = new Point(414, 43);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(270, 50);
            roundedButton7.TabIndex = 18;
            roundedButton7.Text = "ESTADO";
            roundedButton7.TextColor = Color.Black;
            roundedButton7.UseVisualStyleBackColor = false;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(192, 202, 212);
            panel7.Controls.Add(btnEditarH);
            panel7.Controls.Add(labelUserEditor);
            panel7.Controls.Add(btnCancelarH);
            panel7.Controls.Add(lblUser);
            panel7.Controls.Add(richTextBoxAnotacionesH);
            panel7.Controls.Add(comboBoxEstatusH);
            panel7.Controls.Add(label24);
            panel7.Controls.Add(dateTimePickerFechaH);
            panel7.Controls.Add(label23);
            panel7.Controls.Add(label25);
            panel7.Controls.Add(panel8);
            panel7.Location = new Point(46, 110);
            panel7.Name = "panel7";
            panel7.Size = new Size(1012, 406);
            panel7.TabIndex = 21;
            // 
            // btnEditarH
            // 
            btnEditarH.BackColor = Color.FromArgb(1, 87, 155);
            btnEditarH.FlatAppearance.BorderSize = 0;
            btnEditarH.FlatStyle = FlatStyle.Flat;
            btnEditarH.Font = new Font("Century Gothic", 10F);
            btnEditarH.ForeColor = Color.White;
            btnEditarH.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnEditarH.IconColor = Color.White;
            btnEditarH.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditarH.IconSize = 30;
            btnEditarH.Location = new Point(315, 321);
            btnEditarH.Name = "btnEditarH";
            btnEditarH.Size = new Size(179, 34);
            btnEditarH.TabIndex = 20;
            btnEditarH.Text = "ACTUALIZAR";
            btnEditarH.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnEditarH.UseVisualStyleBackColor = false;
            btnEditarH.Click += btnEditarH_Click;
            // 
            // labelUserEditor
            // 
            labelUserEditor.AutoSize = true;
            labelUserEditor.BackColor = Color.White;
            labelUserEditor.Location = new Point(476, 51);
            labelUserEditor.Name = "labelUserEditor";
            labelUserEditor.Size = new Size(45, 17);
            labelUserEditor.TabIndex = 18;
            labelUserEditor.Text = "Fecha";
            // 
            // btnCancelarH
            // 
            btnCancelarH.BackColor = Color.White;
            btnCancelarH.FlatAppearance.BorderSize = 0;
            btnCancelarH.FlatStyle = FlatStyle.Flat;
            btnCancelarH.Font = new Font("Century Gothic", 10F);
            btnCancelarH.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarH.IconColor = Color.Black;
            btnCancelarH.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarH.IconSize = 30;
            btnCancelarH.Location = new Point(520, 321);
            btnCancelarH.Name = "btnCancelarH";
            btnCancelarH.Size = new Size(179, 34);
            btnCancelarH.TabIndex = 19;
            btnCancelarH.Text = "CANCELAR";
            btnCancelarH.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelarH.UseVisualStyleBackColor = false;
            btnCancelarH.Click += btnCancelarH_Click;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.BackColor = Color.White;
            lblUser.Location = new Point(295, 51);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(45, 17);
            lblUser.TabIndex = 17;
            lblUser.Text = "Fecha";
            // 
            // richTextBoxAnotacionesH
            // 
            richTextBoxAnotacionesH.BackColor = Color.White;
            richTextBoxAnotacionesH.BorderStyle = BorderStyle.FixedSingle;
            richTextBoxAnotacionesH.Location = new Point(295, 193);
            richTextBoxAnotacionesH.Name = "richTextBoxAnotacionesH";
            richTextBoxAnotacionesH.Size = new Size(431, 102);
            richTextBoxAnotacionesH.TabIndex = 16;
            richTextBoxAnotacionesH.Text = "";
            // 
            // comboBoxEstatusH
            // 
            comboBoxEstatusH.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxEstatusH.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEstatusH.FlatStyle = FlatStyle.Flat;
            comboBoxEstatusH.FormattingEnabled = true;
            comboBoxEstatusH.Items.AddRange(new object[] { "Ingresada", "Examen de forma", "Examen de fondo", "Requerimiento", "Objeción", "Edicto", "Publicación", "Oposición", "Orden de pago", "Abandono", "Licencia de uso", "Trámite de renovación", "Registrada" });
            comboBoxEstatusH.Location = new Point(476, 116);
            comboBoxEstatusH.Name = "comboBoxEstatusH";
            comboBoxEstatusH.Size = new Size(250, 25);
            comboBoxEstatusH.TabIndex = 15;
            comboBoxEstatusH.SelectedIndexChanged += comboBoxEstatusH_SelectedIndexChanged;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.BackColor = Color.White;
            label24.Location = new Point(295, 91);
            label24.Name = "label24";
            label24.Size = new Size(45, 17);
            label24.TabIndex = 11;
            label24.Text = "Fecha";
            // 
            // dateTimePickerFechaH
            // 
            dateTimePickerFechaH.Format = DateTimePickerFormat.Short;
            dateTimePickerFechaH.Location = new Point(295, 114);
            dateTimePickerFechaH.Name = "dateTimePickerFechaH";
            dateTimePickerFechaH.Size = new Size(154, 22);
            dateTimePickerFechaH.TabIndex = 14;
            dateTimePickerFechaH.ValueChanged += dateTimePickerFechaH_ValueChanged;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.BackColor = Color.White;
            label23.Location = new Point(476, 93);
            label23.Name = "label23";
            label23.Size = new Size(48, 17);
            label23.TabIndex = 12;
            label23.Text = "Estado";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.BackColor = Color.White;
            label25.Location = new Point(295, 170);
            label25.Name = "label25";
            label25.Size = new Size(84, 17);
            label25.TabIndex = 13;
            label25.Text = "Anotaciones";
            // 
            // panel8
            // 
            panel8.BackColor = Color.White;
            panel8.Location = new Point(42, 21);
            panel8.Name = "panel8";
            panel8.Size = new Size(946, 369);
            panel8.TabIndex = 21;
            // 
            // FrmRenovaciones
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmRenovaciones";
            Text = "FrmRenovaciones";
            Load += FrmRenovaciones_Load;
            tabControl1.ResumeLayout(false);
            tabPageRegistradasList.ResumeLayout(false);
            panel20.ResumeLayout(false);
            panel20.PerformLayout();
            panel12.ResumeLayout(false);
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgMarcasRenov).EndInit();
            tabPageMarcaDetail.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            Renovacion.ResumeLayout(false);
            Renovacion.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPageHistorialMarca.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgHistorialR).EndInit();
            tabPageHistorialDetail.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPageRegistradasList;
        private TabPage tabPageMarcaDetail;
        private TabPage tabPageHistorialMarca;
        private TabPage tabPageHistorialDetail;
        private Panel panel4;
        private DataGridView dtgMarcasRenov;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private Panel panel1;
        private Clases.RoundedButton roundedButton6;
        private Label label3;
        private TextBox textBoxEstatus;
        private Label label14;
        private DateTimePicker datePickerFechaSolicitud;
        private Label label13;
        private TextBox txtNombreAgente;
        private Label label12;
        private Clases.RoundedButton btnAgregarAgente;
        private TextBox txtEntidadTitular;
        private Label label11;
        private TextBox txtDireccionTitular;
        private Label label10;
        private TextBox txtNombreTitular;
        private Label label9;
        private Clases.RoundedButton btnAgregarTitular;
        private FontAwesome.Sharp.IconButton btnQuitarImagen;
        private FontAwesome.Sharp.IconButton btnSubirImagen;
        private PictureBox pictureBox1;
        private Label label8;
        private Label label5;
        private TextBox txtClase;
        private Label label4;
        private TextBox txtNombre;
        private Label label6;
        private TextBox txtExpediente;
        private Label label7;
        private Panel panel3;
        private DateTimePicker dateTimePFecha_vencimiento;
        private Label label19;
        private DateTimePicker dateTimePFecha_Registro;
        private Label label18;
        private TextBox txtRegistro;
        private Label label17;
        private TextBox txtFolio;
        private Label label15;
        private TextBox txtLibro;
        private Label label20;
        private CheckBox checkBox1;
        private RichTextBox richTextBox1;
        private Label label16;
        private Panel panel2;
        private FontAwesome.Sharp.IconButton btnEditarEstadoHistorial;
        private DataGridView dtgHistorialR;
        private Panel panel5;
        private Label label22;
        private Panel panel6;
        private Clases.RoundedButton roundedButton7;
        private Panel panel7;
        private FontAwesome.Sharp.IconButton btnEditarH;
        private Label labelUserEditor;
        private FontAwesome.Sharp.IconButton btnCancelarH;
        private Label lblUser;
        private RichTextBox richTextBoxAnotacionesH;
        private ComboBox comboBoxEstatusH;
        private Label label24;
        private DateTimePicker dateTimePickerFechaH;
        private Label label23;
        private Label label25;
        private Panel panel8;
        private ComboBox comboBoxTipoSigno;
        private ComboBox comboBoxSignoDistintivo;
        private Label label21;
        private GroupBox Renovacion;
        private TextBox txtETraspaso;
        private Label label26;
        private TextBox txtERenovacion;
        private Label label27;
        private FontAwesome.Sharp.IconButton iconButton9;
        private FontAwesome.Sharp.IconButton btnTraspasar;
        private FontAwesome.Sharp.IconButton btnCancelarM;
        private FontAwesome.Sharp.IconButton btnActualizarM;
        private Panel panel20;
        private Label label2;
        private TextBox txtBuscar;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private Clases.RoundedButton roundedButton3;
        private Panel panel12;
        private Panel panel13;
        private Panel panel10;
        private Label label28;
        private Clases.RoundedButton roundedButton1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Panel panel14;
        private Panel panel9;
        private Label label1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private Clases.RoundedButton roundedButton2;
    }
}