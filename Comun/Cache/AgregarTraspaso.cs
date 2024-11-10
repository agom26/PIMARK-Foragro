using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public class AgregarTraspaso
    {
        public static int idTitularAnterior;
        public static string nombreTitulara;
        public static int idNuevoTitular;
        public static string nombreTitularN;
        public static string nuevoNombre;
        public static string antiguoNombre;
        public static DateTime fechaCreacion;
        public static bool traspasoFinalizado;

        public static void LimpiarTraspaso()
        {
            idTitularAnterior = 0;
            idNuevoTitular = 0;
            nuevoNombre = "";
            antiguoNombre = "";
        }

    }
}
