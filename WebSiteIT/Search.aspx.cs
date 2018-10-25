using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Services;
using CodeClass;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WorkerGridView.Caption= "<table><tr><td>Results: " + Request.QueryString["q"]+"</ td ></ tr ></ table >";
        localhost.WebService service = new localhost.WebService(); //Access the Web Service
        DataTable set = service.ReturnWorkers(Request.QueryString["q"]??"ALL"); //Get all the people that match the query in the URL (a skill)

        WorkerGridView.DataSource = set;
        WorkerGridView.DataBind(); //Display the matches
        
        for (int i = 0; i < WorkerGridView.Rows.Count; i++)
        {
            for (int j = 0; j < WorkerGridView.Rows[i].Cells.Count; j++)
            {
                if (WorkerGridView.Rows[i].Cells[j].Text.Contains("|"))
                {
                    WorkerGridView.Rows[i].Cells[j].Text = 
                        WorkerGridView.Rows[i].Cells[j].Text.Insert(0, "•");
                    WorkerGridView.Rows[i].Cells[j].Text=
                        string.Join("<br>•",
                        WorkerGridView.Rows[i].Cells[j].Text.Split('|'));
                }
            }
        } //Format the Skills to a bullet list
    }

    protected void Invite(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Invite_btn")
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument); //Get the row index of the sender
                GridViewRow row = WorkerGridView.Rows[index];
                string mail = "";
                foreach (TableCell cell in row.Cells) //Find the cell containing the E-Mail in the chosen row
                {
                    if (cell.Text.Contains('@'))
                        mail = cell.Text;
                }

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Get the company name and E-Mail from database
                conn.Open();
                string check = "SELECT CompanyName FROM [Users] WHERE Id ='" + Session["Current"] + "'";
                SqlCommand checkcom = new SqlCommand(check, conn);
                string Name = (string)checkcom.ExecuteScalar();
                check = "SELECT EMail FROM [Users] WHERE Id ='" + Session["Current"] + "'";
                checkcom = new SqlCommand(check, conn);
                string Address = (string)checkcom.ExecuteScalar();
                conn.Close();

                Code.SendMail(mail, "Invitation to an Interview at "+"", //Invite the person in the selected row
                    "We are pleased to announce that your submission to the Falcon Human Resources firm was reviewed by "+Name.TrimEnd(' ')+
                    " and was found fitting. please contact the company at "+Address.TrimEnd(' ') + " for further details"); 

                LabelSuccess.Visible = true;
                LabelSuccess.Text = "Invitation sent successfully"; //Display successful invitation
            }
            catch (Exception ex) //If an error was encountered, Log the error
            {
                Code.LogError(ex);
            }
        }
    }

    /* Was previously used to convert from object array to DataTable before WebService sent information as DataTable directly
    public DataTable ToDataTable(Object[,] myData)
    {
        DataTable dt = new DataTable();
        for (int i = 0; i < myData.GetLength(1); i++)
        {
            dt.Columns.Add("Column" + (i + 1));
        }

        for (var i = 0; i < myData.GetLength(0); ++i)
        {
            DataRow row = dt.NewRow();
            for (var j = 0; j < myData.GetLength(1); ++j)
            {
                row[j] = myData[i, j];
            }
            dt.Rows.Add(row);
        }
        return dt;
    }*/


    protected void Back_Click(object sender, EventArgs e) //Go to Company page
    {
        Response.Redirect("Company.aspx");
    }

    protected void DownloadCVLink_Click(object sender, EventArgs e) //Covert the Byte array in the database to a PDF file and trigger a download
    {
        LinkButton caller = sender as LinkButton;
        byte[] data = Convert.FromBase64String(caller.CommandName);

        HttpResponse res = HttpContext.Current.Response;
        res.Clear();
        res.ContentType = "application/pdf";
        res.AppendHeader("Content-Disposition", "inline;filename=CV.pdf;");
        res.BufferOutput = true;
        res.AddHeader("Content-Length", ""+data.Length);
        res.BinaryWrite(data);
        res.End();
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
