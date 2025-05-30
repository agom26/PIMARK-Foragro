namespace Presentacion.Marcas_Nacionales
{
    partial class FrmEnviarAOposicion
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
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            richTextBox1 = new RichTextBox();
            lblUser = new Label();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            panel2 = new Panel();
            button2 = new Button();
            button1 = new Button();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            label4 = new Label();
            Fechas = new GroupBox();
            label6 = new Label();
            txtSolicitante = new TextBox();
            txtNombreOpositor = new TextBox();
            groupBox1 = new GroupBox();
            panelBusqueda = new Panel();
            panel3 = new Panel();
            panel2.SuspendLayout();
            Fechas.SuspendLayout();
            groupBox1.SuspendLayout();
            panelBusqueda.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 70);
            label1.Name = "label1";
            label1.Size = new Size(125, 17);
            label1.TabIndex = 0;
            label1.Text = "Fecha de oposición";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 149);
            label3.Name = "label3";
            label3.Size = new Size(84, 17);
            label3.TabIndex = 2;
            label3.Text = "Anotaciones";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(15, 93);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(154, 22);
            dateTimePicker1.TabIndex = 1;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(15, 172);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(347, 70);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(251, 70);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(45, 17);
            lblUser.TabIndex = 6;
            lblUser.Text = "Fecha";
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top;
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 13F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(282, 9);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(290, 50);
            roundedButton1.TabIndex = 7;
            roundedButton1.Text = "ENVIAR A OPOSICIÓN";
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
            panel2.Size = new Size(881, 34);
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
            button2.Location = new Point(830, 0);
            button2.Name = "button2";
            button2.Size = new Size(51, 34);
            button2.TabIndex = 7;
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
            iconButton3.Anchor = AnchorStyles.Bottom;
            iconButton3.BackColor = Color.FromArgb(161, 136, 127);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Gavel;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 25;
            iconButton3.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton3.Location = new Point(263, 349);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(160, 49);
            iconButton3.TabIndex = 5;
            iconButton3.Text = "OPOSICIÓN";
            iconButton3.TextAlign = ContentAlignment.MiddleRight;
            iconButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Bottom;
            iconButton2.BackColor = Color.White;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton2.Location = new Point(459, 349);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(160, 49);
            iconButton2.TabIndex = 6;
            iconButton2.Text = "CANCELAR";
            iconButton2.TextAlign = ContentAlignment.MiddleRight;
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 88);
            label4.Name = "label4";
            label4.Size = new Size(59, 17);
            label4.TabIndex = 12;
            label4.Text = "Opositor";
            // 
            // Fechas
            // 
            Fechas.Controls.Add(label6);
            Fechas.Controls.Add(txtSolicitante);
            Fechas.Controls.Add(txtNombreOpositor);
            Fechas.Controls.Add(label4);
            Fechas.Location = new Point(450, 88);
            Fechas.Name = "Fechas";
            Fechas.Size = new Size(405, 255);
            Fechas.TabIndex = 20;
            Fechas.TabStop = false;
            Fechas.Text = "Oposición";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(33, 172);
            label6.Name = "label6";
            label6.Size = new Size(93, 17);
            label6.TabIndex = 24;
            label6.Text = "Signo opositor";
            // 
            // txtSolicitante
            // 
            txtSolicitante.Location = new Point(33, 195);
            txtSolicitante.Name = "txtSolicitante";
            txtSolicitante.Size = new Size(323, 22);
            txtSolicitante.TabIndex = 4;
            // 
            // txtNombreOpositor
            // 
            txtNombreOpositor.Location = new Point(33, 111);
            txtNombreOpositor.Name = "txtNombreOpositor";
            txtNombreOpositor.Size = new Size(323, 22);
            txtNombreOpositor.TabIndex = 3;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Controls.Add(lblUser);
            groupBox1.Location = new Point(30, 91);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 255);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Historial";
            // 
            // panelBusqueda
            // 
            panelBusqueda.AutoScroll = true;
            panelBusqueda.Controls.Add(groupBox1);
            panelBusqueda.Controls.Add(panel3);
            panelBusqueda.Dock = DockStyle.Top;
            panelBusqueda.Location = new Point(0, 34);
            panelBusqueda.Name = "panelBusqueda";
            panelBusqueda.Size = new Size(881, 441);
            panelBusqueda.TabIndex = 22;
            panelBusqueda.Paint += panelBusqueda_Paint;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.Controls.Add(iconButton2);
            panel3.Controls.Add(iconButton3);
            panel3.Controls.Add(Fechas);
            panel3.Controls.Add(roundedButton1);
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(883, 418);
            panel3.TabIndex = 22;
            // 
            // FrmEnviarAOposicion
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(881, 483);
            Controls.Add(panelBusqueda);
            Controls.Add(panel2);
            Font = new Font("Century Gothic", 9F);
            Name = "FrmEnviarAOposicion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmEnviarAOposicion";
            Load += FrmEnviarAOposicion_Load;
            panel2.ResumeLayout(false);
            Fechas.ResumeLayout(false);
            Fechas.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panelBusqueda.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private RichTextBox richTextBox1;
        private Label lblUser;
        private Clases.RoundedButton roundedButton1;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton iconButton2;
        private Label label4;
        private GroupBox Fechas;
        private GroupBox groupBox1;
        private TextBox txtNombreOpositor;
        private TextBox txtSolicitante;
        private Label label6;
        private Panel panelBusqueda;
        private Panel panel3;
    }
}