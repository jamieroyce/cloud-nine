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

using OfficeOpenXml;

public partial class _Default : System.Web.UI.Page 
{  	

    dal DAL = new dal();

	static string searchText;
	static string searchCol;
	static string searchWE;
	static string status = "Event";
	static string vCat = "Event";
	static string vStatus1 = "Attended";
	static string vStatus2 = "Reconfirmed";
	static string vStatus3 = "Confirmed";
	static string vStatus4 = "Maybe";
	static string vStatus5 = "Not Coming";

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

			DataTable eventList = DAL.getEvents(); 
			ddl_Event.DataSource = eventList;
			ddl_Event.DataTextField = eventList.Columns["event_desc"].ToString();
			ddl_Event.DataValueField = eventList.Columns["event_desc"].ToString();
			ddl_Event.DataBind();
			
			DataTable eventDetailList = DAL.getEvents(); 
			ddl_EventDetail.DataSource = eventList;
			ddl_EventDetail.DataTextField = eventList.Columns["event_desc"].ToString();
			ddl_EventDetail.DataValueField = eventList.Columns["event_desc"].ToString();
			ddl_EventDetail.DataBind();

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
		HeadText.Text = "Event Confirms Log";
		HeaderText.Text = "Event Confirms Log";
		
		OrgText.Visible = true;
		ErrorText.Text = "";
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

		DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

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
		DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
        
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
		DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
        
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
		DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

	}

	protected void Event_Changed(object sender, EventArgs e)
	{

		// string message = ddl_Event.SelectedItem.Text + " - " + ddl_Event.SelectedItem.Value;
		ErrorText.Text += "Changed the Event to: " + ddl_EventDetail.SelectedItem.Text;
		
		string v = ddl_EventDetail.SelectedItem.Text;
		
		DataTable dt = DAL.getEvent(v);
		
		string vNotes = (from DataRow dr in dt.Rows
					  where (string)dr["event_desc"] == v
					  select (string)dr["notes"]).FirstOrDefault();		
		
		string vName = (from DataRow dr in dt.Rows
					  where (string)dr["event_desc"] == v
					  select (string)dr["name"]).FirstOrDefault();		

		lblEventDetail.Text = vNotes;
		lblEventCode.Text = vName;
		
	}	

	protected void ExportToExcel_Click(object sender, EventArgs e)
	{
		// var products = GetProducts();
		var data = DAL.get_log(vCat, OrgText.Text);
		ExcelPackage excel = new ExcelPackage();
		var workSheet = excel.Workbook.Worksheets.Add(HeadText.Text);
		var totalCols = data.Columns.Count;
		var totalRows = data.Rows.Count;

		for (var col = 1; col <= totalCols; col++)
		{
			workSheet.Cells[1, col].Value = data.Columns[col-1].ColumnName;
		}
		for (var row = 1; row <= totalRows; row++)
		{
			for (var col = 0; col < totalCols; col++)
			{
				workSheet.Cells[row + 1, col + 1].Value = data.Rows[row - 1][col];
			}
		}
		using (var memoryStream = new MemoryStream())
		{
			Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			Response.AddHeader("content-disposition", "attachment;  filename=" + HeadText.Text  + ".xlsx");
			excel.SaveAs(memoryStream);
			memoryStream.WriteTo(Response.OutputStream);
			Response.Flush();
			Response.End();
		}
	}	
	
	protected void OnSelectedIndexChanged(object sender, EventArgs e)
	{

		HeaderText.Text = HeadText.Text;
		// string message = ddl_Event.SelectedItem.Text + " - " + ddl_Event.SelectedItem.Value;
		// ErrorText.Text += "Changed the Event to: " + ddl_Event.SelectedItem.Text;
		HeaderText.Text += " (" + ddl_Event.SelectedItem.Text + ")";
		
		//PASS THE DATA TO FILTER SEARCH 
		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
		
		
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

    public void BtnSearch_Click(object sender, EventArgs e)
    {
		searchText = ddlSearchGI.Text;
		searchCol = txtBIS.Text;
		DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		
    }

	protected void text_change(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		
		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
		
	}

	protected void text_change_date(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		
		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
		
	}
	
	protected void text_change_addo(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);		
		SqlCmd(sqlCommandStatement, id, text.Text);

		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");

	}

    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET status = @TEXT, scheduled = getdate(), weekend = getdate() + ( 5 - datepart(dw,getdate()) + 7) % 7  WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	

		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
		
    }

    protected void Selection_Change_ScheduledFor(object sender, EventArgs e)
    {
        DropDownList ddlSchedule = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET scheduled_type = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlSchedule.Text);

		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
		
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
		
		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
		
	}

	protected void Selection_Change_Org(object sender, EventArgs e)
	{
		DropDownList ddlOrg = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET org = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlOrg.Text);		
		
		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
		
	}

	protected void Selection_Change(object sender, EventArgs e)
	{
		DropDownList ddlCat = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET reg_cat_id = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlCat.Text);

		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");

	}

	protected void Rank_Change(object sender, EventArgs e)
	{
		DropDownList ddlRank = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET rank = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlRank.Text);		

		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
		
	}
	
	protected void Display(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		
		TextBox tb = (TextBox)row.FindControl("name");
		string vTxt = tb.Text; // get the value from TextBox		
		lblnameid.Text = vTxt;

		tb = (TextBox)row.FindControl("reg");
		vTxt = tb.Text; // get the value from TextBox		
		regid.Text = vTxt;

		DropDownList dl = (DropDownList)row.FindControl("ddlStatus");
		vTxt = dl.Text; // get the value from TextBox		
		statusid.Text = vTxt;

		dl = (DropDownList)row.FindControl("ddlOrg");
		vTxt = dl.Text; // get the value from TextBox		
		orgid.Text = vTxt;

		tb = (TextBox)row.FindControl("notes");
		vTxt = tb.Text; // get the value from TextBox		
		notesid.Text = vTxt;
	
		string vId = row.Cells[0].Text;		
		id.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
		
	}
	
	protected void DeleteRow(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		id.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteModal();", true);
		
	}
 	
	public void btnArchive_Click(Object sender, EventArgs e)
	{				
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

			DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");

		} 
    }
	
	public void btnUpdate_Click(Object sender, EventArgs e)
	{				

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string id = String.Format("{0}", 		Request.Form["id"]);	
			string name = String.Format("{0}", 		Request.Form["lblnameid"]);	
			string reg = String.Format("{0}", 		Request.Form["regid"]);	
			string status = String.Format("{0}", 	Request.Form["statusid"]);				
			string org = String.Format("{0}", 		Request.Form["orgid"]);				
			string notes = String.Format("{0}", 	Request.Form["notesid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			string sqlCommandStatement = String.Format("UPDATE bis SET name = '{0}', " + 
															" reg 			= '{1}', " + 
															" status	 	= '{2}', " + 
															" org		 	= '{3}', " + 
															" notes	 		= '{4}' " + 
															" WHERE id='{5}'", 
															name.Replace("'", "''"), 
															reg.Replace("'", "''"), 
															status, 
															org,
															notes.Replace("'", "''"), 
															id
															);									
			
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
			
			DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
				
			
		}
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
		
		    var query = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus1 && c.Field<string>("reg_cat_id") == vCat
				      select c).Distinct();

		    var queryDay = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus1 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Day"
				      select c).Distinct() ;

		    var queryFdn = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus1 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Fdn"
				      select c).Distinct() ;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView1.DataSource = t2;
				GridView1.DataBind();
				LabelStatus1.Text = String.Format("{0:D}", query.Count());					
				// LabelStatus1Top.Text = String.Format("{0:D}", query.Count());	
			} else {
				LabelStatus1.Text = String.Format("{0:D}", "0");					
				// LabelStatus1Top.Text = String.Format("{0:D}", "0");					
				GridView1.DataSource = new DataTable();
				GridView1.DataBind();
			}
			// if(queryDay.Any())
				// LabelStatus1Day.Text = String.Format("{0:D}", queryDay.Count());	
			// else 
				// LabelStatus1Day.Text = String.Format("{0:D}", "0");	
			// if(queryFdn.Any())
				// LabelStatus1Fdn.Text = String.Format("{0:D}", queryFdn.Count());	
			// else 
				// LabelStatus1Fdn.Text = String.Format("{0:D}", "0");	
		
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try 
		{
		
		    var query = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus2 && c.Field<string>("reg_cat_id") == vCat
				      select c).Distinct();

		    var queryDay = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus2 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Day"
				      select c).Distinct() ;

		    var queryFdn = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus2 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Fdn"
				      select c).Distinct() ;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView2.DataSource = t2;
				GridView2.DataBind();
				LabelStatus2.Text = String.Format("{0:D}", query.Count());					
				LabelStatus2Top.Text = String.Format("{0:D}", query.Count());	
			} else {
				LabelStatus2.Text = String.Format("{0:D}", "0");					
				LabelStatus2Top.Text = String.Format("{0:D}", "0");					
				GridView2.DataSource = new DataTable();
				GridView2.DataBind();
			}
			if(queryDay.Any())
				LabelStatus2Day.Text = String.Format("{0:D}", queryDay.Count());	
			else 
				LabelStatus2Day.Text = String.Format("{0:D}", "0");	
			if(queryFdn.Any())
				LabelStatus2Fdn.Text = String.Format("{0:D}", queryFdn.Count());	
			else 
				LabelStatus2Fdn.Text = String.Format("{0:D}", "0");	
		
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try 
		{
			
			
		
		    var query = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus3 && c.Field<string>("reg_cat_id") == vCat
				      select c).Distinct();

		    var queryDay = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus3 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Day"
				      select c).Distinct() ;

		    var queryFdn = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus3 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Fdn"
				      select c).Distinct() ;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView3.DataSource = t2;
				GridView3.DataBind();
				LabelStatus3.Text = String.Format("{0:D}", query.Count());					
				LabelStatus3Top.Text = String.Format("{0:D}", query.Count());	
			} else {
				LabelStatus3.Text = String.Format("{0:D}", "0");					
				LabelStatus3Top.Text = String.Format("{0:D}", "0");					
				GridView3.DataSource = new DataTable();
				GridView3.DataBind();
			}
			if(queryDay.Any())
				LabelStatus3Day.Text = String.Format("{0:D}", queryDay.Count());	
			else 
				LabelStatus3Day.Text = String.Format("{0:D}", "0");	
			if(queryFdn.Any())
				LabelStatus3Fdn.Text = String.Format("{0:D}", queryFdn.Count());	
			else 
				LabelStatus3Fdn.Text = String.Format("{0:D}", "0");	
		
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try 
		{
		    var query = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus4 && c.Field<string>("reg_cat_id") == vCat
				      select c).Distinct();

		    var queryDay = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus4 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Day"
				      select c).Distinct() ;

		    var queryFdn = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus4 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Fdn"
				      select c).Distinct() ;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView4.DataSource = t2;
				GridView4.DataBind();
				LabelStatus4.Text = String.Format("{0:D}", query.Count());					
				LabelStatus4Top.Text = String.Format("{0:D}", query.Count());	
			} else {
				LabelStatus4.Text = String.Format("{0:D}", "0");					
				LabelStatus4Top.Text = String.Format("{0:D}", "0");					
				GridView4.DataSource = new DataTable();
				GridView4.DataBind();
			}
			if(queryDay.Any())
				LabelStatus4Day.Text = String.Format("{0:D}", queryDay.Count());	
			else 
				LabelStatus4Day.Text = String.Format("{0:D}", "0");	
			if(queryFdn.Any())
				LabelStatus4Fdn.Text = String.Format("{0:D}", queryFdn.Count());	
			else 
				LabelStatus4Fdn.Text = String.Format("{0:D}", "0");	
		
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}		
		try 
		{
		
		    var query = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus5 && c.Field<string>("reg_cat_id") == vCat
				      select c).Distinct();

		    var queryDay = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus5 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Day"
				      select c).Distinct() ;

		    var queryFdn = (from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == vStatus5 && c.Field<string>("reg_cat_id") == vCat && c.Field<string>("org")=="Fdn"
				      select c).Distinct() ;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView5.DataSource = t2;
				GridView5.DataBind();
				LabelStatus5.Text = String.Format("{0:D}", query.Count());					
				// LabelStatus5Top.Text = String.Format("{0:D}", query.Count());	
			} else {
				LabelStatus5.Text = String.Format("{0:D}", "0");					
				// LabelStatus5Top.Text = String.Format("{0:D}", "0");					
				GridView5.DataSource = new DataTable();
				GridView5.DataBind();
			}
			// if(queryDay.Any())
				// LabelStatus5Day.Text = String.Format("{0:D}", queryDay.Count());	
			// else 
				// LabelStatus5Day.Text = String.Format("{0:D}", "0");	
			// if(queryFdn.Any())
				// LabelStatus5Fdn.Text = String.Format("{0:D}", queryFdn.Count());	
			// else 
				// LabelStatus5Fdn.Text = String.Format("{0:D}", "0");	
		
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
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

		DataGrid_Load(DAL.Search_Combo_BIS("service", ddl_Event.SelectedItem.Text), "reg");
		
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
