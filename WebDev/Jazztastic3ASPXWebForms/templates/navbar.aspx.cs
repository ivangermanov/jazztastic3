using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jazztastic3ASPXWebForms.templates {
    public partial class navbar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            login.HRef = "https://google.com";
            login.InnerHtml = "Logout";
        }

        
    }
}