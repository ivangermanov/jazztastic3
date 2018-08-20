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
using System.Windows.Shapes;

namespace ManagementApplication.DetailWindows {
    /// <summary>
    /// Interaction logic for ReceiptDetailWindow.xaml
    /// </summary>
    public partial class ReceiptDetailWindow : Window {
        int currentReceiptNo;
        public ReceiptDetailWindow(int givenReceiptNo) {
            InitializeComponent();
            currentReceiptNo = givenReceiptNo;
            ProcessDetails();
        }

        private void ProcessDetails() {
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    //Get Receipt info
                    using (MySqlCommand command = new MySqlCommand(SessionData.ReceiptDetailGetInfo(currentReceiptNo), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if (reader.HasRows) {
                                receiptNo.Content = $"{reader[0]}";
                                date.Content = $"{reader[1]}";
                                employeeId.Content = $"{reader[2]}";
                            }
                        }
                    }
                    //Get Total Price
                    using (MySqlCommand command = new MySqlCommand(SessionData.ReceiptDetailGetTotalPrice(currentReceiptNo), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if (reader.HasRows) {
                                totalPrice.Content = $"{reader[0]}";
                                visitorNo.Content = $"{reader[1]}";
                            }
                        }
                    }
                    //Get Items on receipt
                    using (MySqlCommand command = new MySqlCommand(SessionData.ReceiptDetailsGetItems(currentReceiptNo), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while(reader.Read()) {
                                if (reader.HasRows) {
                                    string date = "";
                                    try {
                                        date = reader[4].ToString();
                                    } catch(Exception e) {
                                        date = "N.A.";
                                    }
                                    Label temp = new Label {
                                        Content = $"ItemNo: {reader[0]}, Name: {reader[1]}, Price: {reader[2]}, Type: {reader[3]} Return: {date} Quantity: {reader[5]}"
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
    }
}
