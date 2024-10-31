namespace Marcas
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            TitleBar = new Panel();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            label1 = new Label();
            txtUserName = new TextBox();
            txtPassword = new TextBox();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            checkBoxRememberme = new CheckBox();
            btnGuardar = new Presentacion.Clases.RoundedButton();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // TitleBar
            // 
            TitleBar.BackColor = Color.FromArgb(51, 109, 156);
            TitleBar.Controls.Add(iconPictureBox1);
            TitleBar.Controls.Add(iconButton1);
            TitleBar.Dock = DockStyle.Top;
            TitleBar.Location = new Point(0, 0);
            TitleBar.Name = "TitleBar";
            TitleBar.Size = new Size(473, 50);
            TitleBar.TabIndex = 0;
            TitleBar.Paint += TitleBar_Paint;
            TitleBar.MouseMove += TitleBar_MouseMove;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.FromArgb(51, 109, 156);
            iconPictureBox1.Dock = DockStyle.Right;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.MinusCircle;
            iconPictureBox1.IconColor = Color.White;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 48;
            iconPictureBox1.Location = new Point(375, 0);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(48, 50);
            iconPictureBox1.TabIndex = 2;
            iconPictureBox1.TabStop = false;
            iconPictureBox1.Click += iconPictureBox1_Click_1;
            // 
            // iconButton1
            // 
            iconButton1.Dock = DockStyle.Right;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.ForeColor = Color.FromArgb(133, 182, 222);
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Close;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 40;
            iconButton1.Location = new Point(423, 0);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(50, 50);
            iconButton1.TabIndex = 0;
            iconButton1.UseVisualStyleBackColor = true;
            iconButton1.Click += iconButton1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(51, 109, 156);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 704);
            panel1.Name = "panel1";
            panel1.Size = new Size(473, 15);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(63, 223);
            label1.Name = "label1";
            label1.Size = new Size(79, 23);
            label1.TabIndex = 2;
            label1.Text = "Usuario";
            // 
            // txtUserName
            // 
            txtUserName.Anchor = AnchorStyles.None;
            txtUserName.Font = new Font("Century Gothic", 9F);
            txtUserName.Location = new Point(63, 267);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(350, 26);
            txtUserName.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            txtPassword.Font = new Font("Century Gothic", 9F);
            txtPassword.Location = new Point(60, 385);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(353, 26);
            txtPassword.TabIndex = 5;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(60, 344);
            label2.Name = "label2";
            label2.Size = new Size(125, 23);
            label2.TabIndex = 4;
            label2.Text = "Contraseña";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(125, 94);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(213, 92);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // checkBoxRememberme
            // 
            checkBoxRememberme.Anchor = AnchorStyles.None;
            checkBoxRememberme.AutoSize = true;
            checkBoxRememberme.Font = new Font("Century Gothic", 12F);
            checkBoxRememberme.ForeColor = Color.White;
            checkBoxRememberme.Location = new Point(63, 467);
            checkBoxRememberme.Name = "checkBoxRememberme";
            checkBoxRememberme.Size = new Size(155, 27);
            checkBoxRememberme.TabIndex = 8;
            checkBoxRememberme.Text = "Recordarme";
            checkBoxRememberme.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.None;
            btnGuardar.BackColor = Color.FromArgb(133, 182, 222);
            btnGuardar.BackgroundColor = Color.FromArgb(133, 182, 222);
            btnGuardar.BorderColor = Color.FromArgb(133, 182, 222);
            btnGuardar.BorderRadius = 60;
            btnGuardar.BorderSize = 0;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Century Gothic", 12F);
            btnGuardar.ForeColor = Color.Black;
            btnGuardar.Location = new Point(63, 533);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(350, 62);
            btnGuardar.TabIndex = 48;
            btnGuardar.Text = "Iniciar sesión";
            btnGuardar.TextColor = Color.Black;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(34, 77, 112);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 50);
            panel2.Name = "panel2";
            panel2.Size = new Size(57, 654);
            panel2.TabIndex = 49;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(34, 77, 112);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(416, 50);
            panel3.Name = "panel3";
            panel3.Size = new Size(57, 654);
            panel3.TabIndex = 50;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(34, 77, 112);
            panel4.Controls.Add(pictureBox1);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(btnGuardar);
            panel4.Controls.Add(txtUserName);
            panel4.Controls.Add(checkBoxRememberme);
            panel4.Controls.Add(label2);
            panel4.Controls.Add(txtPassword);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(473, 719);
            panel4.TabIndex = 51;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 59, 104);
            ClientSize = new Size(473, 719);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(TitleBar);
            Controls.Add(panel4);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LOGIN";
            Load += LoginForm_Load;
            MouseMove += LoginForm_MouseMove;
            TitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel TitleBar;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label label1;
        private TextBox txtUserName;
        private TextBox txtPassword;
        private Label label2;
        private PictureBox pictureBox1;
        private CheckBox checkBoxRememberme;
        private Presentacion.Clases.RoundedButton btnGuardar;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
    }
}
