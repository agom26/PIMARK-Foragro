using AccesoDatos.ServiciosEmail;
using Dominio;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Vencimientos
{
    public partial class FrmVencimientos : Form
    {
        VencimientoModel vencimientoModel = new VencimientoModel();
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
                            await Task.Run(() => EnviarCorreo(user.CorreoAgente, marcaId, nombre, tipo, fechaVencimiento));
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



        public async void EnviarCorreo(string receptor, int marcaId, string nombre, string tipo, DateTime fechaVencimiento)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Berger Pemueller", "avisos@bpa.com.es"));
                message.To.Add(new MailboxAddress("Destinatario", receptor));
                message.Subject = "Avisos de vencimiento de Berger Pemueller";

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
    }
}
