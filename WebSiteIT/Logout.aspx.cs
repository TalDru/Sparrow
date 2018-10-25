using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) //When redirected to this page, clear the "Session" cookie and go to Home page
    {
        Session["Current"] = null;
        Response.Redirect("Home.aspx");
    }
}