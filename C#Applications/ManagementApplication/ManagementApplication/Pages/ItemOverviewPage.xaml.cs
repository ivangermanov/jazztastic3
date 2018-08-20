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
    /// Interaction logic for ProductOverviewPage.xaml
    /// </summary>
    public partial class ItemOverviewPage : Page {
        private List<Label> buyItemLabels;
        private List<Label> buyItemSpentLabels;
        private List<Label> loanItemLabels;
        private List<Label> loanItemSpentLabels;
        private Window itemDetailWindow;
        public ItemOverviewPage() {
            InitializeComponent();
            buyItemLabels = new List<Label>() { buyItemOne, buyItemTwo, buyItemThree, buyItemFour, buyItemFive };
            buyItemSpentLabels = new List<Label>() { buyItemOneSpent, buyItemTwoSpent, buyItemThreeSpent, buyItemFourSpent, buyItemFiveSpent };
            itemDetailWindow = new Window();
            loanItemLabels = new List<Label>() { loanItemOne, loanItemTwo, loanItemThree, loanItemFour, loanItemFive };
            loanItemSpentLabels = new List<Label>() { loanItemOneSpent, loanItemTwoSpent, loanItemThreeSpent, loanItemFourSpent, loanItemFiveSpent };
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            LoadTopProfit();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e) {
            LoadTopProfit();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e) {
            itemDetailWindow.Close();
            NavigationService.Navigate(SessionData.Pages[0]);
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e) {
            Searchitem();
        }

        private void LoadTopProfit() {
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.ItemOverviewGetTopProfit("buy"), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            int index = 0;
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    buyItemLabels[index].Content = $"{reader[0]}, {reader[1]}";
                                    buyItemSpentLabels[index].Content = $"{reader[2]}";
                                    index++;
                                }
                            }
                        }
                    }
                    using (MySqlCommand command = new MySqlCommand(SessionData.ItemOverviewGetTopProfit("loan"), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            int index = 0;
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    loanItemLabels[index].Content = $"{reader[0]}, {reader[1]}";
                                    loanItemSpentLabels[index].Content = $"{reader[2]}";
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

        private void Searchitem() {
            listItems.Children.Clear();
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.ItemOverviewSearch(tbSearchItem.Text), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (reader.HasRows) {
                                    Button temp = new Button {
                                        Content = $"{reader[0]}, {reader[1]} {reader[2]}",
                                        FontSize = 16,
                                        Margin = new Thickness(8),
                                    };
                                    temp.Click += DisplayItemDetails;
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
        private void DisplayItemDetails(object sender, RoutedEventArgs e) {
            string temp = ((Button)e.Source).Content.ToString();
            char[] chars = temp.ToCharArray();
            temp = "";
            int index = 0;
            while (chars[index] != ',') {
                temp += chars[index++];
            }
            int itemNo = Convert.ToInt32(temp);
            if (itemDetailWindow.IsActive) {
                itemDetailWindow.Close();
            }
            itemDetailWindow = new ItemDetailWindow(itemNo);
            itemDetailWindow.Show();
        }
    }
}
