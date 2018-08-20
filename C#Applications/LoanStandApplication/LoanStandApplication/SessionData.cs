using System;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanStandApplication {
    public static class SessionData {
        public static MySqlConnection conn;
        public static LoanStatus LoanStatusPage { get; private set; }
        public static home LoanShopPage { get; private set; }
        public static ItemInfo ItemInfoPage { get; private set; }
        public static Dashboard DashboardPage { get; private set; }
        public static Checkout CheckoutPage { get; private set; }

        public static string EmployeeName { get; private set; }
        public static int? EmployeeId { get; private set; }
        public static int? StandNo { get; private set; }

        public static void SetEmployeeName(string name) {
            EmployeeName = name;
        }

        public static void SetEmployeeId(int id) {
            EmployeeId = id;
        }

        public static void SetStand(int standNo) {
            StandNo = standNo;
        }
        public static string shop_type = "loan";
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

        //public static int IdStand { get; set; }

        public static string update_qoh(int id_item,int id_shop,int quantity)
        {
            return $"UPDATE `item_stand` SET `QOH` = {quantity} WHERE `item_stand`.`itemNo` = {id_item} AND `item_stand`.`type` = 'loan' AND `item_stand`.`standNo` = {id_shop} AND `item_stand`.`stand_type` = 'loan';";
        }
        public static string RetrieveStand(int standNo)
        {
            return $"SELECT stand_type FROM stand WHERE stand.standNo={standNo}";
        }
        public static string ScanSQL(string TagRFID)
        {
            return $"SELECT first_name, last_name, governmentId, dob, email FROM event_account WHERE visitorNo = '{TagRFID}'";
        }
        //Start Database connection
        public static int latestReceipt;
        public static string getTheLatestReceipt()
        {
            return "SELECT MAX(receipt.receiptNo) FROM receipt";
        }
        public static string insertElementInBuyLoaned(string visitorN,
            string itemN,
            string itemType,
            string quantity,
            string receiptNo,
            string standN,
            int timeToadd,
            DateTime datePurchase,
            string returnDate = null)
        {
            return $"INSERT INTO account_buy_loan (visitorNo, itemNo, item_type,standNo,purchase_datetime,return_datetime,quantity,receiptNo)VALUES({visitorN},{itemN},'{itemType}',{standN}, '{datePurchase.ToString("yyyy-MM-dd HH:mm:ss")}', '{datePurchase.AddDays(timeToadd).ToString("yyyy-MM-dd HH:mm:ss")}',{quantity},{receiptNo});";
        }
        public static string retrieveClient(string rfid)
        {
            return $"SELECT event_account.governmentId,event_account.first_name,event_account.dob,event_account.last_name,event_account.visitorNo,money FROM rfid JOIN event_account ON rfid.visitorNo=event_account.visitorNo WHERE tag='{rfid}'";
        }
        public static string insertReceipt(DateTime datePurchase)
        {
            return $"INSERT INTO receipt (employeeId, purchase_datetime, receiptNo)VALUES({EmployeeId}, '{datePurchase.ToString("yyyy-MM-dd HH:mm:ss")}', {latestReceipt}); ";
        }

        public static string update_balance(float money,int visitor)
        {
            return $"UPDATE `event_account` SET `money` = '{money}' WHERE `event_account`.`visitorNo` = {visitor};";
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
            return $"SELECT COUNT(*), first_name, employeeId, standNo FROM employee WHERE employeeId = {employeeId} AND password = '{password}'";
        }

        //Loan Status
        public static string ScanSQL(int visitorNo) {
            return $"SELECT first_name, last_name, governmentId, dob, email FROM event_account WHERE visitorNo = '{visitorNo}'";
        }
        public static string GetLoanedSQL(int visitorNo) {
            return $"SELECT DISTINCT a.itemNo, a.standNo, a.purchase_datetime, a.return_datetime, a.quantity, i.name FROM account_buy_loan a JOIN item i ON a.itemNo=i.itemNo WHERE a.item_type='loan' AND a.visitorNo={visitorNo}";
        }

        public static string ReturnItemSQL(int visitorNo, int itemNo, int standNo, string purchase, string returned) {
            return $"DELETE FROM account_buy_loan WHERE item_type='loan' AND visitorNo={visitorNo} AND itemNo={itemNo} AND standNo={standNo} AND purchase_datetime='{purchase}' AND return_datetime='{returned}'";
        }

        public static string GetVisitorNoSQL(string tag) {
            return $"SELECT visitorNo FROM rfid WHERE tag='{tag}'";
        }

        public static string GetStockItem(int standNo, int itemNo) {
            return $"SELECT QOH FROM item_stand WHERE standNo={standNo} AND itemNo={itemNo}";
        }

        public static string UpdateStockSQL(int itemNo, int standNo, int quantity) {
            return $"UPDATE item_stand SET QOH={quantity} WHERE itemNo={itemNo} AND standNo={standNo}";
        }

        public static string UpdateItemRecord(int visitorNo, int itemNo, int standNo, string purchase, string returned, int quantity) {
            return $"UPDATE account_buy_loan SET quantity={quantity} WHERE item_type='loan' AND visitorNo={visitorNo} AND itemNo={itemNo} AND standNo={standNo} AND purchase_datetime='{purchase}' AND return_datetime='{returned}'";
        }

        //Unknown
        public static string Elements()
        {
            //SELECT * FROM item JOIN item_stand ON item.itemNo=item_stand.itemNo AND item.type=item_stand.type AND item_stand.standNo='6' WHERE  item.type='loan'
            //old
            //SELECT * FROM item WHERE item.itemNo IN ( SELECT item_stand.itemNo FROM item_stand WHERE item_stand.standNo='{StandNo}') AND item.type='loan'
            return $"SELECT * FROM item JOIN item_stand ON item.itemNo=item_stand.itemNo AND item.type=item_stand.type AND item_stand.standNo='{StandNo}' WHERE  item.type='loan'";
        }

        //Start Database connection

        //Initialize pages
        public static void CreatePages() {
            LoanStatusPage = new LoanStatus();
            LoanShopPage = new home();
           
            DashboardPage = new Dashboard();
            //CheckoutPage = new Checkout(null);
        }

        //Clear all session data
        public static void ClearSessionData() {
            EmployeeName = null;
            EmployeeId = null;
            StandNo = null;
            LoanStatusPage = null;
            ItemInfoPage = null;
            DashboardPage = null;
            CheckoutPage = null;
        }
    }
}
