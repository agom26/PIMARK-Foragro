namespace Presentacion.Personas
{
    partial class FrmAdministrarAgentes
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabPageListado = new TabPage();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            dtgAgentes = new DataGridView();
            tabPageAgenteDetail = new TabPage();
            txtNombreContacto = new TextBox();
            label9 = new Label();
            txtTelefonoContacto = new TextBox();
            label8 = new Label();
            btnCancelarTit = new FontAwesome.Sharp.IconButton();
            btnGuardarTit = new FontAwesome.Sharp.IconButton();
            txtCorreoContacto = new TextBox();
            label7 = new Label();
            txtDireccionAgente = new TextBox();
            label6 = new Label();
            txtPais = new TextBox();
            label5 = new Label();
            txtNitAgente = new TextBox();
            label4 = new Label();
            txtNombreAgente = new TextBox();
            label3 = new Label();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            label2 = new Label();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            textBox1 = new TextBox();
            label1 = new Label();
            roundedButton5 = new Clases.RoundedButton();
            roundedButton3 = new Clases.RoundedButton();
            panel1 = new Panel();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label10 = new Label();
            roundedButton1 = new Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabPageListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).BeginInit();
            tabPageAgenteDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageListado);
            tabControl1.Controls.Add(tabPageAgenteDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 12F);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 827);
            tabControl1.TabIndex = 1;
            // 
            // tabPageListado
            // 
            tabPageListado.Controls.Add(iconPictureBox2);
            tabPageListado.Controls.Add(label2);
            tabPageListado.Controls.Add(iconButton1);
            tabPageListado.Controls.Add(textBox1);
            tabPageListado.Controls.Add(label1);
            tabPageListado.Controls.Add(roundedButton5);
            tabPageListado.Controls.Add(roundedButton3);
            tabPageListado.Controls.Add(ibtnEditar);
            tabPageListado.Controls.Add(ibtnAgregar);
            tabPageListado.Controls.Add(panel1);
            tabPageListado.Location = new Point(4, 32);
            tabPageListado.Name = "tabPageListado";
            tabPageListado.Padding = new Padding(3);
            tabPageListado.Size = new Size(1161, 791);
            tabPageListado.TabIndex = 0;
            tabPageListado.Text = "Listado de agentes";
            tabPageListado.UseVisualStyleBackColor = true;
            tabPageListado.Click += tabPageListado_Click;
            // 
            // ibtnEditar
            // 
            ibtnEditar.Anchor = AnchorStyles.Top;
            ibtnEditar.BackColor = Color.FromArgb(96, 149, 241);
            ibtnEditar.FlatAppearance.BorderSize = 0;
            ibtnEditar.FlatStyle = FlatStyle.Flat;
            ibtnEditar.Font = new Font("Century Gothic", 9F);
            ibtnEditar.ForeColor = Color.White;
            ibtnEditar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            ibtnEditar.IconColor = Color.White;
            ibtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEditar.IconSize = 25;
            ibtnEditar.Location = new Point(1011, 263);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(144, 37);
            ibtnEditar.TabIndex = 17;
            ibtnEditar.Text = "Editar";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click;
            // 
            // ibtnAgregar
            // 
            ibtnAgregar.Anchor = AnchorStyles.Top;
            ibtnAgregar.BackColor = Color.FromArgb(50, 164, 115);
            ibtnAgregar.FlatAppearance.BorderSize = 0;
            ibtnAgregar.FlatStyle = FlatStyle.Flat;
            ibtnAgregar.Font = new Font("Century Gothic", 9F);
            ibtnAgregar.ForeColor = Color.White;
            ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            ibtnAgregar.IconColor = Color.White;
            ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnAgregar.IconSize = 25;
            ibtnAgregar.Location = new Point(1011, 205);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(144, 37);
            ibtnAgregar.TabIndex = 16;
            ibtnAgregar.Text = "Agregar";
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnAgregar_Click;
            // 
            // dtgAgentes
            // 
            dtgAgentes.AllowUserToAddRows = false;
            dtgAgentes.AllowUserToDeleteRows = false;
            dtgAgentes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgAgentes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dtgAgentes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtgAgentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgAgentes.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgAgentes.BorderStyle = BorderStyle.None;
            dtgAgentes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgAgentes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dtgAgentes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dtgAgentes.ColumnHeadersHeight = 40;
            dtgAgentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dtgAgentes.DefaultCellStyle = dataGridViewCellStyle3;
            dtgAgentes.EnableHeadersVisualStyles = false;
            dtgAgentes.GridColor = Color.LightGray;
            dtgAgentes.Location = new Point(20, 21);
            dtgAgentes.Name = "dtgAgentes";
            dtgAgentes.ReadOnly = true;
            dtgAgentes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtgAgentes.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtgAgentes.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtgAgentes.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dtgAgentes.Size = new Size(904, 495);
            dtgAgentes.TabIndex = 14;
            dtgAgentes.CellClick += dtgAgentes_CellClick;
            dtgAgentes.CellContentClick += dtgAgentes_CellContentClick;
            // 
            // tabPageAgenteDetail
            // 
            tabPageAgenteDetail.Controls.Add(iconPictureBox1);
            tabPageAgenteDetail.Controls.Add(label10);
            tabPageAgenteDetail.Controls.Add(txtNombreContacto);
            tabPageAgenteDetail.Controls.Add(label9);
            tabPageAgenteDetail.Controls.Add(txtTelefonoContacto);
            tabPageAgenteDetail.Controls.Add(label8);
            tabPageAgenteDetail.Controls.Add(btnCancelarTit);
            tabPageAgenteDetail.Controls.Add(btnGuardarTit);
            tabPageAgenteDetail.Controls.Add(txtCorreoContacto);
            tabPageAgenteDetail.Controls.Add(label7);
            tabPageAgenteDetail.Controls.Add(txtDireccionAgente);
            tabPageAgenteDetail.Controls.Add(label6);
            tabPageAgenteDetail.Controls.Add(txtPais);
            tabPageAgenteDetail.Controls.Add(label5);
            tabPageAgenteDetail.Controls.Add(txtNitAgente);
            tabPageAgenteDetail.Controls.Add(label4);
            tabPageAgenteDetail.Controls.Add(txtNombreAgente);
            tabPageAgenteDetail.Controls.Add(label3);
            tabPageAgenteDetail.Controls.Add(roundedButton1);
            tabPageAgenteDetail.Location = new Point(4, 32);
            tabPageAgenteDetail.Name = "tabPageAgenteDetail";
            tabPageAgenteDetail.Padding = new Padding(3);
            tabPageAgenteDetail.Size = new Size(1161, 791);
            tabPageAgenteDetail.TabIndex = 1;
            tabPageAgenteDetail.Text = "Detalle de agente";
            tabPageAgenteDetail.UseVisualStyleBackColor = true;
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Anchor = AnchorStyles.Top;
            txtNombreContacto.Font = new Font("Century Gothic", 9F);
            txtNombreContacto.Location = new Point(159, 406);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(389, 26);
            txtNombreContacto.TabIndex = 7;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 9F);
            label9.Location = new Point(159, 380);
            label9.Name = "label9";
            label9.Size = new Size(80, 20);
            label9.TabIndex = 47;
            label9.Text = "Contacto";
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Anchor = AnchorStyles.Top;
            txtTelefonoContacto.Font = new Font("Century Gothic", 9F);
            txtTelefonoContacto.Location = new Point(596, 331);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(389, 26);
            txtTelefonoContacto.TabIndex = 6;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 9F);
            label8.Location = new Point(596, 305);
            label8.Name = "label8";
            label8.Size = new Size(71, 20);
            label8.TabIndex = 45;
            label8.Text = "Teléfono";
            // 
            // btnCancelarTit
            // 
            btnCancelarTit.Anchor = AnchorStyles.Top;
            btnCancelarTit.BackColor = Color.Gainsboro;
            btnCancelarTit.FlatAppearance.BorderSize = 0;
            btnCancelarTit.FlatStyle = FlatStyle.Flat;
            btnCancelarTit.ForeColor = Color.Black;
            btnCancelarTit.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarTit.IconColor = Color.Black;
            btnCancelarTit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarTit.IconSize = 30;
            btnCancelarTit.Location = new Point(606, 473);
            btnCancelarTit.Name = "btnCancelarTit";
            btnCancelarTit.Size = new Size(389, 37);
            btnCancelarTit.TabIndex = 9;
            btnCancelarTit.Text = "Cancelar";
            btnCancelarTit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelarTit.UseVisualStyleBackColor = false;
            btnCancelarTit.Click += btnCancelarTit_Click;
            // 
            // btnGuardarTit
            // 
            btnGuardarTit.Anchor = AnchorStyles.Top;
            btnGuardarTit.BackColor = Color.FromArgb(1, 87, 155);
            btnGuardarTit.FlatAppearance.BorderSize = 0;
            btnGuardarTit.FlatStyle = FlatStyle.Flat;
            btnGuardarTit.ForeColor = Color.White;
            btnGuardarTit.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardarTit.IconColor = Color.White;
            btnGuardarTit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardarTit.IconSize = 30;
            btnGuardarTit.Location = new Point(159, 473);
            btnGuardarTit.Name = "btnGuardarTit";
            btnGuardarTit.Size = new Size(399, 37);
            btnGuardarTit.TabIndex = 8;
            btnGuardarTit.Text = "Guardar";
            btnGuardarTit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardarTit.UseVisualStyleBackColor = false;
            btnGuardarTit.Click += btnGuardarTit_Click;
            // 
            // txtCorreoContacto
            // 
            txtCorreoContacto.Anchor = AnchorStyles.Top;
            txtCorreoContacto.Font = new Font("Century Gothic", 9F);
            txtCorreoContacto.Location = new Point(159, 331);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(389, 26);
            txtCorreoContacto.TabIndex = 5;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 9F);
            label7.Location = new Point(159, 305);
            label7.Name = "label7";
            label7.Size = new Size(61, 20);
            label7.TabIndex = 44;
            label7.Text = "Correo";
            // 
            // txtDireccionAgente
            // 
            txtDireccionAgente.Anchor = AnchorStyles.Top;
            txtDireccionAgente.Font = new Font("Century Gothic", 9F);
            txtDireccionAgente.Location = new Point(596, 184);
            txtDireccionAgente.Name = "txtDireccionAgente";
            txtDireccionAgente.Size = new Size(389, 26);
            txtDireccionAgente.TabIndex = 2;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 9F);
            label6.Location = new Point(596, 158);
            label6.Name = "label6";
            label6.Size = new Size(80, 20);
            label6.TabIndex = 42;
            label6.Text = "Dirección";
            // 
            // txtPais
            // 
            txtPais.Anchor = AnchorStyles.Top;
            txtPais.Font = new Font("Century Gothic", 9F);
            txtPais.Location = new Point(596, 253);
            txtPais.Name = "txtPais";
            txtPais.Size = new Size(389, 26);
            txtPais.TabIndex = 4;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 9F);
            label5.Location = new Point(596, 227);
            label5.Name = "label5";
            label5.Size = new Size(37, 20);
            label5.TabIndex = 40;
            label5.Text = "Pais";
            // 
            // txtNitAgente
            // 
            txtNitAgente.Anchor = AnchorStyles.Top;
            txtNitAgente.Font = new Font("Century Gothic", 9F);
            txtNitAgente.Location = new Point(159, 253);
            txtNitAgente.Name = "txtNitAgente";
            txtNitAgente.Size = new Size(389, 26);
            txtNitAgente.TabIndex = 3;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 9F);
            label4.Location = new Point(159, 227);
            label4.Name = "label4";
            label4.Size = new Size(30, 20);
            label4.TabIndex = 37;
            label4.Text = "NIT";
            // 
            // txtNombreAgente
            // 
            txtNombreAgente.Anchor = AnchorStyles.Top;
            txtNombreAgente.Font = new Font("Century Gothic", 9F);
            txtNombreAgente.Location = new Point(159, 184);
            txtNombreAgente.Name = "txtNombreAgente";
            txtNombreAgente.Size = new Size(389, 26);
            txtNombreAgente.TabIndex = 1;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 9F);
            label3.Location = new Point(159, 158);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 34;
            label3.Text = "Nombre";
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.Anchor = AnchorStyles.Top;
            iconPictureBox2.BackColor = Color.FromArgb(196, 195, 209);
            iconPictureBox2.ForeColor = SystemColors.ControlText;
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.UserTie;
            iconPictureBox2.IconColor = SystemColors.ControlText;
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 40;
            iconPictureBox2.Location = new Point(428, 18);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(40, 40);
            iconPictureBox2.TabIndex = 54;
            iconPictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(196, 195, 209);
            label2.Font = new Font("Century Gothic", 15F);
            label2.Location = new Point(474, 18);
            label2.Name = "label2";
            label2.Size = new Size(115, 31);
            label2.TabIndex = 48;
            label2.Text = "Agentes";
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.Top;
            iconButton1.BackColor = Color.Black;
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 10F);
            iconButton1.ForeColor = Color.White;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlassPlus;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 30;
            iconButton1.Location = new Point(589, 129);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(144, 32);
            iconButton1.TabIndex = 51;
            iconButton1.Text = "Buscar";
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top;
            textBox1.Location = new Point(250, 130);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(301, 32);
            textBox1.TabIndex = 50;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(236, 236, 238);
            label1.Font = new Font("Century Gothic", 9F);
            label1.Location = new Point(250, 107);
            label1.Name = "label1";
            label1.Size = new Size(195, 20);
            label1.TabIndex = 49;
            label1.Text = "Buscar por nombre o pais";
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
            roundedButton5.Location = new Point(170, 6);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(683, 61);
            roundedButton5.TabIndex = 53;
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
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
            roundedButton3.Location = new Point(226, 101);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(556, 74);
            roundedButton3.TabIndex = 52;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = Color.FromArgb(192, 202, 212);
            panel1.Controls.Add(dtgAgentes);
            panel1.Location = new Point(23, 205);
            panel1.Name = "panel1";
            panel1.Size = new Size(952, 539);
            panel1.TabIndex = 55;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.Anchor = AnchorStyles.Top;
            iconPictureBox1.BackColor = Color.FromArgb(196, 195, 209);
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.UserTie;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 40;
            iconPictureBox1.Location = new Point(490, 37);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(40, 40);
            iconPictureBox1.TabIndex = 57;
            iconPictureBox1.TabStop = false;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top;
            label10.AutoSize = true;
            label10.BackColor = Color.FromArgb(196, 195, 209);
            label10.Font = new Font("Century Gothic", 15F);
            label10.Location = new Point(536, 37);
            label10.Name = "label10";
            label10.Size = new Size(115, 31);
            label10.TabIndex = 55;
            label10.Text = "Agentes";
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top;
            roundedButton1.BackColor = Color.FromArgb(196, 195, 209);
            roundedButton1.BackgroundColor = Color.FromArgb(196, 195, 209);
            roundedButton1.BorderColor = Color.FromArgb(196, 195, 209);
            roundedButton1.BorderRadius = 60;
            roundedButton1.BorderSize = 0;
            roundedButton1.Enabled = false;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(232, 25);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(683, 61);
            roundedButton1.TabIndex = 56;
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // FrmAdministrarAgentes
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAdministrarAgentes";
            Text = "FrmAdministrarAgentes";
            Load += FrmAdministrarAgentes_Load;
            tabControl1.ResumeLayout(false);
            tabPageListado.ResumeLayout(false);
            tabPageListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).EndInit();
            tabPageAgenteDetail.ResumeLayout(false);
            tabPageAgenteDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabPageListado;
        private TabPage tabPageAgenteDetail;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private FontAwesome.Sharp.IconButton ibtnAgregar;
        private DataGridView dtgAgentes;
        private TextBox txtNombreContacto;
        private Label label9;
        private TextBox txtTelefonoContacto;
        private Label label8;
        private FontAwesome.Sharp.IconButton btnCancelarTit;
        private FontAwesome.Sharp.IconButton btnGuardarTit;
        private TextBox txtCorreoContacto;
        private Label label7;
        private TextBox txtDireccionAgente;
        private Label label6;
        private TextBox txtPais;
        private Label label5;
        private TextBox txtNitAgente;
        private Label label4;
        private TextBox txtNombreAgente;
        private Label label3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Label label2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private TextBox textBox1;
        private Label label1;
        private Clases.RoundedButton roundedButton5;
        private Clases.RoundedButton roundedButton3;
        private Panel panel1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label label10;
        private Clases.RoundedButton roundedButton1;
    }
}