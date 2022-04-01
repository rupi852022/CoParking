using ParkingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingProject.Controllers
{
    public class PasswordController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<controller>
        public string Post(int id)
        {
            return "value";
        }

        // GET api/<controller>/5
        public bool Get(string email)
        {
           return Password.SendinEmail(email);
        }

        // PUT api/<controller>/5
        public int PUT(string email, string currentPassword, string password1, string password2)
        {
            return Password.UpdatePassword(email, currentPassword, password1, password2);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}