using ParkingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingProject.Controllers
{
    public class MailController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post(string mail)
        {
            Mail.SendEmail(mail);
        }

        // PUT api/<controller>/5
        public int PUT(string mail, string currentPassword, string password1, string password2)
        {
            return Mail.UpdatePassword(mail, currentPassword, password1, password2);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}