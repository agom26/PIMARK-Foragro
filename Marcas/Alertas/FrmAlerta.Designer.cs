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
            richTextBoxMensaje = new RichTextBox();
            panel1 = new Panel();
            btnNo2 = new Button();
            btnYes2 = new Button();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // picIcon
            // 
            picIcon.Image = Properties.Resources.success;
            picIcon.Location = new Point(3, 16);
            picIcon.Name = "picIcon";
            picIcon.Size = new Size(61, 50);
            picIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picIcon.TabIndex = 0;
            picIcon.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 9F);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(136, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(38, 15);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "label1";
            lblTitulo.Visible = false;
            // 
            // richTextBoxMensaje
            // 
            richTextBoxMensaje.BackColor = Color.White;
            richTextBoxMensaje.BorderStyle = BorderStyle.None;
            richTextBoxMensaje.Font = new Font("Segoe UI", 9F);
            richTextBoxMensaje.ForeColor = Color.Black;
            richTextBoxMensaje.Location = new Point(70, 16);
            richTextBoxMensaje.Name = "richTextBoxMensaje";
            richTextBoxMensaje.ReadOnly = true;
            richTextBoxMensaje.Size = new Size(222, 61);
            richTextBoxMensaje.TabIndex = 5;
            richTextBoxMensaje.Text = "";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonFace;
            panel1.Controls.Add(btnNo2);
            panel1.Controls.Add(btnYes2);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 151);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 64);
            panel1.TabIndex = 6;
            // 
            // btnNo2
            // 
            btnNo2.Location = new Point(214, 22);
            btnNo2.Name = "btnNo2";
            btnNo2.Size = new Size(102, 23);
            btnNo2.TabIndex = 9;
            btnNo2.Text = "No";
            btnNo2.UseVisualStyleBackColor = true;
            btnNo2.Click += btnNo2_Click;
            // 
            // btnYes2
            // 
            btnYes2.Location = new Point(106, 22);
            btnYes2.Name = "btnYes2";
            btnYes2.Size = new Size(102, 23);
            btnYes2.TabIndex = 8;
            btnYes2.Text = "Sí";
            btnYes2.UseVisualStyleBackColor = true;
            btnYes2.Click += btnYes2_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(picIcon);
            panel2.Controls.Add(richTextBoxMensaje);
            panel2.Location = new Point(28, 45);
            panel2.Name = "panel2";
            panel2.Size = new Size(295, 100);
            panel2.TabIndex = 7;
            // 
            // FrmAlerta
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
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
        private RichTextBox richTextBoxMensaje;
        private Panel panel1;
        private Panel panel2;
        private Button btnNo2;
        private Button btnYes2;
    }
}