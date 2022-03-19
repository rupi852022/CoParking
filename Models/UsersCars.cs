using ParkingProject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingProject.Models
{
    public class UsersCars
    {
        private int id;
        private int numberCar;
        private bool isMain;
        private bool handicapped;
        private string carPic;

        public UsersCars() { }
        public UsersCars(int id, int numberCar, bool isMain, bool handicapped, string carPic)
        {
            this.id = id;
            this.numberCar = numberCar;
            this.isMain = isMain;
            this.handicapped = handicapped;
            this.carPic = carPic;
        }
        public int Id { get => id; set => id = value; }
        public int NumberCar { get => numberCar; set => numberCar = value; }
        public bool IsMain { get => isMain; set => isMain = value; }
        public bool Handicapped { get => handicapped; set => handicapped = value; }
        public string CarPic { get => carPic; set => carPic = value; }

        public int Insert()
        {
            DataServices ds = new DataServices();
            UsersCars usersCars = (this);

            int status = ds.InsertUserCar(usersCars);
            return status;
        }
    }
}