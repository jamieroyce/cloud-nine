using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

/// <summary>
///	Author:			Frank

///	Create date:	8-20-13
///	Edit date:		9-09-13

///	Description:	Class to return a stored procedure result set as a dataset . . .

///	Input:          SPName()        <-- stored procedure name as class property
///                 SPPrms()        <-- stored procedure parameters as list of
///                                     SqlParameters class property; possibly
///                                     empty
///
///   Output:       executeSP()     <-- finished result set as .Net DataTable; exposed
///                                     as an object method call
/// </summary>

namespace CSharpDemoWebsite
{
    public class parentDBObject
    {
        private SqlConnection local_conn = databaseConnection.CreateSqlConnection();

        private string _SPName = "";
        private List<SqlParameter> _prmList = new List<SqlParameter>();

        //  Public get-set exposing the private stored procedure name object field . . .

        public string SPName
        {
            get { return _SPName; }
            set { _SPName = value; }
        }

        public List<SqlParameter> SPPrms
        {
            get { return _prmList; }
            set { _prmList = value; }
        }

        public parentDBObject()
        {
            //   Basic constructor . . .
        }

        public virtual DataTable executeSP()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            //  Make sure that the object has a stored procedure name . . . . . .

            if (String.IsNullOrEmpty(this.SPName))
            {
                return null;
            }

            //   Set up the command object, and tie the called stored
            //   procedure to the SPName object property . . .

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = SPName;

            //   The SPPrms object property has all the stored procedure
            //   parameters in a list object, so extract them and add
            //   them to the command object parameters property . . .

            foreach (SqlParameter localPrm in SPPrms)
            {
                cmd.Parameters.Add(localPrm);
            }

            //   Set up the command object connection property . . .

            cmd.Connection = this.local_conn;

            //   Use try-catch to run the command object (AKA the
            //   stored procedure) and fill the data set . . .

            try
            {
                this.local_conn.Open();
                dt.Clear();
                da.SelectCommand = cmd;

                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Page localAlert = HttpContext.Current.CurrentHandler as Page;
                localAlert.ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", string.Format("<script type='text/javascript'>alert('{0}')</script>", ex.ToString()));
            }

            //   Clear out the existing parameter collection - each time
            //   a command object runs, it wants a new parameter
            //   collection. Therefore, throw out the existing
            //   parameters collection after command object
            //   execution . . .

            cmd.Parameters.Clear();

            //   Close the connection property ASAP . . .

            this.local_conn.Close();

            //   Count the rows in the data table; if it has
            //   at least one row, return that row, otherwise
            //   return Nothing . . .

            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
    }
}