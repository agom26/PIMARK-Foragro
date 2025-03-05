using Comun.Cache;
using Dominio;
using FluentFTP;
using Presentacion.Alertas;
using Presentacion.Marcas_Nacionales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Presentacion.Patentes
{
    public partial class FrmMostrarRegistradasPatentes : Form
    {
        PatenteModel patenteModel = new PatenteModel();
        PersonaModel personaModel = new PersonaModel();
        HistorialPatenteModel historialPatenteModel = new HistorialPatenteModel();
        RenovacionesPatenteModel renovacionesModel = new RenovacionesPatenteModel();
        TraspasosPatenteModel traspasosModel = new TraspasosPatenteModel();
        private const int pageSize = 20;
        private int currentPageIndex = 1;
        private int totalPages = 0;
        private int totalRows = 0;
        bool agregoEstado = false;
        private bool buscando = false;


        //ftp
        private string host = "ftp.bpa.com.es"; // Tu host FTP
        private string usuario = "test@bpa.com.es"; // Tu usuario FTP
        private string contraseña = "2O1VsAbUGbUo"; // Tu contraseña FTP
        private string directorioBase = "/bpa.com.es/test/marcas/patentes";
        public FrmMostrarRegistradasPatentes()
        {
            InitializeComponent();
            this.Load += FrmMostrarRegistradasPatentes_Load;

        }
        private async Task LoadPatentes()
        {
            totalRows = patenteModel.GetTotalPatentesRegistradas();
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
            // Obtiene los usuarios
            var marcasN = await Task.Run(() => patenteModel.GetAllPatentesRegistradas(currentPageIndex, pageSize));

            Invoke(new Action(() =>
            {
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                dtgPatentes.DataSource = marcasN;

                if (dtgPatentes.Columns["id"] != null)
                {
                    dtgPatentes.Columns["id"].Visible = false;
                    dtgPatentes.ClearSelection();
                }

            }));
        }

        public async void filtrar()
        {
            string buscar = txtBuscar.Text;
            if (buscar != "")
            {
                totalRows = patenteModel.GetFilteredPatentesRegistradasCount(txtBuscar.Text);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                lblTotalPages.Text = totalPages.ToString();
                lblTotalRows.Text = totalRows.ToString();
                DataTable titulares = patenteModel.FiltrarPatentesRegistradas(buscar, currentPageIndex, pageSize);
                if (titulares.Rows.Count > 0)
                {
                    dtgPatentes.DataSource = titulares;
                    if (dtgPatentes.Columns["id"] != null)
                    {
                        dtgPatentes.Columns["id"].Visible = false;
                    }
                    dtgPatentes.ClearSelection();
                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO EXISTEN PATENTES CON ESOS DATOS", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                    alerta.ShowDialog();
                    //MessageBox.Show("No existen titulares con esos datos");
                    await LoadPatentes();
                }
            }
            else
            {
                await LoadPatentes();
            }
        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }
        private void VerificarSeleccionIdPatenteEdicion()
        {
            if (dtgPatentes.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgPatentes.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgPatentes.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarPatente.id = id;
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

        private void ActualizarFechaVencimiento()
        {
            DateTime fecha_solicitud = datePickerFechaSolicitud.Value;
            DateTime fecha_vencimiento = fecha_solicitud.AddYears(20);
            dateTimePFecha_vencimiento.Value = fecha_vencimiento;
        }
        public void mostrarPanelRegistro(string isRegistrada)
        {
            if (isRegistrada == "si")
            {
                //ActualizarFechaVencimiento();
                lblVencimiento.Visible = true;
                dateTimePFecha_vencimiento.Visible = true;
                checkBox2.Checked = true;
                checkBox2.Enabled = false;
                panel2I.Visible = true;
                tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[0].Height = 62.5f;
                tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[1].Height = 37.5f;
            }
            else
            {
                lblVencimiento.Visible = false;
                dateTimePFecha_vencimiento.Visible = false;
                checkBox2.Enabled = false;
                checkBox2.Checked = false;
                panel2I.Visible = false;
                tableLayoutPanel1.RowStyles[0].Height = 0;
            }
        }
        private async Task CargarDatosPatente()
        {
            try
            {
                DataTable detallesPatente = await Task.Run(() => patenteModel.ObtenerPatentePorId(SeleccionarPatente.id));

                if (detallesPatente.Rows.Count > 0)
                {
                    DataRow row = detallesPatente.Rows[0];

                    if (row["expediente"] != DBNull.Value)
                    {
                        SeleccionarPatente.caso = row["caso"].ToString();
                        SeleccionarPatente.expediente = row["expediente"].ToString();
                        SeleccionarPatente.nombre = row["nombre"].ToString();
                        SeleccionarPatente.tipo = row["tipo"].ToString();
                        SeleccionarPatente.anualidades = int.Parse(row["anualidades"].ToString());
                        SeleccionarPatente.pct = row["pct"].ToString();
                        SeleccionarPatente.fecha_solicitud = (DateTime)row["fecha_solicitud"];
                        
                        //MessageBox.Show(SeleccionarPatente.fecha_vencimiento.ToString());
                        SeleccionarPatente.estado = row["estado"].ToString();
                        SeleccionarPatente.idTitular = int.Parse(row["IdTitular"].ToString());
                        SeleccionarPatente.idAgente = int.Parse(row["IdAgente"].ToString());
                        SeleccionarPatente.comprobante_pagos = row["comprobante_pagos"].ToString();
                        SeleccionarPatente.descripcion = row["descripcion"].ToString();
                        SeleccionarPatente.reivindicaciones = row["reivindicaciones"].ToString();
                        SeleccionarPatente.dibujos = row["dibujos"].ToString();
                        SeleccionarPatente.resumen = row["resumen"].ToString();
                        SeleccionarPatente.documento_cesion = row["documento_cesion"].ToString();
                        SeleccionarPatente.poder_nombramiento = row["poder_nombramiento"].ToString();


                        if (row["Erenov"] != DBNull.Value)
                        {
                            SeleccionarPatente.Erenov = row["Erenov"].ToString();
                            txtERenovacion.Text = SeleccionarPatente.Erenov;
                        }
                        else
                        {
                            txtERenovacion.Text = "";
                        }

                        if (row["Etrasp"] != DBNull.Value)
                        {
                            SeleccionarPatente.Etrasp = row["Etrasp"].ToString();
                            txtETraspaso.Text = SeleccionarPatente.Etrasp;
                        }
                        else
                        {
                            txtETraspaso.Text = "";
                        }

                        var titularTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarPatente.idTitular));
                        var agenteTask = Task.Run(() => personaModel.GetPersonaById(SeleccionarPatente.idAgente));

                        await Task.WhenAll(titularTask, agenteTask);

                        var titular = titularTask.Result;
                        var agente = agenteTask.Result;

                        SeleccionarPersonaPatente.idPersonaT = SeleccionarPatente.idTitular;
                        SeleccionarPersonaPatente.idPersonaA = SeleccionarPatente.idAgente;

                        if (titular.Count > 0)
                        {

                            txtNombreTitular.Text = titular[0].nombre;
                            txtDireccionTitular.Text = titular[0].direccion;

                        }

                        if (agente.Count > 0)
                        {
                            txtNombreAgente.Text = agente[0].nombre;
                        }


                        // Actualizar los controles 
                        txtCaso.Text = SeleccionarPatente.caso;
                        txtExpediente.Text = SeleccionarPatente.expediente;
                        txtNombre.Text = SeleccionarPatente.nombre;
                        textBoxEstatus.Text = SeleccionarPatente.estado;
                        datePickerFechaSolicitud.Value = SeleccionarPatente.fecha_solicitud;
                        
                        comboBoxTipo.SelectedItem = SeleccionarPatente.tipo;
                        comboBoxAnualidades.SelectedItem = SeleccionarPatente.anualidades.ToString();

                        if (SeleccionarPatente.pct == "si")
                        {
                            checkBoxPCT.Checked = true;
                        }
                        else
                        {
                            checkBoxPCT.Checked = false;
                        }

                        // Recorrer cada ítem del CheckedListBox
                        for (int i = 0; i < checkedListBoxDocumentos.Items.Count; i++)
                        {
                            string itemName = checkedListBoxDocumentos.Items[i].ToString();

                            // Comparar el nombre del ítem con las propiedades de SeleccionarPatente
                            if (itemName == "Comprobante de pagos" && SeleccionarPatente.comprobante_pagos == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Descripción (original y 1 copia)" && SeleccionarPatente.descripcion == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Reivindicaciones (original y 1 copia)" && SeleccionarPatente.reivindicaciones == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Dibujo(s) o fórmula (original y 1 copia)" && SeleccionarPatente.dibujos == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Resumen (original y 1 copia)" && SeleccionarPatente.resumen == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Documento de cesión" && SeleccionarPatente.documento_cesion == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                            else if (itemName == "Poder o nombramiento" && SeleccionarPatente.poder_nombramiento == "si")
                            {
                                checkedListBoxDocumentos.SetItemChecked(i, true);
                            }
                        }

                        bool contieneRegistrada = false;

                        if (SeleccionarPatente.estado.Contains("Registro/concesión", StringComparison.OrdinalIgnoreCase) || SeleccionarPatente.estado.Contains("Trámite de renovación", StringComparison.OrdinalIgnoreCase) || SeleccionarPatente.estado.Contains("Trámite de traspaso", StringComparison.OrdinalIgnoreCase))
                        {
                            contieneRegistrada = true;
                        }
                        else
                        {
                            contieneRegistrada = false;
                        }


                        if (contieneRegistrada)
                        {

                            if (row["registro"].ToString() != null)
                            {
                                SeleccionarPatente.registro = row["registro"].ToString();
                                SeleccionarPatente.folio = row["folio"].ToString();
                                SeleccionarPatente.libro = row["libro"].ToString();
                                SeleccionarPatente.fecha_registro = Convert.ToDateTime(row["fecha_registro"]);
                                SeleccionarPatente.fecha_vencimiento = Convert.ToDateTime(row["fecha_vencimiento"]);

                                txtRegistro.Text = SeleccionarPatente.registro;
                                txtFolio.Text = SeleccionarPatente.folio;
                                txtLibro.Text = SeleccionarPatente.libro;
                                dateTimePFecha_Registro.Value = SeleccionarPatente.fecha_registro.Value;
                                dateTimePFecha_vencimiento.Value = SeleccionarPatente.fecha_vencimiento.Value;
                            }
                            checkBox2.Checked = true;
                            mostrarPanelRegistro("si");
                        }
                        else
                        {
                            checkBox2.Checked = false;
                            mostrarPanelRegistro("no");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la patente seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles de la patente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarCampo(string campo)
        {
            return !string.IsNullOrEmpty(campo);
        }


        private bool ValidarCampos(string caso, string expediente, string nombre, string tipo, string anualidad, string estado,
                    bool registroChek, string registro, string folio, string libro)
        {
            // Lista para acumular mensajes de error
            List<string> mensajesError = new List<string>();

            // Validaciones de campos requeridos
            if (!ValidarCampo(caso))
                mensajesError.Add("INGRESE EL CASO\n");
            if (!ValidarCampo(expediente))
                mensajesError.Add("INGRESE EL EXPEDIENTE\n");
            if (!ValidarCampo(nombre))
                mensajesError.Add("INGRESE EL SIGNO\n");
            if (!ValidarCampo(tipo))
                mensajesError.Add("SELECCIONE UN TIPO\n");
            if (!ValidarCampo(anualidad))
                mensajesError.Add("SELECCIONE UN NÚMERO DE ANUALIDAD\n");
            if (!ValidarCampo(estado))
                mensajesError.Add("SELECCIONE UN ESTADO\n");

            // Validación de valores numéricos 

            if (!int.TryParse(anualidad, out _))
                mensajesError.Add("LA ANUALIDAD DEBE SER UN VALOR NUMÉRICO\n");

            if (registroChek)
            {
                if (!int.TryParse(registro, out _))
                    mensajesError.Add("EL REGISTRO DEBE SER UN VALOR NUMÉRICO\n");
                if (!int.TryParse(folio, out _))
                    mensajesError.Add("EL FOLIO DEBE SER UN VALOR NUMÉRICO\n");
                if (!int.TryParse(libro, out _))
                    mensajesError.Add("EL TOMO DEBE SER UN VALOR NUMÉRICO\n");
            }

            // Validación de campos de registro 
            if (registroChek)
            {
                if (!ValidarCampo(folio))
                    mensajesError.Add("INGRESE EL NÚMERO DE FOLIO\n");
                if (!ValidarCampo(registro))
                    mensajesError.Add("INGRESE EL NÚMERO DE REGISTRO\n");
                if (!ValidarCampo(libro))
                    mensajesError.Add("INGRESE EL NÚMERO DE TOMO\n");
            }

            // Si hay mensajes de error, mostrar la alerta con todos los mensajes
            if (mensajesError.Any())
            {
                string mensajeConcatenado = string.Join("", mensajesError);
                FrmAlerta alerta = new FrmAlerta(mensajeConcatenado, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return false;
            }

            return true;
        }

        public void LimpiarFomulario()
        {
            txtCaso.Text = "";
            txtExpediente.Text = "";
            txtNombre.Text = "";
            comboBoxTipo.SelectedIndex = -1;
            comboBoxAnualidades.SelectedIndex = -1;
            checkBoxPCT.Checked = false;
            datePickerFechaSolicitud.Value = DateTime.Now;
            AgregarEtapaPatente.LimpiarEtapa();
            textBoxEstatus.Text = "";
            SeleccionarPersonaPatente.LimpiarPersona();
            checkedListBoxDocumentos.ClearSelected();
            txtFolio.Text = "";
            txtLibro.Text = "";
            txtRegistro.Text = "";
            dateTimePFecha_Registro.Value = DateTime.Now;
            mostrarPanelRegistro("no");
            checkBoxPCT.Checked = false;
            txtNombreAgente.Text = "";
            txtDireccionTitular.Text = "";
            txtNombreTitular.Text = "";
            SeleccionarPersonaPatente.LimpiarPersona();
            ActualizarFechaVencimiento();
            checkBox2.Checked = false;
            checkBox2.Checked = false;
        }

        public async void EditarPatente()
        {
            string caso = txtCaso.Text;
            string expediente = txtExpediente.Text;
            string nombre = txtNombre.Text;
            string tipo = comboBoxTipo.SelectedItem?.ToString();
            string anualidad = comboBoxAnualidades.SelectedItem?.ToString();
            int anualidades = int.Parse(anualidad);
            string folio = txtFolio.Text;
            string libro = txtLibro.Text;
            int idTitular = SeleccionarPersonaPatente.idPersonaT;
            int idAgente = SeleccionarPersonaPatente.idPersonaA;
            DateTime solicitud = datePickerFechaSolicitud.Value;
            string pct = "no";
            string estado = textBoxEstatus.Text;
            bool registroChek = checkBox2.Checked;
            string registro = txtRegistro.Text;
            DateTime fecha_registro = dateTimePFecha_Registro.Value;
            DateTime fecha_vencimiento = dateTimePFecha_vencimiento.Value;
            string etrasp = txtETraspaso.Text;
            string erenov = txtERenovacion.Text;
            string comprobante_pagos = "no";
            string descripcion = "no";
            string reivindicaciones = "no";
            string dibujos = "no";
            string resumen = "no";
            string documento_cesion = "no";
            string poder_nombramiento = "no";


            // Validaciones
            if (idTitular <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("INGRESE UN TITULAR VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un titular válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idAgente <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("INGRESE UN AGENTE VÁLIDO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Por favor, seleccione un agente válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (estado == "Trámite de renovación" && string.IsNullOrEmpty(erenov))
            {
                FrmAlerta alerta = new FrmAlerta("POR FAVOR INGRESE EL NÚMERO DE TRÁMITE DE RENOVACIÓN", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                return;
            }

            if (estado == "Trámite de traspaso" && string.IsNullOrEmpty(etrasp))
            {
                FrmAlerta alerta = new FrmAlerta("POR FAVOR INGRESE EL NÚMERO DE TRÁMITE DE TRASPASO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();

                return;
            }


            if (checkBoxPCT.Checked)
            {
                pct = "si";
            }

            // Validar las selecciones en el CheckedListBox
            if (checkedListBoxDocumentos.CheckedItems.Contains("Comprobante de pagos"))
            {
                comprobante_pagos = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Descripción (original y 1 copia)"))
            {
                descripcion = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Reivindicaciones (original y 1 copia)"))
            {
                reivindicaciones = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Dibujo(s) o fórmula (original y 1 copia)"))
            {
                dibujos = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Resumen (original y 1 copia)"))
            {
                resumen = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Documento de cesión"))
            {
                documento_cesion = "si";
            }

            if (checkedListBoxDocumentos.CheckedItems.Contains("Poder o nombramiento"))
            {
                poder_nombramiento = "si";
            }



            // Validar campos 
            if (!ValidarCampos(caso, expediente, nombre, tipo, anualidad, estado, registroChek, registro, folio, libro))
            {
                return;
            }

            try
            {
                if (registroChek)
                {
                    try
                    {
                        if (agregoEstado == true)
                        {
                            historialPatenteModel.CrearHistorialPatente((DateTime)AgregarEtapaPatente.fecha, AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones, AgregarEtapaPatente.usuario, null, SeleccionarPatente.id);
                            agregoEstado = false;
                        }

                        bool actualizada = patenteModel.EditarPatente(SeleccionarPatente.id, caso, expediente, nombre, estado, tipo, idTitular, idAgente, solicitud,
                            registro, folio, libro, fecha_registro, fecha_vencimiento, erenov, etrasp, anualidades, pct,
                            comprobante_pagos, descripcion, reivindicaciones, dibujos, resumen, documento_cesion,
                            poder_nombramiento);

                        FrmAlerta alerta = new FrmAlerta("PATENTE ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        LimpiarFomulario();
                        DatosRegistro.peligro = false;
                        AnadirTabPage(tabPageIngresadasList);
                        EliminarTabPage(tabPageMarcaDetail);
                        EliminarTabPage(tabPageListaArchivos);
                        EliminarTabPage(tabPageHistorialMarca);
                        tabControl1.SelectedTab = tabPageIngresadasList;
                        await LoadPatentes();
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }
                }
                else
                {
                    try
                    {
                        if (agregoEstado == true)
                        {
                            historialPatenteModel.CrearHistorialPatente((DateTime)AgregarEtapaPatente.fecha, AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones, AgregarEtapaPatente.usuario, null, SeleccionarPatente.id);
                            agregoEstado = false;
                        }

                        bool actualizada = patenteModel.EditarPatente(SeleccionarPatente.id, caso, expediente, nombre, estado, tipo, idTitular, idAgente, solicitud,
                            null, null, null, null, null, null, null, anualidades, pct,
                            comprobante_pagos, descripcion, reivindicaciones, dibujos, resumen, documento_cesion,
                            poder_nombramiento);
                        FrmAlerta alerta = new FrmAlerta("PATENTE ACTUALIZADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        alerta.ShowDialog();
                        LimpiarFomulario();
                        DatosRegistro.peligro = false;
                        AnadirTabPage(tabPageIngresadasList);
                        EliminarTabPage(tabPageMarcaDetail);
                        EliminarTabPage(tabPageListaArchivos);
                        EliminarTabPage(tabPageHistorialMarca);
                        tabControl1.SelectedTab = tabPageIngresadasList;
                        await LoadPatentes();
                    }
                    catch (Exception ex)
                    {
                        FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        alerta.ShowDialog();
                    }
                }


                //LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al " + (registroChek ? "registrar" : "actualizar") + " la marca nacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //LimpiarFormulario();
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
        private async void loadHistorialById()
        {
            try
            {
                var historial = await Task.Run(() => historialPatenteModel.ObtenerHistorialPorIdPatente(SeleccionarPatente.id));


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
                MessageBox.Show("Error al cargar el historial de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private async void loadRenovacionesById()
        {
            try
            {
                DataTable renovaciones = await Task.Run(() => renovacionesModel.GetAllRenovacionesByIdPatente(SeleccionarPatente.id));

                Invoke(new Action(() =>
                {
                    dtgRenovaciones.AutoGenerateColumns = true;
                    dtgRenovaciones.DataSource = renovaciones;
                    dtgRenovaciones.Refresh();

                    if (dtgRenovaciones.Columns["id"] != null)
                    {
                        dtgRenovaciones.Columns["id"].Visible = false;
                        dtgRenovaciones.Columns["IdPatente"].Visible = false;
                    }

                    dtgRenovaciones.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las renovaciones de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task refrescarMarca()
        {
            if (SeleccionarPatente.id > 0)
            {
                try
                {
                    DataTable detallesPatente = await Task.Run(() => patenteModel.ObtenerPatentePorId(SeleccionarPatente.id));

                    if (detallesPatente.Rows.Count > 0)
                    {
                        DataRow row = detallesPatente.Rows[0];

                        bool contieneRegistrada = false;

                        if (SeleccionarPatente.estado.Contains("Registro/concesión", StringComparison.OrdinalIgnoreCase) || SeleccionarPatente.estado.Contains("Trámite de renovación", StringComparison.OrdinalIgnoreCase) || SeleccionarPatente.estado.Contains("Trámite de traspaso", StringComparison.OrdinalIgnoreCase))
                        {
                            contieneRegistrada = true;
                        }
                        else
                        {
                            contieneRegistrada = false;
                        }


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


        private async void loadTraspasosById()
        {
            try
            {
                DataTable traspasos = await Task.Run(() => traspasosModel.ObtenerTraspasosPatentePorIdPatente(SeleccionarPatente.id));


                Invoke(new Action(() =>
                {
                    dtgTraspasos.AutoGenerateColumns = true;
                    dtgTraspasos.DataSource = traspasos;
                    dtgTraspasos.Refresh();

                    if (dtgTraspasos.Columns["id"] != null)
                    {
                        dtgTraspasos.Columns["id"].Visible = false;
                        dtgTraspasos.Columns["IdPatente"].Visible = false;
                        dtgTraspasos.Columns["IdTitularAnterior"].Visible = false;
                        dtgTraspasos.Columns["IdTitularNuevo"].Visible = false;
                    }

                    dtgTraspasos.ClearSelection();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los traspasos de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FrmMostrarRegistradasPatentes_Load(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            await Task.Run(() => LoadPatentes());
            tabControl1.SelectedTab = tabPageIngresadasList;
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageHistorialDetail);
            EliminarTabPage(tabPageHistorialMarca);
            EliminarTabPage(tabPageRenovacionesList);
            EliminarTabPage(tabPageRenovacionDetail);
            EliminarTabPage(tabPageTraspasosList);
            EliminarTabPage(tabPageTraspasoDetail);
            EliminarTabPage(tabPageListaArchivos);
            tabControl1.Visible = true;

            currentPageIndex = 1;
            lblCurrentPage.Text = currentPageIndex.ToString();

        }
        public async void Editar()
        {
            VerificarSeleccionIdPatenteEdicion();
            if (SeleccionarPatente.id > 0)
            {
                await CargarDatosPatente();
                AnadirTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageIngresadasList);
            }
        }
        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnGuardarM_Click(object sender, EventArgs e)
        {

        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabPageHistorialMarca)
            {
                loadHistorialById();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
            }
            else if (tabControl1.SelectedTab == tabPageIngresadasList)
            {
                await LoadPatentes();
                SeleccionarPatente.id = 0;
                
                EliminarTabPage(tabPageMarcaDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
                EliminarTabPage(tabPageListaArchivos);
                DatosRegistro.peligro = false;
            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                await CargarDatosPatente();
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
                EliminarTabPage(tabPageListaArchivos);
            }
            else if (tabControl1.SelectedTab == tabPageRenovacionesList)
            {
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionDetail);
                EliminarTabPage(tabPageTraspasosList);
                EliminarTabPage(tabPageTraspasoDetail);
            }
            else if (tabControl1.SelectedTab == tabPageTraspasosList)
            {
                EliminarTabPage(tabPageHistorialDetail);
                EliminarTabPage(tabPageHistorialMarca);
                EliminarTabPage(tabPageRenovacionesList);
                EliminarTabPage(tabPageRenovacionDetail);
            }
        }

        private async void roundedButton8_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                await Task.Run(() => loadHistorialById());
                AnadirTabPage(tabPageHistorialMarca);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            if (dtgHistorial.SelectedRows.Count > 0)
            {
                Deshabilitar();
                var filaSeleccionada = dtgHistorial.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {

                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorialPatente.id = id;

                    DataTable historial = historialPatenteModel.ObtenerHistorialPorId(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];

                        SeleccionarHistorialPatente.id = Convert.ToInt32(fila["id"]);
                        SeleccionarHistorialPatente.etapa = fila["etapa"].ToString();
                        SeleccionarHistorialPatente.fecha = (DateTime)fila["fecha"];
                        SeleccionarHistorialPatente.anotaciones = fila["anotaciones"].ToString();
                        SeleccionarHistorialPatente.usuario = fila["usuario"].ToString();
                        SeleccionarHistorialPatente.usuarioEdicion = fila["usuarioEdicion"].ToString();

                        comboBoxEstatusH.SelectedItem = SeleccionarHistorialPatente.etapa;
                        dateTimePickerFechaH.Value = SeleccionarHistorialPatente.fecha;
                        richTextBoxAnotacionesH.Text = SeleccionarHistorialPatente.anotaciones;
                        labelUserEditor.Text = UsuarioActivo.usuario;
                        lblUser.Text = SeleccionarHistorialPatente.usuario;

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

        private void btnCancelarH_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageHistorialMarca;
        }
        public void EditarHistorial()
        {
            string etapa = comboBoxEstatusH.SelectedItem?.ToString();
            DateTime fecha = dateTimePickerFechaH.Value;
            string anotaciones = richTextBoxAnotacionesH.Text;
            SeleccionarHistorialPatente.anotaciones = anotaciones;
            string usuario = lblUser.Text;
            string usuarioEditor = labelUserEditor.Text;


            if (comboBoxEstatusH.SelectedIndex != -1)
            {
                string fechaSinHora = dateTimePickerFechaH.Value.ToShortDateString();
                string formato = fechaSinHora + " " + comboBoxEstatusH.SelectedItem.ToString();
                if (anotaciones.Contains(formato))
                {
                    AgregarEtapaPatente.anotaciones = anotaciones;
                }
                else
                {
                    AgregarEtapaPatente.anotaciones = formato + " " + anotaciones;
                }

                try
                {
                    historialPatenteModel.EditarHistorialPatente(SeleccionarHistorialPatente.id, fecha, etapa, AgregarEtapaPatente.anotaciones, usuario, usuarioEditor);
                    FrmAlerta alerta = new FrmAlerta("ESTADO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();
                    tabControl1.SelectedTab = tabPageHistorialMarca;
                    SeleccionarHistorialPatente.LimpiarHistorial();
                    refrescarMarca();
                }
                catch (Exception ex)
                {
                    FrmAlerta alerta = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("SELECCIONE UN ESTADO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No ha seleccionado ningun estado");
            }
        }

        private void btnEditarH_Click(object sender, EventArgs e)
        {


        }

        private void btnEditarEstadoHistorial_Click(object sender, EventArgs e)
        {
            if (dtgHistorial.SelectedRows.Count > 0)
            {
                Habilitar();
                var filaSeleccionada = dtgHistorial.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarHistorialPatente.id = id;

                    DataTable historial = historialPatenteModel.ObtenerHistorialPorId(id);

                    if (historial.Rows.Count > 0)
                    {
                        DataRow fila = historial.Rows[0];
                        SeleccionarHistorialPatente.id = Convert.ToInt32(fila["id"]);
                        SeleccionarHistorialPatente.etapa = fila["etapa"].ToString();
                        SeleccionarHistorialPatente.fecha = (DateTime)fila["fecha"];
                        SeleccionarHistorialPatente.anotaciones = fila["anotaciones"].ToString();
                        SeleccionarHistorialPatente.usuario = fila["usuario"].ToString();
                        SeleccionarHistorialPatente.usuarioEdicion = fila["usuarioEdicion"].ToString();

                        comboBoxEstatusH.SelectedItem = SeleccionarHistorialPatente.etapa;
                        dateTimePickerFechaH.Value = SeleccionarHistorialPatente.fecha;
                        richTextBoxAnotacionesH.Text = SeleccionarHistorialPatente.anotaciones;
                        labelUserEditor.Text = UsuarioActivo.usuario;
                        lblUser.Text = SeleccionarHistorialPatente.usuario;

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

        private void dateTimePickerFechaH_ValueChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void comboBoxEstatusH_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBoxAnotacionesH.Text = dateTimePickerFechaH.Value.ToShortDateString() + " " + comboBoxEstatusH.SelectedItem;
        }

        private void btnCancelarM_Click(object sender, EventArgs e)
        {

        }
        public void VerificarDatosRegistro()
        {
            if (checkBox2.Checked == true && (string.IsNullOrEmpty(txtRegistro.Text) || string.IsNullOrEmpty(txtFolio.Text) || string.IsNullOrEmpty(txtLibro.Text)))
            {
                DatosRegistro.peligro = true;
            }
            else
            {
                DatosRegistro.peligro = false;
            }
        }
        private async void roundedButton6_Click(object sender, EventArgs e)
        {
            FrmAgregarEtapaRegistradaPatente frmAgregarEtapa = new FrmAgregarEtapaRegistradaPatente();
            frmAgregarEtapa.ShowDialog();

            if (AgregarEtapaPatente.etapa != "")
            {
                try
                {
                    agregoEstado = true;
                    textBoxEstatus.Text = AgregarEtapaPatente.etapa;
                    //historialPatenteModel.CrearHistorialPatente((DateTime)AgregarEtapaPatente.fecha, AgregarEtapaPatente.etapa, AgregarEtapaPatente.anotaciones, AgregarEtapaPatente.usuario, null, SeleccionarPatente.id);

                    FrmAlerta alerta = new FrmAlerta("ESTADO AGREGADO CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.ShowDialog();

                    if (AgregarEtapaPatente.etapa == "Registro/concesión" || AgregarEtapaPatente.etapa == "Trámite de renovación" || AgregarEtapaPatente.etapa == "Trámite de traspaso")
                    {
                        checkBox2.Checked = true;
                        mostrarPanelRegistro("si");
                        VerificarDatosRegistro();
                    }
                    else
                    {
                        checkBox2.Checked = false;
                        mostrarPanelRegistro("no");
                    }
                    //await refrescarMarca();
                    //await CargarDatosPatente();


                    if (AgregarEtapaPatente.etapa == "Trámite de renovación" && AgregarEtapaPatente.numExpediente != "0")
                    {
                        txtERenovacion.Text = AgregarEtapaPatente.numExpediente.ToString();
                        txtERenovacion.Enabled = true;
                        /*
                        try
                        {
                            patenteModel.InsertarExpedientePatente(AgregarEtapaPatente.numExpediente.ToString(), SeleccionarPatente.id, "renovacion");

                        }
                        catch (Exception ex)
                        {
                            FrmAlerta alerta2 = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta2.ShowDialog();
                        }*/

                    }
                    else if (AgregarEtapaPatente.etapa == "Trámite de traspaso" && AgregarEtapaPatente.numExpediente != "0")
                    {
                        txtETraspaso.Text = AgregarEtapaPatente.numExpediente.ToString();
                        txtETraspaso.Enabled = true;
                        /*
                        try
                        {
                            patenteModel.InsertarExpedientePatente(AgregarEtapaPatente.numExpediente.ToString(), SeleccionarPatente.id, "traspaso");

                        }
                        catch (Exception ex)
                        {
                            FrmAlerta alerta2 = new FrmAlerta(ex.Message.ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alerta2.ShowDialog();
                        }*/
                    }
                    else
                    {
                        txtERenovacion.Enabled = false;
                        txtETraspaso.Enabled = false;
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void roundedButton10_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                loadRenovacionesById();
                AnadirTabPage(tabPageRenovacionesList);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR LOS DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void btnEditarRenovacion_Click(object sender, EventArgs e)
        {
            if (dtgRenovaciones.SelectedRows.Count > 0)
            {
                //Habilitar();
                var filaSeleccionada = dtgRenovaciones.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarRenovacionPatente.idRenovacion = id;

                    DataTable renovacion = renovacionesModel.GetRenovacionById(id);

                    if (renovacion.Rows.Count > 0)
                    {
                        DataRow fila = renovacion.Rows[0];

                        SeleccionarRenovacionPatente.idRenovacion = Convert.ToInt32(fila["Id"]);
                        //SeleccionarRenovacion.Reg_Antiguo = (DateTime)fila["FechaRegistroAntigua"];
                        //SeleccionarRenovacion.Reg_nuevo = (DateTime)fila["FechaRegistroNueva"];
                        SeleccionarRenovacionPatente.Venc_antiguo = (DateTime)fila["FechaVencimientoAntigua"];
                        SeleccionarRenovacionPatente.Venc_nuevo = (DateTime)fila["FechaVencimientoNueva"];
                        SeleccionarRenovacionPatente.NumExpediente = fila["NumExpediente"].ToString();
                        SeleccionarRenovacionPatente.IdPatente = Convert.ToInt32(fila["IdPatente"]);
                        //Asignar valores a controles
                        txtNoExpediente.Text = SeleccionarRenovacionPatente.NumExpediente;

                        dateFechVencAnt.Value = SeleccionarRenovacionPatente.Venc_antiguo;
                        dateFechVencNueva.Value = SeleccionarRenovacionPatente.Venc_nuevo;

                        AnadirTabPage(tabPageRenovacionDetail);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles de la renovacion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string numExpediente = txtNoExpediente.Text;


            DateTime fechaVencimientoA = dateFechVencAnt.Value;
            DateTime fechaVencimientoN = dateFechVencNueva.Value;
            int id = SeleccionarRenovacionPatente.idRenovacion;
            int idPatente = SeleccionarRenovacionPatente.IdPatente;

            if (!string.IsNullOrEmpty(numExpediente))
            {
                bool actualizado = renovacionesModel.ActualizarRenovacion(id, numExpediente, idPatente, fechaVencimientoA, fechaVencimientoN);

                if (actualizado)
                {
                    loadRenovacionesById();
                    FrmAlerta alerta = new FrmAlerta("RENOVACIÓN ACTUALIZADA CORRECTAMENTE", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alerta.Show();
                    //MessageBox.Show("Registro de renovación actualizado exitosamente.");
                    tabControl1.SelectedTab = tabPageRenovacionesList;

                }
                else
                {
                    FrmAlerta alerta = new FrmAlerta("NO FUE POSIBLE ACTUALIZAR LA RENOVACIÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    alerta.Show();
                    //MessageBox.Show("No se pudo actualizar el registro de renovación.");
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("EL NÚMERO DE EXPEDIENTE NO PUEDE ESTAR VACÍO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.Show();
                //MessageBox.Show("El número de expediente no puede estar vacío.");
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageRenovacionesList;
        }

        private void roundedButton9_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                loadTraspasosById();
                AnadirTabPage(tabPageTraspasosList);
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private void btnEditarTraspaso_Click(object sender, EventArgs e)
        {
            if (dtgTraspasos.SelectedRows.Count > 0)
            {
                //Habilitar();
                var filaSeleccionada = dtgTraspasos.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    // Obtén el ID de la fila seleccionada
                    int id = Convert.ToInt32(dataRowView["id"]);
                    SeleccionarTraspasoPatente.id = id;

                    DataTable traspaso = traspasosModel.ObtenerTraspasoPorId(id);

                    if (traspaso.Rows.Count > 0)
                    {
                        DataRow fila = traspaso.Rows[0];

                        SeleccionarTraspasoPatente.id = Convert.ToInt32(fila["Id"]);
                        SeleccionarTraspasoPatente.IdPatente = Convert.ToInt32(fila["IdPatente"]);
                        SeleccionarTraspasoPatente.numExpediente = fila["NumExpediente"].ToString();
                        SeleccionarTraspasoPatente.idTitularA = Convert.ToInt32(fila["IdTitularAnterior"]);
                        SeleccionarTraspasoPatente.nombreTitularA = fila["TitularAnterior"].ToString();
                        SeleccionarTraspasoPatente.idTitularN = Convert.ToInt32(fila["IdTitularNuevo"]);
                        SeleccionarTraspasoPatente.nombreTitularN = fila["TitularNuevo"].ToString();
                        //Asignar valores a controles
                        txtNumExpedienteTraspaso.Text = SeleccionarTraspasoPatente.numExpediente;

                        txtNombreTitularA.Text = SeleccionarTraspasoPatente.nombreTitularA;
                        txtNombreTitularN.Text = SeleccionarTraspasoPatente.nombreTitularN;

                        AnadirTabPage(tabPageTraspasoDetail);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles del traspaso", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            string nombreTitularAntiguo = txtNombreTitularA.Text.Trim();
            string nombreTitularNuevo = txtNombreTitularN.Text.Trim();
            string numeroExpediente = txtNumExpedienteTraspaso.Text.Trim();


            int idTraspaso = SeleccionarTraspasoPatente.id;
            int idMarca = SeleccionarTraspasoPatente.IdPatente;
            int idTitularAntiguo = SeleccionarTraspasoPatente.idTitularA;
            int idTitularNuevo = SeleccionarTraspasoPatente.idTitularN;


            if (!string.IsNullOrEmpty(numeroExpediente) &&

                !string.IsNullOrEmpty(nombreTitularAntiguo) &&
                !string.IsNullOrEmpty(nombreTitularNuevo))
            {

                traspasosModel.ActualizarTraspaso(idTraspaso, numeroExpediente, idMarca, idTitularAntiguo, idTitularNuevo);
                FrmAlerta alerta = new FrmAlerta("TRASPASO ACTUALIZADO", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alerta.ShowDialog();
                loadTraspasosById();
                //MessageBox.Show("Traspaso actualizado correctamente");
                tabControl1.SelectedTab = tabPageTraspasosList;
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE LLENAR TODOS LOS CAMPOS", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
                //MessageBox.Show("Debe llenar todos los campos para poder actualizar el traspaso", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageTraspasosList;
        }

        private void btnAgregarTitularA_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesEdicionTraspasoPatente frmCrearTraspaso = new FrmMostrarTitularesEdicionTraspasoPatente();
            frmCrearTraspaso.ShowDialog();

            if (SeleccionarTraspasoPatente.idComodin != 0)
            {
                SeleccionarTraspasoPatente.idTitularA = SeleccionarTraspasoPatente.idComodin;
                SeleccionarTraspasoPatente.nombreTitularA = SeleccionarTraspasoPatente.nombreComodin;
                txtNombreTitularA.Text = SeleccionarTraspasoPatente.nombreTitularA;

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ UN TITULAR ANTIGUO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No selecciono un titular antiguo");
            }
        }

        private void btnAgregarTitularN_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesEdicionTraspasoPatente frmCrearTraspaso = new FrmMostrarTitularesEdicionTraspasoPatente();
            frmCrearTraspaso.ShowDialog();

            if (SeleccionarTraspasoPatente.idComodin != 0)
            {
                SeleccionarTraspasoPatente.idTitularN = SeleccionarTraspasoPatente.idComodin;
                SeleccionarTraspasoPatente.nombreTitularN = SeleccionarTraspasoPatente.nombreComodin;
                txtNombreTitularN.Text = SeleccionarTraspasoPatente.nombreTitularN;

            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO SELECCIONÓ UN TITULAR NUEVO", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No selecciono un titular nuevo");
            }
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }

        private void datePickerFechaSolicitud_ValueChanged(object sender, EventArgs e)
        {
            ActualizarFechaVencimiento();

        }

        private void roundedButton5_Click(object sender, EventArgs e)
        {
            FrmMostrarTitularesPatentes frmMostrarTitulares = new FrmMostrarTitularesPatentes();
            frmMostrarTitulares.ShowDialog();

            if (SeleccionarPersonaPatente.idPersonaT != 0)
            {
                txtNombreTitular.Text = SeleccionarPersonaPatente.nombre;
                txtDireccionTitular.Text = SeleccionarPersonaPatente.direccion;
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            FrmMostrarAgentesPatente frmMostrarAgentes = new FrmMostrarAgentesPatente();
            frmMostrarAgentes.ShowDialog();

            if (SeleccionarPersonaPatente.idPersonaA != 0)
            {
                txtNombreAgente.Text = SeleccionarPersonaPatente.nombre;

            }
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            if (dtgPatentes.SelectedRows.Count > 0)
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


                            var filaSeleccionada = dtgPatentes.SelectedRows[0];


                            if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                            {

                                int idPatente = Convert.ToInt32(dataRowView["id"]);

                                historialPatenteModel.CrearHistorialPatente(

                                    fechaAbandono,
                                    "Abandono",
                                    fechaAbandono.ToShortDateString() + " Abandono " + justificacion,
                                    usuarioAbandono,
                                    null,
                                    idPatente
                                );

                                FrmAlerta alerta = new FrmAlerta("LA PATENTE HA SIDO MARCADA COMO ABANDONADA", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                alerta.ShowDialog();

                                await LoadPatentes();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al actualizar el estado de la patente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("NO HA SELECCIONADO UNA PATENTE PARA ABANDONAR", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                alerta.ShowDialog();
            }
        }

        private void dtgPatentes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Editar();
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            VerificarDatosRegistro();
            if (DatosRegistro.peligro == false)
            {
                EditarPatente();
                DatosRegistro.peligro = false;
                
            }
            else
            {
                FrmAlerta alerta = new FrmAlerta("DEBE INGRESAR DATOS DE REGISTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                alerta.ShowDialog();
            }

        }

        private async void iconButton11_Click(object sender, EventArgs e)
        {
            
            LimpiarFomulario();
            DatosRegistro.peligro = false;
            SeleccionarPatente.Erenov = null;
            SeleccionarPatente.Etrasp = null;
            AnadirTabPage(tabPageIngresadasList);
            EliminarTabPage(tabPageMarcaDetail);
            EliminarTabPage(tabPageListaArchivos);
            EliminarTabPage(tabPageHistorialMarca);
            tabControl1.SelectedTab = tabPageIngresadasList;
            await LoadPatentes();


        }

        private void txtNombreMarcaN_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconButton12_Click(object sender, EventArgs e)
        {
            buscando = false;
            txtBuscar.Text = "";
            filtrar();
        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {
            buscando = true;
            currentPageIndex = 1;
            totalRows = patenteModel.GetFilteredPatentesRegistradasCount(txtBuscar.Text);
            totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            lblCurrentPage.Text = currentPageIndex.ToString();
            lblTotalPages.Text = totalPages.ToString();
            lblTotalRows.Text = totalRows.ToString();
            filtrar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscando = true;
                currentPageIndex = 1;
                totalRows = patenteModel.GetFilteredPatentesRegistradasCount(txtBuscar.Text);
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
            if (buscando==true)
            {
                filtrar();
            }
            else
            {
                await LoadPatentes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPageIndex > 1)
            {
                currentPageIndex--;
                if (buscando==true)
                {
                    filtrar();
                }
                else
                {
                    await LoadPatentes();
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
                    await LoadPatentes();
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
                await LoadPatentes();
            }

            lblCurrentPage.Text = currentPageIndex.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtERenovacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRegistro_TextChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true && string.IsNullOrEmpty(txtRegistro.Text))
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
            if (checkBox2.Checked == true && string.IsNullOrEmpty(txtFolio.Text))
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
            if (checkBox2.Checked == true && string.IsNullOrEmpty(txtLibro.Text))
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
            string carpetaMarca = $"{directorioBase}/patente-{idMarca}";
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

                string id = "" + SeleccionarPatente.id;
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
            string carpeta = $"{directorioBase}/patente-{idMarca}/";
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
            string idMarca = "" + SeleccionarPatente.id; // Id de la marca actual
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
            string carpeta = $"{directorioBase}/patente-{idMarca}/";
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
            string idMarca = "" + SeleccionarPatente.id; // Id de la marca actual
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
            string carpetaMarca = $"{directorioBase}/patente-{idMarca}"; // Ruta completa para la carpeta de la marca

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
            string carpeta = $"{directorioBase}/patente-{idMarca}/";
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
        private void roundedButton13_Click(object sender, EventArgs e)
        {
            ListarArchivosEnGeneral();
        }

        private void iconButton16_Click(object sender, EventArgs e)
        {
            SubirArchivo("" + SeleccionarPatente.id);
            ListarArchivosEnGeneral();
        }

        private void iconButton15_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void dtgArchivos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Abrir();
        }

        private void iconButton14_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMarcaDetail;
        }
    }
}
