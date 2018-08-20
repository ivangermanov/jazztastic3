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
using ManagementApplication.Charts;



namespace ManagementApplication.Pages {
    /// <summary>
    /// Interaction logic for EventOverviewPagexaml.xaml
    /// </summary>
    public partial class EventOverviewPage : Page {
        private Window chartWindow;
        public EventOverviewPage() {
            InitializeComponent();
            chartWindow = new Window();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            LoadEventDetails();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e) {
            LoadEventDetails();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e) {
            chartWindow.Close();
            NavigationService.Navigate(SessionData.Pages[0]);
        }

        private void LoadEventDetails() {
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.EventOverviewGetVisitorsTotal(), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if(reader.HasRows) {
                                visitorTotal.Content = $"{reader[0]}";
                            }
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(SessionData.EventOverviewGetVisitorsCheckedIn(), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if (reader.HasRows) {
                                visitorCheckedIn.Content = $"{reader[0]}";
                            }
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(SessionData.EventOverviewGetVisitorsTotalBalance(), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if (reader.HasRows) {
                                visitorTotalBalance.Content = $"{reader[0]}";
                            }
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(SessionData.EventOverviewGetCampspotsFree(), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if (reader.HasRows) {
                                campingSpotsFree.Content = $"{reader[0]}";
                            }
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(SessionData.EventOverviewGetCampspotsOccupied(), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if (reader.HasRows) {
                                campingSpotsOccupied.Content = $"{reader[0]}";
                            }
                        }
                    }

                    using (MySqlCommand command = new MySqlCommand(SessionData.EventOverviewGetTotalRevenue(), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if (reader.HasRows) {
                                eventTotalRevenue.Content = $"{reader[0]}";
                            }
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(SessionData.EventOverviewGetTotalRevenueTickets(), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            int sum = 0;
                            while (reader.Read()) {
                                string c = reader[0].ToString();
                                int counter = 0;
                                for(int i = 0; i < 3; i++) {
                                    if(c[i] == 'T') {
                                        counter++;
                                    }
                                }

                                if(counter == 1) {
                                    sum += 40;
                                } else if(counter == 2) {
                                    sum += 45;
                                } else {
                                    sum += 55;
                                }
                            }
                            ticketTotalRevenue.Content = $"{sum}.00";
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(SessionData.EventOveriewGetTotalRevenueCamping(), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if (reader.HasRows) {
                                campingTotalRevenue.Content = $"{reader[0]}";
                            }
                        }
                    }

                    totalRevenue.Content = $"{Convert.ToDouble(eventTotalRevenue.Content)+ Convert.ToDouble(ticketTotalRevenue.Content)+ Convert.ToDouble(campingTotalRevenue.Content)}";
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayVisitorChart(object sender, RoutedEventArgs e) {
            if (chartWindow.IsActive) {
                chartWindow.Close();
            }
            chartWindow.Close();
            chartWindow = new VisitorCheckedInChart();
            chartWindow.Show();
        }

        private void DisplayRevenueChart(object sender, RoutedEventArgs e) {
            if (chartWindow.IsActive) {
                chartWindow.Close();
            }
            chartWindow.Close();
            chartWindow = new RevenueChart();
            chartWindow.Show();
        }
    }
}
