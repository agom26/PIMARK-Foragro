namespace Presentacion
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelChildForm = new Panel();
            button2 = new Button();
            panelSubMenuPatentes = new Panel();
            btnAbandonadasPatentes = new Button();
            btnTramiteTraspPatentes = new Button();
            btnTramiteRenovPatentes = new Button();
            btnPatentesRegistradas = new Button();
            btnTramiteInicialPatente = new Button();
            btnIngresarPatente = new Button();
            panel2 = new Panel();
            button10 = new Button();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            panelSubMenuMarcasNacionales = new Panel();
            btnAbandonadas = new Button();
            btnTramiteTraspaso = new Button();
            btnTramiteRenovacion = new Button();
            btnRegistradas = new Button();
            btnOposiciones = new Button();
            btnEnTramite = new Button();
            btnTramiteInicial = new Button();
            panel3 = new Panel();
            button1 = new Button();
            button6 = new Button();
            panelSubMenuMarcasInter = new Panel();
            btnAbandonadasInter = new Button();
            btnTraspasoInter = new Button();
            btnRenovInter = new Button();
            btnRegInter = new Button();
            btnOpoInter = new Button();
            btnIngresadasInt = new Button();
            btnTramiteInicialInter = new Button();
            btnClientes = new Button();
            button5 = new Button();
            button4 = new Button();
            btnUsers = new Button();
            button3 = new Button();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            labelUsername = new Label();
            iconPictureBoxUser = new FontAwesome.Sharp.IconPictureBox();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton4 = new FontAwesome.Sharp.IconButton();
            panelSubMenuPatentes.SuspendLayout();
            panel2.SuspendLayout();
            panelSubMenuMarcasNacionales.SuspendLayout();
            panel3.SuspendLayout();
            panelSubMenuMarcasInter.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxUser).BeginInit();
            SuspendLayout();
            // 
            // panelChildForm
            // 
            panelChildForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelChildForm.AutoScroll = true;
            panelChildForm.AutoSize = true;
            panelChildForm.BackColor = Color.White;
            panelChildForm.Font = new Font("Century Gothic", 9F);
            panelChildForm.Location = new Point(287, 106);
            panelChildForm.Name = "panelChildForm";
            panelChildForm.Size = new Size(725, 594);
            panelChildForm.TabIndex = 1;
            panelChildForm.Paint += panelChildForm_Paint;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(34, 77, 112);
            button2.Dock = DockStyle.Top;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Image = Properties.Resources.guatemala;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(0, 333);
            button2.Name = "button2";
            button2.Size = new Size(259, 61);
            button2.TabIndex = 0;
            button2.Text = "      MARCAS NACIONALES";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click_2;
            // 
            // panelSubMenuPatentes
            // 
            panelSubMenuPatentes.BackColor = Color.FromArgb(196, 196, 208);
            panelSubMenuPatentes.Controls.Add(btnAbandonadasPatentes);
            panelSubMenuPatentes.Controls.Add(btnTramiteTraspPatentes);
            panelSubMenuPatentes.Controls.Add(btnTramiteRenovPatentes);
            panelSubMenuPatentes.Controls.Add(btnPatentesRegistradas);
            panelSubMenuPatentes.Controls.Add(btnTramiteInicialPatente);
            panelSubMenuPatentes.Controls.Add(btnIngresarPatente);
            panelSubMenuPatentes.Dock = DockStyle.Top;
            panelSubMenuPatentes.Font = new Font("Microsoft Sans Serif", 12F);
            panelSubMenuPatentes.Location = new Point(0, 1291);
            panelSubMenuPatentes.Name = "panelSubMenuPatentes";
            panelSubMenuPatentes.Size = new Size(259, 310);
            panelSubMenuPatentes.TabIndex = 12;
            panelSubMenuPatentes.Paint += panel5_Paint;
            // 
            // btnAbandonadasPatentes
            // 
            btnAbandonadasPatentes.AutoSize = true;
            btnAbandonadasPatentes.Dock = DockStyle.Top;
            btnAbandonadasPatentes.FlatAppearance.BorderSize = 0;
            btnAbandonadasPatentes.FlatStyle = FlatStyle.Flat;
            btnAbandonadasPatentes.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnAbandonadasPatentes.ForeColor = Color.Black;
            btnAbandonadasPatentes.Location = new Point(0, 235);
            btnAbandonadasPatentes.Name = "btnAbandonadasPatentes";
            btnAbandonadasPatentes.Padding = new Padding(58, 0, 0, 0);
            btnAbandonadasPatentes.Size = new Size(259, 47);
            btnAbandonadasPatentes.TabIndex = 6;
            btnAbandonadasPatentes.Text = "> ABANDONADAS";
            btnAbandonadasPatentes.TextAlign = ContentAlignment.MiddleLeft;
            btnAbandonadasPatentes.UseVisualStyleBackColor = true;
            btnAbandonadasPatentes.Click += btnAbandonadasPatentes_Click;
            // 
            // btnTramiteTraspPatentes
            // 
            btnTramiteTraspPatentes.AutoSize = true;
            btnTramiteTraspPatentes.Dock = DockStyle.Top;
            btnTramiteTraspPatentes.FlatAppearance.BorderSize = 0;
            btnTramiteTraspPatentes.FlatStyle = FlatStyle.Flat;
            btnTramiteTraspPatentes.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTramiteTraspPatentes.ForeColor = Color.Black;
            btnTramiteTraspPatentes.Location = new Point(0, 188);
            btnTramiteTraspPatentes.Name = "btnTramiteTraspPatentes";
            btnTramiteTraspPatentes.Padding = new Padding(58, 0, 0, 0);
            btnTramiteTraspPatentes.Size = new Size(259, 47);
            btnTramiteTraspPatentes.TabIndex = 4;
            btnTramiteTraspPatentes.Text = "> TR. DE TRASPASO";
            btnTramiteTraspPatentes.TextAlign = ContentAlignment.MiddleLeft;
            btnTramiteTraspPatentes.UseVisualStyleBackColor = true;
            btnTramiteTraspPatentes.Click += button34_Click;
            // 
            // btnTramiteRenovPatentes
            // 
            btnTramiteRenovPatentes.AutoSize = true;
            btnTramiteRenovPatentes.Dock = DockStyle.Top;
            btnTramiteRenovPatentes.FlatAppearance.BorderSize = 0;
            btnTramiteRenovPatentes.FlatStyle = FlatStyle.Flat;
            btnTramiteRenovPatentes.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTramiteRenovPatentes.ForeColor = Color.Black;
            btnTramiteRenovPatentes.Location = new Point(0, 141);
            btnTramiteRenovPatentes.Name = "btnTramiteRenovPatentes";
            btnTramiteRenovPatentes.Padding = new Padding(58, 0, 0, 0);
            btnTramiteRenovPatentes.Size = new Size(259, 47);
            btnTramiteRenovPatentes.TabIndex = 3;
            btnTramiteRenovPatentes.Text = "> TR. DE RENOVACIÓN";
            btnTramiteRenovPatentes.TextAlign = ContentAlignment.MiddleLeft;
            btnTramiteRenovPatentes.UseVisualStyleBackColor = true;
            btnTramiteRenovPatentes.Click += button35_Click;
            // 
            // btnPatentesRegistradas
            // 
            btnPatentesRegistradas.AutoSize = true;
            btnPatentesRegistradas.Dock = DockStyle.Top;
            btnPatentesRegistradas.FlatAppearance.BorderSize = 0;
            btnPatentesRegistradas.FlatStyle = FlatStyle.Flat;
            btnPatentesRegistradas.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnPatentesRegistradas.ForeColor = Color.Black;
            btnPatentesRegistradas.Location = new Point(0, 94);
            btnPatentesRegistradas.Name = "btnPatentesRegistradas";
            btnPatentesRegistradas.Padding = new Padding(58, 0, 0, 0);
            btnPatentesRegistradas.Size = new Size(259, 47);
            btnPatentesRegistradas.TabIndex = 5;
            btnPatentesRegistradas.Text = "> REGISTRADAS";
            btnPatentesRegistradas.TextAlign = ContentAlignment.MiddleLeft;
            btnPatentesRegistradas.UseVisualStyleBackColor = true;
            btnPatentesRegistradas.Click += button33_Click;
            // 
            // btnTramiteInicialPatente
            // 
            btnTramiteInicialPatente.AutoSize = true;
            btnTramiteInicialPatente.Dock = DockStyle.Top;
            btnTramiteInicialPatente.FlatAppearance.BorderSize = 0;
            btnTramiteInicialPatente.FlatStyle = FlatStyle.Flat;
            btnTramiteInicialPatente.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTramiteInicialPatente.ForeColor = Color.Black;
            btnTramiteInicialPatente.Location = new Point(0, 47);
            btnTramiteInicialPatente.Name = "btnTramiteInicialPatente";
            btnTramiteInicialPatente.Padding = new Padding(58, 0, 0, 0);
            btnTramiteInicialPatente.Size = new Size(259, 47);
            btnTramiteInicialPatente.TabIndex = 7;
            btnTramiteInicialPatente.Text = "> TRÁMITE INICIAL";
            btnTramiteInicialPatente.TextAlign = ContentAlignment.MiddleLeft;
            btnTramiteInicialPatente.UseVisualStyleBackColor = true;
            btnTramiteInicialPatente.Click += button3_Click;
            // 
            // btnIngresarPatente
            // 
            btnIngresarPatente.AutoSize = true;
            btnIngresarPatente.Dock = DockStyle.Top;
            btnIngresarPatente.FlatAppearance.BorderSize = 0;
            btnIngresarPatente.FlatStyle = FlatStyle.Flat;
            btnIngresarPatente.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnIngresarPatente.ForeColor = Color.Black;
            btnIngresarPatente.Location = new Point(0, 0);
            btnIngresarPatente.Name = "btnIngresarPatente";
            btnIngresarPatente.Padding = new Padding(58, 0, 0, 0);
            btnIngresarPatente.Size = new Size(259, 47);
            btnIngresarPatente.TabIndex = 0;
            btnIngresarPatente.Text = "> INGRESAR PATENTE";
            btnIngresarPatente.TextAlign = ContentAlignment.MiddleLeft;
            btnIngresarPatente.UseVisualStyleBackColor = true;
            btnIngresarPatente.Click += button36_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel2.AutoScroll = true;
            panel2.BackColor = Color.FromArgb(34, 77, 112);
            panel2.Controls.Add(button10);
            panel2.Controls.Add(button9);
            panel2.Controls.Add(button8);
            panel2.Controls.Add(panelSubMenuPatentes);
            panel2.Controls.Add(button7);
            panel2.Controls.Add(panelSubMenuMarcasNacionales);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(button6);
            panel2.Controls.Add(panelSubMenuMarcasInter);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(btnUsers);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(iconButton1);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(280, 700);
            panel2.TabIndex = 3;
            panel2.Paint += panel2_Paint;
            // 
            // button10
            // 
            button10.BackColor = Color.FromArgb(34, 77, 112);
            button10.Dock = DockStyle.Bottom;
            button10.FlatAppearance.BorderSize = 0;
            button10.FlatStyle = FlatStyle.Flat;
            button10.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button10.ForeColor = Color.White;
            button10.Image = Properties.Resources.cerrar_sesion_4_;
            button10.ImageAlign = ContentAlignment.MiddleLeft;
            button10.Location = new Point(0, 1723);
            button10.Name = "button10";
            button10.Size = new Size(259, 61);
            button10.TabIndex = 26;
            button10.Text = "    CERRAR SESIÓN";
            button10.TextImageRelation = TextImageRelation.ImageBeforeText;
            button10.UseVisualStyleBackColor = false;
            button10.Click += button10_Click;
            // 
            // button9
            // 
            button9.BackColor = Color.FromArgb(34, 77, 112);
            button9.Dock = DockStyle.Top;
            button9.FlatAppearance.BorderSize = 0;
            button9.FlatStyle = FlatStyle.Flat;
            button9.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button9.ForeColor = Color.White;
            button9.Image = Properties.Resources.reloj_y_calendario;
            button9.ImageAlign = ContentAlignment.MiddleLeft;
            button9.Location = new Point(0, 1662);
            button9.Name = "button9";
            button9.Size = new Size(259, 61);
            button9.TabIndex = 25;
            button9.Text = "    VENCIMIENTOS";
            button9.TextImageRelation = TextImageRelation.ImageBeforeText;
            button9.UseVisualStyleBackColor = false;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(34, 77, 112);
            button8.Dock = DockStyle.Top;
            button8.FlatAppearance.BorderSize = 0;
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button8.ForeColor = Color.White;
            button8.Image = Properties.Resources.grafico_simple_horizontal_1_;
            button8.ImageAlign = ContentAlignment.MiddleLeft;
            button8.Location = new Point(0, 1601);
            button8.Name = "button8";
            button8.Size = new Size(259, 61);
            button8.TabIndex = 24;
            button8.Text = "     REPORTES";
            button8.TextImageRelation = TextImageRelation.ImageBeforeText;
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click_1;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(34, 77, 112);
            button7.Dock = DockStyle.Top;
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button7.ForeColor = Color.White;
            button7.Image = Properties.Resources.bahai;
            button7.ImageAlign = ContentAlignment.MiddleLeft;
            button7.Location = new Point(0, 1230);
            button7.Name = "button7";
            button7.Size = new Size(259, 61);
            button7.TabIndex = 23;
            button7.Text = "     PATENTES";
            button7.TextImageRelation = TextImageRelation.ImageBeforeText;
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click_1;
            // 
            // panelSubMenuMarcasNacionales
            // 
            panelSubMenuMarcasNacionales.BackColor = Color.FromArgb(196, 196, 208);
            panelSubMenuMarcasNacionales.Controls.Add(btnAbandonadas);
            panelSubMenuMarcasNacionales.Controls.Add(btnTramiteTraspaso);
            panelSubMenuMarcasNacionales.Controls.Add(btnTramiteRenovacion);
            panelSubMenuMarcasNacionales.Controls.Add(btnRegistradas);
            panelSubMenuMarcasNacionales.Controls.Add(btnOposiciones);
            panelSubMenuMarcasNacionales.Controls.Add(btnEnTramite);
            panelSubMenuMarcasNacionales.Controls.Add(btnTramiteInicial);
            panelSubMenuMarcasNacionales.Dock = DockStyle.Top;
            panelSubMenuMarcasNacionales.Font = new Font("Microsoft Sans Serif", 12F);
            panelSubMenuMarcasNacionales.Location = new Point(0, 884);
            panelSubMenuMarcasNacionales.Name = "panelSubMenuMarcasNacionales";
            panelSubMenuMarcasNacionales.Size = new Size(259, 346);
            panelSubMenuMarcasNacionales.TabIndex = 8;
            // 
            // btnAbandonadas
            // 
            btnAbandonadas.AutoSize = true;
            btnAbandonadas.BackColor = Color.FromArgb(196, 196, 208);
            btnAbandonadas.Dock = DockStyle.Top;
            btnAbandonadas.FlatAppearance.BorderSize = 0;
            btnAbandonadas.FlatStyle = FlatStyle.Flat;
            btnAbandonadas.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnAbandonadas.ForeColor = Color.Black;
            btnAbandonadas.Location = new Point(0, 282);
            btnAbandonadas.Margin = new Padding(0);
            btnAbandonadas.Name = "btnAbandonadas";
            btnAbandonadas.Padding = new Padding(58, 0, 0, 0);
            btnAbandonadas.Size = new Size(259, 47);
            btnAbandonadas.TabIndex = 5;
            btnAbandonadas.Text = "> ABANDONADAS";
            btnAbandonadas.TextAlign = ContentAlignment.MiddleLeft;
            btnAbandonadas.UseVisualStyleBackColor = false;
            btnAbandonadas.Click += btnAbandonadas_Click;
            // 
            // btnTramiteTraspaso
            // 
            btnTramiteTraspaso.AutoSize = true;
            btnTramiteTraspaso.BackColor = Color.FromArgb(196, 196, 208);
            btnTramiteTraspaso.Dock = DockStyle.Top;
            btnTramiteTraspaso.FlatAppearance.BorderSize = 0;
            btnTramiteTraspaso.FlatStyle = FlatStyle.Flat;
            btnTramiteTraspaso.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTramiteTraspaso.ForeColor = Color.Black;
            btnTramiteTraspaso.Location = new Point(0, 235);
            btnTramiteTraspaso.Name = "btnTramiteTraspaso";
            btnTramiteTraspaso.Padding = new Padding(58, 0, 0, 0);
            btnTramiteTraspaso.Size = new Size(259, 47);
            btnTramiteTraspaso.TabIndex = 4;
            btnTramiteTraspaso.Text = "> TR. DE TRASPASO";
            btnTramiteTraspaso.TextAlign = ContentAlignment.MiddleLeft;
            btnTramiteTraspaso.UseVisualStyleBackColor = false;
            btnTramiteTraspaso.Click += button22_Click;
            // 
            // btnTramiteRenovacion
            // 
            btnTramiteRenovacion.AutoSize = true;
            btnTramiteRenovacion.BackColor = Color.FromArgb(196, 196, 208);
            btnTramiteRenovacion.Dock = DockStyle.Top;
            btnTramiteRenovacion.FlatAppearance.BorderSize = 0;
            btnTramiteRenovacion.FlatStyle = FlatStyle.Flat;
            btnTramiteRenovacion.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTramiteRenovacion.ForeColor = Color.Black;
            btnTramiteRenovacion.Location = new Point(0, 188);
            btnTramiteRenovacion.Margin = new Padding(0);
            btnTramiteRenovacion.Name = "btnTramiteRenovacion";
            btnTramiteRenovacion.Padding = new Padding(58, 0, 0, 0);
            btnTramiteRenovacion.Size = new Size(259, 47);
            btnTramiteRenovacion.TabIndex = 3;
            btnTramiteRenovacion.Text = "> TR. DE RENOVACIÓN";
            btnTramiteRenovacion.TextAlign = ContentAlignment.MiddleLeft;
            btnTramiteRenovacion.UseVisualStyleBackColor = false;
            btnTramiteRenovacion.Click += button23_Click;
            // 
            // btnRegistradas
            // 
            btnRegistradas.AutoSize = true;
            btnRegistradas.BackColor = Color.FromArgb(196, 196, 208);
            btnRegistradas.Dock = DockStyle.Top;
            btnRegistradas.FlatAppearance.BorderSize = 0;
            btnRegistradas.FlatStyle = FlatStyle.Flat;
            btnRegistradas.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnRegistradas.ForeColor = Color.Black;
            btnRegistradas.Location = new Point(0, 141);
            btnRegistradas.Name = "btnRegistradas";
            btnRegistradas.Padding = new Padding(58, 0, 0, 0);
            btnRegistradas.Size = new Size(259, 47);
            btnRegistradas.TabIndex = 2;
            btnRegistradas.Text = "> REGISTRADAS";
            btnRegistradas.TextAlign = ContentAlignment.MiddleLeft;
            btnRegistradas.UseVisualStyleBackColor = false;
            btnRegistradas.Click += btnRegistradas_Click;
            // 
            // btnOposiciones
            // 
            btnOposiciones.AutoSize = true;
            btnOposiciones.BackColor = Color.FromArgb(196, 196, 208);
            btnOposiciones.Dock = DockStyle.Top;
            btnOposiciones.FlatAppearance.BorderSize = 0;
            btnOposiciones.FlatStyle = FlatStyle.Flat;
            btnOposiciones.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnOposiciones.ForeColor = Color.Black;
            btnOposiciones.Location = new Point(0, 94);
            btnOposiciones.Margin = new Padding(0);
            btnOposiciones.Name = "btnOposiciones";
            btnOposiciones.Padding = new Padding(58, 0, 0, 0);
            btnOposiciones.Size = new Size(259, 47);
            btnOposiciones.TabIndex = 1;
            btnOposiciones.Text = "> OPOSICIONES";
            btnOposiciones.TextAlign = ContentAlignment.MiddleLeft;
            btnOposiciones.UseVisualStyleBackColor = false;
            btnOposiciones.Click += btnOposiciones_Click;
            // 
            // btnEnTramite
            // 
            btnEnTramite.AutoSize = true;
            btnEnTramite.BackColor = Color.FromArgb(196, 196, 208);
            btnEnTramite.Dock = DockStyle.Top;
            btnEnTramite.FlatAppearance.BorderSize = 0;
            btnEnTramite.FlatStyle = FlatStyle.Flat;
            btnEnTramite.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnEnTramite.ForeColor = Color.Black;
            btnEnTramite.Location = new Point(0, 47);
            btnEnTramite.Margin = new Padding(0);
            btnEnTramite.Name = "btnEnTramite";
            btnEnTramite.Padding = new Padding(58, 0, 0, 0);
            btnEnTramite.Size = new Size(259, 47);
            btnEnTramite.TabIndex = 6;
            btnEnTramite.Text = "> TRÁMITE INICIAL";
            btnEnTramite.TextAlign = ContentAlignment.MiddleLeft;
            btnEnTramite.UseVisualStyleBackColor = false;
            btnEnTramite.Click += button4_Click;
            // 
            // btnTramiteInicial
            // 
            btnTramiteInicial.AutoSize = true;
            btnTramiteInicial.BackColor = Color.FromArgb(196, 196, 208);
            btnTramiteInicial.Dock = DockStyle.Top;
            btnTramiteInicial.FlatAppearance.BorderSize = 0;
            btnTramiteInicial.FlatStyle = FlatStyle.Flat;
            btnTramiteInicial.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTramiteInicial.ForeColor = Color.Black;
            btnTramiteInicial.Location = new Point(0, 0);
            btnTramiteInicial.Margin = new Padding(0);
            btnTramiteInicial.Name = "btnTramiteInicial";
            btnTramiteInicial.Padding = new Padding(58, 0, 0, 0);
            btnTramiteInicial.Size = new Size(259, 47);
            btnTramiteInicial.TabIndex = 0;
            btnTramiteInicial.Text = "> INGRESAR MARCA";
            btnTramiteInicial.TextAlign = ContentAlignment.MiddleLeft;
            btnTramiteInicial.UseVisualStyleBackColor = false;
            btnTramiteInicial.Click += button26_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(button1);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 1784);
            panel3.Name = "panel3";
            panel3.Size = new Size(259, 54);
            panel3.TabIndex = 18;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(64, 100, 133);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(3, 10);
            button1.Name = "button1";
            button1.Size = new Size(276, 29);
            button1.TabIndex = 0;
            button1.Text = "Desarrollado por Sitios en Red";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_2;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(34, 77, 112);
            button6.Dock = DockStyle.Top;
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button6.ForeColor = Color.White;
            button6.Image = (Image)resources.GetObject("button6.Image");
            button6.ImageAlign = ContentAlignment.MiddleLeft;
            button6.Location = new Point(0, 823);
            button6.Name = "button6";
            button6.Size = new Size(259, 61);
            button6.TabIndex = 22;
            button6.Text = "     M. INTERNACIONALES";
            button6.TextAlign = ContentAlignment.MiddleRight;
            button6.TextImageRelation = TextImageRelation.ImageBeforeText;
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click_2;
            // 
            // panelSubMenuMarcasInter
            // 
            panelSubMenuMarcasInter.BackColor = Color.FromArgb(196, 196, 208);
            panelSubMenuMarcasInter.Controls.Add(btnAbandonadasInter);
            panelSubMenuMarcasInter.Controls.Add(btnTraspasoInter);
            panelSubMenuMarcasInter.Controls.Add(btnRenovInter);
            panelSubMenuMarcasInter.Controls.Add(btnRegInter);
            panelSubMenuMarcasInter.Controls.Add(btnOpoInter);
            panelSubMenuMarcasInter.Controls.Add(btnIngresadasInt);
            panelSubMenuMarcasInter.Controls.Add(btnTramiteInicialInter);
            panelSubMenuMarcasInter.Controls.Add(btnClientes);
            panelSubMenuMarcasInter.Dock = DockStyle.Top;
            panelSubMenuMarcasInter.Font = new Font("Microsoft Sans Serif", 12F);
            panelSubMenuMarcasInter.Location = new Point(0, 394);
            panelSubMenuMarcasInter.Name = "panelSubMenuMarcasInter";
            panelSubMenuMarcasInter.Size = new Size(259, 429);
            panelSubMenuMarcasInter.TabIndex = 10;
            // 
            // btnAbandonadasInter
            // 
            btnAbandonadasInter.AutoSize = true;
            btnAbandonadasInter.Dock = DockStyle.Top;
            btnAbandonadasInter.FlatAppearance.BorderSize = 0;
            btnAbandonadasInter.FlatStyle = FlatStyle.Flat;
            btnAbandonadasInter.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnAbandonadasInter.ForeColor = Color.Black;
            btnAbandonadasInter.Location = new Point(0, 355);
            btnAbandonadasInter.Name = "btnAbandonadasInter";
            btnAbandonadasInter.Padding = new Padding(58, 0, 0, 0);
            btnAbandonadasInter.Size = new Size(259, 47);
            btnAbandonadasInter.TabIndex = 9;
            btnAbandonadasInter.Text = "> ABANDONADAS";
            btnAbandonadasInter.TextAlign = ContentAlignment.MiddleLeft;
            btnAbandonadasInter.UseVisualStyleBackColor = true;
            btnAbandonadasInter.Click += button2_Click_1;
            // 
            // btnTraspasoInter
            // 
            btnTraspasoInter.AutoSize = true;
            btnTraspasoInter.Dock = DockStyle.Top;
            btnTraspasoInter.FlatAppearance.BorderSize = 0;
            btnTraspasoInter.FlatStyle = FlatStyle.Flat;
            btnTraspasoInter.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTraspasoInter.ForeColor = Color.Black;
            btnTraspasoInter.Location = new Point(0, 308);
            btnTraspasoInter.Name = "btnTraspasoInter";
            btnTraspasoInter.Padding = new Padding(58, 0, 0, 0);
            btnTraspasoInter.Size = new Size(259, 47);
            btnTraspasoInter.TabIndex = 5;
            btnTraspasoInter.Text = "> TR. DE TRASPASO\r\n";
            btnTraspasoInter.TextAlign = ContentAlignment.MiddleLeft;
            btnTraspasoInter.UseVisualStyleBackColor = true;
            btnTraspasoInter.Click += btnTraspasoInter_Click;
            // 
            // btnRenovInter
            // 
            btnRenovInter.AutoSize = true;
            btnRenovInter.Dock = DockStyle.Top;
            btnRenovInter.FlatAppearance.BorderSize = 0;
            btnRenovInter.FlatStyle = FlatStyle.Flat;
            btnRenovInter.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnRenovInter.ForeColor = Color.Black;
            btnRenovInter.Location = new Point(0, 248);
            btnRenovInter.Name = "btnRenovInter";
            btnRenovInter.Padding = new Padding(58, 0, 0, 0);
            btnRenovInter.Size = new Size(259, 60);
            btnRenovInter.TabIndex = 7;
            btnRenovInter.Text = "> TR. DE RENOVACIÓN";
            btnRenovInter.TextAlign = ContentAlignment.MiddleLeft;
            btnRenovInter.UseVisualStyleBackColor = true;
            btnRenovInter.Click += btnRenovInter_Click;
            // 
            // btnRegInter
            // 
            btnRegInter.AutoSize = true;
            btnRegInter.Dock = DockStyle.Top;
            btnRegInter.FlatAppearance.BorderSize = 0;
            btnRegInter.FlatStyle = FlatStyle.Flat;
            btnRegInter.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnRegInter.ForeColor = Color.Black;
            btnRegInter.Location = new Point(0, 201);
            btnRegInter.Name = "btnRegInter";
            btnRegInter.Padding = new Padding(58, 0, 0, 0);
            btnRegInter.Size = new Size(259, 47);
            btnRegInter.TabIndex = 11;
            btnRegInter.Text = "> REGISTRADAS";
            btnRegInter.TextAlign = ContentAlignment.MiddleLeft;
            btnRegInter.UseVisualStyleBackColor = true;
            btnRegInter.Click += button6_Click_1;
            // 
            // btnOpoInter
            // 
            btnOpoInter.AutoSize = true;
            btnOpoInter.Dock = DockStyle.Top;
            btnOpoInter.FlatAppearance.BorderSize = 0;
            btnOpoInter.FlatStyle = FlatStyle.Flat;
            btnOpoInter.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnOpoInter.ForeColor = Color.Black;
            btnOpoInter.Location = new Point(0, 141);
            btnOpoInter.Name = "btnOpoInter";
            btnOpoInter.Padding = new Padding(58, 0, 0, 0);
            btnOpoInter.Size = new Size(259, 60);
            btnOpoInter.TabIndex = 8;
            btnOpoInter.Text = "> OPOSICIONES";
            btnOpoInter.TextAlign = ContentAlignment.MiddleLeft;
            btnOpoInter.UseVisualStyleBackColor = true;
            btnOpoInter.Click += button1_Click_1;
            // 
            // btnIngresadasInt
            // 
            btnIngresadasInt.AutoSize = true;
            btnIngresadasInt.Dock = DockStyle.Top;
            btnIngresadasInt.FlatAppearance.BorderSize = 0;
            btnIngresadasInt.FlatStyle = FlatStyle.Flat;
            btnIngresadasInt.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnIngresadasInt.ForeColor = Color.Black;
            btnIngresadasInt.Location = new Point(0, 94);
            btnIngresadasInt.Name = "btnIngresadasInt";
            btnIngresadasInt.Padding = new Padding(58, 0, 0, 0);
            btnIngresadasInt.Size = new Size(259, 47);
            btnIngresadasInt.TabIndex = 10;
            btnIngresadasInt.Text = "> TRÁMITE INICIAL";
            btnIngresadasInt.TextAlign = ContentAlignment.MiddleLeft;
            btnIngresadasInt.UseVisualStyleBackColor = true;
            btnIngresadasInt.Click += button5_Click_1;
            // 
            // btnTramiteInicialInter
            // 
            btnTramiteInicialInter.AutoSize = true;
            btnTramiteInicialInter.Dock = DockStyle.Top;
            btnTramiteInicialInter.FlatAppearance.BorderSize = 0;
            btnTramiteInicialInter.FlatStyle = FlatStyle.Flat;
            btnTramiteInicialInter.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTramiteInicialInter.ForeColor = Color.Black;
            btnTramiteInicialInter.Location = new Point(0, 47);
            btnTramiteInicialInter.Name = "btnTramiteInicialInter";
            btnTramiteInicialInter.Padding = new Padding(58, 0, 0, 0);
            btnTramiteInicialInter.Size = new Size(259, 47);
            btnTramiteInicialInter.TabIndex = 4;
            btnTramiteInicialInter.Text = "> INGRESAR MARCA";
            btnTramiteInicialInter.TextAlign = ContentAlignment.MiddleLeft;
            btnTramiteInicialInter.UseVisualStyleBackColor = true;
            btnTramiteInicialInter.Click += button29_Click;
            // 
            // btnClientes
            // 
            btnClientes.AutoSize = true;
            btnClientes.Dock = DockStyle.Top;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnClientes.ForeColor = Color.Black;
            btnClientes.Location = new Point(0, 0);
            btnClientes.Name = "btnClientes";
            btnClientes.Padding = new Padding(58, 0, 0, 0);
            btnClientes.Size = new Size(259, 47);
            btnClientes.TabIndex = 3;
            btnClientes.Text = "> CLIENTES";
            btnClientes.TextAlign = ContentAlignment.MiddleLeft;
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += button30_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(34, 77, 112);
            button5.Dock = DockStyle.Top;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button5.ForeColor = Color.White;
            button5.Image = Properties.Resources.legal;
            button5.ImageAlign = ContentAlignment.MiddleLeft;
            button5.Location = new Point(0, 272);
            button5.Name = "button5";
            button5.Size = new Size(259, 61);
            button5.TabIndex = 21;
            button5.Text = "     TITULARES";
            button5.TextImageRelation = TextImageRelation.ImageBeforeText;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click_2;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(34, 77, 112);
            button4.Dock = DockStyle.Top;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button4.ForeColor = Color.White;
            button4.Image = Properties.Resources.empresarios_1_;
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.Location = new Point(0, 211);
            button4.Name = "button4";
            button4.Size = new Size(259, 61);
            button4.TabIndex = 20;
            button4.Text = "     AGENTES";
            button4.TextImageRelation = TextImageRelation.ImageBeforeText;
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click_2;
            // 
            // btnUsers
            // 
            btnUsers.BackColor = Color.FromArgb(34, 77, 112);
            btnUsers.Dock = DockStyle.Top;
            btnUsers.FlatAppearance.BorderSize = 0;
            btnUsers.FlatStyle = FlatStyle.Flat;
            btnUsers.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnUsers.ForeColor = Color.White;
            btnUsers.Image = Properties.Resources.usuario_final;
            btnUsers.ImageAlign = ContentAlignment.MiddleLeft;
            btnUsers.Location = new Point(0, 150);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(259, 61);
            btnUsers.TabIndex = 19;
            btnUsers.Text = "     USUARIOS";
            btnUsers.TextAlign = ContentAlignment.MiddleLeft;
            btnUsers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUsers.UseVisualStyleBackColor = false;
            btnUsers.Click += button4_Click_1;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(34, 77, 112);
            button3.Dock = DockStyle.Top;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button3.ForeColor = Color.White;
            button3.Image = Properties.Resources.casa_silueta_negra_sin_puerta;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Location = new Point(0, 89);
            button3.Name = "button3";
            button3.Size = new Size(259, 61);
            button3.TabIndex = 5;
            button3.Text = "     INICIO";
            button3.TextAlign = ContentAlignment.MiddleRight;
            button3.TextImageRelation = TextImageRelation.ImageBeforeText;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click_1;
            // 
            // iconButton1
            // 
            iconButton1.AutoSize = true;
            iconButton1.BackColor = Color.FromArgb(34, 77, 112);
            iconButton1.BackgroundImage = (Image)resources.GetObject("iconButton1.BackgroundImage");
            iconButton1.BackgroundImageLayout = ImageLayout.Stretch;
            iconButton1.Dock = DockStyle.Top;
            iconButton1.Enabled = false;
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 9F);
            iconButton1.ForeColor = Color.Gainsboro;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            iconButton1.IconColor = Color.FromArgb(230, 148, 226);
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton1.Location = new Point(0, 0);
            iconButton1.Margin = new Padding(0);
            iconButton1.Name = "iconButton1";
            iconButton1.Padding = new Padding(9, 0, 0, 0);
            iconButton1.Size = new Size(259, 89);
            iconButton1.TabIndex = 16;
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = false;
            iconButton1.Click += iconButton1_Click_1;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(34, 77, 112);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(iconButton2);
            panel1.Controls.Add(labelUsername);
            panel1.Controls.Add(iconPictureBoxUser);
            panel1.Controls.Add(iconButton3);
            panel1.Controls.Add(iconButton4);
            panel1.Location = new Point(280, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(744, 89);
            panel1.TabIndex = 4;
            panel1.Paint += panel1_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(33, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(72, 52);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            iconButton2.IconColor = Color.White;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 25;
            iconButton2.Location = new Point(625, 0);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(55, 64);
            iconButton2.TabIndex = 4;
            iconButton2.UseVisualStyleBackColor = true;
            iconButton2.Click += iconButton2_Click_1;
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Font = new Font("Century Gothic", 10F);
            labelUsername.ForeColor = Color.White;
            labelUsername.Location = new Point(177, 34);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(60, 21);
            labelUsername.TabIndex = 3;
            labelUsername.Text = "label1";
            labelUsername.Click += labelUsername_Click;
            // 
            // iconPictureBoxUser
            // 
            iconPictureBoxUser.BackColor = Color.FromArgb(34, 77, 112);
            iconPictureBoxUser.IconChar = FontAwesome.Sharp.IconChar.UserCircle;
            iconPictureBoxUser.IconColor = Color.White;
            iconPictureBoxUser.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBoxUser.IconSize = 59;
            iconPictureBoxUser.Location = new Point(111, 14);
            iconPictureBoxUser.Name = "iconPictureBoxUser";
            iconPictureBoxUser.Size = new Size(60, 59);
            iconPictureBoxUser.TabIndex = 1;
            iconPictureBoxUser.TabStop = false;
            // 
            // iconButton3
            // 
            iconButton3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.X;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 25;
            iconButton3.Location = new Point(686, -1);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(55, 65);
            iconButton3.TabIndex = 0;
            iconButton3.UseVisualStyleBackColor = true;
            iconButton3.Click += iconButton3_Click;
            // 
            // iconButton4
            // 
            iconButton4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconButton4.FlatAppearance.BorderSize = 0;
            iconButton4.FlatStyle = FlatStyle.Flat;
            iconButton4.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            iconButton4.IconColor = Color.White;
            iconButton4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton4.IconSize = 25;
            iconButton4.Location = new Point(564, 2);
            iconButton4.Name = "iconButton4";
            iconButton4.Size = new Size(55, 62);
            iconButton4.TabIndex = 5;
            iconButton4.UseVisualStyleBackColor = true;
            iconButton4.Click += iconButton4_Click_1;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            ClientSize = new Size(1024, 700);
            ControlBox = false;
            Controls.Add(panel1);
            Controls.Add(panelChildForm);
            Controls.Add(panel2);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MinimumSize = new Size(1022, 700);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PIMARK";
            Load += Form1_Load;
            ResizeEnd += Form1_ResizeEnd;
            Resize += Form1_Resize;
            panelSubMenuPatentes.ResumeLayout(false);
            panelSubMenuPatentes.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panelSubMenuMarcasNacionales.ResumeLayout(false);
            panelSubMenuMarcasNacionales.PerformLayout();
            panel3.ResumeLayout(false);
            panelSubMenuMarcasInter.ResumeLayout(false);
            panelSubMenuMarcasInter.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxUser).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelSubMenuMarcasNacionales;
        private System.Windows.Forms.Button btnTramiteTraspaso;
        private System.Windows.Forms.Button btnTramiteRenovacion;
        private System.Windows.Forms.Button btnRegistradas;
        private System.Windows.Forms.Button btnOposiciones;
        private System.Windows.Forms.Button btnTramiteInicial;
        private System.Windows.Forms.Panel panelSubMenuPatentes;
        private System.Windows.Forms.Button btnAbandonadasPatentes;
        private System.Windows.Forms.Button btnPatentesRegistradas;
        private System.Windows.Forms.Button btnTramiteTraspPatentes;
        private System.Windows.Forms.Button btnTramiteRenovPatentes;
        private System.Windows.Forms.Button btnIngresarPatente;
        private System.Windows.Forms.Panel panelSubMenuMarcasInter;
        private System.Windows.Forms.Button btnRenovInter;
        private System.Windows.Forms.Button btnTraspasoInter;
        private System.Windows.Forms.Button btnTramiteInicialInter;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Panel panel1;
        private Button btnOpoInter;
        private Button btnAbandonadasInter;
        private Button btnAbandonadas;
        private Button btnRegInter;
        private Button btnIngresadasInt;
        private FontAwesome.Sharp.IconButton iconButton3;
        private Label labelUsername;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxUser;
        private FontAwesome.Sharp.IconButton iconButton4;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private Button btnEnTramite;
        private Button btnTramiteInicialPatente;
        private PictureBox pictureBox1;
        private Panel panel3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button btnUsers;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
    }
}

