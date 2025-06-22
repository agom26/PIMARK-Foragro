namespace Presentacion.Reportes
{
    partial class FrmMostrarAgentesReportes
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
            panelInferior = new Panel();
            tblLayoutPrincipal = new TableLayoutPanel();
            panel1 = new Panel();
            panel4 = new Panel();
            txtBuscar = new TextBox();
            btnBuscar = new FontAwesome.Sharp.IconButton();
            btnX = new FontAwesome.Sharp.IconButton();
            panel6 = new Panel();
            label1 = new Label();
            lblTotalRows = new Label();
            lblTotalPages = new Label();
            label2 = new Label();
            lblCurrentPage = new Label();
            label10 = new Label();
            panel2 = new Panel();
            dtgAgentes = new DataGridView();
            panel3 = new Panel();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            btnLast = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            btnNext = new FontAwesome.Sharp.IconButton();
            btnFirst = new FontAwesome.Sharp.IconButton();
            btnPrev = new FontAwesome.Sharp.IconButton();
            panelSuperior = new Panel();
            button1 = new Button();
            panelInferior.SuspendLayout();
            tblLayoutPrincipal.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).BeginInit();
            panel3.SuspendLayout();
            panelSuperior.SuspendLayout();
            SuspendLayout();
            // 
            // panelInferior
            // 
            panelInferior.BackColor = Color.Gainsboro;
            panelInferior.Controls.Add(tblLayoutPrincipal);
            panelInferior.Dock = DockStyle.Fill;
            panelInferior.Location = new Point(0, 34);
            panelInferior.Margin = new Padding(3, 2, 3, 2);
            panelInferior.Name = "panelInferior";
            panelInferior.Size = new Size(804, 451);
            panelInferior.TabIndex = 0;
            // 
            // tblLayoutPrincipal
            // 
            tblLayoutPrincipal.ColumnCount = 1;
            tblLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLayoutPrincipal.Controls.Add(panel1, 0, 0);
            tblLayoutPrincipal.Controls.Add(panel2, 0, 1);
            tblLayoutPrincipal.Controls.Add(panel3, 0, 2);
            tblLayoutPrincipal.Dock = DockStyle.Fill;
            tblLayoutPrincipal.Location = new Point(0, 0);
            tblLayoutPrincipal.Name = "tblLayoutPrincipal";
            tblLayoutPrincipal.RowCount = 3;
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 23.006134F));
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 76.993866F));
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            tblLayoutPrincipal.Size = new Size(804, 451);
            tblLayoutPrincipal.TabIndex = 99;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel6);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(798, 76);
            panel1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(txtBuscar);
            panel4.Controls.Add(btnBuscar);
            panel4.Controls.Add(btnX);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(798, 53);
            panel4.TabIndex = 2;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top;
            txtBuscar.Location = new Point(117, 13);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(418, 23);
            txtBuscar.TabIndex = 79;
            txtBuscar.KeyDown += txtBuscar_KeyDown_1;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top;
            btnBuscar.BackColor = Color.FromArgb(251, 140, 0);
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnBuscar.IconColor = Color.White;
            btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnBuscar.IconSize = 18;
            btnBuscar.ImageAlign = ContentAlignment.MiddleLeft;
            btnBuscar.Location = new Point(575, 13);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(107, 27);
            btnBuscar.TabIndex = 80;
            btnBuscar.Text = "BUSCAR";
            btnBuscar.TextAlign = ContentAlignment.MiddleRight;
            btnBuscar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnX
            // 
            btnX.Anchor = AnchorStyles.Top;
            btnX.BackColor = Color.Gainsboro;
            btnX.FlatAppearance.BorderSize = 0;
            btnX.FlatStyle = FlatStyle.Flat;
            btnX.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnX.IconColor = Color.Black;
            btnX.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnX.IconSize = 25;
            btnX.Location = new Point(543, 13);
            btnX.Name = "btnX";
            btnX.Size = new Size(26, 27);
            btnX.TabIndex = 81;
            btnX.UseVisualStyleBackColor = false;
            btnX.Click += btnX_Click;
            // 
            // panel6
            // 
            panel6.Controls.Add(label1);
            panel6.Controls.Add(lblTotalRows);
            panel6.Controls.Add(lblTotalPages);
            panel6.Controls.Add(label2);
            panel6.Controls.Add(lblCurrentPage);
            panel6.Controls.Add(label10);
            panel6.Dock = DockStyle.Bottom;
            panel6.Location = new Point(0, 52);
            panel6.Name = "panel6";
            panel6.Size = new Size(798, 24);
            panel6.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 9F);
            label1.Location = new Point(39, 7);
            label1.Name = "label1";
            label1.Size = new Size(115, 17);
            label1.TabIndex = 89;
            label1.Text = "Total de registros: ";
            // 
            // lblTotalRows
            // 
            lblTotalRows.AutoSize = true;
            lblTotalRows.Font = new Font("Century Gothic", 9F);
            lblTotalRows.Location = new Point(165, 7);
            lblTotalRows.Name = "lblTotalRows";
            lblTotalRows.Size = new Size(15, 17);
            lblTotalRows.TabIndex = 91;
            lblTotalRows.Text = "0";
            // 
            // lblTotalPages
            // 
            lblTotalPages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalPages.AutoSize = true;
            lblTotalPages.Font = new Font("Century Gothic", 9F);
            lblTotalPages.Location = new Point(736, 7);
            lblTotalPages.Name = "lblTotalPages";
            lblTotalPages.Size = new Size(15, 17);
            lblTotalPages.TabIndex = 94;
            lblTotalPages.Text = "0";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F);
            label2.Location = new Point(618, 7);
            label2.Name = "label2";
            label2.Size = new Size(49, 17);
            label2.TabIndex = 90;
            label2.Text = "Página";
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCurrentPage.AutoSize = true;
            lblCurrentPage.Font = new Font("Century Gothic", 9F);
            lblCurrentPage.Location = new Point(676, 7);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(15, 17);
            lblCurrentPage.TabIndex = 92;
            lblCurrentPage.Text = "0";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 9F);
            label10.Location = new Point(706, 7);
            label10.Name = "label10";
            label10.Size = new Size(24, 17);
            label10.TabIndex = 93;
            label10.Text = "de";
            // 
            // panel2
            // 
            panel2.Controls.Add(dtgAgentes);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 85);
            panel2.Name = "panel2";
            panel2.Size = new Size(798, 270);
            panel2.TabIndex = 1;
            // 
            // dtgAgentes
            // 
            dtgAgentes.AllowUserToResizeRows = false;
            dtgAgentes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dtgAgentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgAgentes.BackgroundColor = Color.White;
            dtgAgentes.BorderStyle = BorderStyle.None;
            dtgAgentes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgAgentes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgAgentes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgAgentes.ColumnHeadersHeight = 40;
            dtgAgentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtgAgentes.DefaultCellStyle = dataGridViewCellStyle2;
            dtgAgentes.EnableHeadersVisualStyles = false;
            dtgAgentes.GridColor = Color.LightGray;
            dtgAgentes.Location = new Point(39, 4);
            dtgAgentes.Margin = new Padding(3, 2, 3, 2);
            dtgAgentes.Name = "dtgAgentes";
            dtgAgentes.ReadOnly = true;
            dtgAgentes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgAgentes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgAgentes.RowHeadersWidth = 40;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgAgentes.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dtgAgentes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgAgentes.Size = new Size(709, 267);
            dtgAgentes.TabIndex = 0;
            dtgAgentes.CellContentClick += dataGridView1_CellContentClick;
            // 
            // panel3
            // 
            panel3.Controls.Add(iconButton3);
            panel3.Controls.Add(btnLast);
            panel3.Controls.Add(iconButton2);
            panel3.Controls.Add(btnNext);
            panel3.Controls.Add(btnFirst);
            panel3.Controls.Add(btnPrev);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 361);
            panel3.Name = "panel3";
            panel3.Size = new Size(798, 87);
            panel3.TabIndex = 2;
            // 
            // iconButton3
            // 
            iconButton3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
            iconButton3.Location = new Point(401, 40);
            iconButton3.Margin = new Padding(3, 2, 3, 2);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(160, 40);
            iconButton3.TabIndex = 4;
            iconButton3.Text = "SELECCIONAR";
            iconButton3.TextAlign = ContentAlignment.MiddleRight;
            iconButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
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
            btnLast.Location = new Point(657, 4);
            btnLast.Margin = new Padding(3, 2, 3, 2);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(91, 31);
            btnLast.TabIndex = 98;
            btnLast.Text = "ÚLTIMA";
            btnLast.UseVisualStyleBackColor = false;
            btnLast.Click += btnLast_Click;
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconButton2.BackColor = Color.White;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton2.Location = new Point(588, 40);
            iconButton2.Margin = new Padding(3, 2, 3, 2);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(160, 40);
            iconButton2.TabIndex = 3;
            iconButton2.Text = "CANCELAR";
            iconButton2.TextAlign = ContentAlignment.MiddleRight;
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
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
            btnNext.Location = new Point(553, 5);
            btnNext.Margin = new Padding(3, 2, 3, 2);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(91, 31);
            btnNext.TabIndex = 97;
            btnNext.Text = ">>";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
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
            btnFirst.Location = new Point(336, 6);
            btnFirst.Margin = new Padding(3, 2, 3, 2);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(91, 31);
            btnFirst.TabIndex = 95;
            btnFirst.Text = "PRIMERA";
            btnFirst.UseVisualStyleBackColor = false;
            btnFirst.Click += btnFirst_Click;
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
            btnPrev.Location = new Point(445, 6);
            btnPrev.Margin = new Padding(3, 2, 3, 2);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(91, 31);
            btnPrev.TabIndex = 96;
            btnPrev.Text = "<<";
            btnPrev.UseVisualStyleBackColor = false;
            btnPrev.Click += btnPrev_Click;
            // 
            // panelSuperior
            // 
            panelSuperior.BackColor = Color.FromArgb(34, 77, 112);
            panelSuperior.Controls.Add(button1);
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.Location = new Point(0, 0);
            panelSuperior.Margin = new Padding(3, 2, 3, 2);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(804, 34);
            panelSuperior.TabIndex = 1;
            panelSuperior.MouseDown += panelSuperior_MouseDown;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 12F);
            button1.ForeColor = Color.White;
            button1.Location = new Point(759, 0);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(45, 34);
            button1.TabIndex = 0;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FrmMostrarAgentesReportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(804, 485);
            ControlBox = false;
            Controls.Add(panelInferior);
            Controls.Add(panelSuperior);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmMostrarAgentesReportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmMostrarAgentesReportes";
            Load += FrmMostrarAgentesReportes_Load;
            panelInferior.ResumeLayout(false);
            tblLayoutPrincipal.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panelSuperior.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelInferior;
        private DataGridView dtgAgentes;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private Panel panelSuperior;
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
        private TableLayoutPanel tblLayoutPrincipal;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel6;
        private Panel panel4;
        private TextBox txtBuscar;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private FontAwesome.Sharp.IconButton btnX;
    }
}