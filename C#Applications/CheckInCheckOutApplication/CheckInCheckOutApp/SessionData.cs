using System;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInCheckOutApp {
    public static class SessionData {

        public static MySqlConnection conn;
        public static bool isLoggedIn;
        public static Home homePage { get; private set; }
        public static ScanQR scanQRPage { get; private set; }
        public static CheckIn checkInPage { get; private set; }
        public static CheckOut checkOutPage { get; private set; }

        public static string EmployeeName { get; private set; }
        public static int? EmployeeId { get; private set; }

        public static void SetEmployeeName(string name) {
            EmployeeName = name;
        }

        public static void SetEmployeeId(int id) {
            EmployeeId = id;
        }
        public static bool connectedToDb;
        private static bool firstTime = true;
        public static bool changedStateConnection(bool comparison)
        {
            if (connectedToDb != comparison || firstTime)
            {
                firstTime = false;
                connectedToDb = comparison;
                return true;
            }
            else
                return false;
        }
        //SQL Connection Info

        private static string connectionInfo = "server=studmysql01.fhict.local;" +
                        "database=dbi397773;" +
                        "user id=dbi397773;" +
                        "password=eventimate;" +
                        "connect timeout=30;" +
                        "SslMode=none";

        public static string ConnectionInfo
        {
            get { return connectionInfo; }
        }

        //SQL Statements Methods
        public static string LoginSQL(string employeeId, string password) {
            return $"SELECT COUNT(*), first_name, employeeId FROM employee WHERE employeeId = {employeeId} AND password = '{password}'";
        }

        //Initialize pages
        public static void CreatePages() {
            homePage = new Home();
            scanQRPage = new ScanQR();
            checkInPage = new CheckIn();
            checkOutPage = new CheckOut();
        }

        //Clear all session data
        public static void ClearSessionData() {
            EmployeeName = null;
            EmployeeId = null;
            homePage = null;
            scanQRPage = null;
            checkInPage = null;
            checkOutPage = null;
        }

        //Update employee logged in status in database
        public static void ModifyEmployeeStatus()
        {
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
            {
                connection.Open();
                if (isLoggedIn)
                {
                    string sql = $"UPDATE employee SET loggedIn='f' WHERE employeeId = {SessionData.EmployeeId};";
                    using (MySqlCommand commandUpdate = new MySqlCommand(sql, connection))
                    {
                        commandUpdate.ExecuteNonQuery();
                    }
                    isLoggedIn = false;
                }
                else
                {
                    string sql = $"UPDATE employee SET loggedIn='t' WHERE employeeId = {SessionData.EmployeeId};";
                    using (MySqlCommand commandUpdate = new MySqlCommand(sql, connection))
                    {
                        commandUpdate.ExecuteNonQuery();
                    }
                    isLoggedIn = true;
                }
                connection.Close();
            }
        }
        public static void ModifyEmployeeStatus(object sender, EventArgs e)
        {
            if (EmployeeId != null)
            { 
                using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
                {
                    connection.Open();
                    if (SessionData.EmployeeId != null)
                    {
                        string sql = $"UPDATE employee SET loggedIn='f' WHERE employeeId = {SessionData.EmployeeId};";
                        using (MySqlCommand commandUpdate = new MySqlCommand(sql, connection))
                        {
                            commandUpdate.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string sql = $"UPDATE employee SET loggedIn='t' WHERE employeeId = {SessionData.EmployeeId};";
                        using (MySqlCommand commandUpdate = new MySqlCommand(sql, connection))
                        {
                            commandUpdate.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }
            }
        }

        public static int VisitorExistsInRFID(string rfidTag)
        {
            MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo);
            string sql = $"SELECT visitorNo FROM rfid WHERE tag = '{rfidTag}' AND visitorNo IS NOT NULL";
            MySqlCommand command = new MySqlCommand(sql, connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int currentVisitor = Convert.ToInt32(reader[0]);
                connection.Close();
                return currentVisitor;
            }
            connection.Close();
            return -1;
        }

        public static bool VisitorCheckedIn(string rfidTag)
        {
            MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo);

            string sql = $"SELECT ah.visitorNo FROM account_history ah " +
                         $"JOIN rfid r ON (ah.visitorNo = r.visitorNo) " +
                         $"WHERE datetime = (SELECT MAX(datetime) " +
                                           $"FROM account_history " +
                                           $"WHERE visitorNo = ah.visitorNo) " +
                         $"AND r.tag = '{rfidTag}' " +
                         $"AND ah.status = 'Checked-in'; ";
            MySqlCommand command = new MySqlCommand(sql, connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                connection.Close();
                return true;
            }
            connection.Close();
            return false;
        }
    }
}
