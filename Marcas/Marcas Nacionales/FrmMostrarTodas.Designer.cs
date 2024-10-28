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
            panel1 = new Panel();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabPageListaMarcas = new TabPage();
            ibtnEliminar = new FontAwesome.Sharp.IconButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            dtgMarcasN = new DataGridView();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            textBox1 = new TextBox();
            label2 = new Label();
            tabPageMarcaDetail = new TabPage();
            panel2 = new Panel();
            checkBox1 = new CheckBox();
            btnGuardar = new Clases.RoundedButton();
            richTextBox1 = new RichTextBox();
            label16 = new Label();
            cmbEstado = new ComboBox();
            label15 = new Label();
            textBox11 = new TextBox();
            label14 = new Label();
            datePickerFechaSolicitud = new DateTimePicker();
            label13 = new Label();
            panel3 = new Panel();
            label21 = new Label();
            dateTimePFecha_vencimiento = new DateTimePicker();
            label19 = new Label();
            dateTimePFecha_Registro = new DateTimePicker();
            label18 = new Label();
            txtRegistro = new TextBox();
            label17 = new Label();
            txtFolio = new TextBox();
            label7 = new Label();
            txtLibro = new TextBox();
            label20 = new Label();
            txtNombreAgente = new TextBox();
            label12 = new Label();
            roundedButton2 = new Clases.RoundedButton();
            txtEntidadTitular = new TextBox();
            label11 = new Label();
            txtDireccionTitular = new TextBox();
            label10 = new Label();
            txtNombreTitular = new TextBox();
            label9 = new Label();
            roundedButton1 = new Clases.RoundedButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            pictureBox1 = new PictureBox();
            label8 = new Label();
            txtSignoDistintivo = new TextBox();
            label5 = new Label();
            txtClase = new TextBox();
            label4 = new Label();
            txtNombre = new TextBox();
            label3 = new Label();
            txtExpediente = new TextBox();
            label6 = new Label();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageListaMarcas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgMarcasN).BeginInit();
            tabPageMarcaDetail.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1169, 51);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 19F);
            label1.Location = new Point(398, 9);
            label1.Name = "label1";
            label1.Size = new Size(314, 39);
            label1.TabIndex = 0;
            label1.Text = "Marcas nacionales";
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            tabControl1.Controls.Add(tabPageListaMarcas);
            tabControl1.Controls.Add(tabPageMarcaDetail);
            tabControl1.Location = new Point(0, 51);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 625);
            tabControl1.TabIndex = 1;
            // 
            // tabPageListaMarcas
            // 
            tabPageListaMarcas.Controls.Add(ibtnEliminar);
            tabPageListaMarcas.Controls.Add(ibtnEditar);
            tabPageListaMarcas.Controls.Add(dtgMarcasN);
            tabPageListaMarcas.Controls.Add(ibtnBuscar);
            tabPageListaMarcas.Controls.Add(textBox1);
            tabPageListaMarcas.Controls.Add(label2);
            tabPageListaMarcas.Location = new Point(4, 29);
            tabPageListaMarcas.Name = "tabPageListaMarcas";
            tabPageListaMarcas.Padding = new Padding(3);
            tabPageListaMarcas.Size = new Size(1161, 592);
            tabPageListaMarcas.TabIndex = 0;
            tabPageListaMarcas.Text = "Lista de marcas";
            tabPageListaMarcas.UseVisualStyleBackColor = true;
            // 
            // ibtnEliminar
            // 
            ibtnEliminar.Anchor = AnchorStyles.Right;
            ibtnEliminar.BackColor = Color.FromArgb(244, 98, 96);
            ibtnEliminar.FlatAppearance.BorderSize = 0;
            ibtnEliminar.FlatStyle = FlatStyle.Flat;
            ibtnEliminar.ForeColor = Color.White;
            ibtnEliminar.IconChar = FontAwesome.Sharp.IconChar.Trash;
            ibtnEliminar.IconColor = Color.White;
            ibtnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEliminar.IconSize = 30;
            ibtnEliminar.Location = new Point(1000, 350);
            ibtnEliminar.Name = "ibtnEliminar";
            ibtnEliminar.Size = new Size(144, 37);
            ibtnEliminar.TabIndex = 21;
            ibtnEliminar.Text = "Eliminar";
            ibtnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEliminar.UseVisualStyleBackColor = false;
            // 
            // ibtnEditar
            // 
            ibtnEditar.Anchor = AnchorStyles.Right;
            ibtnEditar.BackColor = Color.FromArgb(96, 149, 241);
            ibtnEditar.FlatAppearance.BorderSize = 0;
            ibtnEditar.FlatStyle = FlatStyle.Flat;
            ibtnEditar.ForeColor = Color.White;
            ibtnEditar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            ibtnEditar.IconColor = Color.White;
            ibtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEditar.IconSize = 30;
            ibtnEditar.Location = new Point(1000, 295);
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
            dtgMarcasN.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtgMarcasN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgMarcasN.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgMarcasN.BorderStyle = BorderStyle.None;
            dtgMarcasN.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgMarcasN.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgMarcasN.ColumnHeadersHeight = 40;
            dtgMarcasN.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgMarcasN.EnableHeadersVisualStyles = false;
            dtgMarcasN.GridColor = Color.LightGray;
            dtgMarcasN.Location = new Point(48, 128);
            dtgMarcasN.Name = "dtgMarcasN";
            dtgMarcasN.ReadOnly = true;
            dtgMarcasN.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgMarcasN.RowHeadersWidth = 51;
            dtgMarcasN.Size = new Size(934, 424);
            dtgMarcasN.TabIndex = 17;
            // 
            // ibtnBuscar
            // 
            ibtnBuscar.Anchor = AnchorStyles.Top;
            ibtnBuscar.BackColor = Color.FromArgb(255, 164, 0);
            ibtnBuscar.FlatAppearance.BorderSize = 0;
            ibtnBuscar.FlatStyle = FlatStyle.Flat;
            ibtnBuscar.Font = new Font("Century Gothic", 10F);
            ibtnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlassPlus;
            ibtnBuscar.IconColor = Color.Black;
            ibtnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnBuscar.IconSize = 30;
            ibtnBuscar.Location = new Point(838, 58);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(144, 32);
            ibtnBuscar.TabIndex = 16;
            ibtnBuscar.Text = "Buscar";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(48, 62);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(767, 26);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F);
            label2.Location = new Point(39, 20);
            label2.Name = "label2";
            label2.Size = new Size(146, 23);
            label2.TabIndex = 0;
            label2.Text = "Buscar marca";
            // 
            // tabPageMarcaDetail
            // 
            tabPageMarcaDetail.AutoScroll = true;
            tabPageMarcaDetail.Controls.Add(panel2);
            tabPageMarcaDetail.Location = new Point(4, 29);
            tabPageMarcaDetail.Name = "tabPageMarcaDetail";
            tabPageMarcaDetail.Padding = new Padding(3);
            tabPageMarcaDetail.Size = new Size(1161, 592);
            tabPageMarcaDetail.TabIndex = 1;
            tabPageMarcaDetail.Text = "Detalle de marca";
            tabPageMarcaDetail.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(checkBox1);
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(richTextBox1);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(cmbEstado);
            panel2.Controls.Add(label15);
            panel2.Controls.Add(textBox11);
            panel2.Controls.Add(label14);
            panel2.Controls.Add(datePickerFechaSolicitud);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(txtNombreAgente);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(roundedButton2);
            panel2.Controls.Add(txtEntidadTitular);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(txtDireccionTitular);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(txtNombreTitular);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(roundedButton1);
            panel2.Controls.Add(iconButton2);
            panel2.Controls.Add(iconButton1);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(txtSignoDistintivo);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(txtClase);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txtNombre);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtExpediente);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(32, 14);
            panel2.Name = "panel2";
            panel2.Size = new Size(1081, 1448);
            panel2.TabIndex = 0;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.None;
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Century Gothic", 10F);
            checkBox1.Location = new Point(35, 1056);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(103, 25);
            checkBox1.TabIndex = 49;
            checkBox1.Text = "Registrar";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.None;
            btnGuardar.BackColor = Color.FromArgb(1, 87, 155);
            btnGuardar.BackgroundColor = Color.FromArgb(1, 87, 155);
            btnGuardar.BorderColor = Color.FromArgb(1, 87, 155);
            btnGuardar.BorderRadius = 60;
            btnGuardar.BorderSize = 0;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Century Gothic", 10F);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(297, 1280);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(315, 52);
            btnGuardar.TabIndex = 47;
            btnGuardar.Text = "Actualizar";
            btnGuardar.TextColor = Color.White;
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.None;
            richTextBox1.Font = new Font("Century Gothic", 10F);
            richTextBox1.Location = new Point(35, 926);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(791, 120);
            richTextBox1.TabIndex = 46;
            richTextBox1.Text = "";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.None;
            label16.AutoSize = true;
            label16.Font = new Font("Century Gothic", 10F);
            label16.Location = new Point(35, 900);
            label16.Name = "label16";
            label16.Size = new Size(136, 21);
            label16.TabIndex = 45;
            label16.Text = "Observaciones";
            // 
            // cmbEstado
            // 
            cmbEstado.Anchor = AnchorStyles.None;
            cmbEstado.Font = new Font("Century Gothic", 10F);
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Items.AddRange(new object[] { "Examen de forma", "Examen de fondo", "Requerimiento", "Objeción", "Edicto", "Publicación", "Oposición", "Orden de pago", "Abandono", "Registrada", "Licencia de uso" });
            cmbEstado.Location = new Point(347, 873);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(479, 29);
            cmbEstado.TabIndex = 44;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.None;
            label15.AutoSize = true;
            label15.Font = new Font("Century Gothic", 10F);
            label15.Location = new Point(347, 848);
            label15.Name = "label15";
            label15.Size = new Size(68, 21);
            label15.TabIndex = 43;
            label15.Text = "Estado";
            // 
            // textBox11
            // 
            textBox11.Anchor = AnchorStyles.None;
            textBox11.Font = new Font("Century Gothic", 10F);
            textBox11.Location = new Point(345, 798);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(481, 28);
            textBox11.TabIndex = 42;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.None;
            label14.AutoSize = true;
            label14.Font = new Font("Century Gothic", 10F);
            label14.Location = new Point(343, 773);
            label14.Name = "label14";
            label14.Size = new Size(122, 21);
            label14.TabIndex = 41;
            label14.Text = "Etapa actual";
            // 
            // datePickerFechaSolicitud
            // 
            datePickerFechaSolicitud.Anchor = AnchorStyles.None;
            datePickerFechaSolicitud.Font = new Font("Century Gothic", 10F);
            datePickerFechaSolicitud.Format = DateTimePickerFormat.Short;
            datePickerFechaSolicitud.Location = new Point(35, 797);
            datePickerFechaSolicitud.Name = "datePickerFechaSolicitud";
            datePickerFechaSolicitud.Size = new Size(250, 28);
            datePickerFechaSolicitud.TabIndex = 40;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.None;
            label13.AutoSize = true;
            label13.Font = new Font("Century Gothic", 10F);
            label13.Location = new Point(35, 772);
            label13.Name = "label13";
            label13.Size = new Size(78, 21);
            label13.TabIndex = 39;
            label13.Text = "Solicitud";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.Controls.Add(label21);
            panel3.Controls.Add(dateTimePFecha_vencimiento);
            panel3.Controls.Add(label19);
            panel3.Controls.Add(dateTimePFecha_Registro);
            panel3.Controls.Add(label18);
            panel3.Controls.Add(txtRegistro);
            panel3.Controls.Add(label17);
            panel3.Controls.Add(txtFolio);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(txtLibro);
            panel3.Controls.Add(label20);
            panel3.Font = new Font("Century Gothic", 10F);
            panel3.Location = new Point(35, 1088);
            panel3.Name = "panel3";
            panel3.Size = new Size(986, 185);
            panel3.TabIndex = 48;
            // 
            // label21
            // 
            label21.Anchor = AnchorStyles.None;
            label21.AutoSize = true;
            label21.Location = new Point(644, 20);
            label21.Name = "label21";
            label21.Size = new Size(49, 21);
            label21.TabIndex = 42;
            label21.Text = "Libro";
            // 
            // dateTimePFecha_vencimiento
            // 
            dateTimePFecha_vencimiento.Anchor = AnchorStyles.None;
            dateTimePFecha_vencimiento.Enabled = false;
            dateTimePFecha_vencimiento.Format = DateTimePickerFormat.Short;
            dateTimePFecha_vencimiento.Location = new Point(317, 135);
            dateTimePFecha_vencimiento.Name = "dateTimePFecha_vencimiento";
            dateTimePFecha_vencimiento.Size = new Size(280, 28);
            dateTimePFecha_vencimiento.TabIndex = 41;
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.None;
            label19.AutoSize = true;
            label19.Location = new Point(316, 109);
            label19.Name = "label19";
            label19.Size = new Size(198, 21);
            label19.TabIndex = 40;
            label19.Text = "Fecha de vencimiento";
            // 
            // dateTimePFecha_Registro
            // 
            dateTimePFecha_Registro.Anchor = AnchorStyles.None;
            dateTimePFecha_Registro.Format = DateTimePickerFormat.Short;
            dateTimePFecha_Registro.Location = new Point(2, 134);
            dateTimePFecha_Registro.Name = "dateTimePFecha_Registro";
            dateTimePFecha_Registro.Size = new Size(280, 28);
            dateTimePFecha_Registro.TabIndex = 39;
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.None;
            label18.AutoSize = true;
            label18.Location = new Point(2, 109);
            label18.Name = "label18";
            label18.Size = new Size(155, 21);
            label18.TabIndex = 38;
            label18.Text = "Fecha de registro";
            // 
            // txtRegistro
            // 
            txtRegistro.Anchor = AnchorStyles.None;
            txtRegistro.Location = new Point(2, 46);
            txtRegistro.Name = "txtRegistro";
            txtRegistro.Size = new Size(278, 28);
            txtRegistro.TabIndex = 39;
            // 
            // label17
            // 
            label17.Anchor = AnchorStyles.None;
            label17.AutoSize = true;
            label17.Location = new Point(0, 21);
            label17.Name = "label17";
            label17.Size = new Size(75, 21);
            label17.TabIndex = 38;
            label17.Text = "Registro";
            // 
            // txtFolio
            // 
            txtFolio.Anchor = AnchorStyles.None;
            txtFolio.Location = new Point(316, 46);
            txtFolio.Name = "txtFolio";
            txtFolio.Size = new Size(280, 28);
            txtFolio.TabIndex = 9;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Location = new Point(316, 20);
            label7.Name = "label7";
            label7.Size = new Size(46, 21);
            label7.TabIndex = 8;
            label7.Text = "Folio";
            // 
            // txtLibro
            // 
            txtLibro.Anchor = AnchorStyles.None;
            txtLibro.Location = new Point(644, 46);
            txtLibro.Name = "txtLibro";
            txtLibro.Size = new Size(280, 28);
            txtLibro.TabIndex = 11;
            // 
            // label20
            // 
            label20.Anchor = AnchorStyles.None;
            label20.AutoSize = true;
            label20.Location = new Point(1070, 65);
            label20.Name = "label20";
            label20.Size = new Size(49, 21);
            label20.TabIndex = 10;
            label20.Text = "Libro";
            // 
            // txtNombreAgente
            // 
            txtNombreAgente.Anchor = AnchorStyles.None;
            txtNombreAgente.Enabled = false;
            txtNombreAgente.Font = new Font("Century Gothic", 10F);
            txtNombreAgente.Location = new Point(35, 723);
            txtNombreAgente.Name = "txtNombreAgente";
            txtNombreAgente.Size = new Size(791, 28);
            txtNombreAgente.TabIndex = 37;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.None;
            label12.AutoSize = true;
            label12.Font = new Font("Century Gothic", 10F);
            label12.Location = new Point(35, 697);
            label12.Name = "label12";
            label12.Size = new Size(77, 21);
            label12.TabIndex = 36;
            label12.Text = "Nombre";
            // 
            // roundedButton2
            // 
            roundedButton2.Anchor = AnchorStyles.None;
            roundedButton2.BackColor = Color.LightSteelBlue;
            roundedButton2.BackgroundColor = Color.LightSteelBlue;
            roundedButton2.BorderColor = Color.LightSteelBlue;
            roundedButton2.BorderRadius = 40;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Century Gothic", 10F);
            roundedButton2.ForeColor = Color.Black;
            roundedButton2.Location = new Point(35, 636);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(791, 51);
            roundedButton2.TabIndex = 35;
            roundedButton2.Text = "+ AGENTE";
            roundedButton2.TextColor = Color.Black;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // txtEntidadTitular
            // 
            txtEntidadTitular.Anchor = AnchorStyles.None;
            txtEntidadTitular.Enabled = false;
            txtEntidadTitular.Font = new Font("Century Gothic", 10F);
            txtEntidadTitular.Location = new Point(35, 591);
            txtEntidadTitular.Name = "txtEntidadTitular";
            txtEntidadTitular.Size = new Size(280, 28);
            txtEntidadTitular.TabIndex = 34;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.None;
            label11.AutoSize = true;
            label11.Font = new Font("Century Gothic", 10F);
            label11.Location = new Point(35, 565);
            label11.Name = "label11";
            label11.Size = new Size(75, 21);
            label11.TabIndex = 33;
            label11.Text = "Entidad";
            // 
            // txtDireccionTitular
            // 
            txtDireccionTitular.Anchor = AnchorStyles.None;
            txtDireccionTitular.Enabled = false;
            txtDireccionTitular.Font = new Font("Century Gothic", 10F);
            txtDireccionTitular.Location = new Point(35, 520);
            txtDireccionTitular.Name = "txtDireccionTitular";
            txtDireccionTitular.Size = new Size(791, 28);
            txtDireccionTitular.TabIndex = 32;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.None;
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 10F);
            label10.Location = new Point(35, 494);
            label10.Name = "label10";
            label10.Size = new Size(88, 21);
            label10.TabIndex = 31;
            label10.Text = "Dirección";
            // 
            // txtNombreTitular
            // 
            txtNombreTitular.Anchor = AnchorStyles.None;
            txtNombreTitular.Enabled = false;
            txtNombreTitular.Font = new Font("Century Gothic", 10F);
            txtNombreTitular.Location = new Point(35, 450);
            txtNombreTitular.Name = "txtNombreTitular";
            txtNombreTitular.Size = new Size(791, 28);
            txtNombreTitular.TabIndex = 30;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.None;
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 10F);
            label9.Location = new Point(35, 424);
            label9.Name = "label9";
            label9.Size = new Size(77, 21);
            label9.TabIndex = 29;
            label9.Text = "Nombre";
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.None;
            roundedButton1.BackColor = Color.LightSteelBlue;
            roundedButton1.BackgroundColor = Color.LightSteelBlue;
            roundedButton1.BorderColor = Color.LightSteelBlue;
            roundedButton1.BorderRadius = 40;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Century Gothic", 10F);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(35, 360);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(791, 51);
            roundedButton1.TabIndex = 28;
            roundedButton1.Text = "+ TITULAR";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
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
            iconButton2.Location = new Point(885, 309);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(74, 33);
            iconButton2.TabIndex = 27;
            iconButton2.UseVisualStyleBackColor = false;
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
            iconButton1.Location = new Point(805, 309);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(74, 33);
            iconButton1.TabIndex = 26;
            iconButton1.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(745, 63);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(276, 240);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 25;
            pictureBox1.TabStop = false;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 10F);
            label8.Location = new Point(842, 37);
            label8.Name = "label8";
            label8.Size = new Size(74, 21);
            label8.TabIndex = 24;
            label8.Text = "Imagen";
            // 
            // txtSignoDistintivo
            // 
            txtSignoDistintivo.Anchor = AnchorStyles.None;
            txtSignoDistintivo.Font = new Font("Century Gothic", 10F);
            txtSignoDistintivo.Location = new Point(381, 213);
            txtSignoDistintivo.Name = "txtSignoDistintivo";
            txtSignoDistintivo.Size = new Size(280, 28);
            txtSignoDistintivo.TabIndex = 23;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 10F);
            label5.Location = new Point(381, 187);
            label5.Name = "label5";
            label5.Size = new Size(132, 21);
            label5.TabIndex = 22;
            label5.Text = "Signo distintivo";
            // 
            // txtClase
            // 
            txtClase.Anchor = AnchorStyles.None;
            txtClase.Font = new Font("Century Gothic", 10F);
            txtClase.Location = new Point(35, 213);
            txtClase.Name = "txtClase";
            txtClase.Size = new Size(280, 28);
            txtClase.TabIndex = 21;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10F);
            label4.Location = new Point(35, 187);
            label4.Name = "label4";
            label4.Size = new Size(57, 21);
            label4.TabIndex = 20;
            label4.Text = "Clase";
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.None;
            txtNombre.Font = new Font("Century Gothic", 10F);
            txtNombre.Location = new Point(35, 139);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(626, 28);
            txtNombre.TabIndex = 19;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 10F);
            label3.Location = new Point(35, 113);
            label3.Name = "label3";
            label3.Size = new Size(77, 21);
            label3.TabIndex = 18;
            label3.Text = "Nombre";
            // 
            // txtExpediente
            // 
            txtExpediente.Anchor = AnchorStyles.None;
            txtExpediente.Font = new Font("Century Gothic", 10F);
            txtExpediente.Location = new Point(35, 63);
            txtExpediente.Name = "txtExpediente";
            txtExpediente.Size = new Size(280, 28);
            txtExpediente.TabIndex = 17;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 10F);
            label6.Location = new Point(35, 37);
            label6.Name = "label6";
            label6.Size = new Size(104, 21);
            label6.TabIndex = 16;
            label6.Text = "Expediente";
            // 
            // FrmMostrarTodas
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            ClientSize = new Size(1169, 676);
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmMostrarTodas";
            Text = "FrmMostrarTodas";
            Load += FrmMostrarTodas_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPageListaMarcas.ResumeLayout(false);
            tabPageListaMarcas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgMarcasN).EndInit();
            tabPageMarcaDetail.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabPageListaMarcas;
        private TabPage tabPageMarcaDetail;
        private Label label2;
        private TextBox textBox1;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private DataGridView dtgMarcasN;
        private FontAwesome.Sharp.IconButton ibtnEliminar;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private Panel panel2;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private PictureBox pictureBox1;
        private Label label8;
        private TextBox txtSignoDistintivo;
        private Label label5;
        private TextBox txtClase;
        private Label label4;
        private TextBox txtNombre;
        private Label label3;
        private TextBox txtExpediente;
        private Label label6;
        private TextBox txtNombreAgente;
        private Label label12;
        private Clases.RoundedButton roundedButton2;
        private TextBox txtEntidadTitular;
        private Label label11;
        private TextBox txtDireccionTitular;
        private Label label10;
        private TextBox txtNombreTitular;
        private Label label9;
        private Clases.RoundedButton roundedButton1;
        private CheckBox checkBox1;
        private Clases.RoundedButton btnGuardar;
        private RichTextBox richTextBox1;
        private Label label16;
        private ComboBox cmbEstado;
        private Label label15;
        private TextBox textBox11;
        private Label label14;
        private DateTimePicker datePickerFechaSolicitud;
        private Label label13;
        private Panel panel3;
        private DateTimePicker dateTimePFecha_vencimiento;
        private Label label19;
        private DateTimePicker dateTimePFecha_Registro;
        private Label label18;
        private TextBox txtRegistro;
        private Label label17;
        private TextBox txtFolio;
        private Label label7;
        private TextBox txtLibro;
        private Label label20;
        private Label label21;
    }
}