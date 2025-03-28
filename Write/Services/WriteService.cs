using Microsoft.Data.SqlClient;
using TFMessage.database;
using TFMessage.models;

namespace TFMessage.write.services;

public interface IWriteService
{
  Task<bool> CreatePost(Message post);
}

public partial class WriteService(WriteDataContext wcontext, FileManagerService fileManager) :IWriteService
{

} 

public partial class WriteService 
{
  public async Task<bool> CreatePost(Message post)
  {
    try
    {
      using SqlConnection conn = wcontext.CreateConnection();
      string query = $@"
        INSERT INTO [Messages](id,content,author)
        VALUES(@id,@content,@author)
        ";

      using SqlCommand cmd = new(query, conn);
      cmd.Parameters.AddWithValue("@id",post.Id);
      cmd.Parameters.AddWithValue("@content",post.Content);
      cmd.Parameters.AddWithValue("@author",post.Author);
      conn.Open();

      int result = cmd.ExecuteNonQuery();
      if (result < 1)
      {
        return false;
      }

      await fileManager.SavePost(post);
      return true;
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      return false;
    }
  }
}
