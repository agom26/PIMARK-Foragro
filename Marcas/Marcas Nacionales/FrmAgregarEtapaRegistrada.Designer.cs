namespace Presentacion.Marcas_Nacionales
{
    partial class FrmAgregarEtapaRegistrada
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            comboBox1 = new ComboBox();
            richTextBox1 = new RichTextBox();
            lblUser = new Label();
            roundedButton1 = new Clases.RoundedButton();
            panel2 = new Panel();
            button2 = new Button();
            button1 = new Button();
            btnSeleccionar = new FontAwesome.Sharp.IconButton();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            txtNoExpedienteRT = new TextBox();
            lblNoExpediente = new Label();
            groupBox1 = new GroupBox();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel4 = new Panel();
            panel3 = new Panel();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(188, 130);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 0;
            label1.Text = "Fecha";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Location = new Point(369, 132);
            label2.Name = "label2";
            label2.Size = new Size(58, 20);
            label2.TabIndex = 1;
            label2.Text = "Estado";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Location = new Point(188, 209);
            label3.Name = "label3";
            label3.Size = new Size(102, 20);
            label3.TabIndex = 2;
            label3.Text = "Anotaciones";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = AnchorStyles.Top;
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(188, 153);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(154, 26);
            dateTimePicker1.TabIndex = 3;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top;
            comboBox1.BackColor = Color.White;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Ingresada", "Examen de forma", "Examen de fondo", "Requerimiento", "Objeción", "Edicto", "Publicación", "Oposición", "Orden de pago", "Registrada", "Licencia de uso", "Trámite de renovación", "Trámite de traspaso" });
            comboBox1.Location = new Point(369, 155);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(250, 28);
            comboBox1.TabIndex = 4;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(188, 232);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(431, 102);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            // 
            // lblUser
            // 
            lblUser.Anchor = AnchorStyles.Top;
            lblUser.AutoSize = true;
            lblUser.Location = new Point(369, 88);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(55, 20);
            lblUser.TabIndex = 6;
            lblUser.Text = "Fecha";
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top;
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 13F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(261, 9);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(270, 50);
            roundedButton1.TabIndex = 7;
            roundedButton1.Text = "AGREGAR ESTADO";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(34, 77, 112);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(779, 34);
            panel2.TabIndex = 8;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 12F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(728, 0);
            button2.Name = "button2";
            button2.Size = new Size(51, 34);
            button2.TabIndex = 1;
            button2.Text = "X";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
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
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.BackColor = Color.FromArgb(1, 87, 155);
            btnSeleccionar.FlatAppearance.BorderSize = 0;
            btnSeleccionar.FlatStyle = FlatStyle.Flat;
            btnSeleccionar.Font = new Font("Century Gothic", 10F);
            btnSeleccionar.ForeColor = Color.White;
            btnSeleccionar.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnSeleccionar.IconColor = Color.White;
            btnSeleccionar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSeleccionar.IconSize = 30;
            btnSeleccionar.Location = new Point(29, 20);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Size = new Size(179, 34);
            btnSeleccionar.TabIndex = 10;
            btnSeleccionar.Text = "SELECCIONAR";
            btnSeleccionar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnSeleccionar.UseVisualStyleBackColor = false;
            btnSeleccionar.Click += iconButton3_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Century Gothic", 10F);
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 30;
            btnCancelar.Location = new Point(234, 20);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(179, 34);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += iconButton2_Click;
            // 
            // txtNoExpedienteRT
            // 
            txtNoExpedienteRT.Location = new Point(138, 43);
            txtNoExpedienteRT.Name = "txtNoExpedienteRT";
            txtNoExpedienteRT.Size = new Size(154, 26);
            txtNoExpedienteRT.TabIndex = 11;
            // 
            // lblNoExpediente
            // 
            lblNoExpediente.AutoSize = true;
            lblNoExpediente.Location = new Point(138, 20);
            lblNoExpediente.Name = "lblNoExpediente";
            lblNoExpediente.Size = new Size(71, 20);
            lblNoExpediente.TabIndex = 12;
            lblNoExpediente.Text = "Traspaso";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtNoExpedienteRT);
            groupBox1.Controls.Add(lblNoExpediente);
            groupBox1.Location = new Point(11, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(431, 89);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "No. Expediente";
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(roundedButton1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblUser);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(779, 542);
            panel1.TabIndex = 14;
            panel1.Paint += panel1_Paint;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel4, 0, 1);
            tableLayoutPanel1.Controls.Add(panel3, 0, 0);
            tableLayoutPanel1.Location = new Point(169, 350);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tableLayoutPanel1.Size = new Size(466, 180);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnSeleccionar);
            panel4.Controls.Add(btnCancelar);
            panel4.Location = new Point(3, 117);
            panel4.Name = "panel4";
            panel4.Size = new Size(451, 60);
            panel4.TabIndex = 16;
            // 
            // panel3
            // 
            panel3.Controls.Add(groupBox1);
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(451, 104);
            panel3.TabIndex = 15;
            // 
            // FrmAgregarEtapaRegistrada
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(779, 576);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAgregarEtapaRegistrada";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAgregarEtapaRegistrada";
            Load += FrmAgregarEtapaRegistrada_Load;
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private ComboBox comboBox1;
        private RichTextBox richTextBox1;
        private Label lblUser;
        private Clases.RoundedButton roundedButton1;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private FontAwesome.Sharp.IconButton btnSeleccionar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private TextBox txtNoExpedienteRT;
        private Label lblNoExpediente;
        private GroupBox groupBox1;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel3;
        private Panel panel4;
    }
}