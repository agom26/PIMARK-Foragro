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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabListTitulares = new TabPage();
            btnEliminarTitular = new FontAwesome.Sharp.IconButton();
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
            panelBusqueda = new Panel();
            iconButton6 = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            txtBuscar = new TextBox();
            roundedButton5 = new Presentacion.Clases.RoundedButton();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            dtgTitulares = new DataGridView();
            tabTitularDetail = new TabPage();
            panel3 = new Panel();
            btnCambios = new Presentacion.Clases.RoundedButton();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            btnGuardarU = new FontAwesome.Sharp.IconButton();
            btnCancelarU = new FontAwesome.Sharp.IconButton();
            label3 = new Label();
            txtNombreTitular = new TextBox();
            label4 = new Label();
            txtNitTitular = new TextBox();
            label5 = new Label();
            label6 = new Label();
            txtDireccionTitular = new TextBox();
            comboBox1 = new ComboBox();
            label7 = new Label();
            txtNombreContacto = new TextBox();
            txtCorreoContacto = new TextBox();
            label9 = new Label();
            label8 = new Label();
            txtTelefonoContacto = new TextBox();
            roundedButton4 = new Presentacion.Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabListTitulares.SuspendLayout();
            panelBusqueda.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgTitulares).BeginInit();
            tabTitularDetail.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabListTitulares);
            tabControl1.Controls.Add(tabTitularDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 12F);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(849, 600);
            tabControl1.TabIndex = 1;
            // 
            // tabListTitulares
            // 
            tabListTitulares.AutoScroll = true;
            tabListTitulares.Controls.Add(btnEliminarTitular);
            tabListTitulares.Controls.Add(btnLast);
            tabListTitulares.Controls.Add(btnNext);
            tabListTitulares.Controls.Add(btnPrev);
            tabListTitulares.Controls.Add(btnFirst);
            tabListTitulares.Controls.Add(lblTotalPages);
            tabListTitulares.Controls.Add(label10);
            tabListTitulares.Controls.Add(lblCurrentPage);
            tabListTitulares.Controls.Add(lblTotalRows);
            tabListTitulares.Controls.Add(label2);
            tabListTitulares.Controls.Add(label1);
            tabListTitulares.Controls.Add(panelBusqueda);
            tabListTitulares.Controls.Add(ibtnEditar);
            tabListTitulares.Controls.Add(ibtnAgregar);
            tabListTitulares.Controls.Add(panel1);
            tabListTitulares.Location = new Point(4, 30);
            tabListTitulares.Margin = new Padding(3, 2, 3, 2);
            tabListTitulares.Name = "tabListTitulares";
            tabListTitulares.Padding = new Padding(3, 2, 3, 2);
            tabListTitulares.Size = new Size(841, 566);
            tabListTitulares.TabIndex = 0;
            tabListTitulares.UseVisualStyleBackColor = true;
            // 
            // btnEliminarTitular
            // 
            btnEliminarTitular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminarTitular.BackColor = Color.FromArgb(229, 115, 115);
            btnEliminarTitular.FlatAppearance.BorderSize = 0;
            btnEliminarTitular.FlatStyle = FlatStyle.Flat;
            btnEliminarTitular.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnEliminarTitular.ForeColor = Color.White;
            btnEliminarTitular.IconChar = FontAwesome.Sharp.IconChar.Trash;
            btnEliminarTitular.IconColor = Color.White;
            btnEliminarTitular.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEliminarTitular.IconSize = 25;
            btnEliminarTitular.ImageAlign = ContentAlignment.MiddleRight;
            btnEliminarTitular.Location = new Point(657, 305);
            btnEliminarTitular.Name = "btnEliminarTitular";
            btnEliminarTitular.Size = new Size(160, 49);
            btnEliminarTitular.TabIndex = 85;
            btnEliminarTitular.Text = "ELIMINAR";
            btnEliminarTitular.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminarTitular.UseVisualStyleBackColor = false;
            btnEliminarTitular.Click += btnEliminarAgente_Click;
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
            btnLast.Location = new Point(560, 593);
            btnLast.Margin = new Padding(3, 2, 3, 2);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(91, 33);
            btnLast.TabIndex = 73;
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
            btnNext.Location = new Point(455, 594);
            btnNext.Margin = new Padding(3, 2, 3, 2);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(91, 33);
            btnNext.TabIndex = 72;
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
            btnPrev.Location = new Point(348, 595);
            btnPrev.Margin = new Padding(3, 2, 3, 2);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(91, 33);
            btnPrev.TabIndex = 71;
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
            btnFirst.Location = new Point(225, 595);
            btnFirst.Margin = new Padding(3, 2, 3, 2);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(104, 33);
            btnFirst.TabIndex = 70;
            btnFirst.Text = "PRIMERA";
            btnFirst.UseVisualStyleBackColor = false;
            btnFirst.Click += btnFirst_Click;
            // 
            // lblTotalPages
            // 
            lblTotalPages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalPages.AutoSize = true;
            lblTotalPages.Font = new Font("Century Gothic", 9F);
            lblTotalPages.Location = new Point(619, 161);
            lblTotalPages.Name = "lblTotalPages";
            lblTotalPages.Size = new Size(15, 17);
            lblTotalPages.TabIndex = 69;
            lblTotalPages.Text = "0";
            lblTotalPages.Click += lblTotalPages_Click;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 9F);
            label10.Location = new Point(589, 161);
            label10.Name = "label10";
            label10.Size = new Size(24, 17);
            label10.TabIndex = 68;
            label10.Text = "de";
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCurrentPage.AutoSize = true;
            lblCurrentPage.Font = new Font("Century Gothic", 9F);
            lblCurrentPage.Location = new Point(558, 161);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(15, 17);
            lblCurrentPage.TabIndex = 67;
            lblCurrentPage.Text = "0";
            // 
            // lblTotalRows
            // 
            lblTotalRows.AutoSize = true;
            lblTotalRows.Font = new Font("Century Gothic", 9F);
            lblTotalRows.Location = new Point(134, 161);
            lblTotalRows.Name = "lblTotalRows";
            lblTotalRows.Size = new Size(15, 17);
            lblTotalRows.TabIndex = 66;
            lblTotalRows.Text = "0";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F);
            label2.Location = new Point(502, 161);
            label2.Name = "label2";
            label2.Size = new Size(49, 17);
            label2.TabIndex = 65;
            label2.Text = "Página";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 9F);
            label1.Location = new Point(8, 161);
            label1.Name = "label1";
            label1.Size = new Size(115, 17);
            label1.TabIndex = 64;
            label1.Text = "Total de registros: ";
            // 
            // panelBusqueda
            // 
            panelBusqueda.Controls.Add(iconButton6);
            panelBusqueda.Controls.Add(iconButton1);
            panelBusqueda.Controls.Add(txtBuscar);
            panelBusqueda.Controls.Add(roundedButton5);
            panelBusqueda.Controls.Add(roundedButton3);
            panelBusqueda.Location = new Point(0, 0);
            panelBusqueda.Name = "panelBusqueda";
            panelBusqueda.Size = new Size(820, 145);
            panelBusqueda.TabIndex = 63;
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
            iconButton6.Location = new Point(436, 96);
            iconButton6.Margin = new Padding(3, 2, 3, 2);
            iconButton6.Name = "iconButton6";
            iconButton6.Size = new Size(22, 27);
            iconButton6.TabIndex = 61;
            iconButton6.UseVisualStyleBackColor = false;
            iconButton6.Click += iconButton6_Click;
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.Top;
            iconButton1.BackColor = Color.FromArgb(251, 140, 0);
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            iconButton1.ForeColor = Color.White;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 18;
            iconButton1.ImageAlign = ContentAlignment.MiddleRight;
            iconButton1.Location = new Point(464, 96);
            iconButton1.Margin = new Padding(3, 2, 3, 2);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(107, 27);
            iconButton1.TabIndex = 58;
            iconButton1.Text = "BUSCAR";
            iconButton1.TextAlign = ContentAlignment.MiddleLeft;
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top;
            txtBuscar.Location = new Point(168, 96);
            txtBuscar.Margin = new Padding(3, 2, 3, 2);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(264, 27);
            txtBuscar.TabIndex = 57;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.Top;
            roundedButton5.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderColor = Color.FromArgb(196, 195, 209);
            roundedButton5.BorderRadius = 40;
            roundedButton5.BorderSize = 0;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.Font = new Font("Century Gothic", 15F);
            roundedButton5.ForeColor = Color.Black;
            roundedButton5.Image = Properties.Resources.legal_1_;
            roundedButton5.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton5.Location = new Point(144, 7);
            roundedButton5.Margin = new Padding(3, 2, 3, 2);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(462, 46);
            roundedButton5.TabIndex = 60;
            roundedButton5.Text = "TITULARES";
            roundedButton5.TextColor = Color.Black;
            roundedButton5.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton5.UseVisualStyleBackColor = false;
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
            roundedButton3.Location = new Point(144, 79);
            roundedButton3.Margin = new Padding(3, 2, 3, 2);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(462, 56);
            roundedButton3.TabIndex = 59;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            roundedButton3.Click += roundedButton3_Click;
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
            ibtnEditar.IconSize = 25;
            ibtnEditar.ImageAlign = ContentAlignment.MiddleRight;
            ibtnEditar.Location = new Point(657, 242);
            ibtnEditar.Margin = new Padding(3, 2, 3, 2);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(160, 49);
            ibtnEditar.TabIndex = 10;
            ibtnEditar.Text = "EDITAR/ VER";
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
            ibtnAgregar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            ibtnAgregar.ForeColor = Color.White;
            ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            ibtnAgregar.IconColor = Color.White;
            ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnAgregar.IconSize = 25;
            ibtnAgregar.ImageAlign = ContentAlignment.MiddleRight;
            ibtnAgregar.Location = new Point(658, 180);
            ibtnAgregar.Margin = new Padding(3, 2, 3, 2);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(160, 49);
            ibtnAgregar.TabIndex = 9;
            ibtnAgregar.Text = "AGREGAR";
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnAgregar_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgTitulares);
            panel1.Location = new Point(7, 180);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(644, 404);
            panel1.TabIndex = 62;
            // 
            // dtgTitulares
            // 
            dtgTitulares.AllowUserToAddRows = false;
            dtgTitulares.AllowUserToDeleteRows = false;
            dtgTitulares.AllowUserToResizeRows = false;
            dtgTitulares.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgTitulares.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgTitulares.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgTitulares.BorderStyle = BorderStyle.None;
            dtgTitulares.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgTitulares.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgTitulares.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgTitulares.ColumnHeadersHeight = 40;
            dtgTitulares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtgTitulares.DefaultCellStyle = dataGridViewCellStyle2;
            dtgTitulares.EnableHeadersVisualStyles = false;
            dtgTitulares.GridColor = Color.LightGray;
            dtgTitulares.Location = new Point(15, 11);
            dtgTitulares.Margin = new Padding(3, 2, 3, 2);
            dtgTitulares.MultiSelect = false;
            dtgTitulares.Name = "dtgTitulares";
            dtgTitulares.ReadOnly = true;
            dtgTitulares.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgTitulares.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgTitulares.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 10F);
            dtgTitulares.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dtgTitulares.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgTitulares.Size = new Size(613, 378);
            dtgTitulares.TabIndex = 7;
            dtgTitulares.CellClick += dtgTitulares_CellClick;
            dtgTitulares.CellContentClick += dtgTitulares_CellContentClick;
            dtgTitulares.CellDoubleClick += dtgTitulares_CellDoubleClick;
            // 
            // tabTitularDetail
            // 
            tabTitularDetail.Controls.Add(panel3);
            tabTitularDetail.Location = new Point(4, 30);
            tabTitularDetail.Margin = new Padding(3, 2, 3, 2);
            tabTitularDetail.Name = "tabTitularDetail";
            tabTitularDetail.Padding = new Padding(3, 2, 3, 2);
            tabTitularDetail.Size = new Size(841, 566);
            tabTitularDetail.TabIndex = 1;
            tabTitularDetail.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.Controls.Add(btnCambios);
            panel3.Controls.Add(iconPictureBox3);
            panel3.Controls.Add(roundedButton1);
            panel3.Controls.Add(btnGuardarU);
            panel3.Controls.Add(btnCancelarU);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(txtNombreTitular);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(txtNitTitular);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(txtDireccionTitular);
            panel3.Controls.Add(comboBox1);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(txtNombreContacto);
            panel3.Controls.Add(txtCorreoContacto);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(txtTelefonoContacto);
            panel3.Controls.Add(roundedButton4);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(835, 562);
            panel3.TabIndex = 161;
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
            btnCambios.Font = new Font("Century Gothic", 12F);
            btnCambios.ForeColor = Color.Black;
            btnCambios.Image = Properties.Resources.lapiz;
            btnCambios.ImageAlign = ContentAlignment.MiddleRight;
            btnCambios.Location = new Point(347, 3);
            btnCambios.Name = "btnCambios";
            btnCambios.Size = new Size(141, 49);
            btnCambios.TabIndex = 165;
            btnCambios.Text = "AGREGAR";
            btnCambios.TextColor = Color.Black;
            btnCambios.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCambios.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = Color.Transparent;
            iconPictureBox3.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox3.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 49;
            iconPictureBox3.Location = new Point(279, 3);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(63, 49);
            iconPictureBox3.TabIndex = 164;
            iconPictureBox3.TabStop = false;
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
            roundedButton1.Font = new Font("Century Gothic", 12F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Image = Properties.Resources.legal_1_;
            roundedButton1.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton1.Location = new Point(6, 3);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(267, 49);
            roundedButton1.TabIndex = 162;
            roundedButton1.Text = "TITULARES";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton1.UseVisualStyleBackColor = false;
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
            btnGuardarU.Location = new Point(171, 425);
            btnGuardarU.Margin = new Padding(3, 2, 3, 2);
            btnGuardarU.Name = "btnGuardarU";
            btnGuardarU.Size = new Size(153, 37);
            btnGuardarU.TabIndex = 160;
            btnGuardarU.Text = "GUARDAR";
            btnGuardarU.TextAlign = ContentAlignment.MiddleRight;
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
            btnCancelarU.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnCancelarU.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarU.IconColor = Color.Black;
            btnCancelarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarU.IconSize = 25;
            btnCancelarU.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelarU.Location = new Point(397, 425);
            btnCancelarU.Margin = new Padding(3, 2, 3, 2);
            btnCancelarU.Name = "btnCancelarU";
            btnCancelarU.Size = new Size(153, 37);
            btnCancelarU.TabIndex = 159;
            btnCancelarU.Text = "CANCELAR";
            btnCancelarU.TextAlign = ContentAlignment.MiddleRight;
            btnCancelarU.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelarU.UseVisualStyleBackColor = false;
            btnCancelarU.Click += btnCancelarU_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(222, 227, 234);
            label3.Font = new Font("Century Gothic", 9.75F);
            label3.Location = new Point(100, 170);
            label3.Name = "label3";
            label3.Size = new Size(61, 17);
            label3.TabIndex = 18;
            label3.Text = "Nombre";
            // 
            // txtNombreTitular
            // 
            txtNombreTitular.Anchor = AnchorStyles.Top;
            txtNombreTitular.Font = new Font("Century Gothic", 9.75F);
            txtNombreTitular.Location = new Point(100, 190);
            txtNombreTitular.Margin = new Padding(3, 2, 3, 2);
            txtNombreTitular.Name = "txtNombreTitular";
            txtNombreTitular.Size = new Size(224, 23);
            txtNombreTitular.TabIndex = 1;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(222, 227, 234);
            label4.Font = new Font("Century Gothic", 9.75F);
            label4.Location = new Point(100, 222);
            label4.Name = "label4";
            label4.Size = new Size(26, 17);
            label4.TabIndex = 21;
            label4.Text = "NIT";
            // 
            // txtNitTitular
            // 
            txtNitTitular.Anchor = AnchorStyles.Top;
            txtNitTitular.Font = new Font("Century Gothic", 9.75F);
            txtNitTitular.Location = new Point(100, 242);
            txtNitTitular.Margin = new Padding(3, 2, 3, 2);
            txtNitTitular.Name = "txtNitTitular";
            txtNitTitular.Size = new Size(224, 23);
            txtNitTitular.TabIndex = 3;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(222, 227, 234);
            label5.Font = new Font("Century Gothic", 9.75F);
            label5.Location = new Point(397, 222);
            label5.Name = "label5";
            label5.Size = new Size(33, 17);
            label5.TabIndex = 24;
            label5.Text = "Pais";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(222, 227, 234);
            label6.Font = new Font("Century Gothic", 9.75F);
            label6.Location = new Point(397, 170);
            label6.Name = "label6";
            label6.Size = new Size(69, 17);
            label6.TabIndex = 26;
            label6.Text = "Dirección";
            // 
            // txtDireccionTitular
            // 
            txtDireccionTitular.Anchor = AnchorStyles.Top;
            txtDireccionTitular.Font = new Font("Century Gothic", 9.75F);
            txtDireccionTitular.Location = new Point(397, 190);
            txtDireccionTitular.Margin = new Padding(3, 2, 3, 2);
            txtDireccionTitular.Name = "txtDireccionTitular";
            txtDireccionTitular.Size = new Size(224, 23);
            txtDireccionTitular.TabIndex = 2;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top;
            comboBox1.BackColor = Color.FromArgb(241, 240, 245);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Century Gothic", 9.75F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita", "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Baréin", "Bangladés", "Barbados", "Bielorrusia", "Birmania (Myanmar)", "Burundi", "Bután", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo (Congo-Brazzaville)", "Corea del Norte", "Corea del Sur", "Costa Rica", "Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Eswatini", "Etiopía", "Fiyi", "Filipinas", "Finlandia", "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Grecia", "Granada", "Guatemala", "Guinea", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Holanda", "Hungría", "Islandia", "India", "Indonesia", "Irán", "Irak", "Irlanda", "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kosovo", "Kuwait", "Laos", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein", "Lituania", "Luxemburgo", "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Mauricio", "Mauritania", "México", "Micronesia", "Moldavia", "Mónaco", "Mongolia", "Mozambique", "Namibia", "Nauru", "Nepal", "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Pakistán", "Países Bajos", "Palaos", "Palestina", "Panamá", "Paraguay", "Perú", "Polonia", "Portugal", "Qatar", "República Centroafricana", "República Checa", "República del Congo (Congo-Kinshasa)", "República Dominicana", "Rumania", "Rusia", "Ruanda", "San Cristóbal y Nieves", "San Marino", "Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Tailandia", "Taiwán", "Tanzania", "Tayikistán", "Timor Oriental", "Togo", "Tonga", "Trinidad y Tobago", "Túnez", "Turquía", "Turkmenistán", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabue" });
            comboBox1.Location = new Point(397, 242);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(224, 25);
            comboBox1.TabIndex = 4;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.FromArgb(222, 227, 234);
            label7.Font = new Font("Century Gothic", 9.75F);
            label7.Location = new Point(100, 280);
            label7.Name = "label7";
            label7.Size = new Size(53, 17);
            label7.TabIndex = 28;
            label7.Text = "Correo";
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Anchor = AnchorStyles.Top;
            txtNombreContacto.Font = new Font("Century Gothic", 9.75F);
            txtNombreContacto.Location = new Point(100, 356);
            txtNombreContacto.Margin = new Padding(3, 2, 3, 2);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(224, 23);
            txtNombreContacto.TabIndex = 7;
            // 
            // txtCorreoContacto
            // 
            txtCorreoContacto.Anchor = AnchorStyles.Top;
            txtCorreoContacto.Font = new Font("Century Gothic", 9.75F);
            txtCorreoContacto.Location = new Point(100, 300);
            txtCorreoContacto.Margin = new Padding(3, 2, 3, 2);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(224, 23);
            txtCorreoContacto.TabIndex = 5;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(222, 227, 234);
            label9.Font = new Font("Century Gothic", 9.75F);
            label9.Location = new Point(100, 337);
            label9.Name = "label9";
            label9.Size = new Size(72, 17);
            label9.TabIndex = 31;
            label9.Text = "Contacto";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(222, 227, 234);
            label8.Font = new Font("Century Gothic", 9.75F);
            label8.Location = new Point(397, 280);
            label8.Name = "label8";
            label8.Size = new Size(62, 17);
            label8.TabIndex = 29;
            label8.Text = "Teléfono";
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Anchor = AnchorStyles.Top;
            txtTelefonoContacto.Font = new Font("Century Gothic", 9.75F);
            txtTelefonoContacto.Location = new Point(397, 300);
            txtTelefonoContacto.Margin = new Padding(3, 2, 3, 2);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(224, 23);
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
            roundedButton4.Font = new Font("Century Gothic", 9.75F);
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(31, 111);
            roundedButton4.Margin = new Padding(3, 2, 3, 2);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(657, 407);
            roundedButton4.TabIndex = 145;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            roundedButton4.Click += roundedButton4_Click;
            // 
            // FrmAdministrarTitulares
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(849, 600);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmAdministrarTitulares";
            Text = "FrmAdministrarTitulares";
            Load += FrmAdministrarTitulares_Load;
            Resize += FrmAdministrarTitulares_Resize;
            tabControl1.ResumeLayout(false);
            tabListTitulares.ResumeLayout(false);
            tabListTitulares.PerformLayout();
            panelBusqueda.ResumeLayout(false);
            panelBusqueda.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgTitulares).EndInit();
            tabTitularDetail.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
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
        private FontAwesome.Sharp.IconButton iconButton1;
        private TextBox txtBuscar;
        private Clases.RoundedButton roundedButton5;
        private Clases.RoundedButton roundedButton3;
        private ComboBox comboBox1;
        private Panel panel1;
        private Clases.RoundedButton roundedButton4;
        private FontAwesome.Sharp.IconButton btnGuardarU;
        private FontAwesome.Sharp.IconButton btnCancelarU;
        private Panel panelBusqueda;
        private Panel panel3;
        private Clases.RoundedButton btnCambios;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private Clases.RoundedButton roundedButton1;
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
        private FontAwesome.Sharp.IconButton btnEliminarTitular;
    }
}