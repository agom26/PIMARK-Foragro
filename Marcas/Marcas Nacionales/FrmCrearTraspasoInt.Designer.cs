namespace Presentacion.Marcas_Internacionales
{
    partial class FrmCrearTraspasoInt
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
            labelEstado = new Label();
            labelAnotaciones = new Label();
            dateTimePickerFecha = new DateTimePicker();
            textBoxAnotaciones = new RichTextBox();
            lblUser = new Label();
            btnTraspasar = new Presentacion.Clases.RoundedButton();
            panel2 = new Panel();
            button2 = new Button();
            button1 = new Button();
            btnAceptar = new FontAwesome.Sharp.IconButton();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            txtEstado = new TextBox();
            labelAntiguoTitular = new Label();
            groupBoxTraspaso = new GroupBox();
            txtNombreTitularN = new TextBox();
            btnNuevoTitular = new Presentacion.Clases.RoundedButton();
            txtNombreTitularA = new TextBox();
            txtNoExpediente = new TextBox();
            labelNoExpediente = new Label();
            groupBoxHistorial = new GroupBox();
            panel1 = new Panel();
            tblLayoutPrincipal = new TableLayoutPanel();
            panelEncabezado = new Panel();
            panel4 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panelBotones = new Panel();
            panel2.SuspendLayout();
            groupBoxTraspaso.SuspendLayout();
            groupBoxHistorial.SuspendLayout();
            panel1.SuspendLayout();
            tblLayoutPrincipal.SuspendLayout();
            panelEncabezado.SuspendLayout();
            panel4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // labelFecha
            // 
            labelFecha.AutoSize = true;
            labelFecha.Location = new Point(31, 70);
            labelFecha.Name = "labelFecha";
            labelFecha.Size = new Size(45, 17);
            labelFecha.TabIndex = 0;
            labelFecha.Text = "Fecha";
            // 
            // labelEstado
            // 
            labelEstado.AutoSize = true;
            labelEstado.Location = new Point(265, 70);
            labelEstado.Name = "labelEstado";
            labelEstado.Size = new Size(48, 17);
            labelEstado.TabIndex = 1;
            labelEstado.Text = "Estado";
            // 
            // labelAnotaciones
            // 
            labelAnotaciones.AutoSize = true;
            labelAnotaciones.Location = new Point(31, 149);
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
            lblUser.Location = new Point(212, 28);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(45, 17);
            lblUser.TabIndex = 6;
            lblUser.Text = "Fecha";
            // 
            // btnTraspasar
            // 
            btnTraspasar.BackColor = Color.FromArgb(175, 192, 218);
            btnTraspasar.BackgroundColor = Color.FromArgb(175, 192, 218);
            btnTraspasar.BorderColor = Color.FromArgb(175, 192, 218);
            btnTraspasar.BorderRadius = 40;
            btnTraspasar.BorderSize = 0;
            btnTraspasar.FlatAppearance.BorderSize = 0;
            btnTraspasar.FlatStyle = FlatStyle.Flat;
            btnTraspasar.Font = new Font("Century Gothic", 13F);
            btnTraspasar.ForeColor = Color.Black;
            btnTraspasar.Location = new Point(409, 49);
            btnTraspasar.Name = "btnTraspasar";
            btnTraspasar.Size = new Size(290, 50);
            btnTraspasar.TabIndex = 7;
            btnTraspasar.Text = "TRASPASAR";
            btnTraspasar.TextColor = Color.Black;
            btnTraspasar.UseVisualStyleBackColor = false;
            btnTraspasar.Click += btnTraspasar_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(34, 77, 112);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1090, 34);
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
            button2.Location = new Point(1039, 0);
            button2.Name = "button2";
            button2.Size = new Size(51, 34);
            button2.TabIndex = 6;
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
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.FromArgb(1, 87, 155);
            btnAceptar.FlatAppearance.BorderSize = 0;
            btnAceptar.FlatStyle = FlatStyle.Flat;
            btnAceptar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnAceptar.ForeColor = Color.White;
            btnAceptar.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnAceptar.IconColor = Color.White;
            btnAceptar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAceptar.IconSize = 25;
            btnAceptar.ImageAlign = ContentAlignment.MiddleLeft;
            btnAceptar.Location = new Point(351, 36);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(160, 45);
            btnAceptar.TabIndex = 4;
            btnAceptar.Text = "ACEPTAR";
            btnAceptar.TextAlign = ContentAlignment.MiddleRight;
            btnAceptar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += iconButton3_Click;
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
            btnCancelar.Location = new Point(554, 36);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(160, 45);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += iconButton2_Click;
            // 
            // txtEstado
            // 
            txtEstado.Location = new Point(265, 95);
            txtEstado.Name = "txtEstado";
            txtEstado.ReadOnly = true;
            txtEstado.Size = new Size(95, 22);
            txtEstado.TabIndex = 11;
            txtEstado.Text = "Registrada";
            // 
            // labelAntiguoTitular
            // 
            labelAntiguoTitular.AutoSize = true;
            labelAntiguoTitular.Location = new Point(76, 91);
            labelAntiguoTitular.Name = "labelAntiguoTitular";
            labelAntiguoTitular.Size = new Size(93, 17);
            labelAntiguoTitular.TabIndex = 12;
            labelAntiguoTitular.Text = "Antiguo titular";
            // 
            // groupBoxTraspaso
            // 
            groupBoxTraspaso.Controls.Add(txtNombreTitularN);
            groupBoxTraspaso.Controls.Add(btnNuevoTitular);
            groupBoxTraspaso.Controls.Add(txtNombreTitularA);
            groupBoxTraspaso.Controls.Add(txtNoExpediente);
            groupBoxTraspaso.Controls.Add(labelNoExpediente);
            groupBoxTraspaso.Controls.Add(labelAntiguoTitular);
            groupBoxTraspaso.Dock = DockStyle.Fill;
            groupBoxTraspaso.Location = new Point(562, 20);
            groupBoxTraspaso.Margin = new Padding(20);
            groupBoxTraspaso.Name = "groupBoxTraspaso";
            groupBoxTraspaso.Size = new Size(502, 335);
            groupBoxTraspaso.TabIndex = 20;
            groupBoxTraspaso.TabStop = false;
            groupBoxTraspaso.Text = "Traspaso";
            groupBoxTraspaso.Resize += groupBoxTraspaso_Resize;
            // 
            // txtNombreTitularN
            // 
            txtNombreTitularN.Location = new Point(76, 200);
            txtNombreTitularN.Name = "txtNombreTitularN";
            txtNombreTitularN.ReadOnly = true;
            txtNombreTitularN.Size = new Size(323, 22);
            txtNombreTitularN.TabIndex = 21;
            // 
            // btnNuevoTitular
            // 
            btnNuevoTitular.BackColor = Color.LightSteelBlue;
            btnNuevoTitular.BackgroundColor = Color.LightSteelBlue;
            btnNuevoTitular.BorderColor = Color.Empty;
            btnNuevoTitular.BorderRadius = 30;
            btnNuevoTitular.BorderSize = 0;
            btnNuevoTitular.FlatAppearance.BorderSize = 0;
            btnNuevoTitular.FlatStyle = FlatStyle.Flat;
            btnNuevoTitular.ForeColor = Color.Black;
            btnNuevoTitular.Location = new Point(76, 164);
            btnNuevoTitular.Name = "btnNuevoTitular";
            btnNuevoTitular.Size = new Size(156, 30);
            btnNuevoTitular.TabIndex = 3;
            btnNuevoTitular.Text = "Nuevo titular";
            btnNuevoTitular.TextColor = Color.Black;
            btnNuevoTitular.UseVisualStyleBackColor = false;
            btnNuevoTitular.Click += roundedButton2_Click;
            // 
            // txtNombreTitularA
            // 
            txtNombreTitularA.Location = new Point(76, 114);
            txtNombreTitularA.Name = "txtNombreTitularA";
            txtNombreTitularA.ReadOnly = true;
            txtNombreTitularA.Size = new Size(323, 22);
            txtNombreTitularA.TabIndex = 19;
            // 
            // txtNoExpediente
            // 
            txtNoExpediente.BorderStyle = BorderStyle.None;
            txtNoExpediente.Location = new Point(173, 46);
            txtNoExpediente.Name = "txtNoExpediente";
            txtNoExpediente.ReadOnly = true;
            txtNoExpediente.Size = new Size(120, 15);
            txtNoExpediente.TabIndex = 12;
            txtNoExpediente.TextAlign = HorizontalAlignment.Center;
            txtNoExpediente.TextChanged += textBox2_TextChanged;
            // 
            // labelNoExpediente
            // 
            labelNoExpediente.AutoSize = true;
            labelNoExpediente.Location = new Point(173, 23);
            labelNoExpediente.Name = "labelNoExpediente";
            labelNoExpediente.Size = new Size(98, 17);
            labelNoExpediente.TabIndex = 12;
            labelNoExpediente.Text = "No. Expediente";
            // 
            // groupBoxHistorial
            // 
            groupBoxHistorial.Controls.Add(dateTimePickerFecha);
            groupBoxHistorial.Controls.Add(txtEstado);
            groupBoxHistorial.Controls.Add(labelFecha);
            groupBoxHistorial.Controls.Add(labelEstado);
            groupBoxHistorial.Controls.Add(labelAnotaciones);
            groupBoxHistorial.Controls.Add(textBoxAnotaciones);
            groupBoxHistorial.Controls.Add(lblUser);
            groupBoxHistorial.Dock = DockStyle.Fill;
            groupBoxHistorial.Location = new Point(20, 20);
            groupBoxHistorial.Margin = new Padding(20);
            groupBoxHistorial.Name = "groupBoxHistorial";
            groupBoxHistorial.Size = new Size(502, 335);
            groupBoxHistorial.TabIndex = 21;
            groupBoxHistorial.TabStop = false;
            groupBoxHistorial.Text = "Historial";
            groupBoxHistorial.Resize += groupBoxHistorial_Resize;
            // 
            // panel1
            // 
            panel1.Controls.Add(tblLayoutPrincipal);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(1090, 645);
            panel1.TabIndex = 22;
            // 
            // tblLayoutPrincipal
            // 
            tblLayoutPrincipal.ColumnCount = 1;
            tblLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLayoutPrincipal.Controls.Add(panelEncabezado, 0, 0);
            tblLayoutPrincipal.Controls.Add(panel4, 0, 1);
            tblLayoutPrincipal.Controls.Add(panelBotones, 0, 2);
            tblLayoutPrincipal.Dock = DockStyle.Fill;
            tblLayoutPrincipal.Location = new Point(0, 0);
            tblLayoutPrincipal.Name = "tblLayoutPrincipal";
            tblLayoutPrincipal.RowCount = 3;
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 26.73077F));
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 73.26923F));
            tblLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 124F));
            tblLayoutPrincipal.Size = new Size(1090, 645);
            tblLayoutPrincipal.TabIndex = 0;
            // 
            // panelEncabezado
            // 
            panelEncabezado.Controls.Add(btnTraspasar);
            panelEncabezado.Dock = DockStyle.Fill;
            panelEncabezado.Location = new Point(3, 3);
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Size = new Size(1084, 133);
            panelEncabezado.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(tableLayoutPanel1);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 142);
            panel4.Name = "panel4";
            panel4.Size = new Size(1084, 375);
            panel4.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(groupBoxTraspaso, 1, 0);
            tableLayoutPanel1.Controls.Add(groupBoxHistorial, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1084, 375);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(btnAceptar);
            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Dock = DockStyle.Fill;
            panelBotones.Location = new Point(3, 523);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(1084, 119);
            panelBotones.TabIndex = 2;
            // 
            // FrmCrearTraspasoInt
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1090, 679);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Font = new Font("Century Gothic", 9F);
            Name = "FrmCrearTraspasoInt";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmCrearTraspasoInt";
            Load += FrmCrearTraspasoInt_Load;
            panel2.ResumeLayout(false);
            groupBoxTraspaso.ResumeLayout(false);
            groupBoxTraspaso.PerformLayout();
            groupBoxHistorial.ResumeLayout(false);
            groupBoxHistorial.PerformLayout();
            panel1.ResumeLayout(false);
            tblLayoutPrincipal.ResumeLayout(false);
            panelEncabezado.ResumeLayout(false);
            panel4.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label labelFecha;
        private Label labelEstado;
        private Label labelAnotaciones;
        private DateTimePicker dateTimePickerFecha;
        private RichTextBox textBoxAnotaciones;
        private Label lblUser;
        private Clases.RoundedButton btnTraspasar;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private FontAwesome.Sharp.IconButton btnAceptar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private TextBox txtEstado;
        private Label labelAntiguoTitular;
        private GroupBox groupBoxTraspaso;
        private GroupBox groupBoxHistorial;
        private TextBox txtNoExpediente;
        private Label labelNoExpediente;
        private Clases.RoundedButton btnNuevoTitular;
        private TextBox txtNombreTitularA;
        private TextBox txtNombreTitularN;
        private Panel panel1;
        private TableLayoutPanel tblLayoutPrincipal;
        private Panel panelEncabezado;
        private Panel panel4;
        private Panel panelBotones;
        private TableLayoutPanel tableLayoutPanel1;
    }
}