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

    pdal PDAL = new pdal();
    dal DAL = new dal();

	// DataSet dstResults = new DataSet();
	// DataView myView;
	// DataView myAddoView; 

	static string searchText;
	static string searchCol;
	static string searchWE;
	static string showDay;
	static string vAddoId = "0";
	static string vPcId = "0";

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
			// DataTable regList = DAL.reg("config"); 
			
			StartingPage();
			
			DataTable pcList = PDAL.getPreclears(OrgText.Text);
			ddl_pc.DataSource = pcList;
			ddl_pc.DataTextField = pcList.Columns["name"].ToString();
			ddl_pc.DataValueField = pcList.Columns["name"].ToString();
			ddl_pc.DataBind();
			ddl_pc.Items.Insert(0, new ListItem("Choose a Preclear",""));
			
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
		Title = "Intensive Log";
		HeadText.Text = Title;
		HeaderText.Text = HeadText.Text;
		searchText = "";
		searchCol = "";
		searchWE = "";

		pcdata.Visible = false;
		
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
			OrgText.Text = "Day";
			day.Attributes["class"] += " active";		
			fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
			cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		   
		}
		
        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		
		searchText = "";
		searchCol = "";
		searchWE = "";
			
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
		LoadData();
		
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
		LoadData();
        
    }
	
	protected void Event_Changed(object sender, EventArgs e)
	{

		ErrorText.Text += "Changed the Event to: " + ddl_pc.SelectedItem.Text;
		
		string v = ddl_pc.SelectedItem.Text;
		
		// DataTable dt = DAL.getIntensives(v); //need addoid here or change the function to accept a name
		
		// string vNotes = (from DataRow dr in dt.Rows
					  // where (string)dr["event_desc"] == v
					  // select (string)dr["notes"]).FirstOrDefault();		
		
		// string vName = (from DataRow dr in dt.Rows
					  // where (string)dr["event_desc"] == v
					  // select (string)dr["name"]).FirstOrDefault();		

		// lblEventDetail.Text = vNotes;
		// lblEventCode.Text = vName;
		
	}	

	protected void LoadData()
	{

		//CHANGE TO GET AN ADDO ID BASED OFF THE NAME 
		DataTable dt = PDAL.getPreclear(ddl_pc.SelectedItem.Text);
		
		//GET THE NEW ADDO ID 
		for (int j = 0; j < dt.Rows.Count; j++)
		{
			for (int i = 0; i < dt.Columns.Count; i++)    
			{    
				if(dt.Columns[i].ColumnName=="addo_id"){
					vAddoId = dt.Rows[j].ItemArray[i].ToString();
				}
				if(dt.Columns[i].ColumnName=="id"){
					vPcId = dt.Rows[j].ItemArray[i].ToString();
				}
			}
		}

		// ErrorText.Text += "With PC_ID found as: " + vPcId + "</BR>";

		DataTable dtIntensives = PDAL.getIntensives(vPcId, OrgText.Text);
		DataTable dtAccount = PDAL.getAccount(vAddoId);
		DataTable dtInvoices = PDAL.getInvoices(vAddoId);
		DataTable dtTE = PDAL.getTE(vAddoId);
		
		GridView_Load(GridViewIntensive, dtIntensives);
		GridView_Load(GridViewAccount, dtAccount);
		GridView_Load(GridViewInvoice, dtInvoices);
		GridView_Load(GridViewTE, dtTE);

		pcdata.Visible = true;
	
	}
	
	protected void OnSelectedIndexChanged(object sender, EventArgs e)
	{

		// ErrorText.Text += "Changed the PC to: " + ddl_pc.SelectedItem.Value + "</BR>";

		HeaderText.Text = HeadText.Text;
		HeaderText.Text += " (" + ddl_pc.SelectedItem.Text + ")";
		
		// PASS THE DATA TO FILTER SEARCH 
		LoadData();
		
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
	
	public void btnAddIntensive_Click(Object sender, EventArgs e)
	{				
		
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string invNbr 		= String.Format("{0}", Request.Form["inv_nbr_ID"]);	
			string invStatus 	= String.Format("{0}", Request.Form["status_ID"]);	
			string dateStarted 	= String.Format("{0}", Request.Form["date_started_ID"]);	
			string weStarted 	= String.Format("{0}", Request.Form["we_started_ID"]);	
			string weCompleted 	= String.Format("{0}", Request.Form["we_completed_ID"]);	
			string vsdValue	 	= String.Format("{0}", Request.Form["vsd_value_ID"]);	
			string vsdCounted  	= String.Format("{0}", Request.Form["vsd_counted_ID"]);	
			string intMinutes  	= String.Format("{0}", Request.Form["intensive_minutes_ID"]);	
			string note 	 	= String.Format("{0}", Request.Form["note_ID"]);	
			string orgid		= "";
			
			if(OrgText.Text=="Day"){
				orgid 	 = System.Configuration.ConfigurationManager.AppSettings["orgid_day"];
			} else {
				orgid 	 = System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"];
			}
			
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into pc_intensives(pc_id, inv_nbr, status, date_started, we_started, we_completed, vsd_value, vsd_counted, intensive_minutes, note, org, org_id, addo_id, date_created, date_modified) "
							 + "VALUES (@pc_id, @inv_nbr, @status, @date_started, @we_started, @we_completed, @vsd_value, @vsd_counted, @intensive_minutes, @note, @org, @org_id, @addo_id, getdate(), getdate())";

				try
				{		
					using(SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@inv_nbr",				invNbr);
						command.Parameters.AddWithValue("@status",				invStatus);
						command.Parameters.AddWithValue("@vsd_value",			vsdValue);
						command.Parameters.AddWithValue("@vsd_counted",			vsdCounted);
						command.Parameters.AddWithValue("@intensive_minutes",	intMinutes);
						command.Parameters.AddWithValue("@note",				note);
						command.Parameters.AddWithValue("@addo_id",				vAddoId);
						command.Parameters.AddWithValue("@pc_id",				vPcId);
						command.Parameters.AddWithValue("@org_id",				orgid);
						command.Parameters.AddWithValue("@org",					OrgText.Text);

						if (dateStarted != ""){ 
							command.Parameters.AddWithValue("@date_started", dateStarted);
						} else { 
							command.Parameters.AddWithValue("@date_started", DBNull.Value); 
						}						

						if (weStarted != ""){ 
							command.Parameters.AddWithValue("@we_started", weStarted);
						} else { 
							command.Parameters.AddWithValue("@we_started", DBNull.Value); 
						}						

						if (weCompleted != ""){ 
							command.Parameters.AddWithValue("@we_completed", weCompleted);
						} else { 
							command.Parameters.AddWithValue("@we_completed", DBNull.Value); 
						}						

						connection.Open();

						int result = command.ExecuteNonQuery();

						ClearModal();
						
						if(result < 0){
							ErrorText.Text = "Error inserting data into Database!";
						}
						connection.Close();
						
					}				
				}
				catch (SqlException ex)
				{
					ErrorText.Text = ex.ToString();
				}
			}
		} 
		LoadData();
    }

	public void btnLogTE_Click(Object sender, EventArgs e)
	{				
		
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			string intsToClear	= String.Format("{0}", Request.Form["ints_to_clear_ID"]);	
			string teDate		= String.Format("{0}", Request.Form["te_date_ID"]);	
			string teBy		 	= String.Format("{0}", Request.Form["te_by_ID"]);	
			string note 	 	= String.Format("{0}", Request.Form["te_note_ID"]);	

			string orgid		= "";
			if(OrgText.Text=="Day"){
				orgid 	 = System.Configuration.ConfigurationManager.AppSettings["orgid_day"];
			} else {
				orgid 	 = System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"];
			}
			
			using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			{
				String query = "INSERT into pc_tech_estimate(pc_id, ints_to_clear, te_date, te_by, note, org, org_id, addo_id, date_created, date_modified) "
							 + "VALUES (@pc_id, @ints_to_clear, @te_date, @te_by, @note, @org, @org_id, @addo_id, getdate(), getdate())";

				try
				{		
					using(SqlCommand command = new SqlCommand(query, connection))
					{

						command.Parameters.AddWithValue("@pc_id",				vPcId);
						command.Parameters.AddWithValue("@ints_to_clear",		intsToClear);
						if (teDate != ""){ 
							command.Parameters.AddWithValue("@te_date", 		teDate);
						} else { 
							command.Parameters.AddWithValue("@te_date",		 	DBNull.Value); 
						}						
						command.Parameters.AddWithValue("@te_by",				teBy);
						command.Parameters.AddWithValue("@note",				note);
						command.Parameters.AddWithValue("@addo_id",				vAddoId);
						command.Parameters.AddWithValue("@org_id",				orgid);
						command.Parameters.AddWithValue("@org",					OrgText.Text);

						connection.Open();

						int result = command.ExecuteNonQuery();

						ClearTEModal();
						
						if(result < 0){
							ErrorText.Text = "Error inserting data into Database!";
						}
						connection.Close();
						
					}				
				}
				catch (SqlException ex)
				{
					ErrorText.Text = ex.ToString();
				}
			}
		} 
		LoadData();
    }
	
	protected void OpenAddIntensive(object sender, EventArgs e)
	{
		ClearModal();
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openIntensive();", true);
		// addonameid.Text = txtAddoSearch.Text;
		inv_nbr_ID.Focus();
	}
	
	public void ClearModal()
	{

		inv_nbr_ID.Text = "";
		date_started_ID.Text = "";
		we_started_ID.Text = "";
		we_completed_ID.Text = "";
		vsd_value_ID.Text = "";
		vsd_counted_ID.Text = "";
		note_ID.Text = "";
		addo_id_ID.Text = "";
		
	} 

	public void ClearTEModal()
	{

		ints_to_clear_ID.Text = "";
		te_by_ID.Text = "";
		te_note_ID.Text = "";
		
	} 

	protected void OpenLogTechEstimate(object sender, EventArgs e)
	{
		ClearModal();
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openTechEstimate();", true);
	}
	
	protected void DeleteRow(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		// RegistrarID.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteModal();", true);
		
	}

	protected void DeleteEvent(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		deleteID.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteEvent();", true);
		
	}
	
	protected void DeleteTE(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		teID.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteTE();", true);
		
	}

	protected void ArchiveEvent(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		archiveID.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmArchiveEvent();", true);
		
	}

	public void btnDeleteEvent_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["deleteID"]);	
			string sqlCommandStatement = String.Format("DELETE FROM pc_intensives WHERE id='{0}'", id );	
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
			LoadData(); 		
		} 
    }
	
	public void btnDeleteTE_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["teID"]);	
			string sqlCommandStatement = String.Format("DELETE FROM pc_tech_estimate WHERE id='{0}'", id );	
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
			LoadData(); 		
		} 
    }

	public void btnArchiveEvent_Click(Object sender, EventArgs e)
	{				
		// Button clickedButton = (Button)sender;
		// if ( clickedButton != null)
		// {
			// string id = String.Format("{0}", 		Request.Form["archiveID"]);	
			// string sqlCommandStatement = String.Format("UPDATE event SET active = 'N' WHERE id='{0}'", id );	
			// try
			// {		
				// using (SqlConnection conn = databaseConnection.CreateSqlConnection())
				// {
				// conn.Open();
				// using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement, conn))
				// {
					// Cmd.ExecuteNonQuery();						
				// }
				// conn.Close();
				// }
			// }
			// catch (SqlException ex)
			// {
				// ErrorText.Text = ex.ToString();
			// }

			// UPDATE THE RECORDS IN THE BIS TABLE 
			// string sqlCommandStatement2 = String.Format("update bis set reg_cat_id = 'Archive' where service in (select event_desc from event where id='{0}')", id );	
			// try
			// {		
				// using (SqlConnection conn = databaseConnection.CreateSqlConnection())
				// {
				// conn.Open();
				// using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement2, conn))
				// {
					// Cmd.ExecuteNonQuery();						
				// }
				// conn.Close();
				// }
			// }
			// catch (SqlException ex)
			// {
				// ErrorText.Text = ex.ToString();
			// }
			// GridView_Load(GridViewIntensive, PDAL.getAuditors(OrgText.Text)); 		
		// } 
    }

	protected void ViewArchiveEvent(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		archiveID.Text = vId; 
		
		// ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmArchiveEvent();", true);
		
	}

 	protected void text_change_auditor(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE pc_intensives SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		// ErrorText.Text = text.Text + " was changed";
	}

	protected void Selection_Change_Org_Auditor(object sender, EventArgs e)
	{
		DropDownList ddlOrg = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE pc_intensives SET org = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlOrg.Text);		
	}

    protected void Selection_Change_Status(object sender, EventArgs e)
    {
        DropDownList ddlStatus = sender as DropDownList;
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        string id = gvRow.Cells[0].Text;
        string sqlCommandStatement = "UPDATE pc_intensives SET status = @TEXT WHERE id=@ID";
        SqlCmd(sqlCommandStatement, id, ddlStatus.Text);	

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
		GridView_Load(GridViewIntensive, PDAL.getAuditors(OrgText.Text)); 		
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
