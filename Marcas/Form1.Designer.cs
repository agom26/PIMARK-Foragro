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
            iconButtonLogout = new FontAwesome.Sharp.IconButton();
            panelSubMenuPatentes = new Panel();
            button32 = new Button();
            button33 = new Button();
            button34 = new Button();
            button35 = new Button();
            button36 = new Button();
            panel2 = new Panel();
            iconButtonVencimientos = new FontAwesome.Sharp.IconButton();
            btnReportes = new FontAwesome.Sharp.IconButton();
            btnPatentes = new FontAwesome.Sharp.IconButton();
            panelSubMenuMarcasInter = new Panel();
            button6 = new Button();
            button5 = new Button();
            button2 = new Button();
            button1 = new Button();
            button27 = new Button();
            button28 = new Button();
            button29 = new Button();
            button30 = new Button();
            iconButton12 = new FontAwesome.Sharp.IconButton();
            panelSubMenuMarcasNacionales = new Panel();
            btnAbandonadas = new Button();
            btnTramiteRenov = new Button();
            btnTramiteRenovacion = new Button();
            btnRegistradas = new Button();
            btnOposiciones = new Button();
            btnEnTramite = new Button();
            btnTramiteInicial = new Button();
            iconButton10 = new FontAwesome.Sharp.IconButton();
            iconButtonTitulares = new FontAwesome.Sharp.IconButton();
            iconButtonAgentes = new FontAwesome.Sharp.IconButton();
            iconButtonUsuarios = new FontAwesome.Sharp.IconButton();
            panel3 = new Panel();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            label1 = new Label();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            labelUsername = new Label();
            labelName_LN = new Label();
            iconPictureBoxUser = new FontAwesome.Sharp.IconPictureBox();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton4 = new FontAwesome.Sharp.IconButton();
            panelSubMenuPatentes.SuspendLayout();
            panel2.SuspendLayout();
            panelSubMenuMarcasInter.SuspendLayout();
            panelSubMenuMarcasNacionales.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxUser).BeginInit();
            SuspendLayout();
            // 
            // panelChildForm
            // 
            panelChildForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            panelChildForm.AutoScroll = true;
            panelChildForm.AutoSize = true;
            panelChildForm.BackColor = Color.White;
            panelChildForm.Font = new Font("Century Gothic", 9F);
            panelChildForm.Location = new Point(285, 106);
            panelChildForm.Name = "panelChildForm";
            panelChildForm.Size = new Size(1184, 794);
            panelChildForm.TabIndex = 1;
            panelChildForm.Paint += panelChildForm_Paint;
            // 
            // iconButtonLogout
            // 
            iconButtonLogout.AutoSize = true;
            iconButtonLogout.BackColor = Color.FromArgb(34, 77, 112);
            iconButtonLogout.Dock = DockStyle.Bottom;
            iconButtonLogout.FlatAppearance.BorderSize = 0;
            iconButtonLogout.FlatStyle = FlatStyle.Flat;
            iconButtonLogout.Font = new Font("Century Gothic", 9F);
            iconButtonLogout.ForeColor = Color.Gainsboro;
            iconButtonLogout.IconChar = FontAwesome.Sharp.IconChar.RightToBracket;
            iconButtonLogout.IconColor = Color.FromArgb(211, 47, 47);
            iconButtonLogout.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonLogout.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonLogout.Location = new Point(0, 1636);
            iconButtonLogout.Name = "iconButtonLogout";
            iconButtonLogout.Padding = new Padding(9, 0, 0, 0);
            iconButtonLogout.Size = new Size(259, 54);
            iconButtonLogout.TabIndex = 14;
            iconButtonLogout.Text = "Cerrar sesión";
            iconButtonLogout.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButtonLogout.UseVisualStyleBackColor = false;
            iconButtonLogout.Click += iconButton2_Click;
            // 
            // panelSubMenuPatentes
            // 
            panelSubMenuPatentes.BackColor = Color.FromArgb(196, 196, 208);
            panelSubMenuPatentes.Controls.Add(button32);
            panelSubMenuPatentes.Controls.Add(button33);
            panelSubMenuPatentes.Controls.Add(button34);
            panelSubMenuPatentes.Controls.Add(button35);
            panelSubMenuPatentes.Controls.Add(button36);
            panelSubMenuPatentes.Dock = DockStyle.Top;
            panelSubMenuPatentes.Font = new Font("Microsoft Sans Serif", 12F);
            panelSubMenuPatentes.Location = new Point(0, 1260);
            panelSubMenuPatentes.Name = "panelSubMenuPatentes";
            panelSubMenuPatentes.Size = new Size(259, 268);
            panelSubMenuPatentes.TabIndex = 12;
            panelSubMenuPatentes.Paint += panel5_Paint;
            // 
            // button32
            // 
            button32.AutoSize = true;
            button32.Dock = DockStyle.Top;
            button32.FlatAppearance.BorderSize = 0;
            button32.FlatStyle = FlatStyle.Flat;
            button32.Font = new Font("Century Gothic", 9F);
            button32.ForeColor = Color.Black;
            button32.Location = new Point(0, 188);
            button32.Name = "button32";
            button32.Padding = new Padding(58, 0, 0, 0);
            button32.Size = new Size(259, 47);
            button32.TabIndex = 6;
            button32.Text = "Abandonadas";
            button32.TextAlign = ContentAlignment.MiddleLeft;
            button32.UseVisualStyleBackColor = true;
            // 
            // button33
            // 
            button33.AutoSize = true;
            button33.Dock = DockStyle.Top;
            button33.FlatAppearance.BorderSize = 0;
            button33.FlatStyle = FlatStyle.Flat;
            button33.Font = new Font("Century Gothic", 9F);
            button33.ForeColor = Color.Black;
            button33.Location = new Point(0, 141);
            button33.Name = "button33";
            button33.Padding = new Padding(58, 0, 0, 0);
            button33.Size = new Size(259, 47);
            button33.TabIndex = 5;
            button33.Text = "Oposiciones";
            button33.TextAlign = ContentAlignment.MiddleLeft;
            button33.UseVisualStyleBackColor = true;
            // 
            // button34
            // 
            button34.AutoSize = true;
            button34.Dock = DockStyle.Top;
            button34.FlatAppearance.BorderSize = 0;
            button34.FlatStyle = FlatStyle.Flat;
            button34.Font = new Font("Century Gothic", 9F);
            button34.ForeColor = Color.Black;
            button34.Location = new Point(0, 94);
            button34.Name = "button34";
            button34.Padding = new Padding(58, 0, 0, 0);
            button34.Size = new Size(259, 47);
            button34.TabIndex = 4;
            button34.Text = "Trámite de traspaso";
            button34.TextAlign = ContentAlignment.MiddleLeft;
            button34.UseVisualStyleBackColor = true;
            button34.Click += button34_Click;
            // 
            // button35
            // 
            button35.AutoSize = true;
            button35.Dock = DockStyle.Top;
            button35.FlatAppearance.BorderSize = 0;
            button35.FlatStyle = FlatStyle.Flat;
            button35.Font = new Font("Century Gothic", 9F);
            button35.ForeColor = Color.Black;
            button35.Location = new Point(0, 47);
            button35.Name = "button35";
            button35.Padding = new Padding(58, 0, 0, 0);
            button35.Size = new Size(259, 47);
            button35.TabIndex = 3;
            button35.Text = "Trámite de renovación";
            button35.TextAlign = ContentAlignment.MiddleLeft;
            button35.UseVisualStyleBackColor = true;
            button35.Click += button35_Click;
            // 
            // button36
            // 
            button36.AutoSize = true;
            button36.Dock = DockStyle.Top;
            button36.FlatAppearance.BorderSize = 0;
            button36.FlatStyle = FlatStyle.Flat;
            button36.Font = new Font("Century Gothic", 9F);
            button36.ForeColor = Color.Black;
            button36.Location = new Point(0, 0);
            button36.Name = "button36";
            button36.Padding = new Padding(58, 0, 0, 0);
            button36.Size = new Size(259, 47);
            button36.TabIndex = 0;
            button36.Text = "Trámite inicial";
            button36.TextAlign = ContentAlignment.MiddleLeft;
            button36.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.BackColor = Color.FromArgb(34, 77, 112);
            panel2.Controls.Add(iconButtonLogout);
            panel2.Controls.Add(iconButtonVencimientos);
            panel2.Controls.Add(btnReportes);
            panel2.Controls.Add(panelSubMenuPatentes);
            panel2.Controls.Add(btnPatentes);
            panel2.Controls.Add(panelSubMenuMarcasInter);
            panel2.Controls.Add(iconButton12);
            panel2.Controls.Add(panelSubMenuMarcasNacionales);
            panel2.Controls.Add(iconButton10);
            panel2.Controls.Add(iconButtonTitulares);
            panel2.Controls.Add(iconButtonAgentes);
            panel2.Controls.Add(iconButtonUsuarios);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(iconButton1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(280, 900);
            panel2.TabIndex = 3;
            panel2.Paint += panel2_Paint;
            // 
            // iconButtonVencimientos
            // 
            iconButtonVencimientos.AutoSize = true;
            iconButtonVencimientos.BackColor = Color.FromArgb(34, 77, 112);
            iconButtonVencimientos.Dock = DockStyle.Top;
            iconButtonVencimientos.FlatAppearance.BorderSize = 0;
            iconButtonVencimientos.FlatStyle = FlatStyle.Flat;
            iconButtonVencimientos.Font = new Font("Century Gothic", 9F);
            iconButtonVencimientos.ForeColor = Color.Gainsboro;
            iconButtonVencimientos.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            iconButtonVencimientos.IconColor = Color.Red;
            iconButtonVencimientos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonVencimientos.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonVencimientos.Location = new Point(0, 1582);
            iconButtonVencimientos.Name = "iconButtonVencimientos";
            iconButtonVencimientos.Padding = new Padding(9, 0, 0, 0);
            iconButtonVencimientos.Size = new Size(259, 54);
            iconButtonVencimientos.TabIndex = 15;
            iconButtonVencimientos.Text = "Vencimientos";
            iconButtonVencimientos.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButtonVencimientos.UseVisualStyleBackColor = false;
            iconButtonVencimientos.Click += iconButton5_Click_1;
            // 
            // btnReportes
            // 
            btnReportes.AutoSize = true;
            btnReportes.BackColor = Color.FromArgb(34, 77, 112);
            btnReportes.Dock = DockStyle.Top;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Century Gothic", 9F);
            btnReportes.ForeColor = Color.Gainsboro;
            btnReportes.IconChar = FontAwesome.Sharp.IconChar.ChartSimple;
            btnReportes.IconColor = Color.FromArgb(38, 166, 154);
            btnReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReportes.ImageAlign = ContentAlignment.MiddleLeft;
            btnReportes.Location = new Point(0, 1528);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(9, 0, 0, 0);
            btnReportes.Size = new Size(259, 54);
            btnReportes.TabIndex = 13;
            btnReportes.Text = "Reportes";
            btnReportes.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += iconButton1_Click;
            // 
            // btnPatentes
            // 
            btnPatentes.AutoSize = true;
            btnPatentes.BackColor = Color.FromArgb(34, 77, 112);
            btnPatentes.Dock = DockStyle.Top;
            btnPatentes.FlatAppearance.BorderSize = 0;
            btnPatentes.FlatStyle = FlatStyle.Flat;
            btnPatentes.Font = new Font("Century Gothic", 9F);
            btnPatentes.ForeColor = Color.Gainsboro;
            btnPatentes.IconChar = FontAwesome.Sharp.IconChar.Certificate;
            btnPatentes.IconColor = Color.FromArgb(255, 190, 69);
            btnPatentes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPatentes.ImageAlign = ContentAlignment.MiddleLeft;
            btnPatentes.Location = new Point(0, 1206);
            btnPatentes.Name = "btnPatentes";
            btnPatentes.Padding = new Padding(9, 0, 0, 0);
            btnPatentes.Size = new Size(259, 54);
            btnPatentes.TabIndex = 11;
            btnPatentes.Text = "Patentes";
            btnPatentes.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnPatentes.UseVisualStyleBackColor = false;
            btnPatentes.Click += iconButton13_Click;
            // 
            // panelSubMenuMarcasInter
            // 
            panelSubMenuMarcasInter.BackColor = Color.FromArgb(196, 196, 208);
            panelSubMenuMarcasInter.Controls.Add(button6);
            panelSubMenuMarcasInter.Controls.Add(button5);
            panelSubMenuMarcasInter.Controls.Add(button2);
            panelSubMenuMarcasInter.Controls.Add(button1);
            panelSubMenuMarcasInter.Controls.Add(button27);
            panelSubMenuMarcasInter.Controls.Add(button28);
            panelSubMenuMarcasInter.Controls.Add(button29);
            panelSubMenuMarcasInter.Controls.Add(button30);
            panelSubMenuMarcasInter.Dock = DockStyle.Top;
            panelSubMenuMarcasInter.Font = new Font("Microsoft Sans Serif", 12F);
            panelSubMenuMarcasInter.Location = new Point(0, 777);
            panelSubMenuMarcasInter.Name = "panelSubMenuMarcasInter";
            panelSubMenuMarcasInter.Size = new Size(259, 429);
            panelSubMenuMarcasInter.TabIndex = 10;
            // 
            // button6
            // 
            button6.AutoSize = true;
            button6.Dock = DockStyle.Top;
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Century Gothic", 9F);
            button6.ForeColor = Color.Black;
            button6.Location = new Point(0, 355);
            button6.Name = "button6";
            button6.Padding = new Padding(58, 0, 0, 0);
            button6.Size = new Size(259, 47);
            button6.TabIndex = 11;
            button6.Text = "Registradas";
            button6.TextAlign = ContentAlignment.MiddleLeft;
            button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.AutoSize = true;
            button5.Dock = DockStyle.Top;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Century Gothic", 9F);
            button5.ForeColor = Color.Black;
            button5.Location = new Point(0, 308);
            button5.Name = "button5";
            button5.Padding = new Padding(58, 0, 0, 0);
            button5.Size = new Size(259, 47);
            button5.TabIndex = 10;
            button5.Text = "Ingresadas";
            button5.TextAlign = ContentAlignment.MiddleLeft;
            button5.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.Dock = DockStyle.Top;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 9F);
            button2.ForeColor = Color.Black;
            button2.Location = new Point(0, 261);
            button2.Name = "button2";
            button2.Padding = new Padding(58, 0, 0, 0);
            button2.Size = new Size(259, 47);
            button2.TabIndex = 9;
            button2.Text = "Abandonadas";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Dock = DockStyle.Top;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 9F);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(0, 201);
            button1.Name = "button1";
            button1.Padding = new Padding(58, 0, 0, 0);
            button1.Size = new Size(259, 60);
            button1.TabIndex = 8;
            button1.Text = "Oposiciones";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = true;
            // 
            // button27
            // 
            button27.AutoSize = true;
            button27.Dock = DockStyle.Top;
            button27.FlatAppearance.BorderSize = 0;
            button27.FlatStyle = FlatStyle.Flat;
            button27.Font = new Font("Century Gothic", 9F);
            button27.ForeColor = Color.Black;
            button27.Location = new Point(0, 141);
            button27.Name = "button27";
            button27.Padding = new Padding(58, 0, 0, 0);
            button27.Size = new Size(259, 60);
            button27.TabIndex = 7;
            button27.Text = "Trámite de traspaso";
            button27.TextAlign = ContentAlignment.MiddleLeft;
            button27.UseVisualStyleBackColor = true;
            // 
            // button28
            // 
            button28.AutoSize = true;
            button28.Dock = DockStyle.Top;
            button28.FlatAppearance.BorderSize = 0;
            button28.FlatStyle = FlatStyle.Flat;
            button28.Font = new Font("Century Gothic", 9F);
            button28.ForeColor = Color.Black;
            button28.Location = new Point(0, 94);
            button28.Name = "button28";
            button28.Padding = new Padding(58, 0, 0, 0);
            button28.Size = new Size(259, 47);
            button28.TabIndex = 5;
            button28.Text = "Trámite de renovación";
            button28.TextAlign = ContentAlignment.MiddleLeft;
            button28.UseVisualStyleBackColor = true;
            // 
            // button29
            // 
            button29.AutoSize = true;
            button29.Dock = DockStyle.Top;
            button29.FlatAppearance.BorderSize = 0;
            button29.FlatStyle = FlatStyle.Flat;
            button29.Font = new Font("Century Gothic", 9F);
            button29.ForeColor = Color.Black;
            button29.Location = new Point(0, 47);
            button29.Name = "button29";
            button29.Padding = new Padding(58, 0, 0, 0);
            button29.Size = new Size(259, 47);
            button29.TabIndex = 4;
            button29.Text = "Trámite inicial";
            button29.TextAlign = ContentAlignment.MiddleLeft;
            button29.UseVisualStyleBackColor = true;
            button29.Click += button29_Click;
            // 
            // button30
            // 
            button30.AutoSize = true;
            button30.Dock = DockStyle.Top;
            button30.FlatAppearance.BorderSize = 0;
            button30.FlatStyle = FlatStyle.Flat;
            button30.Font = new Font("Century Gothic", 9F);
            button30.ForeColor = Color.Black;
            button30.Location = new Point(0, 0);
            button30.Name = "button30";
            button30.Padding = new Padding(58, 0, 0, 0);
            button30.Size = new Size(259, 47);
            button30.TabIndex = 3;
            button30.Text = "Clientes";
            button30.TextAlign = ContentAlignment.MiddleLeft;
            button30.UseVisualStyleBackColor = true;
            button30.Click += button30_Click;
            // 
            // iconButton12
            // 
            iconButton12.AutoSize = true;
            iconButton12.BackColor = Color.FromArgb(34, 77, 112);
            iconButton12.Dock = DockStyle.Top;
            iconButton12.FlatAppearance.BorderSize = 0;
            iconButton12.FlatStyle = FlatStyle.Flat;
            iconButton12.Font = new Font("Century Gothic", 9F);
            iconButton12.ForeColor = Color.Gainsboro;
            iconButton12.IconChar = FontAwesome.Sharp.IconChar.Globe;
            iconButton12.IconColor = Color.FromArgb(129, 136, 255);
            iconButton12.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton12.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton12.Location = new Point(0, 716);
            iconButton12.Name = "iconButton12";
            iconButton12.Padding = new Padding(9, 0, 0, 0);
            iconButton12.Size = new Size(259, 61);
            iconButton12.TabIndex = 9;
            iconButton12.Text = "M. internacionales";
            iconButton12.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton12.UseVisualStyleBackColor = false;
            iconButton12.Click += iconButton12_Click;
            // 
            // panelSubMenuMarcasNacionales
            // 
            panelSubMenuMarcasNacionales.BackColor = Color.FromArgb(196, 196, 208);
            panelSubMenuMarcasNacionales.Controls.Add(btnAbandonadas);
            panelSubMenuMarcasNacionales.Controls.Add(btnTramiteRenov);
            panelSubMenuMarcasNacionales.Controls.Add(btnTramiteRenovacion);
            panelSubMenuMarcasNacionales.Controls.Add(btnRegistradas);
            panelSubMenuMarcasNacionales.Controls.Add(btnOposiciones);
            panelSubMenuMarcasNacionales.Controls.Add(btnEnTramite);
            panelSubMenuMarcasNacionales.Controls.Add(btnTramiteInicial);
            panelSubMenuMarcasNacionales.Dock = DockStyle.Top;
            panelSubMenuMarcasNacionales.Font = new Font("Microsoft Sans Serif", 12F);
            panelSubMenuMarcasNacionales.Location = new Point(0, 370);
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
            btnAbandonadas.Font = new Font("Century Gothic", 9F);
            btnAbandonadas.ForeColor = Color.Black;
            btnAbandonadas.Location = new Point(0, 282);
            btnAbandonadas.Margin = new Padding(0);
            btnAbandonadas.Name = "btnAbandonadas";
            btnAbandonadas.Padding = new Padding(58, 0, 0, 0);
            btnAbandonadas.Size = new Size(259, 47);
            btnAbandonadas.TabIndex = 5;
            btnAbandonadas.Text = "Abandonadas";
            btnAbandonadas.TextAlign = ContentAlignment.MiddleLeft;
            btnAbandonadas.UseVisualStyleBackColor = false;
            // 
            // btnTramiteRenov
            // 
            btnTramiteRenov.AutoSize = true;
            btnTramiteRenov.BackColor = Color.FromArgb(196, 196, 208);
            btnTramiteRenov.Dock = DockStyle.Top;
            btnTramiteRenov.FlatAppearance.BorderSize = 0;
            btnTramiteRenov.FlatStyle = FlatStyle.Flat;
            btnTramiteRenov.Font = new Font("Century Gothic", 9F);
            btnTramiteRenov.ForeColor = Color.Black;
            btnTramiteRenov.Location = new Point(0, 235);
            btnTramiteRenov.Name = "btnTramiteRenov";
            btnTramiteRenov.Padding = new Padding(58, 0, 0, 0);
            btnTramiteRenov.Size = new Size(259, 47);
            btnTramiteRenov.TabIndex = 4;
            btnTramiteRenov.Text = "Trámite de traspaso";
            btnTramiteRenov.TextAlign = ContentAlignment.MiddleLeft;
            btnTramiteRenov.UseVisualStyleBackColor = false;
            btnTramiteRenov.Click += button22_Click;
            // 
            // btnTramiteRenovacion
            // 
            btnTramiteRenovacion.AutoSize = true;
            btnTramiteRenovacion.BackColor = Color.FromArgb(196, 196, 208);
            btnTramiteRenovacion.Dock = DockStyle.Top;
            btnTramiteRenovacion.FlatAppearance.BorderSize = 0;
            btnTramiteRenovacion.FlatStyle = FlatStyle.Flat;
            btnTramiteRenovacion.Font = new Font("Century Gothic", 9F);
            btnTramiteRenovacion.ForeColor = Color.Black;
            btnTramiteRenovacion.Location = new Point(0, 188);
            btnTramiteRenovacion.Margin = new Padding(0);
            btnTramiteRenovacion.Name = "btnTramiteRenovacion";
            btnTramiteRenovacion.Padding = new Padding(58, 0, 0, 0);
            btnTramiteRenovacion.Size = new Size(259, 47);
            btnTramiteRenovacion.TabIndex = 3;
            btnTramiteRenovacion.Text = "Trámite de renovación";
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
            btnRegistradas.Font = new Font("Century Gothic", 9F);
            btnRegistradas.ForeColor = Color.Black;
            btnRegistradas.Location = new Point(0, 141);
            btnRegistradas.Name = "btnRegistradas";
            btnRegistradas.Padding = new Padding(58, 0, 0, 0);
            btnRegistradas.Size = new Size(259, 47);
            btnRegistradas.TabIndex = 2;
            btnRegistradas.Text = "Registradas";
            btnRegistradas.TextAlign = ContentAlignment.MiddleLeft;
            btnRegistradas.UseVisualStyleBackColor = false;
            // 
            // btnOposiciones
            // 
            btnOposiciones.AutoSize = true;
            btnOposiciones.BackColor = Color.FromArgb(196, 196, 208);
            btnOposiciones.Dock = DockStyle.Top;
            btnOposiciones.FlatAppearance.BorderSize = 0;
            btnOposiciones.FlatStyle = FlatStyle.Flat;
            btnOposiciones.Font = new Font("Century Gothic", 9F);
            btnOposiciones.ForeColor = Color.Black;
            btnOposiciones.Location = new Point(0, 94);
            btnOposiciones.Margin = new Padding(0);
            btnOposiciones.Name = "btnOposiciones";
            btnOposiciones.Padding = new Padding(58, 0, 0, 0);
            btnOposiciones.Size = new Size(259, 47);
            btnOposiciones.TabIndex = 1;
            btnOposiciones.Text = "Oposiciones";
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
            btnEnTramite.Font = new Font("Century Gothic", 9F);
            btnEnTramite.ForeColor = Color.Black;
            btnEnTramite.Location = new Point(0, 47);
            btnEnTramite.Margin = new Padding(0);
            btnEnTramite.Name = "btnEnTramite";
            btnEnTramite.Padding = new Padding(58, 0, 0, 0);
            btnEnTramite.Size = new Size(259, 47);
            btnEnTramite.TabIndex = 6;
            btnEnTramite.Text = "En trámite";
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
            btnTramiteInicial.Font = new Font("Century Gothic", 9F);
            btnTramiteInicial.ForeColor = Color.Black;
            btnTramiteInicial.Location = new Point(0, 0);
            btnTramiteInicial.Margin = new Padding(0);
            btnTramiteInicial.Name = "btnTramiteInicial";
            btnTramiteInicial.Padding = new Padding(58, 0, 0, 0);
            btnTramiteInicial.Size = new Size(259, 47);
            btnTramiteInicial.TabIndex = 0;
            btnTramiteInicial.Text = "Trámite inicial";
            btnTramiteInicial.TextAlign = ContentAlignment.MiddleLeft;
            btnTramiteInicial.UseVisualStyleBackColor = false;
            btnTramiteInicial.Click += button26_Click;
            // 
            // iconButton10
            // 
            iconButton10.AutoSize = true;
            iconButton10.BackColor = Color.FromArgb(34, 77, 112);
            iconButton10.Dock = DockStyle.Top;
            iconButton10.FlatAppearance.BorderSize = 0;
            iconButton10.FlatStyle = FlatStyle.Flat;
            iconButton10.Font = new Font("Century Gothic", 9F);
            iconButton10.ForeColor = Color.Gainsboro;
            iconButton10.IconChar = FontAwesome.Sharp.IconChar.Flag;
            iconButton10.IconColor = Color.FromArgb(255, 103, 159);
            iconButton10.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton10.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton10.Location = new Point(0, 316);
            iconButton10.Name = "iconButton10";
            iconButton10.Padding = new Padding(9, 0, 0, 0);
            iconButton10.Size = new Size(259, 54);
            iconButton10.TabIndex = 6;
            iconButton10.Text = "Marcas nacionales";
            iconButton10.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton10.UseVisualStyleBackColor = false;
            iconButton10.Click += iconButton10_Click;
            // 
            // iconButtonTitulares
            // 
            iconButtonTitulares.AutoSize = true;
            iconButtonTitulares.BackColor = Color.FromArgb(34, 77, 112);
            iconButtonTitulares.Dock = DockStyle.Top;
            iconButtonTitulares.FlatAppearance.BorderSize = 0;
            iconButtonTitulares.FlatStyle = FlatStyle.Flat;
            iconButtonTitulares.Font = new Font("Century Gothic", 9F);
            iconButtonTitulares.ForeColor = Color.Gainsboro;
            iconButtonTitulares.IconChar = FontAwesome.Sharp.IconChar.UserTag;
            iconButtonTitulares.IconColor = Color.FromArgb(255, 145, 0);
            iconButtonTitulares.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonTitulares.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonTitulares.Location = new Point(0, 262);
            iconButtonTitulares.Name = "iconButtonTitulares";
            iconButtonTitulares.Padding = new Padding(9, 0, 0, 0);
            iconButtonTitulares.Size = new Size(259, 54);
            iconButtonTitulares.TabIndex = 4;
            iconButtonTitulares.Text = "Titulares";
            iconButtonTitulares.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButtonTitulares.UseVisualStyleBackColor = false;
            iconButtonTitulares.Click += iconButton9_Click;
            // 
            // iconButtonAgentes
            // 
            iconButtonAgentes.AutoSize = true;
            iconButtonAgentes.BackColor = Color.FromArgb(34, 77, 112);
            iconButtonAgentes.Dock = DockStyle.Top;
            iconButtonAgentes.FlatAppearance.BorderSize = 0;
            iconButtonAgentes.FlatStyle = FlatStyle.Flat;
            iconButtonAgentes.Font = new Font("Century Gothic", 9F);
            iconButtonAgentes.ForeColor = Color.Gainsboro;
            iconButtonAgentes.IconChar = FontAwesome.Sharp.IconChar.UserTie;
            iconButtonAgentes.IconColor = Color.FromArgb(116, 89, 255);
            iconButtonAgentes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonAgentes.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonAgentes.Location = new Point(0, 208);
            iconButtonAgentes.Name = "iconButtonAgentes";
            iconButtonAgentes.Padding = new Padding(9, 0, 0, 0);
            iconButtonAgentes.Size = new Size(259, 54);
            iconButtonAgentes.TabIndex = 3;
            iconButtonAgentes.Text = "Agentes";
            iconButtonAgentes.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButtonAgentes.UseVisualStyleBackColor = false;
            iconButtonAgentes.Click += iconButton8_Click;
            // 
            // iconButtonUsuarios
            // 
            iconButtonUsuarios.AutoSize = true;
            iconButtonUsuarios.BackColor = Color.FromArgb(34, 77, 112);
            iconButtonUsuarios.Dock = DockStyle.Top;
            iconButtonUsuarios.FlatAppearance.BorderSize = 0;
            iconButtonUsuarios.FlatStyle = FlatStyle.Flat;
            iconButtonUsuarios.Font = new Font("Century Gothic", 9F);
            iconButtonUsuarios.ForeColor = Color.Gainsboro;
            iconButtonUsuarios.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconButtonUsuarios.IconColor = Color.FromArgb(230, 148, 226);
            iconButtonUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonUsuarios.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonUsuarios.Location = new Point(0, 154);
            iconButtonUsuarios.Margin = new Padding(3, 5, 3, 3);
            iconButtonUsuarios.Name = "iconButtonUsuarios";
            iconButtonUsuarios.Padding = new Padding(9, 0, 0, 0);
            iconButtonUsuarios.Size = new Size(259, 54);
            iconButtonUsuarios.TabIndex = 2;
            iconButtonUsuarios.Text = "Usuarios";
            iconButtonUsuarios.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButtonUsuarios.UseVisualStyleBackColor = false;
            iconButtonUsuarios.Click += iconButtonUsuarios_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(34, 77, 112);
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.BackgroundImageLayout = ImageLayout.Stretch;
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 89);
            panel3.Margin = new Padding(0, 2, 0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(259, 65);
            panel3.TabIndex = 2;
            // 
            // iconButton1
            // 
            iconButton1.AutoSize = true;
            iconButton1.BackColor = Color.FromArgb(34, 77, 112);
            iconButton1.Dock = DockStyle.Top;
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Century Gothic", 9F);
            iconButton1.ForeColor = Color.Gainsboro;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            iconButton1.IconColor = Color.FromArgb(230, 148, 226);
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton1.Location = new Point(0, 0);
            iconButton1.Margin = new Padding(3, 5, 3, 3);
            iconButton1.Name = "iconButton1";
            iconButton1.Padding = new Padding(9, 0, 0, 0);
            iconButton1.Size = new Size(259, 89);
            iconButton1.TabIndex = 16;
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(34, 77, 112);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(iconButton2);
            panel1.Controls.Add(labelUsername);
            panel1.Controls.Add(labelName_LN);
            panel1.Controls.Add(iconPictureBoxUser);
            panel1.Controls.Add(iconButton3);
            panel1.Controls.Add(iconButton4);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(280, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1201, 89);
            panel1.TabIndex = 4;
            panel1.Paint += panel1_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 15F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(19, 13);
            label1.Name = "label1";
            label1.Size = new Size(370, 31);
            label1.TabIndex = 6;
            label1.Text = "PIMARK- Software de marcas";
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
            iconButton2.Location = new Point(1082, 0);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(55, 64);
            iconButton2.TabIndex = 4;
            iconButton2.UseVisualStyleBackColor = true;
            iconButton2.Click += iconButton2_Click_1;
            // 
            // labelUsername
            // 
            labelUsername.Anchor = AnchorStyles.None;
            labelUsername.AutoSize = true;
            labelUsername.Font = new Font("Century Gothic", 9F);
            labelUsername.ForeColor = Color.White;
            labelUsername.Location = new Point(746, 14);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(53, 20);
            labelUsername.TabIndex = 3;
            labelUsername.Text = "label1";
            labelUsername.Click += labelUsername_Click;
            // 
            // labelName_LN
            // 
            labelName_LN.Anchor = AnchorStyles.None;
            labelName_LN.AutoSize = true;
            labelName_LN.Font = new Font("Century Gothic", 9F);
            labelName_LN.ForeColor = Color.White;
            labelName_LN.Location = new Point(746, 45);
            labelName_LN.Name = "labelName_LN";
            labelName_LN.Size = new Size(53, 20);
            labelName_LN.TabIndex = 2;
            labelName_LN.Text = "label1";
            labelName_LN.Click += labelName_LN_Click;
            // 
            // iconPictureBoxUser
            // 
            iconPictureBoxUser.Anchor = AnchorStyles.None;
            iconPictureBoxUser.BackColor = Color.FromArgb(34, 77, 112);
            iconPictureBoxUser.IconChar = FontAwesome.Sharp.IconChar.UserCircle;
            iconPictureBoxUser.IconColor = Color.White;
            iconPictureBoxUser.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBoxUser.IconSize = 60;
            iconPictureBoxUser.Location = new Point(680, 3);
            iconPictureBoxUser.Name = "iconPictureBoxUser";
            iconPictureBoxUser.Size = new Size(60, 61);
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
            iconButton3.Location = new Point(1143, -1);
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
            iconButton4.Location = new Point(1021, 2);
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
            ClientSize = new Size(1481, 900);
            ControlBox = false;
            Controls.Add(panel1);
            Controls.Add(panelChildForm);
            Controls.Add(panel2);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            MinimumSize = new Size(1481, 900);
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
            panelSubMenuMarcasInter.ResumeLayout(false);
            panelSubMenuMarcasInter.PerformLayout();
            panelSubMenuMarcasNacionales.ResumeLayout(false);
            panelSubMenuMarcasNacionales.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBoxUser).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton iconButton10;
        private FontAwesome.Sharp.IconButton iconButtonTitulares;
        private FontAwesome.Sharp.IconButton iconButtonAgentes;
        private FontAwesome.Sharp.IconButton iconButtonUsuarios;
        private System.Windows.Forms.Panel panelSubMenuMarcasNacionales;
        private System.Windows.Forms.Button btnTramiteRenov;
        private System.Windows.Forms.Button btnTramiteRenovacion;
        private System.Windows.Forms.Button btnRegistradas;
        private System.Windows.Forms.Button btnOposiciones;
        private System.Windows.Forms.Button btnTramiteInicial;
        private System.Windows.Forms.Panel panelSubMenuPatentes;
        private System.Windows.Forms.Button button32;
        private System.Windows.Forms.Button button33;
        private System.Windows.Forms.Button button34;
        private System.Windows.Forms.Button button35;
        private System.Windows.Forms.Button button36;
        private FontAwesome.Sharp.IconButton btnPatentes;
        private System.Windows.Forms.Panel panelSubMenuMarcasInter;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button29;
        private System.Windows.Forms.Button button30;
        private FontAwesome.Sharp.IconButton iconButton12;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnReportes;
        private FontAwesome.Sharp.IconButton iconButtonLogout;
        private Button button1;
        private Button button2;
        private Button btnAbandonadas;
        private Button button6;
        private Button button5;
        private FontAwesome.Sharp.IconButton iconButton3;
        private Label labelUsername;
        private Label labelName_LN;
        private FontAwesome.Sharp.IconPictureBox iconPictureBoxUser;
        private FontAwesome.Sharp.IconButton iconButton4;
        private FontAwesome.Sharp.IconButton iconButtonVencimientos;
        private FontAwesome.Sharp.IconButton iconButton2;
        private Panel panel3;
        private Label label1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private Button btnEnTramite;
    }
}

