using AccesoDatos.ServiciosEmail;
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
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;


namespace Presentacion.Vencimientos
{
    public partial class FrmVencimientos : Form
    {


        VencimientoModel vencimientoModel = new VencimientoModel();
        public FrmVencimientos()
        {
            InitializeComponent();
            this.Load += FrmVencimientos_Load; // Mueve la lógica de carga aquí
        }
        public void EnviarCorreo( string receptor)
        {
            try
            {
                // Configuración del mensaje
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Berger Pemueller", "avisos@bpa.com.es"));
                message.To.Add(new MailboxAddress("Destinatario", receptor));
                message.Subject = "Avisos de vencimiento de Berger Pemueller";

                // Cuerpo del mensaje
                message.Body = new TextPart("plain")
                {
                    Text = "Su marca o patente ya va a vencer."
                };

                // Configuración del cliente SMTP
                using (var client = new SmtpClient())
                {
                    client.Connect("mail.bpa.com.es", 465, true); // Usa SSL (puerto 465)

                    // Autenticación
                    client.Authenticate("avisos@bpa.com.es", "Berger*Pemueller*2024");

                    // Enviar el mensaje
                    client.Send(message);
                    MessageBox.Show("Correo enviado con éxito.");
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }


        private void MostrarTitulares()
        {
            dtgVencimientos.DataSource = vencimientoModel.GetAllVencimientos();
            // Ocultar la columna 'id'
            if (dtgVencimientos.Columns["id"] != null)
            {
                dtgVencimientos.Columns["id"].Visible = false;
                dtgVencimientos.Columns["marcaID"].Visible = false;
                // Desactiva la selección automática de la primera fila
                dtgVencimientos.ClearSelection();
            }
        }

        private void LoadVencimientos()
        {
            // Obtiene los usuarios
            var titulares = vencimientoModel.GetAllVencimientos();

            Invoke(new Action(() =>
            {
                dtgVencimientos.DataSource = titulares;

                if (dtgVencimientos.Columns["id"] != null)
                {
                    dtgVencimientos.Columns["id"].Visible = false;
                    dtgVencimientos.Columns["marcaID"].Visible = false;
                }


            }));
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

            string valor = "%" + txtBuscar.Text + "%";
            var vencimientos = vencimientoModel.GetVencimientoByValue(valor);

            if (vencimientos != null)
            {
                dtgVencimientos.DataSource = vencimientos;
                if (dtgVencimientos.Columns["id"] != null)
                {
                    dtgVencimientos.Columns["id"].Visible = false;
                    dtgVencimientos.Columns["marcaID"].Visible = false;
                }

            }
            else
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.");
            }
        }

        private async void FrmVencimientos_Load(object sender, EventArgs e)
        {
            // Cargar usuarios en segundo plano
            await Task.Run(() => LoadVencimientos());
        }

        private async void iconButton2_Click(object sender, EventArgs e)
        {
            if (dtgVencimientos.RowCount <= 0)
            {
                MessageBox.Show("No hay datos para seleccionar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dtgVencimientos.SelectedRows.Count >= 1)
            {
                var filaSeleccionada = dtgVencimientos.SelectedRows[0];
                if (filaSeleccionada.DataBoundItem is DataRowView dataRowView)
                {
                    int marcaid = Convert.ToInt32(dataRowView["marcaID"]);
                    var user = vencimientoModel.GetCorreosPorMarcaId(marcaid);
                    if (user.CorreoAgente == null) return;

                    // Deshabilitar el botón antes de enviar el correo
                    iconButton2.Enabled = false;

                    try
                    {
                        // Enviar el correo de manera asíncrona
                        await Task.Run(() => EnviarCorreo(user.CorreoAgente));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al enviar el correo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Rehabilitar el botón después de enviar el correo
                        iconButton2.Enabled = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
