using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"]??""; //Get the employee Id from the URL
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        conn.Open();
        XmlReader reader = new SqlCommand("SELECT Tasks FROM [Employees] WHERE Id='" + id + "'", conn).ExecuteXmlReader(); //Read the tasks XML from the database for the selected employee
        XmlDocument doc = new XmlDocument();
        doc.Load(reader); //Insert to an XmlDocument
        if (doc.ChildNodes.Count<1) //If the tasks are empty, Insert a root tag
        {
            new SqlCommand("UPDATE [Employees] SET Tasks='<root></root>' WHERE Id='" + id + "'", conn).ExecuteNonQuery();

        }
        conn.Close();
        TaskXmlDataSource.Data = doc.InnerXml;
        TaskXmlDataSource.DataBind(); //Display XML file
    }

    protected void AddTask_Button_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Connect to database
        if (TaskText.Text != String.Empty) 
        {
            conn.Open();
            string id = Request.QueryString["id"] ?? "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(TaskXmlDataSource.Data);
            if (!doc.DocumentElement.InnerXml.Contains(TaskText.Text)) //If there is no similar task already in the queue, add a task element with the 
            {                                                          //Current DateTime as Date and the given task as task attribute
                doc.DocumentElement.InnerXml += "<task Date=\"" + DateTime.Now.ToString("g") + "\" Task=\"" + TaskText.Text + "\"/>";

                new SqlCommand("UPDATE [Employees] SET Tasks='" + doc.InnerXml + "' WHERE Id='" + id + "'", conn).ExecuteNonQuery(); //Update the dtatbase


                Response.Redirect(Request.RawUrl); //Relode page
            }
            else //Else, Display error message
            {
                conn.Close();
                ErrorLabel.Visible = true;
            }
        }
        else //If empty, display error message
        {
            ErrorLabel.Text = "Please enter a value";
            ErrorLabel.Visible = true;
        }
    }

    protected void Delete_Link_Click(object sender, EventArgs e)
    {
        int RowIndex = ((GridViewRow)(sender as Control).Parent.Parent).RowIndex; //Get selected task's row index
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string id = Request.QueryString["id"] ?? "";
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(TaskXmlDataSource.Data); //Load the XML from the page
        conn.Open();
        string value = TaskGridView.Rows[RowIndex].Cells[2].Text; //Get the task-to-delete's text 

        XmlNodeList targets = doc.DocumentElement.SelectNodes("//task[@Task='"+value+"']"); //Collect all matches to a node list
        if(targets != null)
        {
            foreach (XmlNode item in targets)
            {
                item.ParentNode.RemoveChild(item); //Delete all matches
            }
        }
        else //If no matches were found, display error message
        {
            ErrorLabel.Text = "No element found";
            ErrorLabel.Visible = true;
        }
        new SqlCommand("UPDATE [Employees] SET Tasks='" + doc.InnerXml + "' WHERE Id='" + id + "'", conn).ExecuteNonQuery(); //Update dtatbase
        conn.Close();
        Response.Redirect(Request.RawUrl); //Reload
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
}