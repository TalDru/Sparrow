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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        localhost.WebService service = new localhost.WebService();
        LabelSum.Text += service.GetBill("SYSADMIN")+"$";
        Bill.Text += service.GetBill(Session["Current"].ToString()) + "$";
        SqlDataSource.SelectCommand = "SELECT Id, FName AS 'First Name', LName AS 'Last Name', Position, Phone, EMail AS 'E-Mail' " +
            "FROM [Employees] WHERE CompanyId="+(Session["Current"].ToString()??""); //Set the Gridview to find only the user with the ID in the "Session" cookie
    }

    protected void ButtonInvite_Click(object sender, EventArgs e) //Send Invitation if person was never added
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Connect to database
            conn.Open();
            string check = "SELECT count(*) FROM [Users] WHERE EMail ='" + MailText.Text.Replace(" ", "")+"'"; //Count how many rows contain the provided mail address
            SqlCommand checkcom = new SqlCommand(check, conn);
            int count = Convert.ToInt32(checkcom.ExecuteScalar().ToString().Replace(" ", ""));
            conn.Close();
            if (count > 0 || MailText.Text=="" || MailText.Text==null) //If  maches were found (and the address is empty), Display error
            {
                ErrorLabel.Visible = true;
                MailText.ForeColor = System.Drawing.Color.Red;
            }
            else //Else, Invite the person and register an empty entry to the tables with the given address and the inviter's ID (Boss)
            {
                conn.Open();
                check = "INSERT INTO [Users] (Id, EMail, Type) VALUES ('--------', '"+MailText.Text+"', 'E')";
                checkcom = new SqlCommand(check, conn);
                checkcom.ExecuteNonQuery();
                check = "INSERT INTO [Employees] (Id, EMail, CompanyId) VALUES ('--------', '" + MailText.Text + "', '"+Session["Current"].ToString()+"')";
                checkcom = new SqlCommand(check, conn);
                checkcom.ExecuteNonQuery();

                Code.SendMail(MailText.Text,"Inventation to Sparrow", "You where invited by you employer to join Sparrow: A Manpower Manegment Site" +
                    "\nPlease go to the following link to finish your registration process: http://localhost:60173/Register.aspx");
                conn.Close();
                SentLable.Visible = true; //Display Succsess label
            }
        }
        catch (Exception ex) //If an error was encountered, Log the error
        {
            Code.LogError(ex);
        }

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

    protected void SearchButton_Click(object sender, EventArgs e) //Go to the search page with the query as a URL attribute
    {
        localhost.WebService service = new localhost.WebService();
        service.AddToBill(Session["Current"].ToString());
        Response.Redirect((QTextBox.Text == null|| QTextBox.Text=="")? "Search.aspx" : "Search.aspx?q=" + QTextBox.Text);
    }


    protected void Tasks_Link_Click(object sender, EventArgs e) //Go to the task list of the sender , using his ID that is stored in CommandName usin Eval()
    {
        Response.Redirect("Tasks.aspx?id=" + (sender as LinkButton).CommandName);
    }
}