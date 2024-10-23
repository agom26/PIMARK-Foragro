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

namespace Presentacion
{
    public partial class Form1 : Form
    {
        private bool isAdmin;
        public Form1(bool isAdmin)
        {
            InitializeComponent();
            this.isAdmin = isAdmin;
            CustomizeDesign();

            if (isAdmin)
            {
                iconButtonUsuarios.Visible = true; //Mostrar apartado de usuarios
            }
            else
            {
                iconButtonUsuarios.Visible = false; //No mostrarlo si no son admin
            }

            
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
            iconButtonUsuarios.Enabled = false;
            iconButtonAgentes.Enabled = false;
            iconButtonTitulares.Enabled = false;
        }

        public void EnableButtons()
        {
            iconButtonUsuarios.Enabled = true;
            iconButtonAgentes.Enabled = true;
            iconButtonTitulares.Enabled = true;
        }



        private Form activeForm = null;
        
        private async void openChildForm(Form childForm)
        {

            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            //childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
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
            ShowSubMenu(panelLogo);
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

        private void button23_Click(object sender, EventArgs e)
        {
            //openChildForm(new Form3());
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //openChildForm(new Form2());
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

        private void button35_Click(object sender, EventArgs e)
        {
            //openChildForm(new Patentes());
        }

        private void button30_Click(object sender, EventArgs e)
        {
            //openChildForm(new Form6());
        }

        private void button29_Click(object sender, EventArgs e)
        {
            //openChildForm(new Form7());
        }

        private void button34_Click(object sender, EventArgs e)
        {
            //openChildForm(new PatentesForm());
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //openChildForm(new AlertasVencimiento());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            //openChildForm(new Reportes());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelName_LN.Text = UsuarioActivo.nombres + " " + UsuarioActivo.apellidos;
            labelUsername.Text = UsuarioActivo.usuario + " - " + UsuarioActivo.correo;
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
            // Esperar un breve momento para permitir que se muestre el formulario
            await Task.Delay(1000);

            // Habilitar los botones después de un breve retraso
            EnableButtons();
        }

        private async void iconButton8_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmAdministrarAgentes());
            // Esperar un breve momento para permitir que se muestre el formulario
            await Task.Delay(1000);

            // Habilitar los botones después de un breve retraso
            EnableButtons();

        }

        private async void iconButton9_Click(object sender, EventArgs e)
        {
            DisableButtons();
            openChildForm(new FrmAdministrarTitulares());
            // Esperar un breve momento para permitir que se muestre el formulario
            await Task.Delay(1000);

            // Habilitar los botones
            EnableButtons();
        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            // Verifica el estado de la ventana
            if (this.WindowState == FormWindowState.Maximized)
            {
                // Restaura la ventana a su tamaño normal
                this.WindowState = FormWindowState.Normal;
                // Cambia el icono a maximizar
                iconButton2.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize; // Cambia a icono de maximizar
            }
            else
            {
                // Cambia el estado de la ventana a maximizado
                this.WindowState = FormWindowState.Maximized;
                // Cambia el icono a restaurar
                iconButton2.IconChar = FontAwesome.Sharp.IconChar.WindowRestore; // Cambia a icono de restaurar
            }
            //FormResize();

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
            openChildForm(new FrmTramiteInicial());
            await Task.Delay(1000);
            EnableButtons();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //FormResize();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //FormResize();
        }
    }
}
