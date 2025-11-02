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
    public class PatenteModel : ConnectionSQL
    {
        private PatenteDao patenteDao;

        public PatenteModel()
        {
            patenteDao = new PatenteDao();
        }

        public async Task<bool> TieneEtapaRegistradaPatente(int idPatente)
        {
            return await patenteDao.TieneEtapaRegistradaPatente(idPatente);
        }

        public async Task InsertarTraspasoYHistorial(
         string numExpediente,
         int idPatente,
         int idTitularAnterior,
         int idTitularNuevo,
         DateTime fecha,
         string etapa,
         string anotaciones,
         string usuario,
         string usuarioEdicion)
        {
            await patenteDao.InsertarTraspasoYHistorial(numExpediente, idPatente, idTitularAnterior,
                idTitularNuevo, fecha, etapa, anotaciones, usuario, usuarioEdicion);
        }

        public async Task<bool> RenovarPatente(string noExpediente, int idPatente, DateTime fechaVencAnt, DateTime fechaVencNueva,
                                DateTime fecha, string etapa, string anotaciones, string usuario)
        {
            return await patenteDao.RenovarPatente(noExpediente, idPatente, fechaVencAnt, fechaVencNueva, fecha, etapa, anotaciones, usuario);
        }
        public async Task InsertarExpedientePatente(string numExpediente, int idPatente, string tipo)
        {
            await patenteDao.InsertarExpedientePatente(numExpediente, idPatente, tipo);
        }

        public async Task<int> CrearPatente(
           string caso,
           string expediente,
           string nombre,
           string estado,
           string tipo,
           int idTitular,
           int idAgente,
           DateTime fechaSolicitud,
           string registro,
           string folio,
           string libro,
           DateTime? fechaRegistro,
           DateTime? fechaVencimiento,
           string erenov,
           string etrasp,
           int anualidades,
           string pct,
           string comprobantePagos,
           string descripcion,
           string reivindicaciones,
           string dibujos,
           string resumen,
           string documentoCesion,
           string poderNombramiento)
        {
            return await patenteDao.InsertarPatente(
                caso, expediente, nombre, estado, tipo,
                idTitular, idAgente, fechaSolicitud,
                registro, folio, libro, fechaRegistro,
                fechaVencimiento, erenov, etrasp,
                anualidades, pct, comprobantePagos,
                descripcion, reivindicaciones, dibujos,
                resumen, documentoCesion, poderNombramiento
            );
        }

        //patentes en tramite

        public async Task<DataTable> FiltrarPatentesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            return await patenteDao.FiltrarPatentesEnTramite(filtro, currentPageIndex, pageSize);
        }
        public async Task<int> GetTotalPatentesSinRegistro()
        {
            return await patenteDao.GetTotalPatentesSinRegistro();
        }
        public async Task<int> GetFilteredPatentesSinRegistroCount(string value)
        {
            return await patenteDao.GetFilteredPatentesSinRegistroCount(value);
        }
        public async Task<DataTable> GetAllPatentesEnTramite(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await patenteDao.GetAllPatentesEnTramite(currentPage, pageSize);
            return tabla;
        }

        //patentes registradas
        public async Task<DataTable> FiltrarPatentesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            return await patenteDao.FiltrarPatentesRegistradas(filtro, currentPageIndex, pageSize);
        }
        public async Task<int> GetTotalPatentesRegistradas()
        {
            return await patenteDao.GetTotalPatentesRegistradas();
        }
        public async Task<int> GetFilteredPatentesRegistradasCount(string value)
        {
            return await patenteDao.GetFilteredPatentesRegistradasCount(value);
        }
        public async Task<DataTable> GetAllPatentesRegistradas(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await patenteDao.GetAllPatentesRegistradas(currentPage, pageSize);
            return tabla;
        }
        //patentes en renovacion
        public async Task<DataTable> FiltrarPatentesRegistradasEnTramiteDeRenovacion(string filtro, int currentPageIndex, int pageSize)
        {
            return await patenteDao.FiltrarPatentesRegistradasEnTramiteDeRenovacion(filtro, currentPageIndex, pageSize);
        }
        public async Task<int> GetTotalPatentesRegistradasEnTramiteDeRenovacion()
        {
            return await patenteDao.GetTotalPatentesRegistradasEnTramiteDeRenovacion();
        }
        public async Task<int> GetFilteredPatentesRegistradasEnTramiteDeRenovacionCount(string value)
        {
            return await patenteDao.GetFilteredPatentesRegistradasEnTramiteDeRenovacionCount(value);
        }
        public async Task<DataTable> GetAllPatentesRegistradasEnTramiteDeRenovacion(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await patenteDao.GetAllPatentesRegistradasEnTramiteDeRenovacion(currentPage, pageSize);
            return tabla;
        }


        //patentes en traspaso
        public async Task<DataTable> FiltrarPatentesRegistradasEnTramiteDeTraspaso(string filtro, int currentPageIndex, int pageSize)
        {
            return await patenteDao.FiltrarPatentesRegistradasEnTramiteDeTraspaso(filtro, currentPageIndex, pageSize);
        }
        public async Task<int> GetTotalPatentesRegistradasEnTramiteDeTraspaso()
        {
            return await patenteDao.GetTotalPatentesRegistradasEnTramiteDeTraspaso();
        }
        public async Task<int> GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount(string value)
        {
            return await patenteDao.GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount(value);
        }
        public async Task<DataTable> GetAllPatentesRegistradasEnTramiteDeTraspaso(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await patenteDao.GetAllPatentesRegistradasEnTramiteDeTraspaso(currentPage, pageSize);
            return tabla;
        }

        //patentes en abandono
        public async Task<DataTable> FiltrarPatentesEnAbandono(string filtro, int currentPageIndex, int pageSize)
        {
            return await patenteDao.FiltrarPatentesEnAbandono(filtro, currentPageIndex, pageSize);
        }
        public async Task<int> GetTotalPatentesEnAbandono()
        {
            return await patenteDao.GetTotalPatentesEnAbandono();
        }
        public async Task<int> GetFilteredPatentesEnAbandonoCount(string value)
        {
            return await patenteDao.GetFilteredPatentesEnAbandonoCount(value);
        }
        public async Task<DataTable> GetAllPatentesEnAbandono(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await patenteDao.GetAllPatentesEnAbandono(currentPage, pageSize);
            return tabla;
        }

        //desistimiento
        public async Task<DataTable> FiltrarPatentesEnDesistimiento(string filtro, int currentPageIndex, int pageSize)
        {
            return await patenteDao.FiltrarPatentesEnDesistimiento(filtro, currentPageIndex, pageSize);
        }
        public async Task<int> GetTotalPatentesEnDesistimiento()
        {
            return await patenteDao.GetTotalPatentesEnDesistimiento();
        }
        public async Task<int> GetFilteredPatentesEnDesistimientoCount(string value)
        {
            return await patenteDao.GetFilteredPatentesEnDesistimientoCount(value);
        }
        public async Task<DataTable> GetAllPatentesEnDesistimiento(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await patenteDao.GetAllPatentesEnDesistimiento(currentPage, pageSize);
            return tabla;
        }
        public async Task<DataTable> ObtenerPatentePorId(int idPatente)
        {
            return await patenteDao.ObtenerPatentePorId(idPatente);
        }
        public async Task<bool> EditarPatente(
            int id,
            string caso,
            string expediente,
            string nombre,
            string estado,
            string tipo,
            int idTitular,
            int idAgente,
            DateTime fechaSolicitud,
            string registro,
            string folio,
            string libro,
            DateTime? fechaRegistro,
            DateTime? fechaVencimiento,
            string erenov,
            string etrasp,
            int anualidades,
            string pct,
            string comprobantePagos,
            string descripcion,
            string reivindicaciones,
            string dibujos,
            string resumen,
            string documentoCesion,
            string poderNombramiento)
        {
            return await patenteDao.EditarPatente(
                id, caso, expediente, nombre, estado, tipo,
                idTitular, idAgente, fechaSolicitud,
                registro, folio, libro, fechaRegistro,
                fechaVencimiento, erenov, etrasp, anualidades,
                pct, comprobantePagos, descripcion, reivindicaciones,
                dibujos, resumen, documentoCesion, poderNombramiento
            );
        }

        public async Task ActualizarExpedientePatente(int p_id, string p_expediente, DateTime fecha, string estado,
          string anotaciones, string usuario)
        {
            await patenteDao.ActualizarExpedientePatente(p_id, p_expediente, fecha, estado, anotaciones, usuario);
        }


    }
}
