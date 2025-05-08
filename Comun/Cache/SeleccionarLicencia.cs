using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Cache
{
    public static class SeleccionarLicencia
    {
        public static int idLicencia;
        public static int idMarca;
        public static int idTitular;
        public static string tituloPorElQueVerifica;
        public static string tipo;
        public static DateTime fechaInicio;
        public static DateTime fechaFin;
        public static string territorio;
        public static string nombreRazonSocial;
        public static string direccion;
        public static string domicilio;
        public static string nacionalidad;
        public static string apoderadoRepresentanteL;
        public static string estado;
        public static string origen;

        public static int difAnios;
        public static int difMeses; 

        public static string CalcularDiferenciaFormateada()
        {
            DateTime inicio = SeleccionarLicencia.fechaInicio;
            DateTime fin = SeleccionarLicencia.fechaFin;

            int años = fin.Year - inicio.Year;
            int meses = fin.Month - inicio.Month;

            if (fin.Day < inicio.Day)
            {
                meses--;
            }

            if (meses < 0)
            {
                años--;
                meses += 12;
            }
            difAnios = años;
            difMeses = meses;
            return $"{años} años {meses} meses";
        }


        public static void limpiarDiferencia()
        {
            difAnios = 0;
            difMeses = 0;
        }

    }
}
