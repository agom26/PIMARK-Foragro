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
            await Task.Run(() => LoadVencimientos());
            AgregarBotonEnviarCorreo(); 
        }

        private void LoadVencimientos()
        {
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
                    string nombre= dataRowView["Nombre"].ToString();

                    if (!string.IsNullOrEmpty(user.CorreoAgente))
                    {
                        try
                        {
                            dtgVencimientos.Enabled = false;
                            await Task.Run(() => EnviarCorreo(user.CorreoAgente,nombre, tipo, fechaVencimiento));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al enviar el correo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            dtgVencimientos.Enabled = true;
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


        public async void EnviarCorreo(string receptor,string nombre, string tipo, DateTime fechaVencimiento)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Berger Pemueller", "avisos@bpa.com.es"));
                message.To.Add(new MailboxAddress("Destinatario", receptor));
                message.Subject = "Avisos de vencimiento de Berger Pemueller";

                
                string mensajeCuerpo;
                if (tipo.Equals("patente", StringComparison.OrdinalIgnoreCase))
                {
                    mensajeCuerpo = $"La patente {nombre} está próxima a vencer, la fecha de vencimiento es: {fechaVencimiento.ToShortDateString()}.";
                }
                else if (tipo.Equals("marca", StringComparison.OrdinalIgnoreCase))
                {
                    mensajeCuerpo = $"La marca {nombre} está próxima a vencer, la fecha de vencimiento es: {fechaVencimiento.ToShortDateString()}.";
                }
                else
                {
                    mensajeCuerpo = "Su marca o patente está próxima a vencer.";
                }

                message.Body = new TextPart("plain") { Text = mensajeCuerpo };

                using (var client = new SmtpClient())
                {
                    client.Connect("mail.bpa.com.es", 465, true);
                    client.Authenticate("avisos@bpa.com.es", "Berger*Pemueller*2024");
                    await client.SendAsync(message);
                    client.Disconnect(true);
                    MessageBox.Show("Correo enviado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }

    }
}
