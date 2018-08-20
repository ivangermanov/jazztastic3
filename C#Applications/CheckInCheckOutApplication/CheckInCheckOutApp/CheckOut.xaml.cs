using MySql.Data.MySqlClient;
using Phidget22;
using Phidget22.Events;
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

namespace CheckInCheckOutApp
{
    /// <summary>
    /// Interaction logic for CheckOut.xaml
    /// </summary>
    public partial class CheckOut : Page
    {
        private RFID myRFIDReader;
        bool isRFIDScanning;
        string rfidTag;
        int? currentVisitor;
        public CheckOut()
        {
            InitializeComponent();
            try
            {
                myRFIDReader = new RFID();
                myRFIDReader.Tag += MyRFIDReader_Tag;
            }
            catch (PhidgetException ex)
            {
                MessageBox.Show("Could not startup!");
            }
        }

        private void MyRFIDReader_Tag(object sender, RFIDTagEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                rfidTag = e.Tag;
                CheckOutDB();
            });
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (myRFIDReader != null)
                myRFIDReader.Close();
            if (isRFIDScanning)
                ScanRFD();
            NavigationService.Navigate(SessionData.homePage);
        }

        private void ScanRFD()
        {
            try
            {
                if (!isRFIDScanning)
                {
                    myRFIDReader.Open();
                    isRFIDScanning = true;
                }

                else if (isRFIDScanning)
                {
                    myRFIDReader.Close();
                    isRFIDScanning = false;
                }
            }
            catch (PhidgetException ex)
            {
                MessageBox.Show("Could not connect to the RFID-Reader!");
            }
        }

        private void CheckOutDB()
        {
            MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo);
            if (rfidTag != null && rfidTag != "")
            {
                if (SessionData.VisitorExistsInRFID(rfidTag) != -1)
                {
                    if (SessionData.VisitorCheckedIn(rfidTag))
                    {
                        currentVisitor = SessionData.VisitorExistsInRFID(rfidTag);
                        string sql = $"INSERT INTO account_history (visitorNo, datetime, status) VALUES ({currentVisitor}, CURRENT_TIMESTAMP, 'Checked-out')";
                        MySqlCommand command = new MySqlCommand(sql, connection);
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            listBoxCheckOut.Items.Insert(0, $"Visitor {currentVisitor} with RFID {rfidTag} checked out at {DateTime.Now}");
                        }
                        catch (MySqlException ex)
                        {
                            if (ex.Number == 1062)
                            {

                            }
                            else
                                MessageBox.Show(ex.Message);
                        }
                        connection.Close();
                        currentVisitor = null;
                    }
                }
                else
                {
                    listBoxCheckOut.Items.Insert(0, "Attached RFID is not linked to a visitor!");
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ScanRFD();
        }

        private void btnReturnRFID_Click(object sender, RoutedEventArgs e)
        {
            if (rfidTag != null && rfidTag != "")
            {
                if (SessionData.VisitorExistsInRFID(rfidTag) != -1)
                {
                    currentVisitor = SessionData.VisitorExistsInRFID(rfidTag);
                    using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo))
                    {
                        connection.Open();

                        bool isReadyForReturn = true;
                        string sql = $"SELECT a.itemNo, i.name, a.quantity, a.standNo, a.purchase_datetime, a.return_datetime, a.receiptNo FROM account_buy_loan a JOIN item i ON a.itemNo=i.itemNo WHERE a.visitorNo={currentVisitor} AND a.item_type=i.type AND a.item_type='loan';";
                        using (MySqlCommand commandUpdate = new MySqlCommand(sql, connection)) {
                            using(MySqlDataReader reader = commandUpdate.ExecuteReader()) {
                                reader.Read();
                                if(reader.HasRows) {
                                    isReadyForReturn = false;
                                }
                            }
                        }

                        if(isReadyForReturn) {
                            bool hasFines = false;
                            string ticketType = "";
                            int fineCount = 0;
                            int[] days = { 0, 0, 0 };
                            List<DateTime> datesCheckOut = new List<DateTime>();
                            List<DateTime> datesCheckIn = new List<DateTime>();

                            sql = $"SELECT type FROM event_ticket WHERE visitorNo={currentVisitor}";
                            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                                using (MySqlDataReader reader = command.ExecuteReader()) {
                                    reader.Read();
                                    if (reader.HasRows) {
                                        ticketType = reader[0].ToString();
                                    }
                                }
                            }

                            sql = $"SELECT datetime FROM account_history WHERE visitorNo={currentVisitor} AND status='Checked-out'";
                            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                                using (MySqlDataReader reader = command.ExecuteReader()) {
                                    while(reader.Read()) {
                                        if (reader.HasRows) {
                                            datesCheckOut.Add(DateTime.Parse(reader[0].ToString()));
                                        }
                                    }
                                }
                            }

                            sql = $"SELECT datetime FROM account_history WHERE visitorNo={currentVisitor} AND status='Checked-in'";
                            using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                                using (MySqlDataReader reader = command.ExecuteReader()) {
                                    while (reader.Read()) {
                                        if (reader.HasRows) {
                                            datesCheckIn.Add(DateTime.Parse(reader[0].ToString()));
                                        }
                                    }
                                }
                            }

                            foreach (DateTime d in datesCheckOut) {
                                if(d.Day == 26) {
                                    days[0]--;
                                } else if(d.Day == 27) {
                                    days[1]--;
                                } else {
                                    days[2]--;
                                }
                            }

                            foreach (DateTime d in datesCheckIn) {
                                if (d.Day == 26) {
                                    days[0]++;
                                } else if (d.Day == 27) {
                                    days[1]++;
                                } else {
                                    days[2]++;
                                }
                            }

                            if(ticketType == "TFF") {
                                if(days[0] != 0) {
                                    if(days[1] == 0) {
                                        fineCount++;
                                    }
                                    if(days[2] == -1) {
                                        fineCount++;
                                    }
                                    hasFines = true;
                                }
                            } else if(ticketType == "TTF") {
                                if((days[0] == 0 && days[1] == 1) || (days[0] == 1 && days[1] != -1)) {
                                    fineCount++;
                                    hasFines = true;
                                }
                            } else if(ticketType == "FTF") {
                                if((days[1] != 0)) {
                                    fineCount++;
                                    hasFines = true;
                                }
                            } else if(ticketType == "TFT") {
                                if(days[0] != 0) {
                                    fineCount++;
                                    hasFines = true;
                                }
                            }


                            sql = $"UPDATE rfid SET visitorNo=NULL WHERE tag='{rfidTag}';";
                            using (MySqlCommand commandUpdate = new MySqlCommand(sql, connection)) {
                                commandUpdate.ExecuteNonQuery();
                                listBoxCheckOut.Items.Insert(0, $"Visitor {currentVisitor} with RFID {rfidTag} returned the tag and left the event at {DateTime.Now}");
                                /*if (listBoxCheckOut.Items.GetItemAt(1).ToString().StartsWith($"Visitor {currentVisitor}"))
                                    listBoxCheckOut.Items.RemoveAt(1);*/
                            }

                            if (hasFines) {
                                MessageBox.Show($"Visitor has to pay a fine of €{fineCount * 10.0}");
                            }
                            
                        } else {
                            MessageBox.Show("Visitor still needs to return items!");
                        }
                    }
                    rfidTag = null;
                    currentVisitor = 0;
                }
            }
        }
    }
}