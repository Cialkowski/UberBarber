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
        //This functionality allows you to drag the window to any place
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void button_confirm_Click(object sender, RoutedEventArgs e)
        {
            string username = text_username.Text;
            string password = pswd_box.Password;
            string confirm_password = pswd_box_confirm.Password;
            string email = text_email.Text;

            DatabaseQueries query = new DatabaseQueries();
            query.Add_user(username, password, confirm_password, email);
            
        }
        //Minimize the window
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        //Close this window
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
