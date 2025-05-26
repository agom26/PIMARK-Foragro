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

        public DataTable FiltrarLicenciasUso(
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
            return licenciaDao.FiltrarLicenciasUso(
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

        public bool InsertarLicenciaUso(
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
            string origen
        )
        {
            return licenciaDao.InsertarLicenciaUso(
                idMarca, idTitular, titulo, tipo, fechaInicio, fechaFin, territorio, nombreRazonSocial, direccion, domicilio, nacionalidad, apoderadoRepresentanteLegal, estado, origen
            );
        }

        public bool EditarLicenciaUso(
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
            string origen
        )
        {
            return licenciaDao.EditarLicenciaUso(
                id, idMarca, idTitular, titulo, tipo, fechaInicio, fechaFin, territorio, nombreRazonSocial, direccion, domicilio, nacionalidad, apoderadoRepresentanteLegal, estado, origen
            );
        }

        public bool ExisteOtraLicenciaUsoNoExclusiva(int idMarca, int idLicenciaExcluir, string origen)
        {
            return licenciaDao.ExisteOtraLicenciaUsoNoExclusiva(idMarca, idLicenciaExcluir, origen);
        }


        public DataTable ObtenerLicenciasUsoNacionalesExclusivas(string estadoFiltro, int pageSize, int registrosOmitidos)
        {
            return licenciaDao.ObtenerLicenciasUsoNacionalesExclusivas(estadoFiltro, pageSize, registrosOmitidos);
        }

        public int GetTotalLicenciasUsoNacionalesExclusivas(string situacion)
        {
            return licenciaDao.GetTotalLicenciasUsoNacionalesExclusivas(situacion);
        }

        public DataTable FiltrarLicenciasUsoNacionalesExclusivas(string valor, int pageSize, int registrosOmitidos)
        {
            return licenciaDao.FiltrarLicenciasUsoNacionalesExclusivas(valor, pageSize, registrosOmitidos);
        }

        public int GetFilteredLicenciasUsoNacionalesExclusivasCount(string valor)
        {
            return licenciaDao.GetFilteredLicenciasUsoNacionalesExclusivasCount(valor);
        }

        //no exclusivas
        public DataTable ObtenerLicenciasUsoNacionalesNoExclusivas(string estadoFiltro, int pageSize, int registrosOmitidos)
        {
            DataTable tabla = new DataTable();
            tabla=licenciaDao.ObtenerLicenciasUsoNacionalesNoExclusivas(estadoFiltro, pageSize, registrosOmitidos);
            return tabla;
        }

        public int GetTotalLicenciasUsoNacionalesNoExclusivas(string situacion)
        {
            return licenciaDao.GetTotalLicenciasUsoNacionalesNoExclusivas(situacion);
        }

        public DataTable FiltrarLicenciasUsoNacionalesNoExclusivas(string valor, int pageSize, int registrosOmitidos)
        {
            return licenciaDao.FiltrarLicenciasUsoNacionalesNoExclusivas(valor, pageSize, registrosOmitidos);
        }

        public int GetFilteredLicenciasUsoNacionalesNoExclusivasCount(string valor)
        {
            return licenciaDao.GetFilteredLicenciasUsoNacionalesNoExclusivasCount(valor);
        }
        public bool VerificarCompatibilidadLicenciaUso(int idMarca, string tipoLicencia, string origen)
        {
            return licenciaDao.VerificarCompatibilidadLicenciaUso(idMarca, tipoLicencia, origen);
        }


        public DataTable ObtenerLicenciaUsoPorId(int idLicencia)
        {
            DataTable tabla = new DataTable();
            tabla=licenciaDao.ObtenerLicenciaUsoPorId(idLicencia);
            return tabla;
        }

        public (int total, DataTable datos) ObtenerLicenciasUsoNacionalesExclusivasCombinado(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            return licenciaDao.ObtenerLicenciasUsoNacionalesExclusivasCombinado(estadoFiltro, currentPageIndex, pageSize);
        }

        public (int total, DataTable datos) ObtenerLicenciasUsoNacionalesNoExclusivasCombinado(string estadoFiltro, int currentPageIndex, int pageSize)
        {
            return licenciaDao.ObtenerLicenciasUsoNacionalesNoExclusivasCombinado(estadoFiltro, currentPageIndex, pageSize);
        }

        public string FinalizarLicencia(int idLicencia)
        {
            return licenciaDao.CambiarEstadoLicencia(idLicencia);
        }

    }
}
