using Microsoft.Data.SqlClient;
using TFMessage.database;
using TFMessage.models;

namespace TFMessage.worker.service;

public interface IReplicaService 
{
  void Scan();
}

public partial class ReplicaService(ReadDataContext rcontext, IConfiguration configuration) : IReplicaService
{
}

public partial class ReplicaService 
{
  private bool CreatePost(Message post)
  {
    try
    {
      using SqlConnection conn = rcontext.CreateConnection();
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

      return true;
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      return false;
    }
  }

  private void GroupAdd(List<FileInfo> files)
  {
    foreach (FileInfo file in files)
    {
      Message? m = ExtractMessage(file.Content);
      if (m is not null ){
        if (CreatePost(m)){
          DeleFile(file.Name);
        }
      }
    }
  }

  private Message? ExtractMessage(string content)
  {
    string[] parts =  content.Split(',');
    if(parts[0] is null || parts[0] == ""){return null;};
    if(parts[1] is null || parts[1] == ""){return null;};
    if(parts[2] is null || parts[2] == ""){return null;};
    Console.WriteLine(parts[0],parts[1],parts[2]);
    return new Message(parts[0],parts[1],parts[2]);
  }

  private void DeleFile(string filename){
    string directory = configuration["SHARED_FILE_FOLDER"] ?? throw new Exception($"Missing configuration SHARED_FILE_FOLDER");
    string file_path = $"/{directory}/{filename}";
    File.Delete(file_path);
  }

  public void Scan()
  {
    string directory = configuration["SHARED_FILE_FOLDER"] ?? throw new Exception($"Missing configuration SHARED_FILE_FOLDER");
    List<FileInfo> files = [];

    var textFiles = Directory.EnumerateFiles($"/{directory}", "*.txt")
      .Select(filePath => new FileInfo(){
          Name = Path.GetFileName(filePath),
          Content = File.ReadAllText(filePath)
          });

    GroupAdd(textFiles.ToList());
  }
} 

public struct FileInfo 
{
  public string Name {get;set;}
  public string Content {get;set;}
}
