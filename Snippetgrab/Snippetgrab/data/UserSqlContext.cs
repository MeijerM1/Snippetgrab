using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Activation;
using System.Windows.Forms;

namespace Snippetgrab.data
{
    class UserSqlContext : IUserContext
    {
        private SqlConnection Connection;
        private string sqlCon;

        public UserSqlContext()
        {
            sqlCon = @"Data Source = (LocalDB)\MSSQLLocalDB;" +
                     @"AttachDbFilename=|DataDirectory|\Snippetgrab.mdf;" +
                     "Integrated Security = True;" +
                     "Connect Timeout = 30";
        }

        public bool CheckPassword(string email, string password)
        {

            Connection = new SqlConnection(sqlCon);
            User user;
            using (Connection)
            {
                string query = "SELECT * FROM [User] WHERE Email = @email";
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@email";
                    param.Value = email;
                    command.Parameters.Add(param);

                    Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = CreateUserFromReader(reader);
                            if (user.GetPassword() == password)
                                return true;
                            else
                                return false;
                        }
                    }
                }                
            }
            return false;
        }

        public List<User> GetAll()
        {
            Connection = new SqlConnection(sqlCon);

            List<User> result = new List<User>();
            using (Connection)
            {
                string query = "SELECT * FROM [User] ORDER BY UserID";
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateUserFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }

        public List<User> GetAllAdmin()
        {
            Connection = new SqlConnection(sqlCon);

            List<User> result = new List<User>();
            using (Connection)
            {
                string query = "SELECT * FROM User WHERE IsAdmin = 1 ORDER BY Id";
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateUserFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }

        public User GetByEmail(string Email)
        {
            Connection = new SqlConnection(sqlCon);

            User user;
            using (Connection)
            {
                string query = "SELECT * FROM [User] WHERE Email = @email";
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@email";
                    param.Value = Email;
                    command.Parameters.Add(param);

                    Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = CreateUserFromReader(reader);
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public User GetById(int id)
        {
            Connection = new SqlConnection(sqlCon);

            User user;
            using (Connection)
            {
                string query = "SELECT * FROM [User] WHERE UserID = @id";
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@id";
                    param.Value = id;
                    command.Parameters.Add(param);

                    Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = CreateUserFromReader(reader);
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public bool Insert(User user)
        {
            Connection = new SqlConnection(sqlCon);
            using (Connection)
            {
                string query =
                    "INSERT INTO [User] (Name, JoinDate, Reputation, Email, Password, IsAdmin) VALUES (@name, @joindate, @reputation, @email, @password, @isAdmin)";
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    DateTime dateOnly = user.JoinDate.Date;
                    string sqlFormattedDate = dateOnly.ToString("d");

                    command.Parameters.AddWithValue("name", user.Name);
                    command.Parameters.AddWithValue("joindate", sqlFormattedDate);
                    command.Parameters.AddWithValue("reputation", user.Reputation);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("password", user.GetPassword());
                    command.Parameters.AddWithValue("isAdmin", Convert.ToInt32(user.IsAdmin));

                    Connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        // If a PK constraint was violated, handle the exception
                        if (e.Number == 2627)
                        {
                            return false;
                        }
                        // Unexpected error: rethrow to let the caller handle it
                        throw;
                    }
                }
                return true;
            }
        }

        public bool Remove(int id)
        {
            Connection = new SqlConnection(sqlCon);
            using (Connection)
            {
                string query = "DELETE FROM [User] WHERE UserID = @id";
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    Connection.Open();
                    if (Convert.ToInt32(command.ExecuteNonQuery()) == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }

        private User CreateUserFromReader(SqlDataReader reader)
        {
            return new User(
                Convert.ToInt32(reader["UserID"]),
                Convert.ToString(reader["Name"]),
                Convert.ToDateTime(reader["JoinDate"]),
                Convert.ToInt32(reader["Reputation"]),
                Convert.ToString(reader["Email"]),
                Convert.ToBoolean(reader["IsAdmin"]),
                Convert.ToString(reader["Password"]));
        }
    }
}
