using Comun.Cache;
using Dominio;
using Microsoft.VisualBasic.Logging;
using Mysqlx.Datatypes;
using Presentacion.Alertas;
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
using System.Windows.Controls;
using System.Windows.Forms;
using ClosedXML.Excel;
using Presentacion.Marcas_Internacionales;


namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmMostrarOposiciones : Form
    {
        OposicionModel oposicionModel = new OposicionModel();
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento;
        public int numRegistros = 0;
        public float escala = 0;
        string titulo;

        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;

        private const int pageSize2 = 20;
        private int currentPageIndex2 = 1;
        private int totalPages2 = 0;
        private int totalRows2 = 0;
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }

        public FrmMostrarOposiciones()
        {
            InitializeComponent();

            txtNombreTitularAO.Enabled = true;
            txtSignoOpositor.Enabled = true;


            this.Load += FrmMostrarOposiciones_Load;
            SeleccionarMarca.idInt = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            if (UsuarioActivo.isAdmin == false)
            {
                btnAgregarTitular.Enabled = false;
                btnAgregarAgente.Enabled = false;
                btnEditarEstadoHistorial.Visible = false;
                txtExpediente.Enabled = false;
                txtClase.Enabled = false;
                txtNombre.Enabled = false;
                datePickerFechaSolicitud.Enabled = false;
                comboBoxTipoSigno.Enabled = false;
                comboBoxSignoDistintivo.Enabled = false;
                btnSubirImagen.Enabled = false;
                btnQuitarImagen.Enabled = false;
                dateTimePickerFechaH.Enabled = false;
                comboBoxEstatusH.Enabled = false;
                richTextBoxAnotacionesH.Enabled = false;
                btnEditarH.Visible = false;
                lblUser.Visible = false;
                labelUserEditor.Visible = false;
            }
            else if (UsuarioActivo.isAdmin == true)
            {
                btnAgregarTitular.Enabled = true;
                btnAgregarAgente.Enabled = true;
                btnEditarEstadoHistorial.Visible = true;
                txtExpediente.Enabled = true;
                txtClase.Enabled = true;
                txtNombre.Enabled = true;
                datePickerFechaSolicitud.Enabled = true;
                comboBoxTipoSigno.Enabled = true;
                comboBoxSignoDistintivo.Enabled = true;
                btnSubirImagen.Enabled = true;
                btnQuitarImagen.Enabled = true;
                dateTimePickerFechaH.Enabled = true;
                comboBoxEstatusH.Enabled = true;
                richTextBoxAnotacionesH.Enabled = true;
                btnEditarH.Visible = true;
                lblUser.Visible = false;
                labelUserEditor.Visible = false;
            }
        }

        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }
        private async Task LoadMarcasInterpuestas(string situacionActual)
        {
            totalRows2 = oposicionModel.GetTotalOposicionesInteracionalesInterpuestas(situacionActual);
            totalPages2 = (int)Math.Ceiling((double)totalRows2 / pageSize2);
            var marcasN = await Task.Run(() => oposicionModel.GetAllOposicionesInternacionalesInterpuestas(situacionActual, currentPageIndex2, pageSize2));

            Invoke(new Action(() =>
            {
                lblTotalPages2.Text = totalPages2.ToString();
                lblTotalRows2.Text = totalRows2.ToString();
                dtgOpI.DataSource = marcasN;
                dtgOpI.Refresh();
                // Oculta la columna 'id'
                if (dtgOpI.Columns["id"] != null)
                {
                    dtgOpI.Columns["IdMarca"].Visible = false;
                    dtgOpI.Columns["id"].Visible = false;
                    // Desactiva la selección automática de la primera fila
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

        private async Task LoadMarcas(string situacionActual)
        {
            
            totalRows = oposicionModel.GetTotalOposicionesInternacionalesRecibidas(situacionActual);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var marcasN = await Task.Run(() => oposicionModel.GetAllOposicionesInternacionalesRecibidas(situacionActual, currentPageIndex, pageSize));

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
        public async void filtrarRecibidas()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = oposicionModel.GetFilteredMarcasInternacionalesRecibidasCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = oposicionModel.FiltrarOposicionesInternacionalesRecibidas(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgMarcasOp.DataSource = titulares;
                    if (dtgMarcasOp.Columns["id"] != null)
                    {
                        dtgMarcasOp.Columns["id"].Visible = false;
                    }
                    dtgMarcasOp.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN OPOSICIONES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await FiltrarPorSituacionActual();
                }
            }
            else
            {
                //await LoadMarcas();
                await FiltrarPorSituacionActual();
            }
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
        public void mostrarPanelRegistro(string isRegistrada)
        {
            if (isRegistrada == "si")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                panel3.Visible = true;
                tableLayoutPanel2.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel2.RowStyles[0].Height = 64.69f;
                tableLayoutPanel2.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel2.RowStyles[1].Height = 35.31f;
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel3.Visible = false;
                tableLayoutPanel2.RowStyles[0].Height = 0;
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
                //MessageBox.Show(mensaje.ToUpper(), "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            pictureBox1.Image = documento;
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
            comboBoxTipoSigno.SelectedIndex = -1;
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            checkBoxTienePoder.Checked = false;
            convertirImagen();
            pictureBoxOpositor.Image = documento;
            pictureBoxSignoPretendido.Image = documento;
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
                        SeleccionarMarca.idPersonaTitular = Convert.ToInt32(row["idTitular"]);
                        SeleccionarMarca.idPersonaAgente = Convert.ToInt32(row["idAgente"]);
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
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
                            txtDireccionTitular.Text = titular[0].direccion;
                            txtEntidadTitular.Text = titular[0].pais;
                        }

                        if (agente.Count > 0)
                        {
                            txtNombreAgente.Text = agente[0].nombre;
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

        private void MostrarAlerta(string mensaje, string titulo)
        {
            FrmAlerta alerta = new FrmAlerta(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            alerta.ShowDialog();
        }

        private void ProcesarSeleccion(DataGridView dataGridView)
        {
            var filaSeleccionada = dataGridView.SelectedRows[0];

            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
            {
                int? id = dataRowView["id"] as int?;

                if (id.HasValue)
                {
                    SeleccionarOposicion.idInt = id.Value;
                    AnadirTabPage(tabPageAgregarOposicion);
                }
                else
                {
                    MostrarAlerta("ID NO VÁLIDO", "ERROR");
                }
            }
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

                        if (row["estado"] != DBNull.Value && row["observaciones"] != DBNull.Value)
                        {
                            // Actualizar los controles 
                            SeleccionarMarca.estado = row["estado"].ToString();
                            SeleccionarMarca.observaciones = row["observaciones"].ToString();
                            textBoxEstatus.Text = row["estado"].ToString();
                            richTextBox1.Text = row["observaciones"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Verificar si "observaciones" contiene la palabra "registrada"
                        bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("Registrada", StringComparison.OrdinalIgnoreCase);

                        if (contieneRegistrada)
                        {
                            mostrarPanelRegistro("si");
                        }
                        else
                        {
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


        private async void loadHistorialOposicion()
        {
            try
            {
                HistorialOposicionModel historialOposicionModel = new HistorialOposicionModel();
                var historial = await Task.Run(() => historialOposicionModel.ObtenerHistorial(SeleccionarOposicion.idInt));


                Invoke(new Action(() =>
                {
                    dtgHistorialOp.AutoGenerateColumns = true;
                    dtgHistorialOp.DataSource = historial;

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
        private async void loadHistorialById()
        {
            try
            {
                var historial = await Task.Run(() => historialModel.GetHistorialMarcaById(SeleccionarOposicion.idMarca));

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
            convertirImagen();
            pictureBox1.Image = documento;
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
                        mostrarPanelRegistro("si");
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        mostrarPanelRegistro("no");
                    }
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
                txtDireccionTitular.Text = SeleccionarPersona.direccion;
                txtEntidadTitular.Text = SeleccionarPersona.pais;
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

        private async Task CargarDatosOposicion()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                tabControl1.Visible = false;
                DataTable detallesOposicion = await Task.Run(() => oposicionModel.GetOposicionPorId(SeleccionarOposicion.idInt));
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
                        btnAgregarEstadoAO.Enabled = false;
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
                        MostrarLogos();
                    }


                    if (SeleccionarOposicion.idMarca > 0)
                    {
                        txtSignoOpositor.Enabled = true;
                        txtNombreTitularAO.Enabled = true;
                        btnAgregarOpositorAO.Enabled = false;
                        // btnTitular.Visible = true;
                        txtExpedienteAO.Enabled = false;
                        txtClaseAO.Enabled = false;
                        cmbSignoDAO.Enabled = false;
                        txtClaseAO.Enabled = false;
                        txtSignoAO.Enabled = false;
                        var marca = marcaModel.GetMarcaInternacionalById(SeleccionarOposicion.idMarca);
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
                        cmbSignoDAO.Enabled = true;
                        txtSignoOpositor.Enabled = true;
                        txtNombreTitularAO.Enabled = true;
                        txtSolicitanteSignoPretendido.Enabled = true;
                        btnAgregarOpositorAO.Enabled = true;
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

        public async void Editar()
        {
            VerificarSeleccionEdicion();
            if (SeleccionarOposicion.idInt > 0)
            {
                await CargarDatosOposicion();
            }
        }
        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private async void FrmMostrarOposiciones_Load(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            Cursor = Cursors.WaitCursor;
            cmbSituacionActual.SelectedIndex = 0;
            cmbSituacionActualI.SelectedIndex = 0;
            tabControl1.SelectedTab = tabPageListaMarcas;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetalle);
            EliminarTabPage(tabPageAgregarOposicion);
            EliminarTabPage(tabPageReportes);
            ActualizarFechaVencimiento();
            await FiltrarPorSituacionActual();
            await FiltrarPorSituacionActualInterpuestas();
            tabControl1.Visible = true;
            Cursor = Cursors.Default;

            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();

            currentPageIndex2 = 1;
            lblCurrentPage2.Text = currentPageIndex2.ToString();
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {

                EliminarTabPage(tabPageHistorialDetalle);
                EliminarTabPage(tabPageReportes);
            }
            else if (tabControl1.SelectedTab == tabPageListaMarcas)
            {
                tabControl1.Visible = false;
                Cursor cursor = Cursors.WaitCursor;
               
                await FiltrarPorSituacionActual();
                await FiltrarPorSituacionActualInterpuestas();
                SeleccionarOposicion.idInt = 0;
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetalle);
                EliminarTabPage(tabPageAgregarOposicion);
                EliminarTabPage(tabPageReportes);
                tabControl1.Visible = true;
                Cursor cursor1 = Cursors.Default;
            }
            else if (tabControl1.SelectedTab == tabPageAgregarOposicion)
            {
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetalle);
                EliminarTabPage(tabPageReportes);
            }
            else if (tabControl1.SelectedTab == tabPageReportes)
            {
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetalle);
                EliminarTabPage(tabPageAgregarOposicion);
                dtgReportesOp.DataSource = null;
                dtgReportesOp.ClearSelection();
            }
        }
        public void ActualizarMarcaInternacional()
        {

            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string signoDistintivo = comboBoxSignoDistintivo.SelectedItem?.ToString();
            string tipoSigno = comboBoxTipoSigno.SelectedItem?.ToString();
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[] logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;
            string paisRegistro = comboBox1.SelectedItem?.ToString();
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

            // Validaciones
            if (idTitular <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN TITULAR VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un titular válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idAgente <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN AGENTE VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un agente válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            // Editar la marca
            try
            {
                bool esActualizado;

                // Verificar si la marca está registrada
                if (registroChek)
                {
                    esActualizado = marcaModel.EditMarcaInternacionalRegistrada(
                        SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idTitular, idAgente, solicitud,
                        paisRegistro, tiene_poder, null, registro, folio, libro, fecha_registro, fecha_vencimiento, null, null);
                }
                else
                {
                    esActualizado = marcaModel.EditMarcaInternacional(SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idTitular, idAgente, solicitud,
                        paisRegistro, tiene_poder, null);
                }

                DataTable marcaActualizada = marcaModel.GetMarcaNacionalById(SeleccionarMarca.idInt);

                if (esActualizado)
                {
                    // Verificar si la actualización fue exitosa
                    if (esActualizado)
                    {
                        // Verificar si las observaciones ya contienen el estado actual
                        if (marcaActualizada.Rows.Count > 0 && marcaActualizada.Rows[0]["Observaciones"].ToString().Contains(estado))
                        {
                            FrmAlerta alerta = new FrmAlerta("MARCA INTERNACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            //MessageBox.Show("Marca nacional actualizada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SeleccionarMarca.idInt = 0;
                            tabControl1.SelectedTab = tabPageListaMarcas;
                        }
                        else
                        {
                            // Guardar la nueva etapa en el historial
                            historialModel.GuardarEtapa(SeleccionarMarca.idInt, AgregarEtapa.fecha.Value, estado, AgregarEtapa.anotaciones, AgregarEtapa.usuario, "TRÁMITE");
                            FrmAlerta alerta = new FrmAlerta("MARCA INTERNACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            //MessageBox.Show("Marca nacional actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SeleccionarMarca.idInt = 0;
                            tabControl1.SelectedTab = tabPageListaMarcas;
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

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                FrmAlerta alerta = new FrmAlerta("ERROR AL " + (registroChek ? "REGISTRAR" : "ACTUALIZAR") + ": \n" + ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                //MessageBox.Show("Error al " + (registroChek ? "registrar" : "actualizar") + " la marca internacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarFormulario();
            }
        }

        public void Habilitar()
        {
            dateTimePickerFechaH.Enabled = true;
            comboBoxEstatusH.Enabled = true;
            richTextBoxAnotacionesH.Enabled = true;
            btnEditarH.Enabled = true;
        }
        public void Deshabilitar()
        {
            dateTimePickerFechaH.Enabled = false;
            comboBoxEstatusH.Enabled = false;
            richTextBoxAnotacionesH.Enabled = true;
            richTextBoxAnotacionesH.ReadOnly = true;
            btnEditarH.Enabled = false;
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

        private void roundedButton6_Click(object sender, EventArgs e)
        {
            loadHistorialById();
            AnadirTabPage(tabPageHistorialMarca);
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

        }

        private void tabPageListaMarcas_Click(object sender, EventArgs e)
        {

        }
        public void EditarHistorial()
        {
            if (dtgHistorialOp.SelectedRows.Count > 0)
            {
                Habilitar();
                var filaSeleccionada = dtgHistorialOp.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    if (SeleccionarOposicion.idMarca > 0)
                    {
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

                            AnadirTabPage(tabPageHistorialDetalle);
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron detalles del historial", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (SeleccionarOposicion.idMarca == 0)
                    {
                        HistorialOposicionModel historialOposicionModel = new HistorialOposicionModel();
                        DataTable historial = historialOposicionModel.ObtenerHistorialPorId(id);

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

                            AnadirTabPage(tabPageHistorialDetalle);
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron detalles del historial", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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
        private void iconButton5_Click_1(object sender, EventArgs e)
        {
            EditarHistorial();
        }

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
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
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UNA ESTADO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                ///MessageBox.Show("No ha seleccionado ningun estado");
            }
        }

        public void CancelarEdicionHistorial()
        {
            tabControl1.SelectedTab = tabPageHistorialMarca;
        }
        private void btnCancelarH_Click(object sender, EventArgs e)
        {

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
                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", fechaAbandono.ToShortDateString() + " Abandono " + justificacion, usuarioAbandono, "TRÁMITE");
                                FrmAlerta alerta = new FrmAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await FiltrarPorSituacionActual();
                            }
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA MARCA PARA ABANDONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
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

        private void ibtnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            if (dtgHistorialOp.SelectedRows.Count > 0)
            {
                Deshabilitar();
                var filaSeleccionada = dtgHistorialOp.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorial.id = id;

                    DataTable historial = historialModel.GetHistorialById(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];

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

                        AnadirTabPage(tabPageHistorialDetalle);
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
                //MessageBox.Show("Por favor, seleccione una fila del historial.");
            }
        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageAgregarOposicion;
        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {
            if (textBoxEstatus.Text != "Registrada")
            {
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                tabControl1.SelectedTab = tabPageListaMarcas;
            }
            else
            {
                if (!ValidarCampo(txtFolio.Text, "Por favor, ingrese el número de folio. No es posible salir sin ingresar datos de registro, a menos que edite esa etapa") ||
                    !ValidarCampo(txtRegistro.Text, "Por favor, ingrese el número de registro. No es posible salir sin ingresar datos de registro , a menos que edite esa etapa") ||
                    !ValidarCampo(txtLibro.Text, "Por favor, ingrese el número de tomo. No es posible salir sin ingresar datos de registro, a menos que edite esa etapa")
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
                        //MessageBox.Show("El registro, folio y tomo deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        ActualizarMarcaInternacional();
                        EliminarTabPage(tabPageMarcaDetail);
                        EliminarTabPage(tabPageHistorialMarca);
                        tabControl1.SelectedTab = tabPageListaMarcas;
                    }

                }

            }
        }

        private void btnActualizarM_Click(object sender, EventArgs e)
        {
            ActualizarMarcaInternacional();
            EliminarTabPage(tabPageHistorialMarca);
        }

        private async void roundedButton5_Click(object sender, EventArgs e)
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

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            btnEnviarATramite.Visible = false;
            AnadirTabPage(tabPageAgregarOposicion);
            txtSignoOpositor.Enabled = true;
            txtNombreTitularAO.Enabled = true;
            txtSolicitanteSignoPretendido.Enabled = true;
            txtSignoAO.Enabled = true;
            txtExpedienteAO.Enabled = true;
            txtClaseAO.Enabled = true;
            cmbSignoDAO.Enabled = true;

            //btnVerHistorial.Visible = false;
            SeleccionarOposicion.idInt = 0;
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

        private void btnAgregarTitularAO_Click(object sender, EventArgs e)
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

        private void dtgMarcasO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }

        private void dtgHistorialOp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarHistorial();
        }

        private async void btnEditarH_Click_1(object sender, EventArgs e)
        {
            //Editar historial por id
            string etapa = comboBoxEstatusH.SelectedItem?.ToString();
            DateTime fecha = dateTimePickerFechaH.Value;
            string anotaciones = richTextBoxAnotacionesH.Text;
            SeleccionarHistorial.anotaciones = anotaciones;
            string usuario = lblUser.Text;
            string usuarioEditor = labelUserEditor.Text;
            bool actualizar = false;

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

                if (SeleccionarOposicion.idMarca > 0)
                {
                    actualizar = historialModel.EditHistorialById(SeleccionarHistorial.id, etapa, fecha, AgregarEtapa.anotaciones, usuario, usuarioEditor);

                }
                else if (SeleccionarOposicion.idMarca == 0)
                {
                    HistorialOposicionModel historialOposicionModel = new HistorialOposicionModel();
                    actualizar = historialOposicionModel.EditarHistorialOposicion(SeleccionarHistorial.id, etapa, fecha, AgregarEtapa.anotaciones, usuario, usuarioEditor);
                }

                if (actualizar == true)
                {

                    if (SeleccionarOposicion.idMarca > 0)
                    {
                        loadHistorialById();
                    }
                    else if (SeleccionarOposicion.idMarca == 0)
                    {
                        loadHistorialOposicion();
                    }

                    FrmAlerta alerta = new FrmAlerta("ESTADO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    //MessageBox.Show("Estado actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SeleccionarHistorial.id = 0;
                    await recargarDatosOposicion();

                    tabControl1.SelectedTab = tabPageHistorialMarca;
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

        private void btnCancelarH_Click_1(object sender, EventArgs e)
        {
            CancelarEdicionHistorial();
        }

        private void dateTimePickerFechaH_ValueChanged_1(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void comboBoxEstatusH_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void btnCancelarU_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageListaMarcas;
            LimpiarFormularioOposicion();
        }

        private async void ibtnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                DataTable marcas = marcaModel.FiltrarMarcasNacionalesEnOposicion(txtBuscar.Text);
                if (marcas.Rows.Count > 0)
                {
                    dtgMarcasOp.DataSource = marcas;

                    if (dtgMarcasOp.Columns["id"] != null)
                    {
                        dtgMarcasOp.Columns["id"].Visible = false;
                    }
                    dtgMarcasOp.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN MARCAS RECIBIDAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    await FiltrarPorSituacionActual();
                    //LoadMarcas();
                }

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO EXISTEN MARCAS RECIBIDAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                await FiltrarPorSituacionActual();
                //LoadMarcas();
            }
        }

        public async Task recargarDatosOposicion()
        {
            DataTable detallesOposicion = await Task.Run(() => oposicionModel.GetOposicionPorId(SeleccionarOposicion.idInt));
            if (detallesOposicion.Rows.Count > 0)
            {
                DataRow row = detallesOposicion.Rows[0];

                SeleccionarOposicion.observaciones = row["observaciones"] is DBNull ? null : row["observaciones"].ToString();
                SeleccionarOposicion.estado = row["estado"] is DBNull ? null : row["estado"].ToString();

                richtxtObservacionesAO.Text = SeleccionarOposicion.observaciones;
                //txtEstadoAO.Text = SeleccionarOposicion.estado;


            }
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
                    actualizado = oposicionModel.EditarOposicion(SeleccionarOposicion.idInt, expediente, signo_pretendido, signoDistintivo, clase,
                    solicitante_signo_distintivo, null, signoOpositor, situacion_actual, IdMarca, logoOpositor, logoSignoPretendido, opositor,
                    SeleccionarOposicion.idSolicitante);
                }
                else
                {
                    actualizado = actualizado = oposicionModel.EditarOposicion(SeleccionarOposicion.idInt, expediente, signo_pretendido, signoDistintivo, clase,
                    solicitante_signo_distintivo, null, signoOpositor, situacion_actual, IdMarca, logoOpositor, logoSignoPretendido, opositor,
                    null);
                }


                if (actualizado)
                {
                    FrmAlerta alerta = new FrmAlerta("OPOSICIÓN ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    tabControl1.SelectedTab = tabPageListaMarcas;
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

            btnAgregarEstadoAO.Enabled = true;
            btnEnviarATramite.Visible = false;


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
                        logoOpositor, logoSignoPretendido, "internacional", "interpuesta");
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
                    tabControl1.SelectedTab = tabPageListaMarcas;
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

        private void roundedButton2_Click_1(object sender, EventArgs e)
        {

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
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            MostrarLogos();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTipoSigno_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void btnAgregarSignoPretendido_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBoxSignoPretendido.Image = new Bitmap(openFile.FileName);
            }
        }

        private void btnQuitarLogoOpositor_Click(object sender, EventArgs e)
        {
            convertirImagen();
            pictureBoxOpositor.Image = documento;
        }

        private void btnQuitarLogoSignoPretendido_Click(object sender, EventArgs e)
        {
            convertirImagen();
            pictureBoxSignoPretendido.Image = documento;
        }

        private void dtgMarcasO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void roundedButton2_Click_2(object sender, EventArgs e)
        {
            if (SeleccionarOposicion.idMarca > 0)
            {
                loadHistorialById();
                AnadirTabPage(tabPageHistorialMarca);
            }
            else if (SeleccionarOposicion.idMarca == 0)
            {
                AnadirTabPage(tabPageHistorialMarca);
                loadHistorialOposicion();

            }
        }

        private void btnTitular_Click(object sender, EventArgs e)
        {
            FrmMostrarTitulares frmMostrarTitulares = new FrmMostrarTitulares();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersona.idPersonaT != 0)
            {
                SeleccionarOposicion.idSolicitante = SeleccionarPersona.idPersonaT;
                SeleccionarOposicion.solicitante_signo_pretendido = SeleccionarPersona.nombre;
                txtSolicitanteSignoPretendido.Text = SeleccionarPersona.nombre;
                txtSolicitanteSignoPretendido.Enabled = false;
            }
        }

        public async Task FiltrarPorSituacionActual()
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

        public async Task FiltrarPorSituacionActualInterpuestas()
        {
            if (cmbSituacionActualI.SelectedIndex == 0)
            {
                await LoadMarcasInterpuestas("EN TRÁMITE");
            }
            else if (cmbSituacionActualI.SelectedIndex == 1)
            {
                await LoadMarcasInterpuestas("TERMINADA");
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private async void cmbSituacionActual_SelectedIndexChanged(object sender, EventArgs e)
        {
            await FiltrarPorSituacionActual();
        }

        public void TerminarOposicion()
        {
            var cambio = oposicionModel.CambiarSituacionActualATerminada(SeleccionarOposicion.idInt);
            if (cambio == true)
            {
                tabControl1.SelectedTab = tabPageListaMarcas;
                FrmAlerta alerta = new FrmAlerta("OPOSICIÓN TERMINADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
            }
        }

        public void EnviarATramite()
        {

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
            else if (btnEnviarATramite.Text == "ENVIAR A TRÁMITE")
            {
                EnviarATramite();
            }
        }

        private async void filtrarMarcas()
        {
            string valor = txtBuscar.Text;
            string situacion = cmbSituacionActual.SelectedItem.ToString();
            if (valor != "")
            {
                var marcasR = await Task.Run(() => marcaModel.FiltrarMarcasInternacionalesEnOposicion(valor, currentPageIndex, pageSize));
                if (marcasR.Rows.Count > 0)
                {
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
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN OPOSICIONES RECIBIDAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    await LoadMarcas(situacion);
                }

            }
            else
            {

                await LoadMarcas(cmbSituacionActual.SelectedItem.ToString());
            }

        }

        private async void filtrarMarcasInterpuestas()
        {
            string valor = txtBuscar2.Text;
            string situacion = cmbSituacionActualI.SelectedItem.ToString();
            if (valor != "")
            {
                var marcasR = await Task.Run(() => marcaModel.FiltrarMarcasInternacionalesInterpuestasEnOposicion(valor));
                if (marcasR.Rows.Count > 0)
                {
                    Invoke(new Action(() =>
                    {
                        dtgOpI.DataSource = marcasR;
                        dtgOpI.Refresh();

                        if (dtgOpI.Columns["id"] != null)
                        {
                            dtgOpI.Columns["id"].Visible = false;
                            dtgOpI.ClearSelection();
                        }
                    }));
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN OPOSICIONES INTERPUESTAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    await LoadMarcasInterpuestas(situacion);
                }

            }
            else
            {
                await LoadMarcasInterpuestas(situacion);
            }

        }

        private void ibtnBuscar_Click_1(object sender, EventArgs e)
        {
            filtrarRecibidas();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrarRecibidas();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            txtBuscar2.Text = "";
            filtrarMarcasInterpuestas();
        }

        private void ibtnBuscar2_Click(object sender, EventArgs e)
        {
            filtrarMarcasInterpuestas();
        }

        private void cmbSituacionActual_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            filtrarMarcas();
        }

        private void cmbSituacionActualI_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrarMarcasInterpuestas();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filtrarRecibidas();
            }
        }

        private void txtBuscar2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filtrarMarcasInterpuestas();
            }
        }

        private void dtgOpI_DoubleClick(object sender, EventArgs e)
        {
            Editar();
        }

        public async void Filtrar()
        {

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

            titulo = "REPORTE DE OPOSICIONES INTERNACIONALES ";

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

            if (chckSignoOpositorReporte.Checked) { signoOpositor = txtSignoOpositorReporte.Text; }
            else { signoOpositor = null; }

            if (chckSituacionActualReporte.Checked) { situacionA = cmbSituacionActualReporte.SelectedItem.ToString(); }
            else { situacionA = null; }

            if (chckTipoOpReporte.Checked) { tipoOposicion = tipoOposicion; }
            else { tipoOposicion = null; }


            dtgReportesOp.DataSource = oposicionModel.FiltrarOposiciones("op_internacionales", expediente, solicitante, signo_pretendido,
                signoDistintivo, clase, opositor, signoOpositor, estado, situacionA, "internacional", tipoOposicion);
            dtgReportesOp.ClearSelection();



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
            // Ruta al ejecutable de Chrome en tu sistema
            string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"; // Cambia la ruta según tu instalación

            string nombre = titulo + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");
            // Abre un SaveFileDialog para que el usuario seleccione la ruta de guardado
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = nombre + ".pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lanza el navegador Chrome
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,  // Ejecutar en modo headless (sin interfaz gráfica)
                    ExecutablePath = chromePath // Usa Google Chrome en lugar de Chromium
                });

                // Crea una nueva página
                var page = await browser.NewPageAsync();

                // Límite de registros por página (12 registros por página)
                int registrosPorPagina = registrosPagina;
                int totalPaginas = (int)Math.Ceiling((double)dt.Rows.Count / registrosPorPagina);

                // Crear el contenido HTML completo para todo el PDF
                string fullHtmlContent = "";

                // Recorrer las páginas y generar el contenido HTML para cada una
                for (int pagina = 0; pagina < totalPaginas; pagina++)
                {
                    // Crear el contenido de la tabla para la página actual
                    string tableContent = "";
                    int startRecord = pagina * registrosPorPagina;
                    int endRecord = Math.Min((pagina + 1) * registrosPorPagina, dt.Rows.Count);

                    // Crear las filas para la tabla
                    for (int i = startRecord; i < endRecord; i++)
                    {
                        DataRow row = dt.Rows[i];
                        tableContent += "<tr>";
                        foreach (DataColumn column in dt.Columns)
                        {
                            string alignStyle = (column.ColumnName == "REGISTRO" || column.ColumnName == "FOLIO" || column.ColumnName == "TOMO" || column.ColumnName == "CLASE")
                                ? "style='padding: 8px; text-align: right; border: 1px solid #ddd;'"
                                : (column.ColumnName == "TIPO_OPOSICION" || column.ColumnName == "SITUACION_ACTUAL"
                                    ? "style='padding: 8px; text-align: center; border: 1px solid #ddd;'"
                                    : "style='padding: 8px; text-align: left; border: 1px solid #ddd;'");

                            tableContent += $"<td {alignStyle}>{row[column]}</td>";
                        }
                        tableContent += "</tr>";
                    }

                    // Generar los encabezados de la tabla
                    string headers = "";
                    foreach (DataColumn column in dt.Columns)
                    {
                        headers += $"<th style='padding: 8px; text-align: left; border: 1px solid #ddd; background-color: #f2f2f2; font-weight: bold;'>{column.ColumnName}</th>";
                    }

                    // HTML con el logo y el título "Reportes" en el header
                    fullHtmlContent += $@"
                     <html>
                         <head>
                             <style>
                                 body {{
                                     font-family: Arial, sans-serif;
                                 }}
                                 table {{ border-collapse: collapse; width: 100%; }}
                                 th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
                                 th {{ background-color: #f2f2f2; font-weight: bold; }}
                                 img {{
                                     width: 200px;
                                     height: auto;
                                 }}
                                 @page {{
                                     size: legal landscape;
                                     margin: 20mm;
                                 }}
                                 table {{
                                     page-break-inside: auto;
                                 }}
                                 tr {{
                                     page-break-inside: avoid;
                                 }}
                                 td {{
                                     page-break-before: auto;
                                 }}
                                 .footer {{
                                     text-align: center;
                                     position: fixed;
                                     bottom: 10mm;
                                     left: 0;
                                     right: 0;
                                     font-size: 10px;
                                 }}
                                 .header {{
                                     text-align: center;
                                     font-size: 20px;
                                     font-weight: bold;
                                     margin-bottom: 10px;
                                 }}
                             </style>
                         </head>
                         <body>
                             <div class='header'>
                                 {titulo}
                             </div>
                             <div class='fecha'>
                                 <center>
                                     Fecha: {DateTime.Now.ToString("dd-MM-yyyy HH:mm")}
                                 </center>
                             </div>
                             <img src='https://bergerpemueller.com/wp-content/uploads/2024/02/LogoBPA-e1709094810910.jpg' />
                             <table>
                                 <thead>
                                     <tr>
                                         {headers}
                                     </tr>
                                 </thead>
                                 <tbody>
                                     {tableContent}
                                 </tbody>
                             </table>
                             {(pagina < totalPaginas - 1 ? "<div style='page-break-before: always;'></div>" : "")}
                         </body>
                     </html>";
                }


                // Establecer el contenido HTML completo para el PDF
                await page.SetContentAsync(fullHtmlContent);

                // Ruta para guardar el PDF
                string pdfFilePath = saveFileDialog.FileName;

                // Generar el PDF para todo el contenido
                await page.PdfAsync(pdfFilePath, new PdfOptions
                {
                    Format = PaperFormat.Legal,      // Tamaño oficio
                    PrintBackground = true,         // Incluir fondo
                    Landscape = true,               // Orientación horizontal
                    Scale = (Decimal)escalas           // Reducir la escala para ajustar mejor
                });

                // Cerrar el navegador
                await browser.CloseAsync();

                // Confirmar que el PDF se ha generado
                FrmAlerta alerta = new FrmAlerta("PDF GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se seleccionó ninguna ruta para guardar el PDF.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        var worksheet = workbook.Worksheets.Add(titulo);
                        // Fecha actual en el formato deseado
                        string fecha = DateTime.Now.ToString("dd-MM-yyyy-HH-mm");

                        // Insertar el título "Próximos vencimientos" en la celda A1
                        worksheet.Cell(3, 5).Value = titulo;
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Style.Font.Underline = XLFontUnderlineValues.Single;
                        worksheet.Cell(3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;  // Centrar el título

                        // Insertar la fecha debajo del título (en la celda A2)
                        worksheet.Cell(4, 5).Value = "Fecha: " + fecha;
                        worksheet.Cell(4, 5).Style.Font.Italic = true;
                        worksheet.Cell(4, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;  // Centrar la fecha

                        worksheet.Column(1).AdjustToContents();
                        // Agregar logo antes de la tabla
                        if (System.IO.File.Exists(tempLogoPath))
                        {
                            var image = worksheet.AddPicture(tempLogoPath)
                                .MoveTo(worksheet.Cell(3, 1)) // Posición del logo
                                .Scale(0.5); // Ajustar tamaño
                        }

                        // Insertar tabla después del logo
                        int startRow = 10; // Ajustar según el espacio requerido
                        worksheet.Cell(startRow, 1).InsertTable(dataTable);

                        // Ajustar ancho de las columnas
                        worksheet.Columns().AdjustToContents();

                        // Guardar archivo
                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    // Eliminar archivo temporal
                    if (System.IO.File.Exists(tempLogoPath))
                        System.IO.File.Delete(tempLogoPath);

                    FrmAlerta alerta = new FrmAlerta("ARCHIVO GENERADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el archivo: {ex.Message}");
                }
            }
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

        private void iconButton9_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageListaMarcas);
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

        private void btnOpositorReporte_Click(object sender, EventArgs e)
        {
            FrmMostrarOpositoresReportes frmMostrarOpositores = new FrmMostrarOpositoresReportes();
            frmMostrarOpositores.ShowDialog();

            if (SeleccionarPersonaReportes.nombreTitular != "")
            {
                richTextBoxOpositorReporte.Text = SeleccionarPersonaReportes.nombreTitular;
            }
            else
            {
                richTextBoxOpositorReporte.Text = "";
            }
        }

        private void btnIrAReportes_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageReportes);
        }

        private async void btnFirst_Click(object sender, EventArgs e)
        {
            currentPageIndex = 1;
            if (txtBuscar.Text != "")
            {
                filtrarRecibidas();
            }
            else
            {
                await FiltrarPorSituacionActual();
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
                    filtrarRecibidas();
                }
                else
                {
                    await FiltrarPorSituacionActual();
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
                    filtrarRecibidas();
                }
                else
                {
                    await FiltrarPorSituacionActual();
                }

                lblCurrentPage.Text = currentPageIndex.ToString();
            }
        }

        private async void btnLast_Click(object sender, EventArgs e)
        {
            currentPageIndex = totalPages;
            if (txtBuscar.Text != "")
            {
                filtrarRecibidas();
            }
            else
            {
                await FiltrarPorSituacionActual();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnFirst2_Click(object sender, EventArgs e)
        {
            currentPageIndex2 = 1;
            if (txtBuscar2.Text != "")
            {
                filtrarMarcasInterpuestas();
            }
            else
            {
                await FiltrarPorSituacionActualInterpuestas();
            }

            lblCurrentPage2.Text = currentPageIndex2.ToString();
        }

        private async void btnPrev2_Click(object sender, EventArgs e)
        {
            if (currentPageIndex2 > 1)
            {
                currentPageIndex2--;
                if (txtBuscar2.Text != "")
                {
                    filtrarMarcasInterpuestas();
                }
                else
                {
                    await FiltrarPorSituacionActualInterpuestas();
                }

                lblCurrentPage2.Text = currentPageIndex2.ToString();
            }
        }

        private async void btnNext2_Click(object sender, EventArgs e)
        {
            if (currentPageIndex2 < totalPages2)
            {
                currentPageIndex2++;
                if (txtBuscar2.Text != "")
                {
                    filtrarMarcasInterpuestas();
                }
                else
                {
                    await FiltrarPorSituacionActualInterpuestas();
                }

                lblCurrentPage2.Text = currentPageIndex2.ToString();
            }
        }

        private async void btnLast2_Click(object sender, EventArgs e)
        {
            currentPageIndex2 = totalPages2;
            if (txtBuscar2.Text != "")
            {
                filtrarMarcasInterpuestas();
            }
            else
            {
                await FiltrarPorSituacionActualInterpuestas();
            }

            lblCurrentPage2.Text = currentPageIndex2.ToString();
        }
    }
}
