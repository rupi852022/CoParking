using ParkingProject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingProject.Models
{
    public class Cars
    {

        private int numberCar;
        private int idCar;
        private int year;
        private string model;
        private string color;
        private int size;
        private int id;
        private bool isMain;
        private bool handicapped;
        private string carPic;

        public Cars() { }
        public Cars(int numberCar, int idCar, int year,string model, string color, int size)
        {
            this.numberCar = numberCar;
            this.idCar = idCar;
            this.year = year;
            this.model = model;
            this.color = color;
            this.size = size;
        }

        public Cars(int id, int numberCar, bool isMain, bool handicapped, string carPic)
        {
            this.id = id;
            this.numberCar = numberCar;
            this.isMain = isMain;
            this.handicapped = handicapped;
            this.carPic = carPic;
        }

        public Cars(int id, int numberCar, bool isMain, bool handicapped, string carPic, int idCar, int year, string model, string color, int size)
        {
            this.id = id;
            this.numberCar = numberCar;
            this.isMain = isMain;
            this.handicapped = handicapped;
            this.carPic = carPic;
            this.idCar = idCar;
            this.year = year;
            this.model = model;
            this.color = color;
            this.size = size;
        }

        public int NumberCar { get => numberCar; set => numberCar = value; }
        public int Idcar { get => idCar; set => idCar = value; }
        public int Year { get => year; set => year = value; }
        public string Model { get => model; set => model = value; }
        public string Color { get => color; set => color = value; }
        public int Size { get => size; set => size = value; }
        public int Id { get => id; set => id = value; }
        public bool IsMain { get => isMain; set => isMain = value; }
        public bool Handicapped { get => handicapped; set => handicapped = value; }
        public string CarPic { get => carPic; set => carPic = value; }

        public static int InsertCar(Cars C)
        {
            DataServices ds = new DataServices();
            int status = ds.InsertCars(C);
            return status;
        }

        public int DeleteCar()
        {
            DataServices ds = new DataServices();
            int status =  ds.deleteCar(this.numberCar);
            return status;
        }

        public static int UpdateCar(Cars cars)
        {
            DataServices ds = new DataServices();

            if (cars == null)
            {
                return -1; // Cars not exist
            }

            int status = ds.UpdateCars(cars);
            return status;

        }

        public static Cars readCar(int numberCar)
        {
            DataServices ds = new DataServices();
            Cars cars = ds.ReadUser(numberCar);
            return cars;
        }

        public static Cars ReadMainCar(int id)
        {
            DataServices ds = new DataServices();
            Cars cars = ds.ReadMainCar(id);
            return cars;
        }





    }
}