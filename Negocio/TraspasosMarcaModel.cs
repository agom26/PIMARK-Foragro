using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TraspasosMarcaModel
    {
        private TraspasoMarcasDao traspasoMarcasDao;

        public TraspasosMarcaModel()
        {
            traspasoMarcasDao = new TraspasoMarcasDao();
        }

        public void AddTraspaso(string numExpediente, int idMarca, int idTitularAnterior, int idTitularNuevo)
        {
            traspasoMarcasDao.InsertarTraspasoMarca(numExpediente, idMarca, idTitularAnterior, idTitularNuevo);
        }

        public DataTable ObtenerTraspasosMarcaPorIdMarca(int idMarca)
        {
            return traspasoMarcasDao.ObtenerTraspasosDeMarcaPorId(idMarca);
        }
        public DataTable ObtenerTraspasoPorId(int id)
        {
            return traspasoMarcasDao.ObtenerTraspasoPorId(id);
        }
        public bool ActualizarTraspaso(int id, string numExpediente, int idMarca, int idTitularAnterior, int idTitularNuevo)
        {
            return traspasoMarcasDao.ActualizarTraspasoMarca(id, numExpediente, idMarca, idTitularAnterior, idTitularNuevo);
        }
    }
}
