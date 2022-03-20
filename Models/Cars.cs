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

        public int NumberCar { get => numberCar; set => numberCar = value; }
        public int Idcar { get => idCar; set => idCar = value; }
        public int Year { get => year; set => year = value; }
        public string Model { get => model; set => model = value; }
        public string Color { get => color; set => color = value; }
        public int Size { get => size; set => size = value; }

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



    }
}