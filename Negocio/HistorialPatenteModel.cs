using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
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
    }
}
