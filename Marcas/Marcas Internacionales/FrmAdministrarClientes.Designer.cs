namespace Presentacion.Marcas_Internacionales
{
    partial class FrmAdministrarClientes
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
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabClientesList = new TabPage();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            textBox1 = new TextBox();
            label1 = new Label();
            roundedButton3 = new Clases.RoundedButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            dtgClientes = new DataGridView();
            tabClienteDetail = new TabPage();
            btnGuardarU = new FontAwesome.Sharp.IconButton();
            btnCancelarU = new FontAwesome.Sharp.IconButton();
            iconPictureBoxIcono = new FontAwesome.Sharp.IconPictureBox();
            roundedButton2 = new Clases.RoundedButton();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            comboBox1 = new ComboBox();
            txtNombreContacto = new TextBox();
            label9 = new Label();
            txtTelefonoContacto = new TextBox();
            label8 = new Label();
            txtCorreoContacto = new TextBox();
            label7 = new Label();
            txtDireccionCliente = new TextBox();
            label6 = new Label();
            label5 = new Label();
            txtNitCliente = new TextBox();
            label4 = new Label();
            txtNombreCliente = new TextBox();
            label3 = new Label();
            roundedButton4 = new Clases.RoundedButton();
            label28 = new Label();
            roundedButton5 = new Clases.RoundedButton();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            label29 = new Label();
            roundedButton6 = new Clases.RoundedButton();
            label2 = new Label();
            roundedButton1 = new Clases.RoundedButton();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox5 = new FontAwesome.Sharp.IconPictureBox();
            label10 = new Label();
            roundedButton7 = new Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabClientesList.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgClientes).BeginInit();
            tabClienteDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxIcono).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox5).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabClientesList);
            tabControl1.Controls.Add(tabClienteDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 827);
            tabControl1.TabIndex = 2;
            // 
            // tabClientesList
            // 
            tabClientesList.BackColor = Color.White;
            tabClientesList.Controls.Add(label28);
            tabClientesList.Controls.Add(roundedButton5);
            tabClientesList.Controls.Add(iconPictureBox2);
            tabClientesList.Controls.Add(iconPictureBox4);
            tabClientesList.Controls.Add(label29);
            tabClientesList.Controls.Add(roundedButton6);
            tabClientesList.Controls.Add(iconButton2);
            tabClientesList.Controls.Add(iconButton1);
            tabClientesList.Controls.Add(textBox1);
            tabClientesList.Controls.Add(label1);
            tabClientesList.Controls.Add(roundedButton3);
            tabClientesList.Controls.Add(ibtnEditar);
            tabClientesList.Controls.Add(ibtnAgregar);
            tabClientesList.Controls.Add(panel1);
            tabClientesList.Location = new Point(4, 32);
            tabClientesList.Name = "tabClientesList";
            tabClientesList.Padding = new Padding(3);
            tabClientesList.Size = new Size(1161, 791);
            tabClientesList.TabIndex = 0;
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Top;
            iconButton2.BackColor = Color.FromArgb(0, 137, 123);
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 10F);
            iconButton2.ForeColor = Color.White;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Eye;
            iconButton2.IconColor = Color.White;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 30;
            iconButton2.Location = new Point(1011, 198);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(144, 37);
            iconButton2.TabIndex = 57;
            iconButton2.Text = "VER";
            iconButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
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
            iconButton1.Location = new Point(601, 129);
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
            textBox1.Font = new Font("Century Gothic", 10F);
            textBox1.Location = new Point(262, 130);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(301, 28);
            textBox1.TabIndex = 50;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(236, 236, 238);
            label1.Font = new Font("Century Gothic", 9F);
            label1.Location = new Point(262, 107);
            label1.Name = "label1";
            label1.Size = new Size(195, 20);
            label1.TabIndex = 49;
            label1.Text = "Buscar por nombre o pais\r\n";
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.Top;
            roundedButton3.BackColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BackgroundColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderRadius = 80;
            roundedButton3.BorderSize = 0;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(228, 101);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(560, 74);
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
            ibtnEditar.Location = new Point(1009, 310);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(144, 37);
            ibtnEditar.TabIndex = 16;
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
            ibtnAgregar.Location = new Point(1009, 252);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(144, 37);
            ibtnAgregar.TabIndex = 15;
            ibtnAgregar.Text = "AGREGAR";
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnAgregar_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgClientes);
            panel1.Location = new Point(37, 198);
            panel1.Name = "panel1";
            panel1.Size = new Size(952, 539);
            panel1.TabIndex = 56;
            // 
            // dtgClientes
            // 
            dtgClientes.AllowUserToAddRows = false;
            dtgClientes.AllowUserToDeleteRows = false;
            dtgClientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.Font = new Font("Century Gothic", 9F);
            dtgClientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            dtgClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgClientes.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgClientes.BorderStyle = BorderStyle.None;
            dtgClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = SystemColors.Control;
            dataGridViewCellStyle11.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle11.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
            dtgClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            dtgClientes.ColumnHeadersHeight = 40;
            dtgClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgClientes.EnableHeadersVisualStyles = false;
            dtgClientes.GridColor = Color.LightGray;
            dtgClientes.Location = new Point(25, 19);
            dtgClientes.Name = "dtgClientes";
            dtgClientes.ReadOnly = true;
            dtgClientes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgClientes.RowHeadersWidth = 51;
            dataGridViewCellStyle12.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgClientes.RowsDefaultCellStyle = dataGridViewCellStyle12;
            dtgClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgClientes.Size = new Size(898, 501);
            dtgClientes.TabIndex = 13;
            dtgClientes.CellClick += dtgClientes_CellClick;
            dtgClientes.CellContentClick += dtgClientes_CellContentClick;
            // 
            // tabClienteDetail
            // 
            tabClienteDetail.BackColor = Color.White;
            tabClienteDetail.Controls.Add(label2);
            tabClienteDetail.Controls.Add(roundedButton1);
            tabClienteDetail.Controls.Add(iconPictureBox1);
            tabClienteDetail.Controls.Add(iconPictureBox5);
            tabClienteDetail.Controls.Add(label10);
            tabClienteDetail.Controls.Add(roundedButton7);
            tabClienteDetail.Controls.Add(btnGuardarU);
            tabClienteDetail.Controls.Add(btnCancelarU);
            tabClienteDetail.Controls.Add(iconPictureBoxIcono);
            tabClienteDetail.Controls.Add(roundedButton2);
            tabClienteDetail.Controls.Add(iconPictureBox3);
            tabClienteDetail.Controls.Add(comboBox1);
            tabClienteDetail.Controls.Add(txtNombreContacto);
            tabClienteDetail.Controls.Add(label9);
            tabClienteDetail.Controls.Add(txtTelefonoContacto);
            tabClienteDetail.Controls.Add(label8);
            tabClienteDetail.Controls.Add(txtCorreoContacto);
            tabClienteDetail.Controls.Add(label7);
            tabClienteDetail.Controls.Add(txtDireccionCliente);
            tabClienteDetail.Controls.Add(label6);
            tabClienteDetail.Controls.Add(label5);
            tabClienteDetail.Controls.Add(txtNitCliente);
            tabClienteDetail.Controls.Add(label4);
            tabClienteDetail.Controls.Add(txtNombreCliente);
            tabClienteDetail.Controls.Add(label3);
            tabClienteDetail.Controls.Add(roundedButton4);
            tabClienteDetail.Location = new Point(4, 32);
            tabClienteDetail.Name = "tabClienteDetail";
            tabClienteDetail.Padding = new Padding(3);
            tabClienteDetail.Size = new Size(1161, 791);
            tabClienteDetail.TabIndex = 1;
            // 
            // btnGuardarU
            // 
            btnGuardarU.BackColor = Color.FromArgb(1, 87, 155);
            btnGuardarU.FlatAppearance.BorderSize = 0;
            btnGuardarU.FlatStyle = FlatStyle.Flat;
            btnGuardarU.Font = new Font("Century Gothic", 12F);
            btnGuardarU.ForeColor = Color.White;
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnGuardarU.IconColor = Color.White;
            btnGuardarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardarU.IconSize = 30;
            btnGuardarU.Location = new Point(337, 471);
            btnGuardarU.Name = "btnGuardarU";
            btnGuardarU.Size = new Size(191, 34);
            btnGuardarU.TabIndex = 158;
            btnGuardarU.Text = "GUARDAR";
            btnGuardarU.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnGuardarU.UseVisualStyleBackColor = false;
            btnGuardarU.Click += btnGuardarU_Click;
            // 
            // btnCancelarU
            // 
            btnCancelarU.BackColor = Color.White;
            btnCancelarU.FlatAppearance.BorderSize = 0;
            btnCancelarU.FlatStyle = FlatStyle.Flat;
            btnCancelarU.Font = new Font("Century Gothic", 12F);
            btnCancelarU.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarU.IconColor = Color.Black;
            btnCancelarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarU.IconSize = 30;
            btnCancelarU.Location = new Point(586, 471);
            btnCancelarU.Name = "btnCancelarU";
            btnCancelarU.Size = new Size(179, 34);
            btnCancelarU.TabIndex = 157;
            btnCancelarU.Text = "CANCELAR";
            btnCancelarU.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelarU.UseVisualStyleBackColor = false;
            btnCancelarU.Click += btnCancelarU_Click;
            // 
            // iconPictureBoxIcono
            // 
            iconPictureBoxIcono.Anchor = AnchorStyles.Top;
            iconPictureBoxIcono.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBoxIcono.ForeColor = SystemColors.ControlText;
            iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconPictureBoxIcono.IconColor = SystemColors.ControlText;
            iconPictureBoxIcono.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBoxIcono.IconSize = 28;
            iconPictureBoxIcono.Location = new Point(770, 35);
            iconPictureBoxIcono.Name = "iconPictureBoxIcono";
            iconPictureBoxIcono.Size = new Size(40, 28);
            iconPictureBoxIcono.TabIndex = 156;
            iconPictureBoxIcono.TabStop = false;
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
            roundedButton2.Location = new Point(736, 23);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(94, 49);
            roundedButton2.TabIndex = 155;
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
            iconPictureBox3.Location = new Point(678, 23);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(55, 49);
            iconPictureBox3.TabIndex = 154;
            iconPictureBox3.TabStop = false;
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
            comboBox1.Location = new Point(576, 245);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(389, 28);
            comboBox1.TabIndex = 88;
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Font = new Font("Century Gothic", 9F);
            txtNombreContacto.Location = new Point(139, 398);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(389, 26);
            txtNombreContacto.TabIndex = 38;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(222, 227, 234);
            label9.Font = new Font("Century Gothic", 9F);
            label9.Location = new Point(139, 372);
            label9.Name = "label9";
            label9.Size = new Size(80, 20);
            label9.TabIndex = 47;
            label9.Text = "Contacto";
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Font = new Font("Century Gothic", 9F);
            txtTelefonoContacto.Location = new Point(576, 323);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(389, 26);
            txtTelefonoContacto.TabIndex = 37;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(222, 227, 234);
            label8.Font = new Font("Century Gothic", 9F);
            label8.Location = new Point(576, 297);
            label8.Name = "label8";
            label8.Size = new Size(71, 20);
            label8.TabIndex = 46;
            label8.Text = "Teléfono";
            // 
            // txtCorreoContacto
            // 
            txtCorreoContacto.Font = new Font("Century Gothic", 9F);
            txtCorreoContacto.Location = new Point(139, 323);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(389, 26);
            txtCorreoContacto.TabIndex = 36;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(222, 227, 234);
            label7.Font = new Font("Century Gothic", 9F);
            label7.Location = new Point(139, 297);
            label7.Name = "label7";
            label7.Size = new Size(61, 20);
            label7.TabIndex = 45;
            label7.Text = "Correo";
            // 
            // txtDireccionCliente
            // 
            txtDireccionCliente.Font = new Font("Century Gothic", 9F);
            txtDireccionCliente.Location = new Point(576, 176);
            txtDireccionCliente.Name = "txtDireccionCliente";
            txtDireccionCliente.Size = new Size(389, 26);
            txtDireccionCliente.TabIndex = 33;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(222, 227, 234);
            label6.Font = new Font("Century Gothic", 9F);
            label6.Location = new Point(576, 150);
            label6.Name = "label6";
            label6.Size = new Size(80, 20);
            label6.TabIndex = 44;
            label6.Text = "Dirección";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(222, 227, 234);
            label5.Font = new Font("Century Gothic", 9F);
            label5.Location = new Point(576, 219);
            label5.Name = "label5";
            label5.Size = new Size(37, 20);
            label5.TabIndex = 43;
            label5.Text = "Pais";
            // 
            // txtNitCliente
            // 
            txtNitCliente.Font = new Font("Century Gothic", 9F);
            txtNitCliente.Location = new Point(139, 245);
            txtNitCliente.Name = "txtNitCliente";
            txtNitCliente.Size = new Size(389, 26);
            txtNitCliente.TabIndex = 34;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(222, 227, 234);
            label4.Font = new Font("Century Gothic", 9F);
            label4.Location = new Point(139, 219);
            label4.Name = "label4";
            label4.Size = new Size(30, 20);
            label4.TabIndex = 42;
            label4.Text = "NIT";
            // 
            // txtNombreCliente
            // 
            txtNombreCliente.Font = new Font("Century Gothic", 9F);
            txtNombreCliente.Location = new Point(139, 176);
            txtNombreCliente.Name = "txtNombreCliente";
            txtNombreCliente.Size = new Size(389, 26);
            txtNombreCliente.TabIndex = 32;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(222, 227, 234);
            label3.Font = new Font("Century Gothic", 9F);
            label3.Location = new Point(139, 150);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 41;
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
            roundedButton4.Location = new Point(75, 133);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(942, 407);
            roundedButton4.TabIndex = 89;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // label28
            // 
            label28.Anchor = AnchorStyles.Top;
            label28.AutoSize = true;
            label28.BackColor = Color.FromArgb(175, 192, 218);
            label28.Font = new Font("Century Gothic", 12F);
            label28.Location = new Point(517, 27);
            label28.Name = "label28";
            label28.Size = new Size(95, 23);
            label28.TabIndex = 186;
            label28.Text = "CLIENTES";
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.Top;
            roundedButton5.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderRadius = 50;
            roundedButton5.BorderSize = 0;
            roundedButton5.Enabled = false;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(493, 16);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(141, 49);
            roundedButton5.TabIndex = 185;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.BackColor = Color.Transparent;
            iconPictureBox2.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox2.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 49;
            iconPictureBox2.Location = new Point(432, 17);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(55, 49);
            iconPictureBox2.TabIndex = 184;
            iconPictureBox2.TabStop = false;
            // 
            // iconPictureBox4
            // 
            iconPictureBox4.Anchor = AnchorStyles.Top;
            iconPictureBox4.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox4.ForeColor = SystemColors.ControlText;
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.Globe;
            iconPictureBox4.IconColor = SystemColors.ControlText;
            iconPictureBox4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox4.IconSize = 28;
            iconPictureBox4.Location = new Point(78, 29);
            iconPictureBox4.Name = "iconPictureBox4";
            iconPictureBox4.Size = new Size(40, 28);
            iconPictureBox4.TabIndex = 183;
            iconPictureBox4.TabStop = false;
            // 
            // label29
            // 
            label29.Anchor = AnchorStyles.Top;
            label29.AutoSize = true;
            label29.BackColor = Color.FromArgb(175, 192, 218);
            label29.Font = new Font("Century Gothic", 12F);
            label29.Location = new Point(124, 29);
            label29.Name = "label29";
            label29.Size = new Size(280, 23);
            label29.TabIndex = 181;
            label29.Text = "MARCAS INTERNACIONALES";
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
            roundedButton6.Location = new Point(37, 17);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(389, 49);
            roundedButton6.TabIndex = 182;
            roundedButton6.TextColor = Color.White;
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(175, 192, 218);
            label2.Font = new Font("Century Gothic", 12F);
            label2.Location = new Point(555, 34);
            label2.Name = "label2";
            label2.Size = new Size(95, 23);
            label2.TabIndex = 192;
            label2.Text = "CLIENTES";
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
            roundedButton1.Location = new Point(531, 23);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(141, 49);
            roundedButton1.TabIndex = 191;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.Transparent;
            iconPictureBox1.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox1.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 49;
            iconPictureBox1.Location = new Point(470, 24);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(55, 49);
            iconPictureBox1.TabIndex = 190;
            iconPictureBox1.TabStop = false;
            // 
            // iconPictureBox5
            // 
            iconPictureBox5.Anchor = AnchorStyles.Top;
            iconPictureBox5.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox5.ForeColor = SystemColors.ControlText;
            iconPictureBox5.IconChar = FontAwesome.Sharp.IconChar.Globe;
            iconPictureBox5.IconColor = SystemColors.ControlText;
            iconPictureBox5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox5.IconSize = 28;
            iconPictureBox5.Location = new Point(116, 36);
            iconPictureBox5.Name = "iconPictureBox5";
            iconPictureBox5.Size = new Size(40, 28);
            iconPictureBox5.TabIndex = 189;
            iconPictureBox5.TabStop = false;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top;
            label10.AutoSize = true;
            label10.BackColor = Color.FromArgb(175, 192, 218);
            label10.Font = new Font("Century Gothic", 12F);
            label10.Location = new Point(162, 36);
            label10.Name = "label10";
            label10.Size = new Size(280, 23);
            label10.TabIndex = 187;
            label10.Text = "MARCAS INTERNACIONALES";
            // 
            // roundedButton7
            // 
            roundedButton7.Anchor = AnchorStyles.Top;
            roundedButton7.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderRadius = 50;
            roundedButton7.BorderSize = 0;
            roundedButton7.Enabled = false;
            roundedButton7.FlatAppearance.BorderSize = 0;
            roundedButton7.FlatStyle = FlatStyle.Flat;
            roundedButton7.ForeColor = Color.White;
            roundedButton7.Location = new Point(75, 24);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(389, 49);
            roundedButton7.TabIndex = 188;
            roundedButton7.TextColor = Color.White;
            roundedButton7.UseVisualStyleBackColor = false;
            // 
            // FrmAdministrarClientes
            // 
            AutoScaleDimensions = new SizeF(12F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 12F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmAdministrarClientes";
            Text = "FrmAdministrarClientes";
            Load += FrmAdministrarClientes_Load;
            tabControl1.ResumeLayout(false);
            tabClientesList.ResumeLayout(false);
            tabClientesList.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgClientes).EndInit();
            tabClienteDetail.ResumeLayout(false);
            tabClienteDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxIcono).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox5).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabClientesList;
        private TabPage tabClienteDetail;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private FontAwesome.Sharp.IconButton ibtnAgregar;
        private DataGridView dtgClientes;
        private TextBox txtNombreContacto;
        private Label label9;
        private TextBox txtTelefonoContacto;
        private Label label8;
        private TextBox txtCorreoContacto;
        private Label label7;
        private TextBox txtDireccionCliente;
        private Label label6;
        private Label label5;
        private TextBox txtNitCliente;
        private Label label4;
        private TextBox txtNombreCliente;
        private Label label3;
        private FontAwesome.Sharp.IconButton iconButton1;
        private TextBox textBox1;
        private Label label1;
        private Clases.RoundedButton roundedButton3;
        private Panel panel1;
        private ComboBox comboBox1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private Clases.RoundedButton roundedButton4;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxIcono;
        private Clases.RoundedButton roundedButton2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private FontAwesome.Sharp.IconButton btnGuardarU;
        private FontAwesome.Sharp.IconButton btnCancelarU;
        private Label label28;
        private Clases.RoundedButton roundedButton5;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private Label label29;
        private Clases.RoundedButton roundedButton6;
        private Label label2;
        private Clases.RoundedButton roundedButton1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox5;
        private Label label10;
        private Clases.RoundedButton roundedButton7;
    }
}