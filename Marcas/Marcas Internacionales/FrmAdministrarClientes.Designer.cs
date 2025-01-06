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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabClientesList = new TabPage();
            btnLast = new FontAwesome.Sharp.IconButton();
            btnNext = new FontAwesome.Sharp.IconButton();
            btnPrev = new FontAwesome.Sharp.IconButton();
            btnFirst = new FontAwesome.Sharp.IconButton();
            lblTotalPages = new Label();
            label10 = new Label();
            lblCurrentPage = new Label();
            lblTotalRows = new Label();
            label2 = new Label();
            label1 = new Label();
            iconButton6 = new FontAwesome.Sharp.IconButton();
            panel9 = new Panel();
            panel11 = new Panel();
            panel12 = new Panel();
            roundedButton5 = new Clases.RoundedButton();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            panel13 = new Panel();
            roundedButton6 = new Clases.RoundedButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            txtBuscar = new TextBox();
            roundedButton3 = new Clases.RoundedButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            dtgClientes = new DataGridView();
            tabClienteDetail = new TabPage();
            panel3 = new Panel();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            panel2 = new Panel();
            label3 = new Label();
            txtNombreCliente = new TextBox();
            label4 = new Label();
            txtNitCliente = new TextBox();
            label5 = new Label();
            label6 = new Label();
            txtDireccionCliente = new TextBox();
            label7 = new Label();
            txtCorreoContacto = new TextBox();
            btnGuardarU = new FontAwesome.Sharp.IconButton();
            label8 = new Label();
            btnCancelarU = new FontAwesome.Sharp.IconButton();
            txtTelefonoContacto = new TextBox();
            comboBox1 = new ComboBox();
            label9 = new Label();
            txtNombreContacto = new TextBox();
            roundedButton4 = new Clases.RoundedButton();
            roundedButton1 = new Clases.RoundedButton();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton7 = new Clases.RoundedButton();
            btnCambios = new Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabClientesList.SuspendLayout();
            panel9.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            panel13.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgClientes).BeginInit();
            tabClienteDetail.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
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
            tabClientesList.AutoScroll = true;
            tabClientesList.BackColor = Color.White;
            tabClientesList.Controls.Add(btnLast);
            tabClientesList.Controls.Add(btnNext);
            tabClientesList.Controls.Add(btnPrev);
            tabClientesList.Controls.Add(btnFirst);
            tabClientesList.Controls.Add(lblTotalPages);
            tabClientesList.Controls.Add(label10);
            tabClientesList.Controls.Add(lblCurrentPage);
            tabClientesList.Controls.Add(lblTotalRows);
            tabClientesList.Controls.Add(label2);
            tabClientesList.Controls.Add(label1);
            tabClientesList.Controls.Add(iconButton6);
            tabClientesList.Controls.Add(panel9);
            tabClientesList.Controls.Add(iconButton2);
            tabClientesList.Controls.Add(iconButton1);
            tabClientesList.Controls.Add(txtBuscar);
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
            // btnLast
            // 
            btnLast.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLast.AutoSize = true;
            btnLast.BackColor = Color.FromArgb(158, 158, 158);
            btnLast.FlatAppearance.BorderSize = 0;
            btnLast.FlatStyle = FlatStyle.Flat;
            btnLast.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnLast.ForeColor = Color.White;
            btnLast.IconChar = FontAwesome.Sharp.IconChar.None;
            btnLast.IconColor = Color.White;
            btnLast.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLast.IconSize = 25;
            btnLast.Location = new Point(756, 764);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(104, 31);
            btnLast.TabIndex = 198;
            btnLast.Text = "ÚLTIMA";
            btnLast.UseVisualStyleBackColor = false;
            btnLast.Click += btnLast_Click;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNext.AutoSize = true;
            btnNext.BackColor = Color.FromArgb(158, 158, 158);
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnNext.ForeColor = Color.White;
            btnNext.IconChar = FontAwesome.Sharp.IconChar.None;
            btnNext.IconColor = Color.White;
            btnNext.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNext.IconSize = 25;
            btnNext.Location = new Point(646, 763);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(104, 31);
            btnNext.TabIndex = 197;
            btnNext.Text = ">>";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // 
            // btnPrev
            // 
            btnPrev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPrev.AutoSize = true;
            btnPrev.BackColor = Color.FromArgb(158, 158, 158);
            btnPrev.FlatAppearance.BorderSize = 0;
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnPrev.ForeColor = Color.White;
            btnPrev.IconChar = FontAwesome.Sharp.IconChar.None;
            btnPrev.IconColor = Color.White;
            btnPrev.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPrev.IconSize = 25;
            btnPrev.Location = new Point(536, 764);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(104, 31);
            btnPrev.TabIndex = 196;
            btnPrev.Text = "<<";
            btnPrev.UseVisualStyleBackColor = false;
            btnPrev.Click += btnPrev_Click;
            // 
            // btnFirst
            // 
            btnFirst.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFirst.AutoSize = true;
            btnFirst.BackColor = Color.FromArgb(158, 158, 158);
            btnFirst.FlatAppearance.BorderSize = 0;
            btnFirst.FlatStyle = FlatStyle.Flat;
            btnFirst.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnFirst.ForeColor = Color.White;
            btnFirst.IconChar = FontAwesome.Sharp.IconChar.None;
            btnFirst.IconColor = Color.White;
            btnFirst.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnFirst.IconSize = 25;
            btnFirst.Location = new Point(426, 764);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(104, 31);
            btnFirst.TabIndex = 195;
            btnFirst.Text = "1";
            btnFirst.UseVisualStyleBackColor = false;
            btnFirst.Click += btnFirst_Click;
            // 
            // lblTotalPages
            // 
            lblTotalPages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalPages.AutoSize = true;
            lblTotalPages.Font = new Font("Century Gothic", 9F);
            lblTotalPages.Location = new Point(810, 190);
            lblTotalPages.Name = "lblTotalPages";
            lblTotalPages.Size = new Size(17, 20);
            lblTotalPages.TabIndex = 194;
            lblTotalPages.Text = "0";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 9F);
            label10.Location = new Point(775, 190);
            label10.Name = "label10";
            label10.Size = new Size(29, 20);
            label10.TabIndex = 193;
            label10.Text = "de";
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCurrentPage.AutoSize = true;
            lblCurrentPage.Font = new Font("Century Gothic", 9F);
            lblCurrentPage.Location = new Point(741, 190);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(17, 20);
            lblCurrentPage.TabIndex = 192;
            lblCurrentPage.Text = "0";
            // 
            // lblTotalRows
            // 
            lblTotalRows.AutoSize = true;
            lblTotalRows.Font = new Font("Century Gothic", 9F);
            lblTotalRows.Location = new Point(175, 190);
            lblTotalRows.Name = "lblTotalRows";
            lblTotalRows.Size = new Size(17, 20);
            lblTotalRows.TabIndex = 191;
            lblTotalRows.Text = "0";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F);
            label2.Location = new Point(675, 190);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 190;
            label2.Text = "Página";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 9F);
            label1.Location = new Point(31, 190);
            label1.Name = "label1";
            label1.Size = new Size(138, 20);
            label1.TabIndex = 189;
            label1.Text = "Total de registros: ";
            // 
            // iconButton6
            // 
            iconButton6.Anchor = AnchorStyles.Top;
            iconButton6.BackColor = Color.FromArgb(236, 236, 238);
            iconButton6.FlatAppearance.BorderSize = 0;
            iconButton6.FlatStyle = FlatStyle.Flat;
            iconButton6.IconChar = FontAwesome.Sharp.IconChar.Close;
            iconButton6.IconColor = Color.Black;
            iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton6.IconSize = 25;
            iconButton6.Location = new Point(514, 124);
            iconButton6.Name = "iconButton6";
            iconButton6.Size = new Size(26, 32);
            iconButton6.TabIndex = 188;
            iconButton6.UseVisualStyleBackColor = false;
            iconButton6.Click += iconButton6_Click;
            // 
            // panel9
            // 
            panel9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel9.AutoSize = true;
            panel9.Controls.Add(panel11);
            panel9.Controls.Add(iconPictureBox2);
            panel9.Controls.Add(panel13);
            panel9.Location = new Point(6, 6);
            panel9.Name = "panel9";
            panel9.Size = new Size(1042, 49);
            panel9.TabIndex = 187;
            // 
            // panel11
            // 
            panel11.Controls.Add(panel12);
            panel11.Controls.Add(roundedButton5);
            panel11.Dock = DockStyle.Left;
            panel11.Location = new Point(346, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(134, 49);
            panel11.TabIndex = 170;
            // 
            // panel12
            // 
            panel12.AutoSize = true;
            panel12.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel12.BackColor = Color.FromArgb(175, 192, 218);
            panel12.Location = new Point(3, 14);
            panel12.Margin = new Padding(0);
            panel12.Name = "panel12";
            panel12.Size = new Size(0, 0);
            panel12.TabIndex = 175;
            // 
            // roundedButton5
            // 
            roundedButton5.AutoSize = true;
            roundedButton5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            roundedButton5.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderRadius = 40;
            roundedButton5.BorderSize = 0;
            roundedButton5.Dock = DockStyle.Fill;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.Font = new Font("Century Gothic", 9F);
            roundedButton5.ForeColor = Color.Black;
            roundedButton5.Location = new Point(0, 0);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(134, 49);
            roundedButton5.TabIndex = 167;
            roundedButton5.Text = "CLIENTES";
            roundedButton5.TextColor = Color.Black;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.BackColor = Color.Transparent;
            iconPictureBox2.Dock = DockStyle.Left;
            iconPictureBox2.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox2.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 49;
            iconPictureBox2.Location = new Point(297, 0);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(49, 49);
            iconPictureBox2.TabIndex = 166;
            iconPictureBox2.TabStop = false;
            iconPictureBox2.UseGdi = true;
            // 
            // panel13
            // 
            panel13.Controls.Add(roundedButton6);
            panel13.Dock = DockStyle.Left;
            panel13.Location = new Point(0, 0);
            panel13.Name = "panel13";
            panel13.Size = new Size(297, 49);
            panel13.TabIndex = 169;
            // 
            // roundedButton6
            // 
            roundedButton6.AutoSize = true;
            roundedButton6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            roundedButton6.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton6.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton6.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton6.BorderRadius = 40;
            roundedButton6.BorderSize = 0;
            roundedButton6.Dock = DockStyle.Fill;
            roundedButton6.FlatAppearance.BorderSize = 0;
            roundedButton6.FlatStyle = FlatStyle.Flat;
            roundedButton6.Font = new Font("Century Gothic", 9F);
            roundedButton6.ForeColor = Color.Black;
            roundedButton6.Image = Properties.Resources.bandera_5_;
            roundedButton6.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton6.Location = new Point(0, 0);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(297, 49);
            roundedButton6.TabIndex = 164;
            roundedButton6.Text = "MARCAS NACIONALES";
            roundedButton6.TextColor = Color.Black;
            roundedButton6.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconButton2.BackColor = Color.FromArgb(0, 137, 123);
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            iconButton2.ForeColor = Color.White;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Eye;
            iconButton2.IconColor = Color.White;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 30;
            iconButton2.ImageAlign = ContentAlignment.MiddleRight;
            iconButton2.Location = new Point(868, 213);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(174, 49);
            iconButton2.TabIndex = 57;
            iconButton2.Text = "VER";
            iconButton2.TextAlign = ContentAlignment.MiddleLeft;
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
            iconButton1.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            iconButton1.ForeColor = Color.White;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 30;
            iconButton1.Location = new Point(551, 116);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(144, 44);
            iconButton1.TabIndex = 51;
            iconButton1.Text = "BUSCAR";
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top;
            txtBuscar.Font = new Font("Century Gothic", 12F);
            txtBuscar.Location = new Point(207, 124);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(301, 32);
            txtBuscar.TabIndex = 50;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.Top;
            roundedButton3.BackColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BackgroundColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderRadius = 40;
            roundedButton3.BorderSize = 0;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(178, 101);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(560, 74);
            roundedButton3.TabIndex = 52;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // ibtnEditar
            // 
            ibtnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ibtnEditar.BackColor = Color.FromArgb(96, 149, 241);
            ibtnEditar.FlatAppearance.BorderSize = 0;
            ibtnEditar.FlatStyle = FlatStyle.Flat;
            ibtnEditar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            ibtnEditar.ForeColor = Color.White;
            ibtnEditar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            ibtnEditar.IconColor = Color.White;
            ibtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEditar.IconSize = 25;
            ibtnEditar.ImageAlign = ContentAlignment.MiddleRight;
            ibtnEditar.Location = new Point(868, 341);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(174, 49);
            ibtnEditar.TabIndex = 16;
            ibtnEditar.Text = "EDITAR/ VER";
            ibtnEditar.TextAlign = ContentAlignment.MiddleLeft;
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click;
            // 
            // ibtnAgregar
            // 
            ibtnAgregar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ibtnAgregar.BackColor = Color.FromArgb(50, 164, 115);
            ibtnAgregar.FlatAppearance.BorderSize = 0;
            ibtnAgregar.FlatStyle = FlatStyle.Flat;
            ibtnAgregar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            ibtnAgregar.ForeColor = Color.White;
            ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            ibtnAgregar.IconColor = Color.White;
            ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnAgregar.IconSize = 25;
            ibtnAgregar.ImageAlign = ContentAlignment.MiddleRight;
            ibtnAgregar.Location = new Point(868, 277);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(174, 49);
            ibtnAgregar.TabIndex = 15;
            ibtnAgregar.Text = "AGREGAR";
            ibtnAgregar.TextAlign = ContentAlignment.MiddleLeft;
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnAgregar_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgClientes);
            panel1.Location = new Point(31, 213);
            panel1.Name = "panel1";
            panel1.Size = new Size(829, 539);
            panel1.TabIndex = 56;
            // 
            // dtgClientes
            // 
            dtgClientes.AllowUserToAddRows = false;
            dtgClientes.AllowUserToDeleteRows = false;
            dtgClientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F);
            dtgClientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dtgClientes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgClientes.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgClientes.BorderStyle = BorderStyle.None;
            dtgClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgClientes.ColumnHeadersHeight = 40;
            dtgClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgClientes.EnableHeadersVisualStyles = false;
            dtgClientes.GridColor = Color.LightGray;
            dtgClientes.Location = new Point(25, 19);
            dtgClientes.Name = "dtgClientes";
            dtgClientes.ReadOnly = true;
            dtgClientes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgClientes.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgClientes.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dtgClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgClientes.Size = new Size(787, 501);
            dtgClientes.TabIndex = 13;
            dtgClientes.CellClick += dtgClientes_CellClick;
            dtgClientes.CellContentClick += dtgClientes_CellContentClick;
            dtgClientes.CellDoubleClick += dtgClientes_CellDoubleClick;
            // 
            // tabClienteDetail
            // 
            tabClienteDetail.BackColor = Color.White;
            tabClienteDetail.Controls.Add(panel3);
            tabClienteDetail.Location = new Point(4, 29);
            tabClienteDetail.Name = "tabClienteDetail";
            tabClienteDetail.Padding = new Padding(3);
            tabClienteDetail.Size = new Size(1161, 794);
            tabClienteDetail.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.Controls.Add(iconPictureBox3);
            panel3.Controls.Add(panel2);
            panel3.Controls.Add(roundedButton1);
            panel3.Controls.Add(iconPictureBox1);
            panel3.Controls.Add(roundedButton7);
            panel3.Controls.Add(btnCambios);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(1155, 788);
            panel3.TabIndex = 194;
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = Color.Transparent;
            iconPictureBox3.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox3.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 49;
            iconPictureBox3.Location = new Point(582, 2);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(55, 49);
            iconPictureBox3.TabIndex = 154;
            iconPictureBox3.TabStop = false;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.AutoScroll = true;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtNombreCliente);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txtNitCliente);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(txtDireccionCliente);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(txtCorreoContacto);
            panel2.Controls.Add(btnGuardarU);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(btnCancelarU);
            panel2.Controls.Add(txtTelefonoContacto);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(txtNombreContacto);
            panel2.Controls.Add(roundedButton4);
            panel2.Location = new Point(34, 67);
            panel2.Name = "panel2";
            panel2.Size = new Size(1013, 473);
            panel2.TabIndex = 193;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(222, 227, 234);
            label3.Font = new Font("Century Gothic", 9F);
            label3.Location = new Point(92, 36);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 41;
            label3.Text = "Nombre";
            // 
            // txtNombreCliente
            // 
            txtNombreCliente.Font = new Font("Century Gothic", 9F);
            txtNombreCliente.Location = new Point(92, 62);
            txtNombreCliente.Name = "txtNombreCliente";
            txtNombreCliente.Size = new Size(389, 26);
            txtNombreCliente.TabIndex = 32;
            txtNombreCliente.TextChanged += txtNombreCliente_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(222, 227, 234);
            label4.Font = new Font("Century Gothic", 9F);
            label4.Location = new Point(92, 105);
            label4.Name = "label4";
            label4.Size = new Size(30, 20);
            label4.TabIndex = 42;
            label4.Text = "NIT";
            // 
            // txtNitCliente
            // 
            txtNitCliente.Font = new Font("Century Gothic", 9F);
            txtNitCliente.Location = new Point(92, 131);
            txtNitCliente.Name = "txtNitCliente";
            txtNitCliente.Size = new Size(389, 26);
            txtNitCliente.TabIndex = 34;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(222, 227, 234);
            label5.Font = new Font("Century Gothic", 9F);
            label5.Location = new Point(529, 105);
            label5.Name = "label5";
            label5.Size = new Size(37, 20);
            label5.TabIndex = 43;
            label5.Text = "Pais";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(222, 227, 234);
            label6.Font = new Font("Century Gothic", 9F);
            label6.Location = new Point(529, 36);
            label6.Name = "label6";
            label6.Size = new Size(80, 20);
            label6.TabIndex = 44;
            label6.Text = "Dirección";
            // 
            // txtDireccionCliente
            // 
            txtDireccionCliente.Font = new Font("Century Gothic", 9F);
            txtDireccionCliente.Location = new Point(529, 62);
            txtDireccionCliente.Name = "txtDireccionCliente";
            txtDireccionCliente.Size = new Size(389, 26);
            txtDireccionCliente.TabIndex = 33;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(222, 227, 234);
            label7.Font = new Font("Century Gothic", 9F);
            label7.Location = new Point(92, 183);
            label7.Name = "label7";
            label7.Size = new Size(61, 20);
            label7.TabIndex = 45;
            label7.Text = "Correo";
            // 
            // txtCorreoContacto
            // 
            txtCorreoContacto.Font = new Font("Century Gothic", 9F);
            txtCorreoContacto.Location = new Point(92, 209);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(389, 26);
            txtCorreoContacto.TabIndex = 36;
            // 
            // btnGuardarU
            // 
            btnGuardarU.BackColor = Color.FromArgb(50, 164, 115);
            btnGuardarU.FlatAppearance.BorderSize = 0;
            btnGuardarU.FlatStyle = FlatStyle.Flat;
            btnGuardarU.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnGuardarU.ForeColor = Color.White;
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnGuardarU.IconColor = Color.White;
            btnGuardarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardarU.IconSize = 30;
            btnGuardarU.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarU.Location = new Point(290, 356);
            btnGuardarU.Name = "btnGuardarU";
            btnGuardarU.Size = new Size(191, 58);
            btnGuardarU.TabIndex = 158;
            btnGuardarU.Text = "AGREGAR";
            btnGuardarU.TextAlign = ContentAlignment.MiddleRight;
            btnGuardarU.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnGuardarU.UseVisualStyleBackColor = false;
            btnGuardarU.Click += btnGuardarU_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(222, 227, 234);
            label8.Font = new Font("Century Gothic", 9F);
            label8.Location = new Point(529, 183);
            label8.Name = "label8";
            label8.Size = new Size(71, 20);
            label8.TabIndex = 46;
            label8.Text = "Teléfono";
            // 
            // btnCancelarU
            // 
            btnCancelarU.BackColor = Color.White;
            btnCancelarU.FlatAppearance.BorderSize = 0;
            btnCancelarU.FlatStyle = FlatStyle.Flat;
            btnCancelarU.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnCancelarU.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarU.IconColor = Color.Black;
            btnCancelarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarU.IconSize = 30;
            btnCancelarU.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarU.Location = new Point(529, 356);
            btnCancelarU.Name = "btnCancelarU";
            btnCancelarU.Size = new Size(191, 58);
            btnCancelarU.TabIndex = 157;
            btnCancelarU.Text = "CANCELAR";
            btnCancelarU.TextAlign = ContentAlignment.MiddleRight;
            btnCancelarU.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelarU.UseVisualStyleBackColor = false;
            btnCancelarU.Click += btnCancelarU_Click;
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Font = new Font("Century Gothic", 9F);
            txtTelefonoContacto.Location = new Point(529, 209);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(389, 26);
            txtTelefonoContacto.TabIndex = 37;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(241, 240, 245);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Century Gothic", 9F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita", "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Baréin", "Bangladés", "Barbados", "Bielorrusia", "Birmania (Myanmar)", "Burundi", "Bután", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo (Congo-Brazzaville)", "Corea del Norte", "Corea del Sur", "Costa Rica", "Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Eswatini", "Etiopía", "Fiyi", "Filipinas", "Finlandia", "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Grecia", "Granada", "Guatemala", "Guinea", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Hungría", "Islandia", "India", "Indonesia", "Irán", "Irak", "Irlanda", "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kosovo", "Kuwait", "Laos", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein", "Lituania", "Luxemburgo", "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Mauricio", "Mauritania", "México", "Micronesia", "Moldavia", "Mónaco", "Mongolia", "Mozambique", "Namibia", "Nauru", "Nepal", "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Pakistán", "Palaos", "Palestina", "Panamá", "Paraguay", "Perú", "Polonia", "Portugal", "Qatar", "República Centroafricana", "República Checa", "República del Congo (Congo-Kinshasa)", "República Dominicana", "Rumania", "Rusia", "Ruanda", "San Cristóbal y Nieves", "San Marino", "Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Tailandia", "Taiwán", "Tanzania", "Tayikistán", "Timor Oriental", "Togo", "Tonga", "Trinidad y Tobago", "Túnez", "Turquía", "Turkmenistán", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabue" });
            comboBox1.Location = new Point(529, 131);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(389, 28);
            comboBox1.TabIndex = 88;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(222, 227, 234);
            label9.Font = new Font("Century Gothic", 9F);
            label9.Location = new Point(92, 258);
            label9.Name = "label9";
            label9.Size = new Size(80, 20);
            label9.TabIndex = 47;
            label9.Text = "Contacto";
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Font = new Font("Century Gothic", 9F);
            txtNombreContacto.Location = new Point(92, 284);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(389, 26);
            txtNombreContacto.TabIndex = 38;
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
            roundedButton4.Location = new Point(29, 20);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(942, 407);
            roundedButton4.TabIndex = 89;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
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
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(435, 2);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(141, 49);
            roundedButton1.TabIndex = 191;
            roundedButton1.Text = "CLIENTES";
            roundedButton1.TextColor = Color.Black;
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
            iconPictureBox1.Location = new Point(374, 3);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(55, 49);
            iconPictureBox1.TabIndex = 190;
            iconPictureBox1.TabStop = false;
            // 
            // roundedButton7
            // 
            roundedButton7.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderRadius = 40;
            roundedButton7.BorderSize = 0;
            roundedButton7.FlatAppearance.BorderSize = 0;
            roundedButton7.FlatStyle = FlatStyle.Flat;
            roundedButton7.ForeColor = Color.Black;
            roundedButton7.Image = Properties.Resources.bandera_6_;
            roundedButton7.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton7.Location = new Point(8, 3);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(360, 49);
            roundedButton7.TabIndex = 188;
            roundedButton7.Text = "MARCAS NACIONALES";
            roundedButton7.TextColor = Color.Black;
            roundedButton7.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton7.UseVisualStyleBackColor = false;
            // 
            // btnCambios
            // 
            btnCambios.BackColor = Color.FromArgb(175, 192, 218);
            btnCambios.BackgroundColor = Color.FromArgb(175, 192, 218);
            btnCambios.BorderColor = Color.FromArgb(175, 192, 218);
            btnCambios.BorderRadius = 40;
            btnCambios.BorderSize = 0;
            btnCambios.FlatAppearance.BorderSize = 0;
            btnCambios.FlatStyle = FlatStyle.Flat;
            btnCambios.ForeColor = Color.Black;
            btnCambios.Image = Properties.Resources.lapiz;
            btnCambios.ImageAlign = ContentAlignment.MiddleRight;
            btnCambios.Location = new Point(640, 2);
            btnCambios.Name = "btnCambios";
            btnCambios.Size = new Size(162, 49);
            btnCambios.TabIndex = 155;
            btnCambios.Text = "AGREGAR";
            btnCambios.TextColor = Color.Black;
            btnCambios.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCambios.UseVisualStyleBackColor = false;
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
            panel9.ResumeLayout(false);
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgClientes).EndInit();
            tabClienteDetail.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
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
        private TextBox txtBuscar;
        private Clases.RoundedButton roundedButton3;
        private Panel panel1;
        private ComboBox comboBox1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private Clases.RoundedButton roundedButton4;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private FontAwesome.Sharp.IconButton btnGuardarU;
        private FontAwesome.Sharp.IconButton btnCancelarU;
        private Clases.RoundedButton roundedButton1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Clases.RoundedButton roundedButton7;
        private Panel panel2;
        private Panel panel3;
        private Panel panel9;
        private Panel panel11;
        private Panel panel12;
        private Clases.RoundedButton roundedButton5;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Panel panel13;
        private Clases.RoundedButton roundedButton6;
        private Clases.RoundedButton btnCambios;
        private FontAwesome.Sharp.IconButton iconButton6;
        private Label lblTotalPages;
        private Label label10;
        private Label lblCurrentPage;
        private Label lblTotalRows;
        private Label label2;
        private Label label1;
        private FontAwesome.Sharp.IconButton btnLast;
        private FontAwesome.Sharp.IconButton btnNext;
        private FontAwesome.Sharp.IconButton btnPrev;
        private FontAwesome.Sharp.IconButton btnFirst;
    }
}