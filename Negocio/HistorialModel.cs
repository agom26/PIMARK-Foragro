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

        public void GuardarEtapa(int idMarca, DateTime fecha, string etapa, string anotaciones, string usuario)
        {
            historialMarcasDao.GuardarEtapa(idMarca, fecha, etapa, anotaciones, usuario);
        }

        public DataTable GetHistorialMarcaById(int id)
        {
            DataTable tabla = new DataTable();
            tabla = historialMarcasDao.GetAllEtapasByIdMarca(id);
            return tabla;
        }
    }
}
