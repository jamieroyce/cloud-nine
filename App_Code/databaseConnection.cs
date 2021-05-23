using System.Data.SqlClient;

public class databaseConnection
{
    public static SqlConnection CreateSqlConnection()
    {
        string connectionString;

//        connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["reg"].ConnectionString;

        connectionString = "Data Source=D5DRDRQ2\\SQLEXPRESS;initial catalog=dataManager;packet size=4096;integrated security=SSPI;User ID=dataManager; Password=p@ssw0rdm@n@g3r1; initial catalog=dataManager; persist security info=False;MultipleActiveResultSets=True";
//        connectionString = "workstation id=" + System.Environment.MachineName;
//        connectionString += ";packet size=4096;integrated security=SSPI;";
//        connectionString += "data source=";

        //   Use the name of the SQL Server instance hosting the DEMO database
        //   as the data source value in connectionString . . .

        //        connectionString += "D5DRDRQ2\\SQLEXPRESS"; //  <-- The name of the SQL Server instance . . .
//        connectionString += ";persist security info=False;initial catalog=DEMO;MultipleActiveResultSets=True";
        return new SqlConnection(connectionString);
    }
}