using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using AccesoDatos.Entidades;

namespace Dominio
{
    public class RenovacionesMarcaModel:ConnectionSQL
    {
        private RenovacionesMarcasDao renovacionesDao;

        public RenovacionesMarcaModel()
        {
            renovacionesDao = new RenovacionesMarcasDao();
        }

        public async void AddRenovacion(string numExpediente, int idMarca, DateTime fechaVencimientoAntigua, DateTime fechaVencimientoNueva) 
        {
            await renovacionesDao.InsertRenovacionMarca(numExpediente, idMarca,  fechaVencimientoAntigua, fechaVencimientoNueva);
        }

        public async Task<DataTable> GetAllRenovacionesByIdMarca(int idMarca)
        {
            return await renovacionesDao.ObtenerRenovacionesDeMarcaPorId(idMarca);
        }

        public async Task<DataTable> GetRenovacionById(int id)
        {
            return await renovacionesDao.ObtenerRenovacionPorId(id);
        }
        public async Task<bool> ActualizarRenovacion(int id, string numExpediente, int idMarca, DateTime fechaVencimientoAntigua, DateTime fechaVencimientoNueva)
        {
            return await renovacionesDao.ActualizarRenovacionMarca(id, numExpediente, idMarca, fechaVencimientoAntigua, fechaVencimientoNueva);
        }



    }
}
