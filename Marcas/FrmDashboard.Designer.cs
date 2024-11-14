namespace Presentacion
{
    partial class FrmDashboard
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
            panel1 = new Panel();
            panel3 = new Panel();
            roundedButton5 = new Clases.RoundedButton();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            dtgVencimientos = new DataGridView();
            label4 = new Label();
            roundedButton1 = new Clases.RoundedButton();
            panel2 = new Panel();
            panel6 = new Panel();
            panel9 = new Panel();
            label3 = new Label();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton4 = new Clases.RoundedButton();
            panel5 = new Panel();
            panel8 = new Panel();
            label2 = new Label();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            roundedButton3 = new Clases.RoundedButton();
            panel4 = new Panel();
            panel7 = new Panel();
            label1 = new Label();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            panel10 = new Panel();
            roundedButton2 = new Clases.RoundedButton();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).BeginInit();
            panel2.SuspendLayout();
            panel6.SuspendLayout();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            panel5.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            panel4.SuspendLayout();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1169, 466);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(roundedButton5);
            panel3.Controls.Add(iconPictureBox4);
            panel3.Controls.Add(dtgVencimientos);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(roundedButton1);
            panel3.Location = new Point(40, 31);
            panel3.Name = "panel3";
            panel3.Size = new Size(1078, 406);
            panel3.TabIndex = 0;
            // 
            // roundedButton5
            // 
            roundedButton5.BackColor = Color.FromArgb(196, 196, 208);
            roundedButton5.BackgroundColor = Color.FromArgb(196, 196, 208);
            roundedButton5.BorderColor = Color.Empty;
            roundedButton5.BorderRadius = 10;
            roundedButton5.BorderSize = 0;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.Font = new Font("Century Gothic", 10F);
            roundedButton5.ForeColor = SystemColors.ControlText;
            roundedButton5.Location = new Point(441, 320);
            roundedButton5.Margin = new Padding(0);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(214, 60);
            roundedButton5.TabIndex = 1;
            roundedButton5.Text = "VER VENCIMIENTOS";
            roundedButton5.TextColor = SystemColors.ControlText;
            roundedButton5.UseVisualStyleBackColor = false;
            roundedButton5.Click += roundedButton5_Click;
            // 
            // iconPictureBox4
            // 
            iconPictureBox4.Anchor = AnchorStyles.Top;
            iconPictureBox4.BackColor = Color.FromArgb(222, 227, 234);
            iconPictureBox4.ForeColor = SystemColors.ControlText;
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            iconPictureBox4.IconColor = SystemColors.ControlText;
            iconPictureBox4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox4.IconSize = 62;
            iconPictureBox4.Location = new Point(344, 3);
            iconPictureBox4.Name = "iconPictureBox4";
            iconPictureBox4.Size = new Size(62, 62);
            iconPictureBox4.TabIndex = 41;
            iconPictureBox4.TabStop = false;
            // 
            // dtgVencimientos
            // 
            dtgVencimientos.AllowUserToAddRows = false;
            dtgVencimientos.AllowUserToDeleteRows = false;
            dtgVencimientos.AllowUserToOrderColumns = true;
            dtgVencimientos.AllowUserToResizeRows = false;
            dtgVencimientos.Anchor = AnchorStyles.Top;
            dtgVencimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgVencimientos.BackgroundColor = Color.White;
            dtgVencimientos.BorderStyle = BorderStyle.None;
            dtgVencimientos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgVencimientos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgVencimientos.ColumnHeadersHeight = 40;
            dtgVencimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgVencimientos.EnableHeadersVisualStyles = false;
            dtgVencimientos.GridColor = Color.LightGray;
            dtgVencimientos.Location = new Point(86, 71);
            dtgVencimientos.Name = "dtgVencimientos";
            dtgVencimientos.ReadOnly = true;
            dtgVencimientos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgVencimientos.RowHeadersWidth = 40;
            dtgVencimientos.Size = new Size(895, 246);
            dtgVencimientos.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(222, 227, 234);
            label4.Font = new Font("Century Gothic", 14F);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(417, 22);
            label4.Name = "label4";
            label4.Size = new Size(325, 30);
            label4.TabIndex = 3;
            label4.Text = "PRÓXIMOS VENCIMIENTOS";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton1.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton1.BorderColor = Color.FromArgb(222, 227, 234);
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.Dock = DockStyle.Fill;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = SystemColors.ControlText;
            roundedButton1.Location = new Point(0, 0);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(1078, 406);
            roundedButton1.TabIndex = 0;
            roundedButton1.TextColor = SystemColors.ControlText;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(panel4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 466);
            panel2.Name = "panel2";
            panel2.Size = new Size(1169, 346);
            panel2.TabIndex = 1;
            // 
            // panel6
            // 
            panel6.Controls.Add(panel9);
            panel6.Dock = DockStyle.Left;
            panel6.Location = new Point(782, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(384, 346);
            panel6.TabIndex = 2;
            // 
            // panel9
            // 
            panel9.Controls.Add(label3);
            panel9.Controls.Add(iconPictureBox3);
            panel9.Controls.Add(roundedButton4);
            panel9.Location = new Point(21, 35);
            panel9.Name = "panel9";
            panel9.Size = new Size(344, 280);
            panel9.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(222, 227, 234);
            label3.Font = new Font("Century Gothic", 13F);
            label3.ForeColor = SystemColors.ControlText;
            label3.Location = new Point(7, 215);
            label3.Name = "label3";
            label3.Size = new Size(334, 25);
            label3.TabIndex = 3;
            label3.Text = "BUSCAR MARCA REGISTRADA";
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = Color.FromArgb(222, 227, 234);
            iconPictureBox3.ForeColor = Color.FromArgb(255, 183, 77);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconPictureBox3.IconColor = Color.FromArgb(255, 183, 77);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 163;
            iconPictureBox3.Location = new Point(94, 40);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(164, 163);
            iconPictureBox3.TabIndex = 3;
            iconPictureBox3.TabStop = false;
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BorderColor = Color.FromArgb(222, 227, 234);
            roundedButton4.BorderRadius = 40;
            roundedButton4.BorderSize = 0;
            roundedButton4.Dock = DockStyle.Fill;
            roundedButton4.Enabled = false;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(0, 0);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(344, 280);
            roundedButton4.TabIndex = 1;
            roundedButton4.TextColor = Color.White;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            panel5.Controls.Add(panel8);
            panel5.Dock = DockStyle.Left;
            panel5.Location = new Point(390, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(392, 346);
            panel5.TabIndex = 1;
            // 
            // panel8
            // 
            panel8.Controls.Add(label2);
            panel8.Controls.Add(iconPictureBox2);
            panel8.Controls.Add(roundedButton3);
            panel8.Location = new Point(23, 35);
            panel8.Name = "panel8";
            panel8.Size = new Size(344, 280);
            panel8.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(222, 227, 234);
            label2.Font = new Font("Century Gothic", 13F);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(113, 215);
            label2.Name = "label2";
            label2.Size = new Size(116, 25);
            label2.TabIndex = 2;
            label2.Text = "REPORTES";
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.BackColor = Color.FromArgb(222, 227, 234);
            iconPictureBox2.ForeColor = Color.FromArgb(38, 166, 154);
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.ChartSimple;
            iconPictureBox2.IconColor = Color.FromArgb(38, 166, 154);
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 163;
            iconPictureBox2.Location = new Point(94, 40);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(164, 163);
            iconPictureBox2.TabIndex = 2;
            iconPictureBox2.TabStop = false;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton3.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton3.BorderColor = Color.FromArgb(222, 227, 234);
            roundedButton3.BorderRadius = 40;
            roundedButton3.BorderSize = 0;
            roundedButton3.Dock = DockStyle.Fill;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(0, 0);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(344, 280);
            roundedButton3.TabIndex = 1;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            panel4.Controls.Add(panel7);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(390, 346);
            panel4.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.Controls.Add(label1);
            panel7.Controls.Add(iconPictureBox1);
            panel7.Controls.Add(panel10);
            panel7.Controls.Add(roundedButton2);
            panel7.Location = new Point(22, 35);
            panel7.Name = "panel7";
            panel7.Size = new Size(344, 280);
            panel7.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(222, 227, 234);
            label1.Font = new Font("Century Gothic", 13F);
            label1.Location = new Point(18, 215);
            label1.Name = "label1";
            label1.Size = new Size(303, 25);
            label1.TabIndex = 1;
            label1.Text = "INGRESAR TRÁMITE INICIAL";
            label1.Click += label1_Click;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.FromArgb(222, 227, 234);
            iconPictureBox1.ForeColor = Color.FromArgb(60, 120, 172);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.FileContract;
            iconPictureBox1.IconColor = Color.FromArgb(60, 120, 172);
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 163;
            iconPictureBox1.Location = new Point(86, 40);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(164, 163);
            iconPictureBox1.TabIndex = 1;
            iconPictureBox1.TabStop = false;
            iconPictureBox1.Click += iconPictureBox1_Click;
            // 
            // panel10
            // 
            panel10.BackColor = Color.FromArgb(222, 227, 234);
            panel10.Location = new Point(3, 19);
            panel10.Name = "panel10";
            panel10.Size = new Size(338, 242);
            panel10.TabIndex = 1;
            panel10.Click += panel10_Click;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(222, 227, 234);
            roundedButton2.BackgroundColor = Color.FromArgb(222, 227, 234);
            roundedButton2.BorderColor = Color.FromArgb(222, 227, 234);
            roundedButton2.BorderRadius = 40;
            roundedButton2.BorderSize = 0;
            roundedButton2.Dock = DockStyle.Fill;
            roundedButton2.Enabled = false;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(0, 0);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(344, 280);
            roundedButton2.TabIndex = 0;
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            roundedButton2.MouseClick += roundedButton2_MouseClick;
            roundedButton2.MouseDown += roundedButton2_MouseDown;
            // 
            // FrmDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmDashboard";
            Text = "FrmDashboard";
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).EndInit();
            panel2.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            panel5.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            panel4.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Panel panel7;
        private Panel panel9;
        private Panel panel8;
        private Panel panel3;
        private Clases.RoundedButton roundedButton1;
        private Clases.RoundedButton roundedButton2;
        private Clases.RoundedButton roundedButton4;
        private Clases.RoundedButton roundedButton3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label label1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Label label2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private Label label3;
        private Label label4;
        private DataGridView dtgVencimientos;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private Clases.RoundedButton roundedButton5;
        private Panel panel10;
    }
}