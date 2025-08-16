using Microsoft.Data.SqlClient;

namespace TheFirstProject.DBConnection;

public class IConnector
{
    public static string? ConnectionString { get; set; }

    public IConnector(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public SqlConnection GetConnection()
    {
        try
        {
            return new SqlConnection(ConnectionString);        
        } catch (Exception ex)
        {
            throw new Exception("Could not connect to the database", ex);
        }
    }
}
