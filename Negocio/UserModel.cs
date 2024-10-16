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

        public void AddUser(string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo)
        {
            userDao.AddUser(usuario, contrasena, nombres, apellidos, isAdmin, correo);
        }

        public void UpdateUser(int id, string usuario, string contrasena, string nombres, string apellidos, bool isAdmin, string correo)
        {
            userDao.UpdateUser(id, usuario, contrasena, nombres, apellidos, isAdmin, correo);
        }

        public List<(int id,string usuario, string nombres, string apellidos, string correo, string contrasena, bool isAdmin)> GetByValue(string value)
        {
            return userDao.GetByValue(value);
        }
    }
}
