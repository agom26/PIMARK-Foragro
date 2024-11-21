using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public static class SeleccionarHistorialPatente
    {
        public static int id;
        public static string etapa;
        public static DateTime fecha;
        public static string anotaciones;
        public static string usuario;
        public static string usuarioEdicion;
        public static int IdMarca;

        public static void LimpiarHistorial()
        {
            id = 0;
            etapa = "";
            fecha = DateTime.Now;
            anotaciones = "";
            usuario = "";
            usuarioEdicion = "";
            IdMarca = 0;
        }
    }
}
