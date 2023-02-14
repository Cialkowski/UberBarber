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
        public string Add_service(string name, decimal time, decimal price, string description)
        // This method add service
        {
            // add service to database
            string message = "Something went wrong :(";
            Open_connection();
            MySqlCommand query = new($"INSERT INTO serwer165956_projektstudia.service(name,time,price,description) VALUES('{name}', {time}, {price}, '{description}');", _connection);
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
        public List<Service.Service> Get_all_services()
        // This method gets all services assigned to barber. Returns it as a list.
        {
            string getAllServicesForBarberQuery = "select * from serwer165956_projektstudia.service;"; 
                           
            List<Service.Service> services = new();
            Open_connection();
            try
            {
                MySqlCommand query = new(getAllServicesForBarberQuery, _connection);
                _reader = query.ExecuteReader();

                // Add records to list
                while (_reader.Read())
                {
                    services.Add(new Service.Service(_reader));
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
            return services;
        }
        public string Edit_service(int serviceId,string name, decimal time, decimal price, string description)
        // This method edit service
        {
            // add service to database
            string message = "Something went wrong :(";
            Open_connection();
            MySqlCommand query = new($"UPDATE serwer165956_projektstudia.service SET name = '{name}',time = {time},price = {price},description = '{description}' where service_id = {serviceId};", _connection);
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
        public List<Service.Service> Get_all_services_assigned_to_barber(int barberId)
        // This method gets all services assigned to barber. Returns it as a list.
        {
            string getAllServicesForBarberQuery = $"Select * from serwer165956_projektstudia.service s inner join serwer165956_projektstudia.serviceBarber sb on sb.service_id = s.service_id and sb.barber_id = {barberId};";

            List<Service.Service> services = new();
            Open_connection();
            try
            {
                MySqlCommand query = new(getAllServicesForBarberQuery, _connection);
                _reader = query.ExecuteReader();

                // Add records to list
                while (_reader.Read())
                {
                    services.Add(new Service.Service(_reader));
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
            return services;
        }
        public List<Service.Service> Get_all_services_unassigned_to_barber(int barberId)
        // This method gets all services assigned to barber. Returns it as a list.
        {
            string getAllServicesForBarberQuery = $"Select * from serwer165956_projektstudia.service where service_id not in (Select s.service_id from serwer165956_projektstudia.service s inner join serwer165956_projektstudia.serviceBarber sb on sb.service_id = s.service_id and sb.barber_id = {barberId} group by sb.service_id);";

            List<Service.Service> services = new();
            Open_connection();
            try
            {
                MySqlCommand query = new(getAllServicesForBarberQuery, _connection);
                _reader = query.ExecuteReader();

                // Add records to list
                while (_reader.Read())
                {
                    services.Add(new Service.Service(_reader));
                }
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
            return services;
        }
        public string Add_Service_To_Barber(int barberId, int serviceId)
        // This method add service to barber.
        {
            string message = "Something went wrong :(";
            Open_connection();
            MySqlCommand query = new($"INSERT INTO serwer165956_projektstudia.serviceBarber (barber_Id,service_id) VALUES ({barberId},{serviceId});", _connection);
            try
            {
                _reader = query.ExecuteReader();
                message = "Done";
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
            return message;
            
        }
        public string Delete_Service_Of_The_Barber(int barberId, int serviceId)
        // This method add service to barber.
        {
            string message = "Something went wrong :(";
            Open_connection();
            MySqlCommand query = new($"Delete from serwer165956_projektstudia.serviceBarber where barber_id = {barberId} and service_id = {serviceId};", _connection);
            try
            {
                _reader = query.ExecuteReader();
                message = "Done";
            }
            catch (MySqlException e) { MessageBox.Show(e.Message, "MySQL error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { Close_connection(); }
            return message;
        }
        public string Delete_Service(int serviceId)
        // This method add service to barber.
        {
            string message = "Something went wrong :(";
            Open_connection();
            MySqlCommand query = new($"CALL serwer165956_projektstudia.remove_service({serviceId})", _connection);
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
