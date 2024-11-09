namespace Presentacion.Marcas_Nacionales
{
    partial class FrmCrearTraspaso
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
            richTextBox1 = new RichTextBox();
            lblUser = new Label();
            roundedButton1 = new Clases.RoundedButton();
            panel2 = new Panel();
            button2 = new Button();
            button1 = new Button();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            txtEstado = new TextBox();
            label4 = new Label();
            label6 = new Label();
            label7 = new Label();
            Fechas = new GroupBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            roundedButton2 = new Clases.RoundedButton();
            textBox1 = new TextBox();
            txtNoExpediente = new TextBox();
            label8 = new Label();
            groupBox1 = new GroupBox();
            panel2.SuspendLayout();
            Fechas.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 70);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 0;
            label1.Text = "Fecha";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(265, 70);
            label2.Name = "label2";
            label2.Size = new Size(58, 20);
            label2.TabIndex = 1;
            label2.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 149);
            label3.Name = "label3";
            label3.Size = new Size(102, 20);
            label3.TabIndex = 2;
            label3.Text = "Anotaciones";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(31, 93);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(154, 26);
            dateTimePicker1.TabIndex = 3;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(31, 172);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(431, 70);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(212, 28);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(55, 20);
            lblUser.TabIndex = 6;
            lblUser.Text = "Fecha";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(196, 195, 209);
            roundedButton1.BackgroundColor = Color.FromArgb(196, 195, 209);
            roundedButton1.BorderColor = Color.FromArgb(196, 195, 209);
            roundedButton1.BorderRadius = 50;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 13F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(378, 77);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(290, 50);
            roundedButton1.TabIndex = 7;
            roundedButton1.Text = "Traspaso concedido";
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
            panel2.Size = new Size(1090, 34);
            panel2.TabIndex = 8;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 12F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(1039, 0);
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
            // iconButton3
            // 
            iconButton3.BackColor = Color.FromArgb(1, 87, 155);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Century Gothic", 10F);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Check;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 30;
            iconButton3.Location = new Point(319, 610);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(179, 34);
            iconButton3.TabIndex = 10;
            iconButton3.Text = "Aceptar";
            iconButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton2
            // 
            iconButton2.BackColor = Color.White;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 10F);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 30;
            iconButton2.Location = new Point(522, 610);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(179, 34);
            iconButton2.TabIndex = 9;
            iconButton2.Text = "Cancelar";
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // txtEstado
            // 
            txtEstado.Location = new Point(265, 95);
            txtEstado.Name = "txtEstado";
            txtEstado.ReadOnly = true;
            txtEstado.Size = new Size(197, 26);
            txtEstado.TabIndex = 11;
            txtEstado.Text = "Registrada";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(77, 85);
            label4.Name = "label4";
            label4.Size = new Size(110, 20);
            label4.TabIndex = 12;
            label4.Text = "Antiguo titular";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(77, 252);
            label6.Name = "label6";
            label6.Size = new Size(128, 20);
            label6.TabIndex = 16;
            label6.Text = "Nombre antiguo";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(77, 329);
            label7.Name = "label7";
            label7.Size = new Size(120, 20);
            label7.TabIndex = 18;
            label7.Text = "Nuevo nombre";
            // 
            // Fechas
            // 
            Fechas.Controls.Add(textBox4);
            Fechas.Controls.Add(textBox3);
            Fechas.Controls.Add(textBox2);
            Fechas.Controls.Add(roundedButton2);
            Fechas.Controls.Add(textBox1);
            Fechas.Controls.Add(txtNoExpediente);
            Fechas.Controls.Add(label8);
            Fechas.Controls.Add(label4);
            Fechas.Controls.Add(label7);
            Fechas.Controls.Add(label6);
            Fechas.Location = new Point(522, 154);
            Fechas.Name = "Fechas";
            Fechas.Size = new Size(503, 432);
            Fechas.TabIndex = 20;
            Fechas.TabStop = false;
            Fechas.Text = "Traspaso";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(77, 352);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(323, 26);
            textBox4.TabIndex = 23;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(77, 275);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(323, 26);
            textBox3.TabIndex = 22;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(77, 194);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(323, 26);
            textBox2.TabIndex = 21;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.LightSteelBlue;
            roundedButton2.BackgroundColor = Color.LightSteelBlue;
            roundedButton2.BorderColor = Color.Empty;
            roundedButton2.BorderRadius = 30;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.Black;
            roundedButton2.Location = new Point(77, 158);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(156, 30);
            roundedButton2.TabIndex = 20;
            roundedButton2.Text = "Nuevo titular";
            roundedButton2.TextColor = Color.Black;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(77, 108);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(323, 26);
            textBox1.TabIndex = 19;
            // 
            // txtNoExpediente
            // 
            txtNoExpediente.BorderStyle = BorderStyle.None;
            txtNoExpediente.Location = new Point(173, 46);
            txtNoExpediente.Name = "txtNoExpediente";
            txtNoExpediente.ReadOnly = true;
            txtNoExpediente.Size = new Size(120, 19);
            txtNoExpediente.TabIndex = 12;
            txtNoExpediente.TextAlign = HorizontalAlignment.Center;
            txtNoExpediente.TextChanged += textBox2_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(173, 23);
            label8.Name = "label8";
            label8.Size = new Size(120, 20);
            label8.TabIndex = 12;
            label8.Text = "No. Expediente";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(txtEstado);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Controls.Add(lblUser);
            groupBox1.Location = new Point(26, 234);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(472, 272);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Historial";
            // 
            // FrmCrearTraspaso
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1090, 679);
            Controls.Add(Fechas);
            Controls.Add(iconButton3);
            Controls.Add(iconButton2);
            Controls.Add(panel2);
            Controls.Add(roundedButton1);
            Controls.Add(groupBox1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmCrearTraspaso";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAgregarEtapa";
            Load += FrmCrearTraspaso_Load;
            panel2.ResumeLayout(false);
            Fechas.ResumeLayout(false);
            Fechas.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private RichTextBox richTextBox1;
        private Label lblUser;
        private Clases.RoundedButton roundedButton1;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton iconButton2;
        private TextBox txtEstado;
        private Label label4;
        private Label label6;
        private Label label7;
        private GroupBox Fechas;
        private GroupBox groupBox1;
        private TextBox txtNoExpediente;
        private Label label8;
        private Clases.RoundedButton roundedButton2;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox4;
        private TextBox textBox3;
    }
}