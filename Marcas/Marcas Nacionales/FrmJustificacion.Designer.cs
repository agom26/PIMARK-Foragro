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
            roundedButton1 = new Clases.RoundedButton();
            roundedButton2 = new Clases.RoundedButton();
            SuspendLayout();
            // 
            // iconButton1
            // 
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.CircleExclamation;
            iconButton1.IconColor = Color.Red;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 120;
            iconButton1.Location = new Point(300, 32);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(103, 108);
            iconButton1.TabIndex = 0;
            iconButton1.UseVisualStyleBackColor = true;
            // 
            // richTextBoxJustificacion
            // 
            richTextBoxJustificacion.BorderStyle = BorderStyle.None;
            richTextBoxJustificacion.Location = new Point(175, 179);
            richTextBoxJustificacion.Name = "richTextBoxJustificacion";
            richTextBoxJustificacion.Size = new Size(374, 211);
            richTextBoxJustificacion.TabIndex = 1;
            richTextBoxJustificacion.Text = "";
            richTextBoxJustificacion.TextChanged += richTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 10F);
            label1.Location = new Point(245, 155);
            label1.Name = "label1";
            label1.Size = new Size(234, 21);
            label1.TabIndex = 2;
            label1.Text = "Justificacion de abandono";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(183, 28, 28);
            roundedButton1.BackgroundColor = Color.FromArgb(183, 28, 28);
            roundedButton1.BorderColor = Color.FromArgb(183, 28, 28);
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(175, 415);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(164, 50);
            roundedButton1.TabIndex = 3;
            roundedButton1.Text = "Abandonar";
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(189, 189, 189);
            roundedButton2.BackgroundColor = Color.FromArgb(189, 189, 189);
            roundedButton2.BorderColor = Color.FromArgb(189, 189, 189);
            roundedButton2.BorderRadius = 40;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.Black;
            roundedButton2.Location = new Point(385, 415);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(164, 50);
            roundedButton2.TabIndex = 4;
            roundedButton2.Text = "Cancelar";
            roundedButton2.TextColor = Color.Black;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // FrmJustificacion
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(725, 491);
            Controls.Add(roundedButton2);
            Controls.Add(roundedButton1);
            Controls.Add(label1);
            Controls.Add(richTextBoxJustificacion);
            Controls.Add(iconButton1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmJustificacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmJustificacion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FontAwesome.Sharp.IconButton iconButton1;
        private RichTextBox richTextBoxJustificacion;
        private Label label1;
        private Clases.RoundedButton roundedButton1;
        private Clases.RoundedButton roundedButton2;
    }
}