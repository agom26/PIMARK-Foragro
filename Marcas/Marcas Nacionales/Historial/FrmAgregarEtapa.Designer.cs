namespace Presentacion.Marcas_Nacionales.Historial
{
    partial class FrmAgregarEtapa
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
            comboBox1 = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            richTextBox1 = new RichTextBox();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(208, 144);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 0;
            label1.Text = "Etapa";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Examen de forma", "Publicación", "Edicto", "Examen de fondo", "Prórroga", "Rechazo", "Registro/concesión", "Modificación", "Conversión de solicitud", "Corrección del certificado o inscripción" });
            comboBox1.Location = new Point(285, 141);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(296, 28);
            comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(208, 190);
            label2.Name = "label2";
            label2.Size = new Size(55, 20);
            label2.TabIndex = 2;
            label2.Text = "Fecha";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(161, 242);
            label3.Name = "label3";
            label3.Size = new Size(102, 20);
            label3.TabIndex = 3;
            label3.Text = "Anotaciones";
            //label3.Click += this.label3_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Century Gothic", 10F);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(285, 190);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(184, 28);
            dateTimePicker1.TabIndex = 4;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(285, 242);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(296, 89);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(34, 77, 112);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(824, 34);
            panel1.TabIndex = 6;
            // 
            // FrmAgregarEtapa
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(824, 450);
            Controls.Add(panel1);
            Controls.Add(richTextBox1);
            Controls.Add(dateTimePicker1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAgregarEtapa";
            Text = "FrmAgregarEtapa";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private RichTextBox richTextBox1;
        private Panel panel1;
    }
}