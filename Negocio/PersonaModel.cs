using AccesoDatos.Usuarios;
using AccesoDatos;
using System.Data;
using System.Security.Policy;
using Comun.Cache;
using AccesoDatos.Entidades;
namespace Dominio
{
    public class PersonaModel:ConnectionSQL
    {
        private PersonaDao personaDao;


        public PersonaModel()
        {
            personaDao = new PersonaDao();
        }

        public List<(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto)> GetPersonaById(int id)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return personaDao.GetById(id);
        }
        public DataTable GetTitularByValue(string value)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return personaDao.GetTitularByValue(value);
        }
        public DataTable GetAgenteByValue(string value)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return personaDao.GetAgenteByValue(value);
        }
        public DataTable GetClienteByValue(string value)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return personaDao.GetClienteByValue(value);
        }

        public DataTable GetAllAgentes()
        {
            DataTable tabla = new DataTable();
            tabla = personaDao.GetAllAgentes();
            return tabla;
        }

        public DataTable GetAllTitulares()
        {
            DataTable tabla = new DataTable();
            tabla = personaDao.GetAllTitulares();
            return tabla;
        }

        public DataTable GetAllClientes()
        {
            DataTable tabla = new DataTable();
            tabla = personaDao.GetAllClientes();
            return tabla;
        }

        public bool AddPersona(string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto, string tipo)
        {
            return personaDao.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo);
        }

        public bool UpdatePersona(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto)
        {
            return personaDao.UpdatePersona(id, nombre, direccion, nit, pais, correo, telefono, contacto);
        }

        public bool DeleteTitular(int personaId, string deletedUser, string deletedBy)
        {
            return personaDao.RemoveTitular(personaId, deletedUser, deletedBy); 
        }

        public bool DeleteAgente(int personaId, string deletedUser, string deletedBy)
        {
            return personaDao.RemoveAgente(personaId, deletedUser, deletedBy);
        }

    }
}
