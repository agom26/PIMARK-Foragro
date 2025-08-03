using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class BusquedaRetrospectivaModel
    {
        private readonly BusquedaRetrospectivaDao dao;

        public BusquedaRetrospectivaModel()
        {
            dao = new BusquedaRetrospectivaDao();
        }

        public Task<(int? idBusqueda, string mensajeError)> CrearBusquedaAsync(string signo, string clase, string signoDistintivo, string tipo, bool multiclase)
        {
            return dao.CrearBusquedaAsync(signo, clase, signoDistintivo, tipo, multiclase);
        }


        public Task<bool> AgregarPaisAsync(int idBusqueda, string pais, string nivel, string observaciones)
        {
            return dao.AgregarPaisAsync(idBusqueda, pais, nivel, observaciones);
        }

        public Task<(DataRow busqueda, DataTable paises)> ObtenerBusquedaConPaisesAsync(int idBusqueda)
        {
            return dao.ObtenerBusquedaConPaisesAsync(idBusqueda);
        }

        public Task<(int total, DataTable datos)> ObtenerBusquedasAsync(int pageSize, int currentPageIndex)
        {
            return dao.ObtenerBusquedasAsync(pageSize, currentPageIndex);
        }

        public Task<(int total, DataTable datos)> ObtenerBusquedasFiltradoAsync(string filtro, int pageSize, int currentPageIndex)
        {
            return dao.ObtenerBusquedasFiltradoAsync(filtro, pageSize, currentPageIndex);
        }

        public Task<string> EditarBusquedaAsync(int idBusqueda, string signo, string clase, string signoDistintivo, string tipo,bool multiclase)
        {
            return dao.EditarBusquedaAsync(idBusqueda, signo, clase, signoDistintivo, tipo, multiclase);
        }

        public Task<bool> EliminarPaisAsync(int idPais)
        {
            return dao.EliminarPaisAsync(idPais);
        }

        public Task<string> EditarPaisAsync(int idPais, int idBusqueda, string pais, string nivel, string observaciones)
        {
            return dao.EditarPaisAsync(idPais, idBusqueda, pais, nivel, observaciones);
        }

        public Task<bool> EliminarPaisesPorBusquedaAsync(int idBusqueda)
        {
            return dao.EliminarPaisesPorBusquedaAsync(idBusqueda);
        }


    }
}
