using ParkingProject.Models;
using SendMailViaGmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingProject.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public User Get(string email, string password)
        {
            return ParkingProject.Models.User.readUser(email, password);
        }

        // POST api/<controller>
        public int Post([FromBody] User U)
        {
            int id = U.Insert();
            return id;
        }

        // PUT api/<controller>/5
        public string Put()
        {
            return "hello";
        }

        // DELETE api/<controller>/5
        public User[] Delete()
        {
            return ParkingProject.Models.User.GetAll();
        }
    }
}