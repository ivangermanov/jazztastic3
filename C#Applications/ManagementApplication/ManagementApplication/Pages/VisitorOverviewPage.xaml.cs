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
    /// Interaction logic for VisitorOverviewPage.xaml
    /// </summary>
    public partial class VisitorOverviewPage : Page {
        private List<Label> visitorLabels;
        private List<Label> visitorSpentLabels;
        private Window visitorDetailWindow;
        public VisitorOverviewPage() {
            InitializeComponent();
            visitorLabels = new List<Label>() { visitorOne, visitorTwo, visitorThree, visitorFour, visitorFive };
            visitorSpentLabels = new List<Label>() { visitorOneSpent, visitorTwoSpent, visitorThreeSpent, visitorFourSpent, visitorFiveSpent };
            visitorDetailWindow = new Window();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            LoadTopSpender();
        }
        
        private void btn_Refresh_Click(object sender, RoutedEventArgs e) {
            LoadTopSpender();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e) {
            visitorDetailWindow.Close();
            NavigationService.Navigate(SessionData.Pages[0]);
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e) {
            SearchVisitor();
        }

        private void LoadTopSpender() {
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.VisitorOverviewGetTopSpenders(), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            int index = 0;
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    visitorLabels[index].Content = $"{reader[0]}, {reader[1]}";
                                    visitorSpentLabels[index].Content = $"{reader[2]}";
                                    index++;
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchVisitor() {
            listVisitors.Children.Clear();
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.VisitorOverviewSearch(tbSearchVisitor.Text), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    Button temp = new Button {
                                        Content = $"{reader[0]}, {reader[1]} {reader[2]}",
                                        FontSize = 16,
                                        Margin = new Thickness(8),
                                    };
                                    temp.Click += DisplayVisitorDetails;
                                    listVisitors.Children.Add(temp);
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void DisplayVisitorDetails(object sender, RoutedEventArgs e) {
            string temp = ((Button)e.Source).Content.ToString();
            char[] chars = temp.ToCharArray();
            temp = "";
            int index = 0;
            while(chars[index] != ',') {
                temp += chars[index++];
            }
            int visitorNo = Convert.ToInt32(temp);
            if (visitorDetailWindow.IsActive) {
                visitorDetailWindow.Close();
            }
            visitorDetailWindow = new VisitorDetailWindow(visitorNo);
            visitorDetailWindow.Show();
        }
    }

}
