using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jazztastic3ASPXWebForms
{
    public partial class UserPanel : System.Web.UI.Page
    {
        string connectionInfo = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        string visitorNoBackEnd;
        string firstNameBackEnd;
        string lastNameBackEnd;
        string emailBackEnd;
        string governmentIdBackEnd;
        string ticketType;
        string dobBackEnd;
        protected string campingSpotBackEnd;
        protected string areaLetterBackEnd;
        protected string spotsTakenBackEnd;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (Convert.ToBoolean(Session["LoggedIn"]))
            {
                loginNav.InnerHtml = "Log Out";
                loginNav.HRef = "Logout.aspx";
                //userInfo.InnerHtml = DisplayVisitorInfo();
                DisplayVisitorInfo();
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }
        }

        private void DisplayVisitorInfo()
        {
            bool hasCamping = false;
            string sql = $"SELECT et.visitorNo, et.area_letter, et.spotNo, cs.spots_taken FROM event_ticket et JOIN camping_spot cs ON et.area_letter = cs.area_letter AND et.spotNo = cs.spotNo WHERE et.visitorNo = {Session["VisitorNo"]}";
            using (MySqlConnection connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            hasCamping = true;
                    }
                }

                if (hasCamping)
                {
                    sql = $"SELECT e.visitorNo, e.governmentId, e.first_name, e.last_name, e.dob, e.email, e.money, et.type, et.area_letter, et.spotNo, cs.spots_taken FROM event_account e JOIN event_ticket et ON e.visitorNo=et.visitorNo JOIN camping_spot cs ON (et.area_letter = cs.area_letter AND et.spotNo = cs.spotNo) WHERE e.visitorNo={Session["VisitorNo"]}";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            if (reader.HasRows)
                            {
                                visitorNo.InnerHtml = $"{reader[0]}";
                                visitorNoBackEnd = reader[0].ToString();
                                governmentId.InnerHtml = $"{reader[1]}";
                                governmentIdBackEnd = reader[1].ToString();
                                firstName.InnerHtml = $"{reader[2]}";
                                firstNameBackEnd = reader[2].ToString();
                                lastName.InnerHtml = $"{reader[3]}";
                                lastNameBackEnd = reader[3].ToString();
                                DateTime dobDatetime = Convert.ToDateTime(reader[4]);
                                dob.InnerHtml = $"{dobDatetime.ToShortDateString()}";
                                dobBackEnd = dobDatetime.ToShortDateString();
                                email.InnerHtml = $"{reader[5]}";
                                emailBackEnd = reader[5].ToString();
                                money.InnerHtml = $"{reader[6]}";
                                if (reader[7].ToString() == "TTT")
                                {
                                    Session["TicketType"] = true;
                                }
                                else
                                {
                                    Session["TicketType"] = false;
                                }
                                ticket.InnerHtml = $"{Ticket.GetTicketDaysText(reader[7].ToString())}";
                                ticketType = reader[7].ToString();
                                areaLetterBackEnd = reader[8].ToString();
                                campingSpotBackEnd = reader[9].ToString();
                                spotsTakenBackEnd = reader[10].ToString();
                                camping.InnerHtml = $"{areaLetterBackEnd}, {campingSpotBackEnd} with {spotsTakenBackEnd} people";
                            }
                        }
                    }
                }
                else
                {
                    sql = $"SELECT e.visitorNo, e.governmentId, e.first_name, e.last_name, e.dob, e.email, e.money, et.type FROM event_account e JOIN event_ticket et ON e.visitorNo=et.visitorNo WHERE e.visitorNo={Session["VisitorNo"]}";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            if (reader.HasRows)
                            {
                                visitorNo.InnerHtml = $"{reader[0]}";
                                visitorNoBackEnd = reader[0].ToString();
                                governmentId.InnerHtml = $"{reader[1]}";
                                governmentIdBackEnd = reader[1].ToString();
                                firstName.InnerHtml = $"{reader[2]}";
                                firstNameBackEnd = reader[2].ToString();
                                lastName.InnerHtml = $"{reader[3]}";
                                lastNameBackEnd = reader[3].ToString();
                                DateTime dobDatetime = Convert.ToDateTime(reader[4]);
                                dob.InnerHtml = $"{dobDatetime.ToShortDateString()}";
                                dobBackEnd = dobDatetime.ToShortDateString();
                                email.InnerHtml = $"{reader[5]}";
                                emailBackEnd = reader[5].ToString();
                                money.InnerHtml = $"{reader[6]}";
                                if (reader[7].ToString() == "TTT")
                                {
                                    Session["TicketType"] = true;
                                }
                                else
                                {
                                    Session["TicketType"] = false;
                                }
                                ticket.InnerHtml = $"{Ticket.GetTicketDaysText(reader[7].ToString())}";
                                ticketType = reader[7].ToString();
                                areaLetterBackEnd = null;
                                campingSpotBackEnd = null;
                                spotsTakenBackEnd = null;
                                camping.InnerHtml = "No camping";
                            }
                        }
                    }
                }
            }
        }



        private void AddMoney(decimal money)
        {
            if (money > 0)
            {
                string sql = $"UPDATE event_account SET money = money + { money } WHERE visitorNo = { Session["VisitorNo"] }";
                using (MySqlConnection connection = new MySqlConnection(connectionInfo))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    DisplayVisitorInfo();
                }
            }
        }

        void SendMail()
        {
            using (Attachment attachment = GenerateTicket.GetAttachmentAsPDF(Convert.ToInt64(visitorNoBackEnd), firstNameBackEnd, lastNameBackEnd, governmentIdBackEnd, ticketType, campingSpotBackEnd, areaLetterBackEnd))
            {
                //generate new ticket pdf and get it as attachment

                //initialize variables for email sending
                string emailSender = ConfigurationManager.AppSettings["username"].ToString();
                string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
                bool emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                string mailText = $"Dear {firstNameBackEnd} {lastNameBackEnd},\nCongratulations!\nYou have successfully extended your ticket for Jazztastic 3! The event is going to be held" +
                    $" from 01/07/2018 until 03/07/2018. We are excited to have you on board and expect to see you there! Don't forget your ticket, and more importantly, don't forget - Jazz on!";
                string subject = "Jazztastic 3 - Successful extension of ticket";

                //get path for ticket folder
                string path = Server.MapPath($"~/Resources/tickets/ticket{visitorNoBackEnd}.pdf");
                //Generate a new ticket attachment by using the visitor's values

                //base class for sending mail
                MailMessage mailMessage = new MailMessage();
                //set mail "From" ID
                mailMessage.From = new MailAddress(emailSender);
                //set mail "To" ID
                mailMessage.To.Add(emailBackEnd);
                //set mail "Subject" field
                mailMessage.Subject = subject;
                //add attachment pdf ticket to mail "Attachments"
                mailMessage.Attachments.Add(attachment);
                //add mail "Body" field
                mailMessage.Body = mailText;
                //Set the SMTP protocol
                SmtpClient smtp = new SmtpClient();
                //Set HOST server SMTP detail
                smtp.Host = emailSenderHost;
                //Set PORT number of SMTP detail
                smtp.Port = emailSenderPort;
                //Set SSL to true/false
                smtp.EnableSsl = emailIsSSL;

                //Set sender email and password
                NetworkCredential networkCredential = new NetworkCredential(emailSender, emailSenderPassword);
                smtp.Credentials = networkCredential;

                //Send method which sends the email message (created a bit above)
                smtp.Send(mailMessage);

                mailMessage.Dispose();
                smtp.Dispose();
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnAddMoney_ServerClick(object sender, EventArgs e)
        {
            string money = Request["tbBalance"];
            //test.InnerHtml = $"Added {money}";
            AddMoney(Convert.ToDecimal(money));
        }

        private void UpdateTicket(string ticketDateModified, string ticketDate)
        {
            string sql = $"UPDATE event_ticket SET type = '{ ticketDateModified }' WHERE visitorNo = { Session["VisitorNo"] }";
            using (MySqlConnection connection = new MySqlConnection(connectionInfo))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
                DisplayVisitorInfo();
            }
            if (Ticket.TicketAvailable(ticketDate, out string errorMessage))
            {
                Ticket.ReduceTicketQuantityBy1(ticketDate);
                SendMail();
            }
        }

        protected void btnExtendTicket_ServerClick(object sender, EventArgs e)
        {
            string ticketDate = Request["tbTicketDate"];
            string ticketDateModified = $"{ticket.InnerText}{ticketDate}";
            ticketDateModified = Ticket.GetTicketType(ticketDateModified);
            ticketDate = Ticket.GetTicketType(ticketDate);
            //test.InnerHtml = $"Extended {ticketDate}";
            UpdateTicket(ticketDateModified, ticketDate);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void DownloadTicket_ServerClick(object sender, EventArgs e)
        {
            string path = Server.MapPath($"~/Resources/tickets/ticket{Session["VisitorNo"]}.pdf");
            path += "Ђticket" + Session["VisitorNo"] + ".pdf";
            Response.Redirect($"DownloadTicket.ashx?path={path}");
        }

        protected void Subscribe_Click(object sender, EventArgs e)
        {
            SubscribeMail();
        }

        void SubscribeMail()
        {
            //generate new ticket pdf and get it as attachment
            //initialize variables for email sending
            string userEmail = email1.Value;
            if (userEmail.Length <= 64)
            {
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                try
                {
                    connection.Open();
                    MySqlCommand myCommand = connection.CreateCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = $"INSERT INTO subscription_email (email) VALUES ('{userEmail}');";
                    myCommand.ExecuteNonQuery();

                    string emailSender = ConfigurationManager.AppSettings["username"].ToString();
                    string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                    string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                    int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
                    bool emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                    string mailText = $"Dear Sir/Madam,\nCongratulations!\nYou have successfully subscribed to the Jazztastic 3 newsletter! The event is going to be held" +
                        $" from 01/07/2018 until 03/07/2018. Don't forget to buy tickets, because we hope to see you there! We will make sure to " +
                        $"keep you updated with the most important information about Jazztastic 3.";
                    string subject = "Jazztastic 3 - Successfully subscribed!";

                    //base class for sending mail
                    MailMessage mailMessage = new MailMessage();
                    //set mail "From" ID
                    mailMessage.From = new MailAddress(emailSender);
                    //set mail "To" ID
                    mailMessage.To.Add(userEmail);
                    //set mail "Subject" field
                    mailMessage.Subject = subject;
                    //add mail "Body" field
                    mailMessage.Body = mailText;
                    //Set the SMTP protocol
                    SmtpClient smtp = new SmtpClient();
                    //Set HOST server SMTP detail
                    smtp.Host = emailSenderHost;
                    //Set PORT number of SMTP detail
                    smtp.Port = emailSenderPort;
                    //Set SSL to true/false
                    smtp.EnableSsl = emailIsSSL;

                    //Set sender email and password
                    NetworkCredential networkCredential = new NetworkCredential(emailSender, emailSenderPassword);
                    smtp.Credentials = networkCredential;

                    //Send method which sends the email message (created a bit above)
                    smtp.Send(mailMessage);
                }
                catch (Exception)
                {

                }
                finally
                {
                    connection.Close();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }

        protected void AddCamping_ServerClick(object sender, EventArgs e)
        {
            string[] finishedCamping;
            string areaLetter = "";
            string campingSpot = "";
            string spotsTaken = "";

            if (Request["finishedCampingCheckbox"] != null)
            {
                finishedCamping = Request["finishedCampingCheckbox"].Split('.');
                areaLetter = finishedCamping[0];
                campingSpot = finishedCamping[1];
                spotsTaken = Request["rangeOneToSix"];
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                connection.Open();
                MySqlCommand myCommand = connection.CreateCommand();
                MySqlTransaction myTrans;
                myTrans = connection.BeginTransaction();
                myCommand.Connection = connection;
                myCommand.Transaction = myTrans;

                try
                {
                    if (areaLetter != "" && campingSpot != "")
                    {
                        myCommand.CommandText = $"UPDATE camping_spot SET spots_taken={spotsTaken} WHERE spotNo={campingSpot} AND area_letter='{areaLetter}'";
                        myCommand.ExecuteNonQuery();
                        myCommand.CommandText = $"UPDATE event_ticket SET area_letter = '{areaLetter}', spotNo = {campingSpot} WHERE visitorNo = {Session["VisitorNo"]}";
                        myCommand.ExecuteNonQuery();
                        myTrans.Commit();
                    }
                }
                catch (Exception)
                {
                    myTrans.Rollback();
                    return;
                }
                finally
                {
                    connection.Close();
                }

                //generate new ticket pdf and get it as attachment
                Attachment attachment = GenerateTicket.GetAttachmentAsPDF(Convert.ToInt64(visitorNoBackEnd), firstNameBackEnd, lastNameBackEnd, governmentIdBackEnd, ticketType, campingSpot, areaLetter);
                //initialize variables for email sending
                string emailSender = ConfigurationManager.AppSettings["username"].ToString();
                string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
                bool emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                string mailText = $"Dear {firstNameBackEnd} {lastNameBackEnd},\nCongratulations!\nYou have successfully reserved spot {areaLetter}, {campingSpot} for Jazztastic 3! The event is going to be held" +
                    $" from 01/07/2018 until 03/07/2018. We are excited to have you on board and expect to see you there! Don't forget your ticket, and more importantly, don't forget - Jazz on!";
                string subject = $"Jazztastic 3 - Successful camping spot {areaLetter}, {campingSpot} reservation";

                //get path for ticket folder
                string path = Server.MapPath($"~/Resources/tickets/ticket{visitorNoBackEnd}.pdf");
                //Generate a new ticket attachment by using the visitor's values

                //base class for sending mail
                MailMessage mailMessage = new MailMessage();
                //set mail "From" ID
                mailMessage.From = new MailAddress(emailSender);
                //set mail "To" ID
                mailMessage.To.Add(emailBackEnd);
                //set mail "Subject" field
                mailMessage.Subject = subject;
                //add attachment pdf ticket to mail "Attachments"
                mailMessage.Attachments.Add(attachment);
                //add mail "Body" field
                mailMessage.Body = mailText;
                //Set the SMTP protocol
                SmtpClient smtp = new SmtpClient();
                //Set HOST server SMTP detail
                smtp.Host = emailSenderHost;
                //Set PORT number of SMTP detail
                smtp.Port = emailSenderPort;
                //Set SSL to true/false
                smtp.EnableSsl = emailIsSSL;

                //Set sender email and password
                NetworkCredential networkCredential = new NetworkCredential(emailSender, emailSenderPassword);
                smtp.Credentials = networkCredential;

                //Send method which sends the email message (created a bit above)
                smtp.Send(mailMessage);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}