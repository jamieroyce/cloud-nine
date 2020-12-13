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
	static string vCat = "Event";

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

			DataTable regList = DAL.getEvents(); 
			
			ddlReg_Addo.DataSource = regList;
			ddlReg_Addo.DataTextField = regList.Columns["event_desc"].ToString();
			ddlReg_Addo.DataValueField = regList.Columns["event_desc"].ToString();
			ddlReg_Addo.DataBind();
			
			txtAddoSearch.Focus();
			
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

		txtAddoSearch.Text = "";
		ErrorText.Text = "";
        
        RowLable.Visible = true;
		
        if (ViewState["SortExpression"] != null)
            ViewState["SortExpression"] = null;

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
		
        DataGrid_Load(DAL.addo(), "cf");
		
		searchText = "";
		searchCol = "";
		searchWE = "";
		
		ClearAddoModal();
		
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
        
    }
		
	
    public void BtnSearch_Click_Addo(object sender, EventArgs e)
    {
		searchText = "";
		searchCol = txtAddoSearch.Text;
		DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
    }
	
    protected void Selection_Change_Page(object sender, EventArgs e)
	{
		if(searchCol != "")
			DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
		else
			DataGrid_Load(DAL.addo(), "cf");
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
			string reg = String.Format("{0}", 		Request.Form["addo_regid"]);	
			string event_desc = String.Format("{0}", Request.Form["ddlReg_Addo"]);	
			string status = String.Format("{0}", 	Request.Form["addo_statusid"]);				
			string notes = String.Format("{0}", 	Request.Form["addo_noteid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			//SOMEHOW CHECK IF THE NAME ALREADY EXISTS. FOR EVENT IT WOULD BE IF SAME NAME IN SAME EVENT WITH SAME ADDO ID EXISTS. 
			
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into bis(reg_cat_id, addo_id, name, service, status, reg, scheduled, weekend, org, notes) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @service, @status, @reg, getdate(), getdate(), @org, @notes)";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@reg_cat_id",		vCat);
					command.Parameters.AddWithValue("@addo_id",			addoid);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@service",			event_desc);
					command.Parameters.AddWithValue("@status",			status);
					command.Parameters.AddWithValue("@reg",				reg);
					command.Parameters.AddWithValue("@org",				org);
					command.Parameters.AddWithValue("@notes",			notes);

					connection.Open();
					int result = command.ExecuteNonQuery();
					// Check Error
					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
					ClearAddoModal();
					Response.Redirect("log.aspx");
					
				}
			}
		} 
    }
 
	public void ClearAddoModal()
	{
		addo_addoid.Text = "";
		addonameid.Text = "";
		addo_orgid.Text = "";
		addo_noteid.Text = "";
		
	} 
 
	protected void ViewAddo(object sender, EventArgs e)
	{
		Button clickedButton = sender as Button;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		TextInfo textInfo = cultureInfo.TextInfo;
		
        string id = gvRow.Cells[0].Text;
        string name = Server.HtmlDecode(gvRow.Cells[2].Text);
		
		addonameid.Text = name;
		addo_addoid.Text = id;
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		
	}

	protected void OpenAddNew(object sender, EventArgs e)
	{
		ClearAddoModal();
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
		addonameid.Text = txtAddoSearch.Text;
		addonameid.Focus();
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
		
		GridViewAddo.DataSource = dataTable;
		GridViewAddo.DataBind();
		
	}	

    protected void grdData_PageIndexChanging_addo(object sender, GridViewPageEventArgs e)
	{
		GridViewAddo.PageIndex = e.NewPageIndex;

		if(searchCol != "")
			DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
		else
			DataGrid_Load(DAL.addo(), "cf");
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
			
		if(searchCol != "")
			DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
		else
			DataGrid_Load(DAL.addo(), "cf");

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
