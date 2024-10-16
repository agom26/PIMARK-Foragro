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
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabPageListado = new TabPage();
            ibtnEliminar = new FontAwesome.Sharp.IconButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            dtgAgentes = new DataGridView();
            txtSearch = new TextBox();
            label2 = new Label();
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
            flowLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).BeginInit();
            tabPageAgenteDetail.SuspendLayout();
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
            label1.Size = new Size(155, 39);
            label1.TabIndex = 0;
            label1.Text = "AGENTES";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageListado);
            tabControl1.Controls.Add(tabPageAgenteDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 12F);
            tabControl1.Location = new Point(0, 51);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 522);
            tabControl1.TabIndex = 1;
            // 
            // tabPageListado
            // 
            tabPageListado.Controls.Add(ibtnEliminar);
            tabPageListado.Controls.Add(ibtnEditar);
            tabPageListado.Controls.Add(ibtnAgregar);
            tabPageListado.Controls.Add(ibtnBuscar);
            tabPageListado.Controls.Add(dtgAgentes);
            tabPageListado.Controls.Add(txtSearch);
            tabPageListado.Controls.Add(label2);
            tabPageListado.Location = new Point(4, 32);
            tabPageListado.Name = "tabPageListado";
            tabPageListado.Padding = new Padding(3);
            tabPageListado.Size = new Size(1161, 486);
            tabPageListado.TabIndex = 0;
            tabPageListado.Text = "Listado de agentes";
            tabPageListado.UseVisualStyleBackColor = true;
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
            ibtnEliminar.Location = new Point(969, 309);
            ibtnEliminar.Name = "ibtnEliminar";
            ibtnEliminar.Size = new Size(144, 37);
            ibtnEliminar.TabIndex = 18;
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
            ibtnEditar.Location = new Point(969, 254);
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
            ibtnAgregar.Anchor = AnchorStyles.Right;
            ibtnAgregar.BackColor = Color.FromArgb(50, 164, 115);
            ibtnAgregar.FlatAppearance.BorderSize = 0;
            ibtnAgregar.FlatStyle = FlatStyle.Flat;
            ibtnAgregar.ForeColor = Color.White;
            ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            ibtnAgregar.IconColor = Color.White;
            ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnAgregar.IconSize = 30;
            ibtnAgregar.Location = new Point(969, 196);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(144, 37);
            ibtnAgregar.TabIndex = 16;
            ibtnAgregar.Text = "Agregar";
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnAgregar_Click;
            // 
            // ibtnBuscar
            // 
            ibtnBuscar.BackColor = Color.FromArgb(255, 164, 0);
            ibtnBuscar.FlatAppearance.BorderSize = 0;
            ibtnBuscar.FlatStyle = FlatStyle.Flat;
            ibtnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlassPlus;
            ibtnBuscar.IconColor = Color.Black;
            ibtnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnBuscar.IconSize = 30;
            ibtnBuscar.Location = new Point(799, 59);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(144, 32);
            ibtnBuscar.TabIndex = 15;
            ibtnBuscar.Text = "Buscar";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            // 
            // dtgAgentes
            // 
            dtgAgentes.AllowUserToAddRows = false;
            dtgAgentes.AllowUserToDeleteRows = false;
            dtgAgentes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dtgAgentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtgAgentes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtgAgentes.BackgroundColor = Color.White;
            dtgAgentes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgAgentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgAgentes.Location = new Point(48, 122);
            dtgAgentes.Name = "dtgAgentes";
            dtgAgentes.ReadOnly = true;
            dtgAgentes.RowHeadersWidth = 51;
            dtgAgentes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgAgentes.Size = new Size(895, 337);
            dtgAgentes.TabIndex = 14;
            dtgAgentes.CellClick += dtgAgentes_CellClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(48, 62);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(724, 32);
            txtSearch.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F);
            label2.Location = new Point(48, 26);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.No;
            label2.Size = new Size(152, 23);
            label2.TabIndex = 12;
            label2.Text = "Buscar agente";
            // 
            // tabPageAgenteDetail
            // 
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
            tabPageAgenteDetail.Location = new Point(4, 30);
            tabPageAgenteDetail.Name = "tabPageAgenteDetail";
            tabPageAgenteDetail.Padding = new Padding(3);
            tabPageAgenteDetail.Size = new Size(1161, 488);
            tabPageAgenteDetail.TabIndex = 1;
            tabPageAgenteDetail.Text = "Detalle de agente";
            tabPageAgenteDetail.UseVisualStyleBackColor = true;
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Font = new Font("Century Gothic", 10F);
            txtNombreContacto.Location = new Point(172, 314);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(389, 28);
            txtNombreContacto.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 12F);
            label9.Location = new Point(172, 288);
            label9.Name = "label9";
            label9.Size = new Size(105, 23);
            label9.TabIndex = 47;
            label9.Text = "Contacto";
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Font = new Font("Century Gothic", 10F);
            txtTelefonoContacto.Location = new Point(609, 239);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(389, 28);
            txtTelefonoContacto.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 12F);
            label8.Location = new Point(609, 213);
            label8.Name = "label8";
            label8.Size = new Size(93, 23);
            label8.TabIndex = 45;
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
            btnCancelarTit.Location = new Point(609, 386);
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
            btnGuardarTit.Location = new Point(162, 386);
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
            txtCorreoContacto.Location = new Point(172, 239);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(389, 28);
            txtCorreoContacto.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F);
            label7.Location = new Point(172, 213);
            label7.Name = "label7";
            label7.Size = new Size(77, 23);
            label7.TabIndex = 44;
            label7.Text = "Correo";
            // 
            // txtDireccionAgente
            // 
            txtDireccionAgente.Font = new Font("Century Gothic", 10F);
            txtDireccionAgente.Location = new Point(609, 92);
            txtDireccionAgente.Name = "txtDireccionAgente";
            txtDireccionAgente.Size = new Size(389, 28);
            txtDireccionAgente.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F);
            label6.Location = new Point(609, 66);
            label6.Name = "label6";
            label6.Size = new Size(101, 23);
            label6.TabIndex = 42;
            label6.Text = "Dirección";
            // 
            // txtPais
            // 
            txtPais.Font = new Font("Century Gothic", 10F);
            txtPais.Location = new Point(609, 161);
            txtPais.Name = "txtPais";
            txtPais.Size = new Size(389, 28);
            txtPais.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F);
            label5.Location = new Point(609, 135);
            label5.Name = "label5";
            label5.Size = new Size(47, 23);
            label5.TabIndex = 40;
            label5.Text = "Pais";
            // 
            // txtNitAgente
            // 
            txtNitAgente.Font = new Font("Century Gothic", 10F);
            txtNitAgente.Location = new Point(172, 161);
            txtNitAgente.Name = "txtNitAgente";
            txtNitAgente.Size = new Size(389, 28);
            txtNitAgente.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F);
            label4.Location = new Point(172, 135);
            label4.Name = "label4";
            label4.Size = new Size(38, 23);
            label4.TabIndex = 37;
            label4.Text = "NIT";
            // 
            // txtNombreAgente
            // 
            txtNombreAgente.Font = new Font("Century Gothic", 10F);
            txtNombreAgente.Location = new Point(172, 92);
            txtNombreAgente.Name = "txtNombreAgente";
            txtNombreAgente.Size = new Size(389, 28);
            txtNombreAgente.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F);
            label3.Location = new Point(172, 66);
            label3.Name = "label3";
            label3.Size = new Size(90, 23);
            label3.TabIndex = 34;
            label3.Text = "Nombre";
            // 
            // FrmAdministrarAgentes
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1169, 573);
            Controls.Add(tabControl1);
            Controls.Add(flowLayoutPanel1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAdministrarAgentes";
            Text = "FrmAdministrarAgentes";
            Load += FrmAdministrarAgentes_Load;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPageListado.ResumeLayout(false);
            tabPageListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgAgentes).EndInit();
            tabPageAgenteDetail.ResumeLayout(false);
            tabPageAgenteDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabPageListado;
        private TabPage tabPageAgenteDetail;
        private FontAwesome.Sharp.IconButton ibtnEliminar;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private FontAwesome.Sharp.IconButton ibtnAgregar;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private DataGridView dtgAgentes;
        private TextBox txtSearch;
        private Label label2;
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
    }
}