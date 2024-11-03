using Comun.Cache;
using Dominio;
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
        private bool ValidarCampos(string expediente, string nombre, string clase, string signoDistintivo, string estado, ref byte[] logo, bool registroChek, string registro, System.Windows.Forms.ComboBox comboBoxPaisRegistro)
        {
            // Verificar campos obligatorios
            if (string.IsNullOrEmpty(expediente) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(clase) || string.IsNullOrEmpty(signoDistintivo))
            {
                MessageBox.Show("Por favor, llene todos los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar el estado
            if (string.IsNullOrEmpty(estado))
            {
                MessageBox.Show("Por favor, seleccione un estado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar el ComboBox para el país de registro
            if (comboBoxPaisRegistro.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un país de registro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (registroChek && string.IsNullOrEmpty(registro))
            {
                MessageBox.Show("Por favor, ingrese el número de registro.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // Todas las validaciones pasaron
        }

        public void ActualizarMarcaInternacional()
        {
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string clase = txtClase.Text;
            string signoDistintivo = txtSignoDistintivo.Text;
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
            if (!ValidarCampos(expediente, nombre, clase, signoDistintivo, estado, ref logo, registroChek, registro, comboBox1))
            {
                return;
            }

            // Editar la marca
            try
            {
                string paisRegistro = comboBox1.SelectedItem.ToString();
                string tiene_poder = "no";

                if (checkBox1.Checked)
                {
                    tiene_poder = "si";
                }
                else
                {
                    tiene_poder = "no";
                }

                bool actualizada = registroChek ?
                    marcaModel.EditMarcaInternacional(SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente) :
                    marcaModel.EditMarcaInternacionalRegistrada(SeleccionarMarca.idInt, expediente, nombre, signoDistintivo, clase, logo, idTitular, idAgente, solicitud, paisRegistro, tiene_poder, idCliente, registro, folio, libro, fecha_registro, fecha_vencimiento);

                if (actualizada == true)
                {
                    string etapa = textBoxEstatus.Text;
                    if (!string.IsNullOrEmpty(etapa))
                    {
                        historialModel.GuardarEtapa(SeleccionarMarca.idInt, AgregarEtapa.fecha.Value, etapa, AgregarEtapa.anotaciones, AgregarEtapa.usuario);
                    }
                    MessageBox.Show("Marca internacional " + (registroChek ? "registrada" : "actualizada") + " con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("Error al " + (registroChek ? "registrar" : "actualizar") + " la marca internacional.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al " + (registroChek ? "registrar" : "guardar") + " la marca internacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void LimpiarFormulario()
        {
            txtExpediente.Text = "";
            txtNombre.Text = "";
            txtClase.Text = "";
            txtSignoDistintivo.Text = "";
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
                        SeleccionarMarca.logo = row["logo"] is DBNull ? null : (byte[])row["logo"];
                        SeleccionarMarca.idPersonaTitular = Convert.ToInt32(row["idTitular"]);
                        SeleccionarMarca.idPersonaAgente = Convert.ToInt32(row["idAgente"]);
                        SeleccionarMarca.idPersonaCliente = Convert.ToInt32(row["idCliente"]);
                        SeleccionarMarca.fecha_solicitud = Convert.ToDateTime(row["fechaSolicitud"]);
                        SeleccionarMarca.observaciones = row["observaciones"].ToString();
                        SeleccionarMarca.tiene_poder = row["tiene_poder"].ToString();
                        SeleccionarMarca.pais_de_registro = row["pais_de_origen"].ToString();

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
                            txtNombreCliente.Text = cliente[0].nombre; // Asegúrate de que este es el control correcto
                        }

                        // Actualizar los controles de la interfaz con los datos obtenidos
                        txtExpediente.Text = SeleccionarMarca.expediente;
                        txtNombre.Text = SeleccionarMarca.nombre;
                        txtClase.Text = SeleccionarMarca.clase;
                        textBoxEstatus.Text = SeleccionarMarca.estado;
                        txtSignoDistintivo.Text = SeleccionarMarca.signoDistintivo;
                        MostrarLogoEnPictureBox(SeleccionarMarca.logo);
                        datePickerFechaSolicitud.Value = SeleccionarMarca.fecha_solicitud; // Manejar fecha nula
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
            if (SeleccionarMarca.idN > 0)
            {
                try
                {
                    // Obtén los detalles de la marca de manera asíncrona
                    var detallesMarcaN = await Task.Run(() => marcaModel.GetMarcaNacionalById(SeleccionarMarca.idN));

                    if (detallesMarcaN.Count > 0)
                    {
                        // Actualiza los detalles de la marca en los controles de la interfaz
                        var detalle = detallesMarcaN[0]; // Supongamos que solo necesitas el primer resultado

                        textBoxEstatus.Text = detalle.estado;
                        richTextBox1.Text = detalle.observaciones;

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
                    //MessageBox.Show("Error al refrescar los datos de la marca: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            tabControl1.SelectedTab = tabPageIngresadasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageHistorialDetail);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

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

        private void roundedButton6_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePFecha_Registro_ValueChanged(object sender, EventArgs e)
        {

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEstatusH_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void btnEditarH_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarH_Click(object sender, EventArgs e)
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
        }
    }
}
