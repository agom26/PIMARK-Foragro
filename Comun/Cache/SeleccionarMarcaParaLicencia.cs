using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public static class SeleccionarMarcaParaLicencia
    {
        public static int idMarca;
        public static string nombreSigno;
        public static string expediente;
        public static string clase;
        public static string signoDistintivo;
        public static string registro;
        public static string folio;
        public static string tomo;
        public static DateTime fechaVencimiento;
        public static int idTitularMarca;
        public static string nombreTitular;

        public static void LimpiarMarcaParaLicencia()
        {
            idMarca = 0;
            nombreSigno = string.Empty;
            expediente = string.Empty;
            clase = string.Empty;
            signoDistintivo = string.Empty;
            registro = string.Empty;
            folio = string.Empty;
            tomo = string.Empty;
            fechaVencimiento = DateTime.MinValue;
            idTitularMarca = 0;
            nombreTitular = string.Empty;
        }
    }
}
