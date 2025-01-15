using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public static class DatosRegistro
    {
        public static int registro;
        public static int folio;
        public static int tomo;
        public static DateTime fechaRegistro;
        public static DateTime fechaVencimiento;
        public static bool peligro;

        public static void LimpiarDatosMarca()
        {
            registro = 0;
            folio = 0;
            tomo = 0;
            fechaRegistro = DateTime.Now; 
            fechaVencimiento = DateTime.Now.AddYears(10).AddDays(-1);

        }

        public static void LimpiarDatosPatente()
        {
            registro = 0;
            folio = 0;
            tomo = 0;
            fechaRegistro = DateTime.Now;
            fechaVencimiento = DateTime.Now.AddYears(20);
        }
    }
}
