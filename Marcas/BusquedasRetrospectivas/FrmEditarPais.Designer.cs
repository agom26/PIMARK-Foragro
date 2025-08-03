namespace Presentacion.BusquedasRetrospectivas
{
    partial class FrmEditarPais
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
            label2 = new Label();
            label3 = new Label();
            comboBoxPaises = new ComboBox();
            richTextBoxObservaciones = new RichTextBox();
            roundedButton1 = new Presentacion.Clases.RoundedButton();
            panel2 = new Panel();
            button2 = new Button();
            button1 = new Button();
            btnAgregarPais = new FontAwesome.Sharp.IconButton();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            comboBoxNivelRiesgo = new ComboBox();
            label1 = new Label();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Location = new Point(103, 91);
            label2.Name = "label2";
            label2.Size = new Size(31, 17);
            label2.TabIndex = 1;
            label2.Text = "País";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Location = new Point(103, 242);
            label3.Name = "label3";
            label3.Size = new Size(98, 17);
            label3.TabIndex = 2;
            label3.Text = "Observaciones";
            // 
            // comboBoxPaises
            // 
            comboBoxPaises.Anchor = AnchorStyles.Top;
            comboBoxPaises.BackColor = Color.White;
            comboBoxPaises.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPaises.FlatStyle = FlatStyle.Flat;
            comboBoxPaises.FormattingEnabled = true;
            comboBoxPaises.Items.AddRange(new object[] { "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita", "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Baréin", "Bangladés", "Barbados", "Bielorrusia", "Birmania (Myanmar)", "Burundi", "Bután", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo (Congo-Brazzaville)", "Corea del Norte", "Corea del Sur", "Costa Rica", "Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Eswatini", "Etiopía", "Fiyi", "Filipinas", "Finlandia", "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Grecia", "Granada", "Guatemala", "Guinea", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Holanda", "Hungría", "Islandia", "India", "Indonesia", "Irán", "Irak", "Irlanda", "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kosovo", "Kuwait", "Laos", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein", "Lituania", "Luxemburgo", "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Mauricio", "Mauritania", "México", "Micronesia", "Moldavia", "Mónaco", "Mongolia", "Mozambique", "Namibia", "Nauru", "Nepal", "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Pakistán", "Países Bajos", "Palaos", "Palestina", "Panamá", "Paraguay", "Perú", "Polonia", "Portugal", "Qatar", "República Centroafricana", "República Checa", "República del Congo (Congo-Kinshasa)", "República Dominicana", "Rumania", "Rusia", "Ruanda", "San Cristóbal y Nieves", "San Marino", "Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Tailandia", "Taiwán", "Tanzania", "Tayikistán", "Timor Oriental", "Togo", "Tonga", "Trinidad y Tobago", "Túnez", "Turquía", "Turkmenistán", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabue" });
            comboBoxPaises.Location = new Point(103, 120);
            comboBoxPaises.Name = "comboBoxPaises";
            comboBoxPaises.Size = new Size(315, 25);
            comboBoxPaises.TabIndex = 2;
            comboBoxPaises.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // richTextBoxObservaciones
            // 
            richTextBoxObservaciones.Anchor = AnchorStyles.Top;
            richTextBoxObservaciones.BorderStyle = BorderStyle.None;
            richTextBoxObservaciones.Location = new Point(103, 262);
            richTextBoxObservaciones.Name = "richTextBoxObservaciones";
            richTextBoxObservaciones.Size = new Size(358, 66);
            richTextBoxObservaciones.TabIndex = 4;
            richTextBoxObservaciones.Text = "";
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top;
            roundedButton1.BackColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BackgroundColor = Color.FromArgb(175, 192, 218);
            roundedButton1.BorderColor = Color.FromArgb(196, 195, 209);
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 13F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(122, 28);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(270, 50);
            roundedButton1.TabIndex = 7;
            roundedButton1.Text = "EDITAR PAÍS";
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
            panel2.Size = new Size(565, 34);
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
            button2.Location = new Point(514, 0);
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
            // btnAgregarPais
            // 
            btnAgregarPais.Anchor = AnchorStyles.Top;
            btnAgregarPais.BackColor = Color.FromArgb(96, 149, 241);
            btnAgregarPais.FlatAppearance.BorderSize = 0;
            btnAgregarPais.FlatStyle = FlatStyle.Flat;
            btnAgregarPais.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            btnAgregarPais.ForeColor = Color.White;
            btnAgregarPais.IconChar = FontAwesome.Sharp.IconChar.Pen;
            btnAgregarPais.IconColor = Color.White;
            btnAgregarPais.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAgregarPais.IconSize = 25;
            btnAgregarPais.ImageAlign = ContentAlignment.MiddleLeft;
            btnAgregarPais.Location = new Point(103, 353);
            btnAgregarPais.Name = "btnAgregarPais";
            btnAgregarPais.Size = new Size(160, 38);
            btnAgregarPais.TabIndex = 5;
            btnAgregarPais.Text = "EDITAR";
            btnAgregarPais.TextAlign = ContentAlignment.MiddleRight;
            btnAgregarPais.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnAgregarPais.UseVisualStyleBackColor = false;
            btnAgregarPais.Click += iconButton3_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top;
            btnCancelar.BackColor = Color.Gainsboro;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 25;
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(301, 353);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(160, 38);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += iconButton2_Click;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(comboBoxNivelRiesgo);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(roundedButton1);
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(richTextBoxObservaciones);
            panel1.Controls.Add(btnAgregarPais);
            panel1.Controls.Add(comboBoxPaises);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(565, 436);
            panel1.TabIndex = 9;
            // 
            // comboBoxNivelRiesgo
            // 
            comboBoxNivelRiesgo.Anchor = AnchorStyles.Top;
            comboBoxNivelRiesgo.BackColor = Color.White;
            comboBoxNivelRiesgo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNivelRiesgo.FlatStyle = FlatStyle.Flat;
            comboBoxNivelRiesgo.FormattingEnabled = true;
            comboBoxNivelRiesgo.Items.AddRange(new object[] { "Bajo", "Medio", "Alto" });
            comboBoxNivelRiesgo.Location = new Point(103, 197);
            comboBoxNivelRiesgo.Name = "comboBoxNivelRiesgo";
            comboBoxNivelRiesgo.Size = new Size(178, 25);
            comboBoxNivelRiesgo.TabIndex = 9;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(103, 177);
            label1.Name = "label1";
            label1.Size = new Size(97, 17);
            label1.TabIndex = 8;
            label1.Text = "Nivel de riesgo";
            // 
            // FrmEditarPais
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(565, 471);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Font = new Font("Century Gothic", 9F);
            Name = "FrmEditarPais";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmEditarPais";
            Load += FrmEditarPais_Load;
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label2;
        private Label label3;
        private Clases.RoundedButton roundedButton1;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private FontAwesome.Sharp.IconButton btnAgregarPais;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private Panel panel1;
        private Label label1;
        public ComboBox comboBoxPaises;
        public RichTextBox richTextBoxObservaciones;
        public ComboBox comboBoxNivelRiesgo;
    }
}