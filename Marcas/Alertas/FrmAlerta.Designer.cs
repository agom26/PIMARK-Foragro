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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlerta));
            picIcon = new PictureBox();
            lblTitulo = new Label();
            btnYes = new FontAwesome.Sharp.IconButton();
            btnNo = new FontAwesome.Sharp.IconButton();
            richTextBoxMensaje = new RichTextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // picIcon
            // 
            picIcon.Image = Properties.Resources.logoBPA;
            picIcon.Location = new Point(3, 3);
            picIcon.Name = "picIcon";
            picIcon.Size = new Size(50, 50);
            picIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            picIcon.TabIndex = 0;
            picIcon.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(136, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(54, 18);
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
            btnYes.Location = new Point(106, 15);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(102, 37);
            btnYes.TabIndex = 2;
            btnYes.Text = "SI";
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
            btnNo.Location = new Point(214, 15);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(102, 37);
            btnNo.TabIndex = 3;
            btnNo.Text = "NO";
            btnNo.UseVisualStyleBackColor = false;
            btnNo.Click += btnNo_Click;
            // 
            // richTextBoxMensaje
            // 
            richTextBoxMensaje.BackColor = Color.White;
            richTextBoxMensaje.BorderStyle = BorderStyle.None;
            richTextBoxMensaje.Font = new Font("Century Gothic", 7F);
            richTextBoxMensaje.ForeColor = Color.Black;
            richTextBoxMensaje.Location = new Point(59, 3);
            richTextBoxMensaje.Name = "richTextBoxMensaje";
            richTextBoxMensaje.ReadOnly = true;
            richTextBoxMensaje.Size = new Size(233, 94);
            richTextBoxMensaje.TabIndex = 5;
            richTextBoxMensaje.Text = "";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonFace;
            panel1.Controls.Add(btnYes);
            panel1.Controls.Add(btnNo);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 151);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 64);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.Controls.Add(richTextBoxMensaje);
            panel2.Controls.Add(picIcon);
            panel2.Location = new Point(28, 45);
            panel2.Name = "panel2";
            panel2.Size = new Size(295, 100);
            panel2.TabIndex = 7;
            // 
            // FrmAlerta
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(350, 215);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(lblTitulo);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmAlerta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Alerta";
            FormClosing += FrmAlerta_FormClosing;
            Load += FrmAlerta_Load;
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picIcon;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnYes;
        private FontAwesome.Sharp.IconButton btnNo;
        private RichTextBox richTextBoxMensaje;
        private Panel panel1;
        private Panel panel2;
    }
}