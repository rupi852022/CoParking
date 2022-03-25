﻿using ParkingProject.Models;
using ParkingProject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingProject.Controllers
{
    public class ParkingsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int idUser, int parkingCode)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody] Parking P)
        {
            int id = P.Insert();
            return id;
        }

        // PUT api/<controller>/5
        public int Put(int idUser, int parkingCode)
        {
            DataServices ds = new DataServices();
            int status = ds.TakeParking(idUser, parkingCode);
            return status;
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}