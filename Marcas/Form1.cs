using Marcas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using Comun.Cache;
using Presentacion.Personas;
using Presentacion.Marcas_Nacionales;
using Presentacion.Marcas_Internacionales;
using Presentacion.Vencimientos;
using Dominio;
using Presentacion.Reportes;
using Presentacion.Patentes;
using System.Diagnostics;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        VencimientoModel VencimientoModel = new VencimientoModel();
        private bool isAdmin;

        public Form1(bool isAdmin)
        {
            InitializeComponent();
            this.isAdmin = isAdmin;
            CustomizeDesign();

            if (isAdmin)
            {
                iconButtonUsuarios.Visible = true;
            }
            else
            {
                iconButtonUsuarios.Visible = false;
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
            //home
            btnHome.Enabled = false;
            iconButtonUsuarios.Enabled = false;
            iconButtonAgentes.Enabled = false;
            iconButtonTitulares.Enabled = false;
            btnTramiteInicial.Enabled = false;
            btnEnTramite.Enabled = false;
            btnOposiciones.Enabled = false;
            btnTramiteTraspaso.Enabled = false;
            btnTramiteRenovacion.Enabled = false;
            btnRegistradas.Enabled = false;
            btnAbandonadas.Enabled = false;
            //internacionales
            btnClientes.Enabled = false;
            btnTramiteInicialInter.Enabled = false;
            btnIngresadasInt.Enabled = false;
            btnOpoInter.Enabled = false;
            btnRegInter.Enabled = false;
            btnAbandonadasInter.Enabled = false;
            //patentes
            btnIngresarPatente.Enabled = false;
            btnTramiteInicialPatente.Enabled = false;
            btnPatentesRegistradas.Enabled = false;
            btnTramiteRenovPatentes.Enabled = false;
            btnTramiteTraspPatentes.Enabled = false;
            btnAbandonadasPatentes.Enabled = false;
            //otros
            btnReportes.Enabled = false;
            iconButtonLogout.Enabled = false;
            iconButtonVencimientos.Enabled = false;
        }

        public void EnableButtons()
        {
            //home
            btnHome.Enabled = true;
            iconButtonUsuarios.Enabled = true;
            iconButtonAgentes.Enabled = true;
            iconButtonTitulares.Enabled = true;
            btnTramiteInicial.Enabled = true;
            btnEnTramite.Enabled = true;
            btnOposiciones.Enabled = true;
            btnRegistradas.Enabled = true;
            btnAbandonadas.Enabled = true;
            btnTramiteRenovacion.Enabled = true;
            btnTramiteTraspaso.Enabled = true;
            //internacionales
            btnClientes.Enabled = true;
            btnTramiteInicialInter.Enabled = true;
            btnIngresadasInt.Enabled = true;
            btnOpoInter.Enabled = true;
            btnRegInter.Enabled = true;
            btnAbandonadasInter.Enabled = true;
            //patentes
            btnIngresarPatente.Enabled = true;
            btnTramiteInicialPatente.Enabled = true;
            btnPatentesRegistradas.Enabled = true;
            btnTramiteRenovPatentes.Enabled = true;
            btnTramiteTraspPatentes.Enabled = true;
            btnAbandonadasPatentes.Enabled = true;
            //otros
            btnReportes.Enabled = true;
            iconButtonLogout.Enabled = true;
            iconButtonVencimientos.Enabled = true;
        }



        private Form activeForm = null;

        private void FormResize()
        {
            if (activeForm != null)
            {
                activeForm.Hide();
                activeForm.WindowState = FormWindowState.Normal;
                activeForm.WindowState = FormWindowState.Maximized;
                activeForm.Show();
            }
        }
        public void openChildForm(Form childForm)
        {

            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            //panelChildForm.AutoScroll = true;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            FormResize();
        }

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

        private void iconButton12_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelSubMenuMarcasInter);
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelSubMenuPatentes);
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmRenovaciones());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmTraspasos());
            await Task.Delay(1000);
            EnableButtons();
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
            DisableButtons();
            openChildForm(new FrmMostrarTramiteRenovacionPatente());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button30_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmAdministrarClientes());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button29_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmTramiteInicialInternacional());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button34_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMostrarTramiteTraspasoPatente());
            await Task.Delay(1000);
            EnableButtons();
            //openChildForm(new PatentesForm());
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //openChildForm(new AlertasVencimiento());
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmReportes());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //labelName_LN.Text = UsuarioActivo.nombres + " " + UsuarioActivo.apellidos;
            labelUsername.Text = UsuarioActivo.usuario + " - " + UsuarioActivo.correo;
            DisableButtons();
            openChildForm(new FrmDashboard2(this));
            await Task.Delay(1000);
            EnableButtons();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar sesión?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar la aplicación?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private async void iconButtonUsuarios_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmAdministrarUsuarios());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void iconButton8_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmAdministrarAgentes());
            await Task.Delay(1000);
            EnableButtons();

        }

        private async void iconButton9_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmAdministrarTitulares());
            await Task.Delay(1000);
            EnableButtons();
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
            DisableButtons();
            openChildForm(new FrmTramiteIn());
            await Task.Delay(1000);
            EnableButtons();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            FormResize();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            FormResize();
        }

        private async void iconButton5_Click_1(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmVencimientos());
            await Task.Delay(1000);
            EnableButtons();
        }

        private void labelName_LN_Click(object sender, EventArgs e)
        {

        }

        private void labelUsername_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMostrarTodas());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void btnOposiciones_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMostrarOposiciones());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void btnAbandonadas_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMostrarAbandonadas());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void btnRegistradas_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button5_Click_1(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMarcasIntIngresadas());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button6_Click_1(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMarcasIntRegistradas());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMarcasIntOposiciones());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMarcasIntAbandonadas());
            await Task.Delay(1000);
            EnableButtons();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void panel3_Click_1(object sender, EventArgs e)
        {

        }

        private async void btnHome_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmDashboard2(this));
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void btnRenovInter_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmRenovacionesInt());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void btnTraspasoInter_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmTraspasosInt());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmTramiteInicialPatente());
            await Task.Delay(1000);
            EnableButtons();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMostrarIngresadasPatentes());
            await Task.Delay(1000);
            EnableButtons();
        }

        private void iconButton5_Click_2(object sender, EventArgs e)
        {

        }

        private void iconButton5_Click_3(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
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

                MessageBox.Show($"No se pudo abrir el enlace: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void button33_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmMostrarRegistradasPatentes());
            await Task.Delay(1000);
            EnableButtons();
        }
    }
}
