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
            MySqlCommand query = new($"SELECT username, password FROM serwer165956_projektstudia.user WHERE username = '{user_name}' AND password = md5('{user_password}');", _connection);
            try
            {
                _reader = query.ExecuteReader();

                if (!_reader.Read())
                    MessageBox.Show("WRONG CREDENTIALS!");

            }
            catch (MySqlException e) { MessageBox.Show(e.Message); }
            finally { Close_connection(); }
        }

        public void Add_user(string username, string password, string confirm_password, string email)
        {
            if (User_validation(username, password, confirm_password, email))
            {
                Open_connection();
                MySqlCommand query = new($"INSERT INTO `serwer165956_projektstudia`.`user` (`username`, `password`, `is_worker`) VALUES ('{username}', md5('{password}'), 'lol');", _connection);
                try
                {
                    _reader = query.ExecuteReader();
                    MessageBox.Show("Done!");
                }
                catch (MySqlException e) { MessageBox.Show(e.Message);}
                finally { Close_connection(); }
            }
        }

        public bool User_validation(string username, string password, string confirm_password, string email)
        // TODO: email validation
        {
            // password validation
            if (password != confirm_password)
            {
                MessageBox.Show("Passwords does't match!");
                return false;
            }
            Open_connection();
            MySqlCommand query = new($"SELECT username from serwer165956_projektstudia.user WHERE username = '{username}';", _connection);
            // username validation
            try
            {
                _reader = query.ExecuteReader();

                if (_reader.Read())
                {
                    MessageBox.Show("Username already exists!");
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            { Close_connection(); }

            return true;
        }
    }
}
