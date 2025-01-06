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
        public int GetFilteredTitularesCount(string value)
        {
            return personaDao.GetFilteredTitularesCount(value);
        }
        public int GetFilteredAgentesCount(string value)
        {
            return personaDao.GetFilteredAgentesCount(value);
        }
        public int GetFilteredClientesCount(string value)
        {
            return personaDao.GetFilteredClientesCount(value);
        }
        public int GetTotalTitulares()
        {
            return personaDao.GetTotalTitulares();
        }
        public int GetTotalAgentes()
        {
            return personaDao.GetTotalAgentes();
        }
        public int GetTotalClientes()
        {
            return personaDao.GetTotalClientes();
        }
        public List<(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto)> GetPersonaById(int id)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return personaDao.GetById(id);
        }
        public DataTable GetTitularByValue(string value, int pageNumber, int pageSize)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return personaDao.GetTitularByValue(value,pageNumber, pageSize);
        }
        public DataTable GetAgenteByValue(string value, int pageNumber, int pageSize)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return personaDao.GetAgenteByValue(value, pageNumber, pageSize);
        }
        public DataTable GetClienteByValue(string value, int pageNumber, int pageSize)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return personaDao.GetClienteByValue(value, pageNumber, pageSize);
        }

        public DataTable GetAllAgentes(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = personaDao.GetAllAgentes(currentPageIndex, pageSize);
            return tabla;
        }

        public DataTable GetAllTitulares(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = personaDao.GetAllTitulares(currentPageIndex, pageSize);
            return tabla;
        }

        public DataTable GetAllClientes(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = personaDao.GetAllClientes(currentPageIndex, pageSize);
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
