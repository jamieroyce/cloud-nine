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
		Title = "Setup Preclear";
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
			OrgText.Text = "Day";
			day.Attributes["class"] += " active";		
			fdn.Attributes.Add("class", fdn.Attributes["class"].ToString().Replace("active", ""));		      
			cmb.Attributes.Add("class", cmb.Attributes["class"].ToString().Replace("active", ""));		   
		}
		
        if (ViewState["SortExpression"] != null)
			ViewState["SortExpression"] = null;
		
		GridView_Load(GridViewPC, PDAL.getPreclears(OrgText.Text)); 		
		
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
		GridView_Load(GridViewPC, PDAL.getPreclears(OrgText.Text)); 		
        
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
		GridView_Load(GridViewPC, PDAL.getPreclears(OrgText.Text)); 		
        
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
	
	public void btnAddEvent_Click(Object sender, EventArgs e)
	{				
		
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{

			// string eventName = String.Format("{0}", Request.Form["eventname"]);	
			// string eventDesc = String.Format("{0}", Request.Form["eventdesc"]);	
			// string quota	 = String.Format("{0}", Request.Form["quota"]);	
			// string eventDate = String.Format("{0}", Request.Form["eventdate"]);	
			// string eventLoc  = String.Format("{0}", Request.Form["eventloc"]);	
			// string startTime = String.Format("{0}", Request.Form["starttime"]);	
			// string endTime 	 = String.Format("{0}", Request.Form["endtime"]);	
			// string note 	 = String.Format("{0}", Request.Form["note"]);	
			// string orgid 	 = System.Configuration.ConfigurationManager.AppSettings["orgid"];
			
			// using(SqlConnection connection = databaseConnection.CreateSqlConnection())
			// {
				// String query = "INSERT into event(name, event_desc, event_location, attendance_quota, event_date, event_start, event_end, notes, org_id, date_created, date_modified, active) "
							 // + "VALUES (@name, @event_desc, @event_location, @attendance_quota, @event_date, @event_start, @event_end, @notes, @org_id, getdate(), getdate(), 'Y')";

				// try
				// {		
					// using(SqlCommand command = new SqlCommand(query, connection))
					// {

						// command.Parameters.AddWithValue("@name",				eventName);
						// command.Parameters.AddWithValue("@event_desc",			eventDesc);
						// command.Parameters.AddWithValue("@event_location",		eventLoc);
						// command.Parameters.AddWithValue("@attendance_quota",	quota);
						// command.Parameters.AddWithValue("@event_date",			eventDate);
						// command.Parameters.AddWithValue("@event_start",			startTime);
						// command.Parameters.AddWithValue("@event_end",			endTime);
						// command.Parameters.AddWithValue("@notes",				note);
						// command.Parameters.AddWithValue("@org_id",				orgid);

						// connection.Open();

						// int result = command.ExecuteNonQuery();

						// ClearAddEventModal();
						
						// if(result < 0){
							// ErrorText.Text = "Error inserting data into Database!";
						// }
						// connection.Close();
						
					// }				
				// }
				// catch (SqlException ex)
				// {
					// ErrorText.Text = ex.ToString();
				// }
			// }
		} 

		GridView_Load(GridViewPC, PDAL.getPreclears(OrgText.Text)); 

    }

	public void ClearAddEventModal()
	{

		// eventname.Text = "";
		// eventdate.Text = "";
		// eventloc.Text = "";
		// starttime.Text = "";
		// eventdesc.Text = "";
		// endtime.Text = "";
		// note.Text = "";
		// quota.Text = "";
		
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
		eventID.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmDeleteEvent();", true);
		
	}
	
	protected void ArchiveEvent(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		eventID.Text = vId; 
		
		ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmArchiveEvent();", true);
		
	}

	public void btnDeleteEvent_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["eventID"]);	
			string sqlCommandStatement = String.Format("DELETE FROM event WHERE id='{0}'", id );	
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

			GridView_Load(GridViewPC, PDAL.getPreclears(OrgText.Text)); 		
		} 
    }
	
	public void btnArchiveEvent_Click(Object sender, EventArgs e)
	{				
		Button clickedButton = (Button)sender;
		if ( clickedButton != null)
		{
			string id = String.Format("{0}", 		Request.Form["eventID"]);	
			string sqlCommandStatement = String.Format("UPDATE event SET active = 'N' WHERE id='{0}'", id );	
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

			//UPDATE THE RECORDS IN THE BIS TABLE 
			string sqlCommandStatement2 = String.Format("update bis set reg_cat_id = 'Archive' where service in (select event_desc from event where id='{0}')", id );	
			try
			{		
				using (SqlConnection conn = databaseConnection.CreateSqlConnection())
				{
				conn.Open();
				using (SqlCommand Cmd = new SqlCommand(sqlCommandStatement2, conn))
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
			GridView_Load(GridViewPC, PDAL.getPreclears(OrgText.Text)); 		
		} 
    }

	protected void ViewArchiveEvent(object sender, EventArgs e)
	{
		int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
		string vId = row.Cells[0].Text;		
		eventID.Text = vId; 
		
		// ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ConfirmArchiveEvent();", true);
		
	}

	protected void text_change_auditor(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE pc_auditor SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		// ErrorText.Text = text.Text + " was changed";
	}

	protected void text_change_pc(object sender, EventArgs e)
	{
		ErrorText.Text = "";
		TextBox text = sender as TextBox;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement =  string.Format("UPDATE pc SET {0} = @TEXT WHERE id=@ID", text.ID);
		SqlCmd(sqlCommandStatement, id, text.Text);		
		// ErrorText.Text = text.Text + " was changed";
	}

	protected void Selection_Change_Org_PC(object sender, EventArgs e)
	{
		DropDownList ddlOrg = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE pc SET org = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlOrg.Text);		
	}
	
	protected void Selection_Change_Org_Auditor(object sender, EventArgs e)
	{
		DropDownList ddlOrg = sender as DropDownList;
		GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
		string id = gvRow.Cells[0].Text;
		string sqlCommandStatement = "UPDATE pc_auditor SET org = @TEXT WHERE id=@ID";
		SqlCmd(sqlCommandStatement, id, ddlOrg.Text);		
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
		GridView_Load(GridViewPC, PDAL.getPreclears(OrgText.Text)); 		
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
