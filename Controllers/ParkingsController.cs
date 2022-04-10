using ParkingProject.Models;
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

        public Parking Get(int parkingCode, string x = "x")
        {
            return ParkingProject.Models.Parking.GetParking(parkingCode);
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
        public int Delete(int parkingCode)
        {
            int status = Parking.ReturnParking(parkingCode);
            return status;
        }
    }
}