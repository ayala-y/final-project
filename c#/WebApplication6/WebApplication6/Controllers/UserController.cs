using BLL;
using DAL;
using Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        UserIBLL userIBLL;
        public UserController(UserIBLL userIBLL)
        {
            this.userIBLL = userIBLL;
        }

        // GET: api/<UserController>/5
        [HttpGet("id")]
        public User GetUserById(int id)
        {
            return this.userIBLL.GetUserById(id);
        }

        [HttpGet("name")]
        public User GetUserByName(string name)
        {
            return this.userIBLL.GetUserByName(name);
        }

        [HttpGet("email")]
        public User GetUserByEmail(string email)
        {
            return this.userIBLL.GetUserByEmail(email);
        }

        //[HttpGet("{id}/{first}/{second}")]
        [HttpGet("{email}/{password}")]
        public User GetUserByEmailAndPassword(string email,string password)
        {
            return this.userIBLL.GetUserByEmailAndPassword(email,password);
        }


        // POST api/<UserController>
        [HttpPost]
        public User Post(User u)
        {
          return this.userIBLL.AddUser(u);
        }

        // PUT api/<UserController>/5
        [HttpPut("id")]
        public User Put(int id,User u)
        {
             return userIBLL.UpdateUser(id, u);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("id")]
        public void Delete(int id)
        {
            userIBLL.DeleteUser(id);
        }
    }
}
