using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < WorkersGridView.Rows.Count; i++)
        {
            for (int j = 0; j < WorkersGridView.Rows[i].Cells.Count; j++)
            {
                if (WorkersGridView.Rows[i].Cells[j].Text.Contains("|"))
                {
                    WorkersGridView.Rows[i].Cells[j].Text =
                        WorkersGridView.Rows[i].Cells[j].Text.Insert(0, "•");
                    WorkersGridView.Rows[i].Cells[j].Text =
                        string.Join("<br>•",
                        WorkersGridView.Rows[i].Cells[j].Text.Split('|'));
                }
            }
        } //Format the Skills to a bullet list

    }

    protected void AddButton_Click(object sender, EventArgs e) //Add a worker to the Workers table
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceConnectionString"].ConnectionString);
            conn.Open();
            string insertQ = "INSERT INTO [Workers] (Name, Gender, Skills, Mail, CV, Pic) VALUES (@Name, @Gender, @Skills, @Mail, @CV, @Pic)";
            SqlCommand com = new SqlCommand(insertQ, conn);
            com.Parameters.AddWithValue("@Name", NameText.Text);
            com.Parameters.AddWithValue("@Gender", GenderDropDownList.SelectedValue);
            com.Parameters.AddWithValue("@Skills", SkillsText.Text.Replace(',', '|').Replace(" ", ""));
            com.Parameters.AddWithValue("@Mail", MailText.Text);
            com.Parameters.AddWithValue("@CV", CVFileUpload.FileBytes);
            com.Parameters.AddWithValue("@Pic", PictureFileUpload.FileBytes);

            com.ExecuteNonQuery();
            conn.Close();
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception ex)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceConnectionString"].ConnectionString); //Get the Connectionstring
            conn.Open(); //Open a connection

            string insertQ = "INSERT INTO [Log] (Date, Error) VALUES (@Date, @Error)"; //Insert a row containing the current DateTime and the error message
            SqlCommand com = new SqlCommand(insertQ, conn);
            com.Parameters.AddWithValue("@Date", DateTime.Now.ToString("g")); //Format DateTime as DD/MM/YYYY HH:mm
            com.Parameters.AddWithValue("@Error", ex.Message);
            com.ExecuteNonQuery();

            conn.Close(); //Close the Connection
        }

    }

    protected void Delete(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete_btn")
        {
            GridViewRow row = WorkersGridView.Rows[Convert.ToInt32(e.CommandArgument)];//Get the row of the sender
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceConnectionString"].ConnectionString); //Get the Connectionstring
            conn.Open(); //Open a connection
            SqlCommand com = new SqlCommand("DELETE FROM [Workers] WHERE Name='" + row.Cells[1].Text + "'", conn);
            com.ExecuteNonQuery(); 
            conn.Close(); //Close the Connection
            Response.Redirect(Request.RawUrl);
        }
    }
}