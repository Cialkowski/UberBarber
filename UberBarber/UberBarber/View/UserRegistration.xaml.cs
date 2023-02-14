using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UberBarber.database;
using UberBarber.User;

namespace UberBarber
{
    /// <summary>
    /// Logika interakcji dla klasy UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        bool user_permissions = CurrentUser.Get_is_worker();
        public bool Is_constructor_edit = false;
        public User.User Selected_user { get; set; }
        public UserRegistration()
        // default constructor
        {
            InitializeComponent();
            if (user_permissions)
            // if user has external permissions Checkbox is visible
            {
                CheckBoxPremissions.Visibility = Visibility.Visible;
                CheckBoxPremissions.IsEnabled = true;
            }
        }
        public UserRegistration(User.User user)
        // constructor for user editing
        {
            InitializeComponent();
            Selected_user = user;
            text_username.Text = Selected_user.Username;
            text_username.IsEnabled = false;
            if (Selected_user.Is_worker)
            {
                CheckBoxPremissions.IsChecked = true;
            }

            text_email.Text = Selected_user.Email;
            Is_constructor_edit = true;

            if (user_permissions)
            // if user has external permissions Checkbox is visible
            {
                CheckBoxPremissions.Visibility = Visibility.Visible;
                CheckBoxPremissions.IsEnabled = true;
            }
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

            string username = text_username.Text;
            string password = pswd_box.Password;
            string confirm_password = pswd_box_confirm.Password;
            string email = text_email.Text;
            bool is_worker = false;
            string permission = "customer";
            string message;

            if (CheckBoxPremissions.IsChecked == true)
            {
                is_worker = true;
            }
            if (is_worker == true)
            {
                permission = "barber";
            }

            MailSender mailSender = new MailSender();
            DatabaseQueries query = new();
            // Check if edit conctructor was used
            if (!Is_constructor_edit)
            {
                // Default constructor
                message = query.Add_user(username, password, confirm_password, email, is_worker);

                if (message != "Done")
                // Show label if there is an error
                {
                    TextBlockInfoUserReg.Text = message;
                }
                else
                {
                    MessageBox.Show($"User: {username}\nHas been added", message, MessageBoxButton.OK, MessageBoxImage.Information);
                    mailSender.Send(MailSender.Action.Add, email, username, password, permission);
                    Close();
                }
            }
            else
            {
                // Edit constructor
                message = query.Edit_user(password, confirm_password, email, Selected_user.User_id, is_worker);

                if (message != "Done")
                {
                    TextBlockInfoUserReg.Text = message;
                }
                else
                {
                    MessageBox.Show($"User: {username}\nHas been edited", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                    mailSender.Send(MailSender.Action.Edit, email, username, password, permission);
                    Close();
                }
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

            if (string.Equals(pswd_box.Password, pswd_box_confirm.Password, StringComparison.OrdinalIgnoreCase))
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
