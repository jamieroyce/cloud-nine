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
    pdal PDAL = new pdal();

	static string searchText;
	static string searchCol;
	static string searchWE;

	static int ssnMinsThu;
	static int ssnMinsFri;
	static int ssnMinsSat;
	static int ssnMinsSun;
	static int ssnMinsMon;
	static int ssnMinsTue;
	static int ssnMinsWed;
	static int ssnMinsThu2;
	
	static int adminMinsThu;
	static int adminMinsFri;
	static int adminMinsSat;
	static int adminMinsSun;
	static int adminMinsMon;
	static int adminMinsTue;
	static int adminMinsWed;
	static int adminMinsThu2;

	static int purifMinsThu;
	static int purifMinsFri;
	static int purifMinsSat;
	static int purifMinsSun;
	static int purifMinsMon;
	static int purifMinsTue;
	static int purifMinsWed;
	static int purifMinsThu2;

	static int totMinsThu;
	static int totMinsFri;
	static int totMinsSat;
	static int totMinsSun;
	static int totMinsMon;
	static int totMinsTue;
	static int totMinsWed;
	static int totMinsThu2;
	
	static int totSsnMins;
	static int totAdminMins;
	static int totPurifMins;
	static int totMins;

	static string status = "Event";
	static string vCat = "Event";

	static string vAddoId = "0";
	static string vPcId = "0";

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
			DataTable eventList = DAL.getEvents(); 
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
		HeadText.Text = "WDAH Log";
		HeaderText.Text = "WDAH Log";

		DateTime nextThursday = DAL.GetNextWeekday(DateTime.Now, DayOfWeek.Thursday);
		lblWeekending.Text = nextThursday.ToString("dd-MMM-yyyy");   
		// HeaderWeekend.Text = lblWeekending.Text;
		
		OrgText.Visible = true;
		ErrorText.Text = "";
		searchText = "";
		searchCol = "";
		searchWE = "";
		vAddoId = "0";
		vPcId = "0";		
		
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

		DataTable auditorList = PDAL.getAuditors(OrgText.Text); 
		ddl_auditor.DataSource = auditorList;
		ddl_auditor.DataTextField = auditorList.Columns["name"].ToString();
		ddl_auditor.DataValueField = auditorList.Columns["id"].ToString();
		ddl_auditor.DataBind();
		ddl_auditor.Items.Insert(0, new ListItem("Choose an Auditor","0"));
		
		DataTable pcList = PDAL.getPreclears(OrgText.Text); 
		ddl_pc.DataSource = pcList;
		ddl_pc.DataTextField = pcList.Columns["name"].ToString();
		ddl_pc.DataValueField = pcList.Columns["id"].ToString();
		ddl_pc.DataBind();
		ddl_pc.Items.Insert(0, new ListItem("Choose a Preclear","0"));
		
		ShieldChartThu.DataBind();
		ShieldChartFri.DataBind();
		ShieldChartSat.DataBind();
		ShieldChartSun.DataBind();
		ShieldChartMon.DataBind();
		ShieldChartTue.DataBind();
		ShieldChartWed.DataBind();
		ShieldChartThu2.DataBind();
		
		LoadData();

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
		StartingPage();
        
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
		StartingPage();
        
    }

	protected void LoadData()
	{

		int vSsnId = 0;
		string vPc = "";
		string pc_id = "";
		string auditor_id = "";
		string vType = "";
		string vAuditor = "";
		string vSessionDate = "";
		string vIntensiveId = "";
		int vSessionMin = 0;
		int vAdminMin = 0;
		int vTotMin = 0;
		string vWeekend = "";
		int vVsd = 0;
		
		DataTable dtDailySessions = PDAL.getDailySession(OrgText.Text, lblWeekending.Text);			
		
		List<SessionRecord> datasource = new List<SessionRecord>();
		DataTable dtSessions = PDAL.getSessions(OrgText.Text);
		for (int j = 0; j < dtSessions.Rows.Count; j++)
		{
			for (int i = 0; i < dtSessions.Columns.Count; i++)    
			{    
				if(dtSessions.Columns[i].ColumnName=="id"){
					vSsnId = Convert.ToInt32(dtSessions.Rows[j].ItemArray[i]);
				}
				if(dtSessions.Columns[i].ColumnName=="session_date"){
					vSessionDate = dtSessions.Rows[j].ItemArray[i].ToString();
					vSessionDate = DateTime.Parse(vSessionDate).ToString("dd-MMM-yyyy");
				}
				if(dtSessions.Columns[i].ColumnName=="pc_id"){
					pc_id = dtSessions.Rows[j].ItemArray[i].ToString();
				}
				if(dtSessions.Columns[i].ColumnName=="auditor_id"){
					auditor_id = dtSessions.Rows[j].ItemArray[i].ToString();
				}
				if(dtSessions.Columns[i].ColumnName=="type"){
					vType = dtSessions.Rows[j].ItemArray[i].ToString();
				}
				if(dtSessions.Columns[i].ColumnName=="intensive_id"){
					vIntensiveId = dtSessions.Rows[j].ItemArray[i].ToString();
				}
				if(dtSessions.Columns[i].ColumnName=="session_minutes"){
					vSessionMin = Convert.ToInt32(dtSessions.Rows[j].ItemArray[i]);
				}
				if(dtSessions.Columns[i].ColumnName=="admin_minutes"){
					vAdminMin = Convert.ToInt32(dtSessions.Rows[j].ItemArray[i]);
				}
				if(dtSessions.Columns[i].ColumnName=="weekend"){
					vWeekend = dtSessions.Rows[j].ItemArray[i].ToString();
				}
				vTotMin = vSessionMin + vAdminMin;
			}

			DataTable dtAuditor = PDAL.getAuditorFromId( int.Parse(auditor_id));
			for (int jj = 0; jj < dtAuditor.Rows.Count; jj++)
			{
				for (int ii = 0; ii < dtAuditor.Columns.Count; ii++)    
				{    
					if(dtAuditor.Columns[ii].ColumnName=="name"){
						vAuditor = dtAuditor.Rows[jj].ItemArray[ii].ToString();
					}
				}
			}

			DataTable dtPC = PDAL.getPreclearFromId( int.Parse(pc_id));
			for (int jj = 0; jj < dtPC.Rows.Count; jj++)
			{
				for (int ii = 0; ii < dtPC.Columns.Count; ii++)    
				{    
					if(dtPC.Columns[ii].ColumnName=="name"){
						vPc = dtPC.Rows[jj].ItemArray[ii].ToString();
					}
				}
			}

			SessionRecord ssnRec = new SessionRecord{			
				sessionId = vSsnId,
				sessionDate = vSessionDate,
				org = OrgText.Text,
				pc = vPc,
				type = vType,
				auditor = vAuditor,
				intensive = vIntensiveId,
				sessionMin = vSessionMin,
				totMin = vTotMin,
				adminMin = vAdminMin,
				sessionTime = PDAL.GetHoursAndMinutes(vSessionMin),
				adminTime = PDAL.GetHoursAndMinutes(vAdminMin),
				totTime = PDAL.GetHoursAndMinutes(vTotMin)
			};

			datasource.Add(ssnRec);
		}
		
		//LOOP THROUGH THE DATASOURCE AND GRAB EACH DAY OF THE WEEK AND PUT IT IN A SEPARATE GRIDVIEW

		List<SessionRecord> sessionThu = new List<SessionRecord>();
		List<SessionRecord> sessionFri = new List<SessionRecord>();
		List<SessionRecord> sessionSat = new List<SessionRecord>();
		List<SessionRecord> sessionSun = new List<SessionRecord>();
		List<SessionRecord> sessionMon = new List<SessionRecord>();
		List<SessionRecord> sessionTue = new List<SessionRecord>();
		List<SessionRecord> sessionWed = new List<SessionRecord>();
		List<SessionRecord> sessionThu2 = new List<SessionRecord>();
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		
		foreach(SessionRecord rec in datasource)
		{
			if(DateTime.Parse(rec.sessionDate) == vWeekend2.AddDays(-7).Date){
				sessionThu.Add(rec);
			} else if(DateTime.Parse(rec.sessionDate) == vWeekend2.AddDays(-6).Date){
				sessionFri.Add(rec);
			} else if(DateTime.Parse(rec.sessionDate) == vWeekend2.AddDays(-5).Date){
				sessionSat.Add(rec);
			} else if(DateTime.Parse(rec.sessionDate) == vWeekend2.AddDays(-4).Date){
				sessionSun.Add(rec);
			} else if(DateTime.Parse(rec.sessionDate) == vWeekend2.AddDays(-3).Date){
				sessionMon.Add(rec);
			} else if(DateTime.Parse(rec.sessionDate) == vWeekend2.AddDays(-2).Date){
				sessionTue.Add(rec);
			} else if(DateTime.Parse(rec.sessionDate) == vWeekend2.AddDays(-1).Date){
				sessionWed.Add(rec);
			} else if(DateTime.Parse(rec.sessionDate) == vWeekend2.Date){
				sessionThu2.Add(rec);
			}
		}
		
		GridViewSessionThu.DataSource = sessionThu;
		GridViewSessionThu.DataBind();
		GridViewSessionFri.DataSource = sessionFri;
		GridViewSessionFri.DataBind();
		GridViewSessionSat.DataSource = sessionSat;
		GridViewSessionSat.DataBind();
		GridViewSessionSun.DataSource = sessionSun;
		GridViewSessionSun.DataBind();
		GridViewSessionMon.DataSource = sessionMon;
		GridViewSessionMon.DataBind();
		GridViewSessionTue.DataSource = sessionTue;
		GridViewSessionTue.DataBind();
		GridViewSessionWed.DataSource = sessionWed;
		GridViewSessionWed.DataBind();
		GridViewSessionThu2.DataSource = sessionThu2;
		GridViewSessionThu2.DataBind();


		//GET PC SUMMARY 
		DataTable dtPcView = PDAL.getPcView(OrgText.Text, lblWeekending.Text);
		List<SessionRecord> ssnSummary = new List<SessionRecord>();
		
		int pcID = 0;
		string name = "";
		string myVsd = "";
		int myToVsd = 0;
		int minThisWeek = 0;
		int minToInco = 0;
		int myintMinUsed = 0;
		
		for (int j = 0; j < dtPcView.Rows.Count; j++)
		{
			for (int i = 0; i < dtPcView.Columns.Count; i++)    
			{    
				if(dtPcView.Columns[i].ColumnName=="pc_id"){
					pcID = Convert.ToInt32(dtPcView.Rows[j].ItemArray[i]);
				}
				if(dtPcView.Columns[i].ColumnName=="name"){
					name = dtPcView.Rows[j].ItemArray[i].ToString();
				}
				if(dtPcView.Columns[i].ColumnName=="vsd_value"){
					myVsd = dtPcView.Rows[j].ItemArray[i].ToString();
				}
				if(dtPcView.Columns[i].ColumnName=="minutes_to_vsd"){
					myToVsd = Convert.ToInt32(dtPcView.Rows[j].ItemArray[i]);
				}
				if(dtPcView.Columns[i].ColumnName=="minutes_this_week"){
					minThisWeek = Convert.ToInt32(dtPcView.Rows[j].ItemArray[i]);
				}
				if(dtPcView.Columns[i].ColumnName=="minutes_to_inco"){
					minToInco = Convert.ToInt32(dtPcView.Rows[j].ItemArray[i]);
				}
				if(dtPcView.Columns[i].ColumnName=="intensive_min_used"){
					myintMinUsed = Convert.ToInt32(dtPcView.Rows[j].ItemArray[i]);
				}
				
				// ErrorText.Text += dtPcView.Columns[i].ColumnName + ": ";
				// ErrorText.Text += dtPcView.Rows[j].ItemArray[i].ToString() + "</BR>";
				
			}
			
			SessionRecord ssnRec = new SessionRecord{			
				pcId = pcID,
				org = OrgText.Text,
				pc = name,
				vsd = myVsd,
				hrsToVSD = PDAL.GetHoursAndMinutes(myToVsd),
				inco = PDAL.GetHoursAndMinutes(minThisWeek),
				hrsToInco = PDAL.GetHoursAndMinutes(minToInco),
				intMinUsed = PDAL.GetHoursAndMinutes(myintMinUsed)
			};

			ssnSummary.Add(ssnRec);
		}
		
		GridViewSummary.DataSource = ssnSummary;
		GridViewSummary.DataBind();

		DataTable dtAud = PDAL.getAuditorView(OrgText.Text, lblWeekending.Text);
		List<AuditorSummary> audSummary = new List<AuditorSummary>();
		
		int vId = 0;
		string vName = "";
		int myTotSsnMin = 0;
		int myTotAdminMin = 0;
		int myTotMin = 0;
		
		for (int j = 0; j < dtAud.Rows.Count; j++)
		{
			for (int i = 0; i < dtAud.Columns.Count; i++)    
			{    
				if(dtAud.Columns[i].ColumnName=="id"){
					vId = Convert.ToInt32(dtAud.Rows[j].ItemArray[i]);
				}
				if(dtAud.Columns[i].ColumnName=="name"){
					vName = dtAud.Rows[j].ItemArray[i].ToString();
				}
				if(dtAud.Columns[i].ColumnName=="tot_ssn_min"){
					myTotSsnMin = Convert.ToInt32(dtAud.Rows[j].ItemArray[i]);
				}
				if(dtAud.Columns[i].ColumnName=="tot_admin_min"){
					myTotAdminMin = Convert.ToInt32(dtAud.Rows[j].ItemArray[i]);
				}
				if(dtAud.Columns[i].ColumnName=="tot_min"){
					myTotMin = Convert.ToInt32(dtAud.Rows[j].ItemArray[i]);
				}
				ErrorText.Text += dtAud.Columns[i].ColumnName + ": ";
				ErrorText.Text += dtAud.Rows[j].ItemArray[i].ToString() + "</BR>";
			}
			
			AuditorSummary rec = new AuditorSummary{			
				id = vId,
				org = OrgText.Text,
				name = vName,
				totMin = myTotMin,
				totSsnMin = myTotSsnMin,
				totAdminMin = myTotAdminMin,
				adminMinutes = PDAL.GetHoursAndMinutes(myTotAdminMin),
				sessionMinutes = PDAL.GetHoursAndMinutes(myTotSsnMin),
				totMinutes = PDAL.GetHoursAndMinutes(myTotMin)
			};

			audSummary.Add(rec);
		}
		
		// GridViewAuditor.DataSource = audSummary;
		// GridViewAuditor.DataBind();
		
		//ADD UP ALL THE DAILY TOTALS AND PUT THIS IN THE OVERALL LABELS
		int wdahwo = 0;
		totSsnMins = ssnMinsThu + ssnMinsFri + ssnMinsSat + ssnMinsSun + ssnMinsMon + ssnMinsTue + ssnMinsWed + ssnMinsThu2;
		totAdminMins = adminMinsThu + adminMinsFri + adminMinsSat + adminMinsSun + adminMinsMon + adminMinsTue + adminMinsWed + adminMinsThu2;
		totPurifMins = purifMinsThu + purifMinsFri + purifMinsSat + purifMinsSun + purifMinsMon + purifMinsTue + purifMinsWed + purifMinsThu2;
		totMins = totMinsThu + totMinsFri + totMinsSat + totMinsSun + totMinsMon + totMinsTue + totMinsWed + totMinsThu2;
		wdahwo = totSsnMins + totAdminMins;

		lblHeaderTot.Text = PDAL.GetHoursAndMinutes(totMins);
		lblHeaderWdahwo.Text = PDAL.GetHoursAndMinutes(wdahwo);
		lblHeaderChair.Text = PDAL.GetHoursAndMinutes(totSsnMins);
		lblHeaderAdmin.Text = PDAL.GetHoursAndMinutes(totAdminMins);
		lblHeaderPurif.Text = PDAL.GetHoursAndMinutes(totPurifMins);

	}

	private class SessionRecord
	{
		public int sessionId { get; set; }
		public int pcId { get; set; }
		public string sessionDate { get; set; }
		public string org { get; set; }
		public string pc { get; set; }
		public string type { get; set; }
		public string auditor { get; set; }
		public string intensive { get; set; }
		public string sessionTime { get; set; }
		public string adminTime { get; set; }
		public string totTime { get; set; }
		public int totMin { get; set; }
		public int sessionMin { get; set; }
		public int adminMin { get; set; }
		public string intMinUsed { get; set; }
		public string vsd { get; set; }
		public string hrsToVSD { get; set; }
		public string inco { get; set; }
		public string hrsToInco { get; set; }
		public string weekend { get; set; }
	}
			
	private class DailySession
	{
		public string name { get; set; }
		public string adminMinutes { get; set; }
		public string sessionMinutes { get; set; }
		public string totMinutes { get; set; }
		public string type { get; set; }
	}
	
	private class AuditorSummary
	{
		public int id { get; set; }
		public string org { get; set; }
		public string name { get; set; }
		public string adminMinutes { get; set; }
		public string sessionMinutes { get; set; }
		public string totMinutes { get; set; }
		public int totMin { get; set; }
		public int totSsnMin { get; set; }
		public int totAdminMin { get; set; }
	}

	protected void ShieldChartAuditor_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";
		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DataTable dtAud = PDAL.getAuditorView(OrgText.Text, lblWeekending.Text);
		List<AuditorSummary> audSummary = new List<AuditorSummary>();
		
		int vId = 0;
		string vName = "";
		int myTotSsnMin = 0;
		int myTotAdminMin = 0;
		int myTotMin = 0;
		
		for (int j = 0; j < dtAud.Rows.Count; j++)
		{
			for (int i = 0; i < dtAud.Columns.Count; i++)    
			{    
				if(dtAud.Columns[i].ColumnName=="id"){
					vId = Convert.ToInt32(dtAud.Rows[j].ItemArray[i]);
				}
				if(dtAud.Columns[i].ColumnName=="name"){
					vName = dtAud.Rows[j].ItemArray[i].ToString();
				}
				if(dtAud.Columns[i].ColumnName=="tot_ssn_min"){
					myTotSsnMin = Convert.ToInt32(dtAud.Rows[j].ItemArray[i]);
				}
				if(dtAud.Columns[i].ColumnName=="tot_admin_min"){
					myTotAdminMin = Convert.ToInt32(dtAud.Rows[j].ItemArray[i]);
				}
				if(dtAud.Columns[i].ColumnName=="tot_min"){
					myTotMin = Convert.ToInt32(dtAud.Rows[j].ItemArray[i]);
				}
				ErrorText.Text += dtAud.Columns[i].ColumnName + ": ";
				ErrorText.Text += dtAud.Rows[j].ItemArray[i].ToString() + "</BR>";
			}

			double dbl = double.Parse(myTotMin.ToString()) / 60;
			
			AuditorSummary rec = new AuditorSummary{			
				id = vId,
				org = OrgText.Text,
				name = vName,
				totMin = myTotMin ,
				totSsnMin = myTotSsnMin ,
				// totAdminMin = myTotAdminMin ,
				// adminMinutes = myTotAdminMin.ToString(),
				// sessionMinutes = myTotSsnMin.ToString(),
				totMinutes = string.Format("{0:0.0}", dbl / 60)
				// adminMinutes = string.Format("{0:0.0}", myTotAdminMin / 60),
				// sessionMinutes = string.Format("{0:0.0}", myTotSsnMin / 60),
				// totMinutes = string.Format("{0:0.0}", dbl / 60)
			};

			audSummary.Add(rec);
		}

		ErrorText.Text = "AUDITOR SUMMARY: </BR>";
		foreach(AuditorSummary rec in audSummary)	{
			ErrorText.Text += " name = " + rec.name + " | ";
			ErrorText.Text += " totMinutes = " + rec.totMinutes + " | ";
			ErrorText.Text += " sessionMinutes = " + rec.sessionMinutes + " | ";
			ErrorText.Text += " adminMinutes = " + rec.adminMinutes + "</BR>";
		}
		
		if(audSummary.Count >0){
			ShieldChartAuditor.DataSource = audSummary;
		} else {
			ShieldChartAuditor.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartAuditor2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";
		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		DateTime thurs = vWeekend2.AddDays(-7).Date;

		string vThu = thurs.ToString("dd-MMM-yyyy");
		
		DataTable dt = PDAL.getDailySession( OrgText.Text, vThu );
		List<DailySession> datasource = new List<DailySession>();
		
		//FOR HGC
		if(dt != null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				for (int i = 0; i < dt.Columns.Count; i++)    
				{    

					//IF TYPE = HGC
					if(dt.Rows[j].ItemArray[2].ToString()=="HGC"){

						if(dt.Columns[i].ColumnName=="name"){
							vPc = dt.Rows[j].ItemArray[i].ToString();
						}
						if(dt.Columns[i].ColumnName=="session_minutes"){
							session_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="admin_minutes"){
							admin_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="minutes"){
							vMinutes = dt.Rows[j].ItemArray[i].ToString();
						}
					}					
					
					//IF TYPE = PURIF
					if(dt.Rows[j].ItemArray[2].ToString()=="Purif"){
						if(dt.Columns[i].ColumnName=="minutes"){
							purif_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
					}					
				}
				
				if(purif_minutes==0){
					double dbl = double.Parse(vMinutes) / 60;
					DailySession dailyRec = new DailySession{			
						name = vPc,
						totMinutes = string.Format("{0:0.0}", dbl)
					};
					datasource.Add(dailyRec);
				}
				
				tot_session_minutes = tot_session_minutes + session_minutes;
				tot_admin_minutes = tot_admin_minutes + admin_minutes;
				tot_purif_minutes = tot_purif_minutes + purif_minutes;
				purif_minutes = 0;
			}

			total_minutes = tot_session_minutes + tot_admin_minutes + tot_purif_minutes;

			lblTot.Text = PDAL.GetHoursAndMinutes(total_minutes);
			lblChair.Text = PDAL.GetHoursAndMinutes(tot_session_minutes);
			lblAdmin.Text = PDAL.GetHoursAndMinutes(tot_admin_minutes);
			lblPurif.Text = PDAL.GetHoursAndMinutes(tot_purif_minutes);

			ssnMinsThu = tot_session_minutes;
			adminMinsThu = tot_admin_minutes;
			purifMinsThu = tot_purif_minutes;
			totMinsThu = total_minutes;
			
			// foreach(DailySession rec in datasource)	{
				// ErrorText.Text += " name = " + rec.name + " | ";
				// ErrorText.Text += " totMinutes = " + rec.totMinutes + "</BR>";
			// }
			
			if(datasource.Count >0){
				ShieldChartAuditor.DataSource = datasource;
			} else {
				ShieldChartAuditor.SecondaryHeader.Text = "(No Data Found)";
			}
		} else {
			ShieldChartAuditor.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartThu_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";
		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		DateTime thurs = vWeekend2.AddDays(-7).Date;

		string vThu = thurs.ToString("dd-MMM-yyyy");
		
		DataTable dt = PDAL.getDailySession( OrgText.Text, vThu );
		List<DailySession> datasource = new List<DailySession>();
		
		//FOR HGC
		if(dt != null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				for (int i = 0; i < dt.Columns.Count; i++)    
				{    

					//IF TYPE = HGC
					if(dt.Rows[j].ItemArray[2].ToString()=="HGC"){

						if(dt.Columns[i].ColumnName=="name"){
							vPc = dt.Rows[j].ItemArray[i].ToString();
						}
						if(dt.Columns[i].ColumnName=="session_minutes"){
							session_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="admin_minutes"){
							admin_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="minutes"){
							vMinutes = dt.Rows[j].ItemArray[i].ToString();
						}
					}					
					
					//IF TYPE = PURIF
					if(dt.Rows[j].ItemArray[2].ToString()=="Purif"){
						if(dt.Columns[i].ColumnName=="minutes"){
							purif_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
					}					
				}
				
				if(purif_minutes==0){
					double dbl = double.Parse(vMinutes) / 60;
					DailySession dailyRec = new DailySession{			
						name = vPc,
						totMinutes = string.Format("{0:0.0}", dbl)
					};
					datasource.Add(dailyRec);
				}
				
				tot_session_minutes = tot_session_minutes + session_minutes;
				tot_admin_minutes = tot_admin_minutes + admin_minutes;
				tot_purif_minutes = tot_purif_minutes + purif_minutes;
				purif_minutes = 0;
			}

			total_minutes = tot_session_minutes + tot_admin_minutes + tot_purif_minutes;

			lblTot.Text = PDAL.GetHoursAndMinutes(total_minutes);
			lblChair.Text = PDAL.GetHoursAndMinutes(tot_session_minutes);
			lblAdmin.Text = PDAL.GetHoursAndMinutes(tot_admin_minutes);
			lblPurif.Text = PDAL.GetHoursAndMinutes(tot_purif_minutes);

			ssnMinsThu = tot_session_minutes;
			adminMinsThu = tot_admin_minutes;
			purifMinsThu = tot_purif_minutes;
			totMinsThu = total_minutes;
			
			// foreach(DailySession rec in datasource)	{
				// ErrorText.Text += " name = " + rec.name + " | ";
				// ErrorText.Text += " totMinutes = " + rec.totMinutes + "</BR>";
			// }
			
			if(datasource.Count >0){
				ShieldChartThu.DataSource = datasource;
			} else {
				ShieldChartThu.SecondaryHeader.Text = "(No Data Found)";
			}
		} else {
			ShieldChartThu.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartFri_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";
		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		DateTime thurs = vWeekend2.AddDays(-6).Date;
		string vThu = thurs.ToString("dd-MMM-yyyy");
		DataTable dt = PDAL.getDailySession( OrgText.Text, vThu );
		List<DailySession> datasource = new List<DailySession>();
		
		//FOR HGC
		if(dt != null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				for (int i = 0; i < dt.Columns.Count; i++)    
				{    

					//IF TYPE = HGC
					if(dt.Rows[j].ItemArray[2].ToString()=="HGC"){

						if(dt.Columns[i].ColumnName=="name"){
							vPc = dt.Rows[j].ItemArray[i].ToString();
						}
						if(dt.Columns[i].ColumnName=="session_minutes"){
							session_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="admin_minutes"){
							admin_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="minutes"){
							vMinutes = dt.Rows[j].ItemArray[i].ToString();
						}
					}					
					
					//IF TYPE = PURIF
					if(dt.Rows[j].ItemArray[2].ToString()=="Purif"){
						if(dt.Columns[i].ColumnName=="minutes"){
							purif_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
					}					
				}
				
				if(purif_minutes==0){
					double dbl = double.Parse(vMinutes) / 60;
					DailySession dailyRec = new DailySession{			
						name = vPc,
						totMinutes = string.Format("{0:0.0}", dbl)
					};
					datasource.Add(dailyRec);
				}
				
				tot_session_minutes = tot_session_minutes + session_minutes;
				tot_admin_minutes = tot_admin_minutes + admin_minutes;
				tot_purif_minutes = tot_purif_minutes + purif_minutes;
				purif_minutes = 0;
			}

			total_minutes = tot_session_minutes + tot_admin_minutes + tot_purif_minutes;

			lblTotFri.Text = PDAL.GetHoursAndMinutes(total_minutes);
			lblChairFri.Text = PDAL.GetHoursAndMinutes(tot_session_minutes);
			lblAdminFri.Text = PDAL.GetHoursAndMinutes(tot_admin_minutes);
			lblPurifFri.Text = PDAL.GetHoursAndMinutes(tot_purif_minutes);
			
			ssnMinsFri = tot_session_minutes;
			adminMinsFri = tot_admin_minutes;
			purifMinsFri = tot_purif_minutes;
			totMinsFri = total_minutes;
			
			if(datasource.Count >0){
				ShieldChartFri.DataSource = datasource;
			} else {
				ShieldChartFri.SecondaryHeader.Text = "(No Data Found)";
			}
		} else {
			ShieldChartFri.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartSat_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";
		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		DateTime d = vWeekend2.AddDays(-5).Date;
		string vDay = d.ToString("dd-MMM-yyyy");
		DataTable dt = PDAL.getDailySession( OrgText.Text, vDay );
		List<DailySession> datasource = new List<DailySession>();
		
		//FOR HGC
		if(dt != null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				for (int i = 0; i < dt.Columns.Count; i++)    
				{    

					//IF TYPE = HGC
					if(dt.Rows[j].ItemArray[2].ToString()=="HGC"){

						if(dt.Columns[i].ColumnName=="name"){
							vPc = dt.Rows[j].ItemArray[i].ToString();
						}
						if(dt.Columns[i].ColumnName=="session_minutes"){
							session_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="admin_minutes"){
							admin_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="minutes"){
							vMinutes = dt.Rows[j].ItemArray[i].ToString();
						}
					}					
					
					//IF TYPE = PURIF
					if(dt.Rows[j].ItemArray[2].ToString()=="Purif"){
						if(dt.Columns[i].ColumnName=="minutes"){
							purif_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
					}					
				}
				
				if(purif_minutes==0){
					double dbl = double.Parse(vMinutes) / 60;
					DailySession dailyRec = new DailySession{			
						name = vPc,
						totMinutes = string.Format("{0:0.0}", dbl)
					};
					datasource.Add(dailyRec);
				}
				
				tot_session_minutes = tot_session_minutes + session_minutes;
				tot_admin_minutes = tot_admin_minutes + admin_minutes;
				tot_purif_minutes = tot_purif_minutes + purif_minutes;
				purif_minutes = 0;
			}

			total_minutes = tot_session_minutes + tot_admin_minutes + tot_purif_minutes;

			lblTotSat.Text = PDAL.GetHoursAndMinutes(total_minutes);
			lblChairSat.Text = PDAL.GetHoursAndMinutes(tot_session_minutes);
			lblAdminSat.Text = PDAL.GetHoursAndMinutes(tot_admin_minutes);
			lblPurifSat.Text = PDAL.GetHoursAndMinutes(tot_purif_minutes);

			ssnMinsSat = tot_session_minutes;
			adminMinsSat = tot_admin_minutes;
			purifMinsSat = tot_purif_minutes;
			totMinsSat = total_minutes;
			
			if(datasource.Count >0){
				ShieldChartSat.DataSource = datasource;
			} else {
				ShieldChartSat.SecondaryHeader.Text = "(No Data Found)";
			}
		} else {
			ShieldChartSat.SecondaryHeader.Text = "(No Data Found)";
		}
	}	

	protected void ShieldChartSun_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";
		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		DateTime d = vWeekend2.AddDays(-4).Date;
		string vDay = d.ToString("dd-MMM-yyyy");
		DataTable dt = PDAL.getDailySession( OrgText.Text, vDay );
		List<DailySession> datasource = new List<DailySession>();
		
		//FOR HGC
		if(dt != null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				for (int i = 0; i < dt.Columns.Count; i++)    
				{    

					//IF TYPE = HGC
					if(dt.Rows[j].ItemArray[2].ToString()=="HGC"){

						if(dt.Columns[i].ColumnName=="name"){
							vPc = dt.Rows[j].ItemArray[i].ToString();
						}
						if(dt.Columns[i].ColumnName=="session_minutes"){
							session_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="admin_minutes"){
							admin_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="minutes"){
							vMinutes = dt.Rows[j].ItemArray[i].ToString();
						}
					}					
					
					//IF TYPE = PURIF
					if(dt.Rows[j].ItemArray[2].ToString()=="Purif"){
						if(dt.Columns[i].ColumnName=="minutes"){
							purif_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
					}					
				}
				
				if(purif_minutes==0){
					double dbl = double.Parse(vMinutes) / 60;
					DailySession dailyRec = new DailySession{			
						name = vPc,
						totMinutes = string.Format("{0:0.0}", dbl)
					};
					datasource.Add(dailyRec);
				}
				
				tot_session_minutes = tot_session_minutes + session_minutes;
				tot_admin_minutes = tot_admin_minutes + admin_minutes;
				tot_purif_minutes = tot_purif_minutes + purif_minutes;
				purif_minutes = 0;
			}

			total_minutes = tot_session_minutes + tot_admin_minutes + tot_purif_minutes;

			lblTotSun.Text = PDAL.GetHoursAndMinutes(total_minutes);
			lblChairSun.Text = PDAL.GetHoursAndMinutes(tot_session_minutes);
			lblAdminSun.Text = PDAL.GetHoursAndMinutes(tot_admin_minutes);
			lblPurifSun.Text = PDAL.GetHoursAndMinutes(tot_purif_minutes);
			
			ssnMinsSun = tot_session_minutes;
			adminMinsSun = tot_admin_minutes;
			purifMinsSun = tot_purif_minutes;
			totMinsSun = total_minutes;
			
			if(datasource.Count >0){
				ShieldChartSun.DataSource = datasource;
			} else {
				ShieldChartSun.SecondaryHeader.Text = "(No Data Found)";
			}
		} else {
			ShieldChartSun.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartMon_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";

		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		DateTime d = vWeekend2.AddDays(-3).Date;
		string vDay = d.ToString("dd-MMM-yyyy");
		DataTable dt = PDAL.getDailySession( OrgText.Text, vDay );
		List<DailySession> datasource = new List<DailySession>();

		//FOR HGC
		if(dt != null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				for (int i = 0; i < dt.Columns.Count; i++)    
				{    

					//IF TYPE = HGC
					if(dt.Rows[j].ItemArray[2].ToString()=="HGC"){

						if(dt.Columns[i].ColumnName=="name"){
							vPc = dt.Rows[j].ItemArray[i].ToString();
						}
						if(dt.Columns[i].ColumnName=="session_minutes"){
							session_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="admin_minutes"){
							admin_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="minutes"){
							vMinutes = dt.Rows[j].ItemArray[i].ToString();
						}
					}					
					
					//IF TYPE = PURIF
					if(dt.Rows[j].ItemArray[2].ToString()=="Purif"){
						if(dt.Columns[i].ColumnName=="minutes"){
							purif_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
					}					
				}
				
				if(purif_minutes==0){
					double dbl = double.Parse(vMinutes) / 60;
					DailySession dailyRec = new DailySession{			
						name = vPc,
						totMinutes = string.Format("{0:0.0}", dbl)
					};
					datasource.Add(dailyRec);
				}
				
				tot_session_minutes = tot_session_minutes + session_minutes;
				tot_admin_minutes = tot_admin_minutes + admin_minutes;
				tot_purif_minutes = tot_purif_minutes + purif_minutes;
				purif_minutes = 0;
			}

			total_minutes = tot_session_minutes + tot_admin_minutes + tot_purif_minutes;

			lblTotMon.Text = PDAL.GetHoursAndMinutes(total_minutes);
			lblChairMon.Text = PDAL.GetHoursAndMinutes(tot_session_minutes);
			lblAdminMon.Text = PDAL.GetHoursAndMinutes(tot_admin_minutes);
			lblPurifMon.Text = PDAL.GetHoursAndMinutes(tot_purif_minutes);
			
			ssnMinsMon = tot_session_minutes;
			adminMinsMon = tot_admin_minutes;
			purifMinsMon = tot_purif_minutes;
			totMinsMon = total_minutes;
			
			if(datasource.Count >0){
				ShieldChartMon.DataSource = datasource;
			} else {
				ShieldChartMon.SecondaryHeader.Text = "(No Data Found)";
			}
		} else {
			ShieldChartMon.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartTue_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";
		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		DateTime d = vWeekend2.AddDays(-2).Date;
		string vDay = d.ToString("dd-MMM-yyyy");
		DataTable dt = PDAL.getDailySession( OrgText.Text, vDay );
		List<DailySession> datasource = new List<DailySession>();
		
		//FOR HGC
		if(dt != null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				for (int i = 0; i < dt.Columns.Count; i++)    
				{    

					//IF TYPE = HGC
					if(dt.Rows[j].ItemArray[2].ToString()=="HGC"){

						if(dt.Columns[i].ColumnName=="name"){
							vPc = dt.Rows[j].ItemArray[i].ToString();
						}
						if(dt.Columns[i].ColumnName=="session_minutes"){
							session_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="admin_minutes"){
							admin_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="minutes"){
							vMinutes = dt.Rows[j].ItemArray[i].ToString();
						}
					}					
					
					//IF TYPE = PURIF
					if(dt.Rows[j].ItemArray[2].ToString()=="Purif"){
						if(dt.Columns[i].ColumnName=="minutes"){
							purif_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
					}					
				}
				
				if(purif_minutes==0){
					double dbl = double.Parse(vMinutes) / 60;
					DailySession dailyRec = new DailySession{			
						name = vPc,
						totMinutes = string.Format("{0:0.0}", dbl)
					};
					datasource.Add(dailyRec);
				}
				
				tot_session_minutes = tot_session_minutes + session_minutes;
				tot_admin_minutes = tot_admin_minutes + admin_minutes;
				tot_purif_minutes = tot_purif_minutes + purif_minutes;
				purif_minutes = 0;
			}

			total_minutes = tot_session_minutes + tot_admin_minutes + tot_purif_minutes;

			lblTotTue.Text = PDAL.GetHoursAndMinutes(total_minutes);
			lblChairTue.Text = PDAL.GetHoursAndMinutes(tot_session_minutes);
			lblAdminTue.Text = PDAL.GetHoursAndMinutes(tot_admin_minutes);
			lblPurifTue.Text = PDAL.GetHoursAndMinutes(tot_purif_minutes);
			
			ssnMinsTue = tot_session_minutes;
			adminMinsTue = tot_admin_minutes;
			purifMinsTue = tot_purif_minutes;
			totMinsTue = total_minutes;
			
			if(datasource.Count >0){
				ShieldChartTue.DataSource = datasource;
			} else {
				ShieldChartTue.SecondaryHeader.Text = "(No Data Found)";
			}
		} else {
			ShieldChartTue.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartWed_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";
		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		DateTime d = vWeekend2.AddDays(-1).Date;
		string vDay = d.ToString("dd-MMM-yyyy");
		DataTable dt = PDAL.getDailySession( OrgText.Text, vDay );
		List<DailySession> datasource = new List<DailySession>();
		
		//FOR HGC
		if(dt != null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				for (int i = 0; i < dt.Columns.Count; i++)    
				{    

					//IF TYPE = HGC
					if(dt.Rows[j].ItemArray[2].ToString()=="HGC"){

						if(dt.Columns[i].ColumnName=="name"){
							vPc = dt.Rows[j].ItemArray[i].ToString();
						}
						if(dt.Columns[i].ColumnName=="session_minutes"){
							session_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="admin_minutes"){
							admin_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="minutes"){
							vMinutes = dt.Rows[j].ItemArray[i].ToString();
						}
					}					
					
					//IF TYPE = PURIF
					if(dt.Rows[j].ItemArray[2].ToString()=="Purif"){
						if(dt.Columns[i].ColumnName=="minutes"){
							purif_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
					}					
				}
				
				if(purif_minutes==0){
					double dbl = double.Parse(vMinutes) / 60;
					DailySession dailyRec = new DailySession{			
						name = vPc,
						totMinutes = string.Format("{0:0.0}", dbl)
					};
					datasource.Add(dailyRec);
				}
				
				tot_session_minutes = tot_session_minutes + session_minutes;
				tot_admin_minutes = tot_admin_minutes + admin_minutes;
				tot_purif_minutes = tot_purif_minutes + purif_minutes;
				purif_minutes = 0;
			}

			total_minutes = tot_session_minutes + tot_admin_minutes + tot_purif_minutes;

			lblTotWed.Text = PDAL.GetHoursAndMinutes(total_minutes);
			lblChairWed.Text = PDAL.GetHoursAndMinutes(tot_session_minutes);
			lblAdminWed.Text = PDAL.GetHoursAndMinutes(tot_admin_minutes);
			lblPurifWed.Text = PDAL.GetHoursAndMinutes(tot_purif_minutes);

			ssnMinsWed = tot_session_minutes;
			adminMinsWed = tot_admin_minutes;
			purifMinsWed = tot_purif_minutes;
			totMinsWed = total_minutes;
			
			if(datasource.Count >0){
				ShieldChartWed.DataSource = datasource;
			} else {
				ShieldChartWed.SecondaryHeader.Text = "(No Data Found)";
			}
		} else {
			ShieldChartWed.SecondaryHeader.Text = "(No Data Found)";
		}
	}

	protected void ShieldChartThu2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
	
		string vPc = "";
		string vDayofweek = "";
		string vType = "";
		string vMinutes = "";
		int session_minutes = 0;
		int admin_minutes = 0;
		int tot_session_minutes = 0;
		int tot_admin_minutes = 0;
		int tot_purif_minutes = 0;
		int total_minutes = 0;
		int purif_minutes = 0;
		
		DateTime vWeekend2 = DateTime.Parse(lblWeekending.Text);
		DateTime d = vWeekend2.AddDays(-0).Date;
		string vDay = d.ToString("dd-MMM-yyyy");
		DataTable dt = PDAL.getDailySession( OrgText.Text, vDay );
		List<DailySession> datasource = new List<DailySession>();
		
		//FOR HGC
		if(dt != null){
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				for (int i = 0; i < dt.Columns.Count; i++)    
				{    

					//IF TYPE = HGC
					if(dt.Rows[j].ItemArray[2].ToString()=="HGC"){

						if(dt.Columns[i].ColumnName=="name"){
							vPc = dt.Rows[j].ItemArray[i].ToString();
						}
						if(dt.Columns[i].ColumnName=="session_minutes"){
							session_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="admin_minutes"){
							admin_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
						if(dt.Columns[i].ColumnName=="minutes"){
							vMinutes = dt.Rows[j].ItemArray[i].ToString();
						}
					}					
					
					//IF TYPE = PURIF
					if(dt.Rows[j].ItemArray[2].ToString()=="Purif"){
						if(dt.Columns[i].ColumnName=="minutes"){
							purif_minutes = Convert.ToInt32(dt.Rows[j].ItemArray[i]);
						}
					}					
				}
				
				if(purif_minutes==0){
					double dbl = double.Parse(vMinutes) / 60;
					DailySession dailyRec = new DailySession{			
						name = vPc,
						totMinutes = string.Format("{0:0.0}", dbl)
					};
					datasource.Add(dailyRec);
				}
				
				tot_session_minutes = tot_session_minutes + session_minutes;
				tot_admin_minutes = tot_admin_minutes + admin_minutes;
				tot_purif_minutes = tot_purif_minutes + purif_minutes;
				purif_minutes = 0;
			}

			total_minutes = tot_session_minutes + tot_admin_minutes + tot_purif_minutes;

			lblTotThu2.Text = PDAL.GetHoursAndMinutes(total_minutes);
			lblChairThu2.Text = PDAL.GetHoursAndMinutes(tot_session_minutes);
			lblAdminThu2.Text = PDAL.GetHoursAndMinutes(tot_admin_minutes);
			lblPurifThu2.Text = PDAL.GetHoursAndMinutes(tot_purif_minutes);
			
			ssnMinsThu2 = tot_session_minutes;
			adminMinsThu2 = tot_admin_minutes;
			purifMinsThu2 = tot_purif_minutes;
			totMinsThu2 = total_minutes;

			if(datasource.Count >0){
				ShieldChartThu2.DataSource = datasource;
			} else {
				ShieldChartThu2.SecondaryHeader.Text = "(No Data Found)";
			}
		} else {
			ShieldChartThu2.SecondaryHeader.Text = "(No Data Found)";
		}
	}
	
	protected void Event_Changed(object sender, EventArgs e)
	{

		// string message = ddl_Event.SelectedItem.Text + " - " + ddl_Event.SelectedItem.Value;
		
		string v = ddl_EventDetail.SelectedItem.Text;
		
		DataTable dt = DAL.getEvent(v);
		
		string vNotes = (from DataRow dr in dt.Rows
					  where (string)dr["event_desc"] == v
					  select (string)dr["notes"]).FirstOrDefault();		
		
		string vName = (from DataRow dr in dt.Rows
					  where (string)dr["event_desc"] == v
					  select (string)dr["name"]).FirstOrDefault();		

		lblEventDetail.Text = vNotes;
		lblEventCode.Text = vName;
		
	}	

	protected void ExportToExcel_Click(object sender, EventArgs e)
	{
		// var products = GetProducts();
		var data = DAL.get_log(vCat, OrgText.Text);
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

	protected void OnSelectedIndexChanged(object sender, EventArgs e)
	{

		// ErrorText.Text += "Changed the PC to: " + ddl_pc.SelectedItem.Value + "</BR>";
		vPcId = ddl_pc.SelectedItem.Value;

		DataTable pcInt = PDAL.getIntensives(vPcId, OrgText.Text); 
		ddl_int.DataSource = pcInt;
		ddl_int.DataTextField = pcInt.Columns["int_desc"].ToString();
		ddl_int.DataValueField = pcInt.Columns["id"].ToString();
		ddl_int.DataBind();
		// ddl_int.Items.Insert(0, new ListItem("Select an Intensive",""));
		
		DataTable dt = PDAL.getPreclearFromId( int.Parse(vPcId));

		//JUST TO GET THE ADDO ID 
		for (int j = 0; j < dt.Rows.Count; j++)
		{
			for (int i = 0; i < dt.Columns.Count; i++)    
			{    
				if(dt.Columns[i].ColumnName=="addo_id"){
					vAddoId = dt.Rows[j].ItemArray[i].ToString();
				}
			}
		}
		
		// HeaderText.Text = HeadText.Text;
		// HeaderText.Text += " (" + ddl_pc.SelectedItem.Text + ")";
		
		// PASS THE DATA TO FILTER SEARCH 
		// LoadData();
		
	}	

	protected void OpenLog(object sender, EventArgs e)
	{
		// ClearAddoModal();
		
		if(vPcId=="0"){
			AlertText.Text = "You must first select a Preclear before logging a session!";
			ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAlert();", true);
		}
		else {
			ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openLog();", true);
		}

		// addonameid.Text = txtAddoSearch.Text;
		ddl_auditor.Focus();
		session_date_ID.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");   
		
	}
	
	public void btnLog_Click(Object sender, EventArgs e)
	{				

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string id = String.Format("{0}", 		Request.Form["id"]);	
			string ssnDate = String.Format("{0}", 	Request.Form["session_date_ID"]);	
			string auditor = String.Format("{0}", 	Request.Form["ddl_auditor"]);	
			string intensive = String.Format("{0}", Request.Form["ddl_int"]);				
			string type = String.Format("{0}", 		Request.Form["typeID"]);	

			string ssnHr = String.Format("{0}", 	Request.Form["session_hr_ID"]);				
			string ssnMin = String.Format("{0}", 	Request.Form["session_min_ID"]);				
			int ssnHrInt = Int32.Parse(ssnHr);
			int ssnMinInt = Int32.Parse(ssnMin);
			int ssnTotMin = (ssnHrInt * 60) + ssnMinInt;
			
			// ErrorText.Text = "ssnTotMin = " + ssnTotMin.ToString();

			string adminHr = String.Format("{0}", 	Request.Form["admin_hr_ID"]);				
			string adminMin = String.Format("{0}", 	Request.Form["admin_min_ID"]);	
			int adminHrInt = Int32.Parse(adminHr);
			int adminMinInt = Int32.Parse(adminMin);
			int adminTotMin = (adminHrInt * 60) + adminMinInt;
			
			// ErrorText.Text = "adminTotMin = " + adminTotMin.ToString();

			string orgid		= "";
			
			if(OrgText.Text=="Day"){
				orgid 	 = System.Configuration.ConfigurationManager.AppSettings["orgid_day"];
			} else {
				orgid 	 = System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"];
			}
			
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into pc_session(pc_id, session_date, auditor_id, intensive_id, session_minutes, admin_minutes, type, weekend, org, org_id, addo_id, date_created, date_modified) "
							 + "VALUES (@pc_id, @session_date, @auditor_id, @intensive_id, @session_minutes, @admin_minutes, @type, @weekend, @org, @org_id, @addo_id, getdate(), getdate())";

				try
				{		
					using(SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@pc_id",				vPcId);
						command.Parameters.AddWithValue("@auditor_id",			auditor);
						command.Parameters.AddWithValue("@intensive_id",		intensive);
						command.Parameters.AddWithValue("@session_minutes",		ssnTotMin);
						command.Parameters.AddWithValue("@admin_minutes",		adminTotMin);
						command.Parameters.AddWithValue("@type",				type);
						command.Parameters.AddWithValue("@weekend",				lblWeekending.Text);
						command.Parameters.AddWithValue("@org",					OrgText.Text);
						command.Parameters.AddWithValue("@org_id",				orgid);
						command.Parameters.AddWithValue("@addo_id",				vAddoId);

						if (ssnDate != ""){ 
							command.Parameters.AddWithValue("@session_date", ssnDate);
						} else { 
							command.Parameters.AddWithValue("@session_date", DateTime.Now); 
						}						

						// if (weStarted != ""){ 
							// command.Parameters.AddWithValue("@we_started", weStarted);
						// } else { 
							// command.Parameters.AddWithValue("@we_started", DBNull.Value); 
						// }						

						// if (weCompleted != ""){ 
							// command.Parameters.AddWithValue("@we_completed", weCompleted);
						// } else { 
							// command.Parameters.AddWithValue("@we_completed", DBNull.Value); 
						// }						

						connection.Open();

						int result = command.ExecuteNonQuery();

						// ClearModal();
						
						if(result < 0){
							ErrorText.Text = "Error inserting data into Database!";
						}
						connection.Close();
						
					}				
				}
				catch (SqlException ex)
				{
					ErrorText.Text = ex.ToString();
				}
			}
			
			//NOW UPDATE THE INTENSIVE THAT MINUTES WERE JUST USED FROM
			
			//TODO: IF THE INTENSIVE IS USED UP, WORK OUT HOW TO INDICATE THIS WITH THE STATUS 
			
			
		} 
		LoadData();
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

    public void BtnSearch_Click(object sender, EventArgs e)
    {
		// searchText = ddlSearchGI.Text;
		// searchCol = txtBIS.Text;
		// DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		
    }

	protected void text_change(object sender, EventArgs e)
	{
		// ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE pc_session SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
	}

    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET status = @TEXT, scheduled = getdate(), weekend = getdate() + ( 5 - datepart(dw,getdate()) + 7) % 7  WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(searchCol != "" && searchText != ""){
			DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		}
		else {
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
		}
    }

	protected void Selection_Change(object sender, EventArgs e)
	{
		DropDownList ddlCat = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET reg_cat_id = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlCat.Text);
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

	}

	protected void Display(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		
		// TextBox tb = (TextBox)row.FindControl("name");
		// string vTxt = tb.Text; // get the value from TextBox		
		// lblnameid.Text = vTxt;

		// tb = (TextBox)row.FindControl("reg");
		// vTxt = tb.Text; // get the value from TextBox		
		// regid.Text = vTxt;

		// DropDownList dl = (DropDownList)row.FindControl("ddlStatus");
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
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
		
	}
	
	protected void DeleteRow(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		id.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteModal();", true);
		
	}
 	
	public void btnArchive_Click(Object sender, EventArgs e)
	{				
	}
	
	public void btnDelete_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["id"]);	
			string sqlCommandStatement = String.Format("DELETE FROM pc_session WHERE id='{0}'", id );									
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
			LoadData();
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
		
		LoadData();

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
