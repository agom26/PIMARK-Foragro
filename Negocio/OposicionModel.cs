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
        public DataTable GetAllOposicionesInternacionales()
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetAllOposicionesInternacionales();
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
            byte[] logoSignoPretendido)
        {
            return oposicionDao.AddOposicion(expediente, signoPretendido, signoDistintivo, 
                clase, solicitanteSignoPretendido,idSolicitante, idOpositor,opositor, signoOpositor, 
                situacionActual, idMarca, logoOpositor, logoSignoPretendido);
        }

        
        public DataTable GetOposicionPorId(int idOposicion)
        {
            DataTable tabla = new DataTable();
            tabla = oposicionDao.GetOposicionPorId(idOposicion);
            return tabla;
        }
    }
}
