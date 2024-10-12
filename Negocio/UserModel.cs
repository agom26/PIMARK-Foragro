using AccesoDatos;

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
    }
}
