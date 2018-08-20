using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jazztastic3ASPXWebForms
{
    public partial class Map : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetNavbar();
        }
        private void SetNavbar() {
            if (Convert.ToBoolean(Session["LoggedIn"])) {
                /*
                loginNav.InnerHtml = "Log Out";
                loginNav.HRef = "Logout.aspx";
                */

            }
        }
    }
}