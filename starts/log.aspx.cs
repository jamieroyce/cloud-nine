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
	static int filterOn = 0;
	static string filter;
	static string deleteID;
	static string status = "Started";
	static string vCat = "Paid Start";
	

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
		HeadText.Text = "Starts";
		HeaderText.Text = "Starts";
		Title = "Starts";

		OrgText.Visible = true;
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

		ViewState["SortDirection"] = "asc";
		ViewState["SortExpression"] = "status";

		String s = Request.QueryString["filter"];
		if (s!=null){
			FilterArea(s);
		} else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
		}
		
		
		string sortDir = "asc";
		string sortExp = "status";

		DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "all"), "reg");
		
		// PopulateLine();
		
	}
	
	protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			//Find the DropDownList in the Row
			DataTable regList = DAL.listReg(OrgText.Text); 
			regList = resort(regList, "short_name", "desc");

		}
	}
	
	private void PopulateLine()
	{
		DataTable dtd = new DataTable();		
		dtd = DAL.ReportByOrgArea("Paid Start", "Day");

		foreach(DataRow row in dtd.Rows)
		{
			if(row["line"].ToString()=="Resign"){
				resignDay.Text = String.Format("{0:C0} ", row["cnt"]) ;
			} else if(row["line"].ToString()=="CF"){
				cfDay.Text = String.Format("{0:C0} ", row["cnt"]) ;
			} else if(row["line"].ToString()=="Arrival"){
				arrivalDay.Text = String.Format("{0:C0} ", row["cnt"]) ;
			} else if(row["line"].ToString()=="FSM"){
				fsmDay.Text = String.Format("{0:C0} ", row["cnt"]) ;
			} else if(row["line"].ToString()=="Prospecting"){
				prospectingDay.Text = String.Format("{0:C0} ", row["cnt"]) ;
			}	
		}

		DataTable dtf = new DataTable();		
		dtf = DAL.ReportByOrgArea("Paid Start", "Fdn");

		foreach(DataRow rowf in dtf.Rows)
		{
			if(rowf["line"].ToString()=="Resign"){
				resignFdn.Text = String.Format("{0:C0} ", rowf["cnt"]) ;
			} else if(rowf["line"].ToString()=="CF"){
				cfFdn.Text = String.Format("{0:C0} ", rowf["cnt"]) ;
			} else if(rowf["line"].ToString()=="Arrival"){
				arrivalFdn.Text = String.Format("{0:C0} ", rowf["cnt"]) ;
			} else if(rowf["line"].ToString()=="FSM"){
				fsmFdn.Text = String.Format("{0:C0} ", rowf["cnt"]) ;
			} else if(rowf["line"].ToString()=="Prospecting"){
				prospectingFdn.Text = String.Format("{0:C0} ", rowf["cnt"]) ;
			}
		}
		
		if(OrgText.Text=="Day"){
		
			resignFdn.Text = "";
			cfFdn.Text = "";
			arrivalFdn.Text = "";
			fsmFdn.Text = "";
			prospectingFdn.Text = "";
		
		} else if (OrgText.Text=="Fdn"){

			resignDay.Text = "";
			cfDay.Text = "";
			arrivalDay.Text = "";
			fsmDay.Text = "";
			prospectingDay.Text = "";
			
		} else if (OrgText.Text=="Combined"){

			resignFdn.Text += " (Fdn)";
			cfFdn.Text += " (Fdn)";
			arrivalFdn.Text += " (Fdn)";
			fsmFdn.Text += " (Fdn)";
			prospectingFdn.Text += " (Fdn)";

			resignDay.Text += " (Day)";
			cfDay.Text += " (Day)";
			arrivalDay.Text += " (Day)";
			fsmDay.Text += " (Day)";
			prospectingDay.Text += " (Day)";
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
		
		if(filterOn==1){
			DataGrid_Load(DAL.FilterBIS(OrgText.Text, searchCol, vCat), "reg");
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			HeaderText.Text = "Starts";
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
			DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {

			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
			HeaderText.Text = "Starts";
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
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			HeaderText.Text = "Starts";
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
			DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
			HeaderText.Text = "Starts";
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
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			HeaderText.Text = "Starts";
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
			DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
			HeaderText.Text = "Starts";
		}
    }

	public void FilterNamed(string area)
	{
		searchText = area;
		searchCol = "Line";
		DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		
	}

	public void FilterArea(string v)
	{

		HeaderText.Text = "Starts";
		
		//ADD THE button pressed LOOK TO THE BOX
		if(v=="Purif"){
			string s = boxPurif.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxPurif.Attributes["class"] += " bg-info-gradient";		
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
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
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
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
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
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
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
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
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="GAK"){
			string s = boxDiv6Training.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxDiv6Training.Attributes.Add("class", boxDiv6Training.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxDiv6Training.Attributes["class"] += " bg-info-gradient";		
				boxDiv6.Attributes.Add("class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxPurif.Attributes.Add("class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxACADEMY.Attributes.Add("class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxHGC.Attributes.Add("class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxSRD.Attributes.Add("class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		
		searchText = "Area";
		searchCol = v;
		
		HeaderText.Text += " (" + v + ")";

		if(filterOn==1){
			DataGrid_Load(DAL.FilterBIS(OrgText.Text, searchCol, vCat), "reg");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
			HeaderText.Text = "Starts";
		}
			
	}

	
    protected void LinkButton_Command(Object sender, EventArgs e) 
    {
		var v = ((LinkButton)sender).CommandArgument;
		//ErrorText.Text = "You chose: " + v;

		HeaderText.Text = "Starts";
		
		//ADD THE button pressed LOOK TO THE INFO BOX
		if(v=="Resign"){
			string s = boxResign.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxResign.Attributes["class"] += " bg-info-gradient";		
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Arrival"){
			string s = boxArrival.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxArrival.Attributes["class"] += " bg-info-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="CF"){
			string s = boxCF.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxCF.Attributes["class"] += " bg-info-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="FSM"){
			string s = boxFSM.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxFSM.Attributes["class"] += " bg-info-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Prospecting"){
			string s = boxProspecting.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxProspecting.Attributes["class"] += " bg-info-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Other"){
			string s = boxOther.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxOther.Attributes["class"] += " bg-info-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}

		
		searchText = "Line";
		searchCol = v;
		
		HeaderText.Text += " (" + v + ")";
		
		if(filterOn==1){
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
			HeaderText.Text = "Starts";
		}
	}

    protected void LinkButtonArea_Command(Object sender, EventArgs e) 
    {
		var v = ((LinkButton)sender).CommandArgument;
		filter = (string)v;
		FilterArea(filter);

	}
		
	protected void ExportToExcel_Click(object sender, EventArgs e)
	{

		var data = DAL.bis_log(HeadText.Text, OrgText.Text);
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
	
    public void BtnSearch_Click(object sender, EventArgs e)
    {
		searchText = ddlSearchGI.Text;
		searchCol = txtGI.Text;
		DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
    }

	protected void Click_AddToFuture(object sender, EventArgs e)
	{

	//CHOOSE AN ORG FIRST 
		if(OrgText.Text=="Combined"){
			
			AlertPanel.Text = "Choose either Day or Fdn.</br>";
			ModalPopupExtender2.Show();				

		} else {
	
			int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
			GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
			
			string addoid = "";
			// string org = "";
			string name = "";
			string area = "";
			string service = "";
			string line = "CF";
			string status = "Future";
			string rank = "PS";

			addoid = row.Cells[0].Text;
			// name = row.Cells[2].Text;
			name = Server.HtmlDecode(row.Cells[2].Text);
			service = row.Cells[3].Text;

			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());
			
			area = filter;
			
			if(area==null)
				area = "OTHER";

			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, rank, line, scheduled, weekend, org) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, @rank, @line, getdate(), getdate(), @org)";

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
					command.Parameters.AddWithValue("@org",				OrgText.Text);

					try{
						int result = command.ExecuteNonQuery();

						if(result < 0){
							AlertPanel.Text = "Error inserting data into Database!</br>";
							ModalPopupExtender2.Show();				
						} else {
							AlertPanel.Text += "Success! " + name + " was added to the Named Starts for the Week for: " + service + ".</br>";
							// ModalPopupExtender2.Show();	
						}
					} 
					catch (SqlException ex)
					{
						AlertPanel.Text =  "ERROR: " + ex.ToString();
						ModalPopupExtender2.Show();				
					}
					connection.Close();
					// UpdatePanelFuture.Update();
				}
			}	
			
			if(filterOn==1){
				DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
				DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "reg");
			}
			else if(searchCol != "" && searchText != ""){
				HeaderText.Text = "Starts";
				DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
			}
			else {
				DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
				DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
				HeaderText.Text = "Starts";
			}
			
			UpdatePanels();
		}
	}
	
	protected void Click_AddToNamed(object sender, EventArgs e)
	{

	//CHOOSE AN ORG FIRST 
		if(OrgText.Text=="Combined"){
			
			AlertPanel.Text = "Choose either Day or Fdn.</br>";
			ModalPopupExtender2.Show();				

		} else {
	
			int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
			GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
			
			string addoid = "";
			// string org = "";
			string name = "";
			string area = "";
			string service = "";
			string line = "CF";
			string status = "Named";
			string rank = "PS";

			addoid = row.Cells[0].Text;
			name = row.Cells[2].Text;
			service = row.Cells[3].Text;

			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());
			
			area = filter;
			
			if(area==null)
				area = "OTHER";

			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, rank, line, scheduled, weekend, org) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, @rank, @line, getdate(), getdate(), @org)";

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
					command.Parameters.AddWithValue("@org",				OrgText.Text);

					try{
						int result = command.ExecuteNonQuery();

						if(result < 0){
							AlertPanel.Text = "Error inserting data into Database!</br>";
							ModalPopupExtender2.Show();				
						} else {
							AlertPanel.Text += "Success! " + name + " was added to the Named Starts for the Week for: " + service + ".</br>";
							// ModalPopupExtender2.Show();	
						}
					} 
					catch (SqlException ex)
					{
						AlertPanel.Text =  "ERROR: " + ex.ToString();
						ModalPopupExtender2.Show();				
					}
					connection.Close();
					// UpdatePanelFuture.Update();
				}
			}	
			
			if(filterOn==1){
				DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
				DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "reg");
			}
			else if(searchCol != "" && searchText != ""){
				HeaderText.Text = "Starts";
				DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
			}
			else {
				DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
				DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
				HeaderText.Text = "Starts";
			}
			
			UpdatePanels();
		}
	}
	
	protected void Click_AddToScheduled(object sender, EventArgs e)
	{

	//CHOOSE AN ORG FIRST 
		if(OrgText.Text=="Combined"){
			
			AlertPanel.Text = "Choose either Day or Fdn.</br>";
			ModalPopupExtender2.Show();				

		} else {
	
			int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
			GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
			
			string addoid = "";
			// string org = "";
			string name = "";
			string area = "";
			string service = "";
			string line = "CF";
			string status = "Scheduled";
			string rank = "PS";

			addoid = row.Cells[0].Text;
			name = row.Cells[2].Text;
			service = row.Cells[3].Text;

			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());
			
			area = filter;
			
			if(area==null)
				area = "OTHER";

			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, rank, line, scheduled, weekend, org) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, @rank, @line, getdate(), getdate(), @org)";

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
					command.Parameters.AddWithValue("@org",				OrgText.Text);

					try{
						int result = command.ExecuteNonQuery();

						if(result < 0){
							AlertPanel.Text = "Error inserting data into Database!</br>";
							ModalPopupExtender2.Show();				
						} else {
							AlertPanel.Text += "Success! " + name + " was added to the Named Starts for the Week for: " + service + ".</br>";
							// ModalPopupExtender2.Show();	
						}
					} 
					catch (SqlException ex)
					{
						AlertPanel.Text =  "ERROR: " + ex.ToString();
						ModalPopupExtender2.Show();				
					}
					connection.Close();
					// UpdatePanelFuture.Update();
				}
			}	
			
			if(filterOn==1){
				DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
				DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "reg");
			}
			else if(searchCol != "" && searchText != ""){
				HeaderText.Text = "Starts";
				DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
			}
			else {
				DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
				DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
				HeaderText.Text = "Starts";
			}
			
			UpdatePanels();
		}
	}
	
	protected void btnConfirmed_Click(SqlCommand command)
	{
		
		
	}

	protected void Click_AddToInAndStarted(object sender, EventArgs e)
	{

	//CHOOSE AN ORG FIRST 
		if(OrgText.Text=="Combined"){
			
			AlertPanel.Text = "Choose either Day or Fdn.</br>";
			ModalPopupExtender2.Show();				

		} else {
	
			int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
			GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
			
			string addoid = "";
			string name = "";
			string area = "- UNK";
			string service = "";
			string line = "CF";
			string status = "In and Started";

			addoid = row.Cells[1].Text;

			TextBox tb = (TextBox)row.FindControl("name");
			name =  tb.Text;
			
			DropDownList dl = (DropDownList)row.FindControl("ddlArea");
			area = dl.Text; // get the value from TextBox		
			
			if(area=="OTHER")
				area = "- UNK";
				
			if(area=="GAK")
				area = "KNOW";

			tb = (TextBox)row.FindControl("service");
			service =  tb.Text;

			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			AlertPanel.Text = "You are about to add <b>(" + name + " - " + area + " - " + service + ")</b> to the In and Started log. Continue?</br>";
			ModalPopupExtender2.Show();	
			
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area,  line, scheduled, weekend, org) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area,  @line, getdate(), getdate(), @org)";

				using(SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();

					command.Parameters.AddWithValue("@reg_cat_id",		"In and Started");
					command.Parameters.AddWithValue("@addo_id",			addoid);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@status",			status);
					command.Parameters.AddWithValue("@service",			service);
					command.Parameters.AddWithValue("@area",			area);
					command.Parameters.AddWithValue("@line",			line);
					command.Parameters.AddWithValue("@org",				OrgText.Text);

					try{
						int result = command.ExecuteNonQuery();

						if(result < 0){
							AlertPanel.Text = "Error inserting data into Database!</br>";
							ModalPopupExtender2.Show();				
						} else {
							AlertPanel.Text = "Successfully added <b>" + name + "</b> to In and Started!</br>";
							ModalPopupExtender2.Show();	
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
			
			if(filterOn==1){
				DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
				DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "reg");
			}
			else if(searchCol != "" && searchText != ""){
				HeaderText.Text = "Starts";
				DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
			}
			else {
				DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
				DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
				HeaderText.Text = "Starts";
			}
			
			UpdatePanels();
		}
	}
	
	protected void text_change_date(object sender, EventArgs e)
	{
		// ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
	}

	protected void text_change_amount(object sender, EventArgs e)
	{
		// ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		
		UpdatePanelTotal.Update();
		
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

	protected void text_change_addo(object sender, EventArgs e)
	{
		// ErrorText.Text = "";
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
		
		string sqlCommandStatement = "";
		
		sqlCommandStatement = "UPDATE bis SET status = @TEXT, scheduled = getdate(), weekend = getdate() WHERE id=@ID";
		
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
    }

	protected void OpenModal(object sender, EventArgs e)
	{
		// ClearAddoModal();
		
		if(OrgText.Text=="Combined"){
			AlertText.Text = "Choose either Day or Fdn!";
			ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAlert();", true);
		}
		else {
			Response.Redirect("./add.aspx");
		}

		// addonameid.Text = txtAddoSearch.Text;
		// ddl_auditor.Focus();
		// session_date_ID.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");   
		
	}

	protected void OpenArchiveModal(object sender, EventArgs e)
	{
		// ClearAddoModal();
		
		if(weekendingText.Text==""){
			AlertText.Text = "You must first select a weekending!";
			ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAlert();", true);
		} else if (OrgText.Text=="Combined"){
			AlertText.Text = "Choose either Day or Fdn to Archive.";
			ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAlert();", true);
		}
		else {
			ClientScript.RegisterStartupScript(this.GetType(), "Pop", "archiveWarning();", true);
		}

		// addonameid.Text = txtAddoSearch.Text;
		// ddl_auditor.Focus();
		// session_date_ID.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");   
		
	}

	public void Archive_Click(object sender, EventArgs e)
	{

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string we = weekendingText.Text;	
			string nameStr = "";
			string dateStr = "";
			string areaStr = "";

			int i = 0;
			
			GridView gv = (GridView)(sender as Control).Parent.FindControl("GridViewStarted");		
			foreach (GridViewRow row in gv.Rows)
			{
				i++;				
				string rowID = row.Cells[0].Text;
				TextBox name = (TextBox)row.FindControl("name");
				if ( name != null) {
					nameStr = name.Text;
				} else {
					nameStr = "";
				}
				TextBox dt = (TextBox)row.FindControl("scheduled");
				if ( dt != null) 
					dateStr = dt.Text;

				DropDownList area = (DropDownList)row.FindControl("ddlRank");				
				if ( area != null) {
					areaStr = area.Text;
				} else {
					areaStr = "";
				}
				
				if(we != null && we != "" && nameStr != " "  && nameStr != "" && nameStr != null && areaStr != " "  && areaStr != "" && areaStr != null)
				{																	
					string sqlCommandStatement = String.Format( 
					//ARCHIVE BIS 
						"UPDATE bis SET REG_CAT_ID = '{0}', scheduled = '{1}', weekend = '{2}' WHERE ID = '{3}' AND org = '{4}' " , 
																"Archive"	, 
																dateStr		,
																we			,
																rowID		,
																OrgText.Text
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
						ErrorText.Text += sqlCommandStatement;
						ErrorText.Text += ex.ToString();
					}	
					
				}
				else{
					if(we == null || we == "" || we == " ")
						ErrorText.Text += "Please select a Weekending Date... <br />";
					else
						ErrorText.Text += "Row " + i + " is Incomplete [" + nameStr + " / " + areaStr + "] (Name and  Area are required Fields)<br />";
				}
			}
			
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
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
		
    }

    protected void Selection_Change_Line(object sender, EventArgs e)
	{
		DropDownList ddlLine = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET line = @TEXT WHERE id=@ID";

		if(ddlLine.Text == ""){
			SqlCmd(sqlCommandStatement, id, "ddlLineNull");
		}
		else {
			SqlCmd(sqlCommandStatement, id, ddlLine.Text);
		}
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
	}

    protected void Selection_Change_Reg(object sender, EventArgs e)
	{
		DropDownList ddlReg_Addo = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE reg SET area = @TEXT WHERE id=@ID";
		if(ddlReg_Addo.Text == ""){
			SqlCmd(sqlCommandStatement, id, "ddlLineNull");
		}
		else {
			SqlCmd(sqlCommandStatement, id, ddlReg_Addo.Text);
		}
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
		
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
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
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
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
    }

	protected void Area_Change(object sender, EventArgs e)
	{
		DropDownList ddlArea = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET area = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlArea.Text);		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

    }
	
	protected void Display(object sender, EventArgs e)
	{
		// int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        // GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		
		// TextBox tb = (TextBox)row.FindControl("name");
		// string vTxt = tb.Text; // get the value from TextBox		
		// lblnameid.Text = vTxt;

		// DropDownList dl = (DropDownList)row.FindControl("ddlLine");
		// vTxt = dl.Text; // get the value from TextBox		
		// lineid.Text = vTxt;
		
		// tb = (TextBox)row.FindControl("service");
		// vTxt = tb.Text; // get the value from TextBox		
		// serviceid.Text = vTxt;

		// tb = (TextBox)row.FindControl("reg");
		// vTxt = tb.Text; // get the value from TextBox		
		// regid.Text = vTxt;

		// tb = (TextBox)row.FindControl("appt");
		// vTxt = tb.Text; // get the value from TextBox		
		// apptid.Text = vTxt;

		// tb = (TextBox)row.FindControl("phone");
		// vTxt = tb.Text; // get the value from TextBox		
		// phoneid.Text = vTxt;

		// dl = (DropDownList)row.FindControl("ddlStatus");
		// vTxt = dl.Text; // get the value from TextBox		
		// statusid.Text = vTxt;

		// dl = (DropDownList)row.FindControl("ddlOrg");
		// vTxt = dl.Text; // get the value from TextBox		
		// orgid.Text = vTxt;

		// tb = (TextBox)row.FindControl("notes");
		// vTxt = tb.Text; // get the value from TextBox		
		// notesid.Text = vTxt;
	
		// string vId = row.Cells[0].Text;		
		// id.Text = vId; 
		
		// ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
		
	}

	protected void DeleteRow(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
		GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		IdToDelete.Text = vId; 
		deleteID = vId;
		ModalPopupExtender1.Show();		
	}

	public void btnDelete_Click(Object sender, EventArgs e)
	{	
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
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

			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

		} 
		ModalPopupExtender1.Hide();
    }

	public void UpdatePanels()
	{
		UpdatePanelTotal.Update();
		UpdatePanelStarted.Update();
		UpdatePanelScheduled.Update();
		UpdatePanelNamed.Update();
		UpdatePanelFuture.Update();
		UpdatePanelFullyPaid.Update();
	}
	
	public void btnUpdate_Click(Object sender, EventArgs e)
	{				

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			// int id  = Request.Form["id"];	
			// string id = String.Format("{0}", 		Request.Form["id"]);	
			// string name = String.Format("{0}", 		Request.Form["lblnameid"]);	
			// string amount = String.Format("{0}", 	Request.Form["amountid"]);	
			// string rank = String.Format("{0}", 		Request.Form["rankid"]);	
			// string service = String.Format("{0}", 	Request.Form["serviceid"]);	
			// string reg = String.Format("{0}", 		Request.Form["regid"]);	
			// string scheduled = String.Format("{0}", Request.Form["apptid"]);	
			// string line = String.Format("{0}", 		Request.Form["lineid"]);				
			// string status = String.Format("{0}", 	Request.Form["statusid"]);				
			// string bird_dog = String.Format("{0}", 	Request.Form["bird_dogid"]);				
			// string phone = String.Format("{0}", 	Request.Form["phoneid"]);				
			// string org = String.Format("{0}", 		Request.Form["orgid"]);				
			// string notes = String.Format("{0}", 	Request.Form["notesid"]);				
			
			// CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			// TextInfo textInfo = cultureInfo.TextInfo;
			// name = textInfo.ToTitleCase(name.ToLower());

			// string sqlCommandStatement = String.Format("UPDATE bis SET name = '{0}', amount = '{1}', " + 
															// " service 		= '{2}', " + 
															// " reg 			= '{3}', " + 
															// " phone 		= '{4}', " + 
															// " appt	 		= '{5}', " + 
														    // " scheduled_type = '{6}', " + 
															// " rank	 		= '{7}', " + 
															// " line	 		= '{8}', " + 
															// " status	 	= '{9}', " + 
															// " bird_dog	 	= '{10}', " + 
															// " org		 	= '{11}', " + 
															// " notes	 		= '{12}', tm = getdate() " + 
															// " WHERE id='{13}'", 
															// name.Replace("'", "''"), 
															// amount, 
															// service.Replace("'", "''"), 
															// reg.Replace("'", "''"), 
															// phone, 
															// scheduled, 
															// type, 
															// rank, 
															// line, 
															// status, 
															// bird_dog.Replace("'", "''"), 
															// org,
															// notes.Replace("'", "''"), 
															// id
															// );									
			
			// try
			// {		
				// using (SqlConnection conn = databaseConnection.CreateSqlConnection())
				// {
				// conn.Open();
				// using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement, conn))
				// {
					// Cmd.ExecuteNonQuery();						
				// }
				// conn.Close();
				// }
			// }
			// catch (SqlException ex)
			// {
				// ErrorText.Text = ex.ToString();
			// }

			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
			else
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

		// PopulateLine();		
		
		try // STARTED
		{
			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == status
				where c.Field<string>("reg_cat_id") == vCat
				select c ;

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewStarted.DataSource = t2;
				GridViewStarted.DataBind();				
			} else {
				GridViewStarted.DataSource = new DataTable();
				GridViewStarted.DataBind();
			}

			string resultStarted = "0";
			string resultStartedDay = "0";
			string resultStartedFdn = "0";
			
			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and rank = 'PS'  and reg_cat_id = '" + vCat + "'";
			var cmdDay = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and rank = 'PS' and org = 'Day' and reg_cat_id = '" + vCat + "'";
			var cmdFdn = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and rank = 'PS'  and org = 'Fdn' and reg_cat_id = '" + vCat + "'";

			string rstPRPS = "0";
			string rstPRPSDay = "0";
			string rstPRPSFdn = "0";
			
			var cmdPRPS = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and rank = 'PRPS'  and reg_cat_id = '" + vCat + "'";
			var cmdPRPSDay = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and rank = 'PRPS' and org = 'Day' and reg_cat_id = '" + vCat + "'";
			var cmdPRPSFdn = "SELECT COUNT(1) AS total from bis WHERE status = '" + status + "' and rank = 'PRPS'  and org = 'Fdn' and reg_cat_id = '" + vCat + "'";

			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						resultStarted = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdDay, conn))
					{
						resultStartedDay = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdFdn, conn))
					{
						resultStartedFdn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdPRPS, conn))
					{
						rstPRPS = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdPRPSDay, conn))
					{
						rstPRPSDay = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdPRPSFdn, conn))
					{
						rstPRPSFdn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
			}	
			
			if(OrgText.Text=="Combined"){
				lblStart.Text = resultStarted;
				CardStarted.Text = resultStarted;		
				CardStartedDay.Text = resultStartedDay + " (Day)";				
				CardStartedFdn.Text = resultStartedFdn + " (Fdn)";				
				
				lblPRPS.Text = rstPRPS;
				CardStarted6.Text = rstPRPS;		
				CardStartedDay6.Text = rstPRPSDay + " (Day)";				
				CardStartedFdn6.Text = rstPRPSFdn + " (Fdn)";				
				
			}else if(OrgText.Text=="Day"){
				
				lblStart.Text = resultStartedDay;
				CardStarted.Text = resultStartedDay ;				
				CardStartedDay.Text = "";				
				CardStartedFdn.Text = "";				
				
				lblPRPS.Text = rstPRPSDay;
				CardStarted6.Text = rstPRPSDay;		
				CardStartedDay6.Text = "";				
				CardStartedFdn6.Text = "";				

			}else if(OrgText.Text=="Fdn"){
				
				lblStart.Text = resultStartedFdn;
				CardStarted.Text = resultStartedFdn ;	
				CardStartedDay.Text = "";				
				CardStartedFdn.Text = "";				
				
				lblPRPS.Text = rstPRPSFdn;
				CardStarted6.Text = rstPRPSFdn;		
				CardStartedDay6.Text = "";				
				CardStartedFdn6.Text = "";				
			}

		} 
		catch(Exception e) {
			ErrorText.Text = "Caught STARTED Exception: " + e;				
		}
		try // SCHEDULED
		{
			
			string resultScheduled = "0";
			string resultScheduledDay = "0";
			string resultScheduledFdn = "0";
			
			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Scheduled" 
				where c.Field<string>("reg_cat_id") == vCat
				select c ;

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewScheduled.DataSource = t2;
				GridViewScheduled.DataBind();				
			} else {
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
			resultScheduled = String.Format("{0:D}", result1);	

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
			resultScheduledDay = String.Format("{0:D}", result1);	

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
			resultScheduledFdn = String.Format("{0:D}", result1);	
			
			if(OrgText.Text=="Combined"){
				LblSched.Text = resultScheduled;
				CardScheduled.Text = resultScheduled;		
				CardScheduledDay.Text = resultScheduledDay + " (Day)";		
				CardScheduledFdn.Text = resultScheduledFdn + " (Fdn)";				
			}else if(OrgText.Text=="Day"){
				LblSched.Text = resultScheduledDay;
				CardScheduled.Text = resultScheduledDay ;	
				CardScheduledDay.Text = "";				
				CardScheduledFdn.Text = "";				
			}else if(OrgText.Text=="Fdn"){
				LblSched.Text = resultScheduledFdn;
				CardScheduled.Text = resultScheduledFdn ;
				CardScheduledDay.Text = "";				
				CardScheduledFdn.Text = "";				
			}
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught SCHEDULED Exception: " + e;				
		}
		try // NAMED
		{

			string resultNamed = "0";
			string resultNamedDay = "0";
			string resultNamedFdn = "0";
		
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
			resultNamed = String.Format("{0:D}", result1);	

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
			resultNamedDay = String.Format("{0:D}", result1);	

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
			resultNamedFdn = String.Format("{0:D}", result1);	
			
			if(OrgText.Text=="Combined"){
				LblNamed.Text = resultNamed;
				CardNamed.Text = resultNamed;	
				CardNamedDay.Text = resultNamedDay + " (Day)";				
				CardNamedFdn.Text = resultNamedFdn + " (Fdn)";
				
			}else if(OrgText.Text=="Day"){
				
				LblNamed.Text = resultNamedDay;
				CardNamed.Text = resultNamedDay ;				
				CardNamedDay.Text = "";				
				CardNamedFdn.Text = "";				
				
			}else if(OrgText.Text=="Fdn"){
				
				LblNamed.Text = resultNamedFdn;
				CardNamed.Text = resultNamedFdn ;	
				CardNamedDay.Text = "";				
				CardNamedFdn.Text = "";				
				
			}
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught NAMED Exception: " + e;				
		}
		try // FUTURE
		{

			string resultFuture = "0";
			string resultFutureDay = "0";
			string resultFutureFdn = "0";
		
			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Future" 
				where c.Field<string>("reg_cat_id") == vCat
				select c ;

			var mySum = 0;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewFuture.DataSource = t2;
				GridViewFuture.DataBind();
				
			} else {
				GridViewFuture.DataSource = new DataTable();
				GridViewFuture.DataBind();
			}

			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Future' and reg_cat_id = '" + vCat + "'";
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
			resultFuture = String.Format("{0:D}", result1);	

			var cmdDay = "SELECT COUNT(1) AS total from bis WHERE status = 'Future' and org = 'Day' and reg_cat_id = '" + vCat + "'";
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
			resultFutureDay = String.Format("{0:D}", result1);	

			var cmdFdn = "SELECT COUNT(1) AS total from bis WHERE status = 'Future' and org = 'Fdn' and reg_cat_id = '" + vCat + "'";
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
			resultFutureFdn = String.Format("{0:D}", result1);	
			
			if(OrgText.Text=="Combined"){
				LblFuture.Text = resultFuture;
				CardFuture.Text = resultFuture;	
				CardFutureDay.Text = resultFutureDay + " (Day)";				
				CardFutureFdn.Text = resultFutureFdn + " (Fdn)";
				
			}else if(OrgText.Text=="Day"){
				
				LblFuture.Text = resultFutureDay;
				CardFuture.Text = resultFutureDay ;				
				CardFutureDay.Text = "";				
				CardFutureFdn.Text = "";				
				
			}else if(OrgText.Text=="Fdn"){
				
				LblFuture.Text = resultFutureFdn;
				CardFuture.Text = resultFutureFdn ;	
				CardFutureDay.Text = "";				
				CardFutureFdn.Text = "";				
				
			}
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught NAMED Exception: " + e;				
		}

		UpdatePanels();
	}	
	
	private void DataGrid_LoadFPPP(DataTable command, string type)
	{	

		DataTable dataTable = new DataTable();
        dataTable = command;
		string sortDir = ViewState["SortDirFP"] as string;
		string sortExp = ViewState["SortExpFP"] as string;
		
		GridView3.DataSource = dataTable;
		GridView3.DataBind();
		Session["dt"] = dataTable;
		
		//FPPP
		// ErrorText.Text = dataTable.Rows.Count.ToString();

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

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");		
		
		UpdatePanels();
		
    }

	protected void TaskGridViewFPPP_Sorting(object sender, GridViewSortEventArgs e)
	{				
		string sortExp = ViewState["SortExpression"] as string;		
		string sortDir = ViewState["SortDirection"] as string;
		if(sortDir == "asc" & sortExp == e.SortExpression.ToString())
			ViewState["SortDirection"] = "desc";
		else
			ViewState["SortDirection"] = "asc";
		ViewState["SortExpression"] = e.SortExpression.ToString();

		if(filterOn==1){
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "cf");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
		}
		
		UpdatePanels();
		
    }
	
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
	
		if(filterOn==1){
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "cf");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
		}

    }

    protected void Selection_Change_Page(object sender, EventArgs e)
	{
		if(filterOn==1){
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_LoadFPPP(DAL.FilterFP(OrgText.Text, "All"), "reg");
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
