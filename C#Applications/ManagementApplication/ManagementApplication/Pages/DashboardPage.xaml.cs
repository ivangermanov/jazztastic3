using MySql.Data.MySqlClient;
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

namespace ManagementApplication.Pages {
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page {
        public DashboardPage() {
            InitializeComponent();
            employeeName.Content = SessionData.EmployeeName;
            employeeId.Content = SessionData.EmployeeId;
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e) {

            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.LoginStatusUpdateSQL("F"), connection)) {
                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            NavigationService.Navigate(new LoginPage());
        }

        private void btn_StandOverview_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(SessionData.Pages[1]);
        }

        private void btn_EventOverview_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(SessionData.Pages[2]);
        }

        private void btn_VisitorOverview_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(SessionData.Pages[3]);
        }

        private void btn_ItemOverview_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(SessionData.Pages[4]);
        }

        private void btn_ReceiptOverview_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(SessionData.Pages[5]);
        }
        private void btn_MissingItemsOverview_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(SessionData.Pages[6]);
        }
    }
}
