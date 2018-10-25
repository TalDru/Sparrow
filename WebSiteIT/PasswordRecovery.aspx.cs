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
    protected void Recover_Btn_Click(object sender, EventArgs e) //If user was found send recovery mail
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Connect to database
        conn.Open();
        if (Page.IsValid) //If all validator criteria were met
        {
            try
            {
                string check = "SELECT count(*) FROM [Users] WHERE EMail ='" + MailText.Text.Replace(" ", "") + "'"; //Check if there is at least one such E-Mail in database
                SqlCommand checkcom = new SqlCommand(check, conn);
                int count = Convert.ToInt32(checkcom.ExecuteScalar().ToString().Replace(" ", ""));
                conn.Close();
                if (count >0) //If found, send a recovery mail with ID and Password to the given Mail
                {
                    conn.Open();
                    check = "SELECT Password FROM [Users] WHERE EMail ='" + MailText.Text.Replace(" ", "") + "'";
                    checkcom = new SqlCommand(check, conn);
                    string pass = checkcom.ExecuteScalar().ToString().Replace(" ", "");
                    check = "SELECT Id FROM [Users] WHERE EMail ='" + MailText.Text.Replace(" ", "") + "'";
                    checkcom = new SqlCommand(check, conn);
                    string Id = checkcom.ExecuteScalar().ToString().Replace(" ", "");

                    Code.SendMail(MailText.Text, "Recovery Mail From \"Sparrow\"",
                        "You have requested a recovery of your credentials over on \"Sparrow\"\nYour Username is- "+Id+
                        " and your Password is- "+pass+"\nNote: If you haven't made this request please ignore this message.");

                    ConfirmLabel.Visible = true;
                }
                else //Else, Show Error message
                {
                    MailText.BorderColor = System.Drawing.Color.Red;
                    ErrorLable.Visible = true;
                }
            }
            catch (Exception ex)//If an error was encountered, Log the error
            {
                Code.LogError(ex);
            }
            finally
            {
                conn.Close();
            }

        }
        else
        {
            if (!RFV_Mail.IsValid || MailText.Text != "") //Tell that mail was invalid
            {
                MailText.BorderColor = System.Drawing.Color.Red;
            }
        }
    }

}