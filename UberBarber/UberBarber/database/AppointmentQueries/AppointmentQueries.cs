using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UberBarber.User;

namespace UberBarber.database.AppointmentQueries
{
    class AppointmentQueries : DbConnection
    {
        public List<Appointments.Appointments> Get_Appointments_for_current_user()
        {
            List<Appointments.Appointments> appointments = new();
            Open_connection();
            try
            {
                MySqlCommand query =
                    new($"CALL serwer165956_projektstudia.show_appointments_for_user({CurrentUser.Get_user_id()});",
                        _connection);
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
        public void Remove_appointment(int user_id)
        {
            Open_connection();
            try
            {

                MySqlCommand query = new($"DELETE FROM `serwer165956_projektstudia`.`customer` WHERE (`user_id` = '{user_id}');", _connection);
                _reader = query.ExecuteReader();
                MessageBox.Show("Done");
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

        public void Add_appointments(string barber, string service, DateTime date)
        {
            //This method creates a new record of appointment for current user in database.

            // add appointment to database
            List<Appointments.Appointments> appointments = new();
            MySqlCommand query =
                new($"INSERT INTO `serwer165956_projektstudia`.`appointments` (`customer_id`,`barber_id`, `service_id`, `start_time`) VALUES ('{Get_lat_customer_id()}', '{barber}', '{service}', '{date}');", _connection);
            Open_connection();
            try
            {
                _reader = query.ExecuteReader();

                while (_reader.Read())
                {
                    appointments.Add(new Appointments.Appointments(_reader));
                }
                MessageBox.Show("appointment registered");
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

        public void Add_customer(string name, string surrname, string phone_number, DateTime date, int service_id,
            int barber_id, string description)
        {
            //This method creates a new records of customer.

            //add customer to database
            List<Customer.Customer> customers = new();
            Open_connection();
            try
            {
                MySqlCommand query =
                    new(
                        $"INSERT INTO `serwer165956_projektstudia`.`customer` (`name`, `surrname`, `phone_number`, `date`, `service_id`, `barber_id`, `description`, `user_id`) VALUES ('{name}', '{surrname}', '{phone_number}', '{date}', '{service_id}', '{barber_id}', '{description}', '{CurrentUser.User_id}');",
                        _connection);
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

        public int Get_lat_customer_id()
        {
            int id = 0;
            Open_connection();
            try
            {
                MySqlCommand query =
                    new("SELECT customer_id FROM serwer165956_projektstudia.customer ORDER BY customer_id DESC LIMIT 1;", _connection);
                _reader = query.ExecuteReader();

                while (_reader.Read())
                {
                    id = (int)_reader[0];
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

            return id;
        }
    }
}
