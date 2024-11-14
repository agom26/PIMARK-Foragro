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
        public FrmDashboard(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
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
    }
}
