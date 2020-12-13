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
	static string graphWeeks = "26";
	static string fromWE;
	static string toWE;

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
			OrgText.Text = "Day";
			day.Attributes["class"] += " active";		
			fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
			cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		   
		}

		searchText = "";
		searchCol = "";
		searchWE = "";
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
		ShieldChartbyRegThisWeek.DataBind();
		ShieldChartbyRegLastWeek.DataBind();
		ShieldChartPie1.DataBind();
		ShieldChartPie2.DataBind();
		ShieldChartPie3.DataBind();
		ShieldChartPie4.DataBind();
		ShieldChartPie5.DataBind();
		ShieldChartPie6.DataBind();
		ShieldChart7R1.DataBind();
		ShieldChart7R2.DataBind();
		ShieldChart7R3.DataBind();
		ShieldChart7R4.DataBind();
		ShieldChart7R5.DataBind();
		ShieldChart7R6.DataBind();
		ShieldChartReg1.DataBind();
		ShieldChartReg2.DataBind();
		ShieldChartReg3.DataBind();
		ShieldChartReg4.DataBind();
		ShieldChartReg5.DataBind();
		ShieldChartReg6.DataBind();
		ShieldChartbyReg.DataBind();
		
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
		ShieldChartbyRegThisWeek.DataBind();
		ShieldChartbyRegLastWeek.DataBind();
		ShieldChartPie1.DataBind();
		ShieldChartPie2.DataBind();
		ShieldChartPie3.DataBind();
		ShieldChartPie4.DataBind();
		ShieldChartPie5.DataBind();
		ShieldChartPie6.DataBind();
		ShieldChart7R1.DataBind();
		ShieldChart7R2.DataBind();
		ShieldChart7R3.DataBind();
		ShieldChart7R4.DataBind();
		ShieldChart7R5.DataBind();
		ShieldChart7R6.DataBind();
		ShieldChartReg1.DataBind();
		ShieldChartReg2.DataBind();
		ShieldChartReg3.DataBind();
		ShieldChartReg4.DataBind();
		ShieldChartReg5.DataBind();
		ShieldChartReg6.DataBind();
		ShieldChartbyReg.DataBind();
        
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
		ShieldChartbyRegThisWeek.DataBind();
		ShieldChartbyRegLastWeek.DataBind();
		ShieldChartPie1.DataBind();
		ShieldChartPie2.DataBind();
		ShieldChartPie3.DataBind();
		ShieldChartPie4.DataBind();
		ShieldChartPie5.DataBind();
		ShieldChartPie6.DataBind();
		ShieldChart7R1.DataBind();
		ShieldChart7R2.DataBind();
		ShieldChart7R3.DataBind();
		ShieldChart7R4.DataBind();
		ShieldChart7R5.DataBind();
		ShieldChart7R6.DataBind();
		ShieldChartReg1.DataBind();
		ShieldChartReg2.DataBind();
		ShieldChartReg3.DataBind();
		ShieldChartReg4.DataBind();
		ShieldChartReg5.DataBind();
		ShieldChartReg6.DataBind();
		ShieldChartbyReg.DataBind();
        
    }
		
	private class RegBar
	{
		public string full_name { get; set; }
		public string tot { get; set; }
		public string inv { get; set; }
		public string lastwk { get; set; }
	}
	
	private class Daily7R
	{
		public string dayofweek { get; set; }
		public string thisweekgi { get; set; }
		public string thisweekinv { get; set; }
		public string lastweekgi { get; set; }
		public string thisweekcum { get; set; }
		public string thisweekinvcum { get; set; }
		public string lastweekcum { get; set; }
	}

	private Daily7R PrepareDaily7R(Daily7R d){
		
		Daily7R dd = d;
		
		DateTime now = DateTime.Now;
		string s = now.DayOfWeek.ToString();
		string today = s.Substring(0, 3);
		
		if(today=="Fri" && (dd.dayofweek =="Sat" || dd.dayofweek =="Sun" || dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweekgi = null;
			dd.thisweekcum = null;
			dd.thisweekinv = null;
			dd.thisweekinvcum = null;
		}
		else if(today=="Sat" && (dd.dayofweek =="Sun" || dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweekgi = null;
			dd.thisweekcum = null;
			dd.thisweekinv = null;
			dd.thisweekinvcum = null;
		}
		else if(today=="Sun" && (dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweekgi = null;
			dd.thisweekcum = null;
			dd.thisweekinv = null;
			dd.thisweekinvcum = null;
		}
		else if(today=="Mon" && (dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweekgi = null;
			dd.thisweekcum = null;
			dd.thisweekinv = null;
			dd.thisweekinvcum = null;
		}
		else if(today=="Tue" && (dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweekgi = null;
			dd.thisweekcum = null;
			dd.thisweekinv = null;
			dd.thisweekinvcum = null;
		}
		else if(today=="Wed" && (dd.dayofweek =="Thu2")){
			dd.thisweekgi = null;
			dd.thisweekcum = null;
			dd.thisweekinv = null;
			dd.thisweekinvcum = null;
		}

		return dd;
		
	}
	
	private List<Daily7R> Load7RData(List<Daily7R> ds, string regName){

		List<Daily7R> datasource = ds;
		
		//GET THE DATA
		DataTable dataThisWeek = new DataTable();
		dataThisWeek = DAL.GetDailyRegGI("thisweek", regName);

		DataTable dataLastWeek = new DataTable();
		dataLastWeek = DAL.GetDailyRegGI("lastweek", regName);

		DataTable dataThisWeekInv = new DataTable();
		dataThisWeekInv = DAL.GetDailyRegGI("thisweekinv", regName);

		//PREPARE THE DATA
		string d = ""; 
		int thiswk = 0; 
		int thiswkinv = 0; 
		int lastwk = 0;
		int thiswkcum = 0;
		int lastwkcum = 0;
		int thiswkinvcum = 0;
		
		for (int j = 0; j < dataThisWeek.Rows.Count; j++)
		{
			for (int i = 0; i < dataThisWeek.Columns.Count; i++)    
			{    
				if(dataThisWeek.Columns[i].ColumnName=="week_day"){
					d = dataThisWeek.Rows[j].ItemArray[i].ToString();
				} else if(dataThisWeek.Columns[i].ColumnName=="tot"){
					Int32.TryParse(dataThisWeek.Rows[j].ItemArray[i].ToString(), out thiswk);					
					thiswkcum += thiswk;
				}
			}

			Daily7R daily7R = new Daily7R{			
				dayofweek = d,
				thisweekgi = thiswk.ToString(),
				thisweekcum = thiswkcum.ToString()
			};
			datasource.Add(daily7R);			
		};

		for (int j = 0; j < dataLastWeek.Rows.Count; j++)
		{
			foreach(Daily7R dd in datasource)
			{
				if(dataLastWeek.Rows[j].ItemArray[0].ToString()==dd.dayofweek){
					dd.lastweekgi = dataLastWeek.Rows[j].ItemArray[2].ToString();
					Int32.TryParse(dataLastWeek.Rows[j].ItemArray[2].ToString(), out lastwk);	
					lastwkcum += lastwk;
					dd.lastweekcum = lastwkcum.ToString();
				}
			}
		}

		for (int j = 0; j < dataThisWeekInv.Rows.Count; j++)
		{
			foreach(Daily7R dd in datasource)
			{
				if(dataThisWeekInv.Rows[j].ItemArray[0].ToString()==dd.dayofweek){
					dd.thisweekinv = dataThisWeekInv.Rows[j].ItemArray[2].ToString();
					Int32.TryParse(dataThisWeekInv.Rows[j].ItemArray[2].ToString(), out thiswkinv);	
					thiswkinvcum += thiswkinv;
					dd.thisweekinvcum = thiswkinvcum.ToString();
				}
			}
		}
		
		foreach(Daily7R dd in datasource)
		{
			Daily7R d7 = new Daily7R();
			d7 = PrepareDaily7R(dd);
			
			dd.thisweekgi = d7.thisweekgi;
			dd.thisweekcum = d7.thisweekcum;
			dd.thisweekinv = d7.thisweekinv;
			dd.thisweekinvcum = d7.thisweekinvcum;
		}

		return datasource;
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
		
		
	}	

	protected void ShieldChartbyRegThisWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dataThisWeek = new DataTable();
		dataThisWeek = DAL.GetOrgIncomeBar(OrgText.Text, "thisweek");

		DataTable dataThisWeekInv = new DataTable();
		dataThisWeekInv = DAL.GetOrgIncomeBar(OrgText.Text, "thisweekinv");
		
		List<RegBar> datasource = new List<RegBar>();

		string regName = ""; 
		int thiswk = 0; 
		int thiswkinv = 0; 

		if(dataThisWeek!=null){
		
			for (int j = 0; j < dataThisWeek.Rows.Count; j++)
			{
				for (int i = 0; i < dataThisWeek.Columns.Count; i++)    
				{    
					if(dataThisWeek.Columns[i].ColumnName=="full_name"){
						regName = dataThisWeek.Rows[j].ItemArray[i].ToString();
					} else if(dataThisWeek.Columns[i].ColumnName=="tot"){
						Int32.TryParse(dataThisWeek.Rows[j].ItemArray[i].ToString(), out thiswk);					
					}
				}

				RegBar regbar = new RegBar{			
					full_name = regName,
					tot = thiswk.ToString()
				};
				datasource.Add(regbar);			
			};
		}
		//ADD THE INVOICED DATA 
		if(dataThisWeekInv!=null){

			for (int j = 0; j < dataThisWeekInv.Rows.Count; j++)
			{
				foreach(RegBar dd in datasource)
				{
					if(dataThisWeekInv.Rows[j].ItemArray[0].ToString()==dd.full_name){
						dd.inv = dataThisWeekInv.Rows[j].ItemArray[1].ToString();
					}
				}
			}
		}

		if(datasource.Count >0)
			ShieldChartbyRegThisWeek.DataSource = datasource;		
		else
			ShieldChartbyRegThisWeek.SecondaryHeader.Text = "(No Data Found)";
		
	}
	
	protected void ShieldChartbyRegLastWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetOrgIncomeBar(OrgText.Text, "lastweek");

		if(dt.Rows.Count > 0){
			ShieldChartbyRegLastWeek.DataSource = dt;
		} else {
			ShieldChartbyRegLastWeek.SecondaryHeader.Text = "(No Data Found)";
		}

	}
	
	protected void ShieldChartPie1_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "01");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			pie_regid1.Text = regName;

			DataTable dt = new DataTable();
			dt = DAL.GetProcurementPieReg(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartPie1.DataSource = dt;

		}
	}

	protected void ShieldChartPie2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "02");
		
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			pie_regid2.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetProcurementPieReg(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartPie2.DataSource = dt;
		}
	}

	protected void ShieldChartPie3_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "03");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			pie_regid3.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetProcurementPieReg(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartPie3.DataSource = dt;
		}
	}

	protected void ShieldChartPie4_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "04");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			pie_regid4.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetProcurementPieReg(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartPie4.DataSource = dt;
		}
	}

	protected void ShieldChartPie5_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "05");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			pie_regid5.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetProcurementPieReg(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartPie5.DataSource = dt;
		}
	}

	protected void ShieldChartPie6_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "06");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			pie_regid6.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetProcurementPieReg(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartPie6.DataSource = dt;
		}
	}

	protected void ShieldChart7R1_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "01");
		
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			reg7R1.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetWeeklyRegIncome(regName);
		
			//GET THE DATA
			DataTable dataThisWeek = new DataTable();
			dataThisWeek = DAL.GetDailyRegGI("thisweek", regName);

			DataTable dataLastWeek = new DataTable();
			dataLastWeek = DAL.GetDailyRegGI("lastweek", regName);

			DataTable dataThisWeekInv = new DataTable();
			dataThisWeekInv = DAL.GetDailyRegGI("thisweekinv", regName);

			//PREPARE THE DATA
			string d = ""; 
			int thiswk = 0; 
			int thiswkinv = 0; 
			int lastwk = 0;
			int thiswkcum = 0;
			int lastwkcum = 0;
			int thiswkinvcum = 0;
			
			List<Daily7R> datasource = new List<Daily7R>();

			if(dataThisWeek!=null){
				
				for (int j = 0; j < dataThisWeek.Rows.Count; j++)
				{
					for (int i = 0; i < dataThisWeek.Columns.Count; i++)    
					{    
						if(dataThisWeek.Columns[i].ColumnName=="week_day"){
							d = dataThisWeek.Rows[j].ItemArray[i].ToString();
						} else if(dataThisWeek.Columns[i].ColumnName=="tot"){
							Int32.TryParse(dataThisWeek.Rows[j].ItemArray[i].ToString(), out thiswk);					
							thiswkcum += thiswk;
						}
					}

					Daily7R daily7R = new Daily7R{			
						dayofweek = d,
						thisweekgi = thiswk.ToString(),
						thisweekcum = thiswkcum.ToString()
					};
					datasource.Add(daily7R);			
				};
			}

			if(dataLastWeek!=null){
			
				for (int j = 0; j < dataLastWeek.Rows.Count; j++)
				{
					foreach(Daily7R dd in datasource)
					{
						if(dataLastWeek.Rows[j].ItemArray[0].ToString()==dd.dayofweek){
							dd.lastweekgi = dataLastWeek.Rows[j].ItemArray[2].ToString();
							Int32.TryParse(dataLastWeek.Rows[j].ItemArray[2].ToString(), out lastwk);	
							lastwkcum += lastwk;
							dd.lastweekcum = lastwkcum.ToString();
						}
					}
				}
			}

			if(dataThisWeekInv!=null){
			
				for (int j = 0; j < dataThisWeekInv.Rows.Count; j++)
				{
					foreach(Daily7R dd in datasource)
					{
						if(dataThisWeekInv.Rows[j].ItemArray[0].ToString()==dd.dayofweek){
							dd.thisweekinv = dataThisWeekInv.Rows[j].ItemArray[2].ToString();
							Int32.TryParse(dataThisWeekInv.Rows[j].ItemArray[2].ToString(), out thiswkinv);	
							thiswkinvcum += thiswkinv;
							dd.thisweekinvcum = thiswkinvcum.ToString();
						}
					}
				}
			}
			
			foreach(Daily7R dd in datasource)
			{
				Daily7R d7 = new Daily7R();
				d7 = PrepareDaily7R(dd);
				
				dd.thisweekgi = d7.thisweekgi;
				dd.thisweekcum = d7.thisweekcum;
				dd.thisweekinv = d7.thisweekinv;
				dd.thisweekinvcum = d7.thisweekinvcum;
			}

			// ASSIGN THE DATA TO THE GRAPH
			if(datasource.Count >0)
				ShieldChart7R1.DataSource = datasource;		
			else
				ShieldChart7R1.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChart7R2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "02");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			reg7R2.Text = regName;

			//LOAD THE ARRAY 
			datasource = Load7RData(datasource, regName);

			// ASSIGN THE DATA TO THE GRAPH
			if(datasource.Count >0)
				ShieldChart7R2.DataSource = datasource;		
			else
				ShieldChart7R2.SecondaryHeader.Text = "(No Data Found)";
		}
	}
		
	protected void ShieldChart7R3_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "03");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			//UPDATE THE REG LABEL
			reg7R3.Text = regName;

			//LOAD THE ARRAY 
			datasource = Load7RData(datasource, regName);

			// ASSIGN THE DATA TO THE GRAPH
			if(datasource.Count >0)
				ShieldChart7R3.DataSource = datasource;		
			else
				ShieldChart7R3.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChart7R4_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "04");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			//UPDATE THE REG LABEL
			reg7R4.Text = regName;

			//LOAD THE ARRAY 
			datasource = Load7RData(datasource, regName);

			// ASSIGN THE DATA TO THE GRAPH
			if(datasource.Count >0)
				ShieldChart7R4.DataSource = datasource;		
			else
				ShieldChart7R4.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChart7R5_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "05");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			//UPDATE THE REG LABEL
			reg7R5.Text = regName;

			//LOAD THE ARRAY 
			datasource = Load7RData(datasource, regName);

			// ASSIGN THE DATA TO THE GRAPH
			if(datasource.Count >0)
				ShieldChart7R5.DataSource = datasource;		
			else
				ShieldChart7R5.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChart7R6_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "06");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			//UPDATE THE REG LABEL
			reg7R6.Text = regName;

			//LOAD THE ARRAY 
			datasource = Load7RData(datasource, regName);

			// ASSIGN THE DATA TO THE GRAPH
			if(datasource.Count >0)
				ShieldChart7R6.DataSource = datasource;		
			else
				ShieldChart7R6.SecondaryHeader.Text = "(No Data Found)";
		}
	}
		
	public void clickTest(Object sender, EventArgs e){

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "01");
		ErrorText.Text += "reg.Rows.Count = " + reg.Rows.Count + "<BR/>";
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			ErrorText.Text += "regName = " + regName + "<BR/>";
			DataTable dt = new DataTable();
			dt = DAL.GetProcurementPieReg(regName);
		}
		
		
	}
	
    public void BtnRange_Click(object sender, EventArgs e)
    {

		fromWE = "";
		toWE = "";
		ErrorText.Text = fromWE + " " + toWE;
		
		// ShieldChart1.DataSource = DAL.GetProcurementPie(fromWE, toWE);

	}


	protected void ShieldChartReg1_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "01");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			regid1.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetWeeklyRegIncome(regName);

			if(dt.Rows.Count>0)
				ShieldChartReg1.DataSource = dt;		
			else
				ShieldChartReg1.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartReg2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "02");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			regid2.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetWeeklyRegIncome(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartReg2.DataSource = dt;
		}
	}

	protected void ShieldChartReg3_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "03");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			regid3.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetWeeklyRegIncome(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartReg3.DataSource = dt;
		}
	}

	protected void ShieldChartReg4_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "04");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			regid4.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetWeeklyRegIncome(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartReg4.DataSource = dt;
		}
	}

	protected void ShieldChartReg5_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "05");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			regid5.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetWeeklyRegIncome(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartReg5.DataSource = dt;
		}
	}

	protected void ShieldChartReg6_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetOrgReg(OrgText.Text, "name", "06");
		if(reg.Rows.Count > 0){
			String regName = (String)reg.Rows[0][0];
			regid6.Text = regName;
			
			DataTable dt = new DataTable();
			dt = DAL.GetWeeklyRegIncome(regName);
				
			if(dt.Rows.Count>0)
				ShieldChartReg6.DataSource = dt;
		}
	}
	
	protected void ShieldChartbyReg_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetOrgIncomeBar(OrgText.Text, "all");
		if(dt.Rows.Count>0)
			ShieldChartbyReg.DataSource = dt;

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
