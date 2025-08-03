using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Alertas
{
    public partial class FrmLoading : Form
    {
        public Func<Task> ProcesoAsync { get; set; }

        public FrmLoading(Func<Task> procesoAsync)
        {
            InitializeComponent();
            if (procesoAsync == null)
            {
                throw new ArgumentNullException();
            }

            ProcesoAsync = procesoAsync;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                await ProcesoAsync(); // Espera la tarea
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                this.Close();
            }
        }
        private void FrmLoading_Load(object sender, EventArgs e)
        {

        }
    }
}
