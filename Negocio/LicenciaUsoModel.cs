using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using AccesoDatos.Entidades;
using static Dominio.LicenciaUsoModel;
namespace Dominio
{
    public class LicenciaUsoModel : ConnectionSQL
    {
        private LicenciaUso licenciaDao;

        public LicenciaUsoModel()
        {
            licenciaDao = new LicenciaUso();
        }

        public Task<bool> RenovarLicenciaUso(int idLicencia, string numExpediente, DateTime fechaAntigua, DateTime fechaNueva)
        {
            return licenciaDao.RenovarLicenciaUso(idLicencia, numExpediente, fechaAntigua, fechaNueva);
        }

        public Task<DataTable> ObtenerDatosRenovacionLicencia(int idLicencia)
        {
            return licenciaDao.ObtenerDatosRenovacionLicencia(idLicencia);
        }

        public Task<DataTable> ObtenerLicenciasPorMarca(int idMarca)
        {
            return licenciaDao.ObtenerLicenciasPorMarca(idMarca);
        }

        public Task<bool> EliminarLicenciaUso(int idLicencia, string usuario)
        {
            return licenciaDao.EliminarLicenciaUsoConLog(idLicencia, usuario);
        }


        public async Task<DataTable> FiltrarLicenciasUso(
            string tipoLicencia,
            string expediente,
            string titulo,
            string signo,
            string signoDistintivo,
            string estado,
            string clase,
            string origen,
            string nombreRazonSocial,
            string titular)
        {
            return await licenciaDao.FiltrarLicenciasUso(
                tipoLicencia,
                expediente,
                titulo,
                signo,
                signoDistintivo,
                estado,
                clase,
                origen,
                nombreRazonSocial,
                titular
            );
        }

        public Task<bool> InsertarLicenciaUso(
            int idMarca,
            int idTitular,
            string titulo,
            string tipo,
            DateTime fechaInicio,
            DateTime fechaFin,
            string territorio,
            string nombreRazonSocial,
            string direccion,
            string domicilio,
            string nacionalidad,
            string apoderadoRepresentanteLegal,
            string estado,
            string origen,
            int anios, 
            int meses,
            int dias
        )
        {
            return licenciaDao.InsertarLicenciaUso(
                idMarca, idTitular, titulo, tipo, fechaInicio, fechaFin, territorio, nombreRazonSocial, direccion, domicilio, nacionalidad, apoderadoRepresentanteLegal, estado, origen, anios, meses,dias
            );
        }

        public Task<bool> EditarLicenciaUso(
            int id,
            int idMarca,
            int idTitular,
            string titulo,
            string tipo,
            DateTime fechaInicio,
            DateTime fechaFin,
            string territorio,
            string nombreRazonSocial,
            string direccion,
            string domicilio,
            string nacionalidad,
            string apoderadoRepresentanteLegal,
            string estado,
            string origen,
            int anios, 
            int meses,
            int dias
        )
        {
            return licenciaDao.EditarLicenciaUso(
                id, idMarca, idTitular, titulo, tipo, fechaInicio, fechaFin, territorio, nombreRazonSocial, direccion, domicilio, nacionalidad, apoderadoRepresentanteLegal, estado, origen, anios, meses, dias
            );
        }

        public Task<bool> ExisteOtraLicenciaUsoNoExclusiva(int idMarca, int idLicenciaExcluir, string origen)
        {
            return licenciaDao.ExisteOtraLicenciaUsoNoExclusiva(idMarca, idLicenciaExcluir, origen);
        }


        public Task<DataTable> ObtenerLicenciasUsoNacionalesExclusivas(string estadoFiltro, int pageSize, int registrosOmitidos)
        {
            return licenciaDao.ObtenerLicenciasUsoNacionalesExclusivas(estadoFiltro, pageSize, registrosOmitidos);
        }

        public Task<int> GetTotalLicenciasUsoNacionalesExclusivas(string situacion)
        {
            return licenciaDao.GetTotalLicenciasUsoNacionalesExclusivas(situacion);
        }

        public Task<DataTable> FiltrarLicenciasUsoNacionalesExclusivas(string valor, int pageSize, int registrosOmitidos)
        {
            return licenciaDao.FiltrarLicenciasUsoNacionalesExclusivas(valor, pageSize, registrosOmitidos);
        }

        public Task<int> GetFilteredLicenciasUsoNacionalesExclusivasCount(string valor)
        {
            return licenciaDao.ObtenerCantidadFiltradaLicenciasUsoNacionalesExclusivas(valor);
        }

        //no exclusivas
        public Task<DataTable> ObtenerLicenciasUsoNacionalesNoExclusivas(string estadoFiltro, int pageSize, int registrosOmitidos)
        {
           
            return licenciaDao.ObtenerLicenciasUsoNacionalesNoExclusivas(estadoFiltro, pageSize, registrosOmitidos);
           
        }

        public Task<int> GetTotalLicenciasUsoNacionalesNoExclusivas(string situacion)
        {
            return licenciaDao.ObtenerTotalLicenciasUsoNacionalesNoExclusivas(situacion);
        }

        public Task<DataTable> FiltrarLicenciasUsoNacionalesNoExclusivas(string valor, int pageSize, int registrosOmitidos)
        {
            return licenciaDao.FiltrarLicenciasUsoNacionalesNoExclusivas(valor, pageSize, registrosOmitidos);
        }

        public Task<int> GetFilteredLicenciasUsoNacionalesNoExclusivasCount(string valor)
        {
            return licenciaDao.ObtenerCantidadFiltradaLicenciasUsoNacionalesNoExclusivas(valor);
        }

        public Task<bool> VerificarCompatibilidadLicenciaUso(int idMarca, string tipoLicencia, string origen)
        {
            return licenciaDao.VerificarCompatibilidadLicenciaUso(idMarca, tipoLicencia, origen);
        }


        public Task<DataTable> ObtenerLicenciaUsoPorId(int idLicencia)
        {
            return licenciaDao.ObtenerLicenciaUsoPorId(idLicencia);
        }

        public Task<(int total, DataTable data)> ObtenerLicenciasUsoNacionalesExclusivasCombinado(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            return licenciaDao.GetLicenciasUsoNacionalesExclusivasCombinado(estadoFiltro, currentPageIndex, pageSize);
        }

        public Task<(int total, DataTable datos)> ObtenerLicenciasUsoNacionalesNoExclusivasCombinado(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            return licenciaDao.GetLicenciasUsoNacionalesNoExclusivasCombinado(estadoFiltro, currentPageIndex, pageSize);
        }

        public Task<string> FinalizarLicencia(int idLicencia)
        {
            return licenciaDao.CambiarEstadoLicencia(idLicencia);
        }

    }
}
