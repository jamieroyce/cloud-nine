using System.Data.SqlClient;

public class databaseConnection
{
    public static SqlConnection CreateSqlConnection()
    {
        string connectionString;

        connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["reg"].ConnectionString;

        return new SqlConnection(connectionString);
    }
}