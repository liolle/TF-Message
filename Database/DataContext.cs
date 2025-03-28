using System.Data;
using Microsoft.Data.SqlClient;
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
      Console.WriteLine(_connectionString);
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

public class ReadDataContext(string? connectionString) : DataContext(GetConnectionString(connectionString))
{
  private static string GetConnectionString(string? connectionString)
  {
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new Exception($"Missing configuration: DB_READ_CONNECTION_STRING");
    }
    return connectionString;
  }
}

public class WriteDataContext(string? connectionString) : DataContext(GetConnectionString(connectionString)){
  private static string GetConnectionString(string? connectionString)
  {
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new Exception($"Missing configuration: DB_WRITE_CONNECTION_STRING");
    }
    return connectionString;
  }
}
