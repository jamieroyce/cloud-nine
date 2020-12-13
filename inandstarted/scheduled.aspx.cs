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
		ErrorText.Text = "";
		OrgText.Visible = true;
        ddlPage.Visible = true;
		AmountText.Visible = true;
        RowLable.Visible = true;		
		Title = "Scheduled";
		HeadText.Text = Title;
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
		
        DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
		
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
		DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
        
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
		DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
        
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
		DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
        
    }
	
	public void Org_Btn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		Button clickedButton = sender as Button;		
		Title = clickedButton.Text;
		OrgText.Text = clickedButton.Text;
		DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
        
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
				DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
		} 
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

    public void BtnSearch_Click(object sender, EventArgs e)
    {
		searchText = ddlSearch.Text;
		searchCol = txtBIS.Text;
		DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		
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

		tb = (TextBox)row.FindControl("fsm");
		vTxt = tb.Text; // get the value from TextBox		
		fsmid.Text = vTxt;
		
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
		string vId = row.Cells[0].Text;		
		id.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteModal();", true);
		
	}
	
    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET status = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
    }

	protected void Selection_Change_Org(object sender, EventArgs e)
	{
		DropDownList ddlOrg = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET org = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlOrg.Text);		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");

	}

    protected void Selection_Change_Page(object sender, EventArgs e)
	{
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
    }

	protected void Selection_Change(object sender, EventArgs e)
	{
		DropDownList ddlCat = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET reg_cat_id = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlCat.Text);
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");

	}

	protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridViewScheduled.PageIndex = e.NewPageIndex;
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
		
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
	
    protected void Selection_Change_Reg(object sender, EventArgs e)
    {
        DropDownList ddlReg = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE bis SET area = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlReg.Text);	
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");
		
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
				DataGrid_Load(DAL.Search_Combo_BIS(searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.bis("all"), "reg");

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
		try // 
		{
			var query = from c in dataTable.AsEnumerable() 
					   where c.Field<string>("status") == "Scheduled" && c.Field<string>("reg_cat_id") == "In and Started"
					  select c ;	
			
		int a = 0;
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				a = t2.Rows.Count;
				GridViewScheduled.DataSource = t2;
				GridViewScheduled.DataBind();
			} else {
				//ErrorText1.Text = "(None)";	
				GridViewScheduled.DataSource = new DataTable();
				GridViewScheduled.DataBind();
			}

		AmountText.Text = string.Format("{0}", a);
			
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
			DataGrid_Load(DAL.Search_BIS(HeadText.Text, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(HeadText.Text, OrgText.Text), "reg");


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

