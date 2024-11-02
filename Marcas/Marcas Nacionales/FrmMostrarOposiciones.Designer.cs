namespace Presentacion.Marcas_Nacionales
{
    partial class FrmMostrarOposiciones
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabPageListaMarcas = new TabPage();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label1 = new Label();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            ibtnEliminar = new FontAwesome.Sharp.IconButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            textBox1 = new TextBox();
            label2 = new Label();
            roundedButton3 = new Clases.RoundedButton();
            panel4 = new Panel();
            dtgMarcasO = new DataGridView();
            roundedButton5 = new Clases.RoundedButton();
            tabPageMarcaDetail = new TabPage();
            panel1 = new Panel();
            roundedButton6 = new Clases.RoundedButton();
            btnCancelar = new Clases.RoundedButton();
            btnActualizar = new Clases.RoundedButton();
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
            checkBox1 = new CheckBox();
            roundedButton1 = new Clases.RoundedButton();
            label3 = new Label();
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
            txtSignoDistintivo = new TextBox();
            label5 = new Label();
            txtClase = new TextBox();
            label4 = new Label();
            txtNombre = new TextBox();
            label6 = new Label();
            txtExpediente = new TextBox();
            label7 = new Label();
            tabPageHistorialMarca = new TabPage();
            panel2 = new Panel();
            tabPageHistorialDetalle = new TabPage();
            label21 = new Label();
            dtgHistorialO = new DataGridView();
            dtgHistorialOp = new DataGridView();
            panel5 = new Panel();
            label22 = new Label();
            iconButton4 = new FontAwesome.Sharp.IconButton();
            iconButton5 = new FontAwesome.Sharp.IconButton();
            tabControl1.SuspendLayout();
            tabPageListaMarcas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgMarcasO).BeginInit();
            tabPageMarcaDetail.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPageHistorialMarca.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgHistorialO).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtgHistorialOp).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageListaMarcas);
            tabControl1.Controls.Add(tabPageMarcaDetail);
            tabControl1.Controls.Add(tabPageHistorialMarca);
            tabControl1.Controls.Add(tabPageHistorialDetalle);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 827);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPageListaMarcas
            // 
            tabPageListaMarcas.Controls.Add(iconPictureBox1);
            tabPageListaMarcas.Controls.Add(label1);
            tabPageListaMarcas.Controls.Add(iconButton3);
            tabPageListaMarcas.Controls.Add(ibtnEliminar);
            tabPageListaMarcas.Controls.Add(ibtnEditar);
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
            tabPageListaMarcas.Click += tabPageListaMarcas_Click;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.FromArgb(196, 195, 209);
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Flag;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 40;
            iconPictureBox1.Location = new Point(317, 16);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(40, 40);
            iconPictureBox1.TabIndex = 37;
            iconPictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(196, 195, 209);
            label1.Font = new Font("Century Gothic", 15F);
            label1.Location = new Point(363, 16);
            label1.Name = "label1";
            label1.Size = new Size(411, 31);
            label1.TabIndex = 36;
            label1.Text = "Marcas nacionales en oposición";
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
            iconButton3.Location = new Point(1011, 337);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(144, 37);
            iconButton3.TabIndex = 35;
            iconButton3.Text = "Abandonar";
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = false;
            // 
            // ibtnEliminar
            // 
            ibtnEliminar.Anchor = AnchorStyles.Top;
            ibtnEliminar.BackColor = Color.FromArgb(183, 28, 28);
            ibtnEliminar.FlatAppearance.BorderSize = 0;
            ibtnEliminar.FlatStyle = FlatStyle.Flat;
            ibtnEliminar.ForeColor = Color.White;
            ibtnEliminar.IconChar = FontAwesome.Sharp.IconChar.Trash;
            ibtnEliminar.IconColor = Color.White;
            ibtnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEliminar.IconSize = 30;
            ibtnEliminar.Location = new Point(1011, 275);
            ibtnEliminar.Name = "ibtnEliminar";
            ibtnEliminar.Size = new Size(144, 37);
            ibtnEliminar.TabIndex = 31;
            ibtnEliminar.Text = "Eliminar";
            ibtnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEliminar.UseVisualStyleBackColor = false;
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
            ibtnEditar.TabIndex = 30;
            ibtnEditar.Text = "Editar";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click;
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
            ibtnBuscar.Location = new Point(693, 133);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(144, 32);
            ibtnBuscar.TabIndex = 28;
            ibtnBuscar.Text = "Buscar";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top;
            textBox1.Location = new Point(363, 137);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(301, 26);
            textBox1.TabIndex = 27;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(236, 236, 238);
            label2.Font = new Font("Century Gothic", 12F);
            label2.Location = new Point(198, 137);
            label2.Name = "label2";
            label2.Size = new Size(146, 23);
            label2.TabIndex = 26;
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
            roundedButton3.TabIndex = 32;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top;
            panel4.BackColor = Color.FromArgb(192, 202, 212);
            panel4.Controls.Add(dtgMarcasO);
            panel4.Location = new Point(27, 213);
            panel4.Name = "panel4";
            panel4.Size = new Size(972, 542);
            panel4.TabIndex = 33;
            // 
            // dtgMarcasO
            // 
            dtgMarcasO.AllowUserToAddRows = false;
            dtgMarcasO.AllowUserToDeleteRows = false;
            dtgMarcasO.AllowUserToResizeRows = false;
            dtgMarcasO.Anchor = AnchorStyles.Top;
            dtgMarcasO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgMarcasO.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgMarcasO.BorderStyle = BorderStyle.None;
            dtgMarcasO.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgMarcasO.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgMarcasO.ColumnHeadersHeight = 40;
            dtgMarcasO.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgMarcasO.EnableHeadersVisualStyles = false;
            dtgMarcasO.GridColor = Color.LightGray;
            dtgMarcasO.Location = new Point(13, 17);
            dtgMarcasO.Name = "dtgMarcasO";
            dtgMarcasO.ReadOnly = true;
            dtgMarcasO.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgMarcasO.RowHeadersWidth = 51;
            dtgMarcasO.Size = new Size(934, 492);
            dtgMarcasO.TabIndex = 29;
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
            roundedButton5.TabIndex = 34;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // tabPageMarcaDetail
            // 
            tabPageMarcaDetail.AutoScroll = true;
            tabPageMarcaDetail.Controls.Add(panel1);
            tabPageMarcaDetail.Location = new Point(4, 29);
            tabPageMarcaDetail.Name = "tabPageMarcaDetail";
            tabPageMarcaDetail.Padding = new Padding(3);
            tabPageMarcaDetail.Size = new Size(1161, 794);
            tabPageMarcaDetail.TabIndex = 1;
            tabPageMarcaDetail.Text = "Detalle de marca";
            tabPageMarcaDetail.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(roundedButton6);
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(btnActualizar);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(roundedButton1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(textBoxEstatus);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(datePickerFechaSolicitud);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(txtNombreAgente);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(roundedButton2);
            panel1.Controls.Add(txtEntidadTitular);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(txtDireccionTitular);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(txtNombreTitular);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(roundedButton4);
            panel1.Controls.Add(iconButton2);
            panel1.Controls.Add(iconButton1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(txtSignoDistintivo);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(txtClase);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtNombre);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(txtExpediente);
            panel1.Controls.Add(label7);
            panel1.Location = new Point(52, 43);
            panel1.Name = "panel1";
            panel1.Size = new Size(1081, 1241);
            panel1.TabIndex = 0;
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
            roundedButton6.Location = new Point(767, 537);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(276, 56);
            roundedButton6.TabIndex = 107;
            roundedButton6.Text = "VER HISTORIAL";
            roundedButton6.TextColor = Color.Black;
            roundedButton6.UseVisualStyleBackColor = false;
            roundedButton6.Click += roundedButton6_Click;
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
            btnCancelar.Location = new Point(380, 1158);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(315, 62);
            btnCancelar.TabIndex = 106;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
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
            btnActualizar.Location = new Point(57, 1157);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(315, 62);
            btnActualizar.TabIndex = 105;
            btnActualizar.Text = "Actualizar";
            btnActualizar.TextColor = Color.White;
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
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
            panel3.Location = new Point(57, 959);
            panel3.Name = "panel3";
            panel3.Size = new Size(636, 185);
            panel3.TabIndex = 104;
            // 
            // dateTimePFecha_vencimiento
            // 
            dateTimePFecha_vencimiento.CalendarForeColor = Color.Red;
            dateTimePFecha_vencimiento.Enabled = false;
            dateTimePFecha_vencimiento.Format = DateTimePickerFormat.Short;
            dateTimePFecha_vencimiento.Location = new Point(425, 138);
            dateTimePFecha_vencimiento.Name = "dateTimePFecha_vencimiento";
            dateTimePFecha_vencimiento.Size = new Size(198, 28);
            dateTimePFecha_vencimiento.TabIndex = 41;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(425, 103);
            label19.Name = "label19";
            label19.Size = new Size(198, 21);
            label19.TabIndex = 40;
            label19.Text = "Fecha de vencimiento";
            // 
            // dateTimePFecha_Registro
            // 
            dateTimePFecha_Registro.Format = DateTimePickerFormat.Short;
            dateTimePFecha_Registro.Location = new Point(39, 138);
            dateTimePFecha_Registro.Name = "dateTimePFecha_Registro";
            dateTimePFecha_Registro.Size = new Size(172, 28);
            dateTimePFecha_Registro.TabIndex = 39;
            dateTimePFecha_Registro.ValueChanged += dateTimePFecha_Registro_ValueChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(39, 113);
            label18.Name = "label18";
            label18.Size = new Size(155, 21);
            label18.TabIndex = 38;
            label18.Text = "Fecha de registro";
            // 
            // txtRegistro
            // 
            txtRegistro.Location = new Point(39, 56);
            txtRegistro.Name = "txtRegistro";
            txtRegistro.Size = new Size(172, 28);
            txtRegistro.TabIndex = 39;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(37, 31);
            label17.Name = "label17";
            label17.Size = new Size(75, 21);
            label17.TabIndex = 38;
            label17.Text = "Registro";
            // 
            // txtFolio
            // 
            txtFolio.Location = new Point(245, 56);
            txtFolio.Name = "txtFolio";
            txtFolio.Size = new Size(172, 28);
            txtFolio.TabIndex = 9;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(245, 30);
            label15.Name = "label15";
            label15.Size = new Size(46, 21);
            label15.TabIndex = 8;
            label15.Text = "Folio";
            // 
            // txtLibro
            // 
            txtLibro.Location = new Point(451, 56);
            txtLibro.Name = "txtLibro";
            txtLibro.Size = new Size(172, 28);
            txtLibro.TabIndex = 11;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(451, 30);
            label20.Name = "label20";
            label20.Size = new Size(49, 21);
            label20.TabIndex = 10;
            label20.Text = "Libro";
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.None;
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Century Gothic", 10F);
            checkBox1.Location = new Point(49, 924);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(103, 25);
            checkBox1.TabIndex = 103;
            checkBox1.Text = "Registrar";
            checkBox1.UseVisualStyleBackColor = true;
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
            roundedButton1.Location = new Point(57, 256);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(280, 35);
            roundedButton1.TabIndex = 102;
            roundedButton1.Text = "+ ETAPA ACTUAL";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 19F);
            label3.Location = new Point(436, 14);
            label3.Name = "label3";
            label3.Size = new Size(132, 39);
            label3.TabIndex = 73;
            label3.Text = "Edición";
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.None;
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Enabled = false;
            richTextBox1.Font = new Font("Century Gothic", 10F);
            richTextBox1.Location = new Point(57, 791);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(638, 120);
            richTextBox1.TabIndex = 101;
            richTextBox1.Text = "";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.None;
            label16.AutoSize = true;
            label16.Font = new Font("Century Gothic", 10F);
            label16.Location = new Point(57, 765);
            label16.Name = "label16";
            label16.Size = new Size(136, 21);
            label16.TabIndex = 100;
            label16.Text = "Observaciones";
            // 
            // textBoxEstatus
            // 
            textBoxEstatus.Anchor = AnchorStyles.None;
            textBoxEstatus.Enabled = false;
            textBoxEstatus.Font = new Font("Century Gothic", 10F);
            textBoxEstatus.Location = new Point(405, 263);
            textBoxEstatus.Name = "textBoxEstatus";
            textBoxEstatus.ReadOnly = true;
            textBoxEstatus.Size = new Size(290, 28);
            textBoxEstatus.TabIndex = 99;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.None;
            label14.AutoSize = true;
            label14.Font = new Font("Century Gothic", 10F);
            label14.Location = new Point(405, 225);
            label14.Name = "label14";
            label14.Size = new Size(122, 21);
            label14.TabIndex = 98;
            label14.Text = "Etapa actual";
            // 
            // datePickerFechaSolicitud
            // 
            datePickerFechaSolicitud.Anchor = AnchorStyles.None;
            datePickerFechaSolicitud.Font = new Font("Century Gothic", 10F);
            datePickerFechaSolicitud.Format = DateTimePickerFormat.Short;
            datePickerFechaSolicitud.Location = new Point(59, 718);
            datePickerFechaSolicitud.Name = "datePickerFechaSolicitud";
            datePickerFechaSolicitud.Size = new Size(341, 28);
            datePickerFechaSolicitud.TabIndex = 97;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.None;
            label13.AutoSize = true;
            label13.Font = new Font("Century Gothic", 10F);
            label13.Location = new Point(59, 693);
            label13.Name = "label13";
            label13.Size = new Size(78, 21);
            label13.TabIndex = 96;
            label13.Text = "Solicitud";
            // 
            // txtNombreAgente
            // 
            txtNombreAgente.Enabled = false;
            txtNombreAgente.Font = new Font("Century Gothic", 10F);
            txtNombreAgente.Location = new Point(57, 642);
            txtNombreAgente.Name = "txtNombreAgente";
            txtNombreAgente.Size = new Size(280, 28);
            txtNombreAgente.TabIndex = 95;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Century Gothic", 10F);
            label12.Location = new Point(57, 616);
            label12.Name = "label12";
            label12.Size = new Size(77, 21);
            label12.TabIndex = 94;
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
            roundedButton2.Location = new Point(57, 537);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(638, 56);
            roundedButton2.TabIndex = 93;
            roundedButton2.Text = "+ AGENTE";
            roundedButton2.TextColor = Color.Black;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // txtEntidadTitular
            // 
            txtEntidadTitular.Enabled = false;
            txtEntidadTitular.Font = new Font("Century Gothic", 10F);
            txtEntidadTitular.Location = new Point(57, 491);
            txtEntidadTitular.Name = "txtEntidadTitular";
            txtEntidadTitular.Size = new Size(280, 28);
            txtEntidadTitular.TabIndex = 92;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Century Gothic", 10F);
            label11.Location = new Point(57, 465);
            label11.Name = "label11";
            label11.Size = new Size(75, 21);
            label11.TabIndex = 91;
            label11.Text = "Entidad";
            // 
            // txtDireccionTitular
            // 
            txtDireccionTitular.Enabled = false;
            txtDireccionTitular.Font = new Font("Century Gothic", 10F);
            txtDireccionTitular.Location = new Point(409, 424);
            txtDireccionTitular.Name = "txtDireccionTitular";
            txtDireccionTitular.Size = new Size(286, 28);
            txtDireccionTitular.TabIndex = 90;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 10F);
            label10.Location = new Point(409, 400);
            label10.Name = "label10";
            label10.Size = new Size(88, 21);
            label10.TabIndex = 89;
            label10.Text = "Dirección";
            // 
            // txtNombreTitular
            // 
            txtNombreTitular.Enabled = false;
            txtNombreTitular.Font = new Font("Century Gothic", 10F);
            txtNombreTitular.Location = new Point(57, 424);
            txtNombreTitular.Name = "txtNombreTitular";
            txtNombreTitular.Size = new Size(280, 28);
            txtNombreTitular.TabIndex = 88;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 10F);
            label9.Location = new Point(57, 398);
            label9.Name = "label9";
            label9.Size = new Size(77, 21);
            label9.TabIndex = 87;
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
            roundedButton4.Location = new Point(57, 323);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(638, 56);
            roundedButton4.TabIndex = 86;
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
            iconButton2.Location = new Point(907, 346);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(74, 33);
            iconButton2.TabIndex = 85;
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
            iconButton1.Location = new Point(827, 346);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(74, 33);
            iconButton1.TabIndex = 84;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(767, 100);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(276, 240);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 83;
            pictureBox1.TabStop = false;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 10F);
            label8.Location = new Point(859, 74);
            label8.Name = "label8";
            label8.Size = new Size(74, 21);
            label8.TabIndex = 82;
            label8.Text = "Imagen";
            // 
            // txtSignoDistintivo
            // 
            txtSignoDistintivo.Anchor = AnchorStyles.None;
            txtSignoDistintivo.Font = new Font("Century Gothic", 10F);
            txtSignoDistintivo.Location = new Point(403, 173);
            txtSignoDistintivo.Name = "txtSignoDistintivo";
            txtSignoDistintivo.Size = new Size(292, 28);
            txtSignoDistintivo.TabIndex = 81;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 10F);
            label5.Location = new Point(402, 149);
            label5.Name = "label5";
            label5.Size = new Size(132, 21);
            label5.TabIndex = 80;
            label5.Text = "Signo distintivo";
            // 
            // txtClase
            // 
            txtClase.Anchor = AnchorStyles.None;
            txtClase.Font = new Font("Century Gothic", 10F);
            txtClase.Location = new Point(57, 173);
            txtClase.Name = "txtClase";
            txtClase.Size = new Size(280, 28);
            txtClase.TabIndex = 79;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10F);
            label4.Location = new Point(57, 149);
            label4.Name = "label4";
            label4.Size = new Size(57, 21);
            label4.TabIndex = 78;
            label4.Text = "Clase";
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.None;
            txtNombre.Font = new Font("Century Gothic", 10F);
            txtNombre.Location = new Point(403, 100);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(292, 28);
            txtNombre.TabIndex = 77;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 10F);
            label6.Location = new Point(403, 76);
            label6.Name = "label6";
            label6.Size = new Size(77, 21);
            label6.TabIndex = 76;
            label6.Text = "Nombre";
            // 
            // txtExpediente
            // 
            txtExpediente.Anchor = AnchorStyles.None;
            txtExpediente.Font = new Font("Century Gothic", 10F);
            txtExpediente.Location = new Point(57, 100);
            txtExpediente.Name = "txtExpediente";
            txtExpediente.Size = new Size(280, 28);
            txtExpediente.TabIndex = 75;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 10F);
            label7.Location = new Point(57, 74);
            label7.Name = "label7";
            label7.Size = new Size(104, 21);
            label7.TabIndex = 74;
            label7.Text = "Expediente";
            // 
            // tabPageHistorialMarca
            // 
            tabPageHistorialMarca.Controls.Add(panel2);
            tabPageHistorialMarca.Location = new Point(4, 29);
            tabPageHistorialMarca.Name = "tabPageHistorialMarca";
            tabPageHistorialMarca.Size = new Size(1161, 794);
            tabPageHistorialMarca.TabIndex = 2;
            tabPageHistorialMarca.Text = "Historial";
            tabPageHistorialMarca.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top;
            panel2.Controls.Add(iconButton4);
            panel2.Controls.Add(iconButton5);
            panel2.Controls.Add(dtgHistorialOp);
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(label22);
            panel2.Location = new Point(8, 14);
            panel2.Name = "panel2";
            panel2.Size = new Size(1145, 772);
            panel2.TabIndex = 0;
            // 
            // tabPageHistorialDetalle
            // 
            tabPageHistorialDetalle.Location = new Point(4, 29);
            tabPageHistorialDetalle.Name = "tabPageHistorialDetalle";
            tabPageHistorialDetalle.Size = new Size(1161, 794);
            tabPageHistorialDetalle.TabIndex = 3;
            tabPageHistorialDetalle.Text = "Editar historial";
            tabPageHistorialDetalle.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Century Gothic", 19F);
            label21.Location = new Point(495, 9);
            label21.Name = "label21";
            label21.Size = new Size(134, 39);
            label21.TabIndex = 0;
            // 
            // dtgHistorialO
            // 
            dtgHistorialO.AllowUserToAddRows = false;
            dtgHistorialO.AllowUserToDeleteRows = false;
            dtgHistorialO.AllowUserToResizeRows = false;
            dtgHistorialO.Anchor = AnchorStyles.Top;
            dtgHistorialO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgHistorialO.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgHistorialO.BorderStyle = BorderStyle.None;
            dtgHistorialO.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgHistorialO.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtgHistorialO.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtgHistorialO.ColumnHeadersHeight = 40;
            dtgHistorialO.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dtgHistorialO.DefaultCellStyle = dataGridViewCellStyle5;
            dtgHistorialO.EnableHeadersVisualStyles = false;
            dtgHistorialO.GridColor = Color.LightGray;
            dtgHistorialO.Location = new Point(94, 68);
            dtgHistorialO.Name = "dtgHistorialO";
            dtgHistorialO.ReadOnly = true;
            dtgHistorialO.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dtgHistorialO.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dtgHistorialO.RowHeadersWidth = 51;
            dtgHistorialO.Size = new Size(934, 492);
            dtgHistorialO.TabIndex = 43;
            // 
            // dtgHistorialOp
            // 
            dtgHistorialOp.AllowUserToAddRows = false;
            dtgHistorialOp.AllowUserToDeleteRows = false;
            dtgHistorialOp.AllowUserToResizeRows = false;
            dtgHistorialOp.Anchor = AnchorStyles.Top;
            dtgHistorialOp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgHistorialOp.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgHistorialOp.BorderStyle = BorderStyle.None;
            dtgHistorialOp.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgHistorialOp.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgHistorialOp.ColumnHeadersHeight = 40;
            dtgHistorialOp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgHistorialOp.EnableHeadersVisualStyles = false;
            dtgHistorialOp.GridColor = Color.LightGray;
            dtgHistorialOp.Location = new Point(25, 94);
            dtgHistorialOp.Name = "dtgHistorialOp";
            dtgHistorialOp.ReadOnly = true;
            dtgHistorialOp.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgHistorialOp.RowHeadersWidth = 51;
            dtgHistorialOp.Size = new Size(934, 492);
            dtgHistorialOp.TabIndex = 45;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top;
            panel5.BackColor = Color.FromArgb(192, 202, 212);
            panel5.Location = new Point(11, 73);
            panel5.Name = "panel5";
            panel5.Size = new Size(972, 542);
            panel5.TabIndex = 46;
            // 
            // label22
            // 
            label22.Anchor = AnchorStyles.None;
            label22.AutoSize = true;
            label22.Font = new Font("Century Gothic", 19F);
            label22.Location = new Point(492, 0);
            label22.Name = "label22";
            label22.Size = new Size(134, 39);
            label22.TabIndex = 44;
            label22.Text = "Historial";
            // 
            // iconButton4
            // 
            iconButton4.Anchor = AnchorStyles.Top;
            iconButton4.BackColor = Color.FromArgb(183, 28, 28);
            iconButton4.FlatAppearance.BorderSize = 0;
            iconButton4.FlatStyle = FlatStyle.Flat;
            iconButton4.ForeColor = Color.White;
            iconButton4.IconChar = FontAwesome.Sharp.IconChar.Trash;
            iconButton4.IconColor = Color.White;
            iconButton4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton4.IconSize = 30;
            iconButton4.Location = new Point(998, 135);
            iconButton4.Name = "iconButton4";
            iconButton4.Size = new Size(144, 37);
            iconButton4.TabIndex = 48;
            iconButton4.Text = "Eliminar";
            iconButton4.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton4.UseVisualStyleBackColor = false;
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
            iconButton5.Location = new Point(998, 73);
            iconButton5.Name = "iconButton5";
            iconButton5.Size = new Size(144, 37);
            iconButton5.TabIndex = 47;
            iconButton5.Text = "Editar";
            iconButton5.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton5.UseVisualStyleBackColor = false;
            // 
            // FrmMostrarOposiciones
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmMostrarOposiciones";
            Text = "FrmMostrarOposiciones";
            Load += FrmMostrarOposiciones_Load;
            tabControl1.ResumeLayout(false);
            tabPageListaMarcas.ResumeLayout(false);
            tabPageListaMarcas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgMarcasO).EndInit();
            tabPageMarcaDetail.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPageHistorialMarca.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgHistorialO).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtgHistorialOp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPageListaMarcas;
        private TabPage tabPageMarcaDetail;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton ibtnEliminar;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private DataGridView dtgMarcasO;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private TextBox textBox1;
        private Label label2;
        private Clases.RoundedButton roundedButton3;
        private Panel panel4;
        private Clases.RoundedButton roundedButton5;
        private Label label1;
        private Panel panel1;
        private Clases.RoundedButton roundedButton1;
        private Label label3;
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
        private TextBox txtSignoDistintivo;
        private Label label5;
        private TextBox txtClase;
        private Label label4;
        private TextBox txtNombre;
        private Label label6;
        private TextBox txtExpediente;
        private Label label7;
        private CheckBox checkBox1;
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
        private Clases.RoundedButton btnCancelar;
        private Clases.RoundedButton btnActualizar;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private TabPage tabPageHistorialDetalle;
        //private TabPage tabPageHistorialDetail;
        private Clases.RoundedButton roundedButton6;
        private TabPage tabPageHistorialMarca;
        private Panel panel2;
        private Label label21;
        private DataGridView dtgHistorialO;
        private DataGridView dtgHistorialOp;
        private Panel panel5;
        private Label label22;
        private FontAwesome.Sharp.IconButton iconButton4;
        private FontAwesome.Sharp.IconButton iconButton5;
    }
}