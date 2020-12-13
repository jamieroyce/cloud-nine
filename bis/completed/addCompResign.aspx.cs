using System;
using System.Data;
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

				StartingPage();

				DataTable regList = DAL.bis("config"); 
				ddlReg.DataSource = regList;
				ddlReg.DataTextField = regList.Columns["desc1"].ToString();
				ddlReg.DataValueField = regList.Columns["desc1"].ToString();
				ddlReg.DataBind();
				
				txtAddoSearch.Focus();
				
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

		txtAddoSearch.Text = "";
		ErrorText.Text = "";
        
        RowLable.Visible = true;
		
        if (ViewState["SortExpression"] != null)
            ViewState["SortExpression"] = null;

		searchText = "";
		searchCol = "";
		searchWE = "";
        DataGrid_Load(DAL.addo(), "cf");
		
		searchText = "";
		searchCol = "";
		searchWE = "";
		
		ClearAddoModal();
		
	}

    public void BtnSearch_Click_Addo(object sender, EventArgs e)
    {
		searchText = "";
		searchCol = txtAddoSearch.Text;
		DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
    }
	
    protected void Selection_Change_Page(object sender, EventArgs e)
	{
		if(searchCol != "")
			DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
		else
			DataGrid_Load(DAL.addo(), "cf");
    }

	public void btnAddAddo_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string id = String.Format("{0}", 		Request.Form["id2"]);	
			string addoid = String.Format("{0}", 	Request.Form["addo_addoid"]);	
			string name = String.Format("{0}", 		Request.Form["addonameid"]);	
			string org = String.Format("{0}", 		Request.Form["addo_orgid"]);	
			string status = String.Format("{0}", 	Request.Form["addo_statusid"]);				
			string service = String.Format("{0}", 	Request.Form["addo_serviceid"]);	
			string area = String.Format("{0}", 		Request.Form["ddlReg"]);	
			string scheduled = String.Format("{0}", Request.Form["apptid3"]);		
			string notes = String.Format("{0}", 	Request.Form["addo_noteid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			if (String.IsNullOrEmpty(scheduled)){

				using(SqlConnection connection = databaseConnection.CreateSqlConnection())
				{
					String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, org, notes) "
								 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, @org, @notes)";
								 
					using(SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@reg_cat_id",		"Comp Resign");
						command.Parameters.AddWithValue("@addo_id",			addoid);
						command.Parameters.AddWithValue("@name",			name);
						command.Parameters.AddWithValue("@status",			status);
						command.Parameters.AddWithValue("@service",			service);
						command.Parameters.AddWithValue("@area",			area);
						command.Parameters.AddWithValue("@org",				org);
						command.Parameters.AddWithValue("@notes",			notes);

						connection.Open();
						int result = command.ExecuteNonQuery();
						// Check Error
						if(result < 0){
							ErrorText.Text = "Error inserting data into Database!";
						}
						ClearAddoModal();
						Response.Redirect("compresign.aspx");
						
					}
				}
			
			} else {
				
				using(SqlConnection connection = databaseConnection.CreateSqlConnection())
				{
					String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, scheduled, org, notes) "
								 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, @scheduled, @org, @notes)";
								 
					using(SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@reg_cat_id",		"Comp Resign");
						command.Parameters.AddWithValue("@addo_id",			addoid);
						command.Parameters.AddWithValue("@name",			name);
						command.Parameters.AddWithValue("@status",			status);
						command.Parameters.AddWithValue("@service",			service);
						command.Parameters.AddWithValue("@area",			area);
						command.Parameters.AddWithValue("@scheduled",		scheduled);
						command.Parameters.AddWithValue("@org",				org);
						command.Parameters.AddWithValue("@notes",			notes);

						connection.Open();
						int result = command.ExecuteNonQuery();
						if(result < 0){
							ErrorText.Text = "Error inserting data into Database!";
						}
						ClearAddoModal();
						//REDIRECT BACK 
						Response.Redirect("./compresign.aspx");
						
					}
				}
			} 
		} 
    }
 
	public void ClearAddoModal()
	{
		addo_addoid.Text = "";
		addonameid.Text = "";
		addo_serviceid.Text = "";
		addo_orgid.Text = "";
		addo_noteid.Text = "";
		
	} 
 
	protected void ViewAddo(object sender, EventArgs e)
	{
		Button clickedButton = sender as Button;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		TextInfo textInfo = cultureInfo.TextInfo;
		
        string id = gvRow.Cells[0].Text;
        string name = gvRow.Cells[2].Text;
		
		addonameid.Text = name;
		addo_addoid.Text = id;
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		
	}

	protected void OpenAddNew(object sender, EventArgs e)
	{
		ClearAddoModal();
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		addonameid.Focus();
	}
	
	private void DataGrid_Load(DataTable command, string type)
	{	

		DataTable dataTable = new DataTable();
        dataTable = command;

		string sortDir = ViewState["SortDirection"] as string;
		string sortExp = ViewState["SortExpression"] as string;
		
		if(ViewState["SortExpression"] != null)
		{					
			dataTable = resort(dataTable, sortExp, sortDir);
		}
		
		GridViewAddo.DataSource = dataTable;
		GridViewAddo.DataBind();
		
	}	

    protected void grdData_PageIndexChanging_addo(object sender, GridViewPageEventArgs e)
	{
		GridViewAddo.PageIndex = e.NewPageIndex;

		if(searchCol != "")
			DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
		else
			DataGrid_Load(DAL.addo(), "cf");
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
			
		if(searchCol != "")
			DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
		else
			DataGrid_Load(DAL.addo(), "cf");

	}

    public static DataTable resort(DataTable dt, string colName, string direction)
	{
		dt.DefaultView.Sort = colName + " " + direction;
		dt = dt.DefaultView.ToTable();
		return dt;
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
