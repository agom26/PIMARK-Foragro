namespace Presentacion.Marcas_Internacionales
{
    partial class FrmMostrarClientes
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            BtnSalir = new Button();
            panelSuperior = new Panel();
            panelInferior = new Panel();
            tblLayoutPrincipal = new TableLayoutPanel();
            panel1 = new Panel();
            panel5 = new Panel();
            label1 = new Label();
            lblTotalRows = new Label();
            lblTotalPages = new Label();
            label2 = new Label();
            lblCurrentPage = new Label();
            label10 = new Label();
            panel4 = new Panel();
            txtBuscar = new TextBox();
            btnBuscar = new FontAwesome.Sharp.IconButton();
            btnX = new FontAwesome.Sharp.IconButton();
            panel2 = new Panel();
            dtgClientes = new DataGridView();
            panel3 = new Panel();
            btnLast = new FontAwesome.Sharp.IconButton();
            btnNext = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            btnPrev = new FontAwesome.Sharp.IconButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            btnFirst = new FontAwesome.Sharp.IconButton();
            panelSuperior.SuspendLayout();
            panelInferior.SuspendLayout();
            tblLayoutPrincipal.SuspendLayout();
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgClientes).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // BtnSalir
            // 
            BtnSalir.Dock = DockStyle.Right;
            BtnSalir.FlatAppearance.BorderSize = 0;
            BtnSalir.FlatStyle = FlatStyle.Flat;
            BtnSalir.Font = new Font("Century Gothic", 10F);
            BtnSalir.ForeColor = Color.White;
            BtnSalir.Location = new Point(753, 0);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.Size = new Size(51, 34);
            BtnSalir.TabIndex = 10;
            BtnSalir.Text = "X";
            BtnSalir.UseVisualStyleBackColor = true;
            BtnSalir.Click += button1_Click;
            // 
            // panelSuperior
            // 
            panelSuperior.BackColor = Color.FromArgb(34, 77, 112);
            panelSuperior.Controls.Add(BtnSalir);
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.Location = new Point(0, 0);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(804, 34);
            panelSuperior.TabIndex = 2;
            panelSuperior.Paint += panelSuperior_Paint;
            panelSuperior.MouseDown += panelSuperior_MouseDown;
            // 
            // panelInferior
            // 
            panelInferior.BackColor = Color.Gainsboro;
            panelInferior.Controls.Add(tblLayoutPrincipal);
            panelInferior.Dock = DockStyle.Bottom;
            panelInferior.Font = new Font("Century Gothic", 9F);
            panelInferior.Location = new Point(0, 34);
            panelInferior.Name = "panelInferior";
            panelInferior.Size = new Size(804, 509);
            panelInferior.TabIndex = 3;
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
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 20.7729473F));
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 79.22705F));
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            tblLayoutPrincipal.Size = new Size(804, 509);
            tblLayoutPrincipal.TabIndex = 105;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(798, 78);
            panel1.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(label1);
            panel5.Controls.Add(lblTotalRows);
            panel5.Controls.Add(lblTotalPages);
            panel5.Controls.Add(label2);
            panel5.Controls.Add(lblCurrentPage);
            panel5.Controls.Add(label10);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(0, 54);
            panel5.Name = "panel5";
            panel5.Size = new Size(798, 24);
            panel5.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 9F);
            label1.Location = new Point(44, 7);
            label1.Name = "label1";
            label1.Size = new Size(115, 17);
            label1.TabIndex = 99;
            label1.Text = "Total de registros: ";
            // 
            // lblTotalRows
            // 
            lblTotalRows.AutoSize = true;
            lblTotalRows.Font = new Font("Century Gothic", 9F);
            lblTotalRows.Location = new Point(199, 7);
            lblTotalRows.Name = "lblTotalRows";
            lblTotalRows.Size = new Size(15, 17);
            lblTotalRows.TabIndex = 101;
            lblTotalRows.Text = "0";
            // 
            // lblTotalPages
            // 
            lblTotalPages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalPages.AutoSize = true;
            lblTotalPages.Font = new Font("Century Gothic", 9F);
            lblTotalPages.Location = new Point(714, 7);
            lblTotalPages.Name = "lblTotalPages";
            lblTotalPages.Size = new Size(15, 17);
            lblTotalPages.TabIndex = 104;
            lblTotalPages.Text = "0";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F);
            label2.Location = new Point(568, 7);
            label2.Name = "label2";
            label2.Size = new Size(49, 17);
            label2.TabIndex = 100;
            label2.Text = "Página";
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCurrentPage.AutoSize = true;
            lblCurrentPage.Font = new Font("Century Gothic", 9F);
            lblCurrentPage.Location = new Point(645, 7);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(15, 17);
            lblCurrentPage.TabIndex = 102;
            lblCurrentPage.Text = "0";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 9F);
            label10.Location = new Point(679, 7);
            label10.Name = "label10";
            label10.Size = new Size(24, 17);
            label10.TabIndex = 103;
            label10.Text = "de";
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
            panel4.TabIndex = 0;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top;
            txtBuscar.Font = new Font("Century Gothic", 12F);
            txtBuscar.Location = new Point(83, 13);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(418, 27);
            txtBuscar.TabIndex = 1;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
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
            btnBuscar.Location = new Point(541, 13);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(107, 27);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "BUSCAR";
            btnBuscar.TextAlign = ContentAlignment.MiddleRight;
            btnBuscar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += iconButton1_Click;
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
            btnX.Location = new Point(509, 13);
            btnX.Name = "btnX";
            btnX.Size = new Size(26, 32);
            btnX.TabIndex = 2;
            btnX.UseVisualStyleBackColor = false;
            btnX.Click += iconButton6_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(dtgClientes);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 87);
            panel2.Name = "panel2";
            panel2.Size = new Size(798, 314);
            panel2.TabIndex = 1;
            // 
            // dtgClientes
            // 
            dtgClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dtgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgClientes.BackgroundColor = Color.White;
            dtgClientes.BorderStyle = BorderStyle.None;
            dtgClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgClientes.ColumnHeadersHeight = 40;
            dtgClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgClientes.EnableHeadersVisualStyles = false;
            dtgClientes.GridColor = Color.LightGray;
            dtgClientes.Location = new Point(44, 4);
            dtgClientes.Name = "dtgClientes";
            dtgClientes.ReadOnly = true;
            dtgClientes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgClientes.RowHeadersWidth = 40;
            dtgClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgClientes.Size = new Size(695, 310);
            dtgClientes.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnLast);
            panel3.Controls.Add(btnNext);
            panel3.Controls.Add(iconButton2);
            panel3.Controls.Add(btnPrev);
            panel3.Controls.Add(iconButton3);
            panel3.Controls.Add(btnFirst);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 407);
            panel3.Name = "panel3";
            panel3.Size = new Size(798, 99);
            panel3.TabIndex = 2;
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
            btnLast.Location = new Point(635, 3);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(104, 33);
            btnLast.TabIndex = 7;
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
            btnNext.Location = new Point(516, 4);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(104, 33);
            btnNext.TabIndex = 6;
            btnNext.Text = ">>";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
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
            iconButton2.IconSize = 30;
            iconButton2.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton2.Location = new Point(579, 45);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(160, 45);
            iconButton2.TabIndex = 9;
            iconButton2.Text = "CANCELAR";
            iconButton2.TextAlign = ContentAlignment.MiddleRight;
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
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
            btnPrev.Location = new Point(393, 5);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(104, 33);
            btnPrev.TabIndex = 5;
            btnPrev.Text = "<<";
            btnPrev.UseVisualStyleBackColor = false;
            btnPrev.Click += btnPrev_Click;
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
            iconButton3.IconSize = 30;
            iconButton3.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton3.Location = new Point(362, 45);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(160, 45);
            iconButton3.TabIndex = 8;
            iconButton3.Text = "SELECCIONAR";
            iconButton3.TextAlign = ContentAlignment.MiddleRight;
            iconButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
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
            btnFirst.Location = new Point(268, 5);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(104, 33);
            btnFirst.TabIndex = 4;
            btnFirst.Text = "PRIMERA";
            btnFirst.UseVisualStyleBackColor = false;
            btnFirst.Click += btnFirst_Click;
            // 
            // FrmMostrarClientes
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(804, 543);
            Controls.Add(panelInferior);
            Controls.Add(panelSuperior);
            Font = new Font("Century Gothic", 12F);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmMostrarClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmMostrarClientes";
            Load += FrmMostrarClientes_Load;
            panelSuperior.ResumeLayout(false);
            panelInferior.ResumeLayout(false);
            tblLayoutPrincipal.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgClientes).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button BtnSalir;
        private Panel panelSuperior;
        private Panel panelInferior;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton iconButton2;
        private DataGridView dtgClientes;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private TextBox txtBuscar;
        private FontAwesome.Sharp.IconButton btnX;
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
        private Panel panel4;
        private Panel panel5;
    }
}