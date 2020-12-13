using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;

public partial class _Default : System.Web.UI.Page 
{  	
    dal DAL = new dal();
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
			Title = "Public Tracking";
			if (Session["org"] == "Day") {
				OrgText.Text = Session["org"].ToString();
				OrgText2.Text = Session["org"].ToString();
				OrgText3.Text = Session["org"].ToString();
				OrgText4.Text = Session["org"].ToString();
				OrgText5.Text = Session["org"].ToString();
				OrgText6.Text = Session["org"].ToString();
				OrgText7.Text = Session["org"].ToString();
				
				day.Attributes["class"] += " active";		
				fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
				cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		   
			} else if(Session["org"] == "Fdn"){
				OrgText.Text = Session["org"].ToString();
				OrgText2.Text = Session["org"].ToString();
				OrgText3.Text = Session["org"].ToString();
				OrgText4.Text = Session["org"].ToString();
				OrgText5.Text = Session["org"].ToString();
				OrgText6.Text = Session["org"].ToString();
				OrgText7.Text = Session["org"].ToString();
				
				day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
				fdn.Attributes["class"] += " active";		
				cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		      
			} else {
				OrgText.Text = "Combined";
				OrgText2.Text = "Combined";
				OrgText3.Text = "Combined";
				OrgText4.Text = "Combined";
				OrgText5.Text = "Combined";
				OrgText6.Text = "Combined";
				OrgText7.Text = "Combined";
				
				day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
				fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
				cmb.Attributes["class"] += " active";		
			}
			
			DateTime nextThursday = DAL.GetNextWeekday(DateTime.Now, DayOfWeek.Thursday);
			lblWeekending.Text = nextThursday.ToString("dd-MMM-yyyy");   
			// ErrorText.Text = "DEBUG MODE</BR>";

		}			
	}
	catch{}
	}

	public void Day_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Day";
		OrgText2.Text = "Day";
		OrgText3.Text = "Day";
		OrgText4.Text = "Day";
		OrgText5.Text = "Day";
		OrgText6.Text = "Day";
		OrgText7.Text = "Day";
		
		Session["org"] = OrgText.Text;
		ShieldChart7R.DataBind();
		ShieldChartBIS7R.DataBind();       
		ShieldChartInStarted7R.DataBind();
		ShieldChartCompResign7R.DataBind();
		ShieldChartRecruit7R.DataBind();	
		ShieldChartFSS7R.DataBind();	
		ShieldChartThisWeek.DataBind();	
		
		day.Attributes["class"] += " active";		
		fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
		cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		      
		
    }

	public void Fdn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Fdn";
		OrgText2.Text = "Fdn";
		OrgText3.Text = "Fdn";
		OrgText4.Text = "Fdn";
		OrgText5.Text = "Fdn";
		OrgText6.Text = "Fdn";
		OrgText7.Text = "Fdn";
		
		Session["org"] = OrgText.Text;
		ShieldChart7R.DataBind();
		ShieldChartBIS7R.DataBind();        
		ShieldChartInStarted7R.DataBind();
		ShieldChartCompResign7R.DataBind();
		ShieldChartRecruit7R.DataBind();		
		ShieldChartFSS7R.DataBind();		
		ShieldChartThisWeek.DataBind();	

		day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
		fdn.Attributes["class"] += " active";		
		cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		      
    }

	public void Combined_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Combined";
		OrgText2.Text = "Combined";
		OrgText3.Text = "Combined";
		OrgText4.Text = "Combined";
		OrgText5.Text = "Combined";
		OrgText6.Text = "Combined";
		OrgText7.Text = "Combined";

		Session["org"] = OrgText.Text;
		ShieldChart7R.DataBind();
		ShieldChartBIS7R.DataBind();        
		ShieldChartInStarted7R.DataBind();
		ShieldChartCompResign7R.DataBind();
		ShieldChartRecruit7R.DataBind();		
		ShieldChartFSS7R.DataBind();		
		ShieldChartThisWeek.DataBind();	

		day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
		fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
		cmb.Attributes["class"] += " active";		
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

	private Daily7R PrepareDaily7R(Daily7R d)
	{
		
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
		if(dt!=null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				// ErrorText.Text += "dt.Rows[j].ItemArray[0]=" + dt.Rows[j].ItemArray[0].ToString() + "</BR>";
				// ErrorText.Text += "dt.Rows[j].ItemArray[1]=" + dt.Rows[j].ItemArray[1].ToString() + "</BR>";
				// ErrorText.Text += "dt.Rows[j].ItemArray[2]=" + dt.Rows[j].ItemArray[2].ToString() + "</BR>";
				// ErrorText.Text += "DateTime.Parse(dt.Rows[j].ItemArray[2].ToString() =" + DateTime.Parse(dt.Rows[j].ItemArray[2].ToString()) + "</BR>";
				// ErrorText.Text += "vWeekend.AddDays(-7).Date=" + vWeekend.AddDays(-7).Date.ToString() + "</BR>";
				
				try{
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
				} 
				catch(Exception e) {
					ErrorText.Text = "Caught Exception: " + e;				
				}
			}
		}
		
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
		
	protected void ShieldChart7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dataThisWeek = DAL.GetDailyGI("thisweek", OrgText.Text, lblWeekending.Text);
		List<DailyStat> thisweek = PrepareDailyStat(dataThisWeek, lblWeekending.Text);
		
		DataTable dataThisWeekInv = DAL.GetDailyGI("thisweekinv", OrgText.Text, lblWeekending.Text);
		List<DailyStat> thisweekinv = PrepareDailyStat(dataThisWeekInv, lblWeekending.Text);
		
		DateTime lastWeek = DateTime.Parse(lblWeekending.Text).AddDays(-7);
		DataTable dataLastWeek = DAL.GetDailyGI("lastweek", OrgText.Text, lastWeek.ToString("dd-MMM-yyyy"));
		
		List<DailyStat> lastweek = PrepareDailyStat(dataLastWeek, lastWeek.ToString("dd-MMM-yyyy"));

		try{
			if(dataThisWeek.Rows.Count>0){
				CardInConfirmed.Text = String.Format("{0:C0}", int.Parse(dataThisWeek.Compute("Sum(amt)", "").ToString()));		
			} else {
				CardInConfirmed.Text = String.Format("{0:C0}", int.Parse("0"));
			}
			
			if(dataThisWeekInv.Rows.Count>0){
				CardInvoiced.Text = String.Format("{0:C0}", int.Parse(dataThisWeekInv.Compute("Sum(amt)", "").ToString()));
			} else {
				CardInvoiced.Text = String.Format("{0:C0}", int.Parse("0"));		
			}
			
			if(dataLastWeek.Rows.Count> 0){
				LastWeekGI.Text = String.Format("{0:C0}", int.Parse(dataLastWeek.Compute("Sum(amt)", "").ToString()));	
			} else {
				LastWeekGI.Text = String.Format("{0:C0}", int.Parse("0"));		
			}
		} catch(Exception ex) {
			ErrorText.Text = "ERROR: " + ex.ToString();
		}
		
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

	protected void ShieldChartBIS7R2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dataThisWeek = new DataTable();
		dataThisWeek = DAL.GetOrgDailyBIS("thisweek", OrgText.Text);

		DataTable thisWeekSched = new DataTable();
		thisWeekSched = DAL.GetOrgDailyBIS("thisweeksched", OrgText.Text);

		DataTable dataLastWeek = new DataTable();
		dataLastWeek = DAL.GetOrgDailyBIS("lastweek", OrgText.Text);

		string d = ""; 
		int thiswk = 0; 
		int thiswksched = 0; 
		int thiswkschedcum = 0; 
		int lastwk = 0;
		int thiswkcum = 0;
		int lastwkcum = 0;
		
		List<DailyBIS7R> datasource = new List<DailyBIS7R>();

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

				DailyBIS7R dailyBIS7R = new DailyBIS7R{			

					dayofweek = d,
					thisweek = thiswk.ToString(),
					thisweekcum = thiswkcum.ToString()
				};
				
				datasource.Add(dailyBIS7R);			
			};
		}	
//SCHEDULED

		if(thisWeekSched!=null){

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
		}
		
		foreach(DailyBIS7R dd in datasource)
		{
			DailyBIS7R d7 = new DailyBIS7R();
			d7 = PrepareDailyBIS7R(dd);
			
			dd.thisweek = d7.thisweek;
			dd.thisweekcum = d7.thisweekcum;
		}

// LAST WEEK		

		if(dataLastWeek!=null){
			
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

		}
		foreach(DailyBIS7R dd in datasource)
		{
			DailyBIS7R d7 = new DailyBIS7R();
			d7 = PrepareDailyBIS7R(dd);
			
			dd.thisweek = d7.thisweek;
			dd.thisweekcum = d7.thisweekcum;
		}

		//GET ACCURATE COUNT OF THIS WEEK AND LAST WEEK BIS: 

		var cmdBIS 			= "";
		var cmdLastWkBIS 	= "";
		
		if(OrgText.Text=="Combined"){
			cmdBIS 			= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and reg_cat_id <> 'Archive'";
			cmdLastWkBIS 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and reg_cat_id = 'Archive' and weekend > getdate() - 8";
		} else if(OrgText.Text=="Day"){
			cmdBIS 			= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and org = 'Day' and reg_cat_id <> 'Archive'";
			cmdLastWkBIS 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and org = 'Day' and reg_cat_id = 'Archive' and weekend > getdate() - 8";
		} else {
			cmdBIS 			= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and org = 'Fdn' and reg_cat_id <> 'Archive'";
			cmdLastWkBIS 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and org = 'Fdn' and reg_cat_id = 'Archive' and weekend > getdate() - 8";
		}

		string thiswkBIS = "0";
		string lastwkBIS = "0";
		
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
				conn.Open();
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdBIS, conn))					
					{
						thiswkBIS = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdLastWkBIS, conn))					
					{
						lastwkBIS = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				conn.Close();
			}			
		
		ThisWeek.Text = thiswkBIS;
		ThisWeekSched.Text = String.Format("{0} ", thiswkschedcum);
		LastWeekBIS.Text = lastwkBIS;

		if(datasource.Count >0){
			ShieldChartBIS7R.DataSource = datasource;
		} else {
			ShieldChartBIS7R.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartBIS7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dataThisWeek = DAL.GetDailyCategoryDistinct("thisweek", "LineUp", "In The Shop", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisweek = PrepareDailyStat(dataThisWeek, lblWeekending.Text);

		DataTable dataThisWeekSched = DAL.GetDailyCategoryDistinct("thisweeksched", "LineUp", "In The Shop", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisWeekSched = PrepareDailyStat(dataThisWeekSched, lblWeekending.Text);
		
		DateTime lastWk = DateTime.Parse(lblWeekending.Text).AddDays(-7);
		DataTable dataLastWeek = DAL.GetDailyCategoryDistinct("lastweek", "LineUp", "In The Shop", lastWk.ToString("dd-MMM-yyyy"), OrgText.Text);
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
		
		// GET ACCURATE COUNT OF THIS WEEK AND LAST WEEK BIS: 

		var cmdBIS 			= "";
		var cmdScheduled	= "";
		var cmdLastWkBIS 	= "";
		
		if(OrgText.Text=="Combined"){
			cmdBIS 			= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and reg_cat_id <> 'Archive'";
			cmdScheduled	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'Scheduled' and reg_cat_id <> 'In The Shop'";
			cmdLastWkBIS 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and reg_cat_id = 'Archive' and weekend > getdate() - 8";
		} else if(OrgText.Text=="Day"){
			cmdBIS 			= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and org = 'Day' and reg_cat_id <> 'Archive'";
			cmdScheduled	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'Scheduled' and org = 'Day' and reg_cat_id <> 'In The Shop'";
			cmdLastWkBIS 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and org = 'Day' and reg_cat_id = 'Archive' and weekend > getdate() - 8";
		} else {
			cmdBIS 			= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and org = 'Fdn' and reg_cat_id <> 'Archive'";
			cmdScheduled	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'Scheduled' and org = 'Fdn' and reg_cat_id <> 'In The Shop'";
			cmdLastWkBIS 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and org = 'Fdn' and reg_cat_id = 'Archive' and weekend > getdate() - 8";
		}

		string thiswkBIS = "0";
		string thiswkScheduled = "0";
		string lastwkBIS = "0";

		using (SqlConnection conn = databaseConnection.CreateSqlConnection())
		{				
			conn.Open();
			try{	
				using (SqlCommand Cmd = new SqlCommand(cmdBIS, conn))					
				{
					thiswkBIS = String.Format("{0:D}", Cmd.ExecuteScalar());
				}
			}
			catch {}
			try{	
				using (SqlCommand Cmd = new SqlCommand(cmdScheduled, conn))					
				{
					thiswkScheduled = String.Format("{0:D}", Cmd.ExecuteScalar());
				}
			}
			catch {}
			try{	
				using (SqlCommand Cmd = new SqlCommand(cmdLastWkBIS, conn))					
				{
					lastwkBIS = String.Format("{0:D}", Cmd.ExecuteScalar());
				}
			}
			catch {}
			conn.Close();
		}			
	
		ThisWeek.Text = thiswkBIS;
		ThisWeekSched.Text = thiswkScheduled;
		LastWeekBIS.Text = lastwkBIS;		
		
		if(datasource.Count >0)
			ShieldChartBIS7R.DataSource = datasource;		
		else
			ShieldChartBIS7R.SecondaryHeader.Text = "(No Data Found)";
	}
	
	protected void ShieldChartInStarted7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dataThisWeek = DAL.GetDailyCategoryDistinct("thisweek", "In And Started", "In And Started", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisweek = PrepareDailyStat(dataThisWeek, lblWeekending.Text);

		DataTable dataThisWeekSched = DAL.GetDailyCategoryDistinct("thisweeksched", "In And Started", "In And Started", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisWeekSched = PrepareDailyStat(dataThisWeekSched, lblWeekending.Text);
		
		DateTime lastWk = DateTime.Parse(lblWeekending.Text).AddDays(-7);
		DataTable dataLastWeek = DAL.GetDailyCategoryDistinct("lastweek", "In And Started", "In And Started", lastWk.ToString("dd-MMM-yyyy"), OrgText.Text);
		List<DailyStat> lastweek = PrepareDailyStat(dataLastWeek, lastWk.ToString("dd-MMM-yyyy"));

/*		if(dataThisWeek.Rows.Count>0){
			int sum = dataThisWeek.AsEnumerable().Sum(row => row.Field<int>("tot"));
			ThisWeekInStarted.Text = sum.ToString();
		} else {
			ThisWeekInStarted.Text = String.Format("{0}", int.Parse("0"));
		}
		if(dataThisWeekSched.Rows.Count>0){
			int sum = dataThisWeekSched.AsEnumerable().Sum(row => row.Field<int>("tot"));
			ThisWeekSchedInStarted.Text = sum.ToString();
		} else {
			ThisWeekSchedInStarted.Text = String.Format("{0}", int.Parse("0"));		
		}

		if(dataLastWeek.Rows.Count>0){
			int sum = dataLastWeek.AsEnumerable().Sum(row => row.Field<int>("tot"));
			LastWeekInStarted.Text = sum.ToString();
		} else {
			LastWeekInStarted.Text = String.Format("{0}", int.Parse("0"));		
		}
*/		
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

	protected void ShieldChartCompResign7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		DataTable dataThisWeek = DAL.GetDailyCategory("thisweek", "Comp Resign", "Comp Resign", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisweek = PrepareDailyStat(dataThisWeek, lblWeekending.Text);

		DataTable dataThisWeekSched = DAL.GetDailyCategory("thisweeksched", "Comp Resign", "Comp Resign", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisWeekSched = PrepareDailyStat(dataThisWeekSched, lblWeekending.Text);
		
		DateTime lastWk = DateTime.Parse(lblWeekending.Text).AddDays(-7);
		DataTable dataLastWeek = DAL.GetDailyCategory("lastweek", "Comp Resign", "Comp Resign", lastWk.ToString("dd-MMM-yyyy"), OrgText.Text);
		List<DailyStat> lastweek = PrepareDailyStat(dataLastWeek, lastWk.ToString("dd-MMM-yyyy"));
/*		
		if(dataThisWeek.Rows.Count>0){
			int sum = dataThisWeek.AsEnumerable().Sum(row => row.Field<int>("tot"));
			ThisWeekCompResign.Text = sum.ToString();
		} else {
			ThisWeekCompResign.Text = String.Format("{0}", int.Parse("0"));
		}
		if(dataThisWeekSched.Rows.Count>0){
			int sum = dataThisWeekSched.AsEnumerable().Sum(row => row.Field<int>("tot"));
			ThisWeekSchedCompResign.Text = sum.ToString();
		} else {
			ThisWeekSchedCompResign.Text = String.Format("{0}", int.Parse("0"));		
		}

		if(dataLastWeek.Rows.Count>0){
			int sum = dataLastWeek.AsEnumerable().Sum(row => row.Field<int>("tot"));
			LastWeekCompResign.Text = sum.ToString();
		} else {
			LastWeekCompResign.Text = String.Format("{0}", int.Parse("0"));		
		}
		
*/		foreach(DailyStat dd in thisweek)
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
			ShieldChartCompResign7R.DataSource = datasource;		
		else
			ShieldChartCompResign7R.SecondaryHeader.Text = "(No Data Found)";

	}

	protected void ShieldChartRecruit7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
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
				DailyBIS7R dailyBIS7R = new DailyBIS7R{			
					dayofweek = d,
					thisweek = thiswk.ToString(),
					thisweekcum = thiswkcum.ToString()
				};
				datasource.Add(dailyBIS7R);			
			};
		}
		
/*SCHEDULED*/

		if(thisWeekSched!=null){

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
		}
		
		foreach(DailyBIS7R dd in datasource)
		{
			DailyBIS7R d7 = new DailyBIS7R();
			d7 = PrepareDailyBIS7R(dd);
			dd.thisweek = d7.thisweek;
			dd.thisweekcum = d7.thisweekcum;
		}

/*LAST WEEK		*/

		if(dataLastWeek!=null){

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
		}
		
		foreach(DailyBIS7R dd in datasource)
		{
			DailyBIS7R d7 = new DailyBIS7R();
			d7 = PrepareDailyBIS7R(dd);
			dd.thisweek = d7.thisweek;
			dd.thisweekcum = d7.thisweekcum;
		}

		ThisWeekPHS.Text = String.Format("{0} ", thiswkcum);
		ThisWeekSignups.Text = String.Format("{0} ", thiswkschedcum);
		LastWeekPHS.Text = String.Format("{0} ", lastwkcum);
		
		if(datasource.Count >0)
			ShieldChartRecruit7R.DataSource = datasource;		
		else
			ShieldChartRecruit7R.SecondaryHeader.Text = "(No Data Found)";

	}

	protected void ShieldChartFSS7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable dataThisWeek = DAL.GetDailyCategory("thisweek", "First Service", "First Service", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisweek = PrepareDailyStat(dataThisWeek, lblWeekending.Text);

		DataTable dataThisWeekSched = DAL.GetDailyCategory("thisweeksched", "First Service", "First Service", lblWeekending.Text, OrgText.Text);
		List<DailyStat> thisWeekSched = PrepareDailyStat(dataThisWeekSched, lblWeekending.Text);
		
		DateTime lastWk = DateTime.Parse(lblWeekending.Text).AddDays(-7);
		DataTable dataLastWeek = DAL.GetDailyCategory("lastweek", "First Service", "First Service", lastWk.ToString("dd-MMM-yyyy"), OrgText.Text);
		List<DailyStat> lastweek = PrepareDailyStat(dataLastWeek, lastWk.ToString("dd-MMM-yyyy"));
		
/*		if(dataThisWeek.Rows.Count>0){
			int sum = dataThisWeek.AsEnumerable().Sum(row => row.Field<int>("tot"));
			ThisWeekFSS.Text = sum.ToString();
		} else {
			ThisWeekFSS.Text = String.Format("{0}", int.Parse("0"));
		}
		if(dataThisWeekSched.Rows.Count>0){
			int sum = dataThisWeekSched.AsEnumerable().Sum(row => row.Field<int>("tot"));
			ThisWeekScheduled.Text = sum.ToString();
		} else {
			ThisWeekScheduled.Text = String.Format("{0}", int.Parse("0"));		
		}

		if(dataLastWeek.Rows.Count>0){
			int sum = dataLastWeek.AsEnumerable().Sum(row => row.Field<int>("tot"));
			LastWeekFSS.Text = sum.ToString();
		} else {
			LastWeekFSS.Text = String.Format("{0}", int.Parse("0"));		
		}
		
*/		foreach(DailyStat dd in thisweek)
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
			ShieldChartFSS7R.DataSource = datasource;		
		else
			ShieldChartFSS7R.SecondaryHeader.Text = "(No Data Found)";

	}

	protected void ShieldChartPurif7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		// DataTable dataThisWeek = new DataTable();
		// dataThisWeek = DAL.GetOrgDailyCategory("thisweek", "Paid Start", "Paid Start", OrgText.Text);

		// DataTable thisWeekSched = new DataTable();
		// thisWeekSched = DAL.GetOrgDailyCategory("thisweeksched", "Paid Start", "Paid Start", OrgText.Text);

		// DataTable dataLastWeek = new DataTable();
		// dataLastWeek = DAL.GetOrgDailyCategory("lastweek", "Paid Start", "Paid Start", OrgText.Text);

		// string d = ""; 
		// int thiswk = 0; 
		// int thiswksched = 0; 
		// int thiswkschedcum = 0; 
		// int lastwk = 0;
		// int thiswkcum = 0;
		// int lastwkcum = 0;

		// List<DailyBIS7R> datasource = new List<DailyBIS7R>();
			
		// if(dataThisWeek!=null){

			// for (int j = 0; j < dataThisWeek.Rows.Count; j++)
			// {
				// for (int i = 0; i < dataThisWeek.Columns.Count; i++)    
				// {    
					// if(dataThisWeek.Columns[i].ColumnName=="week_day"){
						// d = dataThisWeek.Rows[j].ItemArray[i].ToString();
					// } else if(dataThisWeek.Columns[i].ColumnName=="tot"){
						// Int32.TryParse(dataThisWeek.Rows[j].ItemArray[i].ToString(), out thiswk);					
						// thiswkcum += thiswk;
					// }
				// }
				// DailyBIS7R dailyBIS7R = new DailyBIS7R{			
					// dayofweek = d,
					// thisweek = thiswk.ToString(),
					// thisweekcum = thiswkcum.ToString()
				// };
				// datasource.Add(dailyBIS7R);			
			// };
		// }
		
// /*SCHEDULED*/

		// if(thisWeekSched!=null){

			// for (int j = 0; j < thisWeekSched.Rows.Count; j++)
			// {
				// foreach(DailyBIS7R dd in datasource)
				// {
					// if(thisWeekSched.Rows[j].ItemArray[0].ToString()==dd.dayofweek){
						// dd.thisweeksched = thisWeekSched.Rows[j].ItemArray[2].ToString();
						// Int32.TryParse(thisWeekSched.Rows[j].ItemArray[2].ToString(), out thiswksched);	
						// thiswkschedcum += thiswksched;
						// dd.thisweekschedcum = thiswkschedcum.ToString();
					// }
				// }
			// }
		// }
		
		// foreach(DailyBIS7R dd in datasource)
		// {
			// DailyBIS7R d7 = new DailyBIS7R();
			// d7 = PrepareDailyBIS7R(dd);
			// dd.thisweek = d7.thisweek;
			// dd.thisweekcum = d7.thisweekcum;
		// }

// /*LAST WEEK		*/

		// if(dataLastWeek!=null){

			// for (int j = 0; j < dataLastWeek.Rows.Count; j++)
			// {
				// foreach(DailyBIS7R dd in datasource)
				// {
					// if(dataLastWeek.Rows[j].ItemArray[0].ToString()==dd.dayofweek){
						// dd.lastweek = dataLastWeek.Rows[j].ItemArray[2].ToString();
						// Int32.TryParse(dataLastWeek.Rows[j].ItemArray[2].ToString(), out lastwk);	
						// lastwkcum += lastwk;
						// dd.lastweekcum = lastwkcum.ToString();
					// }
				// }
			// }
		// }
		
		// foreach(DailyBIS7R dd in datasource)
		// {
			// DailyBIS7R d7 = new DailyBIS7R();
			// d7 = PrepareDailyBIS7R(dd);
			// dd.thisweek = d7.thisweek;
			// dd.thisweekcum = d7.thisweekcum;
		// }
		

		// ThisWeekPurif.Text = String.Format("{0} ", thiswkcum);
		// ThisWeekScheduledPurif.Text = String.Format("{0} ", thiswkschedcum);
		// LastWeekPurif.Text = String.Format("{0} ", lastwkcum);
		
		// if(datasource.Count >0)
			// ShieldChartPurif7R.DataSource = datasource;		
		// else
			// ShieldChartPurif7R.SecondaryHeader.Text = "(No Data Found)";

	}

	protected void ShieldChartThisWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		DataTable dt = new DataTable();
		dt = DAL.GetOrgEventsBar(OrgText.Text, "Confirmed");

		if(dt.Rows.Count>0){
			ShieldChartThisWeek.DataSource = dt;
		} else {
			ShieldChartThisWeek.SecondaryHeader.Text = "(No Data Found)";
		}
	}
	
	private class WeeklyRecruit
	{
		public string weekend { get; set; }
		public string phs { get; set; }
		public string signup { get; set; }
	}
	
	public void clickTest(Object sender, EventArgs e){

		ErrorText.Text += "DEBUG STARTED<BR />";

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
