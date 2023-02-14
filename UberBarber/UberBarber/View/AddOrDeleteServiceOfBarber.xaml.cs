using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UberBarber.database;
using UberBarber.database.ServiceQueries;
using UberBarber.User;

namespace UberBarber.View
{
    /// <summary>
    /// Logika interakcji dla klasy EditServiceToBarber.xaml
    /// </summary>
    public partial class EditServiceToBarber : Window
    {
        public int BarberId;
        public bool Is_add;
        public EditServiceToBarber(bool is_add,int barberId)
        {
            BarberId = barberId;
            Is_add = is_add;
            List<Service.Service> services = new List<Service.Service>();
            InitializeComponent();
            if (is_add)
            {
                services = new ServiceQueries().Get_all_services_unassigned_to_barber(barberId);
                Confirm.Content = "Add";
            }
            else
            {
                services = new ServiceQueries().Get_all_services_assigned_to_barber(barberId);
                Confirm.Content = "Delete";
            }
            ComboBoxServices.ItemsSource = services;
        }
        private void Button_confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Is_add)
            {
                Service.Service service = (Service.Service)ComboBoxServices.SelectedItem;
                ServiceQueries query = new ServiceQueries();
                string message = query.Add_Service_To_Barber(BarberId, service.ServiceId);
                if (message != "Done")
                // Show label if there is an error
                {
                    MessageBox.Show($"Service was not added.", message, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else 
            {
                Service.Service service = (Service.Service)ComboBoxServices.SelectedItem;
                ServiceQueries query = new ServiceQueries();
                string message = query.Delete_Service_Of_The_Barber(BarberId, service.ServiceId);
                if (message != "Done")
                // Show label if there is an error
                {
                    MessageBox.Show($"Service was not deleted.", message, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            Close();
        }
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            // This method minimizes the window.

            WindowState = WindowState.Minimized;
        }
       
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // This method closes the window.

            Close();
        }
    }
}
