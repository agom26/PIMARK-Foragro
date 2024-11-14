namespace Presentacion.Personas
{
    partial class FrmAdministrarAgentes
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabPageListado = new TabPage();
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
            dtgAgentes = new DataGridView();
            tabPageAgenteDetail = new TabPage();
            btnCancelar = new Clases.RoundedButton();
            btnGuardarTitular = new Clases.RoundedButton();
            comboBox1 = new ComboBox();
            txtNombreContacto = new TextBox();
            label9 = new Label();
            txtTelefonoContacto = new TextBox();
            label8 = new Label();
            txtCorreoContacto = new TextBox();
            label7 = new Label();
            txtDireccionAgente = new TextBox();
            label6 = new Label();
            label5 = new Label();
            txtNitAgente = new TextBox();
            label4 = new Label();
            txtNombreAgente = new TextBox();
            label3 = new Label();
            roundedButton4 = new Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabPageListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).BeginInit();
            tabPageAgenteDetail.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageListado);
            tabControl1.Controls.Add(tabPageAgenteDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 12F);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 827);
            tabControl1.TabIndex = 1;
            // 
            // tabPageListado
            // 
            tabPageListado.Controls.Add(iconPictureBox2);
            tabPageListado.Controls.Add(label2);
            tabPageListado.Controls.Add(iconButton1);
            tabPageListado.Controls.Add(textBox1);
            tabPageListado.Controls.Add(label1);
            tabPageListado.Controls.Add(roundedButton5);
            tabPageListado.Controls.Add(roundedButton3);
            tabPageListado.Controls.Add(ibtnEditar);
            tabPageListado.Controls.Add(ibtnAgregar);
            tabPageListado.Controls.Add(panel1);
            tabPageListado.Location = new Point(4, 32);
            tabPageListado.Name = "tabPageListado";
            tabPageListado.Padding = new Padding(3);
            tabPageListado.Size = new Size(1161, 791);
            tabPageListado.TabIndex = 0;
            tabPageListado.UseVisualStyleBackColor = true;
            tabPageListado.Click += tabPageListado_Click;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.Anchor = AnchorStyles.Top;
            iconPictureBox2.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox2.ForeColor = SystemColors.ControlText;
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.UserTie;
            iconPictureBox2.IconColor = SystemColors.ControlText;
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 40;
            iconPictureBox2.Location = new Point(419, 18);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(40, 40);
            iconPictureBox2.TabIndex = 54;
            iconPictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(175, 192, 218);
            label2.Font = new Font("Century Gothic", 15F);
            label2.Location = new Point(465, 18);
            label2.Name = "label2";
            label2.Size = new Size(122, 31);
            label2.TabIndex = 48;
            label2.Text = "AGENTES";
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
            iconButton1.TabIndex = 51;
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
            textBox1.TabIndex = 50;
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
            label1.TabIndex = 49;
            label1.Text = "Buscar por nombre o pais";
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.Top;
            roundedButton5.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderRadius = 60;
            roundedButton5.BorderSize = 0;
            roundedButton5.Enabled = false;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(226, 6);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(556, 61);
            roundedButton5.TabIndex = 53;
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
            roundedButton3.TabIndex = 52;
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
            ibtnEditar.Location = new Point(996, 263);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(157, 37);
            ibtnEditar.TabIndex = 17;
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
            ibtnAgregar.Location = new Point(996, 205);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(157, 37);
            ibtnAgregar.TabIndex = 16;
            ibtnAgregar.Text = "AGREGAR";
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnAgregar_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgAgentes);
            panel1.Location = new Point(38, 205);
            panel1.Name = "panel1";
            panel1.Size = new Size(952, 539);
            panel1.TabIndex = 55;
            // 
            // dtgAgentes
            // 
            dtgAgentes.AllowUserToAddRows = false;
            dtgAgentes.AllowUserToDeleteRows = false;
            dtgAgentes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgAgentes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dtgAgentes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtgAgentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgAgentes.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgAgentes.BorderStyle = BorderStyle.None;
            dtgAgentes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgAgentes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgAgentes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgAgentes.ColumnHeadersHeight = 40;
            dtgAgentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dtgAgentes.DefaultCellStyle = dataGridViewCellStyle3;
            dtgAgentes.EnableHeadersVisualStyles = false;
            dtgAgentes.GridColor = Color.LightGray;
            dtgAgentes.Location = new Point(20, 21);
            dtgAgentes.Name = "dtgAgentes";
            dtgAgentes.ReadOnly = true;
            dtgAgentes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtgAgentes.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtgAgentes.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgAgentes.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dtgAgentes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgAgentes.Size = new Size(904, 495);
            dtgAgentes.TabIndex = 14;
            dtgAgentes.CellClick += dtgAgentes_CellClick;
            dtgAgentes.CellContentClick += dtgAgentes_CellContentClick;
            // 
            // tabPageAgenteDetail
            // 
            tabPageAgenteDetail.Controls.Add(btnCancelar);
            tabPageAgenteDetail.Controls.Add(btnGuardarTitular);
            tabPageAgenteDetail.Controls.Add(comboBox1);
            tabPageAgenteDetail.Controls.Add(txtNombreContacto);
            tabPageAgenteDetail.Controls.Add(label9);
            tabPageAgenteDetail.Controls.Add(txtTelefonoContacto);
            tabPageAgenteDetail.Controls.Add(label8);
            tabPageAgenteDetail.Controls.Add(txtCorreoContacto);
            tabPageAgenteDetail.Controls.Add(label7);
            tabPageAgenteDetail.Controls.Add(txtDireccionAgente);
            tabPageAgenteDetail.Controls.Add(label6);
            tabPageAgenteDetail.Controls.Add(label5);
            tabPageAgenteDetail.Controls.Add(txtNitAgente);
            tabPageAgenteDetail.Controls.Add(label4);
            tabPageAgenteDetail.Controls.Add(txtNombreAgente);
            tabPageAgenteDetail.Controls.Add(label3);
            tabPageAgenteDetail.Controls.Add(roundedButton4);
            tabPageAgenteDetail.Location = new Point(4, 32);
            tabPageAgenteDetail.Name = "tabPageAgenteDetail";
            tabPageAgenteDetail.Padding = new Padding(3);
            tabPageAgenteDetail.Size = new Size(1161, 791);
            tabPageAgenteDetail.TabIndex = 1;
            tabPageAgenteDetail.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top;
            btnCancelar.BackColor = Color.Gainsboro;
            btnCancelar.BackgroundColor = Color.Gainsboro;
            btnCancelar.BorderColor = Color.Empty;
            btnCancelar.BorderRadius = 50;
            btnCancelar.BorderSize = 0;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Century Gothic", 10F);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(579, 445);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(160, 50);
            btnCancelar.TabIndex = 143;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardarTitular
            // 
            btnGuardarTitular.Anchor = AnchorStyles.Top;
            btnGuardarTitular.BackColor = Color.FromArgb(1, 87, 155);
            btnGuardarTitular.BackgroundColor = Color.FromArgb(1, 87, 155);
            btnGuardarTitular.BorderColor = Color.FromArgb(1, 87, 155);
            btnGuardarTitular.BorderRadius = 50;
            btnGuardarTitular.BorderSize = 0;
            btnGuardarTitular.FlatAppearance.BorderSize = 0;
            btnGuardarTitular.FlatStyle = FlatStyle.Flat;
            btnGuardarTitular.Font = new Font("Century Gothic", 10F);
            btnGuardarTitular.ForeColor = Color.White;
            btnGuardarTitular.Location = new Point(371, 445);
            btnGuardarTitular.Name = "btnGuardarTitular";
            btnGuardarTitular.Size = new Size(160, 50);
            btnGuardarTitular.TabIndex = 142;
            btnGuardarTitular.Text = "GUARDAR";
            btnGuardarTitular.TextColor = Color.White;
            btnGuardarTitular.UseVisualStyleBackColor = false;
            btnGuardarTitular.Click += btnActualizar_Click;
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
            comboBox1.Location = new Point(579, 234);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(389, 28);
            comboBox1.TabIndex = 87;
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Anchor = AnchorStyles.Top;
            txtNombreContacto.Font = new Font("Century Gothic", 9F);
            txtNombreContacto.Location = new Point(142, 390);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(389, 26);
            txtNombreContacto.TabIndex = 7;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(222, 227, 234);
            label9.Font = new Font("Century Gothic", 9F);
            label9.Location = new Point(142, 364);
            label9.Name = "label9";
            label9.Size = new Size(80, 20);
            label9.TabIndex = 47;
            label9.Text = "Contacto";
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Anchor = AnchorStyles.Top;
            txtTelefonoContacto.Font = new Font("Century Gothic", 9F);
            txtTelefonoContacto.Location = new Point(579, 315);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(389, 26);
            txtTelefonoContacto.TabIndex = 6;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(222, 227, 234);
            label8.Font = new Font("Century Gothic", 9F);
            label8.Location = new Point(579, 289);
            label8.Name = "label8";
            label8.Size = new Size(71, 20);
            label8.TabIndex = 45;
            label8.Text = "Teléfono";
            // 
            // txtCorreoContacto
            // 
            txtCorreoContacto.Anchor = AnchorStyles.Top;
            txtCorreoContacto.Font = new Font("Century Gothic", 9F);
            txtCorreoContacto.Location = new Point(142, 315);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(389, 26);
            txtCorreoContacto.TabIndex = 5;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(222, 227, 234);
            label7.Font = new Font("Century Gothic", 9F);
            label7.Location = new Point(142, 289);
            label7.Name = "label7";
            label7.Size = new Size(61, 20);
            label7.TabIndex = 44;
            label7.Text = "Correo";
            // 
            // txtDireccionAgente
            // 
            txtDireccionAgente.Anchor = AnchorStyles.Top;
            txtDireccionAgente.Font = new Font("Century Gothic", 9F);
            txtDireccionAgente.Location = new Point(579, 168);
            txtDireccionAgente.Name = "txtDireccionAgente";
            txtDireccionAgente.Size = new Size(389, 26);
            txtDireccionAgente.TabIndex = 2;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(222, 227, 234);
            label6.Font = new Font("Century Gothic", 9F);
            label6.Location = new Point(579, 142);
            label6.Name = "label6";
            label6.Size = new Size(80, 20);
            label6.TabIndex = 42;
            label6.Text = "Dirección";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(222, 227, 234);
            label5.Font = new Font("Century Gothic", 9F);
            label5.Location = new Point(579, 211);
            label5.Name = "label5";
            label5.Size = new Size(37, 20);
            label5.TabIndex = 40;
            label5.Text = "Pais";
            // 
            // txtNitAgente
            // 
            txtNitAgente.Anchor = AnchorStyles.Top;
            txtNitAgente.Font = new Font("Century Gothic", 9F);
            txtNitAgente.Location = new Point(142, 237);
            txtNitAgente.Name = "txtNitAgente";
            txtNitAgente.Size = new Size(389, 26);
            txtNitAgente.TabIndex = 3;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(222, 227, 234);
            label4.Font = new Font("Century Gothic", 9F);
            label4.Location = new Point(142, 211);
            label4.Name = "label4";
            label4.Size = new Size(30, 20);
            label4.TabIndex = 37;
            label4.Text = "NIT";
            // 
            // txtNombreAgente
            // 
            txtNombreAgente.Anchor = AnchorStyles.Top;
            txtNombreAgente.Font = new Font("Century Gothic", 9F);
            txtNombreAgente.Location = new Point(142, 168);
            txtNombreAgente.Name = "txtNombreAgente";
            txtNombreAgente.Size = new Size(389, 26);
            txtNombreAgente.TabIndex = 1;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(222, 227, 234);
            label3.Font = new Font("Century Gothic", 9F);
            label3.Location = new Point(142, 142);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 34;
            label3.Text = "Nombre";
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BorderColor = Color.Empty;
            roundedButton4.BorderRadius = 40;
            roundedButton4.BorderSize = 0;
            roundedButton4.Enabled = false;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(85, 123);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(942, 407);
            roundedButton4.TabIndex = 144;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // FrmAdministrarAgentes
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAdministrarAgentes";
            Text = "FrmAdministrarAgentes";
            Load += FrmAdministrarAgentes_Load;
            tabControl1.ResumeLayout(false);
            tabPageListado.ResumeLayout(false);
            tabPageListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).EndInit();
            tabPageAgenteDetail.ResumeLayout(false);
            tabPageAgenteDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabPageListado;
        private TabPage tabPageAgenteDetail;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private FontAwesome.Sharp.IconButton ibtnAgregar;
        private DataGridView dtgAgentes;
        private TextBox txtNombreContacto;
        private Label label9;
        private TextBox txtTelefonoContacto;
        private Label label8;
        private FontAwesome.Sharp.IconButton btnCancelarTit;
        private FontAwesome.Sharp.IconButton btnGuardarTit;
        private TextBox txtCorreoContacto;
        private Label label7;
        private TextBox txtDireccionAgente;
        private Label label6;
        private Label label5;
        private TextBox txtNitAgente;
        private Label label4;
        private TextBox txtNombreAgente;
        private Label label3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Label label2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private TextBox textBox1;
        private Label label1;
        private Clases.RoundedButton roundedButton5;
        private Clases.RoundedButton roundedButton3;
        private Panel panel1;
        private ComboBox comboBox1;
        private Clases.RoundedButton btnGuardarTitular;
        private Clases.RoundedButton btnCancelar;
        private Clases.RoundedButton roundedButton4;
    }
}