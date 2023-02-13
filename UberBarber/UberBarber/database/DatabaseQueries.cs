using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;
using UberBarber.User;
using static UberBarber.User.CurrentUser;
using UberBarber.Appointments;

namespace UberBarber.database
{
    class DatabaseQueries : DbConnection
    {
        public bool Logging(string user_name, string user_password)
        {
            /// <summary> This function opens connection to server and databse and executes logging query. </summary>>
            Open_connection();
            MySqlCommand query =
                new($"call serwer165956_projektstudia.login_pswd_is_worker('{user_name}', '{user_password}');",
                    _connection);
            string message = "failed";
            bool is_worker = false;
            try
            {
                _reader = query.ExecuteReader();

                while (_reader.Read())
                {
                    message = (string)_reader[0];
                }

                if (message == "failed")
                {
                    return false;
                }
                else if (message == "1")
                {
                    is_worker = true;
                }

                CurrentUser.Set_username_permissions(user_name, is_worker);
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                Close_connection();
            }
        }

            public string Add_user(string username, string password, string confirm_password, string email, bool is_worker)
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
                    MySqlCommand query = new($"INSERT INTO `serwer165956_projektstudia`.`user` (`username`, `password`, `email`, `is_worker`) VALUES ('{username}', md5('{password}'), '{email}', '{Convert.ToInt32(is_worker)}');", _connection);
                    try
                    {
                        _reader = query.ExecuteReader();
                        message = "Done";
                    }
                    catch (MySqlException e)
                    {
                        MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        Close_connection();
                    }

                    return message;
                }
        }

        public string User_validation(string username, string password, string confirm_password, string email)
            // This method checks if passwords match, uses procedure to check if email or username are taken.
            // Returns proper message when the validation is correct or not.
        {
            string message = "Something went wrong :(";
            string usernamePattern = "^[a-zA-Z0-9.]{6,}$";
            string passwordPattern = "^[\\w\\d]{6,}$";
            string emailPattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

            // password validation
            if (password != confirm_password)
            {
                message = "Passwords don't match!";
                return message;
            }
            // username validation
            else if (!Regex.IsMatch(username, usernamePattern))
            {
                message = "Username can only contain letters, numbers, dots and have at least 6 characters";
                return message;
            }
            //password validation
            else if (!Regex.IsMatch(password, passwordPattern))
            {
                message = "Password must contain at least 6 characters";
                return message;
            }
            // email validation
            else if (!Regex.IsMatch(email, emailPattern))
            {
                message = "Email address is invalid";
                return message;
            }

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
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Close_connection();
            }

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
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Close_connection();
            }

            return users;
        }

        public void Remove_user(string username)
            // This method removes selected user.
        {
            Open_connection();
            try
            {

                MySqlCommand query = new($"call serwer165956_projektstudia.remove_user('{username}');", _connection);
                _reader = query.ExecuteReader();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Close_connection();
            }
        }

        public string Edit_user(string password, string confirm_password, string email, int user_id)
        {
            // This method edits password and email of selected user.
            string message = "Something went wrong :(";
            if (User_validation("edituser", password, confirm_password, email) != "valid")
            {
                message = User_validation("edituser", password, confirm_password, email);
                return message;
            }
            else
            {
                Open_connection();
                MySqlCommand query =
                    new(
                        $"UPDATE `serwer165956_projektstudia`.`user` SET `password` = md5('{password}'), `is_worker` = '1', `email` = '{email}' WHERE (`user_id` = '{user_id}');",
                        _connection);
                try
                {
                    _reader = query.ExecuteReader();
                    message = "Done";
                }
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    Close_connection();
                }

                return message;
            }

        }

        public List<Appointments.Appointments> Get_Appointments_for_current_user()
        {
            List<Appointments.Appointments> appointments = new();
            Open_connection();
            try
            {
                MySqlCommand query = new($"CALL serwer165956_projektstudia.show_appointments_for_user({CurrentUser.Get_user_id()});", _connection);
                _reader = query.ExecuteReader();

                //Add records to list
                while (_reader.Read())
                {
                    appointments.Add(new Appointments.Appointments(_reader));
                }

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Close_connection();
            }

            return appointments;
        }

        public int Get_current_user_id(string user_name)
        {
            //This function opens connection to server and gets user_id from given username
            Open_connection();
            MySqlCommand query =
                new($"SELECT user_id from serwer165956_projektstudia.user where username = '{user_name}';",
                    _connection);
            int id = 0;
            try
            {
                _reader = query.ExecuteReader();

                while (_reader.Read())
                {
                    id = _reader.GetInt32(0);
                }
                CurrentUser.Set_user_id(id);
                return id;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
                return id;
            }
            finally
            {
                Close_connection();
            }
        }

        public void Remove_appointment()
        {
            //TODO
        }

        public void Add_appointments(string barber, string service, DateTime date)
        {
            //This method creates a new record of appointment for current user in database.

            // add appointment to database
            List<Appointments.Appointments> appointments = new();
            Open_connection();
            try
            {
                MySqlCommand query =
                    new($"INSERT INTO `serwer165956_projektstudia`.`appointments` (`barber_id`, `service_id`, `start_time`) VALUES ('{barber}', '{service}', '{date}');", _connection);
                _reader = query.ExecuteReader();

                while (_reader.Read())
                {
                    appointments.Add(new Appointments.Appointments(_reader));
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Close_connection();
            }

        }

        public void Add_customer(string name, string surrname, string phone_number, DateTime date, int service_id, int barber_id, string description)
        {
            //This method creates a new records of customer.

            //add customer to database
            List<Customer.Customer> customers = new();
            Open_connection();
            try
            {
                MySqlCommand query =
                    new($"INSERT INTO `serwer165956_projektstudia`.`customer` (`name`, `surrname`, `phone_number`, `date`, `service_id`, `barber_id`, `description`, `user_id`) VALUES ('{name}', '{surrname}', '{phone_number}', '{date}', '{service_id}', '{barber_id}', '{description}', '{CurrentUser.User_id}');", _connection);
                _reader = query.ExecuteReader();

                while (_reader.Read())
                {
                    customers.Add(new Customer.Customer(_reader));
                }

                MessageBox.Show("Customer registered");
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Close_connection();
            }
        }
    }
}
