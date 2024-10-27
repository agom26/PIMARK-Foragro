namespace Presentacion.Marcas_Internacionales
{
    partial class FrmAdministrarClientes
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
            tabControl1 = new TabControl();
            tabClientesList = new TabPage();
            ibtnEliminar = new FontAwesome.Sharp.IconButton();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            dtgClientes = new DataGridView();
            txtSearch = new TextBox();
            tabClienteDetail = new TabPage();
            btnCancelar = new Clases.RoundedButton();
            btnGuardarCliente = new Clases.RoundedButton();
            txtNombreContacto = new TextBox();
            label9 = new Label();
            txtTelefonoContacto = new TextBox();
            label8 = new Label();
            txtCorreoContacto = new TextBox();
            label7 = new Label();
            txtDireccionCliente = new TextBox();
            label6 = new Label();
            txtPais = new TextBox();
            label5 = new Label();
            txtNitCliente = new TextBox();
            label4 = new Label();
            txtNombreCliente = new TextBox();
            label3 = new Label();
            tabControl1.SuspendLayout();
            tabClientesList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgClientes).BeginInit();
            tabClienteDetail.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Century Gothic", 19F);
            label1.Location = new Point(413, 14);
            label1.Name = "label1";
            label1.Size = new Size(153, 39);
            label1.TabIndex = 1;
            label1.Text = "CLIENTES";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabClientesList);
            tabControl1.Controls.Add(tabClienteDetail);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1131, 605);
            tabControl1.TabIndex = 2;
            // 
            // tabClientesList
            // 
            tabClientesList.BackColor = Color.White;
            tabClientesList.Controls.Add(ibtnEliminar);
            tabClientesList.Controls.Add(label1);
            tabClientesList.Controls.Add(ibtnEditar);
            tabClientesList.Controls.Add(ibtnAgregar);
            tabClientesList.Controls.Add(ibtnBuscar);
            tabClientesList.Controls.Add(dtgClientes);
            tabClientesList.Controls.Add(txtSearch);
            tabClientesList.Location = new Point(4, 32);
            tabClientesList.Name = "tabClientesList";
            tabClientesList.Padding = new Padding(3);
            tabClientesList.Size = new Size(1123, 569);
            tabClientesList.TabIndex = 0;
            tabClientesList.Text = "Listado";
            // 
            // ibtnEliminar
            // 
            ibtnEliminar.Anchor = AnchorStyles.Right;
            ibtnEliminar.BackColor = Color.FromArgb(244, 98, 96);
            ibtnEliminar.FlatAppearance.BorderSize = 0;
            ibtnEliminar.FlatStyle = FlatStyle.Flat;
            ibtnEliminar.Font = new Font("Century Gothic", 9F);
            ibtnEliminar.ForeColor = Color.White;
            ibtnEliminar.IconChar = FontAwesome.Sharp.IconChar.Trash;
            ibtnEliminar.IconColor = Color.White;
            ibtnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEliminar.IconSize = 25;
            ibtnEliminar.Location = new Point(941, 343);
            ibtnEliminar.Name = "ibtnEliminar";
            ibtnEliminar.Size = new Size(144, 37);
            ibtnEliminar.TabIndex = 17;
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
            ibtnEditar.Font = new Font("Century Gothic", 9F);
            ibtnEditar.ForeColor = Color.White;
            ibtnEditar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            ibtnEditar.IconColor = Color.White;
            ibtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEditar.IconSize = 25;
            ibtnEditar.Location = new Point(941, 288);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(144, 37);
            ibtnEditar.TabIndex = 16;
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
            ibtnAgregar.Font = new Font("Century Gothic", 9F);
            ibtnAgregar.ForeColor = Color.White;
            ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            ibtnAgregar.IconColor = Color.White;
            ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnAgregar.IconSize = 25;
            ibtnAgregar.Location = new Point(941, 230);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(144, 37);
            ibtnAgregar.TabIndex = 15;
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
            ibtnBuscar.Font = new Font("Century Gothic", 9F);
            ibtnBuscar.IconChar = FontAwesome.Sharp.IconChar.Search;
            ibtnBuscar.IconColor = Color.Black;
            ibtnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnBuscar.IconSize = 25;
            ibtnBuscar.Location = new Point(775, 73);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(144, 32);
            ibtnBuscar.TabIndex = 14;
            ibtnBuscar.Text = "Buscar";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            // 
            // dtgClientes
            // 
            dtgClientes.AllowUserToAddRows = false;
            dtgClientes.AllowUserToDeleteRows = false;
            dtgClientes.AllowUserToResizeRows = false;
            dtgClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgClientes.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgClientes.BorderStyle = BorderStyle.None;
            dtgClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtgClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtgClientes.ColumnHeadersHeight = 40;
            dtgClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtgClientes.DefaultCellStyle = dataGridViewCellStyle2;
            dtgClientes.EnableHeadersVisualStyles = false;
            dtgClientes.GridColor = Color.LightGray;
            dtgClientes.Location = new Point(51, 135);
            dtgClientes.Name = "dtgClientes";
            dtgClientes.ReadOnly = true;
            dtgClientes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtgClientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtgClientes.RowHeadersWidth = 51;
            dtgClientes.Size = new Size(868, 391);
            dtgClientes.TabIndex = 13;
            dtgClientes.CellClick += dtgClientes_CellClick;
            dtgClientes.CellContentClick += dtgClientes_CellContentClick;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top;
            txtSearch.Font = new Font("Century Gothic", 9F);
            txtSearch.Location = new Point(51, 73);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(702, 26);
            txtSearch.TabIndex = 12;
            // 
            // tabClienteDetail
            // 
            tabClienteDetail.BackColor = Color.White;
            tabClienteDetail.Controls.Add(btnCancelar);
            tabClienteDetail.Controls.Add(btnGuardarCliente);
            tabClienteDetail.Controls.Add(txtNombreContacto);
            tabClienteDetail.Controls.Add(label9);
            tabClienteDetail.Controls.Add(txtTelefonoContacto);
            tabClienteDetail.Controls.Add(label8);
            tabClienteDetail.Controls.Add(txtCorreoContacto);
            tabClienteDetail.Controls.Add(label7);
            tabClienteDetail.Controls.Add(txtDireccionCliente);
            tabClienteDetail.Controls.Add(label6);
            tabClienteDetail.Controls.Add(txtPais);
            tabClienteDetail.Controls.Add(label5);
            tabClienteDetail.Controls.Add(txtNitCliente);
            tabClienteDetail.Controls.Add(label4);
            tabClienteDetail.Controls.Add(txtNombreCliente);
            tabClienteDetail.Controls.Add(label3);
            tabClienteDetail.Location = new Point(4, 29);
            tabClienteDetail.Name = "tabClienteDetail";
            tabClienteDetail.Padding = new Padding(3);
            tabClienteDetail.Size = new Size(1123, 572);
            tabClienteDetail.TabIndex = 1;
            tabClienteDetail.Text = "Detalle de cliente";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Gainsboro;
            btnCancelar.BackgroundColor = Color.Gainsboro;
            btnCancelar.BorderColor = Color.Gainsboro;
            btnCancelar.BorderRadius = 55;
            btnCancelar.BorderSize = 0;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.Black;
            btnCancelar.Location = new Point(599, 369);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(274, 50);
            btnCancelar.TabIndex = 49;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardarCliente
            // 
            btnGuardarCliente.BackColor = Color.FromArgb(1, 87, 155);
            btnGuardarCliente.BackgroundColor = Color.FromArgb(1, 87, 155);
            btnGuardarCliente.BorderColor = Color.FromArgb(1, 87, 155);
            btnGuardarCliente.BorderRadius = 55;
            btnGuardarCliente.BorderSize = 0;
            btnGuardarCliente.FlatAppearance.BorderSize = 0;
            btnGuardarCliente.FlatStyle = FlatStyle.Flat;
            btnGuardarCliente.ForeColor = Color.White;
            btnGuardarCliente.Location = new Point(277, 369);
            btnGuardarCliente.Name = "btnGuardarCliente";
            btnGuardarCliente.Size = new Size(274, 50);
            btnGuardarCliente.TabIndex = 48;
            btnGuardarCliente.Text = "Guardar";
            btnGuardarCliente.TextColor = Color.White;
            btnGuardarCliente.UseVisualStyleBackColor = false;
            btnGuardarCliente.Click += btnGuardarCliente_Click;
            // 
            // txtNombreContacto
            // 
            txtNombreContacto.Font = new Font("Century Gothic", 10F);
            txtNombreContacto.Location = new Point(162, 301);
            txtNombreContacto.Name = "txtNombreContacto";
            txtNombreContacto.Size = new Size(389, 28);
            txtNombreContacto.TabIndex = 38;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 12F);
            label9.Location = new Point(162, 275);
            label9.Name = "label9";
            label9.Size = new Size(105, 23);
            label9.TabIndex = 47;
            label9.Text = "Contacto";
            // 
            // txtTelefonoContacto
            // 
            txtTelefonoContacto.Font = new Font("Century Gothic", 10F);
            txtTelefonoContacto.Location = new Point(599, 226);
            txtTelefonoContacto.Name = "txtTelefonoContacto";
            txtTelefonoContacto.Size = new Size(389, 28);
            txtTelefonoContacto.TabIndex = 37;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 12F);
            label8.Location = new Point(599, 200);
            label8.Name = "label8";
            label8.Size = new Size(93, 23);
            label8.TabIndex = 46;
            label8.Text = "Teléfono";
            // 
            // txtCorreoContacto
            // 
            txtCorreoContacto.Font = new Font("Century Gothic", 10F);
            txtCorreoContacto.Location = new Point(162, 226);
            txtCorreoContacto.Name = "txtCorreoContacto";
            txtCorreoContacto.Size = new Size(389, 28);
            txtCorreoContacto.TabIndex = 36;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F);
            label7.Location = new Point(162, 200);
            label7.Name = "label7";
            label7.Size = new Size(77, 23);
            label7.TabIndex = 45;
            label7.Text = "Correo";
            // 
            // txtDireccionCliente
            // 
            txtDireccionCliente.Font = new Font("Century Gothic", 10F);
            txtDireccionCliente.Location = new Point(599, 79);
            txtDireccionCliente.Name = "txtDireccionCliente";
            txtDireccionCliente.Size = new Size(389, 28);
            txtDireccionCliente.TabIndex = 33;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F);
            label6.Location = new Point(599, 53);
            label6.Name = "label6";
            label6.Size = new Size(101, 23);
            label6.TabIndex = 44;
            label6.Text = "Dirección";
            // 
            // txtPais
            // 
            txtPais.Font = new Font("Century Gothic", 10F);
            txtPais.Location = new Point(599, 148);
            txtPais.Name = "txtPais";
            txtPais.Size = new Size(389, 28);
            txtPais.TabIndex = 35;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F);
            label5.Location = new Point(599, 122);
            label5.Name = "label5";
            label5.Size = new Size(47, 23);
            label5.TabIndex = 43;
            label5.Text = "Pais";
            // 
            // txtNitCliente
            // 
            txtNitCliente.Font = new Font("Century Gothic", 10F);
            txtNitCliente.Location = new Point(162, 148);
            txtNitCliente.Name = "txtNitCliente";
            txtNitCliente.Size = new Size(389, 28);
            txtNitCliente.TabIndex = 34;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F);
            label4.Location = new Point(162, 122);
            label4.Name = "label4";
            label4.Size = new Size(38, 23);
            label4.TabIndex = 42;
            label4.Text = "NIT";
            // 
            // txtNombreCliente
            // 
            txtNombreCliente.Font = new Font("Century Gothic", 10F);
            txtNombreCliente.Location = new Point(162, 79);
            txtNombreCliente.Name = "txtNombreCliente";
            txtNombreCliente.Size = new Size(389, 28);
            txtNombreCliente.TabIndex = 32;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F);
            label3.Location = new Point(162, 53);
            label3.Name = "label3";
            label3.Size = new Size(90, 23);
            label3.TabIndex = 41;
            label3.Text = "Nombre";
            // 
            // FrmAdministrarClientes
            // 
            AutoScaleDimensions = new SizeF(12F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1131, 605);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 12F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmAdministrarClientes";
            Text = "FrmAdministrarClientes";
            Load += FrmAdministrarClientes_Load;
            tabControl1.ResumeLayout(false);
            tabClientesList.ResumeLayout(false);
            tabClientesList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgClientes).EndInit();
            tabClienteDetail.ResumeLayout(false);
            tabClienteDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabClientesList;
        private TabPage tabClienteDetail;
        private FontAwesome.Sharp.IconButton ibtnEliminar;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private FontAwesome.Sharp.IconButton ibtnAgregar;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private DataGridView dtgClientes;
        private TextBox txtSearch;
        private TextBox txtNombreContacto;
        private Label label9;
        private TextBox txtTelefonoContacto;
        private Label label8;
        private TextBox txtCorreoContacto;
        private Label label7;
        private TextBox txtDireccionCliente;
        private Label label6;
        private TextBox txtPais;
        private Label label5;
        private TextBox txtNitCliente;
        private Label label4;
        private TextBox txtNombreCliente;
        private Label label3;
        private Clases.RoundedButton btnGuardarCliente;
        private Clases.RoundedButton btnCancelar;
    }
}