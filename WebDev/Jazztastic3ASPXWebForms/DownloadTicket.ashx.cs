using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Jazztastic3ASPXWebForms
{
    /// <summary>
    /// Summary description for DownloadTicket
    /// </summary>
    public class DownloadTicket : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = HttpContext.Current.Response;
            HttpRequest request = HttpContext.Current.Request;
            string pathAndNo = request.QueryString["path"];
            string[] pathAndNoArray = pathAndNo.Split('Ђ');
            if (File.Exists(@pathAndNoArray[0]))
            {
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition",
                                   "attachment; filename=" + pathAndNoArray[1] + ";");
                response.TransmitFile(pathAndNoArray[0]);
                response.Flush();
                response.End();
            }
            else
            {
                response.Redirect("UserPanel.aspx");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}