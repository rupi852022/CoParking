﻿using ParkingProject.Models;
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

        public Tuple<User, int> Get(string email)
        {
            User user = Models.User.readUserMail(email);
            Cars cars = Cars.ReadMainCar(user.Id);
            if (cars is null)
            {
                var UserWithNumberCar = new Tuple<User, int>(user, -1);
                return UserWithNumberCar;
            }
            else
            {
                var UserWithNumberCar = new Tuple<User, int>(user, cars.NumberCar);
                return UserWithNumberCar;
            }
        }

        // GET api/<controller>/5
        public Tuple<User, Cars, Manufacture> Get(string email, string password)
        {

            User user = Models.User.readUser(email, password);
            if (user.Status == "off")
            {
                return new Tuple<User, Cars, Manufacture>(user, null, null);
            }
            else
            {
                Cars cars = Cars.ReadMainCar(user.Id);
                if (cars is null)
                {
                    return new Tuple<User, Cars, Manufacture>(user, null, null);
                }
                else
                {
                    Manufacture manufacture = Manufacture.ReadManufacture(cars.Idcar);
                    return new Tuple<User, Cars, Manufacture>(user, cars, manufacture);
                }

            }
        }

        // POST api/<controller>
        public User Post([FromBody] User U)
        {
            User id = U.Insert();
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