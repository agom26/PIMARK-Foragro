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

        public (bool,bool) Login(string user, string pass)
        {
            return userDao.Login(user, pass);
        }

        public int GetTotalUsers()
        {
            return userDao.GetTotalUsuarios();
        }


        public DataTable GetAllUsers(int pageNumber, int pageSize)
        {
            DataTable tabla = new DataTable();
            
            tabla = userDao.GetAllUsers(pageNumber, pageSize);

            return tabla;
        }

        public int GetFilteredUserCount(string value)
        {
            return userDao.GetFilteredUserCount(value);
        }


        public DataTable GetByValue(string value, int pageNumber, int pageSize)
        {
            DataTable tabla = new DataTable();
            tabla = userDao.GetUserByValue(value,pageNumber, pageSize);
            return tabla;
        }
        public DataTable GetById(int id)
        {
            DataTable tabla = new DataTable();
            tabla = userDao.GetUserById(id);
            return tabla;
        }

        public void AddUser(string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo,bool soloLectura)
        {
            userDao.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo, soloLectura);
        }

        public bool UpdateUserSecure(int id, string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo, bool cambiarContrasena, bool soloLectura)
        {
            return userDao.UpdateUser(id, usuario, contrasena, nombres, apellidos, isAdmin, correo, cambiarContrasena, soloLectura);
        }

        public bool RemoveUser(int userId,string deletedUser, string deletedBy )
        {
            return userDao.RemoveUser(userId,deletedUser, deletedBy);
        }

        public int CountAdmins()
        {
            return userDao.ContarAdministradores();
        }

        public bool ProbarConexion()
        {
            return userDao.ProbarConexion();
        }

    }
}
