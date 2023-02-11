using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using UberBarber.User;
using static UberBarber.User.User;

namespace UberBarber.database
{
    class DatabaseQueries : DbConnection
    {
        

        public bool Logging(string user_name, string user_password)
        {
            /// <summary> This function opens connection to server and databse and executes logging query. </summary>>
            Open_connection();
            MySqlCommand query = new($"SELECT username, password FROM serwer165956_projektstudia.user WHERE username = '{user_name}' AND password = md5('{user_password}');", _connection);
            try
            {
                _reader = query.ExecuteReader();

                if (!_reader.Read())
                {
                    return false;
                }
                return true;
            }
            catch (MySqlException e) 
            {
                MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally { Close_connection(); }
        }

        public string Add_user(string username, string password, string confirm_password, string email)
        // This method use validation function, after passing it - adds Edit_user to database.
        {
            string message = "Something went wrong :(";
            if (User_validation(username, password, confirm_password, email) != "valid")
            {
                message = User_validation(username, password, confirm_password, email);
                return message;
            }
            else
            {
                // add user to database
                Open_connection();
                MySqlCommand query = new($"INSERT INTO `serwer165956_projektstudia`.`user` (`username`, `password`, `email`) VALUES ('{username}', md5('{password}'), '{email}');", _connection);
                try
                {
                    _reader = query.ExecuteReader();
                    message = "Done";
                }
                catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
                finally { Close_connection(); }
                return message;
            }
        }

        public string User_validation(string username, string password, string confirm_password, string email)
            // This method checks if passwords match, uses procedure to check if email or username are taken.
            // Returns proper message when the validation is correct or not.
        {
            string message = "Something went wrong :(";
            // password validation
            if (password != confirm_password)
            {
                message = "Passwords don't match!";
                return message;
            }

            // username + email validation
            Open_connection();
            MySqlCommand query = new($"CALL user_validation('{username}', '{email}')", _connection);
            try
            {
                _reader = query.ExecuteReader();
                while (_reader.Read())
                {
                    // assign first element in first column
                    message = (string)_reader[0];
                }
                if (message != "valid")
                {
                    return message;
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally {Close_connection(); }
            return message;
        }

        public List<User.User> Get_users()
            // This method gets all data from users table add returns it as a list.
        {
            List<User.User> users = new();
            Open_connection();
            try
            {
                MySqlCommand query = new("SELECT * FROM serwer165956_projektstudia.user;", _connection);
                _reader = query.ExecuteReader();

                //Add records to list
                while (_reader.Read())
                {
                    users.Add(new User.User(_reader));
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
            return users;
        }

        public void Remove_user(string username)
        {
            Open_connection();
            try
            {
                MySqlCommand query = new($"DELETE FROM serwer165956_projektstudia.user WHERE (username = '{username}');", _connection);
                _reader = query.ExecuteReader();
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
        }

        public string Edit_user(string password, string confirm_password, string email, int user_id)
        {
            string message = "Something went wrong :(";
            if (User_validation("", password, confirm_password, email) != "valid")
            {
                message = User_validation("", password, confirm_password, email);
                return message;
            }
            else
            {
                Open_connection();
                MySqlCommand query = new($"UPDATE `serwer165956_projektstudia`.`user` SET `password` = md5('{password}'), `is_worker` = '1', `email` = '{email}' WHERE (`user_id` = '{user_id}');", _connection);
                try
                {
                    _reader = query.ExecuteReader();
                    message = "Done";
                }
                catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
                finally { Close_connection(); }
                return message;
            }

        }
    }
}
