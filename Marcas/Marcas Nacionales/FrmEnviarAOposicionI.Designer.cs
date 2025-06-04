namespace Presentacion.Marcas_Internacionales
{
    partial class FrmEnviarAOposicionI
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
            labelFecha = new Label();
            labelAnotaciones = new Label();
            dateTimePickerFecha = new DateTimePicker();
            textBoxAnotaciones = new RichTextBox();
            lblUser = new Label();
            btnEnviarOposicion = new Presentacion.Clases.RoundedButton();
            panelSuperior = new Panel();
            button2 = new Button();
            button1 = new Button();
            panel1 = new Panel();
            btnOposicion = new FontAwesome.Sharp.IconButton();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            labelOpositor = new Label();
            groupBoxOposicion = new GroupBox();
            labelSignoOpositor = new Label();
            txtSolicitante = new TextBox();
            txtNombreOpositor = new TextBox();
            groupBoxHistorial = new GroupBox();
            panel2 = new Panel();
            tblLayoutPrincipal = new TableLayoutPanel();
            panelEncabezado = new Panel();
            panelContenidoCentro = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panelBotones = new Panel();
            panelSuperior.SuspendLayout();
            groupBoxOposicion.SuspendLayout();
            groupBoxHistorial.SuspendLayout();
            panel2.SuspendLayout();
            tblLayoutPrincipal.SuspendLayout();
            panelEncabezado.SuspendLayout();
            panelContenidoCentro.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // labelFecha
            // 
            labelFecha.AutoSize = true;
            labelFecha.Location = new Point(28, 67);
            labelFecha.Name = "labelFecha";
            labelFecha.Size = new Size(125, 17);
            labelFecha.TabIndex = 0;
            labelFecha.Text = "Fecha de oposición";
            // 
            // labelAnotaciones
            // 
            labelAnotaciones.AutoSize = true;
            labelAnotaciones.Location = new Point(28, 146);
            labelAnotaciones.Name = "labelAnotaciones";
            labelAnotaciones.Size = new Size(84, 17);
            labelAnotaciones.TabIndex = 2;
            labelAnotaciones.Text = "Anotaciones";
            // 
            // dateTimePickerFecha
            // 
            dateTimePickerFecha.Format = DateTimePickerFormat.Short;
            dateTimePickerFecha.Location = new Point(31, 93);
            dateTimePickerFecha.Name = "dateTimePickerFecha";
            dateTimePickerFecha.Size = new Size(154, 22);
            dateTimePickerFecha.TabIndex = 1;
            dateTimePickerFecha.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // textBoxAnotaciones
            // 
            textBoxAnotaciones.BorderStyle = BorderStyle.None;
            textBoxAnotaciones.Location = new Point(31, 172);
            textBoxAnotaciones.Name = "textBoxAnotaciones";
            textBoxAnotaciones.Size = new Size(431, 70);
            textBoxAnotaciones.TabIndex = 2;
            textBoxAnotaciones.Text = "";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(221, 88);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(45, 17);
            lblUser.TabIndex = 6;
            lblUser.Text = "Fecha";
            // 
            // btnEnviarOposicion
            // 
            btnEnviarOposicion.BackColor = Color.FromArgb(175, 192, 218);
            btnEnviarOposicion.BackgroundColor = Color.FromArgb(175, 192, 218);
            btnEnviarOposicion.BorderColor = Color.FromArgb(175, 192, 218);
            btnEnviarOposicion.BorderRadius = 40;
            btnEnviarOposicion.BorderSize = 0;
            btnEnviarOposicion.FlatAppearance.BorderSize = 0;
            btnEnviarOposicion.FlatStyle = FlatStyle.Flat;
            btnEnviarOposicion.Font = new Font("Century Gothic", 13F);
            btnEnviarOposicion.ForeColor = Color.Black;
            btnEnviarOposicion.Location = new Point(397, 29);
            btnEnviarOposicion.Name = "btnEnviarOposicion";
            btnEnviarOposicion.Size = new Size(290, 50);
            btnEnviarOposicion.TabIndex = 8;
            btnEnviarOposicion.Text = "ENVIAR A OPOSICIÓN";
            btnEnviarOposicion.TextColor = Color.Black;
            btnEnviarOposicion.UseVisualStyleBackColor = false;
            btnEnviarOposicion.Click += btnEnviarOposicion_Click;
            // 
            // panelSuperior
            // 
            panelSuperior.BackColor = Color.FromArgb(34, 77, 112);
            panelSuperior.Controls.Add(button2);
            panelSuperior.Controls.Add(button1);
            panelSuperior.Controls.Add(panel1);
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.Location = new Point(0, 0);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(1090, 34);
            panelSuperior.TabIndex = 8;
            panelSuperior.MouseDown += panelSuperior_MouseDown;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 12F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(1039, 0);
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
            // panel1
            // 
            panel1.Location = new Point(3, 40);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 455);
            panel1.TabIndex = 22;
            // 
            // btnOposicion
            // 
            btnOposicion.BackColor = Color.FromArgb(161, 136, 127);
            btnOposicion.FlatAppearance.BorderSize = 0;
            btnOposicion.FlatStyle = FlatStyle.Flat;
            btnOposicion.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnOposicion.ForeColor = Color.White;
            btnOposicion.IconChar = FontAwesome.Sharp.IconChar.Gavel;
            btnOposicion.IconColor = Color.White;
            btnOposicion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnOposicion.IconSize = 25;
            btnOposicion.ImageAlign = ContentAlignment.MiddleLeft;
            btnOposicion.Location = new Point(362, 30);
            btnOposicion.Name = "btnOposicion";
            btnOposicion.Size = new Size(160, 49);
            btnOposicion.TabIndex = 5;
            btnOposicion.Text = "OPOSICIÓN";
            btnOposicion.TextAlign = ContentAlignment.MiddleRight;
            btnOposicion.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnOposicion.UseVisualStyleBackColor = false;
            btnOposicion.Click += iconButton3_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 25;
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(562, 30);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(160, 49);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += iconButton2_Click;
            // 
            // labelOpositor
            // 
            labelOpositor.AutoSize = true;
            labelOpositor.Location = new Point(84, 88);
            labelOpositor.Name = "labelOpositor";
            labelOpositor.Size = new Size(59, 17);
            labelOpositor.TabIndex = 12;
            labelOpositor.Text = "Opositor";
            // 
            // groupBoxOposicion
            // 
            groupBoxOposicion.Controls.Add(labelSignoOpositor);
            groupBoxOposicion.Controls.Add(txtSolicitante);
            groupBoxOposicion.Controls.Add(txtNombreOpositor);
            groupBoxOposicion.Controls.Add(labelOpositor);
            groupBoxOposicion.Dock = DockStyle.Fill;
            groupBoxOposicion.Location = new Point(562, 20);
            groupBoxOposicion.Margin = new Padding(20);
            groupBoxOposicion.Name = "groupBoxOposicion";
            groupBoxOposicion.Size = new Size(502, 249);
            groupBoxOposicion.TabIndex = 20;
            groupBoxOposicion.TabStop = false;
            groupBoxOposicion.Text = "Oposición";
            // 
            // labelSignoOpositor
            // 
            labelSignoOpositor.AutoSize = true;
            labelSignoOpositor.Location = new Point(87, 172);
            labelSignoOpositor.Name = "labelSignoOpositor";
            labelSignoOpositor.Size = new Size(93, 17);
            labelSignoOpositor.TabIndex = 24;
            labelSignoOpositor.Text = "Signo opositor";
            // 
            // txtSolicitante
            // 
            txtSolicitante.Location = new Point(87, 195);
            txtSolicitante.Name = "txtSolicitante";
            txtSolicitante.Size = new Size(323, 22);
            txtSolicitante.TabIndex = 4;
            // 
            // txtNombreOpositor
            // 
            txtNombreOpositor.Location = new Point(87, 111);
            txtNombreOpositor.Name = "txtNombreOpositor";
            txtNombreOpositor.Size = new Size(323, 22);
            txtNombreOpositor.TabIndex = 3;
            // 
            // groupBoxHistorial
            // 
            groupBoxHistorial.Controls.Add(dateTimePickerFecha);
            groupBoxHistorial.Controls.Add(labelFecha);
            groupBoxHistorial.Controls.Add(labelAnotaciones);
            groupBoxHistorial.Controls.Add(textBoxAnotaciones);
            groupBoxHistorial.Controls.Add(lblUser);
            groupBoxHistorial.Dock = DockStyle.Fill;
            groupBoxHistorial.Location = new Point(20, 20);
            groupBoxHistorial.Margin = new Padding(20);
            groupBoxHistorial.Name = "groupBoxHistorial";
            groupBoxHistorial.Padding = new Padding(0);
            groupBoxHistorial.Size = new Size(502, 249);
            groupBoxHistorial.TabIndex = 21;
            groupBoxHistorial.TabStop = false;
            groupBoxHistorial.Text = "Historial";
            // 
            // panel2
            // 
            panel2.Controls.Add(tblLayoutPrincipal);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 34);
            panel2.Name = "panel2";
            panel2.Size = new Size(1090, 536);
            panel2.TabIndex = 22;
            // 
            // tblLayoutPrincipal
            // 
            tblLayoutPrincipal.ColumnCount = 1;
            tblLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLayoutPrincipal.Controls.Add(panelEncabezado, 0, 0);
            tblLayoutPrincipal.Controls.Add(panelContenidoCentro, 0, 1);
            tblLayoutPrincipal.Controls.Add(panelBotones, 0, 2);
            tblLayoutPrincipal.Dock = DockStyle.Fill;
            tblLayoutPrincipal.Location = new Point(0, 0);
            tblLayoutPrincipal.Name = "tblLayoutPrincipal";
            tblLayoutPrincipal.RowCount = 3;
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 27.954546F));
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 72.0454559F));
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 126F));
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLayoutPrincipal.Size = new Size(1090, 536);
            tblLayoutPrincipal.TabIndex = 0;
            // 
            // panelEncabezado
            // 
            panelEncabezado.Controls.Add(btnEnviarOposicion);
            panelEncabezado.Dock = DockStyle.Fill;
            panelEncabezado.Location = new Point(3, 3);
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Size = new Size(1084, 108);
            panelEncabezado.TabIndex = 0;
            // 
            // panelContenidoCentro
            // 
            panelContenidoCentro.Controls.Add(tableLayoutPanel1);
            panelContenidoCentro.Dock = DockStyle.Fill;
            panelContenidoCentro.Location = new Point(3, 117);
            panelContenidoCentro.Name = "panelContenidoCentro";
            panelContenidoCentro.Size = new Size(1084, 289);
            panelContenidoCentro.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(groupBoxHistorial, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBoxOposicion, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1084, 289);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(btnOposicion);
            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Dock = DockStyle.Fill;
            panelBotones.Location = new Point(3, 412);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(1084, 121);
            panelBotones.TabIndex = 2;
            // 
            // FrmEnviarAOposicionI
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1090, 570);
            Controls.Add(panel2);
            Controls.Add(panelSuperior);
            Font = new Font("Century Gothic", 9F);
            Name = "FrmEnviarAOposicionI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmEnviarAOposicionI";
            Load += FrmEnviarAOposicionI_Load;
            panelSuperior.ResumeLayout(false);
            groupBoxOposicion.ResumeLayout(false);
            groupBoxOposicion.PerformLayout();
            groupBoxHistorial.ResumeLayout(false);
            groupBoxHistorial.PerformLayout();
            panel2.ResumeLayout(false);
            tblLayoutPrincipal.ResumeLayout(false);
            panelEncabezado.ResumeLayout(false);
            panelContenidoCentro.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label labelFecha;
        private Label labelAnotaciones;
        private DateTimePicker dateTimePickerFecha;
        private RichTextBox textBoxAnotaciones;
        private Label lblUser;
        private Clases.RoundedButton btnEnviarOposicion;
        private Panel panelSuperior;
        private Button button1;
        private Button button2;
        private FontAwesome.Sharp.IconButton btnOposicion;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private Label labelOpositor;
        private GroupBox groupBoxOposicion;
        private GroupBox groupBoxHistorial;
        private TextBox txtNombreOpositor;
        private TextBox txtSolicitante;
        private Label labelSignoOpositor;
        private Panel panel1;
        private Panel panel2;
        private TableLayoutPanel tblLayoutPrincipal;
        private Panel panelEncabezado;
        private Panel panelContenidoCentro;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelBotones;
    }
}