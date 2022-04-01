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
        private char gender;
        private string phoneNumber;
        private string image;
        private int searchRadius;
        private int timeDelta;
        private string status;
        private int tokens;


        public User() { }

        public User(string fName, string lName, string email, string password, char gender, string phoneNumber, string image)
        {
            this.fName = fName;
            this.lName = lName;
            this.email = email;
            this.password = password;
            this.gender = gender;
            this.phoneNumber = phoneNumber;
            this.image = image;
        }

        public User(string fName, string lName, string email, string password, char gender, string phoneNumber)
        {
            this.fName = fName;
            this.lName = lName;
            this.email = email;
            this.password = password;
            this.gender = gender;
            this.phoneNumber = phoneNumber;
        }

        public User(int id, string fName, string lName, string email, string password, char gender, string phoneNumber, string image, int searchRadius, int timeDelta, string status, int tokens) : this(fName, lName, email, password, gender, phoneNumber, image)
        {
            this.id = id;
            this.searchRadius = searchRadius;
            this.timeDelta = timeDelta;
            this.status = status;
            this.tokens = tokens;
        }

        public string FName { get => fName; set => fName = value; }
        public string LName { get => lName; set => lName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public char Gender { get => gender; set => gender = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Image { get => image; set => image = value; }
        public int Id { get => id; set => id = value; }
        public int SearchRadius { get => searchRadius; set => searchRadius = value; }
        public int TimeDelta { get => timeDelta; set => timeDelta = value; }
        public string Status { get => status; set => status = value; }
        public int Tokens { get => tokens; set => tokens = value; }


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
            User user = ds.ReadUser(email,2);

            if (user == null || user.password != password)
            {
                Exception ex = new Exception("The password or mail uncorrect.");
                throw ex;
            }

            return user;
        }

        public static User[] GetAll()
        {
            DataServices ds = new DataServices();
            return ds.GetAllUsers();
        }
        public static User readUserMail(string email)
        {
            DataServices ds = new DataServices();
            User user = ds.ReadUser(email,2);
            return user;
        }
    }
}

