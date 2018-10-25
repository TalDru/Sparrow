using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

using CodeClass;

public partial class Default2 : System.Web.UI.Page
{
    protected void Clear_Btn_Click(object sender, EventArgs e) //Clear all text boxes
    {
        Code.Clear(this);
    }

    protected void Register_Btn_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Connect to database
            conn.Open();

            string check = "SELECT count(*) FROM [Users] WHERE EMail ='" + MailText.Text.Replace(" ", "") + "'";
            SqlCommand checkcom = new SqlCommand(check, conn);
            int count = Convert.ToInt32(checkcom.ExecuteScalar().ToString().Replace(" ", ""));

            if (count <1) //If no record of the E-Mail were found, i.e., The person wasn't invited, display error
            { 
                MailText.BorderColor = System.Drawing.Color.Red;
                MailError.Visible = true;
            }
            else
            {
                if (Page.IsValid) //If all text boxes match the validators
                {
                    string id = GenerateShortGUID(conn); //Generate unique ID for the user
                    string insertQ = "UPDATE [Users] SET Id = @Id, Password = @Password, Joined = @Joined, Type = 'E' WHERE EMail = @Email"; //Update the previously empty row to the given information
                    SqlCommand com = new SqlCommand(insertQ, conn);

                    com.Parameters.AddWithValue("@Id", id );
                    com.Parameters.AddWithValue("@Password", PasswordText.Text);
                    com.Parameters.AddWithValue("@Email", MailText.Text);
                    com.Parameters.AddWithValue("@Joined", DateTime.Now.Date);

                    Code.SendMail(MailText.Text, "Welcome to \"Sparrow\"",
                        "Welcome to \"Sparrow\"!\n The following text is your username, and from now on and will be used to access the site-\n " + id); //Send an E-Mail ith the Log-In information

                    com.ExecuteNonQuery();

                    com = new SqlCommand("UPDATE[Employees] SET Id = @Id WHERE EMail = @Email", conn); //Update the Employee table, too
                    com.Parameters.AddWithValue("@Id", id);
                    com.Parameters.AddWithValue("@Email", MailText.Text);
                    com.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Login.aspx"); //Go to Login
                }
            }
        }
        catch (Exception ex) //If an error was encountered, Log the error
        {
            Code.LogError(ex);
        }
    }

    protected string GenerateShortGUID(SqlConnection conn) //Generate a unique ID, 8 characters long, capital letters only
    {
        string id = "";
        Random random = new Random();
        for (int i = 0; i < 8; i++) //8 times choose a random number from 65 to 90, then convert it to a letter using it's ASCII value
        {
            id += (char)random.Next(65, 91);
        }

        string check = "SELECT count(*) FROM [Users] WHERE Id ='" + id + "'";
        SqlCommand checkcom = new SqlCommand(check, conn);
        int count = Convert.ToInt32(checkcom.ExecuteScalar().ToString().Replace(" ", ""));
        if (count!=0) //If ID already exist in database, call function again
        {
            return GenerateShortGUID(conn);
        }
        else //Else, Unique ID was found
        {
            return id;
        }
    }
}