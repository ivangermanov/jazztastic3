using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.Services;
using System.Web.Script.Services;

namespace Jazztastic3ASPXWebForms
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //call Refresh method
            if (DateTime.Now.Date < new DateTime(2018, 07, 1))
            {
                SetNavbar();
                Refresh();
                //called on submit button
                nextBtn.ServerClick += new EventHandler(nextBtn_ServerClick);
            }
            else
            {
                Server.Transfer("Login.aspx");
            }
        }
        private void SetNavbar() {
            if (Convert.ToBoolean(Session["LoggedIn"])) {
                loginNav.InnerHtml = "Log Out";
                loginNav.HRef = "Logout.aspx";
            }
        }

        private void Refresh()
        {
            //set the text of the validation divs to empty
            fNameValidation.InnerHtml = "";
            lNameValidation.InnerHtml = "";
            govtIdValidation.InnerHtml = "";
            emailAddressValidation.InnerHtml = "";
            passwordValidation.InnerHtml = "";
            dobValidation.InnerHtml = "";
            ticketTypeValidation.InnerHtml = "";
        }

        protected void Submit_Click()
        {
            //value from inputs
            string firstName = Request["fname"];
            string lastName = Request["lname"];
            string governmentID = Request["govid"];
            string email = Request["email"];
            string password = Request["pwd"];
            string dobBack = Request["dob"];
            string ticketDates = Request["ticketDate"];
            string nameOnCard = Request["nameOnCard"];
            string cardNumber = Request["cardNumber"];
            string expDate = Request["expDate"];
            string cvv = Request["cvv"];
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
            }
            
            //back-end validation for inputs
            if (
                firstName == "" || firstName.Length > 32
                || lastName == "" || lastName.Length > 32
                || governmentID == "" || governmentID.Length > 11
                || email == "" || email.Length > 64
                || password == "" || password.Length > 32
                || dobBack == "" || dobBack.Length > 10 || dobBack.Length < 10
                || ticketDates == ""
                || nameOnCard == ""
                || cardNumber == ""
                || expDate == ""
                || cvv == "")
                return;

            //insert visitor in the 2 database tables (event_account and event_ticket)
            Visitor newVisitor = new Visitor(firstName, lastName, governmentID, email, dobBack, password, ticketDates, campingSpot, spotsTaken, areaLetter);

            //Insert visitor into database. If it is successful, execute next steps
            if (newVisitor.InsertIntoDB(out string errorMessage))
            {
                //send mail message
                SendMail();
                //transfer website visitor to Login
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (errorMessage.Contains("Government id already in use."))
                {
                    govtIdValidation.InnerHtml = "That government ID is already taken!";
                }
                if (errorMessage.Contains("Email already in use."))
                {
                    emailAddressValidation.InnerHtml = "That email is already taken!";
                }
                if (errorMessage.Contains("No tickets"))
                {
                    ticketTypeValidation.InnerHtml = errorMessage;
                }
                //checks previously entered values, so that if they're not null, the user doesn't have to retype them
                if (firstName != "")
                    fname.Value = firstName;
                if (lastName != "")
                    lname.Value = lastName;
                if (governmentID != "")
                    govid.Value = governmentID;
                if (dobBack != "")
                    dob.Value = dobBack;
                if (ticketDates != "")
                    ticketDate.Value = ticketDates; 
            }

            void SendMail()
            {
                //generate new ticket pdf and get it as attachment
                Attachment attachment = GenerateTicket.GetAttachmentAsPDF(newVisitor.VisitorNo, firstName, lastName, governmentID, newVisitor.TicketType, campingSpot, areaLetter);
                //initialize variables for email sending
                string emailSender = ConfigurationManager.AppSettings["username"].ToString();
                string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
                bool emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                string mailText = $"Dear {firstName} {lastName},\nCongratulations!\nYou have successfully purchased a ticket for Jazztastic 3! The event is going to be held" +
                    $" from 01/07/2018 until 03/07/2018. We are excited to have you on board and expect to see you there! Don't forget your ticket, and more importantly, don't forget - Jazz on!";
                string subject = "Jazztastic 3 - Successful purchase of ticket";

                //get path for ticket folder
                string path = Server.MapPath($"~/Resources/tickets/ticket{newVisitor.VisitorNo}.pdf");
                //Generate a new ticket attachment by using the visitor's values

                //base class for sending mail
                MailMessage mailMessage = new MailMessage();
                //set mail "From" ID
                mailMessage.From = new MailAddress(emailSender);
                //set mail "To" ID
                mailMessage.To.Add(email);
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
            }
        }

        protected void nextBtn_ServerClick(object sender, EventArgs e)
        {
            Submit_Click();
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
                }
            }
        }
    }
}