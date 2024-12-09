using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HistorialModel
    {
        private HistorialMarcasDao historialMarcasDao;

        public HistorialModel()
        {
            historialMarcasDao = new HistorialMarcasDao();
        }

        public void GuardarEtapa(int idMarca, DateTime fecha, string etapa, string anotaciones, string usuario,string origen)
        {
            historialMarcasDao.GuardarEtapa(idMarca, fecha, etapa, anotaciones, usuario, origen);
        }

        public DataTable GetHistorialMarcaById(int id)
        {
            DataTable tabla = new DataTable();
            tabla = historialMarcasDao.GetAllEtapasByIdMarca(id);
            return tabla;
        }

        public DataTable GetHistorialById(int id)
        {
            DataTable tabla = new DataTable();
            tabla = historialMarcasDao.GetHistorialById(id);
            return tabla;
        }

        public bool EditHistorialById(int id, string etapa, DateTime fecha, string anotaciones, string usuario, string usuarioEditor)
        {
            return historialMarcasDao.EditHistorialById(id, etapa, fecha, anotaciones, usuario, usuarioEditor);
        }

        public bool EliminarRegistroHistorial(int id, string deletedBy)
        {
            return historialMarcasDao.EliminarRegistroHistorialYLog(id, deletedBy);
        }
    }
}
