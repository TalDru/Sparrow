using CodeClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.IO;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) //If Page is not PostBack, load all inforamtion to text boxes from database using the "Session" cookie as the ID
    {
        if (!Page.IsPostBack)
        { 
            try
            {

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();


                NameText.Text = (new SqlCommand("SELECT FName FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString().Replace(" ", ""))
                    + " " + (new SqlCommand("SELECT LName FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString().Replace(" ", ""));
                GenderList.SelectedValue = (new SqlCommand("SELECT Gender FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString().Replace(" ", ""));
                DobText.Text = Convert.ToDateTime((new SqlCommand("SELECT DoB FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar()).ToString().TrimEnd(' ')).ToShortDateString();
                DohText.Text = Convert.ToDateTime((new SqlCommand("SELECT HireDate FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar()).ToString().TrimEnd(' ')).ToShortDateString();
                PosText.Text = (new SqlCommand("SELECT Position FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString()).TrimEnd(' ');
                PhoneText.Text = (new SqlCommand("SELECT Phone FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString()).TrimEnd(' ');
                MailText.Text = (new SqlCommand("SELECT EMail FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString()).TrimEnd(' ');

                conn.Close();
            }
            catch (Exception ex) //If an error was encountered, Log the error
            {
                Code.LogError(ex);
            }
        }
    }

    protected void Update_Btn_Click(object sender, EventArgs e) //Update the information from the text boxes to the database
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            string pic = (PictureFileUpload.HasFile)?" Picture=@pic,": ""; //If no file was loaded, do not update picture to Null
            string command = "UPDATE [Employees] SET FName=@fname, LName=@lname,"+pic+" Gender=@gen, DoB=@dob, HireDate=@doh, Position=@pos, Phone=@num, EMail=@mail WHERE Id=@id";
            SqlCommand com = new SqlCommand(command, conn);

            com.Parameters.AddWithValue("@fname", NameText.Text.Split(' ')[0]);
            com.Parameters.AddWithValue("@lname", NameText.Text.Split(' ')[1]);
            com.Parameters.AddWithValue("@gen", GenderList.SelectedValue.TrimEnd(' '));
            com.Parameters.AddWithValue("@dob", Convert.ToDateTime(DobText.Text));
            com.Parameters.AddWithValue("@doh", Convert.ToDateTime(DohText.Text));
            if(PictureFileUpload.HasFile)
                com.Parameters.AddWithValue("@pic", PictureFileUpload.FileBytes);
            com.Parameters.AddWithValue("@pos", PosText.Text.TrimEnd(' '));
            com.Parameters.AddWithValue("@num", PhoneText.Text.TrimEnd(' '));
            com.Parameters.AddWithValue("@mail", MailText.Text.TrimEnd(' '));
            com.Parameters.AddWithValue("@id", Session["Current"]);
            

            com.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("Personal.aspx"); //At the end go to Personal
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
        if (requestUrl.Segments[requestUrl.Segments.Length - 1].Equals(address, StringComparison.OrdinalIgnoreCase)) // If the requested address is this menu item.
            output.Append("class=\"ActiveMenuButton\"");
        else
            output.Append("class=\"MenuButton\"");

        output.AppendFormat("><span>{0}</span></a></li> ", title);
    }
    #endregion

    protected void Reset_Btn_Click(object sender, EventArgs e) //Redirect to Personal
    {
        Response.Redirect("Personal.aspx");
    }
}