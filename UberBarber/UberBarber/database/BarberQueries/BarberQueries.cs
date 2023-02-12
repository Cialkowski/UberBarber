using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;
using UberBarber.User;

namespace UberBarber.database.BarberQueries
{
    class BarberQueries : DbConnection
    {
        public string Barber_Validation(string phoneNumber, int age, int? userId = null)
        // This method checks if age is within the range of 16 to 100 years and if the phone number has 9 digits and if the user is not already assigned to another barber.
        // Returns proper message when the validation is correct or not.
        {
            string message = "Something went wrong :(";
            string phoneNumberPattern = "^\\d{9}$";
            // age validation
            if (age > 100 || age < 16)
            {
                message = "Age must be between 16 and 100";
                return message;
            }
            // phone number validation
            else if (!Regex.IsMatch(phoneNumber, phoneNumberPattern))
            {
                message = "Phone number must be 9 digits";
                return message;
            }
            if (userId == null)
            {
                message = "valid";
                return message;
            }

            Open_connection();
            MySqlCommand query = new($"CALL barber_validation({userId})", _connection);
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
        // This method use validation function, after passing it - adds barber to database and set is_worker to 1.
        {
            string message = "Something went wrong :(";
            if (Barber_Validation(phoneNumber, age, userId) != "valid")
            {
                message = Barber_Validation(phoneNumber, age, userId);
                return message;
            }
            else
            {
                // add barber to database
                Open_connection();
                MySqlCommand query = new($"CALL serwer165956_projektstudia.add_barber_and_update_user({userId}, '{title}', '{name}', {phoneNumber}, {age});");
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
        public List<Barber.Barber> Get_barbers()
        // This method gets all data from barbers table add returns it as a list.
        {
            List<Barber.Barber> barbers = new();
            Open_connection();
            try
            {
                MySqlCommand query = new("SELECT * FROM serwer165956_projektstudia.barber;", _connection);
                _reader = query.ExecuteReader();

                // Add records to list
                while (_reader.Read())
                {
                    barbers.Add(new Barber.Barber(_reader));
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
            return barbers;
        }
        public void Remove_barber(int barberId)
        // This method removes selected barber.
        {
            Open_connection();
            try
            {
                MySqlCommand query = new($"DELETE FROM serwer165956_projektstudia.user WHERE (barber_id = {barberId});", _connection);
                _reader = query.ExecuteReader();
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
        }
        public string Edit_barber(int barberId, string name, string title, string phoneNumber, int age)
        {
            // This method edits name, title, phone number and age of selected barber.
            string message = "Something went wrong :(";
            if (Barber_Validation(phoneNumber, age) != "valid")
            {
                message = Barber_Validation(phoneNumber, age);
                return message;
            }
            else
            {
                // update barber to database
                Open_connection();
                MySqlCommand query = new($"UPDATE `serwer165956_projektstudia`.`barber` SET name = '{name}', title = '{title}', phone_number = '{phoneNumber}', age = {age} where barber_id = {barberId};", _connection);
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
