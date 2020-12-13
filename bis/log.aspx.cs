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
	static string vCategory = "LineUp";
	static string vCat = "LineUp";
	static string deleteID;
	static string filter;
	static int filterOn = 0;

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
				DataTable regList = DAL.bis("config"); 
				ddlReg.DataSource = regList;
				ddlReg.DataTextField = regList.Columns["desc1"].ToString();
				ddlReg.DataValueField = regList.Columns["desc1"].ToString();
				ddlReg.DataBind();

				
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
		HeadText.Text = "Combined BIS";
		HeaderText.Text = "Bodies In The Shop";
		ErrorText.Visible = false;
		
		OrgText.Visible = true;
		
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
		ErrorText.Text = "";

		DateTime nextThursday = DAL.GetNextWeekday(DateTime.Now, DayOfWeek.Thursday);
		lblWeekending.Text = nextThursday.ToString("dd-MMM-yyyy");   

		String s = Request.QueryString["filter"];
		if (s!=null){
			FilterArea(s);
		} else {
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
		}
		
		PopulateBISAreas();
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

	private void PopulateBISAreas()
	{
		DataTable dt = new DataTable();		
		dt = DAL.ReportBISArea("");

		div6d.Text = "";
		lid.Text = "";
		acadd.Text = "";
		hgcd.Text = "";
		ped.Text = "";
		purifd.Text = "";
		srdd.Text = "";
		stccd.Text = "";
		dnd.Text = "";
		knowd.Text = "";
		internd.Text = "";
		hqsd.Text = "";
		div6f.Text = "";
		lif.Text = "";
		acadf.Text = "";
		hgcf.Text = "";
		pef.Text = "";
		puriff.Text = "";
		srdf.Text = "";
		stccf.Text = "";
		dnf.Text = "";
		knowf.Text = "";
		internf.Text = "";
		hqsf.Text = "";

			
		foreach(DataRow row in dt.Rows)
		{
			if(row["org"].ToString()=="Day"){
				if(row["area"].ToString()=="DIV6"){
					div6d.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="LI"){
					lid.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="ACAD"){
					acadd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="HGC"){
					hgcd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="PE"){
					ped.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="PURIF"){
					purifd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="SRD"){
					srdd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="STCC"){
					stccd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="DN"){
					dnd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="KNOW"){
					knowd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="INTERN"){
					internd.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="HQS"){
					hqsd.Text = row["cnt"].ToString();
				}
			}else{
				if(row["area"].ToString()=="DIV6"){
					div6f.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="LI"){
					lif.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="ACAD"){
					acadf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="HGC"){
					hgcf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="PE"){
					pef.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="PURIF"){
					puriff.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="SRD"){
					srdf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="STCC"){
					stccf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="DN"){
					dnf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="KNOW"){
					knowf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="INTERN"){
					internf.Text = row["cnt"].ToString();
				} else if(row["area"].ToString()=="HQS"){
					hqsf.Text = row["cnt"].ToString();
				}
			}
		}
		
		if(OrgText.Text=="Day"){
		
			div6f.Text = "";
			lif.Text = "";
			acadf.Text = "";
			hgcf.Text = "";
			pef.Text = "";
			puriff.Text = "";
			srdf.Text = "";
			stccf.Text = "";
			dnf.Text = "";
			knowf.Text = "";
			internf.Text = "";
			hqsf.Text = "";
		
		} else if (OrgText.Text=="Fdn"){

			div6d.Text = "";
			lid.Text = "";
			acadd.Text = "";
			hgcd.Text = "";
			ped.Text = "";
			purifd.Text = "";
			srdd.Text = "";
			stccd.Text = "";
			dnd.Text = "";
			knowd.Text = "";
			internd.Text = "";
			hqsd.Text = "";
					
		} else if (OrgText.Text=="Combined"){

			div6f.Text += " (Fdn)";
			lif.Text += " (Fdn)";
			acadf.Text += " (Fdn)";
			hgcf.Text += " (Fdn)";
			pef.Text += " (Fdn)";
			puriff.Text += " (Fdn)";
			srdf.Text += " (Fdn)";
			stccf.Text += " (Fdn)";
			dnf.Text += " (Fdn)";
			knowf.Text += " (Fdn)";
			internf.Text += " (Fdn)";
			hqsf.Text += " (Fdn)";

			div6d.Text += " (Day)";
			lid.Text += " (Day)";
			acadd.Text += " (Day)";
			hgcd.Text += " (Day)";
			ped.Text += " (Day)";
			purifd.Text += " (Day)";
			srdd.Text += " (Day)";
			stccd.Text += " (Day)";
			dnd.Text += " (Day)";
			knowd.Text += " (Day)";
			internd.Text += " (Day)";
			hqsd.Text += " (Day)";

		}
	}

	public void FilterNamed(string area)
	{
		searchText = area;
		searchCol = "Area";
		DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		
	}
		
	public void FilterArea(string v)
	{

		//ADD THE button pressed LOOK TO THE INFO BOX
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
		}
		if(v=="SRD"){
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
		}
		if(v=="HGC"){
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
		}
		if(v=="ACAD"){
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
		}
		if(v=="DIV6"){
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
		}
		if(v=="GAK"){
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
		}
		
		searchText = "Area";
		searchCol = v;
		HeaderText.Text = "Bodies In The Shop";
		HeaderText.Text += " (" + v + ")";
		
		if(filterOn==1){
			DataGrid_Load(DAL.FilterBIS(OrgText.Text, searchCol, vCat), "reg");
		}
		else {
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
			HeaderText.Text = "Bodies In The Shop";
		}
	}
		
    protected void LinkButton_Command(Object sender, EventArgs e) 
      {
		var v = ((LinkButton)sender).CommandArgument;
		//ErrorText.Text = "You chose: " + v;

		HeaderText.Text = "Bodies In The Shop";
		
		searchText = "Area";
		searchCol = v;
		
		HeaderText.Text += " (" + v + ")";
		
		DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		
		UpdatePanels();
		
      }

    protected void LinkButtonArea_Command(Object sender, EventArgs e) 
    {
		var v = ((LinkButton)sender).CommandArgument;
		filter = (string)v;
		
		FilterArea(filter);

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
		}
		else {
			HeaderText.Text = "Bodies In The Shop";
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
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
		}
		else {
			HeaderText.Text = "Bodies In The Shop";
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
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
		}
		else {
			HeaderText.Text = "Bodies In The Shop";
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
		}

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
			
			GridView gv = (GridView)(sender as Control).Parent.FindControl("GridViewInTheShop");		
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

				DropDownList area = (DropDownList)row.FindControl("ddlReg");				
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
			LoadLastWeek(we);
		}
	}

	public void LoadLastWeek(string weekend)
	{		

		if(weekend != null){
	
			//PARSE THE DATE AND THEN FORMAT IS YYYY-MM-d
			string formattedWE = DateTime.Parse(weekend).ToString("yyyy-MM-d");
		
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{

				//FIRST MOVE ALL LAST WEEK RECORDS INTO FALLEN OFF
				string sqlCommandStatement = 
					String.Format( "UPDATE bis set status = 'Named' where reg_cat_id = 'LineUp' and status = 'Last Week'"
								+  "AND (weekend <> '{0}' or weekend is null) and org = '{1}' ", weekend, OrgText.Text);
				
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
				
				//ADD ALL OF LAST WEEK RECORDS
				string sqlCommandStatement2 = 
					String.Format( "INSERT INTO bis (addo_id, name, area, service, reg, fsm, phone, email, org, status, line, reg_cat_id, last_week_bis, consecutive_weeks, notes, weekend) "
								+ "SELECT addo_id, name, area, service, reg, fsm, phone, email, org, 'Last Week' as  status, line, 'LineUp' as reg_cat_id, 1 as last_week_bis, isnull(consecutive_weeks,0) + 1 as consecutive_weeks, '{1} LAST BIS' as note, weekend "
								+ "FROM bis "
								+ "WHERE weekend = '{0}' and org = '{2}' and reg_cat_id = 'Archive' and status = 'In The Shop' "
								+ "AND org + name + area not in ( select org + name + area from bis where reg_cat_id = 'LineUp' and status in ('In The Shop', 'Scheduled', 'Last Week') and org = '{2}' )", weekend, formattedWE, OrgText.Text);

				try
				{		
					using (SqlConnection conn = databaseConnection.CreateSqlConnection())
					{
						conn.Open();
						using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement2, conn))
						{
							Cmd.ExecuteNonQuery();						
						}
						conn.Close();
					}
				}
				catch (SqlException ex)
				{
					ErrorText.Text += sqlCommandStatement2;
					ErrorText.Text += ex.ToString();
				}															

				//UPDATED SCHEDULED WITH LAST BIS DATE IF IT WAS LAST WEEK 
				string sqlCommandUpdate = 
					String.Format( "UPDATE bis set note = '{1} LAST BIS' where reg_cat_id = 'LineUp' and status = 'Scheduled' "
								+  "AND org + name in ( select org + name from bis where reg_cat_id = 'Archive' and status in ('In The Shop') and weekend = '{0}' and org = '{2}' ) and org = '{2}' ", weekend, formattedWE, OrgText.Text);
				
				try
				{		
					using (SqlConnection conn = databaseConnection.CreateSqlConnection())
					{
						conn.Open();
						using (SqlCommand Cmd = new SqlCommand(sqlCommandUpdate, conn))
						{
							Cmd.ExecuteNonQuery();						
						}
						conn.Close();
					}
				}
				catch (SqlException ex)
				{
					ErrorText.Text += sqlCommandUpdate;
					ErrorText.Text += ex.ToString();
				}		
				
				//THEN HANDLE DUPLICATES
				string sqlCommandStatement3 = String.Format( "delete from bis where id in (select MAX(ID) from bis where reg_cat_id = 'LineUp' GROUP BY ORG, NAME, AREA HAVING COUNT(1) > 1 )", weekend);
				try
				{	using (SqlConnection conn = databaseConnection.CreateSqlConnection())
					{	conn.Open();
					
						using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement3, conn))
						{ 
							Cmd.ExecuteNonQuery();	
							Cmd.ExecuteNonQuery();	
							Cmd.ExecuteNonQuery();	
						}
						conn.Close();
					}
				}
				catch (SqlException ex)
				{
					ErrorText.Text += sqlCommandStatement3;
					ErrorText.Text += ex.ToString();
				}			
			} 
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
		}
    }	

	public void UpdateLastWeek(string weekend)
	{		

		if(weekend != null){
	
			//PARSE THE DATE AND THEN FORMAT IS YYYY-MM-d
			string formattedWE = DateTime.Parse(weekend).ToString("yyyy-MM-d");
		
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				
				//UPDATE ALL "LAST WEEK" RECORDS TO "NAMED"
				string sqlCommandStatement = 
					String.Format( "UPDATE bis set status = 'Named' where reg_cat_id = 'LineUp' and status = 'Last Week' "
								+  "AND org + name not in ( select org + name from bis where reg_cat_id = 'LineUp' and status in ('Named') and org = '{1}' ) and org = '{1}' ", weekend, OrgText.Text);
				
				try
				{		
					using (SqlConnection conn = databaseConnection.CreateSqlConnection())
					{
						conn.Open();
						using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement, conn))
						{
							AlertText.Text += sqlCommandStatement;
							ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAlert();", true);
							// Cmd.ExecuteNonQuery();						
						}
						conn.Close();
					}
				}
				catch (SqlException ex)
				{
					ErrorText.Text += sqlCommandStatement;
					ErrorText.Text += ex.ToString();
				}			

				//ADD ALL OF LAST WEEK RECORDS
				string sqlCommandStatement2 = 
					String.Format( "INSERT INTO bis (addo_id, name, area, service, reg, fsm, phone, email, org, status, line, reg_cat_id, last_week_bis, consecutive_weeks, notes) "
								 + "SELECT addo_id, name, area, service, reg, fsm, phone, email, org, 'Last Week' as  status, line, 'LineUp' as reg_cat_id, 1 as last_week_bis, isnull(consecutive_weeks,0) + 1 as consecutive_weeks, '{1} LAST BIS' as note "
								 + "FROM bis "
								 + "WHERE weekend = '{0}' and org = '{2}' and reg_cat_id = 'Archive' and status = 'In The Shop' "
								 + "AND org + name not in ( select org + name from bis where reg_cat_id = 'LineUp' and status in ('In The Shop', 'Scheduled', 'Last Week') and org = '{2}' )", weekend, formattedWE, OrgText.Text);

				try
				{		
					using (SqlConnection conn = databaseConnection.CreateSqlConnection())
					{
						conn.Open();
						using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement2, conn))
						{
							
							// AlertText.Text += sqlCommandStatement2;
							// ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAlert();", true);
							// Cmd.ExecuteNonQuery();						
							
						}
						conn.Close();
					}
				}
				catch (SqlException ex)
				{
					ErrorText.Text += sqlCommandStatement2;
					ErrorText.Text += ex.ToString();
				}															
				

				//UPDATED SCHEDULED WITH LAST BIS DATE IF IT WAS LAST WEEK 
				string sqlCommandUpdate = 
					String.Format( "UPDATE bis set note = '{1} LAST BIS' where reg_cat_id = 'LineUp' and status = 'Scheduled' "
								+  "AND org + name in ( select org + name from bis where reg_cat_id = 'Archive' and status in ('In The Shop') and weekend = '{0}' ) and org = '{2}' ", weekend, formattedWE, OrgText.Text);
				
				try
				{		
					using (SqlConnection conn = databaseConnection.CreateSqlConnection())
					{
						conn.Open();
						using (SqlCommand Cmd = new SqlCommand(sqlCommandUpdate, conn))
						{
							// Cmd.ExecuteNonQuery();						
						}
						conn.Close();
					}
				}
				catch (SqlException ex)
				{
					ErrorText.Text += sqlCommandUpdate;
					ErrorText.Text += ex.ToString();
				}		
				
				//THEN HANDLE DUPLICATES
				string sqlCommandStatement3 = String.Format( "delete from bis where id in (select MAX(ID) from bis where reg_cat_id = 'LineUp' GROUP BY ORG, NAME, AREA HAVING COUNT(1) > 1 )", weekend);
				try
				{	using (SqlConnection conn = databaseConnection.CreateSqlConnection())
					{	conn.Open();
					
						using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement3, conn))
						{ 
							// Cmd.ExecuteNonQuery();	
							// Cmd.ExecuteNonQuery();	
							// Cmd.ExecuteNonQuery();	
						}
						conn.Close();
					}
				}
				catch (SqlException ex)
				{
					ErrorText.Text += sqlCommandStatement3;
					ErrorText.Text += ex.ToString();
				}			
			} 
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
		}
    }	

	protected void OpenModal(object sender, EventArgs e)
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
		searchCol = txtBIS.Text;
		DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");		
    }

	protected void text_change(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
		
		UpdatePanels();
	}

	protected void text_change_date(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);	

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");
		
		UpdatePanels();		
		
	}
	
	protected void text_change_addo(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);		
		SqlCmd(sqlCommandStatement, id, text.Text);
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");

		UpdatePanels();		

	}

    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;

		string oldStatus = myStatus.Text;

		NamedLbl.Text += "oldStatus = " + oldStatus + "</BR>";
		NamedLbl.Text += "newStatus = " + ddlStatus.Text + "</BR>";
		
		string sqlCommandStatement = "";
	
		sqlCommandStatement = "UPDATE bis SET status = @TEXT, scheduled = getdate(), weekend = getdate() WHERE id=@ID";
		
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.get_log(vCat, OrgText.Text), "reg");

		// if (ddlStatus.Text=="In The Shop")
			// UpdatePanelStarted.Update();

		// if (ddlStatus.Text=="Named")
			// UpdatePanelNamed.Update();

		// if (ddlStatus.Text=="Scheduled")
			// UpdatePanelScheduled.Update();

		// if (ddlStatus.Text=="Last Week")
			// UpdatePanelLastWeek.Update();

		// UpdatePanelTotal.Update();
		
		UpdatePanels();		

    }

    protected void Selection_Change_ScheduledFor(object sender, EventArgs e)
    {
        DropDownList ddlSchedule = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET scheduled_type = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlSchedule.Text);		
    }

    protected void Selection_Change_Line(object sender, EventArgs e)
	{
		DropDownList ddlLine = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET line = @TEXT WHERE id=@ID";
		if(ddlLine.Text == "")
			SqlCmd(sqlCommandStatement, id, "ddlLineNull");
		else
			SqlCmd(sqlCommandStatement, id, ddlLine.Text);
		
		UpdatePanels();		
		
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
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");

		UpdatePanels();		
		
	}

	protected void Display(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		
		TextBox tb = (TextBox)row.FindControl("name");
		string vTxt = tb.Text; // get the value from TextBox		
		lblnameid.Text = vTxt;
		
		tb = (TextBox)row.FindControl("service");
		vTxt = tb.Text; // get the value from TextBox		
		serviceid.Text = vTxt;

		tb = (TextBox)row.FindControl("reg");
		vTxt = tb.Text; // get the value from TextBox		
		regid.Text = vTxt;

		DropDownList dl = (DropDownList)row.FindControl("ddlReg");
		ddlReg.Text = dl.Text;

		dl = (DropDownList)row.FindControl("ddlStatus");
		vTxt = dl.Text; // get the value from TextBox		
		statusid.Text = vTxt;

		dl = (DropDownList)row.FindControl("ddlOrg");
		vTxt = dl.Text; // get the value from TextBox		
		orgid.Text = vTxt;

		tb = (TextBox)row.FindControl("notes");
		vTxt = tb.Text; // get the value from TextBox		
		notesid.Text = vTxt;
	
		string vId = row.Cells[0].Text;		
		id.Text = vId; 
		
		ModalPopupExtender3.Show();
	}

	public string MapBISArea(string bisArea)
	{

		string area = "";
		
		if(bisArea==null){
			area = "";
		} else if(bisArea=="KNOW"){
			area = "GAK";
		} else if(bisArea=="DN"){
			area = "DIV6";
		} else if(bisArea=="HQS"){
			area = "DIV6";
		} else if(bisArea=="INTERN"){
			area = "ACAD";
		} else if(bisArea=="LI"){
			area = "DIV6";
		} else if(bisArea=="PE"){
			area = "DIV6";
		} else if(bisArea=="STCC"){
			area = "DIV6";
		} else if(bisArea=="- UNK"){
			area = "";
		} else {
			area = bisArea;
		}
		
		return area;
		
	}

	protected void Click_AddToPossibleGI(object sender, EventArgs e)
	{

		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		AlertPanel.Text = "Successfully added to GI Possible for the Week!</br>";
		
		string addoid = "";
		string org = "";
		string name = "";
		string area = "";
		string service = "";
		
		TextBox tb = (TextBox)row.FindControl("name");
		string vTxt = tb.Text; // get the value from TextBox		
		name = vTxt;
		
		DropDownList dl = (DropDownList)row.FindControl("ddlReg");
		area = MapBISArea(dl.Text);
		
		tb = (TextBox)row.FindControl("service");
		vTxt = tb.Text; // get the value from TextBox		
		service = vTxt;

		dl = (DropDownList)row.FindControl("ddlOrg");
		vTxt = dl.Text; // get the value from TextBox		
		org = vTxt;
		
		addoid = row.Cells[1].Text;		
		try{
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into reg(reg_cat_id, addo_id, name, status, service, rank, appt, tm, org) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, getdate(), getdate(), @org)";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
					command.Parameters.AddWithValue("@addo_id",			addoid);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@status",			"Possible");
					command.Parameters.AddWithValue("@service",			service);
					command.Parameters.AddWithValue("@area",			area);
					command.Parameters.AddWithValue("@org",				org);

					connection.Open();
					int result = command.ExecuteNonQuery();

					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					} else {
						ModalPopupExtender2.Show();		
					}
					connection.Close();
				}
			}	
		} catch(Exception ex){
			AlertPanel.Text = "ERROR: " + ex.ToString();
			ModalPopupExtender2.Show();		
		}			
	}
	
	protected void Click_AddToInStarted(object sender, EventArgs e)
	{

		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		AlertPanel.Text = "Successfully added to In and Started!</br>";
		
		string addoid = "";
		string org = "";
		string name = "";
		string area = "";
		string service = "";
		
		TextBox tb = (TextBox)row.FindControl("name");
		string vTxt = tb.Text; // get the value from TextBox		
		name = vTxt;
		
		DropDownList dl = (DropDownList)row.FindControl("ddlReg");
		area = dl.Text;

		tb = (TextBox)row.FindControl("service");
		vTxt = tb.Text; // get the value from TextBox		
		service = vTxt;

		dl = (DropDownList)row.FindControl("ddlOrg");
		vTxt = dl.Text; // get the value from TextBox		
		org = vTxt;

		addoid = row.Cells[1].Text;		
		
		using(SqlConnection connection = databaseConnection.CreateSqlConnection())
		{
			String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, scheduled, weekend, org) "
						 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, getdate(), getdate(), @org)";
						 
			using(SqlCommand command = new SqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@reg_cat_id",		"In and Started");
				command.Parameters.AddWithValue("@addo_id",			addoid);
				command.Parameters.AddWithValue("@name",			name);
				command.Parameters.AddWithValue("@status",			"In and Started");
				command.Parameters.AddWithValue("@service",			service);
				command.Parameters.AddWithValue("@area",			area);
				command.Parameters.AddWithValue("@org",				org);

				connection.Open();
				int result = command.ExecuteNonQuery();

				if(result < 0){
					
					ErrorText.Text = "Error inserting data into Database!";
					
				} else {
					
					ModalPopupExtender2.Show();		
					
				}
				///Response.Redirect("log.aspx");
				connection.Close();
			}
		}	

		
		//CHECK IF ITS A PURIF NAME AND IF SO ADD TO THE PURIF LOG AS WELL

		if(area=="PURIF"){

			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, scheduled, weekend, org) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, getdate(), getdate(), @org)";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@reg_cat_id",		"Purif");
					command.Parameters.AddWithValue("@addo_id",			addoid);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@status",			"Purif Start");
					command.Parameters.AddWithValue("@service",			service);
					command.Parameters.AddWithValue("@area",			area);
					command.Parameters.AddWithValue("@org",				org);

					connection.Open();
					int result = command.ExecuteNonQuery();

					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
					
				}
			}	
			
		}
		
	}
	
	protected void Click_AddToNamedInStarted(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

		AlertPanel.Text = "Successfully added to (Named) In and Started!</br>";
		
		string addoid = "";
		string org = "";
		string name = "";
		string area = "";
		string service = "";
		
		TextBox tb = (TextBox)row.FindControl("name");
		string vTxt = tb.Text; // get the value from TextBox		
		name = vTxt;
		
		DropDownList dl = (DropDownList)row.FindControl("ddlReg");
		area = dl.Text;

		tb = (TextBox)row.FindControl("service");
		vTxt = tb.Text; // get the value from TextBox		
		service = vTxt;

		dl = (DropDownList)row.FindControl("ddlOrg");
		vTxt = dl.Text; // get the value from TextBox		
		org = vTxt;

		addoid = row.Cells[1].Text;		
		
		using(SqlConnection connection = databaseConnection.CreateSqlConnection())
		{
			String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, scheduled, weekend, org) "
					     + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, getdate(), getdate(), @org)";
						 
			connection.Open();
			try{
				using(SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@reg_cat_id",		"In and Started");
					command.Parameters.AddWithValue("@addo_id",			addoid);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@status",			"Named");
					command.Parameters.AddWithValue("@service",			service);
					command.Parameters.AddWithValue("@area",			area);
					command.Parameters.AddWithValue("@org",				org);

					int result = command.ExecuteNonQuery();

					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					} else {
						ModalPopupExtender2.Show();		
					}
				}
			}
			catch(Exception x) {
				ErrorText.Text = "Caught Exception: " + x;				
			}
			connection.Close();
		}		
	}
	
	protected void Click_AddToCompResign(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

		AlertPanel.Text = "Successfully added to the Comp Resign Log!";
		
		string addoid = "";
		string org = "";
		string name = "";
		string area = "";
		string service = "";
		
		TextBox tb = (TextBox)row.FindControl("name");
		string vTxt = tb.Text; // get the value from TextBox		
		name = vTxt;
		
		DropDownList dl = (DropDownList)row.FindControl("ddlReg");
		area = dl.Text;

		tb = (TextBox)row.FindControl("service");
		vTxt = tb.Text; // get the value from TextBox		
		service = vTxt;

		dl = (DropDownList)row.FindControl("ddlOrg");
		vTxt = dl.Text; // get the value from TextBox		
		org = vTxt;

		addoid = row.Cells[1].Text;		
		
		using(SqlConnection connection = databaseConnection.CreateSqlConnection())
		{
			String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, scheduled, weekend, org) "
						 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, getdate(), getdate(), @org)";
						 
			using(SqlCommand command = new SqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@reg_cat_id",		"Comp Resign");
				command.Parameters.AddWithValue("@addo_id",			addoid);
				command.Parameters.AddWithValue("@name",			name);
				command.Parameters.AddWithValue("@status",			"Comp Resign");
				command.Parameters.AddWithValue("@service",			service);
				command.Parameters.AddWithValue("@area",			area);
				command.Parameters.AddWithValue("@org",				org);

				connection.Open();
				int result = command.ExecuteNonQuery();

				if(result < 0){
					ErrorText.Text = "Error inserting data into Database!";
				} else {
						ModalPopupExtender2.Show();		
				}
				//Response.Redirect("log.aspx");
				
			}
		}		
	}

	protected void Click_AddToNamedCompResign(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

		AlertPanel.Text = "Successfully added to the (Named) Comp Resign Log!</br>";
		
		string addoid = "";
		string org = "";
		string name = "";
		string area = "";
		string service = "";
		
		TextBox tb = (TextBox)row.FindControl("name");
		string vTxt = tb.Text; // get the value from TextBox		
		name = vTxt;
		
		DropDownList dl = (DropDownList)row.FindControl("ddlReg");
		area = dl.Text;

		tb = (TextBox)row.FindControl("service");
		vTxt = tb.Text; // get the value from TextBox		
		service = vTxt;

		dl = (DropDownList)row.FindControl("ddlOrg");
		vTxt = dl.Text; // get the value from TextBox		
		org = vTxt;

		addoid = row.Cells[1].Text;		
		
		using(SqlConnection connection = databaseConnection.CreateSqlConnection())
		{
			String query = "INSERT into bis(reg_cat_id, addo_id, name, status, service, area, scheduled, weekend, org) "
						 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @area, getdate(), getdate(), @org)";
						 
			using(SqlCommand command = new SqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@reg_cat_id",		"Comp Resign");
				command.Parameters.AddWithValue("@addo_id",			addoid);
				command.Parameters.AddWithValue("@name",			name);
				command.Parameters.AddWithValue("@status",			"Named");
				command.Parameters.AddWithValue("@service",			service);
				command.Parameters.AddWithValue("@area",			area);
				command.Parameters.AddWithValue("@org",				org);

				connection.Open();
				int result = command.ExecuteNonQuery();

				if(result < 0){
					ErrorText.Text = "Error inserting data into Database!";
				} else {
						ModalPopupExtender2.Show();		
				}
			}
		}		
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
			
			UpdatePanels();
		} 
		ModalPopupExtender1.Hide();
    }

	protected void Button1_Click(object sender, EventArgs e)
	{
		System.Threading.Thread.Sleep(5000);
	}
	
	public void btnUpdate_Click(Object sender, EventArgs e)
	{				

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			// int id  = Request.Form["id"];	
			string id = String.Format("{0}", 		Request.Form["id"]);	
			string name = String.Format("{0}", 		Request.Form["lblnameid"]);	
			string service = String.Format("{0}", 	Request.Form["serviceid"]);	
			string area = String.Format("{0}", 		Request.Form["ddlReg"]);	
			string reg = String.Format("{0}", 		Request.Form["regid"]);	
			string status = String.Format("{0}", 	Request.Form["statusid"]);				
			string fsm = String.Format("{0}", 		Request.Form["fsmid"]);				
			string org = String.Format("{0}", 		Request.Form["orgid"]);				
			string notes = String.Format("{0}", 	Request.Form["notesid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			string sqlCommandStatement = String.Format("UPDATE bis SET name = '{0}', " + 
															" service 		= '{1}', " + 
															" reg 			= '{2}', " + 
															" status	 	= '{3}', " + 
															" fsm		 	= '{4}', " + 
															" org		 	= '{5}', " + 
															" area		 	= '{6}', " + 
															" notes	 		= '{7}' " + 
															" WHERE id='{8}'", 
															name.Replace("'", "''"), 
															service.Replace("'", "''"), 
															reg.Replace("'", "''"), 
															status, 
															fsm.Replace("'", "''"), 
															org,
															area,
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
				DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
			} 
			
		UpdatePanels(); 
			
    }

	public void UpdatePanels()
	{
		UpdatePanelTotal.Update();
		UpdatePanelStarted.Update();
		UpdatePanelScheduled.Update();
		UpdatePanelNamed.Update();
		UpdatePanelLastWeek.Update();
		
	}
	
    protected void Selection_Change_Reg(object sender, EventArgs e)
    {
        DropDownList ddlReg = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET area = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlReg.Text);	
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
		
		UpdatePanels(); 
		
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

		PopulateBISAreas();		
		
		SchedLbl.Text = "";
		LastWeekLbl.Text = "";
		NamedLbl.Text = "";
		// BothBoardLblb.Text = "";
		
		try // TOTALS
		{
			var query = from c in dataTable.AsEnumerable() 
					   where c.Field<string>("status") == "In The Shop" && c.Field<string>("reg_cat_id") == "LineUp"
					  select c ;	
			
			string ResultBothIn = "0";
			string ResultNamed = "0";
			string ResultLastWeek = "0";
			string ResultBothScheduled = "0";
			string ResultBothInSched = "0";
			string ResultBothInSchedNamed  = "0";
			
			string ResultBothOn = "0";
			string ResultDayBIS = "0";
			string ResultDayOn = "0";
			string ResultDaySched = "0";
			string ResultDayNamed = "0";
			string ResultDayLastWeek = "0";
			string ResultDayInv = "0";

			string ResultFdnIn = "0";
			string ResultFdnOn = "0";
			string ResultFdnConf = "0";
			string ResultFdnBIS = "0";
			string ResultFdnNamed = "0";
			string ResultFdnLastWeek = "0";
					  
			var cmdBIS 		 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'In The Shop' and reg_cat_id = 'LineUp'";
			var cmdScheduled 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'Scheduled' and reg_cat_id = 'LineUp'";
			var cmdNamed 	 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'Named' and reg_cat_id = 'LineUp'";
			var cmdLastWeek	 	= "SELECT count(distinct org + name) AS total from bis WHERE status = 'Last Week' and reg_cat_id = 'LineUp'";

			var cmdBISDay 	 	= "SELECT count(distinct name) AS total from bis WHERE status = 'In The Shop' and reg_cat_id = 'LineUp' and org = 'Day'";
			var cmdScheduledDay  = "SELECT count(distinct name) AS total from bis WHERE status = 'Scheduled' and reg_cat_id = 'LineUp' and org = 'Day'";
			var cmdNamedD	 	 = "SELECT count(distinct name) AS total from bis WHERE status = 'Named' and reg_cat_id = 'LineUp' and org = 'Day'";
			var cmdLastWeekDay 	 = "SELECT count(distinct name) AS total from bis WHERE status = 'Last Week' and reg_cat_id = 'LineUp' and org = 'Day'";
			var cmdInSchedNamedD = "SELECT count(distinct name) AS total from bis WHERE status in ('In The Shop', 'Scheduled', 'Named') and reg_cat_id = 'LineUp' and org = 'Day'";
				
			var cmdBISFdn 		 = "SELECT count(distinct name) AS total from bis WHERE status = 'In The Shop' and reg_cat_id = 'LineUp' and org = 'Fdn'";
			var cmdScheduledFdn  = "SELECT count(distinct name) AS total from bis WHERE status = 'Scheduled' and reg_cat_id = 'LineUp' and org = 'Fdn'";
			var cmdNamedFdn 	 = "SELECT count(distinct name) AS total from bis WHERE status = 'Named' and reg_cat_id = 'LineUp' and org = 'Fdn'";
			var cmdLastWeekFdn 	 = "SELECT count(distinct name) AS total from bis WHERE status = 'Last Week' and reg_cat_id = 'LineUp' and org = 'Fdn'";
			var cmdInSchedF      = "SELECT count(distinct name) AS total from bis WHERE status in ('In The Shop', 'Scheduled') and reg_cat_id = 'LineUp' and org = 'Fdn'";
			var cmdInSchedNamedF = "SELECT count(distinct name) AS total from bis WHERE status in ('In The Shop', 'Scheduled', 'Named') and reg_cat_id = 'LineUp' and org = 'Fdn'";

			var cmdInSchedBoth   = "SELECT count(distinct org + name) AS total from bis WHERE status in ('In The Shop', 'Scheduled') and reg_cat_id = 'LineUp'";
			var cmdInSchedNamedBoth   = "SELECT count(1) AS total from bis WHERE status in ('In The Shop', 'Scheduled', 'Named', 'Last Week') and reg_cat_id = 'LineUp'";
			
			var cmdInvD 	= "SELECT count(distinct name) AS total from bis WHERE status = 'In The Shop' and org = 'Day' and reg_cat_id = 'LineUp' ";
			var cmd3 		= "SELECT count(1) AS total from bis WHERE reg_cat_id = 'LineUp' and status = 'In The Shop' and org = 'Fdn'";
			var cmd5 		= "SELECT count(distinct org + name) AS total from bis WHERE org = 'Day' and ((status in ('In The Shop', 'Scheduled') and reg_cat_id = 'LineUp') or (status = 'Scheduled' and rank = 'a' and reg_cat_id = 'LineUp'))";
			var cmd6 		= "SELECT count(distinct org + name) AS total from bis WHERE org = 'Fdn' and ((status in ('In The Shop', 'Scheduled') and reg_cat_id = 'LineUp') or (status = 'Scheduled' and rank = 'a' and reg_cat_id = 'LineUp'))";
				
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
				conn.Open();
				try{
					ResultBothIn = String.Format("{0:D}", cmdBIS);
					
					using (SqlCommand Cmd = new SqlCommand(cmdBIS, conn))
						{
							ResultBothIn = String.Format("{0:D}", Cmd.ExecuteScalar());
						}
				}
				catch(Exception e) {
					ErrorText.Text = e.ToString();
				}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdScheduled, conn))					
					{
						ResultBothScheduled = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdNamed, conn))					
					{
						ResultNamed = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdLastWeek, conn))					
					{
						ResultLastWeek = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInSchedBoth, conn))					
					{
						ResultBothInSched = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInSchedNamedBoth, conn))					
					{
						ResultBothInSchedNamed = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}


				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdBISDay, conn))					
					{
						ResultDayBIS = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdInvD, conn))					
					{
						ResultDayInv = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdScheduledDay, conn))					
					{
						ResultDaySched = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdNamedD, conn))					
					{
						ResultDayNamed = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdLastWeekDay, conn))					
					{
						ResultDayLastWeek = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdBISFdn, conn))					
					{
						ResultFdnBIS = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdScheduledFdn, conn))					
					{
						ResultFdnConf = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdLastWeekFdn, conn))					
					{
						ResultFdnLastWeek = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdNamedFdn, conn))					
					{
						ResultFdnNamed = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try {	
					using (SqlCommand Cmd = new SqlCommand(cmdInSchedF, conn))
					{
						ResultFdnIn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}	
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdScheduled, conn))
					{
						ResultBothOn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}
				try{
				using (SqlCommand Cmd = new SqlCommand(cmdInSchedNamedD, conn))
						{
					ResultDayOn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}
				try{
				using (SqlCommand Cmd = new SqlCommand(cmdInSchedNamedF, conn))
						{
					ResultFdnOn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}						
				conn.Close();
			}			

			if(OrgText.Text=="Combined"){
				
				BothBIS.Text = ResultBothIn;				
				BothScheduled.Text = ResultBothScheduled;				
				BothNamed.Text = ResultNamed;				
				BothLastWeek.Text = ResultLastWeek;				

				DayInv.Text = ResultDayInv + " (Day)";					
				DayConf.Text = ResultDaySched + " (Day)";	
				DayNamed.Text = ResultDayNamed + " (Day)";		
				DayLastWeek.Text = ResultDayLastWeek + " (Day)";	
				FdnInv.Text = ResultFdnBIS + " (Fdn)";	
				FdnConf.Text = ResultFdnConf + " (Fdn)";		
				FdnNamed.Text = ResultFdnNamed + " (Fdn)";		
				FdnLastWeek.Text = ResultFdnLastWeek + " (Fdn)";			
				
			} else if (OrgText.Text=="Day"){
				
				BothBIS.Text = ResultDayInv;				
				BothScheduled.Text = ResultDaySched;				
				BothNamed.Text = ResultDayNamed;				
				BothLastWeek.Text = ResultDayLastWeek;				
			
				DayInv.Text = "";				
				DayConf.Text = "";				
				DayNamed.Text = "";				
				DayLastWeek.Text = "";				
				FdnInv.Text = "";				
				FdnConf.Text = "";				
				FdnNamed.Text = "";				
				FdnLastWeek.Text = "";				
				
			} else if (OrgText.Text=="Fdn"){

				BothBIS.Text = ResultFdnBIS;				
				BothScheduled.Text = ResultFdnConf;				
				BothNamed.Text = ResultFdnNamed;				
				BothLastWeek.Text = ResultFdnLastWeek;				

				DayInv.Text = "";				
				DayConf.Text = "";				
				DayNamed.Text = "";				
				DayLastWeek.Text = "";				
				FdnInv.Text = "";				
				FdnConf.Text = "";				
				FdnNamed.Text = "";				
				FdnLastWeek.Text = "";				
				
			}

			var mySum = 0;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewInTheShop.DataSource = t2;
				GridViewInTheShop.DataBind();	
				
				// int count = t2.AsEnumerable().Count();
				// BothInLbl.Text = String.Format("{0:D}", count);					
				// BothInLbl.Text = String.Format("{0:D}", BothBIS.Text);					

				int count = t2
					.AsEnumerable()
					.Select(r => r.Field<string>("name") + r.Field<string>("org"))
					.Distinct()
					.Count();
					
				BothInLbl.Text = String.Format("{0:D}", count);		
				
				// int count = (from c in t2.AsEnumerable() 
						  // select c.org, c.name).Distinct().Count() ;	
				
				
			} else {
				GridViewInTheShop.DataSource = new DataTable();
				GridViewInTheShop.DataBind();
				BothInLbl.Text = String.Format("{0:D}", 0);					
			}

		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // SCHEDULED
		{
			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Scheduled" 
				where c.Field<string>("reg_cat_id") == "LineUp"
				select c ;

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewScheduled.DataSource = t2;
				GridViewScheduled.DataBind();				

				int count = t2.AsEnumerable().Count();
				SchedLbl.Text = String.Format("{0:D}", count);					
				
			} else {
				//ErrorTextA.Text = "No Data Found";	
				GridViewScheduled.DataSource = new DataTable();
				GridViewScheduled.DataBind();
				SchedLbl.Text = String.Format("{0:D}", 0);					
			}
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		} 
		try // LAST WEEK 
		{
			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Last Week" 
				where c.Field<string>("reg_cat_id") == "LineUp"
				select c ;

			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewLastWeek.DataSource = t2;
				GridViewLastWeek.DataBind();		
				
				int count = t2.AsEnumerable().Count();
				LastWeekLbl.Text = String.Format("{0:D}", count);					
				
			} else {
				GridViewLastWeek.DataSource = new DataTable();
				GridViewLastWeek.DataBind();
				LastWeekLbl.Text = String.Format("{0:D}", 0);					
			}
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}
		try // NAMED
		{

			var query = from c in dataTable.AsEnumerable() 
				where c.Field<string>("status") == "Named"  
				where c.Field<string>("reg_cat_id") == "LineUp"
				select c ;
			
			var mySum = 0;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewNamed.DataSource = t2;
				GridViewNamed.DataBind();

				int count = t2.AsEnumerable().Count();
				NamedLbl.Text = String.Format("{0:D}", count);					
				
			} else {
				GridViewNamed.DataSource = new DataTable();
				GridViewNamed.DataBind();

				NamedLbl.Text = String.Format("{0:D}", 0);					
				
			}

			// var cmd1 = "SELECT SUM(amount) AS total from bis WHERE reg_cat_id = 'lineup' and rank = 'b'";
			var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Scheduled' and reg_cat_id = 'LineUp'";
			string ResultFigure = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
				using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
						ResultFigure = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch{}							
				conn.Close();
				}
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
		GridView gv = sender as GridView;

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
		
		UpdatePanels(); 
		
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
