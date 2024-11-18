using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using AccesoDatos.Entidades;

namespace Dominio
{
    public class RenovacionesMarcaModel:ConnectionSQL
    {
        private RenovacionesMarcasDao renovacionesDao;

        public RenovacionesMarcaModel()
        {
            renovacionesDao = new RenovacionesMarcasDao();
        }

        public void AddRenovacion(string numExpediente, int idMarca, DateTime fechaVencimientoAntigua, DateTime fechaVencimientoNueva) 
        {
            renovacionesDao.InsertRenovacionMarca(numExpediente, idMarca,  fechaVencimientoAntigua, fechaVencimientoNueva);
        }

        public DataTable GetAllRenovacionesByIdMarca(int idMarca)
        {
            return renovacionesDao.ObtenerRenovacionesDeMarcaPorId(idMarca);
        }

        public DataTable GetRenovacionById(int id)
        {
            return renovacionesDao.ObtenerRenovacionPorId(id);
        }
        public bool ActualizarRenovacion(int id, string numExpediente, int idMarca, DateTime fechaVencimientoAntigua, DateTime fechaVencimientoNueva)
        {
            return renovacionesDao.ActualizarRenovacionMarca(id, numExpediente, idMarca, fechaVencimientoAntigua, fechaVencimientoNueva);
        }



    }
}
