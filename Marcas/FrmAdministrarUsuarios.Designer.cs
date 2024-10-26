namespace Presentacion
{
    partial class FrmAdministrarUsuarios
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
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            iconButton4 = new FontAwesome.Sharp.IconButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            dtgUsuarios = new DataGridView();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            txtSearch = new TextBox();
            label2 = new Label();
            tabPageUserDetail = new TabPage();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            txtConfirmarCont = new TextBox();
            label8 = new Label();
            chckbIsAdmin = new CheckBox();
            iconButton6 = new FontAwesome.Sharp.IconButton();
            iconButton5 = new FontAwesome.Sharp.IconButton();
            txtCont = new TextBox();
            label7 = new Label();
            txtCorreo = new TextBox();
            label6 = new Label();
            txtApellidos = new TextBox();
            label5 = new Label();
            txtNombres = new TextBox();
            label4 = new Label();
            txtUsername = new TextBox();
            label3 = new Label();
            flowLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).BeginInit();
            tabPageUserDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 19F);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(169, 39);
            label1.TabIndex = 0;
            label1.Text = "USUARIOS";
            label1.Click += label1_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1169, 51);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPageUserDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 10F);
            tabControl1.Location = new Point(0, 51);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 522);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(iconButton4);
            tabPage1.Controls.Add(iconButton3);
            tabPage1.Controls.Add(iconButton2);
            tabPage1.Controls.Add(dtgUsuarios);
            tabPage1.Controls.Add(ibtnBuscar);
            tabPage1.Controls.Add(txtSearch);
            tabPage1.Controls.Add(label2);
            tabPage1.Font = new Font("Century Gothic", 12F);
            tabPage1.Location = new Point(4, 30);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1161, 488);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Listado de usuarios";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // iconButton4
            // 
            iconButton4.Anchor = AnchorStyles.Right;
            iconButton4.AutoSize = true;
            iconButton4.BackColor = Color.FromArgb(244, 98, 96);
            iconButton4.FlatAppearance.BorderSize = 0;
            iconButton4.FlatStyle = FlatStyle.Flat;
            iconButton4.ForeColor = Color.White;
            iconButton4.IconChar = FontAwesome.Sharp.IconChar.Trash;
            iconButton4.IconColor = Color.White;
            iconButton4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton4.IconSize = 30;
            iconButton4.Location = new Point(985, 306);
            iconButton4.Name = "iconButton4";
            iconButton4.Size = new Size(144, 37);
            iconButton4.TabIndex = 7;
            iconButton4.Text = "Eliminar";
            iconButton4.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton4.UseVisualStyleBackColor = false;
            iconButton4.Click += iconButton4_Click;
            // 
            // iconButton3
            // 
            iconButton3.Anchor = AnchorStyles.Right;
            iconButton3.AutoSize = true;
            iconButton3.BackColor = Color.FromArgb(96, 149, 241);
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Pen;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 30;
            iconButton3.Location = new Point(985, 251);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(144, 37);
            iconButton3.TabIndex = 6;
            iconButton3.Text = "Editar";
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = false;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Right;
            iconButton2.AutoSize = true;
            iconButton2.BackColor = Color.FromArgb(50, 164, 115);
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.ForeColor = Color.White;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            iconButton2.IconColor = Color.White;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 30;
            iconButton2.Location = new Point(985, 193);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(144, 37);
            iconButton2.TabIndex = 5;
            iconButton2.Text = "Agregar";
            iconButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // dtgUsuarios
            // 
            dtgUsuarios.AllowUserToAddRows = false;
            dtgUsuarios.AllowUserToDeleteRows = false;
            dtgUsuarios.AllowUserToResizeRows = false;
            dtgUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtgUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgUsuarios.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgUsuarios.BorderStyle = BorderStyle.None;
            dtgUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgUsuarios.ColumnHeadersHeight = 40;
            dtgUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtgUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            dtgUsuarios.EnableHeadersVisualStyles = false;
            dtgUsuarios.GridColor = Color.LightGray;
            dtgUsuarios.Location = new Point(170, 118);
            dtgUsuarios.Name = "dtgUsuarios";
            dtgUsuarios.ReadOnly = true;
            dtgUsuarios.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgUsuarios.RowHeadersWidth = 51;
            dtgUsuarios.Size = new Size(778, 340);
            dtgUsuarios.TabIndex = 4;
            dtgUsuarios.CellContentClick += dtgUsuarios_CellContentClick;
            // 
            // ibtnBuscar
            // 
            ibtnBuscar.Anchor = AnchorStyles.Top;
            ibtnBuscar.BackColor = Color.FromArgb(255, 164, 0);
            ibtnBuscar.FlatAppearance.BorderSize = 0;
            ibtnBuscar.FlatStyle = FlatStyle.Flat;
            ibtnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlassPlus;
            ibtnBuscar.IconColor = Color.Black;
            ibtnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnBuscar.IconSize = 30;
            ibtnBuscar.Location = new Point(816, 58);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(144, 32);
            ibtnBuscar.TabIndex = 3;
            ibtnBuscar.Text = "Buscar";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            ibtnBuscar.Click += ibtnBuscar_Click;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top;
            txtSearch.Location = new Point(170, 58);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(619, 32);
            txtSearch.TabIndex = 2;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F);
            label2.Location = new Point(170, 19);
            label2.Name = "label2";
            label2.Size = new Size(148, 23);
            label2.TabIndex = 1;
            label2.Text = "Buscar usuario";
            // 
            // tabPageUserDetail
            // 
            tabPageUserDetail.Controls.Add(iconPictureBox1);
            tabPageUserDetail.Controls.Add(txtConfirmarCont);
            tabPageUserDetail.Controls.Add(label8);
            tabPageUserDetail.Controls.Add(chckbIsAdmin);
            tabPageUserDetail.Controls.Add(iconButton6);
            tabPageUserDetail.Controls.Add(iconButton5);
            tabPageUserDetail.Controls.Add(txtCont);
            tabPageUserDetail.Controls.Add(label7);
            tabPageUserDetail.Controls.Add(txtCorreo);
            tabPageUserDetail.Controls.Add(label6);
            tabPageUserDetail.Controls.Add(txtApellidos);
            tabPageUserDetail.Controls.Add(label5);
            tabPageUserDetail.Controls.Add(txtNombres);
            tabPageUserDetail.Controls.Add(label4);
            tabPageUserDetail.Controls.Add(txtUsername);
            tabPageUserDetail.Controls.Add(label3);
            tabPageUserDetail.Font = new Font("Century Gothic", 12F);
            tabPageUserDetail.Location = new Point(4, 30);
            tabPageUserDetail.Name = "tabPageUserDetail";
            tabPageUserDetail.Padding = new Padding(3);
            tabPageUserDetail.Size = new Size(1161, 488);
            tabPageUserDetail.TabIndex = 1;
            tabPageUserDetail.Text = "Detalle de usuario";
            tabPageUserDetail.UseVisualStyleBackColor = true;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.Transparent;
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 120;
            iconPictureBox1.Location = new Point(493, 27);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(128, 120);
            iconPictureBox1.TabIndex = 17;
            iconPictureBox1.TabStop = false;
            // 
            // txtConfirmarCont
            // 
            txtConfirmarCont.Font = new Font("Century Gothic", 10F);
            txtConfirmarCont.Location = new Point(600, 323);
            txtConfirmarCont.Name = "txtConfirmarCont";
            txtConfirmarCont.Size = new Size(389, 28);
            txtConfirmarCont.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 12F);
            label8.Location = new Point(600, 297);
            label8.Name = "label8";
            label8.Size = new Size(223, 23);
            label8.TabIndex = 15;
            label8.Text = "Confirmar contraseña";
            // 
            // chckbIsAdmin
            // 
            chckbIsAdmin.AutoSize = true;
            chckbIsAdmin.Location = new Point(163, 379);
            chckbIsAdmin.Name = "chckbIsAdmin";
            chckbIsAdmin.Size = new Size(286, 27);
            chckbIsAdmin.TabIndex = 7;
            chckbIsAdmin.Text = "Permisos de administrador";
            chckbIsAdmin.UseVisualStyleBackColor = true;
            // 
            // iconButton6
            // 
            iconButton6.BackColor = Color.Gainsboro;
            iconButton6.FlatAppearance.BorderSize = 0;
            iconButton6.FlatStyle = FlatStyle.Flat;
            iconButton6.ForeColor = Color.Black;
            iconButton6.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            iconButton6.IconColor = Color.Black;
            iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton6.IconSize = 30;
            iconButton6.Location = new Point(600, 432);
            iconButton6.Name = "iconButton6";
            iconButton6.Size = new Size(389, 37);
            iconButton6.TabIndex = 9;
            iconButton6.Text = "Cancelar";
            iconButton6.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton6.UseVisualStyleBackColor = false;
            iconButton6.Click += iconButton6_Click;
            // 
            // iconButton5
            // 
            iconButton5.BackColor = Color.FromArgb(1, 87, 155);
            iconButton5.FlatAppearance.BorderSize = 0;
            iconButton5.FlatStyle = FlatStyle.Flat;
            iconButton5.ForeColor = Color.White;
            iconButton5.IconChar = FontAwesome.Sharp.IconChar.Save;
            iconButton5.IconColor = Color.White;
            iconButton5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton5.IconSize = 30;
            iconButton5.Location = new Point(153, 432);
            iconButton5.Name = "iconButton5";
            iconButton5.Size = new Size(399, 37);
            iconButton5.TabIndex = 8;
            iconButton5.Text = "Guardar";
            iconButton5.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton5.UseVisualStyleBackColor = false;
            iconButton5.Click += iconButton5_Click;
            // 
            // txtCont
            // 
            txtCont.Font = new Font("Century Gothic", 10F);
            txtCont.Location = new Point(163, 323);
            txtCont.Name = "txtCont";
            txtCont.Size = new Size(389, 28);
            txtCont.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F);
            label7.Location = new Point(163, 297);
            label7.Name = "label7";
            label7.Size = new Size(125, 23);
            label7.TabIndex = 10;
            label7.Text = "Contraseña";
            // 
            // txtCorreo
            // 
            txtCorreo.Font = new Font("Century Gothic", 10F);
            txtCorreo.Location = new Point(600, 176);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(389, 28);
            txtCorreo.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F);
            label6.Location = new Point(600, 150);
            label6.Name = "label6";
            label6.Size = new Size(77, 23);
            label6.TabIndex = 8;
            label6.Text = "Correo";
            // 
            // txtApellidos
            // 
            txtApellidos.Font = new Font("Century Gothic", 10F);
            txtApellidos.Location = new Point(600, 245);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(389, 28);
            txtApellidos.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F);
            label5.Location = new Point(600, 219);
            label5.Name = "label5";
            label5.Size = new Size(100, 23);
            label5.TabIndex = 6;
            label5.Text = "Apellidos";
            // 
            // txtNombres
            // 
            txtNombres.Font = new Font("Century Gothic", 10F);
            txtNombres.Location = new Point(163, 245);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(389, 28);
            txtNombres.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F);
            label4.Location = new Point(163, 219);
            label4.Name = "label4";
            label4.Size = new Size(98, 23);
            label4.TabIndex = 4;
            label4.Text = "Nombres";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Century Gothic", 10F);
            txtUsername.Location = new Point(163, 176);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(389, 28);
            txtUsername.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F);
            label3.Location = new Point(163, 150);
            label3.Name = "label3";
            label3.Size = new Size(79, 23);
            label3.TabIndex = 2;
            label3.Text = "Usuario";
            // 
            // FrmAdministrarUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1169, 573);
            ControlBox = false;
            Controls.Add(tabControl1);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAdministrarUsuarios";
            Text = "FrmAdministrarUsuarios";
            Load += FrmAdministrarUsuarios_Load;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgUsuarios).EndInit();
            tabPageUserDetail.ResumeLayout(false);
            tabPageUserDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private FlowLayoutPanel flowLayoutPanel1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPageUserDetail;
        private Label label2;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private TextBox txtSearch;
        private FontAwesome.Sharp.IconButton iconButton2;
        private DataGridView dtgUsuarios;
        private FontAwesome.Sharp.IconButton iconButton4;
        private FontAwesome.Sharp.IconButton iconButton3;
        private TextBox txtCorreo;
        private Label label6;
        private TextBox txtApellidos;
        private Label label5;
        private TextBox txtNombres;
        private Label label4;
        private TextBox txtUsername;
        private Label label3;
        private TextBox txtCont;
        private Label label7;
        private FontAwesome.Sharp.IconButton iconButton5;
        private FontAwesome.Sharp.IconButton iconButton6;
        private CheckBox chckbIsAdmin;
        private TextBox txtConfirmarCont;
        private Label label8;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
    }
}