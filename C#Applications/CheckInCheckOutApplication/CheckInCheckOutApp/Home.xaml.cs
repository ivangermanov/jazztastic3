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

namespace CheckInCheckOutApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            Name.Content = SessionData.EmployeeName;
            EmployeeID.Content = SessionData.EmployeeId;
        }

        private void btnScanQR_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(SessionData.scanQRPage);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            SessionData.ModifyEmployeeStatus();
            SessionData.ClearSessionData();
            NavigationService.Navigate(new Login());
        }

        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(SessionData.checkInPage);
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(SessionData.checkOutPage);
        }
    }
}
