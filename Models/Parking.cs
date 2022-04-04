﻿using ParkingProject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingProject.Models
{
    public class Parking
    {
        private int parkingCode;
        private string location;
        private DateTime exitDate;
        private DateTime exitTime;
        private int typeOfParking;
        private string signType;
        private int userCodeOut;
        private int userCodeIn;

        private static DataServices ds = new DataServices();

        public Parking() { }

        public Parking(string location, DateTime exitDate, DateTime exitTime, int typeOfParking, string signType, int userCodeOut, int userCodeIn)
        {
            this.location = location;
            this.exitDate = exitDate;
            this.exitTime = exitTime;
            this.typeOfParking = typeOfParking;
            this.signType = signType;
            this.userCodeOut = userCodeOut;
            this.userCodeIn = userCodeIn;
        }
        public Parking(string location, DateTime exitDate, DateTime exitTime, int typeOfParking, string signType, int userCodeOut)
        {
            this.location = location;
            this.exitDate = exitDate;
            this.exitTime = exitTime;
            this.typeOfParking = typeOfParking;
            this.signType = signType;
            this.userCodeOut = userCodeOut;
        }
        public Parking(int parkingCode, string location, DateTime exitDate, DateTime exitTime, int typeOfParking, string signType, int userCodeOut, int userCodeIn) : this(location, exitDate, exitTime, typeOfParking, signType, userCodeOut, userCodeIn)
        {
            this.parkingCode = parkingCode;
        }

        public int ParkingCode { get => parkingCode; set => parkingCode = value; }
        public string Location { get => location; set => location = value; }
        public DateTime ExitDate { get => exitDate; set => exitDate = value; }
        public DateTime ExitTime { get => exitTime; set => exitTime = value; }
        public int TypeOfParking { get => typeOfParking; set => typeOfParking = value; }
        public string SignType { get => signType; set => signType = value; }
        public int UserCodeOut { get => userCodeOut; set => userCodeOut = value; }
        public int UserCodeIn { get => userCodeIn; set => userCodeIn = value; }

        public int InsertUserIn(int idUser, int parkingCode)
        {
            int status = ds.TakeParking(idUser, parkingCode);
            return status;
        }
        public int Insert()
        {
            Parking parking = (this);
            int status = ds.InsertParking(parking);
            return status;
        }

        public static Parking[] GetAll(int id)
        {
            return ds.GetAllParkings(id);
        }

        public static int TakeParking(int idUser, int parkingCode)
        {
            int status = ds.TakeParking(idUser, parkingCode);
            return status;
        }
        public static int ReturnParking(int parkingCode)
        {
            int status = ds.ReturnParking(parkingCode);
            return status;
        }
        //public static int park(int parkingCode)
        //{
        //    int status = ds.park(parkingCode);
        //    return status;
        //}

    }
}
