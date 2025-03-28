using TFMessage.models;
namespace TFMessage.write.services;

public class FileManagerService(IConfiguration configuration) 
{
  const string SHARED_FILE_FOLDER = "SHARED_FILE_FOLDER"; 
  string _sharedFilePath = configuration[SHARED_FILE_FOLDER] ?? throw new Exception($"Missing configuration: {SHARED_FILE_FOLDER}");

  public async Task SavePost(Message message)
  {
    string path = $"/{_sharedFilePath}/{GenerateFileName()}.txt";
    File.Create(path).Close();
    await File.AppendAllTextAsync(path, message.ToString());
  }

  private string GenerateFileName(){
    return Guid.NewGuid().ToString();
  }
}
