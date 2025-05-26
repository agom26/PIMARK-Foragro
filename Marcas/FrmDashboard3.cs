using Dominio;
using Presentacion.Marcas_Internacionales;
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
    public partial class FrmDashboard3 : Form
    {
        public Form1 mainForm;
        VencimientoModel vencimientoModel = new VencimientoModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        public async void Ejecutar()
        {
            await Task.Run(() => vencimientoModel.EjecutarProcedimiento());
            await Task.Run(() => LoadVencimientos());
        }

        private async void LoadVencimientos()
        {
            currentPageIndex = 1;
            var titulares = await Task.Run(() => vencimientoModel.GetAllVencimientos(currentPageIndex, pageSize));
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    dtgVencimientos.DataSource = titulares;

                    if (dtgVencimientos.Columns["id"] != null)
                    {
                        dtgVencimientos.Columns["id"].Visible = false;
                        dtgVencimientos.Columns["marcaID"].Visible = false;
                        dtgVencimientos.Columns["patenteID"].Visible = false;

                    }
                    dtgVencimientos.Refresh();
                }));
            }


                
        }
        public FrmDashboard3(Form1 mainForm)
        {
            InitializeComponent();
            this.Load += FrmDashboard3_Load;
            this.mainForm = mainForm;
            Ejecutar();
        }

        private async void roundedButton3_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmVencimientos());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private void FrmDashboard3_Load(object sender, EventArgs e)
        {

        }

        private async void iconPictureBox2_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportesMarcasPatentes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void roundedButton4_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmTramiteInicialInternacional(mainForm));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void iconPictureBox1_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmTramiteInicialInternacional(mainForm));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void label1_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmTramiteInicialInternacional(mainForm));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel5_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmTramiteInicialInternacional(mainForm));
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void roundedButton5_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportesMarcasPatentes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel6_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportesMarcasPatentes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void label2_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmReportesMarcasPatentes());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void roundedButton6_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmMarcasIntRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void iconPictureBox3_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmMarcasIntRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void label3_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmMarcasIntRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private async void panel8_Click(object sender, EventArgs e)
        {
            mainForm.DisableButtons();
            mainForm.openChildForm(new FrmMarcasIntRegistradas());
            await Task.Delay(1000);
            mainForm.EnableButtons();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
