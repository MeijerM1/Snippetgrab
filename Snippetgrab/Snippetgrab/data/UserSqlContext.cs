using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Snippetgrab.data
{
    public class UserSqlContext : IUserContext
    {
        private SqlConnection _connection;
        private readonly string _sqlCon;

        public UserSqlContext()
        {
            _sqlCon = @"Data Source = (LocalDB)\MSSQLLocalDB;" +
                     @"AttachDbFilename=|DataDirectory|\Snippetgrab.mdf;" +
                     "Integrated Security = True;" +
                     "Connect Timeout = 30";
        }

        public bool CheckPassword(string email, string password)
        {
            _connection = new SqlConnection(_sqlCon);            

            using (_connection)
            {
                const string query = "SELECT Salt, HashedPassword FROM [User] WHERE Email = @email";
                using (var command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("email", email);

                    _connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var passwordFromDb = Convert.ToString(reader["HashedPassword"]);
                            var hashedPassword = GenerateSha256Hash(password, Convert.ToString(reader["Salt"]));

                            return (hashedPassword == passwordFromDb);
                        }
                    }
                }                
            }
            return false;
        }

        public List<User> GetAll()
        {
            _connection = new SqlConnection(_sqlCon);

            var result = new List<User>();
            using (_connection)
            {
                const string query = "SELECT * FROM [User] ORDER BY UserID";
                using (var command = new SqlCommand(query, _connection))
                {
                    _connection.Open();
                    using (var reader = command.ExecuteReader())
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
            _connection = new SqlConnection(_sqlCon);

            var result = new List<User>();
            using (_connection)
            {
                const string query = "SELECT * FROM User WHERE IsAdmin = 1 ORDER BY Id";
                using (var command = new SqlCommand(query, _connection))
                {
                    using (var reader = command.ExecuteReader())
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
            _connection = new SqlConnection(_sqlCon);

            using (_connection)
            {
                const string query = "SELECT * FROM [User] WHERE Email = @email";
                using (var command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("email", Email);

                    _connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = CreateUserFromReader(reader);
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public User GetById(int id)
        {
            _connection = new SqlConnection(_sqlCon);

            using (_connection)
            {
                const string query = "SELECT * FROM [User] WHERE UserID = @id";
                using (var command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    _connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = CreateUserFromReader(reader);
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public bool Insert(User user, string password)
        {
            _connection = new SqlConnection(_sqlCon);

            var salt = CreateSalt();
            var hashedPassword = GenerateSha256Hash(password, salt);

            using (_connection)
            {
                const string query =
                    "INSERT INTO [User] (Name, JoinDate, Reputation, Email, IsAdmin, Salt, HashedPassword) VALUES (@name, @joindate, @reputation, @email, @isAdmin, @salt, @hashedPassword)";
                using (var command = new SqlCommand(query, _connection))
                {
                    var dateOnly = user.JoinDate.Date;
                    var sqlFormattedDate = dateOnly.ToString("d");

                    command.Parameters.AddWithValue("name", user.Name);
                    command.Parameters.AddWithValue("joindate", sqlFormattedDate);
                    command.Parameters.AddWithValue("reputation", user.Reputation);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("isAdmin", Convert.ToInt32(user.IsAdmin));
                    command.Parameters.AddWithValue("salt", salt);
                    command.Parameters.AddWithValue("hashedPassword", hashedPassword);

                    _connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        if (e.Number == 2627)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public bool Remove(string email)
        {
            _connection = new SqlConnection(_sqlCon);
            using (_connection)
            {
                const string query = "DELETE FROM [User] WHERE Email = @email";
                using (var command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("email", email);

                    _connection.Open();
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
            _connection = new SqlConnection(_sqlCon);
            using (_connection)
            {
                const string query =
                    "UPDATE [User] SET Name = @name, JoinDate = @joindate, Reputation = @reputation, Email = @email, IsAdmin = @isAdmin WHERE UserID = @id";
                using (var command = new SqlCommand(query, _connection))
                {
                    var dateOnly = user.JoinDate.Date;
                    var sqlFormattedDate = dateOnly.ToString("d");

                    command.Parameters.AddWithValue("name", user.Name);
                    command.Parameters.AddWithValue("joindate", sqlFormattedDate);
                    command.Parameters.AddWithValue("reputation", user.Reputation);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("isAdmin", Convert.ToInt32(user.IsAdmin));
                    command.Parameters.AddWithValue("id", user.ID);

                    _connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private static User CreateUserFromReader(SqlDataReader reader)
        {
            return new User(
                Convert.ToInt32(reader["UserID"]),
                Convert.ToString(reader["Name"]),
                Convert.ToDateTime(reader["JoinDate"]),
                Convert.ToInt32(reader["Reputation"]),
                Convert.ToString(reader["Email"]),
                Convert.ToBoolean(reader["IsAdmin"]));
        }

        public string CreateSalt()
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[16];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateSha256Hash(string password, string salt)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            var sha256HashedString = 
                new System.Security.Cryptography.SHA256Managed();
            var hash = sha256HashedString.ComputeHash(bytes);

            return System.Text.Encoding.UTF8.GetString(hash);
        }
    }
}
