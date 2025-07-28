using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System.Data;
using System.Runtime.InteropServices;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMostrarMarcasN : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool buscando = false;
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();


        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmMostrarMarcasN()
        {
            InitializeComponent();
            this.Load += FrmMostrarMarcasN_Load; // Mueve la lógica de carga aquí
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            SeleccionarMarcaOposicion.LimpiarMarcaOposicion();
            this.Close();
        }

        private async Task LoadMarcas()
        {
            totalRows = await marcaModel.GetTotalMarcasNacionales();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var marcas = await marcaModel.GetAllMarcasNacionales(currentPageIndex, pageSize);

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
                totalRows = await marcaModel.GetFilteredMarcasNacionalesCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = await marcaModel.FiltrarMarcasNacionales(buscar, currentPageIndex, pageSize);
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

        private void CentrarPanel(Panel panel)
        {
            int anchoMinimo = panel.Width + 100;

            if (this.ClientSize.Width >= anchoMinimo)
            {
                panel.Anchor = AnchorStyles.Top;
                int x = (this.ClientSize.Width - panel.Width) / 2;
                int y = panel.Location.Y;
                panel.Location = new Point(x, y);
            }
            else
            {
                panel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panel.Location = new Point(0, panel.Location.Y);
            }
        }

        private async void FrmMostrarMarcasN_Load(object sender, EventArgs e)
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            if (screenWidth <= 1105 && screenHeight <= 600)
            {
                this.Size = new Size(750, 540);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.AutoScroll = true;

                panelSuperior.Width = this.ClientSize.Width - 20;
                panelInferior.Width = this.ClientSize.Width - 20;

                tblLayoutPrincipal.Dock = DockStyle.Fill;
                panel1.Dock = DockStyle.Fill;
                panel2.Dock = DockStyle.Fill;
                panel3.Dock = DockStyle.Fill;
                dtgTitulares.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 8, FontStyle.Bold);

                dtgTitulares.DefaultCellStyle.Font = new Font("Century Gothic", 8);

            }
            else
            {
                dtgTitulares.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
                dtgTitulares.DefaultCellStyle.Font = new Font("Century Gothic", 10);
                // Pantalla grande → centrar los paneles
                CentrarPanel(panelSuperior);
                CentrarPanel(panelInferior);
            }

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

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = await marcaModel.GetFilteredMarcasNacionalesCount(txtBuscar.Text);
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
                // Usa DataBoundItem para acceder al objeto vinculado a la fila seleccionada
                var filaSeleccionada = dtgTitulares.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarcaOposicion.idMarca = id;
                    SeleccionarMarcaOposicion.nombreSigno = dataRowView["SIGNO"].ToString();
                    SeleccionarMarcaOposicion.idTitularMarca = Convert.ToInt32(dataRowView["IdTitular"]);
                    SeleccionarMarcaOposicion.nombreTitular = dataRowView["TITULAR"].ToString();
                    this.Close();
                    /*
                    var detallesTitular = personaModel.GetPersonaById(id);

                    if (detallesTitular.Count > 0)
                    {
                        //MessageBox.Show("ID seleccionado: " + SeleccionarPersona.idPersona);

                        // Asignar los valores obtenidos a la clase SeleccionarPersona
                        SeleccionarPersona.nombre = detallesTitular[0].nombre;
                        SeleccionarPersona.direccion = detallesTitular[0].direccion;
                        SeleccionarPersona.correo = detallesTitular[0].correo;
                        SeleccionarPersona.contacto = detallesTitular[0].contacto;
                        SeleccionarPersona.pais = detallesTitular[0].pais;
                        SeleccionarPersona.nit = detallesTitular[0].nit;
                        SeleccionarPersona.telefono = detallesTitular[0].telefono;

                        this.Close(); // Cierra el formulario si todo fue correcto
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del titular", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }*/
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA MARCA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = await marcaModel.GetFilteredMarcasNacionalesCount(txtBuscar.Text);
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

        private void panelSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x83;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                m.Result = new IntPtr(0xF0);   // Align client area to all borders
                return;
            }
            base.WndProc(ref m);
        }
    }
}
