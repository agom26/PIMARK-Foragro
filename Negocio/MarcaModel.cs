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

        public async Task<bool> EliminarMarcaConLog(int idMarca, string usuario)
        {
            return await marcaDao.EliminarMarcaConLog(idMarca, usuario);
        }
        public async Task<DataTable> GetAllMarcasNacionalesParaLicencia(int currentPage, int pageSize)
        {
           return await marcaDao.ObtenerMarcasNacionalesParaLicencia(currentPage, pageSize);
            
        }
        public async Task<DataTable> FiltrarMarcasNacionalesParaLicencia(string filtro, int currentPageIndex, int pageSize)
        {
            return await marcaDao.FiltrarMarcasNacionalesParaLicencia(filtro, currentPageIndex, pageSize);
        }

        public async Task<int> GetTotalMarcasNacionalesParaLicencia()
        {
            return await marcaDao.ObtenerTotalMarcasNacionalesParaLicencia();
        }

        public async Task<int> GetFilteredMarcasNacionalesParaLicenciaCount(string value)
        {
            return await marcaDao.ObtenerTotalFiltradoMarcasNacionalesParaLicencia(value);
        }

        public async Task<bool> TieneEtapaRegistrada(int idMarca)
        {
            return await marcaDao.TieneEtapaRegistrada(idMarca);
        }

        public async void InsertarTraspasoYHistorialMarca(
           string numExpediente,
           int idMarca,
           int idTitularAnterior,
           int idTitularNuevo,
           DateTime fecha,
           string etapa,
           string anotaciones,
           string usuario)
        {
            await marcaDao.InsertarTraspasoYHistorial(numExpediente, idMarca, idTitularAnterior,
                idTitularNuevo, fecha.ToString(), etapa, anotaciones, usuario, "TRÁMITE");
        }

        public async Task<bool> RenovarMarca(string noExpediente, int idMarca, DateTime fechaVencAnt, DateTime fechaVencNueva,
                                DateTime fecha, string etapa, string anotaciones, string usuario)
        {
            return await marcaDao.RenovarMarca(noExpediente, idMarca, fechaVencAnt, fechaVencNueva, fecha, etapa, anotaciones, usuario);
        }

        //todas las marcas nacionales
        public async Task<DataTable> FiltrarMarcasNacionales(string filtro, int pageSize,int currentPageIndex )
        {
            return await marcaDao.FiltrarMarcasNacionales(filtro, pageSize, currentPageIndex);
        }

        public async Task<DataTable> FiltrarMarcasInternacionales(int pageSize, int currentPageIndex,string filtro)
        {
            return await marcaDao.FiltrarMarcasInternacionales(pageSize, currentPageIndex,  filtro);
        }

        public async Task<bool> ExisteRegistro(string registro, int? idMarcaActual)
        {
            return await marcaDao.ExisteRegistro(registro, idMarcaActual);
        }
        public async Task<bool> ExisteRegistroMarcaIngresada(string registro, int? idMarcaActual)
        {
            return await marcaDao.ExisteRegistromarcaIngresada(registro, idMarcaActual);
        }

        public async Task<int> GetTotalMarcasNacionales()
        {
            return await marcaDao.GetTotalMarcasNacionales();
        }

        public async Task<int> GetTotalMarcasInternacionales()
        {
            return await marcaDao.GetTotalMarcasInternacionales();
        }

        public async Task<int> GetFilteredMarcasNacionalesCount(string value)
        {
            return await marcaDao.GetFilteredMarcasNacionalesCount(value);
        }

        public async Task<int> GetFilteredMarcasInternacionalesCount(string value)
        {
            return await marcaDao.GetFilteredMarcasInternacionalesCount(value);
        }

        public async Task<DataTable> GetAllMarcasNacionales(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerTodasMarcasNacionales( pageSize, currentPage);
            
        }

        public async void InsertarExpedienteMarca(string numExpediente, int idMarca, string tipo)
        {
            await marcaDao.InsertarExpedienteMarca(numExpediente, idMarca, tipo);
        }

        public async Task<DataTable> GetAllMarcasInternacionales(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerTodasMarcasInternacionales( pageSize, currentPage);
        }
        public async Task<int> GetTotalMarcasSinRegistro()
        {
            return await marcaDao.GetTotalMarcasSinRegistro();
        }

        public async Task<int> GetFilteredMarcasSinRegistroCount(string value)
        {
            return await marcaDao.GetFilteredMarcasSinRegistroCount(value);
        }

        public async Task<DataTable> GetAllMarcasNacionalesEnTramite(int currentPageIndex, int pageSize)
        {
            return await marcaDao.ObtenerMarcasNacionalesEnTramite(pageSize, currentPageIndex);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            return await marcaDao.FiltrarMarcasNacionalesEnTramite(pageSize, currentPageIndex, filtro  );
        }

        public async Task<DataTable> FiltrarMarcasInternacionalesEnTramite(string filtro, int currentPageIndex, int pageSize)
        {
            return await marcaDao.FiltrarMarcasInternacionalesEnTramite(pageSize,  currentPageIndex, filtro);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesEnOposicion(string filtro)
        {
            return await marcaDao.FiltrarMarcasNacionalesEnOposicion(filtro);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesInterpuestasEnOposicion(string filtro)
        {
            return await marcaDao.FiltrarMarcasNacionalesEnOposicionInterpuestas(filtro);
        }

        public async Task<DataTable> FiltrarMarcasInternacionalesEnOposicion(string filtro, int currentPage, int pageSize)
        {
            return await marcaDao.filtrarMarcasInternacionalesRecibidasEnOposicion(pageSize, currentPage, filtro);
        }

        public async Task<DataTable> FiltrarMarcasInternacionalesInterpuestasEnOposicion(string filtro)
        {
            return await marcaDao.FiltrarMarcasInternacionalesEnOposicionInterpuestas(filtro);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            return await marcaDao.FiltrarMarcasNacionalesRegistradas(pageSize,currentPageIndex, filtro);
        }

        public async Task<int> GetTotalMarcasEnTramiteDeRenovacion()
        {
            return await marcaDao.GetTotalMarcasEnTramiteDeRenovacion();
        }

        public async Task<int> GetFilteredMarcasEnTramiteDeRenovacionCount(string value)
        {
            return await marcaDao.GetFilteredMarcasEnTramiteDeRenovacionCount(value);
        }

        public async Task<DataTable> FiltrarMarcasNacionalesEnTramiteDeRenovacion(string filtro, int currentPage, int pageSize)
        {
            return await marcaDao.FiltrarMarcasNacionalesEnTramiteDeRenovacion(pageSize, currentPage, filtro);
        }

        public async  Task<DataTable> FiltrarMarcasNacionalesEnTramiteDeTraspaso(string filtro, int currentPage, int pageSize)
        {
            return await marcaDao.FiltrarMarcasNacionalesEnTramiteDeTraspaso(pageSize, currentPage, filtro);
        }

        public async Task<int> GetTotalMarcasInternacionalesSinRegistro()
        {
            return await marcaDao.GetTotalMarcasInternacionalesSinRegistro();
        }

        public async Task<int> GetFilteredMarcasInternacionalesSinRegistroCount(string value)
        {
            return await marcaDao.GetFilteredMarcasInternacionalesSinRegistroCount(value);
        }

        public async Task<DataTable> GetAllMarcasInternacionalesIngresadas(int currentPage, int pageSize)
        {
            
            return await marcaDao.ObtenerMarcasInternacionalesSinRegistro(pageSize, currentPage);
            
        }

        public async Task<DataTable> GetAllMarcasNacionalesEnOposicionRecibidas()
        {
            return await marcaDao.ObtenerMarcasNacionalesEnOposicion();
            
        }

        public async Task<int> GetTotalMarcasInternacionalesEnOposicionRecibidas(string situacionA, int pageSize)
        {
            return await marcaDao.GetTotalMarcasInternacionalesEnOposicionRecibidas(situacionA, pageSize);
        }

        public async Task<int> GetFilteredMarcasInternacionalesRecibidasCount(string value)
        {
            return await marcaDao.GetFilteredMarcasInternacionalesRecibidasCount(value);
        }
        public async Task<DataTable> GetAllMarcasInternacionalesEnOposicion(string situacion,int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerMarcasInternacionalesEnOposicionRecibidas(situacion,pageSize, currentPage);
            
        }
        public async Task<int> GetTotalMarcasRegistradas()
        {
            return await marcaDao.GetTotalMarcasRegistradas();
        }

        public async Task<int> GetFilteredMarcasRegistradasCount(string value)
        {
            return await marcaDao.GetFilteredMarcasRegistradasCount(value);
        }

        public async Task<DataTable> GetAllMarcasNacionalesRegistradas(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerMarcasNacionalesRegistradas( pageSize, currentPage);
        }

        //internacionales registradas
        public async Task<DataTable> FiltrarMarcasInternacionalesRegistradas(string filtro, int currentPageIndex, int pageSize)
        {
            return await marcaDao.FiltrarMarcasInternacionalesRegistradas(pageSize, currentPageIndex, filtro);
        }

        public async Task<int> GetTotalMarcasInternacionalesRegistradas()
        {
            return await marcaDao.GetTotalMarcasInternacionalesRegistradas();
        }

        public async Task<int> GetFilteredMarcasInternacionalesRegistradasCount(string value)
        {
            return await marcaDao.GetFilteredMarcasInternacionalesRegistradasCount(value);
        }

        public async Task<DataTable> GetAllMarcasInternacionalesRegistradas(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerMarcasInternacionalesRegistradas(pageSize, currentPage);
            
        }
        public async Task<int> GetTotalMarcasEnAbandono()
        {
            return await marcaDao.GetTotalMarcasEnAbandono();
        }

        public async Task<int> GetFilteredMarcasEnAbandonoCount(string value)
        {
            return await marcaDao.GetFilteredMarcasEnAbandonoCount(value);
        }

        public async Task<DataTable> GetAllMarcasNacionalesEnAbandono(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerMarcasEnAbandono( pageSize, currentPage);
        }

        public async Task<DataTable> FiltrarMarcasEnAbandono(string filtro, int currentPage, int pageSize)
        {
            return await marcaDao.FiltrarMarcasEnAbandono(filtro, pageSize, currentPage );
         
        }
        //internacionales en abandono
        public async Task<int> GetTotalMarcasInternacionalesEnAbandono()
        {
            return await marcaDao.GetTotalMarcasInternacionalesEnAbandono();
        }
        public async Task<int> GetFilteredMarcasInternacionalesEnAbandonoCount(string value)
        {
            return await marcaDao.GetFilteredMarcasInternacionalesEnAbandonoCount(value);
        }

        public async Task<DataTable> GetAllMarcasInternacionalesEnAbandono(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerMarcasInternacionalesEnAbandono(pageSize,currentPage);
        }

        public async Task<DataTable> FiltrarMarcasInternacionalesEnAbandono(string filtro, int currentPage, int pageSize)
        {
            return await marcaDao.FiltrarMarcasInternacionalesEnAbandono(filtro,pageSize,currentPage);
        }

        public async Task<DataTable> GetAllMarcasNacionalesEnTramiteDeRenovacion(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerMarcasRegistradasRenovaciones( pageSize, currentPage);
        }
        public async Task<int> GetTotalMarcasEnTramiteDeTraspaso()
        {
            return await marcaDao.GetTotalMarcasEnTramiteDeTraspaso();
        }
        public async Task<int> GetFilteredMarcasEnTramiteDeTraspasoCount(string value)
        {
            return await marcaDao.GetFilteredMarcasEnTramiteDeTraspasoCount(value);
        }

        public async Task<DataTable> GetAllMarcasNacionalesEnTramiteDeTraspaso(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerMarcasRegistradasEnTramiteDeTraspaso(pageSize, currentPage);
        }
        //internacionales en renovacion
        public async Task<int> GetTotalMarcasInternacionalesEnTramiteDeRenovacion()
        {
            return await marcaDao.GetTotalMarcasInternacionalesEnTramiteDeRenovacion();
        }

        public async Task<int> GetFilteredMarcasInternacionalesEnTramiteDeRenovacionCount(string value)
        {
            return await marcaDao.GetFilteredMarcasInternacionalesEnTramiteDeRenovacionCount(value);
        }
        public async Task<DataTable> FiltrarMarcasInternacionalesEnTramiteDeRenovacion(string filtro, int currentPage, int pageSize)
        {
            return await marcaDao.FiltrarMarcasInternacionalesEnRenovacion(filtro,  pageSize, currentPage);
        }
        public async Task<DataTable> GetAllMarcasInternacionalesEnTramiteDeRenovacion(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerMarcasInternacionalesRegistradasRenovaciones(pageSize,currentPage);
        }
        //internacional en traspaso
        public async Task<DataTable> FiltrarMarcasInternacionalesEnTramiteDeTraspaso(string filtro, int currentPage, int pageSize)
        {
            return await marcaDao.FiltrarMarcaInternacionalEnTramiteDeTraspaso(filtro,  pageSize, currentPage);
        }
        public async Task<int> GetTotalMarcasInternacionalesEnTramiteDeTraspaso()
        {
            return await marcaDao.GetTotalMarcasInternacionalesEnTramiteDeTraspaso();
        }
        public async Task<int> GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount(string value)
        {
            return await marcaDao.GetFilteredMarcasInternacionalesEnTramiteDeTraspasoCount(value);
        }
        public async Task<DataTable> GetAllMarcasInternacionalesEnTramiteDeTraspaso(int currentPage, int pageSize)
        {
            return await marcaDao.ObtenerMarcasInternacionalesRegistradasEnTramiteDeTraspaso( pageSize, currentPage);
        }

        public async Task<DataTable> GetMarcaNacionalById(int id)
        {
            return await marcaDao.ObtenerMarcaNacionalPorId(id);
        }
        public async Task<byte[]> ObtenerLogoMarcaPorId(int id)
        {
            return await marcaDao.ObtenerLogoMarcaPorId(id);
        }

        public async Task<DataTable> GetMarcaInternacionalById(int id)
        {
            return await marcaDao.ObtenerMarcaInternacionalPorId(id);
        }

        public async Task<DataTable> ObtenerTipoMarca(int id)
        {
            return await marcaDao.ObtenerTipoMarca(id);
        }

        public async Task<int> AddMarcaNacional(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, int? idCliente)
        {
            return await marcaDao.AddMarcaNacional(expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud,idCliente);
        }
        public async Task<int> AddMarcaInternacional(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string pais_de_registro, string tiene_poder, int? idCliente)
        {
            return await marcaDao.AddMarcaInternacional(expediente, nombre, signoDistintivo,tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, pais_de_registro, tiene_poder,idCliente);
        }
        public async Task<int> AddMarcaNacionalRegistrada(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string registro, DateTime fechaRegistro, DateTime fechaVencimiento, int? idCliente)
        {
            return await marcaDao.AddMarcaNacionalRegistrada(expediente, nombre, signoDistintivo, tipoSigno, clase, folio, libro, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, registro, fechaRegistro, fechaVencimiento, idCliente);
        }
        public async Task<int> AddMarcaInternacionalRegistrada(string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud,  string pais_de_registro, string tiene_poder, int? idCliente, string registro, string folio, string libro, DateTime fecha_de_registro, DateTime fecha_vencimiento)
        {
            return await marcaDao.AddMarcaInternacionalRegistrada(expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, pais_de_registro, tiene_poder, idCliente, registro, folio, libro, fecha_de_registro, fecha_vencimiento);
        }

        public async Task<bool> EditMarcaNacional(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, int? idCliente)
        {
            return await marcaDao.EditMarcaNacional(id, expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, idCliente);
        }

        public async Task<bool> EditMarcaInternacional(int id, string expediente, string nombre, string signoDistintivo,string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string paisRegistro, string tiene_poder, int? idCliente)
        {
            return await marcaDao.EditMarcaInternacional(id, expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, paisRegistro, tiene_poder, idCliente);
        }

        public async Task<bool> EditMarcaNacionalRegistrada(int id, string expediente, string nombre, string signoDistintivo, string tipoSigno, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string registro, DateTime fechaRegistro, DateTime fechaVencimiento, string erenov, string etrasp, int? idCliente)
        {
            return await marcaDao.EditMarcaNacionalRegistrada(id, expediente, nombre, signoDistintivo, tipoSigno, clase, folio, libro, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, registro, fechaRegistro, fechaVencimiento, erenov, etrasp, idCliente);
        }

        public async Task<bool> EditMarcaInternacionalRegistrada(int id, string expediente, string nombre, string signoDistintivo,string tipoSigno, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string paisRegistro, string tiene_poder, int? idCliente, string registro, string folio, string libro, DateTime fechaRegistro, DateTime fechaVencimiento, string erenov, string etrasp)
        {
            return await marcaDao.EditMarcaInternacionalRegistrada(id, expediente, nombre, signoDistintivo, tipoSigno, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, paisRegistro, tiene_poder, idCliente, registro, folio, libro, fechaRegistro, fechaVencimiento, erenov, etrasp);
        }

        public async Task<DataTable> Filtrar(
            string tipo_filtro,
            string? estado, string? nombre, string? pais, string? folio, string? libro,
            string? registro, string? clase,
            string? fechaSolicitudInicio, string? fechaSolicitudFin,
            string? fechaRegistroInicio, string? fechaRegistroFin,
            string? fechaVencimientoInicio, string? fechaVencimientoFin,
            string? titular, string? agente, string? cliente)
        {
            return await marcaDao.FiltrarMarcas(
                tipo_filtro, estado, nombre, pais, folio, libro, registro, clase,
                fechaSolicitudInicio, fechaSolicitudFin,
                fechaRegistroInicio, fechaRegistroFin,
                fechaVencimientoInicio, fechaVencimientoFin,
                 titular, agente,cliente);
        }


        public async void ActualizarExpedienteMarca(int p_id, string p_expediente, DateTime fecha, string estado, 
            string anotaciones, string usuario)
        {
            await marcaDao.ActualizarExpedienteMarca(p_id, p_expediente, fecha.ToString(), estado, anotaciones, usuario);
        }



    }
}
