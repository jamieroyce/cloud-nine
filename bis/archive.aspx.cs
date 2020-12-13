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
	static string status;
	
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
			DataTable regList = DAL.bis("config", "Combined", status); 
				
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
		ErrorText.Visible = false;
		ErrorText.Text = "";
		OrgText.Visible = true;
        ddlPage.Visible = true;
		AmountText.Visible = true;
        RowLable.Visible = true;		
		Title = "Archive";
		HeadText.Text = "Archive";
		HeaderText.Text = "Archive";
		status = "In The Shop";
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
		
        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		
		DataGrid_Load(DAL.get_bis_data(HeadText.Text, OrgText.Text, status), "reg");
		
	}

	public void Day_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Day";
		day.Attributes["class"] += " active";		
		fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
		cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		   
		Session["org"] = OrgText.Text;

		DataTable dt = new DataTable();
		dt = DAL.get_bis_data(HeadText.Text, OrgText.Text, status);
		DataGrid_Load(FilterTable(dt, searchCol, searchText, searchWE), "reg")	;

		// weText.Text = "";
        
    }

	public void Fdn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Fdn";
		day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
		fdn.Attributes["class"] += " active";		
		cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		      
		Session["org"] = OrgText.Text;

		DataTable dt = new DataTable();
		dt = DAL.get_bis_data(HeadText.Text, OrgText.Text, status);
		DataGrid_Load(FilterTable(dt, searchCol, searchText, searchWE), "reg")	;

		// weText.Text = "";
        
    }

	public void Combined_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Combined";
		day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
		fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
		cmb.Attributes["class"] += " active";		
		Session["org"] = OrgText.Text;

		DataTable dt = new DataTable();
		dt = DAL.get_bis_data(HeadText.Text, OrgText.Text, status);
		DataGrid_Load(FilterTable(dt, searchCol, searchText, searchWE), "reg")	;

		// weText.Text = "";
        
    }

	public DataTable FilterTable(DataTable dt, string searchCol, string searchText, string searchWE)
	{

		// DataTable newTable = new DataTable();

		if(searchText!="" && searchWE == ""){
			DataTable newTable = new DataView(dt, searchCol + " like '%" + searchText + "%'", "weekend desc", DataViewRowState.CurrentRows).ToTable();
			return newTable;

		} else if(searchText!="" && searchWE != ""){
			
			ErrorText.Text += " ACCESSED - COLUMN AND WEEKEND SEARCH </BR>";
			DataTable newTable = new DataView(dt, searchCol + " like '%" + searchText + "%' and weekend = '" + searchWE + "'", "name asc", DataViewRowState.CurrentRows).ToTable();
			return newTable;
		} else if(searchText=="" && searchWE != ""){
			
			ErrorText.Text += " ACCESSED - WEEKEND ONLY SEARCH </BR>";
			DataTable newTable = new DataView(dt, searchCol + " like '%" + searchText + "%' and weekend = '" + searchWE + "'", "name asc", DataViewRowState.CurrentRows).ToTable();
			return newTable;
		}
		return DAL.get_bis_data(HeadText.Text, OrgText.Text, status);
		
	}
		
    public void BtnSearch_Click(object sender, EventArgs e)
    {
		searchCol = ddlSearchArchive.Text;
		searchText = txtArchive.Text;
		searchWE = weText.Text;
		
		ErrorText.Text += "searchCol = " + searchCol + "</BR>";
		ErrorText.Text = "searchText = " + searchText + "</BR>";
		ErrorText.Text += "searchWE = " + searchWE + "</BR>";

		DataTable dt = new DataTable();
		dt = DAL.get_bis_data(HeadText.Text, OrgText.Text, status);
		DataGrid_Load(FilterTable(dt, searchCol, searchText, searchWE), "reg")	;

		// if (searchWE=="")
			// DataGrid_Load(DAL.ArchiveBISSearch(searchText, searchCol, OrgText.Text, status), "reg");				
		// else
			// DataGrid_Load(DAL.ArchiveBISWE_FilterSearch("weekend", searchWE, searchText, searchCol, OrgText.Text, status), "reg");				
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

		if(searchCol != "" && searchText != ""){
			if (searchWE=="")
				DataGrid_Load(DAL.ArchiveBISSearch(searchText, searchCol, OrgText.Text, status), "reg");				
			else
				DataGrid_Load(DAL.ArchiveBISWE_FilterSearch("weekend", searchWE, searchText, searchCol, OrgText.Text, status), "reg");				
		}
		else{
			if (searchWE=="")
				DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text, status), "reg");
			else
				DataGrid_Load(DAL.ArchiveBISWESearch("weekend", searchWE, OrgText.Text, status), "reg");				
		}
    }
	
    public void BtnWESearch_Click(object sender, EventArgs e)
    {

		searchWE = weText.Text;
		if(searchCol != "" && searchText != ""){
			if (searchWE=="")
				DataGrid_Load(DAL.ArchiveBISSearch(searchText, searchCol, OrgText.Text, status), "reg");				
			else
				DataGrid_Load(DAL.ArchiveBISWE_FilterSearch("weekend", searchWE, searchText, searchCol, OrgText.Text, status), "reg");				
		}
		else{
			if (searchWE=="")
				DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text, status), "reg");
			else
				DataGrid_Load(DAL.ArchiveBISWESearch("weekend", searchWE, OrgText.Text, status), "reg");				
		}
		
	}
	
	public void Org_Btn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		Button clickedButton = sender as Button;		
		Title = clickedButton.Text;
		OrgText.Text = clickedButton.Text;
		DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text, status), "reg");
        
    }

	protected void text_change(object sender, EventArgs e)
	{
		// ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
	}

	protected void DeleteRow(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		id.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteModal();", true);
		
	}

	public void btnDelete_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["id"]);	
			string sqlCommandStatement = String.Format("DELETE FROM bis WHERE id='{0}'", id );									
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
			if(searchCol != "" && searchText != ""){
				if (searchWE=="")
					DataGrid_Load(DAL.ArchiveBISSearch(searchText, searchCol, OrgText.Text, status), "reg");				
				else
					DataGrid_Load(DAL.ArchiveBISWE_FilterSearch("weekend", searchWE, searchText, searchCol, OrgText.Text, status), "reg");				
			}
			else{
				if (searchWE=="")
					DataGrid_Load(DAL.get_bis_data(HeadText.Text, OrgText.Text, status), "reg");
				else
					DataGrid_Load(DAL.ArchiveBISWESearch("weekend", searchWE, OrgText.Text, status), "reg");				
			}

		} 
    }
			
    protected void Selection_Change_Reg(object sender, EventArgs e)
    {
        DropDownList ddlReg = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET area = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlReg.Text);	
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text, status), "reg");
		
    }	

    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET status = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text, status), "reg");
		
    }

    protected void Selection_Change_ScheduledFor(object sender, EventArgs e)
    {
        DropDownList ddlSchedule = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET scheduled_type = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlSchedule.Text);		
    }

    protected void Selection_Change_Line(object sender, EventArgs e)
	{
		DropDownList ddlLine = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET line = @TEXT WHERE id=@ID";
		if(ddlLine.Text == "")
			SqlCmd(sqlCommandStatement, id, "ddlLineNull");
		else
			SqlCmd(sqlCommandStatement, id, ddlLine.Text);
	}

	protected void Selection_Change_Org(object sender, EventArgs e)
	{
		DropDownList ddlOrg = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET org = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlOrg.Text);		
	}

    protected void Selection_Change_Page(object sender, EventArgs e)
	{
		if(searchCol != "" && searchText != ""){
			if (searchWE=="")
				DataGrid_Load(DAL.ArchiveBISSearch(searchText, searchCol, OrgText.Text, status), "reg");				
			else
				DataGrid_Load(DAL.ArchiveBISWE_FilterSearch("weekend", searchWE, searchText, searchCol, OrgText.Text, status), "reg");				
		}
		else{
			if (searchWE=="")
				DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text, status), "reg");
			else
				DataGrid_Load(DAL.ArchiveBISWESearch("weekend", searchWE, OrgText.Text, status), "reg");				
		}
    }

	protected void Selection_Change(object sender, EventArgs e)
	{
		DropDownList ddlCat = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET reg_cat_id = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlCat.Text);
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text, status), "reg");

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
		try 
		{
			var query = from c in dataTable.AsEnumerable() 
					   where c.Field<string>("status") == status && c.Field<string>("reg_cat_id") == "Archive"
					  select c ;	
			
		// int a = 0;
			

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				// a = t2.Rows.Count;
				GridViewArchive.DataSource = t2;
				GridViewArchive.DataBind();

				int count = t2
					.AsEnumerable()
					.Select(r => r.Field<string>("name") + r.Field<string>("org"))
					.Distinct()
					.Count();
					
				AmountText.Text = String.Format("{0:D}", count);		
				
			} else {
				GridViewArchive.DataSource = new DataTable();
				GridViewArchive.DataBind();
			}

		// AmountText.Text = string.Format("{0}", a);
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		
	}	

	private void DataGrid_Load2(DataTable command, string type)
	{	

		DataTable dataTable = new DataTable();
        dataTable = command;

		string sortDir = ViewState["SortDirection"] as string;
		string sortExp = ViewState["SortExpression"] as string;
		
		if(ViewState["SortExpression"] != null)
		{					
			dataTable = resort(dataTable, sortExp, sortDir);
		}
		try // IN THE SHOP
		{
			
			var cmdBIS 		 	= "SELECT count(distinct org + name + convert(varchar, weekend)) AS total from bis WHERE status = '" + status + "' and reg_cat_id = 'Archive'";
			var cmdBISDay	 	= "SELECT count(distinct org + name + convert(varchar, weekend)) AS total from bis WHERE status = '" + status + "' and org = 'Day' and reg_cat_id = 'Archive'";
			var cmdBISFdn	 	= "SELECT count(distinct org + name + convert(varchar, weekend)) AS total from bis WHERE status = '" + status + "' and org = 'Fdn' and reg_cat_id = 'Archive'";

			string ResultBoth = "0";
			string ResultDay = "0";
			string ResultFdn = "0";
			
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
				conn.Open();
				try{
					
					using (SqlCommand Cmd = new SqlCommand(cmdBIS, conn))
						{
							ResultBoth = String.Format("{0:D}", Cmd.ExecuteScalar());
						}
				}
				catch(Exception e) {
					ErrorText.Text = e.ToString();
				}
				try{
					
					using (SqlCommand Cmd = new SqlCommand(cmdBISDay, conn))
						{
							ResultDay = String.Format("{0:D}", Cmd.ExecuteScalar());
						}
				}
				catch(Exception e) {
					ErrorText.Text = e.ToString();
				}
				try{
					
					using (SqlCommand Cmd = new SqlCommand(cmdBISFdn, conn))
						{
							ResultFdn = String.Format("{0:D}", Cmd.ExecuteScalar());
						}
				}
				catch(Exception e) {
					ErrorText.Text = e.ToString();
				}
				conn.Close();
			}			

			if(OrgText.Text=="Combined"){
				AmountText.Text = String.Format("{0:D}", ResultBoth);	
			} else if (OrgText.Text=="Day"){
				AmountText.Text = String.Format("{0:D}", ResultDay);	
			} else if (OrgText.Text=="Fdn"){
				AmountText.Text = String.Format("{0:D}", ResultFdn);	
			}
		
			var query = from c in dataTable.AsEnumerable() 
					   where c.Field<string>("status") == status && c.Field<string>("reg_cat_id") == "Archive"
					  select c;	
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewArchive.DataSource = t2;
				GridViewArchive.DataBind();
			} else {
				GridViewArchive.DataSource = new DataTable();
				GridViewArchive.DataBind();
			}
			
		}			
		catch(Exception e) {
			// ErrorText.Text = "Caught Exception: " + e + " -------------------------------";				
		}
		
	}	

	protected void grdArchiveData_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridViewArchive.PageIndex = e.NewPageIndex;
		
		if(searchCol != "" && searchText != ""){
			if (searchWE=="")
				DataGrid_Load(DAL.ArchiveBISSearch(searchText, searchCol, OrgText.Text, status), "reg");				
			else
				DataGrid_Load(DAL.ArchiveBISWE_FilterSearch("weekend", searchWE, searchText, searchCol, OrgText.Text, status), "reg");				
		}
		else{
			if (searchWE=="")
				DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text, status), "reg");
			else
				DataGrid_Load(DAL.ArchiveBISWESearch("weekend", searchWE, OrgText.Text, status), "reg");				
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
