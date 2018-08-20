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
using ManagementApplication.DetailWindows;

namespace ManagementApplication.Pages {
    /// <summary>
    /// Interaction logic for StandOverviewPage.xaml
    /// </summary>
    public partial class StandOverviewPage : Page {
        private Window standDetailWindow;
        public StandOverviewPage() {
            InitializeComponent();
            standDetailWindow = new Window();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            LoadStandDetails();
        }
        
        private void btn_Back_Click(object sender, RoutedEventArgs e) {
            standDetailWindow.Close();
            NavigationService.Navigate(SessionData.Pages[0]);
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e) {
            LoadStandDetails();
        }

        private void LoadStandDetails() {
            listBuyStands.Children.Clear();
            listLoanStands.Children.Clear();
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.StandOverviewGetStands("buy"), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    Button temp = new Button {
                                        Content = $"{reader[0]}",
                                        FontSize = 24,
                                        Margin = new Thickness(8),
                                    };
                                    temp.Click += DisplayStandDetails;
                                    listBuyStands.Children.Add(temp);
                                }
                            }
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(SessionData.StandOverviewGetStands("loan"), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    Button temp = new Button {
                                        Content = $"{reader[0]}",
                                        FontSize = 24,
                                        Margin = new Thickness(8),
                                    };
                                    temp.Click += DisplayStandDetails;
                                    listLoanStands.Children.Add(temp);
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayStandDetails(object sender, RoutedEventArgs e) {
            int standNo = Convert.ToInt32(((Button)e.Source).Content);
            if (standDetailWindow.IsActive) {
                standDetailWindow.Close();
            }
            standDetailWindow = new StandDetailWindow(standNo);
            standDetailWindow.Show();
        }
    }
}
