using AccesoDatos.Entidades;
using System.Data;

namespace Dominio
{
    public class TraspasosMarcaModel
    {
        private TraspasoMarcasDao traspasoMarcasDao;

        public TraspasosMarcaModel()
        {
            traspasoMarcasDao = new TraspasoMarcasDao();
        }

        public async Task AddTraspaso(string numExpediente, int idMarca, int idTitularAnterior, int idTitularNuevo)
        {
            await traspasoMarcasDao.InsertarTraspasoMarca(numExpediente, idMarca, idTitularAnterior, idTitularNuevo);
        }

        public async Task<DataTable> ObtenerTraspasosMarcaPorIdMarca(int idMarca)
        {
            return await traspasoMarcasDao.ObtenerTraspasosDeMarcaPorId(idMarca);
        }
        public async Task<DataTable> ObtenerTraspasoPorId(int id)
        {
            return await traspasoMarcasDao.ObtenerTraspasoPorId(id);
        }
        public async Task<bool> ActualizarTraspaso(int id, string numExpediente, int idMarca, int idTitularAnterior, int idTitularNuevo)
        {
            return await traspasoMarcasDao.ActualizarTraspasoMarca(id, numExpediente, idMarca, idTitularAnterior, idTitularNuevo);
        }
    }
}
