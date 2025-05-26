using Comun.Cache;
using Dominio;
using Mysqlx.Datatypes;
using Presentacion.Alertas;
using Presentacion.Marcas_Nacionales;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Presentacion.Reportes;
using FluentFTP;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Media.Converters;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMarcasLicenciaUso : Form
    {
        LicenciaUsoModel licenciaUso = new LicenciaUsoModel();
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        public int numRegistros = 0;
        public float escala = 0;
        string titulo;
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        bool exclusiva = false;
        bool necesitaArchivo = false;


        private const int pageSize2 = 20;
        private int currentPageIndex2 = 1;
        private int totalPages2 = 0;
        private int totalRows2 = 0;
        private bool buscando1 = false;
        private bool buscando2 = false;
        private bool archivoSubido = false;
        //ftp
        private string host = "ftp.bpa.com.es"; // Tu host FTP
        private string usuario = "test@bpa.com.es"; // Tu usuario FTP
        private string contraseña = "2O1VsAbUGbUo"; // Tu contraseña FTP
        private string directorioBase = "/bpa.com.es/test/licencias/nacionales"; // La ruta base de tu servidor
        public FrmMarcasLicenciaUso()
        {
            InitializeComponent();
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            this.Load += FrmMarcasLicenciaUso_Load;
            verificarBotones();
            comboBoxEstado.SelectedItem = "En Trámite";
            archivoSubido = false;
            necesitaArchivo = false;
            if (UsuarioActivo.isAdmin)
            {
                btnEliminarLicencia.Visible = true;
            }
            else
            {
                btnEliminarLicencia.Visible = false;
            }
            EliminarTabPage(tabPageReportes);
            EliminarTabPage(tabPageArchivos);
            EliminarTabPage(tabPageAgregarOposicion);
            //ActualizarFechaVencimiento();
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }


        private async Task LoadLicenciasUsoExclusivas(string situacionActual)
        {
            var resultado = await Task.Run(() => licenciaUso.ObtenerLicenciasUsoNacionalesExclusivasCombinado(situacionActual, currentPageIndex, pageSize))
                                       .ConfigureAwait(false);


            totalRows = resultado.total;
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            var marcasN = resultado.datos;


            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    lblTotalPages.Text = totalPages.ToString();
                    lblTotalRows.Text = totalRows.ToString();
                    dtgLicenciasExclusivas.DataSource = marcasN;

                    if (dtgLicenciasExclusivas.Columns["id"] != null)
                    {
                        dtgLicenciasExclusivas.Columns["IdMarca"].Visible = false;
                        dtgLicenciasExclusivas.Columns["id"].Visible = false;
                        dtgLicenciasExclusivas.ClearSelection();
                    }
                }));
            }
        }


        private async Task LoadLicenciasUsoNoExclusivas(string situacionActual)
        {
            var resultado = await Task.Run(() => licenciaUso.ObtenerLicenciasUsoNacionalesNoExclusivasCombinado(situacionActual, currentPageIndex2, pageSize2))
                                       .ConfigureAwait(false);

            totalRows2 = resultado.total;
            totalPages2 = (int)Math.Ceiling((double)totalRows2 / pageSize2);
            var marcasN = resultado.datos;

            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    lblTotalPages2.Text = totalPages2.ToString();
                    lblTotalRows2.Text = totalRows2.ToString();
                    dtgLicenciasNoEx.DataSource = marcasN;

                    if (dtgLicenciasNoEx.Columns["id"] != null)
                    {
                        dtgLicenciasNoEx.Columns["IdMarca"].Visible = false;
                        dtgLicenciasNoEx.Columns["id"].Visible = false;
                        dtgLicenciasNoEx.ClearSelection();
                    }
                }));
            }
        }


        public async void filtrarLicenciasUsoExclusivas()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = licenciaUso.GetFilteredLicenciasUsoNacionalesExclusivasCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = licenciaUso.FiltrarLicenciasUsoNacionalesExclusivas(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgLicenciasExclusivas.DataSource = titulares;
                    if (dtgLicenciasExclusivas.Columns["id"] != null)
                    {
                        dtgLicenciasExclusivas.Columns["id"].Visible = false;
                        dtgLicenciasExclusivas.Columns["IdMarca"].Visible = false;
                    }
                    dtgLicenciasExclusivas.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN LICENCIAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    await FiltrarExclusivasPorSituacionActual();
                }
            }
            else
            {
                await FiltrarExclusivasPorSituacionActual();
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


        private void FrmMarcasLicenciaUso_Load(object sender, EventArgs e)
        {

            archivoSubido = false;
            necesitaArchivo = false;
            comboBoxEstado.SelectedItem = "En Trámite";
            cmbSituacionActual.SelectedIndex = 0;
            cmbSituacionActual2.SelectedIndex = 0;

            verificarBotones();
            //_ = CargarDatosLicenciasUsoAsync();
            EliminarTabPage(tabPageReportes);
            EliminarTabPage(tabPageArchivos);
            EliminarTabPage(tabPageAgregarOposicion);
        }

        private async Task CargarDatosLicenciasUsoAsync()
        {
            try
            {
                currentPageIndex = 1;
                currentPageIndex2 = 1;
                string? estado = cmbSituacionActual.SelectedItem.ToString();
                string? estado2 = cmbSituacionActual2.SelectedItem.ToString();
                int currP = await Task.Run(() => licenciaUso.GetTotalLicenciasUsoNacionalesExclusivas(estado));
                int currP2 = await Task.Run(() => licenciaUso.GetTotalLicenciasUsoNacionalesNoExclusivas(estado2));

                
                lblCurrentPage.Text = currentPageIndex.ToString();
                lblCurrentPage2.Text = currentPageIndex2.ToString();

                cmbSituacionActual.SelectedIndex = 0;
                cmbSituacionActual2.SelectedIndex = 0;

                if (tabPageAgregarOposicion != null)
                {
                    EliminarTabPage(tabPageAgregarOposicion);
                    EliminarTabPage(tabPageReportes);
                    EliminarTabPage(tabPageArchivos);
                }

                var filtrarExclusivasTask = FiltrarExclusivasPorSituacionActual();
                var filtrarNoExclusivasTask = FiltrarNoExclusivasPorSituacionActual();

                await Task.WhenAll(filtrarExclusivasTask, filtrarNoExclusivasTask);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar licencias de uso: " + ex.Message);
            }

        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async Task CargarDatosLicenciaUso()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                tabControl1.Visible = false;
                DataTable detallesLicencia = await Task.Run(() => licenciaUso.ObtenerLicenciaUsoPorId(SeleccionarLicencia.idLicencia));
                if (detallesLicencia.Rows.Count > 0)
                {
                    DataRow row = detallesLicencia.Rows[0];
                    SeleccionarLicencia.tituloPorElQueVerifica = row["titulo"].ToString();
                    SeleccionarLicencia.tipo = row["tipo"].ToString();
                    SeleccionarLicencia.territorio = row["territorio"].ToString();
                    SeleccionarLicencia.nombreRazonSocial = row["nombre_razon_social"].ToString();
                    SeleccionarLicencia.direccion = row["direccion"].ToString();
                    SeleccionarLicencia.domicilio = row["domicilio"].ToString();
                    SeleccionarLicencia.nacionalidad = row["nacionalidad"] is DBNull ? null : row["nacionalidad"].ToString();
                    SeleccionarLicencia.apoderadoRepresentanteL = row["apoderado_representante_legal"] is DBNull ? null : row["apoderado_representante_legal"].ToString();
                    SeleccionarLicencia.estado = row["estado"] is DBNull ? null : row["estado"].ToString();
                    SeleccionarLicencia.origen = row["origen"].ToString();
                    SeleccionarLicencia.fechaInicio = Convert.ToDateTime(row["fecha_inicio"]);
                    SeleccionarLicencia.fechaFin = Convert.ToDateTime(row["fecha_fin"]);
                    SeleccionarLicencia.idMarca = int.Parse(row["IdMarca"].ToString());
                    SeleccionarLicencia.idTitular = row["IdTitular"] is DBNull ? 0 : int.Parse(row["IdTitular"].ToString());


                    txtTitulo.Text = SeleccionarLicencia.tituloPorElQueVerifica;
                    txtTerritorio.Text = SeleccionarLicencia.territorio;
                    txtRazonSocial.Text = SeleccionarLicencia.nombreRazonSocial;
                    txtDireccion.Text = SeleccionarLicencia.direccion;
                    txtDomicilio.Text = SeleccionarLicencia.domicilio;
                    txtNacionalidad.Text = SeleccionarLicencia.nacionalidad;
                    txtApoderadoRL.Text = SeleccionarLicencia.apoderadoRepresentanteL;
                    dateTimePickerInicio.Value = SeleccionarLicencia.fechaInicio;
                    dateTimePickerFin.Value = SeleccionarLicencia.fechaFin;
                    numericAnio.Value = Convert.ToInt32(row["Anios"].ToString());
                    numericMes.Value = Convert.ToInt32(row["Meses"].ToString());
                    numericDia.Value = Convert.ToInt32(row["Dias"].ToString());
                    if (SeleccionarLicencia.estado == "Terminada")
                    {
                        if (!comboBoxEstado.Items.Contains("Terminada"))
                        {
                            comboBoxEstado.Items.Add("Terminada");
                        }
                        comboBoxEstado.SelectedItem = "Terminada";
                        comboBoxEstado.Enabled = false;
                    }
                    else
                    {
                        if (comboBoxEstado.Items.Contains("Terminada"))
                        {
                            comboBoxEstado.Items.Remove("Terminada");
                        }
                        comboBoxEstado.SelectedItem = row["estado"].ToString();
                        comboBoxEstado.Enabled = true;
                    }

                    if (SeleccionarLicencia.tipo == "exclusiva")
                    {
                        radioButtonExclusiva.Checked = true;
                    }
                    else
                    {
                        radioButtonNoExclusiva.Checked = true;
                    }


                    if (row["estado"].ToString().Trim().Equals("Terminada", StringComparison.OrdinalIgnoreCase))

                    {
                        btnEnviarATerminar.Visible = false;
                        btnDatosLicenciante.Enabled = false;
                    }
                    else
                    {
                        btnEnviarATerminar.Visible = true;
                        btnDatosLicenciante.Enabled = true;
                    }


                    if (SeleccionarLicencia.idMarca > 0)
                    {
                        btnDatosLicenciante.Enabled = true;


                        var marca = marcaModel.GetMarcaNacionalById(SeleccionarLicencia.idMarca);
                        if (marca.Rows.Count > 0)
                        {
                            DataRow dataRow = marca.Rows[0];

                            txtSigno.Text = dataRow["nombre"].ToString();
                            txtExpediente.Text = dataRow["expediente"].ToString();
                            txtClase.Text = dataRow["clase"].ToString();
                            txtRegistro.Text = dataRow["registro"].ToString();
                            txtFolio.Text = dataRow["folio"].ToString();
                            txtTomo.Text = dataRow["libro"].ToString();
                            comboBoxSignoDist.SelectedItem = dataRow["signoDistintivo"].ToString();
                            dateTimePFecha_vencimiento.Value = Convert.ToDateTime(dataRow["fechaVencimiento"]);

                            if (SeleccionarLicencia.idTitular > 0)
                            {
                                var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarLicencia.idTitular));

                                await Task.WhenAll(titularTask);

                                var titular = titularTask.Result;

                                if (titular.Count > 0)
                                {
                                    txtTitular.Text = titular[0].nombre;
                                }
                            }
                        }
                    }

                    btnGuardarU.Text = "EDITAR";
                    btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.Pen;
                    btnGuardarU.BackColor = Color.FromArgb(96, 149, 241);

                    tabControl1.Visible = true;
                    Cursor = Cursors.Default;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcesarSeleccion(DataGridView dataGridView)
        {
            var filaSeleccionada = dataGridView.SelectedRows[0];

            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
            {
                int? id = dataRowView["id"] as int?;

                if (id.HasValue)
                {
                    SeleccionarLicencia.idLicencia = id.Value;
                    AnadirTabPage(tabPageAgregarOposicion);
                }
                else
                {
                    MostrarAlerta("SELECCIONE UNA LICENCIA", "ERROR");
                }
            }
        }

        private void MostrarAlerta(string mensaje, string titulo)
        {
            FrmAlerta alerta = new FrmAlerta(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            alerta.ShowDialog();
        }

        private void VerificarSeleccionEdicion()
        {
            // Comprobar si hay filas seleccionadas
            if (dtgLicenciasExclusivas.RowCount <= 0 && dtgLicenciasNoEx.RowCount <= 0)
            {
                MostrarAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE");
                return;
            }

            // Verificar si se seleccionó en alguna de las tablas
            if (dtgLicenciasExclusivas.SelectedRows.Count > 0)
            {
                ProcesarSeleccion(dtgLicenciasExclusivas);
            }
            else if (dtgLicenciasNoEx.SelectedRows.Count > 0)
            {
                ProcesarSeleccion(dtgLicenciasNoEx);
            }
            else
            {
                MostrarAlerta("SELECCIONE UNA LICENCIA", "MENSAJE");
            }
        }

        public async void Editar()
        {
            btnEnviarATerminar.Visible = true;
            VerificarSeleccionEdicion();
            if (SeleccionarLicencia.idLicencia > 0)
            {

                await CargarDatosLicenciaUso();
                EliminarTabPage(tabPageOposicionesList);
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
                        if (dtgLicenciasExclusivas.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgLicenciasExclusivas.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);

                                // Actualizar el estado y la justificación en la base de datos
                                //historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", justificacion, usuarioAbandono, "TRÁMITE");
                                FrmAlerta alerta = new FrmAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA MARCA PARA ABANDONAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        async void EditarLicenciaUso()
        {
            string mensajeValidacion = ValidarCamposLicenciaUso("editando");
            if (mensajeValidacion != null)
            {
                FrmAlerta alerta = new FrmAlerta(mensajeValidacion.ToUpper(), "VALIDACIÓN FALLIDA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }

            if (comboBoxEstado.SelectedItem == null)
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN ESTADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }
            string estado = comboBoxEstado.SelectedItem.ToString();
            int idLicencia = SeleccionarLicencia.idLicencia;
            string tituloPorElCualSeVerifica = txtTitulo.Text;
            int? idMarca = SeleccionarLicencia.idMarca;
            int idTitular = SeleccionarLicencia.idTitular;
            string territorio = txtTerritorio.Text;
            string razonSocial = txtRazonSocial.Text;
            string direccion = txtDireccion.Text;
            string domicilio = txtDomicilio.Text;
            string nacionalidad = txtNacionalidad.Text;
            string apoderado = txtApoderadoRL.Text;
            DateTime fechaInicio = dateTimePickerInicio.Value;
            DateTime fechaFin = dateTimePickerFin.Value;
            string tipo;
            int anios = (int)numericAnio.Value;
            int meses = (int)numericMes.Value;
            int dias = (int)numericDia.Value;
            if (exclusiva == true) { tipo = "exclusiva"; }
            else { tipo = "no exclusiva"; }


            try
            {
                LicenciaUsoModel licenciaUso = new LicenciaUsoModel();
                if (licenciaUso.VerificarCompatibilidadLicenciaUso((int)idMarca, tipo, "nacional"))
                {
                    string mensaje = "";

                    if (tipo == "exclusiva")
                    {
                        mensaje = "NO ES POSIBLE MARCAR ESTA LICENCIA COMO EXCLUSIVA PORQUE EXISTEN OTRAS LICENCIAS NO EXCLUSIVAS CON ESTA MARCA.";
                    }
                    else if (tipo == "no exclusiva")
                    {
                        mensaje = "NO ES POSIBLE MARCAR ESTA LICENCIA COMO NO EXCLUSIVA PORQUE EXISTE UNA LICENCIA EXCLUSIVA VIGENTE PARA ESTA MARCA.";
                    }
                    else
                    {
                        mensaje = "TIPO DE LICENCIA NO RECONOCIDO.";
                    }

                    FrmAlerta alertaExiste = new FrmAlerta(mensaje, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alertaExiste.ShowDialog();
                    return;
                }
                else
                {
                    licenciaUso.EditarLicenciaUso(idLicencia, (int)idMarca, idTitular, tituloPorElCualSeVerifica, tipo, fechaInicio, fechaFin, territorio,
                         razonSocial, direccion, domicilio, nacionalidad, apoderado, estado, "nacional", anios, meses,dias);
                    FrmAlerta alerta = new FrmAlerta("LICENCIA DE USO ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();

                }

                LimpiarFormularioLicencia();
                AnadirTabPage(tabPageOposicionesList);
                tabControl1.SelectedTab = tabPageOposicionesList;
                EliminarTabPage(tabPageAgregarOposicion);
                await CargarDatosLicenciasUsoAsync();

            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarH_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEstatusH_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void iconButton4_Click_1(object sender, EventArgs e)
        {

        }

        private void btnActualizarM_Click(object sender, EventArgs e)
        {
            //ActualizarMarcaInternacional();

        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {

        }

        private void dtgMarcasOp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            buscando1 = false;
            Editar();
        }
        public async Task FiltrarExclusivasPorSituacionActual()
        {

            if (cmbSituacionActual.SelectedIndex == 0)
            {
                await LoadLicenciasUsoExclusivas("En Trámite");
            }
            else if (cmbSituacionActual.SelectedIndex == 1)
            {
                await LoadLicenciasUsoExclusivas("En Uso");
            }
            else if (cmbSituacionActual.SelectedIndex == 2)
            {
                await LoadLicenciasUsoExclusivas("Terminada");
            }
        }

        public async Task FiltrarNoExclusivasPorSituacionActual()
        {

            if (cmbSituacionActual2.SelectedIndex == 0)
            {
                await LoadLicenciasUsoNoExclusivas("En Trámite");
            }
            else if (cmbSituacionActual2.SelectedIndex == 1)
            {
                await LoadLicenciasUsoNoExclusivas("En Uso");
            }
            else if (cmbSituacionActual2.SelectedIndex == 2)
            {
                await LoadLicenciasUsoNoExclusivas("Terminada");
            }
        }
        private async void cmbSituacionActual_SelectedIndexChanged(object sender, EventArgs e)
        {
            await FiltrarExclusivasPorSituacionActual();
        }
        public void MostrarLogos()
        {

        }
        private void iconButton6_Click(object sender, EventArgs e)
        {
            btnEnviarATerminar.Visible = false;
            AnadirTabPage(tabPageAgregarOposicion);
            EliminarTabPage(tabPageOposicionesList);
            txtExpediente.Enabled = true;
            txtTitular.Enabled = true;
            txtSigno.Enabled = true;
            txtRegistro.Enabled = true;
            txtRazonSocial.Enabled = true;
            btnDatosLicenciante.Enabled = true;
            //iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            tabControl1.SelectedTab = tabPageAgregarOposicion;
            comboBoxEstado.SelectedItem = "En Trámite";
            btnGuardarU.Text = "AGREGAR";
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnGuardarU.BackColor = Color.FromArgb(50, 164, 115);
        }

        private void checkBoxAgregarLogos_CheckedChanged(object sender, EventArgs e)
        {
            MostrarLogos();
        }

        private string ValidarCamposLicenciaUso(string accion)
        {
            if (accion == "agregando")
            {
                if (SeleccionarMarcaParaLicencia.idMarca == 0)
                    return "Debe seleccionar una marca.";
            }
            else if (accion == "editando")
            {
                if (SeleccionarLicencia.idMarca == 0)
                    return "Debe seleccionar una marca.";
            }

            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
                return "El campo 'Título' es obligatorio.";

            if (string.IsNullOrWhiteSpace(txtTerritorio.Text))
                return "El campo 'Territorio' es obligatorio.";
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                return "El campo 'Nombre o Razón Social' es obligatorio.";
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                return "El campo 'Dirección' es obligatorio.";
            if (string.IsNullOrWhiteSpace(txtDomicilio.Text))
                return "El campo 'Domicilio' es obligatorio.";
            if (string.IsNullOrWhiteSpace(txtNacionalidad.Text))
                return "El campo 'Nacionalidad' es obligatorio.";
            if (string.IsNullOrWhiteSpace(txtApoderadoRL.Text))
                return "El campo 'Apoderado o Representante Legal' es obligatorio.";
            if (comboBoxEstado.SelectedItem == null)
                return "Debe seleccionar un Estado.";
            if (dateTimePickerInicio.Value == DateTime.MinValue)
                return "Debe seleccionar una Fecha de Inicio.";
            if (dateTimePickerFin.Value == DateTime.MinValue)
                return "Debe seleccionar una Fecha de Fin.";
            if (dateTimePickerInicio.Value > dateTimePickerFin.Value)
                return "La Fecha de Inicio no puede ser mayor que la Fecha de Fin.";

            // Todo bien
            return null;
        }



        public async void AgregarLicencia()
        {

            string mensajeValidacion = ValidarCamposLicenciaUso("agregando");
            if (mensajeValidacion != null)
            {
                FrmAlerta alerta = new FrmAlerta(mensajeValidacion.ToUpper(), "VALIDACIÓN FALLIDA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }

            if (comboBoxEstado.SelectedItem == null)
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN ESTADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }
            string estado = comboBoxEstado.SelectedItem.ToString();

            string tituloPorElCualSeVerifica = txtTitulo.Text;
            int? idMarca = SeleccionarMarcaParaLicencia.idMarca;
            int idTitular = SeleccionarMarcaParaLicencia.idTitularMarca;
            string territorio = txtTerritorio.Text;
            string razonSocial = txtRazonSocial.Text;
            string direccion = txtDireccion.Text;
            string domicilio = txtDomicilio.Text;
            string nacionalidad = txtNacionalidad.Text;
            string apoderado = txtApoderadoRL.Text;
            DateTime fechaInicio = dateTimePickerInicio.Value;
            DateTime fechaFin = dateTimePickerFin.Value;
            string tipo;
            int anios = (int)numericAnio.Value;
            int meses = (int)numericMes.Value;
            int dias = (int)numericDia.Value;

            if (exclusiva == true) { tipo = "exclusiva"; }
            else { tipo = "no exclusiva"; }



            try
            {
                LicenciaUsoModel licenciaUso = new LicenciaUsoModel();
                if (licenciaUso.VerificarCompatibilidadLicenciaUso((int)idMarca, tipo, "nacional"))
                {
                    string mensaje = "";

                    if (tipo == "exclusiva")
                    {
                        mensaje = "NO ES POSIBLE AGREGAR UNA LICENCIA DE USO EXCLUSIVA PORQUE EXISTEN LICENCIAS NO EXCLUSIVAS PARA ESTA MARCA.";
                    }
                    else if (tipo == "no exclusiva")
                    {
                        mensaje = "NO ES POSIBLE AGREGAR UNA LICENCIA DE USO NO EXCLUSIVA PORQUE EXISTE UNA LICENCIA EXCLUSIVA VIGENTE PARA ESTA MARCA.";
                    }
                    else
                    {
                        mensaje = "TIPO DE LICENCIA NO RECONOCIDO.";
                    }

                    FrmAlerta alertaExiste = new FrmAlerta(mensaje, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alertaExiste.ShowDialog();
                    return;
                }
                else
                {
                    licenciaUso.InsertarLicenciaUso((int)idMarca, idTitular, tituloPorElCualSeVerifica, tipo, fechaInicio, fechaFin, territorio,
                         razonSocial, direccion, domicilio, nacionalidad, apoderado, estado, "nacional", anios, meses, dias);
                    FrmAlerta alerta = new FrmAlerta("LICENCIA DE USO AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();

                }

                LimpiarFormularioLicencia();
                AnadirTabPage(tabPageOposicionesList);
                tabControl1.SelectedTab = tabPageOposicionesList;
                EliminarTabPage(tabPageAgregarOposicion);
                await CargarDatosLicenciasUsoAsync();


            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void btnGuardarU_Click(object sender, EventArgs e)
        {
            if (btnGuardarU.Text == "AGREGAR")
            {
                AgregarLicencia();

            }
            else if (btnGuardarU.Text == "EDITAR")
            {
                if (!archivoSubido && necesitaArchivo)
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE SUBIR EL TÍTULO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }
                else
                {
                    EditarLicenciaUso();
                }
            }

        }
        public void TerminarLicencia()
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea terminar esta licencia?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                int idLicencia = SeleccionarLicencia.idLicencia;
                string mensaje = licenciaUso.FinalizarLicencia(idLicencia);

                string titulo = mensaje.StartsWith("Estado actualizado") ? "ÉXITO" : "ERROR";

                FrmAlerta alerta = new FrmAlerta(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
            }
            else
            {
                // Si el usuario selecciona "No", no hacer nada o mostrar un mensaje adicional si lo deseas
                MessageBox.Show("La operación ha sido cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnEnviarATramite_Click(object sender, EventArgs e)
        {
            if (SeleccionarLicencia.idLicencia != 0)
            {
                TerminarLicencia();
            }

        }
        public void LimpiarFormularioLicencia()
        {
            txtTitulo.Text = "";
            txtRegistro.Text = "";
            txtSigno.Text = "";
            txtRazonSocial.Text = "";
            txtTitular.Text = "";
            txtExpediente.Text = "";
            txtFolio.Text = "";
            txtTomo.Text = "";
            txtClase.Text = "";
            txtDireccion.Text = "";
            txtDomicilio.Text = "";
            txtNacionalidad.Text = "";
            txtApoderadoRL.Text = "";
            txtTerritorio.Text = "";
            comboBoxEstado.SelectedIndex = -1;
            radioButtonExclusiva.Checked = false;
            radioButtonNoExclusiva.Checked = false;
            dateTimePickerInicio.Value = DateTime.Now;
            dateTimePickerFin.Value = DateTime.Now;
            dateTimePFecha_vencimiento.Value = DateTime.Now;
            SeleccionarMarcaParaLicencia.LimpiarMarcaParaLicencia();
        }

        private async void btnCancelarU_Click(object sender, EventArgs e)
        {

            AnadirTabPage(tabPageOposicionesList);
            EliminarTabPage(tabPageAgregarOposicion);
            tabControl1.SelectedTab = tabPageOposicionesList;
            LimpiarFormularioLicencia();
            await CargarDatosLicenciasUsoAsync();
        }

        /*
        public async Task recargarDatosOposicion()
        {
            DataTable detallesOposicion = await Task.Run(() => oposicionModel.GetOposicionPorId(SeleccionarOposicion.idN));
            if (detallesOposicion.Rows.Count > 0)
            {
                DataRow row = detallesOposicion.Rows[0];

                SeleccionarOposicion.observaciones = row["observaciones"] == DBNull.Value ? null : row["observaciones"].ToString();

                SeleccionarOposicion.estado = row["estado"] == DBNull.Value ? null : row["estado"].ToString();

            }
        }*/

        private async void btnAgregarEstadoAO_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaOposicion frmAgregarEtapa = new FrmAgregarEtapaOposicion();
            frmAgregarEtapa.ShowDialog();


            if (btnGuardarU.Text == "EDITAR")
            {
                if (SeleccionarOposicion.idMarca == 0 || SeleccionarOposicion.idMarca.ToString() == null)
                {
                    if (AgregarEtapaOposicion.etapa != "")
                    {
                        try
                        {
                            HistorialOposicionModel historialOposicionModel = new HistorialOposicionModel();
                            historialOposicionModel.CrearHistorialOposicion((DateTime)AgregarEtapaOposicion.fecha, AgregarEtapaOposicion.etapa,
                                AgregarEtapaOposicion.anotaciones, AgregarEtapaOposicion.usuario, null, "OPOSICIÓN", SeleccionarOposicion.idInt
                                );

                            FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();

                            //await recargarDatosOposicion();
                        }
                        catch (Exception ex)
                        {
                            FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta.ShowDialog();
                        }

                    }
                    else
                    {
                        //txtEstadoAO.Text = "";
                        //richtxtObservacionesAO.Text = "";
                    }
                }
                else
                {
                    try
                    {
                        if (AgregarEtapaOposicion.etapa != "")
                        {
                            /*historialModel.GuardarEtapa(SeleccionarOposicion.idMarca, (DateTime)AgregarEtapaOposicion.fecha,
                            AgregarEtapaOposicion.etapa, AgregarEtapaOposicion.anotaciones,
                            AgregarEtapaOposicion.usuario, "OPOSICIÓN");*/
                            FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            //await recargarDatosOposicion();
                        }
                        else
                        {

                        }

                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }

                }
            }
            else if (btnGuardarU.Text == "AGREGAR")
            {
                if (AgregarEtapaOposicion.etapa != "")
                {
                    //txtEstadoAO.Text = AgregarEtapaOposicion.etapa;

                }
                else
                {
                    //txtEstadoAO.Text = "";
                }
            }
        }

        private void btnAgregarLogoOpositor_Click(object sender, EventArgs e)
        {

        }

        private void btnQuitarLogoOpositor_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarSignoPretendido_Click(object sender, EventArgs e)
        {

        }

        private void btnQuitarLogoSignoPretendido_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarOpositorAO_Click(object sender, EventArgs e)
        {

            FrmMostrarMarcas frmMostrarMarcas = new FrmMostrarMarcas();
            frmMostrarMarcas.ShowDialog();

            if (SeleccionarMarcaOposicion.idMarca != 0)
            {
                txtRegistro.Enabled = false;
                txtRazonSocial.Enabled = false;
                txtRegistro.Text = SeleccionarMarcaOposicion.nombreTitular;
                txtRazonSocial.Text = SeleccionarMarcaOposicion.nombreSigno;
            }
            else
            {
                txtRegistro.Enabled = true;
                txtRazonSocial.Enabled = true;
                txtRegistro.Text = "";
                txtRazonSocial.Text = "";
            }
        }



        private async void filtrarNoExclusivas()
        {
            string valor = txtBuscar2.Text;
            string situacion = cmbSituacionActual2.SelectedItem.ToString();

            if (valor != "")
            {
                totalRows2 = licenciaUso.GetFilteredLicenciasUsoNacionalesNoExclusivasCount(valor);
                totalPages2 = (int)Math.Ceiling((double)totalRows2 / pageSize2);
                lblTotalPages2.Text = totalPages2.ToString();
                lblTotalRows2.Text = totalRows2.ToString();
                var marcasR = await Task.Run(() => licenciaUso.FiltrarLicenciasUsoNacionalesNoExclusivas(valor, currentPageIndex2, pageSize2));
                if (marcasR.Rows.Count > 0)
                {
                    Invoke(new Action(() =>
                    {
                        dtgLicenciasNoEx.DataSource = marcasR;
                        dtgLicenciasNoEx.Refresh();

                        if (dtgLicenciasNoEx.Columns["id"] != null)
                        {
                            dtgLicenciasNoEx.Columns["id"].Visible = false;
                            dtgLicenciasNoEx.Columns["IdMarca"].Visible = false;
                            dtgLicenciasNoEx.ClearSelection();
                        }
                    }));
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN OPOSICIONES INTERPUESTAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    await LoadLicenciasUsoNoExclusivas(situacion);
                }

            }
            else
            {
                await LoadLicenciasUsoNoExclusivas(situacion);
            }

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {
            //filtrarMarcas();
            buscando1 = true;
            currentPageIndex = 1;
            totalRows = licenciaUso.GetFilteredLicenciasUsoNacionalesExclusivasCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrarLicenciasUsoExclusivas();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            buscando1 = false;
            txtBuscar.Text = "";
            //filtrarMarcas();
            filtrarLicenciasUsoExclusivas();
        }

        private void btnAgregarOpositorAO_Click_1(object sender, EventArgs e)
        {
            FrmMostrarMarcasLicencias frmMostrarMarcas = new FrmMostrarMarcasLicencias();
            frmMostrarMarcas.ShowDialog();

            if (SeleccionarMarcaParaLicencia.idMarca > 0)
            {
                txtTitular.Enabled = false;
                txtSigno.Enabled = false;
                txtExpediente.Enabled = false;
                comboBoxSignoDist.Enabled = false;
                txtClase.Enabled = false;
                dateTimePFecha_vencimiento.Enabled = false;
                txtFolio.Enabled = false;
                txtTomo.Enabled = false;
                txtTitular.Text = SeleccionarMarcaParaLicencia.nombreTitular;
                txtSigno.Text = SeleccionarMarcaParaLicencia.nombreSigno;
                txtClase.Text = SeleccionarMarcaParaLicencia.clase;
                txtRegistro.Text = SeleccionarMarcaParaLicencia.registro;
                txtFolio.Text = SeleccionarMarcaParaLicencia.folio;
                txtTomo.Text = SeleccionarMarcaParaLicencia.tomo;
                txtExpediente.Text = SeleccionarMarcaParaLicencia.expediente;
                comboBoxSignoDist.SelectedItem = SeleccionarMarcaParaLicencia.signoDistintivo;
                dateTimePFecha_vencimiento.Value = SeleccionarMarcaParaLicencia.fechaVencimiento;

            }

        }

        private async void btnAgregarEstadoAO_Click_1(object sender, EventArgs e)
        {
            FrmAgregarEtapaOposicionI frmAgregarEtapa = new FrmAgregarEtapaOposicionI();
            frmAgregarEtapa.ShowDialog();


            if (btnGuardarU.Text == "EDITAR")
            {
                if (SeleccionarOposicion.idMarca == 0)
                {
                    if (AgregarEtapaOposicion.etapa != "")
                    {
                        try
                        {
                            FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();

                        }
                        catch (Exception ex)
                        {
                            FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta.ShowDialog();
                        }

                    }
                    else
                    {

                    }
                }
                else
                {
                    try
                    {
                        if (AgregarEtapaOposicion.etapa != "")
                        {
                            /*
                            historialModel.GuardarEtapa(SeleccionarOposicion.idMarca, (DateTime)AgregarEtapaOposicion.fecha,
                            AgregarEtapaOposicion.etapa, AgregarEtapaOposicion.anotaciones,
                            AgregarEtapaOposicion.usuario, "OPOSICIÓN");*/
                            FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                        }
                        else
                        {

                        }

                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }

                }
            }
            else if (btnGuardarU.Text == "AGREGAR")
            {
                if (AgregarEtapaOposicion.etapa != "")
                {

                }
                else
                {
                }
            }
        }

        private async void cmbSituacionActualI_SelectedIndexChanged(object sender, EventArgs e)
        {
            await FiltrarNoExclusivasPorSituacionActual();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            buscando2 = false;
            txtBuscar2.Text = "";
            filtrarNoExclusivas();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando1 = true;
                currentPageIndex = 1;
                totalRows = licenciaUso.GetFilteredLicenciasUsoNacionalesExclusivasCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                filtrarLicenciasUsoExclusivas();
            }
        }

        private void ibtnBuscar2_Click(object sender, EventArgs e)
        {
            buscando2 = true;
            currentPageIndex2 = 1;
            totalRows2 = licenciaUso.GetFilteredLicenciasUsoNacionalesNoExclusivasCount(txtBuscar2.Text);
            totalPages2 = (int)Math.Ceiling((double)totalRows2 / pageSize2);

            lblCurrentPage2.Text = currentPageIndex2.ToString();
            lblTotalPages2.Text = totalPages2.ToString();
            lblTotalRows2.Text = totalRows2.ToString();
            filtrarNoExclusivas();
        }

        private void txtBuscar2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando2 = true;
                currentPageIndex2 = 1;
                totalRows2 = licenciaUso.GetFilteredLicenciasUsoNacionalesNoExclusivasCount(txtBuscar2.Text);
                totalPages2 = (int)Math.Ceiling((double)totalRows2 / pageSize2);

                lblCurrentPage2.Text = currentPageIndex2.ToString();
                lblTotalPages2.Text = totalPages2.ToString();
                lblTotalRows2.Text = totalRows2.ToString();
                filtrarNoExclusivas();
            }
        }

        private void roundedButton6_Click_1(object sender, EventArgs e)
        {

        }

        private void dtgOpI_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            buscando2 = false;
            Editar();
        }

        private void dtgMarcasOp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgLicenciasNoEx.ClearSelection();
        }

        private void dtgOpI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgLicenciasExclusivas.ClearSelection();
        }

        private void dtgMarcasOp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgLicenciasNoEx.ClearSelection();
        }

        private void dtgOpI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgLicenciasExclusivas.ClearSelection();
        }

        private void btnIrAReportes_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageReportes);
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageOposicionesList);
            cmbSituacionActual.SelectedIndex = 0;
            cmbSituacionActual2.SelectedIndex = 0;
            EliminarTabPage(tabPageReportes);
        }

        public void Filtrar()
        {
            string tipoLicencia = null;
            string expediente = null;
            string tituloVerifica = null;
            string signo = null;
            string signoDistintivo = null;
            string estado = null;
            string clase = null;
            string origen = "nacional";
            string nombreRazonSocial = null;
            string titular = null;
            numRegistros = 9;
            escala = 0.85f;

            titulo = "REPORTE DE LICENCIAS DE USO NACIONALES ";

            if (chckTipoLicenciaReporte.Checked)
            {
                switch (comboBoxTipoLicenciaReporte.SelectedIndex)
                {
                    case 0:
                        tipoLicencia = "exclusiva";
                        titulo += "EXCLUSIVAS";
                        break;
                    case 1:
                        tipoLicencia = "no exclusiva";
                        titulo += "NO EXCLUSIVAS";
                        break;
                }
            }
            else
            {
                tipoLicencia = null;
            }

            if (chckExpedienteReporte.Checked)
            { expediente = txtExpedienteReporte.Text; }
            else
            { expediente = null; }


            if (chckTituloPorElQueSeVerificaReporte.Checked)
            { tituloVerifica = txtTituloReporte.Text; }
            else { tituloVerifica = null; }


            if (chckSignoRepo.Checked) { signo = txtSignoReporte.Text; }
            else { signo = null; }

            if (chckSignoDistintivoReporte.Checked) { signoDistintivo = cmbSignoDistintivoReporte.SelectedItem.ToString(); }
            else { signoDistintivo = null; }

            if (chckEstadoReporte.Checked) { estado = cmbEstadolReporte.Text; }
            else { estado = null; }


            if (chckClaseReporte.Checked) { clase = txtClaseReporte.Text; }
            else { clase = null; }

            if (chckNombreRazonSocial.Checked) { nombreRazonSocial = txtNombreRazonSOCIAL.Text; }
            else { nombreRazonSocial = null; }

            if (chckTitularReporte.Checked) { titular = richTextBoxtTitularReporte.Text; }
            else { titular = null; }


            dtgReportesOp.DataSource = licenciaUso.FiltrarLicenciasUso(tipoLicencia, expediente, tituloVerifica, signo, signoDistintivo,
                estado, clase, "nacional", nombreRazonSocial, titular);
            dtgReportesOp.ClearSelection();

        }

        public void LimpiarReportes()
        {
            //checkBoxEstadoReporte.Checked = false;
            //comboBoxEstadoReporte.SelectedIndex = -1;
            chckTipoLicenciaReporte.Checked = false;
            comboBoxTipoLicenciaReporte.SelectedIndex = -1;
            chckExpedienteReporte.Checked = false;
            txtExpedienteReporte.Text = "";

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            dtgReportesOp.DataSource = null;
            dtgReportesOp.ClearSelection();
        }

        private async void CrearPdfDesdeHtmlConLogoYDataTable(DataTable dt, int registrosPagina, float escalas, string titulo)
        {
            // Buscar la ruta de Chrome automáticamente
            string chromePath = "chrome"; // Intentará usar Chrome desde PATH

            string[] possiblePaths = {
        System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Google\\Chrome\\Application\\chrome.exe"),
        System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google\\Chrome\\Application\\chrome.exe"),
        System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\Application\\chrome.exe")
    };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    chromePath = path;
                    break;
                }
            }

            string nombre = titulo + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = nombre + ".pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = chromePath
                });

                var page = await browser.NewPageAsync();
                int totalPaginas = (int)Math.Ceiling((double)dt.Rows.Count / registrosPagina);
                string fullHtmlContent = "";

                for (int pagina = 0; pagina < totalPaginas; pagina++)
                {
                    string tableContent = "";
                    int startRecord = pagina * registrosPagina;
                    int endRecord = Math.Min((pagina + 1) * registrosPagina, dt.Rows.Count);

                    for (int i = startRecord; i < endRecord; i++)
                    {
                        DataRow row = dt.Rows[i];
                        tableContent += "<tr>";
                        foreach (DataColumn column in dt.Columns)
                        {
                            string alignStyle = (column.ColumnName == "REGISTRO" || column.ColumnName == "FOLIO" || column.ColumnName == "TOMO" || column.ColumnName == "CLASE")
                                ? "style='padding: 8px; text-align: right; border: 1px solid #ddd;'"
                                : (column.ColumnName == "TIPO_OPOSICION" || column.ColumnName == "SITUACION_ACTUAL")
                                    ? "style='padding: 8px; text-align: center; border: 1px solid #ddd;'"
                                    : "style='padding: 8px; text-align: left; border: 1px solid #ddd;'";

                            object cellValue = row[column];
                            if (cellValue is DateTime dateValue)
                            {
                                cellValue = dateValue.ToString("dd/MM/yyyy");
                            }

                            tableContent += $"<td {alignStyle}>{cellValue}</td>";
                        }

                        tableContent += "</tr>";

                    }

                    string headers = "";
                    foreach (DataColumn column in dt.Columns)
                    {
                        headers += $"<th style='padding: 8px; text-align: left; border: 1px solid #ddd; background-color: #f2f2f2; font-weight: bold;'>{column.ColumnName}</th>";
                    }

                    string base64Logo;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Properties.Resources.logoBPA2.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageBytes = ms.ToArray();
                        base64Logo = Convert.ToBase64String(imageBytes);
                    }

                    string imageHtml = $"<img src='data:image/png;base64,{base64Logo}' />";

                    fullHtmlContent += $@"<html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; }}
                        table {{ border-collapse: collapse; width: 100%; }}
                        th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
                        th {{ background-color: #f2f2f2; font-weight: bold; }}
                        img {{ width: 200px; height: auto; }}
                        @page {{ size: legal landscape; margin: 20mm; }}
                        table {{ page-break-inside: auto; }}
                        tr {{ page-break-inside: avoid; }}
                        td {{ page-break-before: auto; }}
                        .header {{ text-align: center; font-size: 20px; font-weight: bold; margin-bottom: 10px; }}
                    </style>
                </head>
                <body>
                    <div class='header'>{titulo}</div>
                    <div class='fecha'><center>Fecha: {DateTime.Now:dd-MM-yyyy HH:mm}</center></div>
                    {imageHtml}
                    <table>
                        <thead><tr>{headers}</tr></thead>
                        <tbody>{tableContent}</tbody>
                    </table>
                    {(pagina < totalPaginas - 1 ? "<div style='page-break-before: always;'></div>" : "")}
                </body>
            </html>";
                }

                await page.SetContentAsync(fullHtmlContent);
                string pdfFilePath = saveFileDialog.FileName;

                await page.PdfAsync(pdfFilePath, new PdfOptions
                {
                    Format = PaperFormat.Legal,
                    PrintBackground = true,
                    Landscape = true,
                    Scale = (Decimal)escalas
                });

                await browser.CloseAsync();
                FrmAlerta alerta = new FrmAlerta("PDF GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ NINGUNA RUTA PARA GUARDAR EL PDF", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }


        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            DataTable datos = dtgReportesOp.DataSource as DataTable;

            if (datos != null)
            {
                CrearPdfDesdeHtmlConLogoYDataTable(datos, numRegistros, escala, titulo);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        public void ExportarDataTableAExcel(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }
            string nombre = titulo + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
            {
                Title = "Guardar archivo Excel",
                Filter = "Archivos Excel (*.xlsx)|*.xlsx",
                FileName = nombre + ".xlsx",
                DefaultExt = "xlsx",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string tempLogoPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_logo.png");

                    // Guardar el recurso de imagen en un archivo temporal
                    Properties.Resources.logoBPA2.Save(tempLogoPath);

                    using (var workbook = new XLWorkbook())
                    {
                        // Crear la hoja de trabajo
                        var worksheet = workbook.Worksheets.Add("REPORTE OPOSICIÓN");

                        // Fecha actual en el formato deseado
                        string fecha = DateTime.Now.ToString("dd-MM-yyyy-HH-mm");

                        // Insertar el título "titulo" en la celda A1
                        worksheet.Cell(3, 5).Value = titulo;
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Style.Font.Underline = XLFontUnderlineValues.Single;
                        worksheet.Cell(3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;  // Centrar el título

                        // Insertar la fecha debajo del título (en la celda A2)
                        worksheet.Cell(4, 5).Value = "Fecha: " + fecha;
                        worksheet.Cell(4, 5).Style.Font.Italic = true;
                        worksheet.Cell(4, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;  // Centrar la fecha

                        // Ajustar el ancho de la columna A (para que todo el texto se vea bien centrado)
                        worksheet.Column(1).AdjustToContents();

                        // Agregar logo después del título y la fecha (en la celda A3)
                        if (System.IO.File.Exists(tempLogoPath))
                        {
                            var image = worksheet.AddPicture(tempLogoPath)
                                .MoveTo(worksheet.Cell(3, 1)) // Posicionar el logo en la celda 3, 1
                                .Scale(0.5); // Ajustar tamaño
                        }

                        // Insertar tabla después del logo (a partir de la fila 10)
                        int startRow = 10; // Ajustar según el espacio requerido
                        worksheet.Cell(startRow, 1).InsertTable(dataTable);

                        // Ajustar el ancho de las columnas
                        worksheet.Columns().AdjustToContents();

                        // Guardar archivo
                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    // Eliminar archivo temporal
                    if (System.IO.File.Exists(tempLogoPath))
                        System.IO.File.Delete(tempLogoPath);

                    // Mostrar mensaje de éxito
                    FrmAlerta alerta = new FrmAlerta("ARCHIVO GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el archivo: {ex.Message}");
                }
            }
        }

        private string LimpiarNombreArchivo(string nombre)
        {
            // Reemplazar caracteres no permitidos por un guion bajo o simplemente eliminarlos
            string[] caracteresNoPermitidos = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|", "&", "%", "$" };
            foreach (var caracter in caracteresNoPermitidos)
            {
                nombre = nombre.Replace(caracter, "_");
            }

            // Asegurarse de que el nombre no exceda los 255 caracteres
            if (nombre.Length > 255)
            {
                nombre = nombre.Substring(0, 255);
            }

            return nombre;
        }


        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            DataTable datos = dtgReportesOp.DataSource as DataTable;

            if (datos != null)
            {
                ExportarDataTableAExcel(datos);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA EXPORTAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para exportar.");
            }
        }

        private void btnOpositorReporte_Click(object sender, EventArgs e)
        {
            FrmMostrarOpositoresReportes frmMostrarOpositores = new FrmMostrarOpositoresReportes();
            frmMostrarOpositores.ShowDialog();

            if (SeleccionarPersonaReportes.nombreTitular != "")
            {
                //richTextBoxOpositorReporte.Text = SeleccionarPersonaReportes.nombreTitular;
            }
            else
            {
                //richTextBoxOpositorReporte.Text = "";
            }
        }

        private void btnSolicitanteReporte_Click(object sender, EventArgs e)
        {
            FrmMostrarSolicitantesReportes frmMostrarSolicitantes = new FrmMostrarSolicitantesReportes();
            frmMostrarSolicitantes.ShowDialog();

            if (SeleccionarPersonaReportes.nombreTitular != "")
            {
                richTextBoxtTitularReporte.Text = SeleccionarPersonaReportes.nombreTitular;
            }
            else
            {
                richTextBoxtTitularReporte.Text = "";
            }
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (buscando1 == true)
            {
                filtrarLicenciasUsoExclusivas();
            }
            else
            {
                await FiltrarExclusivasPorSituacionActual();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (buscando1 == true)
                {
                    filtrarLicenciasUsoExclusivas();
                }
                else
                {
                    await FiltrarExclusivasPorSituacionActual();
                }
                
                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPageIndex < totalPages)
            {
                currentPageIndex++;
                if (buscando1 == true)
                {
                    filtrarLicenciasUsoExclusivas();
                }
                else
                {
                    await FiltrarExclusivasPorSituacionActual();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (buscando1 == true)
            {
                filtrarLicenciasUsoExclusivas();
            }
            else
            {
                await FiltrarExclusivasPorSituacionActual();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnFirst2_Click(object sender, EventArgs e)
        {
            currentPageIndex2 = 1;
            if (buscando2 == true)
            {
                filtrarNoExclusivas();
            }
            else
            {
                await FiltrarNoExclusivasPorSituacionActual();
            }

            lblCurrentPage2.Text = currentPageIndex2.ToString();
        }

        private async void btnPrev2_Click(object sender, EventArgs e)
        {
            if (currentPageIndex2 > 1)
            {
                currentPageIndex2--;
                if (buscando2 == true)
                {
                    filtrarNoExclusivas();
                }
                else
                {
                    await FiltrarNoExclusivasPorSituacionActual();
                }

                lblCurrentPage2.Text = currentPageIndex2.ToString();
            }
        }

        private async void btnNext2_Click(object sender, EventArgs e)
        {
            if (currentPageIndex2 < totalPages2)
            {
                currentPageIndex2++;
                if (buscando2 == true)
                {
                    filtrarNoExclusivas();
                }
                else
                {
                    await FiltrarNoExclusivasPorSituacionActual();
                }

                lblCurrentPage2.Text = currentPageIndex2.ToString();
            }
        }

        private async void btnLast2_Click(object sender, EventArgs e)
        {
            currentPageIndex2 = totalPages2;
            if (buscando2 == true)
            {
                filtrarNoExclusivas();
            }
            else
            {
                await FiltrarNoExclusivasPorSituacionActual();
            }

            lblCurrentPage2.Text = currentPageIndex2.ToString();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roundedButton9_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonExclusiva_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonExclusiva.Checked)
            {
                radioButtonNoExclusiva.Checked = false;
                exclusiva = true;
            }
        }

        private void radioButtonNoExclusiva_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNoExclusiva.Checked)
            {
                radioButtonExclusiva.Checked = false;
                exclusiva = false;
            }
        }

        private void dateTimePickerInicio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            verificarBotones();
        }

        private void txtTitular_TextChanged(object sender, EventArgs e)
        {

        }


        void verificarBotones()
        {
            var comboEstado = comboBoxEstado.SelectedItem as string;

            if (!string.IsNullOrEmpty(comboEstado))
            {
                if (SeleccionarLicencia.estado == "En Trámite" && comboEstado == "En Uso")
                {
                    btnAdjuntarT.Visible = true;
                    necesitaArchivo = true;
                    btnVerArchivos.Visible = false;
                }
                else if (SeleccionarLicencia.estado == "En Trámite" && comboEstado == "En Trámite")
                {
                    btnAdjuntarT.Visible = false;
                    necesitaArchivo = false;
                    btnVerArchivos.Visible = false;
                }
                else if (SeleccionarLicencia.estado == "En Uso" && comboEstado == "En Uso")
                {
                    btnAdjuntarT.Visible = false;
                    necesitaArchivo = false;
                    btnVerArchivos.Visible = true;
                }
                else if (SeleccionarLicencia.estado == "Terminada" && comboEstado == "Terminada")
                {
                    btnAdjuntarT.Visible = false;
                    necesitaArchivo = false;
                    btnVerArchivos.Visible = true;
                }
                else if (comboEstado == "En Trámite")
                {
                    btnAdjuntarT.Visible = false;
                    necesitaArchivo = false;
                    btnVerArchivos.Visible = false;
                }
                else if (comboEstado == "En Uso")
                {
                    btnAdjuntarT.Visible = true;
                    necesitaArchivo = true;
                    btnVerArchivos.Visible = false;
                }
            }
            else
            {
                if (comboBoxEstado.SelectedIndex == -1)
                {
                    btnVerArchivos.Visible = false;
                    necesitaArchivo = false;
                    btnAdjuntarT.Visible = false;
                }
                else if (comboBoxEstado.SelectedIndex == 1)
                {
                    btnAdjuntarT.Visible = false;
                    necesitaArchivo = false;
                    btnVerArchivos.Visible = false;
                }
                else if (comboBoxEstado.SelectedIndex == 2)
                {
                    btnAdjuntarT.Visible = true;
                    necesitaArchivo = true;
                    btnVerArchivos.Visible = false;
                }
                else if (comboBoxEstado.SelectedIndex == 3)
                {
                    btnAdjuntarT.Visible = false;
                    necesitaArchivo = false;
                    btnVerArchivos.Visible = true;
                }
            }
        }
        private void comboBoxEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            verificarBotones();


        }
        private void SubirArchivoRegistro(string idLicencia)
        {
            string carpeta = $"{directorioBase}/licencia-{idLicencia}/";
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
            SubirArchivoRegistro("" + SeleccionarLicencia.idLicencia);
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
        public void CrearCarpetaMarca(string idLicencia)
        {
            string carpetaMarca = $"{directorioBase}/licencia-{idLicencia}"; // Ruta completa para la carpeta de la licencia

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
        private List<string> ListarNombresDeArchivos(string idLicencia)
        {
            string carpetaMarca = $"{directorioBase}/licencia-{idLicencia}";
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

                AnadirTabPage(tabPageArchivos);
                //tabControl1.Visible = false;

                string id = "" + SeleccionarLicencia.idLicencia;
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
                //tabControl1.Visible = true;
            }
            finally
            {
                // Restaurar el cursor global a "Default"
                Cursor.Current = Cursors.Default;
            }
        }

        private void SubirArchivo(string idLicencia)
        {
            string carpeta = $"{directorioBase}/licencia-{idLicencia}/";
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

                    MessageBox.Show($"Error al subir el archivo: {ex.Message}\n" + ex.InnerException.Message);
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private void AbrirArchivoDesdeFtp(string idLicencia, string archivoNombre)
        {
            string carpeta = $"{directorioBase}/licencia-{idLicencia}/";
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
            string idMarca = "" + SeleccionarLicencia.idLicencia; // Id de la licencia actual
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

        private void EliminarArchivoDesdeFtp(string idLicencia, string archivoNombre)
        {
            string carpeta = $"{directorioBase}/licencia-{idLicencia}/";
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
            string idMarca = "" + SeleccionarLicencia.idLicencia; // Id de la marca actual
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
        private void btnVerArchivos_Click(object sender, EventArgs e)
        {
            ListarArchivosEnGeneral();
        }

        private void iconButton17_Click(object sender, EventArgs e)
        {
            SubirArchivo("" + SeleccionarLicencia.idLicencia);
            ListarArchivosEnGeneral();
        }

        private void btnAbrirArchivo_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void btnEliminarArchivo_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void iconButton18_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageAgregarOposicion);
            this.BeginInvoke(new Action(() =>
            {
                EliminarTabPage(tabPageArchivos);
            }));

        }

        private async void btnEliminarLicencia_Click(object sender, EventArgs e)
        {
            // Verificar si no hay nada seleccionado en ambos
            if (dtgLicenciasExclusivas.SelectedRows.Count == 0 && dtgLicenciasNoEx.SelectedRows.Count == 0)
            {
                FrmAlerta alerta = new FrmAlerta("DEBE SELECCIONAR UNA LICENCIA PARA ELIMINAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }

            DataGridView gridSeleccionado = null;

            // Si ambas tienen selección, preguntar al usuario
            if (dtgLicenciasExclusivas.SelectedRows.Count > 0 && dtgLicenciasNoEx.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show(
                         "Ha seleccionado una licencia de uso en ambas listas.\n\n" +
                         "¿Qué desea eliminar?\n\n" +
                         "Sí: Eliminar licencia EXCLUSIVA\n" +
                         "No: Eliminar licencia NO EXCLUSIVA\n" +
                         "Cancelar: Anular operación",
                         "¿Qué tipo de licencia desea eliminar?",
                         MessageBoxButtons.YesNoCancel,
                         MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    gridSeleccionado = dtgLicenciasExclusivas;
                }
                else if (resultado == DialogResult.No)
                {
                    gridSeleccionado = dtgLicenciasNoEx;
                }
                else
                {
                    return;
                }

            }

            else if (dtgLicenciasExclusivas.SelectedRows.Count > 0)
            {
                gridSeleccionado = dtgLicenciasExclusivas;
            }
            else if (dtgLicenciasNoEx.SelectedRows.Count > 0)
            {
                gridSeleccionado = dtgLicenciasNoEx;
            }

            // Validar la fila seleccionada
            var filaSeleccionada = gridSeleccionado.SelectedRows[0];
            if (!(filaSeleccionada.DataBoundItem is DataRowView dataRowView))
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL OBTENER LA INFORMACIÓN DE LA LICENCIA ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                return;
            }

            int idLicencia = Convert.ToInt32(dataRowView["id"]);

            FrmAlerta confirmar = new FrmAlerta(
                "¿ESTÁ SEGURO QUE DESEA ELIMINAR ESTA LICENCIA DE USO?",
                "CONFIRMACIÓN",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            var confirmacion = confirmar.ShowDialog();

            if (confirmacion == DialogResult.Yes)
            {

                bool eliminado = false;

                try
                {
                    eliminado = licenciaUso.EliminarLicenciaUso(idLicencia, UsuarioActivo.usuario);
                }
                catch (Exception ex)
                {
                    FrmAlerta error = new FrmAlerta("ERROR AL ELIMINAR LA LICENCIA DE USO:\n" + ex.Message.ToUpper(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error.ShowDialog();
                    return;
                }

                if (eliminado)
                {
                    FrmAlerta exito = new FrmAlerta("LICENCIA DE USO ELIMINADA CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    exito.ShowDialog();
                    cmbSituacionActual.SelectedIndex = 0;
                    cmbSituacionActual2.SelectedIndex = 0;
                    await CargarDatosLicenciasUsoAsync();
                }
                else
                {
                    FrmAlerta error = new FrmAlerta("NO SE PUDO ELIMINAR LA LICENCIA DE USO.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error.ShowDialog();
                }
            }
        }

        private void numericAnio_ValueChanged(object sender, EventArgs e)
        {
            int anios = (int)numericAnio.Value;
            int meses = (int)numericMes.Value;
            int dias = (int)numericDia.Value;
            DateTime fechaInicio = dateTimePickerInicio.Value;

            DateTime nuevaFecha = fechaInicio.AddYears(anios).AddMonths(meses).AddDays(dias);

            dateTimePickerFin.Value = nuevaFecha;
        }

        private void numericMes_ValueChanged(object sender, EventArgs e)
        {
            int anios = (int)numericAnio.Value;
            int meses = (int)numericMes.Value;
            int dias = (int)numericDia.Value;
            DateTime fechaInicio = dateTimePickerInicio.Value;

            DateTime nuevaFecha = fechaInicio.AddYears(anios).AddMonths(meses).AddDays(dias);

            dateTimePickerFin.Value = nuevaFecha;
        }

        private void numericDia_ValueChanged(object sender, EventArgs e)
        {
            int anios = (int)numericAnio.Value;
            int meses = (int)numericMes.Value;
            int dias = (int)numericDia.Value;
            DateTime fechaInicio = dateTimePickerInicio.Value;

            DateTime nuevaFecha = fechaInicio.AddYears(anios).AddMonths(meses).AddDays(dias);

            dateTimePickerFin.Value = nuevaFecha;
        }
    }
}
