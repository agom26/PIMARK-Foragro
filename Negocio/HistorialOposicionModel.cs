using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HistorialOposicionModel
    {
        private HistorialOposicionDao historialOposicionDao;

        public HistorialOposicionModel()
        {
            historialOposicionDao = new HistorialOposicionDao();
        }

        public void CrearHistorialOposicion(
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion,
            string origen,
            int idOposicion)
        {
            historialOposicionDao.InsertarHistorialOposicion(
                fecha,
                etapa,
                anotaciones,
                usuario,
                usuarioEdicion,
                origen,
                idOposicion);
        }
    }
}
