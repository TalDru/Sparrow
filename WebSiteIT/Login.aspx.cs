using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeClass;

public partial class Default2 : System.Web.UI.Page
{
    protected void Clear_Btn_Click(object sender, EventArgs e) //Clear all text boxes
    {
        Code.Clear(this);
    }

    protected void Login_Btn_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Connect to database
        conn.Open();
        if (Page.IsValid || !(IDText.Text=="" || PasswordText.Text =="")) //If all  validation criteria were met and niether text boxes are empty
        {
            try
            {
                string check = "SELECT count(*) FROM [Users] WHERE Id ='" + IDText.Text.Replace(" ", "") + "'";
                SqlCommand checkcom = new SqlCommand(check, conn);
                int count = Convert.ToInt32(checkcom.ExecuteScalar().ToString().Replace(" ", ""));
                conn.Close();
                if (count == 1) //Check if a user with that ID exists
                {
                    conn.Open();
                    check = "SELECT Password FROM [Users] WHERE Id ='"+IDText.Text.Replace(" ","")+"'";
                    checkcom = new SqlCommand(check, conn);
                    if (checkcom.ExecuteScalar().ToString().Replace(" ", "") == PasswordText.Text.Replace(" ", "")) //Check if the password matches the ID
                    {
                        check = "SELECT Type FROM[Users] WHERE Id = '" + IDText.Text.Replace(" ", "") + "'"; //Get the user type
                        checkcom = new SqlCommand(check, conn);
                        char Type = Convert.ToChar(checkcom.ExecuteScalar().ToString().Replace(" ", ""));
                        conn.Close();
                        Session["Current"] = IDText.Text.Replace(" ", ""); //Set the "Session" cookie to the ID
                        if (Type == 'A' ) //Type 'A' - Administrator
                            Response.Redirect("Admin.aspx");
                        else if (Type == 'B') //Type 'B' - Boss
                            Response.Redirect("Company.aspx");
                        else if (Type == 'E') //Type 'E' - Employee
                            Response.Redirect("Personal.aspx");
                        else
                            Response.Redirect("Home.aspx");
                    }
                    else //If password is invalid show error message
                    {
                        IDText.BorderColor = System.Drawing.Color.Red;
                        PasswordText.BorderColor = System.Drawing.Color.Red;
                        ErrorLable.Visible = true;
                    }
                }
                else //If E-Mail is invalid show error message
                {
                    IDText.BorderColor = System.Drawing.Color.Red;
                    PasswordText.BorderColor = System.Drawing.Color.Red;
                    ErrorLable.Visible = true;
                }
            }
            catch (Exception ex) //If an error was encountered, Log the error
            {
                Code.LogError(ex);                
            }

        }
        else //If Validators are activated, light up the responsible text box
        {
            if (!RFV_ID.IsValid || IDText.Text != "")
            {
                IDText.BorderColor = System.Drawing.Color.Red;
            }
            if (!RFV_Password.IsValid || PasswordText.Text != "")
            {
                PasswordText.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
}