using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace TFMessage.database;

public interface IDataContext 
{
  SqlConnection CreateConnection();
}

public abstract class DataContext(string connectionString) : IDataContext
{
    private readonly string _connectionString = connectionString; 

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public int ExecuteNonQuery(string query, SqlParameter[] parameters)
    {
        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = new(query, conn);
        cmd.Parameters.AddRange(parameters);
        conn.Open();
        return cmd.ExecuteNonQuery(); 
    }

    public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
    {
        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = new(query, conn);
        cmd.Parameters.AddRange(parameters);
        conn.Open();

        using SqlDataAdapter adapter = new(cmd);
        DataTable resultTable = new();
        adapter.Fill(resultTable);
        return resultTable;
    }
}

public class ReadDataContext(IConfiguration configuration) : DataContext(GetConnectionString(configuration, "DB_READ_CONNECTION_STRING"))
{
    private static string GetConnectionString(IConfiguration config, string configKey)
    {
        string? connectionString = config[configKey];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception($"Missing configuration: {configKey}");
        }
        return connectionString;
    }
}

public class WriteDataContext(IConfiguration configuration) : DataContext(GetConnectionString(configuration, "DB_WRITE_CONNECTION_STRING"))
{
    private static string GetConnectionString(IConfiguration config, string configKey)
    {
        string? connectionString = config[configKey];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception($"Missing configuration: {configKey}");
        }
        return connectionString;
    }
}
