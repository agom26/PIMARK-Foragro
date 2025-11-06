using AccesoDatos.Usuarios;
using AccesoDatos;
using System.Data;
using System.Security.Policy;
using Comun.Cache;
namespace Dominio
{
    public class UserModel:ConnectionSQL
    {
        private UserDao userDao;


        public UserModel()
        {
            userDao = new UserDao();
        }

        public async Task<(bool,bool)> Login(string user, string pass)
        {
            return await userDao.Login(user, pass);
        }

        public async Task<int> GetTotalUsers()
        {
            return await userDao.GetTotalUsuarios();
        }


        public async Task<DataTable> GetAllUsers(int pageNumber, int pageSize)
        {
            DataTable tabla = new DataTable();
            
            tabla = await userDao.GetAllUsers(pageNumber, pageSize);

            return tabla;
        }

        public async Task<int> GetFilteredUserCount(string value)
        {
            return await userDao.GetFilteredUserCount(value);
        }


        public async Task<DataTable> GetByValue(string value, int pageNumber, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = await userDao.GetUserByValue(value,pageNumber, pageSize);
            return tabla;
        }
        public async Task<DataTable> GetById(int id)
        {
            DataTable tabla = new DataTable();
            tabla = await userDao.GetUserById(id);
            return tabla;
        }

        public async Task AddUser(string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo,bool soloLectura)
        {
            await userDao.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo, soloLectura);
        }

        public async Task<bool> UpdateUserSecure(int id, string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo, bool cambiarContrasena, bool soloLectura)
        {
            return await userDao.UpdateUser(id, usuario, contrasena, nombres, apellidos, isAdmin, correo, cambiarContrasena, soloLectura);
        }

        public async Task<bool> RemoveUser(int userId,string deletedUser, string deletedBy )
        {
            return await userDao.RemoveUser(userId, deletedUser, deletedBy);
        }

        public async Task<int> CountAdmins()
        {
            return await userDao.ContarAdministradores();
        }

        public async Task<bool> ProbarConexion()
        {
            return await userDao.ProbarConexion();
        }

    }
}
