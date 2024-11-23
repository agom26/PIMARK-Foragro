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
    public class TraspasosPatenteModel:ConnectionSQL
    {
        private TraspasoPatenteDao traspasoPatenteDao;

        public TraspasosPatenteModel()
        {
            traspasoPatenteDao= new TraspasoPatenteDao();
        }

        public void AddTraspaso(string numExpediente, int idPatente, int idTitularAnterior, int idTitularNuevo)
        {
            traspasoPatenteDao.InsertarTraspasoPatente(numExpediente, idPatente, idTitularAnterior, idTitularNuevo);
        }

        public DataTable ObtenerTraspasosPatentePorIdPatente(int idPatente)
        {
            return traspasoPatenteDao.ObtenerTraspasosDePatentePorId(idPatente);
        }
        public DataTable ObtenerTraspasoPorId(int id)
        {
            return traspasoPatenteDao.ObtenerTraspasoPatentePorId(id);
        }
        public bool ActualizarTraspaso(int id, string numExpediente, int idPatente, int idTitularAnterior, int idTitularNuevo)
        {
            return traspasoPatenteDao.ActualizarTraspasoPatente(id, numExpediente, idPatente, idTitularAnterior, idTitularNuevo);
        }
    }
}
