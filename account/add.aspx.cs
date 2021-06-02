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

				DataTable regList = DAL.reg("config"); 
				
				ddlReg_Addo.DataSource = regList;
				ddlReg_Addo.DataTextField = regList.Columns["full_name"].ToString();
				ddlReg_Addo.DataValueField = regList.Columns["full_name"].ToString();
				ddlReg_Addo.DataBind();
				
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
		
		if (Session["org"] == "Day") {
			OrgText.Text = Session["org"].ToString();
			day.Attributes["class"] += " active";		
			fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
			cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		   
		} else if(Session["org"] == "Fdn"){
			OrgText.Text = Session["org"].ToString();
			day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
			fdn.Attributes["class"] += " active";		
			cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		      
		} else {
			OrgText.Text = "Combined";
			day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
			fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
			cmb.Attributes["class"] += " active";		
		}
		
        DataGrid_Load(DAL.addo(), "cf");
		
		searchText = "";
		searchCol = "";
		searchWE = "";
		ClearAddoModal();
		
	}

	public void Day_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Day";
		Session["org"] = OrgText.Text;
		day.Attributes["class"] += " active";		
		fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
		cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		      
        
    }

	public void Fdn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Fdn";
		Session["org"] = OrgText.Text;
		day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
		fdn.Attributes["class"] += " active";		
		cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		      

    }

	public void Combined_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Combined";
		Session["org"] = OrgText.Text;
		day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
		fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
		cmb.Attributes["class"] += " active";		
        
    }
	
	protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			TableCell cell = e.Row.Cells[7];
			string addo_id = cell.Text;
			ErrorText.Text = ErrorText.Text + "AddoID=" + addo_id;

		}
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

	public void btnAddAddo_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			//string id = String.Format("{0}", 		Request.Form["id2"]);	
			string addoid = String.Format("{0}", 	Request.Form["addo_addoid"]);	
			string name = String.Format("{0}", 		Request.Form["addonameid"]);	
			string org = String.Format("{0}", 		Request.Form["addo_orgid"]);	
			string rank = String.Format("{0}", 		Request.Form["areaid"]);	
			string status = String.Format("{0}", 	Request.Form["addo_statusid"]);				
			string service = String.Format("{0}", 	Request.Form["addo_serviceid"]);	
			string amount = String.Format("{0}", 	Request.Form["add_amountid"]);	
			string reg = String.Format("{0}", 		Request.Form["ddlReg_Addo"]);	
			string bird_dog = String.Format("{0}", 	Request.Form["fsmid"]);				
			string line = String.Format("{0}", 		Request.Form["lineid3"]);				
			string scheduled = String.Format("{0}", Request.Form["apptid"]);		
			// string phone = String.Format("{0}", 	Request.Form["addophoneid"]);				
			string email  = String.Format("{0}", 	Request.Form["email"]);				
			string notes = String.Format("{0}", 	Request.Form["addo_noteid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			// amount = amount.Replace(",", "");
			// amount = amount.Replace(".", "");
			
			try{
				amount = Convert.ToInt32(Convert.ToDouble(amount)).ToString();
			}
			catch(Exception ex){
				amount = "0";
				ErrorText.Text = "ERROR: Amount was not a number.";
			}

			if (String.IsNullOrEmpty(scheduled)){
			
				using(SqlConnection connection = databaseConnection.CreateSqlConnection())
				{
					String query = "INSERT into reg(reg_cat_id, addo_id, name, status, service, amount, reg, bird_dog, line, rank, org, notes, appt, tm) "
								 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @amount, @reg, @bird_dog, @line, @rank, @org, @notes, getdate(), getdate() )";
								 
					using(SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
						command.Parameters.AddWithValue("@addo_id",			addoid);
						command.Parameters.AddWithValue("@name",			name);
						command.Parameters.AddWithValue("@status",			status);
						command.Parameters.AddWithValue("@service",			service);
						command.Parameters.AddWithValue("@amount",			amount );
						command.Parameters.AddWithValue("@reg",				reg);
						command.Parameters.AddWithValue("@bird_dog",		bird_dog);
						command.Parameters.AddWithValue("@line",			line);
						command.Parameters.AddWithValue("@rank",			rank);
						command.Parameters.AddWithValue("@org",				org);
						command.Parameters.AddWithValue("@notes",			notes);

						connection.Open();

						int result = command.ExecuteNonQuery();

						ClearAddoModal();
						
						StartingPage();
						
						// Check Error
						if(result < 0){
							ErrorText.Text = "Error inserting data into Database!";
						}
					}
				} 
			} else {
				
				using(SqlConnection connection = databaseConnection.CreateSqlConnection())
				{
					String query = "INSERT into reg(reg_cat_id, addo_id, name, status, service, amount, reg, bird_dog, line, rank, org, notes, appt, tm) "
								 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @amount, @reg, @bird_dog, @line, @rank, @org, @notes, @scheduled, getdate() )";
								 
					using(SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
						command.Parameters.AddWithValue("@addo_id",			addoid);
						command.Parameters.AddWithValue("@name",			name);
						command.Parameters.AddWithValue("@status",			status);
						command.Parameters.AddWithValue("@service",			service);
						command.Parameters.AddWithValue("@amount",			amount );
						command.Parameters.AddWithValue("@reg",				reg);
						command.Parameters.AddWithValue("@bird_dog",		bird_dog);
						command.Parameters.AddWithValue("@line",			line);
						command.Parameters.AddWithValue("@scheduled",		scheduled);
						command.Parameters.AddWithValue("@rank",			rank);
						command.Parameters.AddWithValue("@org",				org);
						command.Parameters.AddWithValue("@notes",			notes);

						connection.Open();

						int result = command.ExecuteNonQuery();

						ClearAddoModal();
						
						StartingPage();
						
						// Check Error
						if(result < 0){
							ErrorText.Text = "Error inserting data into Database!";
						}
					}
				}					
					
			}
				Response.Redirect("log.aspx");
				//back to home
		} 
    }
 
	public void ClearAddoModal()
	{
		addo_addoid.Text = "";
		addonameid.Text = "";
		addo_serviceid.Text = "";
		add_amountid.Text = "";
		fsmid.Text = "";
		// addophoneid.Text = "";
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
        // string name = Server.HtmlDecode(gvRow.Cells[2].Text);
        string name = Server.HtmlDecode(gvRow.Cells[2].Text);
		
		addonameid.Text = name;
		addo_addoid.Text = id;
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		
	}

	protected void OpenAddNew(object sender, EventArgs e)
	{
		ClearAddoModal();
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		addonameid.Text = txtAddoSearch.Text;
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
		
		if (gv.ID == "GridViewAddo"){
			
			if(searchCol != "")
				DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
			else
				DataGrid_Load(DAL.addo(), "cf");
			
		}
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
