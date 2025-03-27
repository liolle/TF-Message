using TFMessage.models;

namespace TFMessage.read.service;

public interface IReadService 
{
  List<Message>GetAll();
}

public partial class ReadService : IReadService
{

}

public partial class ReadService 
{
    public List<Message> GetAll()
    {
      List<Message> messages = [];
      // TODO Read from read replica
      return messages;
    }
}
