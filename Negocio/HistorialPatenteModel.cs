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

        public async void CrearHistorialPatente(
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion,
            int idPatente)
        {
            await historialPatenteDao.InsertarHistorialPatente(
                fecha,
                etapa,
                anotaciones,
                usuario,
                usuarioEdicion,
                idPatente);
        }

        public Task<DataTable> ObtenerHistorialPorIdPatente(int idPatente)
        {
            return historialPatenteDao.ObtenerHistorialPatentePorIdPatente(idPatente);
        }

        public Task<DataTable> ObtenerHistorialPorId(int idHistorial)
        {
            return historialPatenteDao.ObtenerHistorialPatentePorId(idHistorial);  // Llamada al DAO
        }
        public async void EditarHistorialPatente(
            int idHistorial,
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion)
        {
            await historialPatenteDao.EditarHistorialPatente(
                idHistorial,
                fecha,
                etapa,
                anotaciones,
                usuario,
                usuarioEdicion);
        }
    }
}
