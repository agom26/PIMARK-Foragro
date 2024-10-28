using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.ServiciosEmail
{
    public  class ServicioEmail:IServicioEmail
    {
        private SmtpClient smtpClient;

        private struct MailServer
        {
            public const string Address = "gaby26@umes.edu.gt";
            public const string Password = "102N14J6";
            public const string DisplayName = "Berger Pemueller";
            public const string Host = "smtp.gmail.com";
            public const int Port = 587;
            public const bool SSL = true;

        };

        public ServicioEmail()
        {
            smtpClient = new SmtpClient();//Inicializar cliente SMTP.
            smtpClient.Credentials = new NetworkCredential(MailServer.Address, MailServer.Password);//Establecer las credenciales (Usuario y contraseña).
            smtpClient.Host = MailServer.Host;//Establecer el host.
            smtpClient.Port = MailServer.Port;//Establecer el puerto.
            smtpClient.EnableSsl = MailServer.SSL;//Establecir el cifrado SSL.
        }
        public void Send(string recipient, string subject, string body)
        {//Este método es responsable de enviar un mensaje de correo a un solo destinatario.

            var mailMessage = new MailMessage();//Inicializar el objeto mensaje de correo.
            var mailSender = new MailAddress(MailServer.Address, MailServer.DisplayName);//Inicializar el objeto dirección de correo electrónico remitente.

            try
            {
                mailMessage.From = mailSender;//Establecer la dirección de correo remitente.
                mailMessage.To.Add(recipient);//Establecer y agregar la dirección de correo destinatario.
                mailMessage.Subject = subject;//Establecer el asunto del mensaje de correo.
                mailMessage.Body = body;//Establecer el contenido del mensaje de correo.
                mailMessage.Priority = MailPriority.Normal;//Establecer la prioridad del mensaje de correo.

                smtpClient.Send(mailMessage);//Enviar el mensaje de correo mediante el cliente SMTP(Protocolo simple de transferencia de correo)
            }
            catch (SmtpException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                mailMessage.Dispose();
            }
        }

    }
}
