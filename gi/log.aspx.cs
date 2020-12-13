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
				
				// lblDate.Text = "Last IsPostBack was: " + DateTime.Now.ToString();
				
			}
			
		}

		if(!IsPostBack)
		{
			// lblDate.Text = "Last !IsPostBack was: " + DateTime.Now.ToString();
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
		HeadText.Text = "Combined GI Grid";
		HeaderText.Text = "Gross Income";
		Title = "Gross Income";

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
		FilterArea(s);

		// if (s!=null){
			// FilterArea(s);
		// } else {
			// DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
			// DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "all"), "reg");
		// }
		
		PopulateLine();
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
	
	private void PopulateLine()
	{
		DataTable dtd = new DataTable();		
		dtd = DAL.ReportByOrgArea("reg", "Day");

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
			} else if(row["line"].ToString()=="Other"){
				otherDay.Text = String.Format("{0:C0} ", row["cnt"]) ;
			}
		}

		DataTable dtf = new DataTable();		
		dtf = DAL.ReportByOrgArea("reg", "Fdn");

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
			} else if(rowf["line"].ToString()=="Other"){
				otherFdn.Text = String.Format("{0:C0} ", rowf["cnt"]) ;
			}
		}
		
		if(OrgText.Text=="Day"){
		
			resignFdn.Text = "";
			cfFdn.Text = "";
			arrivalFdn.Text = "";
			fsmFdn.Text = "";
			prospectingFdn.Text = "";
			otherFdn.Text = "";
		
		} else if (OrgText.Text=="Fdn"){

			resignDay.Text = "";
			cfDay.Text = "";
			arrivalDay.Text = "";
			fsmDay.Text = "";
			prospectingDay.Text = "";
			otherDay.Text = "";
			
		} else if (OrgText.Text=="Combined"){

			resignFdn.Text += " (Fdn)";
			cfFdn.Text += " (Fdn)";
			arrivalFdn.Text += " (Fdn)";
			fsmFdn.Text += " (Fdn)";
			prospectingFdn.Text += " (Fdn)";
			otherFdn.Text += " (Fdn)";

			resignDay.Text += " (Day)";
			cfDay.Text += " (Day)";
			arrivalDay.Text += " (Day)";
			fsmDay.Text += " (Day)";
			prospectingDay.Text += " (Day)";
			otherDay.Text += " (Day)";
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
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "reg");
		}
		else {
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "all"), "reg");
			HeaderText.Text = "Gross Income";
		}		
		// UpdatePanels();
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
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "reg");
		}
		else {
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "all"), "reg");
			HeaderText.Text = "Gross Income";
		}		
		// UpdatePanels();
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
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "reg");
		}
		else {
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "all"), "reg");
			HeaderText.Text = "Gross Income";
		}		
		// UpdatePanels();
    }

	public void FilterNamed(string area)
	{
		searchText = area;
		searchCol = "Line";
		DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		
	}
		
	public void FilterArea(string v)
	{

		HeaderText.Text = "Gross Income";

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
		} else if(v=="SRD"){
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
		} else if(v=="HGC"){
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
		} else if(v=="ACAD"){
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
		} else if(v=="DIV6"){
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
		} else if(v=="GAK"){
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
		} else {
			filterOn = 0;
		}
		
		searchText = "Rank";
		searchCol = v;
		
		HeaderText.Text += " (" + v + ")";

		resetLineBoxes();
		
		if(filterOn==1){
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "reg");
		}
		else {
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "all"), "reg");
			HeaderText.Text = "Gross Income";
			searchText = "";
			searchCol = "";
		}		
	}

	public void resetLineBoxes()
	{

		boxResign.Attributes.Add(		"class", boxResign.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
		boxArrival.Attributes.Add(		"class", boxArrival.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
		boxOther.Attributes.Add(		"class", boxOther.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
		boxCF.Attributes.Add(			"class", boxCF.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
		boxFSM.Attributes.Add(			"class", boxFSM.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
		boxProspecting.Attributes.Add(	"class", boxProspecting.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
		
	}

	public void resetAreaBoxes()
	{

		boxPurif.Attributes.Add(			"class", boxPurif.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
		boxSRD.Attributes.Add(				"class", boxSRD.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
		boxDiv6Processing.Attributes.Add(	"class", boxDiv6Processing.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
		boxHGC.Attributes.Add(				"class", boxHGC.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
		boxACADEMY.Attributes.Add(			"class", boxACADEMY.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
		boxDiv6.Attributes.Add(				"class", boxDiv6.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
		
	}

    protected void LinkButton_Command(Object sender, EventArgs e) 
    {
		var v = ((LinkButton)sender).CommandArgument;
		HeaderText.Text = "Gross Income";
		
		//ADD THE button pressed LOOK TO THE warning BOX
		if(v=="Resign"){
			string s = boxResign.Attributes["class"].ToString();
			if(s.Contains("bg-warning-gradient")){
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 0;
			} else {
				boxResign.Attributes["class"] += " bg-warning-gradient";		
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Arrival"){
			string s = boxArrival.Attributes["class"].ToString();
			if(s.Contains("bg-warning-gradient")){
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 0;
			} else {
				boxArrival.Attributes["class"] += " bg-warning-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="CF"){
			string s = boxCF.Attributes["class"].ToString();
			if(s.Contains("bg-warning-gradient")){
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 0;
			} else {
				boxCF.Attributes["class"] += " bg-warning-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="FSM"){
			string s = boxFSM.Attributes["class"].ToString();
			if(s.Contains("bg-warning-gradient")){
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 0;
			} else {
				boxFSM.Attributes["class"] += " bg-warning-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Prospecting"){
			string s = boxProspecting.Attributes["class"].ToString();
			if(s.Contains("bg-warning-gradient")){
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 0;
			} else {
				boxProspecting.Attributes["class"] += " bg-warning-gradient";		
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Other"){
			string s = boxOther.Attributes["class"].ToString();
			if(s.Contains("bg-warning-gradient")){
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 0;
			} else {
				boxOther.Attributes["class"] += " bg-warning-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-warning-gradient", ""));		      
				filterOn = 1;
			}
		}

		
		searchText = "Line";
		searchCol = v;
		
		HeaderText.Text += " (" + v + ")";

		resetAreaBoxes();
		
		if(filterOn==1){
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "reg");
			
		}
		else {
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "all"), "reg");
			HeaderText.Text = "Gross Income";
		}		

	}

    protected void LinkButtonArea_Command(Object sender, EventArgs e) 
    {
		var v = ((LinkButton)sender).CommandArgument;
		filter = (string)v;
		HeaderText.Text = "Gross Income";
		FilterArea(v);

	}
		
	protected void ExportToExcel_Click(object sender, EventArgs e)
	{

		var data = DAL.reg_log(HeadText.Text, OrgText.Text);
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
		DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		
    }

	protected void text_change_date(object sender, EventArgs e)
	{
		// ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE reg SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

		UpdatePanels();

	}

	protected void text_change_amount(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE reg SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

		UpdatePanels();
		
	}
	
	protected void text_change(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE reg SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

		UpdatePanels();

	}

	protected void text_change_reg(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE listReg SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

		UpdatePanels();

	}
	
	protected void text_change_addo(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE reg SET {0} = @TEXT WHERE id=@ID", text.ID);		
		SqlCmd(sqlCommandStatement, id, text.Text);
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

		UpdatePanels();

	}
		
	protected void text_change_appt(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE regAppt SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

		UpdatePanels();
		
	}

    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;

		string sqlCommandStatement = "";
		
		if(ddlStatus.Text=="GI Invoiced"){
			sqlCommandStatement = "UPDATE reg SET status = @TEXT, appt = getdate(), tm = getdate() WHERE id=@ID";
		} else {
			sqlCommandStatement = "UPDATE reg SET status = @TEXT, tm = getdate() WHERE id=@ID";
		}
		
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

		UpdatePanels();
		
    }

    protected void Selection_Change_ScheduledFor(object sender, EventArgs e)
    {
        DropDownList ddlSchedule = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE reg SET scheduled_type = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlSchedule.Text);		
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		
		
    }

    protected void Selection_Change_Line(object sender, EventArgs e)
	{
		DropDownList ddlLine = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE reg SET line = @TEXT WHERE id=@ID";
		if(ddlLine.Text == ""){
			SqlCmd(sqlCommandStatement, id, "ddlLineNull");
		}
		else {
			SqlCmd(sqlCommandStatement, id, ddlLine.Text);
		}

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		
		
	}

	protected void Selection_Change_Org(object sender, EventArgs e)
	{
		DropDownList ddlOrg = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE reg SET org = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlOrg.Text);		
	}

	protected void Selection_Change(object sender, EventArgs e)
	{
		DropDownList ddlCat = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE reg SET reg_cat_id = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlCat.Text);
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

	}

	protected void Rank_Change(object sender, EventArgs e)
	{
		DropDownList ddlRank = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE reg SET rank = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlRank.Text);		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

    }

	protected void Display(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		
		TextBox tb = (TextBox)row.FindControl("name");
		string vTxt = tb.Text; // get the value from TextBox		
		lblnameid.Text = vTxt;
		
		tb = (TextBox)row.FindControl("amount");
		vTxt = tb.Text; // get the value from TextBox		
		amountid.Text = vTxt;

		DropDownList dl = (DropDownList)row.FindControl("ddlLine");
		vTxt = dl.Text; // get the value from TextBox		
		lineid.Text = vTxt;
		
		tb = (TextBox)row.FindControl("service");
		vTxt = tb.Text; // get the value from TextBox		
		serviceid.Text = vTxt;

		tb = (TextBox)row.FindControl("reg");
		vTxt = tb.Text; // get the value from TextBox		
		regid.Text = vTxt;

		tb = (TextBox)row.FindControl("appt");
		vTxt = tb.Text; // get the value from TextBox		
		apptid.Text = vTxt;

		dl = (DropDownList)row.FindControl("ddlScheduled");
		vTxt = dl.Text; // get the value from TextBox		
		ddlScheduledid.Text = vTxt;

		tb = (TextBox)row.FindControl("phone");
		vTxt = tb.Text; // get the value from TextBox		
		phoneid.Text = vTxt;

		dl = (DropDownList)row.FindControl("ddlStatus");
		vTxt = dl.Text; // get the value from TextBox		
		statusid.Text = vTxt;

		tb = (TextBox)row.FindControl("bird_dog");
		vTxt = tb.Text; // get the value from TextBox		
		bird_dogid.Text = vTxt;
		
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

		ModalPopupExtender1.Show();		
	}

	protected void DeleteRow2(object sender, EventArgs e)
	{

		lblDate.Text += " DELETEROW " ;

		// int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        // GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		// string vId = row.Cells[0].Text;		
		// id.Text = vId; 
		
		// ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteModal();", true);
		
	}
 	
	public void btnDelete_Click(Object sender, EventArgs e)
	{	
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			// lblDate.Text += "btnDelete_Click() deleteID = " + deleteID;
			
			string sqlCommandStatement = String.Format("DELETE FROM reg WHERE id='{0}'", deleteID );									
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
				DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		
			
			UpdatePanels();

		} 


		ModalPopupExtender1.Hide();
    }

	protected void Click_AddToOpenCycles(object sender, EventArgs e)
	{

		AlertPanel.Text = "";

		if(OrgText.Text=="Combined"){
			
			AlertPanel.Text = "Choose either Day or Fdn.</br>";
			ModalPopupExtender2.Show();				

		} else {
	
			int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
			GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
			
			string addoid = "";
			string name = "";
			string area = "";
			string amount = "";
			string service = "";
			string line = "CF";
			string status = "Open Cycle";
			string notes = "Partially Paid";

			addoid = row.Cells[0].Text;
			name = Server.HtmlDecode(row.Cells[2].Text);
			service = Server.HtmlDecode(row.Cells[3].Text);
			amount = Server.HtmlDecode(row.Cells[5].Text);
			
			try{
				amount = Convert.ToInt32(Convert.ToDouble(amount)).ToString();
			}
			catch(Exception ex){
				amount = "0";
				ErrorText.Text = "ERROR: Amount was not a number.";
			}

			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			
			name = textInfo.ToTitleCase(name.ToLower());
			service = textInfo.ToTitleCase(service.ToLower());
			
			area = filter;
			
			if(area==null)
				area = "";

			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into reg(reg_cat_id, addo_id, name, amount, status, service, rank, line, notes, appt, tm, org) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @amount, @status, @service, @area, @line, @notes, getdate(), getdate(), @org)";

				using(SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();

					command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
					command.Parameters.AddWithValue("@addo_id",			addoid);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@amount",			amount);
					command.Parameters.AddWithValue("@status",			status);
					command.Parameters.AddWithValue("@service",			service);
					command.Parameters.AddWithValue("@area",			area);
					command.Parameters.AddWithValue("@line",			line);
					command.Parameters.AddWithValue("@notes",			notes);
					command.Parameters.AddWithValue("@org",				OrgText.Text);

					try{
						int result = command.ExecuteNonQuery();

						if(result < 0){
							AlertPanel.Text = "Error inserting data into Database!</br>";
							ModalPopupExtender2.Show();				
						} else {
							AlertPanel.Text += "Success! " + name + " was added to " + status + " for: " + service + ".</br>";
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
				DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
				DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "reg");
			}
			else {
				DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
				DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "all"), "reg");
				HeaderText.Text = "Gross Income";
			}		
			
			UpdatePanels();
		}
	}

	protected void Click_AddToPossible(object sender, EventArgs e)
	{

		AlertPanel.Text = "";

		if(OrgText.Text=="Combined"){
			
			AlertPanel.Text = "Choose either Day or Fdn.</br>";
			ModalPopupExtender2.Show();				

		} else {
	
			int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
			GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
			
			string addoid = "";
			string name = "";
			string area = "";
			string amount = "";
			string service = "";
			string line = "CF";
			string status = "Possible";
			string notes = "Partially Paid";

			addoid = row.Cells[0].Text;
			name = Server.HtmlDecode(row.Cells[2].Text);
			service = Server.HtmlDecode(row.Cells[3].Text);
			amount = Server.HtmlDecode(row.Cells[5].Text);
			
			try{
				amount = Convert.ToInt32(Convert.ToDouble(amount)).ToString();
			}
			catch(Exception ex){
				amount = "0";
				ErrorText.Text = "ERROR: Amount was not a number.";
			}

			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			
			name = textInfo.ToTitleCase(name.ToLower());
			service = textInfo.ToTitleCase(service.ToLower());
			
			area = filter;
			
			if(area==null)
				area = "";

			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into reg(reg_cat_id, addo_id, name, amount, status, service, rank, line, notes, appt, tm, org) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @amount, @status, @service, @area, @line, @notes, getdate(), getdate(), @org)";

				using(SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();

					command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
					command.Parameters.AddWithValue("@addo_id",			addoid);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@amount",			amount);
					command.Parameters.AddWithValue("@status",			status);
					command.Parameters.AddWithValue("@service",			service);
					command.Parameters.AddWithValue("@area",			area);
					command.Parameters.AddWithValue("@line",			line);
					command.Parameters.AddWithValue("@notes",			notes);
					command.Parameters.AddWithValue("@org",				OrgText.Text);

					try{
						int result = command.ExecuteNonQuery();

						if(result < 0){
							AlertPanel.Text = "Error inserting data into Database!</br>";
							ModalPopupExtender2.Show();				
						} else {
							AlertPanel.Text += "Success! " + name + " was added to " + status + " for: " + service + ".</br>";
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
				DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
				DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "reg");
			}
			else {
				DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
				DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "all"), "reg");
				HeaderText.Text = "Gross Income";
			}		
			
			UpdatePanels();
		}
	}

	private void DataGrid_LoadFPPP(DataTable command, string type)
	{	

		DataTable dataTable = new DataTable();
        dataTable = command;
		string sortDir = ViewState["SortDir"] as string;
		string sortExp = ViewState["SortExp"] as string;

		if(ViewState["SortExp"] != null)
		{					
			dataTable = resortPP(dataTable, sortExp, sortDir);
		}
		
		GridView3.DataSource = dataTable;
		GridView3.DataBind();

	}	
	
	public void UpdatePanels()
	{

		UpdatePanelTotal.Update();
		UpdatePanelInConfirmed.Update();
		UpdatePanelDefinite.Update();
		UpdatePanelPossible.Update();
		UpdatePanelOpenCycle.Update();
		UpdatePanelProspect.Update();
		UpdatePanelPartiallyPaid.Update();
	
	}
	
    protected void loginButton_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);//For testing.
        // programmaticModalPopup.Hide();
  
    }
	
	public void btnUpdate_Click(Object sender, EventArgs e)
	{				

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			// int id  = Request.Form["id"];	
			string id = String.Format("{0}", 		Request.Form["id"]);	
			string name = String.Format("{0}", 		Request.Form["lblnameid"]);	
			string amount = String.Format("{0}", 	Request.Form["amountid"]);	
			string rank = String.Format("{0}", 		Request.Form["rankid"]);	
			string service = String.Format("{0}", 	Request.Form["serviceid"]);	
			string reg = String.Format("{0}", 		Request.Form["regid"]);	
			string scheduled = String.Format("{0}", Request.Form["apptid"]);	
			string type = String.Format("{0}", 		Request.Form["ddlScheduledid"]);				
			string line = String.Format("{0}", 		Request.Form["lineid"]);				
			string status = String.Format("{0}", 	Request.Form["statusid"]);				
			string bird_dog = String.Format("{0}", 	Request.Form["bird_dogid"]);				
			string phone = String.Format("{0}", 	Request.Form["phoneid"]);				
			string org = String.Format("{0}", 		Request.Form["orgid"]);				
			string notes = String.Format("{0}", 	Request.Form["notesid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());
			amount = amount.Replace(",", "");
			amount = amount.Replace(".", "");

			string sqlCommandStatement = String.Format("UPDATE reg SET name = '{0}', amount = '{1}', " + 
															" service 		= '{2}', " + 
															" reg 			= '{3}', " + 
															" phone 		= '{4}', " + 
															" appt	 		= '{5}', " + 
														    " scheduled_type = '{6}', " + 
															" rank	 		= '{7}', " + 
															" line	 		= '{8}', " + 
															" status	 	= '{9}', " + 
															" bird_dog	 	= '{10}', " + 
															" org		 	= '{11}', " + 
															" notes	 		= '{12}', tm = getdate() " + 
															" WHERE id='{13}'", 
															name.Replace("'", "''"), 
															amount, 
															service.Replace("'", "''"), 
															reg.Replace("'", "''"), 
															phone, 
															scheduled, 
															type, 
															rank, 
															line, 
															status, 
															bird_dog.Replace("'", "''"), 
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

			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

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
		
		BothGoodLbl.Text = "";
		BothFigureLbl.Text = "";

		PopulateLine();
		
		try // TOTALS
		{
			var query = from c in dataTable.AsEnumerable() 
					   where c.Field<string>("status") == "GI Invoiced" && c.Field<string>("reg_cat_id") != "Archive"
						  || c.Field<string>("status") == "GI Confirmed" && c.Field<string>("reg_cat_id") != "Archive" 
					  select c ;	
			
			string ResultBothIn = "0";
			string ResultBothConf = "0";
			string ResultBothInv = "0";
			string ResultBothOn = "0";

			string ResultDayIn = "0";
			string ResultDayOn = "0";
			string ResultDayConf = "0";
			string ResultDayInv = "0";

			string ResultFdnIn = "0";
			string ResultFdnOn = "0";
			string ResultFdnConf = "0";
			string ResultFdnInv = "0";
					  
			var cmd1 = "SELECT SUM(amount) AS total from reg WHERE (status = 'GI Invoiced' or status = 'GI Confirmed') and reg_cat_id <> 'Archive'";
			var cmdInv = "SELECT SUM(amount) AS total from reg WHERE status = 'GI Invoiced' and reg_cat_id <> 'Archive'";
			var cmdConf = "SELECT SUM(amount) AS total from reg WHERE status = 'GI Confirmed' and reg_cat_id <> 'Archive'";
			var cmd2 = "SELECT SUM(amount) AS total from reg WHERE (status = 'GI Invoiced' or status = 'GI Confirmed') and reg_cat_id <> 'Archive' and org = 'Day'";
			var cmdInvD = "SELECT SUM(amount) AS total from reg WHERE status = 'GI Invoiced' and org = 'Day' and reg_cat_id <> 'Archive' ";
			var cmdConfD = "SELECT SUM(amount) AS total from reg WHERE status = 'GI Confirmed' and org = 'Day' and reg_cat_id <> 'Archive'";
			var cmdInvF = "SELECT SUM(amount) AS total from reg WHERE status = 'GI Invoiced' and org = 'Fdn' and reg_cat_id <> 'Archive'";
			var cmdConfF = "SELECT SUM(amount) AS total from reg WHERE status = 'GI Confirmed' and org = 'Fdn' and reg_cat_id <> 'Archive'";
			var cmd3 = "SELECT SUM(amount) AS total from reg WHERE reg_cat_id <> 'Archive' and status in ('GI Invoiced', 'GI Confirmed') and org = 'Fdn'";
			var cmd4 = "SELECT SUM(amount) AS total from reg WHERE status in ('GI Invoiced', 'GI Confirmed', 'This Week') and reg_cat_id <> 'Archive'";
			var cmd5 = "SELECT SUM(amount) AS total from reg WHERE org = 'Day' and status in ('GI Invoiced', 'GI Confirmed', 'This Week') and reg_cat_id <> 'Archive'";
			var cmd6 = "SELECT SUM(amount) AS total from reg WHERE org = 'Fdn' and status in ('GI Invoiced', 'GI Confirmed', 'This Week') and reg_cat_id <> 'Archive'";

			CultureInfo ie = new CultureInfo("en-ie");
				
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					//ResultBothIn = String.Format("{0:C0} ", cmd1);
					//ComboGiGridTotal[0] += cmd1.ToString();
					
					using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
						{
							ResultBothIn = String.Format("{0:C0} ", Cmd.ExecuteScalar());
							// ComboGiGridTotal[0] += Cmd.ExecuteScalar().ToString();
							// combinedGI = ResultBothIn;
						}
				}
				catch(Exception e) {
					ErrorText.Text = e.ToString();
				}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInv, conn))					
					{
						ResultBothInv = String.Format("{0:C0}", Cmd.ExecuteScalar());
						//ResultBothInv = String.Format(new CultureInfo("en-ie"), "{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdConf, conn))					
					{
						ResultBothConf = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmd2, conn))					
					{
						ResultDayIn = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInvD, conn))					
					{
						ResultDayInv = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdConfD, conn))					
					{
						ResultDayConf = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInvF, conn))					
					{
						ResultFdnInv = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdConfF, conn))					
					{
						ResultFdnConf = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try {	
					using (SqlCommand Cmd = new SqlCommand(cmd3, conn))
					{
						ResultFdnIn = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch{}	
				try{
					using (SqlCommand Cmd = new SqlCommand(cmd4, conn))
					{
						ResultBothOn = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch{}
				try{
				using (SqlCommand Cmd = new SqlCommand(cmd5, conn))
						{
					ResultDayOn = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch{}
				try{
				using (SqlCommand Cmd = new SqlCommand(cmd6, conn))
						{
					ResultFdnOn = String.Format("{0:C0}", Cmd.ExecuteScalar());
					}
				}
				catch{}						
				conn.Close();
			}			
			//if day fdn combined handling
			if(OrgText.Text=="Combined"){
				CardInvoiced.Text = ResultBothInv;		
				CardConfirmed.Text = ResultBothConf;		
				CardInConfirmed.Text = ResultBothIn;	
				CardInConfirmedA.Text = ResultBothOn;
				
				CardInvoicedDay.Text = ResultDayInv + " (Day)";				
				CardInvoicedFdn.Text = ResultFdnInv + " (Fdn)";				
				CardConfirmedDay.Text = ResultDayConf + " (Day)";		
				CardConfirmedFdn.Text = ResultFdnConf + " (Fdn)";				
				CardInConfirmedDay.Text = ResultDayIn + " (Day)";				
				CardInConfirmedFdn.Text = ResultFdnIn + " (Fdn)";
				CardInConfirmedDayA.Text = ResultDayOn + " (Day)";
				CardInConfirmedFdnA.Text = ResultFdnOn + " (Fdn)";
				
			} else if (OrgText.Text=="Day"){
				CardInvoiced.Text = ResultDayInv ;				
				CardConfirmed.Text = ResultDayConf ;	
				CardInConfirmed.Text = ResultDayIn ;				
				CardInConfirmedA.Text = ResultDayOn ;
				
				CardInvoicedDay.Text = "";				
				CardInvoicedFdn.Text = "";				
				CardConfirmedDay.Text = "";				
				CardConfirmedFdn.Text = "";				
				CardInConfirmedDay.Text = "";				
				CardInConfirmedFdn.Text = "";				
				CardInConfirmedDayA.Text = "";				
				CardInConfirmedFdnA.Text = "";				
				
			} else if (OrgText.Text=="Fdn"){
				CardInvoiced.Text = ResultFdnInv ;	
				CardConfirmed.Text = ResultFdnConf ;
				CardInConfirmed.Text = ResultFdnIn ;	
				CardInConfirmedA.Text = ResultFdnOn ;
				
				CardInvoicedDay.Text = "";				
				CardInvoicedFdn.Text = "";				
				CardConfirmedDay.Text = "";				
				CardConfirmedFdn.Text = "";				
				CardInConfirmedDay.Text = "";				
				CardInConfirmedFdn.Text = "";				
				CardInConfirmedDayA.Text = "";				
				CardInConfirmedFdnA.Text = "";				
				
			}

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView4.DataSource = t2;
				GridView4.DataBind();
				
				var mySum = t2.AsEnumerable().Sum(dr => dr.Field<int>("amount"));					
				BothInLbl.Text = String.Format("{0:C0}", mySum);					
				
				
			} else {
				GridView4.DataSource = new DataTable();
				GridView4.DataBind();
				BothInLbl.Text = String.Format("{0:C0}", 0);				
			}
			
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // RANK A
		{
			
			//TODO: CHANGE THIS TO THE DATATABLE.SELECT FORMAT 
			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "This Week"
				where c.Field<string>("reg_cat_id") == "LineUp"
				select c ;

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView5.DataSource = t2;
				GridView5.DataBind();				
				var mySum = t2.AsEnumerable().Sum(dr => dr.Field<int>("amount"));					
				BothGoodLbl.Text = String.Format("{0:C0}", mySum);					
			} else {
				//ErrorTextA.Text = "No Data Found";	
				GridView5.DataSource = new DataTable();
				GridView5.DataBind();
				BothGoodLbl.Text = String.Format("{0:C0}", 0);									
			}
			
			// var cmd1 = "SELECT SUM(amount) AS total from reg WHERE reg_cat_id = 'lineup' and rank = 'a'";
			var cmd1 = "SELECT SUM(amount) AS total from reg WHERE status = 'This Week'";
			string ResultGood = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						ResultGood = String.Format("{0:C0}", Cmd.ExecuteScalar());
						// ComboGiGridTotal[1] += Cmd.ExecuteScalar().ToString();
					}
				}
				catch{}							
				conn.Close();
			}	
			//BothGoodLbl.Text = ResultGood;				
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // RANK B
		{
			// var query = from c in dataTable.AsEnumerable() where c.Field<string>("reg_cat_id") == "Lineup" && c.Field<string>("rank") == "b"
				// || c.Field<string>("reg_cat_id") == "lineup" && c.Field<string>("rank") == "b" || c.Field<string>("reg_cat_id") == "LineUp" 
				// && c.Field<string>("rank") == "b" select c ;

			var query = from c in dataTable.AsEnumerable() where c.Field<string>("status") == "Possible" select c ;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView6.DataSource = t2;
				GridView6.DataBind();
				var mySum = t2.AsEnumerable().Sum(dr => dr.Field<int?>("amount"));					
				BothFigureLbl.Text = String.Format("{0:C0}", mySum);					
				
			} else {
				//ErrorTextB.Text = "No Data Found";	
				GridView6.DataSource = new DataTable();
				GridView6.DataBind();
				BothFigureLbl.Text = String.Format("{0:C0}", 0);									
			}

			// var cmd1 = "SELECT SUM(amount) AS total from reg WHERE reg_cat_id = 'lineup' and rank = 'b'";
			var cmd1 = "SELECT SUM(amount) AS total from reg WHERE status = 'Possible'";
			string ResultFigure = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
				using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						ResultFigure = String.Format("{0:C0}", Cmd.ExecuteScalar());
						// ComboGiGridTotal[2] += Cmd.ExecuteScalar().ToString();
					}
				}
				catch{}							
				conn.Close();
				}
			//BothFigureLbl.Text = ResultFigure;				
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // RANK C
		{
			// var query = from c in dataTable.AsEnumerable() where c.Field<string>("reg_cat_id") == "Lineup" && c.Field<string>("rank") == "c"
				// || c.Field<string>("reg_cat_id") == "lineup" && c.Field<string>("rank") == "c" || c.Field<string>("reg_cat_id") == "LineUp" 
				// && c.Field<string>("rank") == "c" select c ;

			
			var query = from c in dataTable.AsEnumerable() where c.Field<string>("status") == "Open Cycle" select c ;
				
			//GET ROWS AND CHECK IF MORE THAN ZERO BEFORE ADDING
			// OR TRY LOOPING THROUGH AND USING THE IMPORT ROW
			//CHECK IF THE QUERY HAS ROWS, IF NOT MAKE AN EMPTY GRIDVIEW

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView7.DataSource = t2;
				GridView7.DataBind();
				var mySum = t2.AsEnumerable().Sum(dr => dr.Field<int?>("amount"));					
				BothBoardLbl.Text = String.Format("{0:C0}", mySum);					
			} else {
				//ErrorTextC.Text = "No Data Found";	
				GridView7.DataSource = new DataTable();
				GridView7.DataBind();
				BothBoardLbl.Text = String.Format("{0:C0}", 0);									
			}
			
			// var cmd1 = "SELECT SUM(amount) AS total from reg WHERE reg_cat_id = 'lineup' and rank = 'c'";
			var cmd1 = "SELECT SUM(amount) AS total from reg WHERE status= 'Open Cycle'";
			string ResultBoard = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
				using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
									  
						ResultBoard = String.Format("{0:C0}", Cmd.ExecuteScalar());
						//ResultBoard = String.Format("{0:n0}", Cmd.ExecuteScalar());
						// ComboGiGridTotal[3] += Cmd.ExecuteScalar().ToString();
					}
				}
				catch{}							
				conn.Close();
				}
			//BothBoardLbl.Text = ResultBoard;				

		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // NOW PROSPECT
		{
			var query = from c in dataTable.AsEnumerable() where c.Field<string>("status").Contains("Prospect") select c ;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView7a.DataSource = t2;
				GridView7a.DataBind();
				var mySum = t2.AsEnumerable().Sum(dr => dr.Field<int?>("amount"));					
				BothBoardLbla.Text = String.Format("{0:C0}", mySum);					
			} else {
				//ErrorTextProspects.Text = "No Data Found";
				GridView7a.DataSource = new DataTable();
				GridView7a.DataBind();
				BothBoardLbla.Text = String.Format("{0:C0}", 0);									
			}
			
			// var cmd1 = "SELECT SUM(amount) AS total from reg WHERE reg_cat_id = 'lineup' and rank = 'c'";
			var cmd1 = "SELECT SUM(amount) AS total from reg WHERE status in ('Now Prospect', 'Future Prospect') ";
			string ResultBoard = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
				using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						ResultBoard = String.Format("{0:C0}", Cmd.ExecuteScalar());
						// ComboGiGridTotal[3] += Cmd.ExecuteScalar().ToString();
					}
				}
				catch{}							
				conn.Close();
			}
			//BothBoardLbla.Text = ResultBoard;				
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

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		
		
		UpdatePanels();
		
    }

	protected void TaskGridViewFPPP_Sorting(object sender, GridViewSortEventArgs e)
	{				
		string sortExp = ViewState["SortExp"] as string;		
		string sortDir = ViewState["SortDir"] as string;

		if(sortDir == "asc" & sortExp == e.SortExpression.ToString())
			ViewState["SortDir"] = "desc";
		else
			ViewState["SortDir"] = "asc";

		ViewState["SortExp"] = e.SortExpression.ToString();

		
		if(filterOn==1){
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "cf");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "All"), "reg");
		}
		
		UpdatePanels();
		
    }
	
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
	
		if(filterOn==1){
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "cf");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "All"), "reg");
		}

    }

    protected void grdView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView7.PageIndex = e.NewPageIndex;
	
		if(filterOn==1){
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		}
		else {
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
		}

    }

    protected void grdView7a_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView7a.PageIndex = e.NewPageIndex;
	
		if(filterOn==1){
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		}
		else {
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
		}

    }
	
    protected void Selection_Change_Page(object sender, EventArgs e)
	{
		if(filterOn==1){
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, filter), "reg");
		}
		else if(searchCol != "" && searchText != ""){
			DataGrid_LoadFPPP(DAL.Search_FPPP(searchText, searchCol), "cf");
		}
		else {
			DataGrid_LoadFPPP(DAL.FilterPP(OrgText.Text, "All"), "reg");
		}
    }
	
    protected void Selection_Change_Prospects(object sender, EventArgs e)
	{
		if(filterOn==1){
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		}
		else {
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
		}
    }
	
    public static DataTable resort(DataTable dt, string colName, string direction)
	{
		dt.DefaultView.Sort = colName + " " + direction;
		dt = dt.DefaultView.ToTable();
		return dt;
	}

    public static DataTable resortPP(DataTable dt, string colName, string direction)
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
	