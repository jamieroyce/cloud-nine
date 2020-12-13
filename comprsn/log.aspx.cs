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
	static string status = "Comp Resign";
	static string vCat = "Comp Resign";
	static string filter;
	static int filterOn = 0;
	

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
		HeaderText.Text = "Comp Resign";
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
		
		String s = Request.QueryString["filter"];
		if (s!=null){
			FilterArea(s);
		} else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
		}
		
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

	public void FilterArea(string v)
	{

		//ADD THE button pressed LOOK TO THE INFO BOX
		if(v=="Purif"){
			string s = boxPurif.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxPurif.Attributes["class"] += " bg-info-gradient";		
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6Processing.Attributes.Add("class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="SRD"){
			string s = boxSRD.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxSRD.Attributes["class"] += " bg-info-gradient";		
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6Processing.Attributes.Add("class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="HGC"){
			string s = boxHGC.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxHGC.Attributes["class"] += " bg-info-gradient";		
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6Processing.Attributes.Add("class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="ACAD"){
			string s = boxACADEMY.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxACADEMY.Attributes["class"] += " bg-info-gradient";		
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6Processing.Attributes.Add("class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="DIV6"){
			string s = boxDiv6.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxDiv6.Attributes["class"] += " bg-info-gradient";		
				boxDiv6Processing.Attributes.Add("class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="GAK"){
			string s = boxDiv6Processing.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxDiv6Processing.Attributes.Add("class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxDiv6Processing.Attributes["class"] += " bg-info-gradient";		
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		
		searchText = "Area";
		searchCol = v;
		HeaderText.Text = "Comp Resign";
		HeaderText.Text += " (" + v + ")";

		if(filterOn==1){
			DataGrid_Load(DAL.FilterBIS(OrgText.Text, searchCol, vCat), "reg");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
			HeaderText.Text = "Comp Resign";
		}
	}

	
    protected void LinkButtonArea_Command(Object sender, EventArgs e) 
    {
		var v = ((LinkButton)sender).CommandArgument;
		filter = (string)v;
		FilterArea(filter);

	}
	
	private void PopulateAreas()
	{
		// DataTable dt = new DataTable();		
		// dt = DAL.ReportByArea(status);
				
		// foreach(DataRow row in dt.Rows)
		// {
			// if(row["org"].ToString()=="Day"){
				// if(row["area"].ToString()=="DIV6"){
					// div6d.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="LI"){
					// lid.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="ACAD"){
					// acadd.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="HGC"){
					// hgcd.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="PE"){
					// ped.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="PURIF"){
					// purifd.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="SRD"){
					// srdd.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="STCC"){
					// stccd.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="DN"){
					// dnd.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="KNOW"){
					// knowd.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="INTERN"){
					// internd.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="HQS"){
					// hqsd.Text = row["cnt"].ToString();
				// }
			// }else{
				// if(row["area"].ToString()=="DIV6"){
					// div6f.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="LI"){
					// lif.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="ACAD"){
					// acadf.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="HGC"){
					// hgcf.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="PE"){
					// pef.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="PURIF"){
					// puriff.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="SRD"){
					// srdf.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="STCC"){
					// stccf.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="DN"){
					// dnf.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="KNOW"){
					// knowf.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="INTERN"){
					// internf.Text = row["cnt"].ToString();
				// } else if(row["area"].ToString()=="HQS"){
					// hqsf.Text = row["cnt"].ToString();
				// }
			// }
		// }
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

		if(filterOn==1){
			DataGrid_Load(DAL.FilterBIS(OrgText.Text, searchCol, vCat), "reg");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
			HeaderText.Text = "Comp Resign";
		}
        
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

		if(filterOn==1){
			DataGrid_Load(DAL.FilterBIS(OrgText.Text, searchCol, vCat), "reg");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
			HeaderText.Text = "Comp Resign";
		}
        
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

		if(filterOn==1){
			DataGrid_Load(DAL.FilterBIS(OrgText.Text, searchCol, vCat), "reg");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
			HeaderText.Text = "Comp Resign";
		}

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
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
//        string sqlCommandStatement = "UPDATE bis SET status = @TEXT WHERE id=@ID";
        string sqlCommandStatement = "UPDATE bis SET status = @TEXT, scheduled = getdate(), weekend = getdate() WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
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

		DropDownList dl = (DropDownList)row.FindControl("ddlReg");
		ddlReg.Text = dl.Text;

		dl = (DropDownList)row.FindControl("ddlStatus");
		vTxt = dl.Text; // get the value from TextBox		
		statusid.Text = vTxt;

		tb = (TextBox)row.FindControl("fsm");
		vTxt = tb.Text; // get the value from TextBox		
		fsmid.Text = vTxt;
		
		tb = (TextBox)row.FindControl("email");
		vTxt = tb.Text; // get the value from TextBox		
		emailid.Text = vTxt;

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
			string email = String.Format("{0}", 	Request.Form["emailid"]);	
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
															" email		 	= '{5}', " + 
															" org		 	= '{6}', " + 
															" area		 	= '{7}', " + 
															" notes	 		= '{8}' " + 
															" WHERE id='{9}'", 
															name.Replace("'", "''"), 
															service.Replace("'", "''"), 
															reg.Replace("'", "''"), 
															status, 
															fsm.Replace("'", "''"), 
															email.Replace("'", "''"), 
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
		
		if(filterOn==1){
			DataGrid_Load(DAL.FilterBIS(OrgText.Text, searchCol, vCat), "reg");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
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

		PopulateAreas();		
		
		try // IN 
		{
			
			var query = from c in dataTable.AsEnumerable() 
					   where c.Field<string>("status") == status && c.Field<string>("reg_cat_id") == status
					  select c ;	

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewCompResign.DataSource = t2;
				GridViewCompResign.DataBind();		
				
				int count = t2.AsEnumerable().Count();
				lblTot.Text = String.Format("{0:D}", count);
			} else {
				GridViewCompResign.DataSource = new DataTable();
				GridViewCompResign.DataBind();
				lblTot.Text = String.Format("{0:D}", 0);
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
			
			var cmdDay = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and org = 'Day' and reg_cat_id = '" + status + "'";
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

			var cmdFdn = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and org = 'Fdn' and reg_cat_id = '" + status + "'";
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
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // SCHEDULED
		{
			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Scheduled" 
				where c.Field<string>("reg_cat_id") == status
				select c ;

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewScheduled.DataSource = t2;
				GridViewScheduled.DataBind();		

				int count = t2.AsEnumerable().Count();
				LblSched.Text = String.Format("{0:D}", count);
				
			} else {
				//ErrorTextA.Text = "No Data Found";	
				GridViewScheduled.DataSource = new DataTable();
				GridViewScheduled.DataBind();
				LblSched.Text = String.Format("{0:D}", 0);
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

			var cmdDay = "SELECT COUNT(1) AS total from bis WHERE status = 'Scheduled' and org = 'Day' and reg_cat_id = '" + status + "'";
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

			var cmdFdn = "SELECT COUNT(1) AS total from bis WHERE status = 'Scheduled' and org = 'Fdn' and reg_cat_id = '" + status + "'";
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
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // NAMED
		{

			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Named" 
				where c.Field<string>("reg_cat_id") == status
				select c ;

			var mySum = 0;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewNamed.DataSource = t2;
				GridViewNamed.DataBind();
				
				int count = t2.AsEnumerable().Count();
				LblNamed.Text = String.Format("{0:D}", count);
				
			} else {
				GridViewNamed.DataSource = new DataTable();
				GridViewNamed.DataBind();
				LblNamed.Text = String.Format("{0:D}", 0);
			}

			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Named' and reg_cat_id = '" + status + "'";
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

			var cmdDay = "SELECT COUNT(1) AS total from bis WHERE status = 'Named' and org = 'Day' and reg_cat_id = '" + status + "'";
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

			var cmdFdn = "SELECT COUNT(1) AS total from bis WHERE status = 'Named' and org = 'Fdn' and reg_cat_id = '" + status + "'";
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
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // FAILED RESIGN
		{

			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Failed Resign" 
				where c.Field<string>("reg_cat_id") == status
				select c ;

			var mySum = 0;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewFailedResign.DataSource = t2;
				GridViewFailedResign.DataBind();
				
				int count = t2.AsEnumerable().Count();
				LblFailedResign.Text = String.Format("{0:D}", count);
				
			} else {
				GridViewFailedResign.DataSource = new DataTable();
				GridViewFailedResign.DataBind();
				LblFailedResign.Text = String.Format("{0:D}", 0);
			}

			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Failed Resign' and reg_cat_id = '" + status + "'";
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
			Failed.Text = s;

			var cmdDay = "SELECT COUNT(1) AS total from bis WHERE status = 'Failed Resign' and org = 'Day' and reg_cat_id = '" + status + "'";
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
			DayFailed.Text = s;	

			var cmdFdn = "SELECT COUNT(1) AS total from bis WHERE status = 'Failed Resign' and org = 'Fdn' and reg_cat_id = '" + status + "'";
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
			FdnFailed.Text = s;	
			
			if(OrgText.Text=="Combined"){

				// Both.Text = 
				lblDay.Text += " (Day)";
				lblFdn.Text += " (Fdn)";

				// Sched.Text = 
				DaySched.Text += " (Day)";
				FdnSched.Text += " (Fdn)";

				// Named.Text = 
				DayNamed.Text += " (Day)";
				FdnNamed.Text += " (Fdn)"; 

				// Failed.Text = 
				DayFailed.Text += " (Day)";
				FdnFailed.Text += " (Fdn)";  	
				
			} else if (OrgText.Text=="Day"){
				
				Both.Text = lblDay.Text;
				lblDay.Text = "";
				lblFdn.Text = "";
				
				Sched.Text = DaySched.Text;
				DaySched.Text = "";
				FdnSched.Text = "";

				Named.Text = DayNamed.Text;
				DayNamed.Text = "";
				FdnNamed.Text = "";
				
				Failed.Text = DayFailed.Text;
				DayFailed.Text = "";
				FdnFailed.Text = "";
				
			} else if (OrgText.Text=="Fdn"){

				Both.Text = lblFdn.Text;
				lblDay.Text = "";
				lblFdn.Text = "";
				
				Sched.Text = FdnSched.Text;
				DaySched.Text = "";
				FdnSched.Text = "";

				Named.Text = FdnNamed.Text;
				DayNamed.Text = "";
				FdnNamed.Text = "";
				
				Failed.Text = FdnFailed.Text;
				DayFailed.Text = "";
				FdnFailed.Text = "";
				
			}
			
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
