namespace Presentacion.Vencimientos
{
    partial class FrmVencimientos
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
            dtgVencimientos = new DataGridView();
            panel1 = new Panel();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label2 = new Label();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            textBox1 = new TextBox();
            label3 = new Label();
            roundedButton3 = new Clases.RoundedButton();
            roundedButton5 = new Clases.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
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
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgVencimientos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgVencimientos.ColumnHeadersHeight = 40;
            dtgVencimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgVencimientos.EnableHeadersVisualStyles = false;
            dtgVencimientos.GridColor = Color.LightGray;
            dtgVencimientos.Location = new Point(16, 14);
            dtgVencimientos.Name = "dtgVencimientos";
            dtgVencimientos.ReadOnly = true;
            dtgVencimientos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgVencimientos.RowHeadersWidth = 40;
            dtgVencimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgVencimientos.Size = new Size(1072, 504);
            dtgVencimientos.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgVencimientos);
            panel1.Location = new Point(14, 231);
            panel1.Name = "panel1";
            panel1.Size = new Size(1106, 539);
            panel1.TabIndex = 6;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.Anchor = AnchorStyles.Top;
            iconPictureBox1.BackColor = Color.FromArgb(175, 192, 218);
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 40;
            iconPictureBox1.Location = new Point(277, 22);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(40, 40);
            iconPictureBox1.TabIndex = 40;
            iconPictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(175, 192, 218);
            label2.Font = new Font("Century Gothic", 15F);
            label2.Location = new Point(328, 22);
            label2.Name = "label2";
            label2.Size = new Size(336, 31);
            label2.TabIndex = 34;
            label2.Text = "PRÓXIMOS VENCIMIENTOS";
            // 
            // ibtnBuscar
            // 
            ibtnBuscar.Anchor = AnchorStyles.Top;
            ibtnBuscar.BackColor = Color.FromArgb(251, 140, 0);
            ibtnBuscar.FlatAppearance.BorderSize = 0;
            ibtnBuscar.FlatStyle = FlatStyle.Flat;
            ibtnBuscar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            ibtnBuscar.ForeColor = Color.White;
            ibtnBuscar.IconChar = FontAwesome.Sharp.IconChar.Search;
            ibtnBuscar.IconColor = Color.White;
            ibtnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnBuscar.IconSize = 30;
            ibtnBuscar.Location = new Point(639, 142);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(144, 32);
            ibtnBuscar.TabIndex = 37;
            ibtnBuscar.Text = "BUSCAR";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top;
            textBox1.Location = new Point(309, 146);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(301, 26);
            textBox1.TabIndex = 36;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(236, 236, 238);
            label3.Font = new Font("Century Gothic", 9F);
            label3.Location = new Point(144, 146);
            label3.Name = "label3";
            label3.Size = new Size(148, 20);
            label3.TabIndex = 35;
            label3.Text = "Buscar por nombre";
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
            roundedButton3.Location = new Point(132, 126);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(683, 61);
            roundedButton3.TabIndex = 38;
            roundedButton3.Text = "roundedButton3";
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
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
            roundedButton5.Location = new Point(132, 12);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(683, 61);
            roundedButton5.TabIndex = 39;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // FrmVencimientos
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1132, 796);
            Controls.Add(iconPictureBox1);
            Controls.Add(label2);
            Controls.Add(ibtnBuscar);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(roundedButton3);
            Controls.Add(panel1);
            Controls.Add(roundedButton5);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmVencimientos";
            Text = "FrmVencimientos";
            Load += FrmVencimientos_Load;
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dtgVencimientos;
        private Panel panel1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label label2;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private TextBox textBox1;
        private Label label3;
        private Clases.RoundedButton roundedButton3;
        private Clases.RoundedButton roundedButton5;
    }
}