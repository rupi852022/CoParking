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
        private string manufacturer;
        private bool canEditCar;

        public Cars() { }
        public Cars(int numberCar, int idCar, int year, string model, string color, int size)
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

        public Cars(int id, int numberCar, bool isMain, int idCar, int year, string model, string color, int size, string manufacturer)
        {
            this.id = id;
            this.numberCar = numberCar;
            this.isMain = isMain;
            this.idCar = idCar;
            this.year = year;
            this.model = model;
            this.color = color;
            this.size = size;
            this.manufacturer = manufacturer;
        }

        public Cars(int id, int numberCar, bool isMain, int idCar, int year, string model, string color, int size, string manufacturer, bool canEditCar)
        {
            this.id = id;
            this.numberCar = numberCar;
            this.isMain = isMain;
            this.idCar = idCar;
            this.year = year;
            this.model = model;
            this.color = color;
            this.size = size;
            this.manufacturer = manufacturer;
            this.canEditCar = canEditCar;
        }


        public Cars(int id, int numberCar, bool handicapped, string carPic, int idCar, int year, string model, string color, int size)
        {
            this.id = id;
            this.numberCar = numberCar;
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
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public bool CanEditCar { get => canEditCar; set => canEditCar = value; }


        public static Cars InsertCar(Cars C)
        {
            DataServices ds = new DataServices();
            return ds.InsertCars(C);
        }

        public int DeleteCar(int id)
        {
            DataServices ds = new DataServices();
            int status = ds.deleteCar(this.numberCar, id);
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

        public static Cars readCar(int numberCar,int id)
        {
            DataServices ds = new DataServices();
            Cars cars = ds.ReadUserAndCar(numberCar,id);
            return cars;
        }

        public static Cars ReadMainCar(int id)
        {
            DataServices ds = new DataServices();
            Cars cars = ds.ReadMainCar(id);
            return cars;
        }

        public static Cars[] GetAllUserCars(int id)
        {
            DataServices ds = new DataServices();
            return ds.GetAllUserCars(id);
        }

        public static int MakeMainCar(int numberCar, int id)
        {
            DataServices ds = new DataServices();
            return ds.MakeMainCar(numberCar, id);
        }


    }
}