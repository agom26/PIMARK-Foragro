using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable ObtenerHistorial(int idOposicion)
        {
            return historialOposicionDao.ObtenerHistorialOposicion(idOposicion);
        }

        public DataTable ObtenerHistorialPorId(int idOposicion)
        {
            return historialOposicionDao.ObtenerHistorialOposicionPorId(idOposicion);
        }

        public bool EditarHistorialOposicion(
            int historialId,
            string nuevaEtapa,
            DateTime nuevaFecha,
            string nuevasAnotaciones,
            string nuevoUsuario,
            string nuevoUsuarioEdicion)
        {
            return historialOposicionDao.EditarHistorialOposicion(
                historialId,
                nuevaEtapa,
                nuevaFecha,
                nuevasAnotaciones,
                nuevoUsuario,
                nuevoUsuarioEdicion);
        }
    }
}
