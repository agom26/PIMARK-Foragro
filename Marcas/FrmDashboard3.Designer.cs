namespace Presentacion
{
    partial class FrmDashboard3
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panel1 = new Panel();
            panel2 = new Panel();
            roundedButton3 = new Presentacion.Clases.RoundedButton();
            dtgVencimientos = new DataGridView();
            roundedButton2 = new Presentacion.Clases.RoundedButton();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            tabControl1 = new TabControl();
            tabPage2 = new TabPage();
            panel10 = new Panel();
            roundedButton7 = new Presentacion.Clases.RoundedButton();
            dtgPlazos = new DataGridView();
            roundedButton8 = new Presentacion.Clases.RoundedButton();
            roundedButton9 = new Presentacion.Clases.RoundedButton();
            panel11 = new Panel();
            panel9 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel3 = new Panel();
            panel6 = new Panel();
            label2 = new Label();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton5 = new Presentacion.Clases.RoundedButton();
            panel4 = new Panel();
            panelIngresar = new Panel();
            labelIngresar = new Label();
            iconPictureBoxIngresar = new FontAwesome.Sharp.IconPictureBox();
            roundedButtonIngresar = new Presentacion.Clases.RoundedButton();
            panel7 = new Panel();
            panel8 = new Panel();
            label3 = new Label();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton6 = new Presentacion.Clases.RoundedButton();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).BeginInit();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgPlazos).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel3.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            panel4.SuspendLayout();
            panelIngresar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxIngresar).BeginInit();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(196, 205, 218);
            panel1.Location = new Point(26, 15);
            panel1.Name = "panel1";
            panel1.Size = new Size(761, 5);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(roundedButton3);
            panel2.Controls.Add(dtgVencimientos);
            panel2.Controls.Add(roundedButton2);
            panel2.Controls.Add(roundedButton1);
            panel2.Location = new Point(26, 40);
            panel2.Name = "panel2";
            panel2.Size = new Size(761, 371);
            panel2.TabIndex = 1;
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.Top;
            roundedButton3.BackColor = Color.FromArgb(196, 196, 208);
            roundedButton3.BackgroundColor = Color.FromArgb(196, 196, 208);
            roundedButton3.BorderColor = Color.FromArgb(196, 196, 208);
            roundedButton3.BorderRadius = 10;
            roundedButton3.BorderSize = 0;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            roundedButton3.ForeColor = Color.Black;
            roundedButton3.Location = new Point(286, 316);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(188, 50);
            roundedButton3.TabIndex = 2;
            roundedButton3.Text = "VER VENCIMIENTOS";
            roundedButton3.TextColor = Color.Black;
            roundedButton3.UseVisualStyleBackColor = false;
            roundedButton3.Click += roundedButton3_Click;
            // 
            // dtgVencimientos
            // 
            dtgVencimientos.AllowUserToAddRows = false;
            dtgVencimientos.AllowUserToDeleteRows = false;
            dtgVencimientos.AllowUserToOrderColumns = true;
            dtgVencimientos.AllowUserToResizeRows = false;
            dtgVencimientos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtgVencimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgVencimientos.BackgroundColor = Color.White;
            dtgVencimientos.BorderStyle = BorderStyle.None;
            dtgVencimientos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgVencimientos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgVencimientos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgVencimientos.ColumnHeadersHeight = 40;
            dtgVencimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgVencimientos.EnableHeadersVisualStyles = false;
            dtgVencimientos.GridColor = Color.LightGray;
            dtgVencimientos.Location = new Point(64, 65);
            dtgVencimientos.Name = "dtgVencimientos";
            dtgVencimientos.ReadOnly = true;
            dtgVencimientos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgVencimientos.RowHeadersWidth = 40;
            dtgVencimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgVencimientos.Size = new Size(615, 245);
            dtgVencimientos.TabIndex = 6;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(196, 205, 218);
            roundedButton2.BackgroundColor = Color.FromArgb(196, 205, 218);
            roundedButton2.BorderColor = Color.FromArgb(196, 205, 218);
            roundedButton2.BorderRadius = 40;
            roundedButton2.BorderSize = 0;
            roundedButton2.Dock = DockStyle.Top;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Century Gothic", 15F);
            roundedButton2.ForeColor = Color.Black;
            roundedButton2.Image = Properties.Resources.reloj_y_calendario_1_;
            roundedButton2.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton2.Location = new Point(0, 0);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(761, 62);
            roundedButton2.TabIndex = 1;
            roundedButton2.Text = "PRÓXIMOS VENCIMIENTOS";
            roundedButton2.TextColor = Color.Black;
            roundedButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton1.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton1.BorderColor = Color.Empty;
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.Dock = DockStyle.Fill;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(0, 0);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(761, 371);
            roundedButton1.TabIndex = 0;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1000, 701);
            tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
            tabPage2.Controls.Add(panel10);
            tabPage2.Controls.Add(panel11);
            tabPage2.Controls.Add(panel1);
            tabPage2.Controls.Add(panel9);
            tabPage2.Controls.Add(tableLayoutPanel1);
            tabPage2.Controls.Add(panel2);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(992, 671);
            tabPage2.TabIndex = 1;
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel10
            // 
            panel10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel10.Controls.Add(roundedButton7);
            panel10.Controls.Add(dtgPlazos);
            panel10.Controls.Add(roundedButton8);
            panel10.Controls.Add(roundedButton9);
            panel10.Location = new Point(26, 471);
            panel10.Name = "panel10";
            panel10.Size = new Size(761, 371);
            panel10.TabIndex = 11;
            // 
            // roundedButton7
            // 
            roundedButton7.Anchor = AnchorStyles.Top;
            roundedButton7.BackColor = Color.FromArgb(196, 196, 208);
            roundedButton7.BackgroundColor = Color.FromArgb(196, 196, 208);
            roundedButton7.BorderColor = Color.FromArgb(196, 196, 208);
            roundedButton7.BorderRadius = 10;
            roundedButton7.BorderSize = 0;
            roundedButton7.FlatAppearance.BorderSize = 0;
            roundedButton7.FlatStyle = FlatStyle.Flat;
            roundedButton7.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            roundedButton7.ForeColor = Color.Black;
            roundedButton7.Location = new Point(286, 318);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(188, 50);
            roundedButton7.TabIndex = 2;
            roundedButton7.Text = "VER PLAZOS";
            roundedButton7.TextColor = Color.Black;
            roundedButton7.UseVisualStyleBackColor = false;
            roundedButton7.Click += roundedButton7_Click;
            // 
            // dtgPlazos
            // 
            dtgPlazos.AllowUserToAddRows = false;
            dtgPlazos.AllowUserToDeleteRows = false;
            dtgPlazos.AllowUserToOrderColumns = true;
            dtgPlazos.AllowUserToResizeRows = false;
            dtgPlazos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dtgPlazos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgPlazos.BackgroundColor = Color.White;
            dtgPlazos.BorderStyle = BorderStyle.None;
            dtgPlazos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgPlazos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 10F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtgPlazos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtgPlazos.ColumnHeadersHeight = 40;
            dtgPlazos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgPlazos.EnableHeadersVisualStyles = false;
            dtgPlazos.GridColor = Color.LightGray;
            dtgPlazos.Location = new Point(64, 65);
            dtgPlazos.Name = "dtgPlazos";
            dtgPlazos.ReadOnly = true;
            dtgPlazos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgPlazos.RowHeadersWidth = 40;
            dtgPlazos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgPlazos.Size = new Size(615, 245);
            dtgPlazos.TabIndex = 6;
            dtgPlazos.DataBindingComplete += dtgPlazos_DataBindingComplete;
            // 
            // roundedButton8
            // 
            roundedButton8.BackColor = Color.FromArgb(196, 205, 218);
            roundedButton8.BackgroundColor = Color.FromArgb(196, 205, 218);
            roundedButton8.BorderColor = Color.FromArgb(196, 205, 218);
            roundedButton8.BorderRadius = 40;
            roundedButton8.BorderSize = 0;
            roundedButton8.Dock = DockStyle.Top;
            roundedButton8.FlatAppearance.BorderSize = 0;
            roundedButton8.FlatStyle = FlatStyle.Flat;
            roundedButton8.Font = new Font("Century Gothic", 15F);
            roundedButton8.ForeColor = Color.Black;
            roundedButton8.Image = Properties.Resources.hourglass__2_;
            roundedButton8.ImageAlign = ContentAlignment.MiddleRight;
            roundedButton8.Location = new Point(0, 0);
            roundedButton8.Name = "roundedButton8";
            roundedButton8.Size = new Size(761, 62);
            roundedButton8.TabIndex = 1;
            roundedButton8.Text = "PLAZOS";
            roundedButton8.TextColor = Color.Black;
            roundedButton8.TextImageRelation = TextImageRelation.ImageBeforeText;
            roundedButton8.UseVisualStyleBackColor = false;
            // 
            // roundedButton9
            // 
            roundedButton9.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton9.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton9.BorderColor = Color.Empty;
            roundedButton9.BorderRadius = 40;
            roundedButton9.BorderSize = 0;
            roundedButton9.Dock = DockStyle.Fill;
            roundedButton9.Enabled = false;
            roundedButton9.FlatAppearance.BorderSize = 0;
            roundedButton9.FlatStyle = FlatStyle.Flat;
            roundedButton9.ForeColor = Color.White;
            roundedButton9.Location = new Point(0, 0);
            roundedButton9.Name = "roundedButton9";
            roundedButton9.Size = new Size(761, 371);
            roundedButton9.TabIndex = 0;
            roundedButton9.TextColor = Color.White;
            roundedButton9.UseVisualStyleBackColor = false;
            // 
            // panel11
            // 
            panel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel11.BackColor = Color.FromArgb(196, 205, 218);
            panel11.Location = new Point(26, 868);
            panel11.Name = "panel11";
            panel11.Size = new Size(798, 5);
            panel11.TabIndex = 12;
            // 
            // panel9
            // 
            panel9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel9.BackColor = Color.FromArgb(196, 205, 218);
            panel9.Location = new Point(26, 437);
            panel9.Name = "panel9";
            panel9.Size = new Size(798, 5);
            panel9.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(panel3, 1, 0);
            tableLayoutPanel1.Controls.Add(panel4, 0, 0);
            tableLayoutPanel1.Controls.Add(panel7, 2, 0);
            tableLayoutPanel1.Location = new Point(8, 890);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(856, 274);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.Controls.Add(panel6);
            panel3.Controls.Add(iconPictureBox2);
            panel3.Controls.Add(roundedButton5);
            panel3.Location = new Point(317, 13);
            panel3.Name = "panel3";
            panel3.Size = new Size(221, 248);
            panel3.TabIndex = 13;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.BackColor = Color.FromArgb(38, 166, 154);
            panel6.Controls.Add(label2);
            panel6.Location = new Point(0, 188);
            panel6.Margin = new Padding(3, 4, 3, 4);
            panel6.Name = "panel6";
            panel6.Size = new Size(221, 37);
            panel6.TabIndex = 7;
            panel6.Click += panel6_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(38, 166, 154);
            label2.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(68, 9);
            label2.Name = "label2";
            label2.Size = new Size(71, 17);
            label2.TabIndex = 1;
            label2.Text = "REPORTES";
            label2.Click += label2_Click;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.Anchor = AnchorStyles.None;
            iconPictureBox2.BackColor = Color.FromArgb(201, 211, 221);
            iconPictureBox2.ForeColor = Color.FromArgb(38, 166, 154);
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.ChartSimple;
            iconPictureBox2.IconColor = Color.FromArgb(38, 166, 154);
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 169;
            iconPictureBox2.Location = new Point(26, 39);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(169, 170);
            iconPictureBox2.TabIndex = 4;
            iconPictureBox2.TabStop = false;
            iconPictureBox2.UseGdi = true;
            iconPictureBox2.Click += iconPictureBox2_Click;
            // 
            // roundedButton5
            // 
            roundedButton5.AutoSize = true;
            roundedButton5.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton5.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton5.BorderColor = Color.Empty;
            roundedButton5.BorderRadius = 40;
            roundedButton5.BorderSize = 0;
            roundedButton5.Dock = DockStyle.Fill;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(0, 0);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(221, 248);
            roundedButton5.TabIndex = 1;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            roundedButton5.Click += roundedButton5_Click;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.None;
            panel4.Controls.Add(panelIngresar);
            panel4.Controls.Add(iconPictureBoxIngresar);
            panel4.Controls.Add(roundedButtonIngresar);
            panel4.Location = new Point(32, 13);
            panel4.Name = "panel4";
            panel4.Size = new Size(221, 248);
            panel4.TabIndex = 12;
            // 
            // panelIngresar
            // 
            panelIngresar.Anchor = AnchorStyles.None;
            panelIngresar.BackColor = Color.FromArgb(60, 120, 172);
            panelIngresar.Controls.Add(labelIngresar);
            panelIngresar.Location = new Point(0, 188);
            panelIngresar.Margin = new Padding(3, 4, 3, 4);
            panelIngresar.Name = "panelIngresar";
            panelIngresar.Size = new Size(221, 37);
            panelIngresar.TabIndex = 7;
            panelIngresar.Click += panel5_Click;
            panelIngresar.Paint += panel5_Paint;
            // 
            // labelIngresar
            // 
            labelIngresar.Anchor = AnchorStyles.None;
            labelIngresar.AutoSize = true;
            labelIngresar.BackColor = Color.FromArgb(60, 120, 172);
            labelIngresar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            labelIngresar.ForeColor = Color.White;
            labelIngresar.Location = new Point(10, 9);
            labelIngresar.Name = "labelIngresar";
            labelIngresar.Size = new Size(164, 16);
            labelIngresar.TabIndex = 1;
            labelIngresar.Text = "INGRESAR TRÁMITE INICIAL";
            labelIngresar.Click += label1_Click;
            // 
            // iconPictureBoxIngresar
            // 
            iconPictureBoxIngresar.Anchor = AnchorStyles.None;
            iconPictureBoxIngresar.BackColor = Color.FromArgb(201, 211, 221);
            iconPictureBoxIngresar.ForeColor = Color.FromArgb(60, 120, 172);
            iconPictureBoxIngresar.IconChar = FontAwesome.Sharp.IconChar.FileContract;
            iconPictureBoxIngresar.IconColor = Color.FromArgb(60, 120, 172);
            iconPictureBoxIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBoxIngresar.IconSize = 169;
            iconPictureBoxIngresar.Location = new Point(26, 39);
            iconPictureBoxIngresar.Name = "iconPictureBoxIngresar";
            iconPictureBoxIngresar.Size = new Size(169, 170);
            iconPictureBoxIngresar.TabIndex = 4;
            iconPictureBoxIngresar.TabStop = false;
            iconPictureBoxIngresar.UseGdi = true;
            iconPictureBoxIngresar.Click += iconPictureBox1_Click;
            // 
            // roundedButtonIngresar
            // 
            roundedButtonIngresar.AutoSize = true;
            roundedButtonIngresar.BackColor = Color.FromArgb(222, 227, 234);
            roundedButtonIngresar.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButtonIngresar.BorderColor = Color.Empty;
            roundedButtonIngresar.BorderRadius = 40;
            roundedButtonIngresar.BorderSize = 0;
            roundedButtonIngresar.Dock = DockStyle.Fill;
            roundedButtonIngresar.FlatAppearance.BorderSize = 0;
            roundedButtonIngresar.FlatStyle = FlatStyle.Flat;
            roundedButtonIngresar.ForeColor = Color.White;
            roundedButtonIngresar.Location = new Point(0, 0);
            roundedButtonIngresar.Name = "roundedButtonIngresar";
            roundedButtonIngresar.Size = new Size(221, 248);
            roundedButtonIngresar.TabIndex = 1;
            roundedButtonIngresar.TextColor = Color.White;
            roundedButtonIngresar.UseVisualStyleBackColor = false;
            roundedButtonIngresar.Click += roundedButton4_Click;
            // 
            // panel7
            // 
            panel7.Anchor = AnchorStyles.None;
            panel7.Controls.Add(panel8);
            panel7.Controls.Add(iconPictureBox3);
            panel7.Controls.Add(roundedButton6);
            panel7.Location = new Point(602, 13);
            panel7.Name = "panel7";
            panel7.Size = new Size(221, 248);
            panel7.TabIndex = 14;
            // 
            // panel8
            // 
            panel8.Anchor = AnchorStyles.None;
            panel8.BackColor = Color.FromArgb(255, 183, 77);
            panel8.Controls.Add(label3);
            panel8.Location = new Point(0, 188);
            panel8.Margin = new Padding(3, 4, 3, 4);
            panel8.Name = "panel8";
            panel8.Size = new Size(221, 37);
            panel8.TabIndex = 7;
            panel8.Click += panel8_Click;
            panel8.Paint += panel8_Paint;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(255, 183, 77);
            label3.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(10, 10);
            label3.Name = "label3";
            label3.Size = new Size(163, 15);
            label3.TabIndex = 1;
            label3.Text = "BUSCAR MARCA REGISTRADA";
            label3.Click += label3_Click;
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.Anchor = AnchorStyles.None;
            iconPictureBox3.BackColor = Color.FromArgb(201, 211, 221);
            iconPictureBox3.ForeColor = Color.FromArgb(255, 183, 77);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconPictureBox3.IconColor = Color.FromArgb(255, 183, 77);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 169;
            iconPictureBox3.Location = new Point(26, 39);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(169, 170);
            iconPictureBox3.TabIndex = 4;
            iconPictureBox3.TabStop = false;
            iconPictureBox3.UseGdi = true;
            iconPictureBox3.Click += iconPictureBox3_Click;
            // 
            // roundedButton6
            // 
            roundedButton6.AutoSize = true;
            roundedButton6.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton6.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton6.BorderColor = Color.Empty;
            roundedButton6.BorderRadius = 40;
            roundedButton6.BorderSize = 0;
            roundedButton6.Dock = DockStyle.Fill;
            roundedButton6.FlatAppearance.BorderSize = 0;
            roundedButton6.FlatStyle = FlatStyle.Flat;
            roundedButton6.ForeColor = Color.White;
            roundedButton6.Location = new Point(0, 0);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(221, 248);
            roundedButton6.TabIndex = 1;
            roundedButton6.TextColor = Color.White;
            roundedButton6.UseVisualStyleBackColor = false;
            roundedButton6.Click += roundedButton6_Click;
            // 
            // FrmDashboard3
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            ClientSize = new Size(1000, 701);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmDashboard3";
            Text = "FrmDashboard3";
            Load += FrmDashboard3_Load;
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgPlazos).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panelIngresar.ResumeLayout(false);
            panelIngresar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxIngresar).EndInit();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Clases.RoundedButton roundedButton1;
        private Clases.RoundedButton roundedButton2;
        private DataGridView dtgVencimientos;
        private Clases.RoundedButton roundedButton3;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel4;
        private Panel panelIngresar;
        private Label labelIngresar;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxIngresar;
        private Clases.RoundedButton roundedButtonIngresar;
        private Panel panel7;
        private Panel panel8;
        private Label label3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private Clases.RoundedButton roundedButton6;
        private Panel panel3;
        private Panel panel6;
        private Label label2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Clases.RoundedButton roundedButton5;
        private Panel panel9;
        private Panel panel10;
        private Clases.RoundedButton roundedButton7;
        private DataGridView dtgPlazos;
        private Clases.RoundedButton roundedButton8;
        private Clases.RoundedButton roundedButton9;
        private Panel panel11;
    }
}