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
    /// Interaction logic for MissingItemsOverview.xaml
    /// </summary>
    public partial class MissingItemsOverviewPage : Page {
        public MissingItemsOverviewPage() {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            LoadVisitors();
        }
        private void btn_Back_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(SessionData.Pages[0]);
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e) {
            LoadVisitors();
            listItems.Children.Clear();
        }

        private void btn_Filter_Click(object sender, RoutedEventArgs e) {
            LoadVisitors();
        }

        private void LoadVisitors() {
            listVisitors.Children.Clear();
            try {
                using(MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using(MySqlCommand command = new MySqlCommand(SessionData.MissingItemsOverviewGetVisitors(tbFilterVisitor.Text), connection)) {
                        using(MySqlDataReader reader = command.ExecuteReader()) {
                            while(reader.Read()) {
                                if(reader.HasRows) {
                                    Button temp = new Button {
                                        Content = $"{reader[0]}, {reader[1]} {reader[2]}",
                                        FontSize = 16,
                                        Margin = new Thickness(8),
                                    };
                                    temp.Click += DisplayItemsBorrowed;
                                    listVisitors.Children.Add(temp);
                                }
                            }
                        }
                    }
                }
            } catch(Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        private void DisplayItemsBorrowed(object sender, RoutedEventArgs e) {
            int visitorNo = ParseIntArgument(sender, e);
            listItems.Children.Clear();
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.MissingItemsOVerviewGetItems(visitorNo), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    Label temp = new Label {
                                        Content = $"Item: {reader[0]} {reader[1]}, QTY: {reader[2]}, StandNo: {reader[3]}, Purchase: {reader[4]}, Return:{reader[5]}, ReceiptNo: {reader[6]}",
                                        FontSize = 7
                                    };
                                    listItems.Children.Add(temp);
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
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
