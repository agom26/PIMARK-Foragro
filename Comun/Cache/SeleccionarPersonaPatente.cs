using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public static class SeleccionarPersonaPatente
    {
        public static int idPersonaT;
        public static int idPersonaA;
        public static string nombre;
        public static string direccion;
        public static string nit;
        public static string pais;
        public static string correo;
        public static string telefono;
        public static string contacto;
        public static string tipo;

        public static void LimpiarPersona()
        {
            idPersonaT = 0;
            idPersonaA = 0;
            nombre = "";
            direccion = "";
            nit = "";
            pais = "";
            correo = "";
            telefono = "";
            contacto = "";
            tipo = "";
        }
    }
}
