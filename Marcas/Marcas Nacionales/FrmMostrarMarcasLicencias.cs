using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System.Data;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMostrarMarcasLicencias : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;
        public FrmMostrarMarcasLicencias()
        {
            InitializeComponent();
            this.Load += FrmMostrarMarcasLicencias_Load; // Mueve la lógica de carga aquí
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarMarcaOposicion.LimpiarMarcaOposicion();
            this.Close();
        }

        private async Task LoadMarcas()
        {
            totalRows = marcaModel.GetTotalMarcasNacionalesParaLicencia();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var marcas = await Task.Run(() => marcaModel.GetAllMarcasNacionalesParaLicencia(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgTitulares.DataSource = marcas;

                if (dtgTitulares.Columns["id"] != null)
                {
                    dtgTitulares.Columns["id"].Visible = false;
                    dtgTitulares.Columns["IdTitular"].Visible = false;
                    dtgTitulares.ClearSelection();
                }


            }));
        }
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = marcaModel.GetFilteredMarcasNacionalesParaLicenciaCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = marcaModel.FiltrarMarcasNacionalesParaLicencia(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgTitulares.DataSource = titulares;
                    if (dtgTitulares.Columns["id"] != null)
                    {
                        dtgTitulares.Columns["id"].Visible = false;

                        dtgTitulares.Columns["IdTitular"].Visible = false;
                    }
                    dtgTitulares.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN MARCAS NACIONALES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadMarcas();
                }
            }
            else
            {
                await LoadMarcas();
            }
        }

        private async void FrmMostrarMarcasLicencias_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
            await Task.Run(() => LoadMarcas());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionarMarcaOposicion.LimpiarMarcaOposicion();
            this.Close();
        }

        private void dtgTitulares_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = marcaModel.GetFilteredMarcasNacionalesParaLicenciaCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
            
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            
            if (dtgTitulares.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgTitulares.SelectedRows.Count > 0) // Verifica si hay filas seleccionadas
            {
                buscando = false;
                var filaSeleccionada = dtgTitulares.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarcaParaLicencia.idMarca = id;
                    SeleccionarMarcaParaLicencia.nombreSigno = dataRowView["SIGNO"].ToString();
                    SeleccionarMarcaParaLicencia.idTitularMarca = (int)dataRowView["IdTitular"];
                    SeleccionarMarcaParaLicencia.nombreTitular = dataRowView["TITULAR"].ToString();
                    SeleccionarMarcaParaLicencia.expediente = dataRowView["EXPEDIENTE"].ToString();
                    SeleccionarMarcaParaLicencia.clase = dataRowView["CLASE"].ToString();
                    SeleccionarMarcaParaLicencia.signoDistintivo = dataRowView["SIGNO_DISTINTIVO"].ToString();
                    SeleccionarMarcaParaLicencia.registro = dataRowView["REGISTRO"].ToString();
                    SeleccionarMarcaParaLicencia.folio = dataRowView["FOLIO"].ToString();
                    SeleccionarMarcaParaLicencia.tomo = dataRowView["TOMO"].ToString();
                    SeleccionarMarcaParaLicencia.fechaVencimiento = Convert.ToDateTime(dataRowView["F_VENCIMIENTO"].ToString());


                    this.Close();
                    
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA MARCA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
            }
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (buscando == true)
                {
                    filtrar();
                }
                else
                {
                    await LoadMarcas();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (buscando==true)
            {
                filtrar();
            }
            else
            {
                await LoadMarcas();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (buscando == true)
                {
                    filtrar();
                }
                else
                {
                    await LoadMarcas();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (buscando == true)
            {
                filtrar();
            }
            else
            {
                await LoadMarcas();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = marcaModel.GetFilteredMarcasNacionalesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                filtrar();
            }
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            filtrar();
        }
    }
}
