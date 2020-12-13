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

		searchText = "";
		searchCol = "";
		searchWE = "";
				
	}

	protected void ShieldChartLastWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetCategoryPie("lastweek", "In and Started", "In and Started");
		
		if(dt!=null){
			ShieldChartLastWeek.DataSource = dt;
		} else {
			ShieldChartLastWeek.SecondaryHeader.Text = "(No Data Found)";
		}
	
	}

	protected void ShieldChartThisWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = "";
		toWE = "";

		DataTable dt = new DataTable();
		dt = DAL.GetCategoryPie("thisweek", "In and Started", "In and Started");
		
		if(dt!=null){
			ShieldChartThisWeek.DataSource = dt;
		} else {
			ShieldChartThisWeek.SecondaryHeader.Text = "(No Data Found)";
		}

	}

	protected void ShieldChartPieAll_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetCategoryPie("all", "In and Started", "In and Started");
		
		if(dt!=null){
			ShieldChartPieAll.DataSource = dt;
		} else {
			ShieldChartPieAll.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChart0_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		DataTable dt = new DataTable();
		giweeks.Text = graphWeeks;
		dt = DAL.GetCategoryByWeek("In and Started", "All", graphWeeks);
		
		if(dt.Rows.Count>0){
			ShieldChart0.DataSource = dt;
		} else {
			ShieldChart0.SecondaryHeader.Text = "(No Data Found)";
		}
	}
	
	protected void ShieldChartInStarted7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dataThisWeek = new DataTable();
		dataThisWeek = DAL.GetDailyInStarted("thisweek");

		DataTable thisWeekSched = new DataTable();
		thisWeekSched = DAL.GetDailyInStarted("thisweeksched");

		DataTable dataLastWeek = new DataTable();
		dataLastWeek = DAL.GetDailyInStarted("lastweek");

		string d = ""; 
		int thiswk = 0; 
		int thiswksched = 0; 
		int thiswkschedcum = 0; 
		int lastwk = 0;
		int thiswkcum = 0;
		int lastwkcum = 0;
		
		List<DailyBIS7R> datasource = new List<DailyBIS7R>();
			
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

			DailyBIS7R dailyBIS7R = new DailyBIS7R{			

				dayofweek = d,
				thisweek = thiswk.ToString(),
				thisweekcum = thiswkcum.ToString()
			};
			
			datasource.Add(dailyBIS7R);			
		};

		
//SCHEDULED

		for (int j = 0; j < thisWeekSched.Rows.Count; j++)
		{
			
			foreach(DailyBIS7R dd in datasource)
			{
				if(thisWeekSched.Rows[j].ItemArray[0].ToString()==dd.dayofweek){
					dd.thisweeksched = thisWeekSched.Rows[j].ItemArray[2].ToString();
					Int32.TryParse(thisWeekSched.Rows[j].ItemArray[2].ToString(), out thiswksched);	
					thiswkschedcum += thiswksched;
					dd.thisweekschedcum = thiswkschedcum.ToString();
				}
				
				
			}
		}

		foreach(DailyBIS7R dd in datasource)
		{
			DailyBIS7R d7 = new DailyBIS7R();
			d7 = PrepareDailyBIS7R(dd);
			
			dd.thisweek = d7.thisweek;
			dd.thisweekcum = d7.thisweekcum;
		}

// LAST WEEK		
		
		for (int j = 0; j < dataLastWeek.Rows.Count; j++)
		{
			
			foreach(DailyBIS7R dd in datasource)
			{
				if(dataLastWeek.Rows[j].ItemArray[0].ToString()==dd.dayofweek){
					dd.lastweek = dataLastWeek.Rows[j].ItemArray[2].ToString();
					Int32.TryParse(dataLastWeek.Rows[j].ItemArray[2].ToString(), out lastwk);	
					lastwkcum += lastwk;
					dd.lastweekcum = lastwkcum.ToString();
				}
				
				
			}
		}

		foreach(DailyBIS7R dd in datasource)
		{
			DailyBIS7R d7 = new DailyBIS7R();
			d7 = PrepareDailyBIS7R(dd);
			
			dd.thisweek = d7.thisweek;
			dd.thisweekcum = d7.thisweekcum;
		}
		
		ShieldChartInStarted7R.DataSource = datasource;		

	}

	private DailyBIS7R PrepareDailyBIS7R(DailyBIS7R d)
	{
		
		DailyBIS7R dd = d;
		
		DateTime now = DateTime.Now;
		string s = now.DayOfWeek.ToString();
		string today = s.Substring(0, 3);
		
		if(today=="Fri" && (dd.dayofweek =="Sat" || dd.dayofweek =="Sun" || dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweek = null;
			dd.thisweekcum = null;
		}
		else if(today=="Sat" && (dd.dayofweek =="Sun" || dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweek = null;
			dd.thisweekcum = null;
		}
		else if(today=="Sun" && (dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweek = null;
			dd.thisweekcum = null;
		}
		else if(today=="Mon" && (dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweek = null;
			dd.thisweekcum = null;
		}
		else if(today=="Tue" && (dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.thisweek = null;
			dd.thisweekcum = null;
		}
		else if(today=="Wed" && (dd.dayofweek =="Thu2")){
			dd.thisweek = null;
			dd.thisweekcum = null;
		}
		return dd;
	}
  
	private class DailyBIS7R
	{
		public string dayofweek { get; set; }
		public string thisweek { get; set; }
		public string thisweeksched { get; set; }
		public string lastweek { get; set; }
		public string thisweekcum { get; set; }
		public string thisweekschedcum { get; set; }
		public string lastweekcum { get; set; }
	}

	
	public void clickTest(Object sender, EventArgs e){

		string dateStr = "";
		DateTime today = DateTime.Now;             	// Use current time.
		string format = "dd-MMM-yyyy";   			// Use this format.
		dateStr = today.ToString(format); 	// Write to console.
		ErrorText.Text = dateStr;
		
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
