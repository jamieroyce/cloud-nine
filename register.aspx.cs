using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

	dal DAL = new dal();
	
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
	public void CreateUser_Click(Object sender, EventArgs e)
	{
		string error = "";
		string username = userName.Text;
        string email = emailAddress.Text;
		string password = userPassword.Text;
		string passwordConfirm = confirmPassword.Text;
        string type = ddlUserType.SelectedValue;
        int userType = Int32.Parse(type);

        if (password.Trim() == passwordConfirm.Trim())
        {
            using (SqlConnection connection = databaseConnection.CreateSqlConnection())
            {
                String query = "INSERT into UserDetail(name, email, password, userType) "
                             + "VALUES (@name, @email, @password, @type)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@name", username);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@type", userType);
                    connection.Open();

                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                    {
                        ErrorText.Text = "Error inserting data into Database!";
                    }
                }                
            }
        }
        else
        {
			error = "Passwords did not match";
        }
		ErrorText.Text = error;

		Response.Redirect("account/dashboard.aspx");
	}
}