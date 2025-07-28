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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Presentacion.Marcas_Nacionales
{
    public partial class FrmRenovaciones : Form
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
        private string host = "ftp.foragro.com.es"; // Tu host FTP
        private string usuario = "foragro"; // Tu usuario FTP
        private string contraseña = "gqL8ygtSv6Z8"; // Tu contraseña FTP
        private string directorioBase = "/foragro.com.es/marcas/internacionales";
        public void convertirImagen()
        {

            using (MemoryStream ms = new MemoryStream(defaultImage))
            {
                documento = System.Drawing.Image.FromStream(ms);
            }
        }
        public FrmRenovaciones()
        {
            InitializeComponent();
            archivoSubido = false;
            this.Load += FrmRenovaciones_Load;
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
            totalRows = await marcaModel.GetTotalMarcasInternacionalesEnTramiteDeRenovacion();
            totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalRows / pageSize)));

            var marcasN = await Task.Run(() => marcaModel.GetAllMarcasInternacionalesEnTramiteDeRenovacion(currentPageIndex, pageSize));
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
                totalRows = await marcaModel.GetFilteredMarcasInternacionalesEnTramiteDeRenovacionCount(txtBuscar.Text);
                totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalRows / pageSize)));
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = await marcaModel.FiltrarMarcasInternacionalesEnTramiteDeRenovacion(buscar, currentPageIndex, pageSize);
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
        private bool ValidarCampo(string campo, string mensaje)
        {
            if (string.IsNullOrEmpty(campo))
            {
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidarCampos(string pais, string expediente, string nombre, ref string clase, string signoDistintivo, string tipo, string estado,
   ref byte[] logo, bool registroChek, string registro, string folio, string libro)
        {
            // Verificar campos obligatorios
            if (!ValidarCampo(pais, "Por favor, ingrese un pais.") ||
                !ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
                !ValidarCampo(nombre, "Por favor, ingrese el signo.") ||
                !ValidarCampo(clase, "Por favor, ingrese la clase.") ||
                !ValidarCampo(signoDistintivo, "Por favor, seleccione un signo distintivo.") ||
                !ValidarCampo(tipo, "Por favor, seleccione un tipo.") ||
                !ValidarCampo(estado, "Por favor, seleccione un estado."))
            {
                return false;
            }

            // Normalizar clase quitando espacios extra
            clase = string.Join(",", clase.Split(',')
                                          .Select(c => c.Trim())
                                          .Where(c => !string.IsNullOrWhiteSpace(c)));

            if (checkBoxMulticlase.Checked)
            {
                string[] clases = clase.Split(',');

                foreach (string c in clases)
                {
                    if (!int.TryParse(c, out _))
                    {
                        FrmAlerta alerta = new FrmAlerta("SI EL MODO MULTICLASE ESTÁ ACTIVO,\nLA CLASE DEBE CONTENER SOLO NÚMEROS ENTEROS SEPARADOS POR COMAS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        alerta.ShowDialog();
                        return false;
                    }
                }
            }
            else
            {
                // Solo permitir un número entero
                if (!int.TryParse(clase, out _))
                {
                    FrmAlerta alerta = new FrmAlerta("LA CLASE DEBE SER UN VALOR NUMÉRICO ENTERO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    alerta.ShowDialog();
                    return false;
                }
            }


            // ✅ Nuevo bloque para validar campos alfanuméricos
            if (!string.IsNullOrWhiteSpace(folio) && !EsAlfanumerico(folio))
            {
                FrmAlerta alerta = new FrmAlerta("EL FOLIO DEBE SER UN VALOR ALFANUMÉRICO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(libro) && !EsAlfanumerico(libro))
            {
                FrmAlerta alerta = new FrmAlerta("EL TOMO DEBE SER UN VALOR ALFANUMÉRICO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return false;
            }

            if (registroChek && !string.IsNullOrWhiteSpace(registro) && !EsAlfanumerico(registro))
            {
                FrmAlerta alerta = new FrmAlerta("EL REGISTRO DEBE SER UN VALOR ALFANUMÉRICO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return false;
            }


            if ((comboBoxSignoDistintivo.Text == "Marca" &&
              comboBoxTipoSigno.Text == "Gráfica/Figurativa") ||
              (comboBoxSignoDistintivo.Text == "Marca" &&
              comboBoxTipoSigno.Text == "Mixta") ||
              (comboBoxSignoDistintivo.Text == "Emblema" &&
              comboBoxTipoSigno.Text == "Gráfica/Figurativa") ||
               (comboBoxSignoDistintivo.Text == "Emblema" &&
              comboBoxTipoSigno.Text == "Mixta")
              )
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
                    //MessageBox.Show("Por favor, ingrese una imagen.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (
                    !ValidarCampo(registro, "Por favor, ingrese el número de registro.")

                    )
                {
                    return false;
                }
            }

            return true;
        }

        private bool EsAlfanumerico(string texto)
        {
            // Permite letras, números, guiones
            return Regex.IsMatch(texto, @"^[a-zA-Z0-9\-_]+$");

        }

        public async void ActualizarMarcaNacional()
        {
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string signoDistintivo = comboBoxSignoDistintivo.Text;
            string tipoSigno = comboBoxTipoSigno.Text;
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[] logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            int? idCliente = SeleccionarPersona.idPersonaC;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;

            //Tramites de renovacion y traspaso
            string erenov = txtERenovacion.Text;
            string etrasp = txtETraspaso.Text;

            string paisRegistro = comboBox1.Text;
            string tiene_poder = "no";
            int multiclase = 0;
            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;
            string ubicacionF = txtUbicacion.Text;
            if (checkBoxTienePoder.Checked)
            {
                tiene_poder = "si";
            }
            else
            {
                tiene_poder = "no";
            }

            if (checkBoxMulticlase.Checked)
            {
                multiclase = 1;
            }
            else
            {
                multiclase = 0;
            }

            // Validaciones
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

            if (idCliente == 0)
            {
                idCliente = null;
            }


            // Validar campos 
            if (!ValidarCampos(paisRegistro, expediente, nombre, ref clase, signoDistintivo, tipoSigno, estado, ref logo, registroChek, registro, folio, libro))
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
            // Editar la marca
            try
            {


                bool esActualizado;

                if (registroChek)
                {
                    esActualizado = await marcaModel.EditMarcaInternacionalRegistrada(
                        SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, tipoSigno, clase, multiclase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente, registro, folio, libro, fecha_registro, fecha_vencimiento, erenov, etrasp, ubicacionF);
                }
                else
                {
                    esActualizado = await marcaModel.EditMarcaInternacional(SeleccionarMarca.idInt, expediente, nombre, signoDistintivo
                        , tipoSigno, clase, multiclase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente, ubicacionF);
                }

                DataTable marcaActualizada = await marcaModel.GetMarcaInternacionalById(SeleccionarMarca.idInt);

                if (esActualizado)
                {
                    // Verificar si la actualización fue exitosa
                    if (esActualizado)
                    {
                        if (marcaActualizada.Rows.Count > 0 && marcaActualizada.Rows[0]["Observaciones"].ToString().Contains(estado))
                        {
                            FrmAlerta alerta = new FrmAlerta("MARCA INTERNACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            SeleccionarMarca.idInt = 0;
                            AnadirTabPage(tabPageRegistradasList);
                            EliminarTabPage(tabPageMarcaDetail);
                            EliminarTabPage(tabPageHistorialMarca);
                            EliminarTabPage(tabPageListaArchivos);
                            LimpiarFormulario();
                        }
                        else
                        {
                            historialModel.GuardarEtapa(SeleccionarMarca.idInt, AgregarEtapa.fecha.Value, estado, AgregarEtapa.anotaciones, AgregarEtapa.usuario, "TRÁMITE", null);
                            FrmAlerta alerta = new FrmAlerta("MARCA INTERNACIONAL ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            alerta.ShowDialog();
                            SeleccionarMarca.idInt = 0;
                            AnadirTabPage(tabPageRegistradasList);
                            EliminarTabPage(tabPageMarcaDetail);
                            EliminarTabPage(tabPageHistorialMarca);
                            EliminarTabPage(tabPageListaArchivos);
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
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtFolio.Text = "";
            txtLibro.Text = "";
            pictureBox1.Image = null;
            txtNombreTitular.Text = "";
            txtNombreAgente.Text = "";
            txtETraspaso.Text = "";
            txtERenovacion.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            checkBoxMulticlase.Checked = false;
            ActualizarFechaVencimiento();
            txtRegistro.Text = "";
            richTextBox1.Text = "";
            AgregarEtapa.LimpiarEtapa();
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
                        SeleccionarMarca.clase = row["clase"].ToString();
                        SeleccionarMarca.estado = row["estado"].ToString();
                        SeleccionarMarca.signoDistintivo = row["signoDistintivo"].ToString();
                        SeleccionarMarca.tipoSigno = row["Tipo"].ToString();
                        //SeleccionarMarca.logo = row["logo"] is DBNull ? null : (byte[])row["logo"];
                        SeleccionarMarca.idPersonaTitular = row["idTitular"] != DBNull.Value ? Convert.ToInt32(row["idTitular"]) : 0;
                        SeleccionarMarca.idPersonaAgente = row["idAgente"] != DBNull.Value ? Convert.ToInt32(row["idAgente"]) : 0;
                        SeleccionarMarca.idPersonaCliente = row["idCliente"] != DBNull.Value ? Convert.ToInt32(row["idCliente"]) : 0;
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
                        SeleccionarMarca.erenov = row["Erenov"].ToString();
                        SeleccionarMarca.tiene_poder = row["tiene_poder"] != DBNull.Value ? row["tiene_poder"].ToString() : string.Empty;
                        SeleccionarMarca.pais_de_registro = row["pais_de_registro"] != DBNull.Value ? row["pais_de_registro"].ToString() : string.Empty;
                        SeleccionarMarca.logo = await marcaModel.ObtenerLogoMarcaPorId(SeleccionarMarca.idInt);
                        txtUbicacion.Text = row["ubicacion_fisica"] != DBNull.Value ? row["ubicacion_fisica"].ToString() : string.Empty;
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
                        if (SeleccionarMarca.idPersonaCliente == 0)
                        {
                            SeleccionarPersona.idPersonaC = null;


                        }
                        else
                        {
                            SeleccionarPersona.idPersonaC = SeleccionarMarca.idPersonaCliente;


                        }

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

                        checkBoxTienePoder.Checked = SeleccionarMarca.tiene_poder.Equals("si", StringComparison.OrdinalIgnoreCase);
                        int index = comboBox1.FindString(SeleccionarMarca.pais_de_registro);
                        comboBox1.SelectedIndex = index;


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


                        if (row["multiclase"] != DBNull.Value && int.TryParse(row["multiclase"].ToString(), out int multiclaseInt))
                        {
                            checkBoxMulticlase.Checked = multiclaseInt == 1;
                        }
                        else
                        {
                            checkBoxMulticlase.Checked = false; // o lo que quieras por defecto
                        }

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
                            AgregarRenovacion.fechaVencimientoAntigua = Convert.ToDateTime(SeleccionarMarca.fechaVencimiento);
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

        private async void FrmRenovaciones_Load(object sender, EventArgs e)
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

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
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
                await CargarDatosMarca();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageListaArchivos);
            }*/
        }
        private void LimpiarControlesMarca()
        {
            convertirImagen();
            txtExpediente.Clear();
            txtNombre.Clear();
            txtClase.Clear();
            textBoxEstatus.Clear();
            comboBoxSignoDistintivo.SelectedIndex = -1;
            comboBoxTipoSigno.SelectedIndex = -1;
            pictureBox1.Image = documento;
            datePickerFechaSolicitud.Value = DateTime.Now;
            richTextBox1.Clear();
            txtNombreTitular.Clear();
            txtNombreAgente.Clear();
            checkBox1.Checked = false;
            txtRegistro.Clear();
            txtFolio.Clear();
            txtLibro.Clear();
            dateTimePFecha_Registro.Value = DateTime.Now;
            dateTimePFecha_vencimiento.Value = DateTime.Now;
            txtERenovacion.Clear();
        }
        public async void Editar()
        {
            LimpiarControlesMarca();
            VerificarSeleccionIdMarcaEdicion();
            Cursor = Cursors.WaitCursor;
            if (SeleccionarMarca.idInt > 0)
            {
                await CargarDatosMarca();
                AnadirTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageRegistradasList);

            }
            Cursor = Cursors.Default;
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

                    try
                    {

                        if (dtgMarcasRenov.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasRenov.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);

                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", fechaAbandono.ToString("dd/MM/yyyy") + " Abandono " + justificacion, usuarioAbandono, "TRÁMITE", null);

                                MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadMarcas();
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
                    historialModel.GuardarEtapa(SeleccionarMarca.idInt, Convert.ToDateTime(AgregarEtapa.fecha), AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario, "TRÁMITE", null);
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
                            SeleccionarHistorial.fecha = Convert.ToDateTime(fila["fecha"]);
                            SeleccionarHistorial.anotaciones = fila["anotaciones"].ToString();
                            SeleccionarHistorial.usuario = fila["usuario"].ToString();
                            SeleccionarHistorial.usuarioEdicion = fila["usuarioEdicion"].ToString();

                            comboBoxEstatusH.SelectedItem = SeleccionarHistorial.etapa;
                            dateTimePickerFechaIngreso.Value = SeleccionarHistorial.fecha;
                            richTextBoxAnotacionesH.Text = SeleccionarHistorial.anotaciones;
                            labelUserEditor.Text = UsuarioActivo.usuario;
                            lblUser.Text = SeleccionarHistorial.usuario;

                            if (fila["fechaVencimiento"] != DBNull.Value)
                            {
                                labelVenc.Visible = true;
                                dateTimePickerFechaVencimiento.Visible = true;
                                if (fila["fechaVencimiento"] != DBNull.Value && !string.IsNullOrWhiteSpace(fila["fechaVencimiento"].ToString()))
                                {
                                    dateTimePickerFechaVencimiento.Value = Convert.ToDateTime(fila["fechaVencimiento"]);
                                }
                            }
                            else
                            {
                                labelVenc.Visible = false;
                                dateTimePickerFechaVencimiento.Visible = false;
                            }

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
            if (labelVenc.Visible)
            {
                comboBoxEstatusH_SelectedIndexChanged(sender, e);
            }
        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string etapa = comboBoxEstatusH.Text;
            DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;
            DateTime fechaVencimiento = fechaIngreso;

            // Calcular fecha de vencimiento según la etapa
            switch (etapa)
            {
                case "Examen de fondo":
                case "Objeción":
                case "Publicación":
                    fechaVencimiento = fechaIngreso.AddMonths(2);
                    break;

                case "Requerimiento":
                case "Orden de pago":
                    fechaVencimiento = fechaIngreso.AddMonths(1);
                    break;

                case "Resolución RPI desfavorable":
                    fechaVencimiento = fechaIngreso.AddDays(5);
                    break;
            }

            // Mostrar u ocultar controles de vencimiento
            bool mostrarVencimiento = etapa == "Examen de fondo" ||
                                       etapa == "Requerimiento" ||
                                       etapa == "Objeción" ||
                                       etapa == "Publicación" ||
                                       etapa == "Orden de pago" ||
                                       etapa == "Resolución RPI desfavorable";

            labelVenc.Visible = mostrarVencimiento;
            dateTimePickerFechaVencimiento.Visible = mostrarVencimiento;

            if (mostrarVencimiento)
            {
                dateTimePickerFechaVencimiento.Value = fechaVencimiento;
            }

            // Mostrar anotación en el RichTextBox
            string fecha = fechaIngreso.ToString("dd/MM/yyyy");
            string venc = fechaVencimiento.ToString("dd/MM/yyyy");

            if (etapa == "Resolución RPI desfavorable")
            {
                richTextBoxAnotacionesH.Text = $"{fecha} Por objeción - {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (mostrarVencimiento)
            {
                richTextBoxAnotacionesH.Text = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
            }
            else if (etapa == "Resolución RPI favorable" ||
                     etapa == "Recurso de revocatoria" ||
                     etapa == "Resolución Ministerio de Economía (MINECO)" ||
                     etapa == "Contencioso administrativo")
            {
                richTextBoxAnotacionesH.Text = $"{fecha} Por objeción - {etapa}";
            }
            else
            {
                richTextBoxAnotacionesH.Text = $"{fecha} {etapa}";
            }
        }

        private async void btnEditarH_Click(object sender, EventArgs e)
        {
            string usuario = lblUser.Text;
            string usuarioEditor = labelUserEditor.Text;
            string etapa = comboBoxEstatusH.Text;
            DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;
            DateTime fechaVencimiento = fechaIngreso;

            // Calcular vencimiento automático según etapa
            switch (etapa)
            {
                case "Examen de fondo":
                case "Objeción":
                case "Publicación":
                    fechaVencimiento = fechaIngreso.AddMonths(2);
                    break;

                case "Requerimiento":
                case "Orden de pago":
                    fechaVencimiento = fechaIngreso.AddMonths(1);
                    break;

                case "Resolución RPI desfavorable":
                    fechaVencimiento = fechaIngreso.AddDays(5);
                    break;
            }

            // Mostrar u ocultar controles de vencimiento
            bool requiereVencimiento = etapa == "Examen de fondo" ||
                                        etapa == "Requerimiento" ||
                                        etapa == "Objeción" ||
                                        etapa == "Publicación" ||
                                        etapa == "Orden de pago" ||
                                        etapa == "Resolución RPI desfavorable";

            // Asignar valores a AgregarEtapa
            AgregarEtapa.etapa = etapa;
            AgregarEtapa.fecha = fechaIngreso;
            AgregarEtapa.usuario = usuarioEditor;
            AgregarEtapa.fechaVencimiento = requiereVencimiento ? fechaVencimiento : null;

            if (comboBoxEstatusH.SelectedIndex != -1)
            {
                string anotaciones = richTextBoxAnotacionesH.Text;
                string fecha = fechaIngreso.ToString("dd/MM/yyyy");
                string venc = fechaVencimiento.ToString("dd/MM/yyyy");
                string anotacionFinal = "";

                if (etapa == "Resolución RPI desfavorable")
                {
                    anotacionFinal = $"{fecha} Por objeción - {etapa} | Fecha de vencimiento: {venc}";
                }
                else if (requiereVencimiento)
                {
                    anotacionFinal = $"{fecha} {etapa} | Fecha de vencimiento: {venc}";
                }
                else if (etapa == "Resolución RPI favorable" ||
                         etapa == "Recurso de revocatoria" ||
                         etapa == "Resolución Ministerio de Economía (MINECO)" ||
                         etapa == "Contencioso administrativo")
                {
                    anotacionFinal = $"{fecha} Por objeción - {etapa}";
                }
                else
                {
                    anotacionFinal = $"{fecha} {etapa}";
                }

                if (!anotaciones.Contains(anotacionFinal))
                {
                    AgregarEtapa.anotaciones = anotacionFinal + " " + anotaciones;
                }
                else
                {
                    AgregarEtapa.anotaciones = anotaciones;
                }

                // Guardar en base de datos
                bool actualizado = await historialModel.EditHistorialById(
                    SeleccionarHistorial.id,
                    etapa,
                    fechaIngreso,
                    AgregarEtapa.anotaciones,
                    usuario,
                    usuarioEditor,
                    requiereVencimiento ? fechaVencimiento : (DateTime?)null
                );

                if (actualizado)
                {
                    FrmAlerta alerta = new FrmAlerta("ETAPA ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    EliminarTabPage(tabPageHistorialDetail);
                    AnadirTabPage(tabPageMarcaDetail);
                    SeleccionarHistorial.id = 0;
                    await refrescarMarca();
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO NINGÚN ESTADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }


        }

        private async Task refrescarMarca()
        {
            if (SeleccionarMarca.idInt > 0)
            {
                try
                {
                    DataTable detallesMarcaInt = await marcaModel.GetMarcaInternacionalById(SeleccionarMarca.idInt);

                    if (detallesMarcaInt.Rows.Count > 0)
                    {
                        DataRow row = detallesMarcaInt.Rows[0];

                        if (row["estado"] != DBNull.Value && row["observaciones"] != DBNull.Value)
                        {
                            // Actualizar los controles 
                            textBoxEstatus.Text = row["estado"].ToString();
                            richTextBox1.Text = row["observaciones"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la marca seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        // Verificar si "observaciones" contiene la palabra "registrada"
                        bool contieneRegistrada = await marcaModel.TieneEtapaRegistrada(SeleccionarMarca.idInt);

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

        private void iconButton9_Click(object sender, EventArgs e)
        {
            AnadirTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
                    ActualizarMarcaNacional();

                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void btnTraspasar_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();

            if (!archivoSubido)
            {
                FrmAlerta alerta = new FrmAlerta("DEBE SUBIR EL TÍTULO DE RENOVACIÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
                return;
            }

            if (DatosRegistro.peligro == false)
            {
                FrmAgregarRenovacionConcedida frmAgregarConcesion = new FrmAgregarRenovacionConcedida();
                frmAgregarConcesion.ShowDialog();

                if (AgregarRenovacion.renovacionTerminada == true)
                {
                    LimpiarFormulario();
                    AgregarRenovacion.renovacionTerminada = false;
                    AnadirTabPage(tabPageRegistradasList);
                    EliminarTabPage(tabPageMarcaDetail);
                    EliminarTabPage(tabPageHistorialMarca);
                    EliminarTabPage(tabPageHistorialDetail);
                    EliminarTabPage(tabPageListaArchivos);
                    await LoadMarcas();
                    FrmAlerta alerta = new FrmAlerta("RENOVACIÓN GUARDADA CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();

                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }
        public void VerificarDatosRegistro()
        {
            if (checkBox1.Checked == true && (string.IsNullOrEmpty(txtRegistro.Text)
                //|| string.IsNullOrEmpty(txtFolio.Text) 
                //|| string.IsNullOrEmpty(txtLibro.Text)
                ))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }
        private async void btnCancelarM_Click(object sender, EventArgs e)
        {
            /*
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                DatosRegistro.peligro = false;
                AnadirTabPage(tabPageRegistradasList);
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageListaArchivos);
                tabControl1.SelectedTab = tabPageRegistradasList;
                await LoadMarcas();
                SeleccionarMarca.idInt = 0;
                LimpiarFormulario();
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
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageListaArchivos);

            SeleccionarMarca.idInt = 0;
            LimpiarFormulario();
        }

        private async void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = await marcaModel.GetFilteredMarcasInternacionalesEnTramiteDeRenovacionCount(txtBuscar.Text);
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalRows / pageSize));

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }

        private void dtgMarcasRenov_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }

        private void dtgHistorialR_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarHistorial();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            filtrar();
        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = await marcaModel.GetFilteredMarcasInternacionalesEnTramiteDeRenovacionCount(txtBuscar.Text);
                totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalRows / pageSize)));

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

        private void txtERenovacion_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtERenovacion.Text))
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

        private void dtgMarcasRenov_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgMarcasRenov.Columns["id"] != null)
            {
                dtgMarcasRenov.Columns["id"].Visible = false;
                dtgMarcasRenov.ClearSelection();
            }
        }

        private void dtgHistorialR_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgHistorialR.Columns["id"] != null)
            {
                dtgHistorialR.Columns["id"].Visible = false;
            }

            dtgHistorialR.ClearSelection();
        }
        private void SubirArchivoRenovacion(string idMarca)
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
            SubirArchivoRenovacion("" + SeleccionarMarca.idInt);
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

        private void FrmRenovaciones_Resize(object sender, EventArgs e)
        {
            CentrarPanel();
        }

        private async void btnDesistir_Click(object sender, EventArgs e)
        {
            using (FrmJustificacionDesistimiento justificacionForm = new FrmJustificacionDesistimiento())
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

                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Desistimiento", justificacion, usuarioAbandono, "TRÁMITE", null);
                                FrmAlerta alerta = new FrmAlerta("LA MARCA HA SIDO MARCADA COMO DESISTIDA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();
                                //MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadMarcas();
                            }
                        }
                        else
                        {
                            FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA MARCA PARA DESISTIR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void dateTimePickerFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            if (labelVenc.Visible)
            {
                comboBoxEstatusH_SelectedIndexChanged(sender, e);
            }
        }
    }
}
