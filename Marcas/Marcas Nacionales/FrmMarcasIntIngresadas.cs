using Comun.Cache;
using Dominio;
using Microsoft.Win32;
using Presentacion.Alertas;
using Presentacion.Marcas_Nacionales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FluentFTP;
using Microsoft.VisualBasic.Logging;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMarcasIntIngresadas : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento;
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        private bool archivoSubido = false;
        private bool buscando = false;

        //ftp
        private string host = "ftp.bpa.com.es"; // Tu host FTP
        private string usuario = "test@bpa.com.es"; // Tu usuario FTP
        private string contraseña = "2O1VsAbUGbUo"; // Tu contraseña FTP
        private string directorioBase = "/bpa.com.es/test/marcas/nacionales"; // La ruta base de tu servidor
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }
        public FrmMarcasIntIngresadas()
        {
            InitializeComponent();
            this.Load += FrmMarcasIntIngresadas_Load;
            SeleccionarMarca.idN = 0;
            archivoSubido = false;
            ActualizarFechaVencimiento();
            btnAdjuntarT.Visible = false;
            dtgMarcasIn.DataBindingComplete += dtgMarcasIn_DataBindingComplete;

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
            totalRows = marcaModel.GetTotalMarcasSinRegistro();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            var marcasN = await Task.Run(() => marcaModel.GetAllMarcasNacionalesEnTramite(currentPageIndex, pageSize));


            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke(() =>
                {
                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();
                    dtgMarcasIn.DataSource = marcasN;
                });
            }
        }

        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {

                DataTable titulares = marcaModel.FiltrarMarcasNacionalesEnTramite(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgMarcasIn.DataSource = titulares;
                    if (dtgMarcasIn.Columns["id"] != null)
                    {
                        dtgMarcasIn.Columns["id"].Visible = false;
                    }
                    dtgMarcasIn.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN MARCAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
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

        public void MostrarLogoEnPictureBox(byte[] logo)
        {
            if (logo != null && logo.Length > 0)
            {
                using (var ms = new MemoryStream(logo))
                {
                    pictureBox1.Image = Image.FromStream(ms);
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
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 64.69f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 35.31f;
                btnAdjuntarT.Visible = true;
            }
            else
            {
                btnAdjuntarT.Visible = false;
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                tableLayoutPanel1.RowStyles[0].Height = 0;
            }

        }
        private void ActualizarFechaVencimiento()
        {
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = fecha_registro.AddYears(10).AddDays(-1);
            dateTimePFecha_vencimiento.Value = fecha_vencimiento;
        }
        private bool ValidarCampo(string campo, string mensaje)
        {
            if (string.IsNullOrEmpty(campo))
            {
                FrmAlerta alerta = new FrmAlerta(mensaje.ToUpper(), "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidarCampos(string expediente, string nombre, string clase, string signoDistintivo, string tipo, string estado,
   ref byte[] logo, bool registroChek, string registro, string folio, string libro)
        {
            // Verificar campos obligatorios
            if (!ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
                !ValidarCampo(nombre, "Por favor, ingrese el signo.") ||
                !ValidarCampo(clase, "Por favor, ingrese la clase.") ||
                !ValidarCampo(signoDistintivo, "Por favor, seleccione un signo distintivo.") ||
                !ValidarCampo(tipo, "Por favor, seleccione un tipo.") ||
                !ValidarCampo(estado, "Por favor, seleccione un estado."))
            {
                return false;
            }

            // Validar que el expediente, clase, folio, registro y libro sean enteros
            if (
                !int.TryParse(clase, out _) ||
                (registroChek && !int.TryParse(folio, out _)) ||
                (registroChek && !int.TryParse(libro, out _)))
            {
                FrmAlerta alerta = new FrmAlerta("LA CLASE, FOLIO Y TOMO\nDEBEN SER VAORES NUMÉRICOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("El expediente, clase, folio, registro y libro deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comboBoxSignoDistintivo.SelectedItem.ToString() == "Marca" &&
               comboBoxTipoSigno.SelectedItem.ToString() == "Gráfica/Figurativa" || comboBoxTipoSigno.SelectedItem.ToString() == "Mixta")
            {
                // Verificar que hay una imagen
                if (pictureBox1.Image != null && pictureBox1.Image != documento)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        logo = ms.ToArray();
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("INGRESE UNA IMAGEN", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return false;
                }
            }
            else
            {
                logo = null;
            }


            // Si está registrada, se verifica la información del registro
            if (registroChek)
            {
                // Validar campos adicionales para marcas registradas
                if (!ValidarCampo(folio, "Por favor, ingrese el número de folio.") ||
                    !ValidarCampo(registro, "Por favor, ingrese el número de registro.") ||
                    !ValidarCampo(libro, "Por favor, ingrese el número de tomo.")
                    )
                {
                    return false;
                }
            }

            return true; // Todas las validaciones pasaron
        }


        public async void ActualizarMarcaInternacional()
        {
            try
            {
                byte[]? logo = null;
                int? idCliente = SeleccionarPersona.idPersonaC;

                if(idCliente==null || idCliente==0 ||idCliente<=0)
                {
                    idCliente = null;
                }

                if (SeleccionarPersona.idPersonaT <= 0 || SeleccionarPersona.idPersonaA <= 0)
                {
                    FrmAlerta alerta = new FrmAlerta("SELECCIONE UN TITULAR Y UN AGENTE VÁLIDOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return;
                }

                if (!ValidarCampos(txtExpediente.Text, txtNombre.Text, txtClase.Text, comboBoxSignoDistintivo.SelectedItem?.ToString(), comboBoxTipoSigno.SelectedItem?.ToString(), textBoxEstatus.Text, ref logo, checkBox1.Checked, txtRegistro.Text.Trim(), txtFolio.Text, txtLibro.Text))
                {
                    return;
                }
                /*
                if (logo == null)
                {
                    MessageBox.Show("Por favor, cargue un logo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }*/

                bool esActualizado = false;

                {
                    if (checkBox1.Checked)
                    {
                        esActualizado=marcaModel.EditMarcaNacionalRegistrada(SeleccionarMarca.idN, txtExpediente.Text, txtNombre.Text, comboBoxSignoDistintivo.SelectedItem?.ToString(), comboBoxTipoSigno.SelectedItem?.ToString(), txtClase.Text, txtFolio.Text, txtLibro.Text, logo, SeleccionarPersona.idPersonaT, SeleccionarPersona.idPersonaA, datePickerFechaSolicitud.Value, txtRegistro.Text.Trim(), dateTimePFecha_Registro.Value, dateTimePFecha_vencimiento.Value, null, null, idCliente);
                    }
                    else
                    {
                        esActualizado = marcaModel.EditMarcaNacional(SeleccionarMarca.idN, txtExpediente.Text, txtNombre.Text, comboBoxSignoDistintivo.SelectedItem?.ToString(), comboBoxTipoSigno.SelectedItem?.ToString(), txtClase.Text, logo, SeleccionarPersona.idPersonaT, SeleccionarPersona.idPersonaA, datePickerFechaSolicitud.Value, idCliente);
                    }
                }

                if (esActualizado)
                {

                    FrmAlerta frmAlerta = new FrmAlerta("MARCA ACTUALIZADA CON ÉXITO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmAlerta.ShowDialog();
                    SeleccionarMarca.idN = 0;
                    EliminarTabPage(tabPageHistorialMarca);
                    EliminarTabPage(tabPageListaArchivos);
                    AnadirTabPage(tabPageIngresadasList);
                    await LoadMarcas();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Error al actualizar la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la marca internacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LimpiarControles()
        {
            convertirImagen();
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            textBoxEstatus.Text = "";
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            datePickerFechaSolicitud.Value = DateTime.Today;
            dateTimePFecha_Registro.Value = DateTime.Now;
            richTextBox1.Text = "";
            pictureBox1.Image = documento;
            txtNombreTitular.Text = "";
            txtNombreAgente.Text = "";
            txtNombreCliente.Text = "";
            checkBox1.Checked = false;
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = null;
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            AgregarEtapa.LimpiarEtapa();
        }

        public void LimpiarFormulario()
        {
            convertirImagen();
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = documento;
            txtNombreTitular.Text = "";
            txtNombreAgente.Text = "";
            txtNombreCliente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            AgregarEtapa.LimpiarEtapa();
            SeleccionarPersona.idPersonaA = 0;
            SeleccionarPersona.idPersonaT = 0;
            SeleccionarPersona.idPersonaC = null;
        }

        private async Task CargarDatosMarca()
        {
            try
            {

                DataTable detallesMarcaInter = await Task.Run(() => marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN));

                if (detallesMarcaInter.Rows.Count > 0)
                {
                    DataRow row = detallesMarcaInter.Rows[0];

                    byte[] logo = null;

                    // Asignar los valores de los campos de la marca
                    SeleccionarMarca.expediente = row["expediente"] != DBNull.Value ? row["expediente"].ToString() : string.Empty;
                    SeleccionarMarca.nombre = row["nombre"] != DBNull.Value ? row["nombre"].ToString() : string.Empty;
                    SeleccionarMarca.clase = row["clase"] != DBNull.Value ? row["clase"].ToString() : string.Empty;
                    SeleccionarMarca.estado = row["estado"] != DBNull.Value ? row["estado"].ToString() : string.Empty;
                    SeleccionarMarca.signoDistintivo = row["signoDistintivo"] != DBNull.Value ? row["signoDistintivo"].ToString() : string.Empty;
                    SeleccionarMarca.tipoSigno = row["Tipo"] != DBNull.Value ? row["Tipo"].ToString() : string.Empty;
                    SeleccionarMarca.logo = row["logo"] is DBNull ? null : (byte[])row["logo"];

                    logo = row["logo"] != DBNull.Value ? (byte[])row["logo"] : null;

                    if (logo != null)
                    {
                        using (MemoryStream ms = new MemoryStream(logo))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        convertirImagen();
                        pictureBox1.Image = documento;
                    }

                    SeleccionarMarca.idPersonaTitular = row["idTitular"] != DBNull.Value ? Convert.ToInt32(row["idTitular"]) : 0;
                    SeleccionarMarca.idPersonaAgente = row["idAgente"] != DBNull.Value ? Convert.ToInt32(row["idAgente"]) : 0;
                    SeleccionarMarca.idPersonaCliente = row["idCliente"] != DBNull.Value ? Convert.ToInt32(row["idCliente"]) : 0;
                    SeleccionarMarca.fecha_solicitud = row["fechaSolicitud"] != DBNull.Value ? Convert.ToDateTime(row["fechaSolicitud"]) : DateTime.MinValue;
                    SeleccionarMarca.observaciones = row["observaciones"] != DBNull.Value ? row["observaciones"].ToString() : string.Empty;

                    var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                    var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));

                    var clienteTask = SeleccionarMarca.idPersonaCliente != 0
                        ? Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaCliente))
                        : null;

                    await Task.WhenAll(titularTask, agenteTask);

                    var titular = titularTask.Result;
                    var agente = agenteTask.Result;
                    var cliente = clienteTask?.Result;

                    SeleccionarPersona.idPersonaT = SeleccionarMarca.idPersonaTitular;
                    SeleccionarPersona.idPersonaA = SeleccionarMarca.idPersonaAgente;
                    SeleccionarPersona.idPersonaC = SeleccionarMarca.idPersonaCliente;

                    if (titular.Count > 0)
                    {
                        txtNombreTitular.Text = titular[0].nombre;
                    }

                    if (agente.Count > 0)
                    {
                        txtNombreAgente.Text = agente[0].nombre;
                    }

                    if (cliente != null && cliente.Count > 0)
                    {
                        txtNombreCliente.Text = cliente[0].nombre;
                    }
                    else
                    {
                        txtNombreCliente.Text = "";
                    }

                    txtExpediente.Text = SeleccionarMarca.expediente;
                    txtNombre.Text = SeleccionarMarca.nombre;
                    txtClase.Text = SeleccionarMarca.clase;
                    textBoxEstatus.Text = SeleccionarMarca.estado;
                    comboBoxSignoDistintivo.SelectedItem = SeleccionarMarca.signoDistintivo;
                    comboBoxTipoSigno.SelectedItem = SeleccionarMarca.tipoSigno;
                    datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                    richTextBox1.Text = SeleccionarMarca.observaciones;

                    bool contieneRegistrada = marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idN);
                    if (contieneRegistrada)
                    {
                        checkBox1.Checked = true;
                        mostrarPanelRegistro("si");
                        VerificarDatosRegistro();
                    }
                    else
                    {
                        mostrarPanelRegistro("no");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void VerificarSeleccionIdMarcaEdicion()
        {
            if (dtgMarcasIn.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasIn.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasIn.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarca.idN = id;
                    tabControl1.SelectedTab = tabPageMarcaDetail;
                }
            }
            else
            {
                SeleccionarMarca.idN = 0;
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task refrescarMarca()
        {
            if (SeleccionarMarca.idN > 0)
            {
                try
                {
                    DataTable detallesMarcaInt = await Task.Run(() => marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN));

                    if (detallesMarcaInt.Rows.Count > 0)
                    {
                        DataRow row = detallesMarcaInt.Rows[0];

                        if (row["estado"] != DBNull.Value && row["Observaciones"] != DBNull.Value)
                        {
                            textBoxEstatus.Text = row["estado"].ToString();
                            richTextBox1.Text = row["Observaciones"].ToString();

                        }
                        else
                        {
                            MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        bool contieneRegistrada = marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idN);

                        if (contieneRegistrada)
                        {
                            checkBox1.Checked = true;
                            mostrarPanelRegistro("si");
                            VerificarDatosRegistro();
                        }
                        else
                        {
                            checkBox1.Checked = false;
                            mostrarPanelRegistro("no");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles de la marca.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al refrescar los datos de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void loadHistorialById()
        {
            try
            {
                var historial = await Task.Run(() => historialModel.GetHistorialMarcaById(SeleccionarMarca.idN));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgHistorialIn.AutoGenerateColumns = true;
                    dtgHistorialIn.DataSource = historial;
                    dtgHistorialIn.Refresh();

                    if (dtgHistorialIn.Columns["id"] != null)
                    {
                        dtgHistorialIn.Columns["id"].Visible = false;
                    }

                    dtgHistorialIn.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmMarcasIntIngresadas_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadMarcas());
            SeleccionarMarca.idN = 0;
            archivoSubido = false;
            btnAdjuntarT.Visible = false;
            convertirImagen();
            pictureBox1.Image = documento;
            tabControl1.SelectedTab = tabPageIngresadasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageListaArchivos);
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                EliminarTabPage(tabPageIngresadasList);
                EliminarTabPage(tabPageListaArchivos);
                EliminarTabPage(tabPageHistorialDetail);
            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                await CargarDatosMarca();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageIngresadasList);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageListaArchivos);
            }
            else if (tabControl1.SelectedTab == tabPageIngresadasList)
            {
                EliminarTabPage(tabPageListaArchivos);
                EliminarTabPage(tabPageHistorialMarca);
                await LoadMarcas();
                SeleccionarMarca.idN = 0;
            }
            else if (tabControl1.SelectedTab == tabPageListaArchivos)
            {
                EliminarTabPage(tabPageHistorialMarca);
            }
            */
        }
        public async void Editar()
        {
            VerificarSeleccionIdMarcaEdicion();
            if (SeleccionarMarca.idN > 0)
            {
                LimpiarControles();
                EliminarTabPage(tabPageIngresadasList);
                await CargarDatosMarca();
                AnadirTabPage(tabPageMarcaDetail);
            }
        }
        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            using (FrmJustificacion justificacionForm = new FrmJustificacion())
            {

                if (justificacionForm.ShowDialog() == DialogResult.OK)
                {
                    string justificacion = justificacionForm.Justificacion;
                    DateTime fechaAbandono = justificacionForm.fecha;
                    string usuarioAbandono = justificacionForm.usuarioAbandono;
                    // Cambiar el estado a "Abandonada" y guardar la justificación
                    try
                    {
                        // Obtener el ID de la marca seleccionada
                        if (dtgMarcasIn.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasIn.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);

                                // Actualizar el estado y la justificación en la base de datos
                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", justificacion, usuarioAbandono, "TRÁMITE");
                                FrmAlerta alerta = new FrmAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadMarcas();
                            }
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA MARCA PARA ABANDONAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            alerta.ShowDialog();
                            //MessageBox.Show("No hay marca seleccionada para abandonar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Filter = "Images (*.jpg;*.jpeg;*.png;*.tiff)|*.jpg;*.jpeg;*.png;*.tiff";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            convertirImagen();
            pictureBox1.Image = documento;
        }

        private async void roundedButton1_Click(object sender, EventArgs e)
        {

            FrmAgregarEtapa frmAgregarEtapa = new FrmAgregarEtapa();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                try
                {
                    textBoxEstatus.Text = AgregarEtapa.etapa;
                    richTextBox1.Text += "\n" + AgregarEtapa.anotaciones;
                    historialModel.GuardarEtapa(SeleccionarMarca.idN, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE");
                    FrmAlerta alerta = new FrmAlerta("ESTADO AGREGADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();

                    await refrescarMarca();
                    if (AgregarEtapa.etapa == "Registrada")
                    {
                        checkBox1.Checked = true;
                        mostrarPanelRegistro("si");
                        txtRegistro.Text = "";
                        txtFolio.Text = "";
                        txtLibro.Text = "";
                        dateTimePFecha_Registro.Value = DateTime.Now;
                        ActualizarFechaVencimiento();
                        VerificarDatosRegistro();

                        if (comboBoxSignoDistintivo.Text == "Nombre comercial" && textBoxEstatus.Text == "Registrada")
                        {
                            dateTimePFecha_vencimiento.Enabled = true;
                        }
                        else
                        {
                            dateTimePFecha_vencimiento.Enabled = false;
                        }
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        mostrarPanelRegistro("no");
                        VerificarDatosRegistro();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }


        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {
            FrmMostrarTitulares frmMostrarTitulares = new FrmMostrarTitulares();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersona.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersona.nombre;
            }
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentes frmMostrarAgentes = new FrmMostrarAgentes();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersona.idPersonaA != 0)
            {
                txtNombreAgente.Text = SeleccionarPersona.nombre;

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
            long limiteTamanio = 20 * 1024 * 1024; // 20MB en bytes

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

                // Verificar tamaño del archivo antes de subirlo
                FileInfo fileInfo = new FileInfo(archivoLocal1);
                if (fileInfo.Length > limiteTamanio)
                {
                    MessageBox.Show($"El archivo supera el límite de {limiteTamanio / (1024 * 1024)} MB (100MB).",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Default;
                    return; // No sube el archivo si es demasiado grande
                }

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

        private void roundedButton6_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                loadHistorialById();
                AnadirTabPage(tabPageHistorialMarca);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {


        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        public void EditarVerHistorial()
        {
            if (dtgHistorialIn.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialIn.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    DataTable historial = historialModel.GetHistorialById(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];
                        if (fila["Origen"].ToString() == "TRÁMITE")
                        {
                            // Asignar los valores obtenidos a la clase SeleccionarPersona
                            SeleccionarHistorial.id = Convert.ToInt32(fila["id"]);
                            SeleccionarHistorial.etapa = fila["etapa"].ToString();
                            SeleccionarHistorial.fecha = (DateTime)fila["fecha"];
                            SeleccionarHistorial.anotaciones = fila["anotaciones"].ToString();
                            SeleccionarHistorial.usuario = fila["usuario"].ToString();
                            SeleccionarHistorial.usuarioEdicion = fila["usuarioEdicion"].ToString();

                            comboBoxEstatusH.SelectedItem = SeleccionarHistorial.etapa;
                            dateTimePickerFechaH.Value = SeleccionarHistorial.fecha;
                            richTextBoxAnotacionesH.Text = SeleccionarHistorial.anotaciones;
                            labelUserEditor.Text = UsuarioActivo.usuario;
                            lblUser.Text = SeleccionarHistorial.usuario;

                            AnadirTabPage(tabPageHistorialDetail);
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO SE PUEDE EDITAR UN HISTORIAL QUE NO SEA DE TRÁMITE", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            alerta.ShowDialog();
                        }

                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del historial", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void iconButton5_Click(object sender, EventArgs e)
        {
            EditarVerHistorial();
        }

        private async void iconButton4_Click(object sender, EventArgs e)
        {
            if (dtgHistorialIn.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialIn.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    string etapa = dataRowView["etapa"].ToString();
                    string anotaciones = dataRowView["anotaciones"].ToString();
                    string usuario = UsuarioActivo.usuario;
                    SeleccionarHistorial.id = id;
                    SeleccionarHistorial.etapa = etapa;
                    SeleccionarHistorial.anotaciones = anotaciones;


                    DialogResult confirmacionInicial = MessageBox.Show(" ¿Está seguro que desea eliminar esta etapa? " + usuario, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmacionInicial == DialogResult.Yes)
                    {

                        if (etapa.Equals("Registrada", StringComparison.OrdinalIgnoreCase))
                        {

                            DialogResult confirmacionRegistro = MessageBox.Show("Esta acción eliminará los datos de registro, folio, libro, fecha de registro y fecha de vencimiento. ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (confirmacionRegistro == DialogResult.Yes)
                            {
                                bool eliminarhistorial = historialModel.EliminarRegistroHistorial(id, usuario);

                                if (eliminarhistorial)
                                {

                                    MessageBox.Show("Estado eliminado y datos de registro borrados.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró el estado a eliminar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            bool eliminarhistorial = historialModel.EliminarRegistroHistorial(id, usuario);

                            if (eliminarhistorial)
                            {
                                MessageBox.Show("Estado eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el estado a eliminar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        loadHistorialById();
                        await refrescarMarca();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila para eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void comboBoxEstatusH_SelectedValueChanged(object sender, EventArgs e)
        {
            string etapa = comboBoxEstatusH.SelectedItem?.ToString();
            if (etapa == "Resolución RPI favorable" || etapa == "Resolución RPI desfavorable" ||
               etapa == "Recurso de revocatoria" || etapa == "Resolución Ministerio de Economía(MINECO)" ||
               etapa == "Contencioso administrativo")
            {
                richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " Por objeción-" + comboBoxEstatusH.SelectedItem;
            }
            else
            {
                richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
            }
        }

        private async void btnEditarH_Click(object sender, EventArgs e)
        {
            //Editar historial por id
            string etapa = comboBoxEstatusH.SelectedItem?.ToString();
            DateTime fecha = dateTimePickerFechaH.Value;
            string anotaciones = richTextBoxAnotacionesH.Text;
            SeleccionarHistorial.anotaciones = anotaciones;
            string usuario = lblUser.Text;
            string usuarioEditor = labelUserEditor.Text;
            bool actualizar;

            if (comboBoxEstatusH.SelectedIndex != -1)
            {
                string fechaSinHora = dateTimePickerFechaH.Value.ToShortDateString();
                string formato = fechaSinHora + " " + comboBoxEstatusH.SelectedItem.ToString();
                if (anotaciones.Contains(formato))
                {
                    AgregarEtapa.anotaciones = anotaciones;
                }
                else
                {
                    AgregarEtapa.anotaciones = formato + " " + anotaciones;
                }
                actualizar = historialModel.EditHistorialById(SeleccionarHistorial.id, etapa, fecha, AgregarEtapa.anotaciones, usuario, usuarioEditor);
                if (actualizar == true)
                {
                    FrmAlerta alerta = new FrmAlerta("ESTADO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show("Estado actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedTab = tabPageHistorialMarca;
                    SeleccionarHistorial.id = 0;
                    await refrescarMarca();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el estado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGUN ESTADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("No ha seleccionado ningun estado");
            }
        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            FrmMostrarClientes frmMostrarClientes = new FrmMostrarClientes();
            frmMostrarClientes.ShowDialog();

            if (SeleccionarPersona.idPersonaC != 0)
            {
                txtNombreCliente.Text = SeleccionarPersona.nombre;

            }
        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageHistorialMarca);
            AnadirTabPage(tabPageMarcaDetail);
        }

        private void btnActualizarM_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {

        }

        private void dtgMarcasIn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }

        public void VerificarDatosRegistro()
        {
            if (checkBox1.Checked == true && (string.IsNullOrEmpty(txtRegistro.Text) || string.IsNullOrEmpty(txtFolio.Text) || string.IsNullOrEmpty(txtLibro.Text)))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }



        }

        public void VerificarDatosIngresados()
        {
            if (checkBox1.Checked == true && (string.IsNullOrEmpty(SeleccionarMarca.registro) || string.IsNullOrEmpty(SeleccionarMarca.libro) || string.IsNullOrEmpty(SeleccionarMarca.folio)))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }

        private void CentrarPanel()
        {

            int anchoMinimo = panelBusqueda.Width + 100;

            if (tabControl1.ClientSize.Width >= anchoMinimo)
            {
                // Pantalla suficientemente ancha → centrar
                panelBusqueda.Anchor = AnchorStyles.None;

                int x = (tabControl1.ClientSize.Width - panelBusqueda.Width) / 2;
                int y = 58; // o donde quieras posicionarlo verticalmente
                panelBusqueda.Location = new Point(x, y);
            }
            else
            {
                // Pantalla pequeña → top-left
                panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panelBusqueda.Location = new Point(0, 58); // o donde quieras
            }
        }
        private void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = marcaModel.GetFilteredMarcasSinRegistroCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }

        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                if (!archivoSubido && checkBox1.Checked)
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE SUBIR EL TÍTULO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
                else
                {
                    ActualizarMarcaInternacional();

                }

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void iconButton2_Click_1(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                //Enviar a oposicion

                AgregarEtapa.solicitante = txtNombreTitular.Text;
                FrmEnviarAOposicionI frmEnviarAOposicion = new FrmEnviarAOposicionI();
                frmEnviarAOposicion.ShowDialog();


                if (AgregarEtapa.enviadoAOposicion == true)
                {
                    EliminarTabPage(tabPageMarcaDetail);
                    EliminarTabPage(tabPageHistorialMarca);
                    AnadirTabPage(tabPageIngresadasList);
                    tabControl1.SelectedTab = tabPageIngresadasList;
                    await LoadMarcas();
                    FrmAlerta alerta = new FrmAlerta("MARCA ENVIADA A OPOSICIÓN", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void iconButton5_Click_1(object sender, EventArgs e)
        {
            DatosRegistro.peligro = false;
            AnadirTabPage(tabPageIngresadasList);
            EliminarTabPage(tabPageMarcaDetail);
            await LoadMarcas();
            SeleccionarMarca.idN = 0;
            LimpiarFormulario();

        }

        private void dtgHistorialIn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarVerHistorial();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            filtrar();

        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = marcaModel.GetFilteredMarcasSinRegistroCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                filtrar();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtRegistro_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && string.IsNullOrEmpty(txtRegistro.Text))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && string.IsNullOrEmpty(txtFolio.Text))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }

        private void txtLibro_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && string.IsNullOrEmpty(txtLibro.Text))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }

        private void textBoxEstatus_TextChanged(object sender, EventArgs e)
        {
        }

        private void dateTimePFecha_Registro_ValueChanged_1(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                Rectangle tabRect = tabControl1.GetTabRect(i);
                if (tabRect.Contains(e.Location))
                {
                    // Ignorar clic en la pestaña
                    return;
                }
            }
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

                string id = "" + SeleccionarMarca.idN;
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

        private void roundedButton2_Click_1(object sender, EventArgs e)
        {
            ListarArchivosEnGeneral();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            EliminarTabPage(tabPageListaArchivos);
            AnadirTabPage(tabPageMarcaDetail);
            tabControl1.SelectedTab = tabPageMarcaDetail;

        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            SubirArchivo(SeleccionarMarca.idN.ToString());
            ListarArchivosEnGeneral();
        }



        private void AbrirArchivoDesdeFtp(string idMarca, string archivoNombre)
        {
            string carpeta = $"{directorioBase}/marca-{idMarca}/";
            string rutaRemota = $"{carpeta}/{archivoNombre}";
            string rutaLocal = Path.Combine(Path.GetTempPath(), archivoNombre); // Carpeta temporal

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
            string idMarca = "" + SeleccionarMarca.idN; // Id de la marca actual
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

        private void iconButton9_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void dtgArchivos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Abrir();
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
            string idMarca = "" + SeleccionarMarca.idN; // Id de la marca actual
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
        private void iconButton10_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void SubirArchivoRegistro(string idMarca)
        {
            string carpeta = $"{directorioBase}/marca-{idMarca}/";
            long limiteTamanio = 20 * 1024 * 1024; // 20MB en bytes

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

                // Verificar tamaño del archivo antes de subirlo
                FileInfo fileInfo = new FileInfo(archivoLocal1);
                if (fileInfo.Length > limiteTamanio)
                {
                    MessageBox.Show($"El archivo supera el límite de {limiteTamanio / (1024 * 1024)} MB (20MB).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Default;
                    return; // No sube el archivo si es demasiado grande
                }

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

                        archivoSubido = true; // Indicar que el archivo se ha subido correctamente
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al subir el archivo: {ex.InnerException.Message}");
                    archivoSubido = false;
                }
                Cursor.Current = Cursors.Default;
            }
            else
            {
                archivoSubido = false;
            }
        }

        private void btnAdjuntarT_Click(object sender, EventArgs e)
        {
            SubirArchivoRegistro("" + SeleccionarMarca.idN);
            if (!archivoSubido)
            {
                FrmAlerta alerta = new FrmAlerta("NO SE HA SELECCIONADO NI SUBIDO NINGÚN ARCHIVO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                archivoSubido = false;
            }
            else
            {
                archivoSubido = true;
            }
        }

        private void comboBoxSignoDistintivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSignoDistintivo.Text == "Nombre comercial" && textBoxEstatus.Text == "Registrada")
            {
                dateTimePFecha_vencimiento.Enabled = true;
            }
            else
            {
                dateTimePFecha_vencimiento.Enabled = false;
            }
        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgMarcasIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgMarcasIn.Columns["id"] != null)
                dtgMarcasIn.Columns["id"].Visible = false;

            dtgMarcasIn.ClearSelection();
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmMarcasIntIngresadas_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }
    }
}
