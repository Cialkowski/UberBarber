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
using System.Windows.Shapes;
using UberBarber.database;
using static UberBarber.database.DatabaseQueries;

namespace UberBarber
{
    /// <summary>
    /// Logika interakcji dla klasy UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        public UserRegistration()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //This function allows you to drag the window to any place

            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Button_confirm_Click(object sender, RoutedEventArgs e)
        {
            // This method collect content from user registration forms and creates new User after correct validation.
            // Shows a text block with error information
            // Closes user registration window after succesful operation.

            string username = text_username.Text;
            string password = pswd_box.Password;
            string confirm_password = pswd_box_confirm.Password;
            string email = text_email.Text;

            DatabaseQueries query = new DatabaseQueries();
            string message = query.Add_user(username, password, confirm_password, email);
            
            if ( message != "Done" )
            // Show text block if there is an error
            {
                TextBlockInfoUserReg.Text = message;
            }
            else
            {
                MessageBox.Show($"User: {username}\nHas been added", message, MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
        }
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            // This method minimizes the window.

            WindowState = WindowState.Minimized;
        }
        private void OnPasswordBoxAndConfirmPasswordBoxChanged(object sender, EventArgs e)
        {
            enableOrDisableConfirmRegistrationButton();
        }
        private void enableOrDisableConfirmRegistrationButton()
        {
            // This method enable or disable confirm registration button.
            // If the passwords do not match, then the button is disabled, otherwise the button is enabled 

            if (string.Equals(pswd_box.Password, pswd_box_confirm.Password,StringComparison.OrdinalIgnoreCase))
            {
                Confirm.IsEnabled = true;
            }
            else
                Confirm.IsEnabled = false;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // This method closes the window.

            Close();
        }
    }
}
