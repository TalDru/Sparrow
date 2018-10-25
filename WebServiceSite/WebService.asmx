<%@ WebService Language="C#" Class="WebService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService  : System.Web.Services.WebService {
    [WebMethod]
    public void RegisterShift(string Id, string Type) //Register new shift to Shifts table with the given shift type, employee ID and current DateTime
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceConnectionString"].ConnectionString);
            conn.Open();
            string check = "INSERT INTO [InOut] (Id, DateTime, Type) VALUES (@Id, @Datetime, @Type)";
            SqlCommand checkcom = new SqlCommand(check, conn);
            checkcom.Parameters.AddWithValue("@Id", Id);
            checkcom.Parameters.AddWithValue("@Datetime", DateTime.Now);
            checkcom.Parameters.AddWithValue("@Type", Type);
            checkcom.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    [WebMethod]
    public string LastShiftInput(string ID) //Return last shift type for user with given ID
    {
        string last = "";
        try
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceConnectionString"].ConnectionString);
            conn.Open();
            string check = "SELECT TOP 1 Type FROM [InOut] WHERE Id ='"+ID+"' ORDER BY DateTime DESC" ;
            SqlCommand checkcom = new SqlCommand(check, conn);
            if (checkcom.ExecuteNonQuery()==0)
                return "O";
            else
                last = checkcom.ExecuteScalar().ToString();
            conn.Close();

        }
        catch (Exception ex)
        {
            LogError(ex);
        }
        return last;

    }

    [WebMethod]
    public DataTable ReturnWorkers(string st) //Resturn all workers from Worker table who's skill matches the given query 'st' (Search Term)
    {
        DataTable set2 = new DataTable();
        try
        {
            DataTable set = new DataTable(st);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceConnectionString"].ConnectionString); //Conect to WebService database
            conn.Open();
            string check =(st!="ALL")? "SELECT * FROM [Workers] WHERE Skills LIKE '%" + st + "%'":"SELECT * FROM [Workers]"; //Get all workers who's row contained the search term
            SqlCommand checkcom = new SqlCommand(check, conn);

            DbDataAdapter db = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
            db.SelectCommand = checkcom;

            db.Fill(set); //Fill the found workers to a DataTable
            conn.Close();

            #region Fisher-Yates shuffle for random permutation of a set

            DataRow[] rows = set.Select();
            for (int i = rows.Length - 1; i >= 0; i--) //Shuffle up the DataTable using the Fisher-Yates algorithm (switch each element with another element at a random place)
            {
                int random = new Random().Next(i);

                DataRow temp = rows[random];
                rows[random] = rows[i];
                rows[i] = temp;
            }
            #endregion

            set2 = set.Clone();
            set2.Clear();
            foreach (DataRow row in rows)
            {
                set2.ImportRow(row);
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
        return set2; //Return shuffled-up table
    }

    [WebMethod]
    public int GetBill(string ID)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceConnectionString"].ConnectionString); //Get the Connectionstring
        conn.Open(); //Open a connection
        if((new SqlCommand("SELECT Sum FROM [SearchBill] WHERE Id='" + ID+"'", conn)).ExecuteNonQuery()==0)//If no registry found, create one, else, return the bill
        {
            (new SqlCommand("INSERT INTO [SearchBill] (Id, Sum) VALUES ('"+ID+"', 0)", conn)).ExecuteNonQuery();
            return 0;
        }
        else
        {
            int Bill = int.Parse(((new SqlCommand("SELECT Sum FROM [SearchBill] WHERE Id='" + ID+"'", conn)).ExecuteScalar()??"0").ToString());
            return Bill;
        }
    }

    [WebMethod]
    public void AddToBill(string ID)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceConnectionString"].ConnectionString); //Get the Connectionstring
        conn.Open(); //Open a connection
        int Bill = GetBill(ID);
        int Sum = Bill+GetBill("SYSADMIN");
        (new SqlCommand("UPDATE [SearchBill] SET Sum="+Sum+" WHERE Id='"+ID+"'", conn)).ExecuteNonQuery();
        conn.Close(); //Close the Connection
    }

    [WebMethod]
    public void UpdateBill(string ID, int Sum)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceConnectionString"].ConnectionString); //Get the Connectionstring
        conn.Open(); //Open a connection
        (new SqlCommand("UPDATE [SearchBill] SET Sum="+Sum+" WHERE Id='"+ID+"'", conn)).ExecuteNonQuery();
        conn.Close(); //Close the Connection
    }

    public static void LogError(Exception ex)
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