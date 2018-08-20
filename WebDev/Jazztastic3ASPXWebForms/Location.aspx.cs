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
    public partial class Location : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetNavbar();
        }

        private void SetNavbar() {
            if (Convert.ToBoolean(Session["LoggedIn"])) {
                loginNav.InnerHtml = "Log Out";
                loginNav.HRef = "Logout.aspx";
            }
        }

        protected void Subscribe_Click(object sender, EventArgs e)
        {
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

        protected void SendMail_ServerClick(object sender, EventArgs e)
        {
            string userEmail = email.Value;
            string name = name1.Value;
            string company = company1.Value;
            string mobile = mobile1.Value;
            string message = message1.Value;

            string emailSender = ConfigurationManager.AppSettings["username"].ToString();
            string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
            string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
            int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
            bool emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
            string mailText;
            if (company == "")
                mailText = $"User {name} with email {userEmail} has sent a message.";
            else
                mailText = $"User {name} with email {userEmail} from company {company} has sent a message.";
            if (mobile != "")
                mailText += $" Mobile number provided - {mobile}.";
            mailText += $"\n\n*******************************\n\n{ message}";

            string subject = "Jazztastic 3 - Contact!";

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
    }
}