using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Entidades;
using System.Data;
using AccesoDatos.ServiciosEmail;
namespace Dominio
{
    public class VencimientoModel:ConnectionSQL
    {
        private VencimientoDao vencimientoDao;

        public VencimientoModel()
        {
            vencimientoDao = new VencimientoDao();
        }
        public async Task<DataTable> ObtenerVencimientos()
        {
            DataTable vencimientos= new DataTable();
            vencimientos= await vencimientoDao.ObtenerVencimientosReporteAsync();
            return vencimientos;
        }

        public async Task<DataTable> GetAllVencimientos(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await vencimientoDao.ObtenerVencimientosPaginadosAsync(currentPageIndex, pageSize);
            return tabla;
        }
        public async Task<DataTable> FiltrarVencimientos(string filtro, int currentPageIndex, int pageSize)
        {
            return await vencimientoDao.FiltrarVencimientosAsync(filtro, currentPageIndex, pageSize);
        }

        public async Task<int> GetTotalVencimientos()
        {
            return await vencimientoDao.GetTotalVencimientosAsync();
        }
        public async Task<int> GetFilteredVencimientosCount(string value)
        {
            return await vencimientoDao.GetFilteredVencimientosCountAsync(value);
        }

        public async Task<DataTable> ObtenerTodosLosVencimientosFiltradosReporte(string valor)
        {
            return await vencimientoDao.ObtenerVencimientosFiltradosReporteAsync(valor);
        }
        
        public async Task EjecutarProcedimiento()
        {
            await vencimientoDao.EjecutarInsertarVencimientosAsync();
        }

      
        public async Task ActualizarNotificado(int id, string tipo)
        {
            await vencimientoDao.ActualizarNotificadoAsync(id, tipo);

        }

        public async Task EditarTextoRtf(string tipo, string mensaje)
        {
            await vencimientoDao.EditarTextoRtfAsync(tipo, mensaje);
        }

        public async Task<string?> ObtenerTextoRtfPorTipo(string tipo)
        {
            return await vencimientoDao.ObtenerTextoRtfPorTipoAsync(tipo);
        }


    }
}
