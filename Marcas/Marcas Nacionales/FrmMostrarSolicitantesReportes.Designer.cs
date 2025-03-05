namespace Presentacion.Marcas_Internacionales
{
    partial class FrmMostrarSolicitantesReportes
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
            mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            panel1 = new Panel();
            iconButton6 = new FontAwesome.Sharp.IconButton();
            lblTotalPages = new Label();
            label10 = new Label();
            lblCurrentPage = new Label();
            lblTotalRows = new Label();
            label2 = new Label();
            label1 = new Label();
            btnLast = new FontAwesome.Sharp.IconButton();
            btnNext = new FontAwesome.Sharp.IconButton();
            btnPrev = new FontAwesome.Sharp.IconButton();
            btnFirst = new FontAwesome.Sharp.IconButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            dtgTitulares = new DataGridView();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            txtBuscar = new TextBox();
            panel2 = new Panel();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgTitulares).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // mySqlCommand1
            // 
            mySqlCommand1.CacheAge = 0;
            mySqlCommand1.Connection = null;
            mySqlCommand1.EnableCaching = false;
            mySqlCommand1.Transaction = null;
            // 
            // panel1
            // 
            panel1.Controls.Add(iconButton6);
            panel1.Controls.Add(lblTotalPages);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(lblCurrentPage);
            panel1.Controls.Add(lblTotalRows);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnLast);
            panel1.Controls.Add(btnNext);
            panel1.Controls.Add(btnPrev);
            panel1.Controls.Add(btnFirst);
            panel1.Controls.Add(iconButton3);
            panel1.Controls.Add(iconButton2);
            panel1.Controls.Add(dtgTitulares);
            panel1.Controls.Add(iconButton1);
            panel1.Controls.Add(txtBuscar);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 38);
            panel1.Name = "panel1";
            panel1.Size = new Size(1086, 515);
            panel1.TabIndex = 0;
            // 
            // iconButton6
            // 
            iconButton6.BackColor = Color.Gainsboro;
            iconButton6.FlatAppearance.BorderSize = 0;
            iconButton6.FlatStyle = FlatStyle.Flat;
            iconButton6.IconChar = FontAwesome.Sharp.IconChar.Close;
            iconButton6.IconColor = Color.Black;
            iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton6.IconSize = 25;
            iconButton6.Location = new Point(652, 27);
            iconButton6.Name = "iconButton6";
            iconButton6.Size = new Size(26, 32);
            iconButton6.TabIndex = 2;
            iconButton6.UseVisualStyleBackColor = false;
            iconButton6.Click += iconButton6_Click;
            // 
            // lblTotalPages
            // 
            lblTotalPages.AutoSize = true;
            lblTotalPages.Font = new Font("Century Gothic", 9F);
            lblTotalPages.Location = new Point(996, 68);
            lblTotalPages.Name = "lblTotalPages";
            lblTotalPages.Size = new Size(15, 17);
            lblTotalPages.TabIndex = 83;
            lblTotalPages.Text = "0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 9F);
            label10.Location = new Point(961, 68);
            label10.Name = "label10";
            label10.Size = new Size(24, 17);
            label10.TabIndex = 82;
            label10.Text = "de";
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.AutoSize = true;
            lblCurrentPage.Font = new Font("Century Gothic", 9F);
            lblCurrentPage.Location = new Point(927, 68);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(15, 17);
            lblCurrentPage.TabIndex = 81;
            lblCurrentPage.Text = "0";
            // 
            // lblTotalRows
            // 
            lblTotalRows.AutoSize = true;
            lblTotalRows.Font = new Font("Century Gothic", 9F);
            lblTotalRows.Location = new Point(202, 68);
            lblTotalRows.Name = "lblTotalRows";
            lblTotalRows.Size = new Size(15, 17);
            lblTotalRows.TabIndex = 80;
            lblTotalRows.Text = "0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F);
            label2.Location = new Point(861, 68);
            label2.Name = "label2";
            label2.Size = new Size(49, 17);
            label2.TabIndex = 79;
            label2.Text = "Página";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 9F);
            label1.Location = new Point(47, 68);
            label1.Name = "label1";
            label1.Size = new Size(115, 17);
            label1.TabIndex = 78;
            label1.Text = "Total de registros: ";
            // 
            // btnLast
            // 
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
            btnLast.Location = new Point(920, 407);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(104, 33);
            btnLast.TabIndex = 7;
            btnLast.Text = "ÚLTIMA";
            btnLast.UseVisualStyleBackColor = false;
            btnLast.Click += btnLast_Click;
            // 
            // btnNext
            // 
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
            btnNext.Location = new Point(801, 406);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(104, 33);
            btnNext.TabIndex = 6;
            btnNext.Text = ">>";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // 
            // btnPrev
            // 
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
            btnPrev.Location = new Point(678, 407);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(104, 33);
            btnPrev.TabIndex = 5;
            btnPrev.Text = "<<";
            btnPrev.UseVisualStyleBackColor = false;
            btnPrev.Click += btnPrev_Click;
            // 
            // btnFirst
            // 
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
            btnFirst.Location = new Point(553, 407);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(104, 33);
            btnFirst.TabIndex = 4;
            btnFirst.Text = "PRIMERA";
            btnFirst.UseVisualStyleBackColor = false;
            btnFirst.Click += btnFirst_Click;
            // 
            // iconButton3
            // 
            iconButton3.BackColor = Color.FromArgb(1, 87, 155);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Check;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 25;
            iconButton3.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton3.Location = new Point(628, 458);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(187, 40);
            iconButton3.TabIndex = 8;
            iconButton3.Text = "SELECCIONAR";
            iconButton3.TextAlign = ContentAlignment.MiddleRight;
            iconButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton2
            // 
            iconButton2.BackColor = Color.White;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton2.Location = new Point(845, 458);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(179, 40);
            iconButton2.TabIndex = 9;
            iconButton2.Text = "CANCELAR";
            iconButton2.TextAlign = ContentAlignment.MiddleRight;
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // dtgTitulares
            // 
            dtgTitulares.AllowUserToAddRows = false;
            dtgTitulares.AllowUserToDeleteRows = false;
            dtgTitulares.AllowUserToOrderColumns = true;
            dtgTitulares.AllowUserToResizeRows = false;
            dtgTitulares.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgTitulares.BackgroundColor = Color.White;
            dtgTitulares.BorderStyle = BorderStyle.None;
            dtgTitulares.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgTitulares.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgTitulares.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgTitulares.ColumnHeadersHeight = 40;
            dtgTitulares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtgTitulares.DefaultCellStyle = dataGridViewCellStyle2;
            dtgTitulares.EnableHeadersVisualStyles = false;
            dtgTitulares.GridColor = Color.LightGray;
            dtgTitulares.Location = new Point(47, 91);
            dtgTitulares.Name = "dtgTitulares";
            dtgTitulares.ReadOnly = true;
            dtgTitulares.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgTitulares.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgTitulares.RowHeadersWidth = 40;
            dtgTitulares.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgTitulares.Size = new Size(977, 308);
            dtgTitulares.TabIndex = 11;
            dtgTitulares.CellContentClick += dtgTitulares_CellContentClick;
            // 
            // iconButton1
            // 
            iconButton1.BackColor = Color.FromArgb(251, 140, 0);
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            iconButton1.ForeColor = Color.White;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 25;
            iconButton1.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton1.Location = new Point(693, 24);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(148, 34);
            iconButton1.TabIndex = 3;
            iconButton1.Text = "BUSCAR";
            iconButton1.TextAlign = ContentAlignment.MiddleRight;
            iconButton1.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(229, 27);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(409, 27);
            txtBuscar.TabIndex = 1;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(34, 77, 112);
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1086, 34);
            panel2.TabIndex = 1;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(1029, 3);
            button1.Name = "button1";
            button1.Size = new Size(51, 29);
            button1.TabIndex = 10;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FrmMostrarSolicitantesReportes
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1086, 553);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Century Gothic", 12F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmMostrarSolicitantesReportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmMostrarSolicitantesReportes";
            Load += FrmMostrarSolicitantesReportes_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgTitulares).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private TextBox txtBuscar;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton iconButton2;
        private DataGridView dtgTitulares;
        private Panel panel2;
        private Button button1;
        private FontAwesome.Sharp.IconButton btnLast;
        private FontAwesome.Sharp.IconButton btnNext;
        private FontAwesome.Sharp.IconButton btnPrev;
        private FontAwesome.Sharp.IconButton btnFirst;
        private Label lblTotalPages;
        private Label label10;
        private Label lblCurrentPage;
        private Label lblTotalRows;
        private Label label2;
        private Label label1;
        private FontAwesome.Sharp.IconButton iconButton6;
    }
}