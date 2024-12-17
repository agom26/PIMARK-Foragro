using AccesoDatos.Usuarios;
using AccesoDatos;
using System.Data;
using System.Security.Policy;
using Comun.Cache;
using AccesoDatos.Entidades;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Dominio
{
    public class MarcaModel:ConnectionSQL
    {
        private MarcaDao marcaDao;

        public MarcaModel()
        {
            marcaDao = new MarcaDao();
        }
        public DataTable GetAllMarcasInternacionales()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionales();
            return tabla;
        }
        public DataTable GetAllMarcasNacionalesEnTramite()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionalesEnTramite();
            return tabla;
        }
        public DataTable FiltrarMarcasNacionalesEnTramite(string filtro)
        {
            return marcaDao.FiltrarMarcasNacionalesEnTramite(filtro);
        }
        public DataTable FiltrarMarcasNacionalesEnOposicion(string filtro)
        {
            return marcaDao.FiltrarMarcasNacionalesEnOposicion(filtro);
        }
        public DataTable FiltrarMarcasNacionalesRegistradas(string filtro)
        {
            return marcaDao.FiltrarMarcasNacionalesRegistradas(filtro);
        }
        public DataTable FiltrarMarcasNacionalesEnTramiteDeRenovacion(string filtro)
        {
            return marcaDao.FiltrarMarcasNacionalesEnTramiteDeRenovacion(filtro);
        }
        public DataTable GetAllMarcasInternacionalesIngresadas()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionalesIngresadas();
            return tabla;
        }

        public DataTable GetAllMarcasNacionalesEnOposicion()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionalesEnOposicion();
            return tabla;
        }
        public DataTable GetAllMarcasInternacionalesEnOposicion()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionalesEnOposicion();
            return tabla;
        }
        public DataTable GetAllMarcasNacionalesRegistradas()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionalesRegistradas();
            return tabla;
        }
        public DataTable GetAllMarcasInternacionalesRegistradas()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionalesRegistradas();
            return tabla;
        }
        public DataTable GetAllMarcasNacionalesEnAbandono()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionalesEnAbandono();
            return tabla;
        }
        public DataTable GetAllMarcasInternacionalesEnAbandono()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionalesEnAbandono();
            return tabla;
        }
        public DataTable GetAllMarcasNacionalesEnTramiteDeRenovacion()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.ObtenerMarcasRegistradasEnTramiteDeRenovacion();
            return tabla;
        }
        public DataTable GetAllMarcasNacionalesEnTramiteDeTraspaso()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.ObtenerMarcasRegistradasEnTramiteDeTraspaso();
            return tabla;
        }
        public DataTable GetAllMarcasInternacionalesEnTramiteDeRenovacion()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.ObtenerMarcasInternacionalesRegistradasEnTramiteDeRenovacion();
            return tabla;
        }
        public DataTable GetAllMarcasInternacionalesEnTramiteDeTraspaso()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.ObtenerMarcasInternacionalesRegistradasEnTramiteDeTraspaso();
            return tabla;
        }


        public DataTable GetMarcaNacionalById(int id)
        {
            return marcaDao.GetMarcaNacionalById(id);
        }
        public DataTable GetMarcaInternacionalById(int id)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetMarcaInternacionalById(id);
            return tabla;
        }

        public DataTable ObtenerTipoMarca(int id)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.ObtenerTipoMarca(id);
            return tabla;
        }

        public int AddMarcaNacional(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, int? idCliente)
        {
            return marcaDao.AddMarcaNacional(expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud,idCliente);
        }
        public int AddMarcaInternacional(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string pais_de_registro, string tiene_poder, int? idCliente)
        {
            return marcaDao.AddMarcaInternacional(expediente, nombre, signoDistintivo,tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, pais_de_registro, tiene_poder,idCliente);
        }
        public int AddMarcaNacionalRegistrada(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string registro, DateTime fechaRegistro, DateTime fechaVencimiento, int? idCliente)
        {
            return marcaDao.AddMarcaNacionalRegistrada(expediente, nombre, signoDistintivo, tipoSigno, clase, folio, libro, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, registro, fechaRegistro, fechaVencimiento, idCliente);
        }
        public int AddMarcaInternacionalRegistrada(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud,  string pais_de_registro, string tiene_poder, int? idCliente, string registro, string folio, string libro, DateTime fecha_de_registro, DateTime fecha_vencimiento)
        {
            return marcaDao.AddMarcaInternacionalRegistrada(expediente,nombre, signoDistintivo, tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, pais_de_registro, tiene_poder, idCliente, registro, folio, libro, fecha_de_registro, fecha_vencimiento);
        }

        public bool EditMarcaNacional(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, int? idCliente)
        {
            return marcaDao.EditMarcaNacional(id, expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud,idCliente);
        }
        public bool EditMarcaInternacional(int id, string expediente, string nombre, string signoDistintivo,string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string paisRegistro, string tiene_poder, int? idCliente)
        {
            return marcaDao.EditarMarcaInternacional(id, expediente, nombre, signoDistintivo,tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, paisRegistro, tiene_poder, idCliente);
        }

        public bool EditMarcaNacionalRegistrada(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string registro, DateTime fechaRegistro, DateTime fechaVencimiento, string erenov, string etrasp, int? idCliente)
        {
            return marcaDao.EditMarcaNacionalRegistrada(id, expediente, nombre, signoDistintivo, tipoSigno, clase, folio, libro, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, registro, fechaRegistro, fechaVencimiento, erenov, etrasp, idCliente);
        }

        public bool EditMarcaInternacionalRegistrada(int id, string expediente, string nombre, string signoDistintivo,string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string paisRegistro, string tiene_poder, int? idCliente, string registro, string folio, string libro, DateTime fechaRegistro, DateTime fechaVencimiento, string erenov, string etrasp)
        {
            return marcaDao.EditarMarcaInternacionalRegistrada(id, expediente, nombre, signoDistintivo,tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, paisRegistro, tiene_poder, idCliente, registro, folio, libro, fechaRegistro, fechaVencimiento, erenov, etrasp);
        }

        public DataTable Filtrar(
        string tipo_filtro,
        string? estado, string? nombre, string? pais, string? folio, string? libro,
        string? registro, string? clase,
        string? fechaSolicitudInicio, string? fechaSolicitudFin,
        string? fechaRegistroInicio, string? fechaRegistroFin,
        string? fechaVencimientoInicio, string? fechaVencimientoFin,
        string? titular, string? agente, string? cliente)
        {
            return marcaDao.Filtrar(
                tipo_filtro, estado, nombre, pais, folio, libro, registro, clase,
                fechaSolicitudInicio, fechaSolicitudFin,
                fechaRegistroInicio, fechaRegistroFin,
                fechaVencimientoInicio, fechaVencimientoFin,
                 titular, agente,cliente);
        }


    }
}
