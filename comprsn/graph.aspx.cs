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
	
	public void StartingPage()
	{

		searchText = "";
		searchCol = "";
		searchWE = "";
		ErrorText.Visible = false;

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

		DateTime nextThursday = DAL.GetNextWeekday(DateTime.Now, DayOfWeek.Thursday);
		lblWeekending.Text = nextThursday.ToString("dd-MMM-yyyy");   
		
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
		ShieldChartLastWeek.DataBind();
        ShieldChartThisWeek.DataBind();
		ShieldChartPieAll.DataBind();		
		ShieldChart0.DataBind();		
		ShieldChartInStarted7R.DataBind();
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
		ShieldChartLastWeek.DataBind();
        ShieldChartThisWeek.DataBind();
		ShieldChartPieAll.DataBind();		
		ShieldChart0.DataBind();		
		ShieldChartInStarted7R.DataBind();
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
		ShieldChartLastWeek.DataBind();
        ShieldChartThisWeek.DataBind();
		ShieldChartPieAll.DataBind();		
		ShieldChart0.DataBind();		
		ShieldChartInStarted7R.DataBind();
        
    }

	private class DailyStat
	{
		public string dayofweek { get; set; }
		public string dailyStat { get; set; }
		public string cumulativeStat { get; set; }
	}

	private List<DailyStat> PrepareDailyStat(DataTable dt, string we)
	{

		List<DailyStat> datasource = new List<DailyStat>();

		string d = ""; 
		int thiswk = 0; 
		int thiswkcum = 0;
		
		DateTime vWeekend = DateTime.Parse(we);

		DailyStat thu = new DailyStat{ dayofweek = "Thu", dailyStat = "0"  };
		DailyStat fri = new DailyStat{ dayofweek = "Fri", dailyStat = "0"  };
		DailyStat sat = new DailyStat{ dayofweek = "Sat", dailyStat = "0"  };
		DailyStat sun = new DailyStat{ dayofweek = "Sun", dailyStat = "0"  };
		DailyStat mon = new DailyStat{ dayofweek = "Mon", dailyStat = "0"  };
		DailyStat tue = new DailyStat{ dayofweek = "Tue", dailyStat = "0"  };
		DailyStat wed = new DailyStat{ dayofweek = "Wed", dailyStat = "0"  };
		DailyStat thu2 = new DailyStat{dayofweek = "Thu2",dailyStat = "0"  };
		
		//LOOP THROUGH THE ROWS CHECKING EACH DAY 
		for (int j = 0; j < dt.Rows.Count; j++)
		{
			
			//HANDLE DAILY STATS
			if(DateTime.Parse(dt.Rows[j].ItemArray[2].ToString()) == vWeekend.AddDays(-7).Date){
				thu.dailyStat = dt.Rows[j].ItemArray[3].ToString();
			} else if(DateTime.Parse(dt.Rows[j].ItemArray[2].ToString()) == vWeekend.AddDays(-6).Date){
				fri.dailyStat = dt.Rows[j].ItemArray[3].ToString();
			} else if(DateTime.Parse(dt.Rows[j].ItemArray[2].ToString()) == vWeekend.AddDays(-5).Date){
				sat.dailyStat = dt.Rows[j].ItemArray[3].ToString();
			} else if(DateTime.Parse(dt.Rows[j].ItemArray[2].ToString()) == vWeekend.AddDays(-4).Date){
				sun.dailyStat = dt.Rows[j].ItemArray[3].ToString();
			} else if(DateTime.Parse(dt.Rows[j].ItemArray[2].ToString()) == vWeekend.AddDays(-3).Date){
				mon.dailyStat = dt.Rows[j].ItemArray[3].ToString();
			} else if(DateTime.Parse(dt.Rows[j].ItemArray[2].ToString()) == vWeekend.AddDays(-2).Date){
				tue.dailyStat = dt.Rows[j].ItemArray[3].ToString();
			} else if(DateTime.Parse(dt.Rows[j].ItemArray[2].ToString()) == vWeekend.AddDays(-1).Date){
				wed.dailyStat = dt.Rows[j].ItemArray[3].ToString();
			} else if(DateTime.Parse(dt.Rows[j].ItemArray[2].ToString()) == vWeekend.Date){
				thu2.dailyStat = dt.Rows[j].ItemArray[3].ToString();
			}
		};

		thu.cumulativeStat = thu.dailyStat;
		fri.cumulativeStat = AddTwoStrings(thu.cumulativeStat, fri.dailyStat);
		sat.cumulativeStat = AddTwoStrings(fri.cumulativeStat, sat.dailyStat);
		sun.cumulativeStat = AddTwoStrings(sat.cumulativeStat, sun.dailyStat);
		mon.cumulativeStat = AddTwoStrings(sun.cumulativeStat, mon.dailyStat);
		tue.cumulativeStat = AddTwoStrings(mon.cumulativeStat, tue.dailyStat);
		wed.cumulativeStat = AddTwoStrings(tue.cumulativeStat, wed.dailyStat);
		thu2.cumulativeStat = AddTwoStrings(wed.cumulativeStat, thu2.dailyStat);
		
		datasource.Add(thu);
		datasource.Add(fri);
		datasource.Add(sat);
		datasource.Add(sun);
		datasource.Add(mon);
		datasource.Add(tue);
		datasource.Add(wed);
		datasource.Add(thu2);
		
		return datasource;
	}

	private static string AddTwoStrings(string one, string two) 
	{
		int iOne = 0;
		int iTwo = 0;
		Int32.TryParse(one, out iOne);
		Int32.TryParse(two, out iTwo);
		return (iOne + iTwo).ToString();
	}	

	private DailyStat PrepareDailyGraph(DailyStat d)
	{
	
		DateTime now = DateTime.Now;
		DailyStat dd = d;
		string s = now.DayOfWeek.ToString();
		string today = s.Substring(0, 3);
		
		if(today=="Fri" && (dd.dayofweek =="Sat" || dd.dayofweek =="Sun" || dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.dailyStat = null;
			dd.cumulativeStat = null;
		}
		else if(today=="Sat" && (dd.dayofweek =="Sun" || dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.dailyStat = null;
			dd.cumulativeStat = null;
		}
		else if(today=="Sun" && (dd.dayofweek =="Mon" || dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.dailyStat = null;
			dd.cumulativeStat = null;
		}
		else if(today=="Mon" && (dd.dayofweek =="Tue" || dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.dailyStat = null;
			dd.cumulativeStat = null;
		}
		else if(today=="Tue" && (dd.dayofweek =="Wed" || dd.dayofweek =="Thu2")){
			dd.dailyStat = null;
			dd.cumulativeStat = null;
		}
		else if(today=="Wed" && (dd.dayofweek =="Thu2")){
			dd.dailyStat = null;
			dd.cumulativeStat = null;
		}

		return dd;
		
	}
		
	
	protected void ShieldChartLastWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetOrgCategoryPie(OrgText.Text, "lastweek", "Comp Resign", "Comp Resign");
		
		if(dt.Rows.Count>0){
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
		dt = DAL.GetOrgCategoryPie(OrgText.Text,"thisweek", "Comp Resign", "Comp Resign");
		
		if(dt.Rows.Count>0){
			ShieldChartThisWeek.DataSource = dt;
		} else {
			ShieldChartThisWeek.SecondaryHeader.Text = "(No Data Found)";
		}

	}

	protected void ShieldChartPieAll_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetOrgCategoryPie(OrgText.Text,"all", "Comp Resign", "Comp Resign");
		
		if(dt.Rows.Count>0){
			ShieldChartPieAll.DataSource = dt;
		} else {
			ShieldChartPieAll.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChart0_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		DataTable dt = new DataTable();
		giweeks.Text = graphWeeks;
		dt = DAL.GetOrgCategoryByWeek(OrgText.Text, "Comp Resign", "All", graphWeeks);
		
		if(dt.Rows.Count>0){
			ShieldChart0.DataSource = dt;
		} else {
			ShieldChart0.SecondaryHeader.Text = "(No Data Found)";
		}
	}
	
	protected void ShieldChartInStarted7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dataThisWeek = DAL.GetDailyCategory("thisweek", "Comp Resign", "Comp Resign", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisweek = PrepareDailyStat(dataThisWeek, lblWeekending.Text);

		DataTable dataThisWeekSched = DAL.GetDailyCategory("thisweeksched", "Comp Resign", "Comp Resign", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisWeekSched = PrepareDailyStat(dataThisWeekSched, lblWeekending.Text);
		
		DateTime lastWk = DateTime.Parse(lblWeekending.Text).AddDays(-7);
		DataTable dataLastWeek = DAL.GetDailyCategory("lastweek", "Comp Resign", "Comp Resign", lastWk.ToString("dd-MMM-yyyy"), OrgText.Text);
		List<DailyStat> lastweek = PrepareDailyStat(dataLastWeek, lastWk.ToString("dd-MMM-yyyy"));
		
		foreach(DailyStat dd in thisweek)
		{
			DailyStat d7 = new DailyStat();
			d7 = PrepareDailyGraph(dd);
			dd.dailyStat = d7.dailyStat;
			dd.cumulativeStat = d7.cumulativeStat;
		}

		foreach(DailyStat dd in thisWeekSched)
		{
			DailyStat d7 = new DailyStat();
			d7 = PrepareDailyGraph(dd);
			dd.dailyStat = d7.dailyStat;
			dd.cumulativeStat = d7.cumulativeStat;
		}

		List<DailyBIS7R> datasource = new List<DailyBIS7R>();

		string vDayofweek = "";

		string vThisweek = ""; 
		string vThisweekcum = "";

		string vThisWeekSched = "";
		string vThisWeekSchedcum = "";

		string vLastweek = "";
		string vLastweekcum = "";
		
		foreach(DailyStat a in thisweek)
		{
			vDayofweek = a.dayofweek;
			vThisweek = a.dailyStat;
			vThisweekcum = a.cumulativeStat;

			foreach(DailyStat aa in thisWeekSched)
			{
				if(a.dayofweek==aa.dayofweek){
					vThisWeekSched = aa.dailyStat;
					vThisWeekSchedcum = aa.cumulativeStat;
				}
			}
			foreach(DailyStat bb in lastweek)
			{
				if(a.dayofweek==bb.dayofweek){
					vLastweek = bb.dailyStat;
					vLastweekcum = bb.cumulativeStat;
				}
			}
	
			DailyBIS7R daily7R = new DailyBIS7R{		
			
				dayofweek = vDayofweek,
				thisweek = vThisweek,
				thisweekcum = vThisweekcum,
				thisweeksched = vThisWeekSched,
				thisweekschedcum = vThisWeekSchedcum,
				lastweek = vLastweek,
				lastweekcum = vLastweekcum
			};
			datasource.Add(daily7R);			
		}
		
		if(datasource.Count >0)
			ShieldChartInStarted7R.DataSource = datasource;		
		else
			ShieldChartInStarted7R.SecondaryHeader.Text = "(No Data Found)";

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
