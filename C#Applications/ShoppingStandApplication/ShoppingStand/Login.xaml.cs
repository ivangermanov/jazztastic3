using System;
using System.Net.NetworkInformation;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;

namespace ShoppingStand {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page {
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
        private void retrieveLatestReceiptNumber()
        {
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SessionData.getTheLatestReceipt(), connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            reader.Read();
                            SessionData.latestReceipt= Convert.ToInt32(reader[0])+1;
                           
                        }
                        else
                        {
                            MessageBox.Show("Error MySqlDataReader was null!");
                            
                        }
                    }
                }
            }
        }

      


        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if (SessionData.connectedToDb)
            {
                retrieveLatestReceiptNumber();
                if (ID.Text != "" && Password.Password.ToString() != "")
                {
                    //Succesful login
                    using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: No server connection!", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        using (MySqlCommand command = new MySqlCommand(SessionData.LoginSQL(ID.Text, Password.Password), connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader != null)
                                {
                                    reader.Read();
                                    if (Convert.ToInt32(reader[0]) == 1)
                                    {

                                        SessionData.IdStand = Convert.ToInt32(reader[3]);
                                        SessionData.SetEmployeeId(Convert.ToInt32(reader[2]));
                                        SessionData.SetEmployeeName(reader[1].ToString());
                                       
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
                else
                {
                    MessageBox.Show("Please fill in all the credentials!");
                }
                if(ID.Text != "" && Password.Password.ToString() != "")
                {
                    //Succesful login
                    using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: No server connection!", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        using (MySqlCommand command = new MySqlCommand(SessionData.RetrieveStand(SessionData.IdStand.ToString()), connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader != null)
                                {
                                    reader.Read();
                                    if (((string)reader[0]).Contains(SessionData.shop_type))
                                    {

                                        SessionData.CreatePages();
                                        NavigationService.Navigate(SessionData.ElementsPage);
                                    }
                                    else
                                    {
                                        MessageBox.Show("This is not the correct shop to login into!");
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

              







            }



            ////check if the stand is the correct one
        }
            





        }
    }
