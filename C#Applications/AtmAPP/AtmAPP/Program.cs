using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AtmAPP {
    class Program {


        private static object lockableobject = new object();


        private static List<string> proccesed_files = new List<string>();
        private static List<ATMLog> processed_logs = new List<ATMLog>();


        private static readonly List<string> toProcessFiles = new List<string>();


        private static List<string> processingFiles = new List<string>();


        private static int logNr = Convert.ToInt32(File.ReadAllLines(config)[0]);
        private const string dir = @"ATM Logs";
        private const string config = @"ATM Logs/config.txt";

        private const string connectionInfo = "server=studmysql01.fhict.local;" +
                        "database=dbi397773;" +
                        "user id=dbi397773;" +
                        "password=eventimate;" +
                        "connect timeout=30;" +
                        "SslMode=none";


        private const string type = @"*.txt";
        static void Main(string[] args) {
            FileSystemWatcher f = new FileSystemWatcher();
            f.Path = dir;
            f.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                             | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            f.Filter = type;
            f.Created += oncreatedFile;
            f.EnableRaisingEvents = true;
            Console.WriteLine(logNr);
            /*
            Timer manualWatcher = new Timer(watcherManual, null, 0, 3000);
            */
            Timer manualTaskRunner = new Timer(manualRunner, null, 0, 10000);

            Console.ReadLine();
        }

        private static void oncreatedFile(object sender, FileSystemEventArgs e) {
            lock (lockableobject) {
                toProcessFiles.Add(e.FullPath);
                Console.WriteLine("add from watcher: " + e.FullPath);
                File.WriteAllText(config, (++logNr).ToString());
            }

        }
        /*
        private static void watcherManual(Object o)
        {
            var files = Directory.GetFiles(dir, type);
            lock (lockableobject)
            {
                foreach (var file in files)
                {
                    if (!proccesed_files.Contains(file) && !toProcessFiles.Contains(file) && !processingFiles.Contains(file))
                    {
                        toProcessFiles.Add(file);
                        Console.WriteLine("add from manual timer: " + file);
                    }
                }

            }
        }
        */
        private static void processStuff(string fileToProcces) {
            Console.WriteLine("Processing:" + fileToProcces);
            lock (lockableobject) {
                processingFiles.Remove(fileToProcces);

                //Processing
                ATMLog temp = new ATMLog(fileToProcces);

                ProcessData(connectionInfo, temp.Visitors, temp.StartTime, temp.EndTime, temp.NumberOfDeposits);

                processed_logs.Add(temp);


                //Console.WriteLine(File.ReadAllLines(fileToProcces)[0]);
                proccesed_files.Add(fileToProcces);
            }
        }
        private static bool processing;
        private static void manualRunner(Object o) {
            if (processing)
                return;

            while (true) {

                string fileToProcces = null;

                lock (lockableobject) {
                    fileToProcces = toProcessFiles.FirstOrDefault();
                    if (fileToProcces != null) {
                        processing = true;
                        toProcessFiles.Remove(fileToProcces);
                        processingFiles.Add(fileToProcces);
                    } else {
                        processing = false;
                        break;


                    }
                }

                if (fileToProcces == null) {
                    return;
                }


                processStuff(fileToProcces);
            }
        }
        private static void ProcessData(string connectionInfo, List<Visitor> visitors, string startTime, string endTime, string numberOfDeposits) {
            try {
                using (MySqlConnection connection = new MySqlConnection(connectionInfo)) {
                    connection.Open();
                    CreateLog(connection, startTime, endTime, numberOfDeposits);
                    UpdateBalances(connection, visitors);
                    CreateAccountLog(connection, visitors);
                    Console.WriteLine("Information has been sent to the database!");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        private static void CreateLog(MySqlConnection connection, string startTime, string endTime, string numberOfDeposits) {
            string createLogSQL = $"INSERT INTO log (logNo, start_time, end_time, number_deposits) VALUES ({logNr}, '{startTime}', '{endTime}', {numberOfDeposits})";
            using (MySqlCommand command = new MySqlCommand(createLogSQL, connection)) {
                command.ExecuteNonQuery();
            }
        }

        private static void UpdateBalances(MySqlConnection connection, List<Visitor> visitors) {
            foreach (Visitor v in visitors) {
                //Get current balance from database and add it to the visitor instance
                string getVisitorBalanceSQL = $"SELECT money FROM event_account WHERE visitorNo={v.VisitorNo}";
                using (MySqlCommand command = new MySqlCommand(getVisitorBalanceSQL, connection)) {
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        if (reader.HasRows) {
                            v.AddBalance(Convert.ToDouble(reader[0]));
                        }
                    }
                }
                //Update value in database with updated balanced
                string updateVisitorBalanceSQL = $"UPDATE event_account SET money = {v.Balance} WHERE visitorNo={v.VisitorNo}";
                using (MySqlCommand command = new MySqlCommand(updateVisitorBalanceSQL, connection)) {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void CreateAccountLog(MySqlConnection connection, List<Visitor> visitors) {
            foreach(Visitor v in visitors) {
                string createAccountLogSQL = $"INSERT INTO account_log (visitorNo, logNo, money_deposited) VALUES ({v.Record[0]}, {logNr}, {v.Record[1]})";
                using (MySqlCommand command = new MySqlCommand(createAccountLogSQL, connection)) {
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
