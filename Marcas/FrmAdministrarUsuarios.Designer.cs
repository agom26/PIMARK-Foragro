namespace Presentacion
{
    partial class FrmAdministrarUsuarios
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
            tabPage1 = new TabPage();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            label2 = new Label();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            textBox1 = new TextBox();
            label9 = new Label();
            roundedButton5 = new Clases.RoundedButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            dtgUsuarios = new DataGridView();
            roundedButton3 = new Clases.RoundedButton();
            tabPageUserDetail = new TabPage();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            lblAccion = new Label();
            roundedButton2 = new Clases.RoundedButton();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label1 = new Label();
            roundedButton1 = new Clases.RoundedButton();
            txtConfirmarCont = new TextBox();
            label8 = new Label();
            chckbIsAdmin = new CheckBox();
            iconButton6 = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            txtCont = new TextBox();
            label7 = new Label();
            txtCorreo = new TextBox();
            label6 = new Label();
            txtApellidos = new TextBox();
            label5 = new Label();
            txtNombres = new TextBox();
            label4 = new Label();
            txtUsername = new TextBox();
            label3 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).BeginInit();
            tabPageUserDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPageUserDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 10F);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 827);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(iconPictureBox2);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(iconButton1);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(roundedButton5);
            tabPage1.Controls.Add(iconButton3);
            tabPage1.Controls.Add(iconButton2);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(roundedButton3);
            tabPage1.Font = new Font("Century Gothic", 12F);
            tabPage1.Location = new Point(4, 30);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1161, 793);
            tabPage1.TabIndex = 0;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.Anchor = AnchorStyles.Top;
            iconPictureBox2.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox2.ForeColor = SystemColors.ControlText;
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconPictureBox2.IconColor = SystemColors.ControlText;
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 40;
            iconPictureBox2.Location = new Point(405, 19);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(40, 40);
            iconPictureBox2.TabIndex = 47;
            iconPictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(175, 192, 218);
            label2.Font = new Font("Century Gothic", 15F);
            label2.Location = new Point(451, 19);
            label2.Name = "label2";
            label2.Size = new Size(132, 31);
            label2.TabIndex = 41;
            label2.Text = "USUARIOS";
            label2.Click += label2_Click;
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
            iconButton1.TabIndex = 44;
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
            textBox1.TabIndex = 43;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(236, 236, 238);
            label9.Font = new Font("Century Gothic", 9F);
            label9.Location = new Point(250, 107);
            label9.Name = "label9";
            label9.Size = new Size(218, 20);
            label9.TabIndex = 42;
            label9.Text = "Buscar por nombre o usuario";
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
            roundedButton5.Location = new Point(226, 7);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(556, 61);
            roundedButton5.TabIndex = 46;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // iconButton3
            // 
            iconButton3.Anchor = AnchorStyles.Top;
            iconButton3.AutoSize = true;
            iconButton3.BackColor = Color.FromArgb(96, 149, 241);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Century Gothic", 10F);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Pen;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 25;
            iconButton3.Location = new Point(984, 255);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(174, 37);
            iconButton3.TabIndex = 6;
            iconButton3.Text = "EDITAR/ VER";
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Top;
            iconButton2.AutoSize = true;
            iconButton2.BackColor = Color.FromArgb(50, 164, 115);
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 10F);
            iconButton2.ForeColor = Color.White;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            iconButton2.IconColor = Color.White;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.Location = new Point(984, 200);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(174, 37);
            iconButton2.TabIndex = 5;
            iconButton2.Text = "AGREGAR";
            iconButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgUsuarios);
            panel1.Location = new Point(27, 200);
            panel1.Name = "panel1";
            panel1.Size = new Size(952, 539);
            panel1.TabIndex = 48;
            // 
            // dtgUsuarios
            // 
            dtgUsuarios.AllowUserToAddRows = false;
            dtgUsuarios.AllowUserToDeleteRows = false;
            dtgUsuarios.AllowUserToResizeRows = false;
            dtgUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtgUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgUsuarios.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgUsuarios.BorderStyle = BorderStyle.None;
            dtgUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgUsuarios.ColumnHeadersHeight = 40;
            dtgUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtgUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            dtgUsuarios.EnableHeadersVisualStyles = false;
            dtgUsuarios.GridColor = Color.LightGray;
            dtgUsuarios.Location = new Point(17, 20);
            dtgUsuarios.Name = "dtgUsuarios";
            dtgUsuarios.ReadOnly = true;
            dtgUsuarios.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgUsuarios.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9F);
            dtgUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dtgUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgUsuarios.Size = new Size(916, 494);
            dtgUsuarios.TabIndex = 4;
            dtgUsuarios.CellClick += dtgUsuarios_CellClick;
            dtgUsuarios.CellContentClick += dtgUsuarios_CellContentClick;
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
            roundedButton3.TabIndex = 45;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // tabPageUserDetail
            // 
            tabPageUserDetail.Controls.Add(iconPictureBox4);
            tabPageUserDetail.Controls.Add(lblAccion);
            tabPageUserDetail.Controls.Add(roundedButton2);
            tabPageUserDetail.Controls.Add(iconPictureBox3);
            tabPageUserDetail.Controls.Add(iconPictureBox1);
            tabPageUserDetail.Controls.Add(label1);
            tabPageUserDetail.Controls.Add(roundedButton1);
            tabPageUserDetail.Controls.Add(txtConfirmarCont);
            tabPageUserDetail.Controls.Add(label8);
            tabPageUserDetail.Controls.Add(chckbIsAdmin);
            tabPageUserDetail.Controls.Add(iconButton6);
            tabPageUserDetail.Controls.Add(btnGuardar);
            tabPageUserDetail.Controls.Add(txtCont);
            tabPageUserDetail.Controls.Add(label7);
            tabPageUserDetail.Controls.Add(txtCorreo);
            tabPageUserDetail.Controls.Add(label6);
            tabPageUserDetail.Controls.Add(txtApellidos);
            tabPageUserDetail.Controls.Add(label5);
            tabPageUserDetail.Controls.Add(txtNombres);
            tabPageUserDetail.Controls.Add(label4);
            tabPageUserDetail.Controls.Add(txtUsername);
            tabPageUserDetail.Controls.Add(label3);
            tabPageUserDetail.Font = new Font("Century Gothic", 12F);
            tabPageUserDetail.Location = new Point(4, 30);
            tabPageUserDetail.Name = "tabPageUserDetail";
            tabPageUserDetail.Padding = new Padding(3);
            tabPageUserDetail.Size = new Size(1161, 793);
            tabPageUserDetail.TabIndex = 1;
            tabPageUserDetail.UseVisualStyleBackColor = true;
            // 
            // iconPictureBox4
            // 
            iconPictureBox4.Anchor = AnchorStyles.Top;
            iconPictureBox4.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox4.ForeColor = SystemColors.ControlText;
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconPictureBox4.IconColor = SystemColors.ControlText;
            iconPictureBox4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox4.IconSize = 28;
            iconPictureBox4.Location = new Point(482, 18);
            iconPictureBox4.Name = "iconPictureBox4";
            iconPictureBox4.Size = new Size(40, 28);
            iconPictureBox4.TabIndex = 54;
            iconPictureBox4.TabStop = false;
            // 
            // lblAccion
            // 
            lblAccion.Anchor = AnchorStyles.Top;
            lblAccion.AutoSize = true;
            lblAccion.BackColor = Color.FromArgb(175, 192, 218);
            lblAccion.Font = new Font("Century Gothic", 12F);
            lblAccion.Location = new Point(528, 18);
            lblAccion.Name = "lblAccion";
            lblAccion.Size = new Size(109, 23);
            lblAccion.TabIndex = 52;
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
            roundedButton2.Location = new Point(454, 6);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(242, 49);
            roundedButton2.TabIndex = 53;
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
            iconPictureBox3.Location = new Point(396, 6);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(55, 49);
            iconPictureBox3.TabIndex = 51;
            iconPictureBox3.TabStop = false;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.Anchor = AnchorStyles.Top;
            iconPictureBox1.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 28;
            iconPictureBox1.Location = new Point(194, 18);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(40, 28);
            iconPictureBox1.TabIndex = 50;
            iconPictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(175, 192, 218);
            label1.Font = new Font("Century Gothic", 12F);
            label1.Location = new Point(240, 18);
            label1.Name = "label1";
            label1.Size = new Size(105, 23);
            label1.TabIndex = 48;
            label1.Text = "USUARIOS";
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
            roundedButton1.Location = new Point(153, 6);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(234, 49);
            roundedButton1.TabIndex = 49;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // txtConfirmarCont
            // 
            txtConfirmarCont.Anchor = AnchorStyles.Top;
            txtConfirmarCont.Font = new Font("Century Gothic", 10.2F);
            txtConfirmarCont.Location = new Point(600, 323);
            txtConfirmarCont.Name = "txtConfirmarCont";
            txtConfirmarCont.Size = new Size(389, 28);
            txtConfirmarCont.TabIndex = 6;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 10.2F);
            label8.Location = new Point(600, 297);
            label8.Name = "label8";
            label8.Size = new Size(191, 21);
            label8.TabIndex = 15;
            label8.Text = "Confirmar contraseña";
            // 
            // chckbIsAdmin
            // 
            chckbIsAdmin.Anchor = AnchorStyles.Top;
            chckbIsAdmin.AutoSize = true;
            chckbIsAdmin.Font = new Font("Century Gothic", 10.2F);
            chckbIsAdmin.Location = new Point(153, 378);
            chckbIsAdmin.Name = "chckbIsAdmin";
            chckbIsAdmin.Size = new Size(248, 25);
            chckbIsAdmin.TabIndex = 7;
            chckbIsAdmin.Text = "Permisos de administrador";
            chckbIsAdmin.UseVisualStyleBackColor = true;
            // 
            // iconButton6
            // 
            iconButton6.Anchor = AnchorStyles.Top;
            iconButton6.BackColor = Color.Gainsboro;
            iconButton6.FlatAppearance.BorderSize = 0;
            iconButton6.FlatStyle = FlatStyle.Flat;
            iconButton6.ForeColor = Color.Black;
            iconButton6.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton6.IconColor = Color.Black;
            iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton6.IconSize = 30;
            iconButton6.Location = new Point(600, 432);
            iconButton6.Name = "iconButton6";
            iconButton6.Size = new Size(191, 37);
            iconButton6.TabIndex = 9;
            iconButton6.Text = "CANCELAR";
            iconButton6.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton6.UseVisualStyleBackColor = false;
            iconButton6.Click += iconButton6_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top;
            btnGuardar.BackColor = Color.FromArgb(1, 87, 155);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.White;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 30;
            btnGuardar.Location = new Point(351, 432);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(191, 37);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += iconButton5_Click;
            // 
            // txtCont
            // 
            txtCont.Anchor = AnchorStyles.Top;
            txtCont.Font = new Font("Century Gothic", 10.2F);
            txtCont.Location = new Point(153, 323);
            txtCont.Name = "txtCont";
            txtCont.Size = new Size(389, 28);
            txtCont.TabIndex = 5;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 10.2F);
            label7.Location = new Point(153, 297);
            label7.Name = "label7";
            label7.Size = new Size(109, 21);
            label7.TabIndex = 10;
            label7.Text = "Contraseña";
            // 
            // txtCorreo
            // 
            txtCorreo.Anchor = AnchorStyles.Top;
            txtCorreo.Font = new Font("Century Gothic", 10.2F);
            txtCorreo.Location = new Point(600, 176);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(389, 28);
            txtCorreo.TabIndex = 2;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 10.2F);
            label6.Location = new Point(600, 150);
            label6.Name = "label6";
            label6.Size = new Size(67, 21);
            label6.TabIndex = 8;
            label6.Text = "Correo";
            // 
            // txtApellidos
            // 
            txtApellidos.Anchor = AnchorStyles.Top;
            txtApellidos.Font = new Font("Century Gothic", 10.2F);
            txtApellidos.Location = new Point(600, 245);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(389, 28);
            txtApellidos.TabIndex = 4;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 10.2F);
            label5.Location = new Point(600, 219);
            label5.Name = "label5";
            label5.Size = new Size(85, 21);
            label5.TabIndex = 6;
            label5.Text = "Apellidos";
            // 
            // txtNombres
            // 
            txtNombres.Anchor = AnchorStyles.Top;
            txtNombres.Font = new Font("Century Gothic", 10.2F);
            txtNombres.Location = new Point(153, 245);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(389, 28);
            txtNombres.TabIndex = 3;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10.2F);
            label4.Location = new Point(153, 219);
            label4.Name = "label4";
            label4.Size = new Size(84, 21);
            label4.TabIndex = 4;
            label4.Text = "Nombres";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top;
            txtUsername.Font = new Font("Century Gothic", 10.2F);
            txtUsername.Location = new Point(153, 176);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(389, 28);
            txtUsername.TabIndex = 1;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 10.2F);
            label3.Location = new Point(153, 150);
            label3.Name = "label3";
            label3.Size = new Size(70, 21);
            label3.TabIndex = 2;
            label3.Text = "Usuario";
            // 
            // FrmAdministrarUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            ControlBox = false;
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAdministrarUsuarios";
            Text = "FrmAdministrarUsuarios";
            Load += FrmAdministrarUsuarios_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).EndInit();
            tabPageUserDetail.ResumeLayout(false);
            tabPageUserDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPageUserDetail;
        private FontAwesome.Sharp.IconButton iconButton2;
        private DataGridView dtgUsuarios;
        private FontAwesome.Sharp.IconButton iconButton3;
        private TextBox txtCorreo;
        private Label label6;
        private TextBox txtApellidos;
        private Label label5;
        private TextBox txtNombres;
        private Label label4;
        private TextBox txtUsername;
        private Label label3;
        private TextBox txtCont;
        private Label label7;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton iconButton6;
        private CheckBox chckbIsAdmin;
        private TextBox txtConfirmarCont;
        private Label label8;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Label label2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private TextBox textBox1;
        private Label label9;
        private Clases.RoundedButton roundedButton3;
        private Clases.RoundedButton roundedButton5;
        private Panel panel1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label label1;
        private Clases.RoundedButton roundedButton1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private Label lblAccion;
        private Clases.RoundedButton roundedButton2;
    }
}