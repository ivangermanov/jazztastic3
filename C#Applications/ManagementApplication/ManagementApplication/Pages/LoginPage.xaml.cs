using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page {
        public LoginPage() {
            InitializeComponent();
            Timer errorDisplay = new Timer(200);
            errorDisplay.Elapsed += new ElapsedEventHandler(updateInterface);
            errorDisplay.Start();
        }
        private void updateInterface(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() => {
                if (SessionData.connectedToDb)
                {
                    LoginContainer.Visibility = Visibility.Visible;
                    ServerProblem.Visibility = Visibility.Collapsed;
                }
                else
                {
                    LoginContainer.Visibility = Visibility.Collapsed;
                    ServerProblem.Visibility = Visibility.Visible;

                }

            });
        }
        private void btn_Login_Click(object sender, RoutedEventArgs e) {
            bool success = false;
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.LoginSQL(username.Text, password.Password), connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if(reader.HasRows) {
                                SessionData.SetEmployeeDetails(Convert.ToInt32(reader[0]), $"{reader[1]}");
                                success = true;
                            } else {
                                throw new Exception("Username or Password incorrect.");
                            }
                        }
                    }
                    if (success) {
                        using (MySqlCommand command = new MySqlCommand(SessionData.LoginStatusUpdateSQL("T"), connection)) {
                            command.ExecuteNonQuery();
                        }
                        SessionData.InitializePages();
                        NavigationService.Navigate(SessionData.Pages[0]);
                    }
                }
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_Quit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}

