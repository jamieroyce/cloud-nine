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

public partial class _Default : System.Web.UI.Page 
{  	

    dal DAL = new dal();

	static string searchText;
	static string searchCol;
	static string searchWE;

	public void Add_Addo_ID(object sender, EventArgs e)
    {
		Button Addo_Btn=sender as Button;        
		Control control1 = (Control)sender;
		GridViewRow gvRow3 = (GridViewRow)control1.Parent.Parent;
		TextBox Addo_Input = (TextBox)gvRow3.FindControl("Addo_ID") as TextBox;
		Addo_Btn.Visible = false;
		Addo_Input.Visible = true;		
        return;
    }
	
	public void Del_Addo_ID(object sender, EventArgs e)
	{
		ErrorText.Text = "";		
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  "UPDATE bis SET Addo_ID = @TEXT WHERE id=@ID";		
		SqlCmd(sqlCommandStatement, id, "Addo_ID_Null");
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
	}
	
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
                HeadText.Text = "Appointments";
				Appt.Visible = true;

				
				btnAddModal.Visible = true;		
                Title = HeadText.Text + " " + OrgText.Text;
				ArchiveWeekInv.Visible = false;
				txt.Text = "";
				OrgText.Visible = false;
				ddlPage.Visible = false;
				DataGrid_Load(DAL.reg("all"), "reg");
				Eight.Visible = false;
				ErrorText.Text = "";
				RowLabel.Visible = false;
				ArchiveTable.Visible = false;
				day.Enabled = false;
				fdn.Enabled = false;
				AmountText.Visible = false;
				alertWE.Visible = false;					
				searchText = "";
				searchCol = "";
				searchWE = "";
				Reports.Visible = false;

				DataTable regList = DAL.reg("config"); 
				ddlReg.DataSource = regList;
				ddlReg.DataTextField = regList.Columns["desc1"].ToString();
				ddlReg.DataValueField = regList.Columns["desc1"].ToString();
				ddlReg.DataBind();
				
		}				
	}
	catch{}
	}

	public void StartingPage()
	{


		HeadText.Text = "Combined BIS";
		btnAddModal.Visible = true;		
		Title = HeadText.Text;
		ArchiveWeekInv.Visible = false;
		txt.Text = "";
		OrgText.Visible = false;
		ddlPage.Visible = false;
		DataGrid_Load(DAL.reg("all"), "reg");
		Eight.Visible = false;
		ErrorText.Text = "";
		RowLabel.Visible = false;
		ArchiveTable.Visible = false;
		day.Enabled = false;
		fdn.Enabled = false;
		AmountText.Visible = false;
		alertWE.Visible = false;					
		searchText = "";
		searchCol = "";
		searchWE = "";
		Reports.Visible = false;
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

	public void Btn_Click(Object sender, EventArgs e)
	{
		ErrorText.Text = "";
		OrgText.Visible = true;
		day.Enabled = true;
		fdn.Enabled = true;
		Eight.Visible = false;
		ArchiveTable.Visible = false;
        ddlPage.Visible = true;
		AmountText.Visible = true;
        RowLabel.Visible = true;		
		btnAddModal.Visible = true;		
		txt.Text = "";
		TextBox2.Text = "";
		TextBox1.Text = "";
		txtGI.Text = "";
		searchText = "";
		searchCol = "";
		searchWE = "";
	
        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		
		LinkButton clickedButton = sender as LinkButton;		
		Title = clickedButton.Text + " " + OrgText.Text;
		HeadText.Text = clickedButton.Text;

		ArchiveWeekInv.Visible = false;
		alertWE.Visible = false;					
		GridView1.Sort("name", SortDirection.Ascending);
		
		if(HeadText.Text == "In The Shop"){
			ArchiveWeekInv.Visible = true;	
			string we = weekendingText.Value;
			if(String.IsNullOrEmpty(we)){
				alertWE.Visible = true;
			} else {
				alertWE.Visible = false;					
			}
		}
		else {
			ArchiveWeekInv.Visible = false;
			alertWE.Visible = false;					
			GridView1.Sort("name", SortDirection.Ascending);
		}

        DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
    }

	public void BtnArchive_Click(Object sender, EventArgs e)
	{
		ErrorText.Text = "";
		OrgText.Visible = true;
		day.Enabled = true;
		fdn.Enabled = true;
		Eight.Visible = false;
		ArchiveTable.Visible = true;
        ddlPage.Visible = true;
		AmountText.Visible = true;
        RowLabel.Visible = true;		
		btnAddModal.Visible = false;				
		Button clickedButton = sender as Button;		
		Title = clickedButton.Text + " " + OrgText.Text;
		HeadText.Text = clickedButton.Text;	
		txt.Text = "";
		TextBox2.Text = "";
		TextBox1.Text = "";
		TextBox3.Text = "";
		searchText = "";
		searchCol = "";
		searchWE = "";
		alertWE.Visible = false;	
		
        DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
    }	

	public void Archive_Click(object sender, EventArgs e)
	{

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string we = weekendingText.Value;	

			int i = 0;
			GridView gv = (GridView)(sender as Control).Parent.FindControl("GridViewInvoice");		
			foreach (GridViewRow row in gv.Rows)
			{
				i++;				
				string rowID 		= row.Cells[0].Text;
				TextBox name 	= (TextBox)row.FindControl("name");
				TextBox area 	= (TextBox)row.FindControl("area");
				
				if((we != null && name.Text != " " && area.Text != " " ) 
					&& ( name.Text != "" && area.Text != "" ) 
					&& ( name.Text != null && area.Text != null ))
				{																	
				
					string sqlCommandStatement = String.Format( 
						"UPDATE bis SET REG_CAT_ID = '{0}', weekend = '{1}' WHERE ID = '{2}'	" , 
																"Archive"	, 
																we			,
																rowID
															);			

					try
					{		
						using (SqlConnection conn = new SqlConnection(GetConnectionString("reg")))
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

					DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");			
				}
				else{
					ErrorText.Text += "Row " + i + " is Incomplete (Name, Area and Service are required Fields)<br />";
				}
			}
			LoadLastWeek(we);
		}
	}

	public void LoadLastWeek(string weekend)
	{		

		if(weekend != null){
			
			using(SqlConnection connection = new SqlConnection(GetConnectionString("reg")))
			{
				String query = "INSERT INTO bis (addo_id, name, area, service, reg, fsm, phone, email, org, status, line, reg_cat_id, last_week_bis, consecutive_weeks, notes) "
								+ "SELECT addo_id, name, area, service, reg, fsm, phone, email, org, 'Named' as  status, line, 'LineUp' as reg_cat_id, 1 as last_week_bis, isnull(consecutive_weeks,0) + 1 as consecutive_weeks, cast(isnull(consecutive_weeks,0) + 1 AS varchar(10))  + ' consecutive weeks ' as note"
								+ "FROM bis "
								+ "WHERE weekend = @weekend and org = 'Fdn' and reg_cat_id = 'Archive' "
								+ "AND addo_id not in ( select addo_id from bis where reg_cat_id = 'LineUp' and org = 'Fdn')";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@weekend",	weekend);
					connection.Open();
					int result = command.ExecuteNonQuery();
					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
				}
			} 

			using(SqlConnection connection = new SqlConnection(GetConnectionString("reg")))
			{
				String query = "INSERT INTO bis (addo_id, name, area, service, reg, fsm, phone, email, org, status, line, reg_cat_id, last_week_bis, consecutive_weeks, notes) "
								+ "SELECT addo_id, name, area, service, reg, fsm, phone, email, org, 'Named' as  status, line, 'LineUp' as reg_cat_id, 1 as last_week_bis, isnull(consecutive_weeks,0) + 1 as consecutive_weeks, cast(isnull(consecutive_weeks,0) + 1 AS varchar(10))  + ' consecutive weeks ' as note"
								+ "FROM bis "
								+ "WHERE weekend = @weekend and org = 'Day' and reg_cat_id = 'Archive' "
								+ "AND addo_id not in ( select addo_id from bis where reg_cat_id = 'LineUp' and org = 'Day' )";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@weekend",	weekend);
					connection.Open();
					int result = command.ExecuteNonQuery();
					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
				}
			}
			DataGrid_Load(DAL.reg("all"), "reg");
		}
    }

    public void BtnSearch_Click(object sender, EventArgs e)
    {
		if(HeadText.Text == "Archive"){

			searchText = ddlSearchArchive.Text;
			searchCol = txtArchive.Text;
			if (searchWE=="")
				DataGrid_Load(DAL.ArchiveSearch(searchText, searchCol), "reg");				
			else
				DataGrid_Load(DAL.ArchiveWE_FilterSearch("weekend", searchWE, searchText, searchCol), "reg");				

		}
		else if (HeadText.Text == "Combined BIS"){

			searchText = ddlSearchGI.Text;
			searchCol = txtGI.Text;
			DataGrid_Load(DAL.Search_Combo(searchText, searchCol), "reg");
			
		}
		else {
			
			searchText = ddlSearch.Text;
			searchCol = txt.Text;
			DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");

		}
    }

    public void BtnWESearch_Click(object sender, EventArgs e)
    {

		searchWE = weText.Value;
		if(searchCol != "" && searchText != ""){
			if (searchWE=="")
				DataGrid_Load(DAL.ArchiveSearch(searchText, searchCol), "reg");				
			else
				DataGrid_Load(DAL.ArchiveWE_FilterSearch("weekend", searchWE, searchText, searchCol), "reg");				
		}
		else{
			if (searchWE=="")
				DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
			else
				DataGrid_Load(DAL.ArchiveWESearch("weekend", searchWE), "reg");				
		}

	}
	
    public void BtnSearchInvGrid_Click(object sender, EventArgs e)
    {
		
		searchText = ddlSearchInvoice.Text;
		searchCol = txtInv.Text;
        DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		
    }
	
	public void BtnReport_Click(object sender, EventArgs e)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		sb.Append("<script language='javascript'>");
		sb.Append("function createPieChart() {");
		sb.Append("var numbers = [");			
		string[] colors = new string[20];
		Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
		colors[0] = "#FFDAB9";
		colors[1] = "#DD8F9C";
		colors[2] = "#8880B5";
		colors[3] = "#EEBCB0";
		colors[4] = "#AADDDF";
		colors[5] = "#55AFDD";
		colors[6] = "#AABDFF";
		colors[7] = "#000000";
		int i = 0;
		decimal tot, x;
		tot = x = 0;
		decimal v = 0;		
		foreach(DataRow row in DAL.Report_By_Lines(report_text.Text).Rows)
		{			
			if (decimal.TryParse(row["num"].ToString(), out x))				
				dict.Add(row["line"].ToString(), x);
				tot += x;
			i++;
		}		
		foreach (KeyValuePair<string, decimal> pair in dict)
		{
			if(pair.Key == "")
				v = pair.Value;
		}
		if(v != 0)
		{
			dict.Add("Not Specified", v);
			dict.Remove("");
		}
		foreach (KeyValuePair<string, decimal> pair in dict)
		{
			if(pair.Key == dict.Keys.Last())
				sb.Append(cal_per(pair.Value, tot) + "]; var labels = [\"\"];");
			else
				sb.Append(cal_per(pair.Value, tot) + ", ");
		}
		sb.Append("var color = [ ");
		for(int c = 0; c < i; c++) {
			if(c == (i - 1))
				sb.Append("[\"" + colors[c] + "\"]];");
			else
				sb.Append("[\"" + colors[c] + "\"],");
		}
		//sb.Append("var pieChart = new PieChart( \"piechart\", { " +
		sb.Append("var pieChart = new PieChart( \"piechart\", { " +
						"includeLabels: false, data: numbers, labels: labels, colors: color }); pieChart.draw();} " +
						"</script>");
		if (!ClientScript.IsStartupScriptRegistered("JSScript"))
		{
			ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
		}	
		Page.ClientScript.RegisterStartupScript(this.GetType(), "PieKey", "createPieChart();", true);
		lbltest.InnerHtml = "<div>";
		int d = 0;
		foreach (KeyValuePair<string, decimal> pair in dict)
		{
			lbltest.InnerHtml += "<div><p style='background-color:" + colors[d] +"; float: left; margin: 0;'>&nbsp;&nbsp;&nbsp;</p>&nbsp;<p style='float: left; margin: 0px 10px 0px 5px ;'> " + pair.Key + " - " + pair.Value + "(" + (int)cal_percent(pair.Value, tot) + "%)" + "</p></div>";
			d++;
		}		
		lbltest.InnerHtml += "<p>Total: " + tot.ToString() + "</p></div>";
		try{
			lblChart.Text = char.ToUpper(report_text.Text[0]) + report_text.Text.Substring(1) + " Lines Pie Chart";
		}
		catch{}
	}
	
	public DateTime getWE(DateTime d)
	{		

		DateTime we;
		
		switch ((int) d.DayOfWeek)
		{
			case 0:
				we = d.AddDays(4);
				break;
			case 1:
				we = d.AddDays(3);
			break;
			case 2:
				we = d.AddDays(2);
			break;
			case 3:
				we = d.AddDays(1);
			break;
			case 4:
				we = d;
			break;
			case 5:
				we = d.AddDays(6);
			break;
			case 6:
				we = d.AddDays(5);
			break;
			default:
				we = d;
			break;
		}			
		
		return we;
	}

	private decimal cal_percent(decimal number, decimal tot)
	{		
		return (number / tot * 100);
	}
	
	private decimal cal_per(decimal number, decimal tot)
	{		
		return (number / tot * 100) * (360m / 100m);
	}

    public void BtnSearch_Click_FPPP(object sender, EventArgs e)
    {
		searchText = ddlSearchFPPP.Text;
		searchCol = TextBox1.Text;
		DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
    }

    public void BtnSearch_Click_Inv(object sender, EventArgs e)
    {
		searchText = ddlSearchInv.Text;
		searchCol = TextBox2.Text;

        DataGrid_Load(DAL.Search_Inv(searchText, searchCol), "cf");
    }

    public void BtnSearch_Click_TTL(object sender, EventArgs e)
    {
		searchText = "";
		searchCol = TextBox3.Text;
		DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
    }

    public void Combined_Btn_Click(Object sender, EventArgs e)
	{
		ErrorText.Text = "";
		OrgText.Visible = false;
        ddlPage.Visible = false;
        RowLabel.Visible = false;
		ArchiveTable.Visible = false;
        day.Enabled = false;
		fdn.Enabled = false;
		AmountText.Visible = false;
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		// Button clickedButton = sender as Button;
		LinkButton clickedButton = sender as LinkButton;
		Title = clickedButton.Text;
		HeadText.Text = clickedButton.Text;
		btnAddModal.Visible = false;		
		alertWE.Visible = false;					
		searchText = "";
		searchCol = "";
		searchWE = "";
        DataGrid_Load(DAL.reg("all"), "reg");
		Eight.Visible = false;
    }

    public void Maintenance_BtnClick(Object sender, EventArgs e)
	{
		ErrorText.Text = "";
		One.Visible = false;
		Two.Visible = false;
		Three.Visible = false;
		Four.Visible = false;
		Five.Visible = false;
		InvoiceTable.Visible = false;
		ArchiveTable.Visible = false;
		OrgText.Visible = false;
        ddlPage.Visible = false;
        RowLabel.Visible = false;
		ArchiveTable.Visible = false;
        day.Enabled = false;
		fdn.Enabled = false;
		AmountText.Visible = false;
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		LinkButton clickedButton = sender as LinkButton;
		Title = clickedButton.Text;
		HeadText.Text = clickedButton.Text;
		btnAddModal.Visible = false;		
		alertWE.Visible = false;				
		GridView gv = GridViewLookup;
		GridView_Load(gv, DAL.reg("config")); 
		Eight.Visible = true;
    }

    public void Combined_Nav_Click(Object sender, EventArgs e)
	{
		ErrorText.Text = "";
		OrgText.Visible = false;
        ddlPage.Visible = false;
        RowLabel.Visible = false;
        day.Enabled = false;
		fdn.Enabled = false;
		AmountText.Visible = true;
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
        DataGrid_Load(DAL.reg("all"), "reg");
		Eight.Visible = false;
    }

	public void Inv_Click(Object sender, EventArgs e)
	{
		ErrorText.Text = "";
        OrgText.Visible = false;
        day.Enabled = false;
        fdn.Enabled = false;
        ddlPage.Visible = true;
        RowLabel.Visible = true;
        Eight.Visible = false;
		ArchiveTable.Visible = false;
        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		Button clickedButton = sender as Button;		
		HeadText.Text = clickedButton.Text;
		Title = clickedButton.Text;
		searchText = "";
		searchCol = "";
		searchWE = "";
        DataGrid_Load(DAL.Inv_Table(), "cf");
	}

	public void FPPP_Click(Object sender, EventArgs e)
	{
		ErrorText.Text = "";
        OrgText.Visible = false;
        Eight.Visible = false;
        day.Enabled = false;
        fdn.Enabled = false;
        ddlPage.Visible = true;
        RowLabel.Visible = true;
		ArchiveTable.Visible = false;
        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		LinkButton clickedButton = sender as LinkButton;
		HeadText.Text = clickedButton.Text;
		Title = clickedButton.Text;
		searchText = "";
		searchCol = "";
		searchWE = "";
        //DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");		
	}

    public void TTLPayList_Click(Object sender, EventArgs e)
    {
		ErrorText.Text = "";
        OrgText.Visible = false;
        Eight.Visible = false;
        day.Enabled = false;
        fdn.Enabled = false;
        RowLabel.Visible = true;
		ArchiveTable.Visible = false;
        if (ViewState["SortExpression"] != null)
            ViewState["SortExpression"] = null;
        Button clickedButton = sender as Button;
        HeadText.Text = clickedButton.Text;
        Title = clickedButton.Text;
		searchText = "";
		searchCol = "";
		searchWE = "";
        DataGrid_Load(DAL.TTL(), "cf");
    }

    public void Addresso_Click(Object sender, EventArgs e)
    {
		ErrorText.Text = "";
        OrgText.Visible = false;
        Eight.Visible = false;
        day.Enabled = false;
        fdn.Enabled = false;
        RowLabel.Visible = true;
		ArchiveTable.Visible = false;
        if (ViewState["SortExpression"] != null)
            ViewState["SortExpression"] = null;
		LinkButton clickedButton = sender as LinkButton;
        HeadText.Text = clickedButton.Text;
        Title = clickedButton.Text;
		searchText = "";
		searchCol = "";
		searchWE = "";
        DataGrid_Load(DAL.addo(), "cf");
    }
    
	public void Org_Btn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		Button clickedButton = sender as Button;		
		Title = clickedButton.Text;
		OrgText.Text = clickedButton.Text;
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
        
    }

	public void Reports_Btn_Click(object sender, EventArgs e)
	{		
		ErrorText.Text = "";
		OrgText.Visible = false;
		day.Enabled = false;
        fdn.Enabled = false;
		AmountText.Visible = false;
		Button clickedButton = sender as Button;
		HeadText.Text = clickedButton.Text;
		One.Visible = false;
		Two.Visible = false;
		Three.Visible = false;
		Four.Visible = false;
		Five.Visible = false;
		Six.Visible = false;
		Seven.Visible = true;
		Eight.Visible = false;
		InvoiceTable.Visible = false;
		ArchiveTable.Visible = false;
	
		lbltest.InnerHtml = "";
		
		
		
	}

	public void Calendar_Btn_Click(object sender, EventArgs e)
	{		
		ErrorText.Text = "";
		OrgText.Visible = true;
		day.Enabled = false;
        fdn.Enabled = false;
		AmountText.Visible = false;
		Button clickedButton = sender as Button;
		HeadText.Text = clickedButton.Text;
		One.Visible = false;
		Two.Visible = false;
		Three.Visible = false;
		Four.Visible = false;
		Five.Visible = false;
		Six.Visible = false;
		Seven.Visible = false;
		Eight.Visible = true;
		InvoiceTable.Visible = false;
		
        System.Text.StringBuilder sbtest = new System.Text.StringBuilder();
        System.Text.StringBuilder sHeader = new System.Text.StringBuilder();

		sbtest.Append(@"<div class=""container-fluid"">");

		sbtest.Append(@"<div class=""jumbotron text-center"">");
		sbtest.Append(@"<h1>Reg Calendar - " + OrgText.Text + "</h1><p>This is a calendar of all future names, appointments, scheduled sales and scheduled starts. <br>'A High-Tone individual thinks wholly into the future.' - LRH</p>");
		sbtest.Append(@"</div>");

		//HEADER TEXT
		sHeader.Append(@"<div class=""row"">");
		sHeader.Append(@"<b><div class=""col-xs-2"">Scheduled For</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Name</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Service</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Reg</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Status</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Amount</div>");
		sHeader.Append(@"</div><hr></b>");
		
		DataTable dt = new DataTable();
		dt = DAL.Calendar(HeadText.Text, OrgText.Text);
		//dt.DefaultView.Sort = "appt ASC";
		
		bool isThu = false;
		DateTime curWE = DateTime.Now;
		DateTime lastWE = DateTime.Now;
		bool needsHeader = true;
		
		foreach(DataRow row in dt.Rows){	

			//assign variables: 
			DateTime? 	appt 		= row.Field<DateTime?>("appt");
			DateTime 	d;

			//SET minDate
			if (appt.HasValue || !appt.HasValue){

				if(!appt.HasValue){
					d = DateTime.MinValue;
				} else {
					d = appt.Value;	
				}

				var		 	anAmount	= row.Field<int?>("amount");
				var     	aName   	= row.Field<string>("name");
				var     	aService  	= row.Field<string>("service");
				var     	aReg   		= row.Field<string>("reg" );
				var     	aStatus   	= row.Field<string>("status");
				var     	aScheduledType = row.Field<string>("scheduled_type");
				var			anAppt		="";
				if(!appt.HasValue){
					anAppt = "";
				} 
				else{
					anAppt = d.ToString("dd-MMM-yyyy hh:mm tt");
				};
				var     	aNote   	= row.Field<string>("notes");
				
				//TESTS
				// sHeader.Append(@"SCHEDULED_TYPE IS = " + aScheduledType);
				// if(aScheduledType != null) {sHeader.Append(@"SCHEDULED_TYPE IS = " + aScheduledType);}
				
				//get current weekending: 
				lastWE = curWE;

				//SET HEADER STATUS 
				switch ((int) d.DayOfWeek)
				{
					case 0:
						curWE = d.AddDays(4);
						break;
					case 1:
						curWE = d.AddDays(3);
					break;
					case 2:
						curWE = d.AddDays(2);
					break;
					case 3:
						curWE = d.AddDays(1);
					break;
					case 4:
						curWE = d;
					break;
					case 5:
						curWE = d.AddDays(6);
					break;
					case 6:
						curWE = d.AddDays(5);
					break;
				}				
				
				// CHECK IF ITS THE SAME WEEK OR NOT SO A WEEKENDING HEADER CAN BE ADDED IF NEEDED
				if(curWE.ToString("dd-MMM-yyyy") == lastWE.ToString("dd-MMM-yyyy")){
					needsHeader = false;
				}
				else{
					needsHeader = true;
				}

				//ADD THE HTML WITH A HEADER
				if(needsHeader){

					sbtest.Append(@"<hr><hr><div style=""color:#5e7291"" class=""row"">");
					
					if(anAppt!=""){
						sbtest.Append(@"<div class=""col-xs-12""><h4>Weekending: " + curWE.ToString("dddd, MMMM d, yyyy") + "</h4></div>");
						
					} else {
						sbtest.Append(@"<div class=""col-xs-12""><h4>(To Be Scheduled)" + "</h4></div>");
					}
					
					sbtest.Append(@"</div><hr>");

					sbtest.Append(sHeader);
							
					sbtest.Append(@"<div class=""row"">");
					sbtest.Append(@"<div class=""col-xs-2"">" + anAppt 		+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + aName 		+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + aService	+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + aReg 		+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + aStatus 	+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + string.Format("{0:00000}",anAmount)	+ "</div>");
					sbtest.Append(@"</div>");
					
					//sbtest.Append(@"SCHEDULED_TYPE IS = " + aScheduledType);					
					
					sbtest.Append(@"<div style=""color:grey"" class=""row"">");
					sbtest.Append(@"<div class=""col-xs-2"">" + aScheduledType + "</div>");
					sbtest.Append(@"<div class=""col-xs-10""><small>" + aNote + "</small></div>");
					sbtest.Append(@"</div>");

					needsHeader = false;
					
				} else {
					//ADD THE HTML WITHOUT A HEADER					
					sbtest.Append(@"<hr><div class=""row"">");
					sbtest.Append(@"<div class=""col-xs-2"">" + anAppt 		+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + aName 		+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + aService	+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + aReg 		+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + aStatus 	+ "</div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + string.Format("{0:00000}",anAmount)	+ "</div>");
					sbtest.Append(@"</div>");

					//sbtest.Append(@"SCHEDULED_TYPE IS = " + aScheduledType);					

					
					sbtest.Append(@"<div style=""color:grey"" class=""row"">");
					sbtest.Append(@"<div class=""col-xs-2"">" + aScheduledType + "</div>");
					sbtest.Append(@"<div class=""col-xs-10""><small>" + aNote + "</small></div>");

					sbtest.Append(@"</div>");
				}

				lastWE = curWE;

			}
			
		}

		sbtest.Append(@"</div>");

        Eight.InnerHtml = sbtest.ToString();
        
	}
	
	public void CalendarX_Btn_Click(object sender, EventArgs e)
	{		
		ErrorText.Text = "";
		OrgText.Visible = false;
		day.Enabled = false;
        fdn.Enabled = false;
		AmountText.Visible = false;
		Button clickedButton = sender as Button;
		HeadText.Text = clickedButton.Text;
		One.Visible = false;
		Two.Visible = false;
		Three.Visible = false;
		Four.Visible = false;
		Five.Visible = false;
		Six.Visible = false;
		Seven.Visible = false;
		Eight.Visible = true;
		InvoiceTable.Visible = false;
		
		Title = clickedButton.Text + " " + OrgText.Text;

        System.Text.StringBuilder sbtest = new System.Text.StringBuilder();
        System.Text.StringBuilder x = new System.Text.StringBuilder();
		
		// sbtest.Append(@"<div class=""jumbotron text-center"">");
		// sbtest.Append(@"<h1>Calendar</h1>");
		// sbtest.Append(@"<p>List of all future cycles...</p> ");
		// sbtest.Append(@"</div>");

		sbtest.Append(@"<div class=""container"">");
		sbtest.Append(@"<h2>Reg Cycle Log</h2><p>List of all reg cycles... </p><table class=""table table-condensed"">");
		sbtest.Append(@"<thead><tr><th>Weekending</th><th>Date</th><th>Name</th><th>Service</th><th>Reg</th><th>Status</th><th>Amount</th></tr></thead>");
		
		DataTable dt = new DataTable();
		dt = DAL.Calendar(HeadText.Text, OrgText.Text);
		//dt.DefaultView.Sort = "appt ASC";
		
		bool isThu = false;
		DateTime curWE = DateTime.Now;
		DateTime lastWE = DateTime.Now;
		bool needsHeader = true;
		
		foreach(DataRow row in dt.Rows){	

			
			DateTime? appt = row.Field<DateTime?>("appt");
			DateTime d;

			//SET minDate
			if (appt.HasValue){

				d = appt.Value;
				
				//sbtest.Append(@"<h4>" + row.Field<string>("name") + " | Appt: " + d.ToString("dd-MMM-yyyy") + "</h4>" );			

				//get current weekending: 
				lastWE = curWE;

				//SET HEADER STATUS 
				switch ((int) d.DayOfWeek)
				{
					case 0:
						curWE = d.AddDays(4);
						//sbtest.Append(@" 3 ======= " + d.DayOfWeek + " current Weekending: " + curWE.ToString("dd-MMM-yyyy") + "<br>");	
						break;
					case 1:
						curWE = d.AddDays(3);
						//sbtest.Append(@" 3 =======" + " Appointment Date: " + d.ToString("dd-MMM-yyyy") + " " + d.DayOfWeek + " current Weekending: " + curWE.ToString("dd-MMM-yyyy") + "<br>");	
					break;
					case 2:
						curWE = d.AddDays(2);
						//sbtest.Append(@" 3 =======" + " Appointment Date: " + d.ToString("dd-MMM-yyyy") + " " + d.DayOfWeek + " current Weekending: " + curWE.ToString("dd-MMM-yyyy") + "<br>");	
					break;
					case 3:
						curWE = d.AddDays(1);
						//sbtest.Append(@" 3 =======" + " Appointment Date: " + d.ToString("dd-MMM-yyyy") + " " + d.DayOfWeek + " current Weekending: " + curWE.ToString("dd-MMM-yyyy") + "<br>");	
					break;
					case 4:
						curWE = d;
						//sbtest.Append(@" 3 =======" + " Appointment Date: " + d.ToString("dd-MMM-yyyy") + " " + d.DayOfWeek + " current Weekending: " + curWE.ToString("dd-MMM-yyyy") + "<br>");	
					break;
					case 5:
						curWE = d.AddDays(6);
						//sbtest.Append(@" 3 =======" + " Appointment Date: " + d.ToString("dd-MMM-yyyy") + " " + d.DayOfWeek + " current Weekending: " + curWE.ToString("dd-MMM-yyyy") + "<br>");	
					break;
					case 6:
						curWE = d.AddDays(5);
						//sbtest.Append(@" 3 =======" + " Appointment Date: " + d.ToString("dd-MMM-yyyy") + " " + d.DayOfWeek + " current Weekending: " + curWE.ToString("dd-MMM-yyyy") + "<br>");	
					break;
				}				

				//sbtest.Append(@" 4 ======= last WE:" + lastWE.ToString("dd-MMM-yyyy") + " cur WE: " + curWE.ToString("dd-MMM-yyyy") + " DayOfWeek: " + (int)d.DayOfWeek + " - " + d.DayOfWeek + " <br>" );	
				
				if(curWE == lastWE){
					needsHeader = false;
				}
				else{
					needsHeader = true;
				}
				
				if(needsHeader){
					//insert header and then do stuff
					//sbtest.Append(@" 5a ====== needs Header ");	
					//sbtest.Append("<h3> Weekending: " + curWE.ToString("dddd, MMMM d, yyyy") + "</h3>"); 
					sbtest.Append(@"<tbody><tr><td>" + curWE.ToString("dd-MMM-yyyy") + "</td><td>" + d.ToString("dd-MMM-yyyy") + "</td>");
					sbtest.Append(@"<td>" + row.Field<string>("name") + "</td></td><td>" + row.Field<string>("service") + "</td>");
					sbtest.Append(@"<td>" + row.Field<string>("reg" ) + "</td><td>"      + row.Field<string>("status" ) + "</td><td>" + row.Field<int?>("amount") + "</td></tr>" );
					needsHeader = false;
				}else {
					//do stuff without header
					//sbtest.Append(@" 5b ====== does NOT need Header ");	
					sbtest.Append(@"<tbody><tr><td>" + curWE.ToString("dd-MMM-yyyy") + "</td><td>" + d.ToString("dd-MMM-yyyy") + "</td>");
					sbtest.Append(@"<td>" + row.Field<string>("name") + "</td></td><td>" + row.Field<string>("service") + "</td>");
					sbtest.Append(@"<td>" + row.Field<string>("reg" ) + "</td><td>"      + row.Field<string>("status" ) + "</td><td>" + row.Field<int?>("amount") + "</td></tr>" );
				}

				lastWE = curWE;

			}
			
		}

		sbtest.Append(@"</tbody></table></div>");
		
        //sbtest.Append("<p>test</p>");

        Eight.InnerHtml = sbtest.ToString();
        
	}

	public void Calendar2_Btn_Click(object sender, EventArgs e)
	{		
		ErrorText.Text = "";
		OrgText.Visible = true;
		day.Enabled = false;
        fdn.Enabled = false;
		AmountText.Visible = false;
		Button clickedButton = sender as Button;
		HeadText.Text = clickedButton.Text;
		One.Visible = false;
		Two.Visible = false;
		Three.Visible = false;
		Four.Visible = false;
		Five.Visible = false;
		Six.Visible = false;
		Seven.Visible = false;
		Eight.Visible = true;
		InvoiceTable.Visible = false;

        System.Text.StringBuilder sbtest = new System.Text.StringBuilder();
        System.Text.StringBuilder sLineUp = new System.Text.StringBuilder();
        System.Text.StringBuilder sLogs = new System.Text.StringBuilder();
        System.Text.StringBuilder sInvoiced = new System.Text.StringBuilder();
        System.Text.StringBuilder sHeader = new System.Text.StringBuilder();

		sbtest.Append(@"<div class=""container-fluid"">");

		sbtest.Append(@"<div class=""jumbotron text-center"">");
		sbtest.Append(@"<h1>Current - " + OrgText.Text + " Org</h1><p>All cycles on the lines this week.</p>");
		sbtest.Append(@"</div>");
		
		DataTable dt = new DataTable();
		dt = DAL.CurrentWeek(OrgText.Text);
		
		bool isThu = false;
		DateTime curWE = DateTime.Now;
		DateTime lastWE = DateTime.Now;
		bool needsHeader = true;

		curWE = getWE(DateTime.Now);
		//insert header and then do stuff
		sbtest.Append(@"<hr><div style=""color:#5e7291"" class=""row"">");
		sbtest.Append(@"<div class=""col-xs-12""><h4>Weekending: " + curWE.ToString("dddd, MMMM d, yyyy") + "</h4></div>");
		sbtest.Append(@"</div><hr>");

		int?		aWeekTotal = 0;
		int?		aLineUpTotal = 0;
		int?		aLogsTotal = 0;
		int?		aInvoicedTotal = 0;
		int?		aInConfirmedTotal = 0;
		
		sHeader.Append(@"<div class=""row"">");
		sHeader.Append(@"<b><div class=""col-xs-2"">Scheduled For</div>");
		sHeader.Append(@"<b><div class=""col-xs-2"">Scheduled For</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Name</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Service</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Reg</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Status</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Amount</div>");
		sHeader.Append(@"</div></b>");
		
		foreach(DataRow row in dt.Rows){	

			//assign variables: 
			DateTime? 	appt 		= row.Field<DateTime?>("appt");
			DateTime 	d;
			
			//SET minDate
			if (appt.HasValue || !appt.HasValue){

				if(!appt.HasValue){
					d = DateTime.MinValue;
				} else {
					d = appt.Value;	
				}

				var		 	anAmount	= row.Field<int?>("amount");
				var     	aName   	= row.Field<string>("name");
				var     	aRank   	= row.Field<string>("rank");
				var     	aService  	= row.Field<string>("service");
				var     	aReg   		= row.Field<string>("reg" );
				var     	aStatus   	= row.Field<string>("status");
				var     	aScheduledType = row.Field<string>("scheduled_type");
				var     	myCategory  = row.Field<string>("reg_cat_id" );
				var			anAppt		="";
				
				//VALIDATION
				if(!appt.HasValue){
					anAppt = "";
				} 
				else{
					//anAppt = d.ToString("dd-MMM-yyyy hh:mm tt");
					anAppt = d.ToString("dd-MMM-yyyy");
					DateTime min = new DateTime(2015,1,1);
					if(d < min)
						anAppt = "";
					
				};
				var     	aNote   	= row.Field<string>("notes");

				string upperRank; 
				if(aRank == null){
					upperRank = "";
				} 
				else{
					upperRank = aRank.ToUpper();
				};


				// BUILD LINEUP HTML
				if(myCategory == "LineUp"){
						
					sLineUp.Append(@"<div class=""row"">");
					sLineUp.Append(@"<hr>");
					sLineUp.Append(@"<div class=""col-xs-2""><b>" + upperRank   + "</b> - " + anAppt 		+ "</div>");
					sLineUp.Append(@"<div class=""col-xs-2"">" + aName 		+ "</div>");
					sLineUp.Append(@"<div class=""col-xs-2"">" + aService	+ "</div>");
					sLineUp.Append(@"<div class=""col-xs-2"">" + aReg 		+ "</div>");
					sLineUp.Append(@"<div class=""col-xs-2"">" + aStatus 	+ "</div>");
					sLineUp.Append(@"<div class=""col-xs-2"">" + string.Format("{0:00000}",anAmount)	+ "</div>");
					sLineUp.Append(@"</div>");

					sLineUp.Append(@"<div style=""color:grey"" class=""row"">");
					sLineUp.Append(@"<div align=""left"" class=""col-xs-2"">" + aScheduledType + "</div>");
					sLineUp.Append(@"<div class=""col-xs-10""><small>" + aNote + "</small></div>");
					sLineUp.Append(@"</div>");
					
					aLineUpTotal = aLineUpTotal + anAmount;
					aWeekTotal = aWeekTotal + anAmount;

				} 
				// BUILD LOGS HTML
				else if(myCategory  == "Logs"){
						
					sLogs.Append(@"<div class=""row"">");
					sLogs.Append(@"<hr>");
					sLogs.Append(@"<div class=""col-xs-2""><b>" + upperRank   + "</b> - " + anAppt 		+ "</div>");
					sLogs.Append(@"<div class=""col-xs-2"">" + aName 		+ "</div>");
					sLogs.Append(@"<div class=""col-xs-2"">" + aService	+ "</div>");
					sLogs.Append(@"<div class=""col-xs-2"">" + aReg 		+ "</div>");
					sLogs.Append(@"<div class=""col-xs-2"">" + aStatus 	+ "</div>");
					sLogs.Append(@"<div class=""col-xs-2"">" + string.Format("{0:00000}",anAmount)	+ "</div>");
					sLogs.Append(@"</div>");

					sLogs.Append(@"<div style=""color:grey"" class=""row"">");
					sLogs.Append(@"<div align=""left"" class=""col-xs-2"">" + aScheduledType + "</div>");
					sLogs.Append(@"<div class=""col-xs-10""><small>" + aNote + "</small></div>");
					sLogs.Append(@"</div>");

					aLogsTotal = aLogsTotal + anAmount;
					aWeekTotal = aWeekTotal + anAmount;

				} 
				// BUILD INVOICED HTML
				else if(myCategory == "Invoiced") {
					
					sInvoiced.Append(@"<div class=""row"">");
					sInvoiced.Append(@"<hr>");
					sInvoiced.Append(@"<div class=""col-xs-2""><b>" + upperRank   + "</b> - " + anAppt 		+ "</div>");
					sInvoiced.Append(@"<div class=""col-xs-2"">" + aName 		+ "</div>");
					sInvoiced.Append(@"<div class=""col-xs-2"">" + aService	+ "</div>");
					sInvoiced.Append(@"<div class=""col-xs-2"">" + aReg 		+ "</div>");
					sInvoiced.Append(@"<div class=""col-xs-2"">" + aStatus 	+ "</div>");
					sInvoiced.Append(@"<div class=""col-xs-2"">" + string.Format("{0:00000}",anAmount)	+ "</div>");
					sInvoiced.Append(@"</div>");

					sInvoiced.Append(@"<div style=""color:grey"" class=""row"">");
					sInvoiced.Append(@"<div align=""left"" class=""col-xs-2"">" + aScheduledType + "</div>");
					sInvoiced.Append(@"<div class=""col-xs-10""><small>" + aNote + "</small></div>");
					sInvoiced.Append(@"</div>");
					
					aInvoicedTotal = aInvoicedTotal + anAmount;
					aWeekTotal = aWeekTotal + anAmount;
					
				}
				
			}
			
		}

		aInConfirmedTotal = aInvoicedTotal + aLogsTotal;
		
		sbtest.Append(@"<div class=""page-header"">");
		sbtest.Append(@"<h2>Invoiced (" + string.Format("{0:00000}",aInvoicedTotal) + ")</h2><h3>In and Confirmed (" + string.Format("{0:00000}",aInConfirmedTotal) + ")</h3><p>All invoiced cycles for this week.</p>");
		sbtest.Append(@"</div>");
		sbtest.Append(sHeader.ToString());
		sbtest.Append(sInvoiced.ToString());

		sbtest.Append(@"<div class=""page-header"">");
		sbtest.Append(@"<h2>Logs (" + string.Format("{0:00000}",aLogsTotal) + ")</h2><p>Interviews that were closed and in logistics, meaning they agreed to pay and are definitely being invoiced this week.</p>");
		sbtest.Append(@"</div>");
		sbtest.Append(sHeader.ToString());
		sbtest.Append(sLogs.ToString());

		sbtest.Append(@"<div class=""page-header"">");
		sbtest.Append(@"<h2>Line Up (" + string.Format("{0:00000}",aLineUpTotal) + ")</h2><p>Everyone still on the line up for this week, who is not already in logs or invoiced.</p>");
		sbtest.Append(@"</div>");
		sbtest.Append(sHeader.ToString());
		sbtest.Append(sLineUp.ToString());

		sbtest.Append(@"<div>");
		sbtest.Append(@"</div><hr>");

		sbtest.Append(@"</div>");
        Eight.InnerHtml = sbtest.ToString();
        
	}

	public void Calendar3_Btn_Click(object sender, EventArgs e)
	{		
		ErrorText.Text = "";
		OrgText.Visible = true;
		day.Enabled = false;
        fdn.Enabled = false;
		AmountText.Visible = false;
		Button clickedButton = sender as Button;
		HeadText.Text = clickedButton.Text;
		One.Visible = false;
		Two.Visible = false;
		Three.Visible = false;
		Four.Visible = false;
		Five.Visible = false;
		Six.Visible = false;
		Seven.Visible = false;
		Eight.Visible = true;
		InvoiceTable.Visible = false;

        System.Text.StringBuilder sbtest = new System.Text.StringBuilder();
        System.Text.StringBuilder sHeader = new System.Text.StringBuilder();
        System.Text.StringBuilder sWeek = new System.Text.StringBuilder();

		sbtest.Append(@"<div class=""container-fluid"">");
		sbtest.Append(@"<div class=""jumbotron text-center"">");
		sbtest.Append(@"<h1>Completed Reg Cycles - " + OrgText.Text + "</h1><p>All reg interviews that closed and were invoiced for prior weeks.</p>");
		sbtest.Append(@"</div>");

		//HEADER TEXT
		sHeader.Append(@"<div class=""row"">");
		sHeader.Append(@"<b><div class=""col-xs-2"">Scheduled For</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Name</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Service</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Reg</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Procurement Avenue</div>");
		sHeader.Append(@"<div class=""col-xs-2"">Amount</div>");
		sHeader.Append(@"</div><hr></b>");
				
		DataTable dt = new DataTable();
		dt = DAL.Archive(OrgText.Text);

		int?		aWeekTotal = 0;
		int?		aTotal = 0;
		
		bool isThu = false;
		DateTime curWE = DateTime.Now;
		DateTime lastWE = DateTime.Now;
		bool needsHeader = true;
		var a = 0;
		foreach(DataRow row in dt.Rows){	
			a=a+1;
			//assign variables: 
			
			DateTime? 	appt 		= row.Field<DateTime?>("appt");
			DateTime 	d;

			if (appt.HasValue || !appt.HasValue){

				if(!appt.HasValue){
					d = DateTime.MinValue;
				} else {
					d = appt.Value;	
				}

				var		 	anAmount	= row.Field<int?>("amount");
				var     	aName   	= row.Field<string>("name");
				var     	aRank   	= row.Field<string>("rank");
				var     	aService  	= row.Field<string>("service");
				var     	aReg   		= row.Field<string>("reg" );
				var     	aLine   	= row.Field<string>("line");
				var			anAppt		="";

				if(!appt.HasValue){
					anAppt = "";
				} 
				else{
					anAppt = d.ToString("dd-MMM-yyyy hh:mm tt");
				};
			
				string upperRank; 
				
				if(aRank == null){
					upperRank = "";
				} 
				else{
					upperRank = aRank.ToUpper();
				};

				var     	aNote   	= row.Field<string>("notes");
				
				lastWE = curWE;

				//SET HEADER STATUS 
				switch ((int) d.DayOfWeek)
				{
					case 0:
						curWE = d.AddDays(4);
						break;
					case 1:
						curWE = d.AddDays(3);
					break;
					case 2:
						curWE = d.AddDays(2);
					break;
					case 3:
						curWE = d.AddDays(1);
					break;
					case 4:
						curWE = d;
					break;
					case 5:
						curWE = d.AddDays(6);
					break;
					case 6:
						curWE = d.AddDays(5);
					break;
				}				
			
				if(curWE.ToString("dd-MMM-yyyy") == lastWE.ToString("dd-MMM-yyyy")){
					needsHeader = false;
				}
				else{
					needsHeader = true;
				}
				
				if(needsHeader){

					sWeek.Append(@"<hr><hr><div style=""color:#5e7291"" class=""row"">");
					if(anAppt!=""){
						sWeek.Append(@"<div class=""col-xs-12""><h4>Weekending: " + curWE.ToString("dddd, MMMM d, yyyy") + "</h4></div>");
					} else {
						sWeek.Append(@"<div class=""col-xs-12""><h4>Weekending: (Not Set)" + "</h4></div>");
					}
					sWeek.Append(@"</div><hr>");

					sWeek.Append(sHeader);
					
					sWeek.Append(@"<div class=""row"">");
					sWeek.Append(@"<div class=""col-xs-2"">" + anAppt 		+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + aName 		+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + aService	+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + aReg 		+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + aLine 	+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + string.Format("{0:00000}",anAmount)	+ "</div>");
					sWeek.Append(@"</div>");
					
					sWeek.Append(@"<div style=""color:grey"" class=""row"">");
					sWeek.Append(@"<div class=""col-xs-2""></div>");
					sWeek.Append(@"<div class=""col-xs-10""><small>" + aNote + "</small></div>");
					sWeek.Append(@"</div>");
					
					aTotal = aTotal + anAmount;
					
				}else {
					//do stuff without header
//					sbtest.Append(@"   ////DEBUG//// " + string.Format("{0:00000}",aWeekTotal)");
					// sWeek.Append(@" 5b ====== does NOT need Header ");	
					sWeek.Append(@"<hr><div class=""row"">");
					sWeek.Append(@"<div class=""col-xs-2"">" + anAppt 		+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + aName 		+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + aService		+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + aReg 		+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + aLine 		+ "</div>");
					sWeek.Append(@"<div class=""col-xs-2"">" + string.Format("{0:00000}",anAmount)	+ "</div>");
					sWeek.Append(@"</div>");
					
					sWeek.Append(@"<div style=""color:grey"" class=""row"">");
					sWeek.Append(@"<div class=""col-xs-2""></div>");
					sWeek.Append(@"<div class=""col-xs-10""><small>" + aNote + "</small></div>");

					sWeek.Append(@"</div>");
					
					aTotal = aTotal + anAmount;
					aWeekTotal = aWeekTotal + anAmount;
				}
				
				//ADD THE INCOME TOTAL AT FOOTER TO THE LAST WEEK IF ITS A NEW WEEK
				if(needsHeader){
					//add footer to last week before starting new week 
					sbtest.Append(@"<h3><div class=""row"">");
					sbtest.Append(@"<div class=""col-xs-10""></div>");
					sbtest.Append(@"<div class=""col-xs-2"">" + string.Format("{0:00000}",aWeekTotal)	+ "</div>");
					sbtest.Append(@"</h3></div>");
					needsHeader = false;
					aWeekTotal = 0;	
				}
				else{
					sbtest.Append(sWeek);
				}
					
				sWeek = new System.Text.StringBuilder();
				lastWE = curWE;

			}
			
		}

		
		// sbtest.Append(@"<div class=""row"">");
		// sbtest.Append(@"<div class=""col-xs-10"">");
		// sbtest.Append(@"<div class=""col-xs-2"">" + string.Format("{0:00000}",aTotal	)	+ "</div>");
		// sbtest.Append(@"</div>");

		sbtest.Append(@"</div>");

        Eight.InnerHtml = sbtest.ToString();
        
	}	

	protected void text_change(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
	}

	protected void text_change_reg(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE lookup SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
	}
	
	protected void text_change_addo(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE bis SET {0} = @TEXT WHERE id=@ID", text.ID);		
		SqlCmd(sqlCommandStatement, id, text.Text);
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
	}
		
    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET status = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(HeadText.Text == "Combined BIS")
			DataGrid_Load(DAL.reg("all"), "reg");
		
    }

    protected void Selection_Change_Reg(object sender, EventArgs e)
    {
        DropDownList ddlReg = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET area = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlReg.Text);	
		if(HeadText.Text == "Combined BIS")
			DataGrid_Load(DAL.reg("all"), "reg");
		
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
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");		
	}

	protected void Rank_Change(object sender, EventArgs e)
	{
		DropDownList ddlRank = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET rank = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlRank.Text);		
		if(HeadText.Text == "Combined GI Grid")
			DataGrid_Load(DAL.reg("all"), "reg");
    }

    public void LineUp_Add_Click(Object sender, EventArgs e)
	{		
		Button clickedButton = sender as Button;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		TextInfo textInfo = cultureInfo.TextInfo;
		
        string addo = gvRow.Cells[0].Text;
        string name = gvRow.Cells[2].Text;
		string phone = gvRow.Cells[6].Text;
		
		name = textInfo.ToTitleCase(name.ToLower());
		
		SqlCmd(String.Format("INSERT into reg(reg_cat_id, status, org, name, phone, addo_id) values('LineUp', 'Now Prospect', '{0}', '{1}', '{2}', '{3}')", OrgText.Text, name, phone, addo));
	}

	public virtual void ShowEditButton(Object sender, EventArgs e) { 
	
        Button clickedButton = sender as Button;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		
	}

    public void Edit_Click(Object sender, EventArgs e)
    {
        Button clickedButton = sender as Button;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		TextInfo textInfo = cultureInfo.TextInfo;

        string addo = gvRow.Cells[0].Text;        
        string name = gvRow.Cells[2].Text;
		
		
    }
		
    public void LineUp_Add_Click_TTL(Object sender, EventArgs e)
    {
        Button clickedButton = sender as Button;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		TextInfo textInfo = cultureInfo.TextInfo;

        string addo = gvRow.Cells[0].Text;        
        string name = gvRow.Cells[2].Text;

		name = textInfo.ToTitleCase(name.ToLower());

        SqlCmd(String.Format("INSERT into reg(reg_cat_id, status, org, name, addo_id) values('LineUp', 'Now Prospect', '{0}', '{1}', '{2}')", OrgText.Text, name, addo));
    }

	protected void Display(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

        string id = gvRow.Cells[0].Text;
		addo_addoid.Text = gvRow.Cells[1].Text;
		
		TextBox name 	= (TextBox)gvRow.FindControl("name");		
		addonameid.Text = name.Text;

		DropDownList dl = (DropDownList)gvRow.FindControl("ddlReg");
		ddlReg.Text = dl.Text;

		dl = (DropDownList)gvRow.FindControl("ddlStatus");
		addo_statusid.Text = dl.Text;
		
		TextBox service	= (TextBox)gvRow.FindControl("service");		
		addo_serviceid.Text = service.Text;
		
		TextBox tb = (TextBox)gvRow.FindControl("scheduled");
		apptid3.Text = tb.Text;

		dl = (DropDownList)gvRow.FindControl("ddlOrg");
		addo_orgid.Text = dl.Text;
		
		// dl = (DropDownList)gvRow.FindControl("line");
		// lineid3.Text = dl.Text;
		
		TextBox reg		= (TextBox)gvRow.FindControl("reg");		
		TextBox fsm		= (TextBox)gvRow.FindControl("fsm");
		TextBox phone	= (TextBox)gvRow.FindControl("phone");
		TextBox em		= (TextBox)gvRow.FindControl("email");

		TextBox org		= (TextBox)gvRow.FindControl("org");

		// ddlReg.Text = strArea;

		regid2.Text = reg.Text;
		fsmid.Text = fsm.Text;
		addophoneid.Text = phone.Text;
		email.Text = em.Text;

		

		
		// TextBox tb = (TextBox)row.FindControl("name");
		// string vTxt = tb.Text; // get the value from TextBox		
		// lblnameid.Text = vTxt;

		
/*
		
		
		DropDownList dl = (DropDownList)gvRow.FindControl("ddlRank");
		string vTxt = dl.Text; // get the value from TextBox		
		rankid.Text = vTxt;
		

		TextBox tb = (TextBox)gvRow.FindControl("service");
		vTxt = tb.Text; // get the value from TextBox		
		serviceid.Text = vTxt;

		tb = (TextBox)gvRow.FindControl("reg");
		vTxt = tb.Text; // get the value from TextBox		
		regid.Text = vTxt;


		tb = (TextBox)gvRow.FindControl("phone");
		vTxt = tb.Text; // get the value from TextBox		
		phoneid.Text = vTxt;

		tb = (TextBox)gvRow.FindControl("email");
		vTxt = tb.Text; // get the value from TextBox		
		edit_email.Text = vTxt;


		tb = (TextBox)gvRow.FindControl("fsm");
		vTxt = tb.Text; // get the value from TextBox		
		bird_dogid.Text = vTxt;
		
		dl = (DropDownList)gvRow.FindControl("ddlOrg");
		vTxt = dl.Text; // get the value from TextBox		
		orgid.Text = vTxt;

		tb = (TextBox)gvRow.FindControl("notes");
		vTxt = tb.Text; // get the value from TextBox		
		notesid.Text = vTxt;
*/		

		id2.Text = id; 
		
		// ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		
	}

	protected void ViewAddo(object sender, EventArgs e)
	{
		Button clickedButton = sender as Button;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		TextInfo textInfo = cultureInfo.TextInfo;
		
        string id = gvRow.Cells[0].Text;
        string name = gvRow.Cells[2].Text;
		
		// int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        // GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		
		// TextBox tb = (TextBox)row.FindControl("Name");
		// string vTxt = tb.Text; // get the value from TextBox	
		
		addonameid.Text = name;
		addo_addoid.Text = id;
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		
	}

	protected void OpenAddNew(object sender, EventArgs e)
	{
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
	}
	
	protected void ViewArchive(object sender, EventArgs e)
	{
		Button clickedButton = sender as Button;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		TextInfo textInfo = cultureInfo.TextInfo;
		
        string id = gvRow.Cells[0].Text;
		addo_addoid.Text = gvRow.Cells[1].Text;
		
		TextBox name 	= (TextBox)gvRow.FindControl("name");		
		DropDownList dl = (DropDownList)gvRow.FindControl("area");
		string strArea = dl.Text; // get the value from TextBox		
		
		TextBox service	= (TextBox)gvRow.FindControl("service");		
		TextBox reg		= (TextBox)gvRow.FindControl("reg");		
		TextBox fsm		= (TextBox)gvRow.FindControl("fsm");
		TextBox phone	= (TextBox)gvRow.FindControl("phone");
		TextBox em		= (TextBox)gvRow.FindControl("email");

		TextBox line	= (TextBox)gvRow.FindControl("line");
		TextBox org		= (TextBox)gvRow.FindControl("org");

		addonameid.Text = name.Text;
		ddlReg.Text = strArea;

		addo_serviceid.Text = service.Text;
		regid2.Text = reg.Text;
		fsmid.Text = fsm.Text;
		addophoneid.Text = phone.Text;
		email.Text = em.Text;
		
		//ErrorText.Text = "ID=" + addonameid.Text;
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		
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
 	
	protected void DisplayAdd(object sender, EventArgs e)
	{

		string vTxt = "Now Prospect"; // get the value from TextBox		
		statusid.Text = vTxt;

		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddModal();", true);
		
	}
 
	protected void btnSave_Click(object sender, EventArgs e)
	{
		//Your Saving code.
		
		
		
	}
	
    protected void Selection_Change_Page(object sender, EventArgs e)
	{
	//
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
	{						
		string id = (string)e.Values["ID"].ToString();
		SqlCmd("Delete from bis where id = @ID", id);
        DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
    }

	public void Add_Click(Object sender, EventArgs e)
	{
		SqlCmd(String.Format("INSERT into bis(reg_cat_id, org) values('{0}', '{1}')", HeadText.Text, OrgText.Text));
        DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
    }

	protected void DeleteReg(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
		GridViewRow row = GridViewLookup.Rows[rowIndex];
		string vId = row.Cells[0].Text;		
		RegistrarID.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteReg();", true);
		
	}

	public void btnDeleteReg_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["RegistrarID"]);	
			string sqlCommandStatement = String.Format("DELETE FROM lookup WHERE id='{0}'", id );									
			try
			{		
				using (SqlConnection conn = new SqlConnection(GetConnectionString("reg")))
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

			GridView gv = GridViewLookup;
			GridView_Load(gv, DAL.reg("config")); 
			
		} 
    }

	public void btnAddReg_Click(Object sender, EventArgs e)
	{				
		
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string desc1 = String.Format("{0}", 		Request.Form["desc1"]);	
			string desc2 = String.Format("{0}", 		Request.Form["desc2"]);	
			string desc3 = String.Format("{0}", 		Request.Form["desc3"]);	
					
			using(SqlConnection connection = new SqlConnection(GetConnectionString("reg")))
			{
				String query = "INSERT into lookup(type, desc1, desc2, desc3) "
							 + "VALUES ('bis', @desc1, @desc2, @desc3)";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{

					command.Parameters.AddWithValue("@desc1",		desc1);
					command.Parameters.AddWithValue("@desc2",		desc2);
					command.Parameters.AddWithValue("@desc3",		desc3);

					connection.Open();
					int result = command.ExecuteNonQuery();

					ClearAddRegModal();
					
					// Check Error
					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
				}
			}
		} 
		GridView gv = GridViewLookup;
		GridView_Load(gv, DAL.reg("config")); 
		
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
	
	public void ClearAddRegModal()
	{

		desc1.Text = "";
		desc2.Text = "";
		desc3.Text = "";
		
	} 
	
	public void ClearAddModal()
	{

		nameid.Text = "";
		amountid2.Text = "";
//		rankid2.Text = "";
//		datefor.Text = "";
		serviceid2.Text = "";
//		regid2.Text = "";
		apptid2.Text = "";
		//lineid2.Text = "";
//		statusid2.Text = "";
		bird_dogid2.Text = "";
		phoneid2.Text = "";
		orgid.Text = "";
//		notesid2 = "";
		
	} 
	
	public void btnSubmit_Click(Object sender, EventArgs e)
	{				
		
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string id = String.Format("{0}", 		Request.Form["id"]);	
			string name = String.Format("{0}", 		Request.Form["nameid"]);	
			string amount = String.Format("{0}", 	Request.Form["amountid2"]);	
			string rank = String.Format("{0}", 		Request.Form["rankid2"]);	
			string type = String.Format("{0}", 		Request.Form["datefor"]);				
			string service = String.Format("{0}", 	Request.Form["serviceid2"]);	
			string reg = String.Format("{0}", 		Request.Form["ddlReg"]);	
			string scheduled = String.Format("{0}", Request.Form["apptid2"]);		
			string line = String.Format("{0}", 		Request.Form["lineid2"]);				
			string status = String.Format("{0}", 	HeadText.Text);				
			string bird_dog = String.Format("{0}", 	Request.Form["bird_dogid2"]);				
			string phone = String.Format("{0}", 	Request.Form["phoneid2"]);				
			//string org = String.Format("{0}", 	  Request.Form["orgid"]);				
			string notes = String.Format("{0}", 	Request.Form["notesid2"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());
			
			if (String.IsNullOrEmpty(scheduled))
				scheduled = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
			
			using(SqlConnection connection = new SqlConnection(GetConnectionString("reg")))
			{
				String query = "INSERT into reg(reg_cat_id, org, name, amount, service, reg, phone, appt, scheduled_type, rank, line, status, bird_dog, notes) "
							 + "VALUES (@reg_cat_id,@org,@name, @amount, @service, @reg, @phone, @appt, @scheduled_type, @rank, @line, @status, @bird_dog, @notes)";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{
					// tmp hack until refactoring completed - HeadText.Text needs to be LineUp for now to not break things
					command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
					command.Parameters.AddWithValue("@org",				OrgText.Text);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@amount",			amount);
					command.Parameters.AddWithValue("@service",			service);
					command.Parameters.AddWithValue("@reg",				reg);
					command.Parameters.AddWithValue("@phone",			phone);
					command.Parameters.AddWithValue("@appt",			scheduled);
					command.Parameters.AddWithValue("@scheduled_type",	type);
					command.Parameters.AddWithValue("@rank",			rank);
					command.Parameters.AddWithValue("@line",			line);
					command.Parameters.AddWithValue("@status",			status);
					command.Parameters.AddWithValue("@bird_dog",		bird_dog);
					command.Parameters.AddWithValue("@notes",			notes);

					connection.Open();
					int result = command.ExecuteNonQuery();

					DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");

					ClearAddModal();
					
					// Check Error
					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
				}
			}
			
			// string sql = String.Format("INSERT into reg(reg_cat_id, org, name, amount, service, reg, phone, appt, scheduled_type, rank, line, status, bird_dog, notes) values( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')", "LineUp", OrgText.Text, name, amount, service, reg, phone, scheduled, type, rank, line, status, bird_dog, notes);

		} 
    }

	public void btnDelete_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["id"]);	
			string sqlCommandStatement = String.Format("DELETE FROM bis WHERE id='{0}'", id );									
			try
			{		
				using (SqlConnection conn = new SqlConnection(GetConnectionString("reg")))
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

			if(HeadText.Text=="Combined BIS"){

				if(searchCol != "" && searchText != "")
					DataGrid_Load(DAL.Search_Combo(searchText, searchCol), "reg");
				else
					DataGrid_Load(DAL.reg("all"), "reg");
			}
			else {

				if(searchCol != "" && searchText != "")
					DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
				else
					DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
			}

		} 
    }

	public void btnAddAddo_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string id = String.Format("{0}", 		Request.Form["id2"]);	
			string addoid = String.Format("{0}", 	Request.Form["addo_addoid"]);	
			string name = String.Format("{0}", 		Request.Form["addonameid"]);	
			string org = String.Format("{0}", 		Request.Form["addo_orgid"]);	
			string rank = String.Format("{0}", 		Request.Form["addo_rankid"]);	
			string status = String.Format("{0}", 	Request.Form["addo_statusid"]);				
			string service = String.Format("{0}", 	Request.Form["addo_serviceid"]);	
			string area = String.Format("{0}", 		Request.Form["ddlReg"]);	
			string reg = String.Format("{0}", 		Request.Form["regid2"]);	
			string fsm = String.Format("{0}", 		Request.Form["fsmid"]);				
			string scheduled = String.Format("{0}", Request.Form["apptid3"]);		
			string line = String.Format("{0}", 		Request.Form["lineid3"]);				
			string phone = String.Format("{0}", 	Request.Form["addophoneid"]);				
			string email  = String.Format("{0}", 	Request.Form["email"]);				
			string notes = String.Format("{0}", 	Request.Form["addo_noteid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			if(HeadText.Text=="Add Name" || HeadText.Text =="Archive"){

				if (String.IsNullOrEmpty(scheduled)){

					using(SqlConnection connection = new SqlConnection(GetConnectionString("reg")))
					{
						String query = "INSERT into bis(reg_cat_id, addo_id, name, rank, status, service, area, reg, fsm, phone, email, line, org, notes) "
									 + "VALUES (@reg_cat_id, @addo_id, @name, @rank, @status, @service, @area, @reg, @fsm, @phone, @email, @line, @org, @notes)";
									 
						using(SqlCommand command = new SqlCommand(query, connection))
						{
							command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
							command.Parameters.AddWithValue("@addo_id",			addoid);
							command.Parameters.AddWithValue("@name",			name);
							command.Parameters.AddWithValue("@rank",			rank);
							command.Parameters.AddWithValue("@status",			status);
							command.Parameters.AddWithValue("@service",			service);
							command.Parameters.AddWithValue("@area",			area);
							command.Parameters.AddWithValue("@reg",				reg);
							command.Parameters.AddWithValue("@fsm",				fsm);
							command.Parameters.AddWithValue("@phone",			phone);
							command.Parameters.AddWithValue("@email",			email);
							command.Parameters.AddWithValue("@line",			line);
							command.Parameters.AddWithValue("@org",				org);
							command.Parameters.AddWithValue("@notes",			notes);

							connection.Open();

							int result = command.ExecuteNonQuery();
							//DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
							ClearAddoModal();
							
							StartingPage();
							
							// Check Error
							if(result < 0){
								ErrorText.Text = "Error inserting data into Database!";
							}
						}
					}
				
				} else {
					
					using(SqlConnection connection = new SqlConnection(GetConnectionString("reg")))
					{
						String query = "INSERT into bis(reg_cat_id, addo_id, name, rank, status, service, area, reg, fsm, phone, email, scheduled, line, org, notes) "
									 + "VALUES (@reg_cat_id, @addo_id, @name, @rank, @status, @service, @area, @reg, @fsm, @phone, @email, @scheduled, @line, @org, @notes)";
									 
						using(SqlCommand command = new SqlCommand(query, connection))
						{
							command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
							command.Parameters.AddWithValue("@addo_id",			addoid);
							command.Parameters.AddWithValue("@name",			name);
							command.Parameters.AddWithValue("@rank",			rank);
							command.Parameters.AddWithValue("@status",			status);
							command.Parameters.AddWithValue("@service",			service);
							command.Parameters.AddWithValue("@area",			area);
							command.Parameters.AddWithValue("@reg",				reg);
							command.Parameters.AddWithValue("@fsm",				fsm);
							command.Parameters.AddWithValue("@phone",			phone);
							command.Parameters.AddWithValue("@email",			email);
							command.Parameters.AddWithValue("@scheduled",		scheduled);
							command.Parameters.AddWithValue("@line",			line);
							command.Parameters.AddWithValue("@org",				org);
							command.Parameters.AddWithValue("@notes",			notes);

							connection.Open();

							int result = command.ExecuteNonQuery();
							
							//DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
							ClearAddoModal();

							StartingPage();
							
							// Check Error
							if(result < 0){
								ErrorText.Text = "Error inserting data into Database!";
							}
						}
					}
				}		
			} 
			else {

				if (String.IsNullOrEmpty(scheduled)){
			
					using(SqlConnection connection = new SqlConnection(GetConnectionString("reg")))
					{
						String query = "UPDATE bis SET name = @name, area = @area, service = @service, reg = @reg, phone = @phone, " + 
													" email = @email, rank = @rank, line = @line, status = @status, fsm	= @fsm, org	= @org, notes = @notes " + 
										" WHERE id= @id";	
									 
						using(SqlCommand command = new SqlCommand(query, connection))
						{
							command.Parameters.AddWithValue("@id",				id);
							command.Parameters.AddWithValue("@name",			name);
							command.Parameters.AddWithValue("@area",			area);
							command.Parameters.AddWithValue("@service",			service);
							command.Parameters.AddWithValue("@reg",				reg);
							command.Parameters.AddWithValue("@phone",			phone);
							command.Parameters.AddWithValue("@email",			email);
							command.Parameters.AddWithValue("@rank",			rank);
							command.Parameters.AddWithValue("@line",			line);
							command.Parameters.AddWithValue("@status",			status);
							command.Parameters.AddWithValue("@fsm",				fsm);
							command.Parameters.AddWithValue("@org",				org);
							command.Parameters.AddWithValue("@notes",			notes);

							connection.Open();
							
							int result = command.ExecuteNonQuery();
							if(HeadText.Text=="Combined BIS"){
								DataGrid_Load(DAL.reg("all"), "reg");
							} else {
								DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
							}
							ClearAddoModal();
							if(result < 0){
								ErrorText.Text = "Error inserting data into Database!";
							}
							
						}
					}

				} else {

					using(SqlConnection connection = new SqlConnection(GetConnectionString("reg")))
					{
						String query = "UPDATE bis SET name = @name, area = @area, service = @service, reg = @reg, phone = @phone, scheduled = @scheduled, " + 
													" email = @email, rank = @rank, line = @line, status = @status, fsm	= @fsm, org	= @org, notes = @notes " + 
										" WHERE id= @id";	
									 
						using(SqlCommand command = new SqlCommand(query, connection))
						{
							command.Parameters.AddWithValue("@id",				id);
							command.Parameters.AddWithValue("@name",			name);
							command.Parameters.AddWithValue("@area",			area);
							command.Parameters.AddWithValue("@service",			service);
							command.Parameters.AddWithValue("@reg",				reg);
							command.Parameters.AddWithValue("@phone",			phone);
							command.Parameters.AddWithValue("@scheduled",		scheduled);
							command.Parameters.AddWithValue("@email",			email);
							command.Parameters.AddWithValue("@rank",			rank);
							command.Parameters.AddWithValue("@line",			line);
							command.Parameters.AddWithValue("@status",			status);
							command.Parameters.AddWithValue("@fsm",				fsm);
							command.Parameters.AddWithValue("@org",				org);
							command.Parameters.AddWithValue("@notes",			notes);

							connection.Open();

							int result = command.ExecuteNonQuery();
							
							if(HeadText.Text=="Combined BIS"){
								DataGrid_Load(DAL.reg("all"), "reg");
							} else {
								DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
							}

							ClearAddoModal();
							
							// Check Error
							if(result < 0){
								ErrorText.Text = "Error inserting data into Database!";
							}
						}
					}
				}
			}
		} 
    }
 
	public void ClearAddoModal()
	{
		addo_addoid.Text = "";
		addonameid.Text = "";
		//addo_rankid.Text = "";
		addo_statusid.Text = "";
		addo_serviceid.Text = "";
		//areaid.Text = "";
		regid2.Text = "";
		fsmid.Text = "";
		apptid3.Text = "";
		lineid3.Text = "";
		addophoneid.Text = "";
		email.Text = "";
		addo_orgid.Text = "";
		addo_noteid.Text = "";
	} 
 
	protected void ShieldChart1a_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		// DataTable dataTable = new DataTable();
        // dataTable = DAL.GetBISAll("weekend", searchWE);
		
		// ShieldChart1a.DataSource = dataTable;
		
	}

	public void btnUpdate_Click(Object sender, EventArgs e)
	{				

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			// int id  = Request.Form["id"];	
			string id = String.Format("{0}", 		Request.Form["id"]);	
			string name = String.Format("{0}", 		Request.Form["lblnameid"]);	
			string area = String.Format("{0}", 		Request.Form["edit_area"]);	
			string rank = String.Format("{0}", 		Request.Form["rankid"]);	
			string service = String.Format("{0}", 	Request.Form["serviceid"]);	
			string reg = String.Format("{0}", 		Request.Form["regid"]);	
			string scheduled = String.Format("{0}", Request.Form["edit_scheduled"]);	
			string email = String.Format("{0}", 	Request.Form["edit_email"]);				
			string line = String.Format("{0}", 		Request.Form["lineid"]);				
			string status = String.Format("{0}", 	Request.Form["statusid"]);				
			string fsm = String.Format("{0}", 		Request.Form["bird_dogid"]);				
			string phone = String.Format("{0}", 	Request.Form["phoneid"]);				
			string org = String.Format("{0}", 		Request.Form["orgid"]);				
			string notes = String.Format("{0}", 	Request.Form["notesid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			ErrorText.Text = area;
			
			string sqlCommandStatement = "";
			
			if(scheduled != null) {
				
				sqlCommandStatement = String.Format("UPDATE bis SET name = '{0}', area = '{1}', " + 
															" service 		= '{2}', " + 
															" reg 			= '{3}', " + 
															" phone 		= '{4}', " + 
															" scheduled		= '{5}', " + 
														    " email 		= '{6}', " + 
															" rank	 		= '{7}', " + 
															" line	 		= '{8}', " + 
															" status	 	= '{9}', " + 
															" fsm		 	= '{10}', " + 
															" org		 	= '{11}', " + 
															" notes	 		= '{12}' " + 
															" WHERE id='{13}'", 
															name.Replace("'", "''"), 
															area, 
															service.Replace("'", "''"), 
															reg.Replace("'", "''"), 
															phone, 
															scheduled, 
															email, 
															rank, 
															line, 
															status, 
															fsm.Replace("'", "''"), 
															org,
															notes.Replace("'", "''"), 
															id
															);			

			} else {
				
				sqlCommandStatement = String.Format("UPDATE bis SET name = '{0}', area = '{1}', " + 
															" service 		= '{2}', " + 
															" reg 			= '{3}', " + 
															" phone 		= '{4}', " + 
														    " email 		= '{5}', " + 
															" rank	 		= '{6}', " + 
															" line	 		= '{7}', " + 
															" status	 	= '{8}', " + 
															" fsm		 	= '{9}', " + 
															" org		 	= '{10}', " + 
															" notes	 		= '{11}' " + 
															" WHERE id='{12}'", 
															name.Replace("'", "''"), 
															area, 
															service.Replace("'", "''"), 
															reg.Replace("'", "''"), 
															phone, 
															email, 
															rank, 
															line, 
															status, 
															fsm.Replace("'", "''"), 
															org,
															notes.Replace("'", "''"), 
															id
															);			
				
				
				
				
			}
															
			
			try
			{		
				using (SqlConnection conn = new SqlConnection(GetConnectionString("reg")))
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
			if(HeadText.Text=="Combined BIS"){

				if(searchCol != "" && searchText != "")
					DataGrid_Load(DAL.Search_Combo(searchText, searchCol), "reg");
				else
					DataGrid_Load(DAL.reg("all"), "reg");
			}
			else {

				if(searchCol != "" && searchText != "")
					DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
				else
					DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
			}
		} 
    }

	private void DataGrid_Load(DataTable command, string type)
	{	
		DataTable dataTable = new DataTable();
        dataTable = command;
		string[] ComboGiGridTotal = new string[5];
		string sortDir = ViewState["SortDirection"] as string;
		string sortExp = ViewState["SortExpression"] as string;
		
		if(ViewState["SortExpression"] != null)
		{					
			dataTable = resort(dataTable, sortExp, sortDir);
		}

		if (HeadText.Text == "Combined BIS")
		{		
			One.Visible = false;
			Two.Visible = false;
			Three.Visible = false;
			Four.Visible = true;
            Five.Visible = false;
			Six.Visible = true;
			Seven.Visible = false;
			InvoiceTable.Visible = false;

			SchedLbl.Text = "";
			NamedLbl.Text = "";
			// BothBoardLblb.Text = "";
			
            try // TOTALS
			{
				var query = from c in dataTable.AsEnumerable() 
				           where c.Field<string>("status") == "In The Shop" && c.Field<string>("reg_cat_id") != "Archive"
						  select c ;	
				
				string ResultBothIn = "0";
				string ResultNamed = "0";
				string ResultBothScheduled = "0";
				string ResultBothInSched = "0";
				string ResultBothInSchedNamed  = "0";
				
				string ResultBothOn = "0";
				string ResultDayIn = "0";
				string ResultDayOn = "0";
				string ResultDayConf = "0";
				string ResultDayNamed = "0";
				string ResultDayInv = "0";

				string ResultFdnIn = "0";
				string ResultFdnOn = "0";
				string ResultFdnConf = "0";
				string ResultFdnInv = "0";
				string ResultFdnNamed = "0";
						  
				var cmdBIS 		 	= "SELECT count(1) AS total from bis WHERE status = 'In The Shop' and reg_cat_id <> 'Archive'";
				var cmdScheduled 	= "SELECT count(1) AS total from bis WHERE status = 'Scheduled' and reg_cat_id <> 'Archive'";
				var cmdNamed 	 	= "SELECT count(1) AS total from bis WHERE status = 'Named' and reg_cat_id <> 'Archive'";

				var cmdBISDay 		 = "SELECT count(1) AS total from bis WHERE status = 'In The Shop' and reg_cat_id <> 'Archive' and org = 'Day'";
				var cmdScheduledDay  = "SELECT count(1) AS total from bis WHERE status = 'Scheduled' and reg_cat_id <> 'Archive' and org = 'Day'";
				var cmdNamedD	 	 = "SELECT count(1) AS total from bis WHERE status = 'Named' and reg_cat_id <> 'Archive' and org = 'Day'";
				var cmdInSchedD      = "SELECT count(1) AS total from bis WHERE status in ('In The Shop', 'Scheduled') and reg_cat_id <> 'Archive' and org = 'Day'";
				var cmdInSchedNamedD = "SELECT count(1) AS total from bis WHERE status in ('In The Shop', 'Scheduled', 'Named') and reg_cat_id <> 'Archive' and org = 'Day'";
				    
				var cmdBISFdn 		 = "SELECT count(1) AS total from bis WHERE status = 'In The Shop' and reg_cat_id <> 'Archive' and org = 'Fdn'";
				var cmdScheduledFdn  = "SELECT count(1) AS total from bis WHERE status = 'Scheduled' and reg_cat_id <> 'Archive' and org = 'Fdn'";
				var cmdNamedFdn 	 = "SELECT count(1) AS total from bis WHERE status = 'Named' and reg_cat_id <> 'Archive' and org = 'Fdn'";
				var cmdInSchedF      = "SELECT count(1) AS total from bis WHERE status in ('In The Shop', 'Scheduled') and reg_cat_id <> 'Archive' and org = 'Fdn'";
				var cmdInSchedNamedF = "SELECT count(1) AS total from bis WHERE status in ('In The Shop', 'Scheduled', 'Named') and reg_cat_id <> 'Archive' and org = 'Fdn'";

				var cmdInSchedBoth   = "SELECT count(1) AS total from bis WHERE status in ('In The Shop', 'Scheduled') and reg_cat_id <> 'Archive'";
				var cmdInSchedNamedBoth   = "SELECT count(1) AS total from bis WHERE status in ('In The Shop', 'Scheduled', 'Named') and reg_cat_id <> 'Archive'";
				
				var cmdInvD 	= "SELECT count(1) AS total from bis WHERE status = 'In The Shop' and org = 'Day' and reg_cat_id <> 'Archive' ";
				var cmdConfD 	= "SELECT count(1) AS total from bis WHERE status = 'Scheduled' and org = 'Day' and reg_cat_id <> 'Archive'";
				var cmd3 		= "SELECT count(1) AS total from bis WHERE reg_cat_id <> 'Archive' and status = 'In The Shop' and org = 'Fdn'";
				var cmd4 		= "SELECT count(1) AS total from bis WHERE status in ('In The Shop', 'Scheduled') and reg_cat_id <> 'Archive'";
				var cmd5 		= "SELECT count(1) AS total from bis WHERE org = 'Day' and ((status in ('In The Shop', 'Scheduled') and reg_cat_id <> 'Archive') or (status = 'Scheduled' and rank = 'a' and reg_cat_id <> 'Archive'))";
				var cmd6 		= "SELECT count(1) AS total from bis WHERE org = 'Fdn' and ((status in ('In The Shop', 'Scheduled') and reg_cat_id <> 'Archive') or (status = 'Scheduled' and rank = 'a' and reg_cat_id <> 'Archive'))";
					
				using (SqlConnection conn = new SqlConnection(GetConnectionString(type)))
				{				
			        	conn.Open();
					try{
						ResultBothIn = String.Format("{0:D}", cmdBIS);
						ComboGiGridTotal[0] += cmdBIS.ToString();
						
						using (SqlCommand Cmd = new SqlCommand(cmdBIS, conn))
            				{
								ResultBothIn = String.Format("{0:D}", Cmd.ExecuteScalar());
								ComboGiGridTotal[0] += Cmd.ExecuteScalar().ToString();
        	    			}
					}
					catch(Exception e) {
						ErrorText.Text = e.ToString();
					}
					try{	
						using (SqlCommand Cmd = new SqlCommand(cmdScheduled, conn))					
						{
							ResultBothScheduled = String.Format("{0:D}", Cmd.ExecuteScalar());
							//ResultBothScheduled = String.Format(new CultureInfo("en-ie"), "{0:00000}", Cmd.ExecuteScalar());
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
						using (SqlCommand Cmd = new SqlCommand(cmdInSchedD, conn))					
						{
							ResultDayIn = String.Format("{0:D}", Cmd.ExecuteScalar());
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
						using (SqlCommand Cmd = new SqlCommand(cmdConfD, conn))					
						{
							ResultDayConf = String.Format("{0:D}", Cmd.ExecuteScalar());
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
						using (SqlCommand Cmd = new SqlCommand(cmdBISFdn, conn))					
						{
							ResultFdnInv = String.Format("{0:D}", Cmd.ExecuteScalar());
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
						using (SqlCommand Cmd = new SqlCommand(cmd4, conn))
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

				BothBIS.Text = ResultBothIn;				
				BothScheduled.Text = ResultBothScheduled;				
				BothNamed.Text = ResultNamed;				
				BothInSched.Text = ResultBothInSched;				
				BothInSchedNamed.Text = ResultBothInSchedNamed;

				DayIn.Text = ResultDayIn;				
				DayInv.Text = ResultDayInv;				
				DayConf.Text = ResultDayConf;				
				DayNamed.Text = ResultDayNamed;				
				DayOn.Text = ResultDayOn;

				FdnIn.Text = ResultFdnIn;
				FdnInv.Text = ResultFdnInv;				
				FdnConf.Text = ResultFdnConf;				
				FdnNamed.Text = ResultFdnNamed;				
				FdnOn.Text = ResultFdnOn;				

				BothInLbl.Text = String.Format("{0:D}", ResultBothIn);	
				SchedLbl.Text = String.Format("{0:D}", ResultBothOn);	
				NamedLbl.Text = String.Format("{0:D}", ResultNamed);	

				var mySum = 0;
				
				if(query.Any()){
					DataTable t2 = query.CopyToDataTable();
					GridView4.DataSource = t2;
					GridView4.DataBind();				
				} else {
					//ErrorText1.Text = "(None)";	
					GridView4.DataSource = new DataTable();
					GridView4.DataBind();
				}
			}			
			catch(Exception e) {
				ErrorText.Text = "Caught Exception: " + e;				
			}
			try // SCHEDULED
			{
				var query = from c in dataTable.AsEnumerable() 
					where c.Field<string>("status") == "Scheduled" 
					where c.Field<string>("reg_cat_id") != "Archive"
					select c ;

				if(query.Any()){
					DataTable t2 = query.CopyToDataTable();
					GridView5.DataSource = t2;
					GridView5.DataBind();				
				} else {
					//ErrorTextA.Text = "No Data Found";	
					GridView5.DataSource = new DataTable();
					GridView5.DataBind();
				}
				
				// var cmdBIS = "SELECT SUM(amount) AS total from bis WHERE reg_cat_id = 'lineup' and rank = 'a'";
				var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Scheduled'";
				string ResultGood = "0";
				using (SqlConnection conn = new SqlConnection(GetConnectionString(type)))
				{				
			        	conn.Open();
					try{
						using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
            			{
							ResultGood = String.Format("{0:D}", Cmd.ExecuteScalar());
							ComboGiGridTotal[1] += Cmd.ExecuteScalar().ToString();
                        }
					}
					catch{}							
					conn.Close();
        		}	
				SchedLbl.Text = ResultGood;				
			}			
			catch(Exception e) {
				ErrorText.Text = "Caught Exception: " + e;				
			}
			try // NAMED
			{

				var query = from c in dataTable.AsEnumerable() where c.Field<string>("status") == "Named"  select c ;
				var mySum = 0;
				
				if(query.Any()){
					DataTable t2 = query.CopyToDataTable();
					GridView6.DataSource = t2;
					GridView6.DataBind();
					
				} else {
					//ErrorTextB.Text = "No Data Found";	
					GridView6.DataSource = new DataTable();
					GridView6.DataBind();
				}

				// var cmd1 = "SELECT SUM(amount) AS total from bis WHERE reg_cat_id = 'lineup' and rank = 'b'";
				var cmd1 = "SELECT COUNT(1) AS total from bis WHERE status = 'Scheduled'";
				string ResultFigure = "0";
				using (SqlConnection conn = new SqlConnection(GetConnectionString(type)))
				{				
			        	conn.Open();
					try{
					using (SqlCommand Cmd = new SqlCommand(cmd1, conn))
            			{
							ResultFigure = String.Format("{0:D}", Cmd.ExecuteScalar());
							ComboGiGridTotal[2] += Cmd.ExecuteScalar().ToString();
                        }
					}
					catch{}							
					conn.Close();
        			}
				//NamedLbl.Text = ResultFigure;				
			}			
			catch(Exception e) {
				ErrorText.Text = "Caught Exception: " + e;				
			}
		}
		else if(type == "reg")
		{
			//ROLL THROUGH THE STATUSES AND CHECK WHICH ONE AND THEN FILTER THE DATATABLE WITH THIS STATUS
			if(HeadText.Text == "In The Shop") {
				
				DataRow[] dr = dataTable.Select("status = 'In The Shop'");
				DataTable filteredDataTable = dataTable.Clone();
				foreach (DataRow sourceRow in dr)
				{
				   filteredDataTable.ImportRow(sourceRow);  // or add all fields manually
				}
				
				if(ViewState["SortExpression"] != null)
				{					
					filteredDataTable = resort(filteredDataTable, sortExp, sortDir);
				}
				
				GridViewInvoice.DataSource = filteredDataTable;				
				GridViewInvoice.DataBind();
				One.Visible = false;
				InvoiceTable.Visible = true;
				ArchiveTable.Visible = false;
				
			} else if(HeadText.Text == "Archive") {

				DataRow[] dr = dataTable.Select("reg_cat_id = 'Archive'");
				DataTable filteredDataTable = dataTable.Clone();
				foreach (DataRow sourceRow in dr)
				{
				   filteredDataTable.ImportRow(sourceRow);
				}
				
				One.Visible = false;
				InvoiceTable.Visible = false;
				ArchiveTable.Visible = true;
				
				GridViewArchive.DataSource = filteredDataTable;
				GridViewArchive.DataBind();				
				
			}
			else {
				
				try{
					string myStatus = HeadText.Text;
					DataRow[] dr = dataTable.Select("status = '" + myStatus + "'");			

					DataTable filteredDataTable = dataTable.Clone();
					foreach (DataRow sourceRow in dr)
					{
					   filteredDataTable.ImportRow(sourceRow);
					}
					
					if(ViewState["SortExpression"] != null)
					{					
						filteredDataTable = resort(filteredDataTable, sortExp, sortDir);
					}
					
					One.Visible = true;
					InvoiceTable.Visible = false;
					ArchiveTable.Visible = false;		
					GridView1.DataSource = filteredDataTable;
					GridView1.DataBind();
				}
				catch(Exception e){
					ErrorText.Text = "Caught Exception: " + e;				
				}
			}

			Two.Visible = false;
			Three.Visible = false;
			Four.Visible = false;
            Five.Visible = false;
			Six.Visible = true;
			Seven.Visible = false;		
			
		}
		else if (HeadText.Text == "FP/PP List")
		{
			One.Visible = false;
			Two.Visible = false;
			Three.Visible = true;
			Four.Visible = false;
            Five.Visible = false;
			Six.Visible = true;
			Seven.Visible = false;
			InvoiceTable.Visible = false;
            GridView3.DataSource = dataTable;
			GridView3.DataBind();
		}
        else if (HeadText.Text == "Add Name")
        {
            One.Visible = false;
            Two.Visible = false;
            Three.Visible = false;
            Four.Visible = false;
            Five.Visible = true;
			Six.Visible = true;
			Seven.Visible = false;
			InvoiceTable.Visible = false;
            GridView8.DataSource = dataTable;
            GridView8.DataBind();
        }
	}	

	protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridView1.PageIndex = e.NewPageIndex;
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
		
    }

	protected void grdData_PageIndexChanging_bis(object sender, GridViewPageEventArgs e)
	{
		GridViewInvoice.PageIndex = e.NewPageIndex;
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
		
    }
	
	protected void grdArchiveData_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridViewArchive.PageIndex = e.NewPageIndex;
		
		if(searchCol != "" && searchText != ""){
			if (searchWE=="")
				DataGrid_Load(DAL.ArchiveSearch(searchText, searchCol), "reg");				
			else
				DataGrid_Load(DAL.ArchiveWE_FilterSearch("weekend", searchWE, searchText, searchCol), "reg");				
		}
		else{
			if (searchWE=="")
				DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
			else
				DataGrid_Load(DAL.ArchiveWESearch("weekend", searchWE), "reg");				
		}
    }

    protected void grdData_PageIndexChanging_total(object sender, GridViewPageEventArgs e)
	{
		GridView8.PageIndex = e.NewPageIndex;
        DataGrid_Load(DAL.addo(), "cf");
    }

	protected void grdData_PageIndexChanging_Inv(object sender, GridViewPageEventArgs e)
	{
		GridView2.PageIndex = e.NewPageIndex;
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_Inv(searchText, searchCol), "cf");
		else
			DataGrid_Load(DAL.Inv_Table(), "cf");

    }

    protected void grdData_PageIndexChanging_FPPP(object sender, GridViewPageEventArgs e)
    {

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
			using (SqlConnection conn = new SqlConnection(GetConnectionString(type)))
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
                    {
						Cmd.ExecuteNonQuery();
					}						
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
	
	public static List<DateTime> getThursdays(){
	
		var list = new List<DateTime>();
        DateTime firstThursday = new DateTime(2018,02,01);
        var numberOfThursdayWanted = 1000;
        for (int i = 0; i < numberOfThursdayWanted; ++i)
        {
            list.Add(firstThursday.AddDays(i*7));
        }
        return list;
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

public class dal
{
    public DataTable reg(string head, string org = null)
    {
        if (head == "all")
            return Data_Load(String.Format("SELECT * from bis"), "reg");
		else if(head == "Archive")
			return Data_Load(String.Format("SELECT * from bis where reg_cat_id = 'Archive'"), "reg");
		else if(head == "config")
			return Data_Load(String.Format("SELECT * from lookup where type = 'bis' Order by desc1"), "reg");
        else
			//return Data_Load(String.Format("SELECT * from bis where reg_cat_id = '{0}' and org = '{1}' order by rank, name", head, org), "reg");        
			return Data_Load(String.Format("SELECT * from bis where status = '{0}' and org = '{1}' and reg_cat_id <> 'Archive'", head, org), "reg");        
    }
    
    public DataTable Calendar(string head, string org)
    {
     return Data_Load(String.Format("SELECT appt, status, name, service, reg, amount, line, rank, scheduled_type, notes from bis where reg_cat_id in('Future') and org = '{1}' order by appt ASC", head, org), "reg");
	}

    public DataTable CurrentWeek(string org)
    {
     return Data_Load(String.Format("SELECT appt, status, name, service, reg, amount, line, rank, scheduled_type, reg_cat_id, notes from bis where status in('Scheduled', 'GI Confirmed', 'In The Shop') and org = '{0}' order by rank, appt ASC", org), "reg");
    }

    public DataTable Archive(string org)
    {
     return Data_Load(String.Format("SELECT * from bis where reg_cat_id in('Archive') and org = '{0}' order by scheduled DESC", org), "reg");
    }

    public DataTable Inv_Table()
    {
        return Data_Load("select item.desc1 + ' ' + item.desc2 as item, inv.amt_paid, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
            + "+ addo.last_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
            + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
            + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
            + "WHERE i.inv_type in ('DONATION', 'CASH', 'CREDIT') and addo.last_name > '' and addo.first_name > '' and inv.amt_paid > 0 ", "cf");
    }

    public DataTable FPPP()
    {
        return Data_Load("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
            + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
            + "INNER JOIN item_ref as item on fppp.item_id = item.item_id ", "cf");
    }

    public DataTable TTL()
    {
        return Data_Load("select name, addo_id, sum(inv_total) as tot_paid from dbo.pers_invoice where inv_type in ('DONATION', 'CASH', 'CREDIT') group by name, addo_id having sum(inv_total) > 50 order by tot_paid desc", "cf");
    }

    public DataTable addo()
    {
        return Data_Load("select addo_id, first_name + ' ' + last_name as name, city from dbo.pers_addo where mail_stat in (1,2,5,7,8) and len(first_name) > 0 and len(last_name) > 0", "cf");
    }

    public DataTable Search(string status, string Org, string Search, string Text)
    {
		return Data_Load(String.Format("SELECT * from bis WHERE status = '{0}' and org = '{1}' and {2} like '%{3}%' and reg_cat_id <> 'Archive'", status, Org, Search, Text), "reg");
    }

    public DataTable ArchiveSearch(string Search, string Text)
    {
		return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id = 'Archive' and {0} like '%{1}%'", Search, Text), "reg");
    }

    public DataTable ArchiveWESearch(string Search, string Text)
    {
		return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id = 'Archive' and {0} = '{1}'", Search, Text), "reg");
    }
	
    public DataTable ArchiveWE_FilterSearch(string Search, string Text, string Search2, string Text2)
    {
		return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id = 'Archive' and {0} = '{1}' and {2} like '%{3}%'", Search, Text, Search2, Text2), "reg");
    }

    public DataTable Search_Combo(string Search, string Text)
    {
		return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id <> 'Archive' and {0} like '%{1}%'", Search, Text), "reg");
    }
	
    public DataTable Search_FPPP(string Search, String Text)
    {
        if (Search == "Name")
        {
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where addo.first_name + ' ' + addo.last_name like '%{0}%'", Text), "cf");
        }
        else if (Search == "Service")
        {
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
            + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
            + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where item.desc1 + ' ' + item.desc2 like '%{0}%'", Text), "cf");
        }
        else
            return null;
    }

    public DataTable Search_Inv(string Search, String Text)
    {
        if (Search == "Name")
        {
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, inv.amt_paid, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
             + "+ addo.last_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
             + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
             + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
             + "WHERE i.inv_type in ('DONATION', 'CASH', 'CREDIT') and addo.last_name > '' and addo.first_name > '' and inv.amt_paid > 0 "
             + "and addo.first_name + ' ' + addo.last_name like '%{0}%'", Text), "cf");
        }
        else if (Search == "Service")
        {
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, inv.amt_paid, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
             + "+ addo.last_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
             + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
             + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
             + "WHERE i.inv_type in ('DONATION', 'CASH', 'CREDIT') and addo.last_name > '' and addo.first_name > '' and inv.amt_paid > 0 "
             + "and item.desc1 + ' ' + item.desc2 like '%{0}%'", Text), "cf");
        }
        else
            return null;
    }

    public DataTable Search_Addo(string Search, String Text)
    {       
            return Data_Load(String.Format("select addo_id, first_name + ' ' + last_name as name, city from dbo.pers_addo where mail_stat in (1,2,5,7,8) and len(first_name) > 0"
                + " and (first_name + ' ' + last_name like '%{1}%' or city like '%{1}%')", Search, Text), "cf");
    }
	
    public DataTable Search_TTL(string Search, String Text)
    {       
            return Data_Load(String.Format("select name, addo_id, sum(inv_total) as tot_paid from dbo.pers_invoice where inv_type in ('DONATION', 'CASH', 'CREDIT') "
                + " and {0} like '%{1}%' group by name, addo_id having sum(inv_total) > 50 order by tot_paid desc", Search, Text), "cf");
    }

	public DataTable Report_By_Lines(string Name)
	{		
		if(Name == "")
			return Data_Load(String.Format("select line, count(1) as num from bis where reg_cat_id = 'archive' group by line ", Name), "reg");
		else
			return Data_Load(String.Format("select line, count(1) as num from bis where reg like '%{0}%' and reg_cat_id = 'archive' group by line ", Name), "reg");
	}

    public DataTable GetProcurementPie(string fromWE = null, string toWE = null)
    {
		return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'archive' and line is not null and tm > getdate() - 7 group by line"), "reg");
	
    }

    public DataTable GetBISAll(string Search, string Text)
    {
		return Data_Load(String.Format("SELECT area, count(1) as total from bis WHERE reg_cat_id = 'Archive' and {0} = '{1}' Group By area", Search, Text), "reg");
    }
	
    private DataTable Data_Load(string command, string type)
    {
        DataTable dataTable = new DataTable();        
        try
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString(type)))
            {
                conn.Open();
                using (SqlCommand Cmd = new SqlCommand(command, conn))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(Cmd))
                    {
                        dataAdapter.Fill(dataTable);                        
                    }
                }
                conn.Close();
            }
            return dataTable;
        }

        catch {
            return null;
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