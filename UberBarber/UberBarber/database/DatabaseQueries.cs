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
        private DbConnection database = new();
        private MySqlDataReader _reader;

        internal bool logging(string user_name, string user_password)
        {
            /// <summary> This function opens connection to server and databse and executes logging query. </summary>>
            /// <returns> True if authentication and query execute is successful, otherwise False. </returns>

            try
            {
                database._connection.Open();
            }
            catch (MySqlException ex)
            {
      
                MessageBox.Show("Cannot connect to server. Contact administrator");

            }

            MySqlCommand query = new MySqlCommand("SELECT username, password FROM serwer165956_projektstudia.user WHERE username = '"+user_name+"' AND password = md5('"+user_password+"');", database._connection);
            try
            {
                _reader = query.ExecuteReader();

                if (_reader.Read())
                {
                    database._connection.Close();
                    return true;
                }
                MessageBox.Show("WRONG CREDENTIALS!");
                database._connection.Close();
                return false;

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
                database._connection.Close();
                return false;
            }

        }
    }
}
