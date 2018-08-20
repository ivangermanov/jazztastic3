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

namespace ManagementApplication.DetailWindows {
    /// <summary>
    /// Interaction logic for VisitorDetailWindow.xaml
    /// </summary>
    public partial class VisitorDetailWindow : Window {
        int currentVisitorNo;
        public VisitorDetailWindow(int givenVisitorNo) {
            InitializeComponent();
            currentVisitorNo = givenVisitorNo;
            ProcessDetails();
        }
        
        public void ProcessDetails() {
            using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                connection.Open();
                //Getting visitor info
                using (MySqlCommand command = new MySqlCommand(SessionData.VisitorDetailGetInfo(currentVisitorNo), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        if(reader.HasRows) {
                            visitorNo.Content = $"{reader[0]}";
                            name.Content = $"{reader[1]} {reader[2]}";
                            govId.Content = $"{reader[3]}";
                            money.Content = $"{reader[4]}";
                            dob.Content = $"{reader[5]}";
                            address.Content = $"{reader[6]}";
                            email.Content = $"{reader[7]}";
                            moneySpent.Content = $"{reader[8]}";
                        }
                    }
                }
                //Getting visitor history
                using (MySqlCommand command = new MySqlCommand(SessionData.VisitorDetailsGetHistory(currentVisitorNo), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            if (reader.HasRows) {
                                Label temp = new Label {
                                    Content = $"Date: {reader[0]}, Status: {reader[1]}"
                                };
                                listHistory.Children.Add(temp);
                            }
                        }
                    }
                }
                //Getting all items
                using (MySqlCommand command = new MySqlCommand(SessionData.VisitorDetailGetItems(currentVisitorNo), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            if (reader.HasRows) {
                                string date = "";
                                try {
                                    date = reader[4].ToString();
                                } catch(Exception) {
                                    date = "N.A.";
                                }
                                Label temp = new Label {
                                    Content = $"{reader[0]}, {reader[1]}, Type: {reader[2]}, QTY: {reader[3]}, Return: {date}"
                                };
                                listItems.Children.Add(temp);
                            }
                        }
                    }
                }
            }
        }
    }
}
