using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Threading;
using Phidget22;
using Phidget22.Events;
using MySql.Data.MySqlClient;

namespace CheckInCheckOutApp
{
    /// <summary>
    /// Interaction logic for ScanQR.xaml
    /// </summary>
    public partial class ScanQR : Page
    {
        public delegate void QRTextChangedHandler();
        private FilterInfoCollection captureDevice;
        public VideoCaptureDevice finalFrame;
        System.Timers.Timer timerScanQR;
        System.Drawing.Image img2;
        MemoryStream ms;
        bool cameraOn;
        bool readingOn;
        bool normalAssignment;
        BarcodeReader reader;
        private RFID myRFIDReader;
        event QRTextChangedHandler qrTextChanged;
        bool isRFIDScanning;
        string QRText;
        string rfidTag;
        int? currentVisitor;

        private void StopCamera(object sender, EventArgs e)
        {
            if (finalFrame != null)
            {
                finalFrame.Stop();
            }

        }
        public ScanQR()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(StopCamera);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(StopCamera);
        }

        private void OpenRFIDScanner()
        {
            if (QRText == null || QRText == "")
            {
                try
                {
                    myRFIDReader.Close();
                    isRFIDScanning = false;
                    ChangeLabels();
                }
                catch (PhidgetException)
                {
                    MessageBox.Show("Could not connect to the RFID-Reader");
                }
            }
            else
            {
                try
                {
                    myRFIDReader.Open();
                    isRFIDScanning = true;
                    ChangeLabels();
                }
                catch (PhidgetException)
                {
                    MessageBox.Show("Could not connect to the RFID-Reader!");
                }
            }
        }
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            System.Drawing.Image img = (Bitmap)eventArgs.Frame.Clone();
            img2 = (Bitmap)eventArgs.Frame.Clone();
            ms = new MemoryStream();
            try
            {

                img.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    imgCamBox.Source = bi;
                }));
            }
            catch (Exception)
            {

            }
        }

        private void TimerScanQR_Tick(object sender, EventArgs e)
        {
            Result result = null;
            if (img2 != null)
                result = reader.Decode((Bitmap)img2);
            try
            {
                string decode = "";
                if (result != null)
                    decode = result.ToString();
                if (decode != "")
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        QRText = decode;
                        if (VisitorExistsInDB(QRText))
                        {
                            blackImg.Visibility = Visibility.Visible;
                            labelQR.Content = "QR Code: " + currentVisitor + ", " + QRText;
                            labelSuccess.Content = "Scan RFID";
                            labelSuccess.Foreground = new SolidColorBrush(Colors.Black);
                            qrTextChanged.Invoke();
                        }
                        else
                        {
                            QRText = null;
                            labelSuccess.Content = "Visitor doesn't exist!";
                            labelSuccess.Foreground = new SolidColorBrush(Colors.Red);
                            currentVisitor = null;
                        }
                        cameraOn = true;
                        readingOn = true;
                        ChangeLabels();
                        timerScanQR.Start();
                    });
                }
                timerScanQR.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (finalFrame != null)
                if (finalFrame.IsRunning)
                    finalFrame.Stop();
            finalFrame = null;
            if (timerScanQR != null)
                timerScanQR.Stop();
            if (ms != null)
                ms.Close();
            if (myRFIDReader != null)
                myRFIDReader.Close();
            if (captureDevice != null)
                captureDevice = null;
            cameraOn = false;
            readingOn = false;
            ChangeLabels();
            NavigationService.Navigate(SessionData.homePage);
        }

        private void ChangeLabels()
        {
            if (isRFIDScanning)
            {
                labelRFIDEnabled.Content = "On";
                labelRFIDEnabled.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (!isRFIDScanning)
            {
                labelRFIDEnabled.Content = "Off";
                labelRFIDEnabled.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void AttachRFID()
        {
            MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo);
            if (rfidTag != "" && rfidTag != null)
            {
                if (currentVisitor != null)
                {
                    try
                    {
                        string sql = $"SELECT tag, visitorNo FROM rfid WHERE tag = '{rfidTag}'";
                        MySqlCommand command = new MySqlCommand(sql, connection);
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();

                        //if rfid tag exists in database
                        if (reader.Read())
                        {
                            if (reader[1].ToString() == "")
                            {
                                sql = $"UPDATE rfid SET visitorNo = {currentVisitor} WHERE tag = '{rfidTag}'";
                                command.Dispose();
                                command = new MySqlCommand(sql, connection);
                                command.ExecuteNonQuery();
                                labelQR.Content = "QR: ";
                                SessionData.checkInPage.CheckInDB(rfidTag, currentVisitor);
                                QRText = null;
                                //qrTextChanged.Invoke();
                                labelSuccess.Content = "Visitor clear!";
                                labelSuccess.Foreground = new SolidColorBrush(Colors.Green);
                            }
                            else
                            {
                                MessageBox.Show("RFID already has user!");
                            }
                        }
                        else
                        {
                            sql = $"INSERT INTO rfid (tag, visitorNo) VALUES ('{rfidTag}', {currentVisitor})";
                            command.Dispose();
                            command = new MySqlCommand(sql, connection);
                            command.ExecuteNonQuery();
                            labelQR.Content = "QR: ";
                        }
                    }
                    catch (MySqlException)
                    {
                        MessageBox.Show("The visitor is already attached an RFID! Use reassign instead!");
                    }
                    finally
                    {
                        connection.Close();
                        currentVisitor = null;
                        blackImg.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void MyRFIDReader_Tag(object sender, RFIDTagEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                rfidTag = e.Tag;
                if (normalAssignment)
                    AttachRFID();
                else
                    ReattachRFID();
            });
        }

        private bool VisitorExistsInDB(string govtId)
        {
            MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo);
            string sql = $"SELECT visitorNo, governmentId FROM event_account WHERE governmentId = {govtId}";
            MySqlCommand command = new MySqlCommand(sql, connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                currentVisitor = Convert.ToInt32(reader[0]);
                connection.Close();
                return true;
            }
            connection.Close();
            return false;

        }

        private void ReattachRFID()
        {
            string governmentId = tbReassign.Text;
            MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo);
            if (governmentId != "")
            {
                try
                {
                    if (VisitorExistsInDB(governmentId))
                    {
                        connection.Open();
                        string sql = $"SELECT tag, visitorNo FROM rfid WHERE tag = '{rfidTag}'";
                        MySqlCommand command = new MySqlCommand(sql, connection);
                        MySqlDataReader reader = command.ExecuteReader();

                        MySqlTransaction mySqlTransaction;

                        //if rfid tag exists in database
                        if (reader.Read())
                        {
                            if (reader[1].ToString() == "")
                            {
                                reader.Dispose();
                                mySqlTransaction = connection.BeginTransaction();
                                command.Connection = connection;
                                command.Transaction = mySqlTransaction;
                                try
                                {
                                    command.CommandText = $"UPDATE rfid SET visitorNo = NULL WHERE visitorNo = {currentVisitor}";
                                    command.ExecuteNonQuery();
                                    command.CommandText = $"UPDATE rfid SET visitorNo = {currentVisitor} WHERE tag = '{rfidTag}'";
                                    command.ExecuteNonQuery();
                                    mySqlTransaction.Commit();
                                    labelSuccess.Content = "New RFID Attached!";
                                    labelSuccess.Foreground = new SolidColorBrush(Colors.Green);
                                    normalAssignment = true;
                                }
                                catch (Exception e)
                                {
                                    mySqlTransaction.Rollback();
                                }
                            }
                            else
                                MessageBox.Show("RFID already has user!");
                        }

                        //if rfid tag doesn't exist
                        else
                        {
                            reader.Dispose();
                            mySqlTransaction = connection.BeginTransaction();
                            command.Connection = connection;
                            command.Transaction = mySqlTransaction;
                            try
                            {
                                command.CommandText = $"UPDATE rfid SET visitorNo = NULL WHERE visitorNo = {currentVisitor}";
                                command.ExecuteNonQuery();
                                command.CommandText = $"INSERT INTO rfid (tag, visitorNo) VALUES('{rfidTag}', {currentVisitor})";
                                command.ExecuteNonQuery();
                                mySqlTransaction.Commit();
                                labelSuccess.Content = "New RFID Attached!";
                                labelSuccess.Foreground = new SolidColorBrush(Colors.Green);
                                normalAssignment = true;
                            }
                            catch (Exception e)
                            {
                                mySqlTransaction.Rollback();
                            }
                        }
                    }
                    else
                    {
                        labelSuccess.Content = "Visitor doesn't exist";
                        labelSuccess.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (InvalidDataException ex)
                {
                    MessageBox.Show("That RFID is already attached to a visitor!");
                }
                finally
                {
                    connection.Close();
                    QRText = null;
                    qrTextChanged.Invoke();
                    currentVisitor = null;
                    OpenRFIDScanner();
                    tbReassign.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
            else
                MessageBox.Show("Insert government ID first!");
        }

        private void tbReassign_GotFocus(object sender, RoutedEventArgs e)
        {
            timerScanQR.Stop();
            QRText = null;
            labelQR.Content = "QR Code:";
            rfidTag = null;
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            normalAssignment = false;
            if (!myRFIDReader.Attached)
                myRFIDReader.Open();
            labelRFIDEnabled.Content = "On";
            labelRFIDEnabled.Foreground = new SolidColorBrush(Colors.Green);
            labelSuccess.Content = "Scan new RFID";
            labelSuccess.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void cbChooseCamera_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            finalFrame = new VideoCaptureDevice(captureDevice[cbChooseCamera.SelectedIndex].MonikerString);
            finalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            finalFrame.Stop();
            finalFrame.Start();
            if (finalFrame.IsRunning)
            {
                cameraOn = true;
                ChangeLabels();
            }
        }

        private void CheckCameraAndScanningOn(object sender, AForge.Video.ReasonToFinishPlaying reason)
        {
            reason = ReasonToFinishPlaying.StoppedByUser;
            cameraOn = false;
            ChangeLabels();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            normalAssignment = true;
            captureDevice = null;
            finalFrame = null;
            reader = new BarcodeReader();
            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in captureDevice)
            {
                cbChooseCamera.Items.Add(device.Name);
            }
            cbChooseCamera.SelectedIndex = 0;

            finalFrame = new VideoCaptureDevice(captureDevice[cbChooseCamera.SelectedIndex].MonikerString);
            finalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            finalFrame.PlayingFinished += new PlayingFinishedEventHandler(CheckCameraAndScanningOn);
            finalFrame.Start();
            if (finalFrame.IsRunning)
            {
                cameraOn = true;
                ChangeLabels();
            }

            try
            {
                myRFIDReader = new RFID();
                myRFIDReader.Tag += MyRFIDReader_Tag;
            }
            catch (PhidgetException ex)
            {
                MessageBox.Show("Could not startup!");
            }

            labelSuccess.Content = "Scan QR Code";
            qrTextChanged += new QRTextChangedHandler(OpenRFIDScanner);

            timerScanQR = new System.Timers.Timer(500);
            timerScanQR.AutoReset = false;
            timerScanQR.Elapsed += TimerScanQR_Tick;
            timerScanQR.Start();
            readingOn = true;
            ChangeLabels();
        }

        private void tbReassign_LostFocus(object sender, RoutedEventArgs e)
        {
            timerScanQR.Start();
            rfidTag = null;
            QRText = null;
            tbReassign.Text = "";
            labelQR.Content = "QR Code:";
            rfidTag = null;
            normalAssignment = true;
            if (myRFIDReader.Attached)
                myRFIDReader.Close();
            OpenRFIDScanner();
        }
    }
}

