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

        public bool AddMarcaNacional(string expediente, string nombre, string signoDistintivo, string clase, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado)
        {
            return marcaDao.AddMarcaNacional(expediente, nombre, signoDistintivo, clase, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, estado);
        }
        public bool AddMarcaNacionalRegistrada(string expediente, string nombre, string signoDistintivo, string clase, string folio, string libro, byte[] logo, int idPersonaTitular, int idPersonaAgente, DateTime fecha_solicitud, string estado, string registro, DateTime fechaRegistro, DateTime fechaVencimiento)
        {
            return marcaDao.AddMarcaNacionalRegistrada(expediente, nombre, signoDistintivo, clase, folio, libro, logo, idPersonaTitular, idPersonaAgente, fecha_solicitud, estado, registro, fechaRegistro, fechaVencimiento);
        }

        

    }
}
