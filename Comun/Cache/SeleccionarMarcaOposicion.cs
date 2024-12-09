using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public static class SeleccionarMarcaOposicion
    {
        public static int idMarca;
        public static string nombreSigno;
        public static int idTitularMarca;
        public static string nombreTitular;

        public static void LimpiarMarcaOposicion()
        {
            idMarca = 0;
            nombreSigno = null;
            idTitularMarca = 0;
            nombreTitular = null;
        }
    }
}
