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
	static string filter;
	static string searchWE;
	static string showDay;
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


				DataTable regList = DAL.reg("config"); 
				ddlReg_Addo.DataSource = regList;
				ddlReg_Addo.DataTextField = regList.Columns["full_name"].ToString();
				ddlReg_Addo.DataValueField = regList.Columns["full_name"].ToString();
				ddlReg_Addo.DataBind();

				DataTable bisList = DAL.bis("config"); 
				ddlBIS.DataSource = bisList;
				ddlBIS.DataTextField = bisList.Columns["desc1"].ToString();
				ddlBIS.DataValueField = bisList.Columns["desc1"].ToString();
				ddlBIS.DataBind();

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
		//Title = "Fully Partially Paid";
		HeadText.Text = "FP/PP List";
		searchText = "";
		searchCol = "";
		searchWE = "";
		OrgText.Visible = true;
		
        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		
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

        GridView_Load(GridView3, DAL.FPPP(OrgText.Text));		
		Session["dt"] = GridView3.DataSource as DataTable;

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
			DataGrid_Load(DAL.FilterFPPP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");
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
			DataGrid_Load(DAL.FilterFPPP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");
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
			DataGrid_Load(DAL.FilterFPPP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");
		}

    }

    protected void LinkButton_Command(Object sender, EventArgs e) 
    {
		var v = ((LinkButton)sender).CommandArgument;
		filter = (string)v;
		
		//ErrorText.Text = "You chose: " + v;

		HeadText.Text = "FP/PP";
		
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
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
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
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
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
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="ACADEMY"){
			string s = boxACADEMY.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxACADEMY.Attributes["class"] += " bg-info-gradient";		
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6Processing.Attributes.Add("class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Div 6 Training"){
			string s = boxDiv6Training.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxDiv6Training.Attributes["class"] += " bg-info-gradient";		
				boxDiv6Processing.Attributes.Add("class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Div 6 Processing"){
			string s = boxDiv6Processing.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxDiv6Processing.Attributes.Add("class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxDiv6Processing.Attributes["class"] += " bg-info-gradient";		
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		
		searchText = "Line";
		searchCol = v;
		
		HeadText.Text += " (" + v + ")";
		
		if(filterOn==1){
			DataGrid_Load(DAL.FilterFPPP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			HeadText.Text = "Gross Income";
			DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			HeadText.Text = "Gross Income";
			DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");
		}
	}
	
	protected void ExportToExcel_Click(object sender, EventArgs e)
	{

		var data =  DAL.FPPP(OrgText.Text);
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
			
    public void BtnSearch_Click_FPPP(object sender, EventArgs e)
    {
		searchText = ddlSearchFPPP.Text;
		searchCol = TextBox1.Text;
		DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
		
    }
	
	public void Org_Btn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		Button clickedButton = sender as Button;		
		Title = clickedButton.Text;
		OrgText.Text = clickedButton.Text;

		if(filterOn==1){
			DataGrid_Load(DAL.FilterFPPP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");
		}

        
    }

    public void LineUp_Add_Click(Object sender, EventArgs e)
	{		
		Button clickedButton = sender as Button;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		TextInfo textInfo = cultureInfo.TextInfo;
		
        string addo = gvRow.Cells[0].Text;
        string name = gvRow.Cells[2].Text;
		string phone = gvRow.Cells[6].Text;
		
		name = textInfo.ToTitleCase(name.ToLower());
		
		SqlCmd(String.Format("INSERT into reg(reg_cat_id, status, org, name, phone, addo_id) values('LineUp', 'Now Prospect', '{0}', '{1}', '{2}', '{3}')", OrgText.Text, name, phone, addo));
	}

	public void Add_Click(Object sender, EventArgs e)
	{
		SqlCmd(String.Format("INSERT into reg(reg_cat_id, org) values('{0}', '{1}')", HeadText.Text, OrgText.Text));
        DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
    }

    public void Addresso_Click(Object sender, EventArgs e)
    {
		ErrorText.Text = "";
        OrgText.Visible = false;
        
        RowLable.Visible = true;
		
        if (ViewState["SortExpression"] != null)
            ViewState["SortExpression"] = null;
		Button clickedButton = sender as Button;
        HeadText.Text = clickedButton.Text;
        Title = clickedButton.Text;
		searchText = "";
		searchCol = "";
		searchWE = "";
        DataGrid_Load(DAL.addo(), "cf");
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
			// string rank = String.Format("{0}", 		Request.Form["addo_rankid"]);	
			string status = String.Format("{0}", 	Request.Form["addo_statusid"]);				
			string service = String.Format("{0}", 	Request.Form["addo_serviceid"]);	
			string amount = String.Format("{0}", 	Request.Form["add_amountid"]);	
			string reg = String.Format("{0}", 		Request.Form["ddlReg_Addo"]);	
			string bird_dog = String.Format("{0}", 	Request.Form["fsmid"]);				
			string line = String.Format("{0}", 		Request.Form["lineid3"]);				
			// string phone = String.Format("{0}", 	Request.Form["addophoneid"]);				
			string email  = String.Format("{0}", 	Request.Form["email"]);				
			string notes = String.Format("{0}", 	Request.Form["addo_noteid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			// ErrorText.Text = "ADDOID = " + addoid; 
			
			amount = amount.Replace(",", "");
			amount = amount.Replace(".", "");

			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into reg(reg_cat_id, addo_id, name,  status, service, amount, reg, bird_dog, line, org, notes) "
							 + "VALUES (@reg_cat_id, @addo_id, @name,  @status, @service, @amount, @reg, @bird_dog, @line, @org, @notes)";
							 
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
					command.Parameters.AddWithValue("@org",				org);
					command.Parameters.AddWithValue("@notes",			notes);

					connection.Open();

					int result = command.ExecuteNonQuery();

					//DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
					
					ClearAddoModal();
					
					StartingPage();
					
					// Check Error
					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
				}
			}
				//Response.Redirect("index.aspx");
				//back to home
		} 
    }

	public void btnAddBIS_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string id = String.Format("{0}", 		Request.Form["id3"]);	
			string addoid = String.Format("{0}", 	Request.Form["bis_addoid"]);	
			string name = String.Format("{0}", 		Request.Form["bis_nameid"]);	
			string org = String.Format("{0}", 		Request.Form["bis_orgid"]);	
			string rank = String.Format("{0}", 		Request.Form["bis_rankid"]);	
			string status = String.Format("{0}", 	Request.Form["bis_statusid"]);				
			string service = String.Format("{0}", 	Request.Form["bis_serviceid"]);	
			string area = String.Format("{0}", 		Request.Form["ddlBIS"]);	
			string reg = String.Format("{0}", 		Request.Form["bis_regid"]);	
			string fsm = String.Format("{0}", 		Request.Form["bis_fsmid"]);				
			string scheduled = String.Format("{0}", Request.Form["bis_apptid"]);		
			string line = String.Format("{0}", 		Request.Form["bis_lineid"]);				
			string phone = String.Format("{0}", 	Request.Form["bis_phoneid"]);				
			string email  = String.Format("{0}", 	Request.Form["bis_email"]);				
			string notes = String.Format("{0}", 	Request.Form["bis_noteid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			if (String.IsNullOrEmpty(scheduled)){

				using(SqlConnection connection = databaseConnection.CreateSqlConnection())
				{
					String query = "INSERT into bis(reg_cat_id, addo_id, name, rank, status, service, area, reg, fsm, phone, email, line, org, notes) "
								 + "VALUES (@reg_cat_id, @addo_id, @name, @rank, @status, @service, @area, @reg, @fsm, @phone, @email, @line, @org, @notes)";
								 
					using(SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
						command.Parameters.AddWithValue("@addo_id",			addoid);
						command.Parameters.AddWithValue("@name",			name);
						command.Parameters.AddWithValue("@rank",			rank);
						command.Parameters.AddWithValue("@status",			status);
						command.Parameters.AddWithValue("@service",			service);
						command.Parameters.AddWithValue("@area",			area);
						command.Parameters.AddWithValue("@reg",				reg);
						command.Parameters.AddWithValue("@fsm",				fsm);
						command.Parameters.AddWithValue("@phone",			phone);
						command.Parameters.AddWithValue("@email",			email);
						command.Parameters.AddWithValue("@line",			line);
						command.Parameters.AddWithValue("@org",				org);
						command.Parameters.AddWithValue("@notes",			notes);

						connection.Open();

						int result = command.ExecuteNonQuery();
						//DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
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
					String query = "INSERT into bis(reg_cat_id, addo_id, name, rank, status, service, area, reg, fsm, phone, email, scheduled, line, org, notes) "
								 + "VALUES (@reg_cat_id, @addo_id, @name, @rank, @status, @service, @area, @reg, @fsm, @phone, @email, @scheduled, @line, @org, @notes)";
								 
					using(SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
						command.Parameters.AddWithValue("@addo_id",			addoid);
						command.Parameters.AddWithValue("@name",			name);
						command.Parameters.AddWithValue("@rank",			rank);
						command.Parameters.AddWithValue("@status",			status);
						command.Parameters.AddWithValue("@service",			service);
						command.Parameters.AddWithValue("@area",			area);
						command.Parameters.AddWithValue("@reg",				reg);
						command.Parameters.AddWithValue("@fsm",				fsm);
						command.Parameters.AddWithValue("@phone",			phone);
						command.Parameters.AddWithValue("@email",			email);
						command.Parameters.AddWithValue("@scheduled",		scheduled);
						command.Parameters.AddWithValue("@line",			line);
						command.Parameters.AddWithValue("@org",				org);
						command.Parameters.AddWithValue("@notes",			notes);

						connection.Open();

						int result = command.ExecuteNonQuery();
						
						//DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
						ClearAddoModal();

						StartingPage();
						
						// Check Error
						if(result < 0){
							ErrorText.Text = "Error inserting data into Database!";
						}
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
        string name = gvRow.Cells[2].Text;
		
		addonameid.Text = name;
		addo_addoid.Text = id;
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		
	}

	protected void ViewBIS(object sender, EventArgs e)
	{
		Button clickedButton = sender as Button;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		TextInfo textInfo = cultureInfo.TextInfo;
		
        string id = gvRow.Cells[0].Text;
        string name = gvRow.Cells[2].Text;
		
		bis_nameid.Text = name;
		bis_addoid.Text = id;
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openBISModal();", true);
		
	}

	protected void OpenAddNew(object sender, EventArgs e)
	{
		ClearAddoModal();
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
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
		Session["dt"] = dt;

	}
	
	private void DataGrid_Load(DataTable command, string type)
	{	

		DataTable dataTable = new DataTable();
        dataTable = command;
		string sortDir = ViewState["SortDirection"] as string;
		string sortExp = ViewState["SortExpression"] as string;
		
		GridView3.DataSource = dataTable;
		GridView3.DataBind();
		Session["dt"] = dataTable;

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

		DataTable dt = (DataTable)Session["dt"];
		gv.DataSource = dt;

		// if(filterOn==1){
			// DataGrid_Load(DAL.FilterFPPP(OrgText.Text, filter), "reg");
		// }
		// else if(searchCol != "" && searchText != ""){
			// DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
		// }
		// else {
			// DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");
		// }

    }

    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
	
		if(filterOn==1){
			DataGrid_Load(DAL.FilterFPPP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");
		}

    }

    protected void Selection_Change_Page(object sender, EventArgs e)
	{
		if(filterOn==1){
			DataGrid_Load(DAL.FilterFPPP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");
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
