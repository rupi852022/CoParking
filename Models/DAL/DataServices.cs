using ParkingProject.Models;
using System;
using System.Data;
using System.Data.SqlClient;


namespace Class_demo.Models.DAL
{
    public class DataServices
    {

            public int InsertUser(User U)
            {
                SqlConnection con = null;
                try
                {
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
                    throw new Exception("Failed in Insert of User", ex);
                }

                finally
                {
                    // Close Connection
                    con.Close();
                }
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





        SqlCommand CreateInsertUser(User U, SqlConnection con)
            {

                string insertStr = "INSERT INTO [CoParkingUsers_2022] ([email], [password], [fName], [lName], [phoneNumber], [gender]) VALUES('" + U.Email + "', '" + U.Password + "', '" + U.FName + "', '" + U.LName + "', '" + U.PhoneNumber + "', '" + U.Gender + "')";
                SqlCommand command = new SqlCommand(insertStr, con);
                // TBC - Type and Timeout
                command.CommandType = System.Data.CommandType.Text;
                command.CommandTimeout = 30;
                return command;

            }

        SqlCommand CreateInsertCar(Cars C, SqlConnection con)
        {

            string insertStr = "INSERT INTO [CoParkingCars_2022] ([numberCar], [manufacturer], [model], [year], [color], [size],[handicapped],[carPicture]) VALUES('" + C.NumberCar + "', '" + C.Manufacturer + "', '" + C.Model + "', '" + C.Year + "', '" + C.Color + "', '" + C.Size + "', '" + C.Handicapped + "', '" + C.CarPicture + "')";
            SqlCommand command = new SqlCommand(insertStr, con);
            // TBC - Type and Timeout
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 30;
            return command;

        }




        //Read
        public User ReadUser(string email)
            {
                SqlConnection con = null;
                SqlDataReader dr = null;

                try
                {
                    // Connect
                    con = Connect("webOsDB");

                    // Create the select command
                    SqlCommand selectCommand = creatSelectUserCommand(con, email, password);

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
                    string email = (string)dr["email"];
                    string password = (string)dr["password"];
                    string fName = (string)dr["lName"];
                    string lName = (string)dr["fName"];
                    string phoneNumber = (string)dr["phoneNumber"];
                    string gender = (string)dr["gender"];



                    User user = new User();

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

            private SqlCommand creatSelectUserCommand(SqlConnection con, string email)
            {
                string commandStr = "SELECT * FROM CoParkingUsers_2022 WHERE email=@email";
                SqlCommand cmd = createCommand(con, commandStr);
                cmd.Parameters.AddWithValue("@email", email);

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
        }



    }

