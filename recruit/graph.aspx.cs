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
		ShieldChart0.DataBind();		
		ShieldChart7R.DataBind();
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
		ShieldChart0.DataBind();		
		ShieldChart7R.DataBind();
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
		ShieldChart0.DataBind();		
		ShieldChart7R.DataBind();
        
    }
		
	protected void ShieldChart0_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		DataTable arrivals = new DataTable();
		arrivals = DAL.GetOrgPHSByWeek(OrgText.Text, graphWeeks);

		DataTable signups = new DataTable();
		signups = DAL.GetOrgSignedByWeek(OrgText.Text, graphWeeks);

		giweeks.Text = graphWeeks;

		List<WeeklyRecruit> datasource = new List<WeeklyRecruit>();

		string d = ""; 
		string vphs = ""; 
		string vsigned = ""; 
		
		for (int j = 0; j < arrivals.Rows.Count; j++)
		{
			WeeklyRecruit weeklyRecruit = new WeeklyRecruit{			
				weekend = arrivals.Rows[j].ItemArray[0].ToString(),
				phs = arrivals.Rows[j].ItemArray[1].ToString()
			};
			datasource.Add(weeklyRecruit);			
		};

		// LOOP THROUGH SIGNED
		for (int j = 0; j < signups.Rows.Count; j++)
		{
			foreach(WeeklyRecruit dd in datasource)
			{
				if(signups.Rows[j].ItemArray[0].ToString()==dd.weekend){
					dd.signup = signups.Rows[j].ItemArray[1].ToString();
				}
			}
		}
		
		if(datasource!=null){
			ShieldChart0.DataSource = datasource;
		} else {
			ShieldChart0.SecondaryHeader.Text = "(No Data Found)";
		}
	}
	
	protected void ShieldChart7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dataThisWeek = new DataTable();
		dataThisWeek = DAL.GetOrgDailyCategory("thisweek", "Recruit", "Arrived", OrgText.Text);

		DataTable thisWeekSched = new DataTable();
		thisWeekSched = DAL.GetOrgDailyCategory("thisweeksched", "Recruit", "Arrived", OrgText.Text);

		DataTable dataLastWeek = new DataTable();
		dataLastWeek = DAL.GetOrgDailyCategory("lastweek", "Recruit", "Arrived", OrgText.Text);

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
		
/*SCHEDULED*/

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

/*LAST WEEK		*/
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
		
		if(datasource.Count >0){
			ShieldChart7R.DataSource = datasource;		
		} else {
			ShieldChart7R.SecondaryHeader.Text = "(No Data Found)";
		}
		
	}

	private class WeeklyRecruit
	{
		public string weekend { get; set; }
		public string phs { get; set; }
		public string signup { get; set; }
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

		ErrorText.Text += "ENTERING DEBUG MODE: <BR />";
	
		DataTable dataThisWeek = new DataTable();
		dataThisWeek = DAL.GetOrgDailyCategory("thisweek", "Recruit", "Arrived", OrgText.Text);

		DataTable thisWeekSched = new DataTable();
		thisWeekSched = DAL.GetOrgDailyCategory("thisweeksched", "Recruit", "Arrived", OrgText.Text);

		DataTable dataLastWeek = new DataTable();
		dataLastWeek = DAL.GetOrgDailyCategory("lastweek", "Recruit", "Arrived", OrgText.Text);
		
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
			ErrorText.Text += dailyBIS7R.dayofweek + " | thisweek = " + dailyBIS7R.thisweek + " <BR />";
			
			datasource.Add(dailyBIS7R);			
		};
		
/*SCHEDULED*/

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

/*LAST WEEK		*/
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
