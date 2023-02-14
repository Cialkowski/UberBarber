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
using Google.Protobuf;
using UberBarber.database;
using UberBarber.database.AppointmentQueries;

namespace UberBarber.View
{
    /// <summary>
    /// Interaction logic for AddAppointment.xaml
    /// </summary>
    public partial class AddAppointment : Window
    {
        public DateTime Selected_date { get; set; }

        public AddAppointment()
        {
            InitializeComponent();
            ComboBoxBarber.Items.Add(3);
            ComboBox_Services.Items.Add(3);

        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //This function allows you to drag the window to any place

            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnAddApointment_Click(object sender, RoutedEventArgs e)
        {
            int barber = (int)ComboBoxBarber.SelectedItem;
            int service = (int)ComboBox_Services.SelectionBoxItem;
            string first_name = text_name.Text;
            string surrname = text_Surrname.Text;
            string phone_number = text_PhoneNumber.Text;
            string description = text_Description.Text;
            AppointmentQueries query = new();
            query.Add_customer(first_name, surrname, phone_number, Selected_date, service, barber, description);
            query.Add_appointments(barber.ToString(), service.ToString(), Selected_date);
        }

        private void CalendarAppointments_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalendarAppointments.SelectedDate != null) Selected_date = CalendarAppointments.SelectedDate.Value;
        }
    }
}
