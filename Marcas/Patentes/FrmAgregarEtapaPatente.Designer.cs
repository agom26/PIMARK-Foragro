namespace Presentacion.Patentes
{
    partial class FrmAgregarEtapaPatente
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
            dateTimePickerFechaIngreso = new DateTimePicker();
            comboBox1 = new ComboBox();
            richTextBoxAnotaciones = new RichTextBox();
            lblUser = new Label();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            panel2 = new Panel();
            button2 = new Button();
            button1 = new Button();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            dateTimePickerVencimiento = new DateTimePicker();
            labelVenc = new Label();
            panel1 = new Panel();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(93, 172);
            label1.Name = "label1";
            label1.Size = new Size(91, 17);
            label1.TabIndex = 0;
            label1.Text = "Fecha Ingreso";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(93, 92);
            label2.Name = "label2";
            label2.Size = new Size(48, 17);
            label2.TabIndex = 1;
            label2.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(93, 242);
            label3.Name = "label3";
            label3.Size = new Size(84, 17);
            label3.TabIndex = 2;
            label3.Text = "Anotaciones";
            // 
            // dateTimePickerFechaIngreso
            // 
            dateTimePickerFechaIngreso.Format = DateTimePickerFormat.Short;
            dateTimePickerFechaIngreso.Location = new Point(93, 192);
            dateTimePickerFechaIngreso.Name = "dateTimePickerFechaIngreso";
            dateTimePickerFechaIngreso.Size = new Size(154, 22);
            dateTimePickerFechaIngreso.TabIndex = 3;
            dateTimePickerFechaIngreso.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.White;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Ingresada", "Examen de forma", "Examen de publicación", "Edicto", "Examen de fondo", "Prórroga", "Rechazo", "Registro/concesión", "Modificación", "Conversión de solicitud", "Corrección del certificado o inscripción" });
            comboBox1.Location = new Point(93, 112);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(398, 25);
            comboBox1.TabIndex = 4;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // richTextBoxAnotaciones
            // 
            richTextBoxAnotaciones.BorderStyle = BorderStyle.None;
            richTextBoxAnotaciones.Location = new Point(93, 262);
            richTextBoxAnotaciones.Name = "richTextBoxAnotaciones";
            richTextBoxAnotaciones.Size = new Size(398, 102);
            richTextBoxAnotaciones.TabIndex = 5;
            richTextBoxAnotaciones.Text = "";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(87, 47);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(45, 17);
            lblUser.TabIndex = 6;
            lblUser.Text = "Fecha";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(196, 195, 209);
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 13F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(138, 28);
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
            panel2.Size = new Size(565, 34);
            panel2.TabIndex = 8;
            panel2.MouseDown += panel2_MouseDown;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 12F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(514, 0);
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
            iconButton3.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Check;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 25;
            iconButton3.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton3.Location = new Point(93, 386);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(160, 38);
            iconButton3.TabIndex = 10;
            iconButton3.Text = "SELECCIONAR";
            iconButton3.TextAlign = ContentAlignment.MiddleRight;
            iconButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton2
            // 
            iconButton2.BackColor = Color.Gainsboro;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton2.Location = new Point(331, 386);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(160, 38);
            iconButton2.TabIndex = 9;
            iconButton2.Text = "CANCELAR";
            iconButton2.TextAlign = ContentAlignment.MiddleRight;
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // dateTimePickerVencimiento
            // 
            dateTimePickerVencimiento.Anchor = AnchorStyles.Top;
            dateTimePickerVencimiento.Format = DateTimePickerFormat.Short;
            dateTimePickerVencimiento.Location = new Point(329, 192);
            dateTimePickerVencimiento.Name = "dateTimePickerVencimiento";
            dateTimePickerVencimiento.Size = new Size(154, 22);
            dateTimePickerVencimiento.TabIndex = 11;
            // 
            // labelVenc
            // 
            labelVenc.Anchor = AnchorStyles.Top;
            labelVenc.AutoSize = true;
            labelVenc.Location = new Point(329, 169);
            labelVenc.Name = "labelVenc";
            labelVenc.Size = new Size(144, 17);
            labelVenc.TabIndex = 12;
            labelVenc.Text = "Fecha de Vencimiento";
            // 
            // panel1
            // 
            panel1.Controls.Add(iconButton2);
            panel1.Controls.Add(iconButton3);
            panel1.Controls.Add(dateTimePickerVencimiento);
            panel1.Controls.Add(labelVenc);
            panel1.Controls.Add(richTextBoxAnotaciones);
            panel1.Controls.Add(roundedButton1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(lblUser);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dateTimePickerFechaIngreso);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(565, 436);
            panel1.TabIndex = 13;
            // 
            // FrmAgregarEtapaPatente
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 493);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FrmAgregarEtapaPatente";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAgregarEtapaPatente";
            Load += FrmAgregarEtapaPatente_Load;
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private DateTimePicker dateTimePickerFechaIngreso;
        private ComboBox comboBox1;
        private RichTextBox richTextBoxAnotaciones;
        private Label lblUser;
        private Clases.RoundedButton roundedButton1;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton iconButton2;
        private DateTimePicker dateTimePickerVencimiento;
        private Label labelVenc;
        private Panel panel1;
    }
}