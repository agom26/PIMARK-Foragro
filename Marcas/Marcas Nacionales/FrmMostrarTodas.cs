using Comun.Cache;
using Dominio;
using Presentacion.Alertas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmMostrarTodas : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        byte[] defaultImage = Properties.Resources.logoImage;
        System.Drawing.Image documento;
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }
        public FrmMostrarTodas()
        {
            InitializeComponent();
            int x = (panel17.Size.Width - label30.Size.Width - iconPictureBox3.Size.Width) / 2;
            int y = (panel17.Size.Height - label30.Size.Height) / 2;
            panel18.Location = new Point(x, y);

            int x2 = (panel15.Size.Width - label29.Size.Width) / 2;
            int y2 = (panel15.Size.Height - label29.Size.Height) / 2;
            panel16.Location = new Point(x2, y2);
            iconPictureBox3.IconSize = 25;
            this.Load += FrmMostrarTodas_Load;
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
                btnVerH.Visible = true;
                labelUserEditor.Visible = false;
                lblUser.Visible = false;
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
                btnEditarH.Enabled = true;
                btnEditarEstadoHistorial.Visible = true;
                btnEditarEstadoHistorial.Enabled = true;
                btnVerH.Visible = false;
                btnEditarEstadoHistorial.Location = btnVerH.Location;
                labelUserEditor.Visible = false;
                lblUser.Visible = false;
            }
        }

        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }

        private void MostrarMarcasTramite()
        {
            dtgMarcasN.DataSource = marcaModel.GetAllMarcasNacionalesEnTramite();

            if (dtgMarcasN.Columns["id"] != null)
            {
                dtgMarcasN.Columns["id"].Visible = false;

                dtgMarcasN.ClearSelection();
            }
        }
        private async void LoadMarcas()
        {
            var marcasN = await Task.Run(() => marcaModel.GetAllMarcasInternacionalesIngresadas());


            Invoke(new Action(() =>
            {
                dtgMarcasN.DataSource = marcasN;
                dtgMarcasN.Refresh();
                // Oculta la columna 'id'
                if (dtgMarcasN.Columns["id"] != null)
                {
                    dtgMarcasN.Columns["id"].Visible = false;
                    // Desactiva la selección automática de la primera fila
                    dtgMarcasN.ClearSelection();
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
                pictureBox1.Image = null;
            }
        }

        public void mostrarPanelRegistro()
        {
            if (textBoxEstatus.Text == "Registrada")
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
                !ValidarCampo(nombre, "Por favor, ingrese el nombre.") ||
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
                (registroChek && !int.TryParse(registro, out _)) ||
                (registroChek && !int.TryParse(folio, out _)) ||
                (registroChek && !int.TryParse(libro, out _)))
            {
                FrmAlerta alerta = new FrmAlerta("LA CLASE, FOLIO, REGISTRO Y TOMO\n DEBEN SER VALORES NUMÉRICOS ENTEROS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("El expediente, clase, folio, registro y tomo deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

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
                //MessageBox.Show("Por favor, ingrese una imagen.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (registroChek)
            {
                if (!ValidarCampo(folio, "Por favor, ingrese el número de folio.") ||
                    !ValidarCampo(registro, "Por favor, ingrese el número de registro.") ||
                    !ValidarCampo(libro, "Por favor, ingrese el número de tomo.")
                    )
                {
                    return false;
                }
            }

            return true;
        }

        public void ActualizarMarcaNacional()
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

            // Validar campos 
            if (!ValidarCampos(expediente, nombre, clase, signoDistintivo, tipoSigno, estado, ref logo, registroChek, registro, folio, libro))
            {
                return;
            }

            try
            {
                // Actualizar la marca con tipo
                bool esActualizado = registroChek ?
                    marcaModel.EditMarcaInternacionalRegistrada(
                        SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, null, registro, folio, libro, fecha_registro, fecha_vencimiento, null, null) :
                    marcaModel.EditMarcaInternacional(SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, null);

                DataTable MarcaActualizada = marcaModel.GetMarcaNacionalById(SeleccionarMarca.idInt);

                if (esActualizado)
                {
                    // Verificar si la actualización fue exitosa
                    if (esActualizado)
                    {
                        // Verificar si las observaciones ya contienen el estado actual
                        if (MarcaActualizada.Rows.Count > 0 && MarcaActualizada.Rows[0]["Observaciones"].ToString().Contains(estado))
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
                            historialModel.GuardarEtapa(SeleccionarMarca.idInt, AgregarEtapa.fecha.Value, estado, AgregarEtapa.anotaciones, AgregarEtapa.usuario,"TRÁMITE");
                            //MessageBox.Show("Marca internacional actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FrmAlerta alerta = new FrmAlerta("MARCA INTERNACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            SeleccionarMarca.idInt = 0;
                            tabControl1.SelectedTab = tabPageListaMarcas;
                        }
                    }
                    else
                    {
                        FrmAlerta alerta = new FrmAlerta("ERROR AL ACTUALIZAR MARCA INTERNACIONAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                        //MessageBox.Show("Error al actualizar la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("ERROR AL ACTUALIZAR MARCA INTERNACIONAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.ShowDialog();
                    //MessageBox.Show("Error al actualizar la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la marca nacional." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            comboBoxTipoSigno.SelectedIndex = -1;
            comboBoxSignoDistintivo.SelectedIndex = -1;
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
                            AgregarEtapa.solicitante = titular[0].nombre;
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


        private void VerificarSeleccionEdicion()
        {
            if (dtgMarcasN.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasN.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasN.SelectedRows[0];
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

        private async void refrescarMarca()
        {
            if (SeleccionarMarca.idInt > 0)
            {
                try
                {
                    DataTable detallesMarcaInt = await Task.Run(() => marcaModel.GetMarcaNacionalById(SeleccionarMarca.idInt));

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
                            //MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Verificar si "observaciones" contiene la palabra "registrada"
                        bool contieneRegistrada = SeleccionarMarca.observaciones.Contains("Registrada", StringComparison.OrdinalIgnoreCase);

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


                Invoke(new Action(() =>
                {
                    dtgHistorial.AutoGenerateColumns = true;
                    dtgHistorial.DataSource = historial;
                    dtgHistorial.Refresh();

                    if (dtgHistorial.Columns["id"] != null)
                    {
                        dtgHistorial.Columns["id"].Visible = false;
                    }

                    dtgHistorial.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ibtnAgregar_Click(object sender, EventArgs e)
        {

        }

        private async void FrmMostrarTodas_Load(object sender, EventArgs e)
        {
            await Task.Run(() => LoadMarcas());
            tabControl1.SelectedTab = tabPageListaMarcas;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageHistorialMarca);
            ActualizarFechaVencimiento();
        }

        public async void Editar()
        {
            VerificarSeleccionEdicion();
            if (SeleccionarMarca.idInt > 0)
            {
                tabControl1.Visible = false;
                await CargarDatosMarca();
                AnadirTabPage(tabPageMarcaDetail);
                tabControl1.SelectedTab = tabPageMarcaDetail;
                tabControl1.Visible = true;
            }
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void ibtnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void tabPageMarcaDetail_Click(object sender, EventArgs e)
        {

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
                    FrmAlerta alerta = new FrmAlerta("ETAPA AGREGADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                AgregarEtapa.solicitante = SeleccionarPersona.nombre;
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

        private void btnGuardar_Click(object sender, EventArgs e)
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
            convertirImagen();
            pictureBox1.Image = documento;
        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void roundedButton6_Click(object sender, EventArgs e)
        {

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

                    try
                    {

                        if (dtgMarcasN.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasN.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);


                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", fechaAbandono.ToShortDateString() + " Abandono " + justificacion, usuarioAbandono, "TRÁMITE");
                                FrmAlerta alerta = new FrmAlerta("LA MARCA HA SIDO MARCADA COMO ABANDONADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MostrarMarcasTramite();
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

        private void roundedButton6_Click_1(object sender, EventArgs e)
        {
            loadHistorialById();
            AnadirTabPage(tabPageHistorialMarca);
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                EliminarTabPage(tabPageHistorialDetail);
            }
            else if (tabControl1.SelectedTab == tabPageListaMarcas)
            {
                LoadMarcas();
                SeleccionarMarca.idInt = 0;
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                CargarDatosMarca();
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
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


        public void EditarHistorial()
        {
            if (dtgHistorial.SelectedRows.Count > 0)
            {

                Habilitar();
                var filaSeleccionada = dtgHistorial.SelectedRows[0];
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
        private void iconButton5_Click(object sender, EventArgs e)
        {
            EditarHistorial();
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
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGUN ESTADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("No ha seleccionado ningun estado");
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

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (dtgHistorial.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgHistorial.SelectedRows[0];
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
                        refrescarMarca();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila para eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            if (dtgHistorial.SelectedRows.Count > 0)
            {
                Deshabilitar();
                var filaSeleccionada = dtgHistorial.SelectedRows[0];
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
                //MessageBox.Show("Por favor, seleccione una fila del historial.");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void btnActualizarM_Click(object sender, EventArgs e)
        {
            ActualizarMarcaNacional();
            EliminarTabPage(tabPageHistorialMarca);
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
                        ActualizarMarcaNacional();
                        EliminarTabPage(tabPageMarcaDetail);
                        EliminarTabPage(tabPageHistorialMarca);
                        tabControl1.SelectedTab = tabPageListaMarcas;
                    }

                }

            }
        }

        private void dtgMarcasN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }

        private void dtgHistorial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarHistorial();
        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                DataTable marcas = marcaModel.FiltrarMarcasNacionalesEnTramite(txtBuscar.Text);
                if (marcas.Rows.Count > 0)
                {
                    dtgMarcasN.DataSource = marcas;

                    if (dtgMarcasN.Columns["id"] != null)
                    {
                        dtgMarcasN.Columns["id"].Visible = false;
                    }
                    dtgMarcasN.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN MARCAS CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    LoadMarcas();
                }

            }
            else
            {
                LoadMarcas();
            }
        }

        private void iconButton2_Click_2(object sender, EventArgs e)
        {
            //Enviar a oposicion
            FrmEnviarAOposicion frmEnviarAOposicion = new FrmEnviarAOposicion();
            frmEnviarAOposicion.ShowDialog();

            AgregarEtapa.solicitante = txtNombreTitular.Text;


        }
    }
}
     
