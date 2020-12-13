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
		}		
		
	}
	catch{}
	}

	public void StartingPage()
	{
		searchText = "";
		searchCol = "";
		searchWE = "";
	}


	public string GetHoursAndMinutes(int minutes){

		int Minute = 0;
		int Hour = 0;
		string formattedHours = ""; 

		{
			Hour = minutes / 60;
			Minute = minutes % 60;
			formattedHours = FormatTwoDigits(Hour) + " : " + FormatTwoDigits(Minute) + " ";
		}

		return formattedHours; 
	}

	public void clickTest(Object sender, EventArgs e){

		int totMins = 1233;
		
		string totTime = "";
		totTime = GetHoursAndMinutes(totMins);
		
		ErrorText.Text += "WDAH = " + totTime + " <BR /> ";
	}

	private string FormatTwoDigits (int i)
	{
		string functionReturnValue = null;
		if (10 > i)
		{
			functionReturnValue = "0" + i.ToString();
		}
		else
		{
			functionReturnValue = i.ToString();
		}
		return functionReturnValue;
	}	
		
	public static DateTime GetNextThursday ( DateTime time )
	{

		TimeSpan twopm = new TimeSpan(12, 0, 0); //2 o'clock
		TimeSpan now = time.TimeOfDay;

		if (time.DayOfWeek != DayOfWeek.Thursday){
			return time.Subtract(new TimeSpan((int)time.DayOfWeek - 4, 0, 0, 0));
		} else if (now > twopm){
			return time.AddDays(7);
		} else {
			return time;
		}

	}	
	
	public static DateTime GetLastThursday ( DateTime time )
	{
	   if (time.DayOfWeek != DayOfWeek.Thursday)
		  return time.Add(new TimeSpan((int)time.DayOfWeek - 3, 0, 0, 0));

	   return time;
	}	
}
