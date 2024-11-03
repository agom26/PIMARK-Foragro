using Comun.Cache;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmRenovacion : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        public FrmRenovacion()
        {
            InitializeComponent();
            this.Load += FrmRenovacion_Load;
            SeleccionarMarca.idN = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }

        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }

        private void MostrarRenovaciones()
        {
            dtgRenovaciones.DataSource = marcaModel.GetAllMarcasNacionalesRegistradas();
            // Ocultar la columna 'id'
            if (dtgRenovaciones.Columns["id"] != null)
            {
                dtgRenovaciones.Columns["id"].Visible = false;

                dtgRenovaciones.ClearSelection();
            }
        }
        private async void LoadMarcas()
        {
            // Obtiene los usuarios
            var marcasN = await Task.Run(() => marcaModel.GetAllMarcasNacionalesEnTramite());


            // Invoca el método para actualizar el DataGridView en el hilo principal
            Invoke(new Action(() =>
            {
                dtgRenovaciones.DataSource = marcasN;
                dtgRenovaciones.Refresh();
                // Oculta la columna 'id'
                if (dtgRenovaciones.Columns["id"] != null)
                {
                    dtgRenovaciones.Columns["id"].Visible = false;
                    // Desactiva la selección automática de la primera fila
                    dtgRenovaciones.ClearSelection();
                }
            }));
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }

            tabControl1.SelectedTab = nombre;
        }

        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPageRenovacionesList = new TabPage();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnAgregar = new FontAwesome.Sharp.IconButton();
            dtgRenovaciones = new DataGridView();
            panel4 = new Panel();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            label1 = new Label();
            ibtnBuscar = new FontAwesome.Sharp.IconButton();
            textBox1 = new TextBox();
            label2 = new Label();
            roundedButton3 = new Clases.RoundedButton();
            roundedButton5 = new Clases.RoundedButton();
            tabPageFormRenovacion = new TabPage();
            panel1 = new Panel();
            btnCancelar = new Clases.RoundedButton();
            btnActualizar = new Clases.RoundedButton();
            roundedButton6 = new Clases.RoundedButton();
            textBox4 = new TextBox();
            label8 = new Label();
            roundedButton2 = new Clases.RoundedButton();
            textBox5 = new TextBox();
            label12 = new Label();
            textBox6 = new TextBox();
            label13 = new Label();
            textBox7 = new TextBox();
            label14 = new Label();
            roundedButton4 = new Clases.RoundedButton();
            textBox3 = new TextBox();
            label7 = new Label();
            btnAgenteActual = new Clases.RoundedButton();
            txtEntidadTitular = new TextBox();
            label11 = new Label();
            txtDireccionTitular = new TextBox();
            label10 = new Label();
            txtNombreTitular = new TextBox();
            label9 = new Label();
            btnTitularActual = new Clases.RoundedButton();
            label6 = new Label();
            dateTimePicker2 = new DateTimePicker();
            label5 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label4 = new Label();
            textBox2 = new TextBox();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            label3 = new Label();
            roundedButton1 = new Clases.RoundedButton();
            tabControl1.SuspendLayout();
            tabPageRenovacionesList.SuspendLayout();
            ((ISupportInitialize)dtgRenovaciones).BeginInit();
            ((ISupportInitialize)iconPictureBox1).BeginInit();
            tabPageFormRenovacion.SuspendLayout();
            panel1.SuspendLayout();
            ((ISupportInitialize)iconPictureBox2).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageRenovacionesList);
            tabControl1.Controls.Add(tabPageFormRenovacion);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1169, 827);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPageRenovacionesList
            // 
            tabPageRenovacionesList.Controls.Add(ibtnEditar);
            tabPageRenovacionesList.Controls.Add(ibtnAgregar);
            tabPageRenovacionesList.Controls.Add(dtgRenovaciones);
            tabPageRenovacionesList.Controls.Add(panel4);
            tabPageRenovacionesList.Controls.Add(iconPictureBox1);
            tabPageRenovacionesList.Controls.Add(label1);
            tabPageRenovacionesList.Controls.Add(ibtnBuscar);
            tabPageRenovacionesList.Controls.Add(textBox1);
            tabPageRenovacionesList.Controls.Add(label2);
            tabPageRenovacionesList.Controls.Add(roundedButton3);
            tabPageRenovacionesList.Controls.Add(roundedButton5);
            tabPageRenovacionesList.Location = new Point(4, 29);
            tabPageRenovacionesList.Name = "tabPageRenovacionesList";
            tabPageRenovacionesList.Padding = new Padding(3);
            tabPageRenovacionesList.Size = new Size(1161, 794);
            tabPageRenovacionesList.TabIndex = 0;
            tabPageRenovacionesList.Text = "Lista de renovaciones";
            tabPageRenovacionesList.UseVisualStyleBackColor = true;
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
            ibtnEditar.Location = new Point(1011, 262);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(144, 37);
            ibtnEditar.TabIndex = 37;
            ibtnEditar.Text = "Editar";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click_1;
            // 
            // ibtnAgregar
            // 
            ibtnAgregar.Anchor = AnchorStyles.Top;
            ibtnAgregar.BackColor = Color.FromArgb(57, 73, 171);
            ibtnAgregar.FlatAppearance.BorderSize = 0;
            ibtnAgregar.FlatStyle = FlatStyle.Flat;
            ibtnAgregar.ForeColor = Color.White;
            ibtnAgregar.IconChar = FontAwesome.Sharp.IconChar.Add;
            ibtnAgregar.IconColor = Color.White;
            ibtnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnAgregar.IconSize = 30;
            ibtnAgregar.Location = new Point(1011, 207);
            ibtnAgregar.Name = "ibtnAgregar";
            ibtnAgregar.Size = new Size(144, 37);
            ibtnAgregar.TabIndex = 36;
            ibtnAgregar.Text = "Agregar";
            ibtnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnAgregar.UseVisualStyleBackColor = false;
            ibtnAgregar.Click += ibtnEditar_Click;
            // 
            // dtgRenovaciones
            // 
            dtgRenovaciones.AllowUserToAddRows = false;
            dtgRenovaciones.AllowUserToDeleteRows = false;
            dtgRenovaciones.AllowUserToResizeRows = false;
            dtgRenovaciones.Anchor = AnchorStyles.Top;
            dtgRenovaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgRenovaciones.BackgroundColor = Color.FromArgb(251, 251, 251);
            dtgRenovaciones.BorderStyle = BorderStyle.None;
            dtgRenovaciones.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgRenovaciones.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgRenovaciones.ColumnHeadersHeight = 40;
            dtgRenovaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgRenovaciones.EnableHeadersVisualStyles = false;
            dtgRenovaciones.GridColor = Color.LightGray;
            dtgRenovaciones.Location = new Point(42, 228);
            dtgRenovaciones.Name = "dtgRenovaciones";
            dtgRenovaciones.ReadOnly = true;
            dtgRenovaciones.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgRenovaciones.RowHeadersWidth = 51;
            dtgRenovaciones.Size = new Size(934, 492);
            dtgRenovaciones.TabIndex = 34;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top;
            panel4.BackColor = Color.FromArgb(192, 202, 212);
            panel4.Location = new Point(28, 207);
            panel4.Name = "panel4";
            panel4.Size = new Size(972, 542);
            panel4.TabIndex = 35;
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
            iconPictureBox1.Location = new Point(345, 16);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(40, 40);
            iconPictureBox1.TabIndex = 33;
            iconPictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(196, 195, 209);
            label1.Font = new Font("Century Gothic", 15F);
            label1.Location = new Point(400, 16);
            label1.Name = "label1";
            label1.Size = new Size(301, 31);
            label1.TabIndex = 27;
            label1.Text = "Tramites de renovación";
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
            ibtnBuscar.Location = new Point(632, 145);
            ibtnBuscar.Name = "ibtnBuscar";
            ibtnBuscar.Size = new Size(144, 32);
            ibtnBuscar.TabIndex = 30;
            ibtnBuscar.Text = "Buscar";
            ibtnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBuscar.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top;
            textBox1.Location = new Point(249, 149);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(377, 26);
            textBox1.TabIndex = 29;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(236, 236, 238);
            label2.Font = new Font("Century Gothic", 9F);
            label2.Location = new Point(249, 126);
            label2.Name = "label2";
            label2.Size = new Size(224, 20);
            label2.TabIndex = 28;
            label2.Text = "Buscar por nombre de marca\r\n";
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
            roundedButton3.Location = new Point(173, 120);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(683, 65);
            roundedButton3.TabIndex = 31;
            roundedButton3.TextColor = Color.White;
            roundedButton3.UseVisualStyleBackColor = false;
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
            roundedButton5.Location = new Point(172, 6);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(683, 61);
            roundedButton5.TabIndex = 32;
            roundedButton5.Text = "roundedButton5";
            roundedButton5.TextColor = Color.White;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // tabPageFormRenovacion
            // 
            tabPageFormRenovacion.AutoScroll = true;
            tabPageFormRenovacion.Controls.Add(panel1);
            tabPageFormRenovacion.Location = new Point(4, 29);
            tabPageFormRenovacion.Name = "tabPageFormRenovacion";
            tabPageFormRenovacion.Padding = new Padding(3);
            tabPageFormRenovacion.Size = new Size(1161, 794);
            tabPageFormRenovacion.TabIndex = 1;
            tabPageFormRenovacion.Text = "Renovacion";
            tabPageFormRenovacion.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(btnActualizar);
            panel1.Controls.Add(roundedButton6);
            panel1.Controls.Add(textBox4);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(roundedButton2);
            panel1.Controls.Add(textBox5);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(textBox6);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(textBox7);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(roundedButton4);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(btnAgenteActual);
            panel1.Controls.Add(txtEntidadTitular);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(txtDireccionTitular);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(txtNombreTitular);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(btnTitularActual);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(dateTimePicker2);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(iconPictureBox2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(roundedButton1);
            panel1.Location = new Point(-37, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(1136, 1034);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
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
            btnCancelar.Location = new Point(550, 964);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(231, 62);
            btnCancelar.TabIndex = 82;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextColor = Color.Black;
            btnCancelar.UseVisualStyleBackColor = false;
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
            btnActualizar.Location = new Point(281, 964);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(231, 62);
            btnActualizar.TabIndex = 81;
            btnActualizar.Text = "Guardar";
            btnActualizar.TextColor = Color.White;
            btnActualizar.UseVisualStyleBackColor = false;
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
            roundedButton6.Font = new Font("Century Gothic", 10F);
            roundedButton6.ForeColor = Color.Black;
            roundedButton6.Location = new Point(281, 120);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(500, 44);
            roundedButton6.TabIndex = 80;
            roundedButton6.Text = "MARCA";
            roundedButton6.TextColor = Color.Black;
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // textBox4
            // 
            textBox4.Enabled = false;
            textBox4.Font = new Font("Century Gothic", 9F);
            textBox4.Location = new Point(281, 919);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(231, 26);
            textBox4.TabIndex = 79;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 9F);
            label8.Location = new Point(281, 893);
            label8.Name = "label8";
            label8.Size = new Size(68, 20);
            label8.TabIndex = 78;
            label8.Text = "Nombre";
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
            roundedButton2.Location = new Point(281, 834);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(500, 44);
            roundedButton2.TabIndex = 77;
            roundedButton2.Text = "AGENTE ANTERIOR\r\n";
            roundedButton2.TextColor = Color.Black;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // textBox5
            // 
            textBox5.Enabled = false;
            textBox5.Font = new Font("Century Gothic", 9F);
            textBox5.Location = new Point(281, 791);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(231, 26);
            textBox5.TabIndex = 76;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Century Gothic", 9F);
            label12.Location = new Point(281, 765);
            label12.Name = "label12";
            label12.Size = new Size(64, 20);
            label12.TabIndex = 75;
            label12.Text = "Entidad";
            // 
            // textBox6
            // 
            textBox6.Enabled = false;
            textBox6.Font = new Font("Century Gothic", 9F);
            textBox6.Location = new Point(550, 724);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(231, 26);
            textBox6.TabIndex = 74;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Century Gothic", 9F);
            label13.Location = new Point(550, 701);
            label13.Name = "label13";
            label13.Size = new Size(80, 20);
            label13.TabIndex = 73;
            label13.Text = "Dirección";
            // 
            // textBox7
            // 
            textBox7.Enabled = false;
            textBox7.Font = new Font("Century Gothic", 9F);
            textBox7.Location = new Point(281, 724);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(231, 26);
            textBox7.TabIndex = 72;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Century Gothic", 9F);
            label14.Location = new Point(281, 698);
            label14.Name = "label14";
            label14.Size = new Size(68, 20);
            label14.TabIndex = 71;
            label14.Text = "Nombre";
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
            roundedButton4.Location = new Point(281, 639);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(500, 44);
            roundedButton4.TabIndex = 70;
            roundedButton4.Text = "TITULAR ANTERIOR\r\n";
            roundedButton4.TextColor = Color.Black;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Font = new Font("Century Gothic", 9F);
            textBox3.Location = new Point(281, 590);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(231, 26);
            textBox3.TabIndex = 69;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 9F);
            label7.Location = new Point(281, 564);
            label7.Name = "label7";
            label7.Size = new Size(68, 20);
            label7.TabIndex = 68;
            label7.Text = "Nombre";
            // 
            // btnAgenteActual
            // 
            btnAgenteActual.BackColor = Color.LightSteelBlue;
            btnAgenteActual.BackgroundColor = Color.LightSteelBlue;
            btnAgenteActual.BorderColor = Color.LightSteelBlue;
            btnAgenteActual.BorderRadius = 40;
            btnAgenteActual.BorderSize = 0;
            btnAgenteActual.FlatAppearance.BorderSize = 0;
            btnAgenteActual.FlatStyle = FlatStyle.Flat;
            btnAgenteActual.Font = new Font("Century Gothic", 10F);
            btnAgenteActual.ForeColor = Color.Black;
            btnAgenteActual.Location = new Point(281, 505);
            btnAgenteActual.Name = "btnAgenteActual";
            btnAgenteActual.Size = new Size(500, 44);
            btnAgenteActual.TabIndex = 67;
            btnAgenteActual.Text = "AGENTE ACTUAL";
            btnAgenteActual.TextColor = Color.Black;
            btnAgenteActual.UseVisualStyleBackColor = false;
            // 
            // txtEntidadTitular
            // 
            txtEntidadTitular.Enabled = false;
            txtEntidadTitular.Font = new Font("Century Gothic", 9F);
            txtEntidadTitular.Location = new Point(281, 462);
            txtEntidadTitular.Name = "txtEntidadTitular";
            txtEntidadTitular.Size = new Size(231, 26);
            txtEntidadTitular.TabIndex = 66;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Century Gothic", 9F);
            label11.Location = new Point(281, 436);
            label11.Name = "label11";
            label11.Size = new Size(64, 20);
            label11.TabIndex = 65;
            label11.Text = "Entidad";
            // 
            // txtDireccionTitular
            // 
            txtDireccionTitular.Enabled = false;
            txtDireccionTitular.Font = new Font("Century Gothic", 9F);
            txtDireccionTitular.Location = new Point(550, 395);
            txtDireccionTitular.Name = "txtDireccionTitular";
            txtDireccionTitular.Size = new Size(231, 26);
            txtDireccionTitular.TabIndex = 64;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 9F);
            label10.Location = new Point(550, 371);
            label10.Name = "label10";
            label10.Size = new Size(80, 20);
            label10.TabIndex = 63;
            label10.Text = "Dirección";
            // 
            // txtNombreTitular
            // 
            txtNombreTitular.Enabled = false;
            txtNombreTitular.Font = new Font("Century Gothic", 9F);
            txtNombreTitular.Location = new Point(281, 395);
            txtNombreTitular.Name = "txtNombreTitular";
            txtNombreTitular.Size = new Size(231, 26);
            txtNombreTitular.TabIndex = 62;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 9F);
            label9.Location = new Point(281, 369);
            label9.Name = "label9";
            label9.Size = new Size(68, 20);
            label9.TabIndex = 61;
            label9.Text = "Nombre";
            // 
            // btnTitularActual
            // 
            btnTitularActual.BackColor = Color.LightSteelBlue;
            btnTitularActual.BackgroundColor = Color.LightSteelBlue;
            btnTitularActual.BorderColor = Color.LightSteelBlue;
            btnTitularActual.BorderRadius = 40;
            btnTitularActual.BorderSize = 0;
            btnTitularActual.FlatAppearance.BorderSize = 0;
            btnTitularActual.FlatStyle = FlatStyle.Flat;
            btnTitularActual.Font = new Font("Century Gothic", 10F);
            btnTitularActual.ForeColor = Color.Black;
            btnTitularActual.Location = new Point(281, 310);
            btnTitularActual.Name = "btnTitularActual";
            btnTitularActual.Size = new Size(500, 44);
            btnTitularActual.TabIndex = 60;
            btnTitularActual.Text = "TITULAR ACTUAL";
            btnTitularActual.TextColor = Color.Black;
            btnTitularActual.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(550, 242);
            label6.Name = "label6";
            label6.Size = new Size(227, 20);
            label6.TabIndex = 42;
            label6.Text = "Nueva fecha de vencimiento";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Location = new Point(550, 265);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(231, 26);
            dateTimePicker2.TabIndex = 41;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(281, 242);
            label5.Name = "label5";
            label5.Size = new Size(169, 20);
            label5.TabIndex = 40;
            label5.Text = "Fecha de renovación";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(281, 265);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(231, 26);
            dateTimePicker1.TabIndex = 39;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(281, 167);
            label4.Name = "label4";
            label4.Size = new Size(144, 20);
            label4.TabIndex = 38;
            label4.Text = "Nombre de marca";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(281, 190);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(500, 26);
            textBox2.TabIndex = 37;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.Anchor = AnchorStyles.Top;
            iconPictureBox2.BackColor = Color.FromArgb(196, 195, 209);
            iconPictureBox2.ForeColor = SystemColors.ControlText;
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.Flag;
            iconPictureBox2.IconColor = SystemColors.ControlText;
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 40;
            iconPictureBox2.Location = new Point(338, 13);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(40, 40);
            iconPictureBox2.TabIndex = 36;
            iconPictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(196, 195, 209);
            label3.Font = new Font("Century Gothic", 15F);
            label3.Location = new Point(393, 13);
            label3.Name = "label3";
            label3.Size = new Size(291, 31);
            label3.TabIndex = 34;
            label3.Text = "Tramite de renovación";
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
            roundedButton1.Location = new Point(165, 3);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(683, 61);
            roundedButton1.TabIndex = 35;
            roundedButton1.Text = "roundedButton1";
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // FrmRenovacion
            // 
            BackColor = Color.White;
            ClientSize = new Size(1169, 827);
            Controls.Add(tabControl1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmRenovacion";
            Load += FrmRenovacion_Load;
            tabControl1.ResumeLayout(false);
            tabPageRenovacionesList.ResumeLayout(false);
            tabPageRenovacionesList.PerformLayout();
            ((ISupportInitialize)dtgRenovaciones).EndInit();
            ((ISupportInitialize)iconPictureBox1).EndInit();
            tabPageFormRenovacion.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((ISupportInitialize)iconPictureBox2).EndInit();
            ResumeLayout(false);
        }

        private TabControl tabControl1;
        private TabPage tabPageRenovacionesList;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label label1;
        private FontAwesome.Sharp.IconButton ibtnBuscar;
        private TextBox textBox1;
        private Label label2;
        private Clases.RoundedButton roundedButton3;
        private Clases.RoundedButton roundedButton5;
        private DataGridView dtgRenovaciones;
        private Panel panel4;
        private FontAwesome.Sharp.IconButton ibtnAgregar;
        private Panel panel1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private Label label3;
        private Clases.RoundedButton roundedButton1;
        private Label label4;
        private TextBox textBox2;
        private Label label6;
        private DateTimePicker dateTimePicker2;
        private Label label5;
        private DateTimePicker dateTimePicker1;
        private TextBox txtEntidadTitular;
        private Label label11;
        private TextBox txtDireccionTitular;
        private Label label10;
        private TextBox txtNombreTitular;
        private Label label9;
        private Clases.RoundedButton btnTitularActual;
        private TextBox textBox3;
        private Label label7;
        private Clases.RoundedButton btnAgenteActual;
        private TextBox textBox4;
        private Label label8;
        private Clases.RoundedButton roundedButton2;
        private TextBox textBox5;
        private Label label12;
        private TextBox textBox6;
        private Label label13;
        private TextBox textBox7;
        private Label label14;
        private Clases.RoundedButton roundedButton4;
        private Clases.RoundedButton roundedButton6;
        private Clases.RoundedButton btnCancelar;
        private Clases.RoundedButton btnActualizar;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private TabPage tabPageFormRenovacion;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {

        }

        private async void FrmRenovacion_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadMarcas());
            tabControl1.SelectedTab = tabPageRenovacionesList;
            EliminarTabPage(tabPageFormRenovacion);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ibtnEditar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
