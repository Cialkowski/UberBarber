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
        private MySqlDataReader? _reader;

        internal bool Logging(string user_name, string user_password)
        {
            /// <summary> This function opens connection to server and databse and executes logging query. </summary>>
            /// <returns> True if authentication and query execute is successful, otherwise False. </returns>

            open_connection();
            MySqlCommand query = new MySqlCommand($"SELECT username, password FROM serwer165956_projektstudia.user WHERE username = '{user_name}' AND password = md5('{user_password}');", _connection);
            try
            {
                _reader = query.ExecuteReader();

                if (_reader.Read())
                {
                    _connection.Close();
                    return true;
                }
                MessageBox.Show("WRONG CREDENTIALS!");
                _connection.Close();
                return false;

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
                _connection.Close();
                return false;
            }

        }

        public void Add_user(string username, string password, string email)
        {
            open_connection();

            MySqlCommand query = new MySqlCommand($"INSERT INTO `serwer165956_projektstudia`.`user` (`username`, `password`, `is_worker`) VALUES ('{username}', md5('{password}'), '0');", _connection);

            try
            {
                _reader = query.ExecuteReader();
                if (!_reader.Read())
                    MessageBox.Show("dupa");
                _connection.Close();

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
                _connection.Close();
            }

        }
    }
}
