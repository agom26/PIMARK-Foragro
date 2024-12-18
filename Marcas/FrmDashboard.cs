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
    public partial class FrmDashboard : Form
    {
        public Form1 mainForm;
        VencimientoModel vencimientoModel = new VencimientoModel();

        private async void LoadVencimientos()
        {
            var titulares = await Task.Run(() => vencimientoModel.GetAllVencimientos());
            Invoke(new Action(() =>
            {
                dtgVencimientos.DataSource = titulares;

                if (dtgVencimientos.Columns["id"] != null)
                {
                    dtgVencimientos.Columns["id"].Visible = false;
                    dtgVencimientos.Columns["marcaID"].Visible = false;

                }
                dtgVencimientos.Refresh();
            }));
        }
        public FrmDashboard(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            LoadVencimientos();

        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {


        }

        private void roundedButton2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private async void roundedButton5_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmVencimientos());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void roundedButton2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }

        private async void panel10_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            //mainForm.openChildForm(new FrmTramiteIn(this));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void iconPictureBox1_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
           // mainForm.openChildForm(new FrmTramiteIn(this));
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

        private async void panel11_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportes());
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

        private async void label2_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel12_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void iconPictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void label3_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {

        }

        private async void panel14_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel15_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel13_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            //mainForm.openChildForm(new FrmTramiteIn(this));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel10_Paint(object sender, PaintEventArgs e)
        {
            mainForm.DisableButtons();
            //mainForm.openChildForm(new FrmTramiteIn(this));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }
    }
}
