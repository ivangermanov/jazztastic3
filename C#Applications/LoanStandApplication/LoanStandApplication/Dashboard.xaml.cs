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

namespace LoanStandApplication {
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page {
        public Dashboard() {
            InitializeComponent();
            Name.Content = SessionData.EmployeeName;
            EmployeeId.Content = SessionData.EmployeeId;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Login());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(SessionData.LoanShopPage);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(SessionData.LoanStatusPage);
        }
    }
}
