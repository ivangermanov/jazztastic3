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
    /// Interaction logic for ItemDetailWindow.xaml
    /// </summary>
    public partial class ItemDetailWindow : Window {
        int currentItemNo;
        public ItemDetailWindow(int givenItemNo) {
            InitializeComponent();
            currentItemNo = givenItemNo;
            ProcessDetails();
        }

        public void ProcessDetails() {
            using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                connection.Open();
                //Getting all items
                using (MySqlCommand command = new MySqlCommand(SessionData.ItemDetailGetInfo(currentItemNo), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        if(reader.HasRows) {
                            itemNo.Content = $"{reader[0]}";
                            type.Content = $"{reader[1]}";
                            price.Content = $"{reader[2]}";
                            name.Content = $"{reader[3]}";
                            //itemImage.Content = $"{reader[4]}";
                            description.Content = $"{reader[5]}";
                            intialQOH.Content = $"{reader[6]}";
                        }
                    }
                }
            }
        }
    }
}
