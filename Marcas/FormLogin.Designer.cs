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
            panel4 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            label5 = new Label();
            checkBox2 = new CheckBox();
            panel8 = new Panel();
            label4 = new Label();
            checkBox1 = new CheckBox();
            panel7 = new Panel();
            label3 = new Label();
            TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // TitleBar
            // 
            TitleBar.BackColor = Color.FromArgb(51, 109, 156);
            TitleBar.Controls.Add(iconPictureBox1);
            TitleBar.Controls.Add(iconButton1);
            TitleBar.Dock = DockStyle.Top;
            TitleBar.Location = new Point(0, 0);
            TitleBar.Margin = new Padding(3, 2, 3, 2);
            TitleBar.Name = "TitleBar";
            TitleBar.Size = new Size(414, 38);
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
            iconPictureBox1.IconSize = 38;
            iconPictureBox1.Location = new Point(328, 0);
            iconPictureBox1.Margin = new Padding(3, 2, 3, 2);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(42, 38);
            iconPictureBox1.TabIndex = 2;
            iconPictureBox1.TabStop = false;
            iconPictureBox1.Click += iconPictureBox1_Click_1;
            // 
            // iconButton1
            // 
            iconButton1.Dock = DockStyle.Right;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.ForeColor = Color.FromArgb(51, 109, 156);
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Close;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 40;
            iconButton1.Location = new Point(370, 0);
            iconButton1.Margin = new Padding(3, 2, 3, 2);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(44, 38);
            iconButton1.TabIndex = 0;
            iconButton1.UseVisualStyleBackColor = true;
            iconButton1.Click += iconButton1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(51, 109, 156);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 528);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(414, 11);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(93, 206);
            label1.Name = "label1";
            label1.Size = new Size(65, 19);
            label1.TabIndex = 2;
            label1.Text = "Usuario";
            // 
            // txtUserName
            // 
            txtUserName.Anchor = AnchorStyles.None;
            txtUserName.Font = new Font("Century Gothic", 10F);
            txtUserName.Location = new Point(93, 225);
            txtUserName.Margin = new Padding(3, 2, 3, 2);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(224, 24);
            txtUserName.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            txtPassword.Font = new Font("Century Gothic", 10F);
            txtPassword.Location = new Point(93, 287);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(224, 24);
            txtPassword.TabIndex = 5;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(93, 268);
            label2.Name = "label2";
            label2.Size = new Size(98, 19);
            label2.TabIndex = 4;
            label2.Text = "Contraseña";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(93, 42);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(224, 119);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // checkBoxRememberme
            // 
            checkBoxRememberme.Anchor = AnchorStyles.None;
            checkBoxRememberme.AutoSize = true;
            checkBoxRememberme.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkBoxRememberme.ForeColor = Color.White;
            checkBoxRememberme.Location = new Point(24, 4);
            checkBoxRememberme.Margin = new Padding(3, 2, 3, 2);
            checkBoxRememberme.Name = "checkBoxRememberme";
            checkBoxRememberme.Size = new Size(159, 23);
            checkBoxRememberme.TabIndex = 8;
            checkBoxRememberme.Text = "Recordar usuario";
            checkBoxRememberme.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.None;
            btnGuardar.BackColor = Color.FromArgb(23, 22, 40);
            btnGuardar.BackgroundColor = Color.FromArgb(23, 22, 40);
            btnGuardar.BorderColor = Color.FromArgb(23, 22, 40);
            btnGuardar.BorderRadius = 50;
            btnGuardar.BorderSize = 0;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Century Gothic", 12F);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(93, 367);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(224, 46);
            btnGuardar.TabIndex = 48;
            btnGuardar.Text = "INICIAR SESIÓN";
            btnGuardar.TextColor = Color.White;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(34, 77, 112);
            panel4.Controls.Add(panel3);
            panel4.Controls.Add(panel2);
            panel4.Controls.Add(label2);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(panel8);
            panel4.Controls.Add(pictureBox1);
            panel4.Controls.Add(btnGuardar);
            panel4.Controls.Add(txtUserName);
            panel4.Controls.Add(txtPassword);
            panel4.Controls.Add(panel7);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 0);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(414, 539);
            panel4.TabIndex = 51;
            // 
            // panel3
            // 
            panel3.Location = new Point(322, 178);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(92, 236);
            panel3.TabIndex = 54;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.FromArgb(51, 109, 156);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(checkBox2);
            panel2.Location = new Point(65, 490);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(282, 8);
            panel2.TabIndex = 53;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F);
            label5.ForeColor = Color.White;
            label5.Location = new Point(183, -85);
            label5.Name = "label5";
            label5.Size = new Size(103, 21);
            label5.TabIndex = 4;
            label5.Text = "Contraseña";
            // 
            // checkBox2
            // 
            checkBox2.Anchor = AnchorStyles.None;
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Century Gothic", 12F);
            checkBox2.ForeColor = Color.White;
            checkBox2.Location = new Point(150, -64);
            checkBox2.Margin = new Padding(3, 2, 3, 2);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(126, 25);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "Recordarme";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            panel8.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel8.BackColor = Color.FromArgb(51, 109, 156);
            panel8.Controls.Add(label4);
            panel8.Controls.Add(checkBox1);
            panel8.Location = new Point(65, 467);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(282, 8);
            panel8.TabIndex = 52;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F);
            label4.ForeColor = Color.White;
            label4.Location = new Point(130, -51);
            label4.Name = "label4";
            label4.Size = new Size(103, 21);
            label4.TabIndex = 4;
            label4.Text = "Contraseña";
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.None;
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Century Gothic", 12F);
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(97, -31);
            checkBox1.Margin = new Padding(3, 2, 3, 2);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(126, 25);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Recordarme";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            panel7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel7.Controls.Add(label3);
            panel7.Controls.Add(checkBoxRememberme);
            panel7.Location = new Point(93, 321);
            panel7.Margin = new Padding(3, 2, 3, 2);
            panel7.Name = "panel7";
            panel7.Size = new Size(224, 26);
            panel7.TabIndex = 51;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(76, -17);
            label3.Name = "label3";
            label3.Size = new Size(103, 21);
            label3.TabIndex = 4;
            label3.Text = "Contraseña";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 59, 104);
            ClientSize = new Size(414, 539);
            Controls.Add(panel1);
            Controls.Add(TitleBar);
            Controls.Add(panel4);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
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
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
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
        private Panel panel4;
        private Panel panel7;
        private Label label3;
        private Panel panel8;
        private Label label4;
        private CheckBox checkBox1;
        private Panel panel2;
        private Label label5;
        private CheckBox checkBox2;
        private Panel panel3;
    }
}
