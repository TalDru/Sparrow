using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Clear_Btn_Click(object sender, EventArgs e) //Delete all rows form Log table
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        conn.Open();
        (new SqlCommand("DELETE FROM [Log]", conn)).ExecuteNonQuery();
        conn.Close();
    }
    #region Menu Rendering (See Documentation in MasterPage)

    public string RenderMenu(string id)
    {
        var result = new StringBuilder();
        if (id == "RightMenu")
        {
            if (Session["Current"] == null)
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