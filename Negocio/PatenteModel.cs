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
        public void InsertarExpedientePatente(string numExpediente, int idPatente, string tipo)
        {
            patenteDao.InsertarExpedientePatente(numExpediente, idPatente, tipo);
        }

        public int CrearPatente(
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
            return patenteDao.InsertarPatente(
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

        public DataTable FiltrarPatentesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            return patenteDao.FiltrarPatentesEnTramite(filtro, currentPageIndex, pageSize);
        }
        public int GetTotalPatentesSinRegistro()
        {
            return patenteDao.GetTotalPatentesSinRegistro();
        }
        public int GetFilteredPatentesSinRegistroCount(string value)
        {
            return patenteDao.GetFilteredPatentesSinRegistroCount(value);
        }
        public DataTable GetAllPatentesEnTramite(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = patenteDao.GetAllPatentesEnTramite(currentPage, pageSize);
            return tabla;
        }

        //patentes registradas
        public DataTable FiltrarPatentesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            return patenteDao.FiltrarPatentesRegistradas(filtro, currentPageIndex, pageSize);
        }
        public int GetTotalPatentesRegistradas()
        {
            return patenteDao.GetTotalPatentesRegistradas();
        }
        public int GetFilteredPatentesRegistradasCount(string value)
        {
            return patenteDao.GetFilteredPatentesRegistradasCount(value);
        }
        public DataTable GetAllPatentesRegistradas(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = patenteDao.GetAllPatentesRegistradas(currentPage, pageSize);
            return tabla;
        }
        //patentes en renovacion
        public DataTable FiltrarPatentesRegistradasEnTramiteDeRenovacion(string filtro, int currentPageIndex, int pageSize)
        {
            return patenteDao.FiltrarPatentesRegistradasEnTramiteDeRenovacion(filtro, currentPageIndex, pageSize);
        }
        public int GetTotalPatentesRegistradasEnTramiteDeRenovacion()
        {
            return patenteDao.GetTotalPatentesRegistradasEnTramiteDeRenovacion();
        }
        public int GetFilteredPatentesRegistradasEnTramiteDeRenovacionCount(string value)
        {
            return patenteDao.GetFilteredPatentesRegistradasEnTramiteDeRenovacionCount(value);
        }
        public DataTable GetAllPatentesRegistradasEnTramiteDeRenovacion(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = patenteDao.GetAllPatentesRegistradasEnTramiteDeRenovacion(currentPage, pageSize);
            return tabla;
        }


        //patentes en traspaso
        public DataTable FiltrarPatentesRegistradasEnTramiteDeTraspaso(string filtro, int currentPageIndex, int pageSize)
        {
            return patenteDao.FiltrarPatentesRegistradasEnTramiteDeTraspaso(filtro, currentPageIndex, pageSize);
        }
        public int GetTotalPatentesRegistradasEnTramiteDeTraspaso()
        {
            return patenteDao.GetTotalPatentesRegistradasEnTramiteDeTraspaso();
        }
        public int GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount(string value)
        {
            return patenteDao.GetFilteredPatentesRegistradasEnTramiteDeTraspasoCount(value);
        }
        public DataTable GetAllPatentesRegistradasEnTramiteDeTraspaso(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = patenteDao.GetAllPatentesRegistradasEnTramiteDeTraspaso(currentPage, pageSize);
            return tabla;
        }

        //patentes en abandono
        public DataTable FiltrarPatentesEnAbandono(string filtro, int currentPageIndex, int pageSize)
        {
            return patenteDao.FiltrarPatentesEnAbandono(filtro, currentPageIndex, pageSize);
        }
        public int GetTotalPatentesEnAbandono()
        {
            return patenteDao.GetTotalPatentesEnAbandono();
        }
        public int GetFilteredPatentesEnAbandonoCount(string value)
        {
            return patenteDao.GetFilteredPatentesEnAbandonoCount(value);
        }
        public DataTable GetAllPatentesEnAbandono(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = patenteDao.GetAllPatentesEnAbandono(currentPage, pageSize);
            return tabla;
        }
        public DataTable ObtenerPatentePorId(int idPatente)
        {
            return patenteDao.ObtenerPatentePorId(idPatente);
        }
        public bool EditarPatente(
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
            return patenteDao.EditarPatente(
                id, caso, expediente, nombre, estado, tipo,
                idTitular, idAgente, fechaSolicitud,
                registro, folio, libro, fechaRegistro,
                fechaVencimiento, erenov, etrasp, anualidades,
                pct, comprobantePagos, descripcion, reivindicaciones,
                dibujos, resumen, documentoCesion, poderNombramiento
            );
        }

        public void ActualizarExpedientePatente(int p_id, string p_expediente, DateTime fecha, string estado,
          string anotaciones, string usuario)
        {
            patenteDao.ActualizarExpedientePatente(p_id, p_expediente, fecha, estado, anotaciones, usuario);
        }


    }
}
