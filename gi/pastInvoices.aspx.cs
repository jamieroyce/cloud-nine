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
			ddlReg_Addo.DataSource = regList;
			ddlReg_Addo.DataTextField = regList.Columns["full_name"].ToString();
			ddlReg_Addo.DataValueField = regList.Columns["full_name"].ToString();
			ddlReg_Addo.DataBind();
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
		ErrorText.Text = "";
		Title = "Past Invoices";
		HeadText.Text = "Invoice Table";
		searchText = "";
		searchCol = "";
		searchWE = "";
		OrgText.Visible = false;

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
		
        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		
		DataGrid_Load(DAL.Inv_Table(), "cf");
		
	}

    public void BtnSearch_Click_Inv(object sender, EventArgs e)
    {
		searchText = ddlSearchInv.Text;
		searchCol = TextBox2.Text;

        DataGrid_Load(DAL.Search_Inv(searchText, searchCol), "cf");
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
	
	public void Org_Btn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		Button clickedButton = sender as Button;		
		Title = clickedButton.Text;
		OrgText.Text = clickedButton.Text;
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
        
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

    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE reg SET status = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		
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
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");		
	}
	
	protected void Rank_Change(object sender, EventArgs e)
	{
		DropDownList ddlRank = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE reg SET rank = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlRank.Text);		
		if(HeadText.Text == "Combined GI Grid")
			DataGrid_Load(DAL.reg("all"), "reg");
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
	{						
		string id = (string)e.Values["ID"].ToString();
		SqlCmd("Delete from reg where id = @ID", id);
        DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
    }

	public void Add_Click(Object sender, EventArgs e)
	{
		SqlCmd(String.Format("INSERT into reg(reg_cat_id, org) values('{0}', '{1}')", HeadText.Text, OrgText.Text));
        DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
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
	
    public void Addresso_Click(Object sender, EventArgs e)
    {
		ErrorText.Text = "";
        OrgText.Visible = false;
        
        RowLable.Visible = true;
		
        if (ViewState["SortExpression"] != null)
            ViewState["SortExpression"] = null;
		Button clickedButton = sender as Button;
        HeadText.Text = clickedButton.Text;
        Title = clickedButton.Text;
		searchText = "";
		searchCol = "";
		searchWE = "";
        DataGrid_Load(DAL.addo(), "cf");
    }
	
	public void btnAddAddo_Click(Object sender, EventArgs e)
	{				
		
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			//string id = String.Format("{0}", 		Request.Form["id2"]);	
			string addoid = String.Format("{0}", 	Request.Form["addo_addoid"]);	
			string name = String.Format("{0}", 		Request.Form["addonameid"]);	
			string org = String.Format("{0}", 		Request.Form["addo_orgid"]);	
			// string rank = String.Format("{0}", 		Request.Form["addo_rankid"]);	
			string status = String.Format("{0}", 	Request.Form["addo_statusid"]);				
			string service = String.Format("{0}", 	Request.Form["addo_serviceid"]);	
			string amount = String.Format("{0}", 	Request.Form["add_amountid"]);	
			string reg = String.Format("{0}", 		Request.Form["ddlReg_Addo"]);	
			string bird_dog = String.Format("{0}", 	Request.Form["fsmid"]);				
			string line = String.Format("{0}", 		Request.Form["lineid3"]);				
			// string phone = String.Format("{0}", 	Request.Form["addophoneid"]);				
			string email  = String.Format("{0}", 	Request.Form["email"]);				
			string notes = String.Format("{0}", 	Request.Form["addo_noteid"]);				
			
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			name = textInfo.ToTitleCase(name.ToLower());

			ErrorText.Text = "ADDOID = " + addoid; 
			
			amount = amount.Replace(",", "");
			amount = amount.Replace(".", "");

			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into reg(reg_cat_id, addo_id, name, status, service, amount, reg, bird_dog, line, org, notes) "
							 + "VALUES (@reg_cat_id, @addo_id, @name, @status, @service, @amount, @reg, @bird_dog, @line, @org, @notes)";
							 
				using(SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@reg_cat_id",		"LineUp");
					command.Parameters.AddWithValue("@addo_id",			addoid);
					command.Parameters.AddWithValue("@name",			name);
					command.Parameters.AddWithValue("@status",			status);
					command.Parameters.AddWithValue("@service",			service);
					command.Parameters.AddWithValue("@amount",			amount );
					command.Parameters.AddWithValue("@reg",				reg);
					command.Parameters.AddWithValue("@bird_dog",		bird_dog);
					command.Parameters.AddWithValue("@line",			line);
					command.Parameters.AddWithValue("@org",				org);
					command.Parameters.AddWithValue("@notes",			notes);

					connection.Open();

					int result = command.ExecuteNonQuery();

					//DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
					
					ClearAddoModal();
					
					if(searchCol != "" && searchText != "")
						DataGrid_Load(DAL.Search_Inv(searchText, searchCol), "cf");
					else
						DataGrid_Load(DAL.Inv_Table(), "cf");
					
					// Check Error
					if(result < 0){
						ErrorText.Text = "Error inserting data into Database!";
					}
				}
			}
				//Response.Redirect("../index.aspx");
				//back to home
		} 
    }
 
	public void ClearAddoModal()
	{
		addo_addoid.Text = "";
		addonameid.Text = "";
		addo_serviceid.Text = "";
		add_amountid.Text = "";
		fsmid.Text = "";
		// addophoneid.Text = "";
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
		ClearAddoModal();
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openAddoModal();", true);
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

		GridView2.DataSource = dataTable;
		GridView2.DataBind();
		
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
			DataGrid_Load(DAL.Search_Inv(searchText, searchCol), "cf");
		else
			DataGrid_Load(DAL.Inv_Table(), "cf");
		
	}

	protected void grdData_PageIndexChanging_Inv(object sender, GridViewPageEventArgs e)
	{
		GridView2.PageIndex = e.NewPageIndex;
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_Inv(searchText, searchCol), "cf");
		else
			DataGrid_Load(DAL.Inv_Table(), "cf");

    }

    protected void Selection_Change_Page(object sender, EventArgs e)
	{
        if(HeadText.Text == "Invoice Table"){
			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search_Inv(searchText, searchCol), "cf");
			else
				DataGrid_Load(DAL.Inv_Table(), "cf");
		}
		else if(HeadText.Text == "FP/PP List"){
			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search_FPPP(searchText, searchCol), "cf");
			else
				DataGrid_Load(DAL.FPPP(OrgText.Text), "cf");
		}
        else if (HeadText.Text == "Total Payment List"){
			if(searchCol != "" && searchText != ""){
				DataGrid_Load(DAL.Search_TTL(searchText, searchCol), "cf");
			}
			else{
				DataGrid_Load(DAL.TTL(), "cf");
			}
		}
        else if (HeadText.Text == "Archive"){


		}
        else if (HeadText.Text == "Add Name"){
			if(searchCol != "")
				DataGrid_Load(DAL.Search_Addo(searchText, searchCol), "cf");
			else
				DataGrid_Load(DAL.addo(), "cf");
		}		
        else{
			if(searchCol != "" && searchText != "")
				DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
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
