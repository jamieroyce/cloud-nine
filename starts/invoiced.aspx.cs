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
		Title = "GI Invoiced";
		HeadText.Text = "GI Invoiced";
		HeaderText.Text = "GI Invoiced";
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

        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		
        DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
		
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
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
        
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
		DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
        
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
															" notes	 		= '{12}' " + 
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
			if(HeadText.Text=="Combined GI Grid"){

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

	public void Archive_Click(object sender, EventArgs e)
	{

		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string we = weekendingText.Text;	

			int i = 0;
			GridView gv = (GridView)(sender as Control).Parent.FindControl("GridViewInvoice");		
			foreach (GridViewRow row in gv.Rows)
			{
				i++;				
				string rowID 		= row.Cells[0].Text;
				TextBox name 	= (TextBox)row.FindControl("name");
				TextBox amount 	= (TextBox)row.FindControl("Amount");
				TextBox service = (TextBox)row.FindControl("Service");
				TextBox reg 	= (TextBox)row.FindControl("Reg");
				TextBox appt 	= (TextBox)row.FindControl("Appt");
				DropDownList line = (DropDownList)row.FindControl("ddlLine");				
				
				if((we != null && we != "" && we != " " && name.Text != " " && amount.Text != " " && service.Text != " " && reg.Text != " " && appt.Text != " " && line.Text != " ") 
					&& (name.Text != "" && amount.Text != "" && service.Text != "" && reg.Text != "" && appt.Text != "" && line.Text != "") 
					&& (name.Text != null && amount.Text != null && service.Text != null && reg.Text != null && appt.Text != null && line.Text != null))
				{																	
				
				
					string sqlCommandStatement = String.Format( 
						"UPDATE reg SET REG_CAT_ID = '{0}', TM = '{1}' WHERE ID = '{2}'	" , 
																"Archive"	, 
																we			,
																rowID
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

					DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");			
				}
				else{
					if(we == null || we == "" || we == " ")
						ErrorText.Text += "Please select a Weekending Date... <br />";
					else
						ErrorText.Text += "Row " + i + " is Incomplete (Name, Amount, Service, Reg, Appt and Line are required Fields)<br />";
				}
			}
	
		}

	}
		
	protected void text_change(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE reg SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
	}
	
    public void BtnSearch_Click(object sender, EventArgs e)
    {
		searchText = ddlSearch.Text;
		searchCol = txt.Text;
		DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
    }

	protected void DisplayInvoice(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
		GridViewRow row = GridViewInvoice.Rows[rowIndex];
		
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
		
		ErrorText.Text = vId;
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
	
    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE reg SET status = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(HeadText.Text == "Combined GI Grid")
			DataGrid_Load(DAL.reg("all"), "reg");
		
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

    protected void Selection_Change_Page(object sender, EventArgs e)
	{
		if(searchCol != "" && searchText != ""){
			if (searchWE=="")
				DataGrid_Load(DAL.ArchiveSearch(searchText, searchCol), "reg");				
			else
				DataGrid_Load(DAL.ArchiveWE_FilterSearch("tm", searchWE, searchText, searchCol), "reg");				
		}
		else{
			if (searchWE=="")
				DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");
			else
				DataGrid_Load(DAL.ArchiveWESearch("tm", searchWE), "reg");				
		}
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

	protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridViewInvoice.PageIndex = e.NewPageIndex;
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
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
		
		DataRow[] dr = dataTable.Select("status = 'GI Invoiced'");
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

		int a = 0;
		int x = 0;

		AmountText.Attributes["style"] = "display: ;";
		var test = dataTable.AsEnumerable().Select(row => row.Field<int?>("amount") != null ? row.Field<int?>("amount") : 0);		
		foreach (int item in test)
			a += item;
		AmountText.Text = string.Format("{0:C0}", a);
		
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
			DataGrid_Load(DAL.Search(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.reg(HeadText.Text, OrgText.Text), "reg");

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

public class dal
{
    public DataTable reg(string head, string org = null)
    {
        if (head == "all")
            return Data_Load(String.Format("SELECT * from reg"), "reg");
		else if(head == "Archive")
			return Data_Load(String.Format("SELECT * from reg where reg_cat_id = 'Archive'"), "reg");
		else if(head == "config")
			return Data_Load(String.Format("SELECT * from listReg Order by short_name"), "reg");
		else if(org == "Combined")
			return Data_Load(String.Format("SELECT * from reg where status = '{0}' and reg_cat_id <> 'Archive'", head), "reg");        
		else
			//return Data_Load(String.Format("SELECT * from reg where reg_cat_id = '{0}' and org = '{1}' order by rank, name", head, org), "reg");        
			return Data_Load(String.Format("SELECT * from reg where status = '{0}' and org = '{1}' and reg_cat_id <> 'Archive'", head, org), "reg");        
    }

    public DataTable get_bis(string status, string addoid)
    {
		return Data_Load(String.Format("SELECT * from bis where status = '{0}' and addo_id = '{1}' and reg_cat_id <> 'Archive'", status, addoid), "reg");        
	}

    public DataTable Calendar(string head, string org)
    {
     return Data_Load(String.Format("SELECT appt, status, name, service, reg, amount, line, rank, scheduled_type, notes from reg where reg_cat_id in('Future') and org = '{1}' order by appt ASC", head, org), "reg");
	}

    public DataTable CurrentWeek(string org)
    {
     return Data_Load(String.Format("SELECT appt, status, name, service, reg, amount, line, rank, scheduled_type, reg_cat_id, notes from reg where status in('Open Cycle', 'GI Confirmed', 'GI Invoiced') and org = '{0}' order by rank, appt ASC", org), "reg");
    }

    public DataTable Archive(string org)
    {
     return Data_Load(String.Format("SELECT appt, status, name, service, reg, amount, line, rank, scheduled_type, notes from reg where reg_cat_id in('Archive') and org = '{0}' order by appt DESC", org), "reg");
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

    public DataTable Appt(string day)
    {
        if(day == ""){
			return Data_Load("select * from regAppt where day = format(getdate(), 'd-MMM-yyyy')", "reg");
		} else{
			return Data_Load(String.Format("select * from regAppt where day = '{0}' order by hour_txt", day), "reg");
		}
    }

    public DataTable addo()
    {
        return Data_Load("select addo_id, first_name + ' ' + last_name as name, city from dbo.pers_addo where mail_stat in (1,2,5,7,8) and len(first_name) > 0 and len(last_name) > 0", "cf");
    }
	
    public DataTable Search(string status, string Org, string Search, string Text)
    {
		return Data_Load(String.Format("SELECT * from reg WHERE status = '{0}' and org = '{1}' and {2} like '%{3}%' and reg_cat_id <> 'Archive'", status, Org, Search, Text), "reg");
    }

    public DataTable ArchiveSearch(string Search, string Text)
    {
		return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id = 'Archive' and {0} like '%{1}%'", Search, Text), "reg");
    }

    public DataTable ArchiveWESearch(string Search, string Text)
    {
		return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id = 'Archive' and {0} = '{1}'", Search, Text), "reg");
    }
	
    public DataTable ArchiveWE_FilterSearch(string Search, string Text, string Search2, string Text2)
    {
		return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id = 'Archive' and {0} = '{1}' and {2} like '%{3}%'", Search, Text, Search2, Text2), "reg");
    }

    public DataTable Search_Combo(string Search, string Text)
    {
		return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id <> 'Archive' and {0} like '%{1}%'", Search, Text), "reg");
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

    public DataTable Search_TTL(string Search, String Text)
    {       
            return Data_Load(String.Format("select name, addo_id, sum(inv_total) as tot_paid from dbo.pers_invoice where inv_type in ('DONATION', 'CASH', 'CREDIT') "
                + " and {0} like '%{1}%' group by name, addo_id having sum(inv_total) > 50 order by tot_paid desc", Search, Text), "cf");
    }

    public DataTable Search_Addo(string Search, String Text)
    {       
            return Data_Load(String.Format("select addo_id, first_name + ' ' + last_name as name, city from dbo.pers_addo where mail_stat in (1,2,5,7,8) and len(first_name) > 0"
                + " and (first_name + ' ' + last_name like '%{1}%' or city like '%{1}%')", Search, Text), "cf");
    }
	
	public DataTable Report_By_Lines(string Name)
	{		
		if(Name == "")
			return Data_Load(String.Format("select line, count(1) as num from reg where reg_cat_id = 'archive' group by line ", Name), "reg");
		else
			return Data_Load(String.Format("select line, count(1) as num from reg where reg like '%{0}%' and reg_cat_id = 'archive' group by line ", Name), "reg");
	}

    private DataTable Data_Load(string command, string type)
    {
        DataTable dataTable = new DataTable();        
        try
        {
            using (SqlConnection conn = databaseConnection.CreateSqlConnection())
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