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
using UberBarber.View;
using static UberBarber.database.DatabaseQueries;

namespace UberBarber
{
    /// <summary>
    /// Logika interakcji dla klasy UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        public bool Is_constructor_edit = false;
        public User.User Selected_user { get; set; }
        public UserRegistration()
        {
            InitializeComponent();
        }
        public UserRegistration(User.User user)
        {
            InitializeComponent();
            Selected_user = user;
            text_username.Text = Selected_user.Username;
            text_username.IsEnabled = false;

            text_email.Text = Selected_user.Email;
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
            // Shows a label with error information
            // Closes user registration window after succesful operation.

            string username = text_username.Text;
            string password = pswd_box.Password;
            string confirm_password = pswd_box_confirm.Password;
            string email = text_email.Text;
            string message;

            DatabaseQueries query = new();
            // Check if edit conctructor was used
            if (!Is_constructor_edit)
            {
                // Default constructor
                message = query.Add_user(username, password, confirm_password, email);

                if (message != "Done")
                // Show label if there is an error
                {
                    LabelInfoUserReg.Content = message;
                }
                else
                {
                    MessageBox.Show($"User: {username}\nHas been added", message, MessageBoxButton.OK, MessageBoxImage.Information);

                    Close();
                }
            }
            else
            {
                // Edit constructor
                message = query.Edit_user(password, confirm_password, email, Selected_user.User_id);

                if (message != "Done")
                {
                    LabelInfoUserReg.Content = message;
                }
                else
                {
                    MessageBox.Show($"User: {username}\nHas been edited", "Done", MessageBoxButton.OK, MessageBoxImage.Information);

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
