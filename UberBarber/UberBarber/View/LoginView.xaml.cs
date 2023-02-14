
using System;
using System.Windows;
using System.Windows.Input;
using UberBarber.database;

namespace UberBarber.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //This function allows you to drag the window to any place.

            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            // This method minimizes the window.

            WindowState = WindowState.Minimized;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // This method closes the whole application.

            Application.Current.Shutdown();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // TODO: correct doctring
            //open new window if given credentials are correct
            DatabaseQueries query = new();

            try
            {
                if (!query.Logging(txtUser.Text, txtPassword.Password))
                {
                    label_info.Content = "Wrong credentials!";
                }
                else
                {
                    query.Set_current_user_id(txtUser.Text);
                    new MainWindow().Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnUsernameOrPasswordChanged(object sender, RoutedEventArgs e)
        {
            enableOrDisableLoginButton();
        }
        private void enableOrDisableLoginButton()
        {
            // This method enable or disable login button.
            // if txtPassword is empty and txtUser is empy then the login button is disabled, otherwise the login button is enabled

            if (string.IsNullOrWhiteSpace(txtPassword.Password) || string.IsNullOrWhiteSpace(txtUser.Text))
            {
                btnLogin.IsEnabled = false;
            }
            else
                btnLogin.IsEnabled = true;
        }
        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            // TODO: correct doctring

            new UserRegistration().Show();
        }
    }
}