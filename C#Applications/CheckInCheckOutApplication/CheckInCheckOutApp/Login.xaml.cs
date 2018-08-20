using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace CheckInCheckOutApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ID.Text != "" && Password.Password.ToString() != "")
            {
                //Succesful login
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.LoginSQL(ID.Text, Password.Password), connection))
                    {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader != null)
                                {
                                    reader.Read();
                                    if (Convert.ToInt32(reader[0]) == 1)
                                    {
                                        SessionData.SetEmployeeId(Convert.ToInt32(reader[2]));
                                        SessionData.SetEmployeeName(reader[1].ToString());
                                        SessionData.CreatePages();
                                        reader.Close();
                                        SessionData.ModifyEmployeeStatus();
                                        NavigationService.Navigate(SessionData.homePage);
                                    }
                                    else
                                    {
                                        MessageBox.Show("ID or Password not recognized");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Error MySqlDataReader was null!");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            else
            {
                MessageBox.Show("Please fill in all the credentials!");
            }
        }
    }
}
