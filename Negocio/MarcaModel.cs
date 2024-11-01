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

        public DataTable GetAllMarcasNacionales()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionales();
            return tabla;
        }
        public DataTable GetAllMarcasNacionalesEnTramite()
        {
            DataTable tabla = new DataTable();
            tabla = marcaDao.GetAllMarcasNacionalesEnTramite();
            return tabla;
        }


        public List<(int id, string expediente, string nombre, string signoDistintivo, string clase, string folio, string libro, byte[] logo, string estado, string registro, DateTime? fechaSolicitud, DateTime? fechaRegistro, DateTime? fechaVencimiento, int idTitular, int idAgente, string observaciones)> GetMarcaNacionalById(int id)
        {
            return marcaDao.GetMarcaNacionalById(id);
        }


        public int AddMarcaNacional(string expediente, string nombre, string signoDistintivo, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado, string observaciones)
        {
            return marcaDao.AddMarcaNacional(expediente, nombre, signoDistintivo, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, estado, observaciones);
        }
        public int AddMarcaInternacional(string expediente, string nombre, string signoDistintivo, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado, string pais_de_registro, string tiene_poder, DateTime presentacion, DateTime ultimo_pago, DateTime vencimiento, int idCliente)
        {
            return marcaDao.AddMarcaInternacional(expediente, nombre, signoDistintivo, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, estado, pais_de_registro, tiene_poder, presentacion, ultimo_pago, vencimiento,idCliente);
        }
        public int AddMarcaNacionalRegistrada(string expediente, string nombre, string signoDistintivo, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado, string observaciones, string registro, DateTime fechaRegistro, DateTime fechaVencimiento)
        {
            return marcaDao.AddMarcaNacionalRegistrada(expediente, nombre, signoDistintivo, clase, folio, libro, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, estado, observaciones, registro, fechaRegistro, fechaVencimiento);
        }
        public bool AddMarcaInternacionalRegistrada(string expediente, string nombre, string signoDistintivo, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado, string pais_de_registro, string tiene_poder, DateTime presentacion, DateTime ultimo_pago, DateTime vencimiento, int idCliente, string registro, string folio, string libro, DateTime fecha_de_registro, DateTime fecha_vencimiento)
        {
            return marcaDao.AddMarcaInternacionalRegistrada(expediente, nombre, signoDistintivo, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, estado, pais_de_registro, tiene_poder, presentacion, ultimo_pago, vencimiento, idCliente, registro, folio, libro, fecha_de_registro, fecha_vencimiento);
        }



    }
}
