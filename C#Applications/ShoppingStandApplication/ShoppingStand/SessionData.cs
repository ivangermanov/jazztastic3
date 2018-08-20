using System;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStand {
    public static class SessionData {

        public static string shop_type="buy";
        public static bool connectedToDb;
        private static bool firstTime=true;
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
        public static MySqlConnection conn;

        public static int IdStand{ get; set; }
        
        public static Dashboard DashboardPage { get; private set; }
        public static Checkout CheckoutPage { get; private set; }

        public static home ElementsPage { get; private set; }

        public static string EmployeeName { get; private set; }
        public static int? EmployeeId { get; private set; }

        public static void SetEmployeeName(string name) {
            EmployeeName = name;
        }

        public static void SetEmployeeId(int id) {
            EmployeeId = id;
        }

        //SQL Connection Info

        public static string connectionInfo = "server=studmysql01.fhict.local;" +
                        "database=dbi397773;" +
                        "user id=dbi397773;" +
                        "password=eventimate;" +
                        "connect timeout=30;" +
                        "SslMode=none";

        //SQL Statements Methods
        public static string LoginSQL(string employeeId, string password) {
            return $"SELECT COUNT(*), first_name, employeeId,standNo  FROM employee WHERE employeeId = {employeeId} AND password = '{password}'";
        }
        public static string RetrieveStand(string standNo)
        {
            return $"SELECT stand_type FROM stand WHERE stand.standNo={standNo}";
        }
        public static string Elements()
        {
            //return $"SELECT * FROM item WHERE item.itemNo IN ( SELECT item_stand.itemNo FROM item_stand WHERE item_stand.standNo={IdStand}) AND item.type='buy'";
            return $"SELECT * FROM item JOIN item_stand ON item.itemNo=item_stand.itemNo AND item.type=item_stand.type AND item_stand.standNo='{IdStand}' WHERE  item.type='buy'";
        }
        public static string ScanSQL(string TagRFID) {
            return $"SELECT first_name, last_name, governmentId, dob, email FROM event_account WHERE visitorNo = '{TagRFID}'";
        }
        //Start Database connection
        public static int latestReceipt;
        public static string getTheLatestReceipt()
        {
            return "SELECT MAX(receipt.receiptNo) FROM receipt";
        }

        public static string update_qoh(int id_item, int id_shop, int quantity)
        {
            return $"UPDATE `item_stand` SET `QOH` = {quantity} WHERE `item_stand`.`itemNo` = {id_item} AND `item_stand`.`type` = 'buy' AND `item_stand`.`standNo` = {id_shop} AND `item_stand`.`stand_type` = 'buy';";
        }
        public static string update_balance(float money, int visitor)
        {
            return $"UPDATE `event_account` SET `money` = '{money}' WHERE `event_account`.`visitorNo` = {visitor};";
        }
        public static string GetStockItem(int standNo, int itemNo)
        {
            return $"SELECT QOH FROM item_stand WHERE standNo={standNo} AND itemNo={itemNo}";
        }

        public static string UpdateStockSQL(int itemNo, int standNo, int quantity)
        {
            return $"UPDATE item_stand SET QOH={quantity} WHERE itemNo={itemNo} AND standNo={standNo}";
        }


        public static string insertElementInBuyLoaned(string visitorN,
            string itemN,
            string itemType,
            string quantity,
            string receiptNo,
            string standN,
            DateTime datePurchase,
            string returnDate = null)
        {
            return $"INSERT INTO account_buy_loan (visitorNo, itemNo, item_type,standNo,purchase_datetime,return_datetime,quantity,receiptNo)VALUES({visitorN},{itemN},'{itemType}',{standN}, '{datePurchase.ToString("yyyy-MM-dd HH:mm:ss")}', 0, {quantity},{receiptNo});";
        }
        public static string retrieveClient(string rfid)
        {
            return $"SELECT event_account.governmentId,event_account.first_name,event_account.dob,event_account.last_name,event_account.visitorNo,money FROM rfid JOIN event_account ON rfid.visitorNo=event_account.visitorNo WHERE tag='{rfid}'";
        }
        public static string insertReceipt(DateTime datePurchase)
        {
            return $"INSERT INTO receipt (employeeId, purchase_datetime, receiptNo)VALUES({EmployeeId}, '{datePurchase.ToString("yyyy-MM-dd HH:mm:ss")}', {latestReceipt}); ";
        }
        public static void CreateANewElementsPage()
        {
            ElementsPage = new home();
        }
      
        //Initialize pages
        public static void CreatePages() {
            ElementsPage = new home();
            DashboardPage = new Dashboard();
          
        }

        //Clear all session data
        public static void ClearSessionData() {
            EmployeeName = null;
            EmployeeId = null;
            ElementsPage = null;
            DashboardPage = null;
           
        }
    }
}
