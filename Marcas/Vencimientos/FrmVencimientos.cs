using AccesoDatos.ServiciosEmail;
using Dominio;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun.Cache;
using Presentacion.Alertas;

namespace Presentacion.Vencimientos
{
    public partial class FrmVencimientos : Form
    {
        VencimientoModel vencimientoModel = new VencimientoModel();
        MarcaModel marcaModel = new MarcaModel();
        PatenteModel patenteModel = new PatenteModel();
        PersonaModel personaModel = new PersonaModel();
        private bool isSendingEmail = false;
        public FrmVencimientos()
        {
            InitializeComponent();
            this.Load += FrmVencimientos_Load;

        }

        private async void FrmVencimientos_Load(object sender, EventArgs e)
        {
            await Task.Run(() => vencimientoModel.EjecutarProcedimiento());
            await Task.Run(() => LoadVencimientos());
            AgregarBotonEnviarCorreo();
            foreach (FontFamily font in FontFamily.Families)
            {
                fontComboBox.Items.Add(font.Name);
            }

            // Poblar el ComboBox con tamaños de fuente
            for (int i = 8; i <= 72; i++)
            {
                fontSizeComboBox.Items.Add(i.ToString());
            }

            // Seleccionar fuente y tamaño por defecto
            fontComboBox.SelectedItem = "Arial";
            fontSizeComboBox.SelectedItem = "12";
        }

        private async void LoadVencimientos()
        {
            var titulares = await Task.Run(() => vencimientoModel.GetAllVencimientos());
            Invoke(new Action(() =>
            {
                dtgVencimientos.DataSource = titulares;

                if (dtgVencimientos.Columns["id"] != null)
                {
                    dtgVencimientos.Columns["id"].Visible = false;
                    dtgVencimientos.Columns["marcaID"].Visible = false;
                    dtgVencimientos.Columns["patenteID"].Visible = false;
                }
                dtgVencimientos.Refresh();
            }));
        }

        private void AgregarBotonEnviarCorreo()
        {
            if (!dtgVencimientos.Columns.Contains("EnviarCorreo"))
            {
                var botonColumna = new DataGridViewButtonColumn
                {
                    Name = "EnviarCorreo",
                    HeaderText = "Acciones",
                    Text = "Notificar",
                    UseColumnTextForButtonValue = true
                };

                dtgVencimientos.Columns.Add(botonColumna);
            }

            dtgVencimientos.CellClick += DtgVencimientos_CellClick;


            dtgVencimientos.CellPainting += DtgVencimientos_CellPainting;
        }


        private void DtgVencimientos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dtgVencimientos.Columns["EnviarCorreo"].Index && e.RowIndex >= 0)
            {
                e.Handled = true;

                using (Brush brush = new SolidBrush(ColorTranslator.FromHtml("#34918a")))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                }

                TextRenderer.DrawText(e.Graphics, e.Value?.ToString(), e.CellStyle.Font, e.CellBounds, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Graphics.DrawRectangle(Pens.Black, e.CellBounds);
            }
        }




        private async void DtgVencimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtgVencimientos.Columns["EnviarCorreo"].Index && e.RowIndex >= 0)
            {
                if (isSendingEmail) return;

                isSendingEmail = true;
                var filaSeleccionada = dtgVencimientos.Rows[e.RowIndex];

                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int marcaId = Convert.ToInt32(dataRowView["marcaID"]);
                    var user = vencimientoModel.GetCorreosPorMarcaId(marcaId);
                    string tipo = dataRowView["tipo"].ToString();  // Obtener el tipo
                    DateTime fechaVencimiento = Convert.ToDateTime(dataRowView["Vencimiento"]);
                    string nombre = dataRowView["Nombre"].ToString();

                    if (!string.IsNullOrEmpty(user.CorreoAgente))
                    {
                        try
                        {
                            dtgVencimientos.Enabled = false;
                            await Task.Run(() => EnviarCorreo("Aasunto",user.CorreoAgente, marcaId, nombre, tipo, fechaVencimiento));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al enviar el correo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            dtgVencimientos.Enabled = true;
                            LoadVencimientos();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay un correo registrado para este agente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                isSendingEmail = false;
            }
        }



        public async void EnviarCorreo(string subject, string receptor, int marcaId, string nombre, string tipo, DateTime fechaVencimiento)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Berger Pemueller y Asociados", "avisos@bpa.com.es"));
                message.To.Add(new MailboxAddress("Destinatario", receptor));
                message.Subject = subject;

                string mensajeCuerpo;
                string logoUrl = "https://www.blita.com/hubfs/uXsa_Xqw-1.jpeg";
                string logoHtml = $"<img src='{logoUrl}' alt='Logo de la Empresa' width='400' height='200' />";

                if (tipo.Equals("patente", StringComparison.OrdinalIgnoreCase))
                {
                    mensajeCuerpo = $"<p>La patente <strong>{nombre}</strong> está próxima a vencer, la fecha de vencimiento es: <strong>{fechaVencimiento.ToShortDateString()}</strong>.</p>" +
                                    $"<p>{logoHtml}</p>";
                }
                else if (tipo.Equals("marca", StringComparison.OrdinalIgnoreCase))
                {
                    mensajeCuerpo = $"<p>La marca <strong>{nombre}</strong> está próxima a vencer, la fecha de vencimiento es: <strong>{fechaVencimiento.ToShortDateString()}</strong>.</p>" +
                        $"<p>{logoHtml}</p>";
                }
                else
                {
                    mensajeCuerpo = "<p>Su marca o patente está próxima a vencer.</p>" +
                        $"<p>{logoHtml}</p>";
                }

                message.Body = new TextPart("html") { Text = mensajeCuerpo };

                using (var client = new SmtpClient())
                {
                    client.Connect("mail.bpa.com.es", 465, true);
                    client.Authenticate("avisos@bpa.com.es", "Berger*Pemueller*2024");
                    await client.SendAsync(message);
                    client.Disconnect(true);
                    MessageBox.Show("Correo enviado con éxito.");


                    try
                    {
                        vencimientoModel.ActualizarNotificado(marcaId);
                        LoadVencimientos();
                    }
                    catch (Exception updateEx)
                    {
                        MessageBox.Show($"Error al actualizar el estado de notificado: {updateEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {

        }

        private void ibtnBuscar_Click(object sender, EventArgs e)
        {

        }
        private async void VerificarSeleccionId()
        {
            if (dtgVencimientos.RowCount <= 0)
            {
                FrmAlerta alerta = new FrmAlerta("NO HAY DATOS PARA SELECCIONAR", "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.None);
                alerta.ShowDialog();
                //MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtgVencimientos.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dtgVencimientos.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    if (dataRowView["patenteID"] != DBNull.Value)
                    {
                        int id = Convert.ToInt32(dataRowView["patenteID"]);
                        SeleccionarPatente.id = id;
                        tabControl1.SelectedTab = tabPagePatenteDetail;
                        SeleccionarMarca.idInt = 0;
                        SeleccionarMarca.idN = 0;
                    }
                    else if (dataRowView["marcaID"] != DBNull.Value)
                    {
                        int id = Convert.ToInt32(dataRowView["marcaID"]);

                        DataTable tipoMarca = await Task.Run(() => marcaModel.ObtenerTipoMarca(id));

                        if (tipoMarca.Rows.Count > 0)
                        {
                            DataRow row = tipoMarca.Rows[0];

                            if (row["tipo"] != DBNull.Value)
                            {
                                if (row["tipo"].ToString() == "internacional")
                                {
                                    SeleccionarMarca.idInt = id;
                                    SeleccionarMarca.idN = 0;
                                    SeleccionarPatente.id = 0;
                                }
                                else if (row["tipo"].ToString() == "nacional")
                                {
                                    SeleccionarMarca.idN = id;
                                    SeleccionarMarca.idInt = 0;
                                    SeleccionarPatente.id = 0;
                                }
                            }
                        }
                        tabControl1.SelectedTab = tabPageMarcaDetail;
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
        public async void Ver()
        {
            VerificarSeleccionId();
            if (SeleccionarPatente.id > 0)
            {
                Cursor = Cursors.WaitCursor;
                await CargarDatosPatente();
                Cursor = Cursors.Default;
            }
            else if (SeleccionarMarca.idN > 0)
            {
                await CargarDatosMarcaN();
            }
            else if (SeleccionarMarca.idInt > 0)
            {
                await CargarDatosMarcaInt();
            }
        }
        private void ibtnEditar_Click_1(object sender, EventArgs e)
        {
            Ver();
        }
        private void AnadirTabPage(TabPage nombre)
        {
            if (!tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Add(nombre);
            }

            tabControl1.SelectedTab = nombre;
        }
        public void mostrarPanelRegistroPatente(string isRegistrada)
        {
            if (isRegistrada == "si")
            {
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

        public async Task CargarDatosPatente()
        {
            try
            {

                tabControl1.Visible = false;
                AnadirTabPage(tabPagePatenteDetail);
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
                            txtRenovP.Text = SeleccionarPatente.Erenov;
                        }

                        if (row["Etrasp"] != DBNull.Value)
                        {
                            SeleccionarPatente.Etrasp = row["Etrasp"].ToString();
                            txtTraspasoP.Text = SeleccionarPatente.Etrasp;
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

                            txtNombreTitularP.Text = titular[0].nombre;
                            txtDireccionTitularP.Text = titular[0].direccion;

                        }

                        if (agente.Count > 0)
                        {
                            txtNombreAgenteP.Text = agente[0].nombre;
                        }


                        // Actualizar los controles 
                        txtCaso.Text = SeleccionarPatente.caso;
                        txtExpedienteP.Text = SeleccionarPatente.expediente;
                        txtNombreP.Text = SeleccionarPatente.nombre;
                        txtEstadoP.Text = SeleccionarPatente.estado;
                        dateTPSolicitudP.Value = SeleccionarPatente.fecha_solicitud;
                        comboBoxTipoP.SelectedItem = SeleccionarPatente.tipo;
                        comboBoxAnualidadesP.SelectedItem = SeleccionarPatente.anualidades.ToString();

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

                                txtRegistroP.Text = SeleccionarPatente.registro;
                                txtFolioP.Text = SeleccionarPatente.folio;
                                txtTomoP.Text = SeleccionarPatente.libro;
                                dtpFRegistroP.Value = SeleccionarPatente.fecha_registro.Value;
                                dtpVencimientoP.Value = SeleccionarPatente.fecha_vencimiento.Value;
                            }
                            checkBox1.Checked = true;
                            mostrarPanelRegistroPatente("si");
                        }
                        else
                        {
                            checkBox1.Checked = false;
                            mostrarPanelRegistroPatente("no");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la patente seleccionada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    tabControl1.Visible = true;
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

        public async Task CargarDatosMarcaN()
        {

        }

        public async Task CargarDatosMarcaInt()
        {

        }
        private void EliminarTabPage(TabPage nombre)
        {
            if (tabControl1.TabPages.Contains(nombre))
            {
                tabControl1.TabPages.Remove(nombre);
            }
        }

        private void tabPageVencimientosList_Click(object sender, EventArgs e)
        {

        }

        private void dtgVencimientos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Ver();
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageVencimientosList;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageVencimientosList)
            {
                EliminarTabPage(tabPagePatenteDetail);
                EliminarTabPage(tabPageMarcaDetail);
            }
            else if (tabControl1.SelectedTab == tabPageMarcaDetail)
            {
                EliminarTabPage(tabPagePatenteDetail);
            }
            else if (tabControl1.SelectedTab == tabPagePatenteDetail)
            {
                EliminarTabPage(tabPageMarcaDetail);
            }
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxPCT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageVencimientosList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBoxMensaje.SelectionFont != null)
            {
                Font currentFont = richTextBoxMensaje.SelectionFont;
                FontStyle newFontStyle = currentFont.Style ^ FontStyle.Italic;
                richTextBoxMensaje.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void boldButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBoxMensaje.SelectionFont;
                FontStyle newFontStyle = currentFont.Style ^ FontStyle.Bold;
                richTextBoxMensaje.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void underlineButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBoxMensaje.SelectionFont;
                FontStyle newFontStyle = currentFont.Style ^ FontStyle.Underline;
                richTextBoxMensaje.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            // Abrir un cuadro de diálogo para elegir el color
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBoxMensaje.SelectionColor = colorDialog.Color;
            }
        }

        private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFont = fontComboBox.SelectedItem.ToString();
            richTextBoxMensaje.SelectionFont = new Font(selectedFont, richTextBoxMensaje.SelectionFont.Size);
        }

        private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            float selectedSize = float.Parse(fontSizeComboBox.SelectedItem.ToString());
            richTextBoxMensaje.SelectionFont = new Font(richTextBoxMensaje.SelectionFont.FontFamily, selectedSize);
        }
    }
}
