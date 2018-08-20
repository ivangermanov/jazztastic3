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
    /// Interaction logic for ReceiptOverviewPage.xaml
    /// </summary>
    public partial class ReceiptOverviewPage : Page {

        private Window receiptDetailWindow;
        public ReceiptOverviewPage() {
            InitializeComponent();
            receiptDetailWindow = new Window();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e) {

        }

        private void btn_Back_Click(object sender, RoutedEventArgs e) {
            receiptDetailWindow.Close();
            NavigationService.Navigate(SessionData.Pages[0]);
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e) {
            SearchVisitor();
        }

        private void SearchVisitor() {
            listVisitors.Children.Clear();
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.ReceiptOverviewSearch(tbSearchVisitor.Text), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    Button temp = new Button {
                                        Content = $"{reader[0]}, {reader[1]} {reader[2]}",
                                        FontSize = 16,
                                        Margin = new Thickness(8),
                                    };
                                    temp.Click += LoadReceipts;
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


        private void LoadReceipts(object sender, RoutedEventArgs e) {
            int visitorNo = ParseIntArgument(sender, e);
            listReceipts.Children.Clear();
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.ReceiptOverviewGetReceipts(visitorNo), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    Button temp = new Button {
                                        Content = $"{reader[0]}, {reader[1]}",
                                        FontSize = 24,
                                        Margin = new Thickness(8),
                                    };
                                    temp.Click += DisplayReceiptDetails;
                                    listReceipts.Children.Add(temp);
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayReceiptDetails(object sender, RoutedEventArgs e) {
            int receiptNo = ParseIntArgument(sender, e);
            if (receiptDetailWindow.IsActive) {
                receiptDetailWindow.Close();
            }
            receiptDetailWindow = new ReceiptDetailWindow(receiptNo);
            receiptDetailWindow.Show();
        }

        private int ParseIntArgument(object sender, RoutedEventArgs e) {
            string temp = ((Button)e.Source).Content.ToString();
            char[] chars = temp.ToCharArray();
            temp = "";
            int index = 0;
            while (chars[index] != ',') {
                temp += chars[index++];
            }
            return Convert.ToInt32(temp);
        }
    }
}
