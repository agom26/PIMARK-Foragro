using Marcas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Comun.Cache;
using Presentacion.Personas;
using Presentacion.Marcas_Nacionales;
using Presentacion.Marcas_Internacionales;
using Presentacion.Vencimientos;
using Dominio;
using Presentacion.Reportes;
using Presentacion.Patentes;
using System.Diagnostics;
using Presentacion.Alertas;
using System.Runtime.InteropServices;
using DocumentFormat.OpenXml.Office2016.Excel;
using FontAwesome.Sharp;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Presentacion.Properties;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        VencimientoModel VencimientoModel = new VencimientoModel();
        private bool isAdmin;
        private int borderSize = 8;
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();


        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public Form1(bool isAdmin)
        {
            InitializeComponent();
            this.Shown += Form1_Shown;
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(34, 77, 112);
            this.isAdmin = isAdmin;



            this.Resize += Form1_Resize;
            CustomizeDesign();

            if (isAdmin)
            {
                btnUsers.Visible = true;
            }
            else
            {
                btnUsers.Visible = false;
            }
            VencimientoModel.EjecutarProcedimiento();



        }

        private void CustomizeDesign()
        {
            panelSubMenuMarcasNacionales.Visible = false;
            panelSubMenuMarcasInter.Visible = false;
            panelSubMenuPatentes.Visible = false;
        }

        private void hideSubMenu()
        {
            if (panelSubMenuMarcasInter.Visible == true)
                panelSubMenuMarcasInter.Visible = false;
            if (panelSubMenuPatentes.Visible == true)
                panelSubMenuPatentes.Visible = false;
            if (panelSubMenuMarcasNacionales.Visible == true)
                panelSubMenuMarcasNacionales.Visible = false;
        }

        private void hideSubMenusTodos()
        {
            panelSubMenuMarcasInter.Visible = false;

            panelSubMenuPatentes.Visible = false;

            panelSubMenuMarcasNacionales.Visible = false;

        }

        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //openChildForm(new Form2());
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {


        }

        private void button8_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelSubMenuMarcasNacionales);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelSubMenuPatentes);
        }

        public void DisableButtons()
        {

            //marcas nacionales
            btnIngresadasInt.Enabled = false;
            btnClientes2.Enabled = false;
            btnTramiteInicialInter.Enabled = false;
            btnOpoInter.Enabled = false;
            btnRegInter.Enabled = false;
            btnRenovInter.Enabled = false;
            btnTraspasoInter.Enabled = false;
            btnAbandonadasInter.Enabled = false;


            //internacionales
            btnTramiteInicial.Enabled = false;
            btnEnTramite.Enabled = false;
            btnOposiciones.Enabled = false;
            btnRegistradas.Enabled = false;
            btnTramiteRenovacion.Enabled = false;
            btnTramiteTraspaso.Enabled = false;
            btnAbandonadas.Enabled = false;

            //patentes
            btnIngresarPatente.Enabled = false;
            btnTramiteInicialPatente.Enabled = false;
            btnPatentesRegistradas.Enabled = false;
            btnTramiteRenovPatentes.Enabled = false;
            btnTramiteTraspPatentes.Enabled = false;
            btnAbandonadasPatentes.Enabled = false;
            //otros

            btnInicio.Enabled = false;
            btnUsers.Enabled = false;
            btnAgentes.Enabled = false;
            btnTitulares.Enabled = false;
            btnMarcasNacionales.Enabled = false;
            btnMInternacionales.Enabled = false;
            btnPatentes.Enabled = false;
            btnReportes.Enabled = false;
            btnVencimientos.Enabled = false;
            btnCerrarSesion.Enabled = false;
            //btnReportes.Enabled = false;
            //iconButtonLogout.Enabled = false;
            //iconButtonVencimientos.Enabled = false;
        }

        public void EnableButtons()
        {
            //marcas nacionales
            btnIngresadasInt.Enabled = true;
            btnClientes2.Enabled = true;
            btnTramiteInicialInter.Enabled = true;
            btnOpoInter.Enabled = true;
            btnRegInter.Enabled = true;
            btnRenovInter.Enabled = true;
            btnTraspasoInter.Enabled = true;
            btnAbandonadasInter.Enabled = true;


            //internacionales
            btnTramiteInicial.Enabled = true;
            btnEnTramite.Enabled = true;
            btnOposiciones.Enabled = true;
            btnRegistradas.Enabled = true;
            btnTramiteRenovacion.Enabled = true;
            btnTramiteTraspaso.Enabled = true;
            btnAbandonadas.Enabled = true;

            //patentes
            btnIngresarPatente.Enabled = true;
            btnTramiteInicialPatente.Enabled = true;
            btnPatentesRegistradas.Enabled = true;
            btnTramiteRenovPatentes.Enabled = true;
            btnTramiteTraspPatentes.Enabled = true;
            btnAbandonadasPatentes.Enabled = true;

            //otros
            btnInicio.Enabled = true;
            btnUsers.Enabled = true;
            btnAgentes.Enabled = true;
            btnTitulares.Enabled = true;
            btnMarcasNacionales.Enabled = true;
            btnMInternacionales.Enabled = true;
            btnPatentes.Enabled = true;
            btnReportes.Enabled = true;
            btnVencimientos.Enabled = true;
            btnCerrarSesion.Enabled = true;
        }



        private Form activeForm = null;
        private void FormResize()
        {
            // Ajustar márgenes según el estado de la ventana
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 8, 8);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }

            if (activeForm != null)
            {
                // Ocultar temporalmente para evitar parpadeos
                panelChildForm.Visible = false;

                // Congelar diseño para evitar redibujos intermedios
                panelChildForm.SuspendLayout();
                activeForm.SuspendLayout();

                // Ajustar posicionamiento
                activeForm.Dock = DockStyle.None;
                activeForm.Location = new System.Drawing.Point(0, 0);
                activeForm.Dock = DockStyle.Fill;

                // Reactivar el layout
                activeForm.ResumeLayout(true);
                panelChildForm.ResumeLayout(true);

                // Mostrar suavemente luego de un frame
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    panelChildForm.Visible = true;
                    panelChildForm.Refresh();
                }));
            }
        }

        /* mejorado
        private void FormResize()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 8, 8);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }

            if (activeForm != null)
            {
                // Ocultar mientras se reajusta todo
                panelChildForm.Visible = false;

                panelChildForm.SuspendLayout();
                activeForm.SuspendLayout();

                activeForm.Dock = DockStyle.Fill;
                activeForm.Location = new System.Drawing.Point(0, 0);

                activeForm.ResumeLayout(true);
                panelChildForm.ResumeLayout(true);

                // Reaparecer suavemente después del ajuste
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    panelChildForm.Visible = true;
                }));
            }
        }*/


        /*
        private void FormResize()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 8, 8);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }

            if (activeForm != null)
            {
                //panelChildForm.SuspendLayout();

                activeForm.Dock = DockStyle.Fill; // Mantén tamaño si es necesario
                activeForm.Location = new System.Drawing.Point(0, 0); // Reposiciona al origen
                                         
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    //panelChildForm.ResumeLayout(true);
                    panelChildForm.PerformLayout();
                    panelChildForm.Refresh();
                    activeForm.Refresh();
                }));

            }
        }*/


        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;

            // No dock para que respete su tamaño original
            childForm.Dock = DockStyle.None;

            // No modificar tamaño para respetar el diseño original
            // (si quieres podrías ajustar con lógica extra)

            panelChildForm.Controls.Clear();
            panelChildForm.Controls.Add(childForm);

            panelChildForm.AutoScroll = true;  // IMPORTANTE: para scrollbars

            childForm.BringToFront();
            childForm.Show();

            FormResize();
        }


        /*
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            //childForm.Dock = DockStyle.Fill;
            panelChildForm.AutoScroll = true;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            FormResize();
        }*/

        private void buttonMarcas_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelSubMenuMarcasInter);
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelSubMenuPatentes);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton10_Click(object sender, EventArgs e)
        {

            ShowSubMenu(panelSubMenuMarcasNacionales);
        }

        // ShowSubMenu(panelSubMenuMarcasInter); marcas nacionales
        private void iconButton12_Click(object sender, EventArgs e)
        {

        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelSubMenuPatentes);
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmRenovaciones());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTraspasos());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            //openChildForm(new Form4());
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //openChildForm(new Form5());
        }

        private async void button35_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarTramiteRenovacionPatente());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button30_Click(object sender, EventArgs e)
        {

        }

        private async void button29_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTramiteInicialInternacional(this));
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button34_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarTramiteTraspasoPatente());
                await Task.Delay(1000);
                EnableButtons();
                //openChildForm(new PatentesForm());
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void button18_Click(object sender, EventArgs e)
        {
            //openChildForm(new AlertasVencimiento());
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmReportesMarcasPatentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            panel1.Dock = DockStyle.Top;     // Primero arriba
            panel2.Dock = DockStyle.Left;    // Luego a la izquierda (debajo del top)
            panelChildForm.Dock = DockStyle.Fill; // Lo que queda

            //labelName_LN.Text = UsuarioActivo.nombres + " " + UsuarioActivo.apellidos;
            labelUsername.Text = UsuarioActivo.usuario + " - " + UsuarioActivo.correo;
            DisableButtons();
            openChildForm(new FrmDashboard3(this));
            await Task.Delay(1000);
            EnableButtons();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                if (System.Windows.Forms.MessageBox.Show("¿Está seguro de cerrar sesión?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                if (System.Windows.Forms.MessageBox.Show("¿Está seguro de cerrar la aplicación?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    System.Windows.Forms.Application.Exit();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void iconButtonUsuarios_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmAdministrarUsuarios());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void iconButton8_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmAdministrarAgentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void iconButton9_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmAdministrarTitulares());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Maximized)
            {

                this.WindowState = FormWindowState.Normal;

                iconButton2.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize; // Cambia a icono de maximizar
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;

                iconButton2.IconChar = FontAwesome.Sharp.IconChar.WindowRestore; // Cambia a icono de restaurar
            }
            FormResize();

        }

        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm = null;
            }


        }

        private async void button26_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTramiteIn(this));
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //FormResize();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            FormResize();
        }

        private async void iconButton5_Click_1(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmVencimientos());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void labelName_LN_Click(object sender, EventArgs e)
        {

        }

        private void labelUsername_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarTodas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void btnOposiciones_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarOposiciones());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void btnAbandonadas_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarAbandonadas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void btnRegistradas_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmRegistradas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button5_Click_1(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasIntIngresadas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button6_Click_1(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasIntRegistradas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasIntOposiciones());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasIntAbandonadas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void panel3_Click_1(object sender, EventArgs e)
        {

        }

        public async void cargarDashboard()
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmDashboard3(this));
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void btnHome_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmDashboard3(this));
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void btnRenovInter_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmRenovacionesInt());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void btnTraspasoInter_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTraspasosInt());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTramiteInicialPatente(this));
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarIngresadasPatentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void iconButton5_Click_2(object sender, EventArgs e)
        {

        }

        private void iconButton5_Click_3(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                string url = "https://www.facebook.com/SitiosEnRed/";

                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {

                    System.Windows.Forms.MessageBox.Show($"No se pudo abrir el enlace: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }


        private async void button33_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarRegistradasPatentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void btnAbandonadasPatentes_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarAbandonadasPatentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (panel2.Width == 100)
            {
                hideSubMenusTodos();
                rDropDownMenu1.Show(btnMarcasNacionales, btnMarcasNacionales.Width, 0);
            }
            else
            {
                ShowSubMenu(panelSubMenuMarcasInter);
            }

        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmDashboard3(this));
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void button4_Click_1(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmAdministrarUsuarios());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void button4_Click_2(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmAdministrarAgentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button5_Click_2(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmAdministrarTitulares());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            if (panel2.Width == 100)
            {
                rDropDownMenu2.Show(btnMInternacionales, btnMInternacionales.Width, 0);
                hideSubMenusTodos();

            }
            else
            {
                ShowSubMenu(panelSubMenuMarcasNacionales);
            }

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (panel2.Width == 100)
            {
                rDropDownMenu3.Show(btnPatentes, btnPatentes.Width, 0);
                hideSubMenusTodos();
            }
            else
            {
                ShowSubMenu(panelSubMenuPatentes);
            }

        }

        private async void button8_Click_1(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmReportesMarcasPatentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void button9_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmVencimientos());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                if (System.Windows.Forms.MessageBox.Show("¿Está seguro de cerrar sesión?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button2_Click_3(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasLicenciaUso());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void button3_Click_2(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmAdministrarClientes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        //overridden methods
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x83;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                m.Result = new IntPtr(0xF0);   // Align client area to all borders
                return;
            }
            base.WndProc(ref m);
        }

        private void iconButton5_Click_4(object sender, EventArgs e)
        {
            hideSubMenusTodos();
            CollapseMenu();
            //rDropDownMenu2.Show();
        }


        private void CollapseMenu()
        {
           

            // Oculta durante los cambios para evitar parpadeos
            panel2.Visible = false;

            // Suspende solo lo necesario
            panel2.SuspendLayout();
            panelSubMenuMarcasNacionales.SuspendLayout();
            panelSubMenuMarcasInter.SuspendLayout();
            panelSubMenuPatentes.SuspendLayout();

            bool esExpandido = panel2.Width > 250;

            if (esExpandido)
            {
                panel2.Width = 100;

                // Minimizar botones
                iconButton1.BackgroundImage = Resources.PimarkP;
                foreach (var panel in new[] {
            panel2, panelSubMenuMarcasNacionales,
            panelSubMenuMarcasInter, panelSubMenuPatentes })
                {
                    foreach (Button btn in panel.Controls.OfType<Button>())
                    {
                        btn.Text = "";
                        btn.ImageAlign = ContentAlignment.MiddleCenter;
                        btn.Padding = new Padding(0);
                    }
                }
            }
            else
            {
                panel2.Width = 260;

                iconButton1.BackgroundImage = Resources.PimarkN3;
                foreach (var panel in new[] {
            panel2, panelSubMenuMarcasNacionales,
            panelSubMenuMarcasInter, panelSubMenuPatentes })
                {
                    foreach (Button btn in panel.Controls.OfType<Button>())
                    {
                        btn.TextAlign = ContentAlignment.MiddleLeft;
                        btn.ImageAlign = ContentAlignment.MiddleLeft;
                        btn.Text = btn.Tag?.ToString() ?? ""; // Usa Tag como texto
                        btn.Padding = new Padding(10, 0, 0, 0);
                    }
                }
            }

            // Restaura el layout
            panelSubMenuPatentes.ResumeLayout();
            panelSubMenuMarcasInter.ResumeLayout();
            panelSubMenuMarcasNacionales.ResumeLayout();
            panel2.ResumeLayout();

            // Mostrar con cambios ya aplicados
            panel2.Visible = true;

            // Redibuja correctamente
            this.PerformLayout();
            panel2.Refresh();
            FormResize(); // Si redimensionás el contenido
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }




        /*
        private void CollapseMenu()
        {
            panel2.Visible = false;
            this.SuspendLayout(); // Congela el layout del formulario

            if (this.panel2.Width > 250)
            {

                panel2.Width = 100;
                //panel2.Visible = false;
                foreach (Button menuButton in panel2.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
                foreach (Button menuButton2 in panelSubMenuMarcasNacionales.Controls.OfType<Button>())
                {
                    menuButton2.Text = "";
                    menuButton2.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton2.Padding = new Padding(0);
                }
                foreach (Button menuButton2 in panelSubMenuMarcasInter.Controls.OfType<Button>())
                {
                    menuButton2.Text = "";
                    menuButton2.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton2.Padding = new Padding(0);
                }
                foreach (Button menuButton2 in panelSubMenuPatentes.Controls.OfType<Button>())
                {
                    menuButton2.Text = "";
                    menuButton2.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton2.Padding = new Padding(0);
                }
            }
            else
            {
                panel2.Width = 260;
                foreach (IconButton menuButton in panel2.Controls.OfType<IconButton>())
                {
                    menuButton.TextAlign = ContentAlignment.MiddleLeft;
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Text = menuButton.Tag.ToString();
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
                foreach (Button menuButton in panel2.Controls.OfType<Button>())
                {
                    menuButton.TextAlign = ContentAlignment.MiddleLeft;
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Text = menuButton.Tag.ToString();
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
                foreach (Button menuButton2 in panelSubMenuMarcasNacionales.Controls.OfType<Button>())
                {
                    menuButton2.TextAlign = ContentAlignment.MiddleLeft;
                    menuButton2.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton2.Text = menuButton2.Tag.ToString();
                    menuButton2.Padding = new Padding(10, 0, 0, 0);
                }
                foreach (Button menuButton in panelSubMenuMarcasInter.Controls.OfType<Button>())
                {
                    menuButton.TextAlign = ContentAlignment.MiddleLeft;
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Text = menuButton.Tag.ToString();
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
                foreach (Button menuButton in panelSubMenuPatentes.Controls.OfType<Button>())
                {
                    menuButton.TextAlign = ContentAlignment.MiddleLeft;
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Text = menuButton.Tag.ToString();
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }

            }
            //panelChildForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            //panelChildForm.Refresh();
            panel1.Dock = DockStyle.Top;
            panel2.Dock = DockStyle.Left;
            panelChildForm.Dock = DockStyle.Fill;
            this.ResumeLayout(true); // Aplica el layout nuevamente
            panel2.Visible = true;
            this.Refresh(); // Fuerza actualización del layout del formulario
            FormResize();

        }*/

        private void rEGISTRADASToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void tRDERENOVACIÓToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmRegistradas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void iNGRESARMARCAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTramiteInicialInternacional(this));
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void tRÁMITEINICIALToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasIntIngresadas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void oPOSICIONESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasIntOposiciones());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void rEGISTRADASToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasIntRegistradas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void lICENCIASDEUSOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasLicenciaUso());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void tRDERENOVACIÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmRenovacionesInt());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void tRDETRASPASOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTraspasosInt());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void aBANDONADASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMarcasIntAbandonadas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void iNGRESARMARCAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTramiteIn(this));
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void tRÁMITEINICIALToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarTodas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void oPOSICIONESToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarOposiciones());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void tRDETRASPASOToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmRenovaciones());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void aBANDONADASToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTraspasos());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void aBANDONADASToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarAbandonadas());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void tRÁMITEINICIALToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarIngresadasPatentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void tRDERENOVACIÓNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarTramiteRenovacionPatente());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void iNGRESARPATENTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmTramiteInicialPatente(this));
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void rEGISTRADASToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarRegistradasPatentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void tRDETRASPASOToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarTramiteTraspasoPatente());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private async void aBANDONADASToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (DatosRegistro.peligro == false)
            {
                DisableButtons();
                openChildForm(new FrmMostrarAbandonadasPatentes());
                await Task.Delay(1000);
                EnableButtons();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            CollapseMenu();
        }
    }
}
