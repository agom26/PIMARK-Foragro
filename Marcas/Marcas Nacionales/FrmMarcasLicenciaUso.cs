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

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMarcasLicenciaUso : Form
    {
        LicenciaUsoModel licenciaUso = new LicenciaUsoModel();
        public int numRegistros = 0;
        public float escala = 0;
        string titulo;
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        bool exclusiva = false;

        private const int pageSize2 = 20;
        private int currentPageIndex2 = 1;
        private int totalPages2 = 0;
        private int totalRows2 = 0;
        private bool buscando1 = false;
        private bool buscando2 = false;

        public FrmMarcasLicenciaUso()
        {
            InitializeComponent();
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            this.Load += FrmMarcasLicenciaUso_Load;

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
            

            totalRows = licenciaUso.GetTotalLicenciasUsoNacionalesExclusivas(situacionActual);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            var marcasN = await Task.Run(() => licenciaUso.ObtenerLicenciasUsoNacionalesExclusivas(situacionActual, currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgMarcasOp.DataSource = marcasN;

                if (dtgMarcasOp.Columns["id"] != null)
                {

                    dtgMarcasOp.Columns["IdMarca"].Visible = false;
                    dtgMarcasOp.Columns["id"].Visible = false;
                    dtgMarcasOp.ClearSelection();
                }


            }));
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
                    dtgMarcasOp.DataSource = titulares;
                    if (dtgMarcasOp.Columns["id"] != null)
                    {
                        dtgMarcasOp.Columns["id"].Visible = false;
                        dtgMarcasOp.Columns["IdMarca"].Visible = false;
                    }
                    dtgMarcasOp.ClearSelection();
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


        private async Task LoadLicenciasUsoNoExclusivas(string situacionActual)
        {
            totalRows2 = licenciaUso.GetTotalLicenciasUsoNacionalesNoExclusivas(situacionActual);
            totalPages2 = (int)Math.Ceiling((double)totalRows2 / pageSize2);
            var marcasN = await Task.Run(() => licenciaUso.ObtenerLicenciasUsoNacionalesNoExclusivas(situacionActual, currentPageIndex2, pageSize2));

            Invoke(new Action(() =>
            {
                lblTotalPages2.Text = totalPages2.ToString();
                lblTotalRows2.Text = totalRows2.ToString();
                dtgOpI.DataSource = marcasN;
                dtgOpI.Refresh();

                if (dtgOpI.Columns["id"] != null)
                {
                    dtgOpI.Columns["IdMarca"].Visible = false;
                    dtgOpI.Columns["id"].Visible = false;
                    dtgOpI.ClearSelection();
                }
            }));
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }

            tabControl1.SelectedTab = nombre;
        }




        private bool ValidarCampo(string campo, string mensaje)
        {
            if (string.IsNullOrEmpty(campo))
            {
                FrmAlerta alerta = new FrmAlerta(mensaje.ToUpper(), "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return false;
            }
            return true;
        }

        private void FrmMarcasLicenciaUso_Load(object sender, EventArgs e)
        {

            _ = CargarDatosLicenciasUsoAsync();
        }

        private async Task CargarDatosLicenciasUsoAsync()
        {
            try
            {
                currentPageIndex = 1;
                currentPageIndex2 = 1;

                int currP = await Task.Run(() => licenciaUso.GetTotalLicenciasUsoNacionalesExclusivas("En Trámite"));
                int currP2 = await Task.Run(() => licenciaUso.GetTotalLicenciasUsoNacionalesNoExclusivas("En Trámite"));

                if (currP == 0)
                    currentPageIndex = 0;

                if (currP2 == 0)
                    currentPageIndex2 = 0;

                lblCurrentPage.Text = currentPageIndex.ToString();
                lblCurrentPage2.Text = currentPageIndex2.ToString();

                cmbSituacionActual.SelectedIndex = 0;
                cmbSituacionActual2.SelectedIndex = 0;

                if (tabPageAgregarOposicion != null)
                {
                    EliminarTabPage(tabPageAgregarOposicion);
                    EliminarTabPage(tabPageReportes);
                }

                var filtrarExclusivasTask = FiltrarExclusivasPorSituacionActual();
                var filtrarNoExclusivasTask = FiltrarNoExclusivasPorSituacionActual();

                await Task.WhenAll(filtrarExclusivasTask, filtrarNoExclusivasTask);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar licencias de uso: " + ex.Message);
            }
            finally
            {

            }
        }


        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageOposicionesList)
            {
                //await CargarDatosLicenciasUsoAsync();
            }
            else if (tabControl1.SelectedTab == tabPageAgregarOposicion)
            {
                //await CargarDatosOposicion();
            }

        }
        /*
        private async Task CargarDatosOposicion()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                tabControl1.Visible = false;
                DataTable detallesOposicion = await Task.Run(() => oposicionModel.GetOposicionPorId(SeleccionarOposicion.idN));
                if (detallesOposicion.Rows.Count > 0)
                {
                    DataRow row = detallesOposicion.Rows[0];
                    SeleccionarOposicion.expediente = row["expediente"].ToString();
                    SeleccionarOposicion.signo_pretendido = row["signo_pretendido"].ToString();
                    SeleccionarOposicion.signo_distintivo = row["signo_distintivo"].ToString();
                    SeleccionarOposicion.clase = row["clase"].ToString();
                    SeleccionarOposicion.solicitante_signo_pretendido = row["solicitante_signo_pretendido"].ToString();
                    SeleccionarOposicion.opositor = row["opositor"].ToString();
                    SeleccionarOposicion.signo_opositor = row["signo_opositor"] is DBNull ? null : row["signo_opositor"].ToString();
                    SeleccionarOposicion.observaciones = row["observaciones"] is DBNull ? null : row["observaciones"].ToString();
                    SeleccionarOposicion.estado = row["estado"] is DBNull ? null : row["estado"].ToString();
                    SeleccionarOposicion.situacion_actual = row["situacion_actual"].ToString();
                    SeleccionarOposicion.logoOpositor = row["logo_opositor"] is DBNull ? null : (byte[])row["logo_opositor"];
                    SeleccionarOposicion.logoSignoPretendido = row["logo_signo_pretendido"] is DBNull ? null : (byte[])row["logo_signo_pretendido"];
                    SeleccionarOposicion.idMarca = row["idMarca"] is DBNull ? 0 : int.Parse(row["idMarca"].ToString());
                    SeleccionarOposicion.idSolicitante = 0;
                    //idSolicitante 

                    txtExpediente.Text = SeleccionarOposicion.expediente;
                    txtSigno.Text = SeleccionarOposicion.signo_pretendido;
                    //txtEstado.Text = SeleccionarOposicion.clase;
                    txtTitular.Text = SeleccionarOposicion.solicitante_signo_pretendido;
                    txtRegistro.Text = SeleccionarOposicion.opositor;
                    txtRazonSocial.Text = SeleccionarOposicion.signo_opositor;
                    //richtxtObservacionesAO.Text = SeleccionarOposicion.observaciones;
                    //txtEstadoAO.Text = SeleccionarOposicion.estado;

                    if (row["situacion_actual"].ToString().Trim().Equals("TERMINADA", StringComparison.OrdinalIgnoreCase))

                    {
                        btnEnviarATramite.Visible = false;
                        btnAgregarOpositorAO.Enabled = false;
                    }
                    else
                    {
                        btnEnviarATramite.Visible = true;
                        btnAgregarOpositorAO.Enabled = true;
                    }





                    if (SeleccionarOposicion.idMarca > 0)
                    {
                        btnAgregarOpositorAO.Enabled = false;
                        // btnTitular.Visible = true;
                        txtExpediente.Enabled = false;
                        txtSigno.Enabled = false;
                        txtRazonSocial.Enabled = true;
                        txtRegistro.Enabled = true;
                        var marca = marcaModel.GetMarcaNacionalById(SeleccionarOposicion.idMarca);
                        if (marca.Rows.Count > 0)
                        {
                            DataRow dataRow = marca.Rows[0];
                            SeleccionarOposicion.idSolicitante = dataRow["idTitular"] is DBNull ? 0 : int.Parse(dataRow["idTitular"].ToString());
                            if (SeleccionarOposicion.idSolicitante > 0)
                            {
                                txtTitular.Enabled = false;

                                var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarOposicion.idSolicitante));

                                await Task.WhenAll(titularTask);

                                var titular = titularTask.Result;

                                if (titular.Count > 0)
                                {
                                    txtTitular.Text = titular[0].nombre;
                                }
                            }
                            else
                            {
                                txtTitular.Enabled = true;
                            }
                        }
                    }
                    else if (SeleccionarOposicion.idMarca == 0)
                    {
                        txtSigno.Enabled = true;
                        txtExpediente.Enabled = true;
                        txtTitular.Enabled = true;
                        btnAgregarOpositorAO.Enabled = true;
                        txtRazonSocial.Enabled = true;
                        txtRegistro.Enabled = true;
                        //btnTitular.Visible = false;
                    }

                    //btnVerHistorial.Visible = true;
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
        */
        private void ProcesarSeleccion(DataGridView dataGridView)
        {
            var filaSeleccionada = dataGridView.SelectedRows[0];

            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
            {
                int? id = dataRowView["id"] as int?;

                if (id.HasValue)
                {
                    SeleccionarOposicion.idN = id.Value;
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
            if (dtgMarcasOp.RowCount <= 0 && dtgOpI.RowCount <= 0)
            {
                MostrarAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE");
                return;
            }

            // Verificar si se seleccionó en alguna de las tablas
            if (dtgMarcasOp.SelectedRows.Count > 0)
            {
                ProcesarSeleccion(dtgMarcasOp);
            }
            else if (dtgOpI.SelectedRows.Count > 0)
            {
                ProcesarSeleccion(dtgOpI);
            }
            else
            {
                MostrarAlerta("SELECCIONE UNA FILA", "MENSAJE");
            }
        }

        public async void Editar()
        {
            btnEnviarATerminar.Visible = true;
            VerificarSeleccionEdicion();
            if (SeleccionarOposicion.idN > 0)
            {
                //await CargarDatosOposicion();
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
                        if (dtgMarcasOp.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasOp.SelectedRows[0];
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
            txtExpediente.Enabled = true;
            txtTitular.Enabled = true;
            txtSigno.Enabled = true;
            txtRegistro.Enabled = true;
            txtRazonSocial.Enabled = true;
            btnAgregarOpositorAO.Enabled = true;
            SeleccionarOposicion.idN = 0;
            MostrarLogos();
            //iconPictureBoxIcono.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            tabControl1.SelectedTab = tabPageAgregarOposicion;
            btnGuardarU.Text = "AGREGAR";
            btnGuardarU.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnGuardarU.BackColor = Color.FromArgb(50, 164, 115);
        }

        private void checkBoxAgregarLogos_CheckedChanged(object sender, EventArgs e)
        {
            MostrarLogos();
        }

        private bool ValidarCamposLicenciaUso()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
                return false;
            if (SeleccionarMarcaParaLicencia.idMarca == 0)
                return false;
            if (string.IsNullOrWhiteSpace(txtTerritorio.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtDomicilio.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtNacionalidad.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtApoderadoRL.Text))
                return false;

            if (comboBoxEstado.SelectedItem == null)
                return false;

            if (dateTimePickerInicio.Value == DateTime.MinValue)
                return false;
            if (dateTimePickerFin.Value == DateTime.MinValue)
                return false;
            if (dateTimePickerInicio.Value > dateTimePickerFin.Value)
                return false;

            // Si todo está bien
            return true;
        }


        public void AgregarLicencia()
        {

            bool validacionExitosa = ValidarCamposLicenciaUso();
            if (!validacionExitosa)
            {
                FrmAlerta alerta = new FrmAlerta("COMPLETE TODOS LOS CAMPOS OBLIGATORIOS", "VALIDACIÓN FALLIDA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string territorio = txtTerritorio.Text;
            string razonSocial = txtRazonSocial.Text;
            string direccion = txtDireccion.Text;
            string domicilio = txtDomicilio.Text;
            string nacionalidad = txtNacionalidad.Text;
            string apoderado = txtApoderadoRL.Text;
            DateTime fechaInicio = dateTimePickerInicio.Value;
            DateTime fechaFin = dateTimePickerFin.Value;
            string tipo;

            if (exclusiva == true) { tipo = "exclusiva"; }
            else { tipo = "no Exclusiva"; }



            try
            {
                LicenciaUsoModel licenciaUso = new LicenciaUsoModel();
                if (licenciaUso.ExisteLicenciaUsoExclusiva((int)idMarca)==true)
                {
                    FrmAlerta alertaExiste = new FrmAlerta("YA EXISTE UNA LICENCIA DE USO EXCLUSIVA PARA ESTA MARCA.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alertaExiste.ShowDialog();
                    return;
                }
                else
                {
                    licenciaUso.InsertarLicenciaUso((int)idMarca, tituloPorElCualSeVerifica, tipo, fechaInicio, fechaFin, territorio,
                        razonSocial, direccion, domicilio, nacionalidad, apoderado, estado, "nacional");
                    FrmAlerta alerta = new FrmAlerta("LICENCIA DE USO AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                }

                    

                LimpiarFormularioLicencia();
                AnadirTabPage(tabPageOposicionesList);
                tabControl1.SelectedTab = tabPageOposicionesList;
                EliminarTabPage(tabPageAgregarOposicion);

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

            }

        }
        public void TerminarOposicion()
        {
            /*var cambio = oposicionModel.CambiarSituacionActualATerminada(SeleccionarOposicion.idN);
            if (cambio == true)
            {
                AnadirTabPage(tabPageOposicionesList);
                tabControl1.SelectedTab = tabPageOposicionesList;
                EliminarTabPage(tabPageAgregarOposicion);
                FrmAlerta alerta = new FrmAlerta("OPOSICIÓN TERMINADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
            }*/
        }

        private void btnEnviarATramite_Click(object sender, EventArgs e)
        {
            if (btnEnviarATerminar.Text == "TERMINAR")
            {
                if (SeleccionarOposicion.idMarca == 0)
                {
                    TerminarOposicion();
                }

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

        private void btnCancelarU_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageOposicionesList);
            EliminarTabPage(tabPageAgregarOposicion);
            tabControl1.SelectedTab = tabPageOposicionesList;
            LimpiarFormularioLicencia();
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
                        dtgOpI.DataSource = marcasR;
                        dtgOpI.Refresh();

                        if (dtgOpI.Columns["id"] != null)
                        {
                            dtgOpI.Columns["id"].Visible = false;
                            dtgOpI.Columns["IdMarca"].Visible = false;
                            dtgOpI.ClearSelection();
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
            dtgOpI.ClearSelection();
        }

        private void dtgOpI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgMarcasOp.ClearSelection();
        }

        private void dtgMarcasOp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgOpI.ClearSelection();
        }

        private void dtgOpI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgMarcasOp.ClearSelection();
        }

        private void btnIrAReportes_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageReportes);
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageOposicionesList);
        }
        public async void Filtrar()
        {
            string objeto = null;
            string expediente = null;
            string solicitante = null;
            string signo_pretendido = null;
            string signoDistintivo = null;
            string clase = null;
            string opositor = null;
            string signoOpositor = null;
            string estado = null;
            string situacionA = null;
            string tipoOposicion = null;
            numRegistros = 9;
            escala = 0.85f;

            titulo = "REPORTE DE OPOSICIONES NACIONALES ";

            if (chckTipoOpReporte.Checked)
            {
                switch (comboBoxTipoOposicion.SelectedIndex)
                {
                    case 0:
                        tipoOposicion = "recibida";
                        titulo += "RECIBIDAS";
                        break;
                    case 1:
                        tipoOposicion = "interpuesta";
                        titulo += "INTERPUESTAS";

                        break;
                    case 2:
                        tipoOposicion = null;
                        break;

                }
            }

            /*
            if (checkBoxEstadoReporte.Checked)
            { estado = comboBoxEstadoReporte.SelectedItem.ToString(); }
            else { estado = null; }

            if (chckExpedienteReporte.Checked)
            { expediente = txtExpedienteReporte.Text; }
            else
            { expediente = null; }

            if (chckSolicitanteReporte.Checked) { solicitante = richTextBoxSolicitanteReporte.Text; }
            else { solicitante = null; }

            if (chckSignoPretendidoRepo.Checked) { signo_pretendido = txtSignoPretendidoReporte.Text; }
            else { signo_pretendido = null; }

            if (chckSignoDistintivoReporte.Checked) { signoDistintivo = cmbSignoDistintivoReporte.SelectedItem.ToString(); }
            else { signoDistintivo = null; }

            if (chckClaseReporte.Checked) { clase = txtClaseReporte.Text; }
            else { clase = null; }

            if (chckOpositorReporte.Checked) { opositor = richTextBoxOpositorReporte.Text; }
            else { opositor = null; }

            if (chckSignoOpositorReporte.Checked) { signoOpositor = txtTituloReporte.Text; }
            else { signoOpositor = null; }

            if (chckSituacionActualReporte.Checked) { situacionA = cmbSituacionActualReporte.SelectedItem.ToString(); }
            else { situacionA = null; }

            if (chckTipoOpReporte.Checked) { tipoOposicion = tipoOposicion; }
            else { tipoOposicion = null; }
            
            /*
            dtgReportesOp.DataSource = oposicionModel.FiltrarOposiciones("op_nacionales", expediente, solicitante, signo_pretendido,
                signoDistintivo, clase, opositor, signoOpositor, estado, situacionA, "nacional", tipoOposicion);
            dtgReportesOp.ClearSelection();*/

        }

        public void LimpiarReportes()
        {
            //checkBoxEstadoReporte.Checked = false;
            //comboBoxEstadoReporte.SelectedIndex = -1;
            chckTipoOpReporte.Checked = false;
            comboBoxTipoOposicion.SelectedIndex = -1;
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
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Google\\Chrome\\Application\\chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google\\Chrome\\Application\\chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\Application\\chrome.exe")
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
                            tableContent += $"<td {alignStyle}>{row[column]}</td>";

                            object cellValue = row[column];
                            if (cellValue is DateTime dateValue)
                            {
                                cellValue = dateValue.ToString("dd/MM/yyyy"); // Cambia el formato según necesites
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
                    <img src='https://bergerpemueller.com/wp-content/uploads/2024/02/LogoBPA-e1709094810910.jpg' />
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
                    Properties.Resources.logoBPA.Save(tempLogoPath);

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
                richTextBoxSolicitanteReporte.Text = SeleccionarPersonaReportes.nombreTitular;
            }
            else
            {
                richTextBoxSolicitanteReporte.Text = "";
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
            dateTimePickerFin.Value = dateTimePickerInicio.Value.AddDays(-1);
        }

        private void comboBoxEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnGuardarU.Text == "AGREGAR")
            {
                btnAdjuntarT.Enabled = true;
            }
            else
            {

            }
        }
    }
}
