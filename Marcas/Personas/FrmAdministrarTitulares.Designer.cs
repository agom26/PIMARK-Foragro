namespace Presentacion.Personas
{
    partial class FrmAdministrarTitulares
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabListTitulares = new TabPage();
            ibtnEliminar = new FontAwesome.Sharp.IconButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            dtgTitulares = new DataGridView();
            txtSearch = new TextBox();
            label2 = new Label();
            tabTitularDetail = new TabPage();
            txtNombreContacto = new TextBox();
            label9 = new Label();
            txtTelefonoContacto = new TextBox();
            label8 = new Label();
            btnCancelarTit = new FontAwesome.Sharp.IconButton();
            btnGuardarTit = new FontAwesome.Sharp.IconButton();
            txtCorreoContacto = new TextBox();
            label7 = new Label();
            txtDireccionTitular = new TextBox();
            label6 = new Label();
            txtPais = new TextBox();
            label5 = new Label();
            txtNitTitular = new TextBox();
            label4 = new Label();
            txtNombreTitular = new TextBox();
            label3 = new Label();
            flowLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabListTitulares.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgTitulares).BeginInit();
            tabTitularDetail.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1169, 51);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 19F);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(162, 39);
            label1.TabIndex = 0;
            label1.Text = "TITULARES";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabListTitulares);
            tabControl1.Controls.Add(tabTitularDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 12F);
            tabControl1.Location = new Point(0, 51);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 522);
            tabControl1.TabIndex = 1;
            // 
            // tabListTitulares
            // 
            tabListTitulares.Controls.Add(ibtnEliminar);
            tabListTitulares.Controls.Add(ibtnEditar);
            tabListTitulares.Controls.Add(ibtnAgregar);
            tabListTitulares.Controls.Add(ibtnBuscar);
            tabListTitulares.Controls.Add(dtgTitulares);
            tabListTitulares.Controls.Add(txtSearch);
            tabListTitulares.Controls.Add(label2);
            tabListTitulares.Location = new Point(4, 32);
            tabListTitulares.Name = "tabListTitulares";
            tabListTitulares.Padding = new Padding(3);
            tabListTitulares.Size = new Size(1161, 486);
            tabListTitulares.TabIndex = 0;
            tabListTitulares.Text = "Listado";
            tabListTitulares.UseVisualStyleBackColor = true;
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
            ibtnEliminar.Location = new Point(986, 306);
            ibtnEliminar.Name = "ibtnEliminar";
            ibtnEliminar.Size = new Size(144, 37);
            ibtnEliminar.TabIndex = 11;
            ibtnEliminar.Text = "Eliminar";
            ibtnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEliminar.UseVisualStyleBackColor = false;
            ibtnEliminar.Click += ibtnEliminar_Click;
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
            ibtnEditar.Location = new Point(986, 251);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(144, 37);
            ibtnEditar.TabIndex = 10;
            ibtnEditar.Text = "Editar";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click;
            // 
            // ibtnAgregar
            // 
            ibtnAgregar.Anchor = AnchorStyles.Right;
            ibtnAgregar.BackColor = Color.FromArgb(50, 164, 115);
            ibtnAgregar.FlatAppearance.BorderSize = 0;
            ibtnAgregar.FlatStyle = FlatStyle.Flat;
            ibtnAgregar.ForeColor = Color.White;
            ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            ibtnAgregar.IconColor = Color.White;
            ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnAgregar.IconSize = 30;
            ibtnAgregar.Location = new Point(986, 193);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(144, 37);
            ibtnAgregar.TabIndex = 9;
            ibtnAgregar.Text = "Agregar";
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnAgregar_Click;
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
            ibtnBuscar.TabIndex = 8;
            ibtnBuscar.Text = "Buscar";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            // 
            // dtgTitulares
            // 
            dtgTitulares.AllowUserToAddRows = false;
            dtgTitulares.AllowUserToDeleteRows = false;
            dtgTitulares.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtgTitulares.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtgTitulares.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtgTitulares.BackgroundColor = Color.White;
            dtgTitulares.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgTitulares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgTitulares.Location = new Point(39, 118);
            dtgTitulares.Name = "dtgTitulares";
            dtgTitulares.ReadOnly = true;
            dtgTitulares.RowHeadersWidth = 51;
            dtgTitulares.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgTitulares.Size = new Size(921, 340);
            dtgTitulares.TabIndex = 7;
            dtgTitulares.CellClick += dtgTitulares_CellClick;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top;
            txtSearch.Location = new Point(39, 58);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(750, 32);
            txtSearch.TabIndex = 6;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F);
            label2.Location = new Point(39, 20);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.No;
            label2.Size = new Size(134, 23);
            label2.TabIndex = 5;
            label2.Text = "Buscar titular";
            // 
            // tabTitularDetail
            // 
            tabTitularDetail.Controls.Add(txtNombreContacto);
            tabTitularDetail.Controls.Add(label9);
            tabTitularDetail.Controls.Add(txtTelefonoContacto);
            tabTitularDetail.Controls.Add(label8);
            tabTitularDetail.Controls.Add(btnCancelarTit);
            tabTitularDetail.Controls.Add(btnGuardarTit);
            tabTitularDetail.Controls.Add(txtCorreoContacto);
            tabTitularDetail.Controls.Add(label7);
            tabTitularDetail.Controls.Add(txtDireccionTitular);
            tabTitularDetail.Controls.Add(label6);
            tabTitularDetail.Controls.Add(txtPais);
            tabTitularDetail.Controls.Add(label5);
            tabTitularDetail.Controls.Add(txtNitTitular);
            tabTitularDetail.Controls.Add(label4);
            tabTitularDetail.Controls.Add(txtNombreTitular);
            tabTitularDetail.Controls.Add(label3);
            tabTitularDetail.Location = new Point(4, 32);
            tabTitularDetail.Name = "tabTitularDetail";
            tabTitularDetail.Padding = new Padding(3);
            tabTitularDetail.Size = new Size(1161, 486);
            tabTitularDetail.TabIndex = 1;
            tabTitularDetail.Text = "Detalle de titular";
            tabTitularDetail.UseVisualStyleBackColor = true;
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Font = new Font("Century Gothic", 10F);
            txtNombreContacto.Location = new Point(172, 302);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(389, 28);
            txtNombreContacto.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 12F);
            label9.Location = new Point(172, 276);
            label9.Name = "label9";
            label9.Size = new Size(105, 23);
            label9.TabIndex = 31;
            label9.Text = "Contacto";
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Font = new Font("Century Gothic", 10F);
            txtTelefonoContacto.Location = new Point(609, 227);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(389, 28);
            txtTelefonoContacto.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 12F);
            label8.Location = new Point(609, 201);
            label8.Name = "label8";
            label8.Size = new Size(93, 23);
            label8.TabIndex = 29;
            label8.Text = "Teléfono";
            // 
            // btnCancelarTit
            // 
            btnCancelarTit.BackColor = Color.Gainsboro;
            btnCancelarTit.FlatAppearance.BorderSize = 0;
            btnCancelarTit.FlatStyle = FlatStyle.Flat;
            btnCancelarTit.ForeColor = Color.Black;
            btnCancelarTit.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelarTit.IconColor = Color.Black;
            btnCancelarTit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelarTit.IconSize = 30;
            btnCancelarTit.Location = new Point(609, 374);
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
            btnGuardarTit.BackColor = Color.FromArgb(1, 87, 155);
            btnGuardarTit.FlatAppearance.BorderSize = 0;
            btnGuardarTit.FlatStyle = FlatStyle.Flat;
            btnGuardarTit.ForeColor = Color.White;
            btnGuardarTit.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardarTit.IconColor = Color.White;
            btnGuardarTit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardarTit.IconSize = 30;
            btnGuardarTit.Location = new Point(162, 374);
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
            txtCorreoContacto.Font = new Font("Century Gothic", 10F);
            txtCorreoContacto.Location = new Point(172, 227);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(389, 28);
            txtCorreoContacto.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F);
            label7.Location = new Point(172, 201);
            label7.Name = "label7";
            label7.Size = new Size(77, 23);
            label7.TabIndex = 28;
            label7.Text = "Correo";
            // 
            // txtDireccionTitular
            // 
            txtDireccionTitular.Font = new Font("Century Gothic", 10F);
            txtDireccionTitular.Location = new Point(609, 80);
            txtDireccionTitular.Name = "txtDireccionTitular";
            txtDireccionTitular.Size = new Size(389, 28);
            txtDireccionTitular.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F);
            label6.Location = new Point(609, 54);
            label6.Name = "label6";
            label6.Size = new Size(101, 23);
            label6.TabIndex = 26;
            label6.Text = "Dirección";
            // 
            // txtPais
            // 
            txtPais.Font = new Font("Century Gothic", 10F);
            txtPais.Location = new Point(609, 149);
            txtPais.Name = "txtPais";
            txtPais.Size = new Size(389, 28);
            txtPais.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F);
            label5.Location = new Point(609, 123);
            label5.Name = "label5";
            label5.Size = new Size(47, 23);
            label5.TabIndex = 24;
            label5.Text = "Pais";
            // 
            // txtNitTitular
            // 
            txtNitTitular.Font = new Font("Century Gothic", 10F);
            txtNitTitular.Location = new Point(172, 149);
            txtNitTitular.Name = "txtNitTitular";
            txtNitTitular.Size = new Size(389, 28);
            txtNitTitular.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F);
            label4.Location = new Point(172, 123);
            label4.Name = "label4";
            label4.Size = new Size(38, 23);
            label4.TabIndex = 21;
            label4.Text = "NIT";
            // 
            // txtNombreTitular
            // 
            txtNombreTitular.Font = new Font("Century Gothic", 10F);
            txtNombreTitular.Location = new Point(172, 80);
            txtNombreTitular.Name = "txtNombreTitular";
            txtNombreTitular.Size = new Size(389, 28);
            txtNombreTitular.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F);
            label3.Location = new Point(172, 54);
            label3.Name = "label3";
            label3.Size = new Size(90, 23);
            label3.TabIndex = 18;
            label3.Text = "Nombre";
            // 
            // FrmAdministrarTitulares
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1169, 573);
            Controls.Add(tabControl1);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAdministrarTitulares";
            Text = "FrmAdministrarTitulares";
            Load += FrmAdministrarTitulares_Load;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabListTitulares.ResumeLayout(false);
            tabListTitulares.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgTitulares).EndInit();
            tabTitularDetail.ResumeLayout(false);
            tabTitularDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabListTitulares;
        private DataGridView dtgTitulares;
        private TextBox txtSearch;
        private Label label2;
        private TabPage tabTitularDetail;
        private FontAwesome.Sharp.IconButton ibtnEliminar;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private FontAwesome.Sharp.IconButton ibtnAgregar;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private TextBox txtTelefonoContacto;
        private Label label8;
        private FontAwesome.Sharp.IconButton btnCancelarTit;
        private FontAwesome.Sharp.IconButton btnGuardarTit;
        private TextBox txtCorreoContacto;
        private Label label7;
        private TextBox txtDireccionTitular;
        private Label label6;
        private TextBox txtPais;
        private Label label5;
        private TextBox txtNitTitular;
        private Label label4;
        private TextBox txtNombreTitular;
        private Label label3;
        private TextBox txtNombreContacto;
        private Label label9;
    }
}