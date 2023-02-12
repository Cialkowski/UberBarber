using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UberBarber.User;

namespace UberBarber.database.ServiceQueries
{
    class ServiceQueries : DbConnection
    {
        public void Remove_service(int serviceId, int barberId)
        // This method removes selected service for specific barber.
        {
            Open_connection();
            try
            {
                MySqlCommand query = new($"DELETE FROM serwer165956_projektstudia.service WHERE (barber_id = {barberId} and service_id = {serviceId});", _connection);
                _reader = query.ExecuteReader();
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
        }
        public string Add_service(string name, decimal time, decimal price, int barberId, string description)
        // This method add service
        {
            // add service to database
            string message = "Something went wrong :(";
            Open_connection();
            MySqlCommand query = new($"INSERT INTO serwer165956_projektstudia.service(name,time,price,barber_id,description) VALUES('{name}', {time}, {price}, {barberId}, '{description}');", _connection);
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
        public string Edit_service(string name, decimal time, decimal price, string description)
        // This method edit service
        {
            // add service to database
            string message = "Something went wrong :(";
            Open_connection();
            MySqlCommand query = new($"UPDATE serwer165956_projektstudia.service SET name = '{name}',time = {time},price = {price},description = '{description}');", _connection);
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
