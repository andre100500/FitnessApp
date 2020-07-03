using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SqlTest.Data;

namespace WpfApp
{
    public class SQLHelper
    {
        private static  string host = "localhost";
        private static  int port = 3306;
        private static string database = "fitnes";
        private static string username = "root";
        private static string password = "";

        private static MySqlConnection GetDBConnection()
        {
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }

        public static List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();

            string requestUsers = "SELECT * FROM Users";

            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestUsers, connection);
            MySqlDataReader reader = command.ExecuteReader();



            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userList.Add(new User() { 
                        Id = int.Parse(reader.GetValue(0).ToString()), 
                        Login = reader.GetValue(1).ToString(),
                        //Pass = reader.GetValue(2).ToString() 
                    });
                }
            }

            reader.Close();
            connection.Close();
            return userList;
        }
        //TODO сделать правильный запрос
        public static void SaveSettings(User user)
        {
            string requestUsers = "UPDATE Users SET  Pass=@pass, Age=@age, Mass=@mass, Height=@height WHERE Id=@id";

            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestUsers, connection);
            command.Parameters.AddWithValue("@pass",user.Pass);
           // command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@age",user.Age);
            command.Parameters.AddWithValue("@mass", user.Mass);
            command.Parameters.AddWithValue("@height", user.Height);
            command.Parameters.AddWithValue("@id", user.Id);


            int response = command.ExecuteNonQuery();    
            

            if(response<0)
            {
                throw new Exception($"Error Update User: User ID={user.Id}.");
            }
            connection.Close();
        }

        //public static User ExerciseUser(User user)
        //{
        //    string requestExerice = "SELECT* FROM Exercise WHERE UserId = @userid";
        //    MySqlConnection connection = GetDBConnection();
        //    connection.Open();
        //    MySqlCommand command = new MySqlCommand(requestExerice, connection);
        //    command.Parameters.AddWithValue("@userid", user.ExerciseList.UserId);

        //    MySqlDataReader reader = command.ExecuteReader();
        //    if(reader.HasRows)
        //    {
        //        while(reader.Read())
        //        {
        //            user = new User()
        //            {
        //                Id = int.Parse(reader.GetValue(0).ToString()),
        //                UserId = int.Parse(reader.GetValue(1).ToString())
        //            };

        //        }
        //    }
        //    reader.Close();
        //    connection.Close();
        //    return user;
        //}
        ////TODO Ежедневная добавление массы
        public static User ProgressChartUser(User user)
        {
            string requestProgressChart = "SELECT * FROM Progress WHERE UserId = @UserId";
            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestProgressChart, connection);
            command.Parameters.AddWithValue("@UserId", user.Mass);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user = new User()
                    {
                        Id = int.Parse(reader.GetValue(0).ToString()),
                       //ProgressChart = int.Parse(reader.GetValue(1).ToString())
                    };

                }
            }
            reader.Close();
            connection.Close();
            return user;
        }

        public static int DeleteUser(User user)
        {

            string requestLogins = "DELETE FROM Users WHERE Login = @login Pass=@pass";
            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestLogins, connection);
            command.Parameters.AddWithValue("@login", user.Login);
            command.Parameters.AddWithValue("@pass",user.Pass);
            int i = command.ExecuteNonQuery();

            connection.Close();
            return i;
        }

        public static int InsertUser(User user)
        {

            string requestUsers = "INSERT INTO Users (Login,Pass) VALUE (@login,@pass)";
            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestUsers, connection);
            command.Parameters.AddWithValue("@login", user.Login);
            command.Parameters.AddWithValue("@pass", user.Pass);
            int i = command.ExecuteNonQuery();
            connection.Close();

            return i;
        }

        public static void Regestration(User user)
        {
            string requestRegestration = "INSERT INTO Users (Login,Pass,Gender,Email,Age,Mass,Height) " +
                "VALUE (@login,@pass,@gender,@email,@age,@mass,@height)";
            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestRegestration, connection);
            command.Parameters.AddWithValue("@login", user.Login);
            command.Parameters.AddWithValue("@pass", user.Pass);
            command.Parameters.AddWithValue("@gender", user.Gender);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@age", user.Age);
            command.Parameters.AddWithValue("@mass",user.Mass);
            command.Parameters.AddWithValue("@height", user.Height);
            
            int i = command.ExecuteNonQuery();
            connection.Close();

            if(i<0)
            {
                throw new Exception("No table to new User");
            }
        }
       
        public static User Settings (User user)
        {
            string requestUsers = "SELECT * FROM Users WHERE login=@login AND pass=@pass AND mass=@mass AND height=@height AND age=@age AND email";

            MySqlConnection connection = GetDBConnection();
            connection.Open();

            MySqlCommand command = new MySqlCommand(requestUsers, connection);
            command.Parameters.AddWithValue("@login", user.Login);
            command.Parameters.AddWithValue("@pass",user.Pass);
            command.Parameters.AddWithValue("@mass", user.Mass);
            command.Parameters.AddWithValue("@height", user.Height);
            command.Parameters.AddWithValue("@age", user.Age);
            command.Parameters.AddWithValue("@email", user.Email);

            MySqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    user = new User()
                    {
                        Id = int.Parse(reader.GetValue(0).ToString()),
                        Login = reader.GetValue(1).ToString(),
                        Email = reader.GetValue(4).ToString(),
                        Age= int.Parse(reader.GetValue(5).ToString()),
                        Mass=double.Parse(reader.GetValue(6).ToString()),
                        Height=double.Parse(reader.GetValue(7).ToString())
                    };
                }
            }
            reader.Close();
            connection.Close();

            if(user.Id == 0)
            {
                throw new Exception("No user found.");
            }
            return user;

        }

        public static User Login(User user)
        {

            string requestUsers = "SELECT * FROM Users WHERE login=@login AND pass=@pass";

            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestUsers, connection);
            command.Parameters.AddWithValue("@login", user.Login);
            command.Parameters.AddWithValue("@pass", user.Pass);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user = new User()
                    { 
                        Id = int.Parse(reader.GetValue(0).ToString()),
                        Login = reader.GetValue(1).ToString(),
                        Pass = reader.GetValue(2).ToString()
                    };
                }
            }

            reader.Close();


            if (user.Id == 0)
            {
                throw new Exception("No user found.");
            }
            connection.Close();
            return user;

        }
    }
}
