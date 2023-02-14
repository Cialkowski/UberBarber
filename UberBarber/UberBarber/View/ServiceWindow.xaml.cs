using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UberBarber.database.BarberQueries;
using UberBarber.database;
using UberBarber.database.ServiceQueries;
using System.Globalization;

namespace UberBarber.View
{
    /// <summary>
    /// Logika interakcji dla klasy ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public Service.Service Service;
        public bool Is_constructor_edit = false;

        public ServiceWindow()
        {
            InitializeComponent(); 
        }
        public ServiceWindow(Service.Service service)
        {
            InitializeComponent();
            this.Service = service;
            text_name.Text = service.Name;
            text_time.Text = service.Time.ToString();
            text_price.Text = service.Price.ToString();
            text_description.Text = service.Description;
            Is_constructor_edit = true;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //This function allows you to drag the window to any place

            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Button_confirm_Click(object sender, RoutedEventArgs e)
        {
            // This method collect content from user registration forms and edit or creates new User after correct validation.
            // Shows a text block with error information
            // Sends email and closes user registration window after succesful operation.

            string name = text_name.Text;
            decimal time = decimal.Parse(text_time.Text, CultureInfo.InvariantCulture);
            decimal price = decimal.Parse(text_price.Text, CultureInfo.InvariantCulture);
            string description = text_description.Text;
            string message;
            ServiceQueries query = new ServiceQueries();
            // Check if edit conctructor was used
            if (!Is_constructor_edit)
            {
                // Default constructor
                message = query.Add_service(name,time,price,description);

                if (message != "Done")
                // Show label if there is an error
                {
                    TextBlockInfoUserReg.Text = message;
                }
                else
                {
                    MessageBox.Show($"Service: {name}\nHas been added", message, MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            else
            {
                // Edit constructor
                message = query.Edit_service(Service.ServiceId,name, time, price, description);

                if (message != "Done")
                {
                    TextBlockInfoUserReg.Text = message;
                }
                else
                {
                    MessageBox.Show($"Service: {name}\nHas been edited", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
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
