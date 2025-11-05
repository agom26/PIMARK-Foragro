using Dominio;
using MySql.Data.MySqlClient;
using Presentacion.Marcas_Internacionales;
using Presentacion.Marcas_Nacionales;
using Presentacion.Reportes;
using Presentacion.Plazos;
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
using Comun;
using Comun.Cache;

namespace Presentacion
{
    public partial class FrmDashboard3 : Form, IAsyncLoadable
    {
        public Form1 mainForm;
        PlazosModel plazosModel = new PlazosModel();
        VencimientoModel vencimientoModel = new VencimientoModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;

        public async Task LoadAsync()
        {
            await LoadPlazos(); // aquí llamas a tu método actual
            await LoadVencimientos();
        }

        public async void Ejecutar()
        {
            try
            {
                await  vencimientoModel.EjecutarProcedimiento();
                await  LoadVencimientos();
                await LoadPlazos();
            }
            catch (MySqlException ex) when (ex.Number == 1042) // Ejemplo: error de conexión MySQL
            {
                MessageBox.Show("No se pudo establecer conexión con la base de datos.",
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los datos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadPlazos()
        {
            try
            {

                var (totalRows, datos) = await plazosModel.ObtenerPlazosAsync("marca", pageSize, currentPageIndex);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                // Como estás modificando la UI, necesitas volver al hilo principal
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        //lblTotalPages.Text = totalPages.ToString();
                        //lblTotalRows.Text = totalRows.ToString();
                        dtgPlazos.DataSource = datos;
                    }));
                }
            }
            catch (MySqlException ex) when (ex.Number == 1042)
            {
                MessageBox.Show("No se pudo establecer conexión con la base de datos.",
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los datos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadVencimientos()
        {

            currentPageIndex = 1;
            var titulares = await vencimientoModel.GetAllVencimientos(currentPageIndex, pageSize);
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
            this.mainForm = mainForm;
            Ejecutar();


            if (UsuarioActivo.soloLectura)
            {
                roundedButtonIngresar.Enabled = false;
                panelIngresar.Enabled = false;
                labelIngresar.Enabled = false;
                iconPictureBoxIngresar.Enabled = false;
            }
            else
            {
                roundedButtonIngresar.Enabled = true;
                panelIngresar.Enabled = true;
                labelIngresar.Enabled = true;
                iconPictureBoxIngresar.Enabled = true;
            }

        }

        private async void roundedButton3_Click(object sender, EventArgs e)
        {
            await mainForm.OpenChildFormAsync(new FrmVencimientos());
            //mainForm.openChildForm(new FrmVencimientos());

        }

        private void FrmDashboard3_Load(object sender, EventArgs e)
        {

        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {

            mainForm.openChildForm(new FrmReportesMarcasPatentes());

        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {

            mainForm.openChildForm(new FrmTramiteInicialInternacional(mainForm));

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

            mainForm.openChildForm(new FrmTramiteInicialInternacional(mainForm));

        }

        private void label1_Click(object sender, EventArgs e)
        {

            mainForm.openChildForm(new FrmTramiteInicialInternacional(mainForm));

        }

        private void panel5_Click(object sender, EventArgs e)
        {

            mainForm.openChildForm(new FrmTramiteInicialInternacional(mainForm));

        }

        private void roundedButton5_Click(object sender, EventArgs e)
        {

            mainForm.openChildForm(new FrmReportesMarcasPatentes());

        }

        private void panel6_Click(object sender, EventArgs e)
        {

            mainForm.openChildForm(new FrmReportesMarcasPatentes());

        }

        private void label2_Click(object sender, EventArgs e)
        {

            mainForm.openChildForm(new FrmReportesMarcasPatentes());

        }

        private async void roundedButton6_Click(object sender, EventArgs e)
        {

            await mainForm.OpenChildFormAsync(new FrmMarcasIntRegistradas());

        }

        private async void iconPictureBox3_Click(object sender, EventArgs e)
        {

            await mainForm.OpenChildFormAsync(new FrmMarcasIntRegistradas());

        }

        private async void label3_Click(object sender, EventArgs e)
        {
            await mainForm.OpenChildFormAsync(new FrmMarcasIntRegistradas());

        }

        private async void panel8_Click(object sender, EventArgs e)
        {

            await mainForm.OpenChildFormAsync(new FrmMarcasIntRegistradas());
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void roundedButton7_Click(object sender, EventArgs e)
        {
            await mainForm.OpenChildFormAsync(new FrmPlazos());
        }

        private void dtgPlazos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgPlazos.Columns["id"] != null)
            {
                dtgPlazos.Columns["id"].Visible = false;
            }

            if (dtgPlazos.Columns["IdMarca"] != null)
            {
                dtgPlazos.Columns["IdMarca"].Visible = false;
            }


            if (dtgPlazos.Columns["IdPatente"] != null)
            {
                dtgPlazos.Columns["IdPatente"].Visible = false;
            }

            dtgPlazos.ClearSelection();
        }
    }
}
