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
		
		// ShieldChartThisWeekShieldChartThisWeek.DataSource = DAL.GetProcurementPie("thisweek", fromWE, toWE);
		// ShieldChartLastWeek.DataSource = DAL.GetProcurementPie("lastweek", fromWE, toWE);
		// ShieldChartPieAll.DataSource = DAL.GetProcurementPie("all", fromWE, toWE);
		// ShieldChart2.DataSource = DAL.GetIncomeByWeek("Arrival", graphWeeks);
		// ShieldChart3.DataSource = DAL.GetIncomeByWeek("CF", graphWeeks);
		// ShieldChart4.DataSource = DAL.GetIncomeByWeek("FSM", graphWeeks);
		// ShieldChart5.DataSource = DAL.GetIncomeByWeek("Prospecting", graphWeeks);
		// ShieldChart6.DataSource = DAL.GetIncomeByWeek("Resign", graphWeeks);
		
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

	protected void ShieldChartLastWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = "";
		toWE = "";

		ShieldChartLastWeek.DataSource = DAL.GetProcurementPie("lastweek", fromWE, toWE);

	}

	protected void ShieldChartThisWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = "";
		toWE = "";

		DataTable dt = new DataTable();
		dt = DAL.GetProcurementPie("thisweek", fromWE, toWE);
		
		if(dt.Rows.Count>0){
			ShieldChartThisWeek.DataSource = dt;
		} else {
			ShieldChartThisWeek.SecondaryHeader.Text = "(No Data Found)";
		}

	}

	protected void ShieldChart0_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		giweeks.Text = graphWeeks;
		ShieldChart0.DataSource = DAL.GetIncomeByWeek("All", graphWeeks);

	}

	
	protected void ShieldChartPieAll_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = "";
		toWE = "";

		ShieldChartPieAll.DataSource = DAL.GetProcurementPie("all", fromWE, toWE);

	}

	protected void ShieldChartPie1_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "1");
		String regName = (String)reg.Rows[0][0];
		regid1.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetProcurementPieReg(regName);
			
		if(dt!=null)
			ShieldChartPie1.DataSource = dt;

	}

	protected void ShieldChartPie2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "2");
		String regName = (String)reg.Rows[0][0];
		regid2.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetProcurementPieReg(regName);
			
		if(dt!=null)
			ShieldChartPie2.DataSource = dt;

	}

	protected void ShieldChartPie3_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "3");
		String regName = (String)reg.Rows[0][0];
		regid3.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetProcurementPieReg(regName);
			
		if(dt!=null)
			ShieldChartPie3.DataSource = dt;

	}

	protected void ShieldChartPie4_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "4");
		String regName = (String)reg.Rows[0][0];
		regid4.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetProcurementPieReg(regName);
			
		if(dt!=null)
			ShieldChartPie4.DataSource = dt;

	}

	protected void ShieldChartPie5_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "5");
		String regName = (String)reg.Rows[0][0];
		regid5.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetProcurementPieReg(regName);
			
		if(dt!=null)
			ShieldChartPie5.DataSource = dt;

	}

	protected void ShieldChartPie6_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		DataTable reg = new DataTable();
		reg = DAL.GetReg("name", "6");
		String regName = (String)reg.Rows[0][0];
		regid6.Text = regName;
		
		DataTable dt = new DataTable();
		dt = DAL.GetProcurementPieReg(regName);
			
		if(dt!=null)
			ShieldChartPie6.DataSource = dt;

	}
	
	private class Daily7R
	{
		public string dayofweek { get; set; }
		public string thisweekgi { get; set; }
		public string lastweekgi { get; set; }
		public string thisweekcum { get; set; }
		public string lastweekcum { get; set; }
	}
	
	public void clickTest(Object sender, EventArgs e){

		string dateStr = "";
		DateTime today = DateTime.Now;             	// Use current time.
		string format = "dd-MMM-yyyy";   			// Use this format.
		dateStr = today.ToString(format); 	// Write to console.
		ErrorText.Text = dateStr;
		
	}
	
    public void BtnRange_Click(object sender, EventArgs e)
    {

		fromWE = "";
		toWE = "";
		ErrorText.Text = fromWE + " " + toWE;
		
		// ShieldChart1.DataSource = DAL.GetProcurementPie(fromWE, toWE);

	}

	protected void ShieldChart2_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		ShieldChart2.DataSource = DAL.GetIncomeByWeek("Arrival", graphWeeks);

	}
	
	protected void ShieldChart3_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		ShieldChart3.DataSource = DAL.GetIncomeByWeek("CF", graphWeeks);

	}

	protected void ShieldChart4_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		ShieldChart4.DataSource = DAL.GetIncomeByWeek("FSM", graphWeeks);

	}	
	
	protected void ShieldChart5_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		ShieldChart5.DataSource = DAL.GetIncomeByWeek("Prospecting", graphWeeks);

	}	

	protected void ShieldChart6_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		ShieldChart6.DataSource = DAL.GetIncomeByWeek("Resign", graphWeeks);

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
