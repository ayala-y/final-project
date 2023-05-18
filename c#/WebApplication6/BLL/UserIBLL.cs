
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace BLL
{
    public interface UserIBLL
    {
        User GetUserById(int Id);
        User GetUserByName(string name);
        User GetUserByEmail(string email);
        User GetUserByEmailAndPassword(string email,string password);
        User AddUser(User u);
        User UpdateUser(int Id, User u);
        void DeleteUser(int Id);
    }
}
