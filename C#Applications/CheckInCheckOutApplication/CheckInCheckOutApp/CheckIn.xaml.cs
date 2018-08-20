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

namespace CheckInCheckOutApp {
    /// <summary>
    /// Interaction logic for CheckIn.xaml
    /// </summary>
    public partial class CheckIn : Page {
        private RFID myRFIDReader;
        bool isRFIDScanning;
        string rfidTag;
        int? currentVisitor;
        public CheckIn() {
            InitializeComponent();
            try {
                myRFIDReader = new RFID();
                myRFIDReader.Tag += MyRFIDReader_Tag;
            } catch (PhidgetException ex) {
                MessageBox.Show("Could not startup!");
            }
        }

        private void MyRFIDReader_Tag(object sender, RFIDTagEventArgs e) {
            this.Dispatcher.Invoke(() => {
                rfidTag = e.Tag;
                CheckInDB();
            });
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) {
            if (myRFIDReader != null)
                myRFIDReader.Close();
            if (isRFIDScanning)
                ScanRFD();
            NavigationService.Navigate(SessionData.homePage);
        }

        private void ScanRFD() {
            try {
                if (!isRFIDScanning) {
                    myRFIDReader.Open();
                    isRFIDScanning = true;
                } else if (isRFIDScanning) {
                    myRFIDReader.Close();
                    isRFIDScanning = false;
                }
            } catch (PhidgetException ex) {
                MessageBox.Show("Could not connect to the RFID-Reader!");
            }
        }

        private void CheckInDB() {
            MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo);
            connection.Open();
            if (rfidTag != null && rfidTag != "") {
                if (SessionData.VisitorExistsInRFID(rfidTag) != -1) {
                    if (!SessionData.VisitorCheckedIn(rfidTag)) {
                        currentVisitor = SessionData.VisitorExistsInRFID(rfidTag);
                        string ticketType = "";
                        DateTime currentDay = DateTime.Now;
                        bool canCheckIn = true;

                        string sql = $"SELECT type FROM event_ticket WHERE visitorNo={currentVisitor}";
                        using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                            using (MySqlDataReader reader = command.ExecuteReader()) {
                                reader.Read();
                                if (reader.HasRows) {
                                    ticketType = reader[0].ToString();
                                }
                            }
                        }
                        if (currentDay.Day == 26) {
                            if (ticketType[0] == 'F') {
                                canCheckIn = false;
                            }
                        } else if (currentDay.Day == 27) {
                            if (ticketType[1] == 'F') {
                                canCheckIn = false;
                            }
                        } else {
                            if (ticketType[2] == 'F') {
                                canCheckIn = false;
                            }
                        }
                        if (canCheckIn) {
                            sql = $"INSERT INTO account_history (visitorNo, datetime, status) VALUES ({currentVisitor}, CURRENT_TIMESTAMP, 'Checked-in')";
                            MySqlCommand command = new MySqlCommand(sql, connection);
                            try {
                                command.ExecuteNonQuery();
                                listBoxCheckIn.Items.Insert(0, $"Visitor {currentVisitor} with RFID {rfidTag} checked in at {DateTime.Now}");
                            } catch (MySqlException ex) {
                                if (ex.Number == 1062) {

                                } else
                                    MessageBox.Show(ex.Message);
                            }
                            currentVisitor = null;
                            rfidTag = null;
                        } else {
                            listBoxCheckIn.Items.Insert(0, $"Visitor's ticket is not eligable for this day!");
                        }
                    }
                } else {
                    listBoxCheckIn.Items.Insert(0, "Attached RFID is not linked to a visitor!");
                }
            }
            connection.Close();
        }

        public void CheckInDB(string rfidTag, int? currentVisitor) {
            MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo);
            if (rfidTag != null && rfidTag != "") {
                if (SessionData.VisitorExistsInRFID(rfidTag) != -1 && !SessionData.VisitorCheckedIn(rfidTag)) {
                    currentVisitor = SessionData.VisitorExistsInRFID(rfidTag);
                    string sql = $"INSERT INTO account_history (visitorNo, datetime, status) VALUES ({currentVisitor}, CURRENT_TIMESTAMP, 'Checked-in')";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    connection.Open();
                    try {
                        command.ExecuteNonQuery();
                        listBoxCheckIn.Items.Insert(0, $"Visitor {currentVisitor} with RFID {rfidTag} checked in at {DateTime.Now}");
                    } catch (MySqlException ex) {
                        if (ex.Number == 1062) {

                        } else
                            MessageBox.Show(ex.Message);
                    }
                    connection.Close();
                    rfidTag = null;
                } else if (SessionData.VisitorCheckedIn(rfidTag)) {

                } else {
                    listBoxCheckIn.Items.Insert(0, "Attached RFID is not linked to a visitor!");
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            ScanRFD();
        }
    }
}
