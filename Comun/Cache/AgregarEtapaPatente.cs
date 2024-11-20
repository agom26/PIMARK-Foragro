using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public static class AgregarEtapaPatente
    {
        public static int idPatente;
        public static string etapa;
        public static DateTime? fecha;
        public static string usuario;
        public static string anotaciones;
        public static int numExpediente;

        public static void LimpiarEtapa()
        {
            idPatente = 0;
            etapa = "";
            fecha = null;
            usuario = "";
            anotaciones = "";
            numExpediente = 0;
        }
    }
}
