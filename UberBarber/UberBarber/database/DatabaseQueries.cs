using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace UberBarber.database
{
    class DatabaseQueries : DbConnection
    {
        

        public void Logging(string user_name, string user_password)
        {
            /// <summary> This function opens connection to server and databse and executes logging query. </summary>>

            Open_connection();
            MySqlCommand query = new MySqlCommand($"SELECT username, password FROM serwer165956_projektstudia.user WHERE username = '{user_name}' AND password = md5('{user_password}');", _connection);
            Read_simple(query, "WRONG CREDENTIALS!");

        }

        public void Add_user(string username, string password, string email)
        {
            Open_connection();

            MySqlCommand query = new MySqlCommand($"INSERT INTO `serwer165956_projektstudia`.`user` (`username`, `password`, `is_worker`) VALUES ('{username}', md5('{password}'), '0');", _connection);

           Read_simple(query,"Done!" ,"Something went wrong :/");

        }
    }
}
