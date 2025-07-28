using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public static class AgregarEtapa
    {
        public static int idMarca;
        public static string etapa;
        public static DateTime? fecha;
        public static DateTime? fechaVencimiento;
        public static string usuario;
        public static string anotaciones;
        public static string numExpediente;
        public static string solicitante;
        public static bool enviadoAOposicion;

        public static void LimpiarEtapa()
        {
            idMarca = 0;
            etapa = "";
            fecha = null;
            fechaVencimiento = null;
            usuario = "";
            anotaciones = "";
            numExpediente = "";
            solicitante = "";

        }
    }
}
