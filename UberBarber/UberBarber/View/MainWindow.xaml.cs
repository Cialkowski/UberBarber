using System;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using UberBarber.database;
using UberBarber.User;
using UberBarber.View;
using UberBarber.database.AppointmentQueries;
using UberBarber.database.BarberQueries;
using UberBarber.database.ServiceQueries;
using UberBarber.database.UserQueries;

namespace UberBarber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool user_permissions = CurrentUser.Get_is_worker();
        public MainWindow()
        {
            InitializeComponent();
            // Set contents to non visible.
            Draft1Content.Visibility = Visibility.Collapsed;
            AppointmentsContent.Visibility = Visibility.Collapsed;
            UserContent.Visibility = Visibility.Collapsed;
            BarberContent.Visibility = Visibility.Collapsed;
            ServiceContent.Visibility = Visibility.Collapsed;
            UsernameTextblock.Text = CurrentUser.Get_username();
            if (user_permissions)
            {
                buttonUser.IsEnabled= true;
            }
        }

        //full screen funcitonality and drag move
        [DllImport("user32.dll")]
        
        // WINDOW SETTINGS
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // This method closes application
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            // This method minimizes the window
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            // This method maximizes the window
            if (this.WindowState== WindowState.Normal)
                this.WindowState= WindowState.Maximized;
            else this.WindowState=WindowState.Normal;
        }

        // USER SECTION
        private void buttonUser_Click(object sender, RoutedEventArgs e)
        {
            // This method shows DataGrid with user records.
            BarberContent.Visibility = Visibility.Collapsed;
            ServiceContent.Visibility = Visibility.Collapsed;
            UserContent.Visibility = Visibility.Visible;
            AppointmentsContent.Visibility = Visibility.Collapsed;
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
            if (MessageBox.Show("Are you sure that you want delete this user?",
                                "Delete User",
                                MessageBoxButton.OKCancel,
                                MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                UserQueries query = new();
                query.Remove_user(user.Username);
            }
            Refresh_Dgv_User();
        }

        public void Refresh_Dgv_User()
        {
            // This method refreshes User DataGrid.
            UserQueries query = new(); query = new();
            dgvUser.ItemsSource = query.Get_users();
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            // This method shows UserRegistration window.
            new UserRegistration().Show();
        }

        // APPOINTMENT SECTION
        public void Refresh_Dgv_Appointments()
        {
            // This method refreshes Appointment DataGrid.
            AppointmentQueries query = new();
            dgvAppointments.ItemsSource = query.Get_Appointments_for_current_user();
        }


        private void buttonAppointments_Click(object sender, RoutedEventArgs e)
        {
            // This method show appointments content
            AppointmentsContent.Visibility = Visibility.Visible;
            UserContent.Visibility = Visibility.Collapsed;
            ServiceContent.Visibility = Visibility.Collapsed;
            BarberContent.Visibility = Visibility.Collapsed;
            Refresh_Dgv_Appointments();
        }

        private void ButtonAdd_Appointment_Click(object sender, RoutedEventArgs e)
        {
            // This method shows new appointment window.
            new AddAppointment().Show();
        }

        private void Button_remove_appointment(object sender, RoutedEventArgs e)
        {
            // This method removes selected appointment after confirmation.
            // Appointments.Appointments appointment = (Appointments.Appointments)dgvAppointments.SelectedItem;
            Appointments.Appointments appointments = (Appointments.Appointments)dgvAppointments.SelectedItem;
            if (MessageBox.Show("Are you sure that you want delete this appointment?",
                    "Delete Appointment",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                AppointmentQueries query = new();
                query.Remove_appointment(appointments.User_id);
            }
            Refresh_Dgv_Appointments();
        }

        // BARBER SECTION
        private void ButtonBarber_Click(object sender, RoutedEventArgs e)
        {
            // This method shows DataGrid with barber records.
            UserContent.Visibility = Visibility.Collapsed;
            ServiceContent.Visibility = Visibility.Collapsed;
            BarberContent.Visibility = Visibility.Visible;
            if (!user_permissions)
            {
                ButtonAddBarber.Visibility = Visibility.Collapsed;
                DataGridTemplateBarber.Visibility = Visibility.Collapsed;
                DataGridTemplateBarber2.Visibility = Visibility.Collapsed;
            }
            Refresh_Dgv_Barber();
        }

        private void BtnEditBarber_Click(object sender, RoutedEventArgs e)
        {
            // This method edits selected user.
            Barber.Barber barber = (Barber.Barber)dgvBarber.SelectedItem;
            new BarberEditWindow(barber).Show();
            Refresh_Dgv_Barber();
        }

        private void BtnRemoveBarber_Click(object sender, RoutedEventArgs e)
        {
            // This method removes selected user after confirmation.
            Barber.Barber barber = (Barber.Barber)dgvBarber.SelectedItem;
            if (MessageBox.Show("Are you sure that you want delete this barber?",
                                "Delete barber",
                                MessageBoxButton.OKCancel,
                                MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                BarberQueries query = new();
                query.Remove_barber(barber.BarberId);
            }
            Refresh_Dgv_Barber();
        }

        public void Refresh_Dgv_Barber()
        {
            // This method refreshes DataGrid.
            dgvBarber.ItemsSource = new BarberQueries().Get_barbers();
        }

        private void ButtonAddBarber_Click(object sender, RoutedEventArgs e)
        {
            // This method shows UserRegistration window.
            new BarberEditWindow().Show();
        }
        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            // This method shows EditServicesToBarber window.
            Barber.Barber barber = (Barber.Barber)dgvBarber.SelectedItem;
            new EditServiceToBarber(true,barber.BarberId).Show();
        }
        private void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            // This method shows EditServicesToBarber window.
            Barber.Barber barber = (Barber.Barber)dgvBarber.SelectedItem;
            new EditServiceToBarber(false, barber.BarberId).Show();
        }

        //SERVICE SECTION
        public void Refresh_Dgv_Service()
        {
            // This method refreshes DataGrid.
            ServiceQueries query = new();
            dgvService.ItemsSource = query.Get_all_services();
        }
        private void buttonService_Click(object sender, RoutedEventArgs e)
        {
            // This method shows DataGrid with service records.
            ServiceContent.Visibility = Visibility.Visible;
            BarberContent.Visibility = Visibility.Collapsed;
            UserContent.Visibility = Visibility.Collapsed;
            AppointmentsContent.Visibility = Visibility.Collapsed;
            if (!user_permissions)
            {
                ButtonAddService.Visibility = Visibility.Collapsed;
                DataGridTemplateService.Visibility = Visibility.Collapsed;
            }
            Refresh_Dgv_Service();
        }
        private void ButtonAddService_Click(object sender, RoutedEventArgs e)
        {
            // This method shows UserRegistration window.
            new ServiceWindow().Show();
            Refresh_Dgv_Service();
        }
        private void BtnEditService_Click(object sender, RoutedEventArgs e)
        {
            // This method edits selected user.
            Service.Service service = (Service.Service)dgvService.SelectedItem;
            new ServiceWindow(service).Show();
            Refresh_Dgv_Service();
        }
        private void BtnRemoveService_Click(object sender, RoutedEventArgs e)
        {
            // This method removes selected user after confirmation.
            Service.Service service = (Service.Service)dgvService.SelectedItem;
            if (MessageBox.Show("Are you sure that you want delete this service?",
                                "Delete service",
                                MessageBoxButton.OKCancel,
                                MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                ServiceQueries query = new();
                query.Delete_Service(service.ServiceId);
            }
            Refresh_Dgv_Barber();
        }
    }
}
    
