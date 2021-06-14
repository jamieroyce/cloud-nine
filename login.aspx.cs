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

    protected void LoginControl_Authenticate(object sender, AuthenticateEventArgs e)
    {

    }

    public bool IsAlphaNumeric(string text)
    {
        return Regex.IsMatch(text, "^[a-zA-Z0-9]+$");
    }

    private bool ValidateCredentials(string userName, string password)
    {
        bool returnValue = false;
        string sql = "select count(*) from UserDetail where name = @username and password = @password";

        if (this.IsAlphaNumeric(userName) && userName.Length <= 50 && password.Length <= 50)
        {
            try
            {
                using (SqlConnection conn = databaseConnection.CreateSqlConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlParameter user = new SqlParameter();
                        user.ParameterName = "@username";
                        user.Value = userName.Trim();
                        cmd.Parameters.Add(user);
                        SqlParameter pass = new SqlParameter();
                        //                bool verified = BCrypt.Net.BCrypt.Verify("pasfaksdljfkdlsa", passwordHash);
                        pass.ParameterName = "@password";
                        //                pass.Value = BCrypt.Net.BCrypt.HashPassword(password.Trim()); 
                        pass.Value = password.Trim();
                        cmd.Parameters.Add(pass);
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                            returnValue = true;
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                //ErrorText.Text = "Invalid input";
                //ErrorText.Text = ex.ToString();
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

        string username = userName.Text;
        string password = userPassword.Text;
        string error = "";



        DataTable dt = new DataTable();
        //dt = DAL.GetUser("jamieroyce", "test");

        //dt = DAL.GetUsers();
        //if (dt.Rows.Count > 0)
        //{
        //    Response.Redirect("account/dashboard.aspx");
        //}
        //else
        //{
        //    error = "Incorrect User Name or Password. Please try again.";
        //}



        if (ValidateCredentials(username, password)) {
            Response.Redirect("account/dashboard.aspx");
        } else {
            error = "Incorrect User Name or Password. Please try again.";
        }

        // Check if the user name and password exists in the database
        //DAL.Search_Addo(searchText, searchCol), "reg");



    }
	
}
