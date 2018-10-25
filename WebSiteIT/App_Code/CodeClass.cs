using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace CodeClass
{
    public class Code
    {
        public static void Clear(Control control) //Clear all TextBox items in the given Control
        {
            foreach (Control item in control.Controls) //Go through all of the items in the Control block
            {
                if (item is TextBox) //If the item is a TextBox
                    ((TextBox)item).Text = ""; //Set text to an empty string
                if (item.HasControls()) //If the item contains Controls nested inside
                    Clear(item); //Run the function Clear recursivly on the item 
            }
        }

                                
        public static void LogError(Exception ex) //Log the Exception to the Log table it the database
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Get the Connectionstring
            conn.Open(); //Open a connection

            string insertQ = "INSERT INTO [Log] (Date, Error) VALUES (@Date, @Error)"; //Insert a row containing the current DateTime and the error message
            SqlCommand com = new SqlCommand(insertQ, conn);
            com.Parameters.AddWithValue("@Date", DateTime.Now.ToString("g")); //Format DateTime as DD/MM/YYYY HH:mm
            com.Parameters.AddWithValue("@Error", ex.Message);
            com.ExecuteNonQuery();

            conn.Close(); //Close the Connection
        }

        public static void SendMail(string to, string subject, string body) //Send an E-Mail with the given attributes
        {
            try
            {
                MailMessage mailMessage = new MailMessage("projectmailserver@gmail.com", to, subject, body); //Create an E-Mail with the given attributes

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); //Use Gmail's SMTP server
                smtp.Credentials = new NetworkCredential("projectmailserver@gmail.com", "a1d3g5j7"); //Enter with the given Username and Password
                smtp.EnableSsl = true; //Use HTTPS (Gmail standart)

                smtp.Send(mailMessage); //Send message
            }
            catch (Exception ex) //If an error was encountered, Log the error
            {
                LogError(ex);
            }
        }
    }
}