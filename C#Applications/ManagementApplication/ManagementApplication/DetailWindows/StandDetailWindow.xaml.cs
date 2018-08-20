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
    /// Interaction logic for StandDetailWindow.xaml
    /// </summary>
    public partial class StandDetailWindow : Window {
        int currentStandNo;
        public StandDetailWindow(int givenStandNo) {
            InitializeComponent();
            currentStandNo = givenStandNo;
            Title = $"StandNo {currentStandNo} Details";
            ProcessDetails();
        }

        public void ProcessDetails() {
            using(MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                connection.Open();
                //Getting info about stand
                using(MySqlCommand command = new MySqlCommand(SessionData.StandDetailGetInfo(currentStandNo), connection)) {
                    using(MySqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        if(reader.HasRows) {
                            standNo.Content = $"{currentStandNo}";
                            standType.Content = $"{reader[0]}";
                        }
                    }
                }
                //Getting total revenue stand
                using (MySqlCommand command = new MySqlCommand(SessionData.StandDetailGetTotalRevenue(currentStandNo), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        if (reader.HasRows) {
                            totalRevenue.Content = $"${reader[0]}";
                        }
                    }
                }
                //Getting all employees stand
                using (MySqlCommand command = new MySqlCommand(SessionData.StandDetailGetEmployees(currentStandNo), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while(reader.Read()) {
                            if (reader.HasRows) {
                                Label temp = new Label {
                                    Content = $"EmployeeID: {reader[0]}, {reader[1]} {reader[2]}, Hourly Wage: {reader[3]}"
                                };
                                listEmployees.Children.Add(temp);
                            }
                        }
                    }
                }
                //Getting all items stand
                using (MySqlCommand command = new MySqlCommand(SessionData.StandDetailGetItems(currentStandNo), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while(reader.Read()) {
                            if (reader.HasRows) {
                                Label temp = new Label {
                                    Content = $"ItemNo: {reader[0]}, Name: {reader[1]}, Price: {reader[2]}, QOH: {reader[3]}"
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
