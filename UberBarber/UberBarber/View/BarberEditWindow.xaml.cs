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
using UberBarber.Barber;
using UberBarber.database;
using UberBarber.database.BarberQueries;
using UberBarber.database.ServiceQueries;
using UberBarber.User;

namespace UberBarber.View
{
    /// <summary>
    /// Logika interakcji dla klasy BarberEditWindow.xaml
    /// </summary>
    public partial class BarberEditWindow : Window
    {
        public Barber.Barber Barber;
        public bool Is_constructor_edit = false;
        DatabaseQueries query = new DatabaseQueries();
        
        public BarberEditWindow()
        {
            InitializeComponent();
            List<User.User> users = query.Get_users();
            Combobox_User.ItemsSource = users.Where(x=>x.Is_worker == false);
        }
        public BarberEditWindow(Barber.Barber barber)
        {
            InitializeComponent();
            List<User.User> users = query.Get_users();
            Combobox_User.ItemsSource = users;
            this.Barber = barber;
            text_name.Text = barber.Name;
            textbox_title.Text = barber.Title;
            textbox_phone_number.Text = barber.PhoneNumber;
            textbox_Age.Text = barber.Age.ToString();
            Combobox_User.SelectedItem = users.Find(x=>x.Username==barber.Username);
            Combobox_User.IsEnabled = false;
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
            string title = textbox_title.Text;
            string phone_number = textbox_phone_number.Text;
            int age = int.Parse(textbox_Age.Text);
            string message;
            User.User user = (User.User)Combobox_User.SelectedItem;
            BarberQueries query = new BarberQueries();
            // Check if edit conctructor was used
            if (!Is_constructor_edit)
            {
                // Default constructor
                message = query.Add_Barber(name,title,phone_number,age,user.User_id);

                if (message != "Done")
                // Show label if there is an error
                {
                    TextBlockInfoUserReg.Text = message;
                }
                else
                {
                    MessageBox.Show($"Barber: {name}\nHas been added", message, MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            else
            {
                // Edit constructor
                message = query.Edit_barber(Barber.BarberId,name,title,phone_number,age);

                if (message != "Done")
                {
                    TextBlockInfoUserReg.Text = message;
                }
                else
                {
                    MessageBox.Show($"Barber: {name}\nHas been edited", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
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
