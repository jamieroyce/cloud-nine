using System;

public class regTool : System.Web.UI.Page
{

    dal DAL = new dal();

    private void GridView_Load(GridView grdview, DataTable dt)
    {

        string sortDir = ViewState["SortDirection"] as string;
        string sortExp = ViewState["SortExpression"] as string;

        if (ViewState["SortExpression"] != null)
        {
            dt = resort(dt, sortExp, sortDir);
        }

        grdview.DataSource = dt;
        grdview.DataBind();

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
                    if (id != null && text == null)
                    {
                        Cmd.Parameters.Add("@ID", SqlDbType.Int);
                        Cmd.Parameters["@ID"].Value = Int32.Parse(id);
                        Cmd.ExecuteNonQuery();
                    }
                    else if (text == "ddlLineNull")
                    {
                        Cmd.Parameters.Add("@ID", SqlDbType.Int);
                        Cmd.Parameters.Add("@TEXT", SqlDbType.VarChar);
                        Cmd.Parameters["@ID"].Value = Int32.Parse(id);
                        Cmd.Parameters["@TEXT"].Value = DBNull.Value;
                        Cmd.ExecuteNonQuery();
                    }
                    else if (text == "Addo_ID_Null")
                    {
                        Cmd.Parameters.Add("@ID", SqlDbType.Int);
                        Cmd.Parameters.Add("@TEXT", SqlDbType.VarChar);
                        Cmd.Parameters["@ID"].Value = Int32.Parse(id);
                        Cmd.Parameters["@TEXT"].Value = DBNull.Value;
                        Cmd.ExecuteNonQuery();
                    }
                    else if (id != null && text != null && text != "ddlLineNull")
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
