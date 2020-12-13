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
	static string status = "First Service";
	static string vCat = "First Service";

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
			DataTable regList = DAL.fss("config"); 
			ddlReg.DataSource = regList;
			ddlReg.DataTextField = regList.Columns["desc1"].ToString();
			ddlReg.DataValueField = regList.Columns["desc1"].ToString();
			ddlReg.DataBind();
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

		PopulateAreas();
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

	private void PopulateAreas()
	{
		DataTable dt = new DataTable();		
		dt = DAL.ReportByArea(status);
				
		foreach(DataRow row in dt.Rows)
		{
			if(row["org"].ToString()=="Day"){
				if(row["area"].ToString()=="FSM"){
					fsmd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="BB"){
					bbd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="Walk In"){
					wid.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="4D Campaign"){
					campaignd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="Body Routing"){
					brd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="Prospecting"){
					prospectingd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="Field Group"){
					fieldgroupd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="OCA"){
					ocad.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="SCN TV"){
					scntvd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="- Other"){
					otherd.Text = row["cnt"].ToString();
				}
			}else{
				if(row["area"].ToString()=="FSM"){
					fsmf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="BB"){
					bbf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="Walk In"){
					wif.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="4D Campaign"){
					campaignf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="Body Routing"){
					brf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="Prospecting"){
					prospectingf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="Field Group"){
					fieldgroupf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="OCA"){
					ocaf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="SCN TV"){
					scntvf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="- Other"){
					otherf.Text = row["cnt"].ToString();
				}
			}
		}
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

	protected void ExportToExcel_Click(object sender, EventArgs e)
	{

		var data = DAL.get_log(vCat, OrgText.Text);
		ExcelPackage excel = new ExcelPackage();
		var workSheet = excel.Workbook.Worksheets.Add(vCat);
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
			Response.AddHeader("content-disposition", "attachment;  filename=" + vCat  + ".xlsx");
			excel.SaveAs(memoryStream);
			memoryStream.WriteTo(Response.OutputStream);
			Response.Flush();
			Response.End();
		}
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
	}
	
	protected void text_change_addo(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);		
		SqlCmd(sqlCommandStatement, id, text.Text);
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

	}

	protected void text_change_date(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		

	}
	
    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
        string id = row.Cells[0].Text;
		
        string sqlCommandStatement = "UPDATE bis SET status = @TEXT, scheduled = getdate(), weekend = getdate() WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);

		if(ddlStatus.Text=="First Service"){
			//ADD TO PAID START AS WELL 

			TextBox tb = row.FindControl("name") as TextBox;
			string name = tb.Text;
			
			tb = row.FindControl("service") as TextBox;
			string service = tb.Text;

			string addoid = row.Cells[1].Text;

			string line = "Other";
			string status = "Started";
			string rank = "PS";
			string area = "DIV6";
			string notes = "First Start added from log.";

			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			// AlertPanel.Text = "addoid = " + addoid + "</br>";
			// AlertPanel.Text += "name = " + name +  "</br>";
			// AlertPanel.Text += "service = " + service + "</br>";
			// ModalPopupExtender2.Show();	

			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, rank, line, scheduled, weekend, org, notes) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, @rank, @line, getdate(), getdate(), @org, @notes)";

				using(SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();

					command.Parameters.AddWithValue("@reg_cat_id",		"Paid Start");
					command.Parameters.AddWithValue("@addo_id",			addoid);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@status",			status);
					command.Parameters.AddWithValue("@service",			service);
					command.Parameters.AddWithValue("@area",			area);
					command.Parameters.AddWithValue("@rank",			rank);
					command.Parameters.AddWithValue("@line",			line);
					command.Parameters.AddWithValue("@notes",			notes);
					command.Parameters.AddWithValue("@org",				OrgText.Text);

					try{
						int result = command.ExecuteNonQuery();
						if(result < 0){
							AlertPanel.Text = "Error inserting data into Database!</br>";
							ModalPopupExtender2.Show();				
						} else {
							// AlertPanel.Text += "Success! " + name + " was added to the Named Starts for the Week for: " + service + ".</br>";
							// ModalPopupExtender2.Show();	
						}
					} 
					catch (SqlException ex)
					{
						AlertPanel.Text =  "ERROR: " + ex.ToString();
						ModalPopupExtender2.Show();				
					}
					connection.Close();
				}
			}	
		}
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
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

	protected void Selection_Change(object sender, EventArgs e)
	{
		DropDownList ddlCat = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET reg_cat_id = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlCat.Text);
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

	}

	protected void Display(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		
		TextBox tb = (TextBox)row.FindControl("name");
		string vTxt = tb.Text; // get the value from TextBox		
		lblnameid.Text = vTxt;
		
		tb = (TextBox)row.FindControl("service");
		vTxt = tb.Text; // get the value from TextBox		
		serviceid.Text = vTxt;

		tb = (TextBox)row.FindControl("reg");
		vTxt = tb.Text; // get the value from TextBox		
		regid.Text = vTxt;

		DropDownList dl = (DropDownList)row.FindControl("ddlReg");
		ddlReg.Text = dl.Text;

		dl = (DropDownList)row.FindControl("ddlStatus");
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
		//GridViewRow row = GridView1.Rows[rowIndex];
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
			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

		} 
    }
	
	public void btnUpdate_Click(Object sender, EventArgs e)
	{				

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			// int id  = Request.Form["id"];	
			string id = String.Format("{0}", 		Request.Form["id"]);	
			string name = String.Format("{0}", 		Request.Form["lblnameid"]);	
			string service = String.Format("{0}", 	Request.Form["serviceid"]);	
			string area = String.Format("{0}", 		Request.Form["ddlReg"]);	
			string reg = String.Format("{0}", 		Request.Form["regid"]);	
			string status = String.Format("{0}", 	Request.Form["statusid"]);				
			string fsm = String.Format("{0}", 		Request.Form["fsmid"]);				
			string org = String.Format("{0}", 		Request.Form["orgid"]);				
			string notes = String.Format("{0}", 	Request.Form["notesid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			string sqlCommandStatement = String.Format("UPDATE bis SET name = '{0}', " + 
															" service 		= '{1}', " + 
															" reg 			= '{2}', " + 
															" status	 	= '{3}', " + 
															" fsm		 	= '{4}', " + 
															" org		 	= '{5}', " + 
															" area		 	= '{6}', " + 
															" notes	 		= '{7}' " + 
															" WHERE id='{8}'", 
															name.Replace("'", "''"), 
															service.Replace("'", "''"), 
															reg.Replace("'", "''"), 
															status, 
															fsm.Replace("'", "''"), 
															org,
															area,
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

			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
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
			DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
		
    }
	
	private void DataGrid_Load(DataTable command, string type)
	{	
		DataTable dataTable = new DataTable();
        dataTable = command;
		string[] ComboGiGridTotal = new string[5];
		string sortDir = ViewState["SortDirection"] as string;
		string sortExp = ViewState["SortExpression"] as string;
		
		if(ViewState["SortExpression"] != null)
		{					
			dataTable = resort(dataTable, sortExp, sortDir);
		}

		PopulateAreas();		
		
		try // STARTED
		{
			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == status
				where c.Field<string>("reg_cat_id") == vCat
				select c ;

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewInStarted.DataSource = t2;
				GridViewInStarted.DataBind();				
			} else {
				//ErrorTextA.Text = "No Data Found";	
				GridViewInStarted.DataSource = new DataTable();
				GridViewInStarted.DataBind();
			}
			
			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and reg_cat_id = '" + vCat + "'";
			string result1 = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						result1 = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			string s = String.Format("{0:D}", result1);	
			Both.Text = s;

			var cmdDay = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and org = 'Day' and reg_cat_id = '" + vCat + "'";
			result1 = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdDay, conn))
					{
						result1 = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			s = String.Format("{0:D}", result1);	
			lblDay.Text = s;	

			var cmdFdn = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and org = 'Fdn' and reg_cat_id = '" + vCat + "'";
			result1 = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdFdn, conn))
					{
						result1 = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			s = String.Format("{0:D}", result1);	
			lblFdn.Text = s;	

			if(OrgText.Text=="Combined"){
				lblTot.Text = Both.Text;
			}else if(OrgText.Text=="Day"){
				lblTot.Text = lblDay.Text;
			}else if(OrgText.Text=="Fdn"){
				lblTot.Text = lblFdn.Text;
			}
			
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught STARTED Exception: " + e;				
		}
		try // SCHEDULED
		{
			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Scheduled" 
				where c.Field<string>("reg_cat_id") == vCat
				select c ;

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewScheduled.DataSource = t2;
				GridViewScheduled.DataBind();				
			} else {
				//ErrorTextA.Text = "No Data Found";	
				GridViewScheduled.DataSource = new DataTable();
				GridViewScheduled.DataBind();
			}
			
			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Scheduled' and reg_cat_id = '" + vCat + "'";
			string result1 = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						result1 = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			string s = String.Format("{0:D}", result1);	
			Sched.Text = s;

			var cmdDay = "SELECT COUNT(1) AS total from bis WHERE status = 'Scheduled' and org = 'Day' and reg_cat_id = '" + vCat + "'";
			result1 = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdDay, conn))
					{
						result1 = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			s = String.Format("{0:D}", result1);	
			DaySched.Text = s;	

			var cmdFdn = "SELECT COUNT(1) AS total from bis WHERE status = 'Scheduled' and org = 'Fdn' and reg_cat_id = '" + vCat + "'";
			result1 = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdFdn, conn))
					{
						result1 = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			s = String.Format("{0:D}", result1);	
			FdnSched.Text = s;	

			if(OrgText.Text=="Combined"){
				LblSched.Text = Sched.Text;
			}else if(OrgText.Text=="Day"){
				LblSched.Text = DaySched.Text;
			}else if(OrgText.Text=="Fdn"){
				LblSched.Text = FdnSched.Text;
			}
			
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught SCHEDULED Exception: " + e;				
		}
		try // NAMED
		{

			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Named" 
				where c.Field<string>("reg_cat_id") == vCat
				select c ;

			var mySum = 0;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewNamed.DataSource = t2;
				GridViewNamed.DataBind();
				
			} else {
				GridViewNamed.DataSource = new DataTable();
				GridViewNamed.DataBind();
			}

			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Named' and reg_cat_id = '" + vCat + "'";
			string result1 = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						result1 = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			string s = String.Format("{0:D}", result1);	
			Named.Text = s;

			var cmdDay = "SELECT COUNT(1) AS total from bis WHERE status = 'Named' and org = 'Day' and reg_cat_id = '" + vCat + "'";
			result1 = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdDay, conn))
					{
						result1 = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			s = String.Format("{0:D}", result1);	
			DayNamed.Text = s;	

			var cmdFdn = "SELECT COUNT(1) AS total from bis WHERE status = 'Named' and org = 'Fdn' and reg_cat_id = '" + vCat + "'";
			result1 = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdFdn, conn))
					{
						result1 = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			s = String.Format("{0:D}", result1);	
			FdnNamed.Text = s;	
			
			if(OrgText.Text=="Combined"){
				LblNamed.Text = Named.Text;
			}else if(OrgText.Text=="Day"){
				LblNamed.Text = DayNamed.Text;
			}else if(OrgText.Text=="Fdn"){
				LblNamed.Text = FdnNamed.Text;
			}
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught NAMED Exception: " + e;				
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

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

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
