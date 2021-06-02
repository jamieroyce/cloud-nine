using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;

public partial class _Default : System.Web.UI.Page 
{  	
    dal DAL = new dal();
	static string graphWeeks = "12";
	static string fromWE;
	static string toWE;


    public void BtnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("account/dashboard.aspx");

        //DAL.Search_Addo(searchText, searchCol), "reg");
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
