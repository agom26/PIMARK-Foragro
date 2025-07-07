using Comun.Cache;
using DocumentFormat.OpenXml.Wordprocessing;
using Dominio;
using FluentFTP;
using Presentacion.Alertas;
using Presentacion.Marcas_Internacionales;
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
using ZstdSharp.Unsafe;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmTraspasos : Form
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
        private bool buscando = false;
        private bool archivoSubido = false;

        //ftp
        private string host = "ftp.bpa.com.es"; // Tu host FTP
        private string usuario = "test@bpa.com.es"; // Tu usuario FTP
        private string contraseña = "2O1VsAbUGbUo"; // Tu contraseña FTP
        private string directorioBase = "/bpa.com.es/test/marcas/internacionales";
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }
        public FrmTraspasos()
        {
            InitializeComponent();
            this.Load += FrmTraspasos_Load;
            archivoSubido = false;
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
            totalRows = await marcaModel.GetTotalMarcasInternacionalesEnTramiteDeTraspaso();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            var marcasN = await  marcaModel.GetAllMarcasInternacionalesEnTramiteDeTraspaso(currentPageIndex, pageSize);
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();
                    dtgMarcasRenov.DataSource = marcasN;

                }));
            }
        }
        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = await marcaModel.GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = await marcaModel.FiltrarMarcasInternacionalesEnTramiteDeTraspaso(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgMarcasRenov.DataSource = titulares;
                    if (dtgMarcasRenov.Columns["id"] != null)
                    {
                        dtgMarcasRenov.Columns["id"].Visible = false;
                    }
                    dtgMarcasRenov.ClearSelection();
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
        private bool ValidarCampo(string campo, string mensaje)
        {
            if (string.IsNullOrEmpty(campo))
            {
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidarCampos(string expediente, string nombre, string clase, string signoDistintivo, string tipo, string estado,
    ref byte[] logo, bool registroChek, string registro, string folio, string libro)
        {
            if (!ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
                !ValidarCampo(nombre, "Por favor, ingrese el signo.") ||
                !ValidarCampo(clase, "Por favor, ingrese la clase.") ||
                !ValidarCampo(signoDistintivo, "Por favor, seleccione un signo distintivo.") ||
                !ValidarCampo(tipo, "Por favor, seleccione un tipo.") ||
                !ValidarCampo(estado, "Por favor, seleccione un estado."))
            {
                return false;
            }

            if (
                !int.TryParse(clase, out _) ||
                (registroChek && !int.TryParse(folio, out _)) ||
                (registroChek && !int.TryParse(libro, out _)))
            {
                FrmAlerta alerta = new FrmAlerta("LA CLASE, FOLIO Y TOMO\nDEBEN SER VALORES NUMÉRICOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return false;
            }

            if (comboBoxSignoDistintivo.SelectedItem?.ToString() == "Marca" &&
              comboBoxTipoSigno.SelectedItem?.ToString() == "Gráfica/Figurativa" || comboBoxTipoSigno.SelectedItem?.ToString() == "Mixta")
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
                    MessageBox.Show("Por favor, ingrese una imagen.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                logo = null;
            }



            if (registroChek)
            {
                if (!ValidarCampo(folio, "Por favor, ingrese el número de folio.") ||
                    !ValidarCampo(registro, "Por favor, ingrese el número de registro.") ||
                    !ValidarCampo(libro, "Por favor, ingrese el número de libro.")
                    )
                {
                    return false;
                }
            }

            return true;
        }

        public async Task ActualizarMarcaNacional()
        {
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string? signoDistintivo = comboBoxSignoDistintivo.SelectedItem?.ToString();
            string? tipoSigno = comboBoxTipoSigno.SelectedItem?.ToString();
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[]? logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            int? idCliente = SeleccionarPersona.idPersonaC;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;

            //Tramites de renovacion y traspaso
            string erenov = txtERenovacion.Text;
            string etrasp = txtETraspaso.Text;

            string? paisRegistro = comboBox1.SelectedItem?.ToString();
            string tiene_poder = "no";

            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;


            if (checkBoxTienePoder.Checked)
            {
                tiene_poder = "si";
            }
            else
            {
                tiene_poder = "no";
            }

            if (idTitular <= 0)
            {
                MessageBox.Show("Por favor, seleccione un titular válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idAgente <= 0)
            {
                MessageBox.Show("Por favor, seleccione un agente válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!ValidarCampos(expediente, nombre, clase, signoDistintivo, tipoSigno, estado, ref logo, registroChek, registro, folio, libro))
            {
                return;
            }

            if (estado == "Trámite de renovación" && string.IsNullOrEmpty(erenov))
            {
                MessageBox.Show("Por favor, ingrese el número de trámite de renovación", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (estado == "Tramite de traspaso" && string.IsNullOrEmpty(etrasp))
            {
                MessageBox.Show("Por favor, ingrese el número de trámite de traspaso.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                bool esActualizado;

                if (registroChek)
                {
                    esActualizado = await marcaModel.EditMarcaInternacionalRegistrada(
                        SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente, registro, folio, libro, fecha_registro, fecha_vencimiento, erenov, etrasp);
                }
                else
                {
                    esActualizado = await marcaModel.EditMarcaInternacional(SeleccionarMarca.idInt, expediente, nombre, signoDistintivo
                        , tipoSigno, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente);
                }

                DataTable marcaActualizada = await marcaModel.GetMarcaInternacionalById(SeleccionarMarca.idInt);

                if (esActualizado)
                {
                    if (esActualizado)
                    {
                        if (marcaActualizada.Rows.Count > 0 && marcaActualizada.Rows[0]["Observaciones"].ToString().Contains(estado))
                        {
                            FrmAlerta alerta = new FrmAlerta("MARCA INTERNACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            SeleccionarMarca.idInt = 0;
                            AnadirTabPage(tabPageRegistradasList);
                            EliminarTabPage(tabPageListaArchivos);
                            EliminarTabPage(tabPageMarcaDetail);
                            EliminarTabPage(tabPageHistorialMarca);
                            await LoadMarcas();
                            LimpiarFormulario();
                        }
                        else
                        {
                            historialModel.GuardarEtapa(SeleccionarMarca.idInt, AgregarEtapa.fecha.Value, estado, AgregarEtapa.anotaciones, AgregarEtapa.usuario, "TRÁMITE");
                            FrmAlerta alerta = new FrmAlerta("MARCA INTERNACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            SeleccionarMarca.idInt = 0;
                            AnadirTabPage(tabPageRegistradasList);
                            EliminarTabPage(tabPageListaArchivos);
                            EliminarTabPage(tabPageMarcaDetail);
                            EliminarTabPage(tabPageHistorialMarca);
                            await LoadMarcas();
                            LimpiarFormulario();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error al actualizar la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al " + (registroChek ? "registrar" : "actualizar") + " la marca internacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
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
            txtETraspaso.Text = "";
            txtERenovacion.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            AgregarEtapa.LimpiarEtapa();
            txtERenovacion.Text = "";
            txtETraspaso.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            comboBoxSignoDistintivo.SelectedIndex = -1;
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
                        AgregarTraspaso.antiguoNombre = row["nombre"].ToString();
                        SeleccionarMarca.clase = row["clase"].ToString();
                        SeleccionarMarca.estado = row["estado"].ToString();
                        SeleccionarMarca.signoDistintivo = row["signoDistintivo"].ToString();
                        SeleccionarMarca.tipoSigno = row["Tipo"].ToString();
                        //SeleccionarMarca.logo = row["logo"] is DBNull ? null : (byte[])row["logo"];
                        SeleccionarMarca.idPersonaTitular = Convert.ToInt32(row["idTitular"]);
                        SeleccionarMarca.idPersonaAgente = Convert.ToInt32(row["idAgente"]);
                        SeleccionarMarca.idPersonaCliente = row["idCliente"] != DBNull.Value ? Convert.ToInt32(row["idCliente"]) : 0;
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
                        SeleccionarMarca.tiene_poder = row["tiene_poder"] != DBNull.Value ? row["tiene_poder"].ToString() : string.Empty;
                        SeleccionarMarca.pais_de_registro = row["pais_de_registro"] != DBNull.Value ? row["pais_de_registro"].ToString() : string.Empty;

                        checkBoxTienePoder.Checked = SeleccionarMarca.tiene_poder.Equals("si", StringComparison.OrdinalIgnoreCase);
                        int index = comboBox1.FindString(SeleccionarMarca.pais_de_registro);
                        comboBox1.SelectedIndex = index;

                        SeleccionarMarca.logo = await marcaModel.ObtenerLogoMarcaPorId(SeleccionarMarca.idInt);

                        if (SeleccionarMarca.logo != null && SeleccionarMarca.logo.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(SeleccionarMarca.logo))
                            {
                                pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            convertirImagen();
                            pictureBox1.Image = documento;
                        }

                        if (row["Erenov"] != DBNull.Value)
                        {
                            SeleccionarMarca.erenov = row["Erenov"].ToString();
                            txtERenovacion.Text = SeleccionarMarca.erenov;
                        }

                        if (row["Etrasp"] != DBNull.Value)
                        {
                            SeleccionarMarca.etraspaso = row["Etrasp"].ToString();
                            txtETraspaso.Text = SeleccionarMarca.etraspaso;
                        }

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
                        AgregarTraspaso.idTitularAnterior = SeleccionarMarca.idPersonaTitular;
                        SeleccionarPersona.idPersonaC = SeleccionarMarca.idPersonaCliente;
                        if (titular.Count > 0)
                        {
                            txtNombreTitular.Text = titular[0].nombre;
                            AgregarTraspaso.nombreTitulara = titular[0].nombre;
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
                            SeleccionarPersona.idPersonaC = null;
                            txtNombreCliente.Text = "";
                        }


                        // Actualizar los controles 
                        txtExpediente.Text = SeleccionarMarca.expediente;
                        txtNombre.Text = SeleccionarMarca.nombre;
                        AgregarTraspaso.antiguoNombre = SeleccionarMarca.nombre;
                        txtClase.Text = SeleccionarMarca.clase;
                        textBoxEstatus.Text = SeleccionarMarca.estado;
                        comboBoxSignoDistintivo.SelectedItem = SeleccionarMarca.signoDistintivo;
                        comboBoxTipoSigno.SelectedItem = SeleccionarMarca.tipoSigno;
                        MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                        datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                        richTextBox1.Text = SeleccionarMarca.observaciones;
                        /*
                        if (row["logo"] == DBNull.Value)
                        {
                            convertirImagen();
                            pictureBox1.Image = documento;
                        }*/

                        bool contieneRegistrada = await marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idInt);

                        if (contieneRegistrada)
                        {
                            checkBox1.Checked = true;
                            mostrarPanelRegistro("si");
                            SeleccionarMarca.registro = row["registro"].ToString();
                            SeleccionarMarca.folio = row["folio"].ToString();
                            SeleccionarMarca.libro = row["libro"].ToString();
                            SeleccionarMarca.fechaRegistro = Convert.ToDateTime(row["fechaRegistro"]);
                            SeleccionarMarca.fechaVencimiento = Convert.ToDateTime(row["fechaVencimiento"]);
                            SeleccionarMarca.erenov = row["Erenov"].ToString();

                            txtRegistro.Text = SeleccionarMarca.registro;
                            txtFolio.Text = SeleccionarMarca.folio;
                            txtLibro.Text = SeleccionarMarca.libro;
                            dateTimePFecha_Registro.Value = SeleccionarMarca.fechaRegistro.Value;
                            dateTimePFecha_vencimiento.Value = SeleccionarMarca.fechaVencimiento.Value;
                            txtERenovacion.Text = SeleccionarMarca.erenov;
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
            if (dtgMarcasRenov.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasRenov.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasRenov.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarMarca.idInt = id;
                    tabControl1.SelectedTab = tabPageMarcaDetail;
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private async Task loadHistorialById()
        {
            try
            {
                var historial = await Task.Run(() => historialModel.GetHistorialMarcaById(SeleccionarMarca.idInt));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgHistorialR.AutoGenerateColumns = true;
                    dtgHistorialR.DataSource = historial;
                    dtgHistorialR.Refresh();

                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmTraspasos_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadMarcas());
            tabControl1.SelectedTab = tabPageRegistradasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageListaArchivos);
            ActualizarFechaVencimiento();
            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();
            archivoSubido = false;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                EliminarTabPage(tabPageHistorialDetail);
            }
            else if (tabControl1.SelectedTab == tabPageRegistradasList)
            {
                await LoadMarcas();
                SeleccionarMarca.idInt = 0;
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageListaArchivos);
            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                CargarDatosMarca();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageListaArchivos);
            }*/
        }
        public async void Editar()
        {
            LimpiarFormulario();
            VerificarSeleccionIdMarcaEdicion();
            if (SeleccionarMarca.idInt > 0)
            {
                await CargarDatosMarca();
                AnadirTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageRegistradasList);
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

                    try
                    {

                        if (dtgMarcasRenov.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasRenov.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);

                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", fechaAbandono.ToString("dd/MM/yyyy") + " Abandono " + justificacion, usuarioAbandono, "TRÁMITE");

                                MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadMarcas();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No hay marca seleccionada para abandonar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmAgregarRenovacionConcedida frmAgregarEtapa = new FrmAgregarRenovacionConcedida();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                try
                {
                    historialModel.GuardarEtapa(SeleccionarMarca.idInt, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE");
                    MessageBox.Show("Etapa agregada con éxito");
                    if (AgregarEtapa.etapa == "Registrada")
                    {
                        checkBox1.Checked = true;
                        mostrarPanelRegistro("si");
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        mostrarPanelRegistro("no");
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
                AgregarTraspaso.idTitularAnterior = SeleccionarPersona.idPersonaT;
                txtNombreTitular.Text = SeleccionarPersona.nombre;
                AgregarTraspaso.nombreTitulara = SeleccionarPersona.nombre;
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

        private async void roundedButton6_Click(object sender, EventArgs e)
        {
            await loadHistorialById();
            AnadirTabPage(tabPageHistorialMarca);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        public async void EditarHistorial()
        {
            if (dtgHistorialR.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialR.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    DataTable historial = await historialModel.GetHistorialById(id);

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
                MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void iconButton5_Click(object sender, EventArgs e)
        {
            EditarHistorial();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (dtgHistorialR.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialR.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    string etapa = dataRowView["etapa"].ToString();
                    string anotaciones = dataRowView["anotaciones"].ToString();
                    string usuario = UsuarioActivo.usuario;
                    SeleccionarHistorial.id = id;
                    SeleccionarHistorial.etapa = etapa;
                    SeleccionarHistorial.anotaciones = anotaciones;


                    DialogResult confirmacionInicial = MessageBox.Show("¿Está seguro que desea eliminar esta etapa? " + usuario, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToString("dd/MM/yyyy") + " " + comboBoxEstatusH.SelectedItem;
        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string etapa = comboBoxEstatusH.SelectedItem?.ToString();
            if (etapa == "Resolución RPI favorable" || etapa == "Resolución RPI desfavorable" ||
               etapa == "Recurso de revocatoria" || etapa == "Resolución Ministerio de Economía(MINECO)" ||
               etapa == "Contencioso administrativo")
            {
                richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToString("dd/MM/yyyy") + " Por objeción-" + comboBoxEstatusH.SelectedItem;
            }
            else
            {
                richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToString("dd/MM/yyyy") + " " + comboBoxEstatusH.SelectedItem;
            }
        }

        private async void btnEditarH_Click(object sender, EventArgs e)
        {
            //Editar historial por id
            string? etapa = comboBoxEstatusH.SelectedItem?.ToString();
            DateTime fecha = dateTimePickerFechaH.Value;
            string anotaciones = richTextBoxAnotacionesH.Text;
            SeleccionarHistorial.anotaciones = anotaciones;
            string usuario = lblUser.Text;
            string usuarioEditor = labelUserEditor.Text;
            bool actualizar = await historialModel.EditHistorialById(SeleccionarHistorial.id, etapa, fecha, anotaciones, usuario, usuarioEditor);

            if (actualizar == true)
            {
                MessageBox.Show("Estado actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AnadirTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
            }
            else
            {
                MessageBox.Show("Error al actualizar el estado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void roundedButton8_Click(object sender, EventArgs e)
        {


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
        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
        }

        private async void btnActualizarM_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                bool existeRegistro = await marcaModel.ExisteRegistro(txtRegistro.Text.Trim(), SeleccionarMarca.idN);
                if (existeRegistro)
                {
                    FrmAlerta alerta = new FrmAlerta("EL NÚMERO DE REGISTRO YA EXISTE", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return;
                }
                else
                {
                    await ActualizarMarcaNacional();
                }
                    
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void iconButton1_Click_1(object sender, EventArgs e)
        {
            VerificarDatosRegistro();

            if (!archivoSubido)
            {
                FrmAlerta alerta = new FrmAlerta("DEBE SUBIR EL TÍTULO DE TRASPASO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                return;
            }

            AgregarTraspaso.antiguoNombre = SeleccionarMarca.nombre;
            FrmCrearTraspaso frmCrearTraspaso = new FrmCrearTraspaso();
            frmCrearTraspaso.ShowDialog();

            if (AgregarTraspaso.traspasoFinalizado == true)
            {
                //Limpiar campos
                LimpiarFormulario();
                //Volver a poner traspasos = false
                AgregarTraspaso.traspasoFinalizado = false;
                AnadirTabPage(tabPageRegistradasList);
                EliminarTabPage(tabPageListaArchivos);
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                await LoadMarcas();
                FrmAlerta alerta = new FrmAlerta("TRASPASO GUARDADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
                //MessageBox.Show("Traspaso guardado correctamente");
            }
        }

        private async void btnCancelarM_Click(object sender, EventArgs e)
        {/*
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                DatosRegistro.peligro = false;
                AnadirTabPage(tabPageRegistradasList);
                EliminarTabPage(tabPageListaArchivos);
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                tabControl1.SelectedTab = tabPageRegistradasList;
                await LoadMarcas();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
               */
            DatosRegistro.peligro = false;
            await LoadMarcas();
            AnadirTabPage(tabPageRegistradasList);
            EliminarTabPage(tabPageListaArchivos);
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            LimpiarFormulario();

        }

        private void tabPageRegistradasList_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void dtgMarcasRenov_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }

        private void dtgHistorialR_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarHistorial();
        }

        private async void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = await marcaModel.GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrar();
        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {

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

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = await marcaModel.GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                filtrar();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtETraspaso_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtETraspaso.Text))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
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
                    MessageBox.Show($"El archivo supera el límite de {limiteTamanio / (1024 * 1024)} MB (20MB).",
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
        private void roundedButton11_Click(object sender, EventArgs e)
        {
            ListarArchivosEnGeneral();
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageListaArchivos);

        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            SubirArchivo("" + SeleccionarMarca.idInt);
            ListarArchivosEnGeneral();
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
            Eliminar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            FrmMostrarClientes frmMostrarClientes = new FrmMostrarClientes();
            frmMostrarClientes.ShowDialog();

            if (SeleccionarPersona.idPersonaC != 0)
            {
                txtNombreCliente.Text = SeleccionarPersona.nombre;
            }
            else
            {
                SeleccionarPersona.idPersonaC = null;
                txtNombreCliente.Text = "";
            }
        }

        private void iconButton1_Click_2(object sender, EventArgs e)
        {

        }

        private void dtgMarcasRenov_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgMarcasRenov.Columns["id"] != null)
            {
                dtgMarcasRenov.Columns["id"].Visible = false;
            }
            dtgMarcasRenov.ClearSelection();
        }

        private void dtgHistorialR_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgHistorialR.Columns["id"] != null)
            {
                dtgHistorialR.Columns["id"].Visible = false;
            }

            dtgHistorialR.ClearSelection();
        }
        private void SubirArchivoTraspaso(string idMarca)
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
            SubirArchivoTraspaso("" + SeleccionarMarca.idInt);
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
        private void CentrarPanel()
        {

            int anchoMinimo = panelBusqueda.Width + 100;

            if (tabControl1.ClientSize.Width >= anchoMinimo)
            {
                // Pantalla suficientemente ancha → centrar
                panelBusqueda.Anchor = AnchorStyles.None;

                int x = (tabControl1.ClientSize.Width - panelBusqueda.Width) / 2;
                int y = panelBusqueda.Height; // o donde quieras posicionarlo verticalmente
                panelBusqueda.Location = new Point(x, y);
            }
            else
            {
                // Pantalla pequeña → top-left
                panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                panelBusqueda.Location = new Point(0, panelBusqueda.Height); // o donde quieras
            }
        }

        private void FrmTraspasos_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }
    }
}
