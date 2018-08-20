using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MySql.Data.MySqlClient;
using Phidget22;
using Phidget22.Events;

namespace LoanStandApplication
{
    /// <summary>
    /// Interaction logic for Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {
        List<ToCheckOutElement> elementsToBeCheckedOut;
        private RFID myRFIDReader;
        bool isRFIDScanning;
        string rfidTag;
        int? idClientReadFromRfid;
        int totalCostLocal;
        DateTime timeFromServer;

        public delegate void finishedWithCheckingOut();
        public event finishedWithCheckingOut recalloutside;
        float local_balance,local_totalCost;
        Rectangle outer_reference;
        public Checkout(List<ToCheckOutElement> t,int totalCost,int receipt,Rectangle outer)
        {
            outer_reference = outer;
            getcurrentDateFromServer();
            InitializeComponent();
            totalCostLocal = totalCost;
            DateC.Content =timeFromServer;
            ReceiptIDC.Content = receipt;
            TotalCostValue.Content =totalCost;
            local_totalCost = totalCost;
            try
            {
                myRFIDReader = new RFID();
                myRFIDReader.Attach += MyRFIDReader_Attach;
                myRFIDReader.Detach += MyRFIDReader_Detach;
                myRFIDReader.Tag += MyRFIDReader_Tag;
            }
            catch (PhidgetException ex)
            {
                MessageBox.Show("Could not startup!");
            }
            elementsToBeCheckedOut = t;
            try
            {
                myRFIDReader.Open();
                if (!myRFIDReader.Attached)
                {
                    btnScanRFD.Text = "Connect the RFID!";
                    btnScanRFD.Background = Brushes.Red;

                }
                else
                {
                    btnScanRFD.Text = "Scanning RFID!";
                    btnScanRFD.Background = Brushes.Green;
                }

            }
            catch (PhidgetException ex)
            {
                MessageBox.Show("Could not connect to the RFID-Reader!");
            }











        }



        private void VisitorExistsInRFID(string rfidTag)
        {
            MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo);
            connection.Open();

            MySqlCommand command = new MySqlCommand(SessionData.retrieveClient(rfidTag), connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //MessageBox.Show(reader[2].ToString());
                NameC.Content = reader[1];
                UsernameC.Content = reader[3];
                GovIDC.Content = reader[0];
                DoBC.Content = reader[2];
                idClientReadFromRfid = Convert.ToInt32(reader[4]);
                moneyC.Content = reader[5].ToString() + "€";
                local_balance = (float)Convert.ToDouble(reader[5]);
                connection.Close();
            }


        }

        private void MyRFIDReader_Attach(object sender, AttachEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                lblRFIDDevice.Text = "RFID Device: Attached RFID-Reader";
                btnScanRFD.Text = "Scanning RFID!";
                btnScanRFD.Background = Brushes.Green;
            });
        }

        private void MyRFIDReader_Detach(object sender, DetachEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                lblRFIDDevice.Text = "RFID Device: Detached RFID-Reader";
                btnScanRFD.Text = "Connect RFID!";
                btnScanRFD.Background = Brushes.Red;
            });
        }

        private void MyRFIDReader_Tag(object sender, RFIDTagEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                
                rfidTag = e.Tag;
                VisitorExistsInRFID(e.Tag);

            });
        }
        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isRFIDScanning)
                {
                    myRFIDReader.Open();
                    RfidButton.Content = "Stop RFID Scanning";
                    isRFIDScanning = true;
                }

                else if (isRFIDScanning)
                {
                    myRFIDReader.Close();
                    lblRFIDDevice.Content = "RFID Device: Detached RFID-Reader";
                    lblRFID.Content = "RFID: Tag: " + rfidTag;
                    RfidButton.Content = "Start RFID Scanning";
                    isRFIDScanning = false;
                }
            }
            catch (PhidgetException ex)
            {
                MessageBox.Show("Could not connect to the RFID-Reader!");
            }
        }
        */

        private void getcurrentDateFromServer()
        {
            

            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT NOW() FROM DUAL", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {

                            if (reader.Read())
                            {
                                timeFromServer = Convert.ToDateTime(reader[0].ToString());
                                //MessageBox.Show(timeFromServer.ToString());
                            }
                        }
                    }
                    connection.Close();
                }
            }


        }
        private void insertIntoBuyLoanTable(ToCheckOutElement t, int q, DateTime date,int time)
        {
            if (q > 0)
            {
                using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.insertElementInBuyLoaned(idClientReadFromRfid.ToString(), t.savedElement.itemNo.ToString(), t.savedElement.typeOfTheElement.ToString(), q.ToString(), SessionData.latestReceipt.ToString(), SessionData.StandNo.ToString(), t.time, date, time.ToString()), connection))
                    {
                        //MessageBox.Show(SessionData.insertElementInBuyLoaned(idClientReadFromRfid.ToString(), t.savedElement.itemNo.ToString(), t.savedElement.typeOfTheElement.ToString(), q.ToString(), SessionData.latestReceipt.ToString(), SessionData.StandNo.ToString(),t.time, date,time.ToString()));
                        // MessageBox.Show(t.ToString() + "----" + q + "----" + SessionData.latestReceipt);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
        public bool checkBalance()
        {
            return ((local_balance - local_totalCost) > 0) ?true:false;
        }
        public void updateBalance()
        {
            float local = local_balance - local_totalCost;
            //MessageBox.Show(local.ToString());
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SessionData.update_balance(local,(int)idClientReadFromRfid), connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }
        public void updateQOH(int q,int id_s,int id_i)
        {
            float local = local_balance - local_totalCost;
            //MessageBox.Show(local.ToString());
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SessionData.update_qoh(id_i,id_s,q), connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }
        public Element retrieve_quantity_element(Element t)
        {
            Element tmp=null;
            elementsToBeCheckedOut.ForEach((x) => { if (x.savedElement.itemNo == t.itemNo) tmp= x.savedElement; });
            return tmp;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (idClientReadFromRfid != null)
            {
                if (checkBalance())
                {
                    
                        using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand(SessionData.insertReceipt(timeFromServer), connection))
                            {
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                        for (int i = 0; i < elementsToBeCheckedOut.Count; i++)
                        {
                            insertIntoBuyLoanTable(elementsToBeCheckedOut[i], elementsToBeCheckedOut[i].quantity, timeFromServer, elementsToBeCheckedOut[i].time);
                            updateQOH(retrieve_quantity_element(elementsToBeCheckedOut[i].savedElement).quantity, (int)SessionData.StandNo,elementsToBeCheckedOut[i].savedElement.itemNo);
                        }

                        SessionData.latestReceipt = GlobalFunctions.returnReceiptLastNr();
                        


                        updateBalance();
                        this.myRFIDReader.Close();
                        recalloutside();
                        this.Close();
                    }
                    
                

            }
        }



        public void onclosin_(object sender, CancelEventArgs e)
        {
            
            outer_reference.Visibility = Visibility.Hidden;
            this.myRFIDReader.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }
    }
}
