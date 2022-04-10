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
        private double locationLng;
        private double locationLat;
        private string locationName;
        private DateTime exitDate;
        private int typeOfParking;
        private string signType;
        private int userCodeOut;
        private string numberCarOut;
        private int userCodeIn;
        private string numberCarIn;

        private static DataServices ds = new DataServices();

        public Parking() { }

        public Parking(double locationLng, double locationLat, string locationName, DateTime exitDate, int typeOfParking, string signType, int userCodeOut, string numberCarOut, int userCodeIn, string numberCarIn)
        {
            this.locationLat = locationLat;
            this.locationLng = locationLng;
            this.locationName = locationName;
            this.exitDate = exitDate;
            this.typeOfParking = typeOfParking;
            this.signType = signType;
            this.userCodeOut = userCodeOut;
            this.userCodeIn = userCodeIn;
            this.numberCarIn = numberCarIn;
            this.numberCarOut = numberCarOut;
        }

        public Parking(double locationLng, double locationLat, string locationName, DateTime exitDate, int typeOfParking, string signType, int userCodeOut, string numberCarOut)
        {
            this.locationLat = locationLat;
            this.locationLng = locationLng;
            this.locationName = locationName;
            this.exitDate = exitDate;
            this.typeOfParking = typeOfParking;
            this.signType = signType;
            this.userCodeOut = userCodeOut;
            this.numberCarOut = numberCarOut;
        }
        public Parking(int parkingCode, double locationLng, double locationLat, string locationName, DateTime exitDate, int typeOfParking, string signType, int userCodeOut, string numberCarOut, int userCodeIn, string numberCarIn)
        {
            this.parkingCode = parkingCode;
            this.locationLat = locationLat;
            this.locationLng = locationLng;
            this.locationName = locationName;
            this.exitDate = exitDate;
            this.typeOfParking = typeOfParking;
            this.signType = signType;
            this.userCodeOut = userCodeOut;
            this.numberCarOut = numberCarOut;
            this.userCodeIn = userCodeIn;
            this.numberCarIn = numberCarIn;

        }

        public Parking(int parkingCode, double locationLng, double locationLat, string locationName, DateTime exitDate, int typeOfParking, string signType, int userCodeOut, string numberCarOut)
        {
            this.parkingCode = parkingCode;
            this.locationLat = locationLat;
            this.locationLng = locationLng;
            this.locationName = locationName;
            this.exitDate = exitDate;
            this.typeOfParking = typeOfParking;
            this.signType = signType;
            this.userCodeOut = userCodeOut;
            this.numberCarOut = numberCarOut;

        }

        public int ParkingCode { get => parkingCode; set => parkingCode = value; }
        public double LocationLat { get => locationLat; set => locationLat = value; }
        public double LocationLng { get => locationLng; set => locationLng = value; }
        public string LocationName { get => locationName; set => locationName = value; }
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

        public static Parking GetParking(int parkingCode)
        {
            return ds.GetParking(parkingCode);
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
