using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class UserBLL : UserIBLL
    {
        UserIDAL userIDAL;

        public UserBLL(UserIDAL userIDAL)
        {
            this.userIDAL = userIDAL;
        }

        public User GetUserById(int Id)
        {
            return userIDAL.GetUserById(Id);
        }
        public User GetUserByName(string name)
        {
            return userIDAL.GetUserByName(name);
        }
        public User GetUserByEmail(string email)
        {
            return userIDAL.GetUserByEmail(email);
        }
        public User GetUserByEmailAndPassword(string email,string password)
        {
            return userIDAL.GetUserByEmailAndPassword(email,password);
        }

        public User AddUser(User u)
        {
            return userIDAL.AddUser(u);
        }

        public User UpdateUser(int Id, User u)
        {
           return userIDAL.UpdateUser(Id, u);
        }
        public void DeleteUser(int Id)
        {
            userIDAL.DeleteUser(Id);
        }
    }
}
