using ParkingProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace ParkingProject.Models.DAL
{
    public class DataServices
    {
        public string ErrorMessage = "";

        protected SqlConnection Connect(string connectionStringName)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();

            return con;
        }

        public User[] GetAllUsers()
        {
            SqlConnection con = this.Connect("webOsDB");
            SqlCommand command = new SqlCommand("SELECT * FROM [CoParkingUsers_2022]", con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;

            SqlDataReader dr = command.ExecuteReader();
            List<User> users = new List<User>();
            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["id"]);
                string currentEmail = (string)dr["email"];
                string password = (string)dr["password"];
                string fName = (string)dr["lName"];
                string lName = (string)dr["fName"];
                string phoneNumber = (string)dr["phoneNumber"];
                char gender = Convert.ToChar(dr["gender"]);
                string image = (string)dr["image"];
                int searchRadius = Convert.ToInt32(dr["searchRadius"]);
                int timeDelta = Convert.ToInt32(dr["timeDelta"]);
                string status = (string)dr["status"];
                int tokens = Convert.ToInt32(dr["tokens"]);



                User user = new User(id, fName, lName, currentEmail, password, gender, phoneNumber, image, searchRadius, timeDelta, status, tokens);
                users.Add(user);
            }

            return users.ToArray();
        }

        public Cars[] GetAllUserCars(int id)
        {
            SqlConnection con = this.Connect("webOsDB");
            SqlCommand command = new SqlCommand("SELECT * FROM[CoParkingUsersCars_2022] LEFT JOIN[CoParkingCars_2022] ON CoParkingUsersCars_2022.numberCar = [CoParkingCars_2022].numberCar LEFT JOIN[CoParkingManufacture_2022] ON[CoParkingCars_2022].idCar = [CoParkingManufacture_2022].idCar WHERE[CoParkingUsersCars_2022].id ="
                + id, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;

            SqlDataReader dr = command.ExecuteReader();
            List<Cars> carsList = new List<Cars>();
            while (dr.Read())
            {
                //string currentEmail = (string)dr["email"];
                //string password = (string)dr["password"];
                //string fName = (string)dr["lName"];
                //string lName = (string)dr["fName"];
                //string phoneNumber = (string)dr["phoneNumber"];
                //char gender = Convert.ToChar(dr["gender"]);
                //string image = (string)dr["image"];
                //int searchRadius = Convert.ToInt32(dr["searchRadius"]);
                //int timeDelta = Convert.ToInt32(dr["timeDelta"]);
                //string status = (string)dr["status"];
                //int tokens = Convert.ToInt32(dr["tokens"]);
                string numberCar = (string)(dr["numberCar"]);
                int idCar = Convert.ToInt32(dr["idCar"]);
                int year = Convert.ToInt32(dr["year"]);
                string model = (string)dr["model"];
                string color = (string)dr["color"];
                int size = Convert.ToInt32(dr["size"]);
                string manufacture = (string)dr["manufacturer"];

                bool currentIsmain = true;
                string isMain = (string)dr["isMain"];
                if (isMain == "F") { currentIsmain = false; }

                Cars cars = new Cars(id, numberCar, currentIsmain, idCar, year, model, color, size, manufacture);


                carsList.Add(cars);
            }

            return carsList.ToArray();
        }


        public User InsertUser(User U)
        {
            SqlConnection con = null;
            try
            {
                User user = this.ReadUserEmail(U.Email, 1);

                if (U.Gender == 0)
                {
                    ErrorMessage = "the Gender need to be with one char";
                    Exception ex = new Exception(ErrorMessage);
                    throw ex;
                }

                if (user != null)
                {
                    ErrorMessage = "The email or password alredy exist";
                    Exception ex = new Exception(ErrorMessage);
                    throw ex;
                }
                else
                {
                    if (ValidateEmail(U.Email) is false)
                    {
                        Exception ex = new Exception(ErrorMessage);
                        throw ex;
                    }
                    if (ValidatePassword(U.Password) is false)
                    {
                        Exception ex = new Exception(ErrorMessage);
                        throw ex;
                    }
                }
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                SqlCommand command = CreateInsertUser(U, con);

                // E - Execute
                int affected = command.ExecuteNonQuery();
                return U;

            }

            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public Parking[] GetAllParkings(int id)
        {

            SqlConnection con = this.Connect("webOsDB");
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            SqlCommand command = new SqlCommand(
                "select [parkingCode],[location], CONVERT(varchar(10), [exitDate], 111) as [exitDate], CONVERT(varchar(10), [exitTime], 20) as [exitTime],[typeOfParking],[signType],[userCodeOut],[userCodeIn]  from [CoParkingParkings_2022] where [exitDate] >= '" + currentDate + "' AND [userCodeOut] != '"+id+"';"
                , con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;

            SqlDataReader dr = command.ExecuteReader();
            List<Parking> parkings = new List<Parking>();
            while (dr.Read())
            {
                string test = (string)dr["exitDate"];
                int parkingCode = Convert.ToInt32(dr["parkingCode"]);
                string location = (string)dr["location"];
                DateTime exitDate = DateTime.Parse((string)dr["exitDate"]).Date;
                var date = exitDate.Date;
                DateTime exitTime = DateTime.Parse((string)dr["exitTime"]);
                int typeOfParking = Convert.ToInt32(dr["typeOfParking"]);
                string signType = (string)dr["signType"];
                int userCodeOut = Convert.ToInt32(dr["userCodeOut"]);
                int userCodeIn = Convert.ToInt32(dr["userCodeIn"]);


                Parking parking = new Parking(parkingCode, location, exitDate, exitTime, typeOfParking, signType, userCodeOut, userCodeIn);
                parkings.Add(parking);
            }

            return parkings.ToArray();
        }

        public Manufacture[] GetAllManufacturer()
        {
            SqlConnection con = this.Connect("webOsDB");
            SqlCommand command = new SqlCommand("SELECT * FROM [CoParkingManufacture_2022]", con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;

            SqlDataReader dr = command.ExecuteReader();
            List<Manufacture> manufactures = new List<Manufacture>();
            while (dr.Read())
            {
                int idCar = Convert.ToInt32(dr["idCar"]);
                string manufacturer1 = (string)dr["manufacturer"];
                Manufacture manufacturer = new Manufacture(idCar, manufacturer1);
                manufactures.Add(manufacturer);
            }

            return manufactures.ToArray();
        }

        //public int InsertUserCar(UsersCars U)
        //{
        //    SqlConnection con = null;
        //    try
        //    {
        //        // C - Connect
        //        con = Connect("webOsDB");

        //        // C - Create Command
        //        //SqlCommand command = CreateInsertUserCar(U, con);

        //        //// E - Execute
        //        //int affected = command.ExecuteNonQuery();

        //        //return affected;

        //    }
        //    catch (Exception ex)
        //    {
        //        // write to log file
        //        throw new Exception(ErrorMessage, ex);
        //    }
        //    finally
        //    {
        //        // Close Connection
        //        con.Close();
        //    }
        //}

        public int InsertParking(Parking P)
        {
            checkMinutes(P);
            SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                SqlCommand command = CreateInsertParking(P, con);

                // E - Execute
                int affected = command.ExecuteNonQuery();

                return affected;

            }
            catch (Exception ex)
            {
                // write to log file
                throw new Exception(ErrorMessage, ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public int checkMinutes(Parking P)
        {
            string ParkingDate = P.ExitDate.ToString("yyyy-MM-dd");
            string ParkingTime = P.ExitTime.ToString("HH:mm:ss");
            string time = ParkingDate + " " + ParkingTime + ",531";
            DateTime myDate = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);

            int totalMinute = (int)(myDate - (DateTime.Now)).TotalMinutes;
            Console.WriteLine(totalMinute);

            if (totalMinute <= 30)
            {
                return 1;
            }
            if (totalMinute <= 330)
            {
                return 2;
            }

            else { return 3; }
        }

        SqlCommand CreateInsertParking(Parking P, SqlConnection con)
        {
            string insertStr = "";
            string currentexitDate = P.ExitDate.ToString("dd/MM/yyyy");
            string currentexitHour = P.ExitTime.ToString("HH:mm:ss");
            if (P.UserCodeIn == 0)
            {
                insertStr += " INSERT INTO [CoParkingParkings_2022] ([location], [exitDate], [exitTime], [typeOfParking], [signType], [userCodeOut]) VALUES('" + P.Location + "', '" + currentexitDate + "', '" + currentexitHour + "', '" + P.TypeOfParking + "', '" + P.SignType + "', '" + P.UserCodeOut + "')";
            }
            else
            {
                insertStr += " INSERT INTO [CoParkingParkings_2022] ([location], [exitDate], [exitTime], [typeOfParking], [signType], [userCodeOut], [userCodeIn]) VALUES('" + P.Location + "', '" + currentexitDate + "', '" + currentexitHour + "', '" + P.TypeOfParking + "', '" + P.SignType + "', '" + P.UserCodeOut + "', '" + P.UserCodeIn + "')";
            }
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }

        SqlCommand CreateInsertUserCar(Cars U,string currentMain,int carExist, SqlConnection con)
        {

            if (CheckUserAndNumberCar(U.Id, U.NumberCar) == 1)
            {
                Exception ex = new Exception("the User and the NumberCar are exist.");
                throw ex;
            }
            string insertStr = "";
            string Handicapped = "T";


            SqlCommand command = new SqlCommand("SELECT * FROM CoParkingUsersCars_2022 WHERE id=" + U.Id, con);
            command.Parameters.AddWithValue("@id", U.Id);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;



            //if (U.IsMain is false) { currentMain = "F"; }
            if (U.Handicapped is false) { Handicapped = "F"; }
            //if (U.IsMain is true)
            //{
            //    currentMain = "T";
            //    insertStr += " UPDATE [CoParkingUsersCars_2022] SET [isMain] = 'F'";
            //}
            insertStr += " UPDATE [CoParkingUsers_2022] SET [status] = 'on' where [id] = '" + U.Id + "'";

            if (carExist == 2)
                {
                insertStr += " INSERT INTO [CoParkingUsersCars_2022] ([id], [numberCar], [isMain], [handicapped], [carPic] , [canEditCar]) VALUES('" + U.Id + "', '" + U.NumberCar + "', '" + currentMain + "', '" + Handicapped + "', '" + U.CarPic + "','T')";
            }
            else
            {
                insertStr += " INSERT INTO [CoParkingUsersCars_2022] ([id], [numberCar], [isMain], [handicapped], [carPic] , [canEditCar]) VALUES('" + U.Id + "', '" + U.NumberCar + "', '" + currentMain + "', '" + Handicapped + "', '" + U.CarPic + "','F')";
            }
                SqlCommand command2 = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command2.CommandType = System.Data.CommandType.Text;
            command2.CommandTimeout = 30;
            return command2;

        }

        protected bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                ErrorMessage = "The Email with Uncorrect format";
            Exception ex = new Exception(ErrorMessage);
            throw ex;
        }

        public Cars InsertCars(Cars C)
        {
            int CarExist = 1;
            if (ReadUser(C.NumberCar) is null)
            {
                CarExist = 2;
            }

                SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                if (checkedNumberCar(C.NumberCar,C.Id) == 1)
                {
                    Exception ex = new Exception("the NumberCar is exist.");
                    throw ex;
                }

                if (C.Id == 0)
                {
                    // C - Create Command
                    SqlCommand command = CreateInsertCar(C,CarExist, con);

                    // E - Execute
                    int affected = command.ExecuteNonQuery();

                    Exception ex = new Exception("Enter Id");
                    throw ex;
                }
                else
                {
                    if (C.Idcar != 0)
                    {
                        bool currentEditCar = false;
                        SqlCommand command2 = CreateInsertCar(C, CarExist, con);
                        int affected2 = (command2.ExecuteNonQuery());
                        string currentMain = "F";
                        SqlDataReader dr = null;
                        string commandStr = "SELECT * FROM CoParkingUsersCars_2022 WHERE id=" + C.Id;
                        SqlCommand cmd = createCommand(con, commandStr);
                        cmd.Parameters.AddWithValue("@numberCar", C.Id);
                        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (dr == null || !dr.Read())
                        {
                            currentMain = "T";
                            currentEditCar = true;

                        }
                        if (con != null)
                            con.Close();

                        con = null;
                        con = Connect("webOsDB");

                        SqlCommand command1 = CreateInsertUserCar(C,currentMain,CarExist, con);
                        int affected1 = (command1.ExecuteNonQuery());

                        // E - Execute
                        int affected = affected2 * affected1;

                        if (CarExist == 2)
                        { C.CanEditCar = true; }
                        else
                        { C.CanEditCar = false; }
                        return C;
                    }
                    else
                    {
                        SqlCommand command = CreateInsertUserCar(C,"F",CarExist, con);

                        // E - Execute
                        int affected = command.ExecuteNonQuery();

                        return C;
                    }
                }


            }
            //catch (Exception ex)
            //{
            //    // write to log file
            //    throw new Exception("Failed in Insert of Car", ex);
            //}

            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public int checkedNumberCar(string numberCar, int id)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;
            // C - Connect
            con = Connect("webOsDB");


            // Create the select command
            SqlCommand selectCommand = creatSelectUserCommand3(con, numberCar, id);

            // E - Execute
            int affected = selectCommand.ExecuteNonQuery();

            // Create the reader
            dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Read the records
            // Execute the command
            //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

            if (dr == null || !dr.Read())
            {
                return -1;
            }

            string number = (string)(dr["numberCar"]);
            if (number == null) { return 1; }
            return -1;

        }

        public int MakeMainCar(string numberCar, int id)
        {
            SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                SqlCommand command = CreateUpdateMainCar(numberCar,id, con);

                // E - Execute
                int affected = command.ExecuteNonQuery();

                return affected;

            }
            catch (Exception ex)
            {
                // write to log file
                throw new Exception("Failed in Insert of Main Car", ex);
            }

            finally
            {
                if (con != null)
                    con.Close();
            }
        }


        public int UpdateCars(Cars C)
        {
            SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                SqlCommand command = CreateUpdateCar(C, con);

                // E - Execute
                int affected = command.ExecuteNonQuery();

                return affected;

            }
            catch (Exception ex)
            {
                // write to log file
                throw new Exception("Failed in Insert of Car", ex);
            }

            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        SqlCommand CreateUpdateCar(Cars C, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(
                  "UPDATE [CoParkingCars_2022] " +
                  "SET [idCar] = '" + C.Idcar + "', [year] = '" + C.Year + "', [model] = '" + C.Model + "', [color] = '" + C.Color + "', [size] = '" + C.Size + "' WHERE [numberCar] = '" + C.NumberCar + "'",
                    con);
            //string insertStr = "INSERT INTO [CoParkingCars_2022] ([numberCar], [manufacturer], [year], [color], [size],[handicapped],[carPicture]) VALUES('" + C.NumberCar + "', '" + C.Manufacturer + "', '" + C.Model + "', '" + C.Year + "', '" + C.Color + "', '" + C.Size + "', '" + C.Handicapped + "', '" + C.CarPicture + "')";

            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }

        SqlCommand CreateUpdateMainCar(string numberCar, int id, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(
                  "UPDATE [CoParkingUsersCars_2022] SET isMain = 'F' WHERE id = '"+id+"' UPDATE [CoParkingUsersCars_2022] SET isMain = 'T' WHERE id = '"+id+"' AND numberCar = '"+numberCar+"'", con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }



        public int deleteCar(string numberCar, int id)
        {
            SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                SqlDataReader dr = null;

                SqlCommand selectCommand = checkCanEditCar(id,numberCar, con);
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                selectCommand = null;
                con = Connect("webOsDB");
                string canEditCar = "";
                if (dr.Read())
                {
                    canEditCar = (string)dr["canEditCar"];
                }
                if (con != null)
                    con.Close();

                con = null;
                con = Connect("webOsDB");
                SqlCommand command = DeleteNumberOfCar(numberCar,id, canEditCar, con);

                // E - Execute
                int affected = command.ExecuteNonQuery();

                return affected;

            }
            catch (Exception ex)
            {
                // write to log file
                throw new Exception("Failed in Insert of Car", ex);
            }

            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        SqlCommand DeleteNumberOfCar(string NumberCar,int id,string canEditCar, SqlConnection con)
        {
            string sub = "";
            if (canEditCar == "T")
            {
                sub = "DELETE FROM[CoParkingUsersCars_2022] WHERE[numberCar] = '" + NumberCar + "' AND [id] = '" + id + "'";
                sub += "delete from [CoParkingCars_2022] where [numberCar] = '" + NumberCar + "'";
            }
            else
            {
                sub = "DELETE FROM[CoParkingUsersCars_2022] WHERE[numberCar] = '" + NumberCar + "' AND [id] = '" + id + "'";
            }
            SqlCommand command = new SqlCommand(sub,con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        protected SqlCommand CreateInsertUser(User U, SqlConnection con)
        {

            string insertStr = "INSERT INTO [CoParkingUsers_2022] ([email], [password], [fName], [lName], [phoneNumber], [gender], [image]) VALUES('" + U.Email + "', '" + U.Password + "', '" + U.FName + "', '" + U.LName + "', '" + U.PhoneNumber + "', '" + U.Gender + "', '" + U.Image + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }

        SqlCommand CreateInsertCar(Cars C,int carExist, SqlConnection con)
        {
            string insertStr = "";
            if (carExist == 1)
            {
                insertStr = "SELECT * FROM CoParkingCars_2022 WHERE numberCar='" + ReadUser(C.NumberCar).NumberCar + "'";
            }
            else
            {
                insertStr = "INSERT INTO [CoParkingCars_2022] ([numberCar], [idCar], [year],[model], [color], [size]) VALUES('" + Convert.ToString(C.NumberCar) + "', '" + C.Idcar + "', '" + C.Year + "', '" + C.Model + "', '" + C.Color + "', '" + C.Size + "')";
            }


            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }

        public User ReadUserEmail(string email, int TypeOf)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // Create the select command
                SqlCommand selectCommand = creatSelectUserCommand(con, email);
                int affected = selectCommand.ExecuteNonQuery();

                //if (affected != 1)
                //{
                //    throw new Exception("The email not exist", ex);
                //}

                // Create the reader
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // Read the records
                // Execute the command
                //int id = Convert.ToInt32(insertCommand.ExecuteScalar());
                if (TypeOf == 1)
                {
                    if (dr == null || !dr.Read())
                    {
                        return null;
                    }
                }
                if (dr == null || !dr.Read())
                {
                    ErrorMessage = "the Email is not exist.";
                    Exception ex = new Exception(ErrorMessage);
                    throw ex;
                }

                int id = Convert.ToInt32(dr["id"]);
                string currentEmail = (string)dr["email"];
                string password = (string)dr["password"];
                string fName = (string)dr["fName"];
                string lName = (string)dr["lName"];
                string phoneNumber = (string)dr["phoneNumber"];
                char gender = char.Parse((string)dr["gender"]);
                string image = (string)dr["image"];
                int searchRadius = Convert.ToInt32(dr["searchRadius"]);
                int timeDelta = Convert.ToInt32(dr["timeDelta"]);
                string status = (string)dr["status"];
                int tokens = Convert.ToInt32(dr["tokens"]);

                User user = new User(id, fName, lName, currentEmail, password, gender, phoneNumber, image, searchRadius, timeDelta, status, tokens);

                if (dr.Read())
                {
                    return null;
                }

                return user;
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }

                // Close the connection
                if (con != null)
                    con.Close();
            }
        }

        public Cars ReadUser(string numberCar)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // Create the select command
                SqlCommand selectCommand = creatSelectCarsCommand(con, numberCar);

                // Create the reader
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // Read the records
                // Execute the command
                //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (dr == null || !dr.Read())
                {
                    return null;
                }

                string CurrentnumberCar = (string)(dr["numberCar"]);
                int idCar = Convert.ToInt32(dr["idCar"]);
                int year = Convert.ToInt32(dr["year"]);
                string model = (string)dr["model"];
                string color = (string)dr["color"];
                int size = Convert.ToInt32(dr["size"]);

                Cars cars = new Cars(CurrentnumberCar, idCar, year, model, color, size);

                if (dr.Read())
                {
                    return null;
                }

                return cars;
            }


            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in Log In", ex);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }

                // Close the connection
                if (con != null)
                    con.Close();
            }
        }

        public Cars ReadUserAndCar(string numberCar, int id)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // Create the select command
                SqlCommand selectCommand = GetCarAndId(con, numberCar, id);

                // Create the reader
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // Read the records
                // Execute the command
                //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (dr == null || !dr.Read())
                {
                    return null;
                }

                string CurrentnumberCar = (string)(dr["numberCar"]);
                int idCar = Convert.ToInt32(dr["idCar"]);
                int year = Convert.ToInt32(dr["year"]);
                string model = (string)dr["model"];
                string color = (string)dr["color"];
                int size = Convert.ToInt32(dr["size"]);
                bool isMain = false;
                if ((string)dr["isMain"] == "T")
                { isMain = true; }
                bool handicapped = false;
                if ((string)dr["handicapped"] == "T")
                { handicapped = true; }
                string carPic = (string)dr["carPic"];
                string manufacturer = (string)dr["manufacturer"];

                Cars cars = new Cars(id, CurrentnumberCar, isMain, handicapped, carPic, idCar, year, model, color, size, manufacturer);

                if (dr.Read())
                {
                    return null;
                }

                return cars;
            }


            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in Log In", ex);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }

                // Close the connection
                if (con != null)
                    con.Close();
            }
        }



        public Cars ReadMainCar(int id)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // Create the select command
                SqlCommand selectCommand = creatSelectMainCarCommand(con, id);

                // Create the reader
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // Read the records
                // Execute the command
                //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (dr == null || !dr.Read())
                {
                    return null;
                }

                string numberCar = (string)(dr["numberCar"]);
                bool isMain = true;
                string currenthandicapped = (string)dr["handicapped"];
                bool handicapped = true;
                if (currenthandicapped == "F") { handicapped = false; }
                string carPic = (string)dr["carPic"];
                int idCar = Convert.ToInt32(dr["idCar"]);
                int year = Convert.ToInt32(dr["year"]);
                string model = (string)dr["model"];
                string color = (string)dr["color"];
                int size = Convert.ToInt32(dr["size"]);

                Cars cars = new Cars(id, numberCar, isMain, handicapped, carPic, idCar, year, model, color, size);


                if (dr.Read())
                {
                    return null;
                }

                return cars;
            }


            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed return Main Car", ex);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }

                // Close the connection
                if (con != null)
                    con.Close();
            }
        }

        public Manufacture ReadManufacture(int idCar)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // Create the select command
                SqlCommand selectCommand = creatSelectManufactureCommand(con, idCar);

                // Create the reader
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // Read the records
                // Execute the command
                //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (dr == null || !dr.Read())
                {
                    return null;
                }

                string currentManufacturer = (string)dr["manufacturer"];

                Manufacture manufacturer = new Manufacture(idCar, currentManufacturer);


                return manufacturer;
            }


            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed return manufacturer", ex);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }

                // Close the connection
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand creatSelectMainCarCommand(SqlConnection con, int id)
        {
            string commandStr =
            "SELECT* FROM[CoParkingUsersCars_2022] LEFT JOIN[CoParkingCars_2022] ON CoParkingUsersCars_2022.numberCar = [CoParkingCars_2022].numberCar WHERE[CoParkingUsersCars_2022].id = @id AND[CoParkingUsersCars_2022].isMain = 'T'";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd;
        }

        private SqlCommand creatSelectManufactureCommand(SqlConnection con, int idCar)
        {
            string commandStr = "SELECT * FROM[CoParkingManufacture_2022] WHERE idCar = @idCar";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@idCar", idCar);
            return cmd;
        }

        private SqlCommand creatSelectUserCommand(SqlConnection con, string email)
        {
            string commandStr = "SELECT * FROM CoParkingUsers_2022 WHERE email=@email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@email", email);

            return cmd;
        }

        private SqlCommand creatSelectUserCommand2(SqlConnection con, int id, string numberCar)
        {
            string commandStr = "SELECT * FROM CoParkingUsersCars_2022 WHERE id=@id AND numberCar=@numberCar";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@numberCar", numberCar);

            return cmd;
        }

        private SqlCommand creatSelectUserCommand3(SqlConnection con, string numberCar, int id)
        {
            string commandStr = " SELECT * FROM CoParkingUsersCars_2022 WHERE numberCar='"+numberCar+"' AND id='"+id+"'";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@numberCar", numberCar);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd;
        }

        private SqlCommand creatSelectCarsCommand(SqlConnection con, string numberCar)
        {
            string commandStr = "SELECT * FROM CoParkingCars_2022 WHERE numberCar=@numberCar";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@numberCar", numberCar);

            return cmd;
        }

        private SqlCommand GetCarAndId(SqlConnection con, string numberCar, int id)
        {
            string commandStr = "SELECT * FROM CoParkingUsersCars_2022 LEFT JOIN CoParkingCars_2022 ON CoParkingUsersCars_2022.numberCar = CoParkingCars_2022.numberCar LEFT JOIN CoParkingManufacture_2022 ON CoParkingCars_2022.idCar = CoParkingManufacture_2022.idCar where id = '"+id+"' AND CoParkingCars_2022.numberCar = '"+numberCar+"';";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@numberCar", numberCar);
            cmd.Parameters.AddWithValue("@id", id);


            return cmd;
        }

        
        SqlCommand createCommand(SqlConnection con, string CommandSTR)
        {

            SqlCommand cmd = new SqlCommand();  // create the command object
            cmd.Connection = con;               // assign the connection to the command object
            cmd.CommandText = CommandSTR;       // can be Select, Insert, Update, Delete
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 5; // seconds

            return cmd;
        }

        public void CreateTables()
        {
            SqlConnection con = null;
            try
            {
                con = this.Connect("webOsDB");

                SqlCommand usersCmd = this.createUserTable(con);
                usersCmd.ExecuteNonQuery();

                /*SqlCommand carsCmd = this.createCarsTable(con);//לתקן לרכבים
                carsCmd.ExecuteNonQuery();*/
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        protected bool ValidatePassword(string password)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one lower case letter.";
                Exception ex = new Exception(ErrorMessage);
                throw ex;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one upper case letter.";
                Exception ex = new Exception(ErrorMessage);
                throw ex;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be lesser than 8 or greater than 15 characters.";
                Exception ex = new Exception(ErrorMessage);
                throw ex;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one numeric value.";
                Exception ex = new Exception(ErrorMessage);
                throw ex;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one special case character.";
                Exception ex = new Exception(ErrorMessage);
                throw ex;
            }
            else
            {
                return true;
            }
        }

        public string ReadPaswword(string email)
        {
            {
                SqlConnection con = null;
                SqlDataReader dr = null;
                // C - Connect
                con = Connect("webOsDB");


                // Create the select command
                SqlCommand selectCommand = creatSelectUserCommand(con, email);
                Random rnd = new Random();
                string tmpPassword = "CO" + rnd.Next(99) + "!" + rnd.Next(99) + rnd.Next(99);

                SqlCommand command = createPassword(con, email, tmpPassword);

                // E - Execute
                int affected = command.ExecuteNonQuery();

                // Create the reader
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // Read the records
                // Execute the command
                //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (dr == null || !dr.Read())
                {
                    Exception ex = new Exception("the Email is not exist.");
                    throw ex;
                }



                string password = (string)dr["password"];

                if (dr.Read())
                {
                    return null;
                }

                return password;
            }
        }

        SqlCommand createPassword(SqlConnection con, string email, string Password)
        {
            //Random rnd = new Random();
            //string tmpPassword = "CO"+ rnd.Next(99)+"!"+ rnd.Next(99) + rnd.Next(99);
            string insertStr = "UPDATE [CoParkingUsers_2022] " +
      "SET [password] = '" + Password + "' WHERE [email] = '" + email + "'";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }

        public int UpdatePassword(string mail, string currentPassword, string password1, string password2)
        {
            SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                int status = CreateUpdatePassword(mail, currentPassword, password1, password2, con);
                if (status == -1)
                {
                    Exception ex = new Exception(ErrorMessage);
                    throw ex;
                }
                else return status;

            }
            catch (Exception ex)
            {
                // write to log file
                throw new Exception(ErrorMessage, ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public int TakeParking(int idUser, int parkingCode)
        {
            SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                int status = TryTakePariking(idUser, parkingCode, con);
                return status;

            }
            catch (Exception ex)
            {
                // write to log file
                throw new Exception(ErrorMessage, ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public int ReturnParking(int parkingCode)
        {

            SqlConnection con = null;
            SqlDataReader dr = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command

                SqlCommand selectCommand = CreateUpdateReturnCar(parkingCode, con);

                // E - Execute
                int affected = selectCommand.ExecuteNonQuery();

                // Create the reader
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // Read the records
                // Execute the command
                //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (affected != 1)
                {
                    ErrorMessage = "The parking not exist";
                    Exception ex = new Exception(ErrorMessage);
                    throw ex;
                }

                return affected;
            }
            catch (Exception ex)
            {
                // write to log file
                throw new Exception(ErrorMessage, ex);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        SqlCommand CreateUpdateReturnCar(int parkingCode, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(
                  "UPDATE [CoParkingParkings_2022] " +
                  "SET [userCodeIn] = null WHERE [parkingCode] = '" + parkingCode + "'",
                    con);
            //string insertStr = "INSERT INTO [CoParkingCars_2022] ([numberCar], [manufacturer], [year], [color], [size],[handicapped],[carPicture]) VALUES('" + C.NumberCar + "', '" + C.Manufacturer + "', '" + C.Model + "', '" + C.Year + "', '" + C.Color + "', '" + C.Size + "', '" + C.Handicapped + "', '" + C.CarPicture + "')";

            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }

        public int CreateUpdatePassword(string mail, string currentPassword, string password1, string password2, SqlConnection con)
        {
            int affected = -1;
            if (!(ReadOnlyPaswword(mail).Equals(currentPassword)))
            {
                ErrorMessage = "the password from mail not correct";
                return affected;
            }
            if (!password1.Equals(password2))
            {
                ErrorMessage = "the passwords are not equal";
                return affected;
            }
            if (ValidatePassword(password1) is true)
            {
                SqlCommand command = createPassword(con, mail, password1);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandTimeout = 30;
                affected = command.ExecuteNonQuery();
                return affected;
            }
            return affected;
        }

        public int TryTakePariking(int idUser, int parkingCode, SqlConnection con)
        {
            SqlDataReader dr = null;
            SqlCommand selectCommand = checkParking(parkingCode, con);
            dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            selectCommand = null;
            con = Connect("webOsDB");
            if (dr.Read())
            {
                ErrorMessage = "The parking have alreday UserIn";
                Exception ex = new Exception(ErrorMessage);
                throw ex;
            }

            selectCommand = new SqlCommand("UPDATE[CoParkingParkings_2022] SET[userCodeIn] = '" + idUser + "' WHERE[parkingCode] = '" + parkingCode + "';",
con);
            int affected = selectCommand.ExecuteNonQuery();
            selectCommand.CommandType = System.Data.CommandType.Text;
            selectCommand.CommandTimeout = 30;
            return affected;
        }

        private SqlCommand checkParking(int ParkingCode, SqlConnection con)
        {
            //AND userCodeIn IS NOT NULL
            string commandStr = "SELECT * FROM CoParkingParkings_2022 WHERE parkingCode=@ParkingCode AND userCodeIn IS NOT NULL";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@ParkingCode", ParkingCode);
            int rowsAffected = cmd.ExecuteNonQuery();
            return cmd;
        }
        private SqlCommand checkCanEditCar(int id,string numberCar, SqlConnection con)
        {
            //AND userCodeIn IS NOT NULL
            string commandStr = "select * from[CoParkingUsersCars_2022] where id = '"+id+"' AND numberCar = '"+numberCar+"'";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@numberCar", numberCar);
            cmd.Parameters.AddWithValue("@id", id);
            int rowsAffected = cmd.ExecuteNonQuery();
            return cmd;
        }

        


        private SqlCommand addUserCodeIn(int idUser, int parkingCode, SqlConnection con)
        {
            SqlCommand command = new SqlCommand("UPDATE[CoParkingParkings_2022] SET[userCodeIn] = '" + idUser + "' WHERE[parkingCode] = '" + parkingCode + "';",
            con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }

        public string ReadOnlyPaswword(string email)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;
            // C - Connect
            con = Connect("webOsDB");


            // Create the select command
            SqlCommand selectCommand = creatSelectUserCommand(con, email);
            Random rnd = new Random();

            // E - Execute
            int affected = selectCommand.ExecuteNonQuery();

            // Create the reader
            dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Read the records
            // Execute the command
            //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

            if (dr == null || !dr.Read())
            {
                ErrorMessage = "the Email is not exist.";
                Exception ex = new Exception(ErrorMessage);
                throw ex;
            }
            string password = (string)dr["password"];

            if (dr.Read())
            {
                return null;
            }

            return password;
        }

        public int CheckUserAndNumberCar(int id, string numberCar)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;
            // C - Connect
            con = Connect("webOsDB");


            // Create the select command
            SqlCommand selectCommand = creatSelectUserCommand2(con, id, numberCar);

            // E - Execute
            int affected = selectCommand.ExecuteNonQuery();

            // Create the reader
            dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Read the records
            // Execute the command
            //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

            if (dr == null || !dr.Read())
            {
                return -1;
            }

            string number = (string)(dr["numberCar"]);
            if (number == null) { return 1; }
            return -1;

        }


        SqlCommand createUserTable(SqlConnection con)
        {
            string commandStr = "IF OBJECT_ID (N'[CoParkingCars_2022]', N'U') IS NULL BEGIN " +
                "CREATE TABLE [CoParkingCars_2022] (" +
                "[numberCar] int NOT NULL," +
                "[idCar] int  NOT NULL," +
                "[year] int NOT NULL," +
                "[model] nvarchar (100) NOT NULL," +
                "[color] nvarchar (100) NOT NULL," +
                "[size] int NOT NULL," +
                "Primary key (numberCar));" +
                " END;";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }

        SqlCommand createCarsTable(SqlConnection con)
        {
            string commandStr = "IF OBJECT_ID (N'[CoParkingUsers_2022]', N'U') IS NULL BEGIN " +
                "CREATE TABLE [CoParkingUsers_2022] (" +
                "[id] int IDENTITY(1, 1) NOT NULL," +
                "[email] nvarchar(100) NOT NULL," +
                "[password] nvarchar(100) NOT NULL," +
                "[fName] nvarchar(100) NOT NULL," +
                "[lName] nvarchar(100) NOT NULL," +
                "[phoneNumber] nvarchar(100) NOT NULL," +
                "[gender] nvarchar(100) NOT NULL," +
                "[image] nvarchar(100)," +
                "[searchRadius] int DEFAULT (2000) NOT NULL," +
                "[timeDelta] int DEFAULT (10) NOT NULL," +
                "[status] nvarchar(100) DEFAULT 'Off' NOT NULL," +
                "[tokens] int DEFAULT(30) NOT NULL," +
                "Primary key(id));" +
                " END;";
            SqlCommand cmd = createCommand(con, commandStr);
            return cmd;
        }
    }
}

