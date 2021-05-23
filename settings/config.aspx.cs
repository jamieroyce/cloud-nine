using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Globalization;
using System.Threading;
using System.IO;
using System.Configuration;
using System.Drawing;


public partial class _Default : System.Web.UI.Page 
{  	

    dal DAL = new dal();

	static string searchText;
	static string searchCol;
	static string searchWE;
	static string showDay;

	protected void Page_Load(object sender, EventArgs e)
	{                
	try{
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

		if(!IsPostBack)
		{
			DataTable regList = DAL.reg("config"); 
			StartingPage();
		}		
		
	}
	catch{}
	}

	private int _tabIndex = 0;

	public int TabIndex
	{
		get
		{
			_tabIndex++;
			return _tabIndex;
		}
	}	

	public void StartingPage()
	{

		ErrorText.Text = "";
		Title = "Configure Settings";
		HeadText.Text = "Configure Settings";
		searchText = "";
		searchCol = "";
		searchWE = "";
		
        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		
		GridView_Load(GridViewReg, DAL.getRegList()); 
		GridView_Load(GridViewExpectancy, DAL.getExpectancy()); 
		
	}

		
	private void GridView_Load(GridView grdview, DataTable dt)
	{	

		string sortDir = ViewState["SortDirection"] as string;
		string sortExp = ViewState["SortExpression"] as string;
		
		if(ViewState["SortExpression"] != null)
		{					
			dt = resort(dt, sortExp, sortDir);
		}
	
		grdview.DataSource = dt;
		grdview.DataBind();

	}
	
	public void ClearAddRegModal()
	{

        regnameID.Text = "";
		
	} 
	
	public void btnAddReg_Click(Object sender, EventArgs e)
	{				
		
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string fullName = String.Format("{0}", 		Request.Form["regnameID"]);	
			string shortName = String.Format("{0}", 	Request.Form["shortnameID"]);	
			string area = String.Format("{0}", 			Request.Form["area"]);	
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			fullName = textInfo.ToTitleCase(fullName.ToLower());
					
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into listReg(full_name, short_name, area) "
							 + "VALUES (@full_name, @short_name, @area)";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{

					command.Parameters.AddWithValue("@full_name",		fullName);
					command.Parameters.AddWithValue("@short_name",		shortName);
					command.Parameters.AddWithValue("@area",			area);

					connection.Open();
					int result = command.ExecuteNonQuery();

					ClearAddRegModal();
					
					// Check Error
					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
				}
			}
		} 

		GridView_Load(GridViewReg, DAL.getRegList()); 
		
    }

	public void btnAddQuota_Click(Object sender, EventArgs e)
	{				
		
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string type = "expectancy";	
			string area = String.Format("{0}", 		Request.Form["shortID"]);	
			string quota = String.Format("{0}", 	Request.Form["quotaID2"]);	
					
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into lookup(type, desc1, desc2) "
							 + "VALUES (@type, @area, @quota)";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{

					command.Parameters.AddWithValue("@type",			type);
					command.Parameters.AddWithValue("@area",			area);
					command.Parameters.AddWithValue("@quota",			quota);

					connection.Open();
					int result = command.ExecuteNonQuery();

					ClearAddRegModal();
					
					// Check Error
					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
				}
			}
		} 

		GridView_Load(GridViewExpectancy, DAL.getExpectancy());  
		
    }

	protected void DeleteRow(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		RegistrarID.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteRegModal();", true);
		
	}

	protected void DeleteQuota(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		QuotaID.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteQuotaModal();", true);
		
	}

	public void btnDeleteReg_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["RegistrarID"]);	
			string sqlCommandStatement = String.Format("DELETE FROM listReg WHERE id='{0}'", id );									
			try
			{		
				using (SqlConnection conn = databaseConnection.CreateSqlConnection())
				{
				conn.Open();
				using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement, conn))
				{
					Cmd.ExecuteNonQuery();						
				}
				conn.Close();
				}
			}
			catch (SqlException ex)
			{
				ErrorText.Text = ex.ToString();
			}
			GridView_Load(GridViewReg, DAL.getRegList()); 
		} 
    }

	public void btnDeleteQuota_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["RegistrarID"]);	
			string sqlCommandStatement = String.Format("DELETE FROM lookup WHERE id='{0}'", id );									
			try
			{		
				using (SqlConnection conn = databaseConnection.CreateSqlConnection())
				{
				conn.Open();
				using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement, conn))
				{
					Cmd.ExecuteNonQuery();						
				}
				conn.Close();
				}
			}
			catch (SqlException ex)
			{
				ErrorText.Text = ex.ToString();
			}
			GridView_Load(GridViewExpectancy, DAL.getExpectancy());  
		} 
    }

	protected void text_change_reg(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE listReg SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		// ErrorText.Text = "sqlCommandStatement=" + sqlCommandStatement + "<BR/>";
		GridView_Load(GridViewReg, DAL.getRegList()); 
		
	}

	protected void text_change_desc2(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE lookup SET desc2 = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		// ErrorText.Text = "sqlCommandStatement=" + sqlCommandStatement + "<BR/>";
		GridView_Load(GridViewExpectancy, DAL.getExpectancy());  
		
	}

	protected void Selection_Change_Desc1(object sender, EventArgs e)
	{
		DropDownList desc1 = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE lookup SET desc1 = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, desc1.Text);		
		GridView_Load(GridViewExpectancy, DAL.getExpectancy());  
	}

	protected void Selection_Change_Area(object sender, EventArgs e)
	{
		DropDownList ddlArea = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE listReg SET area = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlArea.Text);	
		GridView_Load(GridViewReg, DAL.getRegList()); 
	}
	
	protected void Selection_Change_Regno(object sender, EventArgs e)
	{
		DropDownList short_name = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE listReg SET short_name = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, short_name.Text);		
		// ErrorText.Text = "UPDATE listReg SET " + id + " = " + short_name.Text + "where ID = " + id + "<BR/>";
		// ErrorText.Text = "short_name.Text=" + short_name.Text + "<BR/>";
		GridView_Load(GridViewReg, DAL.getRegList()); 
	}
	
	protected void TaskGridView_Sorting(object sender, GridViewSortEventArgs e)
	{				
		string sortExp = ViewState["SortExpression"] as string;		
		string sortDir = ViewState["SortDirection"] as string;

		if(sortDir == "asc" & sortExp == e.SortExpression.ToString())
			ViewState["SortDirection"] = "desc";
		else
			ViewState["SortDirection"] = "asc";

		ViewState["SortExpression"] = e.SortExpression.ToString();
		GridView gv = sender as GridView;
		GridView_Load(GridViewReg, DAL.getRegList()); 
		GridView_Load(GridViewExpectancy, DAL.getExpectancy());  
		
	}
	
    public static DataTable resort(DataTable dt, string colName, string direction)
	{
		dt.DefaultView.Sort = colName + " " + direction;
		dt = dt.DefaultView.ToTable();
		return dt;
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
					if(id != null && text == null)
					{
						Cmd.Parameters.Add("@ID", SqlDbType.Int);
						Cmd.Parameters["@ID"].Value = Int32.Parse(id);
						Cmd.ExecuteNonQuery();
					}
					else if(text == "ddlLineNull")
					{
						Cmd.Parameters.Add("@ID", SqlDbType.Int);
						Cmd.Parameters.Add("@TEXT", SqlDbType.VarChar);
						Cmd.Parameters["@ID"].Value = Int32.Parse(id);
						Cmd.Parameters["@TEXT"].Value = DBNull.Value;
						Cmd.ExecuteNonQuery();
					}
					else if(text == "Addo_ID_Null")
					{
						Cmd.Parameters.Add("@ID", SqlDbType.Int);
						Cmd.Parameters.Add("@TEXT", SqlDbType.VarChar);
						Cmd.Parameters["@ID"].Value = Int32.Parse(id);
						Cmd.Parameters["@TEXT"].Value = DBNull.Value;
						Cmd.ExecuteNonQuery();
					}
					else if(id != null && text != null && text != "ddlLineNull")
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
