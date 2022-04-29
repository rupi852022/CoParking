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
        public Parking[] Get(int id)
        {
            return ParkingProject.Models.Parking.GetAll(id);
        }
        public Parking[] Get(int id, int temp1, int temp2)
        {
            return ParkingProject.Models.Parking.GetAllParkingsUser(id);
        }

        public Parking Get(int parkingCode, string x = "x")
        {
            return ParkingProject.Models.Parking.GetParking(parkingCode);
        }

        public Tuple<Parking, Cars, User> Get(int parkingCode, int tmp)
        {

            Parking p = ParkingProject.Models.Parking.GetParking(parkingCode);
            Cars c = ParkingProject.Models.Cars.readCar(p.NumberCarOut, p.UserCodeOut);
            User u = ParkingProject.Models.User.readUserId(p.UserCodeIn);

            return new Tuple<Parking, Cars, User>(p,c,u);
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
            int status = Parking.TakeParking(idUser, parkingCode);
            return status;
        }
        public int Put([FromBody] Parking P)
        {
            int id = P.Insert();
            return id;
        }

        public int Delete(int parkingCode)
        {
            int status = Parking.ReturnParking(parkingCode);
            return status;
        }
    }
}