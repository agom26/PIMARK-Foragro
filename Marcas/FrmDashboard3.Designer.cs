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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panel1 = new Panel();
            panel2 = new Panel();
            roundedButton3 = new Clases.RoundedButton();
            dtgVencimientos = new DataGridView();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            label4 = new Label();
            roundedButton2 = new Clases.RoundedButton();
            roundedButton1 = new Clases.RoundedButton();
            tabControl1 = new TabControl();
            tabPage2 = new TabPage();
            panel9 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel3 = new Panel();
            panel6 = new Panel();
            label2 = new Label();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton5 = new Clases.RoundedButton();
            panel4 = new Panel();
            panel5 = new Panel();
            label1 = new Label();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton4 = new Clases.RoundedButton();
            panel7 = new Panel();
            panel8 = new Panel();
            label3 = new Label();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton6 = new Clases.RoundedButton();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel3.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
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
            panel1.Size = new Size(863, 5);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(roundedButton3);
            panel2.Controls.Add(dtgVencimientos);
            panel2.Controls.Add(iconPictureBox4);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(roundedButton2);
            panel2.Controls.Add(roundedButton1);
            panel2.Location = new Point(26, 40);
            panel2.Name = "panel2";
            panel2.Size = new Size(863, 371);
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
            roundedButton3.ForeColor = Color.Black;
            roundedButton3.Location = new Point(337, 316);
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgVencimientos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            dtgVencimientos.Size = new Size(717, 245);
            dtgVencimientos.TabIndex = 6;
            // 
            // iconPictureBox4
            // 
            iconPictureBox4.Anchor = AnchorStyles.Top;
            iconPictureBox4.BackColor = Color.FromArgb(196, 205, 218);
            iconPictureBox4.ForeColor = SystemColors.ControlText;
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            iconPictureBox4.IconColor = SystemColors.ControlText;
            iconPictureBox4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox4.IconSize = 56;
            iconPictureBox4.Location = new Point(233, 3);
            iconPictureBox4.Name = "iconPictureBox4";
            iconPictureBox4.Size = new Size(62, 56);
            iconPictureBox4.TabIndex = 43;
            iconPictureBox4.TabStop = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(196, 205, 218);
            label4.Font = new Font("Century Gothic", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(320, 17);
            label4.Name = "label4";
            label4.Size = new Size(303, 27);
            label4.TabIndex = 42;
            label4.Text = "PRÓXIMOS VENCIMIENTOS";
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(196, 205, 218);
            roundedButton2.BackgroundColor = Color.FromArgb(196, 205, 218);
            roundedButton2.BorderColor = Color.FromArgb(196, 205, 218);
            roundedButton2.BorderRadius = 40;
            roundedButton2.BorderSize = 0;
            roundedButton2.Dock = DockStyle.Top;
            roundedButton2.Enabled = false;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.Black;
            roundedButton2.Location = new Point(0, 0);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(863, 62);
            roundedButton2.TabIndex = 1;
            roundedButton2.TextColor = Color.Black;
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
            roundedButton1.Size = new Size(863, 371);
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
            tabControl1.Size = new Size(1000, 900);
            tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
            tabPage2.Controls.Add(panel1);
            tabPage2.Controls.Add(panel9);
            tabPage2.Controls.Add(tableLayoutPanel1);
            tabPage2.Controls.Add(panel2);
            tabPage2.Location = new Point(4, 30);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(992, 866);
            tabPage2.TabIndex = 1;
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            panel9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel9.BackColor = Color.FromArgb(196, 205, 218);
            panel9.Location = new Point(26, 437);
            panel9.Name = "panel9";
            panel9.Size = new Size(900, 5);
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
            tableLayoutPanel1.Location = new Point(8, 469);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(958, 274);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top;
            panel3.Controls.Add(panel6);
            panel3.Controls.Add(iconPictureBox2);
            panel3.Controls.Add(roundedButton5);
            panel3.Location = new Point(368, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(221, 248);
            panel3.TabIndex = 13;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(38, 166, 154);
            panel6.Controls.Add(label2);
            panel6.Location = new Point(3, 188);
            panel6.Margin = new Padding(3, 4, 3, 4);
            panel6.Name = "panel6";
            panel6.Size = new Size(213, 37);
            panel6.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(38, 166, 154);
            label2.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(64, 9);
            label2.Name = "label2";
            label2.Size = new Size(87, 19);
            label2.TabIndex = 1;
            label2.Text = "REPORTES";
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.BackColor = Color.FromArgb(201, 211, 221);
            iconPictureBox2.ForeColor = Color.FromArgb(38, 166, 154);
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.ChartSimple;
            iconPictureBox2.IconColor = Color.FromArgb(38, 166, 154);
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 163;
            iconPictureBox2.Location = new Point(25, 31);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(165, 163);
            iconPictureBox2.TabIndex = 4;
            iconPictureBox2.TabStop = false;
            iconPictureBox2.Click += iconPictureBox2_Click;
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.Top;
            roundedButton5.AutoSize = true;
            roundedButton5.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton5.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton5.BorderColor = Color.Empty;
            roundedButton5.BorderRadius = 40;
            roundedButton5.BorderSize = 0;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(2, 3);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(214, 242);
            roundedButton5.TabIndex = 1;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top;
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(iconPictureBox1);
            panel4.Controls.Add(roundedButton4);
            panel4.Location = new Point(49, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(221, 248);
            panel4.TabIndex = 12;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(60, 120, 172);
            panel5.Controls.Add(label1);
            panel5.Location = new Point(4, 188);
            panel5.Margin = new Padding(3, 4, 3, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(214, 37);
            panel5.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(60, 120, 172);
            label1.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(200, 18);
            label1.TabIndex = 1;
            label1.Text = "INGRESAR TRÁMITE INICIAL";
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.FromArgb(201, 211, 221);
            iconPictureBox1.ForeColor = Color.FromArgb(60, 120, 172);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.FileContract;
            iconPictureBox1.IconColor = Color.FromArgb(60, 120, 172);
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 163;
            iconPictureBox1.Location = new Point(26, 31);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(165, 163);
            iconPictureBox1.TabIndex = 4;
            iconPictureBox1.TabStop = false;
            // 
            // roundedButton4
            // 
            roundedButton4.AutoSize = true;
            roundedButton4.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BorderColor = Color.Empty;
            roundedButton4.BorderRadius = 40;
            roundedButton4.BorderSize = 0;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(4, 3);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(214, 242);
            roundedButton4.TabIndex = 1;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // panel7
            // 
            panel7.Anchor = AnchorStyles.Top;
            panel7.Controls.Add(panel8);
            panel7.Controls.Add(iconPictureBox3);
            panel7.Controls.Add(roundedButton6);
            panel7.Location = new Point(687, 3);
            panel7.Name = "panel7";
            panel7.Size = new Size(221, 248);
            panel7.TabIndex = 14;
            // 
            // panel8
            // 
            panel8.BackColor = Color.FromArgb(255, 183, 77);
            panel8.Controls.Add(label3);
            panel8.Location = new Point(3, 188);
            panel8.Margin = new Padding(3, 4, 3, 4);
            panel8.Name = "panel8";
            panel8.Size = new Size(214, 37);
            panel8.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(255, 183, 77);
            label3.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(3, 12);
            label3.Name = "label3";
            label3.Size = new Size(203, 17);
            label3.TabIndex = 1;
            label3.Text = "BUSCAR MARCA REGISTRADA";
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = Color.FromArgb(201, 211, 221);
            iconPictureBox3.ForeColor = Color.FromArgb(255, 183, 77);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconPictureBox3.IconColor = Color.FromArgb(255, 183, 77);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 163;
            iconPictureBox3.Location = new Point(25, 31);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(165, 163);
            iconPictureBox3.TabIndex = 4;
            iconPictureBox3.TabStop = false;
            // 
            // roundedButton6
            // 
            roundedButton6.AutoSize = true;
            roundedButton6.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton6.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton6.BorderColor = Color.Empty;
            roundedButton6.BorderRadius = 40;
            roundedButton6.BorderSize = 0;
            roundedButton6.FlatAppearance.BorderSize = 0;
            roundedButton6.FlatStyle = FlatStyle.Flat;
            roundedButton6.ForeColor = Color.White;
            roundedButton6.Location = new Point(3, 3);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(214, 242);
            roundedButton6.TabIndex = 1;
            roundedButton6.TextColor = Color.White;
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // FrmDashboard3
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            ClientSize = new Size(1000, 900);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmDashboard3";
            Text = "FrmDashboard3";
            Load += FrmDashboard3_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
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
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private Label label4;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel4;
        private Panel panel5;
        private Label label1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Clases.RoundedButton roundedButton4;
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
    }
}