namespace Presentacion.Plazos
{
    partial class FrmPlazos
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
            dtgPlazos = new DataGridView();
            panel1 = new Panel();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            txtBuscar = new TextBox();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            tabControl1 = new TabControl();
            tabPageVencimientosList = new TabPage();
            btnIrAReportes = new FontAwesome.Sharp.IconButton();
            panelBusqueda = new Panel();
            label6 = new Label();
            comboBoxTipoFiltro = new ComboBox();
            roundedButton5 = new Presentacion.Clases.RoundedButton();
            iconButton7 = new FontAwesome.Sharp.IconButton();
            panel8 = new Panel();
            btnLast = new FontAwesome.Sharp.IconButton();
            btnNext = new FontAwesome.Sharp.IconButton();
            btnPrev = new FontAwesome.Sharp.IconButton();
            btnFirst = new FontAwesome.Sharp.IconButton();
            lblTotalPages = new Label();
            label2 = new Label();
            lblCurrentPage = new Label();
            lblTotalRows = new Label();
            label3 = new Label();
            label40 = new Label();
            tabPageHistorialDetail = new TabPage();
            dateTimePickerVencimiento = new DateTimePicker();
            labelVenc = new Label();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            richTextBoxAnotaciones = new RichTextBox();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            comboBoxEstado = new ComboBox();
            label1 = new Label();
            dateTimePickerFechaIngreso = new DateTimePicker();
            label4 = new Label();
            label5 = new Label();
            panel9 = new Panel();
            panel11 = new Panel();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            panel13 = new Panel();
            roundedButton2 = new Presentacion.Clases.RoundedButton();
            roundedButton4 = new Presentacion.Clases.RoundedButton();
            tabPageReportes = new TabPage();
            iconButton9 = new FontAwesome.Sharp.IconButton();
            panel16 = new Panel();
            panel17 = new Panel();
            roundedButton7 = new Presentacion.Clases.RoundedButton();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            panel18 = new Panel();
            roundedButton8 = new Presentacion.Clases.RoundedButton();
            panel23 = new Panel();
            dtgReportes = new DataGridView();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel20 = new Panel();
            dtpFechaingresoFinal = new DateTimePicker();
            dtpFIngresoInicial = new DateTimePicker();
            dtpVencimientoInicial = new DateTimePicker();
            dtpVencimientoFinal = new DateTimePicker();
            checkBoxFIngreso = new CheckBox();
            checkBoxVencimiento = new CheckBox();
            richTextBoxAgenteReporte = new RichTextBox();
            btnAgenteReporte = new Presentacion.Clases.RoundedButton();
            chckAgenteReporte = new CheckBox();
            richTextBoxTitularReporte = new RichTextBox();
            btnTitularReporte = new Presentacion.Clases.RoundedButton();
            checkBoxEstadoReporte = new CheckBox();
            chckTitularReporte = new CheckBox();
            txtClaseReporte = new TextBox();
            comboBoxEstadoReporte = new ComboBox();
            chckClaseReporte = new CheckBox();
            txtExpedienteReporte = new TextBox();
            chckExpedienteReporte = new CheckBox();
            txtSignoReporte = new TextBox();
            chckSignoRepo = new CheckBox();
            cmbFiltro = new ComboBox();
            panelBotones = new Panel();
            btnCancelar = new Presentacion.Clases.RoundedButton();
            btnConsultar = new Presentacion.Clases.RoundedButton();
            panelBotones2 = new Panel();
            btnExportarExcel = new Presentacion.Clases.RoundedButton();
            btnExportarPDF = new Presentacion.Clases.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)dtgPlazos).BeginInit();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageVencimientosList.SuspendLayout();
            panelBusqueda.SuspendLayout();
            tabPageHistorialDetail.SuspendLayout();
            panel9.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            panel13.SuspendLayout();
            tabPageReportes.SuspendLayout();
            panel16.SuspendLayout();
            panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            panel18.SuspendLayout();
            panel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgReportes).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            panel20.SuspendLayout();
            panelBotones.SuspendLayout();
            panelBotones2.SuspendLayout();
            SuspendLayout();
            // 
            // dtgPlazos
            // 
            dtgPlazos.AllowUserToAddRows = false;
            dtgPlazos.AllowUserToDeleteRows = false;
            dtgPlazos.AllowUserToOrderColumns = true;
            dtgPlazos.AllowUserToResizeRows = false;
            dtgPlazos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgPlazos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgPlazos.BackgroundColor = Color.White;
            dtgPlazos.BorderStyle = BorderStyle.None;
            dtgPlazos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgPlazos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgPlazos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgPlazos.ColumnHeadersHeight = 40;
            dtgPlazos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgPlazos.EnableHeadersVisualStyles = false;
            dtgPlazos.GridColor = Color.LightGray;
            dtgPlazos.Location = new Point(16, 14);
            dtgPlazos.MultiSelect = false;
            dtgPlazos.Name = "dtgPlazos";
            dtgPlazos.ReadOnly = true;
            dtgPlazos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgPlazos.RowHeadersWidth = 40;
            dtgPlazos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgPlazos.Size = new Size(886, 509);
            dtgPlazos.TabIndex = 4;
            dtgPlazos.CellDoubleClick += dtgVencimientos_CellDoubleClick;
            dtgPlazos.DataBindingComplete += dtgVencimientos_DataBindingComplete;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgPlazos);
            panel1.Location = new Point(21, 198);
            panel1.Name = "panel1";
            panel1.Size = new Size(914, 539);
            panel1.TabIndex = 6;
            // 
            // ibtnBuscar
            // 
            ibtnBuscar.Anchor = AnchorStyles.Top;
            ibtnBuscar.BackColor = Color.FromArgb(251, 140, 0);
            ibtnBuscar.FlatAppearance.BorderSize = 0;
            ibtnBuscar.FlatStyle = FlatStyle.Flat;
            ibtnBuscar.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            ibtnBuscar.ForeColor = Color.White;
            ibtnBuscar.IconChar = FontAwesome.Sharp.IconChar.Search;
            ibtnBuscar.IconColor = Color.White;
            ibtnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnBuscar.IconSize = 18;
            ibtnBuscar.ImageAlign = ContentAlignment.MiddleRight;
            ibtnBuscar.Location = new Point(560, 96);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(107, 27);
            ibtnBuscar.TabIndex = 37;
            ibtnBuscar.Text = "BUSCAR";
            ibtnBuscar.TextAlign = ContentAlignment.MiddleLeft;
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            ibtnBuscar.Click += ibtnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top;
            txtBuscar.Font = new Font("Century Gothic", 9F);
            txtBuscar.Location = new Point(293, 101);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(229, 22);
            txtBuscar.TabIndex = 36;
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
            roundedButton3.Location = new Point(259, 86);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(580, 52);
            roundedButton3.TabIndex = 38;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
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
            ibtnEditar.Location = new Point(941, 198);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(160, 49);
            ibtnEditar.TabIndex = 44;
            ibtnEditar.Text = "EDITAR/VER";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click_1;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageVencimientosList);
            tabControl1.Controls.Add(tabPageHistorialDetail);
            tabControl1.Controls.Add(tabPageReportes);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1132, 743);
            tabControl1.TabIndex = 45;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPageVencimientosList
            // 
            tabPageVencimientosList.AutoScroll = true;
            tabPageVencimientosList.Controls.Add(btnIrAReportes);
            tabPageVencimientosList.Controls.Add(panelBusqueda);
            tabPageVencimientosList.Controls.Add(panel8);
            tabPageVencimientosList.Controls.Add(btnLast);
            tabPageVencimientosList.Controls.Add(btnNext);
            tabPageVencimientosList.Controls.Add(btnPrev);
            tabPageVencimientosList.Controls.Add(btnFirst);
            tabPageVencimientosList.Controls.Add(lblTotalPages);
            tabPageVencimientosList.Controls.Add(label2);
            tabPageVencimientosList.Controls.Add(lblCurrentPage);
            tabPageVencimientosList.Controls.Add(lblTotalRows);
            tabPageVencimientosList.Controls.Add(label3);
            tabPageVencimientosList.Controls.Add(label40);
            tabPageVencimientosList.Controls.Add(panel1);
            tabPageVencimientosList.Controls.Add(ibtnEditar);
            tabPageVencimientosList.Location = new Point(4, 26);
            tabPageVencimientosList.Name = "tabPageVencimientosList";
            tabPageVencimientosList.Padding = new Padding(3);
            tabPageVencimientosList.Size = new Size(1124, 713);
            tabPageVencimientosList.TabIndex = 0;
            tabPageVencimientosList.UseVisualStyleBackColor = true;
            tabPageVencimientosList.Click += tabPageVencimientosList_Click;
            // 
            // btnIrAReportes
            // 
            btnIrAReportes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnIrAReportes.BackColor = Color.FromArgb(38, 166, 154);
            btnIrAReportes.FlatAppearance.BorderSize = 0;
            btnIrAReportes.FlatStyle = FlatStyle.Flat;
            btnIrAReportes.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnIrAReportes.ForeColor = Color.White;
            btnIrAReportes.IconChar = FontAwesome.Sharp.IconChar.ChartSimple;
            btnIrAReportes.IconColor = Color.White;
            btnIrAReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnIrAReportes.IconSize = 25;
            btnIrAReportes.ImageAlign = ContentAlignment.MiddleRight;
            btnIrAReportes.Location = new Point(941, 264);
            btnIrAReportes.Name = "btnIrAReportes";
            btnIrAReportes.Size = new Size(160, 49);
            btnIrAReportes.TabIndex = 234;
            btnIrAReportes.Text = "IR A REPORTES";
            btnIrAReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnIrAReportes.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnIrAReportes.UseVisualStyleBackColor = false;
            btnIrAReportes.Click += btnIrAReportes_Click;
            // 
            // panelBusqueda
            // 
            panelBusqueda.Controls.Add(label6);
            panelBusqueda.Controls.Add(comboBoxTipoFiltro);
            panelBusqueda.Controls.Add(roundedButton5);
            panelBusqueda.Controls.Add(txtBuscar);
            panelBusqueda.Controls.Add(ibtnBuscar);
            panelBusqueda.Controls.Add(iconButton7);
            panelBusqueda.Controls.Add(roundedButton3);
            panelBusqueda.Location = new Point(3, 6);
            panelBusqueda.Name = "panelBusqueda";
            panelBusqueda.Size = new Size(1098, 150);
            panelBusqueda.TabIndex = 233;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(236, 236, 238);
            label6.Location = new Point(293, 86);
            label6.Name = "label6";
            label6.Size = new Size(122, 17);
            label6.TabIndex = 223;
            label6.Text = "Expediente o Signo";
            // 
            // comboBoxTipoFiltro
            // 
            comboBoxTipoFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTipoFiltro.FormattingEnabled = true;
            comboBoxTipoFiltro.Items.AddRange(new object[] { "MARCAS", "PATENTES" });
            comboBoxTipoFiltro.Location = new Point(685, 98);
            comboBoxTipoFiltro.Name = "comboBoxTipoFiltro";
            comboBoxTipoFiltro.Size = new Size(121, 25);
            comboBoxTipoFiltro.TabIndex = 222;
            comboBoxTipoFiltro.SelectedIndexChanged += comboBoxTipoFiltro_SelectedIndexChanged;
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.Top;
            roundedButton5.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton5.BorderRadius = 40;
            roundedButton5.BorderSize = 0;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.Font = new Font("Century Gothic", 15F);
            roundedButton5.ForeColor = Color.Black;
            roundedButton5.Image = Properties.Resources.hourglass__2_;
            roundedButton5.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton5.Location = new Point(207, 3);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(683, 61);
            roundedButton5.TabIndex = 39;
            roundedButton5.Text = "PLAZOS";
            roundedButton5.TextColor = Color.Black;
            roundedButton5.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // iconButton7
            // 
            iconButton7.Anchor = AnchorStyles.Top;
            iconButton7.BackColor = Color.FromArgb(236, 236, 238);
            iconButton7.FlatAppearance.BorderSize = 0;
            iconButton7.FlatStyle = FlatStyle.Flat;
            iconButton7.IconChar = FontAwesome.Sharp.IconChar.Close;
            iconButton7.IconColor = Color.Black;
            iconButton7.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton7.IconSize = 25;
            iconButton7.Location = new Point(528, 101);
            iconButton7.Name = "iconButton7";
            iconButton7.Size = new Size(26, 22);
            iconButton7.TabIndex = 221;
            iconButton7.UseVisualStyleBackColor = false;
            iconButton7.Click += iconButton7_Click;
            // 
            // panel8
            // 
            panel8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel8.Location = new Point(-1188, 828);
            panel8.Name = "panel8";
            panel8.Size = new Size(130, 27);
            panel8.TabIndex = 228;
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
            btnLast.Location = new Point(831, 743);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(104, 31);
            btnLast.TabIndex = 232;
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
            btnNext.Location = new Point(721, 743);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(104, 31);
            btnNext.TabIndex = 231;
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
            btnPrev.Location = new Point(611, 743);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(104, 31);
            btnPrev.TabIndex = 230;
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
            btnFirst.Location = new Point(501, 743);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(104, 31);
            btnFirst.TabIndex = 229;
            btnFirst.Text = "PRIMERA";
            btnFirst.UseVisualStyleBackColor = false;
            btnFirst.Click += btnFirst_Click;
            // 
            // lblTotalPages
            // 
            lblTotalPages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalPages.AutoSize = true;
            lblTotalPages.Font = new Font("Century Gothic", 9F);
            lblTotalPages.Location = new Point(908, 175);
            lblTotalPages.Name = "lblTotalPages";
            lblTotalPages.Size = new Size(15, 17);
            lblTotalPages.TabIndex = 227;
            lblTotalPages.Text = "0";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F);
            label2.Location = new Point(873, 175);
            label2.Name = "label2";
            label2.Size = new Size(24, 17);
            label2.TabIndex = 226;
            label2.Text = "de";
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCurrentPage.AutoSize = true;
            lblCurrentPage.Font = new Font("Century Gothic", 9F);
            lblCurrentPage.Location = new Point(839, 175);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(15, 17);
            lblCurrentPage.TabIndex = 225;
            lblCurrentPage.Text = "0";
            // 
            // lblTotalRows
            // 
            lblTotalRows.AutoSize = true;
            lblTotalRows.Font = new Font("Century Gothic", 9F);
            lblTotalRows.Location = new Point(163, 175);
            lblTotalRows.Name = "lblTotalRows";
            lblTotalRows.Size = new Size(15, 17);
            lblTotalRows.TabIndex = 224;
            lblTotalRows.Text = "0";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 9F);
            label3.Location = new Point(773, 175);
            label3.Name = "label3";
            label3.Size = new Size(49, 17);
            label3.TabIndex = 223;
            label3.Text = "Página";
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Font = new Font("Century Gothic", 9F);
            label40.Location = new Point(19, 175);
            label40.Name = "label40";
            label40.Size = new Size(115, 17);
            label40.TabIndex = 222;
            label40.Text = "Total de registros: ";
            // 
            // tabPageHistorialDetail
            // 
            tabPageHistorialDetail.AutoScroll = true;
            tabPageHistorialDetail.Controls.Add(dateTimePickerVencimiento);
            tabPageHistorialDetail.Controls.Add(labelVenc);
            tabPageHistorialDetail.Controls.Add(iconButton2);
            tabPageHistorialDetail.Controls.Add(richTextBoxAnotaciones);
            tabPageHistorialDetail.Controls.Add(iconButton3);
            tabPageHistorialDetail.Controls.Add(comboBoxEstado);
            tabPageHistorialDetail.Controls.Add(label1);
            tabPageHistorialDetail.Controls.Add(dateTimePickerFechaIngreso);
            tabPageHistorialDetail.Controls.Add(label4);
            tabPageHistorialDetail.Controls.Add(label5);
            tabPageHistorialDetail.Controls.Add(panel9);
            tabPageHistorialDetail.Controls.Add(roundedButton4);
            tabPageHistorialDetail.Location = new Point(4, 24);
            tabPageHistorialDetail.Name = "tabPageHistorialDetail";
            tabPageHistorialDetail.Padding = new Padding(3);
            tabPageHistorialDetail.Size = new Size(1124, 715);
            tabPageHistorialDetail.TabIndex = 1;
            tabPageHistorialDetail.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerVencimiento
            // 
            dateTimePickerVencimiento.Anchor = AnchorStyles.Top;
            dateTimePickerVencimiento.Format = DateTimePickerFormat.Short;
            dateTimePickerVencimiento.Location = new Point(558, 264);
            dateTimePickerVencimiento.Name = "dateTimePickerVencimiento";
            dateTimePickerVencimiento.Size = new Size(154, 22);
            dateTimePickerVencimiento.TabIndex = 201;
            dateTimePickerVencimiento.ValueChanged += dateTimePickerVencimiento_ValueChanged;
            // 
            // labelVenc
            // 
            labelVenc.Anchor = AnchorStyles.Top;
            labelVenc.AutoSize = true;
            labelVenc.BackColor = Color.FromArgb(222, 227, 234);
            labelVenc.Location = new Point(558, 241);
            labelVenc.Name = "labelVenc";
            labelVenc.Size = new Size(144, 17);
            labelVenc.TabIndex = 205;
            labelVenc.Text = "Fecha de Vencimiento";
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Top;
            iconButton2.BackColor = Color.White;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton2.Location = new Point(560, 457);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(152, 36);
            iconButton2.TabIndex = 204;
            iconButton2.Text = "CANCELAR";
            iconButton2.TextAlign = ContentAlignment.MiddleRight;
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // richTextBoxAnotaciones
            // 
            richTextBoxAnotaciones.Anchor = AnchorStyles.Top;
            richTextBoxAnotaciones.BorderStyle = BorderStyle.None;
            richTextBoxAnotaciones.Location = new Point(354, 331);
            richTextBoxAnotaciones.Name = "richTextBoxAnotaciones";
            richTextBoxAnotaciones.Size = new Size(358, 102);
            richTextBoxAnotaciones.TabIndex = 202;
            richTextBoxAnotaciones.Text = "";
            // 
            // iconButton3
            // 
            iconButton3.Anchor = AnchorStyles.Top;
            iconButton3.BackColor = Color.FromArgb(96, 149, 241);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Pen;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 25;
            iconButton3.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton3.Location = new Point(354, 457);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(152, 36);
            iconButton3.TabIndex = 203;
            iconButton3.Text = "EDITAR";
            iconButton3.TextAlign = ContentAlignment.MiddleRight;
            iconButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // comboBoxEstado
            // 
            comboBoxEstado.Anchor = AnchorStyles.Top;
            comboBoxEstado.BackColor = Color.White;
            comboBoxEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEstado.FlatStyle = FlatStyle.Flat;
            comboBoxEstado.FormattingEnabled = true;
            comboBoxEstado.Items.AddRange(new object[] { "Ingresada", "Examen de forma", "Examen de fondo", "Requerimiento", "Objeción", "Resolución RPI favorable", "Resolución RPI desfavorable", "Recurso de revocatoria", "Resolución Ministerio de Economía (MINECO)", "Contencioso administrativo", "Edicto", "Publicación", "Orden de pago", "Registrada", "Licencia de uso" });
            comboBoxEstado.Location = new Point(354, 190);
            comboBoxEstado.Name = "comboBoxEstado";
            comboBoxEstado.Size = new Size(358, 25);
            comboBoxEstado.TabIndex = 199;
            comboBoxEstado.SelectedIndexChanged += comboBoxEstado_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(222, 227, 234);
            label1.Location = new Point(354, 241);
            label1.Name = "label1";
            label1.Size = new Size(110, 17);
            label1.TabIndex = 196;
            label1.Text = "Fecha de Ingreso";
            // 
            // dateTimePickerFechaIngreso
            // 
            dateTimePickerFechaIngreso.Anchor = AnchorStyles.Top;
            dateTimePickerFechaIngreso.Format = DateTimePickerFormat.Short;
            dateTimePickerFechaIngreso.Location = new Point(354, 264);
            dateTimePickerFechaIngreso.Name = "dateTimePickerFechaIngreso";
            dateTimePickerFechaIngreso.Size = new Size(154, 22);
            dateTimePickerFechaIngreso.TabIndex = 197;
            dateTimePickerFechaIngreso.ValueChanged += dateTimePickerFechaIngreso_ValueChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(222, 227, 234);
            label4.Location = new Point(354, 167);
            label4.Name = "label4";
            label4.Size = new Size(48, 17);
            label4.TabIndex = 198;
            label4.Text = "Estado";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(222, 227, 234);
            label5.Location = new Point(354, 311);
            label5.Name = "label5";
            label5.Size = new Size(84, 17);
            label5.TabIndex = 200;
            label5.Text = "Anotaciones";
            // 
            // panel9
            // 
            panel9.AutoSize = true;
            panel9.Controls.Add(panel11);
            panel9.Controls.Add(iconPictureBox1);
            panel9.Controls.Add(panel13);
            panel9.Location = new Point(8, 6);
            panel9.Name = "panel9";
            panel9.Size = new Size(431, 49);
            panel9.TabIndex = 195;
            // 
            // panel11
            // 
            panel11.AutoSize = true;
            panel11.Controls.Add(roundedButton1);
            panel11.Dock = DockStyle.Left;
            panel11.Location = new Point(253, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(162, 49);
            panel11.TabIndex = 170;
            // 
            // roundedButton1
            // 
            roundedButton1.AutoSize = true;
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.Dock = DockStyle.Fill;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(0, 0);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(162, 49);
            roundedButton1.TabIndex = 167;
            roundedButton1.Text = "DETALLE";
            roundedButton1.TextColor = Color.Black;
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
            iconPictureBox1.Location = new Point(204, 0);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(49, 49);
            iconPictureBox1.TabIndex = 166;
            iconPictureBox1.TabStop = false;
            iconPictureBox1.UseGdi = true;
            // 
            // panel13
            // 
            panel13.AutoSize = true;
            panel13.Controls.Add(roundedButton2);
            panel13.Dock = DockStyle.Left;
            panel13.Location = new Point(0, 0);
            panel13.Name = "panel13";
            panel13.Size = new Size(204, 49);
            panel13.TabIndex = 169;
            // 
            // roundedButton2
            // 
            roundedButton2.AutoSize = true;
            roundedButton2.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton2.BorderRadius = 40;
            roundedButton2.BorderSize = 0;
            roundedButton2.Dock = DockStyle.Fill;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Century Gothic", 9F);
            roundedButton2.ForeColor = Color.Black;
            roundedButton2.Image = Properties.Resources.hourglass__2_;
            roundedButton2.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton2.Location = new Point(0, 0);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(204, 49);
            roundedButton2.TabIndex = 164;
            roundedButton2.Text = "PLAZOS";
            roundedButton2.TextAlign = ContentAlignment.MiddleLeft;
            roundedButton2.TextColor = Color.Black;
            roundedButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton2.UseVisualStyleBackColor = false;
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
            roundedButton4.Location = new Point(212, 140);
            roundedButton4.Margin = new Padding(3, 2, 3, 2);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(631, 399);
            roundedButton4.TabIndex = 206;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // tabPageReportes
            // 
            tabPageReportes.AutoScroll = true;
            tabPageReportes.Controls.Add(iconButton9);
            tabPageReportes.Controls.Add(panel16);
            tabPageReportes.Controls.Add(panel23);
            tabPageReportes.Controls.Add(tableLayoutPanel2);
            tabPageReportes.Controls.Add(panelBotones);
            tabPageReportes.Controls.Add(panelBotones2);
            tabPageReportes.Location = new Point(4, 26);
            tabPageReportes.Name = "tabPageReportes";
            tabPageReportes.Size = new Size(1124, 713);
            tabPageReportes.TabIndex = 2;
            tabPageReportes.UseVisualStyleBackColor = true;
            // 
            // iconButton9
            // 
            iconButton9.BackColor = Color.White;
            iconButton9.FlatAppearance.BorderSize = 0;
            iconButton9.FlatStyle = FlatStyle.Flat;
            iconButton9.IconChar = FontAwesome.Sharp.IconChar.AngleLeft;
            iconButton9.IconColor = Color.Black;
            iconButton9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton9.Location = new Point(18, 69);
            iconButton9.Name = "iconButton9";
            iconButton9.Size = new Size(62, 40);
            iconButton9.TabIndex = 258;
            iconButton9.UseVisualStyleBackColor = false;
            iconButton9.Click += iconButton9_Click_1;
            // 
            // panel16
            // 
            panel16.AutoSize = true;
            panel16.Controls.Add(panel17);
            panel16.Controls.Add(iconPictureBox3);
            panel16.Controls.Add(panel18);
            panel16.Location = new Point(3, 3);
            panel16.Name = "panel16";
            panel16.Size = new Size(505, 49);
            panel16.TabIndex = 257;
            // 
            // panel17
            // 
            panel17.AutoSize = true;
            panel17.Controls.Add(roundedButton7);
            panel17.Dock = DockStyle.Left;
            panel17.Location = new Point(249, 0);
            panel17.Name = "panel17";
            panel17.Size = new Size(245, 49);
            panel17.TabIndex = 170;
            // 
            // roundedButton7
            // 
            roundedButton7.AutoSize = true;
            roundedButton7.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton7.BorderRadius = 40;
            roundedButton7.BorderSize = 0;
            roundedButton7.Dock = DockStyle.Fill;
            roundedButton7.FlatAppearance.BorderSize = 0;
            roundedButton7.FlatStyle = FlatStyle.Flat;
            roundedButton7.Font = new Font("Century Gothic", 9F);
            roundedButton7.ForeColor = Color.Black;
            roundedButton7.Location = new Point(0, 0);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(245, 49);
            roundedButton7.TabIndex = 167;
            roundedButton7.Text = "REPORTES";
            roundedButton7.TextColor = Color.Black;
            roundedButton7.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = Color.Transparent;
            iconPictureBox3.Dock = DockStyle.Left;
            iconPictureBox3.ForeColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            iconPictureBox3.IconColor = Color.FromArgb(1, 87, 155);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 49;
            iconPictureBox3.Location = new Point(200, 0);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(49, 49);
            iconPictureBox3.TabIndex = 166;
            iconPictureBox3.TabStop = false;
            iconPictureBox3.UseGdi = true;
            // 
            // panel18
            // 
            panel18.AutoSize = true;
            panel18.Controls.Add(roundedButton8);
            panel18.Dock = DockStyle.Left;
            panel18.Location = new Point(0, 0);
            panel18.Name = "panel18";
            panel18.Size = new Size(200, 49);
            panel18.TabIndex = 169;
            // 
            // roundedButton8
            // 
            roundedButton8.AutoSize = true;
            roundedButton8.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton8.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton8.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton8.BorderRadius = 40;
            roundedButton8.BorderSize = 0;
            roundedButton8.Dock = DockStyle.Fill;
            roundedButton8.FlatAppearance.BorderSize = 0;
            roundedButton8.FlatStyle = FlatStyle.Flat;
            roundedButton8.Font = new Font("Century Gothic", 9F);
            roundedButton8.ForeColor = Color.Black;
            roundedButton8.Image = Properties.Resources.hourglass__2_;
            roundedButton8.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton8.Location = new Point(0, 0);
            roundedButton8.Name = "roundedButton8";
            roundedButton8.Size = new Size(200, 49);
            roundedButton8.TabIndex = 164;
            roundedButton8.Text = "PLAZOS";
            roundedButton8.TextColor = Color.Black;
            roundedButton8.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton8.UseVisualStyleBackColor = false;
            // 
            // panel23
            // 
            panel23.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel23.BackColor = Color.FromArgb(192, 202, 212);
            panel23.Controls.Add(dtgReportes);
            panel23.Location = new Point(125, 703);
            panel23.MinimumSize = new Size(705, 462);
            panel23.Name = "panel23";
            panel23.Size = new Size(705, 516);
            panel23.TabIndex = 254;
            // 
            // dtgReportes
            // 
            dtgReportes.AllowUserToAddRows = false;
            dtgReportes.AllowUserToDeleteRows = false;
            dtgReportes.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgReportes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            dtgReportes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dtgReportes.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgReportes.BorderStyle = BorderStyle.None;
            dtgReportes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgReportes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgReportes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgReportes.ColumnHeadersHeight = 40;
            dtgReportes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgReportes.EnableHeadersVisualStyles = false;
            dtgReportes.GridColor = Color.LightGray;
            dtgReportes.Location = new Point(15, 15);
            dtgReportes.Name = "dtgReportes";
            dtgReportes.ReadOnly = true;
            dtgReportes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgReportes.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9F);
            dtgReportes.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dtgReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgReportes.Size = new Size(677, 483);
            dtgReportes.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.BackColor = Color.FromArgb(222, 227, 234);
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(panel20, 0, 0);
            tableLayoutPanel2.Location = new Point(125, 104);
            tableLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel2.MinimumSize = new Size(705, 462);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(705, 462);
            tableLayoutPanel2.TabIndex = 253;
            // 
            // panel20
            // 
            panel20.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel20.AutoScroll = true;
            panel20.BackColor = Color.FromArgb(222, 227, 234);
            panel20.Controls.Add(dtpFechaingresoFinal);
            panel20.Controls.Add(dtpFIngresoInicial);
            panel20.Controls.Add(dtpVencimientoInicial);
            panel20.Controls.Add(dtpVencimientoFinal);
            panel20.Controls.Add(checkBoxFIngreso);
            panel20.Controls.Add(checkBoxVencimiento);
            panel20.Controls.Add(richTextBoxAgenteReporte);
            panel20.Controls.Add(btnAgenteReporte);
            panel20.Controls.Add(chckAgenteReporte);
            panel20.Controls.Add(richTextBoxTitularReporte);
            panel20.Controls.Add(btnTitularReporte);
            panel20.Controls.Add(checkBoxEstadoReporte);
            panel20.Controls.Add(chckTitularReporte);
            panel20.Controls.Add(txtClaseReporte);
            panel20.Controls.Add(comboBoxEstadoReporte);
            panel20.Controls.Add(chckClaseReporte);
            panel20.Controls.Add(txtExpedienteReporte);
            panel20.Controls.Add(chckExpedienteReporte);
            panel20.Controls.Add(txtSignoReporte);
            panel20.Controls.Add(chckSignoRepo);
            panel20.Controls.Add(cmbFiltro);
            panel20.Location = new Point(3, 4);
            panel20.Margin = new Padding(3, 4, 3, 4);
            panel20.Name = "panel20";
            panel20.Size = new Size(699, 452);
            panel20.TabIndex = 243;
            // 
            // dtpFechaingresoFinal
            // 
            dtpFechaingresoFinal.Anchor = AnchorStyles.Top;
            dtpFechaingresoFinal.Format = DateTimePickerFormat.Short;
            dtpFechaingresoFinal.Location = new Point(538, 98);
            dtpFechaingresoFinal.Margin = new Padding(3, 2, 3, 2);
            dtpFechaingresoFinal.Name = "dtpFechaingresoFinal";
            dtpFechaingresoFinal.Size = new Size(103, 22);
            dtpFechaingresoFinal.TabIndex = 247;
            // 
            // dtpFIngresoInicial
            // 
            dtpFIngresoInicial.Anchor = AnchorStyles.Top;
            dtpFIngresoInicial.Format = DateTimePickerFormat.Short;
            dtpFIngresoInicial.Location = new Point(381, 98);
            dtpFIngresoInicial.Margin = new Padding(3, 2, 3, 2);
            dtpFIngresoInicial.Name = "dtpFIngresoInicial";
            dtpFIngresoInicial.Size = new Size(103, 22);
            dtpFIngresoInicial.TabIndex = 245;
            // 
            // dtpVencimientoInicial
            // 
            dtpVencimientoInicial.Anchor = AnchorStyles.Top;
            dtpVencimientoInicial.Format = DateTimePickerFormat.Short;
            dtpVencimientoInicial.Location = new Point(381, 172);
            dtpVencimientoInicial.Margin = new Padding(3, 2, 3, 2);
            dtpVencimientoInicial.Name = "dtpVencimientoInicial";
            dtpVencimientoInicial.Size = new Size(103, 22);
            dtpVencimientoInicial.TabIndex = 246;
            // 
            // dtpVencimientoFinal
            // 
            dtpVencimientoFinal.Anchor = AnchorStyles.Top;
            dtpVencimientoFinal.Format = DateTimePickerFormat.Short;
            dtpVencimientoFinal.Location = new Point(538, 172);
            dtpVencimientoFinal.Margin = new Padding(3, 2, 3, 2);
            dtpVencimientoFinal.Name = "dtpVencimientoFinal";
            dtpVencimientoFinal.Size = new Size(103, 22);
            dtpVencimientoFinal.TabIndex = 248;
            // 
            // checkBoxFIngreso
            // 
            checkBoxFIngreso.Anchor = AnchorStyles.Top;
            checkBoxFIngreso.AutoSize = true;
            checkBoxFIngreso.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxFIngreso.Location = new Point(382, 68);
            checkBoxFIngreso.Margin = new Padding(3, 2, 3, 2);
            checkBoxFIngreso.Name = "checkBoxFIngreso";
            checkBoxFIngreso.Size = new Size(110, 21);
            checkBoxFIngreso.TabIndex = 243;
            checkBoxFIngreso.Text = "Fecha Ingreso";
            checkBoxFIngreso.UseVisualStyleBackColor = false;
            // 
            // checkBoxVencimiento
            // 
            checkBoxVencimiento.Anchor = AnchorStyles.Top;
            checkBoxVencimiento.AutoSize = true;
            checkBoxVencimiento.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxVencimiento.Location = new Point(382, 144);
            checkBoxVencimiento.Margin = new Padding(3, 2, 3, 2);
            checkBoxVencimiento.Name = "checkBoxVencimiento";
            checkBoxVencimiento.Size = new Size(144, 21);
            checkBoxVencimiento.TabIndex = 244;
            checkBoxVencimiento.Text = "Fecha Vencimiento";
            checkBoxVencimiento.UseVisualStyleBackColor = false;
            // 
            // richTextBoxAgenteReporte
            // 
            richTextBoxAgenteReporte.Anchor = AnchorStyles.Top;
            richTextBoxAgenteReporte.BackColor = Color.White;
            richTextBoxAgenteReporte.Location = new Point(414, 370);
            richTextBoxAgenteReporte.Name = "richTextBoxAgenteReporte";
            richTextBoxAgenteReporte.Size = new Size(238, 47);
            richTextBoxAgenteReporte.TabIndex = 242;
            richTextBoxAgenteReporte.Text = "";
            // 
            // btnAgenteReporte
            // 
            btnAgenteReporte.Anchor = AnchorStyles.Top;
            btnAgenteReporte.BackColor = Color.LightSteelBlue;
            btnAgenteReporte.BackgroundColor = Color.LightSteelBlue;
            btnAgenteReporte.BorderColor = Color.LightSteelBlue;
            btnAgenteReporte.BorderRadius = 26;
            btnAgenteReporte.BorderSize = 0;
            btnAgenteReporte.FlatAppearance.BorderSize = 0;
            btnAgenteReporte.FlatStyle = FlatStyle.Flat;
            btnAgenteReporte.Font = new Font("Century Gothic", 10F);
            btnAgenteReporte.ForeColor = Color.Black;
            btnAgenteReporte.Location = new Point(437, 329);
            btnAgenteReporte.Name = "btnAgenteReporte";
            btnAgenteReporte.Size = new Size(194, 35);
            btnAgenteReporte.TabIndex = 241;
            btnAgenteReporte.Text = "+ AGENTE";
            btnAgenteReporte.TextColor = Color.Black;
            btnAgenteReporte.UseVisualStyleBackColor = false;
            btnAgenteReporte.Click += btnAgenteReporte_Click;
            // 
            // chckAgenteReporte
            // 
            chckAgenteReporte.Anchor = AnchorStyles.Top;
            chckAgenteReporte.AutoSize = true;
            chckAgenteReporte.BackColor = Color.FromArgb(222, 227, 234);
            chckAgenteReporte.Location = new Point(391, 299);
            chckAgenteReporte.Name = "chckAgenteReporte";
            chckAgenteReporte.Size = new Size(72, 21);
            chckAgenteReporte.TabIndex = 240;
            chckAgenteReporte.Text = "Agente";
            chckAgenteReporte.UseVisualStyleBackColor = false;
            // 
            // richTextBoxTitularReporte
            // 
            richTextBoxTitularReporte.Anchor = AnchorStyles.Top;
            richTextBoxTitularReporte.BackColor = Color.White;
            richTextBoxTitularReporte.Location = new Point(100, 370);
            richTextBoxTitularReporte.Name = "richTextBoxTitularReporte";
            richTextBoxTitularReporte.Size = new Size(238, 47);
            richTextBoxTitularReporte.TabIndex = 235;
            richTextBoxTitularReporte.Text = "";
            // 
            // btnTitularReporte
            // 
            btnTitularReporte.Anchor = AnchorStyles.Top;
            btnTitularReporte.BackColor = Color.LightSteelBlue;
            btnTitularReporte.BackgroundColor = Color.LightSteelBlue;
            btnTitularReporte.BorderColor = Color.LightSteelBlue;
            btnTitularReporte.BorderRadius = 26;
            btnTitularReporte.BorderSize = 0;
            btnTitularReporte.FlatAppearance.BorderSize = 0;
            btnTitularReporte.FlatStyle = FlatStyle.Flat;
            btnTitularReporte.Font = new Font("Century Gothic", 10F);
            btnTitularReporte.ForeColor = Color.Black;
            btnTitularReporte.Location = new Point(123, 329);
            btnTitularReporte.Name = "btnTitularReporte";
            btnTitularReporte.Size = new Size(194, 35);
            btnTitularReporte.TabIndex = 234;
            btnTitularReporte.Text = "+ TITULAR";
            btnTitularReporte.TextColor = Color.Black;
            btnTitularReporte.UseVisualStyleBackColor = false;
            btnTitularReporte.Click += btnTitularReporte_Click;
            // 
            // checkBoxEstadoReporte
            // 
            checkBoxEstadoReporte.Anchor = AnchorStyles.Top;
            checkBoxEstadoReporte.AutoSize = true;
            checkBoxEstadoReporte.BackColor = Color.FromArgb(222, 227, 234);
            checkBoxEstadoReporte.Location = new Point(77, 218);
            checkBoxEstadoReporte.Name = "checkBoxEstadoReporte";
            checkBoxEstadoReporte.Size = new Size(67, 21);
            checkBoxEstadoReporte.TabIndex = 196;
            checkBoxEstadoReporte.Text = "Estado";
            checkBoxEstadoReporte.UseVisualStyleBackColor = false;
            // 
            // chckTitularReporte
            // 
            chckTitularReporte.Anchor = AnchorStyles.Top;
            chckTitularReporte.AutoSize = true;
            chckTitularReporte.BackColor = Color.FromArgb(222, 227, 234);
            chckTitularReporte.Location = new Point(77, 299);
            chckTitularReporte.Name = "chckTitularReporte";
            chckTitularReporte.Size = new Size(62, 21);
            chckTitularReporte.TabIndex = 233;
            chckTitularReporte.Text = "Titular";
            chckTitularReporte.UseVisualStyleBackColor = false;
            // 
            // txtClaseReporte
            // 
            txtClaseReporte.Anchor = AnchorStyles.Top;
            txtClaseReporte.Location = new Point(381, 248);
            txtClaseReporte.Name = "txtClaseReporte";
            txtClaseReporte.Size = new Size(260, 22);
            txtClaseReporte.TabIndex = 228;
            // 
            // comboBoxEstadoReporte
            // 
            comboBoxEstadoReporte.Anchor = AnchorStyles.Top;
            comboBoxEstadoReporte.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxEstadoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEstadoReporte.FlatStyle = FlatStyle.Flat;
            comboBoxEstadoReporte.Font = new Font("Century Gothic", 9F);
            comboBoxEstadoReporte.FormattingEnabled = true;
            comboBoxEstadoReporte.Items.AddRange(new object[] { "Examen de fondo", "Requerimiento", "Objeción", "Resolución RPI desfavorable", "Publicación", "Orden de pago" });
            comboBoxEstadoReporte.Location = new Point(78, 248);
            comboBoxEstadoReporte.Name = "comboBoxEstadoReporte";
            comboBoxEstadoReporte.Size = new Size(260, 25);
            comboBoxEstadoReporte.TabIndex = 217;
            // 
            // chckClaseReporte
            // 
            chckClaseReporte.Anchor = AnchorStyles.Top;
            chckClaseReporte.AutoSize = true;
            chckClaseReporte.BackColor = Color.FromArgb(222, 227, 234);
            chckClaseReporte.Location = new Point(382, 218);
            chckClaseReporte.Name = "chckClaseReporte";
            chckClaseReporte.Size = new Size(60, 21);
            chckClaseReporte.TabIndex = 201;
            chckClaseReporte.Text = "Clase";
            chckClaseReporte.UseVisualStyleBackColor = false;
            // 
            // txtExpedienteReporte
            // 
            txtExpedienteReporte.Anchor = AnchorStyles.Top;
            txtExpedienteReporte.Location = new Point(77, 98);
            txtExpedienteReporte.Multiline = true;
            txtExpedienteReporte.Name = "txtExpedienteReporte";
            txtExpedienteReporte.Size = new Size(261, 26);
            txtExpedienteReporte.TabIndex = 225;
            // 
            // chckExpedienteReporte
            // 
            chckExpedienteReporte.Anchor = AnchorStyles.Top;
            chckExpedienteReporte.AutoSize = true;
            chckExpedienteReporte.BackColor = Color.FromArgb(222, 227, 234);
            chckExpedienteReporte.Location = new Point(77, 68);
            chckExpedienteReporte.Name = "chckExpedienteReporte";
            chckExpedienteReporte.Size = new Size(94, 21);
            chckExpedienteReporte.TabIndex = 197;
            chckExpedienteReporte.Text = "Expediente";
            chckExpedienteReporte.UseVisualStyleBackColor = false;
            // 
            // txtSignoReporte
            // 
            txtSignoReporte.Anchor = AnchorStyles.Top;
            txtSignoReporte.Location = new Point(77, 174);
            txtSignoReporte.Name = "txtSignoReporte";
            txtSignoReporte.Size = new Size(260, 22);
            txtSignoReporte.TabIndex = 226;
            // 
            // chckSignoRepo
            // 
            chckSignoRepo.Anchor = AnchorStyles.Top;
            chckSignoRepo.AutoSize = true;
            chckSignoRepo.BackColor = Color.FromArgb(222, 227, 234);
            chckSignoRepo.Location = new Point(77, 144);
            chckSignoRepo.Name = "chckSignoRepo";
            chckSignoRepo.Size = new Size(60, 21);
            chckSignoRepo.TabIndex = 199;
            chckSignoRepo.Text = "Signo";
            chckSignoRepo.UseVisualStyleBackColor = false;
            // 
            // cmbFiltro
            // 
            cmbFiltro.Anchor = AnchorStyles.Top;
            cmbFiltro.BackColor = Color.FromArgb(241, 240, 245);
            cmbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltro.FlatStyle = FlatStyle.Flat;
            cmbFiltro.Font = new Font("Century Gothic", 9F);
            cmbFiltro.FormattingEnabled = true;
            cmbFiltro.Items.AddRange(new object[] { "MARCAS", "PATENTES" });
            cmbFiltro.Location = new Point(77, 18);
            cmbFiltro.Name = "cmbFiltro";
            cmbFiltro.Size = new Size(260, 25);
            cmbFiltro.TabIndex = 218;
            cmbFiltro.SelectedIndexChanged += cmbFiltro_SelectedIndexChanged;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Controls.Add(btnConsultar);
            panelBotones.Location = new Point(417, 573);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(430, 83);
            panelBotones.TabIndex = 255;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.BackColor = Color.Gainsboro;
            btnCancelar.BackgroundColor = Color.Gainsboro;
            btnCancelar.BorderColor = Color.Gainsboro;
            btnCancelar.BorderRadius = 37;
            btnCancelar.BorderSize = 0;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(224, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(203, 49);
            btnCancelar.TabIndex = 244;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnConsultar
            // 
            btnConsultar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConsultar.BackColor = Color.FromArgb(251, 140, 0);
            btnConsultar.BackgroundColor = Color.FromArgb(251, 140, 0);
            btnConsultar.BorderColor = Color.FromArgb(251, 140, 0);
            btnConsultar.BorderRadius = 37;
            btnConsultar.BorderSize = 0;
            btnConsultar.FlatAppearance.BorderSize = 0;
            btnConsultar.FlatStyle = FlatStyle.Flat;
            btnConsultar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnConsultar.ForeColor = Color.White;
            btnConsultar.Location = new Point(6, 3);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(203, 49);
            btnConsultar.TabIndex = 245;
            btnConsultar.Text = "CONSULTAR";
            btnConsultar.TextColor = Color.White;
            btnConsultar.UseVisualStyleBackColor = false;
            btnConsultar.Click += btnConsultar_Click;
            // 
            // panelBotones2
            // 
            panelBotones2.Controls.Add(btnExportarExcel);
            panelBotones2.Controls.Add(btnExportarPDF);
            panelBotones2.Location = new Point(360, 1225);
            panelBotones2.Name = "panelBotones2";
            panelBotones2.Size = new Size(484, 73);
            panelBotones2.TabIndex = 256;
            // 
            // btnExportarExcel
            // 
            btnExportarExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarExcel.AutoSize = true;
            btnExportarExcel.BackColor = Color.FromArgb(0, 137, 123);
            btnExportarExcel.BackgroundColor = Color.FromArgb(0, 137, 123);
            btnExportarExcel.BorderColor = Color.FromArgb(0, 137, 123);
            btnExportarExcel.BorderRadius = 33;
            btnExportarExcel.BorderSize = 0;
            btnExportarExcel.FlatAppearance.BorderSize = 0;
            btnExportarExcel.FlatStyle = FlatStyle.Flat;
            btnExportarExcel.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnExportarExcel.ForeColor = Color.White;
            btnExportarExcel.Image = Properties.Resources.excel;
            btnExportarExcel.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportarExcel.Location = new Point(275, 3);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.Size = new Size(206, 49);
            btnExportarExcel.TabIndex = 246;
            btnExportarExcel.Text = "EXPORTAR A EXCEL";
            btnExportarExcel.TextColor = Color.White;
            btnExportarExcel.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnExportarExcel.UseVisualStyleBackColor = false;
            btnExportarExcel.Click += btnExportarExcel_Click;
            // 
            // btnExportarPDF
            // 
            btnExportarPDF.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarPDF.AutoSize = true;
            btnExportarPDF.BackColor = Color.FromArgb(229, 115, 115);
            btnExportarPDF.BackgroundColor = Color.FromArgb(229, 115, 115);
            btnExportarPDF.BorderColor = Color.FromArgb(229, 115, 115);
            btnExportarPDF.BorderRadius = 33;
            btnExportarPDF.BorderSize = 0;
            btnExportarPDF.FlatAppearance.BorderSize = 0;
            btnExportarPDF.FlatStyle = FlatStyle.Flat;
            btnExportarPDF.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnExportarPDF.ForeColor = Color.White;
            btnExportarPDF.Image = Properties.Resources.pdf_1_;
            btnExportarPDF.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportarPDF.Location = new Point(66, 3);
            btnExportarPDF.Name = "btnExportarPDF";
            btnExportarPDF.Size = new Size(203, 49);
            btnExportarPDF.TabIndex = 247;
            btnExportarPDF.Text = "EXPORTAR A PDF";
            btnExportarPDF.TextColor = Color.White;
            btnExportarPDF.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnExportarPDF.UseVisualStyleBackColor = false;
            btnExportarPDF.Click += btnExportarPDF_Click;
            // 
            // FrmPlazos
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1132, 743);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmPlazos";
            Text = "FrmPlazos";
            Load += FrmPlazos_Load;
            Resize += FrmPlazos_Resize;
            ((System.ComponentModel.ISupportInitialize)dtgPlazos).EndInit();
            panel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPageVencimientosList.ResumeLayout(false);
            tabPageVencimientosList.PerformLayout();
            panelBusqueda.ResumeLayout(false);
            panelBusqueda.PerformLayout();
            tabPageHistorialDetail.ResumeLayout(false);
            tabPageHistorialDetail.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            tabPageReportes.ResumeLayout(false);
            tabPageReportes.PerformLayout();
            panel16.ResumeLayout(false);
            panel16.PerformLayout();
            panel17.ResumeLayout(false);
            panel17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            panel18.ResumeLayout(false);
            panel18.PerformLayout();
            panel23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgReportes).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            panel20.ResumeLayout(false);
            panel20.PerformLayout();
            panelBotones.ResumeLayout(false);
            panelBotones2.ResumeLayout(false);
            panelBotones2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dtgPlazos;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private TextBox txtBuscar;
        private Clases.RoundedButton roundedButton3;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private TabControl tabControl1;
        private TabPage tabPageVencimientosList;
        private Clases.RoundedButton roundedButton5;
        private TabPage tabPageHistorialDetail;
        private TextBox txtETraspaso;
        private FontAwesome.Sharp.IconButton iconButton7;
        private Label lblTotalPages;
        private Label label2;
        private Label lblCurrentPage;
        private Label lblTotalRows;
        private Label label3;
        private Label label40;
        private Panel panel8;
        private FontAwesome.Sharp.IconButton btnLast;
        private FontAwesome.Sharp.IconButton btnNext;
        private FontAwesome.Sharp.IconButton btnPrev;
        private FontAwesome.Sharp.IconButton btnFirst;
        private Panel panelBusqueda;
        private ComboBox comboBoxTipoFiltro;
        private TabPage tabPageReportes;
        private Panel panel9;
        private Panel panel11;
        private Clases.RoundedButton roundedButton1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Panel panel13;
        private Clases.RoundedButton roundedButton2;
        private DateTimePicker dateTimePickerVencimiento;
        private Label labelVenc;
        private FontAwesome.Sharp.IconButton iconButton2;
        private RichTextBox richTextBoxAnotaciones;
        private FontAwesome.Sharp.IconButton iconButton3;
        private ComboBox comboBoxEstado;
        private Label label1;
        private DateTimePicker dateTimePickerFechaIngreso;
        private Label label4;
        private Label label5;
        private Clases.RoundedButton roundedButton4;
        private Label label6;
        private FontAwesome.Sharp.IconButton btnIrAReportes;
        private Panel panel23;
        private DataGridView dtgReportes;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel20;
        private RichTextBox richTextBoxTitularReporte;
        private Clases.RoundedButton btnTitularReporte;
        private CheckBox checkBoxEstadoReporte;
        private CheckBox chckTitularReporte;
        private TextBox txtClaseReporte;
        private ComboBox comboBoxEstadoReporte;
        private CheckBox chckClaseReporte;
        private TextBox txtExpedienteReporte;
        private CheckBox chckExpedienteReporte;
        private TextBox txtSignoReporte;
        private CheckBox chckSignoRepo;
        private ComboBox cmbFiltro;
        private Panel panelBotones;
        private Clases.RoundedButton btnCancelar;
        private Clases.RoundedButton btnConsultar;
        private Panel panelBotones2;
        private Clases.RoundedButton btnExportarExcel;
        private Clases.RoundedButton btnExportarPDF;
        private RichTextBox richTextBoxAgenteReporte;
        private Clases.RoundedButton btnAgenteReporte;
        private CheckBox chckAgenteReporte;
        private DateTimePicker dtpFechaingresoFinal;
        private DateTimePicker dtpFIngresoInicial;
        private DateTimePicker dtpVencimientoInicial;
        private DateTimePicker dtpVencimientoFinal;
        private CheckBox checkBoxFIngreso;
        private CheckBox checkBoxVencimiento;
        private Panel panel16;
        private Panel panel17;
        private Clases.RoundedButton roundedButton7;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private Panel panel18;
        private Clases.RoundedButton roundedButton8;
        private FontAwesome.Sharp.IconButton iconButton9;
    }
}