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

namespace Presentacion.Patentes
{
    public partial class FrmMostrarIngresadasPatentes : Form
    {
        PatenteModel patenteModel= new PatenteModel();
        public FrmMostrarIngresadasPatentes()
        {
            InitializeComponent();
        }
        private async Task LoadPatentes()
        {
            var patentes = await Task.Run(() => patenteModel.GetAllPatentesEnTramite());


            Invoke(new Action(() =>
            {
                dtgPatentes.DataSource = patentes;
                dtgPatentes.Refresh();
                
                if (dtgPatentes.Columns["id"] != null)
                {
                    dtgPatentes.Columns["id"].Visible = false;
                    dtgPatentes.ClearSelection();
                }
            }));
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }

        private async void FrmMostrarIngresadasPatentes_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadPatentes());
            tabControl1.SelectedTab = tabPageIngresadasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageHistorialMarca);
        }
    }
}
