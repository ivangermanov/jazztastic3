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
using MySql.Data.MySqlClient;
using Phidget22;
using Phidget22.Events;

namespace ShoppingStand
{
    /// <summary>
    /// Interaction logic for Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {
        private RFID myRFIDReader;
       // bool isRFIDScanning;
        string rfidTag;
        int? idClientReadFromRfid;
        DateTime timeFromServer;
        public delegate void reloadElementsPage();
        public event reloadElementsPage receiptFinished;
        int localTotal;
        int balance_user;
        List<Element> temporary_reference;
        public Checkout(int total,List<Element> _placeholder)
        {

            InitializeComponent();
            temporary_reference = _placeholder;
            TotalCostValue.Content = total+" €";
            localTotal = total;
            DateC.Content = DateTime.Now+" ";
            ReceiptIDC.Content = SessionData.latestReceipt;
            TotalCostValue.Content = total.ToString();
            DateC.Content = timeFromServer;
            ReceiptIDC.Content = SessionData.latestReceipt;
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



            try
            {
                
                    myRFIDReader.Open();
                if (!myRFIDReader.Attached)
                {
                    btnScanRFD.Text = "Connect the RFID!";
                    btnScanRFD.Background = Brushes.Red ;

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
               
                NameC.Content = reader[1];
                UsernameC.Content = reader[3];
                GovIDC.Content = reader[0];
                DoBC.Content = reader[2];
                idClientReadFromRfid = Convert.ToInt32(reader[4]);
                moneyC.Content = reader[5].ToString() + "€";
                balance_user = Convert.ToInt32(reader[5]);
            }
                connection.Close();
           

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






















        

        private void getcurrentDateFromServer()
        {
                using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT SYSDATE()", connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                timeFromServer = Convert.ToDateTime(reader[0].ToString());
                            }
                        }
                    connection.Close();
                    }
                }
           
            
        }
        private void insertIntoBuyLoanTable(Element t,int q, DateTime date)
        {
            
            
            if (q > 0)
            {
                using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.insertElementInBuyLoaned(idClientReadFromRfid.ToString(), t.itemNo.ToString(), t.typeOfTheElement.ToString(), q.ToString(), SessionData.latestReceipt.ToString(), SessionData.IdStand.ToString(), date), connection))
                    {
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
           
        }
        private int returnCount(Element t, List<Element> listElements,int superiorLimit)
        {
            int counter = 0;

            for (int k = 0; k < superiorLimit; k++)
            {
                if (t.itemNo == listElements[k].itemNo)
                {
                    counter++;
                }
            }


            return counter;
        }
        public bool checkBalance()
        {
            return ((balance_user - localTotal) > 0) ? true : false;
        }
        public void updateBalance()
        {
            float local = balance_user - localTotal;
            //MessageBox.Show(local.ToString());
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SessionData.update_balance(local, (int)idClientReadFromRfid), connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }
        public void updateQOH(int q, int id_s, int id_i)
        {
            
            
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SessionData.update_qoh(id_i, id_s, q), connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }
        private int return_q(int id)
        {
            foreach (Element f in temporary_reference)
            {
                if (f.itemNo == id)
                    return f.qoh;
            }
            return -1;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
               
                if (idClientReadFromRfid != null)
                {
                    if (checkBalance())
                    {
                        getcurrentDateFromServer();
                        using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
                        {
                            connection.Open();
                            using (MySqlCommand command = new MySqlCommand(SessionData.insertReceipt(timeFromServer), connection))
                            {

                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                        for (int i = 0; i < Logic.CheckOutList.Count; i++)
                        {
                            if (returnCount(Logic.CheckOutList[i], Logic.CheckOutList, i) == 0)
                            {
                           
                                insertIntoBuyLoanTable(Logic.CheckOutList[i], returnCount(Logic.CheckOutList[i], Logic.CheckOutList, Logic.CheckOutList.Count), timeFromServer);
                                updateQOH(return_q(Logic.CheckOutList[i].itemNo), SessionData.IdStand,Logic.CheckOutList[i].itemNo);
                            }

                    }

                        SessionData.latestReceipt++;
                        SessionData.CreateANewElementsPage();
                        updateBalance();
                        receiptFinished();
                        closeThePage();
                    }
                    

                }
                else
                {
                    MessageBox.Show("Error!:First put an RFID tag on the reader!", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            receiptFinished();
            closeThePage();
        }
        private  void closeThePage()
        {
            receiptFinished();
            myRFIDReader.Close();
            this.Close();
        }
       
    }
}
