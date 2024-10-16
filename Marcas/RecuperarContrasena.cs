using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;

namespace Presentacion
{
    public partial class RecuperarContrasena : Form
    {
        public RecuperarContrasena()
        {
            InitializeComponent();
        }
        public void EnviarCorreo(string asunto, string cuerpo, List<string> correosDestinatarios)
        {
            string correoRemitente = "avisas@bpa.com.es";
            string contrasena = "Berger*Pemueller*2024";
            string host = "mail.bpa.com.es"; // El servidor SMTP del dominio de correos
            int puerto = 465; // Puede variar según el servidor SMTP
            bool ssl = true;  // Define si debe usar SSL

            SmtpClient clienteSmtp = new SmtpClient
            {
                Host = host,
                Port = puerto,
                EnableSsl = ssl,
                Credentials = new NetworkCredential(correoRemitente, contrasena)
            };

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(correoRemitente);

                // Añadir cada destinatario
                foreach (string destinatario in correosDestinatarios)
                {
                    mailMessage.To.Add(destinatario);
                }

                // Configurar el correo
                mailMessage.Subject = asunto;
                mailMessage.Body = cuerpo;
                mailMessage.IsBodyHtml = false; // Si el cuerpo es HTML, cambia a true
                mailMessage.Priority = MailPriority.Normal;

                // Enviar el correo
                clienteSmtp.Send(mailMessage);

                Console.WriteLine("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
            }
        }

        public void EnviarEjemplo()
        {
            List<string> correosDestinatarios = new List<string>
            {
                "govalle@sitiosenred.com"
            };

            string asunto = "Solicitud de recuperación de contraseña";
            string cuerpo = "Hola, tu solicitud de recuperación de contraseña fue recibida. Tu nueva contraseña es: 123456";

            EnviarCorreo(asunto, cuerpo, correosDestinatarios);
        }


        private void iconButtonEnviarCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                EnviarEjemplo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            

            /*
            UserModel userModel = new UserModel();
            string resultado=userModel.recuperarContrasena(txtBoxUser.Text);
            labelResultado.Text = resultado;*/
        }
    }
}
