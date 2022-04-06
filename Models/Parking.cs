using ParkingProject.Models.DAL;
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
        private int typeOfParking;
        private string signType;
        private int userCodeOut;
        private string numberCarOut;
        private int userCodeIn;
        private string numberCarIn;

        private static DataServices ds = new DataServices();

        public Parking() { }

        public Parking(string location, DateTime exitDate, int typeOfParking, string signType, int userCodeOut,string numberCarOut, int userCodeIn, string numberCarIn)
        {
            this.location = location;
            this.exitDate = exitDate;
            this.typeOfParking = typeOfParking;
            this.signType = signType;
            this.userCodeOut = userCodeOut;
            this.userCodeIn = userCodeIn;
            this.numberCarIn = numberCarIn;
            this.numberCarOut = numberCarOut;
        }
        public Parking(string location, DateTime exitDate, int typeOfParking, string signType, int userCodeOut, string numberCarOut)
        {
            this.location = location;
            this.exitDate = exitDate;
            this.typeOfParking = typeOfParking;
            this.signType = signType;
            this.userCodeOut = userCodeOut;
            this.numberCarOut = numberCarOut;
        }
        public Parking(int parkingCode, string location, DateTime exitDate, int typeOfParking, string signType, int userCodeOut, string numberCarOut, int userCodeIn, string numberCarIn) : this(location, exitDate, typeOfParking, signType, userCodeOut,numberCarOut, userCodeIn,numberCarIn)
        {
            this.parkingCode = parkingCode;
        }

        public int ParkingCode { get => parkingCode; set => parkingCode = value; }
        public string Location { get => location; set => location = value; }
        public DateTime ExitDate { get => exitDate; set => exitDate = value; }
        public int TypeOfParking { get => typeOfParking; set => typeOfParking = value; }
        public string SignType { get => signType; set => signType = value; }
        public int UserCodeOut { get => userCodeOut; set => userCodeOut = value; }
        public int UserCodeIn { get => userCodeIn; set => userCodeIn = value; }
        public string NumberCarOut { get => numberCarOut; set => numberCarOut = value; }
        public string NumberCarIn { get => numberCarIn; set => numberCarIn = value; }

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
