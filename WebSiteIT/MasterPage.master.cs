using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    #region Menu Rendering
    public string RenderMenu()
    {
        var result = new StringBuilder();
        RenderMenuItem("Home", "Home.aspx", result);
        RenderMenuItem("About", "About.aspx", result);
        return result.ToString();
    }
    public string RenderMenu(string id)
    {
        var result = new StringBuilder();
        if (id == "RightMenu")
        {
            if(Session["Current"]== null)
            {
                RenderMenuItem("Log In", "Login.aspx", result);
                RenderMenuItem("Register", "Register.aspx", result);
                RenderMenuItem("Company Sign-Up", "CompanySignUp.aspx", result);
            }
            else
            {
                RenderMenuItem("Logout", "Logout.aspx", result);
            }
        }
        else
        {
            RenderMenuItem(id, id + ".aspx", result);
        }
        return result.ToString();
    }
    void RenderMenuItem(string title, string address, StringBuilder output)
    {
        output.AppendFormat("<li><a href=\"{0}\" ", address);

        var requestUrl = HttpContext.Current.Request.Url;        
        if (requestUrl.Segments[requestUrl.Segments.Length - 1].Equals(address, StringComparison.OrdinalIgnoreCase)) // If the requested address is this menu item.
            output.Append("class=\"ActiveMenuButton\"");
        else
            output.Append("class=\"MenuButton\"");

        output.AppendFormat("><span>{0}</span></a></li> ", title);
    }
    #endregion
}