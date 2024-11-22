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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnEliminar = new FontAwesome.Sharp.IconButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            dtgUsuarios = new DataGridView();
            panel2 = new Panel();
            lblBuscar = new Label();
            lblTitulo = new Label();
            txtBuscar = new TextBox();
            iconoTitulo = new FontAwesome.Sharp.IconPictureBox();
            btnBuscar = new FontAwesome.Sharp.IconButton();
            btnTitulo = new Clases.RoundedButton();
            btngrisBuscar = new Clases.RoundedButton();
            tabPageUserDetail = new TabPage();
            panel5 = new Panel();
            panel3 = new Panel();
            label3 = new Label();
            txtUsername = new TextBox();
            label4 = new Label();
            txtNombres = new TextBox();
            label5 = new Label();
            txtApellidos = new TextBox();
            btnGuardarU = new FontAwesome.Sharp.IconButton();
            label6 = new Label();
            btnCancelarU = new FontAwesome.Sharp.IconButton();
            txtCorreo = new TextBox();
            txtConfirmarCont = new TextBox();
            label7 = new Label();
            label8 = new Label();
            txtCont = new TextBox();
            chckbIsAdmin = new CheckBox();
            roundedButton4 = new Clases.RoundedButton();
            label1 = new Label();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBoxIcono = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton2 = new Clases.RoundedButton();
            roundedButton1 = new Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconoTitulo).BeginInit();
            tabPageUserDetail.SuspendLayout();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxIcono).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPageUserDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 10F);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(725, 620);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnEliminar);
            tabPage1.Controls.Add(iconButton3);
            tabPage1.Controls.Add(iconButton2);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(panel2);
            tabPage1.Font = new Font("Century Gothic", 12F);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(717, 590);
            tabPage1.TabIndex = 0;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            tabPage1.Resize += tabPage1_Resize;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminar.AutoSize = true;
            btnEliminar.BackColor = Color.FromArgb(229, 115, 115);
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Century Gothic", 10F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.IconChar = FontAwesome.Sharp.IconChar.Trash;
            btnEliminar.IconColor = Color.White;
            btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEliminar.IconSize = 25;
            btnEliminar.Location = new Point(559, 250);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(152, 37);
            btnEliminar.TabIndex = 49;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += iconButton4_Click_1;
            // 
            // iconButton3
            // 
            iconButton3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
            iconButton3.Location = new Point(559, 200);
            iconButton3.Margin = new Padding(3, 2, 3, 2);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(152, 37);
            iconButton3.TabIndex = 6;
            iconButton3.Text = "EDITAR/ VER";
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
            iconButton2.Location = new Point(559, 150);
            iconButton2.Margin = new Padding(3, 2, 3, 2);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(152, 37);
            iconButton2.TabIndex = 5;
            iconButton2.Text = "AGREGAR";
            iconButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgUsuarios);
            panel1.Location = new Point(10, 150);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(532, 404);
            panel1.TabIndex = 48;
            // 
            // dtgUsuarios
            // 
            dtgUsuarios.AllowUserToAddRows = false;
            dtgUsuarios.AllowUserToDeleteRows = false;
            dtgUsuarios.AllowUserToResizeRows = false;
            dtgUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgUsuarios.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgUsuarios.BorderStyle = BorderStyle.None;
            dtgUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgUsuarios.ColumnHeadersHeight = 40;
            dtgUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgUsuarios.EnableHeadersVisualStyles = false;
            dtgUsuarios.GridColor = Color.LightGray;
            dtgUsuarios.Location = new Point(17, 18);
            dtgUsuarios.Margin = new Padding(3, 2, 3, 2);
            dtgUsuarios.Name = "dtgUsuarios";
            dtgUsuarios.ReadOnly = true;
            dtgUsuarios.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgUsuarios.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dtgUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dtgUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgUsuarios.Size = new Size(492, 370);
            dtgUsuarios.TabIndex = 4;
            dtgUsuarios.CellClick += dtgUsuarios_CellClick;
            dtgUsuarios.CellContentClick += dtgUsuarios_CellContentClick;
            dtgUsuarios.CellDoubleClick += dtgUsuarios_CellDoubleClick;
            dtgUsuarios.DoubleClick += dtgUsuarios_DoubleClick;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.AutoSize = true;
            panel2.Controls.Add(lblBuscar);
            panel2.Controls.Add(lblTitulo);
            panel2.Controls.Add(txtBuscar);
            panel2.Controls.Add(iconoTitulo);
            panel2.Controls.Add(btnBuscar);
            panel2.Controls.Add(btnTitulo);
            panel2.Controls.Add(btngrisBuscar);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(714, 145);
            panel2.TabIndex = 50;
            // 
            // lblBuscar
            // 
            lblBuscar.Anchor = AnchorStyles.None;
            lblBuscar.AutoSize = true;
            lblBuscar.BackColor = Color.FromArgb(236, 236, 238);
            lblBuscar.Font = new Font("Century Gothic", 9F);
            lblBuscar.Location = new Point(138, 79);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(175, 17);
            lblBuscar.TabIndex = 42;
            lblBuscar.Text = "Buscar por nombre o usuario";
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.None;
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.FromArgb(175, 192, 218);
            lblTitulo.Font = new Font("Century Gothic", 15F);
            lblTitulo.Location = new Point(306, 13);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(105, 23);
            lblTitulo.TabIndex = 41;
            lblTitulo.Text = "USUARIOS";
            lblTitulo.Click += label2_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.None;
            txtBuscar.Location = new Point(135, 97);
            txtBuscar.Margin = new Padding(3, 2, 3, 2);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(264, 27);
            txtBuscar.TabIndex = 43;
            // 
            // iconoTitulo
            // 
            iconoTitulo.Anchor = AnchorStyles.None;
            iconoTitulo.BackColor = Color.FromArgb(175, 192, 218);
            iconoTitulo.ForeColor = SystemColors.ControlText;
            iconoTitulo.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconoTitulo.IconColor = SystemColors.ControlText;
            iconoTitulo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconoTitulo.IconSize = 30;
            iconoTitulo.Location = new Point(265, 13);
            iconoTitulo.Margin = new Padding(3, 2, 3, 2);
            iconoTitulo.Name = "iconoTitulo";
            iconoTitulo.Size = new Size(35, 30);
            iconoTitulo.TabIndex = 47;
            iconoTitulo.TabStop = false;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.None;
            btnBuscar.BackColor = Color.FromArgb(251, 140, 0);
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Century Gothic", 10F);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnBuscar.IconColor = Color.White;
            btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnBuscar.IconSize = 25;
            btnBuscar.Location = new Point(430, 90);
            btnBuscar.Margin = new Padding(3, 2, 3, 2);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(126, 30);
            btnBuscar.TabIndex = 44;
            btnBuscar.Text = "BUSCAR";
            btnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += iconButton1_Click;
            // 
            // btnTitulo
            // 
            btnTitulo.Anchor = AnchorStyles.None;
            btnTitulo.BackColor = Color.FromArgb(175, 192, 218);
            btnTitulo.BackgroundColor = Color.FromArgb(175, 192, 218);
            btnTitulo.BorderColor = Color.FromArgb(175, 192, 218);
            btnTitulo.BorderRadius = 50;
            btnTitulo.BorderSize = 0;
            btnTitulo.Enabled = false;
            btnTitulo.FlatAppearance.BorderSize = 0;
            btnTitulo.FlatStyle = FlatStyle.Flat;
            btnTitulo.ForeColor = Color.White;
            btnTitulo.Location = new Point(109, 4);
            btnTitulo.Margin = new Padding(3, 2, 3, 2);
            btnTitulo.Name = "btnTitulo";
            btnTitulo.Size = new Size(486, 46);
            btnTitulo.TabIndex = 46;
            btnTitulo.TextColor = Color.White;
            btnTitulo.UseVisualStyleBackColor = false;
            // 
            // btngrisBuscar
            // 
            btngrisBuscar.Anchor = AnchorStyles.None;
            btngrisBuscar.BackColor = Color.FromArgb(236, 236, 238);
            btngrisBuscar.BackgroundColor = Color.FromArgb(236, 236, 238);
            btngrisBuscar.BorderColor = Color.FromArgb(236, 236, 238);
            btngrisBuscar.BorderRadius = 50;
            btngrisBuscar.BorderSize = 0;
            btngrisBuscar.Enabled = false;
            btngrisBuscar.FlatAppearance.BorderSize = 0;
            btngrisBuscar.FlatStyle = FlatStyle.Flat;
            btngrisBuscar.ForeColor = Color.White;
            btngrisBuscar.Location = new Point(109, 75);
            btngrisBuscar.Margin = new Padding(3, 2, 3, 2);
            btngrisBuscar.Name = "btngrisBuscar";
            btngrisBuscar.Size = new Size(486, 56);
            btngrisBuscar.TabIndex = 45;
            btngrisBuscar.TextColor = Color.White;
            btngrisBuscar.UseVisualStyleBackColor = false;
            // 
            // tabPageUserDetail
            // 
            tabPageUserDetail.AutoScroll = true;
            tabPageUserDetail.Controls.Add(panel5);
            tabPageUserDetail.Font = new Font("Century Gothic", 12F);
            tabPageUserDetail.Location = new Point(4, 26);
            tabPageUserDetail.Margin = new Padding(3, 2, 3, 2);
            tabPageUserDetail.Name = "tabPageUserDetail";
            tabPageUserDetail.Padding = new Padding(3, 2, 3, 2);
            tabPageUserDetail.Size = new Size(717, 590);
            tabPageUserDetail.TabIndex = 1;
            tabPageUserDetail.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            panel5.AutoScroll = true;
            panel5.Controls.Add(panel3);
            panel5.Controls.Add(label1);
            panel5.Controls.Add(iconPictureBox1);
            panel5.Controls.Add(iconPictureBoxIcono);
            panel5.Controls.Add(iconPictureBox3);
            panel5.Controls.Add(roundedButton2);
            panel5.Controls.Add(roundedButton1);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(711, 586);
            panel5.TabIndex = 150;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel3.Controls.Add(label3);
            panel3.Controls.Add(txtUsername);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(txtNombres);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(txtApellidos);
            panel3.Controls.Add(btnGuardarU);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(btnCancelarU);
            panel3.Controls.Add(txtCorreo);
            panel3.Controls.Add(txtConfirmarCont);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(txtCont);
            panel3.Controls.Add(chckbIsAdmin);
            panel3.Controls.Add(roundedButton4);
            panel3.Location = new Point(35, 57);
            panel3.Name = "panel3";
            panel3.Size = new Size(659, 358);
            panel3.TabIndex = 186;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(222, 227, 234);
            label3.Font = new Font("Century Gothic", 10.2F);
            label3.Location = new Point(74, 39);
            label3.Name = "label3";
            label3.Size = new Size(58, 19);
            label3.TabIndex = 166;
            label3.Text = "Usuario";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top;
            txtUsername.Font = new Font("Century Gothic", 10.2F);
            txtUsername.Location = new Point(74, 59);
            txtUsername.Margin = new Padding(3, 2, 3, 2);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(205, 24);
            txtUsername.TabIndex = 164;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(222, 227, 234);
            label4.Font = new Font("Century Gothic", 10.2F);
            label4.Location = new Point(74, 91);
            label4.Name = "label4";
            label4.Size = new Size(69, 19);
            label4.TabIndex = 169;
            label4.Text = "Nombres";
            // 
            // txtNombres
            // 
            txtNombres.Anchor = AnchorStyles.Top;
            txtNombres.Font = new Font("Century Gothic", 10.2F);
            txtNombres.Location = new Point(74, 111);
            txtNombres.Margin = new Padding(3, 2, 3, 2);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(205, 24);
            txtNombres.TabIndex = 167;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(222, 227, 234);
            label5.Font = new Font("Century Gothic", 10.2F);
            label5.Location = new Point(363, 91);
            label5.Name = "label5";
            label5.Size = new Size(70, 19);
            label5.TabIndex = 172;
            label5.Text = "Apellidos";
            // 
            // txtApellidos
            // 
            txtApellidos.Anchor = AnchorStyles.Top;
            txtApellidos.Font = new Font("Century Gothic", 10.2F);
            txtApellidos.Location = new Point(363, 111);
            txtApellidos.Margin = new Padding(3, 2, 3, 2);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(205, 24);
            txtApellidos.TabIndex = 168;
            // 
            // btnGuardarU
            // 
            btnGuardarU.Anchor = AnchorStyles.Top;
            btnGuardarU.BackColor = Color.FromArgb(1, 87, 155);
            btnGuardarU.FlatAppearance.BorderSize = 0;
            btnGuardarU.FlatStyle = FlatStyle.Flat;
            btnGuardarU.Font = new Font("Century Gothic", 12F);
            btnGuardarU.ForeColor = Color.White;
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnGuardarU.IconColor = Color.White;
            btnGuardarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardarU.IconSize = 30;
            btnGuardarU.Location = new Point(127, 249);
            btnGuardarU.Margin = new Padding(3, 2, 3, 2);
            btnGuardarU.Name = "btnGuardarU";
            btnGuardarU.Size = new Size(152, 36);
            btnGuardarU.TabIndex = 179;
            btnGuardarU.Text = "GUARDAR";
            btnGuardarU.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnGuardarU.UseVisualStyleBackColor = false;
            btnGuardarU.Click += btnGuardarU_Click_2;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(222, 227, 234);
            label6.Font = new Font("Century Gothic", 10.2F);
            label6.Location = new Point(363, 39);
            label6.Name = "label6";
            label6.Size = new Size(55, 19);
            label6.TabIndex = 174;
            label6.Text = "Correo";
            // 
            // btnCancelarU
            // 
            btnCancelarU.Anchor = AnchorStyles.Top;
            btnCancelarU.BackColor = Color.White;
            btnCancelarU.FlatAppearance.BorderSize = 0;
            btnCancelarU.FlatStyle = FlatStyle.Flat;
            btnCancelarU.Font = new Font("Century Gothic", 12F);
            btnCancelarU.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarU.IconColor = Color.Black;
            btnCancelarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarU.IconSize = 30;
            btnCancelarU.Location = new Point(363, 249);
            btnCancelarU.Margin = new Padding(3, 2, 3, 2);
            btnCancelarU.Name = "btnCancelarU";
            btnCancelarU.Size = new Size(152, 36);
            btnCancelarU.TabIndex = 178;
            btnCancelarU.Text = "CANCELAR";
            btnCancelarU.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelarU.UseVisualStyleBackColor = false;
            btnCancelarU.Click += btnCancelarU_Click_1;
            // 
            // txtCorreo
            // 
            txtCorreo.Anchor = AnchorStyles.Top;
            txtCorreo.Font = new Font("Century Gothic", 10.2F);
            txtCorreo.Location = new Point(363, 59);
            txtCorreo.Margin = new Padding(3, 2, 3, 2);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(205, 24);
            txtCorreo.TabIndex = 165;
            // 
            // txtConfirmarCont
            // 
            txtConfirmarCont.Anchor = AnchorStyles.Top;
            txtConfirmarCont.Font = new Font("Century Gothic", 10.2F);
            txtConfirmarCont.Location = new Point(363, 169);
            txtConfirmarCont.Margin = new Padding(3, 2, 3, 2);
            txtConfirmarCont.Name = "txtConfirmarCont";
            txtConfirmarCont.Size = new Size(205, 24);
            txtConfirmarCont.TabIndex = 171;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(222, 227, 234);
            label7.Font = new Font("Century Gothic", 10.2F);
            label7.Location = new Point(74, 150);
            label7.Name = "label7";
            label7.Size = new Size(90, 19);
            label7.TabIndex = 175;
            label7.Text = "Contraseña";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(222, 227, 234);
            label8.Font = new Font("Century Gothic", 10.2F);
            label8.Location = new Point(363, 150);
            label8.Name = "label8";
            label8.Size = new Size(159, 19);
            label8.TabIndex = 176;
            label8.Text = "Confirmar contraseña";
            // 
            // txtCont
            // 
            txtCont.Anchor = AnchorStyles.Top;
            txtCont.Font = new Font("Century Gothic", 10.2F);
            txtCont.Location = new Point(74, 169);
            txtCont.Margin = new Padding(3, 2, 3, 2);
            txtCont.Name = "txtCont";
            txtCont.Size = new Size(205, 24);
            txtCont.TabIndex = 170;
            // 
            // chckbIsAdmin
            // 
            chckbIsAdmin.Anchor = AnchorStyles.Top;
            chckbIsAdmin.AutoSize = true;
            chckbIsAdmin.BackColor = Color.FromArgb(222, 227, 234);
            chckbIsAdmin.Font = new Font("Century Gothic", 10.2F);
            chckbIsAdmin.Location = new Point(74, 210);
            chckbIsAdmin.Margin = new Padding(3, 2, 3, 2);
            chckbIsAdmin.Name = "chckbIsAdmin";
            chckbIsAdmin.Size = new Size(206, 23);
            chckbIsAdmin.TabIndex = 173;
            chckbIsAdmin.Text = "Permisos de administrador";
            chckbIsAdmin.UseVisualStyleBackColor = false;
            // 
            // roundedButton4
            // 
            roundedButton4.Anchor = AnchorStyles.Top;
            roundedButton4.AutoSize = true;
            roundedButton4.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BorderColor = Color.Empty;
            roundedButton4.BorderRadius = 40;
            roundedButton4.BorderSize = 0;
            roundedButton4.Enabled = false;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(19, 14);
            roundedButton4.Margin = new Padding(3, 2, 3, 2);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(631, 305);
            roundedButton4.TabIndex = 177;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(175, 192, 218);
            label1.Font = new Font("Century Gothic", 12F);
            label1.Location = new Point(81, 10);
            label1.Name = "label1";
            label1.Size = new Size(90, 21);
            label1.TabIndex = 180;
            label1.Text = "USUARIOS";
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 25;
            iconPictureBox1.Location = new Point(41, 7);
            iconPictureBox1.Margin = new Padding(3, 2, 3, 2);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(35, 25);
            iconPictureBox1.TabIndex = 181;
            iconPictureBox1.TabStop = false;
            // 
            // iconPictureBoxIcono
            // 
            iconPictureBoxIcono.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBoxIcono.ForeColor = SystemColors.ControlText;
            iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconPictureBoxIcono.IconColor = SystemColors.ControlText;
            iconPictureBoxIcono.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBoxIcono.IconSize = 28;
            iconPictureBoxIcono.Location = new Point(292, 7);
            iconPictureBoxIcono.Margin = new Padding(3, 2, 3, 2);
            iconPictureBoxIcono.Name = "iconPictureBoxIcono";
            iconPictureBoxIcono.Size = new Size(35, 28);
            iconPictureBoxIcono.TabIndex = 185;
            iconPictureBoxIcono.TabStop = false;
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = Color.Transparent;
            iconPictureBox3.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox3.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 37;
            iconPictureBox3.Location = new Point(214, 2);
            iconPictureBox3.Margin = new Padding(3, 2, 3, 2);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(48, 37);
            iconPictureBox3.TabIndex = 183;
            iconPictureBox3.TabStop = false;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderRadius = 40;
            roundedButton2.BorderSize = 0;
            roundedButton2.Enabled = false;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(260, 2);
            roundedButton2.Margin = new Padding(3, 2, 3, 2);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(90, 37);
            roundedButton2.TabIndex = 184;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(5, 2);
            roundedButton1.Margin = new Padding(3, 2, 3, 2);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(205, 37);
            roundedButton1.TabIndex = 182;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // FrmAdministrarUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(725, 620);
            ControlBox = false;
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmAdministrarUsuarios";
            Text = "FrmAdministrarUsuarios";
            Load += FrmAdministrarUsuarios_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconoTitulo).EndInit();
            tabPageUserDetail.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxIcono).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPageUserDetail;
        private FontAwesome.Sharp.IconButton iconButton2;
        private DataGridView dtgUsuarios;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton iconButton6;
        private FontAwesome.Sharp.IconPictureBox iconoTitulo;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private Clases.RoundedButton btngrisBuscar;
        private Clases.RoundedButton btnTitulo;
        private Panel panel1;
        private Clases.RoundedButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private Panel panel2;
        private Panel panel5;
        private FontAwesome.Sharp.IconButton btnGuardarU;
        private FontAwesome.Sharp.IconButton btnCancelarU;
        private TextBox txtConfirmarCont;
        private Label label8;
        private CheckBox chckbIsAdmin;
        private TextBox txtCont;
        private Label label7;
        private TextBox txtCorreo;
        private Label label6;
        private TextBox txtApellidos;
        private Label label5;
        private TextBox txtNombres;
        private Label label4;
        private TextBox txtUsername;
        private Label label3;
        private Clases.RoundedButton roundedButton4;
        private Label label1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxIcono;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private Clases.RoundedButton roundedButton2;
        private Clases.RoundedButton roundedButton1;
        private Panel panel3;
    }
}