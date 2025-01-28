using Comun.Cache;
using Dominio;
using FluentFTP;
using Presentacion.Alertas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmMostrarAbandonadas : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        RenovacionesMarcaModel renovacionesModel = new RenovacionesMarcaModel();
        TraspasosMarcaModel traspasosModel = new TraspasosMarcaModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;

        //ftp
        private string host = "ftp.bpa.com.es"; // Tu host FTP
        private string usuario = "test@bpa.com.es"; // Tu usuario FTP
        private string contraseña = "2O1VsAbUGbUo"; // Tu contraseña FTP
        private string directorioBase = "/bpa.com.es/test/marcas/internacionales";
        public FrmMostrarAbandonadas()
        {
            InitializeComponent();

            this.Load += FrmMostrarAbandonadas_Load;
            SeleccionarMarca.idInt = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;

        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }

        private async Task LoadMarcas()
        {
            totalRows = marcaModel.GetTotalMarcasInternacionalesEnAbandono();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var marcasN = await Task.Run(() => marcaModel.GetAllMarcasInternacionalesEnAbandono(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgMarcasAban.DataSource = marcasN;

                if (dtgMarcasAban.Columns["id"] != null)
                {
                    dtgMarcasAban.Columns["id"].Visible = false;
                    dtgMarcasAban.ClearSelection();
                }


            }));
        }

        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = marcaModel.GetFilteredMarcasInternacionalesEnAbandonoCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = marcaModel.FiltrarMarcasInternacionalesEnAbandono(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgMarcasAban.DataSource = titulares;
                    if (dtgMarcasAban.Columns["id"] != null)
                    {
                        dtgMarcasAban.Columns["id"].Visible = false;
                    }
                    dtgMarcasAban.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN MARCAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
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
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }

            tabControl1.SelectedTab = nombre;
        }
        // Método para mostrar el logo en un PictureBox
        public void MostrarLogoEnPictureBox(byte[] logo)
        {
            if (logo != null && logo.Length > 0) // Verificar que el logo no esté vacío
            {
                using (var ms = new MemoryStream(logo))
                {
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
        }
        public void mostrarPanelRegistro(string isRegistrada)
        {
            if (isRegistrada == "si")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel3.Visible = true;
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 64.69f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 35.31f;
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel3.Visible = false;
                tableLayoutPanel1.RowStyles[0].Height = 0;
            }
        }
        private void ActualizarFechaVencimiento()
        {
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = fecha_registro.AddYears(10).AddDays(-1);
            dateTimePFecha_vencimiento.Value = fecha_vencimiento;
        }


        public void LimpiarFormulario()
        {
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = null;
            txtNombreTitular.Text = "";
            txtDireccionTitular.Text = "";
            txtEntidadTitular.Text = "";
            txtNombreAgente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            AgregarEtapa.LimpiarEtapa();
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            checkBoxTienePoder.Checked = false;
        }

        private async Task CargarDatosMarca()
        {
            try
            {
                DataTable detallesMarcaInter = await Task.Run(() => marcaModel.GetMarcaInternacionalById(SeleccionarMarca.idInt));

                if (detallesMarcaInter.Rows.Count > 0)
                {
                    DataRow row = detallesMarcaInter.Rows[0];

                    if (row["expediente"] != DBNull.Value)
                    {
                        SeleccionarMarca.expediente = row["expediente"].ToString();
                        SeleccionarMarca.nombre = row["nombre"].ToString();
                        SeleccionarMarca.clase = row["clase"].ToString();
                        SeleccionarMarca.estado = row["estado"].ToString();
                        SeleccionarMarca.signoDistintivo = row["signoDistintivo"].ToString();
                        SeleccionarMarca.tipoSigno = row["Tipo"].ToString();
                        SeleccionarMarca.logo = row["logo"] is DBNull ? null : (byte[])row["logo"];
                        SeleccionarMarca.idPersonaTitular = Convert.ToInt32(row["idTitular"]);
                        SeleccionarMarca.idPersonaAgente = Convert.ToInt32(row["idAgente"]);
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
                        SeleccionarMarca.erenov = row["Erenov"].ToString();
                        SeleccionarMarca.etraspaso = row["Etrasp"].ToString();
                        SeleccionarMarca.tiene_poder = row["tiene_poder"] != DBNull.Value ? row["tiene_poder"].ToString() : string.Empty;
                        SeleccionarMarca.pais_de_registro = row["pais_de_registro"] != DBNull.Value ? row["pais_de_registro"].ToString() : string.Empty;
                        var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                        var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));

                        await Task.WhenAll(titularTask, agenteTask);

                        var titular = titularTask.Result;
                        var agente = agenteTask.Result;

                        SeleccionarPersona.idPersonaT = SeleccionarMarca.idPersonaTitular;
                        SeleccionarPersona.idPersonaA = SeleccionarMarca.idPersonaAgente;

                        if (titular.Count > 0)
                        {
                            txtNombreTitular.Text = titular[0].nombre;
                        }

                        if (agente.Count > 0)
                        {
                            txtNombreAgente.Text = agente[0].nombre;
                        }

                        int index = comboBox1.FindString(SeleccionarMarca.pais_de_registro);
                        comboBox1.SelectedIndex = index;

                        checkBoxTienePoder.Checked = SeleccionarMarca.tiene_poder.Equals("si", StringComparison.OrdinalIgnoreCase);



                        // Actualizar los controles 
                        txtExpediente.Text = SeleccionarMarca.expediente;
                        txtNombre.Text = SeleccionarMarca.nombre;
                        txtClase.Text = SeleccionarMarca.clase;
                        txtClase.Text = SeleccionarMarca.clase;
                        textBoxEstatus.Text = SeleccionarMarca.estado;
                        comboBoxSignoDistintivo.SelectedItem = SeleccionarMarca.signoDistintivo;
                        comboBoxTipoSigno.SelectedItem = SeleccionarMarca.tipoSigno;
                        MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                        datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                        richTextBox1.Text = SeleccionarMarca.observaciones;
                        txtERenovacion.Text = SeleccionarMarca.erenov;
                        txtETraspaso.Text = SeleccionarMarca.etraspaso;



                        bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("Registrada", StringComparison.OrdinalIgnoreCase);

                        if (contieneRegistrada)
                        {
                            checkBox1.Checked = true;
                            mostrarPanelRegistro("si");
                            SeleccionarMarca.registro = row["registro"].ToString();
                            SeleccionarMarca.folio = row["folio"].ToString();
                            SeleccionarMarca.libro = row["libro"].ToString();
                            SeleccionarMarca.fechaRegistro = Convert.ToDateTime(row["fechaRegistro"]);
                            SeleccionarMarca.fechaVencimiento = Convert.ToDateTime(row["fechaVencimiento"]);

                            txtRegistro.Text = SeleccionarMarca.registro;
                            txtFolio.Text = SeleccionarMarca.folio;
                            txtLibro.Text = SeleccionarMarca.libro;
                            dateTimePFecha_Registro.Value = SeleccionarMarca.fechaRegistro.Value;
                            dateTimePFecha_vencimiento.Value = SeleccionarMarca.fechaVencimiento.Value;
                        }
                        else
                        {
                            checkBox1.Checked = false;
                            mostrarPanelRegistro("no");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles de la marca", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void VerificarSeleccionIdMarcaEdicion()
        {
            if (dtgMarcasAban.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasAban.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasAban.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarca.idInt = id;
                    tabControl1.SelectedTab = tabPageMarcaDetail;
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private async void loadHistorialById()
        {
            try
            {
                var historial = await Task.Run(() => historialModel.GetHistorialMarcaById(SeleccionarMarca.idInt));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgHistorialAban.AutoGenerateColumns = true;
                    dtgHistorialAban.DataSource = historial;
                    dtgHistorialAban.Refresh();

                    if (dtgHistorialAban.Columns["id"] != null)
                    {
                        dtgHistorialAban.Columns["id"].Visible = false;
                    }

                    dtgHistorialAban.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void loadRenovacionesById()
        {
            try
            {
                DataTable renovaciones = await Task.Run(() => renovacionesModel.GetAllRenovacionesByIdMarca(SeleccionarMarca.idInt));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgRenovaciones.AutoGenerateColumns = true;
                    dtgRenovaciones.DataSource = renovaciones;
                    dtgRenovaciones.Refresh();

                    if (dtgRenovaciones.Columns["id"] != null)
                    {
                        dtgRenovaciones.Columns["id"].Visible = false;
                        dtgRenovaciones.Columns["IdMarca"].Visible = false;
                    }

                    dtgRenovaciones.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las renovaciones de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void loadTraspasosById()
        {
            try
            {
                DataTable traspasos = await Task.Run(() => traspasosModel.ObtenerTraspasosMarcaPorIdMarca(SeleccionarMarca.idInt));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgTraspasos.AutoGenerateColumns = true;
                    dtgTraspasos.DataSource = traspasos;
                    dtgTraspasos.Refresh();

                    if (dtgTraspasos.Columns["id"] != null)
                    {
                        dtgTraspasos.Columns["id"].Visible = false;
                        dtgTraspasos.Columns["IdMarca"].Visible = false;
                        dtgTraspasos.Columns["IdTitularAnterior"].Visible = false;
                        dtgTraspasos.Columns["IdTitularNuevo"].Visible = false;
                    }

                    dtgTraspasos.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los traspasos de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmMostrarAbandonadas_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadMarcas());
            tabControl1.SelectedTab = tabPageAbandonadasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageRenovacionesList);
            EliminarTabPage(tabPageTraspasosList);
            //EliminarTabPage(tabPageHistorialDetalle);
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageTraspasosList);
                //EliminarTabPage(tabPageHistorialDetalle);
            }
            else if (tabControl1.SelectedTab == tabPageAbandonadasList)
            {
                LoadMarcas();
                SeleccionarMarca.idInt = 0;
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageTraspasosList);
                //EliminarTabPage(tabPageHistorialDetalle);
            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                CargarDatosMarca();
                //EliminarTabPage(tabPageHistorialDetalle);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageTraspasosList);
            }
            else if (tabControl1.SelectedTab == tabPageRenovacionesList)
            {
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageHistorialMarca);
            }
            else if (tabControl1.SelectedTab == tabPageTraspasosList)
            {
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageHistorialMarca);
            }
        }
        public async void Ver()
        {
            VerificarSeleccionIdMarcaEdicion();
            if (SeleccionarMarca.idInt > 0)
            {
                Cursor = Cursors.WaitCursor;
                tabControl1.Visible = false;
                await CargarDatosMarca();
                AnadirTabPage(tabPageMarcaDetail);
                tabControl1.SelectedTab = tabPageMarcaDetail;
                tabControl1.Visible = true;
                Cursor = Cursors.Default;
            }
        }
        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Ver();

        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {
            loadHistorialById();
            AnadirTabPage(tabPageHistorialMarca);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarEstadoHistorial_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            loadRenovacionesById();
            AnadirTabPage(tabPageRenovacionesList);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roundedButton9_Click(object sender, EventArgs e)
        {
            loadTraspasosById();
            AnadirTabPage(tabPageTraspasosList);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void btnQuitarImagen_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            tabControl1.SelectedTab = tabPageAbandonadasList;
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {

        }

        private void dtgMarcasAban_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Ver();
        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {
            filtrar();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filtrar();
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (txtBuscar.Text != "")
            {
                filtrar();
            }
            else
            {
                await LoadMarcas();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (txtBuscar.Text != "")
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

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (txtBuscar.Text != "")
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
            if (txtBuscar.Text != "")
            {
                filtrar();
            }
            else
            {
                await LoadMarcas();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }
        private List<string> ListarNombresDeArchivos(string idMarca)
        {
            string carpetaMarca = $"{directorioBase}/marca-{idMarca}";
            var nombresArchivos = new List<string>();

            using (FtpClient cliente = new FtpClient(host))
            {
                cliente.Credentials = new NetworkCredential(usuario, contraseña);

                try
                {
                    cliente.Connect();

                    // Obtener listado de archivos en el directorio
                    var listado = cliente.GetListing(carpetaMarca);

                    foreach (var item in listado)
                    {
                        if (item.Type == FtpObjectType.File) // Solo archivos
                        {
                            nombresArchivos.Add(item.Name); // Agregar solo el nombre del archivo
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al listar archivos: {ex.Message}");
                }
                finally
                {
                    cliente.Disconnect();
                }
            }

            return nombresArchivos;
        }

        public void ListarArchivosEnGeneral()
        {
            try
            {
                // Cambiar el cursor global a "WaitCursor"
                Cursor.Current = Cursors.WaitCursor;

                AnadirTabPage(tabPageListaArchivos);
                tabControl1.Visible = false;

                string id = "" + SeleccionarMarca.idInt;
                CrearCarpetaMarca(id);

                // Obtener nombres de archivos desde el servidor FTP
                var nombresArchivos = ListarNombresDeArchivos(id);

                // Limpiar y configurar DataGridView
                dtgArchivos.DataSource = null;
                dtgArchivos.Columns.Clear();
                dtgArchivos.Columns.Add("NombreArchivo", "Nombre del Archivo");

                // Agregar los nombres al DataGridView
                foreach (var nombre in nombresArchivos)
                {
                    dtgArchivos.Rows.Add(nombre);
                }

                dtgArchivos.ClearSelection();
                tabControl1.Visible = true;
            }
            finally
            {
                // Restaurar el cursor global a "Default"
                Cursor.Current = Cursors.Default;
            }
        }
        private void AbrirArchivoDesdeFtp(string idMarca, string archivoNombre)
        {
            string carpeta = $"{directorioBase}/marca-{idMarca}/";
            string rutaRemota = $"{carpeta}/{archivoNombre}";
            string rutaLocal = System.IO.Path.Combine(System.IO.Path.GetTempPath(), archivoNombre); // Carpeta temporal

            try
            {
                using (var cliente = new FtpClient(host, usuario, contraseña))
                {
                    cliente.Connect();

                    // Descargar el archivo al directorio temporal
                    cliente.DownloadFile(rutaLocal, rutaRemota, FtpLocalExists.Overwrite, FtpVerify.None);
                }

                // Asegúrate de que el archivo existe localmente antes de abrirlo
                if (File.Exists(rutaLocal))
                {
                    // Abre el archivo con la aplicación predeterminada de manera confiable
                    var process = new System.Diagnostics.Process
                    {
                        StartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = rutaLocal,
                            UseShellExecute = true // Importante para manejar rutas complejas
                        }
                    };
                    process.Start();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("EL ARCHIVO NO SE DESCARGÓ CORRECTAMENTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo: {ex.Message}");
            }
        }

        public void Abrir()
        {
            string idMarca = "" + SeleccionarMarca.idInt; // Id de la marca actual
            string archivoNombre = dtgArchivos.CurrentRow?.Cells[0].Value?.ToString(); // Archivo seleccionado

            if (string.IsNullOrEmpty(archivoNombre))
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN ARCHIVO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            AbrirArchivoDesdeFtp(idMarca, archivoNombre);
            Cursor.Current = Cursors.Default;
        }

        private void EliminarArchivoDesdeFtp(string idMarca, string archivoNombre)
        {
            string carpeta = $"{directorioBase}/marca-{idMarca}/";
            string rutaRemota = $"{carpeta}/{archivoNombre}";

            try
            {
                using (var cliente = new FtpClient(host, usuario, contraseña))
                {
                    cliente.Connect();

                    // Verifica si el archivo existe antes de intentar eliminarlo
                    if (cliente.FileExists(rutaRemota))
                    {
                        cliente.DeleteFile(rutaRemota);
                        FrmAlerta alerta = new FrmAlerta("ARCHIVO ELIMINADO EXITOSAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                    }
                    else
                    {
                        FrmAlerta alerta = new FrmAlerta("EL ARCHIVO NO EXISTE EN EL SERVIDOR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL ELIMINAR EL ARCHIVO: " + ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        public void Eliminar()
        {
            string idMarca = "" + SeleccionarMarca.idInt; // Id de la marca actual
            string archivoNombre = dtgArchivos.CurrentRow?.Cells[0].Value?.ToString(); // Archivo seleccionado

            if (string.IsNullOrEmpty(archivoNombre))
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN ARCHIVO A ELIMINAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                return;
            }

            FrmAlerta alerta2 = new FrmAlerta($"¿ESTÁ SEGURO DE ELIMINAR EL ARCHIVO \"{archivoNombre}\"?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            var confirmacion = alerta2.ShowDialog();

            if (confirmacion == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                EliminarArchivoDesdeFtp(idMarca, archivoNombre);

                // Actualizar la lista de archivos en el DataGridView
                ListarArchivosEnGeneral();
                Cursor.Current = Cursors.Default;
            }
        }

        public void CrearCarpetaMarca(string idMarca)
        {
            string carpetaMarca = $"{directorioBase}/marca-{idMarca}"; // Ruta completa para la carpeta de la marca

            using (FtpClient cliente = new FtpClient(host))
            {
                cliente.Credentials = new NetworkCredential(usuario, contraseña);

                try
                {
                    cliente.Connect(); // Conecta al servidor FTP

                    // Verifica si la carpeta ya existe
                    if (!cliente.DirectoryExists(carpetaMarca))
                    {
                        cliente.CreateDirectory(carpetaMarca); // Crea la carpeta
                        //MessageBox.Show($"Carpeta creada exitosamente: {carpetaMarca}");
                    }
                    else
                    {
                        //MessageBox.Show($"La carpeta ya existe: {carpetaMarca}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al crear la carpeta: {ex.Message}");
                }
                finally
                {
                    cliente.Disconnect(); // Desconecta del servidor FTP
                }
            }
        }
        private void SubirArchivo(string idMarca)
        {
            string carpeta = $"{directorioBase}/marca-{idMarca}/";

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Seleccione un archivo para subir",
                Filter = "Todos los archivos (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string archivoLocal1 = openFileDialog.FileName;
                string nombreArchivo1 = System.IO.Path.GetFileName(archivoLocal1);

                try
                {
                    using (var client = new FtpClient(host, usuario, contraseña))
                    {
                        client.Connect();

                        // Crear carpeta si no existe
                        if (!client.DirectoryExists(carpeta))
                        {
                            client.CreateDirectory(carpeta);
                        }

                        // Subir el archivo
                        string rutaRemota = $"{carpeta}/{nombreArchivo1}";
                        client.UploadFile(archivoLocal1, rutaRemota, FtpRemoteExists.Overwrite);

                        FrmAlerta alerta = new FrmAlerta("ARCHIVO SUBIDO EXITOSAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al subir el archivo: {ex.Message}");
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void roundedButton11_Click(object sender, EventArgs e)
        {
            ListarArchivosEnGeneral();
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            //SubirArchivo("" + SeleccionarMarca.idInt);
            //ListarArchivosEnGeneral();
        }

        private void iconButton12_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void dtgArchivos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Abrir();
        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            //Eliminar();
        }
    }
}
