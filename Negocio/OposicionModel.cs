using AccesoDatos;
using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
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

        public int CrearOposicion(
            string expediente,
            string signoPretendido,
            string signoDistintivo,
            string clase,
            string solicitanteSignoPretendido,
            int idOpositor,
            string signoOpositor,
            string observaciones,
            string estado,
            string situacionActual,
            int? idMarca,
            byte?[] logoOpositor,
            byte?[] logoSignoPretendido)
        {
            return oposicionDao.AddOposicion(expediente, signoPretendido, signoDistintivo, 
                clase, solicitanteSignoPretendido, idOpositor, signoOpositor, observaciones, 
                estado, situacionActual, idMarca, logoOpositor, logoSignoPretendido);
        }
    }
}
