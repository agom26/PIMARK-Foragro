using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public static class SeleccionarPersonaReportes
    {
        public static int idTitular;
        public static int idAgente;
        public static int idCliente;
        public static string? nombreTitular;
        public static string? nombreAgente;
        public static string? nombreCliente;

        public static void LimpiarTitular()
        {
            idTitular = 0;
            nombreTitular = null;
        }

        public static void LimpiarAgente()
        {
            idAgente = 0;
            nombreAgente = null;
        }

        public static void LimpiarCliente()
        {
            idCliente = 0;
            nombreCliente = null;
        }
    }
}
