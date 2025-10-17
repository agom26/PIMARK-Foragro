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
        public async Task<int> GetFilteredTitularesCount(string value)
        {
            return await personaDao.GetFilteredTitularesCount(value);
        }
        public async Task<int> GetFilteredAgentesCount(string value)
        {
            return await personaDao.GetFilteredAgentesCount(value);
        }
        public async Task<int> GetFilteredClientesCount(string value)
        {
            return await personaDao.GetFilteredClientesCount(value);
        }
        public async Task<int> GetTotalTitulares()
        {
            return await personaDao.GetTotalTitulares();
        }
        public async Task<int> GetTotalAgentes()
        {
            return await personaDao.GetTotalAgentes();
        }
        public async Task<int> GetTotalClientes()
        {
            return await personaDao.GetTotalClientes();
        }
        public async Task<List<(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto)>> GetPersonaById(int id)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return await personaDao.GetById(id);
        }
        public async Task<DataTable> GetTitularByValue(string value, int pageNumber, int pageSize)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return await personaDao.GetTitularByValue(value,pageNumber, pageSize);
        }
        public async Task<DataTable> GetAgenteByValue(string value, int pageNumber, int pageSize)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return await personaDao.GetAgenteByValue(value, pageNumber, pageSize);
        }
        public async Task<DataTable> GetClienteByValue(string value, int pageNumber, int pageSize)
        {
            // Llama al método correspondiente en personaDao para obtener la persona y devolver el resultado
            return await personaDao.GetClienteByValue(value, pageNumber, pageSize);
        }

        public async Task<DataTable> GetAllAgentes(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await personaDao.GetAllAgentes(currentPageIndex, pageSize);
            return tabla;
        }

        public async Task<DataTable> GetAllTitulares(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await personaDao.GetAllTitulares(currentPageIndex, pageSize);
            return tabla;
        }

        public async Task<DataTable> GetAllClientes(int currentPageIndex, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await personaDao.GetAllClientes(currentPageIndex, pageSize);
            return tabla;
        }

        public async Task<bool> AddPersona(string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto, string tipo)
        {
            return await personaDao.AddPersona(nombre, direccion, nit, pais, correo, telefono, contacto, tipo);
        }

        public async Task<bool> UpdatePersona(int id, string nombre, string direccion, string nit, string pais, string correo, string telefono, string contacto)
        {
            return await personaDao.UpdatePersona(id, nombre, direccion, nit, pais, correo, telefono, contacto);
        }

        public async Task<bool> DeleteTitular(int personaId, string deletedUser, string deletedBy)
        {
            return await personaDao.RemoveTitular(personaId, deletedUser, deletedBy); 
        }

        public async Task<bool> DeleteAgente(int personaId, string deletedUser, string deletedBy)
        {
            return await personaDao.RemoveAgente(personaId, deletedUser, deletedBy);
        }

    }
}
