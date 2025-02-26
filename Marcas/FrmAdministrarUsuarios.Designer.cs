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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            lblTotalPages = new Label();
            label10 = new Label();
            lblCurrentPage = new Label();
            lblTotalRows = new Label();
            btnLast = new FontAwesome.Sharp.IconButton();
            btnNext = new FontAwesome.Sharp.IconButton();
            btnPrev = new FontAwesome.Sharp.IconButton();
            btnFirst = new FontAwesome.Sharp.IconButton();
            label2 = new Label();
            label1 = new Label();
            btnEliminar = new FontAwesome.Sharp.IconButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            panel2 = new Panel();
            iconButton6 = new FontAwesome.Sharp.IconButton();
            txtBuscar = new TextBox();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            btnTitulo = new Presentacion.Clases.RoundedButton();
            panel1 = new Panel();
            dtgUsuarios = new DataGridView();
            tabPageUserDetail = new TabPage();
            panel5 = new Panel();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            btnCambios = new Presentacion.Clases.RoundedButton();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
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
            roundedButton4 = new Presentacion.Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).BeginInit();
            tabPageUserDetail.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            panel3.SuspendLayout();
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
            tabControl1.Size = new Size(725, 591);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.AutoScroll = true;
            tabPage1.Controls.Add(lblTotalPages);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(lblCurrentPage);
            tabPage1.Controls.Add(lblTotalRows);
            tabPage1.Controls.Add(btnLast);
            tabPage1.Controls.Add(btnNext);
            tabPage1.Controls.Add(btnPrev);
            tabPage1.Controls.Add(btnFirst);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(btnEliminar);
            tabPage1.Controls.Add(iconButton3);
            tabPage1.Controls.Add(iconButton2);
            tabPage1.Controls.Add(panel2);
            tabPage1.Controls.Add(panel1);
            tabPage1.Font = new Font("Century Gothic", 12F);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(717, 561);
            tabPage1.TabIndex = 0;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            tabPage1.Resize += tabPage1_Resize;
            // 
            // lblTotalPages
            // 
            lblTotalPages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalPages.AutoSize = true;
            lblTotalPages.Font = new Font("Century Gothic", 9F);
            lblTotalPages.Location = new Point(521, 157);
            lblTotalPages.Name = "lblTotalPages";
            lblTotalPages.Size = new Size(15, 17);
            lblTotalPages.TabIndex = 60;
            lblTotalPages.Text = "0";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 9F);
            label10.Location = new Point(491, 157);
            label10.Name = "label10";
            label10.Size = new Size(24, 17);
            label10.TabIndex = 59;
            label10.Text = "de";
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCurrentPage.AutoSize = true;
            lblCurrentPage.Font = new Font("Century Gothic", 9F);
            lblCurrentPage.Location = new Point(461, 157);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(15, 17);
            lblCurrentPage.TabIndex = 58;
            lblCurrentPage.Text = "0";
            // 
            // lblTotalRows
            // 
            lblTotalRows.AutoSize = true;
            lblTotalRows.Font = new Font("Century Gothic", 9F);
            lblTotalRows.Location = new Point(136, 159);
            lblTotalRows.Name = "lblTotalRows";
            lblTotalRows.Size = new Size(15, 17);
            lblTotalRows.TabIndex = 57;
            lblTotalRows.Text = "0";
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
            btnLast.Location = new Point(445, 560);
            btnLast.Margin = new Padding(3, 2, 3, 2);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(91, 31);
            btnLast.TabIndex = 56;
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
            btnNext.Location = new Point(341, 561);
            btnNext.Margin = new Padding(3, 2, 3, 2);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(91, 31);
            btnNext.TabIndex = 55;
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
            btnPrev.Location = new Point(233, 561);
            btnPrev.Margin = new Padding(3, 2, 3, 2);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(91, 31);
            btnPrev.TabIndex = 54;
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
            btnFirst.Location = new Point(124, 561);
            btnFirst.Margin = new Padding(3, 2, 3, 2);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(91, 31);
            btnFirst.TabIndex = 53;
            btnFirst.Text = "PRIMERA";
            btnFirst.UseVisualStyleBackColor = false;
            btnFirst.Click += btnFirst_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F);
            label2.Location = new Point(403, 157);
            label2.Name = "label2";
            label2.Size = new Size(49, 17);
            label2.TabIndex = 52;
            label2.Text = "Página";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 9F);
            label1.Location = new Point(10, 159);
            label1.Name = "label1";
            label1.Size = new Size(115, 17);
            label1.TabIndex = 51;
            label1.Text = "Total de registros: ";
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminar.AutoSize = true;
            btnEliminar.BackColor = Color.FromArgb(229, 115, 115);
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.IconChar = FontAwesome.Sharp.IconChar.Trash;
            btnEliminar.IconColor = Color.White;
            btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEliminar.IconSize = 25;
            btnEliminar.ImageAlign = ContentAlignment.MiddleRight;
            btnEliminar.Location = new Point(542, 276);
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
            iconButton3.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Pen;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 25;
            iconButton3.ImageAlign = ContentAlignment.MiddleRight;
            iconButton3.Location = new Point(542, 226);
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
            iconButton2.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            iconButton2.ForeColor = Color.White;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            iconButton2.IconColor = Color.White;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.ImageAlign = ContentAlignment.MiddleRight;
            iconButton2.Location = new Point(542, 176);
            iconButton2.Margin = new Padding(3, 2, 3, 2);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(152, 37);
            iconButton2.TabIndex = 5;
            iconButton2.Text = "AGREGAR";
            iconButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.AutoSize = true;
            panel2.Controls.Add(iconButton6);
            panel2.Controls.Add(txtBuscar);
            panel2.Controls.Add(iconButton1);
            panel2.Controls.Add(roundedButton3);
            panel2.Controls.Add(btnTitulo);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(696, 145);
            panel2.TabIndex = 50;
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
            iconButton6.Location = new Point(303, 98);
            iconButton6.Margin = new Padding(3, 2, 3, 2);
            iconButton6.Name = "iconButton6";
            iconButton6.Size = new Size(23, 24);
            iconButton6.TabIndex = 56;
            iconButton6.UseVisualStyleBackColor = false;
            iconButton6.Click += iconButton6_Click_2;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.None;
            txtBuscar.Location = new Point(66, 98);
            txtBuscar.Margin = new Padding(3, 2, 3, 2);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(232, 27);
            txtBuscar.TabIndex = 53;
            txtBuscar.KeyDown += txtBuscar_KeyDown_1;
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.None;
            iconButton1.BackColor = Color.FromArgb(251, 140, 0);
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton1.ForeColor = Color.White;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 28;
            iconButton1.ImageAlign = ContentAlignment.MiddleRight;
            iconButton1.Location = new Point(332, 89);
            iconButton1.Margin = new Padding(3, 2, 3, 2);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(126, 40);
            iconButton1.TabIndex = 54;
            iconButton1.Text = "BUSCAR";
            iconButton1.TextAlign = ContentAlignment.MiddleLeft;
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click_1;
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
            roundedButton3.Location = new Point(51, 82);
            roundedButton3.Margin = new Padding(3, 2, 3, 2);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(432, 51);
            roundedButton3.TabIndex = 55;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // btnTitulo
            // 
            btnTitulo.Anchor = AnchorStyles.Top;
            btnTitulo.BackColor = Color.FromArgb(175, 192, 218);
            btnTitulo.BackgroundColor = Color.FromArgb(175, 192, 218);
            btnTitulo.BorderColor = Color.FromArgb(175, 192, 218);
            btnTitulo.BorderRadius = 40;
            btnTitulo.BorderSize = 0;
            btnTitulo.FlatAppearance.BorderSize = 0;
            btnTitulo.FlatStyle = FlatStyle.Flat;
            btnTitulo.Font = new Font("Century Gothic", 15F);
            btnTitulo.ForeColor = Color.Black;
            btnTitulo.Image = Properties.Resources.usuario_final_4_;
            btnTitulo.ImageAlign = ContentAlignment.MiddleRight;
            btnTitulo.Location = new Point(50, 4);
            btnTitulo.Margin = new Padding(3, 2, 3, 2);
            btnTitulo.Name = "btnTitulo";
            btnTitulo.Size = new Size(433, 46);
            btnTitulo.TabIndex = 46;
            btnTitulo.Text = "USUARIOS";
            btnTitulo.TextColor = Color.Black;
            btnTitulo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTitulo.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgUsuarios);
            panel1.Location = new Point(10, 176);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(526, 378);
            panel1.TabIndex = 48;
            // 
            // dtgUsuarios
            // 
            dtgUsuarios.AllowUserToAddRows = false;
            dtgUsuarios.AllowUserToDeleteRows = false;
            dtgUsuarios.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 10F);
            dtgUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dtgUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgUsuarios.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgUsuarios.BorderStyle = BorderStyle.None;
            dtgUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgUsuarios.ColumnHeadersHeight = 40;
            dtgUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dtgUsuarios.DefaultCellStyle = dataGridViewCellStyle3;
            dtgUsuarios.EnableHeadersVisualStyles = false;
            dtgUsuarios.GridColor = Color.LightGray;
            dtgUsuarios.Location = new Point(15, 14);
            dtgUsuarios.Margin = new Padding(3, 2, 3, 2);
            dtgUsuarios.Name = "dtgUsuarios";
            dtgUsuarios.ReadOnly = true;
            dtgUsuarios.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 10F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtgUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtgUsuarios.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 10F);
            dtgUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dtgUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgUsuarios.Size = new Size(496, 349);
            dtgUsuarios.TabIndex = 4;
            dtgUsuarios.CellClick += dtgUsuarios_CellClick;
            dtgUsuarios.CellContentClick += dtgUsuarios_CellContentClick;
            dtgUsuarios.CellDoubleClick += dtgUsuarios_CellDoubleClick;
            dtgUsuarios.DoubleClick += dtgUsuarios_DoubleClick;
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
            tabPageUserDetail.Size = new Size(717, 561);
            tabPageUserDetail.TabIndex = 1;
            tabPageUserDetail.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            panel5.AutoScroll = true;
            panel5.Controls.Add(iconPictureBox3);
            panel5.Controls.Add(btnCambios);
            panel5.Controls.Add(roundedButton1);
            panel5.Controls.Add(panel3);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(711, 557);
            panel5.TabIndex = 150;
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
            // btnCambios
            // 
            btnCambios.BackColor = Color.FromArgb(175, 192, 218);
            btnCambios.BackgroundColor = Color.FromArgb(175, 192, 218);
            btnCambios.BorderColor = Color.FromArgb(175, 192, 218);
            btnCambios.BorderRadius = 37;
            btnCambios.BorderSize = 0;
            btnCambios.FlatAppearance.BorderSize = 0;
            btnCambios.FlatStyle = FlatStyle.Flat;
            btnCambios.Font = new Font("Century Gothic", 12F);
            btnCambios.ForeColor = Color.Black;
            btnCambios.Image = Properties.Resources.agregar;
            btnCambios.ImageAlign = ContentAlignment.MiddleRight;
            btnCambios.Location = new Point(268, 2);
            btnCambios.Margin = new Padding(3, 2, 3, 2);
            btnCambios.Name = "btnCambios";
            btnCambios.Size = new Size(162, 37);
            btnCambios.TabIndex = 184;
            btnCambios.Text = "AGREGAR";
            btnCambios.TextColor = Color.Black;
            btnCambios.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCambios.UseVisualStyleBackColor = false;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderRadius = 37;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 12F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Image = Properties.Resources.usuario_final_4_;
            roundedButton1.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton1.Location = new Point(5, 2);
            roundedButton1.Margin = new Padding(3, 2, 3, 2);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(205, 37);
            roundedButton1.TabIndex = 182;
            roundedButton1.Text = "USUARIOS";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
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
            panel3.Location = new Point(35, 59);
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
            label4.Location = new Point(74, 110);
            label4.Name = "label4";
            label4.Size = new Size(69, 19);
            label4.TabIndex = 169;
            label4.Text = "Nombres";
            // 
            // txtNombres
            // 
            txtNombres.Anchor = AnchorStyles.Top;
            txtNombres.Font = new Font("Century Gothic", 10.2F);
            txtNombres.Location = new Point(74, 130);
            txtNombres.Margin = new Padding(3, 2, 3, 2);
            txtNombres.Multiline = true;
            txtNombres.Name = "txtNombres";
            txtNombres.ScrollBars = ScrollBars.Vertical;
            txtNombres.Size = new Size(205, 37);
            txtNombres.TabIndex = 167;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(222, 227, 234);
            label5.Font = new Font("Century Gothic", 10.2F);
            label5.Location = new Point(363, 110);
            label5.Name = "label5";
            label5.Size = new Size(70, 19);
            label5.TabIndex = 172;
            label5.Text = "Apellidos";
            // 
            // txtApellidos
            // 
            txtApellidos.Anchor = AnchorStyles.Top;
            txtApellidos.Font = new Font("Century Gothic", 10.2F);
            txtApellidos.Location = new Point(363, 130);
            txtApellidos.Margin = new Padding(3, 2, 3, 2);
            txtApellidos.Multiline = true;
            txtApellidos.Name = "txtApellidos";
            txtApellidos.ScrollBars = ScrollBars.Vertical;
            txtApellidos.Size = new Size(205, 37);
            txtApellidos.TabIndex = 168;
            // 
            // btnGuardarU
            // 
            btnGuardarU.Anchor = AnchorStyles.Top;
            btnGuardarU.BackColor = Color.FromArgb(96, 149, 241);
            btnGuardarU.FlatAppearance.BorderSize = 0;
            btnGuardarU.FlatStyle = FlatStyle.Flat;
            btnGuardarU.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnGuardarU.ForeColor = Color.White;
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnGuardarU.IconColor = Color.White;
            btnGuardarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardarU.IconSize = 25;
            btnGuardarU.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarU.Location = new Point(127, 268);
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
            btnCancelarU.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnCancelarU.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarU.IconColor = Color.Black;
            btnCancelarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarU.IconSize = 25;
            btnCancelarU.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarU.Location = new Point(363, 268);
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
            txtCorreo.Multiline = true;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.ScrollBars = ScrollBars.Vertical;
            txtCorreo.Size = new Size(205, 37);
            txtCorreo.TabIndex = 165;
            // 
            // txtConfirmarCont
            // 
            txtConfirmarCont.Anchor = AnchorStyles.Top;
            txtConfirmarCont.Font = new Font("Century Gothic", 10.2F);
            txtConfirmarCont.Location = new Point(363, 201);
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
            label7.Location = new Point(74, 182);
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
            label8.Location = new Point(363, 182);
            label8.Name = "label8";
            label8.Size = new Size(159, 19);
            label8.TabIndex = 176;
            label8.Text = "Confirmar contraseña";
            // 
            // txtCont
            // 
            txtCont.Anchor = AnchorStyles.Top;
            txtCont.Font = new Font("Century Gothic", 10.2F);
            txtCont.Location = new Point(74, 201);
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
            chckbIsAdmin.Location = new Point(74, 229);
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
            roundedButton4.Size = new Size(631, 313);
            roundedButton4.TabIndex = 177;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // FrmAdministrarUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(725, 591);
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
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).EndInit();
            tabPageUserDetail.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPageUserDetail;
        private FontAwesome.Sharp.IconButton iconButton2;
        private DataGridView dtgUsuarios;
        private FontAwesome.Sharp.IconButton iconButton3;
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
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private Clases.RoundedButton btnCambios;
        private Clases.RoundedButton roundedButton1;
        private Panel panel3;
        private TextBox txtBuscar;
        private FontAwesome.Sharp.IconButton iconButton1;
        private Clases.RoundedButton roundedButton3;
        private FontAwesome.Sharp.IconButton iconButton6;
        private Label label1;
        private Label label2;
        private FontAwesome.Sharp.IconButton btnPrev;
        private FontAwesome.Sharp.IconButton btnFirst;
        private FontAwesome.Sharp.IconButton btnLast;
        private FontAwesome.Sharp.IconButton btnNext;
        private Label lblTotalRows;
        private Label lblTotalPages;
        private Label label10;
        private Label lblCurrentPage;
    }
}