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
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private async void LoadVencimientos()
        {
            currentPageIndex = 1;
            var titulares = await Task.Run(() => vencimientoModel.GetAllVencimientos(currentPageIndex, pageSize));
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
            
            mainForm.openChildForm(new FrmVencimientos());
           
        }

        private async void roundedButton2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }

        private async void panel10_Click(object sender, EventArgs e)
        {
            
            //mainForm.openChildForm(new FrmTramiteIn(this));
           
        }

        private async void iconPictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private async void label1_Click(object sender, EventArgs e)
        {
            
        }

        private async void panel11_Click(object sender, EventArgs e)
        {
           
            mainForm.openChildForm(new FrmReportes());
           
        }

        private async void iconPictureBox2_Click(object sender, EventArgs e)
        {
           
            mainForm.openChildForm(new FrmReportes());
            
        }

        private async void label2_Click(object sender, EventArgs e)
        {
           
            mainForm.openChildForm(new FrmReportes());
           
        }

        private async void panel12_Click(object sender, EventArgs e)
        {
            
            mainForm.openChildForm(new FrmRegistradas());
            
        }

        private async void iconPictureBox3_Click(object sender, EventArgs e)
        {
            
            mainForm.openChildForm(new FrmRegistradas());
          
        }

        private async void label3_Click(object sender, EventArgs e)
        {
           
            mainForm.openChildForm(new FrmRegistradas());
           
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {

        }

        private async void panel14_Click(object sender, EventArgs e)
        {
           
            mainForm.openChildForm(new FrmReportes());
           
        }

        private async void panel15_Click(object sender, EventArgs e)
        {
            mainForm.openChildForm(new FrmRegistradas());
            
        }

        private async void panel13_Click(object sender, EventArgs e)
        {
           
            //mainForm.openChildForm(new FrmTramiteIn(this));
           
        }

        private async void panel10_Paint(object sender, PaintEventArgs e)
        {
           
            //mainForm.openChildForm(new FrmTramiteIn(this));
           
        }
    }
}
