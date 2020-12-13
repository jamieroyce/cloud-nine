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
	static string graphWeeks = "12";
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
				
				// ddlReg_Addo.DataSource = regList;
				// ddlReg_Addo.DataTextField = regList.Columns["full_name"].ToString();
				// ddlReg_Addo.DataValueField = regList.Columns["full_name"].ToString();
				// ddlReg_Addo.DataBind();
				
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

		searchText = "";
		searchCol = "";
		searchWE = "";

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
	
	public void clickTest(Object sender, EventArgs e){

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "2");
		String regName = (String)reg.Rows[0][0];
		reg7R2.Text = regName;

		ErrorText.Text = "Reg Name: " + regName + "<BR />";
		
		//LOAD THE ARRAY 
		datasource = Load7RData(datasource, regName);
		
		// ASSIGN THE DATA TO THE GRAPH
	
		foreach(Daily7R dd in datasource)
		{
			ErrorText.Text += "----------------" + dd.dayofweek + "<BR />";
			ErrorText.Text += "dayofweek:      " + dd.dayofweek + "<BR />";
			ErrorText.Text += "thisweekgi:     " + dd.thisweekgi + "<BR />";
			ErrorText.Text += "thisweekinv:    " + dd.thisweekinv + "<BR />";
			ErrorText.Text += "thisweekinv:    " + dd.thisweekinv + "<BR />";
			ErrorText.Text += "lastweekgi:     " + dd.lastweekgi + "<BR />";
			ErrorText.Text += "thisweekcum:    " + dd.thisweekcum + "<BR />";
			ErrorText.Text += "thisweekinvcum: " + dd.thisweekinvcum + "<BR />";
			ErrorText.Text += "lastweekcum:    " + dd.lastweekcum + "<BR />";
		}
		
	}

	protected void ShieldChart7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 


		// foreach(Daily7R dd in datasource)
		// {
			
			// DateTime now = DateTime.Now;
			// string s = now.DayOfWeek.ToString();
			// string today = s.Substring(0, 3);
			
			// if(today=="Fri" && (dd.dayofweek =="Sat" || dd.dayofweek =="Sun" || dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
				// dd.thisweekgi = null;
				// dd.thisweekcum = null;
				// dd.thisweekinv = null;
				// dd.thisweekinvcum = null;
			// }
			// else if(today=="Sat" && (dd.dayofweek =="Sun" || dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
				// dd.thisweekgi = null;
				// dd.thisweekcum = null;
				// dd.thisweekinv = null;
				// dd.thisweekinvcum = null;
			// }
			// else if(today=="Sun" && (dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
				// dd.thisweekgi = null;
				// dd.thisweekcum = null;
				// dd.thisweekinv = null;
				// dd.thisweekinvcum = null;
			// }
			// else if(today=="Mon" && (dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
				// dd.thisweekgi = null;
				// dd.thisweekcum = null;
				// dd.thisweekinv = null;
				// dd.thisweekinvcum = null;
			// }
			// else if(today=="Tue" && (dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
				// dd.thisweekgi = null;
				// dd.thisweekcum = null;
				// dd.thisweekinv = null;
				// dd.thisweekinvcum = null;
			// }
			// else if(today=="Wed" && (dd.dayofweek =="Thu2")){
				// dd.thisweekgi = null;
				// dd.thisweekcum = null;
				// dd.thisweekinv = null;
				// dd.thisweekinvcum = null;
			// }
			
		// }


	}

	protected void ShieldChart7R1_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "1");
		
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

		// ASSIGN THE DATA TO THE GRAPH
		if(datasource!=null)
			ShieldChart7R1.DataSource = datasource;		

	}

	protected void ShieldChart7R2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "2");
		String regName = (String)reg.Rows[0][0];
		reg7R2.Text = regName;

		//LOAD THE ARRAY 
		datasource = Load7RData(datasource, regName);

		// ASSIGN THE DATA TO THE GRAPH
		if(datasource!=null)
			ShieldChart7R2.DataSource = datasource;		
	}
		
	protected void ShieldChart7R3_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "3");
		String regName = (String)reg.Rows[0][0];
		//UPDATE THE REG LABEL
		reg7R3.Text = regName;

		//LOAD THE ARRAY 
		datasource = Load7RData(datasource, regName);

		// ASSIGN THE DATA TO THE GRAPH
		if(datasource!=null)
			ShieldChart7R3.DataSource = datasource;		
	}

	protected void ShieldChart7R4_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "4");
		String regName = (String)reg.Rows[0][0];
		//UPDATE THE REG LABEL
		reg7R4.Text = regName;

		//LOAD THE ARRAY 
		datasource = Load7RData(datasource, regName);

		// ASSIGN THE DATA TO THE GRAPH
		if(datasource!=null)
			ShieldChart7R4.DataSource = datasource;		
	}

	protected void ShieldChart7R5_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "5");
		String regName = (String)reg.Rows[0][0];
		//UPDATE THE REG LABEL
		reg7R5.Text = regName;

		//LOAD THE ARRAY 
		datasource = Load7RData(datasource, regName);

		// ASSIGN THE DATA TO THE GRAPH
		if(datasource!=null)
			ShieldChart7R5.DataSource = datasource;		
	}

	protected void ShieldChart7R6_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		//PREPARE THE ARRAY 
		List<Daily7R> datasource = new List<Daily7R>();

		//GET THE REG
		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "6");
		String regName = (String)reg.Rows[0][0];
		//UPDATE THE REG LABEL
		reg7R6.Text = regName;

		//LOAD THE ARRAY 
		datasource = Load7RData(datasource, regName);

		// ASSIGN THE DATA TO THE GRAPH
		if(datasource!=null)
			ShieldChart7R6.DataSource = datasource;		
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
		reg = DAL.GetReg("name", "1");
		String regName = (String)reg.Rows[0][0];
		regid1.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetWeeklyRegIncome(regName);
			
		if(dt!=null)
			ShieldChartReg1.DataSource = dt;

	}

	protected void ShieldChartReg2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "2");
		String regName = (String)reg.Rows[0][0];
		regid2.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetWeeklyRegIncome(regName);
			
		if(dt!=null)
			ShieldChartReg2.DataSource = dt;

	}

	protected void ShieldChartReg3_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "3");
		String regName = (String)reg.Rows[0][0];
		regid3.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetWeeklyRegIncome(regName);
			
		if(dt!=null)
			ShieldChartReg3.DataSource = dt;

	}

	protected void ShieldChartReg4_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "4");
		String regName = (String)reg.Rows[0][0];
		regid4.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetWeeklyRegIncome(regName);
			
		if(dt!=null)
			ShieldChartReg4.DataSource = dt;

	}

	protected void ShieldChartReg5_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "5");
		String regName = (String)reg.Rows[0][0];
		regid5.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetWeeklyRegIncome(regName);
			
		if(dt!=null)
			ShieldChartReg5.DataSource = dt;

	}

	protected void ShieldChartReg6_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "6");
		String regName = (String)reg.Rows[0][0];
		regid6.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetWeeklyRegIncome(regName);
			
		if(dt!=null)
			ShieldChartReg6.DataSource = dt;

	}
	
	protected void ShieldChart0_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		DataTable dt = new DataTable();

		giweeks.Text = graphWeeks;
		dt = DAL.GetIncomeByWeek("All", graphWeeks);
		
		if(dt!=null){
			ShieldChart0.DataSource = dt;
		} else {
			ShieldChart0.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartbyReg_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetIncomeBar("all");
		if(dt!=null)
			ShieldChartbyReg.DataSource = dt;

	}

	private class RegBar
	{
		public string full_name { get; set; }
		public string tot { get; set; }
		public string inv { get; set; }
		public string lastwk { get; set; }
	}
	
	protected void ShieldChartbyRegThisWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dataThisWeek = new DataTable();
		dataThisWeek = DAL.GetIncomeBar("thisweek");
		
		DataTable dataThisWeekInv = new DataTable();
		dataThisWeekInv = DAL.GetIncomeBar("thisweekinv");
		
		List<RegBar> datasource = new List<RegBar>();

		string regName = ""; 
		int thiswk = 0; 
		int thiswkinv = 0; 
			
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

		//ADD THE INVOICED DATA 

		for (int j = 0; j < dataThisWeekInv.Rows.Count; j++)
		{
			foreach(RegBar dd in datasource)
			{
				if(dataThisWeekInv.Rows[j].ItemArray[0].ToString()==dd.full_name){
					dd.inv = dataThisWeekInv.Rows[j].ItemArray[1].ToString();
				}
			}
		}

		if(datasource!=null)
			ShieldChartbyRegThisWeek.DataSource = datasource;

	}
	
	protected void ShieldChartbyRegLastWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetIncomeBar("lastweek");

		if(dt.Rows.Count > 0){
			ShieldChartbyRegLastWeek.DataSource = dt;
		} else {
			ShieldChartbyRegLastWeek.SecondaryHeader.Text = "(No Data Found)";
		}

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
