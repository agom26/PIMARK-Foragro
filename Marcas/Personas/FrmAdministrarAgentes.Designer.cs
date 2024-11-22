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
            tabControl1 = new TabControl();
            tabPageListado = new TabPage();
            panel2 = new Panel();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            panel3 = new Panel();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            label2 = new Label();
            textBox1 = new TextBox();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            label1 = new Label();
            roundedButton5 = new Clases.RoundedButton();
            roundedButton3 = new Clases.RoundedButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            dtgAgentes = new DataGridView();
            tabPageAgenteDetail = new TabPage();
            panel4 = new Panel();
            btnGuardarU = new FontAwesome.Sharp.IconButton();
            btnCancelarU = new FontAwesome.Sharp.IconButton();
            label3 = new Label();
            iconPictureBoxIcono = new FontAwesome.Sharp.IconPictureBox();
            txtNombreAgente = new TextBox();
            roundedButton2 = new Clases.RoundedButton();
            label4 = new Label();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            txtNitAgente = new TextBox();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label5 = new Label();
            label10 = new Label();
            label6 = new Label();
            txtDireccionAgente = new TextBox();
            comboBox1 = new ComboBox();
            label7 = new Label();
            txtNombreContacto = new TextBox();
            txtCorreoContacto = new TextBox();
            label9 = new Label();
            label8 = new Label();
            txtTelefonoContacto = new TextBox();
            roundedButton4 = new Clases.RoundedButton();
            roundedButton1 = new Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabPageListado.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).BeginInit();
            tabPageAgenteDetail.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxIcono).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
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
            tabControl1.Size = new Size(725, 620);
            tabControl1.TabIndex = 1;
            // 
            // tabPageListado
            // 
            tabPageListado.Controls.Add(panel2);
            tabPageListado.Location = new Point(4, 30);
            tabPageListado.Name = "tabPageListado";
            tabPageListado.Padding = new Padding(3);
            tabPageListado.Size = new Size(717, 586);
            tabPageListado.TabIndex = 0;
            tabPageListado.UseVisualStyleBackColor = true;
            tabPageListado.Click += tabPageListado_Click;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(ibtnEditar);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(ibtnAgregar);
            panel2.Controls.Add(panel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(711, 580);
            panel2.TabIndex = 56;
            // 
            // ibtnEditar
            // 
            ibtnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ibtnEditar.BackColor = Color.FromArgb(96, 149, 241);
            ibtnEditar.FlatAppearance.BorderSize = 0;
            ibtnEditar.FlatStyle = FlatStyle.Flat;
            ibtnEditar.Font = new Font("Century Gothic", 9F);
            ibtnEditar.ForeColor = Color.White;
            ibtnEditar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            ibtnEditar.IconColor = Color.White;
            ibtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEditar.IconSize = 25;
            ibtnEditar.Location = new Point(553, 206);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(152, 37);
            ibtnEditar.TabIndex = 17;
            ibtnEditar.Text = "EDITAR/ VER";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Controls.Add(iconPictureBox2);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(textBox1);
            panel3.Controls.Add(iconButton1);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(roundedButton5);
            panel3.Controls.Add(roundedButton3);
            panel3.Location = new Point(3, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(731, 145);
            panel3.TabIndex = 55;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.Anchor = AnchorStyles.None;
            iconPictureBox2.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox2.ForeColor = SystemColors.ControlText;
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.UserTie;
            iconPictureBox2.IconColor = SystemColors.ControlText;
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 40;
            iconPictureBox2.Location = new Point(274, 12);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(46, 40);
            iconPictureBox2.TabIndex = 54;
            iconPictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(175, 192, 218);
            label2.Font = new Font("Century Gothic", 15F);
            label2.Location = new Point(326, 19);
            label2.Name = "label2";
            label2.Size = new Size(97, 23);
            label2.TabIndex = 48;
            label2.Text = "AGENTES";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(162, 101);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(264, 27);
            textBox1.TabIndex = 50;
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.None;
            iconButton1.BackColor = Color.FromArgb(251, 140, 0);
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 10F);
            iconButton1.ForeColor = Color.White;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 30;
            iconButton1.Location = new Point(459, 94);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(126, 30);
            iconButton1.TabIndex = 51;
            iconButton1.Text = "BUSCAR";
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(236, 236, 238);
            label1.Font = new Font("Century Gothic", 10F);
            label1.Location = new Point(163, 83);
            label1.Name = "label1";
            label1.Size = new Size(184, 19);
            label1.TabIndex = 49;
            label1.Text = "Buscar por nombre o pais";
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.None;
            roundedButton5.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderRadius = 50;
            roundedButton5.BorderSize = 0;
            roundedButton5.Enabled = false;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(133, 8);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(486, 46);
            roundedButton5.TabIndex = 53;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.None;
            roundedButton3.BackColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BackgroundColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderRadius = 50;
            roundedButton3.BorderSize = 0;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(133, 79);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(486, 56);
            roundedButton3.TabIndex = 52;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // ibtnAgregar
            // 
            ibtnAgregar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ibtnAgregar.BackColor = Color.FromArgb(50, 164, 115);
            ibtnAgregar.FlatAppearance.BorderSize = 0;
            ibtnAgregar.FlatStyle = FlatStyle.Flat;
            ibtnAgregar.Font = new Font("Century Gothic", 9F);
            ibtnAgregar.ForeColor = Color.White;
            ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            ibtnAgregar.IconColor = Color.White;
            ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnAgregar.IconSize = 25;
            ibtnAgregar.Location = new Point(553, 151);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(152, 37);
            ibtnAgregar.TabIndex = 16;
            ibtnAgregar.Text = "AGREGAR";
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnAgregar_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgAgentes);
            panel1.Location = new Point(5, 151);
            panel1.Name = "panel1";
            panel1.Size = new Size(542, 404);
            panel1.TabIndex = 55;
            // 
            // dtgAgentes
            // 
            dtgAgentes.AllowUserToAddRows = false;
            dtgAgentes.AllowUserToDeleteRows = false;
            dtgAgentes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgAgentes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dtgAgentes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgAgentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgAgentes.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgAgentes.BorderStyle = BorderStyle.None;
            dtgAgentes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgAgentes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 12F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgAgentes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgAgentes.ColumnHeadersHeight = 40;
            dtgAgentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgAgentes.EnableHeadersVisualStyles = false;
            dtgAgentes.GridColor = Color.LightGray;
            dtgAgentes.Location = new Point(17, 24);
            dtgAgentes.Name = "dtgAgentes";
            dtgAgentes.ReadOnly = true;
            dtgAgentes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgAgentes.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 10F);
            dtgAgentes.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dtgAgentes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgAgentes.Size = new Size(501, 364);
            dtgAgentes.TabIndex = 14;
            dtgAgentes.CellClick += dtgAgentes_CellClick;
            dtgAgentes.CellContentClick += dtgAgentes_CellContentClick;
            dtgAgentes.CellDoubleClick += dtgAgentes_CellDoubleClick;
            dtgAgentes.DoubleClick += dtgAgentes_DoubleClick;
            // 
            // tabPageAgenteDetail
            // 
            tabPageAgenteDetail.Controls.Add(panel4);
            tabPageAgenteDetail.Location = new Point(4, 30);
            tabPageAgenteDetail.Name = "tabPageAgenteDetail";
            tabPageAgenteDetail.Padding = new Padding(3);
            tabPageAgenteDetail.Size = new Size(717, 586);
            tabPageAgenteDetail.TabIndex = 1;
            tabPageAgenteDetail.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnGuardarU);
            panel4.Controls.Add(btnCancelarU);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(iconPictureBoxIcono);
            panel4.Controls.Add(txtNombreAgente);
            panel4.Controls.Add(roundedButton2);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(iconPictureBox3);
            panel4.Controls.Add(txtNitAgente);
            panel4.Controls.Add(iconPictureBox1);
            panel4.Controls.Add(label5);
            panel4.Controls.Add(label10);
            panel4.Controls.Add(label6);
            panel4.Controls.Add(txtDireccionAgente);
            panel4.Controls.Add(comboBox1);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(txtNombreContacto);
            panel4.Controls.Add(txtCorreoContacto);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(txtTelefonoContacto);
            panel4.Controls.Add(roundedButton4);
            panel4.Controls.Add(roundedButton1);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(711, 580);
            panel4.TabIndex = 153;
            panel4.Paint += panel4_Paint;
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
            btnGuardarU.Location = new Point(94, 438);
            btnGuardarU.Name = "btnGuardarU";
            btnGuardarU.Size = new Size(218, 34);
            btnGuardarU.TabIndex = 152;
            btnGuardarU.Text = "GUARDAR";
            btnGuardarU.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnGuardarU.UseVisualStyleBackColor = false;
            btnGuardarU.Click += btnGuardarU_Click;
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
            btnCancelarU.Location = new Point(378, 438);
            btnCancelarU.Name = "btnCancelarU";
            btnCancelarU.Size = new Size(205, 34);
            btnCancelarU.TabIndex = 151;
            btnCancelarU.Text = "CANCELAR";
            btnCancelarU.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelarU.UseVisualStyleBackColor = false;
            btnCancelarU.Click += btnCancelarU_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(222, 227, 234);
            label3.Font = new Font("Century Gothic", 10F);
            label3.Location = new Point(107, 130);
            label3.Name = "label3";
            label3.Size = new Size(64, 19);
            label3.TabIndex = 34;
            label3.Text = "Nombre";
            // 
            // iconPictureBoxIcono
            // 
            iconPictureBoxIcono.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBoxIcono.ForeColor = SystemColors.ControlText;
            iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconPictureBoxIcono.IconColor = SystemColors.ControlText;
            iconPictureBoxIcono.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBoxIcono.IconSize = 40;
            iconPictureBoxIcono.Location = new Point(378, 10);
            iconPictureBoxIcono.Name = "iconPictureBoxIcono";
            iconPictureBoxIcono.Size = new Size(46, 40);
            iconPictureBoxIcono.TabIndex = 150;
            iconPictureBoxIcono.TabStop = false;
            // 
            // txtNombreAgente
            // 
            txtNombreAgente.Anchor = AnchorStyles.Top;
            txtNombreAgente.Font = new Font("Century Gothic", 10F);
            txtNombreAgente.Location = new Point(107, 156);
            txtNombreAgente.Name = "txtNombreAgente";
            txtNombreAgente.Size = new Size(205, 24);
            txtNombreAgente.TabIndex = 1;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderRadius = 50;
            roundedButton2.BorderSize = 0;
            roundedButton2.Enabled = false;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(345, 4);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(107, 49);
            roundedButton2.TabIndex = 149;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(222, 227, 234);
            label4.Font = new Font("Century Gothic", 10F);
            label4.Location = new Point(107, 199);
            label4.Name = "label4";
            label4.Size = new Size(27, 19);
            label4.TabIndex = 37;
            label4.Text = "NIT";
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = Color.Transparent;
            iconPictureBox3.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox3.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 49;
            iconPictureBox3.Location = new Point(276, 4);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(63, 49);
            iconPictureBox3.TabIndex = 148;
            iconPictureBox3.TabStop = false;
            // 
            // txtNitAgente
            // 
            txtNitAgente.Anchor = AnchorStyles.Top;
            txtNitAgente.Font = new Font("Century Gothic", 10F);
            txtNitAgente.Location = new Point(107, 225);
            txtNitAgente.Name = "txtNitAgente";
            txtNitAgente.Size = new Size(205, 24);
            txtNitAgente.TabIndex = 3;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.UserTie;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 28;
            iconPictureBox1.Location = new Point(50, 16);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(46, 28);
            iconPictureBox1.TabIndex = 147;
            iconPictureBox1.TabStop = false;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(222, 227, 234);
            label5.Font = new Font("Century Gothic", 10F);
            label5.Location = new Point(378, 199);
            label5.Name = "label5";
            label5.Size = new Size(35, 19);
            label5.TabIndex = 40;
            label5.Text = "Pais";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.FromArgb(175, 192, 218);
            label10.Font = new Font("Century Gothic", 12F);
            label10.Location = new Point(103, 16);
            label10.Name = "label10";
            label10.Size = new Size(83, 21);
            label10.TabIndex = 145;
            label10.Text = "AGENTES";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(222, 227, 234);
            label6.Font = new Font("Century Gothic", 10F);
            label6.Location = new Point(378, 130);
            label6.Name = "label6";
            label6.Size = new Size(74, 19);
            label6.TabIndex = 42;
            label6.Text = "Dirección";
            // 
            // txtDireccionAgente
            // 
            txtDireccionAgente.Anchor = AnchorStyles.Top;
            txtDireccionAgente.Font = new Font("Century Gothic", 10F);
            txtDireccionAgente.Location = new Point(378, 156);
            txtDireccionAgente.Name = "txtDireccionAgente";
            txtDireccionAgente.Size = new Size(205, 24);
            txtDireccionAgente.TabIndex = 2;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top;
            comboBox1.BackColor = Color.FromArgb(241, 240, 245);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Century Gothic", 10F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita", "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Baréin", "Bangladés", "Barbados", "Bielorrusia", "Birmania (Myanmar)", "Burundi", "Bután", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo (Congo-Brazzaville)", "Corea del Norte", "Corea del Sur", "Costa Rica", "Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Eswatini", "Etiopía", "Fiyi", "Filipinas", "Finlandia", "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Grecia", "Granada", "Guatemala", "Guinea", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Hungría", "Islandia", "India", "Indonesia", "Irán", "Irak", "Irlanda", "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kosovo", "Kuwait", "Laos", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein", "Lituania", "Luxemburgo", "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Mauricio", "Mauritania", "México", "Micronesia", "Moldavia", "Mónaco", "Mongolia", "Mozambique", "Namibia", "Nauru", "Nepal", "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Pakistán", "Palaos", "Palestina", "Panamá", "Paraguay", "Perú", "Polonia", "Portugal", "Qatar", "República Centroafricana", "República Checa", "República del Congo (Congo-Kinshasa)", "República Dominicana", "Rumania", "Rusia", "Ruanda", "San Cristóbal y Nieves", "San Marino", "Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Tailandia", "Taiwán", "Tanzania", "Tayikistán", "Timor Oriental", "Togo", "Tonga", "Trinidad y Tobago", "Túnez", "Turquía", "Turkmenistán", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabue" });
            comboBox1.Location = new Point(378, 222);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(205, 25);
            comboBox1.TabIndex = 87;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(222, 227, 234);
            label7.Font = new Font("Century Gothic", 10F);
            label7.Location = new Point(107, 277);
            label7.Name = "label7";
            label7.Size = new Size(55, 19);
            label7.TabIndex = 44;
            label7.Text = "Correo";
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Anchor = AnchorStyles.Top;
            txtNombreContacto.Font = new Font("Century Gothic", 10F);
            txtNombreContacto.Location = new Point(107, 378);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(205, 24);
            txtNombreContacto.TabIndex = 7;
            // 
            // txtCorreoContacto
            // 
            txtCorreoContacto.Anchor = AnchorStyles.Top;
            txtCorreoContacto.Font = new Font("Century Gothic", 10F);
            txtCorreoContacto.Location = new Point(107, 303);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(205, 24);
            txtCorreoContacto.TabIndex = 5;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(222, 227, 234);
            label9.Font = new Font("Century Gothic", 10F);
            label9.Location = new Point(107, 352);
            label9.Name = "label9";
            label9.Size = new Size(76, 19);
            label9.TabIndex = 47;
            label9.Text = "Contacto";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(222, 227, 234);
            label8.Font = new Font("Century Gothic", 10F);
            label8.Location = new Point(378, 277);
            label8.Name = "label8";
            label8.Size = new Size(66, 19);
            label8.TabIndex = 45;
            label8.Text = "Teléfono";
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Anchor = AnchorStyles.Top;
            txtTelefonoContacto.Font = new Font("Century Gothic", 10F);
            txtTelefonoContacto.Location = new Point(378, 303);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(205, 24);
            txtTelefonoContacto.TabIndex = 6;
            // 
            // roundedButton4
            // 
            roundedButton4.Anchor = AnchorStyles.Top;
            roundedButton4.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BorderColor = Color.Empty;
            roundedButton4.BorderRadius = 40;
            roundedButton4.BorderSize = 0;
            roundedButton4.Enabled = false;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.Font = new Font("Century Gothic", 10F);
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(15, 111);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(656, 407);
            roundedButton4.TabIndex = 144;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderRadius = 50;
            roundedButton1.BorderSize = 0;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(3, 4);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(267, 49);
            roundedButton1.TabIndex = 146;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // FrmAdministrarAgentes
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(725, 620);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAdministrarAgentes";
            Text = "FrmAdministrarAgentes";
            Load += FrmAdministrarAgentes_Load;
            tabControl1.ResumeLayout(false);
            tabPageListado.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).EndInit();
            tabPageAgenteDetail.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxIcono).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
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
        private Panel panel1;
        private ComboBox comboBox1;
        private Clases.RoundedButton roundedButton4;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxIcono;
        private Clases.RoundedButton roundedButton2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label label10;
        private Clases.RoundedButton roundedButton1;
        private FontAwesome.Sharp.IconButton btnGuardarU;
        private FontAwesome.Sharp.IconButton btnCancelarU;
        private Panel panel2;
        private Panel panel4;
        private Panel panel3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Label label2;
        private TextBox textBox1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private Label label1;
        private Clases.RoundedButton roundedButton5;
        private Clases.RoundedButton roundedButton3;
    }
}