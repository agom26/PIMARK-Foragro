using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
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
    }
}
