namespace Presentacion.Personas
{
    partial class FrmAdministrarTitulares
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
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabListTitulares = new TabPage();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            label2 = new Label();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            textBox1 = new TextBox();
            label1 = new Label();
            roundedButton5 = new Clases.RoundedButton();
            roundedButton3 = new Clases.RoundedButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            dtgTitulares = new DataGridView();
            tabTitularDetail = new TabPage();
            comboBox1 = new ComboBox();
            txtNombreContacto = new TextBox();
            label9 = new Label();
            txtTelefonoContacto = new TextBox();
            label8 = new Label();
            btnCancelarTit = new FontAwesome.Sharp.IconButton();
            btnGuardarTit = new FontAwesome.Sharp.IconButton();
            txtCorreoContacto = new TextBox();
            label7 = new Label();
            txtDireccionTitular = new TextBox();
            label6 = new Label();
            label5 = new Label();
            txtNitTitular = new TextBox();
            label4 = new Label();
            txtNombreTitular = new TextBox();
            label3 = new Label();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            lblAccion = new Label();
            roundedButton2 = new Clases.RoundedButton();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label10 = new Label();
            roundedButton1 = new Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabListTitulares.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgTitulares).BeginInit();
            tabTitularDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabListTitulares);
            tabControl1.Controls.Add(tabTitularDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 12F);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 827);
            tabControl1.TabIndex = 1;
            // 
            // tabListTitulares
            // 
            tabListTitulares.Controls.Add(iconPictureBox2);
            tabListTitulares.Controls.Add(label2);
            tabListTitulares.Controls.Add(iconButton1);
            tabListTitulares.Controls.Add(textBox1);
            tabListTitulares.Controls.Add(label1);
            tabListTitulares.Controls.Add(roundedButton5);
            tabListTitulares.Controls.Add(roundedButton3);
            tabListTitulares.Controls.Add(ibtnEditar);
            tabListTitulares.Controls.Add(ibtnAgregar);
            tabListTitulares.Controls.Add(panel1);
            tabListTitulares.Location = new Point(4, 32);
            tabListTitulares.Name = "tabListTitulares";
            tabListTitulares.Padding = new Padding(3);
            tabListTitulares.Size = new Size(1161, 791);
            tabListTitulares.TabIndex = 0;
            tabListTitulares.UseVisualStyleBackColor = true;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.Anchor = AnchorStyles.Top;
            iconPictureBox2.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox2.ForeColor = SystemColors.ControlText;
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.HospitalUser;
            iconPictureBox2.IconColor = SystemColors.ControlText;
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 40;
            iconPictureBox2.Location = new Point(418, 18);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(40, 40);
            iconPictureBox2.TabIndex = 61;
            iconPictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(175, 192, 218);
            label2.Font = new Font("Century Gothic", 15F);
            label2.Location = new Point(464, 18);
            label2.Name = "label2";
            label2.Size = new Size(111, 31);
            label2.TabIndex = 55;
            label2.Text = "Titulares";
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.Top;
            iconButton1.BackColor = Color.FromArgb(251, 140, 0);
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 10F);
            iconButton1.ForeColor = Color.White;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 30;
            iconButton1.Location = new Point(589, 129);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(144, 32);
            iconButton1.TabIndex = 58;
            iconButton1.Text = "BUSCAR";
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top;
            textBox1.Location = new Point(250, 130);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(301, 32);
            textBox1.TabIndex = 57;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(236, 236, 238);
            label1.Font = new Font("Century Gothic", 9F);
            label1.Location = new Point(250, 107);
            label1.Name = "label1";
            label1.Size = new Size(195, 20);
            label1.TabIndex = 56;
            label1.Text = "Buscar por nombre o pais";
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.Top;
            roundedButton5.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderColor = Color.FromArgb(196, 195, 209);
            roundedButton5.BorderRadius = 60;
            roundedButton5.BorderSize = 0;
            roundedButton5.Enabled = false;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(226, 6);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(556, 61);
            roundedButton5.TabIndex = 60;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.Top;
            roundedButton3.BackColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BackgroundColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderRadius = 20;
            roundedButton3.BorderSize = 0;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(226, 101);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(556, 74);
            roundedButton3.TabIndex = 59;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // ibtnEditar
            // 
            ibtnEditar.Anchor = AnchorStyles.Top;
            ibtnEditar.BackColor = Color.FromArgb(96, 149, 241);
            ibtnEditar.FlatAppearance.BorderSize = 0;
            ibtnEditar.FlatStyle = FlatStyle.Flat;
            ibtnEditar.Font = new Font("Century Gothic", 9F);
            ibtnEditar.ForeColor = Color.White;
            ibtnEditar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            ibtnEditar.IconColor = Color.White;
            ibtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEditar.IconSize = 25;
            ibtnEditar.Location = new Point(1001, 261);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(152, 37);
            ibtnEditar.TabIndex = 10;
            ibtnEditar.Text = "EDITAR/ VER";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click;
            // 
            // ibtnAgregar
            // 
            ibtnAgregar.Anchor = AnchorStyles.Top;
            ibtnAgregar.BackColor = Color.FromArgb(50, 164, 115);
            ibtnAgregar.FlatAppearance.BorderSize = 0;
            ibtnAgregar.FlatStyle = FlatStyle.Flat;
            ibtnAgregar.Font = new Font("Century Gothic", 9F);
            ibtnAgregar.ForeColor = Color.White;
            ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            ibtnAgregar.IconColor = Color.White;
            ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnAgregar.IconSize = 25;
            ibtnAgregar.Location = new Point(1001, 203);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(152, 37);
            ibtnAgregar.TabIndex = 9;
            ibtnAgregar.Text = "AGREGAR";
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnAgregar_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgTitulares);
            panel1.Location = new Point(28, 203);
            panel1.Name = "panel1";
            panel1.Size = new Size(952, 539);
            panel1.TabIndex = 62;
            // 
            // dtgTitulares
            // 
            dtgTitulares.AllowUserToAddRows = false;
            dtgTitulares.AllowUserToDeleteRows = false;
            dtgTitulares.AllowUserToResizeRows = false;
            dataGridViewCellStyle16.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgTitulares.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            dtgTitulares.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtgTitulares.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgTitulares.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgTitulares.BorderStyle = BorderStyle.None;
            dtgTitulares.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgTitulares.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = SystemColors.Control;
            dataGridViewCellStyle17.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle17.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle17.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = DataGridViewTriState.True;
            dtgTitulares.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            dtgTitulares.ColumnHeadersHeight = 40;
            dtgTitulares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = SystemColors.Window;
            dataGridViewCellStyle18.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle18.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = DataGridViewTriState.False;
            dtgTitulares.DefaultCellStyle = dataGridViewCellStyle18;
            dtgTitulares.EnableHeadersVisualStyles = false;
            dtgTitulares.GridColor = Color.LightGray;
            dtgTitulares.Location = new Point(15, 14);
            dtgTitulares.Name = "dtgTitulares";
            dtgTitulares.ReadOnly = true;
            dtgTitulares.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = SystemColors.Control;
            dataGridViewCellStyle19.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle19.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = DataGridViewTriState.True;
            dtgTitulares.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            dtgTitulares.RowHeadersWidth = 51;
            dataGridViewCellStyle20.Font = new Font("Century Gothic", 9F);
            dtgTitulares.RowsDefaultCellStyle = dataGridViewCellStyle20;
            dtgTitulares.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgTitulares.Size = new Size(921, 500);
            dtgTitulares.TabIndex = 7;
            dtgTitulares.CellClick += dtgTitulares_CellClick;
            dtgTitulares.CellContentClick += dtgTitulares_CellContentClick;
            // 
            // tabTitularDetail
            // 
            tabTitularDetail.Controls.Add(iconPictureBox4);
            tabTitularDetail.Controls.Add(lblAccion);
            tabTitularDetail.Controls.Add(roundedButton2);
            tabTitularDetail.Controls.Add(iconPictureBox3);
            tabTitularDetail.Controls.Add(iconPictureBox1);
            tabTitularDetail.Controls.Add(label10);
            tabTitularDetail.Controls.Add(roundedButton1);
            tabTitularDetail.Controls.Add(comboBox1);
            tabTitularDetail.Controls.Add(txtNombreContacto);
            tabTitularDetail.Controls.Add(label9);
            tabTitularDetail.Controls.Add(txtTelefonoContacto);
            tabTitularDetail.Controls.Add(label8);
            tabTitularDetail.Controls.Add(btnCancelarTit);
            tabTitularDetail.Controls.Add(btnGuardarTit);
            tabTitularDetail.Controls.Add(txtCorreoContacto);
            tabTitularDetail.Controls.Add(label7);
            tabTitularDetail.Controls.Add(txtDireccionTitular);
            tabTitularDetail.Controls.Add(label6);
            tabTitularDetail.Controls.Add(label5);
            tabTitularDetail.Controls.Add(txtNitTitular);
            tabTitularDetail.Controls.Add(label4);
            tabTitularDetail.Controls.Add(txtNombreTitular);
            tabTitularDetail.Controls.Add(label3);
            tabTitularDetail.Location = new Point(4, 32);
            tabTitularDetail.Name = "tabTitularDetail";
            tabTitularDetail.Padding = new Padding(3);
            tabTitularDetail.Size = new Size(1161, 791);
            tabTitularDetail.TabIndex = 1;
            tabTitularDetail.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top;
            comboBox1.BackColor = Color.FromArgb(241, 240, 245);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Century Gothic", 9F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita", "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Baréin", "Bangladés", "Barbados", "Bielorrusia", "Birmania (Myanmar)", "Burundi", "Bután", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo (Congo-Brazzaville)", "Corea del Norte", "Corea del Sur", "Costa Rica", "Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Eswatini", "Etiopía", "Fiyi", "Filipinas", "Finlandia", "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Grecia", "Granada", "Guatemala", "Guinea", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Hungría", "Islandia", "India", "Indonesia", "Irán", "Irak", "Irlanda", "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kosovo", "Kuwait", "Laos", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein", "Lituania", "Luxemburgo", "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Mauricio", "Mauritania", "México", "Micronesia", "Moldavia", "Mónaco", "Mongolia", "Mozambique", "Namibia", "Nauru", "Nepal", "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Pakistán", "Palaos", "Palestina", "Panamá", "Paraguay", "Perú", "Polonia", "Portugal", "Qatar", "República Centroafricana", "República Checa", "República del Congo (Congo-Kinshasa)", "República Dominicana", "Rumania", "Rusia", "Ruanda", "San Cristóbal y Nieves", "San Marino", "Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Tailandia", "Taiwán", "Tanzania", "Tayikistán", "Timor Oriental", "Togo", "Tonga", "Trinidad y Tobago", "Túnez", "Turquía", "Turkmenistán", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabue" });
            comboBox1.Location = new Point(611, 211);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(389, 28);
            comboBox1.TabIndex = 86;
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Anchor = AnchorStyles.Top;
            txtNombreContacto.Font = new Font("Century Gothic", 9F);
            txtNombreContacto.Location = new Point(164, 364);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(389, 26);
            txtNombreContacto.TabIndex = 7;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 9F);
            label9.Location = new Point(164, 338);
            label9.Name = "label9";
            label9.Size = new Size(80, 20);
            label9.TabIndex = 31;
            label9.Text = "Contacto";
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Anchor = AnchorStyles.Top;
            txtTelefonoContacto.Font = new Font("Century Gothic", 9F);
            txtTelefonoContacto.Location = new Point(611, 289);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(389, 26);
            txtTelefonoContacto.TabIndex = 6;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 9F);
            label8.Location = new Point(611, 263);
            label8.Name = "label8";
            label8.Size = new Size(71, 20);
            label8.TabIndex = 29;
            label8.Text = "Teléfono";
            // 
            // btnCancelarTit
            // 
            btnCancelarTit.Anchor = AnchorStyles.Top;
            btnCancelarTit.BackColor = Color.Gainsboro;
            btnCancelarTit.FlatAppearance.BorderSize = 0;
            btnCancelarTit.FlatStyle = FlatStyle.Flat;
            btnCancelarTit.ForeColor = Color.Black;
            btnCancelarTit.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarTit.IconColor = Color.Black;
            btnCancelarTit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarTit.IconSize = 30;
            btnCancelarTit.Location = new Point(611, 440);
            btnCancelarTit.Name = "btnCancelarTit";
            btnCancelarTit.Size = new Size(389, 37);
            btnCancelarTit.TabIndex = 9;
            btnCancelarTit.Text = "Cancelar";
            btnCancelarTit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelarTit.UseVisualStyleBackColor = false;
            btnCancelarTit.Click += btnCancelarTit_Click;
            // 
            // btnGuardarTit
            // 
            btnGuardarTit.Anchor = AnchorStyles.Top;
            btnGuardarTit.BackColor = Color.FromArgb(1, 87, 155);
            btnGuardarTit.FlatAppearance.BorderSize = 0;
            btnGuardarTit.FlatStyle = FlatStyle.Flat;
            btnGuardarTit.ForeColor = Color.White;
            btnGuardarTit.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardarTit.IconColor = Color.White;
            btnGuardarTit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardarTit.IconSize = 30;
            btnGuardarTit.Location = new Point(164, 440);
            btnGuardarTit.Name = "btnGuardarTit";
            btnGuardarTit.Size = new Size(399, 37);
            btnGuardarTit.TabIndex = 8;
            btnGuardarTit.Text = "Guardar";
            btnGuardarTit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardarTit.UseVisualStyleBackColor = false;
            btnGuardarTit.Click += btnGuardarTit_Click;
            // 
            // txtCorreoContacto
            // 
            txtCorreoContacto.Anchor = AnchorStyles.Top;
            txtCorreoContacto.Font = new Font("Century Gothic", 9F);
            txtCorreoContacto.Location = new Point(164, 289);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(389, 26);
            txtCorreoContacto.TabIndex = 5;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 9F);
            label7.Location = new Point(164, 263);
            label7.Name = "label7";
            label7.Size = new Size(61, 20);
            label7.TabIndex = 28;
            label7.Text = "Correo";
            // 
            // txtDireccionTitular
            // 
            txtDireccionTitular.Anchor = AnchorStyles.Top;
            txtDireccionTitular.Font = new Font("Century Gothic", 9F);
            txtDireccionTitular.Location = new Point(611, 142);
            txtDireccionTitular.Name = "txtDireccionTitular";
            txtDireccionTitular.Size = new Size(389, 26);
            txtDireccionTitular.TabIndex = 2;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 9F);
            label6.Location = new Point(611, 116);
            label6.Name = "label6";
            label6.Size = new Size(80, 20);
            label6.TabIndex = 26;
            label6.Text = "Dirección";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 9F);
            label5.Location = new Point(611, 185);
            label5.Name = "label5";
            label5.Size = new Size(37, 20);
            label5.TabIndex = 24;
            label5.Text = "Pais";
            // 
            // txtNitTitular
            // 
            txtNitTitular.Anchor = AnchorStyles.Top;
            txtNitTitular.Font = new Font("Century Gothic", 9F);
            txtNitTitular.Location = new Point(164, 211);
            txtNitTitular.Name = "txtNitTitular";
            txtNitTitular.Size = new Size(389, 26);
            txtNitTitular.TabIndex = 3;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 9F);
            label4.Location = new Point(164, 185);
            label4.Name = "label4";
            label4.Size = new Size(30, 20);
            label4.TabIndex = 21;
            label4.Text = "NIT";
            // 
            // txtNombreTitular
            // 
            txtNombreTitular.Anchor = AnchorStyles.Top;
            txtNombreTitular.Font = new Font("Century Gothic", 9F);
            txtNombreTitular.Location = new Point(164, 142);
            txtNombreTitular.Name = "txtNombreTitular";
            txtNombreTitular.Size = new Size(389, 26);
            txtNombreTitular.TabIndex = 1;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 9F);
            label3.Location = new Point(164, 116);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 18;
            label3.Text = "Nombre";
            // 
            // iconPictureBox4
            // 
            iconPictureBox4.Anchor = AnchorStyles.Top;
            iconPictureBox4.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox4.ForeColor = SystemColors.ControlText;
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.HospitalUser;
            iconPictureBox4.IconColor = SystemColors.ControlText;
            iconPictureBox4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox4.IconSize = 28;
            iconPictureBox4.Location = new Point(493, 34);
            iconPictureBox4.Name = "iconPictureBox4";
            iconPictureBox4.Size = new Size(40, 28);
            iconPictureBox4.TabIndex = 93;
            iconPictureBox4.TabStop = false;
            // 
            // lblAccion
            // 
            lblAccion.Anchor = AnchorStyles.Top;
            lblAccion.AutoSize = true;
            lblAccion.BackColor = Color.FromArgb(175, 192, 218);
            lblAccion.Font = new Font("Century Gothic", 12F);
            lblAccion.Location = new Point(539, 34);
            lblAccion.Name = "lblAccion";
            lblAccion.Size = new Size(109, 23);
            lblAccion.TabIndex = 91;
            lblAccion.Text = "AGREGAR";
            // 
            // roundedButton2
            // 
            roundedButton2.Anchor = AnchorStyles.Top;
            roundedButton2.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderRadius = 50;
            roundedButton2.BorderSize = 0;
            roundedButton2.Enabled = false;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(465, 22);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(242, 49);
            roundedButton2.TabIndex = 92;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = Color.Transparent;
            iconPictureBox3.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox3.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 49;
            iconPictureBox3.Location = new Point(407, 22);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(55, 49);
            iconPictureBox3.TabIndex = 90;
            iconPictureBox3.TabStop = false;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.Anchor = AnchorStyles.Top;
            iconPictureBox1.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.HospitalUser;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 28;
            iconPictureBox1.Location = new Point(205, 34);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(40, 28);
            iconPictureBox1.TabIndex = 89;
            iconPictureBox1.TabStop = false;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top;
            label10.AutoSize = true;
            label10.BackColor = Color.FromArgb(175, 192, 218);
            label10.Font = new Font("Century Gothic", 12F);
            label10.Location = new Point(251, 34);
            label10.Name = "label10";
            label10.Size = new Size(101, 23);
            label10.TabIndex = 87;
            label10.Text = "TITULARES";
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top;
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderRadius = 50;
            roundedButton1.BorderSize = 0;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(164, 22);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(234, 49);
            roundedButton1.TabIndex = 88;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // FrmAdministrarTitulares
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAdministrarTitulares";
            Text = "FrmAdministrarTitulares";
            Load += FrmAdministrarTitulares_Load;
            tabControl1.ResumeLayout(false);
            tabListTitulares.ResumeLayout(false);
            tabListTitulares.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgTitulares).EndInit();
            tabTitularDetail.ResumeLayout(false);
            tabTitularDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabListTitulares;
        private DataGridView dtgTitulares;
        private TabPage tabTitularDetail;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private FontAwesome.Sharp.IconButton ibtnAgregar;
        private TextBox txtTelefonoContacto;
        private Label label8;
        private FontAwesome.Sharp.IconButton btnCancelarTit;
        private FontAwesome.Sharp.IconButton btnGuardarTit;
        private TextBox txtCorreoContacto;
        private Label label7;
        private TextBox txtDireccionTitular;
        private Label label6;
        private Label label5;
        private TextBox txtNitTitular;
        private Label label4;
        private TextBox txtNombreTitular;
        private Label label3;
        private TextBox txtNombreContacto;
        private Label label9;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Label label2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private TextBox textBox1;
        private Label label1;
        private Clases.RoundedButton roundedButton5;
        private Clases.RoundedButton roundedButton3;
        private ComboBox comboBox1;
        private Panel panel1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private Label lblAccion;
        private Clases.RoundedButton roundedButton2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label label10;
        private Clases.RoundedButton roundedButton1;
    }
}