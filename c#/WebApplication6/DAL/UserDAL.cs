using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL : UserIDAL
    {
        public SuitYouDbContext db = new SuitYouDbContext();

        public User AddUser(User u)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if( Regex.IsMatch(u.Email, pattern))
            {
                try
                {
                    db.Users.Add(u);
                    db.SaveChanges();
                    return GetUserById(u.Id);
                }
                catch
                {
                    User user= new User();
                    user.Id = 0; ;
                    return user;
                }

            }
            return null;

        }
        public void DeleteUser(int Id)
        {
            User u = db.Users.FirstOrDefault(x => x.Id == Id);
            db.Users.Remove(u);
            db.SaveChanges();
        }


        public User GetUserById(int Id)
        {
               return db.Users.FirstOrDefault(x => x.Id == Id);
        }
        public User GetUserByName(string name)
        {
            return db.Users.FirstOrDefault(x => (x.Name) == name);
        }
        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(x => (x.Email) == email);
        }
        public User GetUserByEmailAndPassword(string email,string password)
        {
            return db.Users.FirstOrDefault(x => (x.Email) == email && (x.Password) == password);
        }
        public User UpdateUser(int Id, User u)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                user.Name = u.Name;
                user.Password = u.Password;
                user.Email = u.Email;
                db.SaveChanges();
            }
            return user;
        }
    }
}
