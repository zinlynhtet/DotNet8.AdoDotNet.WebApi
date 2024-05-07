using System.Data.SqlClient;

namespace DotNet8.AdoDotNet.WebApi;

internal static class SqlConnectionString
{
    public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder
    {
        DataSource = ".",
        InitialCatalog = "TestDb",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };
}