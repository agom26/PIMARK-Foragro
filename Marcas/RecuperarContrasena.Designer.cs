namespace Presentacion
{
    partial class RecuperarContrasena
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
            txtBoxUser = new TextBox();
            iconButtonEnviarCorreo = new FontAwesome.Sharp.IconButton();
            labelResultado = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 20F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(66, 62);
            label1.Name = "label1";
            label1.Size = new Size(650, 40);
            label1.TabIndex = 0;
            label1.Text = "Ingrese su usuario o correo electrónico";
            // 
            // txtBoxUser
            // 
            txtBoxUser.Font = new Font("Century Gothic", 12F);
            txtBoxUser.ForeColor = Color.FromArgb(49, 59, 104);
            txtBoxUser.Location = new Point(66, 138);
            txtBoxUser.Name = "txtBoxUser";
            txtBoxUser.Size = new Size(638, 32);
            txtBoxUser.TabIndex = 1;
            // 
            // iconButtonEnviarCorreo
            // 
            iconButtonEnviarCorreo.BackColor = Color.FromArgb(9, 98, 246);
            iconButtonEnviarCorreo.FlatAppearance.BorderSize = 0;
            iconButtonEnviarCorreo.FlatStyle = FlatStyle.Flat;
            iconButtonEnviarCorreo.Font = new Font("Century Gothic", 12F);
            iconButtonEnviarCorreo.ForeColor = Color.White;
            iconButtonEnviarCorreo.IconChar = FontAwesome.Sharp.IconChar.None;
            iconButtonEnviarCorreo.IconColor = Color.Black;
            iconButtonEnviarCorreo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonEnviarCorreo.Location = new Point(522, 210);
            iconButtonEnviarCorreo.Name = "iconButtonEnviarCorreo";
            iconButtonEnviarCorreo.Size = new Size(182, 50);
            iconButtonEnviarCorreo.TabIndex = 2;
            iconButtonEnviarCorreo.Text = "Enviar";
            iconButtonEnviarCorreo.UseVisualStyleBackColor = false;
            iconButtonEnviarCorreo.Click += iconButtonEnviarCorreo_Click;
            // 
            // labelResultado
            // 
            labelResultado.AutoSize = true;
            labelResultado.Font = new Font("Century Gothic", 12F);
            labelResultado.ForeColor = Color.White;
            labelResultado.Location = new Point(66, 304);
            labelResultado.Name = "labelResultado";
            labelResultado.Size = new Size(108, 23);
            labelResultado.TabIndex = 3;
            labelResultado.Text = "Resultado";
            // 
            // RecuperarContrasena
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 59, 104);
            ClientSize = new Size(932, 450);
            Controls.Add(labelResultado);
            Controls.Add(iconButtonEnviarCorreo);
            Controls.Add(txtBoxUser);
            Controls.Add(label1);
            Name = "RecuperarContrasena";
            Text = "RecuperarContrasena";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtBoxUser;
        private FontAwesome.Sharp.IconButton iconButtonEnviarCorreo;
        private Label labelResultado;
    }
}