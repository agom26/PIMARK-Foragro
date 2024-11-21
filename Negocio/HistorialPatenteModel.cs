using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HistorialPatenteModel
    {
        private HistorialPatenteDao historialPatenteDao;

        public HistorialPatenteModel()
        {
            historialPatenteDao = new HistorialPatenteDao();
        }

        public void CrearHistorialPatente(
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion,
            int idPatente)
        {
            historialPatenteDao.InsertarHistorialPatente(
                fecha,
                etapa,
                anotaciones,
                usuario,
                usuarioEdicion,
                idPatente);
        }

        public DataTable ObtenerHistorialPorIdPatente(int idPatente)
        {
            return historialPatenteDao.GetAllEstadosByIdPatente(idPatente);
        }

        public DataTable ObtenerHistorialPorId(int idHistorial)
        {
            return historialPatenteDao.GetHistorialById(idHistorial);  // Llamada al DAO
        }
        public void EditarHistorialPatente(
            int idHistorial,
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion)
        {
            historialPatenteDao.EditarHistorialPatente(
                idHistorial,
                fecha,
                etapa,
                anotaciones,
                usuario,
                usuarioEdicion);
        }
    }
}
