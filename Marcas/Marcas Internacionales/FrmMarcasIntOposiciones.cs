using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using Presentacion.Marcas_Nacionales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMarcasIntOposiciones : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        OposicionModel oposicionModel = new OposicionModel();
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento;
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }
        public FrmMarcasIntOposiciones()
        {
            InitializeComponent();
            
            this.Load += FrmMarcasIntOposiciones_Load;
            SeleccionarMarca.idInt = 0;
            //ActualizarFechaVencimiento();
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }

        private void MostrarMarcasIngresadas()
        {
            dtgMarcasOp.DataSource = marcaModel.GetAllMarcasInternacionalesEnOposicion();
            if (dtgMarcasOp.Columns["id"] != null)
            {
                dtgMarcasOp.Columns["id"].Visible = false;

                dtgMarcasOp.ClearSelection();
            }
        }
        private async Task LoadMarcas(string situacionActual)
        {

            var marcasN = await Task.Run(() => oposicionModel.GetAllOposicionesNacionales(situacionActual));

            Invoke(new Action(() =>
            {
                dtgMarcasOp.DataSource = marcasN;
                dtgMarcasOp.Refresh();
                // Oculta la columna 'id'
                if (dtgMarcasOp.Columns["id"] != null)
                {
                    dtgMarcasOp.Columns["IdMarca"].Visible = false;
                    dtgMarcasOp.Columns["id"].Visible = false;
                    // Desactiva la selección automática de la primera fila
                    dtgMarcasOp.ClearSelection();
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
                convertirImagen();
                pictureBox1.Image = documento;
            }
        }

        public void MostrarLogoEnPictureBoxOpositor(byte[] logo)
        {
            if (logo != null && logo.Length > 0)
            {
                using (var ms = new MemoryStream(logo))
                {
                    var img = System.Drawing.Image.FromStream(ms);
                    pictureBoxOpositor.Image = img;
                }
            }
            else
            {
                convertirImagen();
                pictureBoxOpositor.Image = documento;
            }
        }

        public void MostrarLogoEnPictureBoxSignoPretendido(byte[] logo)
        {
            if (logo != null && logo.Length > 0)
            {
                using (var ms = new MemoryStream(logo))
                {
                    pictureBoxSignoPretendido.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            else
            {
                convertirImagen();
                pictureBoxSignoPretendido.Image = documento;
            }
        }

        public void mostrarPanelRegistro()
        {
            if (textBoxEstatus.Text == "Registrada")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel3.Visible = true;
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel3.Visible = false;
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

        private bool ValidarCampos(string expediente, string signo_pretendido, string signo_distintivo, string clase, string solicitante, string opositor,
      bool logos, ref byte[] logoOpositor, ref byte[] logoSignoPretendido, string signo_opositor)
        {


            // Verificar campos obligatorios
            if (!ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
                !ValidarCampo(signo_pretendido, "Por favor, ingrese el signo pretendido.") ||
                !ValidarCampo(signo_distintivo, "Por favor, seleccione el signo distintivo.") ||
                !ValidarCampo(clase, "Por favor, ingrese la clase.") ||
                !ValidarCampo(solicitante, "Por favor, ingrese el solicitante del signo pretendido.") ||
                !ValidarCampo(opositor, "Por favor, ingrese el opositor.") ||
                !ValidarCampo(signo_opositor, "Por favor, ingrese el signo opositor."))
            {
                return false;
            }

            if (logos == true)
            {
                // Verificar que hay una imagen
                if (pictureBoxOpositor.Image != null && pictureBoxOpositor.Image != documento
                    && pictureBoxSignoPretendido.Image != null && pictureBoxSignoPretendido.Image != documento)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        pictureBoxOpositor.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        logoOpositor = ms.ToArray();

                    }


                    using (var ms2 = new System.IO.MemoryStream())
                    {
                        pictureBoxSignoPretendido.Image.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
                        logoSignoPretendido = ms2.ToArray();
                    }

                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("INGRESE EL LOGO DEL OPOSITOR Y DEL SIGNO PRETENDIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return false;
                }
            }
            else
            {

            }

            return true;
        }

        private bool ValidarCamposEdicion(string expediente, string signo_pretendido, string signo_distintivo, string clase, string solicitante, string opositor,
     bool logos, ref byte[] logoOpositor, ref byte[] logoSignoPretendido, string signo_opositor)
        {


            // Verificar campos obligatorios
            if (!ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
                !ValidarCampo(signo_pretendido, "Por favor, ingrese el signo pretendido.") ||
                !ValidarCampo(signo_distintivo, "Por favor, seleccione el signo distintivo.") ||
                !ValidarCampo(clase, "Por favor, ingrese la clase.") ||
                !ValidarCampo(solicitante, "Por favor, ingrese el solicitante del signo pretendido.") ||
                !ValidarCampo(opositor, "Por favor, ingrese el opositor.") ||
                !ValidarCampo(signo_opositor, "Por favor, ingrese el signo opositor."))
            {
                return false;
            }

            if (logos == true)
            {
                if (pictureBoxOpositor.Image != null && pictureBoxOpositor.Image != documento)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        pictureBoxOpositor.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        logoOpositor = ms.ToArray();
                    }
                }
                else
                {
                    logoOpositor = null;/*
                    FrmAlerta alerta = new FrmAlerta("INGRESE EL LOGO DEL OPOSITOR ", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return false;*/
                }


                if (pictureBoxSignoPretendido.Image != null && pictureBoxSignoPretendido.Image != documento)
                {
                    using (var ms2 = new System.IO.MemoryStream())
                    {
                        pictureBoxSignoPretendido.Image.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
                        logoSignoPretendido = ms2.ToArray();
                    }
                }
                else
                {
                    logoSignoPretendido = null;
                    FrmAlerta alerta = new FrmAlerta("INGRESE EL LOGO DEL SIGNO PRETENDIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return false;
                }

            }
            else
            {

            }

            return true;
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
            txtNombreAgente.Text = "";
            txtNombreCliente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            comboBox1.SelectedIndex = -1;
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            AgregarEtapa.LimpiarEtapa();
        }

        private async void CargarDatosMarca()
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
                        SeleccionarMarca.idPersonaTitular = row["idTitular"] != DBNull.Value ? Convert.ToInt32(row["idTitular"]) : 0;
                        SeleccionarMarca.idPersonaAgente = row["idAgente"] != DBNull.Value ? Convert.ToInt32(row["idAgente"]) : 0;
                        SeleccionarMarca.idPersonaCliente = row["idCliente"] != DBNull.Value ? Convert.ToInt32(row["idCliente"]) : 0;
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
                        SeleccionarMarca.tiene_poder = row["tiene_poder"].ToString();
                        SeleccionarMarca.pais_de_registro = row["pais_de_registro"].ToString();

                        // Cargar datos del titular y agente 
                        var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                        var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));

                        var clienteTask = SeleccionarMarca.idPersonaCliente != null
                            ? Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaCliente))
                            : null;

                        await Task.WhenAll(titularTask, agenteTask, clienteTask);

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

                        // Actualizar los controles 
                        txtExpediente.Text = SeleccionarMarca.expediente;
                        txtNombre.Text = SeleccionarMarca.nombre;
                        txtClase.Text = SeleccionarMarca.clase;
                        textBoxEstatus.Text = SeleccionarMarca.estado;
                        comboBoxSignoDistintivo.SelectedItem = SeleccionarMarca.signoDistintivo;
                        comboBoxTipoSigno.SelectedItem = SeleccionarMarca.tipoSigno;
                        MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                        datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud;
                        richTextBox1.Text = SeleccionarMarca.observaciones;
                        int index = comboBox1.FindString(SeleccionarMarca.pais_de_registro);
                        comboBox1.SelectedIndex = index;

                        checkBoxTienePoder.Checked = SeleccionarMarca.tiene_poder.Equals("si", StringComparison.OrdinalIgnoreCase);


                        bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("registrada", StringComparison.OrdinalIgnoreCase);

                        if (contieneRegistrada)
                        {
                            mostrarPanelRegistro();
                        }
                        else
                        {
                            mostrarPanelRegistro();
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
            if (dtgMarcasOp.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasOp.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasOp.SelectedRows[0];
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
            }
        }

        private async void refrescarMarca()
        {
            if (SeleccionarMarca.idInt > 0)
            {
                try
                {
                    DataTable detallesMarcaInt = await Task.Run(() => marcaModel.GetMarcaInternacionalById(SeleccionarMarca.idInt));

                    if (detallesMarcaInt.Rows.Count > 0)
                    {
                        DataRow row = detallesMarcaInt.Rows[0];

                        if (row["estado"] != DBNull.Value && row["Observaciones"] != DBNull.Value)
                        {
                            // Actualizar los controles 
                            textBoxEstatus.Text = row["estado"].ToString();
                            richTextBox1.Text = row["Observaciones"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Verificar si "observaciones" contiene la palabra "registrada"
                        bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("registrada", StringComparison.OrdinalIgnoreCase);

                        if (contieneRegistrada)
                        {
                            mostrarPanelRegistro();
                        }
                        else
                        {
                            mostrarPanelRegistro();
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
                var historial = await Task.Run(() => historialModel.GetHistorialMarcaById(SeleccionarMarca.idInt));

                // Invoca el método para actualizar el DataGridView en el hilo principal
                Invoke(new Action(() =>
                {
                    dtgHistorialOp.AutoGenerateColumns = true;
                    dtgHistorialOp.DataSource = historial;
                    dtgHistorialOp.Refresh();

                    if (dtgHistorialOp.Columns["id"] != null)
                    {
                        dtgHistorialOp.Columns["id"].Visible = false;
                    }

                    dtgHistorialOp.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmMarcasIntOposiciones_Load(object sender, EventArgs e)
        {
            cmbSituacionActual.SelectedIndex = 0;
            FiltrarPorSituacionActual();
            SeleccionarMarca.idInt = 0;
            tabControl1.SelectedTab = tabPageOposicionesList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {

                EliminarTabPage(tabPageHistorialMarca);
            }
            else if (tabControl1.SelectedTab == tabPageOposicionesList)
            {
                FiltrarPorSituacionActual();
                SeleccionarOposicion.idN = 0;
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageAgregarOposicion);
            }
            else if (tabControl1.SelectedTab == tabPageAgregarOposicion)
            {
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
            }
        }
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

                    txtExpedienteAO.Text = SeleccionarOposicion.expediente;
                    txtSignoAO.Text = SeleccionarOposicion.signo_pretendido;
                    cmbSignoDAO.SelectedItem = SeleccionarOposicion.signo_distintivo;
                    txtClaseAO.Text = SeleccionarOposicion.clase;
                    txtSolicitanteSignoPretendido.Text = SeleccionarOposicion.solicitante_signo_pretendido;
                    txtNombreTitularAO.Text = SeleccionarOposicion.opositor;
                    txtSignoOpositor.Text = SeleccionarOposicion.signo_opositor;
                    richtxtObservacionesAO.Text = SeleccionarOposicion.observaciones;
                    //txtEstadoAO.Text = SeleccionarOposicion.estado;

                    if (SeleccionarOposicion.situacion_actual == "TERMINADA")
                    {
                        btnEnviarATramite.Visible = false;
                        btnAgregarEstadoAO.Enabled = false;
                        btnAgregarOpositorAO.Enabled = false;
                    }
                    else
                    {
                        btnEnviarATramite.Visible = true;
                        btnAgregarEstadoAO.Enabled = true;
                        btnAgregarOpositorAO.Enabled = true;
                    }


                    if (SeleccionarOposicion.logoSignoPretendido != null || SeleccionarOposicion.logoOpositor != null)
                    {
                        checkBoxAgregarLogos.Checked = true;
                        MostrarLogos();
                        if (SeleccionarOposicion.logoOpositor != null)
                        {
                            MostrarLogoEnPictureBoxOpositor((byte[])row["logo_opositor"]);
                        }
                        if (SeleccionarOposicion.logoSignoPretendido != null)
                        {
                            MostrarLogoEnPictureBoxSignoPretendido((byte[])row["logo_signo_pretendido"]);
                        }

                    }
                    else
                    {
                        checkBoxAgregarLogos.Checked = false;
                    }


                    if (SeleccionarOposicion.idMarca > 0)
                    {

                        btnAgregarOpositorAO.Enabled = false;
                        // btnTitular.Visible = true;
                        txtExpedienteAO.Enabled = false;
                        txtClaseAO.Enabled = false;
                        cmbSignoDAO.Enabled = false;
                        txtSignoAO.Enabled = false;
                        txtSignoOpositor.Enabled = true;
                        txtNombreTitularAO.Enabled = true;
                        var marca = marcaModel.GetMarcaNacionalById(SeleccionarOposicion.idMarca);
                        if (marca.Rows.Count > 0)
                        {
                            DataRow dataRow = marca.Rows[0];
                            SeleccionarOposicion.idSolicitante = dataRow["idTitular"] is DBNull ? 0 : int.Parse(dataRow["idTitular"].ToString());
                            if (SeleccionarOposicion.idSolicitante > 0)
                            {
                                txtSolicitanteSignoPretendido.Enabled = false;

                                var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarOposicion.idSolicitante));

                                await Task.WhenAll(titularTask);

                                var titular = titularTask.Result;

                                if (titular.Count > 0)
                                {
                                    txtSolicitanteSignoPretendido.Text = titular[0].nombre;
                                }
                            }
                            else
                            {
                                txtSolicitanteSignoPretendido.Enabled = true;
                            }
                        }
                    }
                    else if (SeleccionarOposicion.idMarca == 0)
                    {
                        txtSignoAO.Enabled = true;
                        txtClaseAO.Enabled = true;
                        txtExpedienteAO.Enabled = true;
                        txtClaseAO.Enabled = true;
                        cmbSignoDAO.Enabled = true;
                        txtClaseAO.Enabled = true;
                        txtSolicitanteSignoPretendido.Enabled = true;
                        btnAgregarOpositorAO.Enabled = true;
                        txtSignoOpositor.Enabled = true;
                        txtNombreTitularAO.Enabled = true;
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
        private void VerificarSeleccionEdicion()
        {
            if (dtgMarcasOp.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasOp.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasOp.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    if (dataRowView["id"] != DBNull.Value)
                    {
                        int id = Convert.ToInt32(dataRowView["id"]);
                        SeleccionarOposicion.idN = id;
                    }

                    AnadirTabPage(tabPageAgregarOposicion);
                    tabControl1.SelectedTab = tabPageAgregarOposicion;
                    //tabControl1.SelectedTab = tabPageMarcaDetail;
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA FILA", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async void Editar()
        {
            btnEnviarATramite.Visible = true;
            VerificarSeleccionEdicion();
            if (SeleccionarOposicion.idN > 0)
            {
                await CargarDatosOposicion();
            }
        }
        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void iconButton3_Click(object sender, EventArgs e)
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
                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", justificacion, usuarioAbandono, "TRÁMITE");
                                FrmAlerta alerta = new FrmAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MostrarMarcasIngresadas();
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
            FrmAgregarEtapa frmAgregarEtapa = new FrmAgregarEtapa();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                try
                {
                    historialModel.GuardarEtapa(SeleccionarMarca.idInt, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE");
                    FrmAlerta alerta = new FrmAlerta("ESTADO AGREGADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show("Etapa agregada con éxito");
                    if (AgregarEtapa.etapa == "Registrada")
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                    mostrarPanelRegistro();
                    refrescarMarca();

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

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            FrmMostrarClientes frmMostrarClientes = new FrmMostrarClientes();
            frmMostrarClientes.ShowDialog();

            if (SeleccionarPersona.idPersonaC != 0)
            {
                txtNombreCliente.Text = SeleccionarPersona.nombre;

            }
        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {
            loadHistorialById();
            AnadirTabPage(tabPageHistorialMarca);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            if (dtgHistorialOp.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialOp.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    DataTable historial = historialModel.GetHistorialById(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];
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

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (dtgHistorialOp.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorialOp.SelectedRows[0];
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
                        refrescarMarca();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila para eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEditarH_Click(object sender, EventArgs e)
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
                    refrescarMarca();
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
                MessageBox.Show("No ha seleccionado ningun estado");
            }
        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageHistorialMarca;
        }

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void comboBoxEstatusH_SelectedValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void btnActualizarM_Click(object sender, EventArgs e)
        {
            //ActualizarMarcaInternacional();
            EliminarTabPage(tabPageHistorialMarca);
        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {
            if (textBoxEstatus.Text != "Registrada")
            {
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                tabControl1.SelectedTab = tabPageOposicionesList;
            }
            else
            {
                if (!ValidarCampo(txtFolio.Text, "Por favor, ingrese el número de folio.\n No es posible salir sin ingresar datos de registro,\n a menos que edite ese estado") ||
                    !ValidarCampo(txtRegistro.Text, "Por favor, ingrese el número de registro.\n No es posible salir sin ingresar datos de registro,\n a menos que edite ese estado") ||
                    !ValidarCampo(txtLibro.Text, "Por favor, ingrese el número de tomo.\n No es posible salir sin ingresar datos de registro,\n a menos que edite ese estado")
                    )
                {

                }
                else
                {
                    if (
                        (!int.TryParse(txtRegistro.Text, out _)) ||
                        (!int.TryParse(txtFolio.Text, out _)) ||
                        (!int.TryParse(txtLibro.Text, out _)))
                    {
                        FrmAlerta alerta = new FrmAlerta("EL REGISTRO, FOLIO Y TOMO\nDEBEN SER VALORES NUMÉRICOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        alerta.ShowDialog();
                        //MessageBox.Show("El registro, folio y libro deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        //ActualizarMarcaInternacional();
                        EliminarTabPage(tabPageHistorialMarca);
                        EliminarTabPage(tabPageMarcaDetail);
                    }

                }

            }
        }

        private void dtgMarcasOp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }
        public async void FiltrarPorSituacionActual()
        {
            if (cmbSituacionActual.SelectedIndex == 0)
            {
                await LoadMarcas("EN TRÁMITE");
            }
            else if (cmbSituacionActual.SelectedIndex == 1)
            {
                await LoadMarcas("TERMINADA");
            }
        }
        private void cmbSituacionActual_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarPorSituacionActual();
        }
        public void MostrarLogos()
        {
            if (checkBoxAgregarLogos.Checked)
            {
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 79.82f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 20.18f;
            }
            else
            {
                tableLayoutPanel1.RowStyles[0].Height = 0;
            }
        }
        private void iconButton6_Click(object sender, EventArgs e)
        {
            btnEnviarATramite.Visible = false;
            AnadirTabPage(tabPageAgregarOposicion);
            txtNombreTitularAO.Enabled = true;
            txtSignoOpositor.Enabled = true;
            //btnVerHistorial.Visible = false;
            SeleccionarOposicion.idN = 0;
            //btnTitular.Visible = false;
            btnAgregarOpositorAO.Enabled = true;
            convertirImagen();
            pictureBoxOpositor.Image = documento;
            pictureBoxSignoPretendido.Image = documento;
            checkBoxAgregarLogos.Checked = false;
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

        public void EditarOposicion()
        {
            byte[] logoOpositor = null;
            byte[] logoSignoPretendido = null;
            string expediente = txtExpedienteAO.Text;
            string signo_pretendido = txtSignoAO.Text;
            string signoDistintivo = cmbSignoDAO.SelectedItem?.ToString();
            string clase = txtClaseAO.Text;
            string solicitante_signo_distintivo = txtSolicitanteSignoPretendido.Text;
            string situacion_actual = SeleccionarOposicion.situacion_actual;
            int idMarca = SeleccionarOposicion.idMarca;
            int? IdMarca = null;
            string opositor = txtNombreTitularAO.Text;
            string signoOpositor = txtSignoOpositor.Text;

            bool validacionExitosa = ValidarCamposEdicion(expediente, signo_pretendido, signoDistintivo, clase, solicitante_signo_distintivo,
                opositor, checkBoxAgregarLogos.Checked, ref logoOpositor, ref logoSignoPretendido,
                signoOpositor);

            if (!validacionExitosa)
            {
                return;
            }

            if (SeleccionarOposicion.idMarca != 0)
            {
                IdMarca = SeleccionarOposicion.idMarca;
            }
            else
            {
                IdMarca = null;
            }



            try
            {
                bool actualizado = false;

                if (SeleccionarOposicion.idSolicitante != 0)
                {
                    actualizado = oposicionModel.EditarOposicion(SeleccionarOposicion.idN, expediente, signo_pretendido, signoDistintivo, clase,
                    solicitante_signo_distintivo, null, signoOpositor, situacion_actual, IdMarca, logoOpositor, logoSignoPretendido, opositor,
                    SeleccionarOposicion.idSolicitante);
                }
                else
                {
                    actualizado = actualizado = oposicionModel.EditarOposicion(SeleccionarOposicion.idN, expediente, signo_pretendido, signoDistintivo, clase,
                    solicitante_signo_distintivo, null, signoOpositor, situacion_actual, IdMarca, logoOpositor, logoSignoPretendido, opositor,
                    null);
                }


                if (actualizado)
                {
                    FrmAlerta alerta = new FrmAlerta("OPOSICIÓN ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    tabControl1.SelectedTab = tabPageOposicionesList;
                }

            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }
        }
        public void AgregarOposicion()
        {
            byte[] logoOpositor = null;
            byte[] logoSignoPretendido = null;
            string expediente = txtExpedienteAO.Text;
            string signo_pretendido = txtSignoAO.Text;
            string signoDistintivo = cmbSignoDAO.SelectedItem?.ToString();
            string clase = txtClaseAO.Text;
            string solicitante_signo_distintivo = txtSolicitanteSignoPretendido.Text;

            int? idMarca = null;
            string opositor = txtNombreTitularAO.Text;
            string signoOpositor = txtSignoOpositor.Text;




            bool validacionExitosa = ValidarCampos(expediente, signo_pretendido, signoDistintivo, clase, solicitante_signo_distintivo,
                opositor, checkBoxAgregarLogos.Checked, ref logoOpositor, ref logoSignoPretendido,
                signoOpositor);

            if (!validacionExitosa)
            {
                return;
            }

            try
            {
                if (AgregarEtapaOposicion.etapa != "")
                {
                    OposicionModel oposicionModel = new OposicionModel();
                    int idOposicion = oposicionModel.CrearOposicion(expediente, signo_pretendido, signoDistintivo, clase,
                        solicitante_signo_distintivo, null, null, opositor, signoOpositor, "EN TRÁMITE", idMarca,
                        logoOpositor, logoSignoPretendido, "nacional");
                    if (idOposicion > 0)
                    {
                        HistorialOposicionModel historialOposicionModel = new HistorialOposicionModel();
                        historialOposicionModel.CrearHistorialOposicion((DateTime)AgregarEtapaOposicion.fecha, AgregarEtapaOposicion.etapa,
                            AgregarEtapaOposicion.anotaciones, AgregarEtapaOposicion.usuario, null, "OPOSICIÓN", idOposicion
                            );
                    }
                    FrmAlerta alerta = new FrmAlerta("OPOSICIÓN AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    LimpiarFormularioOposicion();
                    tabControl1.SelectedTab = tabPageOposicionesList;
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("DEBE AGREGAR UN ESTADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                }

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
                AgregarOposicion();
            }
            else if (btnGuardarU.Text == "EDITAR")
            {
                //editar
                EditarOposicion();
            }

        }
        public void TerminarOposicion()
        {
            var cambio = oposicionModel.CambiarSituacionActualATerminada(SeleccionarOposicion.idN);
            if (cambio == true)
            {
                tabControl1.SelectedTab = tabPageOposicionesList;
                FrmAlerta alerta = new FrmAlerta("OPOSICIÓN TERMINADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
            }
        }

        private void btnEnviarATramite_Click(object sender, EventArgs e)
        {
            if (btnEnviarATramite.Text == "TERMINAR")
            {
                if (SeleccionarOposicion.idMarca == 0)
                {
                    TerminarOposicion();
                }
                else
                {
                    FrmAgregarEtapa frmAgregarEtapa = new FrmAgregarEtapa();
                    frmAgregarEtapa.ShowDialog();


                    if (AgregarEtapa.etapa != "")
                    {
                        try
                        {
                            historialModel.GuardarEtapa(SeleccionarOposicion.idMarca, (DateTime)AgregarEtapa.fecha,
                            AgregarEtapa.etapa, AgregarEtapa.anotaciones,
                            AgregarEtapa.usuario, "TRÁMITE");
                            TerminarOposicion();

                            //MessageBox.Show("Etapa agregada con éxito");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        FrmAlerta alerta = new FrmAlerta("NO SE ENVIÓ A TERMINADA PORQUE NO SELECCIONÓ UN ESTADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }
                }

            }
        }
        public void LimpiarFormularioOposicion()
        {
            txtNombreTitularAO.Text = "";
            txtSignoAO.Text = "";
            cmbSignoDAO.SelectedIndex = -1;
            richtxtObservacionesAO.Text = "";
            txtSignoOpositor.Text = "";
            txtClaseAO.Text = "";
            txtSignoOpositor.Text = "";
            txtSolicitanteSignoPretendido.Text = "";
            //txtEstadoAO.Text = "";
            txtExpedienteAO.Text = "";

            pictureBoxOpositor.Image = documento;
            pictureBoxSignoPretendido.Image = documento;

        }

        private void btnCancelarU_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageOposicionesList;
            LimpiarFormularioOposicion();
        }
        public async Task recargarDatosOposicion()
        {
            DataTable detallesOposicion = await Task.Run(() => oposicionModel.GetOposicionPorId(SeleccionarOposicion.idN));
            if (detallesOposicion.Rows.Count > 0)
            {
                DataRow row = detallesOposicion.Rows[0];

                SeleccionarOposicion.observaciones = row["observaciones"] is DBNull ? null : row["observaciones"].ToString();
                SeleccionarOposicion.estado = row["estado"] is DBNull ? null : row["estado"].ToString();

                richtxtObservacionesAO.Text = SeleccionarOposicion.observaciones;
                //txtEstadoAO.Text = SeleccionarOposicion.estado;


            }
        }

        private async void btnAgregarEstadoAO_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaOposicion frmAgregarEtapa = new FrmAgregarEtapaOposicion();
            frmAgregarEtapa.ShowDialog();


            if (btnGuardarU.Text == "EDITAR")
            {
                if (SeleccionarOposicion.idMarca == 0)
                {
                    if (AgregarEtapaOposicion.etapa != "")
                    {
                        try
                        {
                            HistorialOposicionModel historialOposicionModel = new HistorialOposicionModel();
                            historialOposicionModel.CrearHistorialOposicion((DateTime)AgregarEtapaOposicion.fecha, AgregarEtapaOposicion.etapa,
                                AgregarEtapaOposicion.anotaciones, AgregarEtapaOposicion.usuario, null, "OPOSICIÓN", SeleccionarOposicion.idN
                                );

                            FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();

                            await recargarDatosOposicion();
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
                            historialModel.GuardarEtapa(SeleccionarOposicion.idMarca, (DateTime)AgregarEtapaOposicion.fecha,
                            AgregarEtapaOposicion.etapa, AgregarEtapaOposicion.anotaciones,
                            AgregarEtapaOposicion.usuario, "OPOSICIÓN");
                            FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            await recargarDatosOposicion();
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
                    richtxtObservacionesAO.Text = AgregarEtapaOposicion.anotaciones;

                }
                else
                {
                    //txtEstadoAO.Text = "";
                    richtxtObservacionesAO.Text = "";
                }
            }
        }

        private void btnAgregarLogoOpositor_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBoxOpositor.Image = new Bitmap(openFile.FileName);
            }
        }

        private void btnQuitarLogoOpositor_Click(object sender, EventArgs e)
        {
            convertirImagen();
            pictureBoxOpositor.Image = documento;
        }

        private void btnAgregarSignoPretendido_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBoxSignoPretendido.Image = new Bitmap(openFile.FileName);
            }
        }

        private void btnQuitarLogoSignoPretendido_Click(object sender, EventArgs e)
        {
            convertirImagen();
            pictureBoxSignoPretendido.Image = documento;
        }

        private void btnAgregarOpositorAO_Click(object sender, EventArgs e)
        {

            FrmMostrarMarcas frmMostrarMarcas = new FrmMostrarMarcas();
            frmMostrarMarcas.ShowDialog();

            if (SeleccionarMarcaOposicion.idMarca != 0)
            {
                txtNombreTitularAO.Enabled = false;
                txtSignoOpositor.Enabled = false;
                txtNombreTitularAO.Text = SeleccionarMarcaOposicion.nombreTitular;
                txtSignoOpositor.Text = SeleccionarMarcaOposicion.nombreSigno;
            }
            else
            {
                txtNombreTitularAO.Enabled = true;
                txtSignoOpositor.Enabled = true;
                txtNombreTitularAO.Text = "";
                txtSignoOpositor.Text = "";
            }
        }

        private async void filtrarMarcas()
        {
            string valor = txtBuscar.Text;
            string situacion = cmbSituacionActual.SelectedItem.ToString();
            if (valor != "" && situacion != null)
            {
                var marcasR = await Task.Run(() => marcaModel.FiltrarMarcasNacionalesEnOposicion(valor, situacion));

                Invoke(new Action(() =>
                {
                    dtgMarcasOp.DataSource = marcasR;
                    dtgMarcasOp.Refresh();

                    if (dtgMarcasOp.Columns["id"] != null)
                    {
                        dtgMarcasOp.Columns["id"].Visible = false;
                        dtgMarcasOp.ClearSelection();
                    }
                }));
            }
            else
            {
                await LoadMarcas(cmbSituacionActual.SelectedItem.ToString());
            }

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {
            filtrarMarcas();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrarMarcas();

        }

        private void btnAgregarOpositorAO_Click_1(object sender, EventArgs e)
        {
            FrmMostrarMarcasN frmMostrarMarcas = new FrmMostrarMarcasN();
            frmMostrarMarcas.ShowDialog();

            if (SeleccionarMarcaOposicion.idMarca != 0)
            {
                txtNombreTitularAO.Enabled = false;
                txtSignoOpositor.Enabled = false;
                txtNombreTitularAO.Text = SeleccionarMarcaOposicion.nombreTitular;
                txtSignoOpositor.Text = SeleccionarMarcaOposicion.nombreSigno;
            }
            else
            {
                txtNombreTitularAO.Enabled = true;
                txtSignoOpositor.Enabled = true;
                txtNombreTitularAO.Text = "";
                txtSignoOpositor.Text = "";
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
                            HistorialOposicionModel historialOposicionModel = new HistorialOposicionModel();
                            historialOposicionModel.CrearHistorialOposicion((DateTime)AgregarEtapaOposicion.fecha, AgregarEtapaOposicion.etapa,
                                AgregarEtapaOposicion.anotaciones, AgregarEtapaOposicion.usuario, null, "OPOSICIÓN", SeleccionarOposicion.idInt
                                );

                            FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();

                            await recargarDatosOposicion();
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
                            historialModel.GuardarEtapa(SeleccionarOposicion.idMarca, (DateTime)AgregarEtapaOposicion.fecha,
                            AgregarEtapaOposicion.etapa, AgregarEtapaOposicion.anotaciones,
                            AgregarEtapaOposicion.usuario, "OPOSICIÓN");
                            FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            await recargarDatosOposicion();
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
                    richtxtObservacionesAO.Text = AgregarEtapaOposicion.anotaciones;

                }
                else
                {
                    //txtEstadoAO.Text = "";
                    richtxtObservacionesAO.Text = "";
                }
            }
        }
    }
}
