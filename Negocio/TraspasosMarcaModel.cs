using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
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

        public void AddTraspaso(string numExpediente, int idMarca, int idTitularAnterior, int idTitularNuevo, string antiguoNombre, string nuevoNombre)
        {
            traspasoMarcasDao.InsertarTraspasoMarca(numExpediente, idMarca, idTitularAnterior, idTitularNuevo, antiguoNombre, nuevoNombre);
        }
    }
}
