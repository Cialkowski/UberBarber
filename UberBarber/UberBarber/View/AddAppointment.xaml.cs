﻿using System;
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
        }

        private void CalendarVisitDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalendarVisitDate.SelectedDate != null) Selected_date = CalendarVisitDate.SelectedDate.Value;
            MessageBox.Show(Selected_date.ToString());

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
    }
}
