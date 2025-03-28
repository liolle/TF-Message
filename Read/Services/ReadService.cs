using Microsoft.Data.SqlClient;
using TFMessage.database;
using TFMessage.models;

namespace TFMessage.read.service;

public interface IReadService 
{
  List<Message>GetAll();
}

public partial class ReadService(ReadDataContext rContext) : IReadService
{

}

public partial class ReadService 
{
  public List<Message> GetAll()
  {
    try
    {
      using SqlConnection conn = rContext.CreateConnection();
      List<Message> messages = [];

      string sql_query = $@"
        SELECT 
        *
        FROM [Messages] 
        ";

      using SqlCommand cmd = new(sql_query, conn);
      conn.Open();
      using SqlDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        Message m = new(
            (string)reader["id"],
            (string)reader["author"],
            (string)reader["content"]
            );
        messages.Add(m);
      }

      return messages; 
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      return []; 
    }
  }
}
