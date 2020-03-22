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
                        Pass = reader.GetValue(2).ToString() 
                    });
                }
            }

            reader.Close();
            connection.Close();
            return userList;
        }

        public static void UpdateUser(User user)
        {

            string requestUsers = "UPDATE Users SET Pass=@pass, Login=@login WHERE id = @Uid";

            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestUsers, connection);
            command.Parameters.AddWithValue("@pass", user.Pass);
            command.Parameters.AddWithValue("@login", user.Login);
            command.Parameters.AddWithValue("@Uid", user.Id);
            int response = command.ExecuteNonQuery();
            connection.Close();

            if(response < 0)
            {
                throw new Exception($"Error update User: User ID={user.Id}.");
            }

        }

        public static int DeleteUser(User user)
        {

            string requestLogins = "DELETE FROM Users WHERE Login = @login";
            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestLogins, connection);
            command.Parameters.AddWithValue("@login", user.Login);
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
            string requestRegestration = "INSERT INTO Users (Login,Pass,Gender) VALUE (@login,@pass,@gender)";
            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestRegestration, connection);
            command.Parameters.AddWithValue("@login", user.Login);
            command.Parameters.AddWithValue("@pass", user.Pass);
            command.Parameters.AddWithValue("@gender", user.Gender);
            
            int i = command.ExecuteNonQuery();
            connection.Close();

            if(i<0)
            {
                throw new Exception("No table to new User");
            }
        }
       
        public static User Login(string login,string pass)
        {
            User user = new User();

            string requestUsers = "SELECT * FROM Users WHERE login=@login AND pass=@pass";

            MySqlConnection connection = GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand(requestUsers, connection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@pass", pass);
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
            connection.Close();

            if (user.Id == 0)
            {
                throw new Exception("No user found.");
            }

            return user;

        }
    }
}
