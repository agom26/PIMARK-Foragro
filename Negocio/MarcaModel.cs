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

        public bool TieneEtapaRegistrada(int idMarca)
        {
            return marcaDao.TieneEtapaRegistrada(idMarca);
        }

        public void InsertarTraspasoYHistorialMarca(
       string numExpediente,
       int idMarca,
       int idTitularAnterior,
       int idTitularNuevo,
       DateTime fecha,
       string etapa,
       string anotaciones,
       string usuario)
        {
            marcaDao.InsertarTraspasoYHistorialMarca(numExpediente, idMarca, idTitularAnterior,
                idTitularNuevo, fecha, etapa, anotaciones, usuario, "TRÁMITE");
        }
        public bool RenovarMarca(string noExpediente, int idMarca, DateTime fechaVencAnt, DateTime fechaVencNueva,
                                DateTime fecha, string etapa, string anotaciones, string usuario)
        {
            return marcaDao.RenovarMarca(noExpediente, idMarca, fechaVencAnt, fechaVencNueva, fecha, etapa, anotaciones, usuario);
        }

        //todas las marcas nacionales
        public DataTable FiltrarMarcasNacionales(string filtro, int currentPageIndex, int pageSize)
        {
            return marcaDao.filtrarMarcasNacionales(filtro, currentPageIndex, pageSize);
        }
        public DataTable FiltrarMarcasInternacionales(string filtro, int currentPageIndex, int pageSize)
        {
            return marcaDao.filtrarMarcasInternacionales(filtro, currentPageIndex, pageSize);
        }
        public bool ExisteRegistro(string registro, int? idMarcaActual)
        {
            return marcaDao.ExisteRegistro(registro, idMarcaActual);
        }
        public int GetTotalMarcasNacionales()
        {
            return marcaDao.GetTotalMarcasNacionales();
        }
        public int GetTotalMarcasInternacionales()
        {
            return marcaDao.GetTotalMarcasInternacionales();
        }
        public int GetFilteredMarcasNacionalesCount(string value)
        {
            return marcaDao.GetFilteredMarcasNacionalesCount(value);
        }
        public int GetFilteredMarcasInternacionalesCount(string value)
        {
            return marcaDao.GetFilteredMarcasInternacionalesCount(value);
        }

        public DataTable GetAllMarcasNacionales(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionales(currentPage, pageSize);
            return tabla;
        }
        public void InsertarExpedienteMarca(string numExpediente, int idMarca, string tipo)
        {
            marcaDao.InsertarExpedienteMarca(numExpediente, idMarca, tipo);
        }

        public DataTable GetAllMarcasInternacionales(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionales(currentPage, pageSize);
            return tabla;
        }


        public int GetTotalMarcasSinRegistro()
        {
            return marcaDao.GetTotalMarcasSinRegistro();
        }
        public int GetFilteredMarcasSinRegistroCount(string value)
        {
            return marcaDao.GetFilteredMarcasSinRegistroCount(value);
        }
        public DataTable GetAllMarcasNacionalesEnTramite(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionalesEnTramite(currentPageIndex, pageSize);
            return tabla;
        }
        public DataTable FiltrarMarcasNacionalesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            return marcaDao.FiltrarMarcasNacionalesEnTramite(filtro, currentPageIndex, pageSize);
        }
        public DataTable FiltrarMarcasInternacionalesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            return marcaDao.FiltrarMarcasInternacionalesEnTramite(filtro, currentPageIndex, pageSize);
        }
        public DataTable FiltrarMarcasNacionalesEnOposicion(string filtro)
        {
            return marcaDao.FiltrarMarcasNacionalesEnOposicion(filtro);
        }
        public DataTable FiltrarMarcasNacionalesInterpuestasEnOposicion(string filtro)
        {
            return marcaDao.FiltrarMarcasNacionalesEnOposicionInterpuestas(filtro);
        }

        public DataTable FiltrarMarcasInternacionalesEnOposicion(string filtro, int currentPage, int pageSize)
        {
            return marcaDao.FiltrarMarcasInternacionalesEnOposicion(filtro, currentPage, pageSize);
        }
        public DataTable FiltrarMarcasInternacionalesInterpuestasEnOposicion(string filtro)
        {
            return marcaDao.FiltrarMarcasInternacionalesEnOposicionInterpuestas(filtro);
        }
        public DataTable FiltrarMarcasNacionalesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            return marcaDao.FiltrarMarcasNacionalesRegistradas(filtro, currentPageIndex, pageSize);
        }
        public int GetTotalMarcasEnTramiteDeRenovacion()
        {
            return marcaDao.GetTotalMarcasEnTramiteDeRenovacion();
        }
        public int GetFilteredMarcasEnTramiteDeRenovacionCount(string value)
        {
            return marcaDao.GetFilteredMarcasEnTramiteDeRenovacionCount(value);
        }
        public DataTable FiltrarMarcasNacionalesEnTramiteDeRenovacion(string filtro, int currentPage, int pageSize)
        {
            return marcaDao.FiltrarMarcasNacionalesEnTramiteDeRenovacion(filtro, currentPage, pageSize);
        }
        public DataTable FiltrarMarcasNacionalesEnTramiteDeTraspaso(string filtro, int currentPage, int pageSize)
        {
            return marcaDao.FiltrarMarcasNacionalesEnTramiteDeTraspaso(filtro, currentPage, pageSize);
        }
        public int GetTotalMarcasInternacionalesSinRegistro()
        {
            return marcaDao.GetTotalMarcasInternacionalesSinRegistro();
        }
        public int GetFilteredMarcasInternacionalesSinRegistroCount(string value)
        {
            return marcaDao.GetFilteredMarcasInternacionalesSinRegistroCount(value);
        }
        public DataTable GetAllMarcasInternacionalesIngresadas(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionalesIngresadas(currentPage, pageSize);
            return tabla;
        }

        public DataTable GetAllMarcasNacionalesEnOposicionRecibidas()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionalesEnOposicion();
            return tabla;
        }
        public int GetTotalMarcasInternacionalesEnOposicionRecibidas(string situacionA, int currentPage, int pageSize)
        {
            return marcaDao.GetTotalMarcasInternacionalesEnOposicionRecibidas(situacionA, currentPage, pageSize);
        }
        public int GetFilteredMarcasInternacionalesRecibidasCount(string value)
        {
            return marcaDao.GetFilteredMarcasInternacionalesRecibidasCount(value);
        }
        public DataTable GetAllMarcasInternacionalesEnOposicion(string situacion,int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionalesEnOposicion(situacion,currentPage, pageSize);
            return tabla;
        }
        public int GetTotalMarcasRegistradas()
        {
            return marcaDao.GetTotalMarcasRegistradas();
        }
        public int GetFilteredMarcasRegistradasCount(string value)
        {
            return marcaDao.GetFilteredMarcasRegistradasCount(value);
        }
        public DataTable GetAllMarcasNacionalesRegistradas(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionalesRegistradas(currentPage, pageSize);
            return tabla;
        }
        //internacionales registradas
        public DataTable FiltrarMarcasInternacionalesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            return marcaDao.FiltrarMarcasInternacionalesRegistradas(filtro, currentPageIndex, pageSize);
        }
        public int GetTotalMarcasInternacionalesRegistradas()
        {
            return marcaDao.GetTotalMarcasInternacionalesRegistradas();
        }
        public int GetFilteredMarcasInternacionalesRegistradasCount(string value)
        {
            return marcaDao.GetFilteredMarcasInternacionalesRegistradasCount(value);
        }
        public DataTable GetAllMarcasInternacionalesRegistradas(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionalesRegistradas(currentPage, pageSize);
            return tabla;
        }
        public int GetTotalMarcasEnAbandono()
        {
            return marcaDao.GetTotalMarcasEnAbandono();
        }
        public int GetFilteredMarcasEnAbandonoCount(string value)
        {
            return marcaDao.GetFilteredMarcasEnAbandonoCount(value);
        }
        public DataTable GetAllMarcasNacionalesEnAbandono(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionalesEnAbandono(currentPage, pageSize);
            return tabla;
        }
        public DataTable FiltrarMarcasEnAbandono(string filtro, int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.FiltrarMarcasNacionalesEnAbandono(filtro, currentPage, pageSize);
            return tabla;
        }
        //internacionales en abandono
        public int GetTotalMarcasInternacionalesEnAbandono()
        {
            return marcaDao.GetTotalMarcasInternacionalesEnAbandono();
        }
        public int GetFilteredMarcasInternacionalesEnAbandonoCount(string value)
        {
            return marcaDao.GetFilteredMarcasInternacionalesEnAbandonoCount(value);
        }
        public DataTable GetAllMarcasInternacionalesEnAbandono(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasInternacionalesEnAbandono(currentPage, pageSize);
            return tabla;
        }
        public DataTable FiltrarMarcasInternacionalesEnAbandono(string filtro, int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.FiltrarMarcasInternacionalesEnAbandono(filtro, currentPage, pageSize);
            return tabla;
        }
        public DataTable GetAllMarcasNacionalesEnTramiteDeRenovacion(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.ObtenerMarcasRegistradasEnTramiteDeRenovacion(currentPage, pageSize);
            return tabla;
        }
        public int GetTotalMarcasEnTramiteDeTraspaso()
        {
            return marcaDao.GetTotalMarcasEnTramiteDeTraspaso();
        }
        public int GetFilteredMarcasEnTramiteDeTraspasoCount(string value)
        {
            return marcaDao.GetFilteredMarcasEnTramiteDeTraspasoCount(value);
        }
        public DataTable GetAllMarcasNacionalesEnTramiteDeTraspaso(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.ObtenerMarcasRegistradasEnTramiteDeTraspaso(currentPage, pageSize);
            return tabla;
        }
        //internacionales en renovacion
        public int GetTotalMarcasInternacionalesEnTramiteDeRenovacion()
        {
            return marcaDao.GetTotalMarcasInternacionalesEnTramiteDeRenovacion();
        }
        public int GetFilteredMarcasInternacionalesEnTramiteDeRenovacionCount(string value)
        {
            return marcaDao.GetFilteredMarcasInternacionalesEnTramiteDeRenovacionCount(value);
        }
        public DataTable FiltrarMarcasInternacionalesEnTramiteDeRenovacion(string filtro, int currentPage, int pageSize)
        {
            return marcaDao.FiltrarMarcasInternacionalesEnTramiteDeRenovacion(filtro, currentPage, pageSize);
        }
        public DataTable GetAllMarcasInternacionalesEnTramiteDeRenovacion(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.ObtenerMarcasInternacionalesRegistradasEnTramiteDeRenovacion(currentPage, pageSize);
            return tabla;
        }
        //internacional en traspaso
        public DataTable FiltrarMarcasInternacionalesEnTramiteDeTraspaso(string filtro, int currentPage, int pageSize)
        {
            return marcaDao.FiltrarMarcasInternacionalesEnTramiteDeTraspaso(filtro, currentPage, pageSize);
        }
        public int GetTotalMarcasInternacionalesEnTramiteDeTraspaso()
        {
            return marcaDao.GetTotalMarcasInternacionalesEnTramiteDeTraspaso();
        }
        public int GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount(string value)
        {
            return marcaDao.GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount(value);
        }
        public DataTable GetAllMarcasInternacionalesEnTramiteDeTraspaso(int currentPage, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.ObtenerMarcasInternacionalesRegistradasEnTramiteDeTraspaso(currentPage, pageSize);
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


        public void ActualizarExpedienteMarca(int p_id, string p_expediente, DateTime fecha, string estado, 
            string anotaciones, string usuario)
        {
            marcaDao.ActualizarExpedienteMarca(p_id, p_expediente, fecha, estado, anotaciones, usuario);
        }



    }
}
