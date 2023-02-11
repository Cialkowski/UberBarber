using System;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using UberBarber.database;

namespace UberBarber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Set contents to non visible.
            Draft1Content.Visibility = Visibility.Collapsed;
            Draft2Content.Visibility = Visibility.Collapsed;
            UserContent.Visibility = Visibility.Collapsed;
        }

        //full screen funcitonality and drag move
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState== WindowState.Normal)
                this.WindowState= WindowState.Maximized;
            else this.WindowState=WindowState.Normal;
        }

        private void buttonUser_Click(object sender, RoutedEventArgs e)
        {
            // This method shows DataGrid with user records.
            UserContent.Visibility = Visibility.Visible;
            Refresh_Dgv_User();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            // This method edits selected user.
            User.User user = (User.User)dgvUser.SelectedItem;
            new UserRegistration(user).Show();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            // This method removes selected user after confirmation.
            User.User user = (User.User)dgvUser.SelectedItem;
            if (MessageBox.Show("Are you sure that you want delete this office?",
                                "Delete Office",
                                MessageBoxButton.OKCancel,
                                MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                DatabaseQueries query = new();
                query.Remove_user(user.Username);
            }
            Refresh_Dgv_User();
        }

        public void Refresh_Dgv_User()
        {
            // This method refreshes DataGrid.
            DatabaseQueries query = new();
            dgvUser.ItemsSource = query.Get_users();
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            // This method shows UserRegistration window.
            new UserRegistration().Show();
        }
    }
}
