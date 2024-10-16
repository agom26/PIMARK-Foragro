using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace AccesoDatos.ServiciosEmail
{
    public class ServidorCorreoMaestro
    {
        private SmtpClient smtpClient;
        public string correoRemitente;
        public string contrasena;
        public string host;
        public int port;
        public bool ssl;

        protected void initializeSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(correoRemitente, contrasena);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;

        }

        public void enviarCorreo(string asunto, string cuerpo, List<String> correoReceptor)
        {
            MailMessage mailMessage = new MailMessage();
            try
            {
                mailMessage.From=new MailAddress(correoRemitente);
                foreach (string destinatario in correoReceptor)
                {
                    mailMessage.To.Add(destinatario);
                }
                mailMessage.Subject = asunto;
                mailMessage.Body = cuerpo;
                mailMessage.Priority=MailPriority.Normal;

                smtpClient.Send(mailMessage);
            }   
            catch (Exception ex)
            {
                
            }


            finally
            {
                mailMessage.Dispose();
                smtpClient.Dispose();
                
            }
        }
    }
}
