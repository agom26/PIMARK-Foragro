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

        public DataTable GetAllPatentesEnTramite()
        {
            DataTable tabla = new DataTable();
            tabla = patenteDao.GetAllPatentesEnTramite();
            return tabla;
        }
        public DataTable ObtenerPatentePorId(int idPatente)
        {
            return patenteDao.ObtenerPatentePorId(idPatente);
        }

    }
}
