using MySql.Data.MySqlClient;

namespace UberBarber.User
{
    public class User
    {
        public User(MySqlDataReader reader)
        {
            User_id = (int)reader["user_id"];
            Username = (string)reader["username"];
            Password = (string)reader["password"];
            Is_worker = (bool)reader["is_worker"];
            Email = (string)reader["email"];
        }
        public int User_id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Is_worker { get; set; }
        public string Email { get; set;}
        public override string ToString()
        {
            return Username;
        }
    }
}
