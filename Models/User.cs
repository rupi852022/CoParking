﻿
using ParkingProject.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingProject.Models
{
    public class User
    {

        private int id;
        private string fName;
        private string lName;
        private string email;
        private string password;
        private string gender;
        private string phoneNumber;

        public User() { }


        public User(string fName, string lName, string email, string password, string gender, string phoneNumber)
        {
            this.fName = fName;
            this.lName = lName;
            this.email = email;
            this.password = password;
            this.gender = gender;
            this.phoneNumber = phoneNumber;
        }
        public User(int id, string fName, string lName, string email, string password, string gender, string phoneNumber) : this(fName, lName, email, password, gender, phoneNumber)
        {
            this.id = id;
        }

        public string FName { get => fName; set => fName = value; }
        public string LName { get => lName; set => lName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Gender { get => gender; set => gender = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public int Id { get => id; set => id = value; }

        public int Insert()
        {
            DataServices ds = new DataServices();
            User user = (this);

            int status = ds.InsertUser(user);
            return status;
        }

        public static User readUser(string email, string password)
        {
            DataServices ds = new DataServices();
            User user = ds.ReadUser(email);

            if (user == null || user.password != password)
            {
                return null; // user not exist or password is wrong
            }

            return user;
        }

        public static User[] GetAll()
        {
            DataServices ds = new DataServices();
            return ds.GetAllUsers();
        }
    }
}

