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

        public async void CrearHistorialOposicion(
            DateTime fecha,
            string etapa,
            string anotaciones,
            string usuario,
            string usuarioEdicion,
            string origen,
            int idOposicion)
        {
            await historialOposicionDao.InsertarHistorialOposicion(
                fecha,
                etapa,
                anotaciones,
                usuario,
                usuarioEdicion,
                origen,
                idOposicion);
        }

        public Task<DataTable> ObtenerHistorial(int idOposicion)
        {
            return historialOposicionDao.ObtenerHistorialOposicion(idOposicion);
        }

        public Task<DataTable> ObtenerHistorialPorId(int idOposicion)
        {
            return historialOposicionDao.ObtenerHistorialOposicionPorId(idOposicion);
        }

        public Task<bool> EditarHistorialOposicion(
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
