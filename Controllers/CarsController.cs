﻿using ParkingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingProject.Controllers
{
    public class CarsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public Cars Get(int numberCar)
        {
            return ParkingProject.Models.Cars.readCar(numberCar);
        }

        // POST api/<controller>/
        public int Post([FromBody] Cars C)
        {
            int id = Cars.InsertCar(C);
            return id;
        }

        // PUT api/<controller>
        public int Put([FromBody] Cars C)
        {
            int id = Cars.UpdateCar(C);
            return id;
        }


        // DELETE api/<controller>/5
        public int Delete(Cars C)
        {
            int satatus = C.DeleteCar();
            return satatus;
        }
    }
}