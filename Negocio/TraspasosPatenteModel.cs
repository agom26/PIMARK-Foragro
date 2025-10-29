using System.Data;
using AccesoDatos;
using AccesoDatos.Entidades;
namespace Dominio
{
    public class TraspasosPatenteModel:ConnectionSQL
    {
        private TraspasoPatenteDao traspasoPatenteDao;

        public TraspasosPatenteModel()
        {
            traspasoPatenteDao= new TraspasoPatenteDao();
        }

        public async Task AddTraspaso(string numExpediente, int idPatente, int idTitularAnterior, int idTitularNuevo)
        {
            await traspasoPatenteDao.InsertarTraspasoPatente(numExpediente, idPatente, idTitularAnterior, idTitularNuevo);
        }

        public async Task<DataTable> ObtenerTraspasosPatentePorIdPatente(int idPatente)
        {
            return await traspasoPatenteDao.ObtenerTraspasosDePatentePorId(idPatente);
        }
        public async Task<DataTable> ObtenerTraspasoPorId(int id)
        {
            return await traspasoPatenteDao.ObtenerTraspasoPatentePorId(id);
        }
        public async Task<bool> ActualizarTraspaso(int id, string numExpediente, int idPatente, int idTitularAnterior, int idTitularNuevo)
        {
            return await traspasoPatenteDao.ActualizarTraspasoPatente(id, numExpediente, idPatente, idTitularAnterior, idTitularNuevo);
        }
    }
}
