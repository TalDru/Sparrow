using CodeClass;
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
    protected void Clear_Btn_Click(object sender, EventArgs e) //Clear all text boxes
    {
        Code.Clear(this);
    }

    protected void Register_Btn_Click(object sender, EventArgs e) //If user is not registered already, add a company with the given attributes to the Users table
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            string check = "SELECT count(*) FROM [Users] WHERE EMail ='" + MailText.Text.Replace(" ", "") + "'";
            SqlCommand checkcom = new SqlCommand(check, conn);
            int count = Convert.ToInt32(checkcom.ExecuteScalar().ToString().Replace(" ", ""));

            if (count >0)
            {
                MailText.BorderColor = System.Drawing.Color.Red;
                MailError.Visible = true;
            }
            else
            {
                if (Page.IsValid) //If all text boxes match the validators
                {
                    string id = GenerateShortGUID(conn);
                    string insertQ = "INSERT INTO [Users] (Id, Password, Joined, Type, CompanyName) VALUES (@Id, @Password, @Joined, 'B', @Name)";
                    SqlCommand com = new SqlCommand(insertQ, conn);

                    com.Parameters.AddWithValue("@Id", id);
                    com.Parameters.AddWithValue("@Password", PasswordText.Text);
                    com.Parameters.AddWithValue("@Email", MailText.Text);
                    com.Parameters.AddWithValue("@Joined", DateTime.Now.Date);
                    com.Parameters.AddWithValue("@Name", DateTime.Now.Date);
                    
                    com.ExecuteNonQuery();

                    conn.Close();
                    Response.Redirect("Login.aspx");



                }
            }



        }
        catch (Exception ex) //If an error was encountered, Log the error
        {
            Code.LogError(ex);
        }
    }

    protected string GenerateShortGUID(SqlConnection conn) //Generate a unique ID, 8 characters long, numbers only
    {
        string id = "";
        Random random = new Random();
        for (int i = 0; i < 8; i++) //8 times choose a random number from 0 to 9
        {
            id += random.Next(0, 10);
        }

        string check = "SELECT count(*) FROM [Users] WHERE Id ='" + id + "'";
        SqlCommand checkcom = new SqlCommand(check, conn);
        int count = Convert.ToInt32(checkcom.ExecuteScalar().ToString().Replace(" ", ""));
        if (count != 0) //If ID already exist in database, call function again
        {
            return GenerateShortGUID(conn);
        }
        else //Else, Unique ID was found
        {
            return id;
        }
    }
}