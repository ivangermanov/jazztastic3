﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(SessionData.ModifyEmployeeStatus);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(SessionData.ModifyEmployeeStatus);
            MainWindowFrame.NavigationService.Navigate(new Login());
            Timer t = new Timer(500);
            t.Elapsed += new ElapsedEventHandler(CheckInternetConnection);

            t.Start();
        }
        void resetApp()
        {
            if (!SessionData.connectedToDb)
            {
                MainWindowFrame.NavigationService.Navigate(new Login());
            }
        }
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            /*
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;

               
            }
            catch (PingException p)
            {
                // Discard PingExceptions and return false;
                MessageBox.Show(p.Message);
            }
            */



            Console.WriteLine(Dns.GetHostName());
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.MapToIPv4().ToString().Contains("192.168.30"))
                    pingable = true;
            }
            return pingable;
        }
        void CheckInternetConnection(object sender, ElapsedEventArgs e)
        {
            if (SessionData.changedStateConnection(PingHost("8.8.8.8")))
            {
                //MessageBox.Show("Error! No connection to the server!");
                Dispatcher.Invoke(() =>
                {
                    resetApp();
                });
            }


        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if(SessionData.scanQRPage != null) {
                if (SessionData.scanQRPage.finalFrame != null) {
                    SessionData.scanQRPage.finalFrame.Stop();
                }
            }
            Application.Current.Shutdown();
        }
    }
}
