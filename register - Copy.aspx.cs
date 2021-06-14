using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Security.Cryptography;

public partial class _Default : System.Web.UI.Page
{
    dal DAL = new dal();
    static string graphWeeks = "12";
    static string fromWE;
    static string toWE;


    public bool IsAlphaNumeric(string text)
    {
        return Regex.IsMatch(text, "^[a-zA-Z0-9]+$");
    }

    private bool ValidateCredentials(string userName, string password)
    {
        bool returnValue = false;

        if (this.IsAlphaNumeric(userName) && userName.Length <= 50 && password.Length <= 50)
        {
            SqlConnection conn = null; try
            {
                string sql = "select count(*) from users where name = @username and password = @password";
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["reg"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter user = new SqlParameter();
                user.ParameterName = "@name";
                user.Value = userName.Trim();
                cmd.Parameters.Add(user);
                SqlParameter pass = new SqlParameter();

                //                bool verified = BCrypt.Net.BCrypt.Verify("pasfaksdljfkdlsa", passwordHash);

                pass.ParameterName = "@password";
                pass.Value = BCrypt.Net.BCrypt.HashPassword(password.Trim());
                cmd.Parameters.Add(pass);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                    returnValue = true;
            }
            catch (Exception ex)
            {
                // Log your error
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        else
        {
            // Log error - user name not alpha-numeric or 
            // username or password exceed the length limit!
        }

        return returnValue;
    }
    public void BtnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("account/dashboard.aspx");

        //DAL.Search_Addo(searchText, searchCol), "reg");
    }
    static private string GetConnectionString(string type)
    {

        if (type == "reg")
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["reg"].ConnectionString;
            return connStr;
        }
        else if (type == "cf")
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["cf"].ConnectionString;
            return connStr;
        }
        return null;
    }


    protected void CreateUser_Click(object sender, EventArgs e)
    {
        // Default UserStore constructor uses the default connection string named: DefaultConnection
        //var userStore = new UserStore<IdentityUser>();
        //var manager = new UserManager<IdentityUser>(userStore);

        //var user = new IdentityUser() { UserName = UserName.Text };
        //IdentityResult result = manager.Create(user, Password.Text);
        /*
        if (result.succeeded)
        {
            statusmessage.text = string.format("user {0} was created successfully!", user.username);
        }
        else
        {
            statusmessage.text = result.errors.firstordefault();
        }
        */
    }

}
