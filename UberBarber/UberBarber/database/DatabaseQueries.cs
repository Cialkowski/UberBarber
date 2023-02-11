using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using MySql.Data.MySqlClient;

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
        // This method use validation function, after passing it - adds User to database.
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
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        public string Barber_Validation(string phoneNumber, int age, int userId)
        // This method checks if passwords match, uses procedure to check if email or username are taken.
        // Returns proper message when the validation is correct or not.
        {
            string message = "Something went wrong :(";
            string phoneNumberPattern = "^\\d{9}$";
            //age validation
            if (age > 100 || age < 16)
            {
                message = "Age must be between 16 and 100";
                return message;
            }
            //phone number validation
            else if (!Regex.IsMatch(phoneNumber, phoneNumberPattern))
            {
                message = "Phone number must be 9 digits";
                return message;
            }
            Open_connection();
            MySqlCommand query = new($"CALL barber_validation({userId}, '{phoneNumber}')", _connection);
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
            finally { Close_connection(); }
            return message;
        }
        public string Add_Barber(string name, string title, string phoneNumber, int age, int userId)
        // This method use validation function, after passing it - adds barber to database.
        {
            string message = "Something went wrong :(";
            if (BarberValidation(phoneNumber, age, userId) != "valid")
            {
                message = BarberValidation(phoneNumber, age, userId);
                return message;
            }
            else
            {
                // add barber to database
                Open_connection();
                MySqlCommand query = new($"INSERT INTO `serwer165956_projektstudia`.`barber` (`name`, `title`, `phone_number`,`age`,`user_id`) VALUES ('{name}', '{title}', '{phoneNumber}', {age}, {userId});", _connection);
                try
                {
                    _reader = query.ExecuteReader();
                    message = "Done";
                }
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally { Close_connection(); }
                return message;
            }
        }


    }
}
