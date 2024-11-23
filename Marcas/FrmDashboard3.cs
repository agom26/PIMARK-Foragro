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

namespace Presentacion
{
    public partial class FrmDashboard3 : Form
    {
        public Form1 mainForm;
        VencimientoModel vencimientoModel = new VencimientoModel();

        public async void Ejecutar()
        {
            await Task.Run(() => vencimientoModel.EjecutarProcedimiento());
            await Task.Run(() => LoadVencimientos());
        }
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
        public FrmDashboard3(Form1 mainForm)
        {
            InitializeComponent();
            this.Load += FrmDashboard3_Load;
            this.mainForm = mainForm;
            Ejecutar();
        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {

        }

        private void FrmDashboard3_Load(object sender, EventArgs e)
        {

        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
