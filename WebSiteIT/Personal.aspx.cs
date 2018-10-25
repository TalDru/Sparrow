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
using System.Xml;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Load_info(sender, e);

        localhost.WebService client = new localhost.WebService(); //Access the WebService
        if (Session["Current"]==null)
            Response.Redirect("Login.aspx");
        string last =client.LastShiftInput((string)Session["Current"] ?? ""); //Get last registered shift type for the current user
        if (last.Contains("I")) //If it was an "In" regestration, leave only "Out" button enabled, and vice versa.
            In_Btn.Enabled = false;
        else
            Out_Btn.Enabled = false;

        string id = Session["Current"].ToString() ?? "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Connect to database
        conn.Open();
        XmlReader reader = new SqlCommand("SELECT Tasks FROM [Employees] WHERE Id='" + id + "'", conn).ExecuteXmlReader(); //Get the tasks XML
        XmlDocument doc = new XmlDocument();
        doc.Load(reader);
        if (doc.ChildNodes.Count < 1) //If the document is emty, add a root tag
        {
            new SqlCommand("UPDATE [Employees] SET Tasks='<root></root>' WHERE Id='" + id + "'", conn).ExecuteNonQuery();

        }
        conn.Close();
        TaskXmlDataSource.Data = doc.InnerXml;
        TaskXmlDataSource.DataBind(); //Display the tasks XML
    }

    protected void Load_info(object sender, EventArgs e) //Display user Details from datatbase
    {
        try
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            
            NameText.Text = (new SqlCommand("SELECT FName FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString().Replace(" ", ""))
                +" "+ (new SqlCommand("SELECT LName FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString().Replace(" ", ""));
            string gen = (new SqlCommand("SELECT Gender FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString().Replace(" ", ""));
            if (gen == "M")
                GenderText.Text = "Male";
            else if (gen == "F")
                GenderText.Text = "Female";
            else
                GenderText.Text = "Other";
            DobText.Text = ((DateTime)new SqlCommand("SELECT DoB FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar()).ToShortDateString();
            DohText.Text = ((DateTime)new SqlCommand("SELECT HireDate FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar()).ToShortDateString();
            PosText.Text = (new SqlCommand("SELECT Position FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString());
            PhoneText.Text= (new SqlCommand("SELECT Phone FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString());
            MailText.Text = (new SqlCommand("SELECT EMail FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar().ToString());
            byte[] image = (byte[])(new SqlCommand("SELECT Picture FROM [Employees] WHERE Id = '" + Session["Current"] + "'", conn).ExecuteScalar());
            Picture.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(image); //Convert image from a BLOB to an image
            conn.Close();
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

    protected void Update_Btn_Click(object sender, EventArgs e) //Go to Personal Update
    {
        Response.Redirect("PersonalUpdate.aspx");
    }


    protected void Shift_Btn_Click(object sender, EventArgs e)
    {
        localhost.WebService client = new localhost.WebService(); //Access the WebService 

        client.RegisterShift(Session["Current"].ToString(), ((Button)sender).CommandName); //Register Shift to the WebService database

        Response.Redirect(Request.RawUrl); //Reload
    }

    protected void Delete_Link_Click(object sender, EventArgs e)
    {
        int RowIndex = ((GridViewRow)(sender as Control).Parent.Parent).RowIndex; //Get selected task's row index
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string id = Session["Current"].ToString()??"";
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(TaskXmlDataSource.Data);  //Load the XML from the page
        conn.Open();
        string value = TaskGridView.Rows[RowIndex].Cells[2].Text;  //Get the task-to-delete's text 

        XmlNodeList targets = doc.DocumentElement.SelectNodes("//task[@Task='" + value + "']"); //Collect all matches to a node list
        if (targets != null)
        {
            foreach (XmlNode item in targets)
            {
                item.ParentNode.RemoveChild(item); //Delete all matches
            }
        }
        new SqlCommand("UPDATE [Employees] SET Tasks='" + doc.InnerXml + "' WHERE Id='" + id + "'", conn).ExecuteNonQuery(); //Update dtatbase
        conn.Close();
        Response.Redirect(Request.RawUrl); //Reload
    }

}