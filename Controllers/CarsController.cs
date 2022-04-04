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
        public Cars Get(int numberCar, int id)
        {
            return ParkingProject.Models.Cars.readCar(numberCar,id);
        }

        public Cars[] Get(int id)
        {
            return Cars.GetAllUserCars(id);
        }


        // POST api/<controller>/
        public Cars Post([FromBody] Cars C)
        {
            return Cars.InsertCar(C);
        }

        // PUT api/<controller>
        public int Put([FromBody] Cars C)
        {
            int id = Cars.UpdateCar(C);
            return id;
        }
        public Cars Put(int numberCar, int id)
        {
            return Cars.MakeMainCar(numberCar,id);
        }



        // DELETE api/<controller>/5
        public int Delete(int numberCar,int id)
        {
            int satatus = Cars.DeleteCar(numberCar, id);
            return satatus;
        }
    }
}