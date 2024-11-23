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
    public class RenovacionesPatenteModel:ConnectionSQL
    {
        private RenovacionesPatenteDao renovacionesDao;

        public RenovacionesPatenteModel()
        {
            renovacionesDao = new RenovacionesPatenteDao();
        }

        public void AddRenovacion(string numExpediente, int idPatente, DateTime fechaVencimientoAntigua, DateTime fechaVencimientoNueva)
        {
            renovacionesDao.InsertRenovacionPatente(numExpediente, idPatente, fechaVencimientoAntigua, fechaVencimientoNueva);
        }

        public DataTable GetAllRenovacionesByIdPatente(int idPatente)
        {
            return renovacionesDao.ObtenerRenovacionesDePatentePorId(idPatente);
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
