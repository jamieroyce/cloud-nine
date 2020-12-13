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
	static string vStatus = "Scheduled";
	static string vCategory = "LineUp";

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
		Title = vStatus;
		HeadText.Text = Title;
		HeaderText.Text = vStatus;

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

        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		
        DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");
		PopulateBISAreas();
		
	}

	private void PopulateBISAreas()
	{
		DataTable dt = new DataTable();	
		
		dt = DAL.ReportAreaStatus(vCategory, vStatus);
		//ErrorText.Text += "DATASOURCE ROWS = " + dt.Rows.Count + "</br>";
		
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
	}


	public void FilterNamed(string area)
	{
		searchText = area;
		searchCol = "Area";
		DataGrid_Load(DAL.Search_BIS(vStatus, OrgText.Text, searchText, searchCol), "reg");
		
	}
		
    protected void LinkButton_Command(Object sender, EventArgs e) 
      {
		var v = ((LinkButton)sender).CommandArgument;
		//ErrorText.Text = "You chose: " + v;

		HeaderText.Text = Title;
		
		searchText = "Area";
		searchCol = v;
		
		HeaderText.Text += " (" + v + ")";
		
		DataGrid_Load(DAL.Search_BIS(vStatus, OrgText.Text, searchText, searchCol), "reg");
		
      }
	
	public void Day_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		searchText = "";
		searchCol = "";
		searchWE = "";
		HeaderText.Text = Title;
		
		OrgText.Text = "Day";
		Session["org"] = OrgText.Text;
		day.Attributes["class"] += " active";		
		fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
		cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		   
        DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");
        
    }

	public void Fdn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		searchText = "";
		searchCol = "";
		searchWE = "";
		HeaderText.Text = Title;
		
		OrgText.Text = "Fdn";
		Session["org"] = OrgText.Text;
		day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
		fdn.Attributes["class"] += " active";		
		cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		      
		DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");
        
    }

	public void Combined_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;

		searchText = "";
		searchCol = "";
		searchWE = "";
		HeaderText.Text = Title;
		
		OrgText.Text = "Combined";
		Session["org"] = OrgText.Text;
		day.Attributes.Add("class", day.Attributes["class"].ToString().Replace("active", ""));		      
		fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
		cmb.Attributes["class"] += " active";		
		DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");
        
    }
	
	public void Org_Btn_Click(Object sender, EventArgs e)
	{
		if(ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		Button clickedButton = sender as Button;		
		Title = clickedButton.Text;
		OrgText.Text = clickedButton.Text;
		DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");
        
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
				DataGrid_Load(DAL.Search_BIS(vStatus, OrgText.Text, searchText, searchCol), "reg");
			else
				DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");
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
		DataGrid_Load(DAL.Search_BIS(vStatus, OrgText.Text, searchText, searchCol), "reg");
		
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
        string sqlCommandStatement = "UPDATE bis SET status = @TEXT, scheduled = getdate(), weekend = getdate() WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(vStatus, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");

	}

	protected void Selection_Change_Org(object sender, EventArgs e)
	{
		DropDownList ddlOrg = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET org = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlOrg.Text);		

		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(vStatus, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");

	}

    protected void Selection_Change_Page(object sender, EventArgs e)
	{
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(vStatus, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");
    }

	protected void Selection_Change(object sender, EventArgs e)
	{
		DropDownList ddlCat = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE bis SET reg_cat_id = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlCat.Text);
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(vStatus, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");

	}

	protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridViewScheduled.PageIndex = e.NewPageIndex;
		
		if(searchCol != "" && searchText != "")
			DataGrid_Load(DAL.Search_BIS(vStatus, OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis(vStatus, OrgText.Text), "reg");
		
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
			DataGrid_Load(DAL.Search_BIS_log(OrgText.Text, searchText, searchCol), "reg");
		else
			DataGrid_Load(DAL.bis_log(HeadText.Text, OrgText.Text), "reg");
		
		PopulateBISAreas();
		
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
					   where c.Field<string>("status") == vStatus && c.Field<string>("reg_cat_id") == vCategory
					  select c ;	

			string ResultScheduled = "0";
			string ResultScheduledDay = "0";
			string ResultScheduledFdn = "0";
			
			var cmdScheduled 	 = "SELECT count(distinct org + name) AS total from bis WHERE status = 'Scheduled' and reg_cat_id = 'LineUp'";
			var cmdScheduledDay  = "SELECT count(distinct name) AS total from bis WHERE status = 'Scheduled' and reg_cat_id = 'LineUp' and org = 'Day'";
			var cmdScheduledFdn  = "SELECT count(distinct name) AS total from bis WHERE status = 'Scheduled' and reg_cat_id = 'LineUp' and org = 'Fdn'";

			using (SqlConnection conn = databaseConnection.CreateSqlConnection())
			{				
					conn.Open();
				try{
					using (SqlCommand Cmd = new SqlCommand(cmdScheduled, conn))
						{
							ResultScheduled = String.Format("{0:D}", Cmd.ExecuteScalar());
						}
				}
				catch(Exception e) {
					ErrorText.Text = e.ToString();
				}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdScheduledDay, conn))					
					{
						ResultScheduledDay = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
				try{	
					using (SqlCommand Cmd = new SqlCommand(cmdScheduledFdn, conn))					
					{
						ResultScheduledFdn = String.Format("{0:D}", Cmd.ExecuteScalar());
					}
				}
				catch {}
					
				conn.Close();
			}			
			
			if(OrgText.Text=="Combined"){
				AmountText.Text = String.Format("{0:D}", ResultScheduled);	
			} else if (OrgText.Text=="Day"){
				AmountText.Text = String.Format("{0:D}", ResultScheduledDay);	
			} else if (OrgText.Text=="Fdn"){
				AmountText.Text = String.Format("{0:D}", ResultScheduledFdn);	
			}
			
			if(query.Any()){
				DataTable t2 = query.CopyToDataTable();
				GridViewScheduled.DataSource = t2;
				GridViewScheduled.DataBind();
			} else {
				//ErrorText1.Text = "(None)";	
				GridViewScheduled.DataSource = new DataTable();
				GridViewScheduled.DataBind();
			}
			
		}			
		catch(Exception e) {
			ErrorText.Text = "Caught Exception: " + e;				
		}

		//ErrorText.Text += "HeadText.Text = " + HeadText.Text + "</BR>";
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

