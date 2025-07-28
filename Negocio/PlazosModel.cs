using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PlazosModel
    {
        private PlazosDao plazosDao;
        public PlazosModel()
        {
            plazosDao= new PlazosDao();
        }

        public Task<(int total, DataTable datos)> ObtenerPlazosAsync(string tipoRegistro, int pageSize, int currentPageIndex)
        {
            return plazosDao.ObtenerPlazosAsync(tipoRegistro, pageSize, currentPageIndex);
        }

        public Task<(int total, DataTable datos)> ObtenerPlazosFiltradoAsync(string tipoRegistro, int pageSize, int currentPageIndex, string filtroBusqueda)
        {
            return plazosDao.ObtenerPlazosFiltradoAsync(tipoRegistro, pageSize, currentPageIndex, filtroBusqueda);
        }

        public Task<DataTable> FiltrarPlazosAsync(
            string tipoPlazos,
            // int pageSize,
            // int currentPageIndex,
            string filtroExpediente,
            string filtroSigno,
            string filtroEstado,
            string filtroClase,
            string? fechaIngresoInicio,
            string? fechaIngresoFin,
            string? fechaVencimientoInicio,
            string? fechaVencimientoFin,
            string filtroTitular,
            string filtroAgente)
        {
            return plazosDao.FiltrarPlazosAsync(
                tipoPlazos,
                // pageSize,
                // currentPageIndex,
                filtroExpediente,
                filtroSigno,
                filtroEstado,
                filtroClase,
                fechaIngresoInicio,
                fechaIngresoFin,
                fechaVencimientoInicio,
                fechaVencimientoFin,
                filtroTitular,
                filtroAgente);
        }



    }
}
