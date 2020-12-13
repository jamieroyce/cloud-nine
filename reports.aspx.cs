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

/*using IronPdf;
*/
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

        OrgText.Visible = false;
		searchText = "";
		searchCol = "";
		searchWE = "";
		
		ShieldChartThisWeek.DataSource = DAL.GetProcurementPie("thisweek", fromWE, toWE);
		ShieldChartLastWeek.DataSource = DAL.GetProcurementPie("lastweek", fromWE, toWE);
		ShieldChartPieAll.DataSource = DAL.GetProcurementPie("all", fromWE, toWE);
		ShieldChart0.DataSource = DAL.GetIncomeByWeek("All", graphWeeks);		
		ShieldChart2.DataSource = DAL.GetIncomeByWeek("Arrival", graphWeeks);
		ShieldChart3.DataSource = DAL.GetIncomeByWeek("CF", graphWeeks);
		ShieldChart4.DataSource = DAL.GetIncomeByWeek("FSM", graphWeeks);
		ShieldChart5.DataSource = DAL.GetIncomeByWeek("Prospecting", graphWeeks);
		ShieldChart6.DataSource = DAL.GetIncomeByWeek("Resign", graphWeeks);

	}

	
	public void PrintPage(Object sender, EventArgs e)
	{

		// Create a PDF from any existing web page
/*		var Renderer = new IronPdf.HtmlToPdf();
		var PDF = Renderer.RenderUrlAsPdf("http://10.131.16.4/reports.aspx");
		*/
			
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


	public void Day_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Day";
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
        
    }

	public void Fdn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Fdn";
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
        
    }

	public void Combined_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		OrgText.Text = "Combined";
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
        
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


	protected void ShieldChartLastWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = fromText.Text;
		toWE = toText.Text;

		ShieldChartLastWeek.DataSource = DAL.GetProcurementPie("lastweek", fromWE, toWE);

	}

	protected void ShieldChartThisWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = fromText.Text;
		toWE = toText.Text;

		ShieldChartThisWeek.DataSource = DAL.GetProcurementPie("thisweek", fromWE, toWE);

	}

	protected void ShieldChartPieAll_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		fromWE = fromText.Text;
		toWE = toText.Text;

		ShieldChartPieAll.DataSource = DAL.GetProcurementPie("all", fromWE, toWE);

	}
	
	// protected void ShieldChart7R_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	// { 
		
		// fromWE = fromText.Text;
		// toWE = toText.Text;
		
		// DataTable dt = new DataTable();
		// dt = DAL.GetDailyGI("thisweek");

		// ShieldChart7R.DataSource = dt;

	// }

    public void BtnRange_Click(object sender, EventArgs e)
    {

		fromWE = fromText.Text;
		toWE = toText.Text;
		ErrorText.Text = fromWE + " " + toWE;
		
		// ShieldChart1.DataSource = DAL.GetProcurementPie(fromWE, toWE);

	}
	
	protected void ShieldChart0_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 

		
		ShieldChart0.DataSource = DAL.GetIncomeByWeek("All", graphWeeks);

	}

	protected void ShieldChartbyReg_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetIncomeBar("all");
		ShieldChartbyReg.DataSource = dt;

	}
	
	protected void ShieldChartbyRegThisWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetIncomeBar("thisweek");
		ShieldChartbyRegThisWeek.DataSource = dt;

	}
	
	protected void ShieldChartbyRegLastWeek_TakeDataSource(object sender, Shield.Web.UI.ChartTakeDataSourceEventArgs e) 
	{ 
		
		DataTable dt = new DataTable();
		dt = DAL.GetIncomeBar("lastweek");
		ShieldChartbyRegLastWeek.DataSource = dt;

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

	protected void Selection_GraphWeek(object sender, EventArgs e)
	{
		DropDownList ddlCat = sender as DropDownList;
		graphWeeks = ddlCat.Text; // get the value from TextBox		

		ShieldChart0.DataSource = DAL.GetIncomeByWeek("All", graphWeeks);
		
		
		if(graphWeeks=="12"){

			ShieldChart0.DataSource = DAL.GetIncomeByWeek("All", "12");
			
		}
		else if(graphWeeks=="26"){

			ShieldChart0.DataSource = DAL.GetIncomeByWeek("All", "26");
		
		}
		else{
			
			ShieldChart0.DataSource = DAL.GetIncomeByWeek("All", "52");
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
