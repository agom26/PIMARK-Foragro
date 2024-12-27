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

        public DataTable GetAllUsers()
        {
            DataTable tabla=new DataTable();
            tabla=userDao.GetAllUsers();
            return tabla;
        }
        public DataTable GetByValue(string value)
        {
            DataTable tabla = new DataTable();
            tabla = userDao.GetUserByValue(value);
            return tabla;
        }
        public DataTable GetById(int id)
        {
            DataTable tabla = new DataTable();
            tabla = userDao.GetUserById(id);
            return tabla;
        }

        public void AddUser(string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo)
        {
            userDao.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo);
        }

        public void UpdateUser(int id, string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo)
        {
            userDao.UpdateUser(id, usuario, contrasena, nombres, apellidos, isAdmin, correo);
        }

        public bool RemoveUser(int userId,string deletedUser, string deletedBy )
        {
            return userDao.RemoveUser(userId,deletedUser, deletedBy);
        }

        public int CountAdmins()
        {
            return userDao.ContarAdministradores();
        }

    }
}
