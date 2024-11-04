using Comun.Cache;
using Dominio;
using Microsoft.Win32;
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
using System.Windows.Forms.VisualStyles;

namespace Presentacion.Marcas_Internacionales
{
    public partial class FrmMarcasIntIngresadas : Form
    {
        MarcaModel marcaModel = new MarcaModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialModel historialModel = new HistorialModel();
        public FrmMarcasIntIngresadas()
        {
            InitializeComponent();
            this.Load += FrmMarcasIntIngresadas_Load;
            SeleccionarMarca.idInt = 0;
            ActualizarFechaVencimiento();
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
            dtgMarcasIn.DataSource = marcaModel.GetAllMarcasInternacionalesIngresadas();
            if (dtgMarcasIn.Columns["id"] != null)
            {
                dtgMarcasIn.Columns["id"].Visible = false;

                dtgMarcasIn.ClearSelection();
            }
        }
        private async void LoadMarcas()
        {
            var marcasN = await Task.Run(() => marcaModel.GetAllMarcasInternacionalesIngresadas());

            Invoke(new Action(() =>
            {
                dtgMarcasIn.DataSource = marcasN;
                dtgMarcasIn.Refresh();
                if (dtgMarcasIn.Columns["id"] != null)
                {
                    dtgMarcasIn.Columns["id"].Visible = false;
                    dtgMarcasIn.ClearSelection();
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
                    pictureBox1.Image = Image.FromStream(ms);
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
                btnActualizar.Location = new Point(47, panel3.Location.Y + panel3.Height + 10);
                btnCancelar.Location = new Point(370, panel3.Location.Y + panel3.Height + 10);
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                panel3.Visible = false;
                btnActualizar.Location = new Point(47, 960);
                btnCancelar.Location = new Point(370, 960);
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

        private bool ValidarCampos(string expediente, string nombre, string paisRegistro, string clase, string signoDistintivo, string tipo, string estado,
   ref byte[] logo, bool registroChek, string registro, string folio, string libro)
        {
            // Verificar campos obligatorios
            if (!ValidarCampo(expediente, "Por favor, ingrese el expediente.") ||
                !ValidarCampo(nombre, "Por favor, ingrese el nombre.") ||
                !ValidarCampo(clase, "Por favor, ingrese la clase.") ||
                !ValidarCampo(paisRegistro, "Por favor, ingrese un pais.") ||
                !ValidarCampo(signoDistintivo, "Por favor, seleccione un signo distintivo.") ||
                !ValidarCampo(tipo, "Por favor, seleccione un tipo.") ||
                !ValidarCampo(estado, "Por favor, seleccione un estado."))
            {
                return false;
            }

            // Validar que el expediente, clase, folio, registro y libro sean enteros
            if (!int.TryParse(expediente, out _) ||
                !int.TryParse(clase, out _) ||
                (registroChek && !int.TryParse(registro, out _)) ||
                (registroChek && !int.TryParse(folio, out _)) ||
                (registroChek && !int.TryParse(libro, out _)))
            {
                MessageBox.Show("El expediente, clase, folio, registro y libro deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar que hay una imagen
            if (pictureBox1.Image != null)
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

            // Si está registrada, se verifica la información del registro
            if (registroChek)
            {
                // Validar campos adicionales para marcas registradas
                if (!ValidarCampo(folio, "Por favor, ingrese el número de folio.") ||
                    !ValidarCampo(registro, "Por favor, ingrese el número de registro.") ||
                    !ValidarCampo(libro, "Por favor, ingrese el número de libro.")
                    )
                {
                    return false;
                }
            }

            return true; // Todas las validaciones pasaron
        }


        public void ActualizarMarcaInternacional()
        {
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string paisRegistro = comboBox1.SelectedItem?.ToString();
            string signoDistintivo = comboBoxSignoDistintivo.SelectedItem?.ToString();
            string tipoSigno=comboBoxTipoSigno.SelectedItem?.ToString();
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            byte[] logo = null;
            int idTitular = SeleccionarPersona.idPersonaT;
            int idAgente = SeleccionarPersona.idPersonaA;
            int idCliente = SeleccionarPersona.idPersonaC;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string observaciones = richTextBox1.Text;

            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox1.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;

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
            if (idCliente <= 0)
            {
                MessageBox.Show("Por favor, seleccione un cliente válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar campos 
            if (!ValidarCampos(expediente, nombre, paisRegistro, clase, signoDistintivo, tipoSigno, estado, ref logo, registroChek, registro, folio, libro))
            {
                return;
            }

            // Editar la marca
            try
            {
                
                string tienePoder = checkBoxTienePoder.Checked ? "si" : "no";

                bool esActualizado;

                // Verificar si la marca está registrada
                if (registroChek)
                {
                    esActualizado = marcaModel.EditMarcaInternacionalRegistrada(
                        SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tienePoder, idCliente, registro, folio, libro, fecha_registro, fecha_vencimiento);
                }
                else
                {
                    esActualizado = marcaModel.EditMarcaInternacional(SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tienePoder, idCliente);
                }

                DataTable marcaActualizada = marcaModel.GetMarcaInternacionalById(SeleccionarMarca.idInt);

                if (esActualizado)
                {
                    // Verificar si la actualización fue exitosa
                    if (esActualizado)
                    {
                        // Verificar si las observaciones ya contienen el estado actual
                        if (marcaActualizada.Rows.Count > 0 && marcaActualizada.Rows[0]["Observaciones"].ToString().Contains(estado))
                        {
                            MessageBox.Show("Marca internacional actualizada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SeleccionarMarca.idInt = 0;
                            tabControl1.SelectedTab = tabPageIngresadasList;
                        }
                        else
                        {
                            // Guardar la nueva etapa en el historial
                            historialModel.GuardarEtapa(SeleccionarMarca.idInt, AgregarEtapa.fecha.Value, estado, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                            MessageBox.Show("Marca internacional actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SeleccionarMarca.idInt = 0;
                            tabControl1.SelectedTab = tabPageIngresadasList;
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
                MessageBox.Show("Error al " + (registroChek ? "registrar" : "actualizar") + " la marca internacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarFormulario();
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
            txtNombreCliente.Text = "";
            datePickerFechaSolicitud.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            dateTimePFecha_Registro.Value = DateTime.Now;
            textBoxEstatus.Text = "";
            checkBox1.Checked = false;
            comboBox1.SelectedIndex = -1;
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

                if (detallesMarcaInter.Rows.Count > 0) // Usa Rows.Count en lugar de Count
                {
                    DataRow row = detallesMarcaInter.Rows[0]; // Accede a la primera fila del DataTable

                    if (row["expediente"] != DBNull.Value) // Comprueba si "registro" no es DBNull
                    {
                        SeleccionarMarca.expediente = row["expediente"].ToString();
                        SeleccionarMarca.nombre = row["nombre"].ToString();
                        SeleccionarMarca.clase = row["clase"].ToString();
                        SeleccionarMarca.estado = row["estado"].ToString();
                        SeleccionarMarca.signoDistintivo = row["signoDistintivo"].ToString();
                        SeleccionarMarca.tipoSigno= row["Tipo"].ToString();
                        SeleccionarMarca.logo = row["logo"] is DBNull ? null : (byte[])row["logo"];
                        SeleccionarMarca.idPersonaTitular = Convert.ToInt32(row["idTitular"]);
                        SeleccionarMarca.idPersonaAgente = Convert.ToInt32(row["idAgente"]);
                        SeleccionarMarca.idPersonaCliente = Convert.ToInt32(row["idCliente"]);
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
                        SeleccionarMarca.tiene_poder = row["tiene_poder"].ToString();
                        SeleccionarMarca.pais_de_registro = row["pais_de_registro"].ToString();

                        var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaTitular));
                        var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaAgente));
                        var clienteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarMarca.idPersonaCliente));
                        await Task.WhenAll(titularTask, agenteTask, clienteTask);

                        var titular = titularTask.Result;
                        var agente = agenteTask.Result;
                        var cliente = clienteTask.Result;

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

                        if (cliente.Count > 0)
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
            if (dtgMarcasIn.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgMarcasIn.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgMarcasIn.SelectedRows[0];
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
            SeleccionarMarca.idInt = 0;
            tabControl1.SelectedTab = tabPageIngresadasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                EliminarTabPage(tabPageHistorialDetail);
            }
            else if (tabControl1.SelectedTab == tabPageIngresadasList)
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
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
            }
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            VerificarSeleccionIdMarcaEdicion();
            if (SeleccionarMarca.idInt > 0)
            {
                CargarDatosMarca();
                AnadirTabPage(tabPageMarcaDetail);
                tabControl1.SelectedTab = tabPageMarcaDetail;
            }
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
                        if (dtgMarcasIn.SelectedRows.Count > 0)
                        {
                            var filaSeleccionada = dtgMarcasIn.SelectedRows[0];
                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {
                                int idMarca = Convert.ToInt32(dataRowView["id"]);

                                // Actualizar el estado y la justificación en la base de datos
                                historialModel.GuardarEtapa(idMarca, fechaAbandono, "Abandono", justificacion, usuarioAbandono);

                                MessageBox.Show("La marca ha sido marcada como 'Abandonada'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MostrarMarcasIngresadas();
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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
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

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapa frmAgregarEtapa = new FrmAgregarEtapa();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapa.etapa != "")
            {
                try
                {
                    historialModel.GuardarEtapa(SeleccionarMarca.idInt, (DateTime)AgregarEtapa.fecha, AgregarEtapa.etapa, AgregarEtapa.anotaciones, UsuarioActivo.usuario);
                    MessageBox.Show("Etapa agregada con éxito");
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
                catch(Exception ex)
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

        private void roundedButton6_Click(object sender, EventArgs e)
        {
            loadHistorialById();
            AnadirTabPage(tabPageHistorialMarca);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarMarcaInternacional();
            EliminarTabPage(tabPageHistorialMarca);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (textBoxEstatus.Text != "Registrada")
            {
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                tabControl1.SelectedTab = tabPageIngresadasList;
            }
            else
            {
                if (!ValidarCampo(txtFolio.Text, "Por favor, ingrese el número de folio. No es posible salir sin ingresar datos de registro, a menos que elimine esa etapa") ||
                    !ValidarCampo(txtRegistro.Text, "Por favor, ingrese el número de registro. No es posible salir sin ingresar datos de registro , a menos que elimine esa etapa") ||
                    !ValidarCampo(txtLibro.Text, "Por favor, ingrese el número de libro. No es posible salir sin ingresar datos de registro, a menos que elimine esa etapa")
                    )
                {
                    // Validar que el expediente, clase, folio, registro y libro sean enteros
                    
                    //MessageBox.Show("No es posible salir sin ingresar datos de registro");
                }
                else
                {
                    if (
                        (!int.TryParse(txtRegistro.Text, out _)) ||
                        (!int.TryParse(txtFolio.Text, out _)) ||
                        (!int.TryParse(txtLibro.Text, out _)))
                    {
                        MessageBox.Show("El registro, folio y libro deben ser valores numéricos enteros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        ActualizarMarcaInternacional();
                        EliminarTabPage(tabPageHistorialMarca);
                    }
                    
                }
                
            }
            
        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();
        }

        private void iconButton5_Click(object sender, EventArgs e)
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
                MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
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

                    
                    DialogResult confirmacionInicial = MessageBox.Show(" ¿Está seguro que desea eliminar esta etapa? "+usuario, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                            bool eliminarhistorial = historialModel.EliminarRegistroHistorial(id,usuario );

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

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void comboBoxEstatusH_SelectedValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void btnEditarH_Click(object sender, EventArgs e)
        {
            //Editar historial por id
            string etapa = comboBoxEstatusH.SelectedItem.ToString();
            DateTime fecha = dateTimePickerFechaH.Value;
            string anotaciones = richTextBoxAnotacionesH.Text;
            SeleccionarHistorial.anotaciones = anotaciones;
            string usuario = lblUser.Text;
            string usuarioEditor = labelUserEditor.Text;
            bool actualizar = historialModel.EditHistorialById(SeleccionarHistorial.id, etapa, fecha, anotaciones, usuario, usuarioEditor);

            if (actualizar == true)
            {
                MessageBox.Show("Estado actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControl1.SelectedTab = tabPageHistorialMarca;
                SeleccionarHistorial.id = 0;
                refrescarMarca();
            }
            else
            {
                MessageBox.Show("Error al actualizar el estado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageHistorialMarca;
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
    }
}
