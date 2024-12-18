namespace Presentacion.Alertas
{
    partial class FrmAlerta
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
            picIcon = new PictureBox();
            lblTitulo = new Label();
            btnYes = new FontAwesome.Sharp.IconButton();
            btnNo = new FontAwesome.Sharp.IconButton();
            lblMessage = new Label();
            richTextBoxMensaje = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            SuspendLayout();
            // 
            // picIcon
            // 
            picIcon.Image = Properties.Resources.logoBPA;
            picIcon.Location = new Point(259, 31);
            picIcon.Name = "picIcon";
            picIcon.Size = new Size(118, 108);
            picIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            picIcon.TabIndex = 0;
            picIcon.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Century Gothic", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(259, 160);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(118, 40);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "label1";
            // 
            // btnYes
            // 
            btnYes.BackColor = Color.FromArgb(51, 109, 156);
            btnYes.FlatAppearance.BorderSize = 0;
            btnYes.FlatStyle = FlatStyle.Flat;
            btnYes.ForeColor = Color.White;
            btnYes.IconChar = FontAwesome.Sharp.IconChar.None;
            btnYes.IconColor = Color.Black;
            btnYes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnYes.Location = new Point(152, 335);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(147, 54);
            btnYes.TabIndex = 2;
            btnYes.Text = "iconButton1";
            btnYes.UseVisualStyleBackColor = false;
            btnYes.Click += btnYes_Click;
            // 
            // btnNo
            // 
            btnNo.BackColor = Color.FromArgb(51, 109, 156);
            btnNo.FlatAppearance.BorderSize = 0;
            btnNo.FlatStyle = FlatStyle.Flat;
            btnNo.ForeColor = Color.White;
            btnNo.IconChar = FontAwesome.Sharp.IconChar.None;
            btnNo.IconColor = Color.Black;
            btnNo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNo.Location = new Point(336, 335);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(147, 54);
            btnNo.TabIndex = 3;
            btnNo.Text = "iconButton2";
            btnNo.UseVisualStyleBackColor = false;
            btnNo.Click += btnNo_Click;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Century Gothic", 12F);
            lblMessage.Location = new Point(197, 269);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(72, 23);
            lblMessage.TabIndex = 4;
            lblMessage.Text = "label1";
            // 
            // richTextBoxMensaje
            // 
            richTextBoxMensaje.BackColor = Color.White;
            richTextBoxMensaje.BorderStyle = BorderStyle.None;
            richTextBoxMensaje.ForeColor = Color.Black;
            richTextBoxMensaje.Location = new Point(103, 220);
            richTextBoxMensaje.Name = "richTextBoxMensaje";
            richTextBoxMensaje.ReadOnly = true;
            richTextBoxMensaje.Size = new Size(435, 95);
            richTextBoxMensaje.TabIndex = 5;
            richTextBoxMensaje.Text = "";
            // 
            // FrmAlerta
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(666, 423);
            Controls.Add(richTextBoxMensaje);
            Controls.Add(lblMessage);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Controls.Add(lblTitulo);
            Controls.Add(picIcon);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAlerta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAlerta";
            FormClosing += FrmAlerta_FormClosing;
            Load += FrmAlerta_Load;
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picIcon;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnYes;
        private FontAwesome.Sharp.IconButton btnNo;
        private Label lblMessage;
        private RichTextBox richTextBoxMensaje;
    }
}