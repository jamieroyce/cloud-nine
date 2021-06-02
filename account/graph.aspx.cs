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
			OrgText.Text = "Combined";
			day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
			fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
			cmb.Attributes["class"] += " active";		
		}
	
		searchText = "";
		searchCol = "";
		searchWE = "";

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
		ShieldChart7R.DataBind();
        ShieldChart0.DataBind();
		ShieldChart2.DataBind();		
		ShieldChart3.DataBind();		
		ShieldChart4.DataBind();		
		ShieldChart5.DataBind();		
		ShieldChart6.DataBind();		
		ShieldChartThisWeek.DataBind();
		ShieldChartPieAll.DataBind();
		ShieldChartLastWeek.DataBind();
		
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
		ShieldChart7R.DataBind();
        ShieldChart0.DataBind();
		ShieldChart2.DataBind();		
		ShieldChart3.DataBind();		
		ShieldChart4.DataBind();		
		ShieldChart5.DataBind();		
		ShieldChart6.DataBind();		
		ShieldChartThisWeek.DataBind();
		ShieldChartPieAll.DataBind();
		ShieldChartLastWeek.DataBind();
        
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
		ShieldChart7R.DataBind();
        ShieldChart0.DataBind();
		ShieldChart2.DataBind();		
		ShieldChart3.DataBind();		
		ShieldChart4.DataBind();		
		ShieldChart5.DataBind();		
		ShieldChart6.DataBind();		
		ShieldChartThisWeek.DataBind();
		ShieldChartPieAll.DataBind();
		ShieldChartLastWeek.DataBind();
        
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

	public void clickTest(Object sender, EventArgs e){

		
	}

	protected void ShieldChartThisWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = "";
		toWE = "";

		DataTable dt = new DataTable();
		dt = DAL.GetOrgProcurementPie(OrgText.Text, "thisweek", fromWE, toWE);
		
		if(dt.Rows.Count>0){
			ShieldChartThisWeek.DataSource = dt;
		} else {
			ShieldChartThisWeek.SecondaryHeader.Text = "(No Data Found)";
		}

	}

	protected void ShieldChartLastWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = "";
		toWE = "";

		DataTable dt = new DataTable();
		dt = DAL.GetOrgProcurementPie(OrgText.Text, "lastweek", fromWE, toWE);
		
		if(dt.Rows.Count>0){
			ShieldChartLastWeek.DataSource = dt;
		} else {
			ShieldChartLastWeek.SecondaryHeader.Text = "(No Data Found)";
		}

	}

	protected void ShieldChartPieAll_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = "";
		toWE = "";

		DataTable dt = new DataTable();
		dt = DAL.GetOrgProcurementPie(OrgText.Text, "all", fromWE, toWE);
		
		if(dt.Rows.Count>0){
			ShieldChartPieAll.DataSource = dt;
		} else {
			ShieldChartPieAll.SecondaryHeader.Text = "(No Data Found)";
		}

	}
	
	protected void ShieldChart7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dataThisWeek = DAL.GetDailyGI("thisweek", OrgText.Text, lblWeekending.Text);
		List<DailyStat> thisweek = PrepareDailyStat(dataThisWeek, lblWeekending.Text);

		DataTable dataThisWeekInv = DAL.GetDailyGI("thisweekinv", OrgText.Text, lblWeekending.Text);
		List<DailyStat> thisweekinv = PrepareDailyStat(dataThisWeekInv, lblWeekending.Text);
		
		DateTime lastWeek = DateTime.Parse(lblWeekending.Text).AddDays(-7);
		DataTable dataLastWeek = DAL.GetDailyGI("lastweek", OrgText.Text, lastWeek.ToString("dd-MMM-yyyy"));
		List<DailyStat> lastweek = PrepareDailyStat(dataLastWeek, lastWeek.ToString("dd-MMM-yyyy"));

		// CardInConfirmed.Text = String.Format("{0:C0} ", int.Parse(dataThisWeek.Compute("Sum(amt)", "").ToString()));		
		// CardInvoiced.Text = String.Format("{0:C0} ", int.Parse(dataThisWeekInv.Compute("Sum(amt)", "").ToString()));
		// LastWeekGI.Text = String.Format("{0:C0} ", int.Parse(dataLastWeek.Compute("Sum(amt)", "").ToString()));	
		
		foreach(DailyStat dd in thisweek)
		{
			DailyStat d7 = new DailyStat();
			d7 = PrepareDailyGraph(dd);
			dd.dailyStat = d7.dailyStat;
			dd.cumulativeStat = d7.cumulativeStat;
		}

		foreach(DailyStat dd in thisweekinv)
		{
			DailyStat d7 = new DailyStat();
			d7 = PrepareDailyGraph(dd);
			dd.dailyStat = d7.dailyStat;
			dd.cumulativeStat = d7.cumulativeStat;
		}

		List<Daily7R> datasource = new List<Daily7R>();

		string vDayofweek = "";

		string vThisweekgi = ""; 
		string vThisweekcum = "";

		string vThisweekinv = "";
		string vThisweekinvcum = "";

		string vLastweekgi = "";
		string vLastweekcum = "";
		
		foreach(DailyStat a in thisweek)
		{
			vDayofweek = a.dayofweek;
			vThisweekgi = a.dailyStat;
			vThisweekcum = a.cumulativeStat;

			foreach(DailyStat aa in thisweekinv)
			{
				if(a.dayofweek==aa.dayofweek){
					vThisweekinv = aa.dailyStat;
					vThisweekinvcum = aa.cumulativeStat;
				}
			}
			foreach(DailyStat bb in lastweek)
			{
				if(a.dayofweek==bb.dayofweek){
					vLastweekgi = bb.dailyStat;
					vLastweekcum = bb.cumulativeStat;
				}
			}
			
			Daily7R daily7R = new Daily7R{			
				dayofweek = vDayofweek,
				thisweekgi = vThisweekgi,
				thisweekcum = vThisweekcum,
				thisweekinv = vThisweekinv,
				thisweekinvcum = vThisweekinvcum,
				lastweekgi = vLastweekgi,
				lastweekcum = vLastweekcum
			};
			datasource.Add(daily7R);			
		}

		if(datasource.Count >0){
			ShieldChart7R.DataSource = datasource;
		} else {
			ShieldChart7R.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChart2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dt = new DataTable();
		dt = DAL.GetOrgIncomeByWeek(OrgText.Text, "Arrival", graphWeeks);
		
		if(dt.Rows.Count>0){
			ShieldChart2.DataSource = dt;
		} else {
			ShieldChart2.SecondaryHeader.Text = "(No Data Found)";
		}

	}
	
	protected void ShieldChart3_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dt = new DataTable();
		dt = DAL.GetOrgIncomeByWeek(OrgText.Text, "CF", graphWeeks);
		
		if(dt.Rows.Count>0){
			ShieldChart3.DataSource = dt;
		} else {
			ShieldChart3.SecondaryHeader.Text = "(No Data Found)";
		}

	}

	protected void ShieldChart4_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dt = new DataTable();
		dt = DAL.GetOrgIncomeByWeek(OrgText.Text, "FSM", graphWeeks);
		
		if(dt.Rows.Count>0){
			ShieldChart4.DataSource = dt;
		} else {
			ShieldChart4.SecondaryHeader.Text = "(No Data Found)";
		}

	}	
	
	protected void ShieldChart5_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dt = new DataTable();
		dt = DAL.GetOrgIncomeByWeek(OrgText.Text, "Prospecting", graphWeeks);
		
		if(dt.Rows.Count>0){
			ShieldChart5.DataSource = dt;
		} else {
			ShieldChart5.SecondaryHeader.Text = "(No Data Found)";
		}

	}	

	protected void ShieldChart6_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dt = new DataTable();
		dt = DAL.GetOrgIncomeByWeek(OrgText.Text, "Resign", graphWeeks);
		
		if(dt.Rows.Count>0){
			ShieldChart6.DataSource = dt;
		} else {
			ShieldChart6.SecondaryHeader.Text = "(No Data Found)";
		}

	}	

	private class DailyStat
	{
		public string dayofweek { get; set; }
		public string dailyStat { get; set; }
		public string cumulativeStat { get; set; }
	}

	private List<DailyStat> PrepareDailyStat(DataTable dt, string we){

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

	private DailyStat PrepareDailyGraph(DailyStat d){
	
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
	protected void ShieldChart0_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		DataTable dt = new DataTable();
		giweeks.Text = graphWeeks;
		// dt = DAL.GetIncomeByWeek("All", graphWeeks);
		dt = DAL.GetOrgIncomeByWeek(OrgText.Text, "All", graphWeeks);
		
		if(dt.Rows.Count>0){
			ShieldChart0.DataSource = dt;
		} else {
			ShieldChart0.SecondaryHeader.Text = "(No Data Found)";
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
