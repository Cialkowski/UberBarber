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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Org.BouncyCastle.Asn1.X509;
using UberBarber.database;
using static UberBarber.database.DatabaseQueries;

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
            Draft1Content.Visibility = Visibility.Collapsed;
            Draft2Content.Visibility = Visibility.Collapsed;
            UserContent.Visibility = Visibility.Collapsed;
        }

        private void button_user_Click(object sender, RoutedEventArgs e)
        {
            // TODO: users handling
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
            // This method shows DataGrid with user records.
        {
            UserContent.Visibility = Visibility.Visible;
            DatabaseQueries query = new();
            dgvUser.ItemsSource = query.GetUsers();
        }

        private void ButtonRemoveUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
