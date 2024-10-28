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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            txtBuscar = new TextBox();
            dtgVencimientos = new DataGridView();
            label1 = new Label();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).BeginInit();
            SuspendLayout();
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.None;
            iconButton1.BackColor = Color.FromArgb(255, 164, 0);
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconButton1.IconColor = Color.Black;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 30;
            iconButton1.Location = new Point(861, 104);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(148, 34);
            iconButton1.TabIndex = 3;
            iconButton1.Text = "Buscar";
            iconButton1.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.None;
            txtBuscar.Location = new Point(32, 106);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(823, 26);
            txtBuscar.TabIndex = 2;
            // 
            // dtgVencimientos
            // 
            dtgVencimientos.AllowUserToAddRows = false;
            dtgVencimientos.AllowUserToDeleteRows = false;
            dtgVencimientos.AllowUserToOrderColumns = true;
            dtgVencimientos.AllowUserToResizeRows = false;
            dtgVencimientos.Anchor = AnchorStyles.None;
            dtgVencimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgVencimientos.BackgroundColor = Color.White;
            dtgVencimientos.BorderStyle = BorderStyle.None;
            dtgVencimientos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgVencimientos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgVencimientos.ColumnHeadersHeight = 40;
            dtgVencimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dtgVencimientos.DefaultCellStyle = dataGridViewCellStyle1;
            dtgVencimientos.EnableHeadersVisualStyles = false;
            dtgVencimientos.GridColor = Color.LightGray;
            dtgVencimientos.Location = new Point(32, 198);
            dtgVencimientos.Name = "dtgVencimientos";
            dtgVencimientos.ReadOnly = true;
            dtgVencimientos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgVencimientos.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgVencimientos.RowHeadersWidth = 40;
            dtgVencimientos.Size = new Size(895, 504);
            dtgVencimientos.TabIndex = 4;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 19F);
            label1.Location = new Point(322, 34);
            label1.Name = "label1";
            label1.Size = new Size(370, 39);
            label1.TabIndex = 5;
            label1.Text = "Próximos vencimientos";
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.None;
            iconButton2.BackColor = Color.FromArgb(0, 150, 136);
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.ForeColor = Color.White;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Envelope;
            iconButton2.IconColor = Color.White;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 30;
            iconButton2.Location = new Point(947, 241);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(103, 77);
            iconButton2.TabIndex = 6;
            iconButton2.Text = "Enviar correo";
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // FrmVencimientos
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1071, 729);
            Controls.Add(iconButton2);
            Controls.Add(label1);
            Controls.Add(dtgVencimientos);
            Controls.Add(iconButton1);
            Controls.Add(txtBuscar);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmVencimientos";
            Text = "FrmVencimientos";
            Load += FrmVencimientos_Load;
            ((System.ComponentModel.ISupportInitialize)dtgVencimientos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FontAwesome.Sharp.IconButton iconButton1;
        private TextBox txtBuscar;
        private DataGridView dtgVencimientos;
        private Label label1;
        private FontAwesome.Sharp.IconButton iconButton2;
    }
}