using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;

public partial class _Default : System.Web.UI.Page
{
	dal DAL = new dal();
	static string graphWeeks = "12";
	static string fromWE;
	static string toWE;

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (IsPostBack)
			{
				Control control = null;
				string controlName = Request.Params["__EVENTTARGET"];
				if (!String.IsNullOrEmpty(controlName))
				{
					control = FindControl(controlName);
					GridViewRow gvRow1 = (GridViewRow)control.Parent.Parent;
					string controlID = control.ID.ToString();
				}
			}

			if (!IsPostBack)
			{
				DateTime nextThursday = DAL.GetNextWeekday(DateTime.Now, DayOfWeek.Thursday);
				lblWeekending.Text = nextThursday.ToString("dd-MMM-yyyy");
				// ErrorText.Text = "DEBUG MODE</BR>";

			}
		}
		catch { }
	}
	private void SqlCmd(string cmd, string id = null, string text = null, string type = "reg")
	{
		try
		{
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{
				conn.Open();
				using (SqlCommand Cmd = new SqlCommand(cmd, conn))
				{
					if (id != null && text == null)
					{
						Cmd.Parameters.Add("@ID", SqlDbType.Int);
						Cmd.Parameters["@ID"].Value = Int32.Parse(id);
						Cmd.ExecuteNonQuery();
					}
					else if (text == "ddlLineNull")
					{
						Cmd.Parameters.Add("@ID", SqlDbType.Int);
						Cmd.Parameters.Add("@TEXT", SqlDbType.VarChar);
						Cmd.Parameters["@ID"].Value = Int32.Parse(id);
						Cmd.Parameters["@TEXT"].Value = DBNull.Value;
						Cmd.ExecuteNonQuery();
					}
					else if (text == "Addo_ID_Null")
					{
						Cmd.Parameters.Add("@ID", SqlDbType.Int);
						Cmd.Parameters.Add("@TEXT", SqlDbType.VarChar);
						Cmd.Parameters["@ID"].Value = Int32.Parse(id);
						Cmd.Parameters["@TEXT"].Value = DBNull.Value;
						Cmd.ExecuteNonQuery();
					}
					else if (id != null && text != null && text != "ddlLineNull")
					{
						Cmd.Parameters.Add("@ID", SqlDbType.Int);
						Cmd.Parameters.Add("@TEXT", SqlDbType.VarChar);
						Cmd.Parameters["@ID"].Value = Int32.Parse(id);
						Cmd.Parameters["@TEXT"].Value = text;
						Cmd.ExecuteNonQuery();
					}
					else
						Cmd.ExecuteNonQuery();
				}
				conn.Close();
			}
		}
		catch (SqlException ex)
		{
			//ErrorText.Text = "Invalid input";
			ErrorText.Text = ex.ToString();
		}
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

}
