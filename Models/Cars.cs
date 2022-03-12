using Class_demo.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingProject.Models
{
    public class Cars
    {

        private int numberCar;
        private string manufacturer;
        private string model;
        private string year;
        private string color;
        private string size;
        private bool handicapped;
        private string carPicture;

        public Cars() { }
        public Cars(int numberCar, string manufacturer, string model, string year, string color, string size, bool handicapped, string carPicture)
        {
            this.numberCar = numberCar;
            this.manufacturer = manufacturer;
            this.model = model;
            this.year = year;
            this.color = color;
            this.size = size;
            this.Handicapped = handicapped;
            this.carPicture = carPicture;
        }

        public int NumberCar { get => numberCar; set => numberCar = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public string Model { get => model; set => model = value; }
        public string Year { get => year; set => year = value; }
        public string Color { get => color; set => color = value; }
        public string Size { get => size; set => size = value; }
        public bool Handicapped { get => handicapped; set => handicapped = value; }
        public string CarPicture { get => carPicture; set => carPicture = value; }

        public int InsertCar()
        {
            DataServices ds = new DataServices();
            int status = ds.InsertCars(this);
            return status;
        }

        public int DeleteCar()
        {
            DataServices ds = new DataServices();
            int status =  ds.deleteCar(this.numberCar);
            return status;
        }

        public int UpdateCar()
        {
            DataServices ds = new DataServices();
            Cars cars = (this);

            if (cars == null)
            {
                return -1; // Cars not exist
            }

            int status = ds.UpdateCars(cars);
            return status;

        }



    }
}