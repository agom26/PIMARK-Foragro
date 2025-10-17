
using System.Data;
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

        public async Task AddRenovacion(string numExpediente, int idPatente, DateTime fechaVencimientoAntigua, DateTime fechaVencimientoNueva)
        {
            await renovacionesDao.InsertRenovacionPatente(numExpediente, idPatente, fechaVencimientoAntigua, fechaVencimientoNueva);
        }

        public async Task<DataTable> GetAllRenovacionesByIdPatente(int idPatente)
        {
            return await renovacionesDao.ObtenerRenovacionesDePatentePorId(idPatente);
        }

        public async Task<DataTable> GetRenovacionById(int id)
        {
            return await renovacionesDao.ObtenerRenovacionPorId(id);
        }
        public async Task<bool> ActualizarRenovacion(int id, string numExpediente, int idMarca, DateTime fechaVencimientoAntigua, DateTime fechaVencimientoNueva)
        {
            return await renovacionesDao.ActualizarRenovacionPatente(id, numExpediente, idMarca, fechaVencimientoAntigua, fechaVencimientoNueva);
        }
    }
}
