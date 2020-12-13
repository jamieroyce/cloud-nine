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
	static string status = "Recruit";
	static string vCat = "Recruit";
	static string deleteID;

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
		HeadText.Text = "Recruitment Log";
		
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

	protected void text_change_date(object sender, EventArgs e)
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

    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET status = @TEXT, scheduled = getdate(), weekend = getdate() + ( 5 - datepart(dw,getdate()) + 7) % 7  WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(searchCol != "" && searchText != ""){
			DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
		}
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

	protected void Rank_Change(object sender, EventArgs e)
	{
		DropDownList ddlRank = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET rank = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlRank.Text);		
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
		IdToDelete.Text = vId; 
		deleteID = vId;

		NamedLbl.Text += "deleteID = " + deleteID;
		
		// ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteModal();", true);
		// AjaxControlToolkit.ModalPopupExtender ModalPopupExtender1 = (AjaxControlToolkit.ModalPopupExtender)row.FindControl("ModalPopupExtender1");
		ModalPopupExtender1.Show();		
		
	}
 	
	public void btnDelete_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			NamedLbl.Text += "btnDelete_Click() deleteID = " + deleteID;
			string sqlCommandStatement = String.Format("DELETE FROM bis WHERE id='{0}'", deleteID );									
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
		} 

		UpdatePanels();
		
		ModalPopupExtender1.Hide();
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
			
			if(searchCol != "" && searchText != ""){
				DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
			}
			else {
				DataGrid_Load(DAL.getRegCat("Recruit"), "reg");
			}
				
			
		}
    }

	public void UpdatePanels()
	{

		UpdatePanelInConfirmed.Update();
		UpdatePanelDefinite.Update();
		UpdatePanelPossible.Update();
	
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
		
		SchedLbl.Text = "";
		NamedLbl.Text = "";
		
		try // TOTALS
		{
			var query = from c in dataTable.AsEnumerable() 
					   where c.Field<string>("status") == "Arrived" && c.Field<string>("reg_cat_id") == "Recruit"
					  select c ;	
			
			string ResultBothIn = "0";
			string ResultNamed = "0";
			string ResultBothScheduled = "0";
			string ResultBothInSched = "0";
			string ResultBothInSchedNamed  = "0";
			
			string ResultBothOn = "0";
			string ResultDayIn = "0";
			string ResultDayOn = "0";
			string ResultDayConf = "0";
			string ResultDayNamed = "0";
			string ResultDayInv = "0";

			string ResultFdnIn = "0";
			string ResultFdnOn = "0";
			string ResultFdnConf = "0";
			string ResultFdnInv = "0";
			string ResultFdnNamed = "0";
					  
			var cmdBIS 		 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'Arrived' and reg_cat_id = 'Recruit'";
			var cmdScheduled 	= "SELECT count(1) AS total from bis WHERE status = 'Signed' and reg_cat_id = 'Recruit'";
			var cmdNamed 	 	= "SELECT count(1) AS total from bis WHERE status = 'Prospect' and reg_cat_id = 'Recruit'";

			var cmdBISDay 		 = "SELECT count(distinct name) AS total from bis WHERE status = 'Arrived' and reg_cat_id = 'Recruit' and org = 'Day'";
			var cmdScheduledDay  = "SELECT count(1) AS total from bis WHERE status = 'Signed' and reg_cat_id = 'Recruit' and org = 'Day'";
			var cmdNamedD	 	 = "SELECT count(1) AS total from bis WHERE status = 'Prospect' and reg_cat_id = 'Recruit' and org = 'Day'";
			var cmdInSchedD      = "SELECT count(1) AS total from bis WHERE status in ('Arrived', 'Signed') and reg_cat_id = 'Recruit' and org = 'Day'";
			var cmdInSchedNamedD = "SELECT count(1) AS total from bis WHERE status in ('Arrived', 'Signed', 'Prospect') and reg_cat_id = 'Recruit' and org = 'Day'";
				
			var cmdBISFdn 		 = "SELECT count(distinct name) AS total from bis WHERE status = 'Arrived' and reg_cat_id = 'Recruit' and org = 'Fdn'";
			var cmdScheduledFdn  = "SELECT count(1) AS total from bis WHERE status = 'Signed' and reg_cat_id = 'Recruit' and org = 'Fdn'";
			var cmdNamedFdn 	 = "SELECT count(1) AS total from bis WHERE status = 'Prospect' and reg_cat_id = 'Recruit' and org = 'Fdn'";
			var cmdInSchedF      = "SELECT count(1) AS total from bis WHERE status in ('Arrived', 'Signed') and reg_cat_id = 'Recruit' and org = 'Fdn'";
			var cmdInSchedNamedF = "SELECT count(1) AS total from bis WHERE status in ('Arrived', 'Signed', 'Prospect') and reg_cat_id = 'Recruit' and org = 'Fdn'";

			var cmdInSchedBoth   = "SELECT count(1) AS total from bis WHERE status in ('Arrived', 'Signed') and reg_cat_id = 'Recruit'";
			var cmdInSchedNamedBoth   = "SELECT count(1) AS total from bis WHERE status in ('Arrived', 'Signed', 'Prospect') and reg_cat_id = 'Recruit'";
			
			var cmdInvD 	= "SELECT count(distinct name) AS total from bis WHERE status = 'Arrived' and org = 'Day' and reg_cat_id = 'Recruit' ";
			var cmdConfD 	= "SELECT count(1) AS total from bis WHERE status = 'Signed' and org = 'Day' and reg_cat_id = 'Recruit'";
			var cmd3 		= "SELECT count(1) AS total from bis WHERE reg_cat_id = 'Recruit' and status = 'Arrived' and org = 'Fdn'";
			var cmd4 		= "SELECT count(distinct org + name) AS total from bis WHERE status in ('Arrived', 'Signed') and reg_cat_id = 'Recruit'";
			var cmd5 		= "SELECT count(distinct org + name) AS total from bis WHERE org = 'Day' and ((status in ('Arrived', 'Signed') and reg_cat_id = 'Recruit') or (status = 'Signed' and rank = 'a' and reg_cat_id = 'Recruit'))";
			var cmd6 		= "SELECT count(distinct org + name) AS total from bis WHERE org = 'Fdn' and ((status in ('Arrived', 'Signed') and reg_cat_id = 'Recruit') or (status = 'Signed' and rank = 'a' and reg_cat_id = 'Recruit'))";
				
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					ResultBothIn = String.Format("{0:D}", cmdBIS);
					ComboGiGridTotal[0] += cmdBIS.ToString();
					
					using (SqlCommand Cmd = new SqlCommand(cmdBIS, conn))
						{
							ResultBothIn = String.Format("{0:D}", Cmd.ExecuteScalar());
							ComboGiGridTotal[0] += Cmd.ExecuteScalar().ToString();
						}
				}
				catch(Exception e) {
					ErrorText.Text = e.ToString();
				}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdScheduled, conn))					
					{
						ResultBothScheduled = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdNamed, conn))					
					{
						ResultNamed = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInSchedBoth, conn))					
					{
						ResultBothInSched = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInSchedNamedBoth, conn))					
					{
						ResultBothInSchedNamed = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}


				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInSchedD, conn))					
					{
						ResultDayIn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInvD, conn))					
					{
						ResultDayInv = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdConfD, conn))					
					{
						ResultDayConf = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdNamedD, conn))					
					{
						ResultDayNamed = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdBISFdn, conn))					
					{
						ResultFdnInv = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdScheduledFdn, conn))					
					{
						ResultFdnConf = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdNamedFdn, conn))					
					{
						ResultFdnNamed = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try {	
					using (SqlCommand Cmd = new SqlCommand(cmdInSchedF, conn))
					{
						ResultFdnIn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}	
				try{
					using (SqlCommand Cmd = new SqlCommand(cmd4, conn))
					{
						ResultBothOn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}
				try{
				using (SqlCommand Cmd = new SqlCommand(cmdInSchedNamedD, conn))
						{
					ResultDayOn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}
				try{
				using (SqlCommand Cmd = new SqlCommand(cmdInSchedNamedF, conn))
						{
					ResultFdnOn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}						
				conn.Close();
			}			
			
			BothBIS.Text = ResultBothIn;				
			BothScheduled.Text = ResultBothScheduled;				
			BothNamed.Text = ResultNamed;				
			DayInv.Text = ResultDayInv;				
			DayConf.Text = ResultDayConf;				
			DayNamed.Text = ResultDayNamed;				
			FdnInv.Text = ResultFdnInv;				
			FdnConf.Text = ResultFdnConf;				
			FdnNamed.Text = ResultFdnNamed;				

			BothInLbl.Text = String.Format("{0:D}", ResultBothIn);	
			SchedLbl.Text = String.Format("{0:D}", ResultBothOn);	
			NamedLbl.Text = String.Format("{0:D}", ResultNamed);	

			var mySum = 0;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewArrived.DataSource = t2;
				GridViewArrived.DataBind();				
			} else {
				GridViewArrived.DataSource = new DataTable();
				GridViewArrived.DataBind();
			}

		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // SIGNED
		{
		    var query = from c in dataTable.AsEnumerable() 
				       where c.Field<string>("status") == "Signed" && c.Field<string>("reg_cat_id") == "Recruit"
				      select c ;

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewSigned.DataSource = t2;
				GridViewSigned.DataBind();				
			} else {
				GridViewSigned.DataSource = new DataTable();
				GridViewSigned.DataBind();
			}
			
			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Signed' and reg_cat_id = 'Recruit' ";
			string ResultGood = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						ResultGood = String.Format("{0:D}", Cmd.ExecuteScalar());
						ComboGiGridTotal[1] += Cmd.ExecuteScalar().ToString();
					}
				}
				catch{}							
				conn.Close();
			}	
			SchedLbl.Text = ResultGood;				
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // PROSPECT
		{
		
			var query = from c in dataTable.AsEnumerable() 
					   where c.Field<string>("status") == "Prospect" && c.Field<string>("reg_cat_id") == "Recruit"
					  select c ;	

			var mySum = 0;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewProspect.DataSource = t2;
				GridViewProspect.DataBind();
				
			} else {
				GridViewProspect.DataSource = new DataTable();
				GridViewProspect.DataBind();
			}

			// var cmd1 = "SELECT SUM(amount) AS total from bis WHERE reg_cat_id = 'lineup' and rank = 'b'";
			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Signed'";
			string ResultFigure = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
				using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						ResultFigure = String.Format("{0:D}", Cmd.ExecuteScalar());
						ComboGiGridTotal[2] += Cmd.ExecuteScalar().ToString();
					}
				}
				catch{}							
				conn.Close();
				}
			//NamedLbl.Text = ResultFigure;				
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
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

		UpdatePanels();
		
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
