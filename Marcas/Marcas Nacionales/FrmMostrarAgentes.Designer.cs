namespace Presentacion.Marcas_Nacionales
{
    partial class FrmMostrarAgentes
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
            panel1 = new Panel();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            dtgAgentes = new DataGridView();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            txtBuscar = new TextBox();
            panel2 = new Panel();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gainsboro;
            panel1.Controls.Add(iconButton3);
            panel1.Controls.Add(iconButton2);
            panel1.Controls.Add(dtgAgentes);
            panel1.Controls.Add(iconButton1);
            panel1.Controls.Add(txtBuscar);
            panel1.Location = new Point(12, 31);
            panel1.Name = "panel1";
            panel1.Size = new Size(1071, 500);
            panel1.TabIndex = 0;
            // 
            // iconButton3
            // 
            iconButton3.BackColor = Color.FromArgb(1, 87, 155);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Century Gothic", 12F);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Check;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 30;
            iconButton3.Location = new Point(640, 440);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(179, 34);
            iconButton3.TabIndex = 4;
            iconButton3.Text = "Seleccionar";
            iconButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton2
            // 
            iconButton2.BackColor = Color.White;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 12F);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 30;
            iconButton2.Location = new Point(845, 440);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(179, 34);
            iconButton2.TabIndex = 3;
            iconButton2.Text = "Cancelar";
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // dtgAgentes
            // 
            dtgAgentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgAgentes.BackgroundColor = Color.White;
            dtgAgentes.BorderStyle = BorderStyle.None;
            dtgAgentes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgAgentes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
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
            dtgAgentes.Location = new Point(47, 105);
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
            dtgAgentes.Size = new Size(977, 308);
            dtgAgentes.TabIndex = 0;
            dtgAgentes.CellContentClick += dataGridView1_CellContentClick;
            // 
            // iconButton1
            // 
            iconButton1.BackColor = Color.FromArgb(255, 164, 0);
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 12F);
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconButton1.IconColor = Color.Black;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 30;
            iconButton1.Location = new Point(876, 41);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(148, 34);
            iconButton1.TabIndex = 1;
            iconButton1.Text = "Buscar";
            iconButton1.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Century Gothic", 12F);
            txtBuscar.Location = new Point(40, 44);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(830, 32);
            txtBuscar.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(19, 12, 70);
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
            button1.Font = new Font("Century Gothic", 12F);
            button1.ForeColor = Color.White;
            button1.Location = new Point(1035, 0);
            button1.Name = "button1";
            button1.Size = new Size(51, 29);
            button1.TabIndex = 0;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FrmMostrarAgentes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1086, 543);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmMostrarAgentes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmMostrarAgentes";
            Load += FrmMostrarAgentes_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtBuscar;
        private FontAwesome.Sharp.IconButton iconButton1;
        private DataGridView dtgAgentes;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private Panel panel2;
        private Button button1;
    }
}