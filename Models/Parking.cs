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
        private DateTime exitTime;
        private string typeOfParking;
        private string singType;
        private int userCodeOut;
        private int userCodeIn;

        private static DataServices ds = new DataServices();

        public Parking() { }

        public Parking(string location, DateTime exitDate, DateTime exitTime, string typeOfParking, string singType, int userCodeOut, int userCodeIn)
        {
            this.location = location;
            this.exitDate = exitDate;
            this.exitTime = exitTime;
            this.typeOfParking = typeOfParking;
            this.singType = singType;
            this.userCodeOut = userCodeOut;
            this.userCodeIn = userCodeIn;
        }
        public Parking(string location, DateTime exitDate, DateTime exitTime, string typeOfParking, string singType, int userCodeOut)
        {
            this.location = location;
            this.exitDate = exitDate;
            this.exitTime = exitTime;
            this.typeOfParking = typeOfParking;
            this.singType = singType;
            this.userCodeOut = userCodeOut;
        }
        public Parking(int parkingCode, string location, DateTime exitDate, DateTime exitTime, string typeOfParking, string singType, int userCodeOut, int userCodeIn) : this(location, exitDate, exitTime, typeOfParking, singType, userCodeOut, userCodeIn)
        {
            this.parkingCode = parkingCode;
        }

        public int ParkingCode { get => parkingCode; set => parkingCode = value; }
        public string Location { get => location; set => location = value; }
        public DateTime ExitDate { get => exitDate; set => exitDate = value; }
        public DateTime ExitTime { get => exitTime; set => exitTime = value; }
        public string TypeOfParking { get => typeOfParking; set => typeOfParking = value; }
        public string SingType { get => singType; set => singType = value; }
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

        public static Parking[] GetAll()
        {
            return ds.GetAllParkings();
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
