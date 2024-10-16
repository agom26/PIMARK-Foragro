using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.ServiciosEmail
{
    public class CorreoSoporteSistema: ServidorCorreoMaestro
    {
        public CorreoSoporteSistema()
        {
            correoRemitente = "avisos@bpa.com.es";
            contrasena = "Berger*Pemueller*2024";
            host = "mail.bpa.com.es";
            port = 465;
            ssl = true;

            initializeSmtpClient();
        }

        public void Notificaciones()
        {

        }

        public void Errores()
        {

        }
    }
}
