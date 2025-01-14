using AccesoDatos;
using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class OposicionModel:ConnectionSQL
    {
        private OposicionesDao oposicionDao;

        public OposicionModel()
        {
            oposicionDao = new OposicionesDao();
        }

        public DataTable FiltrarOposiciones(
          string tipo_filtro,
          string expediente, string solicitante, string signoPretendido,
          string signoDistintivo, string clase, string opositor, string signoOpositor,
          string estado, string situacionActual, string tipo, string tipoOposicion)
        {
            return oposicionDao.FiltrarOposiciones(
                tipo_filtro, expediente, solicitante, signoPretendido,
                signoDistintivo, clase, opositor, signoOpositor,
                estado, situacionActual, tipo, tipoOposicion
                );
        }
        //internacionales recibidas
        public int GetTotalOposicionesInternacionalesRecibidas(string situacion)
        {
            return oposicionDao.GetTotalOposicionesInternacionalesRecibidas(situacion);
        }
        public int GetFilteredMarcasInternacionalesRecibidasCount(string value)
        {
            return oposicionDao.GetFilteredOposicionesInternacionalesRecibidasCount(value);
        }
        public DataTable FiltrarOposicionesInternacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            return oposicionDao.FiltrarOposicionesInternacionalesRecibidas(filtro, currentPageIndex, pageSize);
        }
        public DataTable GetAllOposicionesInternacionalesRecibidas(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetAllOposicionesInternacionalesRecibidas(situacionActual, currentPageIndex, pageSize);
            return tabla;
        }
        //internacionales internpuestas
        public int GetTotalOposicionesInteracionalesInterpuestas(string situacion)
        {
            return oposicionDao.GetTotalOposicionesInternacionalesInterpuestas(situacion);
        }
        public int GetFilteredOposicionesInternacionalesInterpuestasCount(string value)
        {
            return oposicionDao.GetFilteredOposicionesInternacionalesInterpuestasCount(value);
        }
        public DataTable FiltrarOposicionesInternacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            return oposicionDao.FiltrarOposicionesInternacionalesInterpuestas(filtro, currentPageIndex, pageSize);
        }
        public DataTable GetAllOposicionesInternacionalesInterpuestas(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetAllOposicionesInternacionalesInterpuestas(situacionActual, currentPageIndex, pageSize);
            return tabla;
        }
        public int GetTotalOposicionesNacionalesRecibidas(string situacion)
        {
            return oposicionDao.GetTotalOposicionesNacionalesRecibidas(situacion);
        }
        public int GetFilteredOposicionesNacionalesRecibidasCount(string value)
        {
            return oposicionDao.GetFilteredOposicionesNacionalesRecibidasCount(value);
        }
        public DataTable FiltrarOposicionesNacionalesRecibidas(string filtro, int currentPageIndex, int pageSize)
        {
            return oposicionDao.FiltrarOposicionesNacionalesRecibidas(filtro, currentPageIndex, pageSize);
        }
        public DataTable GetAllOposicionesNacionales(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetAllOposicionesNacionales(situacionActual, currentPageIndex, pageSize);
            return tabla;
        }
        public int GetTotalOposicionesNacionalesInterpuestas(string situacion)
        {
            return oposicionDao.GetTotalOposicionesNacionalesInterpuestas(situacion);
        }
        public int GetFilteredOposicionesNacionalesInterpuestasCount(string value)
        {
            return oposicionDao.GetFilteredOposicionesNacionalesInterpuestasCount(value);
        }
        public DataTable FiltrarOposicionesNacionalesInterpuestas(string filtro, int currentPageIndex, int pageSize)
        {
            return oposicionDao.FiltrarOposicionesNacionalesInterpuestas(filtro, currentPageIndex, pageSize);
        }
        public DataTable GetAllOposicionesNacionalesInterpuestas(string situacionActual, int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetAllOposicionesNacionalesInterpuestas(situacionActual, currentPageIndex, pageSize);
            return tabla;
        }

        public int CrearOposicion(
            string expediente,
            string signoPretendido,
            string signoDistintivo,
            string clase,
            string solicitanteSignoPretendido,
            int? idSolicitante,
            int? idOpositor,
            string opositor,
            string signoOpositor,
            string situacionActual,
            int? idMarca,
            byte[] logoOpositor,
            byte[] logoSignoPretendido,
            string tipo, 
            string tipoOposicion)
        {
            return oposicionDao.AddOposicion(expediente, signoPretendido, signoDistintivo, 
                clase, solicitanteSignoPretendido,idSolicitante, idOpositor,opositor, signoOpositor, 
                situacionActual, idMarca, logoOpositor, logoSignoPretendido, tipo, tipoOposicion);
        }

        
        public DataTable GetOposicionPorId(int idOposicion)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetOposicionPorId(idOposicion);
            return tabla;
        }

        public bool EditarOposicion(
           int idOposicion,
           string expediente,
           string signoPretendido,
           string signoDistintivo,
           string clase,
           string solicitanteSignoPretendido,
           int? idOpositor,
           string signoOpositor,
           string situacionActual,
           int? idMarca,
           byte[] logoOpositor,
           byte[] logoSignoPretendido,
           string opositor,
           int? idSolicitante)
        {
            return oposicionDao.EditOposicion(
                idOposicion,
                expediente,
                signoPretendido,
                signoDistintivo,
                clase,
                solicitanteSignoPretendido,
                idOpositor,
                signoOpositor,
                situacionActual,
                idMarca,
                logoOpositor,
                logoSignoPretendido,
                opositor,
                idSolicitante);
        }

        public bool CambiarSituacionActualATerminada(int id)
        {
            return oposicionDao.CambiarSituacionActualATerminada(id);
        }

    }
}
