namespace Presentacion.Marcas_Nacionales
{
    partial class FrmMostrarTodas
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
            tabControl1 = new TabControl();
            tabPageListaMarcas = new TabPage();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            dtgMarcasN = new DataGridView();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            textBox1 = new TextBox();
            label2 = new Label();
            roundedButton3 = new Clases.RoundedButton();
            panel4 = new Panel();
            roundedButton5 = new Clases.RoundedButton();
            tabPageMarcaDetail = new TabPage();
            panel2 = new Panel();
            comboBoxTipoSigno = new ComboBox();
            comboBoxSignoDistintivo = new ComboBox();
            label25 = new Label();
            roundedButton6 = new Clases.RoundedButton();
            btnCancelar = new Clases.RoundedButton();
            roundedButton1 = new Clases.RoundedButton();
            checkBox1 = new CheckBox();
            label3 = new Label();
            btnActualizar = new Clases.RoundedButton();
            richTextBox1 = new RichTextBox();
            label16 = new Label();
            textBoxEstatus = new TextBox();
            label14 = new Label();
            datePickerFechaSolicitud = new DateTimePicker();
            label13 = new Label();
            txtNombreAgente = new TextBox();
            label12 = new Label();
            roundedButton2 = new Clases.RoundedButton();
            txtEntidadTitular = new TextBox();
            label11 = new Label();
            txtDireccionTitular = new TextBox();
            label10 = new Label();
            txtNombreTitular = new TextBox();
            label9 = new Label();
            roundedButton4 = new Clases.RoundedButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            pictureBox1 = new PictureBox();
            label8 = new Label();
            label5 = new Label();
            txtClase = new TextBox();
            label4 = new Label();
            txtNombre = new TextBox();
            label6 = new Label();
            txtExpediente = new TextBox();
            label7 = new Label();
            panel3 = new Panel();
            dateTimePFecha_vencimiento = new DateTimePicker();
            label19 = new Label();
            dateTimePFecha_Registro = new DateTimePicker();
            label18 = new Label();
            txtRegistro = new TextBox();
            label17 = new Label();
            txtFolio = new TextBox();
            label15 = new Label();
            txtLibro = new TextBox();
            label20 = new Label();
            tabPageHistorialMarca = new TabPage();
            panel1 = new Panel();
            iconButton5 = new FontAwesome.Sharp.IconButton();
            dtgHistorial = new DataGridView();
            panel5 = new Panel();
            label21 = new Label();
            tabPageHistorialDetail = new TabPage();
            panel6 = new Panel();
            roundedButton7 = new Clases.RoundedButton();
            panel7 = new Panel();
            btnEditarH = new FontAwesome.Sharp.IconButton();
            labelUserEditor = new Label();
            btnCancelarH = new FontAwesome.Sharp.IconButton();
            lblUser = new Label();
            richTextBoxAnotacionesH = new RichTextBox();
            comboBoxEstatusH = new ComboBox();
            label24 = new Label();
            dateTimePickerFechaH = new DateTimePicker();
            label23 = new Label();
            label22 = new Label();
            panel8 = new Panel();
            tabControl1.SuspendLayout();
            tabPageListaMarcas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtgMarcasN).BeginInit();
            tabPageMarcaDetail.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            tabPageHistorialMarca.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgHistorial).BeginInit();
            tabPageHistorialDetail.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(196, 195, 209);
            label1.Font = new Font("Century Gothic", 15F);
            label1.Location = new Point(369, 18);
            label1.Name = "label1";
            label1.Size = new Size(388, 31);
            label1.TabIndex = 0;
            label1.Text = "Marcas nacionales ingresadas";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageListaMarcas);
            tabControl1.Controls.Add(tabPageMarcaDetail);
            tabControl1.Controls.Add(tabPageHistorialMarca);
            tabControl1.Controls.Add(tabPageHistorialDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 827);
            tabControl1.TabIndex = 1;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPageListaMarcas
            // 
            tabPageListaMarcas.Controls.Add(iconPictureBox1);
            tabPageListaMarcas.Controls.Add(iconButton3);
            tabPageListaMarcas.Controls.Add(label1);
            tabPageListaMarcas.Controls.Add(ibtnEditar);
            tabPageListaMarcas.Controls.Add(dtgMarcasN);
            tabPageListaMarcas.Controls.Add(ibtnBuscar);
            tabPageListaMarcas.Controls.Add(textBox1);
            tabPageListaMarcas.Controls.Add(label2);
            tabPageListaMarcas.Controls.Add(roundedButton3);
            tabPageListaMarcas.Controls.Add(panel4);
            tabPageListaMarcas.Controls.Add(roundedButton5);
            tabPageListaMarcas.Location = new Point(4, 29);
            tabPageListaMarcas.Name = "tabPageListaMarcas";
            tabPageListaMarcas.Padding = new Padding(3);
            tabPageListaMarcas.Size = new Size(1161, 794);
            tabPageListaMarcas.TabIndex = 0;
            tabPageListaMarcas.Text = "Lista de marcas";
            tabPageListaMarcas.UseVisualStyleBackColor = true;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.Anchor = AnchorStyles.Top;
            iconPictureBox1.BackColor = Color.FromArgb(196, 195, 209);
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Flag;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 40;
            iconPictureBox1.Location = new Point(314, 18);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(40, 40);
            iconPictureBox1.TabIndex = 26;
            iconPictureBox1.TabStop = false;
            // 
            // iconButton3
            // 
            iconButton3.Anchor = AnchorStyles.Top;
            iconButton3.BackColor = Color.FromArgb(255, 167, 38);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 30;
            iconButton3.Location = new Point(1011, 267);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(144, 37);
            iconButton3.TabIndex = 25;
            iconButton3.Text = "Abandonar";
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // ibtnEditar
            // 
            ibtnEditar.Anchor = AnchorStyles.Top;
            ibtnEditar.BackColor = Color.FromArgb(96, 149, 241);
            ibtnEditar.FlatAppearance.BorderSize = 0;
            ibtnEditar.FlatStyle = FlatStyle.Flat;
            ibtnEditar.ForeColor = Color.White;
            ibtnEditar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            ibtnEditar.IconColor = Color.White;
            ibtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEditar.IconSize = 30;
            ibtnEditar.Location = new Point(1011, 213);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(144, 37);
            ibtnEditar.TabIndex = 20;
            ibtnEditar.Text = "Editar";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click;
            // 
            // dtgMarcasN
            // 
            dtgMarcasN.AllowUserToAddRows = false;
            dtgMarcasN.AllowUserToDeleteRows = false;
            dtgMarcasN.AllowUserToResizeRows = false;
            dtgMarcasN.Anchor = AnchorStyles.Top;
            dtgMarcasN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgMarcasN.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgMarcasN.BorderStyle = BorderStyle.None;
            dtgMarcasN.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgMarcasN.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgMarcasN.ColumnHeadersHeight = 40;
            dtgMarcasN.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgMarcasN.EnableHeadersVisualStyles = false;
            dtgMarcasN.GridColor = Color.LightGray;
            dtgMarcasN.Location = new Point(41, 234);
            dtgMarcasN.Name = "dtgMarcasN";
            dtgMarcasN.ReadOnly = true;
            dtgMarcasN.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgMarcasN.RowHeadersWidth = 51;
            dtgMarcasN.Size = new Size(934, 492);
            dtgMarcasN.TabIndex = 17;
            // 
            // ibtnBuscar
            // 
            ibtnBuscar.Anchor = AnchorStyles.Top;
            ibtnBuscar.BackColor = Color.Black;
            ibtnBuscar.FlatAppearance.BorderSize = 0;
            ibtnBuscar.FlatStyle = FlatStyle.Flat;
            ibtnBuscar.Font = new Font("Century Gothic", 10F);
            ibtnBuscar.ForeColor = Color.White;
            ibtnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlassPlus;
            ibtnBuscar.IconColor = Color.White;
            ibtnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnBuscar.IconSize = 30;
            ibtnBuscar.Location = new Point(681, 136);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(144, 32);
            ibtnBuscar.TabIndex = 16;
            ibtnBuscar.Text = "Buscar";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top;
            textBox1.Location = new Point(351, 140);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(301, 26);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(236, 236, 238);
            label2.Font = new Font("Century Gothic", 12F);
            label2.Location = new Point(186, 140);
            label2.Name = "label2";
            label2.Size = new Size(146, 23);
            label2.TabIndex = 0;
            label2.Text = "Buscar marca";
            // 
            // roundedButton3
            // 
            roundedButton3.Anchor = AnchorStyles.Top;
            roundedButton3.BackColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BackgroundColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderColor = Color.FromArgb(236, 236, 238);
            roundedButton3.BorderRadius = 20;
            roundedButton3.BorderSize = 0;
            roundedButton3.Enabled = false;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(174, 120);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(683, 61);
            roundedButton3.TabIndex = 22;
            roundedButton3.Text = "roundedButton3";
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top;
            panel4.BackColor = Color.FromArgb(192, 202, 212);
            panel4.Location = new Point(27, 213);
            panel4.Name = "panel4";
            panel4.Size = new Size(972, 542);
            panel4.TabIndex = 23;
            // 
            // roundedButton5
            // 
            roundedButton5.Anchor = AnchorStyles.Top;
            roundedButton5.BackColor = Color.FromArgb(196, 195, 209);
            roundedButton5.BackgroundColor = Color.FromArgb(196, 195, 209);
            roundedButton5.BorderColor = Color.FromArgb(196, 195, 209);
            roundedButton5.BorderRadius = 60;
            roundedButton5.BorderSize = 0;
            roundedButton5.Enabled = false;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(173, 6);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(683, 61);
            roundedButton5.TabIndex = 24;
            roundedButton5.Text = "roundedButton5";
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // tabPageMarcaDetail
            // 
            tabPageMarcaDetail.AutoScroll = true;
            tabPageMarcaDetail.Controls.Add(panel2);
            tabPageMarcaDetail.Location = new Point(4, 29);
            tabPageMarcaDetail.Name = "tabPageMarcaDetail";
            tabPageMarcaDetail.Padding = new Padding(3);
            tabPageMarcaDetail.Size = new Size(1161, 794);
            tabPageMarcaDetail.TabIndex = 1;
            tabPageMarcaDetail.Text = "Detalle de marca";
            tabPageMarcaDetail.UseVisualStyleBackColor = true;
            tabPageMarcaDetail.Click += tabPageMarcaDetail_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(comboBoxTipoSigno);
            panel2.Controls.Add(comboBoxSignoDistintivo);
            panel2.Controls.Add(label25);
            panel2.Controls.Add(roundedButton6);
            panel2.Controls.Add(btnCancelar);
            panel2.Controls.Add(roundedButton1);
            panel2.Controls.Add(checkBox1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(btnActualizar);
            panel2.Controls.Add(richTextBox1);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(textBoxEstatus);
            panel2.Controls.Add(label14);
            panel2.Controls.Add(datePickerFechaSolicitud);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(txtNombreAgente);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(roundedButton2);
            panel2.Controls.Add(txtEntidadTitular);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(txtDireccionTitular);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(txtNombreTitular);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(roundedButton4);
            panel2.Controls.Add(iconButton2);
            panel2.Controls.Add(iconButton1);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(txtClase);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txtNombre);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(txtExpediente);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(23, 19);
            panel2.Name = "panel2";
            panel2.Size = new Size(1081, 1241);
            panel2.TabIndex = 0;
            // 
            // comboBoxTipoSigno
            // 
            comboBoxTipoSigno.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxTipoSigno.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTipoSigno.FlatStyle = FlatStyle.Flat;
            comboBoxTipoSigno.FormattingEnabled = true;
            comboBoxTipoSigno.Items.AddRange(new object[] { "Denominativa", "Mixta", "Gráfica/Figurativa", "Sonora", "Olfativa" });
            comboBoxTipoSigno.Location = new Point(393, 265);
            comboBoxTipoSigno.Name = "comboBoxTipoSigno";
            comboBoxTipoSigno.Size = new Size(292, 28);
            comboBoxTipoSigno.TabIndex = 77;
            // 
            // comboBoxSignoDistintivo
            // 
            comboBoxSignoDistintivo.BackColor = Color.FromArgb(241, 240, 245);
            comboBoxSignoDistintivo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSignoDistintivo.FlatStyle = FlatStyle.Flat;
            comboBoxSignoDistintivo.FormattingEnabled = true;
            comboBoxSignoDistintivo.Items.AddRange(new object[] { "Marca", "Nombre comercial", "Señal de publicidad", "Emblema", "Indicación geográfica", "Denominación de origen" });
            comboBoxSignoDistintivo.Location = new Point(47, 265);
            comboBoxSignoDistintivo.Name = "comboBoxSignoDistintivo";
            comboBoxSignoDistintivo.Size = new Size(280, 28);
            comboBoxSignoDistintivo.TabIndex = 76;
            // 
            // label25
            // 
            label25.Anchor = AnchorStyles.None;
            label25.AutoSize = true;
            label25.Font = new Font("Century Gothic", 10F);
            label25.Location = new Point(393, 241);
            label25.Name = "label25";
            label25.Size = new Size(44, 21);
            label25.TabIndex = 75;
            label25.Text = "Tipo";
            // 
            // roundedButton6
            // 
            roundedButton6.BackColor = Color.LightSteelBlue;
            roundedButton6.BackgroundColor = Color.LightSteelBlue;
            roundedButton6.BorderColor = Color.LightSteelBlue;
            roundedButton6.BorderRadius = 40;
            roundedButton6.BorderSize = 0;
            roundedButton6.FlatAppearance.BorderSize = 0;
            roundedButton6.FlatStyle = FlatStyle.Flat;
            roundedButton6.Font = new Font("Century Gothic", 9F);
            roundedButton6.ForeColor = Color.Black;
            roundedButton6.Location = new Point(757, 626);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(276, 56);
            roundedButton6.TabIndex = 74;
            roundedButton6.Text = "VER HISTORIAL";
            roundedButton6.TextColor = Color.Black;
            roundedButton6.UseVisualStyleBackColor = false;
            roundedButton6.Click += roundedButton6_Click_1;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.None;
            btnCancelar.BackColor = Color.Gainsboro;
            btnCancelar.BackgroundColor = Color.Gainsboro;
            btnCancelar.BorderColor = Color.Empty;
            btnCancelar.BorderRadius = 60;
            btnCancelar.BorderSize = 0;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(370, 1161);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(315, 62);
            btnCancelar.TabIndex = 73;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += roundedButton6_Click;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.LightSteelBlue;
            roundedButton1.BackgroundColor = Color.LightSteelBlue;
            roundedButton1.BorderColor = Color.LightSteelBlue;
            roundedButton1.BorderRadius = 10;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 9F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(47, 344);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(280, 35);
            roundedButton1.TabIndex = 72;
            roundedButton1.Text = "+ ESTADO ACTUAL";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.None;
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Century Gothic", 10F);
            checkBox1.Location = new Point(49, 924);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(103, 25);
            checkBox1.TabIndex = 71;
            checkBox1.Text = "Registrar";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 19F);
            label3.Location = new Point(445, 0);
            label3.Name = "label3";
            label3.Size = new Size(132, 39);
            label3.TabIndex = 40;
            label3.Text = "Edición";
            // 
            // btnActualizar
            // 
            btnActualizar.Anchor = AnchorStyles.None;
            btnActualizar.BackColor = Color.FromArgb(1, 87, 155);
            btnActualizar.BackgroundColor = Color.FromArgb(1, 87, 155);
            btnActualizar.BorderColor = Color.FromArgb(1, 87, 155);
            btnActualizar.BorderRadius = 60;
            btnActualizar.BorderSize = 0;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(47, 1160);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(315, 62);
            btnActualizar.TabIndex = 69;
            btnActualizar.Text = "Actualizar";
            btnActualizar.TextColor = Color.White;
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnGuardar_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.None;
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Enabled = false;
            richTextBox1.Font = new Font("Century Gothic", 10F);
            richTextBox1.Location = new Point(47, 791);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(638, 120);
            richTextBox1.TabIndex = 68;
            richTextBox1.Text = "";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.None;
            label16.AutoSize = true;
            label16.Font = new Font("Century Gothic", 10F);
            label16.Location = new Point(47, 765);
            label16.Name = "label16";
            label16.Size = new Size(136, 21);
            label16.TabIndex = 67;
            label16.Text = "Observaciones";
            // 
            // textBoxEstatus
            // 
            textBoxEstatus.Anchor = AnchorStyles.None;
            textBoxEstatus.Enabled = false;
            textBoxEstatus.Font = new Font("Century Gothic", 10F);
            textBoxEstatus.Location = new Point(395, 351);
            textBoxEstatus.Name = "textBoxEstatus";
            textBoxEstatus.ReadOnly = true;
            textBoxEstatus.Size = new Size(290, 28);
            textBoxEstatus.TabIndex = 66;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.None;
            label14.AutoSize = true;
            label14.Font = new Font("Century Gothic", 10F);
            label14.Location = new Point(395, 327);
            label14.Name = "label14";
            label14.Size = new Size(128, 21);
            label14.TabIndex = 65;
            label14.Text = "Estado actual";
            // 
            // datePickerFechaSolicitud
            // 
            datePickerFechaSolicitud.Anchor = AnchorStyles.None;
            datePickerFechaSolicitud.Font = new Font("Century Gothic", 10F);
            datePickerFechaSolicitud.Format = DateTimePickerFormat.Short;
            datePickerFechaSolicitud.Location = new Point(395, 174);
            datePickerFechaSolicitud.Name = "datePickerFechaSolicitud";
            datePickerFechaSolicitud.Size = new Size(290, 28);
            datePickerFechaSolicitud.TabIndex = 64;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.None;
            label13.AutoSize = true;
            label13.Font = new Font("Century Gothic", 10F);
            label13.Location = new Point(395, 149);
            label13.Name = "label13";
            label13.Size = new Size(78, 21);
            label13.TabIndex = 63;
            label13.Text = "Solicitud";
            // 
            // txtNombreAgente
            // 
            txtNombreAgente.Enabled = false;
            txtNombreAgente.Font = new Font("Century Gothic", 10F);
            txtNombreAgente.Location = new Point(47, 730);
            txtNombreAgente.Name = "txtNombreAgente";
            txtNombreAgente.Size = new Size(280, 28);
            txtNombreAgente.TabIndex = 62;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Century Gothic", 10F);
            label12.Location = new Point(47, 704);
            label12.Name = "label12";
            label12.Size = new Size(77, 21);
            label12.TabIndex = 61;
            label12.Text = "Nombre";
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.LightSteelBlue;
            roundedButton2.BackgroundColor = Color.LightSteelBlue;
            roundedButton2.BorderColor = Color.LightSteelBlue;
            roundedButton2.BorderRadius = 40;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Century Gothic", 10F);
            roundedButton2.ForeColor = Color.Black;
            roundedButton2.Location = new Point(47, 625);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(638, 56);
            roundedButton2.TabIndex = 60;
            roundedButton2.Text = "+ AGENTE";
            roundedButton2.TextColor = Color.Black;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // txtEntidadTitular
            // 
            txtEntidadTitular.Enabled = false;
            txtEntidadTitular.Font = new Font("Century Gothic", 10F);
            txtEntidadTitular.Location = new Point(47, 579);
            txtEntidadTitular.Name = "txtEntidadTitular";
            txtEntidadTitular.Size = new Size(280, 28);
            txtEntidadTitular.TabIndex = 59;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Century Gothic", 10F);
            label11.Location = new Point(47, 553);
            label11.Name = "label11";
            label11.Size = new Size(75, 21);
            label11.TabIndex = 58;
            label11.Text = "Entidad";
            // 
            // txtDireccionTitular
            // 
            txtDireccionTitular.Enabled = false;
            txtDireccionTitular.Font = new Font("Century Gothic", 10F);
            txtDireccionTitular.Location = new Point(399, 512);
            txtDireccionTitular.Name = "txtDireccionTitular";
            txtDireccionTitular.Size = new Size(286, 28);
            txtDireccionTitular.TabIndex = 57;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 10F);
            label10.Location = new Point(399, 488);
            label10.Name = "label10";
            label10.Size = new Size(88, 21);
            label10.TabIndex = 56;
            label10.Text = "Dirección";
            // 
            // txtNombreTitular
            // 
            txtNombreTitular.Enabled = false;
            txtNombreTitular.Font = new Font("Century Gothic", 10F);
            txtNombreTitular.Location = new Point(47, 512);
            txtNombreTitular.Name = "txtNombreTitular";
            txtNombreTitular.Size = new Size(280, 28);
            txtNombreTitular.TabIndex = 55;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 10F);
            label9.Location = new Point(47, 486);
            label9.Name = "label9";
            label9.Size = new Size(77, 21);
            label9.TabIndex = 54;
            label9.Text = "Nombre";
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.LightSteelBlue;
            roundedButton4.BackgroundColor = Color.LightSteelBlue;
            roundedButton4.BorderColor = Color.LightSteelBlue;
            roundedButton4.BorderRadius = 40;
            roundedButton4.BorderSize = 0;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.Font = new Font("Century Gothic", 10F);
            roundedButton4.ForeColor = Color.Black;
            roundedButton4.Location = new Point(47, 411);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(638, 56);
            roundedButton4.TabIndex = 53;
            roundedButton4.Text = "+ TITULAR";
            roundedButton4.TextColor = Color.Black;
            roundedButton4.UseVisualStyleBackColor = false;
            roundedButton4.Click += roundedButton4_Click;
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.None;
            iconButton2.BackColor = Color.MistyRose;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 30;
            iconButton2.Location = new Point(897, 346);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(74, 33);
            iconButton2.TabIndex = 52;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.None;
            iconButton1.BackColor = Color.PowderBlue;
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.Upload;
            iconButton1.IconColor = Color.Black;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 30;
            iconButton1.Location = new Point(817, 346);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(74, 33);
            iconButton1.TabIndex = 51;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(757, 100);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(276, 240);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 50;
            pictureBox1.TabStop = false;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 10F);
            label8.Location = new Point(849, 74);
            label8.Name = "label8";
            label8.Size = new Size(74, 21);
            label8.TabIndex = 49;
            label8.Text = "Imagen";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 10F);
            label5.Location = new Point(47, 241);
            label5.Name = "label5";
            label5.Size = new Size(132, 21);
            label5.TabIndex = 47;
            label5.Text = "Signo distintivo";
            // 
            // txtClase
            // 
            txtClase.Anchor = AnchorStyles.None;
            txtClase.Font = new Font("Century Gothic", 10F);
            txtClase.Location = new Point(47, 173);
            txtClase.Name = "txtClase";
            txtClase.Size = new Size(280, 28);
            txtClase.TabIndex = 46;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10F);
            label4.Location = new Point(47, 149);
            label4.Name = "label4";
            label4.Size = new Size(57, 21);
            label4.TabIndex = 45;
            label4.Text = "Clase";
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.None;
            txtNombre.Font = new Font("Century Gothic", 10F);
            txtNombre.Location = new Point(393, 100);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(292, 28);
            txtNombre.TabIndex = 44;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 10F);
            label6.Location = new Point(393, 76);
            label6.Name = "label6";
            label6.Size = new Size(77, 21);
            label6.TabIndex = 43;
            label6.Text = "Nombre";
            // 
            // txtExpediente
            // 
            txtExpediente.Anchor = AnchorStyles.None;
            txtExpediente.Font = new Font("Century Gothic", 10F);
            txtExpediente.Location = new Point(47, 100);
            txtExpediente.Name = "txtExpediente";
            txtExpediente.Size = new Size(280, 28);
            txtExpediente.TabIndex = 42;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 10F);
            label7.Location = new Point(47, 74);
            label7.Name = "label7";
            label7.Size = new Size(104, 21);
            label7.TabIndex = 41;
            label7.Text = "Expediente";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.Controls.Add(dateTimePFecha_vencimiento);
            panel3.Controls.Add(label19);
            panel3.Controls.Add(dateTimePFecha_Registro);
            panel3.Controls.Add(label18);
            panel3.Controls.Add(txtRegistro);
            panel3.Controls.Add(label17);
            panel3.Controls.Add(txtFolio);
            panel3.Controls.Add(label15);
            panel3.Controls.Add(txtLibro);
            panel3.Controls.Add(label20);
            panel3.Font = new Font("Century Gothic", 10F);
            panel3.Location = new Point(49, 959);
            panel3.Name = "panel3";
            panel3.Size = new Size(636, 185);
            panel3.TabIndex = 70;
            // 
            // dateTimePFecha_vencimiento
            // 
            dateTimePFecha_vencimiento.CalendarForeColor = Color.Red;
            dateTimePFecha_vencimiento.Enabled = false;
            dateTimePFecha_vencimiento.Format = DateTimePickerFormat.Short;
            dateTimePFecha_vencimiento.Location = new Point(396, 139);
            dateTimePFecha_vencimiento.Name = "dateTimePFecha_vencimiento";
            dateTimePFecha_vencimiento.Size = new Size(222, 28);
            dateTimePFecha_vencimiento.TabIndex = 41;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(396, 113);
            label19.Name = "label19";
            label19.Size = new Size(198, 21);
            label19.TabIndex = 40;
            label19.Text = "Fecha de vencimiento";
            // 
            // dateTimePFecha_Registro
            // 
            dateTimePFecha_Registro.Format = DateTimePickerFormat.Short;
            dateTimePFecha_Registro.Location = new Point(34, 139);
            dateTimePFecha_Registro.Name = "dateTimePFecha_Registro";
            dateTimePFecha_Registro.Size = new Size(224, 28);
            dateTimePFecha_Registro.TabIndex = 39;
            dateTimePFecha_Registro.ValueChanged += dateTimePFecha_Registro_ValueChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(34, 114);
            label18.Name = "label18";
            label18.Size = new Size(155, 21);
            label18.TabIndex = 38;
            label18.Text = "Fecha de registro";
            // 
            // txtRegistro
            // 
            txtRegistro.Location = new Point(34, 55);
            txtRegistro.Name = "txtRegistro";
            txtRegistro.Size = new Size(172, 28);
            txtRegistro.TabIndex = 39;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(32, 30);
            label17.Name = "label17";
            label17.Size = new Size(75, 21);
            label17.TabIndex = 38;
            label17.Text = "Registro";
            // 
            // txtFolio
            // 
            txtFolio.Location = new Point(240, 55);
            txtFolio.Name = "txtFolio";
            txtFolio.Size = new Size(172, 28);
            txtFolio.TabIndex = 9;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(240, 29);
            label15.Name = "label15";
            label15.Size = new Size(46, 21);
            label15.TabIndex = 8;
            label15.Text = "Folio";
            // 
            // txtLibro
            // 
            txtLibro.Location = new Point(446, 55);
            txtLibro.Name = "txtLibro";
            txtLibro.Size = new Size(172, 28);
            txtLibro.TabIndex = 11;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(446, 29);
            label20.Name = "label20";
            label20.Size = new Size(49, 21);
            label20.TabIndex = 10;
            label20.Text = "Libro";
            // 
            // tabPageHistorialMarca
            // 
            tabPageHistorialMarca.Controls.Add(panel1);
            tabPageHistorialMarca.Location = new Point(4, 29);
            tabPageHistorialMarca.Name = "tabPageHistorialMarca";
            tabPageHistorialMarca.Size = new Size(1161, 794);
            tabPageHistorialMarca.TabIndex = 2;
            tabPageHistorialMarca.Text = "Historial";
            tabPageHistorialMarca.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.Controls.Add(iconButton5);
            panel1.Controls.Add(dtgHistorial);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(label21);
            panel1.Location = new Point(8, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1150, 783);
            panel1.TabIndex = 0;
            // 
            // iconButton5
            // 
            iconButton5.Anchor = AnchorStyles.Top;
            iconButton5.BackColor = Color.FromArgb(96, 149, 241);
            iconButton5.FlatAppearance.BorderSize = 0;
            iconButton5.FlatStyle = FlatStyle.Flat;
            iconButton5.ForeColor = Color.White;
            iconButton5.IconChar = FontAwesome.Sharp.IconChar.Pen;
            iconButton5.IconColor = Color.White;
            iconButton5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton5.IconSize = 30;
            iconButton5.Location = new Point(1001, 82);
            iconButton5.Name = "iconButton5";
            iconButton5.Size = new Size(144, 37);
            iconButton5.TabIndex = 44;
            iconButton5.Text = "Editar";
            iconButton5.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton5.UseVisualStyleBackColor = false;
            iconButton5.Click += iconButton5_Click;
            // 
            // dtgHistorial
            // 
            dtgHistorial.AllowUserToAddRows = false;
            dtgHistorial.AllowUserToDeleteRows = false;
            dtgHistorial.AllowUserToResizeRows = false;
            dtgHistorial.Anchor = AnchorStyles.Top;
            dtgHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgHistorial.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgHistorial.BorderStyle = BorderStyle.None;
            dtgHistorial.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgHistorial.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgHistorial.ColumnHeadersHeight = 40;
            dtgHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgHistorial.EnableHeadersVisualStyles = false;
            dtgHistorial.GridColor = Color.LightGray;
            dtgHistorial.Location = new Point(28, 103);
            dtgHistorial.Name = "dtgHistorial";
            dtgHistorial.ReadOnly = true;
            dtgHistorial.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgHistorial.RowHeadersWidth = 51;
            dtgHistorial.Size = new Size(934, 492);
            dtgHistorial.TabIndex = 42;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top;
            panel5.BackColor = Color.FromArgb(192, 202, 212);
            panel5.Location = new Point(14, 82);
            panel5.Name = "panel5";
            panel5.Size = new Size(972, 542);
            panel5.TabIndex = 43;
            // 
            // label21
            // 
            label21.Anchor = AnchorStyles.None;
            label21.AutoSize = true;
            label21.Font = new Font("Century Gothic", 19F);
            label21.Location = new Point(495, 9);
            label21.Name = "label21";
            label21.Size = new Size(134, 39);
            label21.TabIndex = 41;
            label21.Text = "Historial";
            // 
            // tabPageHistorialDetail
            // 
            tabPageHistorialDetail.Controls.Add(panel6);
            tabPageHistorialDetail.Location = new Point(4, 29);
            tabPageHistorialDetail.Name = "tabPageHistorialDetail";
            tabPageHistorialDetail.Padding = new Padding(3);
            tabPageHistorialDetail.Size = new Size(1161, 794);
            tabPageHistorialDetail.TabIndex = 3;
            tabPageHistorialDetail.Text = "Editar historial";
            tabPageHistorialDetail.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Top;
            panel6.Controls.Add(roundedButton7);
            panel6.Controls.Add(panel7);
            panel6.Location = new Point(31, 52);
            panel6.Name = "panel6";
            panel6.Size = new Size(1112, 555);
            panel6.TabIndex = 0;
            // 
            // roundedButton7
            // 
            roundedButton7.BackColor = Color.FromArgb(196, 195, 209);
            roundedButton7.BackgroundColor = Color.FromArgb(196, 195, 209);
            roundedButton7.BorderColor = Color.FromArgb(196, 195, 209);
            roundedButton7.BorderRadius = 50;
            roundedButton7.BorderSize = 0;
            roundedButton7.Enabled = false;
            roundedButton7.FlatAppearance.BorderSize = 0;
            roundedButton7.FlatStyle = FlatStyle.Flat;
            roundedButton7.Font = new Font("Century Gothic", 13F);
            roundedButton7.ForeColor = Color.Black;
            roundedButton7.Location = new Point(414, 43);
            roundedButton7.Name = "roundedButton7";
            roundedButton7.Size = new Size(270, 50);
            roundedButton7.TabIndex = 18;
            roundedButton7.Text = "Editar estado";
            roundedButton7.TextColor = Color.Black;
            roundedButton7.UseVisualStyleBackColor = false;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(192, 202, 212);
            panel7.Controls.Add(btnEditarH);
            panel7.Controls.Add(labelUserEditor);
            panel7.Controls.Add(btnCancelarH);
            panel7.Controls.Add(lblUser);
            panel7.Controls.Add(richTextBoxAnotacionesH);
            panel7.Controls.Add(comboBoxEstatusH);
            panel7.Controls.Add(label24);
            panel7.Controls.Add(dateTimePickerFechaH);
            panel7.Controls.Add(label23);
            panel7.Controls.Add(label22);
            panel7.Controls.Add(panel8);
            panel7.Location = new Point(46, 110);
            panel7.Name = "panel7";
            panel7.Size = new Size(1012, 406);
            panel7.TabIndex = 21;
            // 
            // btnEditarH
            // 
            btnEditarH.BackColor = Color.FromArgb(1, 87, 155);
            btnEditarH.FlatAppearance.BorderSize = 0;
            btnEditarH.FlatStyle = FlatStyle.Flat;
            btnEditarH.Font = new Font("Century Gothic", 10F);
            btnEditarH.ForeColor = Color.White;
            btnEditarH.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnEditarH.IconColor = Color.White;
            btnEditarH.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditarH.IconSize = 30;
            btnEditarH.Location = new Point(315, 321);
            btnEditarH.Name = "btnEditarH";
            btnEditarH.Size = new Size(179, 34);
            btnEditarH.TabIndex = 20;
            btnEditarH.Text = "Actualizar";
            btnEditarH.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnEditarH.UseVisualStyleBackColor = false;
            btnEditarH.Click += btnEditarH_Click;
            // 
            // labelUserEditor
            // 
            labelUserEditor.AutoSize = true;
            labelUserEditor.BackColor = Color.White;
            labelUserEditor.Location = new Point(476, 51);
            labelUserEditor.Name = "labelUserEditor";
            labelUserEditor.Size = new Size(55, 20);
            labelUserEditor.TabIndex = 18;
            labelUserEditor.Text = "Fecha";
            // 
            // btnCancelarH
            // 
            btnCancelarH.BackColor = Color.White;
            btnCancelarH.FlatAppearance.BorderSize = 0;
            btnCancelarH.FlatStyle = FlatStyle.Flat;
            btnCancelarH.Font = new Font("Century Gothic", 10F);
            btnCancelarH.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarH.IconColor = Color.Black;
            btnCancelarH.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarH.IconSize = 30;
            btnCancelarH.Location = new Point(520, 321);
            btnCancelarH.Name = "btnCancelarH";
            btnCancelarH.Size = new Size(179, 34);
            btnCancelarH.TabIndex = 19;
            btnCancelarH.Text = "Cancelar";
            btnCancelarH.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCancelarH.UseVisualStyleBackColor = false;
            btnCancelarH.Click += btnCancelarH_Click;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.BackColor = Color.White;
            lblUser.Location = new Point(295, 51);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(55, 20);
            lblUser.TabIndex = 17;
            lblUser.Text = "Fecha";
            // 
            // richTextBoxAnotacionesH
            // 
            richTextBoxAnotacionesH.BackColor = Color.White;
            richTextBoxAnotacionesH.BorderStyle = BorderStyle.FixedSingle;
            richTextBoxAnotacionesH.Location = new Point(295, 193);
            richTextBoxAnotacionesH.Name = "richTextBoxAnotacionesH";
            richTextBoxAnotacionesH.Size = new Size(431, 102);
            richTextBoxAnotacionesH.TabIndex = 16;
            richTextBoxAnotacionesH.Text = "";
            // 
            // comboBoxEstatusH
            // 
            comboBoxEstatusH.BackColor = Color.White;
            comboBoxEstatusH.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEstatusH.FormattingEnabled = true;
            comboBoxEstatusH.Items.AddRange(new object[] { "Ingresada", "Examen de forma", "Examen de fondo", "Requerimiento", "Objeción", "Edicto", "Publicación", "Oposición", "Orden de pago", "Abandono", "Registrada", "Licencia de uso" });
            comboBoxEstatusH.Location = new Point(476, 116);
            comboBoxEstatusH.Name = "comboBoxEstatusH";
            comboBoxEstatusH.Size = new Size(250, 28);
            comboBoxEstatusH.TabIndex = 15;
            comboBoxEstatusH.SelectedIndexChanged += comboBoxEstatusH_SelectedIndexChanged;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.BackColor = Color.White;
            label24.Location = new Point(295, 91);
            label24.Name = "label24";
            label24.Size = new Size(55, 20);
            label24.TabIndex = 11;
            label24.Text = "Fecha";
            // 
            // dateTimePickerFechaH
            // 
            dateTimePickerFechaH.Format = DateTimePickerFormat.Short;
            dateTimePickerFechaH.Location = new Point(295, 114);
            dateTimePickerFechaH.Name = "dateTimePickerFechaH";
            dateTimePickerFechaH.Size = new Size(154, 26);
            dateTimePickerFechaH.TabIndex = 14;
            dateTimePickerFechaH.ValueChanged += dateTimePickerFechaH_ValueChanged;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.BackColor = Color.White;
            label23.Location = new Point(476, 93);
            label23.Name = "label23";
            label23.Size = new Size(58, 20);
            label23.TabIndex = 12;
            label23.Text = "Estado";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.BackColor = Color.White;
            label22.Location = new Point(295, 170);
            label22.Name = "label22";
            label22.Size = new Size(102, 20);
            label22.TabIndex = 13;
            label22.Text = "Anotaciones";
            // 
            // panel8
            // 
            panel8.BackColor = Color.White;
            panel8.Location = new Point(42, 21);
            panel8.Name = "panel8";
            panel8.Size = new Size(946, 369);
            panel8.TabIndex = 21;
            // 
            // FrmMostrarTodas
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmMostrarTodas";
            Text = "FrmMostrarTodas";
            Load += FrmMostrarTodas_Load;
            tabControl1.ResumeLayout(false);
            tabPageListaMarcas.ResumeLayout(false);
            tabPageListaMarcas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtgMarcasN).EndInit();
            tabPageMarcaDetail.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            tabPageHistorialMarca.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgHistorial).EndInit();
            tabPageHistorialDetail.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabPageListaMarcas;
        private TabPage tabPageMarcaDetail;
        private Label label2;
        private TextBox textBox1;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private DataGridView dtgMarcasN;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private Clases.RoundedButton roundedButton3;
        private Panel panel4;
        private Panel panel2;
        private Clases.RoundedButton roundedButton1;
        private CheckBox checkBox1;
        private Label label3;
        private Clases.RoundedButton btnActualizar;
        private RichTextBox richTextBox1;
        private Label label16;
        private TextBox textBoxEstatus;
        private Label label14;
        private DateTimePicker datePickerFechaSolicitud;
        private Label label13;
        private TextBox txtNombreAgente;
        private Label label12;
        private Clases.RoundedButton roundedButton2;
        private TextBox txtEntidadTitular;
        private Label label11;
        private TextBox txtDireccionTitular;
        private Label label10;
        private TextBox txtNombreTitular;
        private Label label9;
        private Clases.RoundedButton roundedButton4;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private PictureBox pictureBox1;
        private Label label8;
        private Label label5;
        private TextBox txtClase;
        private Label label4;
        private TextBox txtNombre;
        private Label label6;
        private TextBox txtExpediente;
        private Label label7;
        private Panel panel3;
        private DateTimePicker dateTimePFecha_vencimiento;
        private Label label19;
        private DateTimePicker dateTimePFecha_Registro;
        private Label label18;
        private TextBox txtRegistro;
        private Label label17;
        private TextBox txtFolio;
        private Label label15;
        private TextBox txtLibro;
        private Label label20;
        private Clases.RoundedButton roundedButton5;
        private Clases.RoundedButton btnCancelar;
        private FontAwesome.Sharp.IconButton iconButton3;
        private Clases.RoundedButton roundedButton6;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private TabPage tabPageHistorialMarca;
        private Panel panel1;
        private Label label21;
        private FontAwesome.Sharp.IconButton iconButton5;
        private DataGridView dtgHistorial;
        private Panel panel5;
        private TabPage tabPageHistorialDetail;
        private Panel panel6;
        private FontAwesome.Sharp.IconButton btnEditarH;
        private FontAwesome.Sharp.IconButton btnCancelarH;
        private Clases.RoundedButton roundedButton7;
        private Label lblUser;
        private RichTextBox richTextBoxAnotacionesH;
        private ComboBox comboBoxEstatusH;
        private DateTimePicker dateTimePickerFechaH;
        private Label label22;
        private Label label23;
        private Label label24;
        private Panel panel7;
        private Label labelUserEditor;
        private Panel panel8;
        private ComboBox comboBoxTipoSigno;
        private ComboBox comboBoxSignoDistintivo;
        private Label label25;
    }
}