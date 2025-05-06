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
    public class LicenciaUsoModel : ConnectionSQL
    {
        private LicenciaUso licenciaDao;

        public LicenciaUsoModel()
        {
            licenciaDao = new LicenciaUso();
        }

        public bool InsertarLicenciaUso(
            int idMarca,
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
                idMarca, titulo, tipo, fechaInicio, fechaFin, territorio, nombreRazonSocial, direccion, domicilio, nacionalidad, apoderadoRepresentanteLegal, estado, origen
            );
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
        public bool ExisteLicenciaUsoExclusiva(int idMarca)
        {
            return licenciaDao.ExisteLicenciaUsoExclusiva(idMarca);
        }


    }
}
