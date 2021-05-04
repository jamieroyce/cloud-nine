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
		Title = "Reg Tracking System";

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

		DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
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
		DataTable dt = new DataTable();		
		dt = DAL.ReportByArea("reg");

		//ErrorText.Text += "ReportByArea(reg) count=" + dt.Rows.Count;
		foreach(DataRow row in dt.Rows)
		{
			if(row["org"].ToString()=="Day"){
				if(row["line"].ToString()=="Resign"){
					resignDay.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="CF"){
					cfDay.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="Arrival"){
					arrivalDay.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="FSM"){
					fsmDay.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="Prospecting"){
					prospectingDay.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="Other"){
					otherDay.Text = String.Format("{0:C0} ", row["cnt"]);
				}
			}else{
				if(row["line"].ToString()=="Resign"){
					resignFdn.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="CF"){
					cfFdn.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="Arrival"){
					arrivalFdn.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="FSM"){
					fsmFdn.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="Prospecting"){
					prospectingFdn.Text = String.Format("{0:C0} ", row["cnt"]);
				} else if(row["line"].ToString()=="Other"){
					otherFdn.Text = String.Format("{0:C0} ", row["cnt"]);
				}
			}
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
		DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
        
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
		DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
        
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
		DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
        
    }

	public void FilterNamed(string area)
	{
		searchText = area;
		searchCol = "Line";
		DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		
	}
		
    protected void LinkButton_Command(Object sender, EventArgs e) 
    {
		var v = ((LinkButton)sender).CommandArgument;
		//ErrorText.Text = "You chose: " + v;

		HeaderText.Text = "Gross Income";
		
		//ADD THE button pressed LOOK TO THE INFO BOX
		if(v=="Resign"){
			string s = boxResign.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxResign.Attributes["class"] += " bg-info-gradient";		
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Arrival"){
			string s = boxArrival.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxArrival.Attributes["class"] += " bg-info-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="CF"){
			string s = boxCF.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxCF.Attributes["class"] += " bg-info-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="FSM"){
			string s = boxFSM.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxFSM.Attributes["class"] += " bg-info-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Prospecting"){
			string s = boxProspecting.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxProspecting.Attributes["class"] += " bg-info-gradient";		
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}
		if(v=="Other"){
			string s = boxOther.Attributes["class"].ToString();
			if(s.Contains("bg-info-gradient")){
				boxOther.Attributes.Add("class", boxOther.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 0;
			} else {
				boxOther.Attributes["class"] += " bg-info-gradient";		
				boxResign.Attributes.Add("class", boxResign.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxProspecting.Attributes.Add("class", boxProspecting.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxFSM.Attributes.Add("class", boxFSM.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxCF.Attributes.Add("class", boxCF.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				boxArrival.Attributes.Add("class", boxArrival.Attributes["class"].ToString().Replace("bg-info-gradient", ""));		      
				filterOn = 1;
			}
		}

		
		searchText = "Line";
		searchCol = v;
		
		HeaderText.Text += " (" + v + ")";
		
		if(filterOn==1){
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		}
		else {
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");
			HeaderText.Text = "Gross Income";
		}
	}
	
    public void BtnSearch_Click(object sender, EventArgs e)
    {
		searchText = ddlSearchGI.Text;
		searchCol = txtGI.Text;
		DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
    }

	protected void text_change_date(object sender, EventArgs e)
	{
		// ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE reg SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		// ErrorText.Text = sqlCommandStatement + " ----- " + id + " ----- " + text.Text;
	}

	protected void text_change(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE reg SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		
		// ErrorText.Text = text.Text + " was changed";
	}

	protected void text_change_reg(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE listReg SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
	}
	
	protected void text_change_addo(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE reg SET {0} = @TEXT WHERE id=@ID", text.ID);		
		SqlCmd(sqlCommandStatement, id, text.Text);

	}
		
	protected void text_change_appt(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE regAppt SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
	}

    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        //string sqlCommandStatement = "UPDATE reg SET status = @TEXT WHERE id=@ID";
        string sqlCommandStatement = "UPDATE reg SET status = @TEXT, tm = getdate() WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		

		
    }

    protected void Selection_Change_ScheduledFor(object sender, EventArgs e)
    {
        DropDownList ddlSchedule = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE reg SET scheduled_type = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlSchedule.Text);		
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
			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		
			
		}
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

		DropDownList dl = (DropDownList)row.FindControl("ddlRank");
		vTxt = dl.Text; // get the value from TextBox		
		rankid.Text = vTxt;
		
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
		//GridViewRow row = GridView1.Rows[rowIndex];
		string vId = row.Cells[0].Text;		
		id.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteModal();", true);
		
	}
 	
	public void btnDelete_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["id"]);	
			string sqlCommandStatement = String.Format("DELETE FROM reg WHERE id='{0}'", id );									
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
		string combinedGI = "";
		string[] ComboGiGridTotal = new string[5];
		string sortDir = ViewState["SortDirection"] as string;
		string sortExp = ViewState["SortExpression"] as string;
		
		if(ViewState["SortExpression"] != null)
		{					
			dataTable = resort(dataTable, sortExp, sortDir);
		}
		
		BothGoodLbl.Text = "";
		BothFigureLbl.Text = "";
		//BothBoardLblb.Text = "";

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


			// CONFIG
			var title1 = "SELECT name from config WHERE type = 'title' and seq = 2";
			var title2 = "SELECT name from config WHERE type = 'title' and seq = 2";
			var title3 = "SELECT name from config WHERE type = 'title' and seq = 3";
			var title4 = "SELECT name from config WHERE type = 'title' and seq = 4";
			var title5 = "SELECT name from config WHERE type = 'title' and seq = 5";
			var title6 = "SELECT name from config WHERE type = 'title' and seq = 6";



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
							ComboGiGridTotal[0] += Cmd.ExecuteScalar().ToString();
							combinedGI = ResultBothIn;
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
			CardInConfirmed.Text = ResultBothIn;				
			Card1.Text = ResultBothInv;	
			Title1.Text = title1;	
			
			CardConfirmed.Text = ResultBothConf;		
			CardInConfirmedA.Text = ResultBothOn;

			CardInConfirmedDay.Text = ResultDayIn;				
			CardInvoicedDay.Text = ResultDayInv;				
			CardConfirmedDay.Text = ResultDayConf;				
			CardInConfirmedDayA.Text = ResultDayOn;

			CardInConfirmedFdn.Text = ResultFdnIn;				
			CardInvoicedFdn.Text = ResultFdnInv;				
			CardConfirmedFdn.Text = ResultFdnConf;				
			CardInConfirmedFdnA.Text = ResultFdnOn;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridView4.DataSource = t2;
				GridView4.DataBind();
				
				var mySum = t2.AsEnumerable().Sum(dr => dr.Field<int>("amount"));					
				//string mySumStr = mySum.ToString();	
				BothInLbl.Text = String.Format("{0:C0}", mySum);					
				
				
			} else {
				//ErrorText1.Text = "(None)";	
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
						ComboGiGridTotal[1] += Cmd.ExecuteScalar().ToString();
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
						ComboGiGridTotal[2] += Cmd.ExecuteScalar().ToString();
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

			
			var query = from c in dataTable.AsEnumerable() where c.Field<string>("status") == "Open Cycle" && c.Field<string>("rank") == "c"
				|| c.Field<string>("status") == "Open Cycle" && c.Field<string>("rank") == "c" || c.Field<string>("status") == "Open Cycle" 
				&& c.Field<string>("rank") == "c" select c ;
				
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
			var cmd1 = "SELECT SUM(amount) AS total from reg WHERE status= 'Open Cycle' and rank = 'c'";
			string ResultBoard = "0";
			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
				using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
					{
									  
						ResultBoard = String.Format("{0:C0}", Cmd.ExecuteScalar());
						//ResultBoard = String.Format("{0:n0}", Cmd.ExecuteScalar());
						ComboGiGridTotal[3] += Cmd.ExecuteScalar().ToString();
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
						ComboGiGridTotal[3] += Cmd.ExecuteScalar().ToString();
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

		GridView gv = sender as GridView;
		
        // check view state is not null  
        if (ViewState["gigrid"] != null)  
        {  
            //get datatable from view state   
            DataTable dtCurrentTable = (DataTable)ViewState["gigrid"];  

			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search_reg_log(OrgText.Text, searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.reg_log(HeadText.Text, OrgText.Text), "reg");		
			
		} 
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
