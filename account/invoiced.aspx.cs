using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;


public partial class _Default : System.Web.UI.Page 
{  	

    dal DAL = new dal();

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

	
	public void StartingPage()
	{
		ErrorText.Text = "";
        DataGrid_Load(DAL.GetUsers(), "reg");
	}
	
	private void DataGrid_Load(DataTable command, string type)
	{	

		DataTable dataTable = new DataTable();
        dataTable = command;

		string sortDir = ViewState["SortDirection"] as string;
		string sortExp = ViewState["SortExpression"] as string;

		DataRow[] dr = dataTable.Select("name = 'Test'");
		DataTable filteredDataTable = dataTable.Clone();
		foreach (DataRow sourceRow in dr)
		{
		   filteredDataTable.ImportRow(sourceRow);  // or add all fields manually
		}

		GridViewInvoice.DataSource = filteredDataTable;				
		GridViewInvoice.DataBind();

		int a = 0;
		int x = 0;

		var test = dataTable.AsEnumerable().Select(row => row.Field<int?>("amount") != null ? row.Field<int?>("amount") : 0);		
		foreach (int item in test)
			a += item;
		
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
