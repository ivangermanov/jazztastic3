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
    public partial class Login : System.Web.UI.Page
    {
        string connectionInfo = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e) {
            //System.Diagnostics.Debug.WriteLine("Login Page Loaded");
            SetNavbar();
        }

        private void SetNavbar() {
            if (Convert.ToBoolean(Session["LoggedIn"])) {
                loginNav.InnerHtml = "Log Out";
                loginNav.HRef = "Logout.aspx";
            }
        }

        protected void Submit_Clicked(object sender, EventArgs e) {
            try {
                string email = $"{Request.Form["inputEmail"]}";
                string password = $"{Request.Form["inputPassword"]}";
                //Debug
                /*
                if(email == "test@gmail.com" && password == "pokemon1") {
                    Session["LoggedIn"] = true;
                    Server.Transfer("UserPanel.aspx", true);
                } else {
                    Session["LoggedIn"] = false;
                    Response.Write("<script>alert('Login Failed')</script>");
                }
                */
                //System.Diagnostics.Debug.WriteLine($"{email} {password}");

                //Disabled sql logic
                string sql = $"SELECT visitorNo FROM event_account WHERE email='{email}' AND password='{password}'";
                using (MySqlConnection connection = new MySqlConnection(connectionInfo)) {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sql, connection)) {
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            reader.Read();
                            if (reader.HasRows) {
                                Session["LoggedIn"] = true;
                                Session["VisitorNo"] = Convert.ToInt32(reader[0]);
                            } else {
                                Response.Write("<script>alert('Login Failed')</script>");
                                Session["LoggedIn"] = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (Convert.ToBoolean(Session["LoggedIn"]))
                    Response.Redirect("UserPanel.aspx", true);
            }
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