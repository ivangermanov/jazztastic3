using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using Phidget22;
using Phidget22.Events;
using System.Collections.Generic;

namespace LoanStandApplication {
    /// <summary>
    /// Interaction logic for LoanStatus.xaml
    /// </summary>
    /// 

    public delegate void ReturnItemHandler(int itemNo, int standNo, string purchased, string returned, int itemQuantity);
    public partial class LoanStatus : Page {

        private RFID myRFIDReader;
        private int visitorNo = 0;
        private string currentTag = "";
        private List<LoanItemInfo> loanedItems;
        public LoanStatus() {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            InitializeRFID();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e) {
            myRFIDReader.Close();
            ProcessActions();
            NavigationService.Navigate(SessionData.DashboardPage);
        }

        private void InitializeRFID() {
            try {
                myRFIDReader = new RFID();
                myRFIDReader.Attach += MyRFIDReader_Attach;
                myRFIDReader.Detach += MyRFIDReader_Detach;
                myRFIDReader.Tag += MyRFIDReader_Tag;
            } catch (PhidgetException ex) {
                MessageBox.Show(ex.Message);
            }
            myRFIDReader.Open();
        }

        private void RecognizedTag(string tag) {
            loanedItems = new List<LoanItemInfo>();
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo)) {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SessionData.GetVisitorNoSQL(tag), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        if (reader != null) {
                            reader.Read();
                            if (reader.HasRows) {
                                try {
                                    visitorNo = Convert.ToInt32(reader[0]);
                                    LoadVisitorInfo();
                                    LoadItemsRent();
                                } catch (Exception e) {
                                    MessageBox.Show(e.Message);
                                }
                            } else {
                                MessageBox.Show("No visitor assigned to this RFID Tag!");
                            }
                        }
                    }
                }
                connection.Close();
            }
        }

        //Get Visitor Info
        private void LoadVisitorInfo() {
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo)) {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SessionData.ScanSQL(visitorNo), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        if (reader != null) {
                            reader.Read();
                            FirstName.Content = reader[0];
                            LastName.Content = reader[1];
                            GovCode.Content = reader[2];
                            DoB.Content = reader[3];
                            Email.Content = reader[4];
                        }
                    }
                }
                connection.Close();
            }
        }

        //Get the loaned products which are on the visitor's name
        private void LoadItemsRent() {
            ItemList.Children.Clear();
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo)) {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SessionData.GetLoanedSQL(visitorNo), connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        if (reader != null) {
                            while (reader.Read()) {
                                int itemNo = Convert.ToInt32(reader[0]);
                                LoanItemInfo loanItemInfo = new LoanItemInfo(visitorNo, reader);
                                loanItemInfo.ItemReturn += new ReturnItemHandler(ReturnItem);
                                ((Button)loanItemInfo.Children[1]).Click += new RoutedEventHandler(UpdateInformationLoanItemInfo);
                                loanedItems.Add(loanItemInfo);
                            }
                        }
                        connection.Close();
                    }
                }
            }
            DisplayLoanedItems();
        }

        //Display the loaned items from the loaned item list
        private void DisplayLoanedItems() {
            ItemList.Children.Clear();
            foreach (LoanItemInfo l in loanedItems) {
                ItemList.Children.Add(l);
            }
        }

        //Marius suggested this if anyone is asking
        private void UpdateInformationLoanItemInfo(object sender, RoutedEventArgs e) {
            ((LoanItemInfo)((Button)(e.Source)).Parent).UpdateLoanItemInfo();
        }

        //Removal of LoanItemInfo instance method
        private void ReturnItem(int itemNo, int standNo, string purchased, string returned, int itemQuantity) {
            for (int i = 0; i < loanedItems.Count; i++) {
                LoanItemInfo l = loanedItems[i];
                if (l.ItemNo == itemNo && l.StandNo == standNo && l.Purchased == purchased && l.Returned == returned && l.Quantity == itemQuantity) {
                    loanedItems[i].DecreaseQuantity();
                }
            }
            DisplayLoanedItems();
        }

        //Read through the list of loaned items and forward the changes to the database
        private void ProcessActions() {
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo)) {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                //bug solved by marius and ivan at 5.20, 3h till deadline, gg jeffrey
                if (loanedItems!=null)
                {
                    foreach (LoanItemInfo l in loanedItems)
                    {
                        try
                        {
                            int quantityRemoved = l.OriginalQuantity - l.Quantity;
                            command.CommandText = SessionData.GetStockItem(l.StandNo, l.ItemNo);
                            MySqlDataReader reader = command.ExecuteReader();
                            reader.Read();
                            //Read initial qoh in item stand
                            int qoh = Convert.ToInt32(reader[0]);
                            reader.Close();
                            reader.Dispose();

                            if (l.Quantity == 0)
                            {
                                //if all the items are returned delete record
                                command.CommandText = SessionData.ReturnItemSQL(visitorNo, l.ItemNo, l.StandNo, l.Purchased, l.Returned);
                                command.ExecuteNonQuery();
                                qoh += l.OriginalQuantity;
                            }
                            else if (l.OriginalQuantity != l.Quantity)
                            {
                                //else update the record if necessary
                                command.CommandText = SessionData.UpdateItemRecord(visitorNo, l.ItemNo, l.StandNo, l.Purchased, l.Returned, l.Quantity);
                                command.ExecuteNonQuery();
                                qoh += quantityRemoved;
                            }

                            //Update the QOH in item stand
                            command.CommandText = SessionData.UpdateStockSQL(l.ItemNo, l.StandNo, qoh);
                            command.ExecuteNonQuery();

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                }
            }
        }

        //RFID event methods
        private void MyRFIDReader_Attach(object sender, AttachEventArgs e) {
        }

        private void MyRFIDReader_Detach(object sender, DetachEventArgs e) {
        }

        private void MyRFIDReader_Tag(object sender, RFIDTagEventArgs e) {
            this.Dispatcher.Invoke(() => {
                RecognizedTag(e.Tag);
                currentTag = e.Tag;
            });
        }
    }
}
