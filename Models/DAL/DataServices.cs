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

        SqlConnection Connect(string connectionStringName)
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
                char gender = (char)dr["gender"];
                string image = (string)dr["image"];
                int searchRadius= Convert.ToInt32(dr["searchRadius"]);
                int timeDelta= Convert.ToInt32(dr["timeDelta"]);
                string status = (string)dr["status"];
                int tokens = Convert.ToInt32(dr["tokens"]);



                User user = new User(id, fName, lName, currentEmail, password, gender, phoneNumber, image, searchRadius, timeDelta, status, tokens);
                users.Add(user);
            }

            return users.ToArray();
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



        public int InsertUser(User U)
        {
            SqlConnection con = null;
            try
            {
                User user = this.ReadUser(U.Email);
                
                if (user != null)
                {
                    ErrorMessage = "Failed in Insert of User - The email or password alredy exist";
                    return -1;
                }
                else
                {
                    if(ValidateEmail(U.Email) is false)
                    {
                        return -1;
                    }
                    if (ValidatePassword(U.Password) is false)
                    {
                        return -1;
                    }
                }
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                SqlCommand command = CreateInsertUser(U, con);

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
                // Close Connection
                con.Close();
            }
        }


        public int InsertUserCar(UsersCars U)
        {
            SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                SqlCommand command = CreateInsertUserCar(U, con);

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
                // Close Connection
                con.Close();
            }
        }

        public int InsertParking(Parking P)
        {
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
                // Close Connection
                con.Close();
            }
        }

        SqlCommand CreateInsertParking(Parking P, SqlConnection con)
        {
            string insertStr = "";
            string currentexitDate = P.ExitDate.ToString("dd/MM/yyyy");
            string currentexitHour = P.ExitTime.ToString("HH:mm:ss");
            if (P.UserCodeIn == 0)
            {
                insertStr += " INSERT INTO [CoParkingParkings_2022] ([location], [exitDate], [exitTime], [typeOfParking], [singType], [userCodeOut]) VALUES('" + P.Location + "', '" + currentexitDate + "', '" + currentexitHour + "', '" + P.TypeOfParking + "', '" + P.SingType + "', '" + P.UserCodeOut + "')";
            }
            else
            {
                insertStr += " INSERT INTO [CoParkingParkings_2022] ([location], [exitDate], [exitTime], [typeOfParking], [singType], [userCodeOut], [userCodeIn]) VALUES('" + P.Location + "', '" + currentexitDate + "', '" + currentexitHour + "', '" + P.TypeOfParking + "', '" + P.SingType + "', '" + P.UserCodeOut + "', '" + P.UserCodeIn + "')";
            }
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }

        SqlCommand CreateInsertUserCar(UsersCars U, SqlConnection con)
        {
            string insertStr = "";
            string currentMain = "T";
            string Handicapped = "T";
            if (U.IsMain is false) { currentMain = "F"; }
            if (U.Handicapped is false) { Handicapped = "F"; }
            if (U.IsMain is true) {
                currentMain = "T";
                insertStr += " UPDATE [CoParkingUsersCars_2022] SET [isMain] = 'F'";
            }
            insertStr += " UPDATE [CoParkingUsers_2022] SET [status] = 'on' where [id] = '"+U.Id+"'";

            insertStr += " INSERT INTO [CoParkingUsersCars_2022] ([id], [numberCar], [isMain], [handicapped], [carPic]) VALUES('" + U.Id + "', '" + U.NumberCar + "', '" + currentMain + "', '" + Handicapped + "', '" + U.CarPic + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }



        private bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                ErrorMessage = "The Email with Uncorrect format";
                 return false;
        }

        public int InsertCars(Cars C)
        {
            SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                SqlCommand command = CreateInsertCar(C, con);

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
                // Close Connection
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
                // Close Connection
                con.Close();
            }
        }

        SqlCommand CreateUpdateCar(Cars C, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(
                  "UPDATE [CoParkingCars_2022] " +
                  "SET [idCar] = '" + C.Idcar + "', [year] = '" + C.Year + "', [model] = '" + C.Model +  "', [color] = '" + C.Color + "', [size] = '" + C.Size  + "' WHERE [numberCar] = '" + C.NumberCar + "'",
                    con);
            //string insertStr = "INSERT INTO [CoParkingCars_2022] ([numberCar], [manufacturer], [year], [color], [size],[handicapped],[carPicture]) VALUES('" + C.NumberCar + "', '" + C.Manufacturer + "', '" + C.Model + "', '" + C.Year + "', '" + C.Color + "', '" + C.Size + "', '" + C.Handicapped + "', '" + C.CarPicture + "')";

            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }


        public int deleteCar(int NumberCar)
        {
            SqlConnection con = null;
            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // C - Create Command
                SqlCommand command = DeleteNumberOfCar(NumberCar, con);

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
                // Close Connection
                con.Close();
            }
        }

        SqlCommand DeleteNumberOfCar(int NumberCar, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(
                   "DELETE FROM [CoParkingCars_2022] WHERE [numberCar]='" + NumberCar + "'",
                     con);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;
        }




        SqlCommand CreateInsertUser(User U, SqlConnection con)
        {

            string insertStr = "INSERT INTO [CoParkingUsers_2022] ([email], [password], [fName], [lName], [phoneNumber], [gender], [image]) VALUES('" + U.Email + "', '" + U.Password + "', '" + U.FName + "', '" + U.LName + "', '" + U.PhoneNumber + "', '" + U.Gender + "', '"+U.Image+"')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }

        SqlCommand CreateInsertCar(Cars C, SqlConnection con)
        {


            string insertStr = "INSERT INTO [CoParkingCars_2022] ([numberCar], [idCar], [year],[model], [color], [size]) VALUES('" + Convert.ToString(C.NumberCar) + "', '" + C.Idcar + "', '" + C.Year + "', '" + C.Model + "', '" + C.Color + "', '" + C.Size + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }

        public User ReadUser(string email)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;

            try
            {
                // C - Connect
                con = Connect("webOsDB");

                // Create the select command
                SqlCommand selectCommand = creatSelectUserCommand(con, email);

                // Create the reader
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // Read the records
                // Execute the command
                //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (dr == null || !dr.Read())
                {
                    return null;
                }

                int id = Convert.ToInt32(dr["id"]);
                string currentEmail = (string)dr["email"];
                string password = (string)dr["password"];
                string fName = (string)dr["lName"];
                string lName = (string)dr["fName"];
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

        public Cars ReadUser(int numberCar)
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

                int CurrentnumberCar = Convert.ToInt32(dr["numberCar"]);
                int idCar = Convert.ToInt32(dr["idCar"]);
                int year = Convert.ToInt32(dr["year"]);
                string model = (string)dr["model"];
                string color = (string)dr["color"];
                int size = Convert.ToInt32(dr["size"]);

                Cars cars = new Cars(CurrentnumberCar, idCar, year,model, color, size);

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

        private SqlCommand creatSelectUserCommand(SqlConnection con, string email)
        {
            string commandStr = "SELECT * FROM CoParkingUsers_2022 WHERE email=@email";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@email", email);

            return cmd;
        }

        private SqlCommand creatSelectCarsCommand(SqlConnection con, int numberCar)
        {
            string commandStr = "SELECT * FROM CoParkingCars_2022 WHERE numberCar=@numberCar";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.AddWithValue("@numberCar", numberCar);

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

        private bool ValidatePassword(string password)
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
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one upper case letter.";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be lesser than 8 or greater than 15 characters.";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one numeric value.";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one special case character.";
                return false;
            }
            else
            {
                return true;
            }
        }

        public string ReadPaswword(string email)
        {
            SqlConnection con = null;
            SqlDataReader dr = null;

                // C - Connect
                con = Connect("webOsDB");

                // Create the select command
                SqlCommand selectCommand = creatSelectUserCommand(con, email);

                // Create the reader
                dr = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

                // Read the records
                // Execute the command
                //int id = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (dr == null || !dr.Read())
                {
                    return null;
                }
                string password = (string)dr["password"];

                if (dr.Read())
                {
                    return null;
                }

                return password;
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
                "[image] nvarchar(100) NOT NULL," +
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

