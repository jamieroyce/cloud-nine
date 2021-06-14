using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

	dal DAL = new dal();
	protected void CreateUser_Click(object sender, EventArgs e)
	{


        /*
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

        */

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
