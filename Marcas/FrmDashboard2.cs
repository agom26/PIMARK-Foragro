using Dominio;
using Presentacion.Marcas_Nacionales;
using Presentacion.Reportes;
using Presentacion.Vencimientos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FrmDashboard2 : Form
    {
        public Form1 mainForm;
        VencimientoModel vencimientoModel = new VencimientoModel();

        public async void Ejecutar()
        {
            await Task.Run(() => vencimientoModel.EjecutarProcedimiento());
            
        }
      
        public FrmDashboard2(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            Ejecutar();

        }

        private async void roundedButton5_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmVencimientos());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void label1_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            //mainForm.openChildForm(new FrmTramiteIn(this));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void iconPictureBox1_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            //mainForm.openChildForm(new FrmTramiteIn(this));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void roundedButton2_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            //mainForm.openChildForm(new FrmTramiteIn(this));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void iconPictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void roundedButton3_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void panel21_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void label5_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel10_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            //mainForm.openChildForm(new FrmTramiteIn(this));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void iconPictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void roundedButton4_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel18_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void label6_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel22_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel12_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }
    }
}
