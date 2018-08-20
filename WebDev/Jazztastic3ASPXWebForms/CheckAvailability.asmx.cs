using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Jazztastic3ASPXWebForms
{
    /// <summary>
    /// Summary description for CheckAvailability
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CheckAvailability : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string AvailableTickets()
        {
            if (!Ticket.TicketAvailable("TTT", out string errorMessage))
            {
                if (errorMessage.Contains("No tickets"))
                {
                    return errorMessage;
                }
            }
            return errorMessage;
        }
    }
}
