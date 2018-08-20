using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ManagementApplication.Pages;

namespace ManagementApplication {
    static class SessionData {

        //Properties
        public static string ConnectionInfo { get; } = "server=studmysql01.fhict.local;" +
                        "database=dbi397773;" +
                        "user id=dbi397773;" +
                        "password=eventimate;" +
                        "connect timeout=30;" +
                        "SslMode=none";
        public static List<Page> Pages { get; private set; }
        public static int EmployeeId { get; private set; }
        public static string EmployeeName { get; private set; }
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
        //Methods
        public static void InitializePages() {
            Pages = new List<Page>();
            Pages.Add(new DashboardPage());
            Pages.Add(new StandOverviewPage());
            Pages.Add(new EventOverviewPage());
            Pages.Add(new VisitorOverviewPage());
            Pages.Add(new ItemOverviewPage());
            Pages.Add(new ReceiptOverviewPage());
            Pages.Add(new MissingItemsOverviewPage());
        }

        public static void SetEmployeeDetails(int employeeId, string employeeName) {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
        }

        //SQL Statements
        //Login Page
        #region Login
        public static string LoginSQL(string employeeId, string password) {
            return $"SELECT employeeId, first_name, last_name FROM employee WHERE employeeId={employeeId} AND password='{password}'";
        }

        public static string LoginStatusUpdateSQL(string value) {
            return $"UPDATE employee SET loggedIn='{value}' WHERE employeeId={EmployeeId}";
        }
        #endregion

        //Stand Overview Page
        #region Stand Overview
        public static string StandOverviewGetStands(string type) {
            return $"SELECT standNo FROM stand WHERE stand_type='{type}'";
        }

        public static string StandDetailGetInfo(int standNo) {
            return $"SELECT stand_type FROM stand WHERE standNo={standNo}";
        }

        public static string StandDetailGetTotalRevenue(int standNo) {
            return $"SELECT SUM(i.price * a.quantity) FROM item i JOIN account_buy_loan a ON i.itemNo=a.itemNo WHERE a.standNo={standNo} AND i.type=a.item_type";
        }
    
        public static string StandDetailGetEmployees(int standNo) {
            return $"SELECT employeeId, first_name, last_name, hourly_wage from employee WHERE standNo={standNo}";
        }
        public static string StandDetailGetItems(int standNo) {
            return $"SELECT i.itemNo, i.name, i.price, s.QOH FROM item_stand s JOIN item i ON s.itemNo=i.itemNo WHERE s.standNo={standNo} AND i.type=s.type";
        }
        #endregion

        //Event Overview Page
        #region Event Overview
        public static string EventOverviewGetVisitorsTotal() {
            return "SELECT COUNT(*) FROM event_account;";
        }
        public static string EventOverviewGetVisitorsCheckedIn() {
            return "SELECT COUNT(*) FROM account_history ah1 WHERE `status` != 'Checked-out' AND `datetime` = (SELECT MAX(datetime) FROM account_history WHERE visitorNo = ah1.visitorNo);";
        }
        public static string EventOverviewGetVisitorsTotalBalance() {
            return "SELECT SUM(money) FROM event_account";
        }
        public static string EventOverviewGetCampspotsOccupied() {
            return "SELECT COUNT(*) FROM `camping_spot` WHERE spots_taken != 0;";
        }
        public static string EventOverviewGetCampspotsFree() {
            return "SELECT COUNT(*) FROM `camping_spot` WHERE spots_taken = 0;";
        }

        public static string EventOverviewGetVisitorChartData(string startDate, string endDate) {
            return $"SELECT COUNT(*) FROM account_history WHERE datetime BETWEEN '{startDate}' AND '{endDate}' AND status='Checked-in';";
        }

        public static string EventOverviewGetRevenueChartData(string startDate, string endDate) {
            return $"SELECT IFNULL(SUM(a.quantity*i.price), 0) FROM account_buy_loan a JOIN item i ON a.itemNo=i.itemNo WHERE a.item_type=i.type AND a.purchase_datetime BETWEEN '{startDate}' AND '{endDate}';";
        }
        
        public static string EventOverviewGetTotalRevenue() {
            return "SELECT SUM(i.price * abl.quantity) FROM item i JOIN item_stand ist ON(i.itemNo = ist.itemNo) AND(i.type = ist.type) JOIN account_buy_loan abl ON(ist.itemNo = abl.itemNo) AND(ist.type = abl.item_type) AND(ist.standNo = abl.standNo); ";
        }

        public static string EventOverviewGetTotalRevenueTickets() {
            return "SELECT type FROM event_ticket;";
        }

        public static string EventOveriewGetTotalRevenueCamping() {
            return $"SELECT SUM(IF(area_letter != 'A', spots_taken*20.00, spots_taken*20*1.10)) FROM `camping_spot`";
        }

        #endregion

        //Visitor Overview Page
        #region Visitor Overview
        public static string VisitorOverviewGetTopSpenders() {
            return $"SELECT a.visitorNo, e.last_name, SUM(a.quantity * i.price) visitorSum FROM account_buy_loan a JOIN item i ON a.itemNo=i.itemNo JOIN event_account e ON a.visitorNo=e.visitorNo WHERE a.item_type=i.type GROUP BY a.visitorNo ORDER BY visitorSum DESC LIMIT 5";
        }

        public static string VisitorOverviewSearch(string pattern) {
            return $"SELECT visitorNo, first_name, last_name FROM event_account WHERE visitorNo LIKE '%{pattern}%' OR first_name LIKE '%{pattern}%' OR last_name LIKE '%{pattern}%'";
        }

        public static string VisitorDetailGetInfo(int visitorNo) {
            return $"SELECT e.visitorNo, e.first_name, e.last_name, e.governmentId, e.money, e.dob, e.address, e.email, SUM(a.quantity * i.price) FROM event_account e JOIN account_buy_loan a ON e.visitorNo=a.visitorNo JOIN item i ON a.itemNo=i.itemNo WHERE e.visitorNo={visitorNo}";
        }
   
        public static string VisitorDetailsGetHistory(int visitorNo) {
            return $"SELECT datetime, status FROM account_history WHERE visitorNo={visitorNo} ORDER BY datetime";
        }

        public static string VisitorDetailGetItems(int visitorNo) {
            return $"SELECT a.itemNo, i.name, a.item_type, a.quantity, a.return_datetime FROM account_buy_loan a JOIN item i ON a.itemNo=i.itemNo WHERE visitorNo={visitorNo} AND a.item_type=i.type ORDER BY i.type, i.itemNo";
        }
        #endregion

        //Item Overview Page
        #region Item Overview
        public static string ItemOverviewGetTopProfit(string type) {
            return $"SELECT i.itemNo, i.name, SUM(a.quantity * i.price) itemSum FROM account_buy_loan a JOIN item i ON a.itemNo=i.itemNo WHERE a.item_type=i.type AND a.item_type='{type}' GROUP BY i.itemNo ORDER BY itemSum DESC LIMIT 5";
        }

        public static string ItemOverviewSearch(string pattern) {
            return $"SELECT itemNo, name, type FROM item WHERE itemNo LIKE '%{pattern}%' OR name LIKE '%{pattern}%'";
        }

        public static string ItemDetailGetInfo(int itemNo) {
            return $"SELECT * FROM item WHERE itemNo={itemNo}";
        }
        #endregion

        //Receipt Overview Page
        #region Receipt Overview
        public static string ReceiptOverviewSearch(string pattern) {
            return $"SELECT visitorNo, last_name, governmentId FROM event_account WHERE visitorNo LIKE '%{pattern}%' OR first_name LIKE '%{pattern}%' OR last_name LIKE '%{pattern}%'";
        }

        public static string ReceiptOverviewGetReceipts(int visitorNo) {
            return $"SELECT DISTINCT a.receiptNo, r.purchase_datetime FROM account_buy_loan a JOIN receipt r ON a.receiptNo=r.receiptNo WHERE a.visitorNo={visitorNo} ORDER BY r.purchase_datetime";
        }

        public static string ReceiptDetailGetInfo(int receiptNo) {
            return $"SELECT * FROM receipt WHERE receiptNo={receiptNo}";
        }

        public static string ReceiptDetailGetTotalPrice(int receiptNo) {
            return $"SELECT IFNULL(SUM(a.quantity*i.price), 0), a.visitorNo FROM receipt r JOIN account_buy_loan a ON r.receiptNo=a.receiptNo JOIN item i ON a.itemNo=i.itemNo WHERE a.receiptNo={receiptNo} AND a.item_type=i.type";
        }

        public static string ReceiptDetailsGetItems(int receiptNo) {
            return $"SELECT a.itemNo, i.name, i.price, a.item_type, a.return_datetime, a.quantity FROM receipt r JOIN account_buy_loan a ON r.receiptNo=a.receiptNo JOIN item i ON a.itemNo=i.itemNo WHERE a.receiptNo={receiptNo} AND a.item_type=i.type";
        }
        #endregion

        //Missing Items Overview Page
        #region Missing Item Overview
        public static string MissingItemsOverviewGetVisitors(string pattern) {
            return $"SELECT DISTINCT a.visitorNo, e.first_name, e.last_name FROM account_buy_loan a JOIN event_account e ON a.visitorNo=e.visitorNo WHERE a.visitorNo LIKE '%{pattern}%' OR e.first_name LIKE '%{pattern}%' OR e.last_name LIKE '%{pattern}%' OR e.governmentId LIKE '%{pattern}%' AND a.item_type='loan'";
        }

        public static string MissingItemsOVerviewGetItems(int visitorNo) {
            return $"SELECT a.itemNo, i.name, a.quantity, a.standNo, a.purchase_datetime, a.return_datetime, a.receiptNo FROM account_buy_loan a JOIN item i ON a.itemNo=i.itemNo WHERE a.visitorNo={visitorNo} AND a.item_type=i.type AND a.item_type='loan'";
        }
        #endregion
    }
}
