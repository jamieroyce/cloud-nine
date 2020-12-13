using System;

/// <summary>
/// Class childDBObject derives from class parentDBObject
/// </summary>

namespace CSharpDemoWebsite
{
    public class childDBObject : parentDBObject
    {
        private DateTime _dateTimeStamp;

        //   Public get-set exposing the private date/time stamp field . . .

        public DateTime dateTimeStamp
        {
            get { return _dateTimeStamp; }
            set { _dateTimeStamp = value; }
        }

        public override DataTable executeSP()
        {
            DataTable localDataTable;
            String localRowCount;
            String localSPName;

            localDataTable = base.executeSP();

            if (!(localDataTable == null))
            {
                localRowCount = localDataTable.Rows.Count.ToString();
                localSPName = base.SPName;
                string msgString;

                //  The message box shows that the call to
                //  the derived class method actually
                //  worked. For each hard return in the
                //  popup, use "\n" and remember that C# expects
                //  me to escape the backslash . . .

                msgString = "Stored procedure\\n\\n" + localSPName + "\\n\\n";
                msgString += "executed\\n\\n" + this.dateTimeStamp + "\\n\\n";
                msgString += "and returned " + localRowCount + " row(s).";

                Page localAlert = HttpContext.Current.CurrentHandler as Page;
                localAlert.ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", string.Format("<script type='text/javascript'>alert('{0}')</script>", msgString));

                //  Return the finished datatable as the return value . . .

                return localDataTable;
            }
            else
            {
                return null;
            }
        }
    }
}