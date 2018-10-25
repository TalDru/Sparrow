using CodeClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        localhost.WebService service = new localhost.WebService();
        if(!Page.IsPostBack)
            CostTextBox.Text = service.GetBill("SYSADMIN")+"";
        if ((string)Session["Current"] != "SYSADMIN") //If the username "Session" cookie isn't set to the admin's ID, Go to Home page
        {
            Response.Redirect("Home.aspx");
        }

        try
        {
            if (Request.QueryString["tbl"] == "1") //If the URL query is set to 1 display Employee list
            {
                SqlDataSource.SelectCommand = "SELECT * FROM [Employees]";
                SwitchButton.Text = "View Users";
            }
            else //Else, display the Users list
            {
                SqlDataSource.SelectCommand = "SELECT Id, Password, EMail 'E-Mail', Joined AS 'Join Date', Type, CompanyName AS 'Company Name' FROM [Users]";
                SwitchButton.Text = "View Employees";
            }
            GridView.Caption =(Request.QueryString["tbl"] == "1") ? "<table width='90%' style='font-size:30px; text-align:center;'><tr><td>Employees</td></tr></table>":
                "<table width='90%' style='font-size:30px; text-align:center;'><tr><td>Users</td></tr></table>";


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Connect to database
            conn.Open();
            string check = "SELECT Error FROM [Log] WHERE Date =(SELECT MAX(Date) FROM [Log])"; //Select the most recent error message
            SqlCommand checkcom = new SqlCommand(check, conn);
            string error = checkcom.ExecuteScalar().ToString().TrimStart(' ').TrimEnd(' ');

            check = "SELECT MAX(Date) FROM [Log]"; //Select the date of the most recent error message
            checkcom = new SqlCommand(check, conn);
            string date = checkcom.ExecuteScalar().ToString().TrimStart(' ').TrimEnd(' ');
            LabelLog2.Text = " At " + date + " : " + error; //Display the latest error
        }
        catch (Exception ex) //If an error was encountered, Log the error
        {
            Code.LogError(ex);
        }

    }

    protected void SwitchButton_Click(object sender, EventArgs e) //If button is clicked display the other table
    {
        if (Request.QueryString["tbl"] == "1")
            Response.Redirect("Admin.aspx?tbl=0");
        else
            Response.Redirect("Admin.aspx?tbl=1");

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
        if (requestUrl.Segments[requestUrl.Segments.Length - 1].Equals(address, StringComparison.OrdinalIgnoreCase)) 
            output.Append("class=\"ActiveMenuButton\"");
        else
            output.Append("class=\"MenuButton\"");

        output.AppendFormat("><span>{0}</span></a></li> ", title);
    }
    #endregion

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        localhost.WebService service = new localhost.WebService();
        service.UpdateBill("SYSADMIN", int.Parse(CostTextBox.Text));
        Response.Redirect(Request.RawUrl);
    }
}