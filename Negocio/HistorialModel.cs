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

        public async void GuardarEtapa(int idMarca, DateTime fecha, string etapa, string anotaciones, string usuario,string origen)
        {
            await historialMarcasDao.GuardarEtapa(idMarca, fecha, etapa, anotaciones, usuario, origen);
        }

        public async Task<DataTable> GetHistorialMarcaById(int id)
        {
            return await historialMarcasDao.GetAllEtapasByIdMarca(id);
        }


        public async Task<DataTable> GetHistorialById(int id)
        {
            return await historialMarcasDao.GetHistorialById(id);
        }

        public async Task<bool> EditHistorialById(int id, string etapa, DateTime fecha, string anotaciones, string usuario, string usuarioEditor)
        {
            return await historialMarcasDao.EditHistorialById(id, etapa, fecha, anotaciones, usuario, usuarioEditor);
        }


        public bool EliminarRegistroHistorial(int id, string deletedBy)
        {
            return historialMarcasDao.EliminarRegistroHistorialYLog(id, deletedBy).Result;
        }
    }
}
