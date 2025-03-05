namespace Presentacion.Marcas_Nacionales
{
    partial class FrmJustificacion
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
            iconButton1 = new FontAwesome.Sharp.IconButton();
            richTextBoxJustificacion = new RichTextBox();
            label1 = new Label();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            roundedButton2 = new Presentacion.Clases.RoundedButton();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            labelUsuarioAbandono = new Label();
            SuspendLayout();
            // 
            // iconButton1
            // 
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.CircleExclamation;
            iconButton1.IconColor = Color.Red;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 120;
            iconButton1.Location = new Point(299, 12);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(110, 107);
            iconButton1.TabIndex = 0;
            iconButton1.UseVisualStyleBackColor = true;
            // 
            // richTextBoxJustificacion
            // 
            richTextBoxJustificacion.BorderStyle = BorderStyle.None;
            richTextBoxJustificacion.Location = new Point(175, 237);
            richTextBoxJustificacion.Name = "richTextBoxJustificacion";
            richTextBoxJustificacion.Size = new Size(374, 153);
            richTextBoxJustificacion.TabIndex = 2;
            richTextBoxJustificacion.Text = "";
            richTextBoxJustificacion.TextChanged += richTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 10F);
            label1.Location = new Point(175, 213);
            label1.Name = "label1";
            label1.Size = new Size(197, 19);
            label1.TabIndex = 2;
            label1.Text = "Justificacion de abandono";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(183, 28, 28);
            roundedButton1.BackgroundColor = Color.FromArgb(183, 28, 28);
            roundedButton1.BorderColor = Color.FromArgb(183, 28, 28);
            roundedButton1.BorderRadius = 10;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(175, 415);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(164, 50);
            roundedButton1.TabIndex = 3;
            roundedButton1.Text = "ABANDONAR";
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(189, 189, 189);
            roundedButton2.BackgroundColor = Color.FromArgb(189, 189, 189);
            roundedButton2.BorderColor = Color.FromArgb(189, 189, 189);
            roundedButton2.BorderRadius = 10;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            roundedButton2.ForeColor = Color.Black;
            roundedButton2.Location = new Point(385, 415);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(164, 50);
            roundedButton2.TabIndex = 4;
            roundedButton2.Text = "CANCELAR";
            roundedButton2.TextColor = Color.Black;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(175, 172);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(164, 22);
            dateTimePicker1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 10F);
            label2.Location = new Point(175, 148);
            label2.Name = "label2";
            label2.Size = new Size(53, 19);
            label2.TabIndex = 6;
            label2.Text = "Fecha";
            // 
            // labelUsuarioAbandono
            // 
            labelUsuarioAbandono.AutoSize = true;
            labelUsuarioAbandono.Font = new Font("Century Gothic", 10F);
            labelUsuarioAbandono.Location = new Point(385, 177);
            labelUsuarioAbandono.Name = "labelUsuarioAbandono";
            labelUsuarioAbandono.Size = new Size(53, 19);
            labelUsuarioAbandono.TabIndex = 7;
            labelUsuarioAbandono.Text = "Fecha";
            // 
            // FrmJustificacion
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(725, 491);
            Controls.Add(labelUsuarioAbandono);
            Controls.Add(label2);
            Controls.Add(dateTimePicker1);
            Controls.Add(roundedButton2);
            Controls.Add(roundedButton1);
            Controls.Add(label1);
            Controls.Add(richTextBoxJustificacion);
            Controls.Add(iconButton1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FrmJustificacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ABANDONO";
            Load += FrmJustificacion_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FontAwesome.Sharp.IconButton iconButton1;
        private RichTextBox richTextBoxJustificacion;
        private Label label1;
        private Clases.RoundedButton roundedButton1;
        private Clases.RoundedButton roundedButton2;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Label labelUsuarioAbandono;
    }
}